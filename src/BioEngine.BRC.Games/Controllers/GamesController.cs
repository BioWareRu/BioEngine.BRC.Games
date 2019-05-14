using BioEngine.BRC.Domain.Entities;
using BioEngine.Core.Repository;
using BioEngine.Core.Site;
using BioEngine.Core.Web;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.Games.Controllers
{
    [Route("/games")]
    public class GamesController : SectionController<Game>
    {
        public GamesController(BaseControllerContext<Game> context, PostsRepository postsRepository) : base(
            context, postsRepository)
        {
        }
    }
}
