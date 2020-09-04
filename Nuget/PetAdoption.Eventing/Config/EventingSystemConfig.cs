using System;
using System.Collections.Generic;
using System.Text;

namespace PetAdoption.Eventing.Config
{
    public class EventingSystemConfig
    {
        public string SystemUrlList { get; set; } // Either the broker list for kafka or rabbitmq server list etc.
    }
}
