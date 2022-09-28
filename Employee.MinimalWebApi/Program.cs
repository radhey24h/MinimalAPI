using Employee.Dal;
using Emp = Employee.Models.Entities;
using Employee.Dal.Repository.AsyncCommonRepository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConStr")));

builder.Services.AddScoped<IAsyncCommonRepository<Emp.Employee>, AsyncCommonRepository<Emp.Employee>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/employees", async (IAsyncCommonRepository<Emp.Employee> _repostiory) =>
{
    var employees = await _repostiory.GetAll();
    if (employees.Count <= 0)
    {
        return Results.NotFound();
    }
    return Results.Ok(employees);
}).WithName("GetAll")
 .Produces<IEnumerable<Emp.Employee>>(StatusCodes.Status200OK)
 .Produces(StatusCodes.Status404NotFound);

app.MapGet("/api/employees/{id}", async (int id, IAsyncCommonRepository<Emp.Employee> _repostiory) =>
{
    var employee = await _repostiory.GetDetails(id);
    return employee == null ? Results.NotFound() : Results.Ok(employee);
})
 .Produces<Emp.Employee>(StatusCodes.Status200OK)
 .Produces(StatusCodes.Status404NotFound)
 .WithName("Details");

app.MapPost("/api/employees", async (Emp.Employee employee, IAsyncCommonRepository<Emp.Employee> _repostiory) =>
{
    var output = await _repostiory.Insert(employee);
    if (output != null)
    {
        return Results.Created($"/api/employees/{output.EmployeeId}", output);
    }
    return Results.BadRequest();
}).WithName("Create")
 .Produces<Emp.Employee>(StatusCodes.Status201Created)
 .Produces(StatusCodes.Status400BadRequest);

app.MapPut("/api/employees", async (Emp.Employee employee, IAsyncCommonRepository<Emp.Employee> _repostiory) =>
{
    var output = await _repostiory.Update(employee);
    if (output != null)
    {
        return Results.NoContent();
    }
    return Results.BadRequest();
}).WithName("Update")
 .Produces<Emp.Employee>(StatusCodes.Status204NoContent)
 .Produces(StatusCodes.Status400BadRequest);

app.MapDelete("/api/employees/{id}", async (int id, IAsyncCommonRepository<Emp.Employee> _repostiory) =>
{
    var output = await _repostiory.Delete(id);
    if (output == null)
    {
        return Results.NotFound();
    }
    return Results.NoContent();
}).WithName("Delete")
 .Produces<Emp.Employee>(StatusCodes.Status204NoContent)
 .Produces(StatusCodes.Status404NotFound);

app.Run();