using Microsoft.AspNetCore.Mvc;
using System.Net;
using VillaApi.Data;
using VillaApi.Models;
using VillaApi.Models.Dto;

namespace VillaApi.Controllers
{
    [Route("api/[controller]")]
    //[Route("api/VillaAPI")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        //[HttpGet]
        //public IEnumerable<VillaDTO> GetVillas()
        //{
        //    return VillaStore.villaList;
        //}

        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }

        //[HttpGet("{id:int}")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //public ActionResult<VillaDTO> GetVilla(int id)
        //{
        //    if (id == 0)
        //    {
        //        return BadRequest("Invalid Id");
        //    }
        //    var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
        //    if (villa == null)
        //    {
        //        return NotFound(id);
        //    }
        //    return Ok(villa);
        //}

        [HttpGet("{id:int}", Name = "VillaAPI")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid Id");
            }
            var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
            if (villa == null)
            {
                return NotFound(id);
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }

            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            villaDTO.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);

            return CreatedAtRoute("VillaAPI", new { id = villaDTO.Id }, villaDTO);

            //return Ok(villaDTO);
        }
    }
}
