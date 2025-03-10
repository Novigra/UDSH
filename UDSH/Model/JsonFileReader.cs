// Copyright (C) 2025 Mohammed Kenawy
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace UDSH.Model
{
    enum FileType
    {
        ApplicationResource,
        UserData
    }
    class JsonFileReader
    {
        public string Read(string jsonFile, FileType type)
        {
            try
            {
                if(type == FileType.ApplicationResource)
                {
                    var uri = new Uri("pack://application:,,,/Resource/Data/" + jsonFile);
                    var resourceStream = Application.GetResourceStream(uri);
                    using StreamReader streamReader = new(resourceStream.Stream);

                    return streamReader.ReadToEnd();
                }
                else
                {
                    if(!File.Exists(jsonFile))
                    {
                        Debug.WriteLine("File Doesn't Exist. Creating A New File...");
                        return "";
                    }

                    using StreamReader streamReader = new(jsonFile);
                    return streamReader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"ERROR::MESSAGE::{ex}");
                return "";
            }
        }
    }
}
