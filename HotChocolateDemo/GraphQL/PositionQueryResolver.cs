using HotChocolateDemo.Models;
using HotChocolateDemo.Services;

namespace HotChocolateDemo.GraphQL;

[ExtendObjectType("FootballQuery")]
public class PositionQueryResolver
{
	[GraphQLName("positions")]
    [GraphQLDescription("Positions API")]
	public async Task<IEnumerable<Position>> GetAllPositionsAsync(
		[Service] IPositionService positionService)
	{
		return await positionService.GetAllPositionsAsync();
	}
}