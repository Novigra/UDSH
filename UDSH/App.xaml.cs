using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace UDSH
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Debug.WriteLine("Application Launched");

            try
            {
                using StreamReader streamReader = new(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "hmm.txt"));
                string text = streamReader.ReadToEnd();
                Debug.WriteLine(text);
            }
            catch
            {
                Debug.WriteLine("Couldn't Find Hmm");
            }
        }
    }

}
