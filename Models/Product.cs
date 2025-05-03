using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JoshuaWood_ST10296167_PROG7311_POE.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(10)]
        public string ProductCode { get; set; }

        [ForeignKey("FarmerCode")]
        [StringLength(10)]
        public string FarmerCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(20)]
        public string Category { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }


    }
}
