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
[Route("api/transactions/")]
public class TransactionController : Controller
{
    private readonly ILogger<TransactionController> _logger;
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;
    private readonly IUserService _useService;
    private readonly ITransactionService _transactionService;
    public TransactionController(ILogger<TransactionController> logger, IMapper mapper, IAccountService accountService, IUserService useService, ITransactionService transactionService)
    {
        _mapper = mapper;
        _logger = logger;
        _accountService = accountService;
        _useService = useService;
        _transactionService = transactionService;
    }

    /// <summary>
    /// This api is used to deposit money to user account.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    [Authorize(Roles = "Customer")]
    [HttpPost("deposit")]
    public async Task<IActionResult> DepositMoney(DepositMoneyRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(TransactionController)} Method : {nameof(DepositMoney)} starts executing with request {request.ToJson()}");
        if (!ModelState.IsValid)
            throw new AppException("Please provide all the details.", 400);
        var transactionDto = _mapper.Map<TransactionDtos>(request);
        var txnResponse = await _transactionService.Deposit(transactionDto);
        var response = new TransactionResponse()
        {
            Message = "Transaction Posted Successfully.",
            TransactionId = txnResponse.Id,
            NewBalance = txnResponse.NewBalance
        };
        _logger.LogInformation($"Class : {nameof(TransactionController)} Method : {nameof(DepositMoney)} stops executing with response {response.ToJson()}");
        return Ok(response);
    }

    /// <summary>
    /// This api is used to withdraw money from user's account.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    [Authorize(Roles = "Customer")]
    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawMoney(WithdrawMoneyRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(TransactionController)} Method : {nameof(WithdrawMoney)} starts executing with request {request.ToJson()}");
        if (!ModelState.IsValid)
            throw new AppException("Please provide all the details.", 400);
        var transactionDto = _mapper.Map<TransactionDtos>(request);
        var txnResponse = await _transactionService.Withdraw(transactionDto);
        var response = new TransactionResponse()
        {
            Message = "Transaction Posted Successfully.",
            TransactionId = txnResponse.Id,
            NewBalance = txnResponse.NewBalance
        };
        _logger.LogInformation($"Class : {nameof(TransactionController)} Method : {nameof(WithdrawMoney)} stops executing with response {response.ToJson()}");
        return Ok(response);
    }

    /// <summary>
    /// This api is used to transfer money from one account to other.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="AppException"></exception>
    [Authorize(Roles = "Customer")]
    [HttpPost("transfer")]
    public async Task<IActionResult> TransferMoney(TransferMoneyRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(TransactionController)} Method : {nameof(DepositMoney)} starts executing with request {request.ToJson()}");
        if (!ModelState.IsValid)
            throw new AppException("Please provide all the details.", 400);
        var transactionDto = _mapper.Map<TransactionDtos>(request);
        var txnResponse = await _transactionService.Transfer(transactionDto);
        var response = new TransactionResponse()
        {
            Message = "Transaction Posted Successfully.",
            TransactionId = txnResponse.Id,
            NewBalance = txnResponse.NewBalance
        };
        _logger.LogInformation($"Class : {nameof(TransactionController)} Method : {nameof(DepositMoney)} stops executing with response {response.ToJson()}");
        return Ok(response);
    }

    /// <summary>
    /// This api is used to get transaction history of an account.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "Customer")]
    [HttpGet("{id}")]
    public async Task<IActionResult> TransactionHistory(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Class : {nameof(TransactionController)} Method : {nameof(TransactionHistory)} starts executing with request {id}");
        var transactions = await _transactionService.GetTransactionByAccountId(id);
        var response = _mapper.Map<List<TransactionHistoryResponse>>(transactions);
        _logger.LogInformation($"Class : {nameof(TransactionController)} Method : {nameof(TransactionHistory)} stops executing with response {response.ToJson()}");
        return Ok(response);
    }
}
