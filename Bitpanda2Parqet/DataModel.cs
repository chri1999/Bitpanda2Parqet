using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class DataModel
    {
        // An object of this class contains a list Activities (Transactions) and methods to edit/work with the list

        public event EventHandler ModelChanged;


        private List<Activity> _activities;

        public DataModel(List<Activity> listOfActivities)
        {
            this._activities = listOfActivities;
        }


        public void SetNewDataList(List<Activity> newDataList)
        {
            _activities = newDataList;           
            ModelChanged?.Invoke(this, new EventArgs());
        }

        public void ExportParqetCSV(MainViewParameters parameters)
        {
            DataExchanger.ExportParquetCSV(GetFilteredActivityList(parameters.Settings), parameters.Sync.FilePath, parameters.Sync.FileName);
        }

        public List<Activity> GetFilteredActivityList(MainViewSettingsParameters settings)
        {
            List<Activity> filtered = new List<Activity>();

            filtered.AddRange(
            from n in _activities
            where
            n.timestamp > settings.DateOfOldestData &&
            ((settings.IgnoreStaking == true && n.isStaking == false) ||
            settings.IgnoreStaking == false)
            select n);


            return filtered;
        }


       

    }
}
