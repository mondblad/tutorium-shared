using Tutorium.Shared.Utils.EntityFramework.Attributes;
using Microsoft.EntityFrameworkCore;

namespace Tutorium.Shared.Utils.EntityFramework.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplySeparateTableAttribute(ModelBuilder modelBuilder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var baseTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && t.IsAbstract &&
                            t.GetCustomAttributes(typeof(SeparateTableAttribute), false).Any()).ToList();

            foreach (var baseType in baseTypes)
            {
                modelBuilder.Entity(baseType).UseTptMappingStrategy();

                var derivedTypes = assemblies
                    .SelectMany(a => a.GetTypes())
                    .Where(t => t.IsClass && !t.IsAbstract &&
                                baseType.IsAssignableFrom(t));

                foreach (var derivedType in derivedTypes)
                    modelBuilder.Entity(derivedType).ToTable(derivedType.Name);
            }
        }
    }
}
