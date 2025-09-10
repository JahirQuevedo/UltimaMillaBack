

using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ALOG.Repositorios.Utilerias
{
    public class UtileriasCifrados
    {
        public UtileriasCifrados()
        {

        }
        #region Funciones
        //public string obtenermd5(string valor)
        //{
        //    MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
        //    byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
        //    data = x.ComputeHash(data);
        //    string resp = "";
        //    for (int i = 0; i < data.Length; i++)
        //        resp += data[i].ToString("x2").ToLower();
        //    return resp;
        //}

        public string ComputeSha256Hash(string rawData)
        {
            // Verificar que la entrada no sea nula
            if (string.IsNullOrEmpty(rawData))
                throw new ArgumentNullException(nameof(rawData));

            // Crear una instancia de SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir la cadena de entrada en un arreglo de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convertir el arreglo de bytes en una cadena de texto hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString(); // Retorna el hash como una cadena hexadecimal
            }
        }

        #region HASHSALT
        public string ComputeSha256HashWithSalt(string password, out string salt)
        {
            // Generar un salt aleatorio
            salt = Convert.ToBase64String(GenerateRandomBytes());

            // Concatenar el salt a la contraseña
            string saltedPassword = salt + password;

            // Hashear la contraseña con el salt
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash, string salt)
        {
            // Concatenar el salt proporcionado con la contraseña ingresada
            string saltedPassword = salt + enteredPassword;

            // Calcular el hash de la contraseña con el salt
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                // Comparar el hash calculado con el hash almacenado
                return builder.ToString() == storedHash;
            }
        }


        // Método para generar un arreglo de bytes aleatorios
        public static byte[] GenerateRandomBytes(int size = 16)
        {
            byte[] randomBytes = new byte[size];
            using (System.Security.Cryptography.RandomNumberGenerator rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes); // Rellena el arreglo con bytes aleatorios
            }
            return randomBytes;
        }
        #endregion HASHSALT

        #region VALIDACONTRASEÑA
        public bool ValidarContraseña(string contraseña)
        {
            // Verifica que la contraseña tenga al menos 10 caracteres
            if (contraseña.Length < 10)
            {
                return false;
            }
            // Verifica que la contraseña contenga al menos una letra mayúscula, una minúscula y un número
            if (!Regex.IsMatch(contraseña, @"[A-Z]"))
            {
                return false;
            }
            if (!Regex.IsMatch(contraseña, @"[a-z]"))
            {
                return false;
            }
            if (!Regex.IsMatch(contraseña, @"\d"))
            {
                return false;
            }
            return true;
        }
        #endregion VALIDACONTRASEÑA

        #endregion Funciones
    }
}
