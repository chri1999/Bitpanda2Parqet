using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpanda2Parqet
{
    public class DataModel
    {
        public event EventHandler ModelChanged;


        public List<Activity> activities { get; private set; }

        public DataModel(List<Activity> listOfActivities)
        {
            this.activities = listOfActivities;
        }


        public void SetNewDataList(List<Activity> newDataList)
        {
            activities = newDataList;           
            ModelChanged?.Invoke(this, new EventArgs());
        }

        public void ExportParqetCSV(string filePath)
        {
            DataExchanger.ExportParquetCSV(activities, filePath);
        }


       

    }
}
