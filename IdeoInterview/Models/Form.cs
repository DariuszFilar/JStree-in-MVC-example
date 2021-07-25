using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IdeoInterview.Models
{
    public class Form
    {        
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Title { get; set; }
        
        public int id { get; set; }
        [ForeignKey("id")]
        public virtual JsTreeModel JsTreeModels { get; set; }

    }
}