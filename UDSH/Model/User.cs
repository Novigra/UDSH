// Copyright (C) 2025 Mohammed Kenawy
namespace UDSH.Model
{
    public class User
    {
        public string UserID { get; set; }
        public string DisplayName { get; set; }
        public bool IsProfilePictureSet { get; set; }
        public bool IsCustomIconSet { get; set; }
        public List<Project> Projects { get; set; }
    }
}
