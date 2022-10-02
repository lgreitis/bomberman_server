using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Table("LobbyUser")]
    public class LobbyUserEntity : Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LobbyUserId { get; set; }
        public int UserId { get; set; }
        public int LobbyId { get; set; }
        public UserEntity User { get; set; }
        public LobbyEntity Lobby { get; set; }
    }
}
