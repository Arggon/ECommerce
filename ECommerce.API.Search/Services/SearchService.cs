using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Services;

public class SearchService : ISearchService
{
    public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(SearchTerm term)
    {
        await Task.Delay(1);
        return (true, new { Message = "Hello" });
    }
}