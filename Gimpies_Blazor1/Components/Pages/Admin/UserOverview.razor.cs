using Gimpies_Blazor1.Components.Pages.Dialogs;
using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using static MudBlazor.Colors;

namespace Gimpies_Blazor1.Components.Pages.Admin
{
    public partial class UserOverview
    {
        private List<User> users;
        private User userToDelete;
        private User userToEdit;
        private User newUser = new User();
        private string? errorMessage;
        private List<Role> roles;

        protected override async Task OnInitializedAsync()
        {
            users = await DbContext.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        private async Task OpenAddUserDialog()
        {
            var parameters = new DialogParameters
            {
                {"ButtonText", "Toevoegen" },
                {"ActionType", "AddUser" },
                {"newUser", newUser }
            };

            var dialog = DialogService.Show<ConfirmDialog>("Medewerker toevoegen", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                await HandleAddUser();
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
                Navigation.Refresh(forceReload: true);
                Snackbar.Add("Medewerker toegevoegd!", Severity.Success);
            }
            catch (Exception ex)
            {
                errorMessage = $"Er is een fout opgetreden: {ex.Message}";
            }
        }


        private async Task OpenEditUserDialog(User user)
        {
            userToEdit = user;

            var parameters = new DialogParameters<ConfirmDialog>
            {
                { "newUser", userToEdit }, // Zorg ervoor dat de parameter exact overeenkomt
                {"ActionType", "EditUser" },
                {"ButtonText", "Bewerken" }
            };

            var options = new DialogOptions { CloseButton = true, FullWidth = true };
            var dialog = DialogService.Show<ConfirmDialog>("Schoenen bewerken", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var updatedUser = (User)result.Data;
                await HandleEditUser(updatedUser);
            }
        }

        private async Task HandleEditUser(User updatedUser)
        {
            try
            {
                var duplicate = await DbContext.Users.AnyAsync(u =>
                    u.Username == updatedUser.Username);
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

        private async Task OpenDeleteUserDialog(User user)
        {
            userToDelete = user;

            var parameters = new DialogParameters<ConfirmDialog>
            {
                { "newUser", userToDelete }, // Zorg ervoor dat de parameter exact overeenkomt
                {"ActionType", "DeleteUser" },
                {"ButtonText", "Verwijderen" }
            };

            var dialog = DialogService.Show<ConfirmDialog>("Bevestiging", parameters);
            var result = await dialog.Result;

            if (!result.Canceled) // Verwijder de medewerker als de gebruiker bevestigt
            {
                await DeleteUser(userToDelete);
        }
        }
        private async Task DeleteUser(User userToDelete)
        {
            DbContext.Users.Remove(userToDelete);
            await DbContext.SaveChangesAsync();
            users = await DbContext.Users
                .Include(u => u.Role) 
                .ToListAsync(); // Verfris de lijst met medewerkers na verwijderen
        }
    }
}