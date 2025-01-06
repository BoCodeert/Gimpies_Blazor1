using System.Data;

namespace Gimpies_Blazor1.Database.Models.Entities
{
    public class UserPolicy
    {

        public int Id { get; set; }
        public int fk_UserRoleID { get; set; } // Verwijzing naar Role
        public string PolicyName { get; set; }
        public bool IsEnabled { get; set; }
        public virtual Role Role { get; set; } // Navigatie-eigenschap


        public const string View_Shoes = "View_Shoes";
        public const string Add_Shoes = "Add_Shoes";
        public const string Edit_Shoes = "Edit_Shoes";
        public const string Delete_Shoes = "Delete_Shoes";
        public const string Sell_Shoes = "Sell_Shoes";
        public const string Buy_Shoes = "Buy_Shoes";
        public const string View_Users = "View_Users";
        public const string Add_Users = "Add_Users";
        public const string Edit_Users = "Edit_Users";
        public const string Delete_Users = "Delete_Users";

        public static List<string> GetPolicies()
        {
            return new List<string>
            {
                View_Shoes,
                Add_Shoes,
                Edit_Shoes,
                Delete_Shoes,
                Sell_Shoes,
                Buy_Shoes,
                View_Users,
                Add_Users,
                Edit_Users,
                Delete_Users
            };
        }
    }
}
