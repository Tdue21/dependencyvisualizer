//----------------------------------------------------------------
// Dependency Visualizer
//----------------------------------------------------------------
//
// Reference vertex provider class to be used with Quickgraph
//
// Copyright © 2007 Simon Dahlbacka
//
// Created: 29.3 2007 Simon Dahlbacka
// $Id: $
//----------------------------------------------------------------
// $NoKeywords: $

using System;
using System.Collections.Generic;
using QuickGraph.Concepts.Providers;
using QuickGraph.Concepts;

namespace DependencyVisualizer {
    /// <summary>
    /// Vertex factory for reference vertices
    /// </summary>
    public sealed class ReferenceVertexProvider : IVertexProvider {
        
        private int m_nextId;

        /// <summary>
        /// Gets the total vertex count.
        /// </summary>
        /// <value>The vertex count.</value>
        public int VertexCount {
            get { return m_nextId; }
        }

        #region IVertexProvider Members

        /// <summary>
        /// Provides the vertex.
        /// </summary>
        /// <returns></returns>
        public IVertex ProvideVertex() {
            return new ReferenceVertex(m_nextId++);
        }

        /// <summary>
        /// Updates the vertex.
        /// </summary>
        /// <param name="v">The v.</param>
        public void UpdateVertex(IVertex v) {
            UpdateVertex((ReferenceVertex)v);
        }

        /// <summary>
        /// Updates the vertex.
        /// </summary>
        /// <param name="v">The v.</param>
        public void UpdateVertex(ReferenceVertex v) {
            if (v == null) {
                throw new ArgumentNullException("v");
            }
            v.ID = m_nextId++;
        }

        /// <summary>
        /// Gets the type of the vertex.
        /// </summary>
        /// <value>The type of the vertex.</value>
        public Type VertexType {
            get { return typeof(ReferenceVertex); }
        }

        #endregion
    }
}
