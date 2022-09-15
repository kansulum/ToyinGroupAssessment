using System;
using System.Linq;
using API.Entities;

namespace API.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<Todo> MarkUndonAsDone(this IQueryable<Todo> query, string currentUsername)
        {
            var unreadMessages = query.Include(x=> x.User).Where(m => m.CreationTime == null
                && m.User.UserName == currentUsername);

            if (unreadMessages.Any())
            {
                foreach (var message in unreadMessages)
                {
                    message.IsDone =true;
                }
            }

            return query;
        }
    }
}