using MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace MVC.DAOs
{
    public class UsuariosDAO : IUsuariosDAO
    {
        private readonly HttpClient _httpClient;
        string url = $"https://localhost:7089/";

        public UsuariosDAO(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Usuarios>> ListarTodosAsync()
        {
            var respuesta = await _httpClient.GetAsync(url + "usuarios");

            if (respuesta.IsSuccessStatusCode)
            {
                var usuarios = await respuesta.Content.ReadFromJsonAsync<List<Usuarios>>();
                return usuarios;
            }
            else
            {
                throw new Exception("No se pudo obtener la lista de usuarios");
            }
        }

        public async Task<Usuarios> ObtenerPorIdAsync(int id)
        {
            var respuesta = await _httpClient.GetAsync(url + "usuarios/" + id);

            if (respuesta.IsSuccessStatusCode)
            {
                var usuario = await respuesta.Content.ReadFromJsonAsync<Usuarios>();
                return usuario;
            }
            else
            {
                throw new Exception($"No se pudo obtener el usuario con el id {id}");
            }
        }

        public async Task GuardarAsync(Usuarios usuario)
        {

            string data = JsonConvert.SerializeObject(usuario);
            StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");

            var respuesta = await _httpClient.PostAsync(url + "usuarios/", contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                throw new Exception("No se pudo guardar el usuario");
            }
        }

        public async Task EditarAsync(Usuarios usuario)
        {
            string data = JsonConvert.SerializeObject(usuario);
            StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");

            var respuesta = await _httpClient.PutAsync(url + "usuarios/" + usuario.Id, contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                throw new Exception("No se pudo editar el usuario");
            }
        }

        public async Task EliminarAsync(int id)
        {
            var respuesta = await _httpClient.DeleteAsync(url + "usuarios/" + id);

            if (!respuesta.IsSuccessStatusCode)
            {
                throw new Exception($"No se pudo eliminar el usuario con el id {id}");
            }
        }
    }


}
