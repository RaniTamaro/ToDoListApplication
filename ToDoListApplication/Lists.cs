using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ToDoListApplication
{
    public class Lists
    {
        [Key]
        public int ListId { get; set; }
        public string Name { get; set; }

        public bool Hide { get; set; }

        public virtual List<ToDo> ToDoList { get; set; }

    }

    public class ToDo
    {
        [Key]
        public int TaskId { get; set; }

        public bool Complete { get; set; }

        public string Task { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }


        public int ListId { get; set; }

        public virtual Lists ListName { get; set; }
    }
}
