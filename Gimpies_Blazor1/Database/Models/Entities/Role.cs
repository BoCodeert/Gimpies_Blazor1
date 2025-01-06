using System.ComponentModel.DataAnnotations;

namespace Gimpies_Blazor1.Database.Models.Entities
{
    public class Role
    {

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        public virtual ICollection<UserPolicy> Policies { get; set; } = new List<UserPolicy>();
    }
}
