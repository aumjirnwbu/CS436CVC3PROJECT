namespace CS436CVC3PROJECT.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Sale { get; set; }  // SaleD1, SaleD2, SaleD3, SaleD4
    }

}
