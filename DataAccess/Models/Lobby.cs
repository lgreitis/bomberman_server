using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    [Table("Lobby")]
    public class LobbyEntity : Entity
    {
        public LobbyEntity()
        {
            LobbyUsers = new HashSet<LobbyUserEntity>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LobbyId { get; set; }

        public ICollection<LobbyUserEntity> LobbyUsers; 
    }
}
