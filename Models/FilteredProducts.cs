namespace JoshuaWood_ST10296167_PROG7311_POE.Models
{
    public class FilteredProducts
    {
        public List<Product> Products { get; set; }
        public List<string> FarmerCodes { get; set; }
        public List<string> Categories { get; set; }

        public string? SelectedFarmerCode { get; set; }
        public string? SelectedCategory { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
