using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameWorkTest.Entities
{
    class Install
    {
        public int Id { get; set; }
        public int CompId { get; set; }
        public int SoftId { get; set; } 
        public string Version { get; set; }

    }
}
