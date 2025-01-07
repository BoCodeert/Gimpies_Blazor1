using Gimpies_Blazor1.Database.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gimpies_Blazor1.Components.Pages.Seller
{
    public partial class SellShoes
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
    }
}