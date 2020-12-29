using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace TaskManager.Domain.Concrete
{
    class EFDbContextFactory : IDbContextFactory<EFDbContext>
    {
        public EFDbContext Create()
        {
            return new EFDbContext("EFDbContext");
        }
    }
}
