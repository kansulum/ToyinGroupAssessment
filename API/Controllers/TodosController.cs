using API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository todoRepository;
        private readonly IMapper mapper;

        public TodoController(ITodoRepository todoRepository, IMapper mapper)
        {
            this.todoRepository = todoRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasksForUser(int userId,
           [FromQuery] TaskParams taskParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            taskParams.UserId = userId;

            var messagesFromRepo = await todoRepository.GetTasksForUser(taskParams);

            var tasks = mapper.Map<IEnumerable<TaskToReturnDto>>(messagesFromRepo);

            Response.AddPaginationHeader(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize,
                    messagesFromRepo.TotalCount, messagesFromRepo.TotalPages);

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(int userId, TaskForCreationDto taskForCreationDto)
        {
            var user = await todoRepository.GetUser(userId);

            if (user.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();


            var todo = mapper.Map<Todo>(taskForCreationDto);

            todo.UserId = userId;
            todo.User = user;

            todoRepository.Add(todo);

            if (await todoRepository.SaveAll())
            {
                var todoToReturn = mapper.Map<TaskToReturnDto>(todo);
                return Ok(todoToReturn);
                  
            }

            throw new Exception("Creating the message failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var task = await todoRepository.GetTask(id);



            if (task.UserId == userId)
                todoRepository.Delete(task);

            if (await todoRepository.SaveAll())
                return NoContent();

            throw new Exception("Error deleting the task");
        }

        [HttpPost("{id}/done")]
        public async Task<IActionResult> MarkTaskAsDone(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var task = await todoRepository.GetTask(id);

            if (task.UserId != userId)
                return Unauthorized();

            task.IsDone =true;

            await todoRepository.SaveAll();

            return NoContent();
        }
    }
}
