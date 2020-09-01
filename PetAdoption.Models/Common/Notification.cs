using System;
using System.Collections.Generic;
using System.Text;

namespace PetAdoption.Models.Common
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        public string Message { get; set; }
    }
}
