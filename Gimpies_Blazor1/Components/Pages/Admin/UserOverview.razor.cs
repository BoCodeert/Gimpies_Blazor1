using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gimpies_Blazor1.Components.Pages.Admin
{
    public partial class UserOverview
    {
        private List<User> users;

        protected override async Task OnInitializedAsync()
        {
            users = await DbContext.Users
                .Include(u => u.Role)
                .ToListAsync();
        }
    }
}