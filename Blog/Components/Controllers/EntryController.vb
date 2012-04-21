'
' DotNetNuke� - http://www.dotnetnuke.com
' Copyright (c) 2002-2012
' by DotNetNuke Corporation
'
' Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
' documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
' the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
' to permit persons to whom the Software is furnished to do so, subject to the following conditions:
'
' The above copyright notice and this permission notice shall be included in all copies or substantial portions 
' of the Software.
'
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
' DEALINGS IN THE SOFTWARE.
'

Imports DotNetNuke.Entities.Content
Imports DotNetNuke.Modules.Blog.Data
Imports DotNetNuke.Common.Utilities

Namespace Business

    Public Class EntryController

        Public Function GetEntry(ByVal EntryID As Integer, ByVal PortalId As Integer) As EntryInfo
            Return CType(CBO.FillObject(DataProvider.Instance().GetEntry(EntryID, PortalId), GetType(EntryInfo)), EntryInfo)
        End Function

        Public Function ListEntries(ByVal PortalID As Integer, ByVal BlogID As Integer, ByVal BlogDate As Date, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As ArrayList
            Return CBO.FillCollection(DataProvider.Instance().ListEntries(PortalID, BlogID, BlogDate, ShowNonPublic, ShowNonPublished), GetType(EntryInfo))
        End Function

        Public Function ListEntriesByBlog(ByVal BlogID As Integer, ByVal BlogDate As Date, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False, Optional ByVal RecentEntriesMax As Integer = 10) As ArrayList
            Return CBO.FillCollection(DataProvider.Instance().ListEntriesByBlog(BlogID, BlogDate, ShowNonPublic, ShowNonPublished, RecentEntriesMax), GetType(EntryInfo))
        End Function

        Public Function ListAllEntriesByBlog(ByVal BlogID As Integer) As ArrayList
            Return CBO.FillCollection(DataProvider.Instance().ListAllEntriesByBlog(BlogID), GetType(EntryInfo))
        End Function

        Public Function ListEntriesByPortal(ByVal PortalID As Integer, ByVal BlogDate As Date, ByVal BlogDateType As String, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False, Optional ByVal RecentEntriesMax As Integer = 10) As ArrayList
            Return CBO.FillCollection(DataProvider.Instance().ListEntriesByPortal(PortalID, BlogDate, BlogDateType, ShowNonPublic, ShowNonPublished, RecentEntriesMax), GetType(EntryInfo))
        End Function

        Public Function ListAllEntriesByPortal(ByVal PortalID As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As ArrayList
            Return CBO.FillCollection(DataProvider.Instance().ListAllEntriesByPortal(PortalID, ShowNonPublic, ShowNonPublished), GetType(EntryInfo))
        End Function

        Public Function ListAllEntriesByCategory(ByVal PortalID As Integer, ByVal CatID As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As ArrayList
            Return CBO.FillCollection(DataProvider.Instance().ListAllEntriesByCategory(PortalID, CatID, ShowNonPublic, ShowNonPublished), GetType(EntryInfo))
        End Function

        Public Function ListAllEntriesByTag(ByVal PortalID As Integer, ByVal TagID As Integer, Optional ByVal ShowNonPublic As Boolean = False, Optional ByVal ShowNonPublished As Boolean = False) As ArrayList
            Return CBO.FillCollection(DataProvider.Instance().ListAllEntriesByTag(PortalID, TagID, ShowNonPublic, ShowNonPublished), GetType(EntryInfo))
        End Function

        Public Function AddEntry(ByVal objEntry As EntryInfo, ByVal tabId As Integer) As EntryInfo
            objEntry.EntryID = CType(DataProvider.Instance().AddEntry(objEntry.BlogID, objEntry.Title, objEntry.Description, objEntry.Entry, objEntry.Published, objEntry.AllowComments, objEntry.AddedDate, objEntry.DisplayCopyright, objEntry.Copyright, objEntry.PermaLink), Integer)

            objEntry.ContentItemId = CompleteEntryCreation(objEntry, tabId)



            Return objEntry
        End Function

        Public Sub UpdateEntry(ByVal objEntry As EntryInfo, ByVal tabId As Integer, ByVal portalId As Integer)
            DataProvider.Instance().UpdateEntry(objEntry.BlogID, objEntry.EntryID, objEntry.Title, objEntry.Description, objEntry.Entry, objEntry.Published, objEntry.AllowComments, objEntry.AddedDate, objEntry.DisplayCopyright, objEntry.Copyright, objEntry.PermaLink, objEntry.ContentItemId)

            CompleteEntryUpdate(objEntry, tabId, portalId)
        End Sub

        Public Sub DeleteEntry(ByVal EntryID As Integer, ByVal contentItemId As Integer)
            DataProvider.Instance().DeleteEntry(EntryID)

            CompleteEntryDelete(contentItemId)
        End Sub

#Region "Private Methods"

        ''' <summary>
        ''' This completes the things necessary for creating a content item in the data store.
        ''' </summary>
        ''' <param name="objEntry"></param>
        ''' <param name="tabId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function CompleteEntryCreation(ByVal objEntry As EntryInfo, ByVal tabId As Integer) As Integer
            Dim cntTaxonomy As New Content()
            Dim objContentItem As ContentItem = cntTaxonomy.CreateContentItem(objEntry, tabId)

            Return objContentItem.ContentItemId
        End Function

        ''' <summary>
        ''' Handles any content item/taxonomy updates, then deals w/ cache clearing (if applicable)
        ''' </summary>
        ''' <param name="objEntry"></param>
        ''' <param name="tabId"></param>
        ''' <remarks></remarks>
        Private Shared Sub CompleteEntryUpdate(ByVal objEntry As EntryInfo, ByVal tabId As Integer, ByVal portalId As Integer)
            Dim cntTaxonomy As New Content()
            cntTaxonomy.UpdateContentItem(objEntry, tabId, portalId)
        End Sub

        Private Shared Sub CompleteEntryDelete(ByVal contentItemId As Integer)
            Content.DeleteContentItem(contentItemId)
        End Sub

#End Region

    End Class

End Namespace