using AutoMapper;
using ElectronicMenu.BL.Users.Entities;
using ElectronicMenu.BL.Users;
using ElectronicMenu.Services.Controllers.Users.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ElectronicMenu.BL.Auth;
using static Duende.IdentityServer.Models.IdentityResources;
using ElectronicMenu.BL.Auth.Entities;

namespace ElectronicMenu.Services.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAuthProvider _authProvider;
        private readonly IUsersManager _usersManager;
        private readonly IUsersProvider _usersProvider;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UsersController(IAuthProvider authProvdider, IUsersManager usersManager, IUsersProvider usersProvider,
            IMapper mapper, ILogger<UsersController> logger)
        {
            _authProvider = authProvdider;
            _usersManager = usersManager;
            _usersProvider = usersProvider;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                TokensResponse tokens = await _authProvider.AuthorizeUser(email, password);
                return Ok(tokens);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
        {
            try
            {
                await _authProvider.RegisterUser(request.Login, request.Password);
                UserModel user = _usersManager.CreateUser(_mapper.Map<CreateUserModel>(request));

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            IEnumerable<UserModel> users = _usersProvider.GetAllUsers();

            return Ok(new UsersListResponse()
            {
                Users = users.ToList()
            });
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser([FromRoute] Guid id)
        {
            try
            {
                UserModel user = _usersProvider.GetUser(id);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateUserInfo([FromRoute] Guid id, UpdateUserRequest request)
        {
            try
            {
                UserModel user = _usersManager.UpdateUser(id, _mapper.Map<UpdateUserModel>(request));
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            try
            {
                _usersManager.DeleteUser(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ex.Message);
            }
        }
    }
}
