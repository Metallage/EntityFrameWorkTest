using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameWorkTest.Entities
{
    class Software
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
