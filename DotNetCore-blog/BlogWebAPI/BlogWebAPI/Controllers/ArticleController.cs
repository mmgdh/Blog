using ArticleService.WebAPI.Controllers.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ArticleController : Controller
    {

        public ArticleController()
        {

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add(string Json)
        {
            return Ok();
        }
    }
}
