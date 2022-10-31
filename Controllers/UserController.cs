
using GlobalErrorApp.Exceptions;
using GlobalErrorApp.UseCase;
using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IFindUserUseCase _findUseCase;
        private readonly IAddUserUseCase _addUseCase;
        public UserController(IFindUserUseCase findUseCase, IAddUserUseCase addUseCase)
        {
            _findUseCase = findUseCase;
            _addUseCase = addUseCase;
        }

        [HttpGet]
        public IActionResult FindById([FromQuery] int id)
        {
            var user = _findUseCase.FindById(id);
            if (user is null)
            {
                throw new NotFoundException("ユーザー ID : " + id + " が見つかりません。");
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult Add(AddUserCommand command)
        {
            var result = new AddUserCommandValidator().Validate(command);
            if (!result.IsValid)
            {
                throw new BadRequestException("バリデーションエラーが発生しました。");
            }
            
            _addUseCase.Add(command);
            return Ok(new { message = "ユーザーを登録しました" });
        }
    }
}