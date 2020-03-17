﻿//******************************************************************************
// <copyright file="license.md" company="RawCMS project  (https://github.com/arduosoft/RawCMS)">
// Copyright (c) 2019 RawCMS project  (https://github.com/arduosoft/RawCMS)
// RawCMS project is released under GPL3 terms, see LICENSE file on repository root at  https://github.com/arduosoft/RawCMS .
// </copyright>
// <author>Daniele Fontani, Emanuele Bucarelli, Francesco Mina'</author>
// <autogenerated>true</autogenerated>
//******************************************************************************
using Microsoft.Extensions.Configuration;
using RawCMS.Client.BLL.Interfaces;
using System.IO;

namespace RawCMS.Client.BLL.Services
{
    public class ClientConfigService : IClientConfigService
    {
        private static IConfigurationBuilder _builder = null;
        private static IConfigurationRoot _configuration = null;

        public static IConfigurationRoot Config
        {
            get
            {
                if (_configuration == null)
                {
                    _builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    _configuration = _builder.Build();
                }
                return _configuration;
            }
            private set { }
        }

        public T GetValue<T>(string key)
        {
            return Config.GetValue<T>(key);
        }
    }
}