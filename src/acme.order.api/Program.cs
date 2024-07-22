using acme.order.api.Middleware;
using acme.order.service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IJsonToXmlAdapter, JsonToXmlAdapter>();
builder.Services.AddTransient<IXmlToJsonAdapter, XmlToJsonAdapter>();
builder.Services.AddTransient<IFacade, Facade>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder => builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Capturar excepciones
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("MyPolicy");

app.Run();
