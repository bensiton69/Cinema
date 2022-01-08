using System;
using System.Collections.Generic;
using System.Linq;
using API.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        [HttpGet("Genres")]
        public IEnumerable<string> GetStatuses()
        {
            return Enum.GetValues(typeof(eGenre))
                .Cast<eGenre>()
                .Select(v => v.ToString());
        }

        [HttpGet("MovieTypes")]
        public IEnumerable<string> GetMovieTypes()
        {
            return Enum.GetValues(typeof(eMovieType))
                .Cast<eMovieType>()
                .Select(v => v.ToString());
        }

        [HttpGet("SeatTypes")]
        public IEnumerable<string> GetSeatType()
        {
            return Enum.GetValues(typeof(eMovieType))
                .Cast<eMovieType>()
                .Select(v => v.ToString());
        }
    }
}
