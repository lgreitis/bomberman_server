using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    [Table("User")]
    public class UserEntity : Entity
    {
        public UserEntity() 
        { 
            LobbyUsers = new HashSet<LobbyUserEntity>(); 
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? LoginToken { get; set; }
        public ICollection<LobbyUserEntity> LobbyUsers { get; set; }
    }
}
