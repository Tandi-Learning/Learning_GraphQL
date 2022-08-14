using HotChocolateDemo.Data;
using HotChocolateDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services;

public class PositionService : IPositionService
{
    private readonly FootballDbContext _context;
 
    public PositionService(FootballDbContext context)
    {
        _context = context;
    }
 
    public async Task<IEnumerable<Position>> GetAllPositionsAsync()
    {
        return await _context.Positions
            .ToListAsync();
    } 
}