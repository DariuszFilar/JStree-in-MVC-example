using IdeoInterview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeoInterview.ViewModels
{
    public class FormViewModel
    {
        public FormViewModel()
        {

        }
        public FormViewModel(Form form)
        {
            this.Title = form.Title;
            this.Question = form.Question;
            this.Answer = form.Answer;
            this.id = form.id;            
        }
        public string Title { get; set; }

        public string Question { get; set; }
        public string Answer { get; set; }
        public int id { get; set; }
        public JsTreeModel Node { get; }
    }
}