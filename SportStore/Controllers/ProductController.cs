using Microsoft.AspNetCore.Mvc;
using SportStore.Data.Repositories.Interfaces;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public int PageSize = 4;
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
            
            
        }

        public IActionResult Index()
        {
            return View(_repository.Products);
        }
        public ViewResult List(string category,int productPage = 1)
        {
            var productListViewModel = new ProductListViewModel()
            {
                Products = _repository.Products.Where(p=> category==null || p.Category==category)
                                                .OrderBy(p => p.ProductId)
                                                .Skip((productPage - 1) * PageSize)
                                                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category==null ? _repository.Products.Count()
                                            : _repository.Products.Where(p=>p.Category==category).Count()
                },
                CurrentCategory = category
            };
       
            return View(productListViewModel);
        }
    }
}
