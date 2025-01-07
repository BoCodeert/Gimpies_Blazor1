using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using Size = Gimpies_Blazor1.Database.Models.Entities.Size;

namespace Gimpies_Blazor1.Components.Pages.Admin
{
    public partial class AddShoe
    {
        private string? errorMessage;
        private List<Shoe> shoes;
        private Shoe newShoe = new Shoe()
        {
            Brand = new Brand(),
            Model = new Model(),
            Colour = new Colour(),
            Size = new Size()
        };

        protected override async Task OnInitializedAsync()
        {
            shoes = await DbContext.Shoes
                .Include(s => s.Brand)
                .Include(s => s.Model)
                .Include(s => s.Colour)
                .Include(s => s.Size)
                .ToListAsync();
        }

        private async Task HandleAddShoe()
        {
            // Controleer op duplicaten
            var duplicate = await DbContext.Shoes.AnyAsync(s =>
                s.Brand.BrandName == newShoe.Brand.BrandName &&
                s.Model.ModelName == newShoe.Model.ModelName &&
                s.Colour.ColourName == newShoe.Colour.ColourName &&
                s.Size.SizeValue == newShoe.Size.SizeValue);

            if (duplicate)
            {
                errorMessage = "Een schoen met dezelfde specificaties bestaat al.";
                return;
            }

            // Voeg de nieuwe schoen toe
            DbContext.Shoes.Add(newShoe);
            await DbContext.SaveChangesAsync();

            // Navigeren terug naar het overzicht
            Navigation.NavigateTo("/shoeOverview");
        }

        private async Task ReturnToOverview()
        {
            Navigation.NavigateTo("/shoeOverview");
        }


    }
}