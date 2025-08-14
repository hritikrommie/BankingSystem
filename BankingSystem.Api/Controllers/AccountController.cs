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
[Route("api/account/")]
public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;
    private readonly IUserService _useService;
    public AccountController(ILogger<AccountController> logger, IMapper mapper, IAccountService accountService, IUserService useService)
    {
        _mapper = mapper;
        _logger = logger;
        _accountService = accountService;
        _useService = useService;

    }

    /// <summary>
    /// This api is used to create accounts like Saving, Checking etc by users.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    [Authorize(Roles = "Customer")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount(CreateAccountRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(AccountController)} Method : {nameof(CreateAccount)} starts executing with request {request.ToJson()}");
        if (!ModelState.IsValid)
            throw new AppException("Please provide all the required details", 400);
        var user = await _useService.GetUser(request.UserId);
        if (user == null)
            throw new AppException("Invalid User.");

        var accountDto = _mapper.Map<AccountDtos>(request);
        accountDto.AccountNumber = "123456789";
        var account = await _accountService.CreateAccount(accountDto);
        var response = _mapper.Map<CreateAccountResponse>(account);
        _logger.LogInformation($"Class : {nameof(AccountController)} Method : {nameof(CreateAccount)} stops executing with response {response.ToJson()}");
        return Ok(response);
    }

    /// <summary>
    /// This api is used to get all accounts of an user.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    [Authorize(Roles = "Customer")]
    [HttpGet("{userid}")]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
    public async Task<IActionResult> GetAccounts(int userid, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(AccountController)} Method : {nameof(GetBalance)} starts executing with request {userid}");
        var response = await _accountService.GetAccounts(userid);
        _logger.LogInformation($"Class : {nameof(AccountController)} Method : {nameof(GetBalance)} stops executing with response {response.ToJson()}");
        return Ok(response);
    }

    /// <summary>
    /// This api is used by user to check their account balance/details.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    [Authorize(Roles = "Customer")]
    [HttpGet("{id}/balance")]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
    public async Task<IActionResult> GetBalance(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(AccountController)} Method : {nameof(GetBalance)} starts executing with request {id}");
        var response = await _accountService.GetAccount(id);
        if (response == null)
            throw new AppException("Invalid Account Id.");

        _logger.LogInformation($"Class : {nameof(AccountController)} Method : {nameof(GetBalance)} stops executing with response {response.ToJson()}");
        return Ok(response);
    }
}
