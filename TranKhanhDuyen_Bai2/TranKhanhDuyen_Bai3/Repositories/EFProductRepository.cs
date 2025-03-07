﻿using Microsoft.EntityFrameworkCore;
using TranKhanhDuyen_Bai3.Models;

namespace TranKhanhDuyen_Bai3.Repositories
{
    public class EFProductRepository: IProductRepository
    {
        //khai báo context là nguyên bảng database
        private readonly ApplicationDbContext _context;

        //public context để dùng
        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //implement các hàm trong IProductRepository
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.Products
            .Include(p => p.Category) // Include thông tin về category
            .ToListAsync();
        }

        //async và await để đánh dấu xử lý bất đồng bộ 
        public async Task<Product> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.Products.Include(p =>
           p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}

