// <copyright file="ReferenceVertex.cs" company="Lemon Design">
// Copyright (c) 2007-2008 Lemon Design. All rights reserved.
// </copyright>
// <author>Simon Dahlbacka</author>
// <email>simon.dahlbacka@gmail.com</email>
// <date>2007-03-29</date>
// <summary>A reference vertex class to be used with Quickgraph</summary>

namespace DependencyVisualizer
{
    using System;
    using QuickGraph;

    /// <summary>
    /// A reference vertex
    /// </summary>
    public class ReferenceVertex : Vertex
    {
        /// <summary>
        /// The path to the file represented by this vertex
        /// </summary>
        private string path;

        /// <summary>
        /// The name of the reference
        /// </summary>
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceVertex"/> class.
        /// </summary>
        public ReferenceVertex()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceVertex"/> class.
        /// </summary>
        /// <param name="id">The vertex id.</param>
        public ReferenceVertex(int id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets or sets the type of the reference.
        /// </summary>
        /// <value>The type of the reference.</value>
        public ReferenceType ReferenceType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path
        {
            get
            {
                return this.path;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.path = value;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.name = value;
            }
        }
    }
}
