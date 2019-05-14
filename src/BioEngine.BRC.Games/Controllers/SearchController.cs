using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BioEngine.BRC.Domain.Entities;
using BioEngine.Core.Entities;
using BioEngine.Core.Entities.Blocks;
using BioEngine.Core.Search;
using BioEngine.Core.Site.Controllers;
using BioEngine.Core.Web;
using Microsoft.AspNetCore.Mvc;

namespace BioEngine.BRC.Games.Controllers
{
    public class SearchController : BaseSearchController
    {
        public SearchController(BaseControllerContext context, IEnumerable<ISearchProvider> searchProviders) : base(
            context, searchProviders)
        {
        }

        [HttpGet("")]
        public override async Task<IActionResult> IndexAsync([FromQuery] string query, string block)
        {
            var viewModel = new SearchViewModel(GetPageContext(), query);

            if (!string.IsNullOrEmpty(query))
            {
                var hasBlock = !string.IsNullOrEmpty(block);
                var limit = hasBlock ? 0 : 5;
                if (!hasBlock || block == "games")
                {
                    var searchBlock = await BuildBlockAsync<Game>(query, limit, "Игры", "games");
                    if (searchBlock != null)
                    {
                        viewModel.AddBlock(searchBlock);
                    }
                }

                if (!hasBlock || block == "developers")
                {
                    var searchBlock = await BuildBlockAsync<Developer>(query, limit, "Разработчики", "developers");
                    if (searchBlock != null)
                    {
                        viewModel.AddBlock(searchBlock);
                    }
                }

                if (!hasBlock || block == "topics")
                {
                    var searchBlock = await BuildBlockAsync<Topic>(query, limit, "Темы", "topics");
                    if (searchBlock != null)
                    {
                        viewModel.AddBlock(searchBlock);
                    }
                }

                if (!hasBlock || block == "posts")
                {
                    var searchBlock = await BuildBlockAsync<Post>(query, limit, "Публикации", "posts");
                    if (searchBlock != null)
                    {
                        viewModel.AddBlock(searchBlock);
                    }
                }
            }

            return View("Index", viewModel);
        }

        [SuppressMessage("ReSharper", "Mvc.ActionNotResolved")]
        private async Task<SearchBlock> BuildBlockAsync<T>(string query, int limit, string blockTitle, string blockKey)
            where T : BaseEntity, IContentEntity
        {
            var entitiesCount = await CountEntitiesAsync<T>(query);
            if (entitiesCount > 0)
            {
                var entities = await SearchEntitiesAsync<T>(query, limit);
                var searchBlock = CreateSearchBlock(blockTitle,
                    new Uri(Url.Action("Index", "Search", new {query, block = blockKey}), UriKind.Relative),
                    entitiesCount,
                    entities, x => x.Title,
                    x => new Uri(x.PublicUrl, UriKind.Relative),
                    x => GetDescriptionFromHtml((x.Blocks.FirstOrDefault(b => b is TextBlock) as TextBlock)
                        ?.Data
                        .Text), x => x.DateUpdated);
                return searchBlock;
            }

            return null;
        }
    }
}
