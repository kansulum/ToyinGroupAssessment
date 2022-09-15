using API.Helpers;

namespace API.Interfaces
{
    public interface ITodoRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();

        Task<AppUser> GetUser(int id);
        Task<Todo> GetTasks(int userId);

        Task<Todo> GetTask(int id);

        Task<PagedList<Todo>> GetTasksForUser(TaskParams taskParams);
    }
}

