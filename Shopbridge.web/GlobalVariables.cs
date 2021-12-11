using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Shopbridge.Web
{
    public static class GlobalVariables
    {
        public static HttpClient shopBridgeClient = new();

        static GlobalVariables()
        {
            shopBridgeClient.BaseAddress = new Uri("https://localhost:44323/api/");
            shopBridgeClient.DefaultRequestHeaders.Clear();
            shopBridgeClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
