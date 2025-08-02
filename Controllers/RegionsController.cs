using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdamyCourse.Data;
using UdamyCourse.Model.Domain;
using UdamyCourse.Model.DTOs;

namespace UdamyCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;

        public RegionsController(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetRegions()
        {
            var regions = _dbContext.Regions.ToList();

            var regionDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            return Ok(regionDto);

        }

        [HttpGet("{id}")]

        public IActionResult GetRegionById(int id)
        {
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(region);
        }

        [HttpPost]

        public IActionResult AddRegion([FromBody] AddRegionDto addRegionDto)
        {

            var region = new Region
            {
                Code = addRegionDto.Code,
                Name = addRegionDto.Name,
                RegionImageUrl = addRegionDto.RegionImageUrl
            };

            _dbContext.Regions.Add(region);
            _dbContext.SaveChanges();

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = region.Id }, regionDto);



        }

        [HttpPut("{id}")]
        public IActionResult UpdateRegion(int id, [FromBody] AddRegionDto addRegionDto)
        {
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            region.Code = addRegionDto.Code;
            region.Name = addRegionDto.Name;
            region.RegionImageUrl = addRegionDto.RegionImageUrl;
            _dbContext.SaveChanges();

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteRegion(int id)
        {
            var region = _dbContext.Regions.FirstOrDefault(r => r.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            _dbContext.Regions.Remove(region);
            _dbContext.SaveChanges();
            return NoContent();
        }


    }
}