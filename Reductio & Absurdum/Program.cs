using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Reductio___Absurdum;

List<Product> products = new List<Product>()
{
    new Product()
    {
        Name = "Love Potion",
        Price = 20.00M,
        Sold = false,
        ProductTypeId = 1,
        DateStocked = new DateTime(2023, 04, 12)
    },
    new Product ()
    {
        Name = "Spellcasting Wand",
        Price = 40.00M,
        Sold = false,
        ProductTypeId = 2,
        DateStocked = new DateTime(2023, 05, 20)
    },
    new Product ()
    {
        Name = "Special Witch Robe",
        Price = 150.00M,
        Sold = false,
        ProductTypeId = 3,
        DateStocked = new DateTime(2023, 07, 11)
    },
    new Product ()
    {
        Name = "Good Fortune Elixir",
        Price = 16.000M,
        Sold = false,
        ProductTypeId = 4,
        DateStocked = new DateTime(2023, 08, 17)
    }
};

List<ProductType> productTypes = new List<ProductType>()
                                 {
    new ProductType()
    {
        Id = 1,
        Name = "Potion"
     },
    new ProductType()
    {
        Id = 2,
        Name = "Wand"
    },
    new ProductType()
    {
        Id = 3,
        Name = "Apparel"
    },
    new ProductType()
    {
        Id = 4,
        Name = "Enchanted Objects"
    }
};

string greeting = @" Welcome to Reductio & Absurdum! Your number one stop for magic goods.";

Console.WriteLine(greeting);

string choice = null!;
while (choice != "0")

{
    Console.WriteLine(@"Choose an option:
                        0. Exit
                        1. View All Products
                        2. Add A Product
                        3. Delete A Product
                        4. Update A Product
                        5. Search Products By Type
                        6. View All Available Products");

    choice = Console.ReadLine()!;
    switch (choice)
    {
        case "0":
        Console.WriteLine("Thank you, Goodbye!"); ;
            break;
        case "1":
        ViewProducts();
            break;
        case "2":
        CreateProduct();
            break;
        case "3":
        DeleteProduct();
            break;
         case "4":
         UpdateProduct();
            break;
         case "5":
          SearchProductType();
            break;
        case "6":
            ListAllAvailableProducts();
            break;

    }
}

//view products
void ViewProducts()
{
    Console.WriteLine("Products:");
    foreach (Product product in products)
    {
        Console.WriteLine($"{products.IndexOf(product) + 1}. {ProductDetails(product)}");
    }
}

//view product types
void ListAllProductTypes()
{
    Console.WriteLine("Product Types: ");
    foreach (ProductType productType in productTypes)
    {
        Console.WriteLine($"{productTypes.IndexOf(productType) + 1}. {productType.Name}");
    }
}

//view all available products
void ListAllAvailableProducts()
{
    List<Product> unsoldProducts = products.Where(p => !p.Sold).ToList();
    foreach (Product product in unsoldProducts)
    {
        Console.WriteLine($"{unsoldProducts.IndexOf(product) + 1}. {ProductDetails(product)}");
    }
}

//create product
void CreateProduct()
{ 
Console.WriteLine("Enter the product name:");
string nameToAdd = Console.ReadLine().Trim();
Console.WriteLine("Enter the price of your product:");
decimal price = Convert.ToDecimal(Console.ReadLine());
Console.WriteLine("Enter the product id #: ");
    ListAllProductTypes();
    int addProductTypeId = int.Parse(Console.ReadLine());

    Product productToAdd = new Product();
    productToAdd.Name = nameToAdd;
    productToAdd.Price = price;
    productToAdd.ProductTypeId = addProductTypeId;
    productToAdd.Sold = false;

    products.Add(productToAdd);

    Console.WriteLine("Congratulations! You have added your product to inventory.");
}
string ProductDetails(Product product)
{
    string currentProductType = productTypes
        .Where(productType => productType.Id == product.ProductTypeId)
        .Select(productType => productType.Name)
        .FirstOrDefault() ?? "";

    string productString = $"{product.Name} ({currentProductType}) ${product.Price}, Days On Shelf: {product.DaysOnShelf}";

    return productString;
}

//delete product
void DeleteProduct()
{
    Console.WriteLine("Enter the number of the product to delete:");
    ViewProducts();

    int userInput = int.Parse(Console.ReadLine());

    Product deleteAProduct = products.FirstOrDefault((product) => userInput == products.IndexOf(product) + 1); if (deleteAProduct != null)
    {
        products.Remove(deleteAProduct);
    }

    Console.WriteLine("You have successfully removed this item.");
}

//update products
void UpdateProduct()
    {
        Console.WriteLine("Enter the number of the product to update it:");
        ViewProducts();
        int userInput = int.Parse(Console.ReadLine());

        int chosenProductIndex = userInput - 1;
        
        if (chosenProductIndex >= 0 && chosenProductIndex < products.Count)
    {
        Product chosenProduct = products[chosenProductIndex];

        Console.WriteLine("Which detail of the product would you want to update?");
        Console.WriteLine($"1. {nameof(Product.Name)}");
        Console.WriteLine($"2. {nameof(Product.Price)}");
        Console.WriteLine($"3. {nameof(Product.Sold)}");
        Console.WriteLine($"4. {nameof(Product.ProductTypeId)}");
        int productPropertyInt = int.Parse(Console.ReadLine());
        switch (productPropertyInt)
        {
            case 1:
                Console.WriteLine("Type the updated product name: ");
                string updatedName = Console.ReadLine();
                chosenProduct.Name = updatedName;
                break;
            case 2:
                Console.WriteLine("Type the updated product price:");
                decimal updatedPrice = decimal.Parse(Console.ReadLine());
                chosenProduct.Price = updatedPrice;
                break;
            case 3:
                Console.WriteLine("Has the product been sold? Type 'yes' or 'no'");
                string updatedSold = Console.ReadLine();
                chosenProduct.Sold = updatedSold.ToLower() == "yes";
                break;

            case 4:
                Console.WriteLine("Type in the number of the product type you wish to change your product to: ");
                ListAllProductTypes();
                int updatedProductTypeId = int.Parse(Console.ReadLine());
                  chosenProduct.ProductTypeId = updatedProductTypeId;
                break;
                    default:
                        Console.WriteLine("Not an option. Back to the main menu!");
                        return;
                }

        Console.WriteLine($"Congratulations! You've successfully updated {ProductDetails(chosenProduct)}");
    }
    else
    {
        Console.WriteLine("Invalid product number. Please try again.");
    }
}

//search products
void SearchProductType()
{
    Console.WriteLine("Enter the number of the types of products to show:");
    ListAllProductTypes();

    int userInput = int.Parse(Console.ReadLine());

    List<Product> productsChosenType = products.Where(p => p.ProductTypeId == userInput).ToList();
    Console.WriteLine(@$"Here's a list of product matching product type:");
    foreach (Product product in productsChosenType)
    {
        Console.WriteLine(@$"{ProductDetails(product)}");
    }
}