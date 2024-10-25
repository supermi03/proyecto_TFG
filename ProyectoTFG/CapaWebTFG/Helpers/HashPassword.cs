using System;
using System.Security.Cryptography;

public class PasswordHelper
{
    // Método para hashear la contraseña
    public static string HashPassword(string password)
    {
        // Generar un salt único
        using (var hmac = new HMACSHA256())
        {
            byte[] salt = hmac.Key; // Usamos la clave como salt

            // Hashear la contraseña
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(32); // 32 bytes para el hash

            // Concatenar el salt y el hash para almacenarlos
            byte[] hashBytes = new byte[64];
            Array.Copy(salt, 0, hashBytes, 0, 32);
            Array.Copy(hash, 0, hashBytes, 32, 32);

            // Convertir a base64 para almacenar en la base de datos
            return Convert.ToBase64String(hashBytes);
        }
    }

    // Método para verificar la contraseña ingresada
    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        // Convertir el hash almacenado de base64 a bytes
        byte[] hashBytes = Convert.FromBase64String(storedHash);

        // Extraer el salt de los primeros 32 bytes
        byte[] salt = new byte[32];
        Array.Copy(hashBytes, 0, salt, 0, 32);

        // Hashear la contraseña ingresada usando el mismo salt
        var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(32);

        // Comparar el hash calculado con el almacenado
        for (int i = 0; i < 32; i++)
        {
            if (hashBytes[i + 32] != hash[i])
            {
                return false; // La contraseña no coincide
            }
        }

        return true; // La contraseña coincide
    }
}

