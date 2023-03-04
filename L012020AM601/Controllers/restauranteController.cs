using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L012020AM601.Models;
using Microsoft.EntityFrameworkCore;

namespace L012020AM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class restauranteController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public restauranteController(restauranteContext restauranteContext) 
        {
            restauranteContext = restauranteContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<clientes> listadoClientes = (from e in _restauranteContext.clientes select e). ToList ();

            if (listadoClientes.Count() == 0)
            {
                return NotFound ();
            }

            return Ok (listadoClientes);
        }
    }
}
