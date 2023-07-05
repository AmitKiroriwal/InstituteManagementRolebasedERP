namespace InstituteManagement.Models.ViewModels
{
    public class PlansViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public List<string> Features { get; set;}
        public string SubscribeUrl { get; set; }

    }
}
