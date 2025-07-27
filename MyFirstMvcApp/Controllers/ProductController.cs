using Microsoft.AspNetCore.Mvc;
using MyFirstMvcApp.Models; // ✅ Add this to use the Product model

namespace MyFirstMvcApp.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "This Text is Passed from Controller of Product(ControllerProduct.cs)!";
            ViewBag.Message2 = "Welcome Amir!";
            ViewBag.Date = DateTime.Now.ToShortDateString() + "  " + DateTime.Now.ToShortTimeString();

            /* Create a sample product & setting that inside the models product.cs 
            var product = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 999.99,
                    Description = "A high-performance laptop for all your computing needs."
                },
                new Product
                {
                    Id = 2,
                    Name = "Smartphone",
                    Price = 499.99,
                    Description = "A latest model smartphone with advanced features."
                },
                new Product
                {
                    Id = 3,
                    Name = "Headphones",
                    Price = 199.99,
                    Description = "Noise-cancelling headphones for an immersive audio experience."
                }

            };
            return View(product);   // ✅ Pass model to view*/

            return View(); // ✅ Return the view without passing any model
        }
    

    //[HttpGet] action to return the empty form
    [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost] action to handle form submission
        [HttpPost]
        public IActionResult Create(Product product)     // ✅ Use the Product model to receive form data
        {
            if (ModelState.IsValid)                      // Check if the model state is valid
            {
                ViewBag.Message = "Product Created Successfully!";
                return View("Success", product);       // Redirect to a success view after successful creation
            }
            return View(product);                      // If the model state is invalid, return the same view with the product data
        }
    }
}
