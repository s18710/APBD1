using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using apbdCW3.Services;
using Microsoft.AspNetCore.Http;

namespace apbdCW3.middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IStudentsDbService service)
        {
            if (httpContext.Request != null)
            {
                string path = httpContext.Request.Path;
                string queryString = httpContext.Request.QueryString.ToString();
                string method = httpContext.Request.Method.ToString();
                string body = "";

                using(StreamReader reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    body = await reader.ReadToEndAsync();

                }
                string logPath= @"C:\Users\awiercinska\Desktop\APBD\APBD1\apbdCW3\apbdCW3\Logging\logs.txt";
                string toLog = DateTime.Now+" path: " + path + " query String: " + queryString + " method: " + method + " body: " + body;
                File.WriteAllText(logPath, toLog);
            }
            await _next(httpContext);
        }
    }
}
