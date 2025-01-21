using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.Json;

namespace UDSH.Model
{
    public class Session
    {
        public User User { get; set; }

        public MainWindow mainWindow { get; set; }

        public string CurrentProjectsDirectory { get; set; }

        public Project CurrentProject { get; set; }

        public int NumberOfProjects { get; set; }

        public FileSystem CurrentFile { get; set; }

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
            string ApplicationPath = AppContext.BaseDirectory;
            string ProjectsPath = Path.Combine(ApplicationPath, "Workflow", $"{User.DisplayName}", "Projects");
            CurrentProjectsDirectory = ProjectsPath;

            if (User.Projects == null || User.Projects.Count == 0)
            {
                NumberOfProjects = 0;

                if (!Path.Exists(ProjectsPath))
                {
                    Directory.CreateDirectory(ProjectsPath);
                }
            }
            else
            {
                NumberOfProjects = User.Projects.Count;
                
                foreach (Project project in User.Projects)
                {
                    if(project.IsLastOpenedProject == true)
                        CurrentProject = project;
                }
            }
        }

        public void SaveUserData()
        {

        }

        public void CreateNewProject(string NewProjectName, string ProjectVersion, bool IsSecured, string Password)
        {
            Project project = new Project()
            {
                ProjectName = NewProjectName,
                ProjectAuthor = User.DisplayName,
                ProjectVersion = ProjectVersion,
                IsLastOpenedProject = (NumberOfProjects == 0) ? true : false,
                IsProjectProtected = IsSecured,
                ProjectPassword = Password,
                ProjectCreationDate = DateTime.Now,
                ProjectLastModificationDate = DateTime.Now,
                ProjectDirectory = Path.Combine(CurrentProjectsDirectory, NewProjectName),
                Files = new List<FileSystem>()
            };

            User.Projects.Add(project);
            NumberOfProjects++;

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

            Directory.CreateDirectory(project.ProjectDirectory);

        }

        public void CreateNewFile(string NewFileName, string FileType, string ProjectDirectory)
        {
            CurrentFile = new FileSystem()
            {
                FileName = NewFileName,
                FileType = FileType,
                FileAuthor = User.DisplayName,
                FileDirectory = Path.Combine(ProjectDirectory, NewFileName + "." + FileType),
                FileVersion = CurrentProject.ProjectVersion,
                IsLastOpenedFile = true,
                FileCreationDate = DateTime.Now,
                FileLastModificationDate = DateTime.Now
            };

            CurrentProject.Files.Add(CurrentFile);

            try
            {
                string JsonUpdate = JsonSerializer.Serialize(User, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(UserFileDirectory, JsonUpdate);

                string[] directory = CurrentFile.FileDirectory.Split(NewFileName+"."+FileType);
                Directory.CreateDirectory(directory[0]);
                FileStream fileStream = File.Create(CurrentFile.FileDirectory);
                fileStream.Close();
            }
            catch
            {
                Debug.WriteLine("ERROR::FAILED UPDATING JSON FILE");
            }
        }

        public void UpdateFileDetails()
        {
            try
            {
                string JsonUpdate = JsonSerializer.Serialize(User, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(UserFileDirectory, JsonUpdate);
            }
            catch
            {
                Debug.WriteLine("ERROR::FAILED UPDATING JSON FILE");
            }
        }
    }
}
