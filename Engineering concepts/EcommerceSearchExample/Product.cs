namespace EcommerceSearchExample
{
    public class Product
    {
        private int productId;
        private string productName;
        private string category;
        public Product(int productId, string productName, string category)
        {
            this.productId = productId;
            this.productName = productName;
            this.category = category;
        }
        public int GetProductId() { return productId; }
        public string GetProductName() { return productName; }
        public string GetCategory() { return category; }
        public override string ToString()
        {
            return $"Product{{productId={productId}, productName='{productName}', category='{category}'}}";
        }
    }
}
