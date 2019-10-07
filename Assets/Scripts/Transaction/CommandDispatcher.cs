using BoardGameModels;
using Commands;
using Core;
using Model;
using Providers.DataProviders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction
{
    class CommandDispatcher : Singleton<CommandDispatcher>
    {
        private SaveGameProvider saveGameProvider;
        private ProjectProvider projectProvider;
        private List<Memento> lastMementos;
        private string ProjectId => projectProvider.CurrentProject;


        public bool HasSavedGame(string id) => saveGameProvider.HasSavedGame(id);

        public SavedGame LoadSavedGame(string id)
        {
            SavedGame game = saveGameProvider.LoadSavedGame(id);
            CreateMemento(game.players.Values.ToList());
            return game;
        }

        internal BoardGame GetState(string id)
        {
            saveGameProvider?.SetProjectId(id);
            return projectProvider.LoadProject(id);
        }

        public BoardGame CreateFromState(string state)
        {
            return projectProvider.CreateFromState(state);
        }

        internal Dictionary<string, ProjectData> GetProjects()
        {
            return projectProvider.Projects;
        }

        internal BoardGame CreateNewGame(string projectName, string currencyName, int currencyAmount)
        {
            return projectProvider.CreateNew(projectName, currencyName, currencyAmount);
        }
        
        internal void Initialize(ProjectProvider projectProvider, SaveGameProvider saveGameProvider)
        {
            this.projectProvider = projectProvider;
            this.saveGameProvider = saveGameProvider;
            projectProvider.Initialize();
        }

        public List<T> GetLastCreatedMementos<T>() where T : Memento
        {
            if (lastMementos != null)
            {
                return lastMementos.OfType<T>().ToList();
            }
            return null;
        }

        Stopwatch sw = new Stopwatch();

        public bool Execute<T>(params object[] parameters) where T : Command
        {
            T command = CreateCommand<T>();
            if(command.Execute(parameters))
            {
                CreateMemento(command);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreateMemento(IEnumerable<Memento> mementos)
        {
            foreach (Memento mem in mementos)
            {
                saveGameProvider?.Store(mem);
                projectProvider.Store(mem);
            }
            projectProvider.Save();
            saveGameProvider?.Save();
            lastMementos = mementos.ToList();
        }

        private void CreateMemento(Command command)
        {
            CreateMemento(command.mementos.Values);
            command.mementos.Clear();
        }

        private T CreateCommand<T>() where T : Command
        {
            T command = Activator.CreateInstance<T>();
            return command;
        }
    }
}
