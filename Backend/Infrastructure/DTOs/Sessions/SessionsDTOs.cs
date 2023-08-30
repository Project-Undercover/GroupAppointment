namespace Infrastructure.DTOs.Sessions
{
    public class SessionsDTOs
    {

        public class Requests
        {
            public record AddParticipant
            {
                public string? FirstName { get; set; }
                public string? LastName { get; set; }
                public Guid SessionId { get; set; }
                public Guid UserId { get; set; }
            }
            public record Create
            {
                public DateTime startDate { get; set; }
                public DateTime endDate { get; set; }
                public int maxParticipants { get; set; }
            }
            public record Edit(Guid id) : Create;
            public record Delete(Guid id) : Create;
        }


        public class Responses
        {
            public record GetById : EntityDTO
            {
                public int ParticipantsCount { get; set; }
                public int MaxParticipants { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime EndDate { get; set; }
                public List<Participant> Participants { get; set; }


                public record Participant
                {
                    public Guid id { get; set; }
                    public string FirstName { get; set; }
                    public string LastName { get; set; }
                }
            }


            public record GetAllDT : EntityDTO
            {
                public int ParticipantsCount { get; set; }
                public int MaxParticipants { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime EndDate { get; set; }
            }
        }
    }
}
