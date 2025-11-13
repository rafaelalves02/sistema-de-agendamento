using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SistemaDeAgendamento.Web.Models;

namespace SistemaDeAgendamento.Web.ViewComponents
{
    public class BrandingViewComponent : ViewComponent
    {
        private readonly BrandingOptions _brandingOptions;

        public BrandingViewComponent(IOptions<BrandingOptions> brandingOptions)
        {
            _brandingOptions = brandingOptions.Value;
        }

        public IViewComponentResult Invoke()
        {
            return View(_brandingOptions);
        }
    }
}

