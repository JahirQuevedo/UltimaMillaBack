using System.Security.Cryptography;
using System.Text;

public static class AesEncryptionHelper
{
    private static readonly string Key = "d9Tz7Lm1Xb3QrP2Avf5Ng8YjRw0Vs6Hp"; // 32 caracteres para AES-256
    private static readonly string Iv = "m4Xb7Vz9Pk2Rs1Qd"; // 16 caracteres para AES

    public static string Encrypt(string plainText)
    {
        //if (string.IsNullOrEmpty(plainText))
        //    throw new ArgumentNullException(nameof(plainText));

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Key);
        aes.IV = Encoding.UTF8.GetBytes(Iv);
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var ms = new MemoryStream();
        using (var cryptoStream = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        using (var sw = new StreamWriter(cryptoStream))
        {
            sw.Write(plainText);
        }

        var base64 = Convert.ToBase64String(ms.ToArray());

        // Convertir a Base64 URL-safe
        return base64.Replace("+", "-").Replace("/", "_").Replace("=", "");
    }

    public static string Decrypt(string cipherText)
    {
        if (string.IsNullOrEmpty(cipherText))
            throw new ArgumentNullException(nameof(cipherText));

        // Revertir a Base64 estándar
        string base64 = cipherText.Replace("-", "+").Replace("_", "/");
        int padding = 4 - (base64.Length % 4);
        if (padding < 4) base64 += new string('=', padding);

        var buffer = Convert.FromBase64String(base64);

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Key);
        aes.IV = Encoding.UTF8.GetBytes(Iv);
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var ms = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using var sr = new StreamReader(cryptoStream);

        return sr.ReadToEnd();
    }
}
