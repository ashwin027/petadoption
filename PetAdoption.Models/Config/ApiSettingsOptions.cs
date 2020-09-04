using PetAdoption.Eventing.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetAdoption.Models.Config
{
    public class ApiSettingsOptions
    {
        public const string ApiSettings = "ApiSettings";
        public AuthSettingsOptions AuthZeroSettings { get; set; }
        public EventingSystemConfig EventingSystemConfig { get; set; }
        public SpaConfig SpaConfig { get; set; }
    }
}
