using UniversityLibrary.Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("MySqlConnection")!;
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); 


builder.Services.AddSingleton<IBookRepository>(new BookRepository(connectionString));

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors();
app.Run();


