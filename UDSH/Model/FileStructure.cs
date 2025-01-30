using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;
using UDSH.ViewModel;

namespace UDSH.Model
{
    /// <summary>
    /// Class responsible for creating the structure of the system when it comes to where
    /// are the files stored and so on.
    /// </summary>
    public class FileStructure
    {
        string[] Sizes = { "KB", "MB", "GB", "TB" };

        /// <summary>
        /// Build a single directory of the project
        /// </summary>
        /// <param name="project"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public ObservableCollection<ContentFileStructure> CreateCurrentLevelList(Project project, string currentDirectory, ContentSort sort)
        {
            ObservableCollection<ContentFileStructure> contentFileStructures = new ObservableCollection<ContentFileStructure>();
            string[] Directories = Directory.GetDirectories(currentDirectory);
            string[] Files = Directory.GetFiles(currentDirectory);

            foreach (var directory in Directories)
            {
                DirectoryInfo DirectoryInfo = new DirectoryInfo(directory);
                var DirectorySize = Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories).AsParallel().Select(file => new FileInfo(file).Length).Sum();
                double Size = DirectorySize / 1024.0;
                int index = 0;
                while (Size >= 1024 && index < Sizes.Length - 1)
                {
                    Size /= 1024;
                    index++;
                }
                string FinalSize = Size.ToString("F") + " " + Sizes[index];

                contentFileStructures.Add(new ContentFileStructure
                {
                    Name = DirectoryInfo.Name,
                    LastDateModification = DirectoryInfo.LastWriteTime,
                    Type = "Folder",
                    Size = FinalSize,
                    Author = project.ProjectAuthor,
                    Directory = directory,
                    Image = new BitmapImage(new Uri("pack://application:,,,/Resource/FolderContent.png")),
                    LargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Folder_Large.png")),
                    LargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/HighlightFolder_Large.png")),
                    File = null
                });
            }

            foreach (var file in Files)
            {
                foreach (var savedFile in project.Files)
                {
                    BitmapImage BitmapLargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Placeholder.png"));
                    BitmapImage BitmapLargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Placeholder.png"));
                    switch (savedFile.FileType)
                    {
                        case "mkb":
                            BitmapLargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/BN_Large.png"));
                            BitmapLargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/HighlightMKB_Large.png"));
                            break;
                        case "mkc":
                            BitmapLargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/MKC_Large.png"));
                            BitmapLargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/HighlightMKC_Large.png"));
                            break;
                        case "mkm":
                            BitmapLargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/MKM_Large.png"));
                            BitmapLargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/HighlightMKM_Large.png"));
                            break;
                        default:
                            break;
                    }

                    if(file.Equals(savedFile.FileDirectory))
                    {
                        contentFileStructures.Add(new ContentFileStructure
                        {
                            Name = savedFile.FileName,
                            LastDateModification = savedFile.FileLastModificationDate,
                            Type = savedFile.FileType.ToUpper(),
                            Size = savedFile.FileSize,
                            Author = savedFile.FileAuthor,
                            Directory = string.Empty,
                            Image = new BitmapImage(new Uri("pack://application:,,,/Resource/FileType.png")),
                            LargeNormalImage = BitmapLargeNormalImage,
                            LargeHighlightImage = BitmapLargeHighlightImage,
                            File = savedFile
                        });
                        break;
                    }
                }
            }

            ObservableCollection<ContentFileStructure> SortedStructure = new ObservableCollection<ContentFileStructure>();
            SortedStructure = SortListItems(contentFileStructures, sort);

            return SortedStructure;
        }

        public ObservableCollection<ContentFileStructure> UpdateCurrentLevelList(ObservableCollection<ContentFileStructure> currentFiles, Project project, FileSystem AddedFile, string CurrentDirectory, ContentSort sort)
        {
            string[] TextSplit = AddedFile.FileDirectory.Split(CurrentDirectory);
            if (TextSplit[1].Contains('\\'))
            {
                string[] PathSplit = TextSplit[1].Split('\\');
                string FolderName = PathSplit[0];
                bool CanProceed = true;

                foreach (var item in currentFiles)
                {
                    if (item.Name.Equals(FolderName))
                    {
                        CanProceed = false;
                        break;
                    }
                }

                if (CanProceed == true)
                {
                    string directory = Path.Combine(CurrentDirectory, FolderName);

                    DirectoryInfo DirectoryInfo = new DirectoryInfo(directory);
                    var DirectorySize = Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories).AsParallel().Select(file => new FileInfo(file).Length).Sum();
                    double Size = DirectorySize / 1024.0;
                    int index = 0;
                    while (Size >= 1024 && index < Sizes.Length - 1)
                    {
                        Size /= 1024;
                        index++;
                    }
                    string FinalSize = Size.ToString("F") + " " + Sizes[index];

                    currentFiles.Add(new ContentFileStructure
                    {
                        Name = DirectoryInfo.Name,
                        LastDateModification = DirectoryInfo.LastWriteTime,
                        Type = "Folder",
                        Size = FinalSize,
                        Author = project.ProjectAuthor,
                        Directory = directory,
                        Image = new BitmapImage(new Uri("pack://application:,,,/Resource/FolderContent.png")),
                        LargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Folder_Large.png")),
                        LargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/HighlightFolder_Large.png")),
                        File = null
                    });

                    return SortListItems(currentFiles, sort);
                }
                else
                {
                    return currentFiles;
                }
            }
            else
            {
                string FileName = TextSplit[1];
                string file = Path.Combine(CurrentDirectory, FileName);

                foreach (var savedFile in project.Files)
                {
                    BitmapImage BitmapLargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Placeholder.png"));
                    BitmapImage BitmapLargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Placeholder.png"));
                    switch (savedFile.FileType)
                    {
                        case "mkb":
                            BitmapLargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/BN_Large.png"));
                            BitmapLargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/HighlightMKB_Large.png"));
                            break;
                        case "mkc":
                            BitmapLargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/MKC_Large.png"));
                            BitmapLargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/HighlightMKC_Large.png"));
                            break;
                        case "mkm":
                            BitmapLargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/MKM_Large.png"));
                            BitmapLargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/HighlightMKM_Large.png"));
                            break;
                        default:
                            break;
                    }

                    if (file.Equals(savedFile.FileDirectory))
                    {
                        currentFiles.Add(new ContentFileStructure
                        {
                            Name = savedFile.FileName,
                            LastDateModification = savedFile.FileLastModificationDate,
                            Type = savedFile.FileType.ToUpper(),
                            Size = savedFile.FileSize,
                            Author = savedFile.FileAuthor,
                            Directory = string.Empty,
                            Image = new BitmapImage(new Uri("pack://application:,,,/Resource/FileType.png")),
                            LargeNormalImage = BitmapLargeNormalImage,
                            LargeHighlightImage = BitmapLargeHighlightImage,
                            File = savedFile
                        });
                        break;
                    }
                }

                return SortListItems(currentFiles, sort);
            }
        }

        public ObservableCollection<ContentFileStructure> CreateFolder(ObservableCollection<ContentFileStructure> CurrentFiles, Project project, string directory, ContentSort sort)
        {
            DirectoryInfo DirectoryInfo = new DirectoryInfo(directory);
            DirectoryInfo.Create();

            CurrentFiles.Add(new ContentFileStructure
            {
                Name = DirectoryInfo.Name,
                LastDateModification = DirectoryInfo.LastWriteTime,
                Type = "Folder",
                Size = "0.00 KB",
                Author = project.ProjectAuthor,
                Directory = directory,
                Image = new BitmapImage(new Uri("pack://application:,,,/Resource/FolderContent.png")),
                LargeNormalImage = new BitmapImage(new Uri("pack://application:,,,/Resource/Folder_Large.png")),
                LargeHighlightImage = new BitmapImage(new Uri("pack://application:,,,/Resource/HighlightFolder_Large.png")),
                File = null
            });

            ObservableCollection<ContentFileStructure> SortedStructure = new ObservableCollection<ContentFileStructure>();
            SortedStructure = SortListItems(CurrentFiles, sort);

            return SortedStructure;
        }

        public void AddEmptyFolderTreeItemName(Node root, Project project, string directory)
        {
            string[] TextSplit = directory.Split('\\');
            int index = Array.IndexOf(TextSplit, project.ProjectName);
            string ItemName = string.Empty;

            if (!string.IsNullOrEmpty(TextSplit.Last()))
                ItemName = TextSplit.Last();
            else
                ItemName = TextSplit[TextSplit.Length - 2];

            Node CurrentNode = root;

            for (int i = index + 1; i < TextSplit.Length; ++i)
            {
                Node? NextNode = null;

                foreach (var node in CurrentNode.SubNodes)
                {
                    if (node.NodeType == DataType.File)
                        break;

                    if (node.Name.Equals(TextSplit[i]))
                    {
                        NextNode = node;
                        break;
                    }
                }

                if (NextNode == null)
                    break;

                CurrentNode = NextNode;
            }

            CurrentNode.SubNodes.Add(new Node
            {
                Name = ItemName,
                NodeImage = new BitmapImage(new Uri("pack://application:,,,/Resource/FolderContent.png")),
                NodeType = DataType.Folder,
                NodeDirectory = directory,
                NodeFile = null,
                SubNodes = new ObservableCollection<Node>()
            });

            CurrentNode.SubNodes = new ObservableCollection<Node>(CurrentNode.SubNodes.OrderBy(d => d.NodeType == DataType.File).ThenBy(n => n.Name, new SortComparer()));
        }

        public ObservableCollection<ContentFileStructure> SortListItems(ObservableCollection<ContentFileStructure> currentFiles, ContentSort sort)
        {
            ObservableCollection<ContentFileStructure> SortedStructure = new ObservableCollection<ContentFileStructure>();
            switch (sort)
            {
                case ContentSort.FilesFirst_Ascending:
                    SortedStructure = new ObservableCollection<ContentFileStructure>(currentFiles.OrderBy(item => item.Type.Contains("Folder")).ThenBy(n => n.Name, new SortComparer()));
                    break;
                case ContentSort.FilesFirst_Descending:
                    SortedStructure = new ObservableCollection<ContentFileStructure>(currentFiles.OrderBy(item => item.Type.Contains("Folder")).ThenByDescending(n => n.Name, new SortComparer()));
                    break;
                case ContentSort.FoldersFirst_Ascending:
                    SortedStructure = new ObservableCollection<ContentFileStructure>(currentFiles.OrderBy(item => item.Type.Contains("MK")).ThenBy(n => n.Name, new SortComparer()));
                    break;
                case ContentSort.FoldersFirst_Descending:
                    SortedStructure = new ObservableCollection<ContentFileStructure>(currentFiles.OrderBy(item => item.Type.Contains("MK")).ThenByDescending(n => n.Name, new SortComparer()));
                    break;
                default:
                    break;
            }

            return SortedStructure;
        }

        public ObservableCollection<ContentFileStructure> FilterListItems(ObservableCollection<ContentFileStructure> currentFiles, ContentFilter filter)
        {
            switch (filter)
            {
                case ContentFilter.None:
                    return new ObservableCollection<ContentFileStructure>();
                case ContentFilter.MKB:
                    return ConstructFilteredList(currentFiles, "mkb");
                case ContentFilter.MKC:
                    return ConstructFilteredList(currentFiles, "mkc");
                case ContentFilter.MKM:
                    return ConstructFilteredList(currentFiles, "mkm");
                default:
                    return new ObservableCollection<ContentFileStructure>();
            }
        }

        private ObservableCollection<ContentFileStructure> ConstructFilteredList(ObservableCollection<ContentFileStructure> currentFiles, string type)
        {
            ObservableCollection<ContentFileStructure> FilteredStructure = new ObservableCollection<ContentFileStructure>();

            foreach(var file in currentFiles)
            {
                if (file.File != null)
                {
                    if (file.File.FileType.Equals(type))
                        FilteredStructure.Add(file);
                }
            }
            return FilteredStructure;
        }

        /// <summary>
        /// Build Content Tree, Sorted.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public Node ContentBuildSideTree(Project project)
        {
            Node root = new Node { Name = project.ProjectName, NodeType = DataType.Folder };

            foreach (var file in project.Files)
            {
                string[] TextSplit = file.FileDirectory.Split('\\');
                string DirectoryBuildUp = project.ProjectDirectory + "\\";

                // Start at the project root as the user could be storing the app files deep inside.
                int index = Array.IndexOf(TextSplit, project.ProjectName);

                Node CurrentNode = root;
                for (int i = index + 1; i < TextSplit.Length; ++i)
                {
                    string CurrentFileNodeDirectory = file.FileDirectory;
                    BitmapImage CurrentImage;
                    DataType CurrentType;
                    bool IsFile = false;
                    if (TextSplit[i].Contains("mk"))
                    {
                        IsFile = true;
                        CurrentType = DataType.File;
                        CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/FileType.png"));
                    }
                    else
                    {
                        CurrentFileNodeDirectory = DirectoryBuildUp + TextSplit[i] + "\\";
                        DirectoryBuildUp = CurrentFileNodeDirectory;
                        CurrentType = DataType.Folder;
                        CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/FolderContent.png"));
                    }

                    // Avoid replicas
                    var matchingNodes = CurrentNode.SubNodes.Where(n => n.Name == TextSplit[i]).ToList();
                    Node? subNode = matchingNodes.Count > 0 ? matchingNodes[0] : null;

                    if (subNode == null)
                    {
                        subNode = new Node { Name = TextSplit[i], NodeImage = CurrentImage, NodeType = CurrentType, NodeDirectory = CurrentFileNodeDirectory, NodeFile = (IsFile == true) ? file : null };
                        CurrentNode.SubNodes.Add(subNode);
                    }

                    CurrentNode = subNode;
                }
            }

            // Sorting and adding to the Root TreeView
            SortNodes(root);

            return root;
        }

        public Node BuildSideContentTree(Project project)
        {
            Node root = new Node { Name = project.ProjectName, NodeType = DataType.Folder };

            foreach (var file in project.Files)
            {
                string[] TextSplit = file.FileDirectory.Split('\\');
                string DirectoryBuildUp = project.ProjectDirectory + "\\";

                // Start at the project root as the user could be storing the app files deep inside.
                int index = Array.IndexOf(TextSplit, project.ProjectName);

                Node CurrentNode = root;
                for (int i = index + 1; i < TextSplit.Length; ++i)
                {
                    string CurrentFileNodeDirectory = file.FileDirectory;
                    BitmapImage CurrentImage;
                    DataType CurrentType;
                    bool IsFile = false;
                    if (TextSplit[i].Contains(".mkm"))
                    {
                        IsFile = true;
                        CurrentType = DataType.File;
                        CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKM.png"));
                    }
                    else if (TextSplit[i].Contains(".mkc"))
                    {
                        IsFile = true;
                        CurrentType = DataType.File;
                        CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKC.png"));
                    }
                    else if (TextSplit[i].Contains(".mkb"))
                    {
                        IsFile = true;
                        CurrentType = DataType.File;
                        CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKB.png"));
                    }
                    else
                    {
                        CurrentFileNodeDirectory = DirectoryBuildUp + TextSplit[i] + "\\";
                        DirectoryBuildUp = CurrentFileNodeDirectory;
                        CurrentType = DataType.Folder;
                        CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/FolderSidebar.png"));
                    }

                    // Avoid replicas
                    var matchingNodes = CurrentNode.SubNodes.Where(n => n.Name == TextSplit[i]).ToList();
                    Node? subNode = matchingNodes.Count > 0 ? matchingNodes[0] : null;

                    if (subNode == null)
                    {
                        subNode = new Node { Name = TextSplit[i], NodeImage = CurrentImage, NodeType = CurrentType, NodeDirectory = CurrentFileNodeDirectory, NodeFile = (IsFile == true) ? file : null };
                        CurrentNode.SubNodes.Add(subNode);
                    }

                    CurrentNode = subNode;
                }
            }

            SortNodes(root);

            return root;
        }

        public void BuildSearchTree(Node root, ObservableCollection<Node> RootSearch)
        {
            if (root.NodeType == DataType.File)
                RootSearch.Add(root);

            foreach (var subNode in root.SubNodes)
            {
                BuildSearchTree(subNode, RootSearch);
            }
        }

        public Node AddNewFile(Project project, Node Root, FileSystem file, bool IsContentWindow = false)
        {
            string[] TextSplit = file.FileDirectory.Split('\\');
            string DirectoryBuildUp = project.ProjectDirectory + "\\";

            int index = Array.IndexOf(TextSplit, project.ProjectName);

            if (Root == null)
                Root = new Node { Name = project.ProjectName, NodeType = DataType.Folder };

            Node CurrentNode = Root;
            Node AddedFileNode = new Node();

            for (int i = index + 1; i < TextSplit.Length; ++i)
            {
                string CurrentFileNodeDirectory = file.FileDirectory;
                BitmapImage CurrentImage;
                DataType CurrentType;
                bool IsFile = false;

                switch (IsContentWindow)
                {
                    case true:
                        if (TextSplit[i].Contains("mk"))
                        {
                            IsFile = true;
                            CurrentType = DataType.File;
                            CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/FileType.png"));
                        }
                        else
                        {
                            CurrentFileNodeDirectory = DirectoryBuildUp + TextSplit[i] + "\\";
                            DirectoryBuildUp = CurrentFileNodeDirectory;
                            CurrentType = DataType.Folder;
                            CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/FolderContent.png"));
                        }
                        break;

                    case false:
                        if (TextSplit[i].Contains(".mkm"))
                        {
                            IsFile = true;
                            CurrentType = DataType.File;
                            CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKM.png"));
                        }
                        else if (TextSplit[i].Contains(".mkc"))
                        {
                            IsFile = true;
                            CurrentType = DataType.File;
                            CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKC.png"));
                        }
                        else if (TextSplit[i].Contains(".mkb"))
                        {
                            IsFile = true;
                            CurrentType = DataType.File;
                            CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKB.png"));
                        }
                        else
                        {
                            CurrentFileNodeDirectory = DirectoryBuildUp + TextSplit[i] + "\\";
                            DirectoryBuildUp = CurrentFileNodeDirectory;
                            CurrentType = DataType.Folder;
                            CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/FolderSidebar.png"));
                        }
                        break;
                }

                // Avoid replicas
                var matchingNodes = CurrentNode.SubNodes.Where(n => n.Name == TextSplit[i]).ToList();
                Node? subNode = matchingNodes.Count > 0 ? matchingNodes[0] : null;

                if (subNode == null)
                {
                    subNode = new Node { Name = TextSplit[i], NodeImage = CurrentImage, NodeType = CurrentType, NodeDirectory = CurrentFileNodeDirectory, NodeFile = (IsFile == true) ? file : null };
                    CurrentNode.SubNodes.Add(subNode);

                    var sortedNodes = CurrentNode.SubNodes.OrderBy(d => d.NodeType == DataType.File).ThenBy(n => n.Name, new SortComparer()).ToList();
                    CurrentNode.SubNodes.Clear();
                    foreach (var node in sortedNodes)
                    {
                        CurrentNode.SubNodes.Add(node);
                    }

                    if (IsFile == true)
                        AddedFileNode = subNode;
                }

                CurrentNode = subNode;
            }

            return AddedFileNode;
        }

        private void SortNodes(Node root)
        {
            root.SubNodes = new ObservableCollection<Node>(root.SubNodes.OrderBy(d => d.NodeType == DataType.File).ThenBy(n => n.Name, new SortComparer()));

            foreach (var subNode in root.SubNodes)
            {
                SortNodes(subNode);
            }
        }

        public void UpdateTreeItemName(Node root, Project project, ContentFileStructure SelectedItem, string OldDirectory, int CurrentLevel)
        {
            string[] TextSplit = OldDirectory.Split('\\');
            int index = Array.IndexOf(TextSplit, project.ProjectName);
            Node CurrentNode = root;
            Node ParentNode = root;

            for (int i = index + 1; i < TextSplit.Length; ++i)
            {
                Node? NextNode = null;

                foreach (var node in CurrentNode.SubNodes)
                {
                    if (node.Name.Equals(TextSplit[i]))
                    {
                        NextNode = node;
                        break;
                    }
                }

                if (NextNode == null)
                    break;

                ParentNode = CurrentNode;
                CurrentNode = NextNode;
            }

            
            if (SelectedItem.File != null)
            {
                CurrentNode.NodeDirectory = SelectedItem.File.FileDirectory;

                CurrentNode.Name = SelectedItem.Name + "." + SelectedItem.File.FileType;
                CurrentNode.NodeFile = SelectedItem.File;
            }
            else
            {
                CurrentNode.NodeDirectory = SelectedItem.Directory;

                CurrentNode.Name = SelectedItem.Name;
                UpdateFileNodesDirectory(CurrentNode, SelectedItem, CurrentLevel);
            }

            ParentNode.SubNodes = new ObservableCollection<Node>(ParentNode.SubNodes.OrderBy(d => d.NodeType == DataType.File).ThenBy(n => n.Name, new SortComparer()));
        }

        private void UpdateFileNodesDirectory(Node CurrentRoot, ContentFileStructure SelectedItem, int CurrentLevel)
        {
            Stack<Node> StackNode = new Stack<Node>();
            StackNode.Push(CurrentRoot);

            string DirectoryName = SelectedItem.Name;
            bool SkipNodeToggle = true;

            while (StackNode.Count > 0)
            {
                Node CurrentNode = StackNode.Pop();

                if (SkipNodeToggle == true)
                {
                    SkipNodeToggle = false;
                }
                else
                {
                    if (CurrentNode.NodeType == DataType.File)
                    {
                        string[] DirectorySplit = CurrentNode.NodeFile.FileDirectory.Split('\\');
                        DirectorySplit[CurrentLevel] = DirectoryName;

                        string NewFileDirectory = string.Empty;

                        for (int i = 0; i < DirectorySplit.Length; ++i)
                        {
                            if (i == DirectorySplit.Length - 1)
                                NewFileDirectory += DirectorySplit[i];
                            else
                                NewFileDirectory += DirectorySplit[i] + "\\";
                        }

                        CurrentNode.NodeDirectory = NewFileDirectory;
                        CurrentNode.NodeFile.FileDirectory = NewFileDirectory;
                    }
                    else if (CurrentNode.NodeType == DataType.Folder)
                    {
                        string[] DirectorySplit = CurrentNode.NodeDirectory.Split('\\');
                        DirectorySplit[CurrentLevel] = DirectoryName;

                        string NewFileDirectory = string.Empty;

                        foreach (var text in DirectorySplit)
                        {
                            if (!string.IsNullOrEmpty(text))
                                NewFileDirectory += text + "\\";
                        }

                        CurrentNode.NodeDirectory = NewFileDirectory;
                    }
                }

                if (CurrentNode.SubNodes != null && CurrentNode.SubNodes.Count > 0)
                {
                    foreach (Node Node in CurrentNode.SubNodes)
                        StackNode.Push(Node);
                }
            }
        }

        public void RenameItem(ContentFileStructure ContentItem, string ItemNewName)
        {
            FileSystem file = new FileSystem();
            file = ContentItem.File;

            if (file != null)
            {
                string[] DirectorySplit = file.FileDirectory.Split("\\");
                DirectorySplit[DirectorySplit.Length - 1] = ItemNewName + "." + file.FileType;
                string NewFileDirectory = string.Empty;
                for (int i = 0; i < DirectorySplit.Length; ++i)
                {
                    if (i == DirectorySplit.Length - 1)
                        NewFileDirectory += DirectorySplit[i];
                    else
                        NewFileDirectory += DirectorySplit[i] + "\\";
                }

                File.Move(file.FileDirectory, NewFileDirectory);
                File.SetLastWriteTime(NewFileDirectory, DateTime.Now);

                file.FileName = ItemNewName;
                file.FileDirectory = NewFileDirectory;
                file.FileLastModificationDate = DateTime.Now;

                ContentItem.File = file;

                ContentItem.Name = ItemNewName;
                ContentItem.LastDateModification = file.FileLastModificationDate;
            }
            else
            {
                string[] TextSplit = ContentItem.Directory.Split("\\");
                if (!string.IsNullOrEmpty(TextSplit.Last()))
                {
                    TextSplit[TextSplit.Length - 1] = ItemNewName;
                }
                else
                {
                    TextSplit[TextSplit.Length - 2] = ItemNewName;
                }

                string NewFolderDirectory = string.Empty;
                foreach (string text in TextSplit)
                {
                    NewFolderDirectory += text + "\\";
                }

                Directory.Move(ContentItem.Directory, NewFolderDirectory);
                Directory.SetLastWriteTime(NewFolderDirectory, DateTime.Now);

                ContentItem.Name = ItemNewName;
                ContentItem.Directory = NewFolderDirectory;
                ContentItem.LastDateModification = DateTime.Now;
            }
        }

        public void DeleteItem(ObservableCollection<ContentFileStructure> CurrentFiles, Project project, ContentFileStructure SelectedItem)
        {
            if (SelectedItem.File != null)
            {
                string FileDirectory = SelectedItem.File.FileDirectory;
                
                foreach (var file in project.Files)
                {
                    if (file == SelectedItem.File)
                    {
                        project.Files.Remove(file);
                        break;
                    } 
                }

                foreach (var contentItem in CurrentFiles)
                {
                    if (contentItem == SelectedItem)
                    {
                        CurrentFiles.Remove(contentItem);
                        break;
                    }
                }

                File.Delete(FileDirectory);
            }
            else
            {
                List<string> Directories = Directory.EnumerateFiles(SelectedItem.Directory, "*", SearchOption.AllDirectories).ToList();

                foreach (var directory in Directories)
                {
                    foreach (var file in project.Files.ToList())
                    {
                        if (file.FileDirectory.Equals(directory))
                        {
                            project.Files.Remove(file);
                        }
                    }
                }

                foreach (var contentItem in CurrentFiles)
                {
                    if (contentItem == SelectedItem)
                    {
                        CurrentFiles.Remove(contentItem);
                        break;
                    }
                }

                Directory.Delete(SelectedItem.Directory, true);
            }
        }

        public void UpdateTreeAfterDeletion(Node Root, string Directory)
        {
            Stack<Node> StackNode = new Stack<Node>();
            StackNode.Push(Root);

            while (StackNode.Count > 0)
            {
                Node CurrentNode = StackNode.Pop();
                foreach (var Node in CurrentNode.SubNodes.ToList())
                {
                    if (Node.NodeDirectory.Contains(Directory))
                    {
                        CurrentNode.SubNodes.Remove(Node);
                    }
                }

                if (CurrentNode.SubNodes != null && CurrentNode.SubNodes.Count > 0)
                {
                    foreach (Node Node in CurrentNode.SubNodes)
                        StackNode.Push(Node);
                }
            }
        }

        public void RemoveEmptyFolderNodes(Node Root)
        {
            Stack<Node> StackNode = new Stack<Node>();
            StackNode.Push(Root);

            while (StackNode.Count > 0)
            {
                Node CurrentNode = StackNode.Pop();
                foreach (var Node in CurrentNode.SubNodes.ToList())
                {
                    if (Node.NodeType == DataType.Folder && Node.SubNodes.Count == 0)
                    {
                        CurrentNode.SubNodes.Remove(Node);
                    }
                }

                if (CurrentNode.SubNodes != null && CurrentNode.SubNodes.Count > 0)
                {
                    foreach (Node Node in CurrentNode.SubNodes)
                        StackNode.Push(Node);
                }
            }
        }

        public void MoveItemsTo(ObservableCollection<ContentFileStructure> CurrentFiles, List<ContentFileStructure> SelectedItems, ContentFileStructure TargetItem, Project project)
        {
            string newFilesDirectory = TargetItem.Directory;
            if (!newFilesDirectory.EndsWith("\\"))
                newFilesDirectory += "\\";
            
            foreach (var item in SelectedItems)
            {
                if (item.File != null)
                {
                    string OldDirectory = item.File.FileDirectory;

                    string directory = newFilesDirectory + item.Name + "." + item.File.FileType;
                    int index = 0;
                    bool newInstance = false;
                    string fileNewName = string.Empty;
                    while(File.Exists(directory))
                    {
                        index++;
                        fileNewName = item.Name + "_" + index;
                        directory = newFilesDirectory + fileNewName + "." + item.File.FileType;
                        newInstance = true;
                    }

                    item.File.FileDirectory = directory;
                    if (newInstance == true)
                    {
                        item.File.FileName = fileNewName;
                        /*Guid FileGuid = Guid.NewGuid();
                        item.File.FileID = FileGuid.ToString();*/
                    }

                    File.Move(OldDirectory, item.File.FileDirectory);
                    File.SetLastWriteTime(item.File.FileDirectory, DateTime.Now);
                }
                else
                {
                    string OldDirectory = item.Directory;

                    item.Directory = newFilesDirectory + item.Name + "\\";
                    if (!Directory.Exists(item.Directory))
                    {
                        Directory.Move(OldDirectory, item.Directory);
                        Directory.SetLastWriteTime(item.Directory, DateTime.Now);
                    }
                    else
                    {
                        string[] Files = Directory.GetFiles(OldDirectory);
                        // TODO: use Get Directories and move the items while there are directories keep adding


                        foreach (string file in Files)
                        {
                            string fileName = Path.GetFileName(file);
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
                            string filetype = Path.GetExtension(file);

                            string targetFileDirectory = item.Directory + fileName;
                            int index = 0;

                            while (File.Exists(targetFileDirectory))
                            {
                                index++;
                                fileName = fileNameWithoutExtension + "_" + index;
                                targetFileDirectory = newFilesDirectory + item.Name + "\\" + fileName + filetype;
                            }

                            foreach (var savedFile in project.Files)
                            {
                                if (file.Equals(savedFile.FileDirectory))
                                {
                                    savedFile.FileName = fileName;
                                    savedFile.FileDirectory = targetFileDirectory;
                                }
                            }

                            File.Move(file, targetFileDirectory);
                            File.SetLastWriteTime(targetFileDirectory, DateTime.Now);
                        }
                    }

                    //Directory.Delete(OldDirectory);
                }
            }

            foreach (var SelectedItem in SelectedItems)
            {
                foreach (var Item in CurrentFiles.ToList())
                {
                    if (SelectedItem == Item)
                        CurrentFiles.Remove(Item);
                }
            }
        }

        public Node UpdateTreeAfterMovingItems(Node Root, List<ContentFileStructure> SelectedItems, ContentFileStructure TargetItem, string CurrentDirectory)
        {
            Queue<string> DirectoryQueue = new Queue<string>();

            string[] DirectorySplit = CurrentDirectory.Split('/');
            foreach (var Directory in DirectorySplit)
            {
                if (!string.IsNullOrEmpty(Directory))
                    DirectoryQueue.Enqueue(Directory);
            }

            Node CurrentNode = Root;

            while (DirectoryQueue.Count > 0)
            {
                string directory = DirectoryQueue.Dequeue();

                foreach (var subNode in CurrentNode.SubNodes)
                {
                    if (subNode.Name == directory && subNode.NodeType == DataType.Folder)
                    {
                        CurrentNode = subNode;
                        break;
                    }
                }
            }

            Node TargetNode = null;
            Stack<Node> StackNodes = new Stack<Node>();
            Stack<Node> StackNodesFolders = new Stack<Node>();

            foreach (var SelecteItem in SelectedItems)
            {
                foreach(var subNode in CurrentNode.SubNodes.ToList())
                {
                    string fileSubNode = Path.GetFileNameWithoutExtension(subNode.NodeDirectory);
                    if (SelecteItem.Name == subNode.Name && SelecteItem.Type == "Folder" && subNode.NodeType == DataType.Folder)
                    {
                        StackNodes.Push(subNode);
                        StackNodesFolders.Push(subNode);

                        CurrentNode.SubNodes.Remove(subNode);
                    }
                    else if (SelecteItem.Name == fileSubNode && SelecteItem.Type.Contains("MK") && subNode.NodeType == DataType.File)
                    {
                        subNode.Name = subNode.NodeFile.FileName + "." + subNode.NodeFile.FileType;
                        subNode.NodeDirectory = subNode.NodeFile.FileDirectory;

                        StackNodes.Push(subNode);
                        StackNodesFolders.Push(subNode);

                        CurrentNode.SubNodes.Remove(subNode);
                    }

                    if (TargetItem.Name == subNode.Name && TargetItem.Type == "Folder" && subNode.NodeType == DataType.Folder)
                    {
                        TargetNode = subNode;
                    }
                }
            }

            if (TargetNode != null)
            {
                while (StackNodes.Count > 0)
                {
                    var node = StackNodes.Pop();

                    if (!TargetNode.SubNodes.Contains(node))
                        TargetNode.SubNodes.Add(node);
                }

                TargetNode.SubNodes = new ObservableCollection<Node>(TargetNode.SubNodes.OrderBy(d => d.NodeType == DataType.File).ThenBy(n => n.Name, new SortComparer()));

                UpdateSubDirectoriesAfterDragMoveAction(StackNodesFolders, TargetNode);

                RemoveDuplicate(TargetNode);
            }

            return Root;
        }

        private void RemoveDuplicate(Node TargetNode)
        {
            Stack<Node> StackNodes = new Stack<Node>();

            Node Temp = TargetNode;

            foreach(var node in TargetNode.SubNodes)
            {
                foreach (var otherNode in Temp.SubNodes)
                {
                    if (node != otherNode && string.Equals(node.Name, otherNode.Name, StringComparison.OrdinalIgnoreCase) && node.NodeType == otherNode.NodeType)
                    {
                        StackNodes.Push(node);
                    }
                }
            }

            if (StackNodes.Count == 0)
                return;

            Node node1 = StackNodes.Pop();
            Node node2 = StackNodes.Pop();

            foreach (var node in node2.SubNodes)
            {
                node1.SubNodes.Add(node);
            }

            TargetNode.SubNodes.Remove(node2);

            foreach (var node in node1.SubNodes)
            {
                if (node.NodeType == DataType.File && node.Name != node.NodeFile.FileName)
                {
                    string[] textBlock = node.NodeFile.FileDirectory.Split(node.Name);
                    node.Name = node.NodeFile.FileName;
                    node.NodeDirectory = textBlock[0] + node.Name + "." + node.NodeFile.FileType;
                    node.NodeFile.FileDirectory = node.NodeDirectory;
                }
            }

            node1.SubNodes = new ObservableCollection<Node>(node1.SubNodes.OrderBy(d => d.NodeType == DataType.File).ThenBy(n => n.Name, new SortComparer()));

            
        }

        private void UpdateSubDirectoriesAfterDragMoveAction(Stack<Node> StackNode, Node TargetNode)
        {
            string[] TargetDirectoryBlocks = TargetNode.NodeDirectory.Split("\\");
            while (StackNode.Count > 0)
            {
                Node CurrentNode = StackNode.Pop();
                List<string> CurrentNodeDirectoryBlocks = new List<string>();
                string[] NodeDirectory = CurrentNode.NodeDirectory.Split("\\");
                
                foreach(var targetDirectory in TargetDirectoryBlocks)
                {
                    if(!string.IsNullOrEmpty(targetDirectory))
                        CurrentNodeDirectoryBlocks.Add(targetDirectory);
                }

                foreach(var currentDirectory in NodeDirectory)
                {
                    if (!CurrentNodeDirectoryBlocks.Contains(currentDirectory) && !string.IsNullOrEmpty(currentDirectory))
                        CurrentNodeDirectoryBlocks.Add(currentDirectory);
                }

                string NewDirectory = string.Empty;

                if (CurrentNode.NodeType == DataType.File)
                {
                    for (int i = 0; i < CurrentNodeDirectoryBlocks.Count; ++i)
                    {
                        if (i == CurrentNodeDirectoryBlocks.Count - 1)
                            NewDirectory += CurrentNodeDirectoryBlocks[i];
                        else
                            NewDirectory += CurrentNodeDirectoryBlocks[i] + "\\";
                    }

                    CurrentNode.NodeDirectory = NewDirectory;
                    CurrentNode.NodeFile.FileDirectory = NewDirectory;
                }
                else
                {
                    for (int i = 0; i < CurrentNodeDirectoryBlocks.Count; ++i)
                    {
                        NewDirectory += CurrentNodeDirectoryBlocks[i] + "\\";
                    }

                    CurrentNode.NodeDirectory = NewDirectory;
                }

                if (CurrentNode.SubNodes != null && CurrentNode.SubNodes.Count > 0)
                {
                    foreach (Node Node in CurrentNode.SubNodes)
                        StackNode.Push(Node);
                }
            }
        }
    }
}
