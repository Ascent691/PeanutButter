﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailSpooler.Win32Service.Models;
using PeanutButter.ServiceShell;
using ServiceShell;

namespace EmailSpooler.Win32Service
{
    public class EmailSpoolerDependencies: IEmailSpoolerDependencies
    {
        private ISimpleLogger _logger;
        public IEmailContext DbContext { get; private set; }
        public Func<IEmail> EmailGenerator { get; private set; }
        public IEmailSpoolerConfig EmailSpoolerConfig { get; private set; }
        public IEmailConfiguration EmailConfig { get; private set; }

        public EmailSpoolerDependencies(ISimpleLogger logger)
        {
            this._logger = logger;
            this.DbContext = new EmailContext();
            this.EmailConfig = EmailConfiguration.CreateFromAppConfig();
            this.EmailSpoolerConfig = new EmailSpoolerConfig(logger);
            this.EmailGenerator = () => new Email(this.EmailConfig);
        }
    }
}
