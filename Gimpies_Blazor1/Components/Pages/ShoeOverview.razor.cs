using Gimpies_Blazor1.Components.Pages.Dialogs;
using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace Gimpies_Blazor1.Components.Pages
{
    public partial class ShoeOverview
    {
        private List<Shoe> shoes;
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
            shoes = await DbContext.Shoes
                .Where(s => s.isActive)
                .Include(s => s.Brand)
                .Include(s => s.Model)
                .Include(s => s.Colour)
                .Include(s => s.Size)
                .ToListAsync();
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
                s.Brand.BrandName == newShoe.Brand.BrandName &&
                s.Model.ModelName == newShoe.Model.ModelName &&
                s.Colour.ColourName == newShoe.Colour.ColourName &&
                s.Size.SizeValue == newShoe.Size.SizeValue);

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
                errorMessage = "Een schoen met dezelfde specificaties bestaat al.";
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
                Snackbar.Add($"Er zijn niet genoeg schoenen op voorraad. Er zijn slechts {shoe.Unit} paar schoenen beschikbaar.", Severity.Error);
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
                Snackbar.Add($"Je hebt {quantity} paar schoenen verkocht voor {quantity * shoe.Value}.", Severity.Success);
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

            var parameters = new DialogParameters<ConfirmDialog>
            {
                { "Shoe", shoeToEdit }, // Zorg ervoor dat de parameter exact overeenkomt
                {"ActionType", "EditShoe" },
                {"ButtonText", "Bewerken" }
            };

            var options = new DialogOptions { CloseButton = true, FullWidth = true };
            var dialog = DialogService.Show<ConfirmDialog>("Schoenen bewerken", parameters);
            var result = await dialog.Result;

            if (!result.Canceled)
            {
                var updatedShoe = (Shoe)result.Data;
                await HandleEditShoe(updatedShoe);
            }

        }

        private async Task HandleEditShoe(Shoe updatedShoe)
        {
            // Controleer op duplicaten
            var duplicate = await DbContext.Shoes.AnyAsync(s =>
                s.Brand.BrandName == updatedShoe.Brand.BrandName &&
                s.Model.ModelName == updatedShoe.Model.ModelName &&
                s.Colour.ColourName == updatedShoe.Colour.ColourName &&
                s.Size.SizeValue == updatedShoe.Size.SizeValue);

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
                errorMessage = "Een schoen met dezelfde specificaties bestaat al.";
                return;
            }

            try
            {
                // Bijwerken van de schoen in de database
                if (updatedShoe != null)
                {
                    DbContext.Update(shoeToEdit);
                    await DbContext.SaveChangesAsync();
                    Navigation.Refresh(forceReload: true); // Navigeren naar de overzichtspagina na het opslaan
                    Snackbar.Add("Schoen bewerkt!", Severity.Success);
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
                return "";
            }
        }
    }
}

