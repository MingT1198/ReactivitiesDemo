using Repository.SeedDatas;
using API.Extensions;
using System.Data;
using API.Filterware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => {
    options.Filters.Add<CustomExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//註冊擴充
builder.Services.RegisterServices(builder.Configuration);

//建立app
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

//執行migration
using var scope = app.Services.CreateScope();
var service = scope.ServiceProvider;
try
{
    //用entity
    // var context = service.GetRequiredService<SqliteDataContext>();
    // await context.Database.MigrateAsync();
    // await Seed.ActivitySeedData(context);

    //用Dapper
    var connection = service.GetRequiredService<IDbConnection>();
    await Seed.ActivitySeedData(connection);
}
catch (Exception ex)
{
    var logger = service.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during");
}

app.Run();
