#region Copyright (c) 2022 Spencer Hoffa
// \file BootStrapper.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using Autofac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace XneloUtils.Desktop.Bootstrap.Autofac.Net
{
	public static class BootStrapper
	{
		private static IContainer s_Container;

		private static List<Assembly> GetAssemblies(BootStrapperConfig config)
		{
			var returnAssemblies = new List<Assembly>();
			var loadedAssemblies = new HashSet<string>();
			

			foreach(string assemblyToLoad in config.AssembliesToLoad)
			{
				if (!loadedAssemblies.Contains(assemblyToLoad))
				{
					try
					{
						Assembly assembly = Assembly.Load(assemblyToLoad);
						
						loadedAssemblies.Add(assemblyToLoad);
						returnAssemblies.Add(assembly);
					}
					catch (FileNotFoundException e)
					{
						System.Console.WriteLine("Error Loading assembly: " + assemblyToLoad);
					}
				}
			}
			return returnAssemblies;
		}

		private static void BuildContainer(BootStrapperConfig config)
        {
			ContainerBuilder builder = new();

			var assemblies = GetAssemblies(config);
			foreach (var assembly in assemblies)
			{
				var assemblyTypes = assembly.GetTypes();
				foreach (Type type in assemblyTypes)
				{
					if (type.GetCustomAttributes(typeof(AutofacRegistryAttribute), true).Length > 0)
					{
						var instance = Activator.CreateInstance(type) as IAutofacRegistry;
						if (instance != null)
						{
							instance.Register(builder);
						}
					}
				}
			}

			s_Container = builder.Build();
		}

		public static void Init(BootStrapperConfig config)
		{
			// FIRST BUILD CONTAINER
			BuildContainer(config);
		}

		public static T GetTypeOf<T>()
		{
			return s_Container.Resolve<T>();
		}

		public static IEnumerable<T> GetTypesOf<T>()
        {
			return s_Container.Resolve<IEnumerable<T>>();
        }
	}
}
