using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMagain.ViewModels;


namespace MVVMagain.Interfaces
{
    public interface IGame
    {
        void IncreaseScore(PlayerViewModel player);
        void DecreaseScore(PlayerViewModel player);
    }
}
