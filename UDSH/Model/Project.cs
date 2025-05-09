﻿// Copyright (C) 2025 Mohammed Kenawy
namespace UDSH.Model
{
    public class Project
    {
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectAuthor { get; set; }
        public string ProjectVersion { get; set; }
        public DateTime ProjectCreationDate { get; set; }
        public DateTime ProjectLastModificationDate { get; set; }
        public bool IsLastOpenedProject { get; set; }
        public bool IsProjectProtected { get; set; }
        public string ProjectPassword { get; set; }
        public string ProjectDirectory { get; set; }
        public bool IsConnectedToGameProject { get; set; }
        public string GameProjectDirectory { get; set; }
        public List<FileSystem> Files { get; set; }
    }
}
