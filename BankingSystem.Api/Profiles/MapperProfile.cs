using AutoMapper;
using BankingSystem.Api.Models.Request;
using BankingSystem.Api.Models.Response;
using BankingSystem.Core.Dtos;

namespace BankingSystem.Api.Profiles;

public class MapperProfile : Profile
{
    /// <summary>
    /// All the mapping profiles for auto mapper.
    /// </summary>
    public MapperProfile()
    {
        //Request Mapping
        CreateMap<UserLoginRequest, UserDtos>()
            .ForMember(s => s.Id, d => d.MapFrom(d => d.CustomerId));
        CreateMap<UserRegisterRequest, UserDtos>();
        
        //Response Mapping
        CreateMap<UserDtos, UserLoginResponse>();
        CreateMap<UserDtos, UserRegisterResponse>()
            .ForMember(s => s.CustomerId, d => d.MapFrom(d => d.Id));

        CreateMap<TokenDtos, UserLoginResponse>()
            .ForMember(s => s.TokenIssuedAt, d => d.MapFrom(d => d.IssuedAt)).
            ForMember(s => s.TokenExpiresAt, d => d.MapFrom(d => d.ExpiredAt));

        CreateMap<CreateAccountRequest, AccountDtos>();
        CreateMap<AccountDtos, CreateAccountResponse>()
            .ForMember(s => s.AccountId, d => d.MapFrom(d => d.Id));

        CreateMap<DepositMoneyRequest, TransactionDtos>();
        CreateMap<WithdrawMoneyRequest, TransactionDtos>();
        CreateMap<TransferMoneyRequest, TransactionDtos>();
        CreateMap<TransactionDtos, TransactionHistoryResponse>();
    }
}
