using System;
using System.Collections.Generic;
using System.Text;

namespace PetAdoption.Eventing.Messages
{
    public class UserPetCreatedMessage
    {
        public const string Topic = Topics.UserPetCreatedTopic;
        public int UserPetId { get; set; }
        public string UserId { get; set; }
    }
}
