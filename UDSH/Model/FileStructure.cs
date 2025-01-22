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
        public ObservableCollection<ContentFileStructure> CreateCurrentLevelList(Project project, ContentSort sort)
        {
            ObservableCollection<ContentFileStructure> contentFileStructures = new ObservableCollection<ContentFileStructure>();
            string[] Directories = Directory.GetDirectories(project.ProjectDirectory);
            string[] Files = Directory.GetFiles(project.ProjectDirectory);

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
                    File = null
                });
            }

            foreach (var file in Files)
            {
                foreach (var savedFile in project.Files)
                {
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
                            File = savedFile
                        });
                        break;
                    }
                }
            }

            ObservableCollection<ContentFileStructure> SortedStructure = new ObservableCollection<ContentFileStructure>();
            switch (sort)
            {
                case ContentSort.FilesFirst_Ascending:
                    break;
                case ContentSort.FilesFirst_Descending:
                    break;
                case ContentSort.FoldersFirst_Ascending:
                    SortedStructure = new ObservableCollection<ContentFileStructure>(contentFileStructures.OrderBy(item => item.Type.Contains("MK")).ThenBy(n => n.Name, StringComparer.Ordinal));
                    break;
                case ContentSort.FoldersFirst_Descending:
                    break;
                default:
                    break;
            }

            return SortedStructure;
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
                        CurrentFileNodeDirectory = string.Empty;
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
                        CurrentFileNodeDirectory = string.Empty;
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
                            CurrentFileNodeDirectory = string.Empty;
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
                            CurrentFileNodeDirectory = string.Empty;
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

                    var sortedNodes = CurrentNode.SubNodes.OrderBy(d => d.NodeType == DataType.File).ThenBy(n => n.Name, StringComparer.Ordinal).ToList();
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
            root.SubNodes = new ObservableCollection<Node>(root.SubNodes.OrderBy(d => d.NodeType == DataType.File).ThenBy(n => n.Name, StringComparer.Ordinal));

            foreach (var subNode in root.SubNodes)
            {
                SortNodes(subNode);
            }
        }

        public void UpdateTreeItemName(Node root, Project project, ContentFileStructure SelectedItem, string OldDirectory)
        {
            string[] TextSplit = OldDirectory.Split('\\');
            int index = Array.IndexOf(TextSplit, project.ProjectName);
            Node CurrentNode = root;
            Node ParentNode = root;

            for (int i = index + 1; i < TextSplit.Length; ++i)
            {
                /*Node? NextNode = CurrentNode.SubNodes.FirstOrDefault(node => node.Name.Contains(TextSplit[i]));
                if (NextNode == null)
                    break;*/
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

            
            CurrentNode.NodeDirectory = SelectedItem.Directory;
            if (SelectedItem.File != null)
            {
                CurrentNode.Name = SelectedItem.Name + "." + SelectedItem.File.FileType;
                CurrentNode.NodeFile = SelectedItem.File;
            }
            else
            {
                CurrentNode.Name = SelectedItem.Name;
                UpdateFileNodesDirectory(CurrentNode, SelectedItem);
            }

            ParentNode.SubNodes = new ObservableCollection<Node>(ParentNode.SubNodes.OrderBy(d => d.NodeType == DataType.File).ThenBy(n => n.Name, StringComparer.Ordinal));
        }

        private void UpdateFileNodesDirectory(Node CurrentRoot, ContentFileStructure SelectedItem)
        {
            Stack<Node> StackNode = new Stack<Node>();
            StackNode.Push(CurrentRoot);

            string DirectoryName = SelectedItem.Name;

            while (StackNode.Count > 0)
            {
                Node CurrentNode = StackNode.Pop();
                if (CurrentNode.NodeType == DataType.File)
                {
                    string[] DirectorySplit = CurrentNode.NodeFile.FileDirectory.Split('\\');
                    string OldDirectoryName = DirectorySplit[DirectorySplit.Length - 2];

                    string NewFileDirectory = CurrentNode.NodeFile.FileDirectory.Replace(OldDirectoryName, DirectoryName);

                    CurrentNode.NodeDirectory = NewFileDirectory;
                    CurrentNode.NodeFile.FileDirectory = NewFileDirectory;
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
                string NewFileDirectory = file.FileDirectory.Replace(file.FileName, ItemNewName);
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
                string NewFolderDirectory = ContentItem.Directory.Replace(ContentItem.Name, ItemNewName);
                Directory.Move(ContentItem.Directory, NewFolderDirectory);
                Directory.SetLastWriteTime(NewFolderDirectory, DateTime.Now);

                ContentItem.Name = ItemNewName;
                ContentItem.Directory = NewFolderDirectory;
                ContentItem.LastDateModification = DateTime.Now;
            }
        }
    }
}
