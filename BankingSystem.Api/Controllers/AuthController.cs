using AutoMapper;
using BankingSystem.Api.Models.Request;
using BankingSystem.Api.Models.Response;
using BankingSystem.Core.Common;
using BankingSystem.Core.Dtos;
using BankingSystem.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthController> _logger;
    private readonly IMapper _mapper;
    public AuthController(IUserService userService, ITokenService tokenService, ILogger<AuthController> logger, IMapper mapper)
    {
        _userService = userService;
        _tokenService = tokenService;
        _logger = logger;
        _mapper = mapper;
    }

    /// <summary>
    /// This api is used to login.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(AuthController)} Method : {nameof(Login)} starts executing with request {request.ToJson()}");

        if (!ModelState.IsValid)
        {
            _logger.LogTrace("Bad Request : {modelstate}", ModelState);
            throw new AppException("Please provide all the details.", 400);
        }

        var userDto = await _userService.GetUser(request.CustomerId);
        if (userDto == null)
        {
            _logger.LogInformation("There is no user in the system with customer id : , {customerid}", request.CustomerId);
            throw new AppException("Please check customerid and password.");
        }

        var checkPassword = await _userService.ValidatePassword(userDto, request.Password);
        if(!checkPassword)
        {
            _logger.LogInformation("Password did not match.");
            throw new AppException("Please check customerid and password.");
        }

        var tokenDto = await _tokenService.GetToken(userDto);
        var response = _mapper.Map<UserLoginResponse>(tokenDto);
        response.CustomerId = request.CustomerId;
        response.Name = userDto.Name!;

        _logger.LogInformation($"Class : {nameof(AuthController)} Method : {nameof(Login)} stops executing with response {response.ToJson()}");
        return Ok(response);
    }

    /// <summary>
    /// This api is used to register a new user.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(AuthController)} Method : {nameof(Register)} starts executing with request {request.ToJson()}");

        if (!ModelState.IsValid)
        {
            _logger.LogTrace("Bad Request : {modelstate}", ModelState);
            throw new AppException("Please provide all the details.", 400);
        }

        var userDto = _mapper.Map<UserDtos>(request);
        var addedUser = await _userService.CreateUser(userDto);
        var response = _mapper.Map<UserRegisterResponse>(addedUser);
        _logger.LogInformation($"Class : {nameof(AuthController)} Method : {nameof(Register)} stops executing with response {response.ToJson()}");
        return Ok(response);
    }

    /// <summary>
    /// This api is used to get details of all users, Only Admin can access.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles ="Admin")]
    [HttpGet("users")]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(AuthController)} Method : {nameof(GetAllUsers)} starts executing.");
        var response = await _userService.GetAllUsers();
        _logger.LogInformation($"Class : {nameof(AuthController)} Method : {nameof(GetAllUsers)} stops executing with response {response.ToJson()}");
        return Ok(response);
    }
}
