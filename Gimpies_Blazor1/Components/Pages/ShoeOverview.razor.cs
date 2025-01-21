using Gimpies_Blazor1.Components.Pages.Dialogs;
using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace Gimpies_Blazor1.Components.Pages
{
    public partial class ShoeOverview
    {
        private List<Shoe> shoes;
        private List<Brand> brands;
        private List<Model> models;
        private List<Colour> colours;
        private List<Database.Models.Entities.Size> sizes;
        private string? errorMessage;
        private Shoe shoeToDelete;
        private Shoe shoeToEdit;  // De instantie van Shoe die bewerkt moet worden
        private Shoe newShoe = new Shoe()
        {
            Brand = new Brand(),
            Model = new Model(),
            Colour = new Colour(),
            Size = new Database.Models.Entities.Size()
        };

        protected override async Task OnInitializedAsync()
        {
            await LoadShoes();
            await LoadBrands();
            await LoadModels();
            await LoadColours();
            await LoadSizes();
        }

        private async Task LoadShoes()
        {
            shoes = await DbContext.Shoes
                .Where(s => s.isActive)
                .Include(s => s.Brand)
                .Include(s => s.Model)
                .Include(s => s.Colour)
                .Include(s => s.Size)
                .ToListAsync();
        }

        private async Task LoadBrands()
        {
            brands = await DbContext.Brands.ToListAsync();
        }

        private async Task LoadModels()
        {
            models = await DbContext.Models.ToListAsync();
        }

        private async Task LoadColours()
        {
            colours = await DbContext.Colours.ToListAsync();
        }

        private async Task LoadSizes()
        {
            sizes = await DbContext.Sizes.ToListAsync();
        }

        private async Task OpenAddShoeDialog()
        {
            var parameters = new DialogParameters
            {
                {"ButtonText", "Toevoegen" },
                {"ActionType", "AddShoe" },
                {"newShoe", newShoe }
            };

            var dialog = DialogService.Show<ConfirmDialog>("Schoen toevoegen", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                newShoe = (Shoe)result.Data;
                await HandleAddShoe();
            }
        }
        private async Task HandleAddShoe()
        {

            // Controleer op duplicaten
            var duplicate = await DbContext.Shoes.AnyAsync(s =>
                s.Brand.BrandName.Replace(" ", "") == newShoe.Brand.BrandName.Replace(" ", "") &&
                s.Model.ModelName.Replace(" ", "") == newShoe.Model.ModelName.Replace(" ", "") &&
                s.Colour.ColourName.Replace(" ", "") == newShoe.Colour.ColourName.Replace(" ", "") &&
                s.Size.SizeValue.ToString().Replace(" ", "") == newShoe.Size.SizeValue.ToString().Replace(" ", ""));

            if (duplicate)
            {
                var existingShoe = await DbContext.Shoes
                    .FirstOrDefaultAsync(s =>
                    s.Brand.BrandName == newShoe.Brand.BrandName &&
                    s.Model.ModelName == newShoe.Model.ModelName &&
                    s.Colour.ColourName == newShoe.Colour.ColourName &&
                    s.Size.SizeValue == newShoe.Size.SizeValue);

                if (existingShoe != null)
                {
                    // Als de schoen inactief is, maak het dan actief
                    if (existingShoe.isActive == false)
                    {
                        existingShoe.isActive = true;
                        DbContext.Update(existingShoe); // Update de bestaande schoen in de database
                        await DbContext.SaveChangesAsync();
                        shoes = await DbContext.Shoes
                            .Where(s => s.isActive)
                            .Include(s => s.Brand)
                            .Include(s => s.Model)
                            .Include(s => s.Colour)
                            .Include(s => s.Size)
                            .ToListAsync();// Sla de wijziging op in de database
                        Snackbar.Add("Schoen geheractiveerd!", Severity.Success);
                    }

                }
                Snackbar.Add("Een schoen met dezelfde specificaties bestaat al!", Severity.Error);
                return;
            }

            // Voeg de nieuwe schoen toe
            DbContext.Shoes.Add(newShoe);
            await DbContext.SaveChangesAsync();

            // Navigeren terug naar het overzicht
            Navigation.Refresh(forceReload: true);

            Snackbar.Add("Schoen toegevoegd!", Severity.Success);

        }

        private async Task OpenDeleteShoeDialog(Shoe shoe)
        {
            shoeToDelete = shoe;

            var parameters = new DialogParameters
            {
                { "Shoe", shoeToDelete },
                { "ButtonText", "Verwijderen" },
                { "ActionType", "DeleteShoe" }
            };

            var dialog = DialogService.Show<ConfirmDialog>("Schoen verwijderen", parameters);
            var result = await dialog.Result;

            if (!result.Canceled) // Verwijder de schoen als de gebruiker bevestigt
            {
                await DeleteShoe(shoeToDelete);
            }
        }
        private async Task DeleteShoe(Shoe shoe)
        {

            shoe.isActive = false;

            DbContext.Shoes.Update(shoe);
            await DbContext.SaveChangesAsync();
            shoes = await DbContext.Shoes
                .Where(s => s.isActive)
                .Include(s => s.Brand)
                .Include(s => s.Model)
                .Include(s => s.Colour)
                .Include(s => s.Size)
                .ToListAsync(); // Verfris de lijst met schoenen na verwijderen
            Snackbar.Add("Schoen verwijderd!", Severity.Warning);

        }

        private async Task OpenSellShoesDialog(Shoe shoe)
        {
            var parameters = new DialogParameters<ConfirmDialog>
            {
                { "Shoe", shoe }, // Zorg ervoor dat de parameter exact overeenkomt
                {"ButtonText", "Verkopen" },
                {"ActionType", "SellShoe" }
            };

            var options = new DialogOptions { CloseButton = true, FullWidth = true };


            var dialog = DialogService.Show<ConfirmDialog>("Schoenen verkopen", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var quantity = (int)result.Data;
                await HandleSellShoes(shoe, quantity);
            }
        }

        private async Task HandleSellShoes(Shoe shoe, int quantity)
        {

            if (quantity > shoe.Unit)
            {
                Snackbar.Add($"Er zijn niet genoeg schoenen op voorraad. Er zijn slechts {shoe.Unit} paar schoenen beschikbaar.", Severity.Warning);
                return;
            }

            try
            {
                // Voeg het aantal schoenen toe aan de voorraad
                shoe.Unit -= quantity;

                DbContext.Shoes.Update(shoe);

                var saleTransaction = new SalesTransaction
                {
                    ShoeId = shoe.ShoeId,
                    Quantity = quantity,
                    Price = shoe.Value,  // De prijs per paar schoen
                    Date = DateTime.Now
                };
                DbContext.SalesTransactions.Add(saleTransaction);

                await DbContext.SaveChangesAsync();

                // Optioneel: een notificatie tonen
                Snackbar.Add($"Je hebt {quantity} paar schoenen verkocht voor €{quantity * shoe.Value}.", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Er is een fout opgetreden: {ex.Message}", Severity.Error);
            }
        }

        private async Task OpenBuyShoesDialog(Shoe shoe)
        {
            var parameters = new DialogParameters<ConfirmDialog>
            {
                { "Shoe", shoe }, // Zorg ervoor dat de parameter exact overeenkomt
                {"ActionType", "BuyShoe" },
                {"ButtonText", "Inkopen" }

            };

            var options = new DialogOptions { CloseButton = true, FullWidth = true };
            var dialog = DialogService.Show<ConfirmDialog>("Schoenen inkopen", parameters);
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
                Snackbar.Add($"Je hebt {quantity} paar schoenen ingekocht.", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Er is een fout opgetreden: {ex.Message}", Severity.Error);
            }
        }

        private async Task OpenEditShoeDialog(Shoe shoe)
        {
            shoeToEdit = shoe;

            var parameters = new DialogParameters
                {
                    { "shoeToEdit", shoeToEdit },
                    { "ActionType", "EditShoe" },
                    { "ButtonText", "Bewerken" }
                };

            var options = new DialogOptions { CloseButton = true, FullWidth = true };
            var dialog = DialogService.Show<ConfirmDialog>("Schoenen bewerken", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {

                // Controleer wat er wordt teruggegeven: Shoe of ID?
                if (result.Data is Shoe editedShoe)
                {
                    await HandleEditShoe(editedShoe); // Gebruik een unieke naam hier
                }
                else if (result.Data is int shoeId) // Als alleen een ID wordt geretourneerd
                {
                    var existingShoe = await DbContext.Shoes
                        .Include(s => s.Brand)
                        .Include(s => s.Model)
                        .Include(s => s.Colour)
                        .Include(s => s.Size)
                        .FirstOrDefaultAsync(s => s.ShoeId == shoeId);

                    if (existingShoe != null)
                    {
                        await HandleEditShoe(existingShoe); // Geen conflict met unieke naam
                    }
                }
            }
        }

        private async Task HandleEditShoe(Shoe existingShoe)
        {
            try
            {
                var duplicate = await DbContext.Shoes.AnyAsync(s =>
                    s.Brand.BrandName.Replace(" ", "") == existingShoe.Brand.BrandName.Replace(" ", "") &&
                    s.Model.ModelName.Replace(" ", "") == existingShoe.Model.ModelName.Replace(" ", "") &&
                    s.Colour.ColourName.Replace(" ", "") == existingShoe.Colour.ColourName.Replace(" ", "") &&
                    s.Size.SizeValue.ToString().Replace(" ", "") == existingShoe.Size.SizeValue.ToString().Replace(" ", "") &&
                    s.ShoeId != existingShoe.ShoeId); // Zorg dat het duplicaat een ander ID heeft

                if (duplicate)
                {
                    var existingDuplicateShoe = await DbContext.Shoes.FirstOrDefaultAsync(s =>
                        s.Brand.BrandName == existingShoe.Brand.BrandName &&
                        s.Model.ModelName == existingShoe.Model.ModelName &&
                        s.Colour.ColourName == existingShoe.Colour.ColourName &&
                        s.Size.SizeValue == existingShoe.Size.SizeValue &&
                        s.ShoeId != existingShoe.ShoeId);

                    if (existingDuplicateShoe != null && !existingDuplicateShoe.isActive)
                    {
                        existingDuplicateShoe.isActive = true;
                        DbContext.Update(existingDuplicateShoe);
                        await DbContext.SaveChangesAsync();
                        shoes = await DbContext.Shoes
                            .Where(s => s.isActive)
                            .Include(s => s.Brand)
                            .Include(s => s.Model)
                            .Include(s => s.Colour)
                            .Include(s => s.Size)
                            .ToListAsync();

                        Snackbar.Add("Schoen geheractiveerd!", Severity.Success);
                        return;
                    }

                    Snackbar.Add("Een schoen met dezelfde specificaties bestaat al!", Severity.Error);
                    return;
                }
                DbContext.Update(existingShoe);
                await DbContext.SaveChangesAsync();
                shoes = await DbContext.Shoes
                    .Where(s => s.isActive)
                    .Include(s => s.Brand)
                    .Include(s => s.Model)
                    .Include(s => s.Colour)
                    .Include(s => s.Size)
                    .ToListAsync();

                Snackbar.Add("Schoen succesvol bijgewerkt!", Severity.Success);
            }
            catch (Exception ex)
            {
                Snackbar.Add($"Er is een fout opgetreden: {ex.Message}", Severity.Error);
            }
        }
        private string GetStockClass(int unit)
        {
            if (unit == 0)
            {
                return "shoe-item-outofstock";
            }
            else if (unit > 0 && unit <= 5)
            {
                return "shoe-item-lowstock";
            }
            else
            {
                return "shoe-item";
            }
        }
    }
}

