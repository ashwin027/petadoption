using System;
using System.Collections.Generic;
using System.Text;

namespace PetAdoption.Models.Messages
{
    public class UserPetCreationMessage
    {
        public int PreviousUserPetId { get; set; }
        public string UserId { get; set; }
    }
}
