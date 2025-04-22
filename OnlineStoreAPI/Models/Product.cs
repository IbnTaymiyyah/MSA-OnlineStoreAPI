namespace OnlineStoreAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string Description { get; set; }
        public int Price { get; set; } 
        public int Count { get; set; }
        public Boolean Availability { get; set; }

    }
}
