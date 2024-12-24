namespace UDSH.Model
{
    public class User
    {
        public string DisplayName { get; set; }
        public bool IsProfilePictureSet { get; set; }
        public bool IsCustomIconSet { get; set; }
        public List<Project> Projects { get; set; }
    }
}
