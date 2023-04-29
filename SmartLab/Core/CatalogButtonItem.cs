using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartLab.Core
{
    public class CatalogButtonItem
    {
        public string Name { get; set; }
        public ICommand ButtonCommand { get; set; }
    }
}
