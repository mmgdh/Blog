using EventBus;
using Microsoft.AspNetCore.Mvc;

namespace ArticleService.WebAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestController :Controller
    {
        private IEventBus eventBus;

        public TestController(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        [HttpPost]
        public int Test(string name)
        {
            eventBus.publish("FileUpload", name);
            return 123;
        }
    }
}
