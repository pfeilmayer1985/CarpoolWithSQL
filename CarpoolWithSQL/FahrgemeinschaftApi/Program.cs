using Swashbuckle.AspNetCore.Filters;
using TecAlliance.Carpool.Business;
using TecAlliance.Carpool.Business.Providers;
using TecAlliance.Carpool.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSingleton<ICarpoolsBusinessService, CarpoolsBusinessService>();
builder.Services.AddSingleton<ICarpoolsDataServiceSQL, CarpoolsDataServiceSQL>();
builder.Services.AddSingleton<IUsersDataServiceSQL, UsersDataServiceSQL>();
builder.Services.AddSingleton<IUserBusinessService, UserBusinessService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();
});
builder.Services.AddSingleton<CarpoolModelProvider>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<CarpoolModelProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
