using Infrastructure.Entities.Shared;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities.Sessions
{
    public class Session : Entity
    {
        public int ParticipantsCount { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Participant> Participants { get; set; }


        [Timestamp]
        public byte[] Version { get; set; }
    }
}
