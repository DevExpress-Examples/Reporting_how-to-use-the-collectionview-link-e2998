﻿Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports DevExpress.Xpf.Printing
' ...

Namespace UseCollectionViewLinkSL
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()
		End Sub

		#Region "#CollectionViewLink"
		Private Sub Page_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			' Create a link and bind it to the PrintPreview instance.
			Dim link As New CollectionViewLink()
			preview.Model = New LinkPreviewModel(link)

			' Create an ICollectionView object.
			link.CollectionView = CreateMonthCollectionView()
			link.ExportServiceUri = "ExportService1.svc"

			' Provide export templates.
			link.Detail = CType(Resources("monthNameTemplate"), DataTemplate)
			link.GroupInfos.Add(New GroupInfo(CType(Resources("monthQuarterTemplate"), DataTemplate)))

			' Create a document.
			link.CreateDocument(True)
		End Sub

		Private Function CreateMonthCollectionView() As ICollectionView
			Const monthCount As Integer = 12
			Dim monthNames() As String = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames

			Dim data(Math.Min(monthNames.Length, monthCount) - 1) As MonthItem
			For i As Integer = 0 To data.Length - 1
				data(i) = New MonthItem(monthNames(i), (i \ 3) + 1)
			Next i

			Dim source As New CollectionViewSource()
			source.Source = data
			source.GroupDescriptions.Add(New PropertyGroupDescription("Quarter"))

			Return source.View
		End Function

		Public Class MonthItem
			Private privateName As String
			Public Property Name() As String
				Get
					Return privateName
				End Get
				Private Set(ByVal value As String)
					privateName = value
				End Set
			End Property
			Private privateQuarter As Integer
			Public Property Quarter() As Integer
				Get
					Return privateQuarter
				End Get
				Private Set(ByVal value As Integer)
					privateQuarter = value
				End Set
			End Property

			Public Sub New(ByVal name As String, ByVal quarter As Integer)
				Name = name
				Quarter = quarter
			End Sub
		End Class
		#End Region ' #CollectionViewLink

	End Class
End Namespace
