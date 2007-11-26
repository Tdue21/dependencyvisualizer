//----------------------------------------------------------------
// DependencyVisualizer
//----------------------------------------------------------------
//
// File repository - keeps track of file references
//
// Copyright © 2007 Simon Dahlbacka
//
// Created: 13.2 2007 Simon Dahlbacka
// $Id: $
//----------------------------------------------------------------
// $NoKeywords: $

using System;
using System.Collections.Generic;

using QuickGraph.Concepts;
using System.IO;

namespace DependencyVisualizer {
    /// <summary>
    /// Repository of used file vertices
    /// </summary>
    public class FileRepository {
        private Dictionary<string, IVertex> m_store;
        private static readonly FileRepository m_instance = new FileRepository();

        private FileRepository() {
            m_store = new Dictionary<string, IVertex>();
        }

        /// <summary>
        /// Gets the repository instance.
        /// </summary>
        /// <value>The instance.</value>
        public static FileRepository Instance {
            get { return m_instance; }
        }

        /// <summary>
        /// Determines whether the specified name is a .NET 2.0 assembly.
        /// </summary>
        /// <param name="name">The assembly name.</param>
        /// <returns>
        /// 	<c>true</c> if the specified name is a .NET 2.0 assembly; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNet20(string name) {
            if (File.Exists(Path.Combine(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727", name + ".dll"))) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified name is a .NET 3.0 assembly.
        /// </summary>
        /// <param name="name">The assembly name.</param>
        /// <returns>
        /// 	<c>true</c> if the specified name is a .NET 3.0 assembly; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNet30(string name) {
            if (File.Exists(Path.Combine(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0", name + ".dll"))) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified name is a .NET 3.5 assembly.
        /// </summary>
        /// <param name="name">The assembly name.</param>
        /// <returns>
        /// 	<c>true</c> if the specified name is a .NET 3.5 assembly; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNet35(string name)
        {
            if (File.Exists(Path.Combine(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5", name + ".dll")))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Mangles the file name.
        /// </summary>
        /// <param name="name">The original name.</param>
        /// <returns>the mangled name</returns>
        public static string MangleName(string name) {
            if (!Properties.Settings.Default.CombineFrameworkAssemblies)
            {
                return name;
            }
            if (File.Exists(Path.Combine(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727", name + ".dll"))) {
                return ".NET 2.0";
            }
            if (File.Exists(Path.Combine(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0", name + ".dll"))) {
                return ".NET 3.0";
            }
            if (File.Exists(Path.Combine(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5", name + ".dll")))
            {
                return ".NET 3.5";
            }
            return name;
        }

        /// <summary>
        /// Adds the specified file.
        /// </summary>
        /// <param name="file">The file to add.</param>
        /// <param name="vertex">The corresponding vertex.</param>
        public void Add(string file, IVertex vertex) {

            m_store[MangleName(file)] = vertex;
        }

        /// <summary>
        /// Gets the vertex of the file
        /// </summary>
        /// <param name="name">The name of the file.</param>
        /// <returns>the vertex or <c>null</c> if this file haven't been used yet</returns>
        public IVertex GetByName(string name) {
            if (m_store.ContainsKey(MangleName(name))) {
                return m_store[MangleName(name)];
            }
            return null;
        }
    }
}
