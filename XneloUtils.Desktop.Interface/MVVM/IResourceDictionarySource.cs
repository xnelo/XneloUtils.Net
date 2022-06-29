#region Copyright (c) 2022 Spencer Hoffa
// \file IResourceDictionarySource.cs
// \author Spencer Hoffa
// \copyright \link LICENSE.md MIT License\endlink 2022 Spencer Hoffa 
#endregion

using System.Windows;

namespace XneloUtils.Desktop.Interface.MVVM
{
	/// <summary>
	/// Allows injection of View Resource Dictionaries
	/// </summary>
	public interface IResourceDictionarySource
	{
		/// <summary>
		/// Get the resource dictionary that needs to be injected.
		/// </summary>
		ResourceDictionary ResourceDictionary { get; }
	}
}
