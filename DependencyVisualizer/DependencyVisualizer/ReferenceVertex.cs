//----------------------------------------------------------------
// Dependency visualizer
//----------------------------------------------------------------
//
// A reference vertex class to be used with Quickgraph
//
// Copyright © 2007 Simon Dahlbacka
//
// Created: 29.3 2007 Simon Dahlbacka
// $Id: $
//----------------------------------------------------------------
// $NoKeywords: $

using System;
using System.Collections.Generic;
using System.Text;
using QuickGraph.Concepts;
using QuickGraph;

namespace DependencyVisualizer {

    /// <summary>
    /// A reference vertex
    /// </summary>
    public class ReferenceVertex : Vertex {
        private ReferenceType m_referenceType;
        private string m_path;
        private string m_name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceVertex"/> class.
        /// </summary>
        public ReferenceVertex() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceVertex"/> class.
        /// </summary>
        /// <param name="id">The vertex id.</param>
        public ReferenceVertex(int id) : base(id) { }

        /// <summary>
        /// Gets or sets the type of the reference.
        /// </summary>
        /// <value>The type of the reference.</value>
        public ReferenceType ReferenceType {
            get { return m_referenceType; }
            set { m_referenceType = value; }
        }
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path {
            get { return m_path; }
            set {
                if (value == null)
                    throw new ArgumentNullException("value");
                m_path = value;
            }
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {
            get { return m_name; }
            set {
                if (value == null)
                    throw new ArgumentNullException("value");
                m_name = value;
            }
        }
    }
}
