using System.ComponentModel;
using System.Windows;
using System.Runtime.CompilerServices;

namespace UDSH.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler<Point> UpdateCoordinates;

        protected void OnUpdateCoordinates(object sender, Point Position)
        {
            UpdateCoordinates?.Invoke(sender, Position);
        }
    }
}
