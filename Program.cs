using GeoLocationAPI.DBHandler;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

#region Application Scope & JWT Middleware
services.AddHttpContextAccessor();//TO Access HTTPContext from constructor.
services.AddTransient(typeof(IConnectionService), typeof(ConnectionService));
#endregion


builder.Services.AddControllers();
//Enable Cors
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//This line enables swagger in production as well.
app.UseSwagger();
app.UseSwaggerUI();


#region Enable CORS
//Ref: https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials
#endregion

#region Setting custom default page
//Specify the MyCustomPage1.html as the default page
DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
defaultFilesOptions.DefaultFileNames.Clear();
defaultFilesOptions.DefaultFileNames.Add("Swagger/Index.html");
//Setting the Default Files
app.UseDefaultFiles(defaultFilesOptions);
//Adding Static Files Middleware to serve the static files
app.UseStaticFiles();
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
