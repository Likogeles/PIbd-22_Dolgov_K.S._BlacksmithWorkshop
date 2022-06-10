using AbstractForgeContracts.StoragesContracts;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AbstractForgeDatabaseImplement.Implements
{
    public class BackUpInfo : IBackUpInfo
    {
        public Assembly GetAssembly() => typeof(BackUpInfo).Assembly;
        public List<PropertyInfo> GetFullList()
        {
            using var context = new AbstractForgeDatabase();
            var type = context.GetType();
            return type.GetProperties().Where(x =>
            x.PropertyType.FullName.StartsWith("Microsoft.EntityFrameworkCore.DbSet")).ToList();
        }
        public List<T> GetList<T>() where T : class, new()
        {
            using var context = new AbstractForgeDatabase();
            return context.Set<T>().ToList();
        }
    }
}
