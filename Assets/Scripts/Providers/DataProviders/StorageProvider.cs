using System.IO;
using UnityEngine;

namespace Providers.DataProviders
{
    public abstract class StorageProvider
    {
        public string CurrentProject { get; protected set; }

        protected string CurrentFolder => Path.Combine(Application.persistentDataPath, PATH, CurrentProject);

        protected string CurrentPath => Path.Combine(CurrentFolder, FILENAME);

        protected abstract string FILENAME{ get; }

        protected abstract string PATH { get; }

        protected string PROJECTSNAMEPATH => Path.Combine(Application.persistentDataPath, PATH, "Projects.txt");
        protected string CreatedGamesPath => Path.Combine(Application.persistentDataPath, PATH);



        protected void CreateIfNotExists(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        protected void SaveToDisk(string json)
        {
            CreateIfNotExists(CurrentFolder);
            File.WriteAllText(CurrentPath, json);
        }

    }
}