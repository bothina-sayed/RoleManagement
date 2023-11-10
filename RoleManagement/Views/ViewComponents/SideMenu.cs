using Microsoft.AspNetCore.Mvc;

namespace RoleManagement.Views.ViewComponents
{
    public class SideMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            var x = 1;
            return View();
        }
    }
}
