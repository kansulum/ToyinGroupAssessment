using System.Threading.Tasks;

namespace API.Entities
{
    public class Todo
    {
        public  int Id { get; set; }
        public virtual string Description { get; set; }

        public virtual DateTime CreationTime { get; set; }
        public virtual bool IsDone { get; set; }
        public virtual int UserId { get; set; }
        public virtual AppUser User { get; set; }

        public Todo()
        {
            CreationTime = DateTime.Now;
            IsDone = false;
        }
    }
}
