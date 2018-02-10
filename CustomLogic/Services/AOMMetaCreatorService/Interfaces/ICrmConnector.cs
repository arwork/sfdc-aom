using System.Collections.Generic;
using CustomLogic.Core.Models;
using CustomLogic.Services.AOMMetaCreatorService.Models;

namespace CustomLogic.Services.AOMMetaCreatorService.Interfaces
{
    public interface ICrmConnector
    {
        /// <summary>
        /// Returns a list of all tables with in the CRM Organisation
        /// </summary>
        /// <returns></returns>
        List<string> GatTablesList();
        /// <summary>
        /// Log in to the CRM stores session into concrete Connector
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Response<bool> LogIn(string userName, string password);

        /// <summary>
        /// Describes the tables from the sent in list into a generic form 
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        List<TableInfo> DescribeTables(string[] tables);
    }
}