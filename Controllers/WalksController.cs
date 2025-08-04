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
            var walk = _mapper.Map<Walk>(addWalkDto);

            await _walkRepository.CreatWalkAsync(walk);

            var walkDto = _mapper.Map<WalkDto>(walk);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetWalks()
        {
            var walks = await _walkRepository.GetAllAsync();
            var walkDtos = _mapper.Map<List<WalkDto>>(walks);
            return Ok(walkDtos);
        }
    }
}
