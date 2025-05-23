﻿// Copyright (C) 2025 Mohammed Kenawy
namespace UDSH.Model
{
    public enum ContentAdd
    {
        Folder,
        MKB,
        MKC,
        MKM
    }
    public enum ContentSort
    {
        FilesFirst_Ascending,
        FilesFirst_Descending,

        FoldersFirst_Ascending,
        FoldersFirst_Descending
    }

    public enum ContentFilter
    {
        None,
        MKB,
        MKC,
        MKM
    }

    public enum ContentView
    {
        LargeIcons,
        Details
    }
}
