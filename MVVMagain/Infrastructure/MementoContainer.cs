using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMagain.Infrastructure
{
    public class MementoContainer
    {
        private Memento memento;
        public Memento Memento
        {
            set { memento = value; }
            get { return memento; }
        }
    }
}
