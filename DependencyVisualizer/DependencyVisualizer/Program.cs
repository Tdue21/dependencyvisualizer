// <copyright file="Program.cs" company="Lemon Design">
// Copyright (c) 2007-2008 Lemon Design. All rights reserved.
// </copyright>
// <author>Simon Dahlbacka</author>
// <email>simon.dahlbacka@gmail.com</email>
// <date>2007-02-13</date>
// <summary>Entry point for Dependency Visualizer</summary>

namespace DependencyVisualizer
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Windows.Forms;

    using DependencyVisualizer.Properties;

    /// <summary>
    /// Entry point
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args">The command line args.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is not library code")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "RTL support is not interesting")]
        [STAThread]
        public static void Main(string[] args)
        {
            Tracer.Debug("------ Startup Dependency Visualizer {0} -------------------------------------------------", Assembly.GetExecutingAssembly().GetName().Version.ToString());
            Tracer.Debug("Working folder: {0}", Environment.CurrentDirectory);
            foreach (string arg in args)
            {
                Tracer.Debug("got argument: {0}", arg);
            }

            if (Settings.Default.ShouldUpdate)
            {
                Settings.Default.Upgrade();
                Settings.Default.ShouldUpdate = false;
                Settings.Default.Save();
            }

            if (args.Length < 1)
            {
                Usage();
            }

            if (Array.Exists<string>(args, delegate(string needle) { return needle.Equals("/configure", StringComparison.CurrentCultureIgnoreCase); }))
            {
                ConfigureDependencyVisualizer dlg = new ConfigureDependencyVisualizer();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.Save();
                }

                return;
            }

            DependencyGraphVisualizer viz = new DependencyGraphVisualizer();
            try
            {
                viz.Visualize(args[0]);
            }
            catch (Exception e)
            {
                Tracer.Error(e.ToString());
                MessageBox.Show(
                    string.Format(CultureInfo.CurrentCulture, Resources.UnexpectedError, e.Message),
                    Resources.DependencyVisualizationError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Display the instructions how to use this software. 
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "RTL support isn't interesting")]
        private static void Usage()
        {
            MessageBox.Show(
                Resources.ProgramDescription + Environment.NewLine +
                            string.Empty + Environment.NewLine +
                            Resources.Usage + ": " + Application.ProductName + " solution.sln" + Environment.NewLine +
                            ((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright + Environment.NewLine +
                            Resources.AdditionalCopyright,
                            Resources.UsageInformation,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
            Environment.Exit(1);
        }
    }
}
