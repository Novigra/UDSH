// Copyright (C) 2025 Mohammed Kenawy
namespace UDSH.Model
{
    public class DirectoriesEventArgs : EventArgs
    {
        public string directory { get; }
        public string[] directories { get; }
        public string type { get; }

        public DirectoriesEventArgs(string directory, string[] directories, string type)
        {
            this.directory = directory;
            this.directories = directories;
            this.type = type;
        }
    }
}
