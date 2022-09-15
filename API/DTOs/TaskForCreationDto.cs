

namespace API.DTOs
{
    public class TaskForCreationDto
    {
        public virtual string Description { get; set; }
        public virtual DateTime CreationTime { get; set; }       
        public virtual int UserId { get; set; }
    }
}
