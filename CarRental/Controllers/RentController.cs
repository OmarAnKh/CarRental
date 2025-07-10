using System.Text.Json;
using AutoMapper;
using CarRental.Models.RentDto;
using CarRentalDB.Repositories.Interfaces;
using CarRentalModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

/// <summary>
/// the rent controller 
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class RentController : ControllerBase
{
    private readonly IRentRepository _rentRepository;
    private readonly ILogger<RentController> _logger;
    private readonly IMapper _mapper;
    private const int MaxPageSize = 100;
    /// <summary>
    /// the Rent controller constructor 
    /// </summary>
    /// <param name="rentRepository">the rent repo you want to use </param>
    /// <param name="logger">a logger to log info</param>
    /// <param name="mapper">a mapper to map between data dto</param>
    public RentController(IRentRepository rentRepository, ILogger<RentController> logger, IMapper mapper)
    {
        _rentRepository = rentRepository;
        _logger = logger;
        _mapper = mapper;
    }
    /// <summary>
    /// Get rents paginated 
    /// </summary>
    /// <param name="search"></param>
    /// <param name="filterQuery"></param>
    /// <param name="pageNumber">page number you want (default is 1)</param>
    /// <param name="pageSize">page size you want (default is 10 and max is 20)</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RentDto>>> GetRents(string? search, string? filterQuery, int pageNumber = 1, int pageSize = 10)
    {
        if (pageSize is < 1 or > MaxPageSize)
        {
            pageSize = MaxPageSize;
        }
        var (rentEntities, paginationMetaData) = await _rentRepository.GetAllAsync(search, filterQuery, pageNumber, pageSize);
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
        return Ok(_mapper.Map<IEnumerable<RentDto>>(rentEntities));

    }
    /// <summary>
    /// Create a new Rent
    /// </summary>
    /// <param name="rent">the rent info</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<RentDto>> CreateRent(RentCreationDto rent)
    {
        var rentToCreate = _mapper.Map<Rent>(rent);
        rentToCreate.UserId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

        var isValid = await _rentRepository.AddAsync(rentToCreate);
        if (!isValid)
        {
            return BadRequest();
        }
        await _rentRepository.SaveChangesAsync();
        return Ok(_mapper.Map<RentDto>(rentToCreate));
    }
    /// <summary>
    /// cancel a rent 
    /// </summary>
    /// <param name="id">the rent id </param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> CancelRent(int id)
    {
        var isValid = await _rentRepository.CancelRent(id, Convert.ToInt32(User.FindFirst("UserId")?.Value));
        if (!isValid)
        {
            return BadRequest();
        }
        return NoContent();
    }
}