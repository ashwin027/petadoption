using System;
using System.Collections.Generic;
using System.Text;

namespace UserPetInfo.Models.Messages
{
    public class UserPetCreatedMessage
    {
        public int UserPetId { get; set; }
        public string UserId { get; set; }
    }
}
