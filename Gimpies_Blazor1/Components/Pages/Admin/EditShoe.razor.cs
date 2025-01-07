using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using Size = Gimpies_Blazor1.Database.Models.Entities.Size;

namespace Gimpies_Blazor1.Components.Pages.Admin
{
    public partial class EditShoe
    {
        [Parameter]
        public int ShoeId { get; set; }

        private Shoe shoeToEdit;  // De instantie van Shoe die bewerkt moet worden
        private string errorMessage;
        private bool isDataLoaded = false;  // Houdt bij of de gegevens zijn geladen

        protected override async Task OnInitializedAsync()
        {
            try
            {
                // Ophalen van de schoen op basis van de ShoeId
                shoeToEdit = await DbContext.Shoes
                    .Include(s => s.Brand)
                    .Include(s => s.Model)
                    .Include(s => s.Colour)
                    .Include(s => s.Size)
                    .FirstOrDefaultAsync(s => s.ShoeId == ShoeId);

                // Foutafhandeling wanneer de schoen niet gevonden is
                if (shoeToEdit == null)
                {
                    errorMessage = "Schoen niet gevonden.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Er is een fout opgetreden bij het ophalen van de gegevens: {ex.Message}";
            }

            isDataLoaded = true;  // Markeer als gegevens zijn geladen
        }

        private async Task HandleEditShoe()
        {
            // Controleer op duplicaten
            var duplicate = await DbContext.Shoes.AnyAsync(s =>
                s.Brand.BrandName == shoeToEdit.Brand.BrandName &&
                s.Model.ModelName == shoeToEdit.Model.ModelName &&
                s.Colour.ColourName == shoeToEdit.Colour.ColourName &&
                s.Size.SizeValue == shoeToEdit.Size.SizeValue);

            if (duplicate)
            {
                errorMessage = "Een schoen met dezelfde specificaties bestaat al.";
                return;
            }

            try
            {
                // Bijwerken van de schoen in de database
                if (shoeToEdit != null)
                {
                    DbContext.Update(shoeToEdit);
                    await DbContext.SaveChangesAsync();
                    Navigation.NavigateTo("/shoeOverview"); // Navigeren naar de overzichtspagina na het opslaan
                }
                else
                {
                    errorMessage = "Schoen kan niet worden bewerkt, het is niet gevonden.";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Er is een fout opgetreden bij het opslaan: {ex.Message}";
            }
        }

        private void ReturnToOverview()
        {
            Navigation.NavigateTo("/shoeOverview"); // Terug naar de overzichtspagina
        }
    }
}