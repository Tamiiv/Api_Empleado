using Api_Empleado.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Empleado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly DbempleadoContext _dbContext;
        public EmpleadoController(DbempleadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var listaEmpleado = await _dbContext.Empleados.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listaEmpleado);
        }

        [HttpGet]
        [Route("Obtener/{id:int}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var empleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
            return StatusCode(StatusCodes.Status200OK, empleado);
        }

        [HttpPost]
        [Route("Nuevo")]
        public async Task<IActionResult> Nuevo([FromBody] Empleado empleado)
        {
            await _dbContext.Empleados.AddAsync(empleado);
            await _dbContext.SaveChangesAsync();
            
            return StatusCode(StatusCodes.Status200OK, new {mensaje = "Empleado agregado"});
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Empleado empleado)
        {
            _dbContext.Empleados.Update(empleado);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "Empleado editado" });
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var empleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);
            _dbContext.Empleados.Remove(empleado);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new {mensaje = "Registro eliminado"});
        }
    }
}
