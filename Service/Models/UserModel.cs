using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EFMC.Service.Models
{
    public class UserRegistration
    {
        [Required]
        [MaxLength(16)]
        [MinLength(8)]
        public string UserName { get; set; }
        [MinLength(0)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(8)]
        public string Password { get; set; }
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public int RoleId { get; set; }
#nullable enable
        public string? Email { get; set; }
        public string? Address { get; set; }

    }

    public class UserLogin
    {
#nullable disable
        [Required]
        [MaxLength(16)]
        [MinLength(8)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(16)]
        [MinLength(8)]
        public string Password { get; set; }
    }

    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string Status { get; set; }
        public bool IsLogin { get; set; }

#nullable enable
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Address { get; set; }
        public double? LoginFailedCount { get; set; }

    }
}
