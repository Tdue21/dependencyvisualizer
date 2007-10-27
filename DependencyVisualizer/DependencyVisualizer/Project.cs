//----------------------------------------------------------------
// DependencyVisualiser
//----------------------------------------------------------------
//
// Project class
//
// Represents and parses MSBuild compatible project files
//
// Copyright © 2007 Simon Dahlbacka
//
// Created: 13.2 2007 Simon Dahlbacka
// $Id: $
//----------------------------------------------------------------
// $NoKeywords: $

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Xml;

using QuickGraph.Concepts;
using System.Reflection;
using DependencyVisualizer.Properties;





namespace DependencyVisualizer {
    /// <summary>
    /// A msbuild compatible project
    /// </summary>
    public class Project {

        private string m_name;
        private string m_path;
        private Guid m_guid;
        private Guid m_projectType;
        private IVertex m_vertex;
        private Collection<Project> m_projectReferences;
        private Collection<string> m_references;
        private Solution m_solution;
        private List<Guid> m_projectDependencyGuids;

        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project(Solution solution) {
            m_references = new Collection<string>();
            m_projectReferences = new Collection<Project>();
            m_solution = solution;
            m_projectDependencyGuids = new List<Guid>();
        }

        /// <summary>
        /// Parses the project file
        /// </summary>
        public void Parse() {
            if (System.IO.Path.GetExtension(m_path).Equals(".vcproj", StringComparison.CurrentCultureIgnoreCase)) {
                ParseVCProj();
                return;
            }
            Tracer.Debug("Parsing project {0} ...", m_path);
            XmlDocument doc = new XmlDocument();
            doc.Load(m_path);
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("msbuild", "http://schemas.microsoft.com/developer/msbuild/2003");

            foreach (XmlAttribute att in doc.SelectNodes("/msbuild:Project/msbuild:ItemGroup/msbuild:Reference/@Include", ns)) {
                string name = att.Value.Split(',')[0];
                Tracer.Debug("Found file reference {0}", name);
                m_references.Add(name);
            }

            foreach (XmlElement el in doc.SelectNodes("/msbuild:Project/msbuild:ItemGroup/msbuild:ProjectReference", ns)) {
                Project p = ProjectRepository.Instance.GetByGuid(new Guid(el.SelectSingleNode("msbuild:Project/text()", ns).Value));
                if (p != null) {
                    Tracer.Debug("Found already parsed project reference {0}", p.Name);
                    m_projectReferences.Add(p);
                } else {
                    p = Project.FromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(m_path), el.SelectSingleNode("@Include", ns).Value), m_solution);
                    Tracer.Debug("Found new project reference {0}", p.Name);
                    p.Parse();
                    m_projectReferences.Add(p);
                }
            }
            //ProjectRepository.Instance.Add(this);
        }

        internal void ParseVCProj() {
            Tracer.Debug("Parsing VCProj ...");
            XmlDocument doc = new XmlDocument();
            doc.Load(m_path);
            foreach (XmlAttribute att in doc.SelectNodes("/VisualStudioProject/References/AssemblyReference/@AssemblyName")) {
                string name = att.Value.Split(',')[0];
                Tracer.Debug("Found file reference {0}", name);
                m_references.Add(name);
            }

            foreach (XmlElement el in doc.SelectNodes("/VisualStudioProject/References/ProjectReference")) {
                Project p = ProjectRepository.Instance.GetByGuid(new Guid(el.SelectSingleNode("@ReferencedProjectIdentifier").Value));
                if (p != null) {
                    Tracer.Debug("Found already parsed project reference {0}", p.Name);
                    m_projectReferences.Add(p);
                } else {
                    p = Project.FromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(m_solution.FileName), el.SelectSingleNode("@RelativePathToProject").Value), m_solution);
                    Tracer.Debug("Found new project reference {0}", p.Name);
                    p.Parse();
                    m_projectReferences.Add(p);
                }
            }
            /* add the pseudo references for project dependencies */
            foreach (Guid g in m_projectDependencyGuids) {
                Project p = ProjectRepository.Instance.GetByGuid(g);
                if (p != null) {
                    m_projectReferences.Add(p);
                } else {
                    throw new DependencyException(Resources.UnkownProjectDependency);
                }
            }
        }

        /// <summary>
        /// Adds a project dependency.
        /// </summary>
        /// <param name="guid">The GUID of the project depended on.</param>
        public void AddProjectDependency(string guid) {
            m_projectDependencyGuids.Add(new Guid(guid));
        }

        /// <summary>
        /// Gets or sets the vertex.
        /// </summary>
        /// <value>The vertex.</value>
        public IVertex Vertex {
            get { return m_vertex; }
            set { m_vertex = value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path {
            get { return m_path; }
            set { m_path = value; }
        }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        /// <value>The GUID.</value>
        public Guid Guid {
            get { return m_guid; }
            set {
                m_guid = value;
                ProjectRepository.Instance.Add(this);
            }
        }

        /// <summary>
        /// Gets or sets the type of the project.
        /// </summary>
        /// <value>The type of the project.</value>
        public Guid ProjectType {
            get { return m_projectType; }
            set { m_projectType = value; }
        }



        /// <summary>
        /// Gets the project references.
        /// </summary>
        /// <value>The project references.</value>
        public Collection<Project> ProjectReferences {
            get { return m_projectReferences; }
        }



        /// <summary>
        /// Gets or sets the references.
        /// </summary>
        /// <value>The references.</value>
        public Collection<string> References {
            get { return m_references; }
        }


        /// <summary>
        /// Create a project from file
        /// </summary>
        /// <param name="projectPath">The project path.</param>
        /// <param name="solution">The parent solution.</param>
        /// <returns>a partially parsed project</returns>
        internal static Project FromFile(string projectPath, Solution solution) {
            if (System.IO.Path.GetExtension(projectPath).Equals(".vcproj", StringComparison.CurrentCultureIgnoreCase)) {
                return FromVCProjFile(projectPath, solution);
            }
            Tracer.Debug("Pre-parsing {0} ...", projectPath);
            Project project = new Project(solution);

            XmlDocument doc = new XmlDocument();
            doc.Load(projectPath);
            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("msbuild", "http://schemas.microsoft.com/developer/msbuild/2003");

            project.Name = doc.SelectSingleNode("/msbuild:Project/msbuild:PropertyGroup/msbuild:AssemblyName/text()", ns).Value;
            Tracer.Debug("Project name: {0}", project.Name);
            project.ProjectType = Guid.Empty; // TODO: fix project type guid somehow
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

        internal static Project FromVCProjFile(string projectPath, Solution solution) {
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
        /// Gets the project kind.
        /// </summary>
        /// <param name="projectGuid">The project type GUID.</param>
        /// <returns></returns>
        public static ProjectKind GetKind(Guid projectGuid) {
            foreach (ProjectKind kind in Enum.GetValues(typeof(ProjectKind))) {
                FieldInfo field = kind.GetType().GetField(kind.ToString());
                if (projectGuid == new Guid(((DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description)) {
                    return kind;
                }
            }
            return ProjectKind.Unknown;
        }
    }



    /// <summary>
    /// Project kind
    /// </summary>
    public enum ProjectKind {
        /// <summary>
        /// Unknown project type
        /// </summary>
        [Description("{00000000-0000-0000-0000-000000000000}")]
        Unknown,
        /// <summary>
        /// C# project
        /// </summary>
        [Description("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}")]
        CSharp,
        /// <summary>
        /// VB.NET project
        /// </summary>
        [Description("{F184B08F-C81C-45F6-A57F-5ABD9991F28F}")]
        VBNet,
        /// <summary>
        /// Solution folder
        /// </summary>
        [Description("{2150E333-8FDC-42A3-9474-1A3956D46DE8}")]
        SolutionItems,
        /// <summary>
        /// Setup project
        /// </summary>
        [Description("{54435603-DBB4-11D2-8724-00A0C9A8B90C}")]
        Setup,
        /// <summary>
        /// C++ project
        /// </summary>
        [Description("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}")]
        Cpp,
        /// <summary>
        /// J# project
        /// </summary>
        [Description("{E6FDF86B-F3D1-11D4-8576-0002A516ECE8}")]
        JSharp,
        /// <summary>
        /// Web project 
        /// </summary>
        [Description("{E24C65DC-7377-472b-9ABA-BC803B73C61A}")]
        WebProject,
        /// <summary>
        /// WiX project
        /// </summary>
        [Description("{930C7802-8A8C-48F9-8165-68863BCCD9DD}")]
        Wix
    }
}
