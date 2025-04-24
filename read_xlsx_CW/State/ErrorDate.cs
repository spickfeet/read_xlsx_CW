using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_read_xlsx.State
{
    public class ErrorData
    {
        public int Line { get; set; }
        public int LocalIndex { get; set; }
        public string Text { get; set; }
        public int GlobalIndex { get; set; }

        public ErrorData(int line, int localIndex, string text, int globalIndex) 
        {
            Line = line;
            LocalIndex = localIndex;
            Text = text;
            GlobalIndex = globalIndex;
        }

    }
}
