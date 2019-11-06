using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace authutils_csharp
{
    public class AetrustHttpClient
    {
        private readonly HttpClient _client;
        private readonly string _secret;
        private readonly string _apiKey;
        private readonly string _origin;

        public AetrustHttpClient(HttpClient client, string secret, string apiKey, string origin)
        {
            _client = client;
            _secret = secret;
            _apiKey = apiKey;
            _origin = origin;
        }

        public virtual HttpRequestMessage CreateRequest(string url, HttpMethod method, string body = null, DateTime? serverCurrent = null)
        {
            var message = new HttpRequestMessage
            {
                RequestUri = new System.Uri(url),
                Method = method
            };

            var methodAsString = method.Method.ToUpper();
            var signature = AmericanEstateTrust.GenerateSignature(_secret, message.RequestUri.AbsolutePath, serverCurrent ?? DateTime.UtcNow, methodAsString, body);

            message.Headers.Add("Signature", signature.Signature);
            message.Headers.TryAddWithoutValidation("Content-Type", "application/vnd.api+json");
            message.Headers.Add("Timestamp", signature.Timestamp.ToString());
            message.Headers.Add("ApiKey", _apiKey);
            message.Headers.Add("Origin", _origin);

            if(body != null)
            {
                message.Content = new StringContent(body, Encoding.UTF8, "application/vnd.api+json");
            }

            return message;
        }

        public virtual async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage message)
        {
            return await _client.SendAsync(message);
        }
    }
}
