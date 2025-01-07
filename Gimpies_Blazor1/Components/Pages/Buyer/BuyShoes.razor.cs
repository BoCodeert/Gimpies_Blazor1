using Gimpies_Blazor1.Components.Pages.Dialogs;
using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace Gimpies_Blazor1.Components.Pages.Buyer
{
    public partial class BuyShoes
    {
        private List<Shoe> shoes;

        protected override async Task OnInitializedAsync()
        {
            shoes = await DbContext.Shoes
                .Include(s => s.Brand)
                .Include(s => s.Model)
                .Include(s => s.Colour)
                .Include(s => s.Size)
                .ToListAsync();
        }

        private async Task OpenBuyShoesDialog(Shoe shoe)
        {
            var parameters = new DialogParameters
    {
        { "Shoe", shoe } // Zorg ervoor dat de parameter exact overeenkomt
    };

            //var options = new DialogOptions { CloseButton = true, FullWidth = true };
            var dialog = DialogService.Show<BuyShoesDialog>("Schoenen Inkopen", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var quantity = (int)result.Data;
                await HandleBuyShoes(shoe, quantity);
            }
        }

        private async Task HandleBuyShoes(Shoe shoe, int quantity)
        {
            try
            {
                // Voeg het aantal schoenen toe aan de voorraad
                shoe.Unit += quantity;

                DbContext.Shoes.Update(shoe);
                await DbContext.SaveChangesAsync();

                // Optioneel: een notificatie tonen
                //Snackbar.Add($"Je hebt {quantity} paar schoenen ingekocht.", Severity.Success);
            }
            catch (Exception ex)
            {
                //Snackbar.Add($"Er is een fout opgetreden: {ex.Message}", Severity.Error);
            }
        }
        private string GetRowClass(int voorraad)
        {
            if (voorraad == 0)
            {
                return "voorraad-nul";
            }
            else if (voorraad > 0 && voorraad <= 5)
            {
                return "voorraad-laag";
            }
            else
            {
                return string.Empty;
            }
        }


    }
}