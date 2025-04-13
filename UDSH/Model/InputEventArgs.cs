namespace UDSH.Model
{
    public class InputEventArgs : EventArgs
    {
        public string CurrentActiveWorkspaceID { get; }
        public System.Windows.Input.KeyEventArgs KeyEvent { get; }

        public InputEventArgs(string currentActiveWorkspaceID, System.Windows.Input.KeyEventArgs keyEvent)
        {
            CurrentActiveWorkspaceID = currentActiveWorkspaceID;
            KeyEvent = keyEvent;
        }
    }
}
