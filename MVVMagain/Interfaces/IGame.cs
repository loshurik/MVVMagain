using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVMagain.ViewModels;
using System.Collections.ObjectModel;


namespace MVVMagain.Interfaces
{
    public interface IGame
    {
        void IncreaseScore(PlayerViewModel player);
        void DecreaseScore(PlayerViewModel player);
        ObservableCollection<PlayerViewModel> Players { get; }

    }
}
