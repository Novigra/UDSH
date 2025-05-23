﻿// Copyright (C) 2025 Mohammed Kenawy
using UDSH.Model;

namespace UDSH.Services
{
    public class HeaderServices : IHeaderServices
    {
        public event EventHandler<FileSystem> FileStructureSelectionChanged;
        public IUserDataServices UserDataServices { get; }
        public IServiceProvider Services { get; }

        public HeaderServices(IUserDataServices userDataServices, IServiceProvider services)
        {
            UserDataServices = userDataServices;
            Services = services;
        }

        public async Task OnFileSelectionChanged(FileSystem file)
        {
            FileStructureSelectionChanged?.Invoke(this, file);
            UserDataServices.CurrentSelectedFile = file;

            await Task.CompletedTask;
        }
    }
}
