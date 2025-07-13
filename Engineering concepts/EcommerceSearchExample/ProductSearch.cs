using System;

namespace EcommerceSearchExample
{
    public class ProductSearch
    {
        public static int LinearSearch(Product[] products, int productId)
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].GetProductId() == productId)
                {
                    return i;
                }
            }
            return -1;
        }
        public static int BinarySearch(Product[] products, int productId)
        {
            int left = 0, right = products.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (products[mid].GetProductId() == productId)
                {
                    return mid;
                }
                else if (products[mid].GetProductId() < productId)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return -1;
        }
    }
}
