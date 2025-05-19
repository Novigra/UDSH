// Copyright (C) 2025 Mohammed Kenawy

namespace UDSH.Model
{
    public class MKBFileConnectionUpdateEventArgs : EventArgs
    {
        public FileSystem MKBFile { get; set; }
        public FileSystem MKCFile { get; set; }

        public MKBFileConnectionUpdateEventArgs(FileSystem mKBFile, FileSystem mKCFile)
        {
            MKBFile = mKBFile;
            MKCFile = mKCFile;
        }
    }
}
