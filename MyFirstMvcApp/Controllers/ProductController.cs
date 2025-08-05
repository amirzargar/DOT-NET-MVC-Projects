using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyFirstMvcApp.Data; // ✅ Add this to use the Data namespace
using MyFirstMvcApp.Models; // ✅ Add this to use the Product model
using X.PagedList; // ✅ Add this to use the PagedList extension for pagination


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


        public IActionResult Index( string? searchTerm , string? sortOrder, int? page)
        {
            
            var products = _context.Products.AsQueryable(); // ✅ Use the database context to get products as a queryable collection

            if (!string.IsNullOrEmpty(searchTerm)) // Check if the search term is provided
            {
                products = products.Where(p => p.Name.Contains(searchTerm));
                ViewBag.SearchTerm = searchTerm; // Filter products by name
            }


            //sort logic by name and price

            ViewBag.NameSort= sortOrder == "name_asc"? "name_desc" : "name_asc"; // Toggle sort order for name
            ViewBag.PriceSort = sortOrder == "price_asc" ? "price_desc" : "price_asc"; // Toggle sort order for price

            products = sortOrder switch
            {
                "name_asc" => products.OrderBy(p => p.Name), // Sort by name ascending
                "name_desc" => products.OrderByDescending(p => p.Name), // Sort by name descending
                "price_asc" => products.OrderBy(p => p.Price), // Sort by price ascending
                "price_desc" => products.OrderByDescending(p => p.Price), // Sort by price descending
                _ => products // Default case, no sorting applied
            };

            int pageSize = 5;            // ✅ Set the number of products per page
            int pageNumber = page ?? 1;  // ✅ Get the current page number, defaulting to 1 if not provided
            return View(products.ToPagedList(pageNumber, pageSize));  // ✅ Return the paginated list of products to the view


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