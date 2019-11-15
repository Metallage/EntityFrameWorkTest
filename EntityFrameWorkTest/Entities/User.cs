using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFrameWorkTest.Entities
{
    class User
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public string Telephone { get; set; }

        public override string ToString()
        {
            return Fio;
        }
    }
}
