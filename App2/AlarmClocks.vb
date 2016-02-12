Namespace AlarmClocks
    Public Class Clock
        Public Property StartTime As DateTime
        Public Property TimeInterval As TimeSpan
        Public Property TimeRemaining As TimeSpan

        Public Sub New(ByVal starttime As DateTime, ByVal timeinterval As TimeSpan)
            Me.StartTime = starttime
            Me.TimeInterval = timeinterval
            Me.TimeRemaining = timeinterval
        End Sub


    End Class

    Public Class BlackClock

    End Class
End Namespace