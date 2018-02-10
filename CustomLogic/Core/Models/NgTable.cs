using System.Collections.Generic;

namespace CustomLogic.Core.Models
{
    public class NgTable<T> : Response<T>
    {
        public NgTable()
        {
            Data = new List<T>();
        }

        public int Count { get; set; }

        public new List<T> Data { get; set; }
    }
}