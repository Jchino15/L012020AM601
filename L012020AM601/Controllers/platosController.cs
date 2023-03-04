using L012020AM601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L012020AM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class platosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public platosController(restauranteContext _restauranteContexto)
        {
            _restauranteContext = _restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<platos> listadoplatos = (from e in _restauranteContext.platos select e).ToList();

            if (listadoplatos.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoplatos);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            platos? pedido = (from e in _restauranteContext.platos
                               where e.platoId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpGet]
        [Route("Find/{valor}")]
        public IActionResult FindByPedido(decimal valor)
        {
            List<platos> platos = (from e in _restauranteContext.platos
                               where e.precio < valor
                               select e).ToList();

            if (platos == null)
            {
                return NotFound();
            }
            return Ok(platos);
        }


        [HttpPost]
        [Route("Add")]

        public IActionResult Guardarpedido([FromBody] platos pedido)
        {
            try
            {
                _restauranteContext.platos.Add(pedido);
                _restauranteContext.SaveChanges();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult Actualizarpedido(int id, [FromBody] platos pedidoModificar)
        {
            platos? pedidoActual = (from e in _restauranteContext.platos
                                     where e.platoId == id
                                     select e).FirstOrDefault();

            if (pedidoActual == null)
            {
                return NotFound();
            }

            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.nombrePlato = pedidoModificar.nombrePlato;
            pedidoActual.precio = pedidoModificar.precio;
            

            _restauranteContext.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(pedidoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Eliminarpedido(int id)
        {
            platos? pedido = (from e in _restauranteContext.platos
                               where e.platoId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            _restauranteContext.platos.Attach(pedido);
            _restauranteContext.platos.Remove(pedido);
            _restauranteContext.SaveChanges();

            return Ok(pedido);
        }


    }
}

