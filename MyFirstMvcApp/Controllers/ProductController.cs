using Microsoft.AspNetCore.Mvc;
using MyFirstMvcApp.Data; // ✅ Add this to use the Data namespace
using MyFirstMvcApp.Models; // ✅ Add this to use the Product model

namespace MyFirstMvcApp.Controllers
{
    public class ProductController : Controller
    {
        //removed this static list and used AppDbcontext instead
        /* ✅ This is a static list to hold products in memory for demonstration purposes
        private static List<Product> _products = new List<Product>();*/

        private readonly AppDbContext _context; // ✅ Use dependency injection to access the database context

        // ✅ Constructor injection of AppDbContext
        public ProductController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            
            var products = _context.Products.ToList(); // ✅ Get list of products from DB
            return View(products); // ✅ Pass the correct model


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
                _context.Products.Add(product);                  // Add the product to the database context
                _context.SaveChanges();                 // Save changes to the database
                TempData["Success"] = "Product created successfully!"; // Store a success message in TempData
                return RedirectToAction("Success", product);            // Redirect to the Success action with the product data
            }
            
            return View(product); // If the model state is invalid, return the view with validation errors

        }

        public IActionResult Success(Product product)
        {
           
            return View(product); // ✅ Return the view with the product data
        }

        //Get edit product

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id); // Find the product by ID
            if (product == null)
            {
                return NotFound(); // Return 404 if product not found
            }
            return View(product); // Return the view with the product data
        }

        //post edit product

        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid) // Check if the model state is valid
            {
                _context.Products.Update(product); // Update the product in the database context
                _context.SaveChanges(); // Save changes to the database
                TempData["Message"] = "Product updated successfully!"; // Store a success message in TempData
                return RedirectToAction("Index"); // Redirect to the Index action
            }
            return View(product); // If the model state is invalid, return the view with validation errors
        }


        //Get Delete product

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product=_context.Products.FirstOrDefault(p => p.Id == id );   // Find the product by ID
            if (product == null)
            {
                return NotFound();
            }
            return View(product);     // Return the view with the product data

        }


        //post Delete product

        [HttpPost, ActionName("Delete")]                  //Use ActionName to specify the action name for the POST method
        [ValidateAntiForgeryToken]                        // Validate the anti-forgery token to prevent CSRF attacks
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                TempData["Message"] = "Product deleted successfully!";
            }

            return RedirectToAction(nameof(Index));               // Redirect to the Index action after deletion
        }





    }
}
// ✅ This code defines a ProductController for managing products in an ASP.NET Core MVC application.