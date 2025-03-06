using Microsoft.EntityFrameworkCore;
using TranKhanhDuyen_Bai3.Models;

namespace TranKhanhDuyen_Bai3.Repositories
{
    public class EFCategoryRepository: ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public EFCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.Categorys.ToListAsync();
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.Categorys.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Category category)
        {
            _context.Categorys.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category Category)
        {
            _context.Categorys.Update(Category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categorys.FindAsync(id);
            _context.Categorys.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
