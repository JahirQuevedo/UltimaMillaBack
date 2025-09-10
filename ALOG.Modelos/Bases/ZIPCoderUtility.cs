using ICSharpCode.SharpZipLib.Zip;
using System.IO.Compression;

namespace ALOG.Modelos;

public sealed class ZIPCoderUtility
{
    private static int numFileZip = 0;
    private static object versionadorSIR = new object();

    /// <summary>ZipDatabaseFile: Comprime un zip</summary>
    /// <param name="rutaOrigen">Path de los archivos a zipear</param>
    /// <param name="rutaDestino">Path destino donde se colocará el zip</param>
    public static void Zip(string rutaOrigen, string rutaDestino)
    {
        FileStream fileStream1 = new FileStream(rutaOrigen, FileMode.Open, FileAccess.Read, FileShare.Read);
        byte[] buffer = new byte[fileStream1.Length];
        fileStream1.Read(buffer, 0, buffer.Length);
        FileStream fileStream2 = new FileStream(rutaDestino, FileMode.OpenOrCreate, FileAccess.Write);
        GZipStream gzipStream = new GZipStream((Stream)fileStream2, CompressionMode.Compress, true);
        gzipStream.Write(buffer, 0, buffer.Length);
        fileStream1.Close();
        gzipStream.Close();
        fileStream2.Close();
    }

    /// <summary>Genera un Archivo Zip para varios Archivos.</summary>
    /// <param name="solutionFilePath">Ruta de el Archivo ZIP</param>
    /// <param name="filePaths">Lista de Paths o Ubicaciones de los Archivos a Comprimir</param>
    /// <param name="relativePath">Indica si las Rutas son relativas o no</param>
    public static void GenerateZip(string solutionFilePath, string[] filePaths, bool relativePath)
    {
        ZIPCoderUtility.CreateZipFile(solutionFilePath, filePaths, relativePath);
    }

    /// <summary>Genera un Archivo Zip para varios Archivos.</summary>
    /// <param name="solutionFilePath">Ruta de el Archivo ZIP</param>
    /// <param name="filePaths">Lista de Paths o Ubicaciones de los Archivos a Comprimir</param>
    /// <param name="relativePath">Indica si las Rutas son relativas o no</param>
    /// <param name="includeOriginalPath"></param>
    public static void GenerateZip(
        string solutionFilePath,
        string[] filePaths,
        bool relativePath,
        bool includeOriginalPath)
    {
        ZIPCoderUtility.CreateZipFile(solutionFilePath, filePaths, relativePath, includeOriginalPath);
    }

    /// <summary>Genera un Archivo Zip para de un Archivo.</summary>
    /// <param name="filePath">Ruta del archivo a Comprimir</param>
    /// <param name="includeFileName"></param>
    /// <returns></returns>
    private static string ZipEntry(string filePath, bool includeFileName)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
        string str = !(directoryInfo.Root.FullName == directoryInfo.Root.Name) ? directoryInfo.Parent.FullName.Substring(directoryInfo.Parent.FullName.IndexOf(directoryInfo.Root.Name)) : (directoryInfo.Parent == null ? "" : directoryInfo.Parent.FullName.Replace(directoryInfo.Root.FullName, ""));
        if (!str.EndsWith("\\") && str.Length > 0)
            str += "\\";
        if (includeFileName)
            str += directoryInfo.Name;
        return str;
    }

    /// <summary>Genera un Archivo Zip para de un Archivo.</summary>
    /// <param name="filePath">Ruta del archivo a Comprimir</param>
    /// <returns></returns>
    private static string ZipEntry(string filePath) => "" + new DirectoryInfo(filePath).Name;

    /// <summary>Genera un Archivo Zip para una lista Archivos.</summary>
    /// <param name="filePath">ruta de archivo</param>
    /// <param name="fullZipEntry">ruta del zip</param>
    /// <param name="rootZipPath">ruta base del zip</param>
    /// <param name="fullRootPath">ruta completa</param>
    /// <returns></returns>
    private static string RootZipEntry(
        string filePath,
        string fullZipEntry,
        ref string rootZipPath,
        ref string fullRootPath)
    {
        string str = "";
        if (fullZipEntry == null)
            fullZipEntry = ZIPCoderUtility.ZipEntry(filePath);
        if (filePath != null && filePath != "")
        {
            string oldValue = ZIPCoderUtility.ZipEntry(filePath);
            if (oldValue != fullZipEntry && !oldValue.EndsWith("\\"))
                oldValue += "\\";
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            if (rootZipPath.IndexOf(oldValue) == -1 && directoryInfo.Parent != null)
            {
                fullRootPath = directoryInfo.Parent.FullName;
                str = ZIPCoderUtility.RootZipEntry(fullRootPath, fullZipEntry, ref rootZipPath, ref fullRootPath);
            }
            else
            {
                rootZipPath = oldValue;
                str = fullZipEntry.Replace(oldValue, "");
            }
        }
        return str;
    }

    /// <summary>Genera un Archivo Zip para una lista Archivos.</summary>
    /// <param name="initRootPath">ruta raiz</param>
    /// <param name="filePaths">lista de archivos</param>
    /// <returns></returns>
    private static string RootPathFromFiles(string initRootPath, string[] filePaths)
    {
        string str1 = "";
        if (new DirectoryInfo(initRootPath).Exists)
            str1 = ZIPCoderUtility.ZipEntry(initRootPath);
        else if (new FileInfo(initRootPath).Exists)
            str1 = ZIPCoderUtility.ZipEntry(initRootPath);
        foreach (string filePath in filePaths)
        {
            if (filePath != null)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists || fileInfo.Directory.Exists)
                {
                    string str2 = ZIPCoderUtility.ZipEntry(initRootPath);
                    string rootZipPath = str2;
                    string fullRootPath = initRootPath;
                    ZIPCoderUtility.RootZipEntry(filePath, (string)null, ref rootZipPath, ref fullRootPath);
                    if (str2.IndexOf(rootZipPath) == 0 && rootZipPath.Length < str2.Length)
                        return ZIPCoderUtility.RootPathFromFiles(fullRootPath, filePaths);
                }
            }
        }
        return str1;
    }

    /// <summary>Genera un Archivo Zip para varios Archivos.</summary>
    /// <param name="solutionFilePath">Ruta de el Archivo ZIP</param>
    /// <param name="filePaths">Lista de Paths o Ubicaciones de los Archivos a Comprimir</param>
    /// <param name="relativePath">Indica si las Rutas son relativas o no</param>
    private static void CreateZipFile(
        string solutionFilePath,
        string[] filePaths,
        bool relativePath)
    {
        FileStream baseOutputStream = new FileStream(solutionFilePath, FileMode.Create);
        ZipOutputStream zipStream = new ZipOutputStream((Stream)baseOutputStream);
        string str = ZIPCoderUtility.RootPathFromFiles(solutionFilePath, filePaths);
        foreach (string filePath in filePaths)
        {
            if (filePath != null)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    string name;
                    if (relativePath)
                    {
                        string fullRootPath = filePath;
                        name = ZIPCoderUtility.RootZipEntry(filePath, (string)null, ref str, ref fullRootPath);
                    }
                    else
                        name = ZIPCoderUtility.ZipEntry(filePath);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(name);
                    zipStream.PutNextEntry(entry);
                    byte[] buffer = new byte[16384];
                    int count1 = fileStream.Read(buffer, 0, buffer.Length);
                    zipStream.Write(buffer, 0, count1);
                    int count2;
                    for (; (long)count1 < fileStream.Length; count1 += count2)
                    {
                        count2 = fileStream.Read(buffer, 0, buffer.Length);
                        zipStream.Write(buffer, 0, count2);
                    }
                    fileStream.Close();
                }
                else if (fileInfo.Directory.Exists && new DirectoryInfo(filePath).Exists)
                    ZIPCoderUtility.ZipDirectorio(filePath, ref str, relativePath, zipStream);
            }
        }
        zipStream.Finish();
        zipStream.Close();
        baseOutputStream.Close();
    }

    /// <summary>Genera un Archivo Zip para varios Archivos.</summary>
    /// <param name="solutionFilePath">Ruta de el Archivo ZIP</param>
    /// <param name="filePaths">Lista de Paths o Ubicaciones de los Archivos a Comprimir</param>
    /// <param name="relativePath">Indica si las Rutas son relativas o no</param>
    /// <param name="includeOriginalPath"></param>
    private static void CreateZipFile(
        string solutionFilePath,
        string[] filePaths,
        bool relativePath,
        bool includeOriginalPath)
    {
        FileStream baseOutputStream = new FileStream(solutionFilePath, FileMode.Create);
        ZipOutputStream zipStream = new ZipOutputStream((Stream)baseOutputStream);
        string str = ZIPCoderUtility.RootPathFromFiles(solutionFilePath, filePaths);
        foreach (string filePath in filePaths)
        {
            if (filePath != null)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    string name;
                    if (relativePath)
                    {
                        string fullRootPath = filePath;
                        name = ZIPCoderUtility.RootZipEntry(filePath, (string)null, ref str, ref fullRootPath);
                    }
                    else
                        name = ZIPCoderUtility.ZipEntry(filePath);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(name);
                    zipStream.PutNextEntry(entry);
                    byte[] buffer = new byte[16384];
                    int count1 = fileStream.Read(buffer, 0, buffer.Length);
                    zipStream.Write(buffer, 0, count1);
                    int count2;
                    for (; (long)count1 < fileStream.Length; count1 += count2)
                    {
                        count2 = fileStream.Read(buffer, 0, buffer.Length);
                        zipStream.Write(buffer, 0, count2);
                    }
                    fileStream.Close();
                }
                else if (fileInfo.Directory.Exists && new DirectoryInfo(filePath).Exists)
                    ZIPCoderUtility.ZipDirectorio(filePath, ref str, relativePath, zipStream);
            }
        }
        zipStream.Finish();
        zipStream.Close();
        baseOutputStream.Close();
    }

    /// <summary>
    /// ZipDirectorio:Método que crea un archivo Zip para multiples carpetas
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="zipRootPath"></param>
    /// <param name="relativePath"></param>
    /// <param name="zipStream"></param>
    private static void ZipDirectorio(
        string filePath,
        ref string zipRootPath,
        bool relativePath,
        ZipOutputStream zipStream)
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (file.Exists)
                {
                    FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                    string name;
                    if (relativePath)
                    {
                        string fullName = file.FullName;
                        name = ZIPCoderUtility.RootZipEntry(file.FullName, (string)null, ref zipRootPath, ref fullName);
                    }
                    else
                        name = ZIPCoderUtility.ZipEntry(file.FullName);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(name);
                    zipStream.PutNextEntry(entry);
                    byte[] buffer = new byte[16384];
                    int count1 = fileStream.Read(buffer, 0, buffer.Length);
                    zipStream.Write(buffer, 0, count1);
                    int count2;
                    for (; (long)count1 < fileStream.Length; count1 += count2)
                    {
                        count2 = fileStream.Read(buffer, 0, buffer.Length);
                        zipStream.Write(buffer, 0, count2);
                    }
                    fileStream.Close();
                }
                else if (file.Directory.Exists)
                    ZIPCoderUtility.ZipDirectorio(file.FullName, ref zipRootPath, relativePath, zipStream);
            }
            foreach (FileSystemInfo directory in directoryInfo.GetDirectories())
                ZIPCoderUtility.ZipDirectorio(directory.FullName, ref zipRootPath, relativePath, zipStream);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// GetGenerateNumFileZip:Método que obtiene el total de archivos que se van a agregar a la carpeta zip que se desea crear
    /// </summary>
    /// <param name="solutionFilePath">Ruta de el Archivo ZIP</param>
    /// <param name="filePaths">Lista de Paths o Ubicaciones de los Archivos a Comprimir</param>
    /// <param name="versionador">objeto para el cual se delega el progress del zip</param>
    public static void GetGenerateNumFileZip(
        string solutionFilePath,
        string[] filePaths,
        object versionador)
    {
        ZIPCoderUtility.versionadorSIR = versionador;
        ZIPCoderUtility.numFileZip = 0;
        ZIPCoderUtility.GetNumFileZip(solutionFilePath, filePaths);
    }

    /// <summary>
    /// GetNumFileZip:Método que obtiene el total de archivos que se van a agregar a la carpeta zip que se desea crear
    /// </summary>
    /// <param name="solutionFilePath">Ruta de el Archivo ZIP</param>
    /// <param name="filePaths">Lista de Paths o Ubicaciones de los Archivos a Comprimir</param>
    private static void GetNumFileZip(string solutionFilePath, string[] filePaths)
    {
        foreach (string filePath in filePaths)
        {
            if (filePath != null)
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                    ++ZIPCoderUtility.numFileZip;
                else if (fileInfo.Directory.Exists && new DirectoryInfo(filePath).Exists)
                    ZIPCoderUtility.GetNumFileZip(filePath);
            }
        }
    }

    /// <summary>
    /// GetNumFileZip:Método que obtiene el total de archivos que se van a agregar a la carpeta zip que se desea crear recorre cada una de las carpetas
    /// </summary>
    /// <param name="filePath">Ruta de los archivos</param>
    private static void GetNumFileZip(string filePath)
    {
        try
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (file.Exists)
                    ++ZIPCoderUtility.numFileZip;
                else if (file.Directory.Exists)
                    ZIPCoderUtility.GetNumFileZip(file.FullName);
            }
            foreach (FileSystemInfo directory in directoryInfo.GetDirectories())
                ZIPCoderUtility.GetNumFileZip(directory.FullName);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>UnZipDatabaseFile: Descomprime el zip</summary>
    /// <param name="rutaOrigen">Path donde se encuentra el zip</param>
    /// <param name="rutaDestino">Path destino donde se colocarán los archivos de base de datos</param>
    public static void UnZip(string rutaOrigen, string rutaDestino)
    {
        FileStream fileStream1 = new FileStream(rutaOrigen, FileMode.Open, FileAccess.Read, FileShare.Read);
        GZipStream gzipStream = new GZipStream((Stream)fileStream1, CompressionMode.Decompress, true);
        byte[] buffer1 = new byte[4];
        fileStream1.Position = (long)((int)fileStream1.Length - 4);
        fileStream1.Read(buffer1, 0, 4);
        fileStream1.Position = 0L;
        byte[] buffer2 = new byte[BitConverter.ToInt32(buffer1, 0) + 100];
        int offset = 0;
        int count = 0;
        while (true)
        {
            int num = gzipStream.Read(buffer2, offset, 100);
            if (num != 0)
            {
                offset += num;
                count += num;
            }
            else
                break;
        }
        FileStream fileStream2 = new FileStream(rutaDestino, FileMode.Create);
        fileStream2.Write(buffer2, 0, count);
        fileStream1.Close();
        gzipStream.Close();
        fileStream2.Close();
    }
}


