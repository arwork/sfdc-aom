using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CustomLogic.Core.Models;
using CustomLogic.sforce;
using CustomLogic.Services.AOMMetaCreatorService.Interfaces;
using CustomLogic.Services.AOMMetaCreatorService.Models;

namespace CustomLogic.Services.AOMMetaCreatorService.Connectors
{
    public class SalesforceConnector : IDisposable, ICrmConnector
    {

        private SforceService binding;
        public List<string> sobjectsNames;

        public Response<bool> LogIn(string username, string password)
        {
            var result =  new Response<bool>();
            // try catch add error please
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            binding = new SforceService();
            binding.Timeout = 60000;
            LoginResult lr = binding.login(username, password);
            String authEndPoint = binding.Url;
            binding.Url = lr.serverUrl;
            binding.SessionHeaderValue = new SessionHeader();
            binding.SessionHeaderValue.sessionId = lr.sessionId;
            result.Data = true;
            return result;
        }

        /// <summary>
        /// Returns a list of tables from salesforce 
        /// </summary>
        /// <returns></returns>
        public List<string> GatTablesList()
        {
            sobjectsNames = new List<string>();
            DescribeGlobalResult dgr = binding.describeGlobal();
            for (int i = 0; i < dgr.sobjects.Length; i++)
            {
                sobjectsNames.Add(dgr.sobjects[i].name);
            }

            return sobjectsNames;
        }

        /// <summary>
        /// Retruns table definitions from salesforce for sent in tables
        /// </summary>
        /// <param name="tableNames">A list of table names you would like information about</param>
        /// <returns></returns>
        public List<TableInfo> DescribeTables(string[] tableNames)
        {
            DescribeSObjectResult[] dsrArray = binding.describeSObjects(tableNames.Take(50).ToArray()); // get everything as this project grows we will need more from this
            var results = dsrArray.Select(c=> new TableInfo
            {
                name = c.name,
                label = c.label,
                fields = c.fields?.Select(x=> new FieldInformation
                {
                    label = x.label,
                    name = x.name,
                    type = x.type.ToString()
                }).ToList(),
                relationships = c.childRelationships?.Select(x=> new RelationshipInformation
                {
                    ChildTable = x.childSObject,
                    ChildField = x.field
                }).ToList()
            }).ToList();

            return results;
        }

        public void Dispose()
        {
            binding.logout();
        }
    }
}
