' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports Microsoft.CodeAnalysis.Editor.UnitTests.Workspaces
Imports Microsoft.VisualStudio.LanguageServices.Implementation.SolutionExplorer
Imports Microsoft.VisualStudio.LanguageServices.SolutionExplorer
Imports Roslyn.Test.Utilities

Namespace Microsoft.VisualStudio.LanguageServices.UnitTests.SolutionExplorer
    Public Class AnalyzerItemTests
        <Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)>
        Public Sub Name()
            Dim workspaceXml =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Analyzer Name="Foo" FullPath="C:\Users\Bill\Documents\Analyzers\Foo.dll"/>
                    </Project>
                </Workspace>

            Using workspace = TestWorkspaceFactory.CreateWorkspace(workspaceXml)
                Dim project = workspace.Projects.Single()

                Dim analyzerFolder = New AnalyzersFolderItem(workspace, project.Id, Nothing)
                Dim analyzer = New AnalyzerItem(analyzerFolder, project.AnalyzerReferences.Single())

                Assert.Equal(expected:="Foo", actual:=analyzer.Text)
            End Using
        End Sub

        <Fact, Trait(Traits.Feature, Traits.Features.Diagnostics)>
        Public Sub BrowseObject()
            Dim workspaceXml =
                <Workspace>
                    <Project Language="C#" CommonReferences="true">
                        <Analyzer Name="Foo" FullPath="C:\Users\Bill\Documents\Analyzers\Foo.dll"/>
                    </Project>
                </Workspace>

            Using workspace = TestWorkspaceFactory.CreateWorkspace(workspaceXml)
                Dim project = workspace.Projects.Single()

                Dim analyzerFolder = New AnalyzersFolderItem(workspace, project.Id, Nothing)
                Dim analyzer = New AnalyzerItem(analyzerFolder, project.AnalyzerReferences.Single())
                Dim browseObject = DirectCast(analyzer.GetBrowseObject(), AnalyzerItem.BrowseObject)

                Assert.Equal(expected:=SolutionExplorerShim.AnalyzerItem_PropertyWindowClassName, actual:=browseObject.GetClassName())
                Assert.Equal(expected:="Foo", actual:=browseObject.GetComponentName())
                Assert.Equal(expected:="Foo", actual:=browseObject.Name)
                Assert.Equal(expected:="C:\Users\Bill\Documents\Analyzers\Foo.dll", actual:=browseObject.Path)
            End Using
        End Sub
    End Class
End Namespace
