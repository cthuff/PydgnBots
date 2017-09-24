﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;

using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PydgnBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }

        public static async Task<bool> MakeUser(string username, string conversationID, string serviceType)
        {
            var client = new HttpClient();
           // var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request parameters
            var uri = "http://serveymcserverface.com/user";

            HttpResponseMessage response;

            var body = $"userName={username}&conversationID={conversationID}&serviceType={serviceType}";
            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                //response = await client.PostAsync(uri, content);
                response = client.GetAsync(requestUri: "http://serveymcserverface.com/user").Result;
                
            }

            System.Diagnostics.Debug.WriteLine(response.StatusCode);
            System.Console.WriteLine(response.Content);
            return response.IsSuccessStatusCode;
        }

        public static async Task<JObject> SendMessage(string message, string conversationID, string serviceType)
        {
            var client = new HttpClient();
            // var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request parameters
            var uri = "http://serveymcserverface.com/message";

            HttpResponseMessage response;

            var body = $"message={message}&senderSession={conversationID}";
            // Request body
            byte[] byteData = Encoding.UTF8.GetBytes(body);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
                //response = await client.PostAsync(uri, content);
                response = client.GetAsync(requestUri: "http://serveymcserverface.com/user").Result;
            }

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                return JObject.Parse(data);
            }
            else
            {
                return JObject.Parse("");
            }
        }
    }
}
    
}
