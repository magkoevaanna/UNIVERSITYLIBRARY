using UniversityLibrary.Backend.Repositories;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("MySqlConnection")!;
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(); 


builder.Services.AddSingleton<IBookRepository>(new BookRepository(connectionString));
builder.Services.AddSingleton<IMemberRepository>(new MemberRepository(connectionString));
builder.Services.AddSingleton<IStatisticsRepository>(new StatisticsRepository(connectionString));

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();
app.MapControllers();
app.Run();


