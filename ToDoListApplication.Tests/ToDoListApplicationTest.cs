using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ToDoListApplication;

namespace ToDoListApplication.Tests
{
    /// <summary>
    /// Test for ToDoListApplication.
    /// </summary>
    public class ToDoListApplicationTest
    {
        private readonly string listName = "MyList1";
        private readonly string taskTitle = "MyTask1";
        private readonly string newListName = "MyNewList1";
        private readonly string newTaskName = "MyNewTask1";

        /// <summary>
        /// Method check for existance of list in database.
        /// </summary>
        /// <param name="name">List name.</param>
        /// <returns>True if list exist, false if doesn't exist.</returns>
        public static bool CheckListExist(string name)
        {
            using (var db = new AppContext())
            {
                var find = db.AllLists.Where(x => x.Name == name).FirstOrDefault();
                if (find != null)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Method check for existance of task in list.
        /// </summary>
        /// <param name="listName">List name.</param>
        /// <param name="task">Task name.</param>
        /// <returns>True if task exist, false if doesn't exist.</returns>
        public static bool CheckTaskExist(string listName, string task)
        {
            using (var db = new AppContext())
            {
                var find = db.ToDoTask.Where(x => x.ListName.Name == listName).Where(y => y.Task == task);
                if (find != null)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Method return a list status.
        /// </summary>
        /// <param name="listName">List name.</param>
        /// <returns>True if status is hidden, else false.</returns>
        public static bool CheckListStatus(string listName)
        {
            using (var db = new AppContext())
            {
                var find = db.AllLists.Where(x => x.Name == listName).FirstOrDefault();
                return find.Hide;
            }
        }

        /// <summary>
        /// Method return a task status.
        /// </summary>
        /// <param name="listName">List name.</param>
        /// <param name="task">Task name.</param>
        /// <returns>True if status is completed, else false.</returns>
        public static bool CheckTaskStatus(string listName, string task)
        {
            using (var db = new AppContext())
            {
                var find = db.ToDoTask.Where(x => x.ListName.Name == listName).Where(y => y.Task == task).FirstOrDefault();
                return find.Complete;
            }
        }

        /// <summary>
        /// Test check adding new list.
        /// </summary>
        [Test]
        public void AddListTest()
        {
            ToDoApplication.AddList(this.listName);

            Assert.AreEqual(true, CheckListExist(this.listName));
        }

        /// <summary>
        /// Method should return ArgumentNullException if List Name is null.
        /// </summary>
        [Test]
        public void AddListNullNameTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => ToDoApplication.AddList(null),
                message: "Method throws ArgumentNullException in case an string is null");
        }

        /// <summary>
        /// Method should return ArgumentException if List Name is empty.
        /// </summary>
        [Test]
        public void AddListEmptyNameTest()
        {
            Assert.Throws<ArgumentException>(
                () => ToDoApplication.AddList(string.Empty),
                message: "Method throws ArgumentNullException in case an string is empty");
        }

        /// <summary>
        /// Test check adding new task. After test test deleted list from database.
        /// </summary>
        [Test]
        public void AddTaskTest()
        {
            ToDoApplication.AddTask(this.listName, this.taskTitle);

            Assert.AreEqual(true, CheckTaskExist(this.listName, this.taskTitle));
            ToDoApplication.RemoveList(this.listName);
        }

        /// <summary>
        /// Method should return ArgumentNullException if List Name is null.
        /// </summary>
        [Test]
        public void AddTaskNullListNameTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => ToDoApplication.AddTask(null, "SimpleTask"),
                message: "Method throws ArgumentNullException in case an string is null");
        }

        /// <summary>
        /// Method should return ArgumentException if List Name is empty.
        /// </summary>
        [Test]
        public void AddTaskEmptyListNameTest()
        {
            Assert.Throws<ArgumentException>(
                () => ToDoApplication.AddTask(string.Empty, "SimpleTask"),
                message: "Method throws ArgumentNullException in case an string is empty");
        }

        /// <summary>
        /// Method should return ArgumentNullException if Task Name is null.
        /// </summary>
        [Test]
        public void AddTaskNullTaskNameTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => ToDoApplication.AddTask("SimpleList", null),
                message: "Method throws ArgumentNullException in case an string is null");
        }

        /// <summary>
        /// Method should return ArgumentException if Task Name is empty.
        /// </summary>
        [Test]
        public void AddTaskEmptyTaskNameTest()
        {
            Assert.Throws<ArgumentException>(
                () => ToDoApplication.AddTask("SimpleList", string.Empty),
                message: "Method throws ArgumentNullException in case an string is empty");
        }

        /// <summary>
        /// Test check removing new list.
        /// </summary>
        [Test]
        public void RemoveListTest()
        {
            string myName = "NewList1";
            ToDoApplication.AddList(myName);
            ToDoApplication.RemoveList(myName);

            Assert.AreEqual(false, CheckListExist(myName));
        }

        /// <summary>
        /// Method should return ArgumentNullException if List Name is null.
        /// </summary>
        [Test]
        public void RemoveListNullNameTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => ToDoApplication.RemoveList(null),
                message: "Method throws ArgumentNullException in case an string is null");
        }

        /// <summary>
        /// Method should return ArgumentException if List Name is empty.
        /// </summary>
        [Test]
        public void RemoveListEmptyNameTest()
        {
            Assert.Throws<ArgumentException>(
                () => ToDoApplication.RemoveList(string.Empty),
                message: "Method throws ArgumentNullException in case an string is empty");
        }

        /// <summary>
        /// Test check updating list.
        /// </summary>
        [Test]
        public void UpdateListTest()
        {
            ToDoApplication.AddList(this.listName);
            ToDoApplication.UpdateLists(this.listName, this.newListName);

            Assert.AreEqual(true, CheckListExist(this.newListName));
            ToDoApplication.RemoveList(this.newListName);
        }

        /// <summary>
        /// Method should return ArgumentNullException if List Name is null.
        /// </summary>
        [Test]
        public void UpdateListNullNameTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => ToDoApplication.UpdateLists(null),
                message: "Method throws ArgumentNullException in case an string is null");
        }

        /// <summary>
        /// Method should return ArgumentException if List Name is empty.
        /// </summary>
        [Test]
        public void UpdateListEmptyNameTest()
        {
            Assert.Throws<ArgumentException>(
                () => ToDoApplication.UpdateLists(string.Empty),
                message: "Method throws ArgumentNullException in case an string is empty");
        }

        /// <summary>
        /// Method should return ArgumentNullException if New List Name is null.
        /// </summary>
        [Test]
        public void UpdateListNullNewNameTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => ToDoApplication.UpdateLists("SimpleName", null),
                message: "Method throws ArgumentNullException in case an string is null");
        }

        /// <summary>
        /// Test check updating list.
        /// </summary>
        [Test]
        public void UpdateTaskTest()
        {
            ToDoApplication.AddList(this.listName);
            ToDoApplication.AddTask(this.listName, this.taskTitle);
            ToDoApplication.UpdateTask(this.listName, this.taskTitle, this.newTaskName, "SimpleDescription");

            Assert.AreEqual(true, CheckTaskExist(this.listName, this.newTaskName));
            ToDoApplication.RemoveList(this.listName);
        }

        /// <summary>
        /// Method should return ArgumentNullException if List Name is null.
        /// </summary>
        [Test]
        public void UpdateTaskNullListNameTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => ToDoApplication.UpdateTask(null, "SimpleTask"),
                message: "Method throws ArgumentNullException in case an string is null");
        }

        /// <summary>
        /// Method should return ArgumentException if List Name is empty.
        /// </summary>
        [Test]
        public void UpdateTaskEmptyListNameTest()
        {
            Assert.Throws<ArgumentException>(
                () => ToDoApplication.UpdateTask(string.Empty, "SimpleTask"),
                message: "Method throws ArgumentNullException in case an string is empty");
        }

        /// <summary>
        /// Method should return ArgumentNullException if Task Name is null.
        /// </summary>
        [Test]
        public void UpdateTaskNullTaskNameTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => ToDoApplication.UpdateTask("SimpleList", null),
                message: "Method throws ArgumentNullException in case an string is null");
        }

        /// <summary>
        /// Method should return ArgumentException if Task Name is empty.
        /// </summary>
        [Test]
        public void UpdateTaskEmptyTaskNameTest()
        {
            Assert.Throws<ArgumentException>(
                () => ToDoApplication.UpdateTask("SimpleList", string.Empty),
                message: "Method throws ArgumentNullException in case an string is empty");
        }

        /// <summary>
        /// Method should return ArgumentException if New Task Name is empty.
        /// </summary>
        [Test]
        public void UpdateTaskNullNewTaskNameTest()
        {
            Assert.Throws<ArgumentNullException>(
                () => ToDoApplication.UpdateTask("SimpleList", "SimpleTask", null),
                message: "Method throws ArgumentNullException in case an string is null");
        }

        /// <summary>
        /// Test check what user want to do with list.
        /// </summary>
        [Test]
        public void HideOrRemoveRemoveTest()
        {
            ToDoApplication.AddList(this.listName);
            ToDoApplication.HideOrRemoveList("Remove", this.listName);

            Assert.AreEqual(false, CheckListExist(this.listName));
        }

        /// <summary>
        /// Test check what user want to do with list.
        /// </summary>
        [Test]
        public void HideOrRemoveHideTest()
        {
            ToDoApplication.AddList(this.listName);
            ToDoApplication.HideOrRemoveList("Hide", this.listName);

            Assert.AreEqual(true, CheckListStatus(this.listName));
            ToDoApplication.RemoveList(this.listName);
        }

        /// <summary>
        /// Method should return ArgumentException if action is wrong.
        /// </summary>
        [Test]
        public void HideOrRemoveUnknowAcctionTest()
        {
            Assert.Throws<ArgumentException>(
                () => ToDoApplication.HideOrRemoveList("WrongAction", "SimpleName"),
                message: "Method throws ArgumentNullException in case an action is wrong");
        }

        /// <summary>
        /// Test check set status for task.
        /// </summary>
        [Test]
        public void SetStatusTest()
        {
            ToDoApplication.AddList(this.listName);
            ToDoApplication.AddTask(this.listName, this.taskTitle);
            ToDoApplication.SetStatus(this.listName, this.taskTitle, false);

            Assert.AreEqual(false, CheckTaskStatus(this.listName, this.taskTitle));
            ToDoApplication.RemoveList(this.listName);
        }

        /// <summary>
        /// Test check set status to completed.
        /// </summary>
        [Test]
        public void FinishTaskTest()
        {
            ToDoApplication.AddList(this.listName);
            ToDoApplication.AddTask(this.listName, this.taskTitle);
            ToDoApplication.FinishTask(this.listName, this.taskTitle);

            Assert.AreEqual(true, CheckTaskStatus(this.listName, this.taskTitle));
            ToDoApplication.RemoveList(this.listName);
        }
    }
}
