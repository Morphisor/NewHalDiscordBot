using HalDiscordBot.Models.Dtos;
using HalDiscordBot.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using HalDiscrodBot.Utils;

namespace HalDiscrodBot.DataAccess.Services
{
    public class ErrorLogService : SQLiteBaseService<ErrorLogDto, ErrorLog>
    {
        public ErrorLogService() : base()
        {
            tableName = "hal_error_logs";
        }

        internal override void InitDb()
        {
            _connection.Open();
            string[] command = File.ReadAllLines(_sqlScriptPath + "CreateTableErrorLogs.txt");
            using (SQLiteCommand SQLcommand = new SQLiteCommand(string.Join("", command), _connection))
            {
                SQLcommand.ExecuteNonQuery();
            }
            _connection.Close();
        }

        internal override ErrorLog MapDtoToEntity(ErrorLogDto model)
        {
            var toReturn = new ErrorLog()
            {
                CreateDate = model.CreateDate.GetUnixTime(),
                ExceptionMessage = model.ExceptionMessage,
                ExceptionStackTrace = model.ExceptionStackTrace,
                Message = model.Message
            };
            return toReturn;
        }

        internal override ErrorLogDto MapEntityToDto(ErrorLog model)
        {
            var toReturn = new ErrorLogDto()
            {
                CreateDate = model.CreateDate.GetDateTime(),
                ExceptionMessage = model.ExceptionMessage,
                ExceptionStackTrace = model.ExceptionStackTrace,
                Message = model.Message
            };
            return toReturn;
        }
    }
}
