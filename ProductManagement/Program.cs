using Microsoft.AspNetCore.WebSockets;
using ProductManagement.Core.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

#region Injections
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
#endregion

#region MediatR
var handlerAssemblies = AppDomain.CurrentDomain
    .GetAssemblies()
    .Where(a => a.FullName.Contains("ProductManagement.Core"))
    .ToArray();

builder.Services.AddMediatR(cfg =>
{
    //cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssemblies(handlerAssemblies);

});
#endregion


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
