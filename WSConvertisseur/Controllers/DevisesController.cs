using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    [Route("api/devises")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        private List<Devise> lesDevises;
        public List<Devise> LesDevises
        {
            get { return lesDevises; }
            set { lesDevises = value; }
        }


        // GET: api/<DevisesController>
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return LesDevises;
        }

        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name = "GetDevise")]
        public ActionResult<Devise> GetById([FromRoute]int id)
        {
            Devise? devise = LesDevises.FirstOrDefault((d) => d.ID == id);
            if (devise is null)
                return NotFound();
            return devise;
        }

        // POST api/<DevisesController>
        [HttpPost]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            LesDevises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.ID }, devise);
        }

        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != devise.ID)
                return BadRequest();

            int index = LesDevises.FindIndex((d) => d.ID == id);

            if (index < 0)
                return NotFound();

            LesDevises[index] = devise;
            return NoContent();
        }

        // DELETE api/<DevisesController>/5
        [HttpDelete("{id}")]
        public ActionResult<Devise> Delete(int id)
        {
            Devise? devise = LesDevises.FirstOrDefault((d) => d.ID == id);
            if (devise is null)
                return NotFound();
            LesDevises.Remove(devise);
            return devise;
        }

        public DevisesController()
        {
            LesDevises = new List<Devise>()
            {
                new Devise(1, "Dollar", 1.08),
                new Devise(2, "Franc Suisse", 1.07),
                new Devise(3, "Yen", 120),
            };
        }
    }
}
