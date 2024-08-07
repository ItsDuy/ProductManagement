namespace WriteFileProduct;
class Product
{
    public string ProductID { get; set; }
    public string ProductName { get; set; }
    public string Manufacturer { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return $"{ProductID},{ProductName},{Manufacturer},{Price},{Description}";
    }

    public static Product FromString(string productString)
    {
        var parts = productString.Split(',');
        return new Product
        {
            ProductID = parts[0],
            ProductName = parts[1],
            Manufacturer = parts[2],
            Price = decimal.Parse(parts[3]),
            Description = parts[4]
        };
    }
}


class Program
{
    static string filePath = @"C:\Users\Admin\OneDrive\Máy tính\CODING\2024-Y1\C#\Excercise\WriteFileProduct\product.csv";
    static void Main(string[] args)
    {

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Display Products");
                Console.WriteLine("3. Search Product");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;
                    case 2:
                        DisplayProducts();
                        break;
                    case 3:
                        SearchProduct();
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddProduct()
        {
            Product product = new Product();

            Console.Write("Enter Product ID: ");
            product.ProductID = Console.ReadLine();

            Console.Write("Enter Product Name: ");
            product.ProductName = Console.ReadLine();

            Console.Write("Enter Manufacturer: ");
            product.Manufacturer = Console.ReadLine();

            Console.Write("Enter Price: ");
            product.Price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Description: ");
            product.Description = Console.ReadLine();

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(product.ToString());
            }

            Console.WriteLine("Product added successfully!");
        }

        static void DisplayProducts()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No products found.");
                return;
            }

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Product product = Product.FromString(line);
                    Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Manufacturer: {product.Manufacturer}, Price: {product.Price}, Description: {product.Description}");
                }
            }
        }

        static void SearchProduct()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No products found.");
                return;
            }

            Console.Write("Enter Product ID to search: ");
            string searchID = Console.ReadLine();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                bool found = false;
                while ((line = sr.ReadLine()) != null)
                {
                    Product product = Product.FromString(line);
                    if (product.ProductID == searchID)
                    {
                        Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Manufacturer: {product.Manufacturer}, Price: {product.Price}, Description: {product.Description}");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Product not found.");
                }
            }
        }
    }
}
