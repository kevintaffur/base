using System.Text;
using front.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        // redirects
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.HttpContext.Response.Redirect("/Auth/Login");
                    context.HandleResponse();
                }
                return Task.CompletedTask;
            },
            OnForbidden = context =>
            {
                context.HttpContext.Response.Redirect("/Home/Index");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IProductoService, ProductoService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

// Middleware para pasar el token de sesión al header Authorization y que la autenticación lo encuentre.
app.Use(async (context, next) =>
{
    if (context.Request.Headers["Authorization"].ToString() == "" && context.Session.GetString("token") != null)
    {
        var token = context.Session.GetString("token");
        context.Request.Headers["Authorization"] = "Bearer " + token;
    }
    await next.Invoke();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Producto}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "auth",
    pattern: "{controller=Auth}/{action=Signup}/{id?}");

app.Run();
