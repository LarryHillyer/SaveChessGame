Imports App2.Pieces1
Imports App2.Turn1
Namespace Players1
    Public Class Players

        Public Property WhitePlayer
        Public Property BlackPlayer

        Public Sub New(ByVal insTurn As Turn)
            If insTurn.PlayerNumber = Turn.Player.Player1 Then
                Me.WhitePlayer = Turn.Player.Player1
                Me.BlackPlayer = Turn.Player.Player2
            Else
                Me.WhitePlayer = Turn.Player.Player2
                Me.BlackPlayer = Turn.Player.Player1
            End If
        End Sub

    End Class

    Public Class Direction
        Public Property RowIncrement
        Public Property ColumnIncrement

        Public Enum Directions
            Forward
            Backward
            Right
            Left
            ForwardToRight
            ForwardToLeft
            BackwardToRight
            BackwardToLeft

            KnForwardToRight
            KnBackwardToRight
            KnForwardToLeft
            KnBackwardToLeft
            KnRightToForward
            KnRightToBackward
            KnLeftToForward
            KnLeftToBackward
        End Enum
        Public Sub New()

        End Sub

        Public Sub New(ByVal rowincrement As Integer, ByVal columnincrement As Integer)
            Me.RowIncrement = rowincrement
            Me.ColumnIncrement = columnincrement
        End Sub
    End Class

    Public Class Player1Directions

        Public Shared RookPlayer1 As New Generic.Dictionary(Of Direction.Directions, Direction)
        Public Shared KnightPlayer1 As New Generic.Dictionary(Of Direction.Directions, Direction)
        Public Shared BishopPlayer1 As New Generic.Dictionary(Of Direction.Directions, Direction)
        Public Shared QueenPlayer1 As New Generic.Dictionary(Of Direction.Directions, Direction)
        Public Shared KingPlayer1 As New Generic.Dictionary(Of Direction.Directions, Direction)
        Public Shared PawnPlayer1 As New Generic.Dictionary(Of Direction.Directions, Direction)

        Public Forward1 As New Direction(-1, 0)
        Public Backward1 As New Direction(1, 0)
        Public Right1 As New Direction(0, 1)
        Public Left1 As New Direction(0, -1)
        Public ForwardToRight1 As New Direction(-1, 1)
        Public BackwardToRight1 As New Direction(1, 1)
        Public ForwardToLeft1 As New Direction(-1, -1)
        Public BackwardToLeft1 As New Direction(1, -1)

        Public KnForwardToRight1 As New Direction(-2, 1)
        Public KnForwardToLeft1 As New Direction(-2, -1)
        Public KnBackwardToRight1 As New Direction(2, 1)
        Public KnBackwardToLeft1 As New Direction(2, -1)
        Public KnRightToForward1 As New Direction(-1, 2)
        Public KnRightToBackward1 As New Direction(1, 2)
        Public KnLeftToForward1 As New Direction(-1, -2)
        Public KnLeftToBackward1 As New Direction(1, -2)


        Public Sub New()

            RookPlayer1.Add(Direction.Directions.Forward, Forward1)
            RookPlayer1.Add(Direction.Directions.Backward, Backward1)
            RookPlayer1.Add(Direction.Directions.Right, Right1)
            RookPlayer1.Add(Direction.Directions.Left, Left1)

            KnightPlayer1.Add(Direction.Directions.KnForwardToRight, KnForwardToRight1)
            KnightPlayer1.Add(Direction.Directions.KnForwardToLeft, KnForwardToLeft1)
            KnightPlayer1.Add(Direction.Directions.KnBackwardToRight, KnBackwardToRight1)
            KnightPlayer1.Add(Direction.Directions.KnBackwardToLeft, KnBackwardToLeft1)
            KnightPlayer1.Add(Direction.Directions.KnRightToForward, KnRightToForward1)
            KnightPlayer1.Add(Direction.Directions.KnRightToBackward, KnRightToBackward1)
            KnightPlayer1.Add(Direction.Directions.KnLeftToForward, KnLeftToForward1)
            KnightPlayer1.Add(Direction.Directions.KnLeftToBackward, KnLeftToBackward1)

            BishopPlayer1.Add(Direction.Directions.ForwardToRight, ForwardToRight1)
            BishopPlayer1.Add(Direction.Directions.BackwardToRight, BackwardToRight1)
            BishopPlayer1.Add(Direction.Directions.ForwardToLeft, ForwardToLeft1)
            BishopPlayer1.Add(Direction.Directions.BackwardToLeft, BackwardToLeft1)

            QueenPlayer1.Add(Direction.Directions.Forward, Forward1)
            QueenPlayer1.Add(Direction.Directions.Backward, Backward1)
            QueenPlayer1.Add(Direction.Directions.Right, Right1)
            QueenPlayer1.Add(Direction.Directions.Left, Left1)
            QueenPlayer1.Add(Direction.Directions.ForwardToRight, ForwardToRight1)
            QueenPlayer1.Add(Direction.Directions.ForwardToLeft, ForwardToLeft1)
            QueenPlayer1.Add(Direction.Directions.BackwardToRight, BackwardToRight1)
            QueenPlayer1.Add(Direction.Directions.BackwardToLeft, BackwardToLeft1)

            KingPlayer1.Add(Direction.Directions.Forward, Forward1)
            KingPlayer1.Add(Direction.Directions.Backward, Backward1)
            KingPlayer1.Add(Direction.Directions.Right, Right1)
            KingPlayer1.Add(Direction.Directions.Left, Left1)
            KingPlayer1.Add(Direction.Directions.ForwardToRight, ForwardToRight1)
            KingPlayer1.Add(Direction.Directions.ForwardToLeft, ForwardToLeft1)
            KingPlayer1.Add(Direction.Directions.BackwardToRight, BackwardToRight1)
            KingPlayer1.Add(Direction.Directions.BackwardToLeft, BackwardToLeft1)

            PawnPlayer1.Add(Direction.Directions.Forward, Forward1)


        End Sub

    End Class

    Public Class Player2Directions
        Public Shared RookPlayer2 As New Generic.Dictionary(Of Direction.Directions, Direction)
        Public Shared KnightPlayer2 As New Generic.Dictionary(Of Direction.Directions, Direction)
        Public Shared BishopPlayer2 As New Generic.Dictionary(Of Direction.Directions, Direction)
        Public Shared QueenPlayer2 As New Generic.Dictionary(Of Direction.Directions, Direction)
        Public Shared KingPlayer2 As New Generic.Dictionary(Of Direction.Directions, Direction)
        Public Shared PawnPlayer2 As New Generic.Dictionary(Of Direction.Directions, Direction)

        Public Forward2 As New Direction(1, 0)
        Public Backward2 As New Direction(-1, 0)
        Public Right2 As New Direction(0, -1)
        Public Left2 As New Direction(0, 1)
        Public ForwardToRight2 As New Direction(1, -1)
        Public BackwardToRight2 As New Direction(-1, -1)
        Public ForwardToLeft2 As New Direction(1, 1)
        Public BackwardToLeft2 As New Direction(-1, 1)

        Public KnForwardToRight2 As New Direction(2, -1)
        Public KnForwardToLeft2 As New Direction(2, 1)
        Public KnBackwardToRight2 As New Direction(-2, -1)
        Public KnBackwardToLeft2 As New Direction(-2, 1)
        Public KnRightToForward2 As New Direction(1, -2)
        Public KnRightToBackward2 As New Direction(-1, -2)
        Public KnLeftToForward2 As New Direction(1, 2)
        Public KnLeftToBackward2 As New Direction(-1, 2)

        Public Sub New()

            RookPlayer2.Add(Direction.Directions.Forward, Forward2)
            RookPlayer2.Add(Direction.Directions.Backward, Backward2)
            RookPlayer2.Add(Direction.Directions.Right, Right2)
            RookPlayer2.Add(Direction.Directions.Left, Left2)

            KnightPlayer2.Add(Direction.Directions.KnForwardToRight, KnForwardToRight2)
            KnightPlayer2.Add(Direction.Directions.KnForwardToLeft, KnForwardToLeft2)
            KnightPlayer2.Add(Direction.Directions.KnBackwardToRight, KnBackwardToRight2)
            KnightPlayer2.Add(Direction.Directions.KnBackwardToLeft, KnBackwardToLeft2)
            KnightPlayer2.Add(Direction.Directions.KnRightToForward, KnRightToForward2)
            KnightPlayer2.Add(Direction.Directions.KnRightToBackward, KnRightToBackward2)
            KnightPlayer2.Add(Direction.Directions.KnLeftToForward, KnLeftToForward2)
            KnightPlayer2.Add(Direction.Directions.KnLeftToBackward, KnLeftToBackward2)

            BishopPlayer2.Add(Direction.Directions.ForwardToRight, ForwardToRight2)
            BishopPlayer2.Add(Direction.Directions.BackwardToRight, BackwardToRight2)
            BishopPlayer2.Add(Direction.Directions.ForwardToLeft, ForwardToLeft2)
            BishopPlayer2.Add(Direction.Directions.BackwardToLeft, BackwardToLeft2)

            QueenPlayer2.Add(Direction.Directions.Forward, Forward2)
            QueenPlayer2.Add(Direction.Directions.Backward, Backward2)
            QueenPlayer2.Add(Direction.Directions.Right, Right2)
            QueenPlayer2.Add(Direction.Directions.Left, Left2)
            QueenPlayer2.Add(Direction.Directions.ForwardToRight, ForwardToRight2)
            QueenPlayer2.Add(Direction.Directions.ForwardToLeft, ForwardToLeft2)
            QueenPlayer2.Add(Direction.Directions.BackwardToRight, BackwardToRight2)
            QueenPlayer2.Add(Direction.Directions.BackwardToLeft, BackwardToLeft2)

            KingPlayer2.Add(Direction.Directions.Forward, Forward2)
            KingPlayer2.Add(Direction.Directions.Backward, Backward2)
            KingPlayer2.Add(Direction.Directions.Right, Right2)
            KingPlayer2.Add(Direction.Directions.Left, Left2)
            KingPlayer2.Add(Direction.Directions.ForwardToRight, ForwardToRight2)
            KingPlayer2.Add(Direction.Directions.ForwardToLeft, ForwardToLeft2)
            KingPlayer2.Add(Direction.Directions.BackwardToRight, BackwardToRight2)
            KingPlayer2.Add(Direction.Directions.BackwardToLeft, BackwardToLeft2)

            PawnPlayer2.Add(Direction.Directions.Forward, Forward2)


        End Sub
    End Class

End Namespace