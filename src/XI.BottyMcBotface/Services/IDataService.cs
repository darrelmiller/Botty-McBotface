using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XI.BottyMcBotface.Services
{
    public interface IDataService
    {

        Task<bool> CreateTeam(string displayName, string owner, string members, bool isPrivate);

    }
}
