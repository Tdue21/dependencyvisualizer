//----------------------------------------------------------------
// DependencyVisualizer
//----------------------------------------------------------------
//
// Solution class
//
// Represents and parses Visual Studio 2005 and Visual Studio 2008 solution files
//
// Copyright © 2007-2008 Simon Dahlbacka
//
// Created: 13.2 2007 Simon Dahlbacka
// $Id: $
//----------------------------------------------------------------
// $NoKeywords: $

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using DependencyVisualizer.Properties;


namespace DependencyVisualizer {
    /// <summary>
    /// A visual studio solution file
    /// </summary>
    public class Solution {
        private Dictionary<Guid, Project> m_projects;
        private string m_fileName;

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName {
            get { return m_fileName; }
            set { m_fileName = value; }
        }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>The projects.</value>
        public Dictionary<Guid, Project> Projects {
            get { return m_projects; }
        }

        private Solution() {
            m_projects = new Dictionary<Guid, Project>();
        }

        /// <summary>
        /// Creates a new empty solution
        /// </summary>
        /// <returns></returns>
        public static Solution EmptySolution() {
            return new Solution();
        }

        /// <summary>
        /// Parse a solution file
        /// </summary>
        /// <param name="filename">The solution file name.</param>
        /// <returns></returns>
        public static Solution FromFile(string filename) {
            if (filename == null) {
                throw new ArgumentNullException("filename");
            }
            Tracer.Debug("Loading solution {0} ...", filename);
            Solution solution = new Solution();
            solution.FileName = filename;
            string contents = File.ReadAllText(filename);
            if (!contents.Contains("Microsoft Visual Studio Solution File, Format Version 9") &&
                !contents.Contains("Microsoft Visual Studio Solution File, Format Version 10")) {
                throw new UnsupportedFileFormatException(string.Format(CultureInfo.CurrentCulture, Resources.BadSolutionFormat, filename));
            }
            Regex re = new Regex(@"Project\(""(?<ProjectType>{[\w-]+})""\)\s+=\s+""(?<Name>[-_ \(\)\w\.]+?)"",\s+""(?<Path>[-_ \(\)\w\.\\:]+?)"",\s+""(?<ProjectGuid>{[\w-]+})"".*?EndProject\b", RegexOptions.Singleline);
            MatchCollection coll = re.Matches(contents);
            foreach (Match m in coll) {
                if (m.Success) {                    
                    Project project = new Project(solution);
                    project.Name = m.Groups["Name"].Value;
                    Tracer.Debug("Found project named {0} in solution file ...", project.Name);
                    project.ProjectType = new Guid(m.Groups["ProjectType"].Value);
                    project.Guid = new Guid(m.Groups["ProjectGuid"].Value);
                    project.Path = Path.Combine(Path.GetDirectoryName(filename), m.Groups["Path"].Value);
                    /* pseudo vc++ references, project dependencies */
                    Regex projectDependenciesRegex = new Regex(@"ProjectSection\(ProjectDependencies\)\s=\spostProject(?<Data>.*?)EndProjectSection", RegexOptions.Singleline);
                    Match projectDependencyMatch = projectDependenciesRegex.Match(contents, m.Index, m.Length);
                    if (projectDependencyMatch.Success) {
                        string data = projectDependencyMatch.Groups["Data"].Value;
                        string[] lines = data.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string line in lines) {
                            string[] keyVal = line.Split('=');
                            if (keyVal.Length == 2) {
                                // found a project dependency ... store it for now
                                project.AddProjectDependency(keyVal[0].Trim());
                            }
                        }
                    }
                    if (!File.Exists(project.Path)) {
                        Tracer.Debug("Skipping non-existent project {0}, solution folder ?", project.Name);
                        continue;
                    }                    
                    ProjectKind kind = Project.GetKind(project.ProjectType);
                    if (kind == ProjectKind.Unknown ||
                        kind == ProjectKind.SolutionItems ||
                        kind == ProjectKind.Setup) {
                        Tracer.Debug("Skipping project {0} of type {1}, which is currently unsupported", project.Name, kind);
                        continue;
                    }
                    solution.Projects[project.Guid] = project;
                }
            }

            // first chew the entire solution before parsing ...
            foreach (Project p in solution.Projects.Values) {
                p.Parse();
            }
            return solution;
        }
    }

}
