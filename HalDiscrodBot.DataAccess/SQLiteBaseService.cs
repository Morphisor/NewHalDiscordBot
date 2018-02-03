using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
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

            try
            {
                _connection.Open();
                command.Connection = _connection;
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                toReturn = false;
            }
            finally
            {
                _connection.Close();
            }

            return toReturn;
        }

        internal abstract void InitDb();
        internal abstract Dto MapEntityToDto(Entity model);
        internal abstract Entity MapDtoToEntity(Dto model);
    }
}
