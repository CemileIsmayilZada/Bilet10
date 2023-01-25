using App.DataAccess.Contexts;
using App.DataAccess.Repositories.Implementations;
using App.DataAccess.Repositories.Interfaces;
using AppBusiness.Validations.Auth;
using AppBusiness.Validations.TeamMembers;
using AppBusiness.ViewModels.Auth;
using AppBusiness.ViewModels.TeamMemberrs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var conString = builder.Configuration["ConnectionStrings:default"];
builder.Services.AddDbContext<AppDbContex>(opt=>opt.UseSqlServer(conString));
builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
builder.Services.AddScoped<IValidator<UpdateTeamMemberVm>, UpdateTeamMemberValidator>();
builder.Services.AddScoped<IValidator<LoginTeamViewModel>, LoginValidator>();
builder.Services.AddScoped<IValidator<RegisterTeamViewModel>,RegisterValidator>();

builder.Services.AddValidatorsFromAssemblyContaining<UpdateTeamMemberValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
