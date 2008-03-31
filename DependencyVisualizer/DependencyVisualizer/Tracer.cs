//----------------------------------------------------------------
// Dependency Visualizer
//----------------------------------------------------------------
//
// Tracing utility class
//
// Copyright © 2007-2008 Simon Dahlbacka
//
// Created: 29.3 2007 Simon Dahlbacka
// $Id: $
//----------------------------------------------------------------
// $NoKeywords: $

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Globalization;



namespace DependencyVisualizer {
    /// <summary>
    /// Tracer class
    /// </summary>
    public static class Tracer {
        static readonly TraceSwitch m_switch = new TraceSwitch("TraceLevelSwitch", "Entire application");

        /// <summary>
        /// Output specified logMessage if allowed by app.config
        /// </summary>
        /// <param name="logMessage">The logMessage format.</param>
        /// <param name="args">The args.</param>
        public static void Debug(string logMessage, params object[] args) {
            Trace.WriteIf(m_switch.TraceVerbose, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) + " ");
            Trace.WriteLineIf(m_switch.TraceVerbose, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        /// <summary>
        /// Output specified logMessage if allowed by app.config
        /// </summary>
        /// <param name="logMessage">The logMessage format.</param>
        /// <param name="args">The args.</param>
        public static void Info(string logMessage, params object[] args) {
            Trace.WriteIf(m_switch.TraceInfo, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) + " ");
            Trace.WriteLineIf(m_switch.TraceInfo, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        /// <summary>
        /// Output specified logMessage if allowed by app.config
        /// </summary>
        /// <param name="logMessage">The logMessage format.</param>
        /// <param name="args">The args.</param>
        public static void Warning(string logMessage, params object[] args) {
            Trace.WriteIf(m_switch.TraceWarning, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) + " ");
            Trace.WriteLineIf(m_switch.TraceWarning, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        /// <summary>
        /// Output specified logMessage if allowed by app.config
        /// </summary>
        /// <param name="logMessage">The logMessage format.</param>
        /// <param name="args">The args.</param>
        public static void Error(string logMessage, params object[] args) {
            Trace.WriteIf(m_switch.TraceError, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) + " ");
            Trace.WriteLineIf(m_switch.TraceError, string.Format(CultureInfo.InvariantCulture, logMessage, args));
        }

        /// <summary>
        /// Indents this instance.
        /// </summary>
        public static void Indent() {
            Trace.Indent();
        }

        /// <summary>
        /// Dedents this instance.
        /// </summary>
        public static void Dedent() {
            Trace.Unindent();
        }
    }
}
