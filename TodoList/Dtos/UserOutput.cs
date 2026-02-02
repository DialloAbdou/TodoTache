using System.ComponentModel;
using TodoList.Models;

namespace TodoList.Dtos
{
    public class UserOutput
    (
        int Id, 
        string Name,
        string Token,
       IEnumerable<Tache> taches
    );
    
}
