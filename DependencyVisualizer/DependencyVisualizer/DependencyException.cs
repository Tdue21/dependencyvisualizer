//----------------------------------------------------------------
// Dependency Visualizer
//----------------------------------------------------------------
//
// Dependency exception
//
// Copyright © 2007 Simon Dahlbacka  
//
// Created: 27.10 2007 Simon Dahlbacka
// $Id: $
//----------------------------------------------------------------
// $NoKeywords: $

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace DependencyVisualizer
{
    /// <summary>
    /// Dependency exception
    /// </summary>
    [Serializable]
    public class DependencyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyException"/> class.
        /// </summary>
        public DependencyException() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DependencyException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DependencyException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyException"/> class.
        /// </summary>
        /// <param name="serializationInfo">The serialization info.</param>
        /// <param name="context">The context.</param>
        protected DependencyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }
    }
}
