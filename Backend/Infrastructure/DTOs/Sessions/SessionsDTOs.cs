using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Infrastructure.DTOs.Sessions
{
    public class SessionsDTOs
    {

        public class Requests
        {
            public record AddParticipant
            {
                public Guid SessionId { get; set; }
                public List<Guid> Children { get; set; }
            }
            public record Create
            {
                [Required]
                public string title { get; set; }

                [Required]
                public DateTime startDate { get; set; }

                [Required]
                public DateTime endDate { get; set; }

                [Range(1, 100), Required]
                public int maxParticipants { get; set; }


                public string? locationName { get; set; }
                public Location? location { get; set; }

                [Required, MinLength(1, ErrorMessage = "At least one instructor required")]
                public List<Guid> instructors { get; set; } = new List<Guid>();


                public class Location
                {
                    public double longitude { get; set; }
                    public double latitude { get; set; }
                }
            }
            public record Edit : Create
            {
                [Required] public Guid id { get; set; }
                [Required] public bool isAvailable { get; set; }
            };
            public record Delete(Guid id) : Create;
        }


        public class Responses
        {

            public record Instructor
            {
                public Guid id { get; set; }
                public string name { get; set; }
            }


            public record SessionPaticipant
            {
                public User user { get; set; }
                public List<Child> children { get; set; }


                public class User
                {
                    public Guid id { get; set; }
                    public string name { get; set; }
                }

                public class Child
                {
                    public Guid id { get; set; }
                    public string name { get; set; }
                }
            }


            public record GetById : EntityDTO
            {
                public string title { get; set; }
                public string? image { get; set; }
                public int ParticipantsCount { get; set; }
                public int MaxParticipants { get; set; }
                public DateTimeOffset StartDate { get; set; }
                public DateTimeOffset EndDate { get; set; }
                public string? locationName { get; set; }
                public Location? location { get; set; }
                public bool isAvailable { get; set; }
                public List<Participant> Participants { get; set; }
                public List<Instructor> Instructors { get; set; }


                public class Instructor
                {
                    public Guid id { get; set; }
                    public string name { get; set; }
                }

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
                public string title { get; set; }
                public string? image { get; set; }
                public int ParticipantsCount { get; set; }
                public int MaxParticipants { get; set; }
                public DateTimeOffset StartDate { get; set; }
                public DateTimeOffset EndDate { get; set; }
                public string? locationName { get; set; }
                public List<Instructor> instructors { get; set; }
                public bool isAvailable { get; set; }
                public bool isParticipating { get => children.Count() > 0; }
                public List<Child> children { get; set; }

                public record Child
                {
                    public Guid id { get; set; }
                    public string name { get; set; }
                }

                public record Instructor
                {
                    public Guid id { get; set; }
                    public string name { get; set; }
                }
            }


            public record UserSession : EntityDTO
            {
                public string title { get; set; }
                public string? image { get; set; }
                public int ParticipantsCount { get; set; }
                public int MaxParticipants { get; set; }
                public DateTimeOffset StartDate { get; set; }
                public DateTimeOffset EndDate { get; set; }
                public string? locationName { get; set; }
                public List<string> instructors { get; set; }
                public bool isAvailable { get; set; }
                public List<Child> children { get; set; }

                public record Child
                {
                    public Guid id { get; set; }
                    public string name { get; set; }
                }
            }
        }
    }
}
