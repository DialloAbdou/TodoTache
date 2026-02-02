namespace TodoList.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Token { get; set; }
        public ICollection<Tache>? Taches { get; set; } = new List<Tache>();
    }
}
