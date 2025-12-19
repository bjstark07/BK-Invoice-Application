using BKInvoiceApplication.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddScoped<InvoicePdfService>();

// API Controllers (only if you actually have controllers)
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
	options.UseSqlServer(connectionString);
});

// OPTIONAL: if you want to change default root later, fix path here
// Note: your folder is /Invoices, not /Invoice
builder.Services.AddRazorPages()
	.AddRazorPagesOptions(options =>
	{
		// This line is NOT needed for dashboard flow; we will redirect in Index.cshtml.cs
		// Remove or correct if you keep it:
		// options.Conventions.AddPageRoute("/Invoices/Index", "");
	});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Endpoints
app.MapRazorPages();
app.MapControllers();

app.Run();
