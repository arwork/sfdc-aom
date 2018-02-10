using System.Collections.Generic;

namespace CustomLogic.Services.AOMMetaCreatorService.Models
{
    public class TableInfo
    {

        public TableInfo()
        {
            fields = new List<FieldInformation>();
            relationships = new List<RelationshipInformation>();
        }
        public string name;
        public string label;
        public List<FieldInformation> fields;
        public List<RelationshipInformation> relationships;
    }
}