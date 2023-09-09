using Infrastructure.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Infrastructure.Entities.Sessions
{
    public class Session : Entity
    {
        public string Title { get; set; }
        public string? Image { get; set; }
        public int ParticipantsCount { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? LocationName { get; set; }
        public Location? Location { get; set; }
        public bool IsAvailable { get; set; } = true;

        public List<Participant> Participants { get; set; }
        public List<Instructor> Instructors { get; set; }


        [Timestamp]
        public byte[] Version { get; set; }
    }
}
