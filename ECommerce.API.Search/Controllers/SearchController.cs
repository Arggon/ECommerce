using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Search.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController(ISearchService searchService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SearchAsync(SearchTerm term)
    {
        var result = await searchService.SearchAsync(term);
        return result.IsSuccess ? (IActionResult)Ok(result.SearchResults) : NotFound();
    }
}