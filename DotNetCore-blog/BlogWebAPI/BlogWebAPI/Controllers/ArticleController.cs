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
        public async Task<ActionResult<string>> Index()
        {
            return Ok("11112222");
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add(string Json)
        {
            return Ok();
        }
    }
}
