using TranKhanhDuyen_Bai2.Models;

namespace TranKhanhDuyen_Bai2.Repositories
{
    public class MockProductRepository : IProductRepository
    {
        private readonly List<Product> _products;
        public MockProductRepository()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop1", Price = 1000, Description = "A high-end laptop"},
                new Product { Id = 2, Name = "Laptop2", Price = 1000, Description = "A high-end laptop"},
                new Product { Id = 3, Name = "Laptop3", Price = 1000, Description = "A high-end laptop"},
                new Product { Id = 4, Name = "Laptop4", Price = 1000, Description = "A high-end laptop"},
                new Product { Id = 5, Name = "Laptop5", Price = 1000, Description = "A high-end laptop"},
            };
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }
        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }
        public void Add(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
        }
        public void Update(Product product)
        {
            var index = _products.FindIndex(p => p.Id == product.Id);
            if (index != -1)
            {
                _products[index] = product;
            }
        }
        public void Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}
