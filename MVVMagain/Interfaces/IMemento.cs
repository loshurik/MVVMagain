using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMagain.Infrastructure;

namespace MVVMagain.Interfaces
{
    public interface IMemento
    {
        Memento SaveMemento();
        void RestoreMemento(Memento memento);
    }
}
