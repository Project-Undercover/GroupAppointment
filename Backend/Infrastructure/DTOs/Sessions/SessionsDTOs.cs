namespace Infrastructure.DTOs.Sessions
{
    public class SessionsDTOs
    {

        public class Requests
        {
            public record AddParticipant
            {
                public Guid SessionId { get; set; }
                public Guid ChildId { get; set; }
            }
            public record Create
            {
                public string title { get; set; }
                public DateTime startDate { get; set; }
                public DateTime endDate { get; set; }
                public int maxParticipants { get; set; }
                public string? locationName { get; set; }
                public Location? location { get; set; }


                public class Location
                {
                    public double longitude { get; set; }
                    public double latitude { get; set; }
                }
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
                public string? locationName { get; set; }
                public Location? location { get; set; }
                public List<Participant> Participants { get; set; }



                public class Location
                {
                    public double longitude { get; set; }
                    public double latitude { get; set; }
                }

                public record Participant
                {
                    public Guid id { get; set; }
                    public User user { get; set; }
                    public required string participantName { get; set; }

                    public record User
                    {
                        public Guid id { get; set; }
                        public string name { get; set; }
                    }
                }
            }


            public record GetAllDT : EntityDTO
            {
                public int ParticipantsCount { get; set; }
                public int MaxParticipants { get; set; }
                public DateTime StartDate { get; set; }
                public DateTime EndDate { get; set; }
                public string? locationName { get; set; }
            }
        }
    }
}
