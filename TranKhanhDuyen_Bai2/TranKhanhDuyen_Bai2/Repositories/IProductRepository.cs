using TranKhanhDuyen_Bai2.Models;

namespace TranKhanhDuyen_Bai2.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll(); //ienumerable chi lay du lieu, khong thay doi du lieu
        Product GetById(int id); //lay du lieu dua vao id
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}
