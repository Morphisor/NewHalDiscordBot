using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Linq;
using HalDiscrodBot.Utils;
using HalDiscrodBot.Utils.Attributes;
using System.Reflection;

namespace HalDiscrodBot.DataAccess
{
    internal static class SQLiteUtils
    {
        internal static SQLiteCommand InsertCommand<T>(string tableName, object model)
        {
            var toReturn = new SQLiteCommand();
            var command = new StringBuilder($"INSERT INTO {tableName} (");
            var properties = typeof(T).GetProperties();
            var filteredProperties = new List<PropertyInfo>();

            foreach (var prop in properties)
            {
                if (prop.GetCustomAttribute<SQLitePrimaryKey>() == null)
                    filteredProperties.Add(prop);
            }

            var modelType = model.GetType();
            var columnNames = new StringBuilder();
            var columnValues = new StringBuilder("VALUES (");
            foreach (var prop in filteredProperties)
            {
                columnNames.Append(prop.Name + ",");
                columnValues.Append("$" + prop.Name + ",");

                var propInfo = modelType.GetProperty(prop.Name);
                var value = propInfo.GetValue(model);
                toReturn.Parameters.AddWithValue("$" + prop.Name, value);
            }

            columnNames.RemoveLastChar();
            columnValues.RemoveLastChar();
            columnNames.Append(")");
            columnValues.Append(");");
            command.Append(columnNames.ToString());
            command.Append(columnValues.ToString());
            toReturn.CommandText = command.ToString();
            return toReturn;
        }

        internal static SQLiteCommand CreateTableCommant<T>(string tableName)
        {
            var toReturn = new SQLiteCommand();
            var command = new StringBuilder($"CREATE TABLE IF NOT EXISTS {tableName}(");
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                command.Append(prop.Name + " ");
                command.Append(prop.GetSQLiteType() + ",");
            }

            command.RemoveLastChar();
            command.Append(");");
            toReturn.CommandText = command.ToString();

            return toReturn;
        }

        internal static List<T> ReadEntity<T>(SQLiteDataReader reader)
        {
            var properties = typeof(T).GetProperties();
            var toReturn = new List<T>();

            if (!reader.HasRows) return toReturn;

            while (reader.Read())
            {
                var instance = Activator.CreateInstance<T>();
                foreach (var prop in properties)
                {
                    var value = reader.GetValue(reader.GetOrdinal(prop.Name));
                    prop.SetValue(instance, Convert.ChangeType(value, prop.PropertyType));
                }
                toReturn.Add(instance);
            }

            return toReturn;
        }
    }
}
