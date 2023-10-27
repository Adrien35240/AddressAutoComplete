using Microsoft.AspNetCore.Mvc;
using OsmSharp.Streams;
using System;
using System.Diagnostics; // Add this for Stopwatch
using System.IO;

namespace AdressAutoComplete.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AnyController : ControllerBase
    {
        /// <summary>
        /// Gets PBF data based on the provided query and limit.
        /// </summary>
        /// <param name="q">The search query.</param>
        /// <param name="limit">The limit of results to return.</param>
        /// <returns>Returns a list of OSM data based on the search query.</returns>
        [HttpGet]
        public IActionResult GetPbfData([FromQuery] string q, [FromQuery] int limit)
        {
            string fileName = "bretagne-latest.osm.pbf";
            string filePath = Path.Combine("osmFiles", fileName);

            try
            {
                using var fileStream = System.IO.File.OpenRead(filePath);
                var source = new PBFOsmStreamSource(fileStream);
                var filteredCities = from osmGeo in source
                                     where osmGeo.Tags.ContainsKey("name") && osmGeo.Tags["name"].ToLower().Contains(q.Trim().ToLower())
                                     || osmGeo.Tags.Any(tag => tag.Value.ToLower().Contains(q.Trim().ToLower()))
                                     select osmGeo;

                var osmData = filteredCities.Distinct().Take(limit).ToList();

                return Ok(osmData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
