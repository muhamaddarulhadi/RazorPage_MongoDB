using TestMongoDB.Services; //USING PANGGIL BALIK SERVICES CLASS TU
using TestMongoDB.Models;   //USING PANGGIL BALIK MODELS CLASS TU

var builder = WebApplication.CreateBuilder(args);

// Add services to the container for connection service
builder.Services.Configure<UUMDBsetting>(builder.Configuration.GetSection("UMMDBConnection"));      //mongo db service
builder.Services.AddSingleton<DepartmentsServices>();       //mongo db service

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
