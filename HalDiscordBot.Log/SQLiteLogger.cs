using Discord;
using HalDiscordBot.Models.Dtos;
using HalDiscordBot.Models.Misc;
using HalDiscrodBot.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalDiscordBot.Log
{
    public class SQLiteLogger
    {
        private static SQLiteLogger _instance;

        public static SQLiteLogger Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SQLiteLogger();

                return _instance;
            }
        }


        private DiscordLogService _discordLogService;
        private ErrorLogService _errorLogService;

        private SQLiteLogger() {
            _discordLogService = new DiscordLogService();
            _errorLogService = new ErrorLogService();
            _discordLogService.OnError += OnDiscordLogError;
        }

        public void LogDiscordError(string message, LogSeverity severity, string source, string exMessage)
        {
            var discordLogDto = new DiscordLogDto()
            {
                CreateDate = DateTime.Now,
                ExceptionMessage = exMessage,
                LogMessage = message,
                LogSeverity = severity,
                Source = source
            };
            _discordLogService.Insert(discordLogDto);
        }

        public void LogError(string message, string exMessage, string stackTrace)
        {
            var errorLog = new ErrorLogDto()
            {
                CreateDate = DateTime.Now,
                Message = message,
                ExceptionMessage = exMessage,
                ExceptionStackTrace = stackTrace
            };
            _errorLogService.Insert(errorLog);
        }

        private void OnDiscordLogError(OnErrorArgs<DiscordLogDto> e)
        {
            LogError("Error Logging discord error", e.Error.Message, e.Error.StackTrace);
        }
    }
}
