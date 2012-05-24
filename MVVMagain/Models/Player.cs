using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMagain.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public bool? State { get; set; }
    }
}
