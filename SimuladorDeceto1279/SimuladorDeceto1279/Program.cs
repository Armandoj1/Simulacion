var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configurar el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Redirigir la ruta raíz a SalaryCalculator.cshtml
app.MapGet("/", context =>
{
    context.Response.Redirect("/SalaryCalculator");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
