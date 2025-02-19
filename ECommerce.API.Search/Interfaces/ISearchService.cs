using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces;

public interface ISearchService
{
    Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(SearchTerm term);
}