using L012020AM601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L012020AM601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clientesController : ControllerBase
    {
        private readonly restauranteContext _restauranteContext;

        public clientesController(restauranteContext _restauranteContexto)
        {
            _restauranteContext = _restauranteContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<clientes> listadoClientes = (from e in _restauranteContext.clientes select e).ToList();

            if (listadoClientes.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoClientes);
        }

        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            clientes? equipo = (from e in _restauranteContext.clientes
                               where e.clienteId == id
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
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

        public IActionResult GuardarEquipo([FromBody] clientes equipo)
        {
            try
            {
                _restauranteContext.clientes.Add(equipo);
                _restauranteContext.SaveChanges();
                return Ok(equipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult ActualizarEquipo(int id, [FromBody] clientes equipoModificar)
        {
            clientes? equipoActual = (from e in _restauranteContext.clientes
                                     where e.clienteId == id
                                     select e).FirstOrDefault();

            if (equipoActual == null)
            {
                return NotFound();
            }

            equipoActual.clienteId = equipoModificar.clienteId;
            equipoActual.nombreCliente = equipoModificar.nombreCliente;
            equipoActual.direccion = equipoModificar.direccion;
           

            _restauranteContext.Entry(equipoActual).State = EntityState.Modified;
            _restauranteContext.SaveChanges();

            return Ok(equipoModificar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]

        public IActionResult EliminarEquipo(int id)
        {
            clientes? equipo = (from e in _restauranteContext.clientes
                               where e.clienteId == id
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }

            _restauranteContext.clientes.Attach(equipo);
            _restauranteContext.clientes.Remove(equipo);
            _restauranteContext.SaveChanges();

            return Ok(equipo);
        }


    }
}

