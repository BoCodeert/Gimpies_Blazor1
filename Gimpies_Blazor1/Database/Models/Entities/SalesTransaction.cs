using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gimpies_Blazor1.Database.Models.Entities
{
    public class SalesTransaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int ShoeId { get; set; } // Foreign key naar Shoe
        public Shoe Shoe { get; set; }
        public int Quantity { get; set; } // Aantal verkochte paren schoenen
        public decimal Price { get; set; } // Prijs per paar schoenen
        public DateTime Date { get; set; } // Datum van de transactie
    }
}
