
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using TodoApi;
using System.Diagnostics;
var builder = WebApplication.CreateBuilder(args);

// CORS הוספת שירותי 

builder.Services.AddCors(options =>
 { 
    options.AddPolicy("AllowSpecificOrigin", 
// הוספת מדיניות CORS
// 
builder => { builder.WithOrigins("https://todolist-18cd.onrender.com","http://localhost:5000") 
// החלף בכתובת האתר שאתה רוצה לאפשר 
.AllowAnyHeader() 
.AllowAnyMethod(); }); });

// קישור מסד נתונים ל-MySQL
builder.Services.AddDbContext<ToDoDbContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    
    new MySqlServerVersion(new Version(8, 0, 40)))); 
    
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
        return Results.Ok(new { message = "Task with id ${id} deleted successfully" });
    }
    return Results.NotFound(new { message = "Task not found" });
});


app.MapGet( " /" , () => "AuthServer API is runing");
app.Run();

