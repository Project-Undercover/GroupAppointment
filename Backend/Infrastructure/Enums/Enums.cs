using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Enums
{
    public class Enums
    {
        public enum Day
        {
            Sunady,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
        }



        public enum UserRole
        {
            Admin,
            Instructor,
            Parent,
        }



        public enum Folder
        {
            Default,
            SessionImages,
            UserImages,
        }
    }
}
