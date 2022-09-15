using API.Helpers;

namespace API.Data
{
    public class TodoRepository: ITodoRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TodoRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Todo> GetTasks(int userId)
        {
            return await _context.Todos.FirstOrDefaultAsync(u =>
                u.UserId == userId);
        }

        public async Task<AppUser> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<Todo> GetTask(int id)
        {
            return await _context.Todos.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PagedList<Todo>> GetTasksForUser(TaskParams taskParams)
        {
            var tasks = _context.Todos.AsQueryable();

            //Change to  create date 

            tasks = tasks.OrderByDescending(d => d.Description);

            return await PagedList<Todo>.CreateAsync(tasks, taskParams.PageNumber, taskParams.PageSize);
        }
    }
}
