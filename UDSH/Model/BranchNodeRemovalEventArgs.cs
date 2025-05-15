// Copyright (C) 2025 Mohammed Kenawy
using UDSH.View;

namespace UDSH.Model
{
    public class BranchNodeRemovalEventArgs : EventArgs
    {
        public List<System.Windows.Shapes.Path> Paths { get; set; }
        public ConnectionType NodeConnectionType { get; set; }

        public BranchNodeRemovalEventArgs(List<System.Windows.Shapes.Path> paths, ConnectionType nodeConnectionType)
        {
            Paths = paths;
            NodeConnectionType = nodeConnectionType;
        }
    }
}
