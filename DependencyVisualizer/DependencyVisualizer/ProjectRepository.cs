// <copyright file="ProjectRepository.cs" company="Lemon Design">
// Copyright (c) 2007-2008 Lemon Design. All rights reserved.
// </copyright>
// <author>Simon Dahlbacka</author>
// <email>simon.dahlbacka@gmail.com</email>
// <date>2007-02-13</date>
// <summary>Project repository - keeps track of projects</summary>

namespace DependencyVisualizer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Project repository
    /// </summary>
    public class ProjectRepository
    {
        /// <summary>
        /// The singleton instance
        /// </summary>
        private static readonly ProjectRepository instance = new ProjectRepository();

        /// <summary>
        /// The backing field for projects
        /// </summary>
        private Dictionary<Guid, Project> projectStore;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        private ProjectRepository()
        {
            this.projectStore = new Dictionary<Guid, Project>();
        }

        /// <summary>
        /// Gets the repository instance.
        /// </summary>
        /// <value>The instance.</value>
        public static ProjectRepository Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Gets the project by GUID.
        /// </summary>
        /// <param name="projectIdentifier">The project GUID.</param>
        /// <returns>the specified project or <c>null</c> if specified project does not yet exist</returns>
        public Project GetByGuid(Guid projectIdentifier)
        {
            if (this.projectStore.ContainsKey(projectIdentifier))
            {
                return this.projectStore[projectIdentifier];
            }

            return null;
        }

        /// <summary>
        /// Adds the specified project.
        /// </summary>
        /// <param name="project">The project.</param>
        public void Add(Project project)
        {
            this.projectStore[project.Guid] = project;
        }
    }
}
