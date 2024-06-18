using QTask.Middelware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();
builder.Services.AddControllers().AddJsonOptions(Options => { Options.JsonSerializerOptions.PropertyNamingPolicy = null; });
builder.Services.AddSession(options => { options.IOTimeout = TimeSpan.FromMinutes(30); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseSession();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.Use(async (context, next) =>
{
	context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
	context.Response.Headers.Add("X-Frame-Options", "DENY");
	context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
	context.Response.Headers.Add("Referrer-Policy", "no-referrer");
	await next();
});

app.UseWhen(context => context.Request.Path.Value.ToLower().Contains("/api"), appBuilder => appBuilder.UseAPIAuthenticationMiddleware());
app.UseWhen(context => !context.Request.Path.Value.ToLower().Contains("/api"), appBuilder => appBuilder.UseAutharizationMiddelware());

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
