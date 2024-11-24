using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UDSH.MVVM;

namespace UDSH.ViewModel
{
    class NewFileCreationWindowViewModel : ViewModelBase
    {
        private bool isItemSelected;
        public bool IsItemSelected
        {
            get { return isItemSelected; }
            set { isItemSelected = value; OnPropertyChanged(); }
        }

        private string currentDatatype;
        public string CurrentDatatype
        {
            get { return currentDatatype; }
            set { currentDatatype = value; OnPropertyChanged(); }
        }

        public Window CurrentWindow { get; set; }
        public Grid? GridTarget { get; set; }
        public RelayCommand<Button> CloseWindow => new RelayCommand<Button>(execute => CloseNewFileProcessWindow());
        public RelayCommand<Grid> LoadedGrid => new RelayCommand<Grid>(OnGridLoaded);
        public RelayCommand<string> SelectDataType => new RelayCommand<string>(SelectData);

        public NewFileCreationWindowViewModel(Window window)
        {
            CurrentWindow = window;
            IsItemSelected = false;
        }

        private void OnGridLoaded(Grid grid)
        {
            GridTarget = grid;
        }

        private void CloseNewFileProcessWindow()
        {
            var Storyboard = new Storyboard();
            var ClosingTranslateAnimation = new DoubleAnimation
            {
                To = 100.0f,
                Duration = TimeSpan.FromSeconds(0.3),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(ClosingTranslateAnimation, GridTarget);
            Storyboard.SetTargetProperty(ClosingTranslateAnimation, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            Storyboard.Children.Add(ClosingTranslateAnimation);

            var ClosingOpacityAnimation = new DoubleAnimation
            {
                To = 0.0f,
                Duration = TimeSpan.FromSeconds(0.3),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(ClosingOpacityAnimation, GridTarget);
            Storyboard.SetTargetProperty(ClosingOpacityAnimation, new PropertyPath("Opacity"));
            Storyboard.Children.Add(ClosingOpacityAnimation);

            Storyboard.Completed += (sender, args) => { CurrentWindow.Close(); };
            Storyboard.Begin();
        }

        // Tried passing string then converting to int. will try just using string(of course string are operation heavy, but it is more readable).
        // Don't forget to do profiling!
        private void SelectData(string dataType)
        {
            IsItemSelected = true;
            CurrentDatatype = dataType;
        }
    }
}
