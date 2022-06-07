using APIDiff.DataAccess.Models;
using APIDiff.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using static APIDiff.JsonUtility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDiff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiffController : ControllerBase
    {
        // GET: api/<DiffController>
        [HttpGet]
        public IActionResult Get()
        {
            var items = ReadFromJson();
            return Ok(items);
        }

        // GET api/<DiffController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var items = ReadFromJson();

            var leftItem = items.Left.FindEntity(id);
            var rightItem = items.Right.FindEntity(id);

            //Comparison of Left/Right data from JSON
            //Covered all cases of input/output sample
            if (leftItem == null || rightItem == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                Response response = new Response();

                //Making empty list to prevent null preview
                response.Diffs = new List<Diff>();

                if (leftItem.Data == rightItem.Data)
                    response.DiffResultType = "Equals";

                else if (leftItem.Data.Length != rightItem.Data.Length)
                    response.DiffResultType = "Size do not match";

                else if (leftItem.Data != rightItem.Data && leftItem.Data.Length == rightItem.Data.Length)
                {
                    response.DiffResultType = "Content do not match";

                    byte[] leftDecoded = Base64Decode(leftItem.Data);
                    byte[] rightDecoded = Base64Decode(rightItem.Data);

                    //Algorithm that calculates offset and its length
                    response.Diffs = FindOffset(leftDecoded, rightDecoded);
                }

                return Ok(response);
            }
        }
    }
}
