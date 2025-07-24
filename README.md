# ASP.NET Core MVC – Product List (Day 6)

This is a beginner-friendly ASP.NET Core MVC app where I practiced how to:
- Use **strongly typed views**
- Pass a **list of custom objects** (Product) from Controller to View
- Dynamically render data in Razor View using `@foreach`
- Keep logic clean using the MVC pattern

## 🔧 Features
- Defined a `Product` model with `Id`, `Name`, `Price`, and `Description`
- Created dummy product list inside `ProductController.cs`
- Passed the list to `Views/Product/Index.cshtml`
- Displayed products in a tabular format using Razor syntax

## 🧠 Concepts Practiced
- MVC Pattern (Model-View-Controller)
- Strongly Typed Views in Razor
- `List<T>` and object passing from Controller to View
- View Rendering with `@foreach`

## 📁 Folder Structure
MyFirstMvcApp/
├── Controllers/
│ └── ProductController.cs
├── Models/
│ └── Product.cs
├── Views/
│ └── Product/
│ └── Index.cshtml
├── Program.cs
└── appsettings.json

## 📸 Screenshot
> *Sample table displaying products on browser.*

## 📚 Learning Log Entry
- ✅ Practiced MVC architecture deeper
- ✅ Created dummy product list in C#
- ✅ Rendered HTML table from backend list using Razor

## 🔗 GitHub
[View Code on GitHub](https://github.com/amirzargar/dotnet-learning-log-2025/tree/master/Day%206%20ASP.NET%20Product%20List)

