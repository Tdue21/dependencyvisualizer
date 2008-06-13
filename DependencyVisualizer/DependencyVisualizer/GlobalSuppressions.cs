// <copyright file="GlobalSuppressions.cs" company="Lemon Design">
// Copyright (c) 2007-2008 Lemon Design. All rights reserved.
// </copyright>
// <author>Simon Dahlbacka</author>
// <email>simon.dahlbacka@gmail.com</email>
// <date>2008-06-13</date>
// <summary>Global suppressions for code analysis.</summary>

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "This is an exe and Quickgraph isn't signed...")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Visualizer")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Visualizer", Scope = "type", Target = "DependencyVisualizer.DependencyGraphVisualizer")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Visualizer", Scope = "type", Target = "DependencyVisualizer.ConfigureDependencyVisualizer")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Visualizer", Scope = "namespace", Target = "DependencyVisualizer")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "Visualizer", Scope = "resource", Target = "DependencyVisualizer.ConfigureDependencyVisualizer.resources")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "Cansdale", Scope = "resource", Target = "DependencyVisualizer.Properties.Resources.resources")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "Halleux", Scope = "resource", Target = "DependencyVisualizer.Properties.Resources.resources")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "Visualizer", Scope = "resource", Target = "DependencyVisualizer.Properties.Resources.resources")]
