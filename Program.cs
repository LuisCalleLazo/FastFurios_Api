
using FastFurios_Api.Config;
using FastFurios_Api.Services;
using FastFurios_Api.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CloudinarySetting>
  (builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddAutoMapper(typeof(AuthService).Assembly);

// TODO: Habilitando los Cors
string MyAllowSpecificOrigins = "AllowAnyOrigin";
builder.Services.ConfigureCors(MyAllowSpecificOrigins);

// TODO: Cargando la injeccion de dependencias
LoadDependecyInjection.LoadServices(builder.Services);
LoadDependecyInjection.LoadRepositories(builder.Services);


// TODO: Habilitando el contexto de la base de datos
builder.Services.ConfigureContext(builder);

// TODO: Authentication
builder.Services.ConfigureAuth(builder);

  
// TODO: Endpoints
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Swagger with google authentication
builder.Services.ConfigureSwagger();

var app = builder.Build();

// TODO: Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseWebSockets();
app.UseCors(MyAllowSpecificOrigins);
 
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();