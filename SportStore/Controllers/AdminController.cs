using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.Data.Repositories.Interfaces;
using SportStore.Models;

namespace SportStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository _repository;
        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index()
        {
            return View(_repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _repository.SaveProductAsync(product);
                TempData["message"] = $"{product.Name} был сохранен";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit",new Product());
        }

        [HttpDelete]
        public IActionResult Delete(int productId)
        {
            var deletedProduct =  _repository.DeleteProduct(productId);
            if(deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} был удален";
            }
            return RedirectToAction("Index");
        }
    }
}
