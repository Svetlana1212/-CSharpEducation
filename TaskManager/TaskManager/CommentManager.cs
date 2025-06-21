using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTaskManager
{
    public class CommentManager
    {
        public static string path = "comments.txt";
        public static List<Comment> comments = new List<Comment>();

        public static void Load()
        {
            comments.Clear();
            if (!File.Exists(path)) return;

            foreach (var line in File.ReadAllLines(path))
            {
                var parts = line.Split('|', 5);
                if (parts.Length == 5)
                {
                    comments.Add(new Comment
                    {
                        Id = int.Parse(parts[0]),
                        TaskId = int.Parse(parts[1]),
                        UserId = int.Parse(parts[2]),
                        Date = parts[3],
                        Text = parts[4]
                    });
                }
            }
        }

        public static void Save()
        {
            File.WriteAllLines(path, comments.Select(c => $"{c.Id}|{c.TaskId}|{c.UserId}|{c.Date}|{c.Text}"));
        }

        public static void ShowComments(int taskId, List<User> users)
        {
            var taskComments = comments.Where(c => c.TaskId == taskId).ToList();
            if (!taskComments.Any())
            {
                Console.WriteLine("Комментариев нет.");
                return;
            }

            int i = 1;
            foreach (var c in taskComments)
            {
                var author = users.FirstOrDefault(u => u.Id == c.UserId);
                string userInfo = author != null ? $"{author.Name} {author.Surname} ({author.Email})" : "Неизвестный пользователь";
                Console.WriteLine($"{i++}. [{c.Date}] {userInfo}: {c.Text}");
            }

            Console.WriteLine();

        }

        public static void AddComment(int taskId, User user, string text)
        {
            int newId = comments.Any() ? comments.Max(c => c.Id) + 1 : 1;
            comments.Add(new Comment
            {
                Id = newId,
                TaskId = taskId,
                UserId = user.Id,
                Date = DateTime.Now.ToString("G"),
                Text = text
            });
            Save();
        }

        public static bool DeleteComment(int commentId, User currentUser)
        {
            var comment = comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null) return false;
            if (currentUser.Role == "admin" || comment.UserId == currentUser.Id)
            {
                comments.Remove(comment);
                Save();
                return true;
            }
            return false;
        }

        public static bool EditComment(int commentId, User currentUser, string newText)
        {
            var comment = comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null) return false;
            if (currentUser.Role == "admin" || comment.UserId == currentUser.Id)
            {
                comment.Text = newText;
                comment.Date = DateTime.Now.ToString("G");
                Save();
                return true;
            }
            return false;
        }

        public static List<Comment> GetCommentsByTask(int taskId)
        {
            return comments.Where(c => c.TaskId == taskId).ToList();
        }
    }
}
