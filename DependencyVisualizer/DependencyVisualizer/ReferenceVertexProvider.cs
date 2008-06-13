// <copyright file="ReferenceVertexProvider.cs" company="Lemon Design">
// Copyright (c) 2007-2008 Lemon Design. All rights reserved.
// </copyright>
// <author>Simon Dahlbacka</author>
// <email>simon.dahlbacka@gmail.com</email>
// <date>2007-03-29</date>
// <summary>Reference vertex provider class to be used with Quickgraph</summary>

namespace DependencyVisualizer
{
    using System;
    using QuickGraph.Concepts;
    using QuickGraph.Concepts.Providers;

    /// <summary>
    /// Vertex factory for reference vertices
    /// </summary>
    public sealed class ReferenceVertexProvider : IVertexProvider
    {
        /// <summary>
        /// The id for the next vertex to be provided.
        /// </summary>
        private int nextId;

        /// <summary>
        /// Gets the total vertex count.
        /// </summary>
        /// <value>The vertex count.</value>
        public int VertexCount
        {
            get { return this.nextId; }
        }

        #region IVertexProvider Members
        
        /// <summary>
        /// Gets the type of the vertex.
        /// </summary>
        /// <value>The type of the vertex.</value>
        public Type VertexType
        {
            get { return typeof(ReferenceVertex); }
        }

        /// <summary>
        /// Provides the vertex.
        /// </summary>
        /// <returns>A new vertex</returns>
        public IVertex ProvideVertex()
        {
            return new ReferenceVertex(this.nextId++);
        }

        /// <summary>
        /// Updates the vertex.
        /// </summary>
        /// <param name="v">The vertex to be updated.</param>
        public void UpdateVertex(IVertex v)
        {
            this.UpdateVertex((ReferenceVertex)v);
        }

        /// <summary>
        /// Updates the vertex.
        /// </summary>
        /// <param name="vertex">The vertex to be updated.</param>
        public void UpdateVertex(ReferenceVertex vertex)
        {
            if (vertex == null)
            {
                throw new ArgumentNullException("vertex");
            }

            vertex.ID = this.nextId++;
        }

        #endregion
    }
}
