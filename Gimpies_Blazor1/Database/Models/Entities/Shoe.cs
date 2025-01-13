namespace Gimpies_Blazor1.Database.Models.Entities
{
    public class Shoe
    {
        public int ShoeId { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int ColourId { get; set; }
        public Colour Colour { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
        public decimal Value { get; set; }
        public int Unit { get; set; }
        public bool isActive { get; set; } = true;
        public ICollection<SalesTransaction> SalesTransactions { get; set; } 

    }
}
