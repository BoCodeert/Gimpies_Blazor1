// <auto-generated />
using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using Size = Gimpies_Blazor1.Database.Models.Entities.Size;

namespace Gimpies_Blazor1.Components.Pages.Admin
{
    public partial class AddData
    {
        private MudForm brandForm;
        private MudForm modelForm;
        private MudForm sizeForm;
        private MudForm colourForm;

        private string newBrandName;
        private string newModelName;
        private decimal newSizeValue;
        private string newColourName;

        private async Task AddBrand()
        {
            try
            {
                // Controleer op duplicaten
                if (await DbContext.Brands.AnyAsync(b => b.BrandName == newBrandName))
                {
                    Snackbar.Add("Merknaam bestaat al!", Severity.Warning);
                    return;
                }

                // Voeg toe als het geen duplicaat is
                var newBrand = new Brand { BrandName = newBrandName };
                DbContext.Brands.Add(newBrand);
                await DbContext.SaveChangesAsync();
                Snackbar.Add("Merk succesvol toegevoegd!", Severity.Success);
                newBrandName = string.Empty; // Reset inputveld
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Fout bij het toevoegen van merk: {ex.Message}", Severity.Error);
            }
        }

        private async Task AddModel()
        {
            try
            {
                // Controleer op duplicaten
                if (await DbContext.Models.AnyAsync(m => m.ModelName == newModelName))
                {
                    Snackbar.Add("Modelnaam bestaat al!", Severity.Warning);
                    return;
                }

                // Voeg toe als het geen duplicaat is
                var newModel = new Model { ModelName = newModelName };
                DbContext.Models.Add(newModel);
                await DbContext.SaveChangesAsync();
                Snackbar.Add("Model succesvol toegevoegd!", Severity.Success);
                newModelName = string.Empty; // Reset inputveld
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Fout bij het toevoegen van model: {ex.Message}", Severity.Error);
            }
        }

        private async Task AddSize()
        {
            try
            {
                // Controleer op duplicaten
                if (await DbContext.Sizes.AnyAsync(s => s.SizeValue == newSizeValue))
                {
                    Snackbar.Add("Maat bestaat al!", Severity.Warning);
                    return;
                }

                // Voeg toe als het geen duplicaat is
                var newSize = new Size { SizeValue = newSizeValue };
                DbContext.Sizes.Add(newSize);
                await DbContext.SaveChangesAsync();
                Snackbar.Add("Maat succesvol toegevoegd!", Severity.Success);
                newSizeValue = 0; // Reset inputveld
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Fout bij het toevoegen van maat: {ex.Message}", Severity.Error);
            }
        }

        private async Task AddColour()
        {
            try
            {
                // Controleer op duplicaten
                if (await DbContext.Colours.AnyAsync(c => c.ColourName == newColourName))
                {
                    Snackbar.Add("Kleurnaam bestaat al!", Severity.Warning);
                    return;
                }

                // Voeg toe als het geen duplicaat is
                var newColour = new Colour { ColourName = newColourName };
                DbContext.Colours.Add(newColour);
                await DbContext.SaveChangesAsync();
                Snackbar.Add("Kleur succesvol toegevoegd!", Severity.Success);
                newColourName = string.Empty; // Reset inputveld
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Fout bij het toevoegen van kleur: {ex.Message}", Severity.Error);
            }
        }
    }
}