using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.ToList();
            if (produtos is null)
            {
                return NotFound();
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produtos = _context.Produtos.FirstOrDefault(p => p.ProdutoID == id);
            if (produtos is null)
            {
                return NotFound("Produto não encontrado...");
            }
            return produtos;
        }

        [HttpPost]
        public ActionResult Post(Produto produtos)
        {
            if (produtos is null)
                return BadRequest();

            _context.Produtos.Add(produtos);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produtos.ProdutoID }, produtos);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produtos)
        {
            if (id != produtos.ProdutoID)
            {
                return BadRequest();
            }

            _context.Entry(produtos).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produtos);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produtos = _context.Produtos.FirstOrDefault(p => p.ProdutoID == id);
            //var produto = _context.Produtos.Find(id);

            if (produtos is null)
            {
                return NotFound("Produto não localizado...");
            }
            _context.Produtos.Remove(produtos);
            _context.SaveChanges();

            return Ok(produtos);
        }
    }
}
