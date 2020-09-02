using System;
using System.Collections.Generic;
using System.Text;

namespace UserPetInfo.Models.Messages
{
    public class UserPetCreationMessage
    {
        public int PreviousUserPetId { get; set; }
        public string UserId { get; set; }
    }
}
