using BackEnd.DTOs;
using BackEnd.Model;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedor service;
        
        public FornecedorController(IFornecedor service) { this.service = service; }


        [HttpGet]
        public async Task<ActionResult<List<FornecedorDTO>>> GetAll()
        {
            try
            {
                return Ok(await service.GetFornecedores());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("cnpj/{cnpj}")]
        public async Task<ActionResult<PessoaJuridicaModel>> GetByCNPJ(string cnpj)
        {
            try
            {
                return Ok(await service.GetPessoaJuridicaByCNPJ(cnpj));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<PessoaFisicaModel>> GetByCPF(string cpf)
        {
            try
            {
                return Ok(await service.GetPessoaByCPF(cpf));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("empresa/{empresa}")]
        public async Task<ActionResult<List<FornecedorDTO>>> GetFornecedorByEmpresa(string empresa)
        {
            try
            {
                return Ok(await service.GetFornecedorByEmpresa(empresa));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<List<FornecedorDTO>>> GetByName(string nome)
        {
            try
            {
                var peoples = await service.GetPessoaByNome(nome);
                return Ok(peoples);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("data/{date}")]
        public async Task<ActionResult<List<FornecedorDTO>>> GetByDate(DateTime date)
        {
            try{

                return Ok(await service.GetPessoaByData(date));

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("pessoaFisica")]
        public async Task<IActionResult> PostPerson([FromBody] PessoaFisicaModel model)
        {
            try
            {
                var response = await service.CreateFornecedor(model);
                return Ok(response);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("pessoaJuridica")]
        public async Task<IActionResult> PostPerson(PessoaJuridicaModel model)
        {
            try
            {
                var response = await service.CreateFornecedor(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
