using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TranKhanhDuyen_Bai2.Models;
using TranKhanhDuyen_Bai2.Repositories;

namespace TranKhanhDuyen_Bai2.Controllers
{
    public class ProductController : Controller
    {
        //gọi micro service cần dùng
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            Console.WriteLine("ProductController initialized.");
            _categoryRepository = categoryRepository;
        }

        //hàm hiển thị danh sách các sản phẩm 
        public IActionResult Index()
        {
            //lấy ds sản phẩm
            var products = _productRepository.GetAll();
            return View(products);
        }

        //hàm thêm sản phẩm 
        //gđ 1 hiển thị giao diện 
        public IActionResult Add()
        {
            var categories = _categoryRepository.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }
        
        // Các actions khác như Display, Update, Delete
        // Display a single product
        public IActionResult Display(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Show the product update form
        public IActionResult Update(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Process the product update
        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }
        // Show the product delete confirmation
        public IActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Process the product deletion
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.Delete(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile imageUrl, List<IFormFile> imageUrls)
        {
            LoadCategories();
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            try
            {
                // Xử lý ảnh đại diện
                if (imageUrl != null)
                {
                    try
                    {
                        product.ImageUrl = await SaveImage(imageUrl);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("ImageUrl", "Lỗi tải ảnh đại diện: " + ex.Message);
                        return View(product);
                    }
                }
                // Xử lý danh sách ảnh khác
                if (imageUrls != null && imageUrls.Count > 0)
                {
                    product.ImageUrls = new List<string>();
                    foreach (var file in imageUrls)
                    {
                        try
                        {
                            product.ImageUrls.Add(await SaveImage(file));
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("ImageUrls", "Lỗi tải ảnh: " + ex.Message);
                            return View(product);
                        }
                    }
                }
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Có lỗi xảy ra: " + ex.Message);
                return View(product);
            }
        }
        private void LoadCategories()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAllCategories(), "Id", "Name");
        }


        private async Task<string> SaveImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                throw new ArgumentException("File tải lên không hợp lệ hoặc rỗng.");
            }

            // Kiểm tra định dạng tệp (chỉ JPG, JPEG, PNG)
            var permittedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            {
                throw new ArgumentException("Chỉ chấp nhận các tệp ảnh định dạng JPG, JPEG, PNG.");
            }

            // Kiểm tra kích thước tệp (giới hạn 5MB)
            const long maxSize = 5 * 1024 * 1024; // 5MB
            if (image.Length > maxSize)
            {
                throw new ArgumentException("Kích thước tệp quá lớn. Vui lòng tải lên ảnh có dung lượng dưới 5MB.");
            }
            try
            {
                var savePath = Path.Combine("wwwroot/images", image.FileName); // Thay đổi đường dẫn theo cấu hình của bạn
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return "/images/" + image.FileName; // Trả về đường dẫn tương đối
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
