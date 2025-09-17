//using System.Text.Json.Serialization;
//using LibraryManagementAPI.Endpoints;
//using LibraryManagementAPI.EndPoints;
//using LibraryManagementAPI.Services;


//var builder=WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton<IAuthorService, AuthorService>();
//builder.Services.AddSingleton<IBookService, BookService>();
//builder.Services.AddSingleton<IBorrowService, BorrowService>();


//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//builder.Services.ConfigureHttpJsonOptions(options =>
//{
//    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//    options.SerializerOptions.WriteIndented = true;
//});

//var app = builder.Build();

//if(app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.MapAuthorEndpoints();
//app.MapBookEndpoints();
//app.MapBorrowEndpoints();



//app.Run();






using System.Text.Json.Serialization;
using LibraryManagementAPI.Endpoints;
using LibraryManagementAPI.EndPoints;
using LibraryManagementAPI.Services;
using Microsoft.EntityFrameworkCore;
using LibraryManagementAPI.Data;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    );

//registering services
//builder.Services.AddSingleton<IAuthorService, AuthorService>();
//builder.Services.AddSingleton<IBookService, BookService>();
//builder.Services.AddSingleton<IBorrowService, BorrowService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBorrowService, BorrowService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAuthorEndpoints();
app.MapBookEndpoints();
app.MapBorrowEndpoints();



app.Run();