// <auto-generated />
using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using static MudBlazor.Colors;
using static MudBlazor.Icons.Custom;
using System.Drawing;

namespace Gimpies_Blazor1.Components.Pages.Dialogs
{
    public partial class ConfirmDialog
    {
        [Parameter] public Shoe Shoe { get; set; }
        [Parameter] public string ButtonText { get; set; }
        [Parameter] public string ActionType { get; set; }
        [Parameter] public int Quantity { get; set; }
        [Parameter] public Shoe newShoe { get; set; }
        [Parameter] public int? shoeToDelete { get; set; }
        [Parameter] public Shoe shoeToEdit { get; set; }
        [Parameter] public User newUser { get; set; }
        [Parameter] public int? userToDelete { get; set; }
        [Parameter] public int userToEdit { get; set; }

        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; } // Gebruik MudDialogInstance

        private List<Role> roles;
                private List<Shoe> shoes;
        private List<Brand> brands;
        private List<Model> models;
        private List<Colour> colours;
        private List<Database.Models.Entities.Size> sizes;

        protected override async Task OnInitializedAsync()
        {
            roles = await DbContext.Roles.ToListAsync();
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

        private void Submit()
        {
            if (Quantity > 0)
            {
                MudDialogInstance.Close(DialogResult.Ok(Quantity));
            }

            else if (newShoe != null)
            {
                MudDialogInstance.Close(DialogResult.Ok(newShoe));
            }

            else if (shoeToDelete.HasValue)
            {
                MudDialogInstance.Close(DialogResult.Ok(shoeToDelete.Value));

            }
            else if (Shoe != null)
            {
                MudDialogInstance.Close(DialogResult.Ok(Shoe));
            }
            else if (shoeToEdit != null)
            {
                MudDialogInstance.Close(DialogResult.Ok(shoeToEdit));
            }
            else if (newUser != null)
            {
                MudDialogInstance.Close(DialogResult.Ok(newUser));
            }
            else if (userToDelete.HasValue)
            {
                MudDialogInstance.Close(DialogResult.Ok(userToDelete.Value));
            }
            else if (userToEdit != null)
            {
                MudDialogInstance.Close(DialogResult.Ok(userToEdit));
            }
            else
            {
                MudDialogInstance.Cancel();
            }
        }

        private void Cancel()
        {
            MudDialogInstance.Cancel();
        }
    }
}