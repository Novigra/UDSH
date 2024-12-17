using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace UDSH.Model
{
    public class Session
    {
        public User User { get; set; }

        public MainWindow mainWindow { get; set; }

        public string CurrentProjectsDirectory { get; set; }

        public Project CurrentProject { get; set; }

        public int CurrentProjectIndex { get; set; }

        private string UserFileDirectory { get; set; }

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
                UserFileDirectory = UserDataFilePath;

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
                    CurrentProjectsDirectory = ProjectsPath;
                    CurrentProjectIndex = -1;
                }
                else
                {
                    Directory.CreateDirectory(ProjectsPath);
                    CurrentProjectsDirectory = ProjectsPath;

                    CurrentProjectIndex = -1;
                }
            }
        }

        public void SaveUserData()
        {

        }

        public void CreateNewProject(string NewProjectName, string ProjectVersion, bool IsSecured, string Password)
        {
            CurrentProjectIndex++;

            Project project = new Project();
            project.ProjectName = NewProjectName;
            project.ProjectAuthor = User.DisplayName;
            project.ProjectVersion = ProjectVersion;
            project.IsLastOpenedProject = true;
            project.IsProjectProtected = IsSecured;
            project.ProjectPassword = Password;
            project.ProjectCreationDate = DateTime.Now;
            project.ProjectLastModificationDate = DateTime.Now;

            User.Projects.Add(project);
            CurrentProject = project;

            try
            {
                string JsonUpdate = JsonSerializer.Serialize(User, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(UserFileDirectory, JsonUpdate);
            }
            catch
            {
                Debug.WriteLine("ERROR::FAILED UPDATING JSON FILE");
            }

            /*
             * TODO:
             * - We need to animate the project title when creating a new one.
             * - When creating a new project, we must set the old one to false (IsLastOpenedProject).
             * - Manage so when entering "ManageContentFolder", if the count is larger than zero, we open the last opened project.
             * - if the user doesn't exist we need to create a new one.
             */
        }
    }
}
