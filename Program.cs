using CRUDOperationUsingWEBAPI.Context;
using CRUDOperationUsingWEBAPI.Data;
using CRUDOperationUsingWEBAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionSTring = builder.Configuration.GetConnectionString("StudentDB");
builder.Services.AddDbContext<StudentDBContext>(options => options.UseSqlServer(connectionSTring));
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddIdentity<Users,IdentityRole>().AddEntityFrameworkStores<StudentDBContext>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
