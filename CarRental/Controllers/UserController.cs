using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using CarRental.Models.UserDto;
using CarRentalDB.Repositories.Interfaces;
using CarRentalModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;
    private const int _maxPageSize = 20;
    private readonly string? _secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
    private readonly string? _issuer = Environment.GetEnvironmentVariable("ISSUER");
    private readonly string? _audience = Environment.GetEnvironmentVariable("AUDIENCE");
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="logger"></param>
    /// <param name="mapper"></param>
    public UserController(IUserRepository userRepository, ILogger<UserController> logger, IMapper mapper)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// Get all users paginated 
    /// </summary>
    /// <param name="email">the email of user you want</param>
    /// <param name="searchQuery">some search query if you want</param>
    /// <param name="pageNumber">the page number (default is 1)</param>
    /// <param name="pageSize">the page size you want (default is 10 and max is 20)</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(string? email, string? searchQuery, int pageNumber = 1, int pageSize = 10)
    {
        if (pageSize is > _maxPageSize or < 0)
        {
            pageSize = _maxPageSize;
        }
        var (userEntities, paginationMetaData) = await _userRepository.GetAllAsync(email, searchQuery, pageNumber, pageSize);
        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
        return Ok(_mapper.Map<IEnumerable<UserDto>>(userEntities));
    }
    /// <summary>
    /// Get a specific user using the user id 
    /// </summary>
    /// <param name="id">the user of the user you want </param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            _logger.LogError($"User with id: {id} was not found");
        }
        return Ok(_mapper.Map<UserDto>(user));
    }

    /// <summary>
    /// Create a user
    /// </summary>
    /// <param name="userDto">the user info</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> PostUser(UserCreationDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var isValid = await _userRepository.AddAsync(user);
        if (!isValid)
        {
            return BadRequest();
        }
        await _userRepository.SaveChangesAsync();
        return Ok();

    }

    /// <summary>
    /// Login controller
    /// </summary>
    /// <param name="email"> the email for the user that want to login</param>
    /// <param name="password"> the password for the user that want to login</param>
    /// <returns></returns>
    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(string email, string password)
    {
        var user = await _userRepository.ValidateCredentialsAsync(email, password);
        if (user == null)
        {
            return Unauthorized("Invalid credentials");
        }

        if (string.IsNullOrEmpty(_secretKey) || string.IsNullOrEmpty(_issuer) || string.IsNullOrEmpty(_audience))
        {
            return StatusCode(500, "JWT configuration is missing.");
        }

        var key = new SymmetricSecurityKey(Convert.FromBase64String(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim("UserId", user.UserId.ToString()),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role.ToString()),

        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(tokenString);
    }

    /// <summary>
    /// Use it to update the user role 
    /// </summary>
    /// <param name="id">the id of the user you want to udpate</param>
    /// <param name="userRole">the new role</param>
    /// <returns></returns>
    [Authorize(Policy = "MustBeAnAdmin")]
    [HttpPatch("/role/{id}")]
    public async Task<ActionResult> UpdateRole(int id, JsonPatchDocument<UserUpdateRoleDto> userRole)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            _logger.LogError($"User with id: {id} was not found");
            return NotFound("User not found");
        }
        UserUpdateRoleDto? updateRole = _mapper.Map<UserUpdateRoleDto>(user);
        userRole.ApplyTo(updateRole);
        if (!TryValidateModel(updateRole))
        {
            return BadRequest();
        }
        _mapper.Map(updateRole, user);
        await _userRepository.SaveChangesAsync();
        return Ok();
    }
}