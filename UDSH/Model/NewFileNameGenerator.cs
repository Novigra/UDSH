// Copyright (C) 2025 Mohammed Kenawy
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace UDSH.Model
{
    enum Language
    {
        English
    }

    // We only have music ref for now.
    enum MediaType
    {
        Music
    }

    public class DataStorage
    {
        public Dictionary<string, DataDetails> Languages { get; set; }
    }

    public class DataDetails
    {
        public List<string> Music { get; set; }
    }

    class NewFileNameGenerator
    {
        private string JsonFile;
        private DataStorage Data { get; set; }

        public NewFileNameGenerator()
        {
            try
            {
                var uri = new Uri("pack://application:,,,/Resource/Data/NewFileHighlightedTitle.json");
                var resourceStream = Application.GetResourceStream(uri);
                using StreamReader streamReader = new(resourceStream.Stream);

                JsonFile = streamReader.ReadToEnd();

                if (!string.IsNullOrEmpty(JsonFile))
                {
                    Data = JsonSerializer.Deserialize<DataStorage>(JsonFile);
                }
                else
                {
                    Debug.WriteLine("WARNING::JSON FILE IS NULL");
                }
            }
            catch
            {
                Debug.WriteLine("WARNING::CAN'T FIND THE FILE");
            }          
        }

        public string Generate(Language language, MediaType type)
        {
            if(Data != null && Data.Languages.ContainsKey(language.ToString()))
            {
                // TODO: Add randomness to media type when adding other media types.
                List<string> Content = Data.Languages[language.ToString()].Music;
                
                // Keep in mind that the content could be not null but empty
                if(Content != null && Content.Count > 0)
                {
                    Random random = new Random();
                    int choice = random.Next(0, Content.Count - 1);

                    return Content[choice];
                }
                else
                {
                    return "Enter Your File Name";
                }
            }
            else
            {
                return "Enter Your File Name";
            }
        }
    }
}
