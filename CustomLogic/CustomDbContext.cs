using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomLogic.Core.Interfaces;

namespace CustomLogic
{
    public partial class AomDbContext : IUnitOfWork
    {
        private static void AomDbContextStaticPartial()
        {

        }

        public IDbSet<T> With<T>() where T : class
        {
            return Set<T>();
        }
    }
}
