using TranKhanhDuyen_Bai2.Models;

namespace TranKhanhDuyen_Bai2.Repositories
{
    public class MockCategoryRepository: ICategoryRepository
    {
        private List<Category> categories;
        public MockCategoryRepository()
        {
            categories = new List<Category>
            {
                new Category { Id = 1, Name = "Laptop" },
                new Category { Id = 2, Name = "Desktop" }
                
            };
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return categories;
        }
        public Category GetById(int id)
        {
            return categories.FirstOrDefault(p => p.Id == id);
        }
        public void Add(Category category)
        {
            category.Id = categories.Max(p => p.Id) + 1;
            categories.Add(category);
        }
        public void Update(Category category)
        {
            var index = categories.FindIndex(p => p.Id == category.Id);
            if (index != -1)
            {
                categories[index] = category;
            }
        }
        public void Delete(int id)
        {
            var product = categories.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                categories.Remove(product);
            }
        }
    }
}
