using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static MudBlazor.Colors;

namespace Gimpies_Blazor1.Components.Pages.Admin
{
    public partial class UserOverview
    {
        private List<User> users;
        private User userToDelete;

        protected override async Task OnInitializedAsync()
        {
            users = await DbContext.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        private async Task OpenAddUserDialog()
        {
            Navigation.NavigateTo("/addUser");
        }

        private async Task OpenEditUserDialog(User user)
        {
            Navigation.NavigateTo($"/editUser/{user.Userid}");
        }

        private async Task OpenDeleteUserDialog(User user)
        {
            userToDelete = user;

            //var parameters = new DialogParameters
            //{
            //    { nameof(ConfirmDialog.ContentText), "Weet je zeker dat je deze medewerker wilt verwijderen?" },
            //    { nameof(ConfirmDialog.ButtonText), "Verwijderen" },
            //    { nameof(ConfirmDialog.Color), Color.Error }
            //};

            //var dialog = DialogService.Show<ConfirmDialog>("Bevestiging", parameters);
            //var result = await dialog.Result;

            //if (!result.Canceled) // Verwijder de medewerker als de gebruiker bevestigt
            //{
            await DeleteUser(userToDelete);
            //}
        }
        private async Task DeleteUser(User user)
        {
            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();
            users = await DbContext.Users
                .Include(u => u.Role) 
                .ToListAsync(); // Verfris de lijst met medewerkers na verwijderen
        }
    }
}