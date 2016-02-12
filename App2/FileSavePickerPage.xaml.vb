Imports Windows.Storage.Pickers.Provider

' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

''' <summary>
''' An empty page that can be used on its own or navigated to within a Frame.
''' </summary>
''' 


Public NotInheritable Class FileSavePickerPage
    'Inherits Global.SDKTemplate.Common.LayoutAwarePage
    Inherits Page
    Friend savepickerUI As FileSavePickerUI = Nothing
    'Public Const FEATURE_NAME As String = "File Save Picker Page"
    'Public Event ScenarioLoaded As System.EventHandler
    Public Event FileSavePickerPageResized As EventHandler(Of FileSavePickerPageSizeChangedEventArgs)

    Public LaunchArgs As Windows.ApplicationModel.Activation.LaunchActivatedEventArgs

    Public Shared Current As FileSavePickerPage

    Private HiddenFrame As Frame = Nothing

    Friend Sub Activate(ByVal args As FileSavePickerActivatedEventArgs)
        savepickerUI = args.FileSavePickerUI
        Window.Current.Content = Me
        Me.OnNavigatedTo(Nothing)
        Window.Current.Activate()

    End Sub
End Class


Public Class FileSavePickerPageSizeChangedEventArgs
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


