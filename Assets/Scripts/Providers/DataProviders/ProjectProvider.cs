using BoardGameModels;
using Core;
using Database;
using Extensions;
using JsonDemo;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction;
using UnityEngine;
using View;

namespace Providers.DataProviders
{
    class ProjectData
    {
        public string Name { get; set; }
        public bool Finished { get; set; }
    }

    class ProjectProvider : StorageProvider
    {
        public Dictionary<string, ProjectData> Projects { get; private set; }
        public List<string> ProjectsNames { get; private set; }

        protected override string PATH
        {
            get
            {
                return "CreatedGames";
            }
        }

        protected override string FILENAME => "rules.txt";

        private BoardGame currentGame;

        public ProjectProvider()
        {
            CreateIfNotExists(CreatedGamesPath);
            Projects = new Dictionary<string, ProjectData>();
            ProjectsNames = new List<string>();            
        }
        
        internal void Initialize()
        {
            Projects = ReadProjectNames();
            ProjectsNames = Projects.Values.Select(x => x.Name).ToList();
        }

        private Dictionary<string, ProjectData> ReadProjectNames()
        {
            if (File.Exists(PROJECTSNAMEPATH))
            {
                string json = File.ReadAllText(PROJECTSNAMEPATH);
                return JsonConvert.DeserializeObject<Dictionary<string, ProjectData>>(json);
            }
            else
            {
                return new Dictionary<string, ProjectData>();
            }
        }

        /// <summary>
        /// ProjectName, currencyName, currencyAmount
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public BoardGame CreateNew(params object[] parameters)
        {
            string projectName = string.IsNullOrEmpty(parameters[0] as string) ? "Game #" + ProjectsNames.Count: parameters[0] as string;
            CurrentProject = Guid.NewGuid().ToString();
            currentGame = new BoardGame()
            {
                currency = parameters[1] as string,
                startingCurrency = (int)parameters[2],
                edges = new List<DbEdge>(),
                fields = new List<DbField>(),
                //hardcoded
                winningEvent = new CurrencyWinningEvent() { amount = 15000 },
                items = new List<DbItem>()
            };
            ProjectsNames.Add(CurrentProject);
            UserPreferences.LastID = CurrentProject;
            SaveProjectName(projectName, false);
            Save();
            return currentGame;
        }

        private void SaveProjectName(string projectName, bool finished)
        {
            Projects[CurrentProject] = new ProjectData() { Name = projectName, Finished = finished };
            File.WriteAllText(PROJECTSNAMEPATH, JsonConvert.SerializeObject(Projects));
        }

        public void Store(Memento mem)
        {
            if(mem is DbField)
            {
                if (mem.Deleted)
                {
                    currentGame.fields.Remove(mem as DbField);
                }
                else
                {
                    currentGame.fields.ReplaceOrAdd(mem as DbField);
                }
            }
            else if(mem is DbItem)
            {
                if (mem.Deleted)
                {
                    currentGame.items.Remove(mem as DbItem);
                }
                else
                {
                    currentGame.items.ReplaceOrAdd(mem as DbItem);
                }
            }
            else if(mem is DbEdge)
            {
                if (mem.Deleted)
                {
                    currentGame.edges.Remove(mem as DbEdge);
                }
                else
                {
                    currentGame.edges.ReplaceOrAdd(mem as DbEdge);
                }
            }
        }

        public void Save()
        {
            CheckProjectFinished();
            string json = JsonConvert.SerializeObject(currentGame);
            SaveToDisk(json);
        }

        private void CheckProjectFinished()
        {
            bool finished = currentGame.IsFinished();
            if(Projects[CurrentProject].Finished != finished)
            {
                SaveProjectName(Projects[CurrentProject].Name, finished);
            }
        }

       
        public BoardGame CreateFromState(string state)
        {
            BoardGame bg = JsonConvert.DeserializeObject<BoardGame>(state);
            return bg;
        }

        internal BoardGame LoadProject(string id)
        {
            CurrentProject = id;
            if (!string.IsNullOrEmpty(CurrentFolder))
            {
                string json = File.ReadAllText(CurrentPath);
                currentGame = JsonConvert.DeserializeObject<BoardGame>(json, new JsonWinningEventConverter());
            }
            else
            {
                currentGame = new BoardGame();
            }
            return currentGame;
        }
    }
}
