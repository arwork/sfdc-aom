using System;
using System.Collections.Generic;
using System.Linq;
using CustomLogic.Core.Models;
using CustomLogic.Services.AOMMetaCreatorService.Interfaces;
using CustomLogic.Services.AOMMetaCreatorService.Models;

namespace CustomLogic.Services.AOMMetaCreatorService
{
    public class AOMMetaCreatorService
    {
        public ICrmConnector Connector { get; private set; }
        public List<FieldType> FieldTypes { get; set; }

        public AOMMetaCreatorService(ICrmConnector connector)
        {
            Connector = connector;
        }

        public NgTable<AomMeta> CreateTables(string userName, string password, string[] tables)
        {
            var result = new NgTable<AomMeta>();
            // 1. Log in to sales force 
            var loggedIn = Connector.LogIn(userName, password);
            if (!loggedIn.Data)
            {
                result.LogError("Could not log into sales force");
                return result;
            }

            // 2. check that the tables are correct
            var tableNames = Connector.GatTablesList();
            bool isSubset = !tables.Except(tableNames).Any();
            if (!isSubset)
            {
                result.LogError("Tables are not a subset of salesforce tables");
                return result;
            }

            // 2. get the table definitions

            //    var tableDefinitions = Connector.DescribeTables(tables);

            // lets gt all of them
            var listN = splitList(tableNames, 50);
            List<TableInfo> tableDefinitions = new List<TableInfo>();
            foreach (var list in listN)
            {
                tableDefinitions.AddRange(Connector.DescribeTables(list.ToArray()));
            }

         


            var context = new AomDbContext();

            foreach (var tableDefinition in tableDefinitions)
            {

                if (!tables.ToList().Contains(tableDefinition.name))
                {
                    continue;
                }
                // 3. create table for each table definition
                AomMeta newTable = new AomMeta
                {
                    Name = tableDefinition.name,
                    Display = tableDefinition.label
                };

                // 4. create table field for each definition
                foreach (var field in tableDefinition.fields)
                {

                    // 5. create a type for each field type 
                    var fieldType = context.FieldTypes.FirstOrDefault(c => c.Name == field.type);
                    if (fieldType==null)
                    {
                        fieldType = new FieldType
                        {
                            Name = field.type
                        };
                        context.FieldTypes.Add(fieldType);
                        context.SaveChanges();
                    }

                    AomFieldMeta newField = new AomFieldMeta
                    {
                        Name = field.name,
                        Display = field.label,
                        FieldType = fieldType
                        // FieldTypeId = new Guid("F96D8BB4-C3E4-437B-A8AE-9E6BCB9CFE52")

                    };
                    newTable.AomFieldMetas.Add(newField);
                }

                result.Data.Add(newTable);
                context.AomMetas.Add(newTable);
            }
            context.SaveChanges();
            result.Success = true;


            // 5 create ralationships
            foreach (var tableDefinition in tableDefinitions)
            {

                // primary key table - which is this table
                var primaryTable = context.AomMetas.SingleOrDefault(c => c.Name == tableDefinition.name);

                if(primaryTable==null)
                    continue;

                // primary key table field - which is this tables Id
                var primaryTableField =
                    context.AomFieldMetas.SingleOrDefault(c => c.Name == "Id" && c.AomMetaId == primaryTable.Id);
                if (primaryTableField == null)
                    continue;


                if(tableDefinition.relationships==null)
                    continue;

                foreach (var relationship in tableDefinition.relationships)
                {
                    // forigen key table - the ccurrent child table 
                    var forigenTable = context.AomMetas.SingleOrDefault(c => c.Name == relationship.ChildTable);
                    if (forigenTable == null)
                        continue;

                    // forigen key table field  
                    var forigenTableField =
                     context.AomFieldMetas.SingleOrDefault(c => c.Name == relationship.ChildField && c.AomMetaId == forigenTable.Id);
                    if (forigenTableField == null)
                        continue;


                    // yeah baby we have a valid relationship SWEET AS 
                    context.RelationshipMetas.Add(new RelationshipMeta
                    {
                        PkAomMetaId = primaryTable.Id,
                        FkAomMetaId = forigenTable.Id,
                        FkAomFieldMetaId = forigenTableField.Id,
                        PkAomFieldMetaId = primaryTableField.Id
                    });
                    context.SaveChanges();

                }
            }

            return result;
        }

        public static IEnumerable<List<T>> splitList<T>(List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }
    }
}
