using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdeoInterview.ViewModels
{
    public class JsTreeViewModel
    {
        public string parent { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public string state { get; set; }
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
        public string li_attr { get; set; }
        public string a_attr { get; set; }
    }
}