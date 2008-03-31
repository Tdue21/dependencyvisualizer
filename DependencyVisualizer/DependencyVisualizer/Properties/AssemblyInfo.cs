using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Resources;


// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DependencyVisualizer")]
[assembly: AssemblyDescription("Visualize inter-project dependencies")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Simon Dahlbacka")]
[assembly: AssemblyProduct("DependencyVisualizer")]
[assembly: AssemblyCopyright("Copyright © 2007-2008 Simon Dahlbacka")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("a5624131-8a5c-4c64-b013-31b261e82a53")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("1.2.8.0")]

[assembly: ReflectionPermission(SecurityAction.RequestMinimum, Flags=ReflectionPermissionFlag.NoFlags)]
[assembly: FileIOPermission(SecurityAction.RequestMinimum, AllLocalFiles=FileIOPermissionAccess.Read)]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguage("en-US")]