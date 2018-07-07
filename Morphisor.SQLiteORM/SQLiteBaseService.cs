using Morphisor.SQLiteORM.Models.Misc;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace Morphisor.SQLiteORM
{
    public abstract class SQLiteBaseService<Dto, Entity>
    {
        protected static string _databaseFolder;
        protected static string _databasePath;
        protected static string _connectionString;
        protected static string _sqlScriptPath;
        protected string tableName;

        public event OnPreSaveHandler OnPreSave;
        public event OnPostSaveHandler OnPostSave;
        public event OnErrorSaveHandler OnError;

        public delegate void OnPreSaveHandler(OnPreSaveArgs<Dto> e);
        public delegate void OnPostSaveHandler(OnPostSaveArgs<Dto> e);
        public delegate void OnErrorSaveHandler(OnErrorArgs<Dto> e);

        public SQLiteBaseService(string tBName)
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

            tableName = tBName;
            CreateTable();
        }

        public bool CreateTable()
        {
            bool toReturn = false;
            var command = SQLiteUtils.CreateTableCommant<Entity>(tableName);
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();

            toReturn = true;
            return toReturn;
        }

        public bool Insert(Dto model)
        {
            bool toReturn = false;
            var entity = MapDtoToEntity(model);
            var command = SQLiteUtils.InsertCommand<Entity>(tableName, entity);
            OnPreSave?.Invoke(new OnPreSaveArgs<Dto>(model));
            var connection = new SQLiteConnection(_connectionString);
            try
            {
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                toReturn = false;
                OnError?.Invoke(new OnErrorArgs<Dto>(model, ex, OnErrorType.Insert));
            }
            finally
            {
                connection.Close();
                OnPostSave?.Invoke(new OnPostSaveArgs<Dto>(model, toReturn));
            }

            return toReturn;
        }

        public IEnumerable<Dto> Get(Func<Dto, bool> lambda)
        {
            var command = new SQLiteCommand($"SELECT * FROM {tableName}");
            List<Dto> dtos = new List<Dto>();
            var connection = new SQLiteConnection(_connectionString);
            try
            {
                connection.Open();
                command.Connection = connection;
                var reader = command.ExecuteReader();
                List<Entity> entities = SQLiteUtils.ReadEntity<Entity>(reader);
                foreach (var e in entities)
                {
                    var dto = MapEntityToDto(e);
                    dtos.Add(dto);
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(new OnErrorArgs<Dto>(default(Dto), ex, OnErrorType.Get));
            }
            finally
            {
                connection.Close();
            }

            var result = new List<Dto>();
            foreach (var dto in dtos)
            {
                if (lambda(dto))
                    result.Add(dto);
            }
            
            return result;
        }

        internal abstract Dto MapEntityToDto(Entity model);
        internal abstract Entity MapDtoToEntity(Dto model);
    }
}
