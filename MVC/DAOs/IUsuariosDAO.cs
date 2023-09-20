using MVC.Models;

namespace MVC.DAOs
{
    public interface IUsuariosDAO
    {
        Task<List<Usuarios>> ListarTodosAsync();
        Task<Usuarios> ObtenerPorIdAsync(int id);
        Task GuardarAsync(Usuarios usuario);
        Task EditarAsync(Usuarios usuario);
        Task EliminarAsync(int id);
    }
}
