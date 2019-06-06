
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphqlCoreDemo.Infrastructure.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string Barcode { get; set; }
        public string Title { get; set; }
        public decimal SellingPrice { get; set; }

        public int StoreId { get; set; }

        [ForeignKey("StoreId")]
        public Store Store { get; set; }
    }
}
