using System;
using System.Collections.Generic;
using System.Text;

namespace UserPetInfo.Models.Config
{
    public class ApiSettingsOptions
    {
        public const string ApiSettings = "ApiSettings";
        public AuthSettingsOptions AuthZeroSettings { get; set; }
    }
}
