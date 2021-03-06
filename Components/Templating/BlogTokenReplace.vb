'
' DNN Connect - http://dnn-connect.org
' Copyright (c) 2014
' by DNN Connect
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

Imports DotNetNuke.Entities.Portals
Imports DotNetNuke.Entities.Users
Imports DotNetNuke.Services.Tokens
Imports DotNetNuke.Modules.Blog.Entities.Blogs
Imports DotNetNuke.Modules.Blog.Entities.Posts
Imports DotNetNuke.Modules.Blog.Entities.Terms
Imports DotNetNuke.Modules.Blog.Entities.Comments

Namespace Templating
 Public Class BlogTokenReplace
  Inherits GenericTokenReplace

  Public Sub New(moduleId As Integer)
   MyBase.new(Scope.DefaultSettings)

   Dim actModule As DotNetNuke.Entities.Modules.ModuleInfo = (New DotNetNuke.Entities.Modules.ModuleController).GetModule(moduleId)
   Me.ModuleInfo = actModule
   Me.UseObjectLessExpression = False

  End Sub

  Public Sub New(actModule As DotNetNuke.Entities.Modules.ModuleInfo, security As Modules.Blog.Security.ContextSecurity, blog As BlogInfo, post As PostInfo, settings As Common.ModuleSettings, viewSettings As ViewSettings)
   MyBase.new(Scope.DefaultSettings)

   Me.ModuleInfo = actModule
   Me.UseObjectLessExpression = False
   Me.PropertySource("security") = security
   Me.PropertySource("settings") = settings
   Me.PropertySource("viewsettings") = viewSettings
   If blog IsNot Nothing Then
    Me.PropertySource("blog") = blog
    Me.PropertySource("owner") = New LazyLoadingUser(PortalSettings.PortalId, blog.OwnerUserId, blog.Username)
   End If
   If post IsNot Nothing Then
    Me.PropertySource("post") = post
   End If

  End Sub

  Public Sub New(actModule As DotNetNuke.Entities.Modules.ModuleInfo, security As Modules.Blog.Security.ContextSecurity, blog As BlogInfo, Post As PostInfo, settings As Common.ModuleSettings, viewSettings As ViewSettings, comment As CommentInfo)
   MyBase.new(Scope.DefaultSettings)

   Me.PrimaryObject = comment
   Me.ModuleInfo = actModule
   Me.UseObjectLessExpression = False
   Me.PropertySource("security") = security
   Me.PropertySource("settings") = settings
   Me.PropertySource("viewsettings") = viewSettings
   If Post IsNot Nothing Then
    Me.PropertySource("post") = Post
    Me.PropertySource("author") = New LazyLoadingUser(PortalSettings.PortalId, Post.CreatedByUserID, Post.Username)
    Me.PropertySource("blog") = Post.Blog
    Me.PropertySource("owner") = New LazyLoadingUser(PortalSettings.PortalId, Post.Blog.OwnerUserId, Post.Blog.Username)
   ElseIf blog IsNot Nothing Then
    Me.PropertySource("blog") = blog
    Me.PropertySource("owner") = New LazyLoadingUser(PortalSettings.PortalId, blog.OwnerUserId, blog.Username)
   End If
   Me.PropertySource("comment") = comment
   Me.PropertySource("commenter") = New LazyLoadingUser(PortalSettings.PortalId, comment.CreatedByUserID, comment.Username)

  End Sub

  Public Sub New(blogModule As BlogModuleBase)
   MyBase.new(Scope.DefaultSettings)

   Me.ModuleInfo = blogModule.ModuleConfiguration
   Me.UseObjectLessExpression = False
   Me.PropertySource("query") = blogModule.BlogContext
   Me.PropertySource("security") = blogModule.BlogContext.Security
   Me.PropertySource("urls") = blogModule.BlogContext.ModuleUrls
   Me.PropertySource("settings") = blogModule.Settings
   Me.PropertySource("viewsettings") = blogModule.ViewSettings
   If blogModule.BlogContext.Blog IsNot Nothing Then
    Me.PropertySource("blog") = blogModule.BlogContext.Blog
    Me.PropertySource("owner") = New LazyLoadingUser(PortalSettings.PortalId, blogModule.BlogContext.Blog.OwnerUserId, blogModule.BlogContext.Blog.Username)
   End If
   If blogModule.BlogContext.Post IsNot Nothing Then
    Me.PropertySource("post") = blogModule.BlogContext.Post
    Me.PropertySource("author") = New LazyLoadingUser(PortalSettings.PortalId, blogModule.BlogContext.Post.CreatedByUserID, blogModule.BlogContext.Post.Username)
   End If
   If blogModule.BlogContext.Term IsNot Nothing Then
    Me.PropertySource("selectedterm") = blogModule.BlogContext.Term
   End If
   If blogModule.BlogContext.Author IsNot Nothing Then
    Me.PropertySource("author") = blogModule.BlogContext.Author
   End If

  End Sub

  Public Sub New(blogModule As BlogModuleBase, blog As BlogInfo)
   MyBase.new(Scope.DefaultSettings)

   Me.PrimaryObject = blog
   Me.ModuleInfo = blogModule.ModuleConfiguration
   Me.UseObjectLessExpression = False
   Me.PropertySource("query") = blogModule.BlogContext
   Me.PropertySource("security") = blogModule.BlogContext.Security
   Me.PropertySource("urls") = blogModule.BlogContext.ModuleUrls
   Me.PropertySource("settings") = blogModule.Settings
   Me.PropertySource("viewsettings") = blogModule.ViewSettings
   Me.PropertySource("blog") = blog
   Me.PropertySource("owner") = New LazyLoadingUser(PortalSettings.PortalId, blog.OwnerUserId, blog.Username)
   If blogModule.BlogContext.Post IsNot Nothing Then
    Me.PropertySource("post") = blogModule.BlogContext.Post
    Me.PropertySource("author") = New LazyLoadingUser(PortalSettings.PortalId, blogModule.BlogContext.Post.CreatedByUserID, blogModule.BlogContext.Post.Username)
   ElseIf blogModule.BlogContext.Author IsNot Nothing Then
    Me.PropertySource("author") = blogModule.BlogContext.Author
   End If
   If blogModule.BlogContext.Term IsNot Nothing Then
    Me.PropertySource("selectedterm") = blogModule.BlogContext.Term
   End If

  End Sub

  Public Sub New(blogModule As BlogModuleBase, objBlogCalendar As BlogCalendarInfo)
   MyBase.new(Scope.DefaultSettings)

   Me.PrimaryObject = objBlogCalendar
   Me.ModuleInfo = blogModule.ModuleConfiguration
   Me.UseObjectLessExpression = False
   Me.PropertySource("query") = blogModule.BlogContext
   Me.PropertySource("security") = blogModule.BlogContext.Security
   Me.PropertySource("urls") = blogModule.BlogContext.ModuleUrls
   Me.PropertySource("settings") = blogModule.Settings
   Me.PropertySource("viewsettings") = blogModule.ViewSettings
   If blogModule.BlogContext.Blog IsNot Nothing Then
    Me.PropertySource("blog") = blogModule.BlogContext.Blog
   End If
   Me.PropertySource("calendar") = objBlogCalendar
   If blogModule.BlogContext.Post IsNot Nothing Then
    Me.PropertySource("post") = blogModule.BlogContext.Post
    Me.PropertySource("author") = New LazyLoadingUser(PortalSettings.PortalId, blogModule.BlogContext.Post.CreatedByUserID, blogModule.BlogContext.Post.Username)
   ElseIf blogModule.BlogContext.Author IsNot Nothing Then
    Me.PropertySource("author") = blogModule.BlogContext.Author
   End If
   If blogModule.BlogContext.Term IsNot Nothing Then
    Me.PropertySource("selectedterm") = blogModule.BlogContext.Term
   End If

  End Sub

  Public Sub New(blogModule As BlogModuleBase, objAuthor As LazyLoadingUser)
   MyBase.new(Scope.DefaultSettings)

   Me.PrimaryObject = objAuthor
   Me.ModuleInfo = blogModule.ModuleConfiguration
   Me.UseObjectLessExpression = False
   Me.PropertySource("query") = blogModule.BlogContext
   Me.PropertySource("security") = blogModule.BlogContext.Security
   Me.PropertySource("urls") = blogModule.BlogContext.ModuleUrls
   Me.PropertySource("settings") = blogModule.Settings
   Me.PropertySource("viewsettings") = blogModule.ViewSettings
   If blogModule.BlogContext.Blog IsNot Nothing Then
    Me.PropertySource("blog") = blogModule.BlogContext.Blog
   End If
   Me.PropertySource("author") = objAuthor
   If blogModule.BlogContext.Post IsNot Nothing Then
    Me.PropertySource("post") = blogModule.BlogContext.Post
   End If
   If blogModule.BlogContext.Term IsNot Nothing Then
    Me.PropertySource("selectedterm") = blogModule.BlogContext.Term
   End If

  End Sub

  Public Sub New(blogModule As BlogModuleBase, Post As PostInfo)
   MyBase.new(Scope.DefaultSettings)

   Me.PrimaryObject = Post
   Me.ModuleInfo = blogModule.ModuleConfiguration
   Me.UseObjectLessExpression = False
   Me.PropertySource("query") = blogModule.BlogContext
   Me.PropertySource("security") = blogModule.BlogContext.Security
   Me.PropertySource("urls") = blogModule.BlogContext.ModuleUrls
   Me.PropertySource("settings") = blogModule.Settings
   Me.PropertySource("viewsettings") = blogModule.ViewSettings
   Me.PropertySource("post") = Post
   Me.PropertySource("author") = New LazyLoadingUser(PortalSettings.PortalId, Post.CreatedByUserID, Post.Username)
   Me.PropertySource("blog") = Post.Blog
   Me.PropertySource("owner") = New LazyLoadingUser(PortalSettings.PortalId, Post.Blog.OwnerUserId, Post.Blog.Username)
   If blogModule.BlogContext.Term IsNot Nothing Then
    Me.PropertySource("selectedterm") = blogModule.BlogContext.Term
   End If

  End Sub

  Public Sub New(blogModule As BlogModuleBase, Post As PostInfo, term As TermInfo)
   MyBase.new(Scope.DefaultSettings)

   Me.PrimaryObject = term
   Me.ModuleInfo = blogModule.ModuleConfiguration
   Me.UseObjectLessExpression = False
   Me.PropertySource("query") = blogModule.BlogContext
   Me.PropertySource("security") = blogModule.BlogContext.Security
   Me.PropertySource("urls") = blogModule.BlogContext.ModuleUrls
   Me.PropertySource("settings") = blogModule.Settings
   Me.PropertySource("viewsettings") = blogModule.ViewSettings
   If Post IsNot Nothing Then
    Me.PropertySource("post") = Post
    Me.PropertySource("author") = New LazyLoadingUser(PortalSettings.PortalId, Post.CreatedByUserID, Post.Username)
    Me.PropertySource("blog") = Post.Blog
    Me.PropertySource("owner") = New LazyLoadingUser(PortalSettings.PortalId, Post.Blog.OwnerUserId, Post.Blog.Username)
   ElseIf blogModule.BlogContext.Blog IsNot Nothing Then
    Me.PropertySource("blog") = blogModule.BlogContext.Blog
    Me.PropertySource("owner") = New LazyLoadingUser(PortalSettings.PortalId, blogModule.BlogContext.Blog.OwnerUserId, blogModule.BlogContext.Blog.Username)
   End If
   Me.PropertySource("term") = term
   If blogModule.BlogContext.Term IsNot Nothing Then
    Me.PropertySource("selectedterm") = blogModule.BlogContext.Term
   End If

  End Sub

  Public Sub New(blogModule As BlogModuleBase, Post As PostInfo, comment As CommentInfo)
   MyBase.new(Scope.DefaultSettings)

   Me.PrimaryObject = comment
   Me.ModuleInfo = blogModule.ModuleConfiguration
   Me.UseObjectLessExpression = False
   Me.PropertySource("query") = blogModule.BlogContext
   Me.PropertySource("security") = blogModule.BlogContext.Security
   Me.PropertySource("urls") = blogModule.BlogContext.ModuleUrls
   Me.PropertySource("settings") = blogModule.Settings
   Me.PropertySource("viewsettings") = blogModule.ViewSettings
   If Post IsNot Nothing Then
    Me.PropertySource("post") = Post
    Me.PropertySource("author") = New LazyLoadingUser(PortalSettings.PortalId, Post.CreatedByUserID, Post.Username)
    Me.PropertySource("blog") = Post.Blog
    Me.PropertySource("owner") = New LazyLoadingUser(PortalSettings.PortalId, Post.Blog.OwnerUserId, Post.Blog.Username)
   ElseIf blogModule.BlogContext.Blog IsNot Nothing Then
    Me.PropertySource("blog") = blogModule.BlogContext.Blog
    Me.PropertySource("owner") = New LazyLoadingUser(PortalSettings.PortalId, blogModule.BlogContext.Blog.OwnerUserId, blogModule.BlogContext.Blog.Username)
   End If
   Me.PropertySource("comment") = comment
   Me.PropertySource("commenter") = New LazyLoadingUser(PortalSettings.PortalId, comment.CreatedByUserID, comment.Username)

  End Sub

 End Class
End Namespace
