using System;
using ToDoListApplication;

namespace Client
{
#pragma warning disable S1118 // Utility classes should not have public constructors
    public class Client
#pragma warning restore S1118 // Utility classes should not have public constructors
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in To Do Aplication");

            Console.WriteLine("Add a new list to DataBase");
            ToDoApplication.AddList("My Homeworks");
            ToDoApplication.ShowAllLists();

            Console.WriteLine("Add some new tasks to List");
            ToDoApplication.AddTask("My Homeworks", "English");
            ToDoApplication.AddTask("My Homeworks", "Math");
            ToDoApplication.AddTask("My Homeworks", "History");
            ToDoApplication.AddTask("My Homeworks", "Art History");
            ToDoApplication.ShowAllTask("My Homeworks");

            Console.WriteLine("Update few tasks and add desctription");
            ToDoApplication.UpdateTask("My Homeworks", "English", newDescripton: "Read pages 120 - 125", newDate: new DateTime(2022, 06, 15));
            ToDoApplication.UpdateTask("My Homeworks", "History", newDescripton: "Do exercises 1, 2, 3", newDate: new DateTime(2022, 06, 18));
            ToDoApplication.ShowAllTask("My Homeworks");

            Console.WriteLine("Finish some tasks");
            ToDoApplication.FinishTask("My Homeworks", "Math");
            ToDoApplication.FinishTask("My Homeworks", "History");
            ToDoApplication.ShowAllTask("My Homeworks");

            Console.WriteLine("Change a list name");
            ToDoApplication.UpdateLists("My Homeworks", "My Old Homeworks");
            ToDoApplication.ShowAllLists();
            ToDoApplication.ShowAllTask("My Old Homeworks");

            Console.WriteLine("Hide a list");
            ToDoApplication.HideOrRemoveList("Hide", "My Old Homeworks");
            ToDoApplication.ShowAllLists();

            Console.WriteLine("Show a list");
            ToDoApplication.UpdateLists("My Old Homeworks");
            ToDoApplication.ShowAllLists();

            Console.WriteLine("Remove a list");
            ToDoApplication.RemoveList("My Old Homeworks");
            ToDoApplication.ShowAllLists();

            Console.ReadKey();
        }
    }
}
