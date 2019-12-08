using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Core.LongTimeOperation.ViewComponent
{
    public class ProgressBarViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}