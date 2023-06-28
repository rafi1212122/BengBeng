using Common.Utils;
using Common;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace HttpServer
{
    public class Program
    {
        private static readonly Logger c = new("HTTP", ConsoleColor.Green);

        public static void Main()
        {
            Thread.CurrentThread.IsBackground = true;
            var builder = WebApplication.CreateBuilder();
            var app = builder.Build();

            app.UsePathBase("/");
            app.Urls.Add($"http://*:{Global.config.Http.HttpPort}");
            app.Urls.Add($"https://*:{Global.config.Http.HttpsPort}");

            app.UseMiddleware<RequestLoggingMiddleware>();

            try
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    RequestPath = string.Empty,
                    ServeUnknownFileTypes = true,
                    FileProvider = new PhysicalFileProvider(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Resources\\Static"))
                });
            }
            catch (Exception)
            {
                c.Warn("Static files are not being served!");
            }

            c.Log($"HTTP server started on port {Global.config.Http.HttpPort} & {Global.config.Http.HttpsPort}");
            app.Run();
        }

        private class RequestLoggingMiddleware
        {
            private readonly RequestDelegate _next;
            private static readonly string[] SurpressedRoutes = new string[] { "/report", "/sdk/dataUpload" };

            public RequestLoggingMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                finally
                {
                    if ((int)Global.config.VerboseLevel > (int)VerboseLevel.Normal)
                    {
                        c.Log($"{context.Response.StatusCode} {context.Request.Method.ToUpper()} {context.Request.Path}");
                    }
                    else if (((int)Global.config.VerboseLevel > (int)VerboseLevel.Silent) && !SurpressedRoutes.Contains(context.Request.Path.ToString()))
                    {
                        c.Log($"{context.Response.StatusCode} {context.Request.Method.ToUpper()} {context.Request.Path}");
                    }
                }
            }
        }
    }
}