using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartLab.Core
{
    public class CatalogItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string category { get; set; }
        public string time_result { get; set; }
        public string preparation { get; set; }
        public string bio { get; set; }
        public CatalogButtonItem catalogButtonItem { get; set; }
    }
}
