using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace UrlShortner.Helper
{
    public static class Hash
    {
        public static string Encode(string input)
        {
            string CustomChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const short SHORT_CODE_LENGTH = 6;

            using (SHA256 algo = SHA256.Create())
            {
                byte[] bytes = algo.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < SHORT_CODE_LENGTH; i++)
                {
                    int index = bytes[i] % CustomChars.Length;
                    builder.Append(CustomChars[index]);
                }

                return builder.ToString();
            }
        }
    }
}