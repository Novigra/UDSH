using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace UDSH.Model
{
    public class Session
    {
        public User User { get; set; }

        public MainWindow mainWindow { get; set; }

        public Session()
        {
            LoadUserData();

            Debug.WriteLine("Session Started...");
        }

        public void LoadUserData()
        {
            try
            {
                /*
                 * Important Notes:
                 * OFFLINE:
                 *  - We have a json file in the AppData/Roaming
                 *  - TODO: When creating a new user windows don't forget to implement new user logic.
                 * 
                 * ONLINE:
                 *  - TODO: when having online functionalities, add Mongo DB and Azure implementaitons and sync the data.
                 */

                string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UDSH");
                if (!Directory.Exists(AppData))
                    Directory.CreateDirectory(AppData);

                string UserDataFilePath = Path.Combine(AppData, "UserData.json");

                // Consider Disposing if necessary
                JsonFileReader reader = new JsonFileReader();
                string JsonFile = reader.Read(UserDataFilePath, FileType.UserData);

                if (!string.IsNullOrEmpty(JsonFile))
                {
                    User = JsonSerializer.Deserialize<User>(JsonFile);
                    ManageContentFolder();
                    Debug.WriteLine($"User: {User.DisplayName}");
                }
                else
                {
                    // Create a new user
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR::CANNOT READ JSON FILE::MESSAGE::{ex}");
            }
        }

        private void ManageContentFolder()
        {
            if(User.Projects.Count == 0)
            {
                string ApplicationPath = AppContext.BaseDirectory;
                string ProjectsPath = Path.Combine(ApplicationPath, "Workflow", $"{User.DisplayName}", "Projects");
                if(Path.Exists(ProjectsPath))
                {

                }
                else
                {
                    Directory.CreateDirectory(ProjectsPath);
                }
            }
        }

        public void SaveUserData()
        {

        }
    }
}
