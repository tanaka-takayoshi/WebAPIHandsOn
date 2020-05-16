using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIHandsOn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Lab2Controller : ControllerBase
    {
        private static HashSet<Pet> petsInMemoryStore = new HashSet<Pet>();

        //TODOステータスコードの明示
        //TODO FromBodyは推論するので省略可能
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Pet> Create(/* [FromBody]*/Pet pet)
        {
            if (pet.Name?.Length <= 2)
            {
                return BadRequest("名前は3文字以上!");
            }
            pet.Id = petsInMemoryStore.Any() ?
                     petsInMemoryStore.Max(p => p.Id) + 1 : 1;
            petsInMemoryStore.Add(pet);

            return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Pet> GetById(int id)
        {
            return petsInMemoryStore.FirstOrDefault(p => p.Id == id);
        }

        [HttpGet("getbyname")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Pet>> GetByName(
                [FromQuery] string name)
        {
            List<Pet> pets = null;

            if (name != null)
            {
                if (name.Length <= 2)
                {
                    return BadRequest("名前は3文字以上!");
                }
                pets = petsInMemoryStore.Where(p => p.Name == name).ToList();
            }
            else
            {
                pets = petsInMemoryStore.ToList();
            }

            if (!pets.Any())
            {
                return NotFound("いないよ");
            }

            return pets;
        }
    }
}