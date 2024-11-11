using System.Collections.ObjectModel;
using UDSH.Model;
using UDSH.MVVM;

namespace UDSH.ViewModel
{
    internal class CSFUserControlViewModel : ViewModelBase
    {
        public ObservableCollection<CSFData> CSFData { get; set; }
        public CSFUserControlViewModel()
        {
            CSFData = new ObservableCollection<CSFData>();
            CSFData.Add(new CSFData
            {
                Content = "",
                DataType = "Scene Heading",
                IsConnected = false
            });
            CSFData.Add(new CSFData
            {
                Content = "",
                DataType = "Action",
                IsConnected = false
            });

            //SelectedData = CSFData[0];
        }

        private CSFData selectedData;
        public bool ContentChanged { get { return string.IsNullOrEmpty(selectedData.Content); } }
        public CSFData SelectedData
        {
            get {  return selectedData; }
            set
            {
                selectedData = value;
                OnPropertyChanged();
            }
        }
    }
}
