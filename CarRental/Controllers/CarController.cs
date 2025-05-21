using System.Text.Json;
using AutoMapper;
using CarRental.Models.CarDto;
using CarRentalDB.Repositories.Interfaces;
using CarRentalModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers;

/// <summary>
/// the cars controller
/// </summary>
[ApiController]
[Route("[controller]")]
[Authorize]
public class CarController : ControllerBase
{
    private readonly ICarRepository _carRepository;
    private readonly ILogger<CarController> _logger;
    private readonly IMapper _mapper;
    private const int MaxPageSize = 20;
    /// <summary>
    /// Car controller constructor 
    /// </summary>
    /// <param name="carRepository">the car repository you want to use</param>
    /// <param name="logger">the logger you want to use either a custom or the system one</param>
    /// <param name="mapper">a mapper to map between data dto</param>
    public CarController(ICarRepository carRepository, ILogger<CarController> logger, IMapper mapper)
    {
        _carRepository = carRepository;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Get All Cars Paginated into pages
    /// </summary>
    /// <param name="search">a search criteria </param>
    /// <param name="filterQuery">a filtering criteria</param>
    /// <param name="pageNumber">the page number you want(default is 1)</param>
    /// <param name="pageSize">the page size you want (default is 10 and max is 20)</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarDto>>> Get(string? search, string? filterQuery, int pageNumber = 1, int pageSize = 10)
    {
        if (pageSize is < 1 or > MaxPageSize)
        {
            pageSize = MaxPageSize;
        }
        var (carEntity, paginationMetaData) = await _carRepository.GetAllAsync(search, filterQuery, pageNumber, pageSize);
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
        return Ok(_mapper.Map<IEnumerable<CarDto>>(carEntity));
    }
    /// <summary>
    /// Add a new Car (you must be an admin to use this)
    /// </summary>
    /// <param name="carCreationDto">the car info</param>
    /// <returns></returns>
    [Authorize(Policy = "MustBeAnAdmin")]
    [HttpPost]
    public async Task<ActionResult<CarDto>> Post(CarCreationDto carCreationDto)
    {
        var car = _mapper.Map<Car>(carCreationDto);
        var succeed = await _carRepository.AddAsync(car);
        if (!succeed)
        {
            return BadRequest();
        }
        await _carRepository.SaveChangesAsync();
        return Ok(_mapper.Map<CarDto>(car));
    }
}