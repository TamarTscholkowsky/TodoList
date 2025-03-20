
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TodoApi;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// CORS הוספת שירותי 

builder.Services.AddCors(options =>
 { 
    options.AddPolicy("AllowSpecificOrigin", 
// הוספת מדיניות CORS
// 
builder => {
    builder.AllowAnyOrigin()

// החלף בכתובת האתר שאתה רוצה לאפשר 
.AllowAnyHeader() 
.AllowAnyMethod(); }); });

// קישור מסד נתונים ל-MySQL
builder.Services.AddDbContext<ToDoDbContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("ToDoListDB")??
 Environment.GetEnvironmentVariable("CONNECTIONSTRINGS_TODOLISTDB"),
    
    new MySqlServerVersion(new Version(8, 0, 40))));

//"ConnectionStrings": {
//    //"DefaultConnection": "server=bhktoolxbtabm2tfdrkm-mysql.services.clever-cloud.com;user=ubegousxff9p3rws;password=ubegousxff9p3rws;database=bhktoolxbtabm2tfdrkm"
//    //"DefaultConnection": "server=localhost;user=root;password=0527123469;database=items"
//    "Items": "Server=bhktoolxbtabm2tfdrkm-mysql.services.clever-cloud.com; Port=3306; Database=bhktoolxbtabm2tfdrkm; User Id=ubegousxff9p3rws;  Password=tHBSDY2Efpr23pgnl2DW;SslMode=Required;"




//  },
    
// הוספת שירותי Swagger 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
////

var app = builder.Build();
app.UseRouting(); 
app.UseCors("AllowSpecificOrigin");

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
//var baseUrl = builder.Configuration.GetSection("ApiConfig")["BaseUrl"];
//Console.WriteLine($"The API BaseUrl is: {baseUrl}");
var baseUrl = builder.Configuration.GetSection("ApiConfig")["BaseUrl"] ?? "Base URL not found!";
Console.WriteLine($"The API BaseUrl is: {baseUrl}");





app.MapGet("/time", () => DateTime.Now);

app.MapGet("/tasks", async (ToDoDbContext context) =>
{ 
    var tasks = await context.Tasks.ToListAsync();
   return Results.Ok(tasks);
}); 
// מחזיר את כל הרשומות
//הוספה
app.MapPost("/{name}", async (ToDoDbContext context, string name) =>
{
    var newTask = new Tasks
    {
        Name = name,
        IsComplete = false
    };
    context.Tasks.Add(newTask);
    await context.SaveChangesAsync();
    return Results.Created($"/tasks/{newTask.Id}", newTask);
    });

app.MapPut("/tasks/{id}/{isComplete}", async (ToDoDbContext context, int id, bool isComplete) =>
{
    var existingTask = await context.Tasks.FindAsync(id);
    if (existingTask != null)
    {
        existingTask.IsComplete = isComplete;
        await context.SaveChangesAsync();
        return Results.Ok(existingTask);
    }
      
    return Results.NotFound();
});


// // מסלול DELETE לנתיב "/tasks/{id}" למחיקת משימה 
app.MapDelete("/tasks/{id}", async (ToDoDbContext context, int id) =>
{ 
    var task = await context.Tasks.FindAsync(id);
    
    if (task != null)
    {
        context.Tasks.Remove(task);
        await context.SaveChangesAsync();
        return Results.Ok(new { message = $"Task with id {id} deleted successfully"});
    }
    return Results.NotFound(new { message = "Task not found" });
});


app.MapGet( "/" , () => "AuthServer API is runing");
app.Run();

