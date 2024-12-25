﻿using System.Diagnostics;
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

        public int NumberOfProjects { get; set; }

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


            /*project.ProjectName = NewProjectName;
            project.ProjectAuthor = User.DisplayName;
            project.ProjectVersion = ProjectVersion;
            if (NumberOfProjects == 0)
                project.IsLastOpenedProject = true;
            project.IsProjectProtected = IsSecured;
            project.ProjectPassword = Password;
            project.ProjectCreationDate = DateTime.Now;
            project.ProjectLastModificationDate = DateTime.Now;
            project.ProjectDirectory = Path.Combine(CurrentProjectsDirectory, NewProjectName);*/

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
