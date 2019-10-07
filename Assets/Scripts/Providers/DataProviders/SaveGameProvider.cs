using BoardGameModels;
using Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;

namespace Providers.DataProviders
{
    class SaveGameProvider : StorageProvider
    {
        private SavedGame current;
        List<Memento> currentMementos;

        protected override string PATH
        {
            get
            {
                return "SavedGames";
            }
        }
        
        protected override string FILENAME => "savedgame.txt";

        public SaveGameProvider()
        {
            currentMementos = new List<Memento>();
            current = new SavedGame()
            {
                players = new Dictionary<string, DbPlayer>()
            };
        }

        public bool HasSavedGame(string id)
        {
            string folder = Path.Combine(CreatedGamesPath, id);
            string path = Path.Combine(folder, FILENAME);
            return Directory.Exists(folder) && File.Exists(path);
        }

        public SavedGame LoadSavedGame(string id)
        {
            string folder = Path.Combine(CreatedGamesPath, id);
            if (HasSavedGame(id))
            {
                string path = Path.Combine(folder, FILENAME);
                string json = File.ReadAllText(path);
                current = JsonConvert.DeserializeObject<SavedGame>(json);
                return current;
            }
            else
            {
                return null;
            }
        }

        internal void Store(Memento mem)
        {
            if(mem is DbPlayer)
            {
                current.players[mem.Id] = mem as DbPlayer;
                currentMementos.Add(mem);
            }
        }

        internal void Save()
        {
            string json = JsonConvert.SerializeObject(currentMementos);
            Networking.NetworkHandler.Instance.SendMsg(json, Networking.GameCommand.GameState);
            currentMementos.Clear();
            SaveToDisk(JsonConvert.SerializeObject(current));
        }

        internal void SetProjectId(string projectId)
        {
            CurrentProject = projectId;
        }
    }
}
