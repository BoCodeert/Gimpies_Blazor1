using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace Gimpies_Blazor1.Components.Pages
{
    public partial class ShoeOverview
    {
        private List<Shoe> shoes;
        private Shoe shoeToDelete;

        protected override async Task OnInitializedAsync()
        {
            shoes = await DbContext.Shoes
                .Include(s => s.Brand)
                .Include(s => s.Model)
                .Include(s => s.Colour)
                .Include(s => s.Size)
                .ToListAsync();
        }

        private async Task OpenAddShoeDialog()
        {
            Navigation.NavigateTo("/addShoe");
        }

        private async Task OpenDeleteShoeDialog(Shoe shoe)
        {
            shoeToDelete = shoe;

            //var parameters = new DialogParameters
            //{
            //    { nameof(ConfirmDialog.ContentText), "Weet je zeker dat je deze schoen wilt verwijderen?" },
            //    { nameof(ConfirmDialog.ButtonText), "Verwijderen" },
            //    { nameof(ConfirmDialog.Color), Color.Error }
            //};

            //var dialog = DialogService.Show<ConfirmDialog>("Bevestiging", parameters);
            //var result = await dialog.Result;

            //if (!result.Canceled) // Verwijder de schoen als de gebruiker bevestigt
            //{
            await DeleteShoe(shoeToDelete);
            //}
        }
        private async Task DeleteShoe(Shoe shoe)
        {
            DbContext.Shoes.Remove(shoe);
            await DbContext.SaveChangesAsync();
            shoes = await DbContext.Shoes
                .Include(s => s.Brand)
                .Include(s => s.Model)
                .Include(s => s.Colour)
                .Include(s => s.Size)
                .ToListAsync(); // Verfris de lijst met schoenen na verwijderen
        }

        private async Task OpenEditShoeDialog(Shoe shoe)
        {
            Navigation.NavigateTo($"/editShoe/{shoe.ShoeId}");
        }
    }
}

