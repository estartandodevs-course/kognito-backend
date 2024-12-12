using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Kognito.Usuarios.App.Infra.Data;

public class UsuarioContextFactory : IDesignTimeDbContextFactory<UsuarioContext>
{
    public UsuarioContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UsuarioContext>();
        
        optionsBuilder.UseSqlServer("Server=localhost;Database=KognitoDb;Trusted_Connection=True;MultipleActiveResultSets=true");

        return new UsuarioContext(optionsBuilder.Options);
    }
}