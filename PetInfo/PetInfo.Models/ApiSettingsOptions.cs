using System;
using System.Collections.Generic;
using System.Text;

namespace PetInfo.Models
{
    public class ApiSettingsOptions
    {
        public const string ApiSettings = "ApiSettings";
        public List<EndpointInfo> Endpoints { get; set; }
    }
}
