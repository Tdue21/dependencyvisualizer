// <copyright file="Tracer.cs" company="Lemon Design">
// Copyright (c) 2007-2008 Lemon Design. All rights reserved.
// </copyright>
// <author>Simon Dahlbacka</author>
// <email>simon.dahlbacka@gmail.com</email>
// <date>2007-03-29</date>
// <summary>Tracing utility class</summary>

namespace DependencyVisualizer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Globalization;

    /// <summary>
    /// Tracer class
    /// </summary>
    public static class Tracer
    {
        /// <summary>
        /// The trace switch used
        /// </summary>
        private static readonly TraceSwitch traceSwitch = new TraceSwitch("TraceLevelSwitch", "Entire application");

        /// <summary>
        /// Output specified logMessage if allowed by app.config
        /// </summary>
        /// <param name="logMessage">The logMessage format.</param>
        /// <param name="args">The parameters for the log message.</param>
        public static void Debug(string logMessage, params object[] args)
        {
            Trace.WriteIf(traceSwitch.TraceVerbose, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) + " ");
            Trace.WriteLineIf(traceSwitch.TraceVerbose, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        /// <summary>
        /// Output specified logMessage if allowed by app.config
        /// </summary>
        /// <param name="logMessage">The logMessage format.</param>
        /// <param name="args">The parameters for the log message.</param>
        public static void Info(string logMessage, params object[] args)
        {
            Trace.WriteIf(traceSwitch.TraceInfo, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) + " ");
            Trace.WriteLineIf(traceSwitch.TraceInfo, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        /// <summary>
        /// Output specified logMessage if allowed by app.config
        /// </summary>
        /// <param name="logMessage">The logMessage format.</param>
        /// <param name="args">The parameters for the log message.</param>
        public static void Warning(string logMessage, params object[] args)
        {
            Trace.WriteIf(traceSwitch.TraceWarning, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) + " ");
            Trace.WriteLineIf(traceSwitch.TraceWarning, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        /// <summary>
        /// Output specified logMessage if allowed by app.config
        /// </summary>
        /// <param name="logMessage">The logMessage format.</param>
        /// <param name="args">The parameters for the log message.</param>
        public static void Error(string logMessage, params object[] args)
        {
            Trace.WriteIf(traceSwitch.TraceError, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) + " ");
            Trace.WriteLineIf(traceSwitch.TraceError, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        /// <summary>
        /// Indents this instance.
        /// </summary>
        public static void Indent()
        {
            Trace.Indent();
        }

        /// <summary>
        /// Dedents this instance.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dedent", Justification = "This is the correct spelling")]
        public static void Dedent()
        {
            Trace.Unindent();
        }
    }
}
