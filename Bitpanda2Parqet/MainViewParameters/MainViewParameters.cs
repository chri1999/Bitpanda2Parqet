using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{

    public class MainViewParameters
    {
        // An object of this class stores strings entered into the MainView textboxes

        public MainViewSyncParameters Sync {get; set;}

        public MainViewSettingsParameters Settings { get; set; }


        public MainViewParameters(MainViewSyncParameters sync, MainViewSettingsParameters settings)
        {
            Sync = sync;
            Settings = settings;
        }

    }
}
