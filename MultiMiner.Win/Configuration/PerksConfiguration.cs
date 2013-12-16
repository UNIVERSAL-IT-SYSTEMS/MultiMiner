﻿using MultiMiner.Utility;
using System;
using System.IO;

namespace MultiMiner.Win.Configuration
{
    public class PerksConfiguration
    {
        public bool PerksEnabled { get; set; }
        public bool ShowExchangeRates { get; set; }
        public bool ShowIncomeRates { get; set; }
        public bool ShowIncomeInUsd { get; set; }

        private int donationPercent = 1;
        public int DonationPercent
        {
            get
            {
                return donationPercent;
            }
            set
            {
                donationPercent = Math.Max(1, Math.Min(100, value));
            }
        }

        private string configDirectory;
        public string PerksConfigurationFileName()
        {
            return Path.Combine(configDirectory, "PerksConfiguration.xml");
        }

        public void SavePerksConfiguration(string configDirectory = null)
        {
            if (!String.IsNullOrEmpty(configDirectory))
                this.configDirectory = configDirectory;

            ConfigurationReaderWriter.WriteConfiguration(this, PerksConfigurationFileName());
        }

        public void LoadPerksConfiguration(string configDirectory)
        {
            if (String.IsNullOrEmpty(configDirectory))
                this.configDirectory = ApplicationPaths.AppDataPath();
            else
                this.configDirectory = configDirectory;

            PerksConfiguration tmp = ConfigurationReaderWriter.ReadConfiguration<PerksConfiguration>(PerksConfigurationFileName());

            ObjectCopier.CopyObject(tmp, this);
        }
    }
}
