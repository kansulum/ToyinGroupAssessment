using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
    
        ITodoRepository TodoRepository {get;}

        Task<bool> Complete();
        bool HasChanges();
    }
}