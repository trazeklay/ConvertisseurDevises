using Microsoft.AspNetCore.Mvc;
using WSConvertisseur.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WSConvertisseur.Controllers
{
    /// <summary>
    /// Le controlleur de la classe 'Devise'
    /// </summary>
    /// <returns>DeviseController</returns>
    [Route("api/devises")]
    [ApiController]
    public class DevisesController : ControllerBase
    {
        private List<Devise> lesDevises;

        /// <summary>
        /// La liste des devises qui est dans l'appi
        /// </summary>
        /// <returns>La liste des devises</returns>
        public List<Devise> LesDevises
        {
            get { return lesDevises; }
            set { lesDevises = value; }
        }

        /// <summary>
        /// Get all currencies
        /// </summary>
        /// <returns>The list of every currencies</returns>
        // GET: api/<DevisesController>
        [HttpGet]
        public IEnumerable<Devise> GetAll()
        {
            return LesDevises;
        }

        /// <summary>
        /// Get a single currency
        /// </summary>
        /// <param name="id">The id of the currency</param>
        /// <returns>Http response</returns>
        /// <response code="200">When the currency is found</response>
        /// <response code="404">When the currency is not found</response>
        // GET api/<DevisesController>/5
        [HttpGet("{id}", Name = "GetDevise")]
        public ActionResult<Devise> GetById([FromRoute]int id)
        {
            Devise? devise = LesDevises.FirstOrDefault((d) => d.ID == id);
            if (devise is null)
                return NotFound();
            return devise;
        }

        /// <summary>
        /// Adds a currency to the currencies
        /// </summary>
        /// <param name="devise">The currency that you want to add</param>
        /// <returns>Http response</returns>
        /// <response code="201">When the currency is successfully added to the list</response>
        /// <response code="400">When the currency is not found</response>
        // POST api/<DevisesController>
        [HttpPost]
        public ActionResult<Devise> Post([FromBody] Devise devise)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            LesDevises.Add(devise);
            return CreatedAtRoute("GetDevise", new { id = devise.ID }, devise);
        }

        /// <summary>
        /// Modifies a currency in the list at a position
        /// </summary>
        /// <param name="id">The position</param>
        /// <param name="devise">The currency</param>
        /// <returns>HTTP response</returns>
        /// <response code="404">When the currency is not found or could not be modifieds</response>
        /// <response code="400">Put method successfull</response>
        // PUT api/<DevisesController>/5
        [HttpPut("{id}")]
        public ActionResult<Devise> Put(int id, [FromBody] Devise devise)
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

        /// <summary>
        /// Deletes a currency from the list
        /// </summary>
        /// <param name="id">The id of the currency that you want to delete</param>
        /// <returns>HTTP Response</returns>
        /// <response code="400">Put method successfull</response>
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

        /// <summary>
        /// Le constructeur
        /// </summary>
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
