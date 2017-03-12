using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser
{
    public class Tag
    {
        private string tagName = "";
        public string Title { get { return tagName; } }

        public Tag(string tagName)
        {
            this.tagName = tagName;
        }

    }
}
