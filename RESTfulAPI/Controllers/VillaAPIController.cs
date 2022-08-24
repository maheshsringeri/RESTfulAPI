using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RESTfulAPI.Data;
using RESTfulAPI.Logging;
using RESTfulAPI.Models.Dto;
using System.Linq;

namespace RESTfulAPI.Controllers
{
    [Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logger;

        public VillaAPIController(ILogging logger)
        {
            this._logger = logger;
        }


        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.Log("Get all villas", "");

            return Ok(VillaStore.villaList);
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(VillaDTO))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.Log("Error while get villa: " + id, "error");
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(q => q.Id == id);

            if (villa == null)
            {
                return NoContent();
            }


            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (VillaStore.villaList.FirstOrDefault(q => q.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Coustum Error", "Villa name already exists.");

                return BadRequest(ModelState);

            }

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }

            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDTO.Id = VillaStore.villaList.OrderByDescending(q => q.Id).FirstOrDefault().Id + 1;


            VillaStore.villaList.Add(villaDTO);


            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public ActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(q => q.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.villaList.Remove(villa);

            return NoContent();

        }


        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public ActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {

            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(q => q.Id == id);
            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;



            return NoContent();

        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        public ActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (id == 0 || patchDTO == null)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(q => q.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(villa, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }
}
