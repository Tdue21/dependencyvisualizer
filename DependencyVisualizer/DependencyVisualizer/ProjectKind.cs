// <copyright file="ProjectKind.cs" company="Lemon Design">
// Copyright (c) 2007-2008 Lemon Design. All rights reserved.
// </copyright>
// <author>Simon Dahlbacka</author>
// <email>simon.dahlbacka@gmail.com</email>
// <date>2008-06-13</date>
// <summary>Project kinds</summary>

namespace DependencyVisualizer
{
    using System.ComponentModel;

    /// <summary>
    /// Project kind
    /// </summary>
    public enum ProjectKind
    {
        /// <summary>
        /// Unknown project type
        /// </summary>
        [Description("{00000000-0000-0000-0000-000000000000}")]
        Unknown,

        /// <summary>
        /// C# project
        /// </summary>
        [Description("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}")]
        CSharp,

        /// <summary>
        /// VB.NET project
        /// </summary>
        [Description("{F184B08F-C81C-45F6-A57F-5ABD9991F28F}")]
        VBNet,

        /// <summary>
        /// Solution folder
        /// </summary>
        [Description("{2150E333-8FDC-42A3-9474-1A3956D46DE8}")]
        SolutionItems,

        /// <summary>
        /// Setup project
        /// </summary>
        [Description("{54435603-DBB4-11D2-8724-00A0C9A8B90C}")]
        Setup,

        /// <summary>
        /// C++ project
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cpp", Justification = "This is the correct name")]
        [Description("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}")]
        Cpp,

        /// <summary>
        /// J# project
        /// </summary>
        [Description("{E6FDF86B-F3D1-11D4-8576-0002A516ECE8}")]
        JSharp,

        /// <summary>
        /// Web project 
        /// </summary>
        [Description("{E24C65DC-7377-472b-9ABA-BC803B73C61A}")]
        WebProject,

        /// <summary>
        /// WiX project
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wix", Justification = "This is the correct name")]
        [Description("{930C7802-8A8C-48F9-8165-68863BCCD9DD}")]
        Wix,

        /// <summary>
        /// C++ project (vcxproj)
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Cpp", Justification = "This is the correct name")]
        [Description("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}")]
        CppX
    }
}
