using HotChocolateDemo.Models;
using HotChocolateDemo.Services;

namespace HotChocolateDemo.GraphQL;

public class FootballQuery
{
    public async Task<IEnumerable<Player>> GetAllPlayersAsync(
        [Service] IPlayerService playerService)
    {
        return await playerService.GetAllPlayersAsync();
    }
}