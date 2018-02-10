using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using HalDiscordBot.Models.Misc;
using HalDiscrodBot.Utils;

namespace HalDiscrodBot.DataAccess
{
    public abstract class SQLiteBaseService<Dto, Entity>
    {
        protected static string _databaseFolder;
        protected static string _databasePath;
        protected static string _connectionString;
        protected static string _sqlScriptPath;

        protected SQLiteConnection _connection;
        protected string tableName;

        public event OnPreSaveHandler OnPreSave;
        public event OnPostSaveHandler OnPostSave;
        public event OnErrorSaveHandler OnError;

        public delegate void OnPreSaveHandler(OnPreSaveArgs<Dto> e);
        public delegate void OnPostSaveHandler(OnPostSaveArgs<Dto> e);
        public delegate void OnErrorSaveHandler(OnErrorArgs<Dto> e);

        public SQLiteBaseService()
        {
            _databaseFolder = Environment.CurrentDirectory + "\\Database";
            _databasePath = Environment.CurrentDirectory + "\\Database\\HalDatabase.db";
            _connectionString = "DataSource=" + _databasePath + ";Version=3;";
            _sqlScriptPath = Environment.CurrentDirectory + "\\SQLScripts\\";

            if (!Directory.Exists(_databaseFolder))
            {
                Directory.CreateDirectory(_databaseFolder);
                SQLiteConnection.CreateFile(_databasePath);
            }
            else if (!File.Exists(_databasePath))
            {
                SQLiteConnection.CreateFile(_databasePath);
            }

            _connection = new SQLiteConnection(_connectionString);
            InitDb();
        }

        public bool Insert(Dto model)
        {
            bool toReturn = false;
            var entity = MapDtoToEntity(model);
            var command = SQLiteUtils.InsertCommand<Entity>(tableName, entity);
            OnPreSave?.Invoke(new OnPreSaveArgs<Dto>(model));

            try
            {
                _connection.Open();
                command.Connection = _connection;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                toReturn = false;
                OnError?.Invoke(new OnErrorArgs<Dto>(model, ex));
            }
            finally
            {
                _connection.Close();
                OnPostSave?.Invoke(new OnPostSaveArgs<Dto>(model, toReturn));
            }

            return toReturn;
        }

        internal abstract void InitDb();
        internal abstract Dto MapEntityToDto(Entity model);
        internal abstract Entity MapDtoToEntity(Dto model);
    }
}
