
using PluginSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace MainApp.Common
{
    internal class PluginLoader
    {
        //public static IEnumerable<IPlugin> LoadedPlugins { get; private set; }

        static IEnumerable<IPlugin> CreatePluginInstances(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetInterface("IPlugin") != null)
                {
                    IPlugin? result = Activator.CreateInstance(type) as IPlugin;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements IPlugin in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }

        static Assembly LoadPlugin(string pluginLocation)
        {

            Console.WriteLine($"Loading plugins from: {pluginLocation}");
            PluginLoadContext loadContext = new PluginLoadContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        public IEnumerable<IPlugin> Load()
        {
            string ROOT_FOLDER = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            string PLUGIN_FOLDER = Path.Combine(ROOT_FOLDER, "Plugins");

            if (!Directory.Exists(PLUGIN_FOLDER))
            {
                Directory.CreateDirectory(PLUGIN_FOLDER);
            }

            string[] plugin_folders = Directory.GetDirectories(PLUGIN_FOLDER);


            IEnumerable<IPlugin> Plugins = plugin_folders.SelectMany(pluginPath =>
            {
                var entery = new DirectoryInfo(pluginPath).Name + ".dll";
                string plugin_main = Path.Combine(pluginPath, entery);
                Assembly pluginAssembly = LoadPlugin(plugin_main);
                return CreatePluginInstances(pluginAssembly);
            }).ToList();

            return Plugins;


        }

        class PluginLoadContext : AssemblyLoadContext
        {
            private AssemblyDependencyResolver _resolver;

            public PluginLoadContext(string pluginPath)
            {
                _resolver = new AssemblyDependencyResolver(pluginPath);
            }

            protected override Assembly Load(AssemblyName assemblyName)
            {
                string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
                if (assemblyPath != null)
                {
                    return LoadFromAssemblyPath(assemblyPath);
                }

                return null;
            }

            protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
            {
                string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
                if (libraryPath != null)
                {
                    return LoadUnmanagedDllFromPath(libraryPath);
                }

                return IntPtr.Zero;
            }
        }
    }
}
