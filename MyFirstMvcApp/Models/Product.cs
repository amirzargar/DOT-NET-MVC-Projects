using System.ComponentModel.DataAnnotations;


namespace MyFirstMvcApp.Models
{
   

    public class Product
    {
        
        [Required(ErrorMessage ="Name is Required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string? Name { get; set; }
        
        [Required(ErrorMessage = "Price is Required")]
        [Range(1, 100000, ErrorMessage = "Price must be between 1 and 100000.")]
        public double Price { get; set; } 

        
        [Required(ErrorMessage = "Quantity is Required")]
        [Range(1, 10000, ErrorMessage = "Quantity must be between 1 and 10000.")]
        public int Quantity { get; set; }

    }
}
