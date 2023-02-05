namespace ParamAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
