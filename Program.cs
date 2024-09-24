using ControleContatos.Data;
using ControleContatos.Repository;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //CONFIGURAÇÕES
        //adiciona o arquivo para ser lido para configuração
        builder.Configuration.AddJsonFile("appsettings.json");
        //SERVIÇOS E INJEÇÃO DE DEPENDENCIA  
        // Add services to the container.
        builder.Services.AddControllersWithViews();//.AddDataAnnotationsLocalization();//Adicionando dependencia de localização
        //Configure DbContext to use SQL Server
        builder.Services.AddDbContext<BancoContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<IContatoRepository,ContatoRepository>();//configurtaa para quando a interface for usada, utilizar a classe
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        var app = builder.Build();
        //Configura dependendo do ambiente
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }else{
            app.UseDeveloperExceptionPage();
        }
      //  app.UseRequestLocalization();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
/*
COMANDOS:
dotnet tool install --global dotnet-ef : Garantir poder usar o comando dotnet ef
dotnet ef migrations add ContatosTabela --context BancoContext :Adicionar a migração
dotnet ef database update -c BancoContext : sob as alterações para o banco

*/