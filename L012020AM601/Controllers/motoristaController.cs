using L012020AM601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L012020AM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class motoristaController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public motoristaController(restauranteContext _restauranteContexto)
        {
            _restauranteContext = _restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<motorista> listadomotorista = (from e in _restauranteContext.motorista select e).ToList();

            if (listadomotorista.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadomotorista);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            motorista? pedido = (from e in _restauranteContext.motorista
                               where e.motoristaId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpGet]
        [Route("Find/{nombre}")]
        public IActionResult FindByPedido(string nombre)
        {
            motorista? pedido = (from e in _restauranteContext.motorista
                               where e.nombreMotorista.Contains(nombre)
                               select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpGet]
        [Route("Find/{motorista}")]
        public IActionResult FindByMotorista(int motorista)
        {
            motorista? pedido = (from e in _restauranteContext.motorista
                               where e.motoristaId == motorista
                               select e).FirstOrDefault();
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        [HttpPost]
        [Route("Add")]

        public IActionResult Guardarpedido([FromBody] motorista pedido)
        {
            try
            {
                _restauranteContext.motorista.Add(pedido);
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

        public IActionResult Actualizarpedido(int id, [FromBody] motorista pedidoModificar)
        {
            motorista? pedidoActual = (from e in _restauranteContext.motorista
                                     where e.motoristaId == id
                                     select e).FirstOrDefault();

            if (pedidoActual == null)
            {
                return NotFound();
            }

            pedidoActual.nombreMotorista = pedidoModificar.nombreMotorista;
            pedidoActual.motoristaId = pedidoModificar.motoristaId;
         
            _restauranteContext.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(pedidoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Eliminarpedido(int id)
        {
            motorista? pedido = (from e in _restauranteContext.motorista
                               where e.motoristaId == id
                               select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            _restauranteContext.motorista.Attach(pedido);
            _restauranteContext.motorista.Remove(pedido);
            _restauranteContext.SaveChanges();

            return Ok(pedido);
        }

    }
}
