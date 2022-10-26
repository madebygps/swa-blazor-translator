using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorApp.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;

namespace ApiIsolated
{
    public class TranslateText
    {
        private readonly ILogger _logger;
        private readonly HttpClient httpClient;

        public TranslateText(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TranslateText>();
            httpClient = new HttpClient();

        }

        [Function("TranslateText")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {



            var newTranslation = System.Text.Json.JsonSerializer.Deserialize<Translation>(req.Body);

            var response = req.CreateResponse(HttpStatusCode.OK);
            HttpResponseMessage theresponse;
            string result = "";

            // Input and output languages are defined as parameters.
            //string route = $"/translate?api-version=3.0&from={newTranslation.FromLang}&to={newTranslation.ToLang}";
            string route = $"/translate?api-version=3.0&from=en&to=es";
            object[] body = new object[] { new { Text = newTranslation.Text } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                // Build the request.
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri($"{System.Environment.GetEnvironmentVariable("Endpoint")}{route}");
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", System.Environment.GetEnvironmentVariable("Key"));
                // location required if you're using a multi-service or regional (not global) resource.
                request.Headers.Add("Ocp-Apim-Subscription-Region", "eastus2");

                // Send the request and get response.
                theresponse = await client.SendAsync(request).ConfigureAwait(false);
                // Read response as a string.
                result = await theresponse.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }



            _logger.LogInformation("C# HTTP trigger function processed a request.");

            response.Headers.Add("Content-Type", "json/application; charset=utf-8");

            response.WriteString(result);

            return response;
        }
    }
}
