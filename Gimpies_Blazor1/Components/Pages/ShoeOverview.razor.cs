using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace Gimpies_Blazor1.Components.Pages
{
    public partial class ShoeOverview
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

        private async Task OpenAddShoeDialog()
        {


        }

        private async Task OpenDeleteShoeDialog()
        {

            var parameters = new DialogParameters
            {
                { nameof(ConfirmDialog.ContentText), "Weet u zeker dat u deze schoen wilt verwijderen?" },
                { nameof(ConfirmDialog.ButtonText), "Verwijderen" },
                { nameof(ConfirmDialog.Color), Color.Error }
            };

            DialogService.Show<ConfirmDialog>("Bevestiging", parameters);

            var dialogresult = await DialogService.ShowAsync<ConfirmDialog>("Confirm", parameters);
            var result = await dialogresult.Result;

            //if (!result)
            //{
            //    return;
            //}
        }
        private void DeleteShoe()
        {

        }
    }
}

