using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdamyCourse.Data;
using UdamyCourse.Model.Domain;
using UdamyCourse.Model.DTOs;
using UdamyCourse.Repositories;

namespace UdamyCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RegionsController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger logger;

        public RegionsController(DataBaseContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            _dbContext = dbContext;
            _regionRepository = regionRepository;
            _mapper = mapper;
            this.logger = logger;
        }
        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegions()
        {
            logger.LogInformation("GetRegions method called");
            var regions = await _regionRepository.GetAllAsync();

             var regionDto = _mapper.Map<List<RegionDto>>(regions);
            return Ok(regionDto);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Reader")]

        public async Task<IActionResult> GetRegionById(int id)
        {
            var region = await _regionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            //var regionDto = new RegionDto()
            //{
            //    Id = region.Id,
            //    Code = region.Code,
            //    Name = region.Name,
            //    RegionImageUrl = region.RegionImageUrl
            //};

            var regionDto = _mapper.Map<RegionDto>(region);

            return Ok(region);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddRegion([FromBody] AddRegionDto addRegionDto)
        {
            if (ModelState.IsValid)
            {
                var region = _mapper.Map<Region>(addRegionDto);

                region = await _regionRepository.CreateRegionAsync(region);
                var regionDto = _mapper.Map<RegionDto>(region);

                return CreatedAtAction(nameof(GetRegionById), new { id = region.Id }, regionDto);

            }
            else
            {
                return BadRequest(ModelState);
            }
            



        }
        [Authorize(Roles = "Writer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegion(int id, [FromBody] AddRegionDto addRegionDto)
        {

            if (ModelState.IsValid)
            {
                var region = _mapper.Map<Region>(addRegionDto);

                region = await _regionRepository.UpdateReginAsync(region, id);


                if (region == null)
                {
                    return NotFound();
                }


                var regionDto = _mapper.Map<RegionDto>(region);

                return Ok(regionDto);

            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [Authorize(Roles = "Writer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult>  deleteRegion(int id)
        {
            var region = await _regionRepository.DeleteReginAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}