using BioEngine.BRC.Domain.Entities;
using BioEngine.Core.Repository;
using BioEngine.Core.Site;
using BioEngine.Core.Web;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.Games.Controllers
{
    [Route("/developers")]
    public class DevelopersController : SectionController<Developer>
    {
        public DevelopersController(BaseControllerContext<Developer> context, PostsRepository postsRepository) : base(
            context, postsRepository)
        {
        }
    }
}
