using Microsoft.EntityFrameworkCore;
using PresentConnectionInt.Services;
using PresentConnectionInt.Data;
using PresentConnectionInt.Models; // Ensure this matches your namespace for models

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register EF Core in-memory database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("QuizDb"));

// Register services
builder.Services.AddScoped<IQuizService, QuizService>();

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Allow frontend URL
              .AllowAnyHeader()  // Allow any header
              .AllowAnyMethod(); // Allow any method (GET, POST, etc.)
    });
});

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Quizzes.AddRange(new[]
    {
        new Quiz { Id = 1, Question = "What is 2 + 2?", Type = "Radio", Options = new[] { "3", "4", "5" }, CorrectAnswers = new[] { "4" } },
        new Quiz { Id = 2, Question = "Which of the following are the colors of the flag of the United States?", Type = "Checkbox", Options = new[] { "Red", "Blue", "Green", "White" }, CorrectAnswers = new[] { "Red", "Blue", "White" } },
        new Quiz { Id = 3, Question = "What is the capital of France?", Type = "Radio", Options = new[] { "Berlin", "Madrid", "Paris" }, CorrectAnswers = new[] { "Paris" } },
        new Quiz { Id = 4, Question = "Which planet is known as the Red Planet?", Type = "Radio", Options = new[] { "Earth", "Mars", "Venus" }, CorrectAnswers = new[] { "Mars" } },
        new Quiz { Id = 5, Question = "Enter the sum of 100 and 250:", Type = "Textbox", Options = new string[] { }, CorrectAnswers = new[] { "350" } },
        new Quiz { Id = 6, Question = "Enter the largest prime number below 100:", Type = "Textbox", Options = new string[] { }, CorrectAnswers = new[] { "97" } },
        new Quiz { Id = 7, Question = "Select the oceans of the world:", Type = "Checkbox", Options = new[] { "Atlantic", "Pacific", "Indian", "Arctic" }, CorrectAnswers = new[] { "Atlantic", "Pacific", "Indian", "Arctic" } },
        new Quiz { Id = 8, Question = "Select the types of triangles based on sides:", Type = "Checkbox", Options = new[] { "Equilateral", "Isosceles", "Scalene", "Pentagon" }, CorrectAnswers = new[] { "Equilateral", "Isosceles", "Scalene" } },
        new Quiz { Id = 9, Question = "Select the continents where the Amazon rainforest is located:", Type = "Checkbox", Options = new[] { "Africa", "Asia", "South America", "Australia" }, CorrectAnswers = new[] { "South America" } },
        new Quiz { Id = 10, Question = "Select the programming languages that are commonly used for web development:", Type = "Checkbox", Options = new[] { "Java", "Python", "JavaScript", "C#" }, CorrectAnswers = new[] { "JavaScript", "Python", "Java" } }

    });

    context.HighScores.AddRange(new[]
    {
        new HighScore { Id = 1, Email = "test@example.com", Score = 500, DateTime = DateTime.Now }
    });

    context.SaveChanges();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS middleware to apply the CORS policy
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
