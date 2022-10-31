using GlobalErrorApp.Configurations;
using GlobalErrorApp.UseCase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// builder.Services.AddFluentValidationAutoValidation();
// builder.Services.AddScoped<IValidator<AddUserCommand>, AddUserCommandValidator>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFindUserUseCase, FindUserUseCase>();
builder.Services.AddScoped<IAddUserUseCase, AddUserUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.AddGlobalErrorHandler();

app.Run();
