using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameWorkTest.Entities
{
    class Computer
    {
        public int Id { get; set; }
        public string NsName { get; set; }
        public string Ip { get; set; }

        public int? UserId { get; set; }

        public override string ToString()
        {
            return NsName;
        }
    }
}
