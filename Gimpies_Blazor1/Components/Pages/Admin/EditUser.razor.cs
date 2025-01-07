using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.AccessControl;

namespace Gimpies_Blazor1.Components.Pages.Admin
{
    public partial class EditUser
    {
        [Parameter] public int Userid { get; set; }

        private User userToEdit;
        private string errorMessage;
        private bool isDataLoaded = false;
        private List<Role> roles;


        protected override async Task OnInitializedAsync()
        {
            roles = await DbContext.Roles.ToListAsync();
            if (roles == null || !roles.Any())
            {
                errorMessage = "Er zijn geen rollen beschikbaar.";
            }

            try
            {
                userToEdit = await DbContext.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Userid == Userid);
                if (userToEdit == null)
                {
                    errorMessage = "Gebruiker niet gevonden.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Er is een fout opgetreden bij het ophalen van de gegevens: {ex.Message}";
            }
            isDataLoaded = true;

        }

        private async Task HandleEditUser()
        {
            try
            {
                var duplicate = await DbContext.Users.AnyAsync(u =>
                    u.Username == userToEdit.Username);
                if (duplicate)
                {
                    errorMessage = "Een gebruiker met dezelfde gebruikersnaam bestaat al.";
                    return;
                }
                if (userToEdit.fk_UserRoleID == 0)
                {
                    errorMessage = "Selecteer een rol voor de gebruiker.";
                    return;
                }
                DbContext.Users.Update(userToEdit);
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