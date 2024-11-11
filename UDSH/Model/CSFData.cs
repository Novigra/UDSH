namespace UDSH.Model
{
    /*enum DataType : UInt16
    {
        SceneHeading,
        Action,
        Character,
        Dialogue,
        BranchNode
    }*/
    internal class CSFData
    {
        public string Content { get; set; }
        public string DataType { get; set; }
        public bool IsConnected { get; set; }
    }
}
