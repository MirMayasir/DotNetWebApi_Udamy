using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdamyCourse.Model.Domain;
using UdamyCourse.Model.DTOs;
using UdamyCourse.Repositories;

namespace UdamyCourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {

            _mapper = mapper;
            _walkRepository = walkRepository;
        }



        [HttpPost]
        public async Task<IActionResult> AddWalk([FromBody] AddWalkDto addWalkDto)
        {
            if ((ModelState.IsValid))
            {
                var walk = _mapper.Map<Walk>(addWalkDto);

                await _walkRepository.CreatWalkAsync(walk);

                var walkDto = _mapper.Map<WalkDto>(walk);

                return Ok();

            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber=1, [FromQuery] int pageSize = 1000)
        {
            var walks = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending??true, pageNumber, pageSize);
            var walkDtos = _mapper.Map<List<WalkDto>>(walks);
            return Ok(walkDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var walk = await _walkRepository.GetWalByIdAsync(id);

            var walkDto = _mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> updateWalk([FromBody] AddWalkDto addWalkDto, int id)
        {
            if ((ModelState.IsValid))
            {
                var curWalk = _mapper.Map<Walk>(addWalkDto);
                curWalk = await _walkRepository.UpdateWalkAsync(curWalk, id);
                if (curWalk == null)
                {
                    return BadRequest();
                }

                return Ok(_mapper.Map<Walk>(curWalk));

            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteWalk(int id)
        {
            var currWalk = await _walkRepository.DeleteWalkAsync(id);
            if(currWalk == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
