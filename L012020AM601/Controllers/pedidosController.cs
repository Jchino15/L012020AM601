using L012020AM601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L012020AM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pedidosController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public pedidosController(restauranteContext _restauranteContexto)
        {
            _restauranteContext = _restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<pedidos> listadopedidos = (from e in _restauranteContext.pedidos select e).ToList();

            if (listadopedidos.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadopedidos);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            pedidos? pedido = (from e in _restauranteContext.pedidos
                                where e.pedidoId == id
                                select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpGet]
        [Route("Find/{cliente}")]
        public IActionResult FindByPedido(int cliente)
        {
            pedidos? pedido = (from e in _restauranteContext.pedidos
                               where e.clienteId == cliente
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
            pedidos? pedido = (from e in _restauranteContext.pedidos
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

        public IActionResult Guardarpedido([FromBody] pedidos pedido)
        {
            try
            {
                _restauranteContext.pedidos.Add(pedido);
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

        public IActionResult Actualizarpedido(int id, [FromBody] pedidos pedidoModificar)
        {
            pedidos? pedidoActual = (from e in _restauranteContext.pedidos
                                      where e.clienteId == id
                                      select e).FirstOrDefault();

            if (pedidoActual == null)
            {
                return NotFound();
            }

            pedidoActual.pedidoId = pedidoModificar.pedidoId;
            pedidoActual.motoristaId = pedidoModificar.motoristaId;
            pedidoActual.clienteId = pedidoModificar.clienteId;
            pedidoActual.platoId = pedidoModificar.platoId;
            pedidoActual.cantidad = pedidoModificar.cantidad;
            pedidoActual.precio = pedidoModificar.precio;

            _restauranteContext.Entry(pedidoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(pedidoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult Eliminarpedido(int id)
        {
            pedidos? pedido = (from e in _restauranteContext.pedidos
                                where e.clienteId == id
                                select e).FirstOrDefault();

            if (pedido == null)
            {
                return NotFound();
            }

            _restauranteContext.pedidos.Attach(pedido);
            _restauranteContext.pedidos.Remove(pedido);
            _restauranteContext.SaveChanges();

            return Ok(pedido);
        }


    }
}

