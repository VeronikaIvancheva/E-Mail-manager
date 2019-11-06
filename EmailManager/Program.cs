using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace EmailManager
{
    public class Program
    {
        //// If modifying these scopes, delete your previously saved credentials
        //// at ~/.credentials/gmail-dotnet-quickstart.json
        //static string[] Scopes = { GmailService.Scope.GmailReadonly };
        //static string ApplicationName = "Gmail API .NET Quickstart";

        public static void Main(string[] args)
        {   
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
