using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDoListApplication
{
#pragma warning disable S1118 // Utility classes should not have public constructors
    public class ToDoApplication
#pragma warning restore S1118 // Utility classes should not have public constructors
    {
        static void Main(string[] args)
        {
            // Method intentionally left empty.
        }

        public static void AddList(string name)
        {
            CheckValidationListName(name);
            using (var db = new AppContext())
            {
                var taskList = new Lists { Name = name, Hide = false };
                db.AllLists.Add(taskList);
                db.SaveChanges();
            }
        }

        public static void RemoveList(string name)
        {
            CheckValidationListName(name);
            using (var db = new AppContext())
            {
                var taskList = db.AllLists.Where(x => x.Name.Equals(name)).Select(x => x).FirstOrDefault();
                var allTask = CheckTaskExist(taskList);
                if (allTask != null)
                {
                    db.ToDoTask.RemoveRange(allTask);
                    db.SaveChanges();
                }

                db.AllLists.Remove(taskList);
                db.SaveChanges();
            }
        }

        public static void AddTask(string nameList, string task)
        {
            CheckValidationListName(nameList);
            CheckValidationTaskName(task);
            using (var db = new AppContext())
            {
                var taskList = db.AllLists.Where(x => x.Name.Equals(nameList)).Select(x => x).FirstOrDefault();
                var newTask = new ToDo { Complete = false, Task = task, ListName = taskList, ListId = taskList.ListId };
                db.ToDoTask.Add(newTask);
                db.SaveChanges();
            }
        }

        public static void UpdateLists(string name, string newName = "",  bool isHidden = false)
        {
            CheckValidationListName(name);

            if (newName is null)
            {
                throw new ArgumentNullException(nameof(newName));
            }

            using (var db = new AppContext())
            {
                var firstList = db.AllLists.Where(x => x.Name.Equals(name)).FirstOrDefault();
                List<ToDo> tasks = null;
                if (firstList != null)
                {
                    if (newName != string.Empty)
                    {
                        tasks = CheckTaskExist(firstList);
                        firstList.Name = newName;
                    }

                    firstList.Hide = isHidden;

                    if (tasks != null)
                    {
                        foreach (var t in tasks)
                        {
                            UpdateTask(name, t.Task, newList: firstList);
                        }
                    }

                    db.AllLists.Update(firstList);
                    db.SaveChanges();
                }
            }
        }

        public static void UpdateTask(string listName, string task, string newTitle = "", string newDescripton = "", DateTime? newDate = null, bool isCompleted = false, Lists newList = null)
        {
            CheckValidationListName(listName);
            CheckValidationTaskName(task);

            if (newTitle is null)
            {
                throw new ArgumentNullException(nameof(newTitle));
            }

            using (var db = new AppContext())
            {
                var editetTask = db.ToDoTask.Where(x => x.ListName.Name.Equals(listName)).Where(y => y.Task.Equals(task)).FirstOrDefault();
                if (editetTask != null)
                {
                    if (newTitle != string.Empty)
                    {
                        editetTask.Task = newTitle;
                    }

                    if (newDescripton != string.Empty)
                    {
                        editetTask.Description = newDescripton;
                    }
                    
                    if (newDate != null)
                    {
                        editetTask.DueDate = (DateTime)newDate;
                    }

                    if (newList != null)
                    {
                        editetTask.ListName = newList;
                    }

                    editetTask.Complete = isCompleted;

                    db.ToDoTask.Update(editetTask);
                    db.SaveChanges();
                }
            }
        }

        public static void HideOrRemoveList(string action, string name)
        {
            if (action.Equals("Remove", StringComparison.OrdinalIgnoreCase))
            {
                RemoveList(name);
            }
            else if (action.Equals("Hide", StringComparison.OrdinalIgnoreCase))
            {
                UpdateLists(name, isHidden: true);
            }
            else
            {
                throw new ArgumentException("Unknown action", nameof(action));
            }
        }

        public static void SetStatus(string listName, string taskName, bool status)
        {
            UpdateTask(listName, taskName, isCompleted: status);
        }

        public static void FinishTask(string listName, string taskName)
        {
            SetStatus(listName, taskName, true);
        }

        public static void ShowAllLists()
        {
            using (var db = new AppContext())
            {
                var lists = db.AllLists.Where(x => !x.Hide).ToList();

                Console.WriteLine("All ToDo Lists: ");
                foreach(var l in lists)
                {
                    Console.WriteLine($"{l.Name}");
                }
                Console.WriteLine("");
            }
        }

        public static void ShowAllTask(string listName)
        {
            CheckValidationListName(listName);
            using (var db = new AppContext())
            {
                var tasks = db.ToDoTask.Where(x => x.ListName.Name.Equals(listName)).ToList();

                Console.WriteLine($"All tasks in: {listName} list");
                foreach (var t in tasks)
                {
                    Console.WriteLine($"{t.Task}\nFinish: {t.Complete}\tDue Date: {t.DueDate.ToString("dd.MM.yyyy")}\nDescription: {t.Description}");
                }
                Console.WriteLine("");
            }
        }

        public static void CheckValidationListName(string listName)
        {
            if (listName is null)
            {
                throw new ArgumentNullException(nameof(listName));
            }

            if (listName == string.Empty)
            {
                throw new ArgumentException("List name can't be empty", nameof(listName));
            }
        }

        public static void CheckValidationTaskName(string taskName)
        {
            if (taskName is null)
            {
                throw new ArgumentNullException(nameof(taskName));
            }

            if (taskName == string.Empty)
            {
                throw new ArgumentException("Task name can't be empty", nameof(taskName));
            }
        }

        public static List<ToDo> CheckTaskExist(Lists tasksList)
        {
            using (var db = new AppContext())
            {
                return db.ToDoTask.Where(x => x.ListName == tasksList).ToList();
            }
        }
    }
}
