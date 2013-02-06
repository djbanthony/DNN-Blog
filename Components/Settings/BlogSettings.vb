﻿'
' DotNetNuke® - http://www.dotnetnuke.com
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
Imports System.Xml
Imports DotNetNuke.Modules.Blog.Providers.Data
Imports DotNetNuke.Modules.Blog.Common
Imports DotNetNuke.Common.Utilities

Namespace Settings

 ''' <summary>
 ''' This class abstracts all settings for the module and makes sure they're (a) defaulted and (b) hard typed 
 ''' throughout the application.
 ''' </summary>
 ''' <remarks></remarks>
 ''' <history>
 '''		[pdonker]	12/14/2009	created
 ''' </history>
 Public Class BlogSettings

#Region " Private Members "

  Private _allSettings As Hashtable
  Private _SearchSummaryMaxLength As Integer = 255

  Private _portalId As Integer = -1
  Private _tabId As Integer = -1

#End Region

#Region " Constructors "

  Public Sub New(ByVal PortalID As Integer, ByVal TabID As Integer)
   Dim mc As New DotNetNuke.Entities.Modules.ModuleController
   _portalId = PortalID
   _tabId = TabID
   _allSettings = New Hashtable

   Dim dr As IDataReader = DataProvider.Instance().GetSettings(PortalID, TabID)
   While dr.Read()
    _allSettings(dr.GetString(0)) = dr.GetValue(1)
   End While
   dr.Close()

   _allSettings.ReadValue("PageBlogs", PageBlogs)
   _allSettings.ReadValue("EntryDescriptionRequired", EntryDescriptionRequired)
   _allSettings.ReadValue("SummaryMaxLength", SummaryMaxLength)
   _allSettings.ReadValue("MaxImageWidth", MaxImageWidth)
   _allSettings.ReadValue("RecentEntriesMax", RecentEntriesMax)
   _allSettings.ReadValue("RecentRssEntriesMax", RecentRssEntriesMax)
   _allSettings.ReadValue("EnableUploadOption", EnableUploadOption)
   _allSettings.ReadValue("ShowSummary", ShowSummary)
   _allSettings.ReadValue("ShowSeoFriendlyUrl", ShowSeoFriendlyUrl)
   _allSettings.ReadValue("EnforceSummaryTruncation", EnforceSummaryTruncation)
   _allSettings.ReadValue("IncludeBody", IncludeBody)
   _allSettings.ReadValue("IncludeCategoriesInDescription", IncludeCategoriesInDescription)
   _allSettings.ReadValue("IncludeTagsInDescription", IncludeTagsInDescription)
   _allSettings.ReadValue("AllowSummaryHtml", AllowSummaryHtml)
   _allSettings.ReadValue("AllowWLW", AllowWLW)
   _allSettings.ReadValue("IncludeFiles", IncludeFiles)
   _allSettings.ReadValue("SocialSharingMode", SocialSharingMode)
   _allSettings.ReadValue("AddThisId", AddThisId)
   _allSettings.ReadValue("FacebookAppId", FacebookAppId)
   _allSettings.ReadValue("EnablePlusOne", EnablePlusOne)
   _allSettings.ReadValue("EnableTwitter", EnableTwitter)
   _allSettings.ReadValue("EnableLinkedIn", EnableLinkedIn)
   _allSettings.ReadValue("CommentMode", CommentMode)

   ' WLW implementation parameters
   _allSettings.ReadValue("AllowMultipleCategories", AllowMultipleCategories)
   _allSettings.ReadValue("UseWLWExcerpt", UseWLWExcerpt)

   _allSettings.ReadValue("VocabularyId", VocabularyId)

  End Sub

  Public Shared Function GetBlogSettings(ByVal PortalId As Integer, ByVal TabId As Integer) As BlogSettings
   Dim CacheKey As String = "BlogSettings" & PortalId.ToString & "-" & TabId.ToString
   Dim bs As BlogSettings = CType(DotNetNuke.Common.Utilities.DataCache.GetCache(CacheKey), BlogSettings)
   If bs Is Nothing Then
    bs = New BlogSettings(PortalId, TabId)
    DotNetNuke.Common.Utilities.DataCache.SetCache(CacheKey, bs)
   End If
   Return bs
  End Function

#End Region

#Region " Public Members "

  Public Overridable Sub UpdateSettings()
   Dim objModules As New DotNetNuke.Entities.Modules.ModuleController
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "PageBlogs", Me.PageBlogs.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "EntryDescriptionRequired", Me.EntryDescriptionRequired.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "SummaryMaxLength", Me.SummaryMaxLength.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "MaxImageWidth", Me.MaxImageWidth.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "RecentEntriesMax", Me.RecentEntriesMax.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "RecentRssEntriesMax", Me.RecentRssEntriesMax.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "EnableUploadOption", Me.EnableUploadOption.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "ShowSummary", Me.ShowSummary.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "ShowSeoFriendlyUrl", Me.ShowSeoFriendlyUrl.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "EnforceSummaryTruncation", Me.EnforceSummaryTruncation.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "IncludeBody", Me.IncludeBody.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "IncludeCategoriesInDescription", Me.IncludeCategoriesInDescription.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "IncludeTagsInDescription", Me.IncludeTagsInDescription.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "AllowSummaryHtml", Me.AllowSummaryHtml.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "AllowWLW", Me.AllowWLW.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "IncludeFiles", Me.IncludeFiles)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "SocialSharingMode", Me.SocialSharingMode.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "AddThisId", Me.AddThisId)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "FacebookAppId", Me.FacebookAppId)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "EnablePlusOne", Me.EnablePlusOne.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "EnableTwitter", Me.EnableTwitter.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "EnableLinkedIn", Me.EnableLinkedIn.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "CommentMode", Me.CommentMode.ToString)

   ' WLW implementation parameters
   Business.Utility.UpdateBlogModuleSetting(_portalId, -1, "AllowMultipleCategories", Me.AllowMultipleCategories.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, -1, "UseWLWExcerpt", Me.UseWLWExcerpt.ToString)

   ' We save this at 'global' and tab level so its available to WLW as well as normal module usage.
   Business.Utility.UpdateBlogModuleSetting(_portalId, -1, "VocabularyId", Me.VocabularyId.ToString)
   Business.Utility.UpdateBlogModuleSetting(_portalId, _tabId, "VocabularyId", Me.VocabularyId.ToString)

   ' Clear cache
   Dim tagsKey As String = Common.Constants.TermsKey + "1"
   DataCache.RemoveCache(tagsKey)
   Dim categoriesKey As String = Common.Constants.TermsKey + VocabularyId.ToString()
   DataCache.RemoveCache(categoriesKey)

   'DataCache.ClearCache(Constants.ModuleCacheKeyPrefix + Constants.VocabTermsCacheKey + Constants.VocabSuffixCacheKey + VocabularyId.ToString())

   Dim CacheKey As String = "BlogSettings" & _portalId.ToString & "-" & _tabId.ToString
   DotNetNuke.Common.Utilities.DataCache.RemoveCache(CacheKey)
  End Sub

  Public Sub WriteWLWManifest(ByRef output As XmlTextWriter)
   output.Formatting = Formatting.Indented
   output.WriteStartDocument()
   output.WriteStartElement("manifest")
   output.WriteAttributeString("xmlns", "http://schemas.microsoft.com/wlw/manifest/weblog")
   output.WriteStartElement("options")

   output.WriteElementString("clientType", "Metaweblog")
   output.WriteElementString("supportsMultipleCategories", Globals.CYesNo(AllowMultipleCategories))
   output.WriteElementString("supportsCategories", "Yes")
   output.WriteElementString("supportsCustomDate", "Yes")
   output.WriteElementString("supportsKeywords", "Yes")
   output.WriteElementString("supportsTrackbacks", "Yes")
   output.WriteElementString("supportsEmbeds", "No")
   output.WriteElementString("supportsAuthor", "No")
   output.WriteElementString("supportsExcerpt", Globals.CYesNo(UseWLWExcerpt))
   output.WriteElementString("supportsPassword", "No")
   output.WriteElementString("supportsPages", "No")
   output.WriteElementString("supportsPageParent", "No")
   output.WriteElementString("supportsPageOrder", "No")
   output.WriteElementString("supportsExtendedEntries", "Yes")
   output.WriteElementString("supportsCommentPolicy", "Yes")
   output.WriteElementString("supportsPingPolicy", "Yes")
   output.WriteElementString("supportsPostAsDraft", "Yes")
   output.WriteElementString("supportsFileUpload", "Yes")
   output.WriteElementString("supportsSlug", "No")
   output.WriteElementString("supportsHierarchicalCategories", "Yes")
   output.WriteElementString("supportsCategoriesInline", "Yes")
   output.WriteElementString("supportsNewCategories", "No")
   output.WriteElementString("supportsNewCategoriesInline", "No")

   output.WriteEndElement() ' manifest
   output.WriteEndElement() ' options
   output.Flush()
  End Sub

#End Region

#Region " Properties "

  Public Property PageBlogs As Integer = -1
  Public Property EntryDescriptionRequired As Boolean = False
  Public Property SummaryMaxLength As Integer = 1024
  Public Property MaxImageWidth As Integer = 400
  Public Property RecentEntriesMax As Integer = 10
  Public Property RecentRssEntriesMax As Integer = 10
  Public Property EnableUploadOption As Boolean = False
  Public Property ShowSummary As Boolean = False
  Public Property ShowSeoFriendlyUrl As Boolean = True
  Public Property EnforceSummaryTruncation As Boolean = False
  Public Property IncludeBody As Boolean = False
  Public Property IncludeCategoriesInDescription As Boolean = True
  Public Property IncludeTagsInDescription As Boolean = True
  Public Property AllowSummaryHtml As Boolean = True
  Public Property AllowWLW As Boolean = True
  Public Property AllowMultipleCategories As Boolean = True
  Public Property UseWLWExcerpt As Boolean = False
  Public Property IncludeFiles As String = ""

  ''' <summary>
  ''' This is the base vocabulary (restricted to current portal level) to be used for all categories selection options.
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property VocabularyId As Integer = -1
  Public Property SocialSharingMode As Integer = Constants.SocialSharingMode.Default
  Public Property AddThisId As String = ""
  Public Property FacebookAppId As String = ""
  Public Property EnablePlusOne As Boolean = True
  Public Property EnableTwitter As Boolean = True
  Public Property EnableLinkedIn As Boolean = True
  Public Property CommentMode As Integer = Constants.CommentMode.Default
#End Region

 End Class

End Namespace