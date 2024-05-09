using FlowerShop_API.Data;
using FlowerShop_API.Models;
using FlowerShop_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop_API.Controllers
{
    [Route("api/FlowerShopAPI")]
    [ApiController]
    public class FlowerShopAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<FlowerDto>> GetFlowers()
        {
            return FlowerStore.flowerList;
        }

        [HttpGet("id", Name = "get-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FlowerDto> GetFlowerById(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var flower = FlowerStore.flowerList.FirstOrDefault(u => u.Id == id);
            if (flower == null)
            {
                return BadRequest("Not found");
            }
            return Ok(flower);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FlowerDto> AddFlower([FromBody] FlowerDto flower)
        {
            if (flower == null)
            {
                return BadRequest();
            }
            if (flower.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            flower.Id = FlowerStore.flowerList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            FlowerStore.flowerList.Add(flower);

            return CreatedAtRoute("get-by-id", new { id = flower.Id }, flower);
        }

        [HttpDelete("id", Name = "delete-flower")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RemoveFlower(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var flower = FlowerStore.flowerList.FirstOrDefault(u => u.Id == id);
            if (id == null)
            {
                return BadRequest("Not found");
            }
            FlowerStore.flowerList.Remove(flower);
            return NoContent();
        }

        [HttpPut("id", Name = "update-flower")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateFlower(int id, [FromBody]FlowerDto flower)
        {
            if (id == null)
            {
                return BadRequest("Not found");
            }
            var existflower = FlowerStore.flowerList.FirstOrDefault(u => u.Id == id);
            existflower.Name = flower.Name;
            existflower.Price = flower.Price;

            return Ok(existflower);
        }
    }
}
