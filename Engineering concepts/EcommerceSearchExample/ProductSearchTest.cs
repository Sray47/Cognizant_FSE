using System;
using System.Linq;

namespace EcommerceSearchExample
{
    public class ProductSearchTest
    {
        public static void Main(string[] args)
        {
            Product[] products = {
                new Product(101, "Laptop", "Electronics"),
                new Product(102, "Shirt", "Apparel"),
                new Product(103, "Book", "Books"),
                new Product(104, "Phone", "Electronics"),
                new Product(105, "Shoes", "Footwear")
            };
            int indexLinear = ProductSearch.LinearSearch(products, 104);
            Console.WriteLine($"Linear Search: Found at index {indexLinear}, Product: {(indexLinear != -1 ? products[indexLinear].ToString() : "Not Found")}");
            products = products.OrderBy(p => p.GetProductId()).ToArray();
            int indexBinary = ProductSearch.BinarySearch(products, 104);
            Console.WriteLine($"Binary Search: Found at index {indexBinary}, Product: {(indexBinary != -1 ? products[indexBinary].ToString() : "Not Found")}");
            Console.WriteLine();
            Console.WriteLine("Analysis:");
            Console.WriteLine("Linear Search Time Complexity: O(n)");
            Console.WriteLine("Binary Search Time Complexity: O(log n) (requires sorted array)");
            Console.WriteLine("Binary search is more suitable for large, sorted datasets due to its faster performance compared to linear search.");
        }
    }
}
