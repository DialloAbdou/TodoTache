using System.Reflection.Metadata.Ecma335;

namespace TodoList.Models
{
    public class Tache
    {
        public int Id { get; set; }
        public string Titre { get; set; }

        public DateTime DateDebut { get; set; }

        public DateTime? DateFin { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
