using AutoMapper;
using ElectronicMenu.BL.Positions;
using ElectronicMenu.BL.Positions.Entities;
using ElectronicMenu.Services.Controllers.Positions.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicMenu.Services.Controllers.Positions
{
    [ApiController]
    [Route("[controller]")]
    public class PositionsController : ControllerBase
    {
        private readonly IPositionsProvider _positionsProvider;
        private readonly IPositionsManager _positionsManager;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PositionsController(IPositionsProvider positionsProvider, IPositionsManager positionsManager,
            IMapper mapper, ILogger<PositionsController> logger)
        {
            _positionsProvider = positionsProvider;
            _positionsManager = positionsManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllPositions()
        {
            IEnumerable<PositionModel> positions = _positionsProvider.GetPositions();
            return Ok(new PositionsListResponse()
            {
                Positions = positions.ToList()
            });
        }

        [HttpGet]
        [Route("filter")]
        public IActionResult GetFilteredPositions([FromQuery] PositionsFilter filter)
        {
            IEnumerable<PositionModel> positions = _positionsProvider.GetPositions(_mapper.Map<PositionModelFilter>(filter));
            return Ok(new PositionsListResponse()
            {
                Positions = positions.ToList()
            });
        }

        [HttpGet]
        [Route("page")]
        public IActionResult GetFilteredSortedPositionsPage([FromQuery] GetFilteredSortedPageRequest rules)
        {
            IEnumerable<PositionModel> positions = _positionsProvider.GetFilteredSortedPagePositions(
                _mapper.Map<GetFilteredSortedPagePositionModel>(rules)
                );
            return Ok(new PositionsPageResponse
            {
                Page = rules.Page,
                Positions = positions.ToArray()
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPosition([FromRoute] int id)
        {
            try
            {
                PositionModel position = _positionsProvider.GetPosition(id);
                return Ok(position);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost]
        public IActionResult CreatePosition(CreatePositionRequest request) //automatic validation
        {
            try
            {
                PositionModel position = _positionsManager.CreatePosition(_mapper.Map<CreatePositionModel>(request));
                return Ok(position);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdatePosition([FromRoute] int id, UpdatePositionRequest request)
        {
            try
            {
                PositionModel position = _positionsManager.UpdatePosition(id, _mapper.Map<UpdatePositionModel>(request));
                return Ok(position);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeletePosition([FromRoute] int id)
        {
            try
            {
                _positionsManager.DeletePosition(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ex.Message);
            }
        }
    }
}
