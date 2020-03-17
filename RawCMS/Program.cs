﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina'</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Diagnostics;
using System.IO;

namespace RawCMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Debug.WriteLine("Starting MAIN");
                Run(args);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        private static void Run(string[] args)
        {
            var url = Environment.GetEnvironmentVariable("ASPNETCORE_SERVER_URLS");
            var port = Environment.GetEnvironmentVariable("PORT");
            Console.WriteLine(url);
            if (port != null && url != null)
            {
                url = url.Replace("$PORT", port);
                Console.WriteLine(url);
            }

            Debug.WriteLine(Environment.GetEnvironmentVariable("PORT"));

            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                            .UseContentRoot(Directory.GetCurrentDirectory())
                            .UseSetting("detailedErrors", "true")
                            .ConfigureLogging(logger =>
                            {
                                logger.ClearProviders();
                                logger.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                            })
                            .UseNLog()
                            .UseStartup<Startup>();
                    if (url != null)
                    {
                        webBuilder.UseUrls(url);
                    }
                }).Build().Run();
        }
    }
}