using HotChocolateDemo.Models;

namespace HotChocolateDemo.Services;

public interface IPositionService
{
    Task<IEnumerable<Position>> GetAllPositionsAsync();
}