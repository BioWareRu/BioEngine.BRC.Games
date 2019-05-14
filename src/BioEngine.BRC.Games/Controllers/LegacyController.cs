using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.Games.Controllers
{
    public class LegacyController : Controller
    {
        [HttpGet("/rss")]
        [HttpGet("/news/rss.xml")]
        [HttpGet("/news/rss")]
        public IActionResult RssOld()
        {
            return RedirectToActionPermanent("RssAsync", "Posts");
        }
    }
}
