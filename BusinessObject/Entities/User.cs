using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectsLayer.Entities
{
    public class User : BaseEntity
    {
        public string? Token { get; set; }
        public DateTime Expiry { get; set; }
        public string? FullName { get; set; }
        public int? EntityId { get; set; }
        public int? EnrollmentId { get; set; }
        public dynamic? StudentEnrollment { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public int RoleId { get; set; }
        public Int32 UserTypeId { get; set; }
        public string? PictureUrl { get; set; }
        public int UnReadCount { get; set; }
    }
    public class UserDto:BaseEntity
    {
        public string? FullName { get; set; }
        public int? UserTypeId { get; set; }
        public int? EntityId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? UserName { get; set; }
        public string? PictueUrl { get; set; }
    }


}
