using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using static APIDiff.JsonUtility;
using static APIDiff.Extensions.EntityExtensions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using APIDiff.DataAccess.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDiff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeftController : ControllerBase
    {
        // PUT api/<LeftController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] JObject value)
        {
            var data = value.SelectToken("data")?.Value<string>();

            //Checking input data and making sure it is Base64 Encoded
            if (!string.IsNullOrWhiteSpace(data))
            {
                try
                {
                    Base64Decode(data);
                }
                catch(FormatException ex)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { ex.Message });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var items = ReadFromJson();
            var entity = items.Left.FindEntity(id);
            
            if(entity == null)
            {
                //Create new Left
                items.AddEntity(new Left
                {
                    Id = id,
                    Data = data
                });

                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                //Update existed
                entity.Data = data;

                var result = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });

                WriteJsonIntoFile(result);

                return StatusCode(StatusCodes.Status201Created);
            }
        }
    }
}
