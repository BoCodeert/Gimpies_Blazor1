using Gimpies_Blazor1.Database.Models;
using System.Data;
namespace Gimpies_Blazor1.Database.Models.Entities
{
    public class User
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string PasswordHashed { get; set; }
        public int fk_UserRoleID { get; set; } // Verwijzing naar Role
        public virtual Role Role { get; set; } // Navigatie-eigenschap
    }

}
