// <copyright file="FileRepository.cs" company="Lemon Design">
// Copyright (c) 2007-2008 Lemon Design. All rights reserved.
// </copyright>
// <author>Simon Dahlbacka</author>
// <email>simon.dahlbacka@gmail.com</email>
// <date>2007-02-13</date>
// <summary>File repository - keeps track of file references</summary>

namespace DependencyVisualizer
{
    using System.Collections.Generic;
    using System.IO;
    using QuickGraph.Concepts;
    
    /// <summary>
    /// Repository of used file vertices
    /// </summary>
    public class FileRepository
    {
        /// <summary>
        /// The instance
        /// </summary>
        private static readonly FileRepository instance = new FileRepository();

        /// <summary>
        /// Storage for the vertices
        /// </summary>
        private Dictionary<string, IVertex> fileStore;        

        /// <summary>
        /// Initializes a new instance of the <see cref="FileRepository"/> class.
        /// </summary>
        private FileRepository()
        {
            this.fileStore = new Dictionary<string, IVertex>();
        }

        /// <summary>
        /// Gets the repository instance.
        /// </summary>
        /// <value>The instance.</value>
        public static FileRepository Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Determines whether the specified name is a .NET 2.0 assembly.
        /// </summary>
        /// <param name="name">The assembly name.</param>
        /// <returns>
        /// 	<c>true</c> if the specified name is a .NET 2.0 assembly; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNet20(string name)
        {
            if (File.Exists(Path.Combine(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727", name + ".dll")))
            {
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
        public static bool IsNet30(string name)
        {
            if (File.Exists(Path.Combine(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0", name + ".dll")))
            {
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
        public static string MangleName(string name)
        {
            if (!Properties.Settings.Default.CombineFrameworkAssemblies)
            {
                return name;
            }

            if (File.Exists(Path.Combine(@"C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727", name + ".dll")))
            {
                return ".NET 2.0";
            }

            if (File.Exists(Path.Combine(@"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.0", name + ".dll")))
            {
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
        public void Add(string file, IVertex vertex)
        {
            this.fileStore[MangleName(file)] = vertex;
        }

        /// <summary>
        /// Gets the vertex of the file
        /// </summary>
        /// <param name="name">The name of the file.</param>
        /// <returns>the vertex or <c>null</c> if this file haven't been used yet</returns>
        public IVertex GetByName(string name)
        {
            if (this.fileStore.ContainsKey(MangleName(name)))
            {
                return this.fileStore[MangleName(name)];
            }

            return null;
        }
    }
}
