﻿using System;
using System.Configuration;
using PeanutButter.ServiceShell;
using ServiceShell;

namespace EmailSpooler.Win32Service
{
    public class EmailSpoolerConfig : IEmailSpoolerConfig
    {
        public ISimpleLogger Logger { get; protected set; }
        public int MaxSendAttempts { get; private set; }
        public int BackoffIntervalInMinutes { get; private set; }
        public int BackoffMultiplier { get; private set; }
        public int PurgeMessageWithAgeInDays { get; private set; }
        public EmailSpoolerConfig(ISimpleLogger logger)
        {
            this.Logger = logger;
            this.MaxSendAttempts = GetConfiguredIntVal("MaxSendAttempts", 5);
            this.BackoffIntervalInMinutes = GetConfiguredIntVal("BackoffIntervalInMinutes", 2);
            this.BackoffMultiplier = GetConfiguredIntVal("BackoffMultiplier", 2);
            this.PurgeMessageWithAgeInDays = GetConfiguredIntVal("PurgeMessageWithAgeInDays", 30);
        }

        private int GetConfiguredIntVal(string keyName, int defaultValue)
        {
            var configured = ConfigurationManager.AppSettings[keyName];
            if (configured == null)
            {
                this.Logger.LogInfo(String.Join("", new[] { 
                    "No configured value for '", keyName, "'; falling back on default value: '", defaultValue.ToString(), "'" 
                }));
                return defaultValue;
            }
            int configuredValue;
            if (Int32.TryParse(configured, out configuredValue))
                return configuredValue;
            this.Logger.LogWarning(String.Join("", new[] {
                "Configured value of '", configured, "' cannot be parsed into an integer; falling back on default value '", defaultValue.ToString(), "'"
            }));
            return defaultValue;
        }
    }
}