using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace Common.Utilities
{
    public static class ReflectionUtilities
    {
        public static List<Assembly> GetAssemblies(string assmblyNames)
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                if (IsCandidateCompilationLibrary(library, assmblyNames.Split(',')))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }

            return assemblies;
        }

        private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary, string[] assmblyName)
            => assmblyName.Any(d => compilationLibrary.Name.Contains(d))
               || compilationLibrary.Dependencies.Any(d => assmblyName.Any(c => d.Name.Contains(c)));
    }
}
