using System;
using System.Runtime.Serialization;

namespace WebHost.Models
{
    [DataContract(Name = "user")]
    [Serializable]
    public class User
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "username")]
        public string UserName { get; set; }

        /// <summary>
        /// Note: Should only be filled when the user registers
        /// </summary>
        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "dob")]
        public DateTime DateOfBirth { get; set; }

        public User()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
            this.UserName = string.Empty;
            this.Password = string.Empty;
        }
    }
}
