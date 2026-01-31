using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace UrlShortner.Helper
{
    public static class Hash
    {
        public static string Encode(string input)
        {
            string customChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const short SHORT_CODE_LENGTH = 6;
            string uuid = Guid.NewGuid().ToString();
            string updatedUrl = uuid + customChars;

            using (SHA256 algo = SHA256.Create())
            {
                byte[] bytes = algo.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < SHORT_CODE_LENGTH; i++)
                {
                    int index = bytes[i] % updatedUrl.Length;
                    builder.Append(updatedUrl[index]);
                }

                return builder.ToString();
            }
        }
    }
}