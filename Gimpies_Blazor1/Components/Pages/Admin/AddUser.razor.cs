using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gimpies_Blazor1.Components.Pages.Admin
{
    public partial class AddUser
    {
        private User newUser = new User();
        private string? errorMessage;
        private List<Role> roles;

        protected override async Task OnInitializedAsync()
        {
            roles = await DbContext.Roles.ToListAsync();
            if (roles == null || !roles.Any())
            {
                errorMessage = "Er zijn geen rollen beschikbaar.";
            }
        }

        private async Task HandleAddUser()
        {
            try
            {
                // Controleer op duplicaten
                var duplicate = await DbContext.Users.AnyAsync(u =>
                    u.Username == newUser.Username);
                if (duplicate)
                {
                    errorMessage = "Een gebruiker met dezelfde gebruikersnaam bestaat al.";
                    return;
                }
                if (newUser.fk_UserRoleID == 0)
                {
                    errorMessage = "Selecteer een rol voor de gebruiker.";
                    return;
                }

                DbContext.Users.Add(newUser);
                await DbContext.SaveChangesAsync();
                Navigation.NavigateTo("/userOverview");
            }
            catch (Exception ex)
            {
                errorMessage = $"Er is een fout opgetreden: {ex.Message}";
            }

        }
        private async Task ReturnToOverview()
        {
            Navigation.NavigateTo("/userOverview");
        }
    }
}