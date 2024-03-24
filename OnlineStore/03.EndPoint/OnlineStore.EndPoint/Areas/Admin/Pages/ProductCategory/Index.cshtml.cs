using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductManagement.Application.IServices;

namespace OnlineStore.EndPoint.Areas.Admin.Pages.ProductCategory
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryService _productCategoryService;
        public IndexModel(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public void OnGet()
        {
            //_productCategoryService.FindAll()
        }
    }
}
