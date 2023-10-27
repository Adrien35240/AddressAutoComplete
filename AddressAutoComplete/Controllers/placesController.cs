using Microsoft.AspNetCore.Mvc;
using OsmSharp.Streams;
using System.Diagnostics;

namespace AdressAutoComplete.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class placesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPbfData([FromQuery] string city, [FromQuery] string places, [FromQuery] int limit)
        {
            string fileName = "bretagne-latest.osm.pbf";
            string filePath = Path.Combine("osmFiles", fileName);

            try
            {
                using var fileStream = System.IO.File.OpenRead(filePath);
                var source = new PBFOsmStreamSource(fileStream);
                var filteredCyim = from osmGeo in source
                                   where osmGeo.Tags.ContainsKey("name") && osmGeo.Tags["name"].Contains(places)
                                         && osmGeo.Tags.ContainsKey("addr:city") && osmGeo.Tags["addr:city"] == city
                                   select osmGeo;

                var osmData = filteredCyim.Distinct().Take(limit).ToList();

                return Ok(osmData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
