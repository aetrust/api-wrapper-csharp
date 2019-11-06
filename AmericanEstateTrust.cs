using System;
using System.Security.Cryptography;

namespace authutils_csharp
{
    public static class AmericanEstateTrust
    {
        public static AetrustSignature GenerateSignature(string secret, string requestPath, DateTime current, string method = "GET", string body = null)
        {
           
            var currentTimestamp = Math.Floor(
                current.ToUniversalTime()
                .Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                .TotalMilliseconds/1000);

            var accountData = body != null && body.Length > 0 ? body.Replace(" ", "") : "";

            var payload = currentTimestamp + method + requestPath + accountData;

            return new AetrustSignature(Convert.ToBase64String(CreateHMACSHA256(payload, secret)), (long)currentTimestamp);
        }

        public static byte[] CreateHMACSHA256(string message, string secret)
        {
            var encoding = new System.Text.ASCIIEncoding();

            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);

            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                return hmacsha256.ComputeHash(messageBytes);
            }
        }
    }
}
