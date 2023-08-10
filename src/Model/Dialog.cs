using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DlgExtract.Model
{
    internal class Dialog
    {
        public int Strref { get; set; }
        public string Text { get; set; }
        public string Name { get; internal set; }
        public string Sound { get; internal set; }
    }
}
