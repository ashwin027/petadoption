using System;
using System.Collections.Generic;
using System.Text;

namespace PetAdoption.Eventing.Messages
{
    public class UserPetCreationMessage
    {
        public const string Topic = Topics.UserPetCreationTopic;
        public int PreviousUserPetId { get; set; }
        public string UserId { get; set; }
    }
}
