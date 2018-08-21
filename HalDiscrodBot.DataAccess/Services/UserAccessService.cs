using HalDiscordBot.Models.Dtos;
using HalDiscordBot.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using HalDiscrodBot.Utils;

namespace HalDiscrodBot.DataAccess.Services
{
    public class UserAccessService : SQLiteBaseService<UserAccessDto, UserAccess>
    {
        public UserAccessService() : base("user_access")
        {

        }

        internal override UserAccess MapDtoToEntity(UserAccessDto model)
        {
            var toReturn = new UserAccess()
            {
                UserName = model.UserName,
                EnterDate = model.EnterDate.GetUnixTime(),
                ExitDate = model.ExitDate.GetUnixTime(),
                ServerName = model.ServerName
            };
            return toReturn;
        }

        internal override UserAccessDto MapEntityToDto(UserAccess model)
        {
            var toReturn = new UserAccessDto()
            {
                UserName = model.UserName,
                EnterDate = model.EnterDate.GetDateTime(),
                ExitDate = model.ExitDate.GetDateTime(),
                ServerName = model.ServerName
            };
            return toReturn;
        }
    }
}
