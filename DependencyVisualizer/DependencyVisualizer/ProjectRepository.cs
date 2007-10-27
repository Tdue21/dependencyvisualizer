//----------------------------------------------------------------
// DependencyVisualizer
//----------------------------------------------------------------
//
// Project repository - keeps track of projects
//
// Copyright © 2007 Simon Dahlbacka
//
// Created: 13.2 2007 Simon Dahlbacka
// $Id: $
//----------------------------------------------------------------
// $NoKeywords: $

using System;
using System.Collections.Generic;


namespace DependencyVisualizer {
    /// <summary>
    /// Project repository
    /// </summary>
    public class ProjectRepository {

        private Dictionary<Guid, Project> m_store;
        private static readonly ProjectRepository m_instance = new ProjectRepository();

        private ProjectRepository() {
            m_store = new Dictionary<Guid, Project>();
        }


        /// <summary>
        /// Gets the repository instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ProjectRepository Instance {
            get { return m_instance; }
        }

        /// <summary>
        /// Gets the project by GUID.
        /// </summary>
        /// <param name="guid">The project GUID.</param>
        /// <returns>the specified project or <c>null</c> if specified project does not yet exist</returns>
        public Project GetByGuid(Guid guid) {
            if (m_store.ContainsKey(guid)) {
                return m_store[guid];
            }
            return null;
        }

        /// <summary>
        /// Adds the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void Add(Project project) {
            m_store[project.Guid] = project;
        }
    }
}
