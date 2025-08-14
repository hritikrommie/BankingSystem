using BankingSystem.Core;
using BankingSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

//Register Core Services
builder.Services.RegisterCoreServicesForApi(builder.Configuration);
//Register Db related services
builder.Services.RegisterInfraStructureServices();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
//Register Core Middlewares
app.RegisterCoreAppForApi();
app.RegisterInfraStructureApp();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
