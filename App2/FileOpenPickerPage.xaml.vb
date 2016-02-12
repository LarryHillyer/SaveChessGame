Imports Windows.Storage.Pickers.Provider
' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
Public NotInheritable Class FileOpenPickerPage
    Inherits Page
    Public Const FEATURE_NAME As String = "File Open Picker Page"

    Public Event ScenarioLoaded As System.EventHandler
    Public Event FileOpenPickerPageResized As EventHandler(Of FileOpenPickerPageSizeChangedEventArgs)

    Public LaunchArgs As Windows.ApplicationModel.Activation.LaunchActivatedEventArgs

    Public Shared Current As FileOpenPickerPage

    Private HiddenFrame As Frame = Nothing

    Friend openPickerUI As FileOpenPickerUI = Nothing


    Public Sub Activate(ByVal args As FileOpenPickerActivatedEventArgs)
        ' cache FileOpenPickerUI
        openPickerUI = args.FileOpenPickerUI
        Window.Current.Content = Me
        Me.OnNavigatedTo(Nothing)
        Window.Current.Activate()
    End Sub

End Class

Public Class FileOpenPickerPageSizeChangedEventArgs
    Inherits EventArgs

    Private _width As Double

    Public Property Width() As Double
        Get
            Return _width
        End Get
        Set(ByVal value As Double)
            _width = value
        End Set
    End Property
End Class


