using ApiCaller;
using ClassLibrary1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        // Constructor que recibe un ILogger para el registro de eventos
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        // Método asociado a solicitudes HTTP GET en la ruta base del controlador (/User)
        [HttpGet]
        // El parámetro 'u' se espera en el cuerpo de la solicitud (FromBody)
        public IEnumerable<Users> Get([FromBody] Users u)
        {
            // Se crea una instancia de la clase 'conexion'
            conexion sl = new conexion();

            // Se llama al método 'GetUsers' de la instancia 'sl', pasando el objeto 'u'
            IList<Users> us = sl.GetUsers(u);

            // Se devuelve la lista de usuarios obtenida*
            return us;
        }
    }

}
