// <copyright file="Project.cs" company="Lemon Design">
// Copyright (c) 2007-2008 Lemon Design. All rights reserved.
// </copyright>
// <author>Simon Dahlbacka</author>
// <email>simon.dahlbacka@gmail.com</email>
// <date>2007-02-13</date>
// <summary>Represents and parses MSBuild compatible project files</summary>

namespace DependencyVisualizer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Xml;
    using DependencyVisualizer.Properties;
    using QuickGraph.Concepts;

    /// <summary>
    /// A msbuild compatible project
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Backing field for the <see cref="Guid"/> property.
        /// </summary>
        private Guid guid;

        /// <summary>
        /// Collection containing all project references.
        /// </summary>
        private Collection<Project> projectReferences;

        /// <summary>
        /// Collection containing all non-project references.
        /// </summary>
        private Collection<string> references;

        /// <summary>
        /// The parent solution
        /// </summary>
        private Solution solution;

        /// <summary>
        /// The guids of all project referenced.
        /// </summary>
        private List<Guid> projectDependencyGuids;

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        /// <param name="solution">The parent solution.</param>
        public Project(Solution solution)
        {
            this.references = new Collection<string>();
            this.projectReferences = new Collection<Project>();
            this.solution = solution;
            this.projectDependencyGuids = new List<Guid>();
        }

        /// <summary>
        /// Gets or sets the vertex.
        /// </summary>
        /// <value>The vertex.</value>
        public IVertex Vertex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>        
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        public Guid Guid
        {
            get
            {
                return this.guid;
            }

            set
            {
                this.guid = value;
                ProjectRepository.Instance.Add(this);
            }
        }

        /// <summary>
        /// Gets or sets the type of the project.
        /// </summary>
        /// <value>The type of the project.</value>
        public Guid ProjectType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the project references.
        /// </summary>
        /// <value>The project references.</value>
        public Collection<Project> ProjectReferences
        {
            get { return this.projectReferences; }
        }

        /// <summary>
        /// Gets the references.
        /// </summary>
        public Collection<string> References
        {
            get { return this.references; }
        }

        /// <summary>
        /// Gets the project kind.
        /// </summary>
        /// <param name="projectIdentifier">The project type GUID.</param>
        /// <returns>The project kind represented by the type in <paramref name="projectIdentifier"/></returns>
        public static ProjectKind GetKind(Guid projectIdentifier)
        {
            foreach (ProjectKind kind in Enum.GetValues(typeof(ProjectKind)))
            {
                FieldInfo field = kind.GetType().GetField(kind.ToString());
                if (projectIdentifier == new Guid(((DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description))
                {
                    return kind;
                }
            }

            return ProjectKind.Unknown;
        }

        /// <summary>
        /// Parses the project file
        /// </summary>
        public void Parse()
        {
            if (System.IO.Path.GetExtension(this.Path).Equals(".vcproj", StringComparison.CurrentCultureIgnoreCase))
            {
                this.ParseVCProj();
                return;
            }

            Tracer.Debug("Parsing project {0} ...", this.Path);
            XmlDocument doc = new XmlDocument();
            doc.Load(this.Path);
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("msbuild", "http://schemas.microsoft.com/developer/msbuild/2003");

            foreach (XmlAttribute att in doc.SelectNodes("/msbuild:Project/msbuild:ItemGroup/msbuild:Reference/@Include", ns))
            {
                string name = att.Value.Split(',')[0];
                Tracer.Debug("Found file reference {0}", name);
                this.references.Add(name);
            }

            foreach (XmlElement el in doc.SelectNodes("/msbuild:Project/msbuild:ItemGroup/msbuild:ProjectReference", ns))
            {
                Project p = ProjectRepository.Instance.GetByGuid(new Guid(el.SelectSingleNode("msbuild:Project/text()", ns).Value));
                if (p != null)
                {
                    Tracer.Debug("Found already parsed project reference {0}", p.Name);
                    this.projectReferences.Add(p);
                }
                else
                {
                    string projectPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.Path), el.SelectSingleNode("@Include", ns).Value);
                    string urldecoded = DecodePath(projectPath);
                    if (!System.IO.File.Exists(projectPath) && System.IO.File.Exists(urldecoded))
                    {
                        // for some reason Visual Studio seem to urlencode some paths 
                        projectPath = urldecoded;
                    }

                    p = Project.FromFile(projectPath, this.solution);
                    Tracer.Debug("Found new project reference {0}", p.Name);
                    p.Parse();
                    this.projectReferences.Add(p);
                }
            }

            // ProjectRepository.Instance.Add(this);
        }

        /// <summary>
        /// Adds a project dependency.
        /// </summary>
        /// <param name="projectIdentifier">The GUID of the project depended on.</param>
        public void AddProjectDependency(string projectIdentifier)
        {
            this.projectDependencyGuids.Add(new Guid(projectIdentifier));
        }

        /// <summary>
        /// Create a project from file
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <param name="solution">The parent solution.</param>
        /// <returns>a partially parsed project</returns>
        internal static Project FromFile(string projectPath, Solution solution)
        {
            if (System.IO.Path.GetExtension(projectPath).Equals(".vcproj", StringComparison.CurrentCultureIgnoreCase))
            {
                return FromVCProjFile(projectPath, solution);
            }

            string urldecoded = DecodePath(projectPath);
            if (!System.IO.File.Exists(projectPath) && System.IO.File.Exists(urldecoded))
            {
                // for some reason Visual Studio seem to urlencode some paths 
                projectPath = urldecoded;
            }

            Tracer.Debug("Pre-parsing {0} ...", projectPath);
            Project project = new Project(solution);

            XmlDocument doc = new XmlDocument();
            doc.Load(projectPath);
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("msbuild", "http://schemas.microsoft.com/developer/msbuild/2003");

            project.Name = doc.SelectSingleNode("/msbuild:Project/msbuild:PropertyGroup/msbuild:AssemblyName/text()", ns).Value;
            Tracer.Debug("Project name: {0}", project.Name);
            project.ProjectType = Guid.Empty; // TODO: fix project type projectIdentifier somehow
            project.Guid = new Guid(doc.SelectSingleNode("/msbuild:Project/msbuild:PropertyGroup/msbuild:ProjectGuid/text()", ns).Value);
            Tracer.Debug("Project guid: {0}", project.Guid);
            /*Project parsed = ProjectRepository.Instance.GetByGuid(project.Guid);
            if (parsed != null)
            {
                return parsed;
            }*/
            project.Path = System.IO.Path.GetFullPath(projectPath);
            Tracer.Debug("Project path: {0}", project.Path);

            return project;
        }

        /// <summary>
        /// Create a project instance from a Visual C++ project file
        /// </summary>
        /// <param name="projectPath">The path to the project file</param>
        /// <param name="solution">The parent solution</param>
        /// <returns>A project instance representing the specified project</returns>
        internal static Project FromVCProjFile(string projectPath, Solution solution)
        {
            Tracer.Debug("Pre-parsing C++ {0} ...", projectPath);
            Project project = new Project(solution);
            XmlDocument doc = new XmlDocument();
            doc.Load(projectPath);

            project.Name = doc.SelectSingleNode("/VisualStudioProject/@Name").Value;
            Tracer.Debug("Project name: {0}", project.Name);
            project.ProjectType = Guid.Empty; // TODO: fix
            project.Guid = new Guid(doc.SelectSingleNode("/VisualStudioProject/@ProjectGUID").Value);
            Tracer.Debug("Project guid: {0}", project.Guid);
            project.Path = System.IO.Path.GetFullPath(projectPath);
            Tracer.Debug("Project path: {0}", project.Path);

            return project;
        }

        /// <summary>
        /// Parse a Visual C++ project file
        /// </summary>
        internal void ParseVCProj()
        {
            Tracer.Debug("Parsing VCProj ...");
            XmlDocument doc = new XmlDocument();
            doc.Load(this.Path);
            foreach (XmlAttribute att in doc.SelectNodes("/VisualStudioProject/References/AssemblyReference/@AssemblyName"))
            {
                string name = att.Value.Split(',')[0];
                Tracer.Debug("Found file reference {0}", name);
                this.references.Add(name);
            }

            foreach (XmlElement el in doc.SelectNodes("/VisualStudioProject/References/ProjectReference"))
            {
                Project p = ProjectRepository.Instance.GetByGuid(new Guid(el.SelectSingleNode("@ReferencedProjectIdentifier").Value));
                if (p != null)
                {
                    Tracer.Debug("Found already parsed project reference {0}", p.Name);
                    this.projectReferences.Add(p);
                }
                else
                {
                    p = Project.FromFile(
                        System.IO.Path.Combine(
                        System.IO.Path.GetDirectoryName(this.solution.FileName), 
                        el.SelectSingleNode("@RelativePathToProject").Value), 
                        this.solution);
                    Tracer.Debug("Found new project reference {0}", p.Name);
                    p.Parse();
                    this.projectReferences.Add(p);
                }
            }

            /* add the pseudo references for project dependencies */
            foreach (Guid g in this.projectDependencyGuids)
            {
                Project p = ProjectRepository.Instance.GetByGuid(g);
                if (p != null)
                {
                    this.projectReferences.Add(p);
                }
                else
                {
                    throw new DependencyException(Resources.UnkownProjectDependency);
                }
            }
        }

        /// <summary>
        /// Decode the funky almost url-encoded path that Visual Studio sometimes use.
        /// </summary>
        /// <param name="path">The possibly encoded path</param>
        /// <returns>The decoded path</returns>
        private static string DecodePath(string path)
        {
            string decoded = Regex.Replace(
                path,
                @"%([A-Fa-f\d]{2})",
                match =>
                {
                    return ((char)Convert.ToInt32(match.Groups[1].Value, 16)).ToString();
                });

            return decoded;
        }
    }
}
