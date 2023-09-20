using Microsoft.EntityFrameworkCore;
using PruebaApi.Models;

namespace PruebaApi.Data
{
    public class UsuariosDB : DbContext
    {

        public UsuariosDB(DbContextOptions<UsuariosDB> options) : base(options) 
        { 
        
        }

        public DbSet<Usuarios> Usuarios => Set<Usuarios>();

    }
}
