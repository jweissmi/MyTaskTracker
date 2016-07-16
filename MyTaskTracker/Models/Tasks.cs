using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyTaskTracker.Models
{
    public class Tasks
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public DateTime DueDate { get; set; }
        public string TaskText { get; set; }
        public bool TaskComplete { get; set; }

        public virtual IEnumerable <SelectListItem> SelectListItem { get; set; }

        public virtual IEnumerable SelectTaskStatus { get; set; }
    }
}