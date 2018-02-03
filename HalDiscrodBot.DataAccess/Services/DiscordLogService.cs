using HalDiscordBot.Models.Dtos;
using HalDiscordBot.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using HalDiscrodBot.Utils;
using Discord;

namespace HalDiscrodBot.DataAccess.Services
{
    public class DiscordLogService : SQLiteBaseService<DiscordLogDto, DiscordLog>
    {

        public DiscordLogService() : base()
        {
            tableName = "hal_logs";
        }

        internal override void InitDb()
        {
            _connection.Open();
            string[] command = File.ReadAllLines(_sqlScriptPath + "CreateTableLogs.txt");
            using (SQLiteCommand SQLcommand = new SQLiteCommand(string.Join("", command), _connection))
            {
                SQLcommand.ExecuteNonQuery();
            }
            _connection.Close();
        }

        internal override DiscordLog MapDtoToEntity(DiscordLogDto model)
        {
            var toReturn = new DiscordLog()
            {
                CreateDate = model.CreateDate.GetUnixTime(),
                ExceptionMessage = model.ExceptionMessage,
                LogMessage = model.LogMessage,
                LogSeverity = model.LogSeverity.ToString(),
                Source = model.Source
            };
            return toReturn;
        }

        internal override DiscordLogDto MapEntityToDto(DiscordLog model)
        {
            var toReturn = new DiscordLogDto()
            {
                CreateDate = model.CreateDate.GetDateTime(),
                ExceptionMessage = model.ExceptionMessage,
                LogMessage = model.LogMessage,
                LogSeverity = (LogSeverity)Enum.Parse(typeof(LogSeverity), model.LogSeverity, true),
                Source = model.Source
            };
            return toReturn;
        }
    }
}
