﻿using Pawze.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pawze.API.Domain
{

    public class PawzeConfiguration
    {
        public PawzeConfiguration()
        {

        }
        public PawzeConfiguration(PawzeConfigurationsModel configuration)
        {
            this.Update(configuration);
        }

        public void Update(PawzeConfigurationsModel configuration)
        {
            ConfigurationId = configuration.ConfigurationId;
            CurrentBoxItemPrice = configuration.CurrentBoxItemPrice;
        }

        public int ConfigurationId { get; set; }
        public decimal CurrentBoxItemPrice { get; set; }
    }
}