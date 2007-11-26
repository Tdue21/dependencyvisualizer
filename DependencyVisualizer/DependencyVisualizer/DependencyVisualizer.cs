//----------------------------------------------------------------
// DependencyVisualizer
//----------------------------------------------------------------
//
// Actual graphing stuff
//
// Multiline_description
//
// Created: 13.2 2007 Simon Dahlbacka
// $Id: $
//----------------------------------------------------------------
// $NoKeywords: $

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using QuickGraph.Algorithms.Graphviz;
using QuickGraph.Collections;
using QuickGraph.Concepts;
using QuickGraph.Providers;
using QuickGraph.Representations;

using NGraphviz.Helpers;

using DependencyVisualizer.Properties;




namespace DependencyVisualizer {
    /// <summary>
    /// Type of reference
    /// </summary>
    public enum ReferenceType {
        /// <summary>
        /// File reference
        /// </summary>
        File,
        /// <summary>
        /// Project reference
        /// </summary>
        Project
    }

    /// <summary>
    /// Graph manipulation class
    /// </summary>
    public class DependencyGraphVisualizer {

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyGraphVisualizer"/> class.
        /// </summary>
        public DependencyGraphVisualizer() {
        }

        void alg_WriteVertex(object sender, VertexEventArgs e) {
            GraphvizAlgorithm algo = (GraphvizAlgorithm)sender;
            GraphvizVertex vertex = new GraphvizVertex();
            ReferenceVertex v = e.Vertex as ReferenceVertex;
            vertex.Label = v.Name;
            if (!string.IsNullOrEmpty(v.Path)) {
                vertex.Url = "file://" + Path.GetFullPath(v.Path);
            }
            // TODO: add .URL
            if (v.ReferenceType == ReferenceType.Project) {
                vertex.Shape = GraphvizVertexShape.Box;
            }
            algo.Output.Write(vertex.ToDot());
        }

        /// <summary>
        /// Visualizes the specified solution.
        /// </summary>
        /// <param name="solutionPath">The path to the solution</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions")]
        public void Visualize(string solutionPath) {
            Tracer.Info("Vizualizing {0} ...", solutionPath);
            AdjacencyGraph graph = new AdjacencyGraph(new ReferenceVertexProvider(), new EdgeProvider(), false);
            Solution solution;
            Project proj;
            switch (Path.GetExtension(solutionPath)) {
                case ".sln":
                    solution = Solution.FromFile(solutionPath);
                    break;
                case ".csproj":
                    solution = Solution.EmptySolution();
                    proj = Project.FromFile(solutionPath, solution);
                    proj.Parse();
                    solution.Projects.Add(proj.Guid, proj);
                    break;
                case ".vbproj":
                    solution = Solution.EmptySolution();
                    proj = Project.FromFile(solutionPath, solution);
                    proj.Parse();
                    solution.Projects.Add(proj.Guid, proj);
                    break;
                /*case ".vcproj":
                    // This is broken, RelativePathToProject is relative from Solution folder
                    solution = Solution.EmptySolution();
                    proj = Project.FromVCProjFile(solutionPath);
                    proj.ParseVCProj();
                    solution.Projects.Add(proj.Guid, proj);
                    break;*/
                default:
                    MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.UnsupportedExtension, Path.GetExtension(solutionPath)),
                        Resources.DependencyVisualizer, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
            }

            GraphvizAlgorithm svg = new GraphvizAlgorithm(graph, Resources.Dependencies, GraphvizImageType.Svg);
            GraphvizAlgorithm png = new GraphvizAlgorithm(graph, Resources.Dependencies, GraphvizImageType.Png);
            foreach (Project project in solution.Projects.Values) {
                AddProjectToGraph(project, graph);
            }
            Tracer.Info("Added {0} vertices to graph ...",
                (graph.VertexProvider as ReferenceVertexProvider).VertexCount);
            svg.WriteVertex += new VertexEventHandler(alg_WriteVertex);
            svg.Write(Path.GetFileNameWithoutExtension(solutionPath));

            png.WriteVertex += new VertexEventHandler(alg_WriteVertex);
            png.Write(Path.GetFileNameWithoutExtension(solutionPath));
        }

        /// <summary>
        /// Adds the specified project to graph.
        /// </summary>
        /// <param name="project">The project to add.</param>
        /// <param name="graph">The target graph.</param>
        public void AddProjectToGraph(Project project, AdjacencyGraph graph) {
            Tracer.Indent();
            if (project.Vertex == null) {
                Tracer.Debug("Adding project {0} to graph ...", project.Name);
            }
            ReferenceVertex v = (project.Vertex ?? graph.AddVertex()) as ReferenceVertex;
            v.Name = project.Name;
            v.ReferenceType = ReferenceType.Project;
            v.Path = project.Path;
            project.Vertex = v;
            foreach (string file in project.References) {

                if (Settings.Default.HideNet2Assemblies && FileRepository.IsNet20(file)) {
                    continue;
                }
                if (Settings.Default.HideNet3Assemblies && FileRepository.IsNet30(file)) {
                    continue;
                }
                if (Settings.Default.HideNet35Assemblies && FileRepository.IsNet35(file))
                {
                    continue;
                }
                if (FileRepository.Instance.GetByName(file) == null) {
                    Tracer.Debug("Adding file reference {0} to graph ...", file);
                }
                ReferenceVertex u = (FileRepository.Instance.GetByName(file) ?? graph.AddVertex()) as ReferenceVertex;
                u.Name = FileRepository.MangleName(file);
                u.ReferenceType = ReferenceType.File;
                FileRepository.Instance.Add(file, u);
                if (!graph.ContainsEdge(v, u)) {
                    Tracer.Debug("Adding edge {0} -> {1}", project.Name, file);
                    graph.AddEdge(v, u);
                }

            }

            foreach (Project projRef in project.ProjectReferences) {

                if (projRef.Vertex == null) {
                    Tracer.Debug("Adding project reference {0} to graph ...", projRef.Name);
                }
                ReferenceVertex u = (projRef.Vertex ?? graph.AddVertex()) as ReferenceVertex;
                u.Name = projRef.Name;
                u.ReferenceType = ReferenceType.Project;
                u.Path = projRef.Path;
                //m_nameMap[u] = projRef.Name;
                //m_typeMap[u] = ReferenceType.Project;
                projRef.Vertex = u;
                if (!graph.ContainsEdge(v, u)) {
                    Tracer.Debug("Adding edge {0} -> {1}", project.Name, projRef.Name);
                    graph.AddEdge(v, u);
                }

                AddProjectToGraph(projRef, graph);
            }
            Tracer.Dedent();
        }
    }
}
