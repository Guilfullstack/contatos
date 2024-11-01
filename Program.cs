using ControleContatos.Data;
using ControleContatos.Helper;
using ControleContatos.Repository;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //CONFIGURAÇÕES
        builder.Configuration.AddJsonFile("appsettings.json");
        //SERVIÇOS E INJEÇÃO DE DEPENDENCIA  
        builder.Services.AddControllersWithViews();
        //Configure DbContext to use SQL Server
        builder.Services.AddDbContext<BancoContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IContatoRepository,ContatoRepository>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
        builder.Services.AddScoped<ISecao,Secao>();
        builder.Services.AddScoped<IEmail,Email>();
        builder.Services.AddSession(o=>{
            o.Cookie.HttpOnly = true;
            o.Cookie.IsEssential = true;
        });
        var app = builder.Build();
        //Configura dependendo do ambiente
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }else{
            app.UseDeveloperExceptionPage();
        }
        app.UseRequestLocalization();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSession();
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Login}/{action=Index}/{id?}");

        app.Run();
    }
}
/*
COMANDOS:
dotnet tool install --global dotnet-ef : Garantir poder usar o comando dotnet ef
dotnet ef migrations add ContatosTabela --context BancoContext :Adicionar a migração
dotnet ef database update -c BancoContext : sob as alterações para o banco

*/