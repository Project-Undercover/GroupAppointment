using Infrastructure.Entities.Shared;
using System.ComponentModel.DataAnnotations;
using static Infrastructure.Enums.Enums;

namespace Infrastructure.Entities.Sessions
{
    public class StaticAppointment : Entity
    {
        public int MaxParticipants { get; set; }

        public Day Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }



        [Timestamp]
        public byte[] Version { get; set; }
    }
}
