using System.Security.Cryptography;

namespace TodoList.Tools
{
    public static class Outils
    {
        public static string GenerateCode8()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var data = new byte[8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(data);

            var result = new char[8];
            for (int i = 0; i < 8; i++)
            {
                result[i] = chars[data[i] % chars.Length];
            }

            return new string(result);
        }
    }
}
