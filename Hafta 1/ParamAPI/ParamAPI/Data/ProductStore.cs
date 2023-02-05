using ParamAPI.Models.DTOs;

namespace ParamAPI.Data
{
    public class ProductStore
    {
        public static List<ProductDTO> productsList = new List<ProductDTO>
        {
            new ProductDTO() {Id=1, Name= "Laptop", Stock= 10, Price= 5000},
            new ProductDTO() {Id=2, Name= "Phone", Stock= 30, Price= 4000},
            new ProductDTO() {Id=3, Name= "Keyboard", Stock= 200, Price= 550},
            new ProductDTO() {Id=4, Name= "Mouse", Stock= 100, Price= 275},
            new ProductDTO() {Id=5, Name= "Fridge", Stock= 50, Price= 3030}

        };   
    }
}
