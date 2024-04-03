using BackEnd.Model;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresa service;

        public EmpresaController(IEmpresa service) { this.service = service; }

        [HttpGet]
        public async Task<ActionResult<List<EmpresaModel>>> GetAll()
        {
            try
            {
                return Ok(await service.GetAllEmpresa());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok( await service.GetEmpresa(id));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name")]
        public async Task<ActionResult<EmpresaModel>> GetEmpresaByName(string name)
        {
            try
            {
                return Ok(await service.GetEmpresa(name));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostEmpresa(EmpresaModel model)
        {
            try
            {
                return Ok(await service.CreateEmpresa(model));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmpresa(int id)
        {
            try{

                return Ok(await service.DeleteEmpresa(id));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
