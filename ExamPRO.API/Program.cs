using ExamPRO.API.Settings;
using ExamPRO.API.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// קביעת ההגדרות של MongoDB מה-AppSettings
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// הוספת שירותים ל-API
builder.Services.AddSingleton<ExamService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<StudyMaterialService>();
builder.Services.AddSingleton<SubjectCategoryService>();
builder.Services.AddControllers();  // הוספת תמיכה בבקרים (Controllers)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// הפעלת Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();  // חיבור הבקרים למערכת

app.Run();
