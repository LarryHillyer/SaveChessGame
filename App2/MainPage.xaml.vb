' The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238


Imports App2.Board1
Imports App2.Board1.BoardLocation1
Imports App2.Board1.BoardStatus1
Imports App2.Board1.BoardImage1
Imports App2.Board1.TransposeBoard1
Imports App2.Board1.BoardLocator1

Imports App2.Turn1
Imports App2.Turn1.Turn
Imports App2.Pieces1
Imports App2.Pieces1.AnyPiece
Imports App2.Pieces1.PieceCollection1
Imports App2.Pieces1.PieceCollection2
Imports App2.Pieces1.OldPieceCollection1
Imports App2.Pieces1.WhiteCapturedPieces1
Imports App2.Pieces1.BlackCapturedPieces1
Imports App2.Pieces1.CapturedPieces1
Imports App2.Pieces1.KingPossibleMoves
Imports App2.Pieces1.PawnPossibleMoves
Imports App2.Pieces1.RookPossibleMoves
Imports App2.Pieces1.BishopPossibleMoves
Imports App2.Pieces1.QueenPossibleMoves
Imports App2.Pieces1.KnightPossibleMoves
Imports App2.Move1
Imports App2.Move1.WhiteMove2
Imports App2.Move1.BlackMove2
Imports App2.Move1.PieceSelectedPossibleMoves1
Imports App2.Players1
Imports App2.Players1.Direction
Imports App2.Players1.Player1Directions
Imports App2.Players1.Player2Directions


Imports System.Threading.Timer

Imports System.Xml
Imports System.Text
Imports System.IO
Imports System
Imports System.String
Imports Windows.Storage
Imports Windows.Storage.Pickers
Imports Windows.Storage.Pickers.FileOpenPicker
Imports Windows.Storage.Pickers.FileSavePicker
Imports Windows.Storage.Pickers.FolderPicker
Imports Windows.Storage.Pickers.Provider
Imports System.Math
Imports SDKTemplate

Public NotInheritable Class MainPage
    Inherits Page

    Public sendername As String

    Public myName As String = "MyName"
    Public myOpponentsName As String = "MyOpponent"

    Public gameName As String
    Public gameStamp As DateTime
    Public gameStampString As String

    Public gameTime As TimeSpan
    Public gameTimeHours As Int16 = 0
    Public gameTimeMinutes As Int16 = 30

    Public playernumber As Player

    Public direction As New Direction
    Public player1Directions As New Player1Directions
    Public player2Directions As New Player2Directions

    Public Shared insTurn As Turn
    Public Shared insPlayers As Players

    Public insWhiteMove As PossibleMove
    Public insBlackMove As PossibleMove

    Public insReplayWhiteMove As PossibleMove
    Public insReplayBlackMove As PossibleMove


    Public Shared CheckMate As Boolean = False

    Public Shared KingIsChecked As Boolean = False
    Public Shared PieceIsCaptured As Boolean = False

    Public Shared PawnCapturedByEnPassant As Boolean = False

    Public Shared WhiteKingIsProtected As Boolean = True
    Public Shared BlackKingIsProtected As Boolean = True

    Public Shared WhiteKingIsChecked As Boolean = False
    Public Shared BlackKingIsChecked As Boolean = False

    Public Shared WhiteKingWasChecked As Boolean = False
    Public Shared BlackKingWasChecked As Boolean = False

    Public Shared CanWhiteKingCastleToQueenSide As Boolean = False
    Public Shared CanWhiteKingCastleToKingSide As Boolean = False

    Public Shared CanBlackKingCastleToQueenSide As Boolean = False
    Public Shared CanBlackKingCastleToKingSide As Boolean = False

    Public Shared CanCastledWhiteRookCheckBlackKing As Boolean = False
    Public Shared CanCastledBlackRookCheckWhiteKing As Boolean = False

    Public Shared WhiteKingIsCastling As Boolean = False
    Public Shared WhiteKingWasCastling As Boolean = False

    Public Shared BlackKingIsCastling As Boolean = False
    Public Shared BlackKingWasCastling As Boolean = False

    Public WhiteKingCheckedImageOn As Boolean = True
    Public BlackKingCheckedImageOn As Boolean = True

    Public NewGameImageOn As Boolean = True
    Public StartClockImageOn As Boolean = True

    Public Shared rownumber As Integer
    Public Shared rowdifference As Integer

    Public Shared columnnumber As Integer
    Public Shared columndifference As Integer

    Public Shared number As Integer
    Public Shared numberMax As Integer = 8
    Public Shared numberMin As Integer = 1

    Public insPawnPossibleMoves As PawnPossibleMoves
    Public insRookPossibleMoves As RookPossibleMoves
    Public insKnightPossibleMoves As KnightPossibleMoves
    Public insBishopPossibleMoves As BishopPossibleMoves
    Public insQueenPossibleMoves As QueenPossibleMoves
    Public insKingPossibleMoves As KingPossibleMoves

    Public Shared insPossibleMove As PossibleMove
    Public Shared insCastledRookMove As PossibleMove
    Public Shared moveLimit As Boolean = False

    Public Shared whiteMoveNumber As Integer = 1
    Public Shared blackMoveNumber As Integer = 1

    Public Shared whiteMoveString As New ObservableCollection(Of String)
    Public Shared blackMoveString As New ObservableCollection(Of String)

    Public pieceSelected As AnyPiece
    Public pieceSelectedOld As AnyPiece

    Public pieceSelected2 As AnyPiece
    Public pieceSelected2Old As AnyPiece

    Public locationpiece2 As AnyPiece

    Public promotedWhitePawn1 As AnyPiece
    Public promotedWhitePawn2 As AnyPiece
    Public promotedWhitePawn3 As AnyPiece
    Public promotedWhitePawn4 As AnyPiece
    Public promotedWhitePawn5 As AnyPiece
    Public promotedWhitePawn6 As AnyPiece
    Public promotedWhitePawn7 As AnyPiece
    Public promotedWhitePawn8 As AnyPiece

    Public promotedBlackPawn1 As AnyPiece
    Public promotedBlackPawn2 As AnyPiece
    Public promotedBlackPawn3 As AnyPiece
    Public promotedBlackPawn4 As AnyPiece
    Public promotedBlackPawn5 As AnyPiece
    Public promotedBlackPawn6 As AnyPiece
    Public promotedBlackPawn7 As AnyPiece
    Public promotedBlackPawn8 As AnyPiece

    Public promotedWhitePawnCount As Integer
    Public promotedBlackPawnCount As Integer

    Public Shared whitePieceCheckingBlackKing As AnyPiece
    Public Shared blackPieceCheckingWhiteKing As AnyPiece

    Public Shared CapturedBlackPiece As AnyPiece
    Public Shared CapturedWhitePiece As AnyPiece
    Public Shared insCapturedPiece As AnyPiece

    Public Shared PossiblePiecesCheckingWhiteKing As New Generic.Dictionary(Of String, AnyPiece)
    Public Shared PossiblePiecesCheckingBlackKing As New Generic.Dictionary(Of String, AnyPiece)

    Public Shared OpenSpacesInBetweenKingAndPieceCheckingKing As New Generic.Dictionary(Of String, String)
    Public Shared AllOpenSpacesInBetweenKingAndPiecesCheckingKing As New Generic.Dictionary(Of String, String)

    Public Shared PiecesCheckingWhiteKing As New Generic.Dictionary(Of String, AnyPiece)
    Public Shared PiecesCheckingBlackKing As New Generic.Dictionary(Of String, AnyPiece)

    Public Shared PossibleCapturedWhitePieces As New Generic.Dictionary(Of String, AnyPiece)
    Public Shared PossibleCapturedBlackPieces As New Generic.Dictionary(Of String, AnyPiece)

    Public Shared CapturedWhitePieces As New Generic.Dictionary(Of String, AnyPiece)
    Public Shared CapturedBlackPieces As New Generic.Dictionary(Of String, AnyPiece)

    Public isPieceSelected As Boolean = False
    Public isLocationToMoveSelected As Boolean = False
    Public isPieceCaptured As Boolean = False
    Public PieceSelectedCanMove As Boolean = False

    Public insPieceCollection As New PieceCollection1
    Public insPieceCollection3 As New PieceCollection2
    Public insOldPieceCollection As New OldPieceCollection1

    Public insBoardStatus As New BoardStatus1
    Public insBoardLocation As New BoardLocation1
    Public insBoardLocator As New BoardLocator1
    Public insTransposeBoard As New TransposeBoard1
    Public insBoardImage As New BoardImage1

    Public insImage As Image

    Public insWhiteCapturedPiece As CapturedPiece
    Public insBlackCapturedPiece As CapturedPiece

    Public CapturedPieceOld As CapturedPiece

    Private Shared WithEvents timerBlinkSelectedPiece As New DispatcherTimer

    Private Shared WithEvents timerWhiteKingChecked As New DispatcherTimer
    Private Shared WithEvents timerBlackKingChecked As New DispatcherTimer

    Private Shared WithEvents timerPlayer1Clock As New DispatcherTimer
    Private Shared WithEvents timerPlayer2Clock As New DispatcherTimer

    Private Shared WithEvents timerNewGame As New DispatcherTimer
    Private Shared WithEvents timerStartClock As New DispatcherTimer

    Private rotateBoard As New RotateTransform

    Private rotateControl0 As New RotateTransform
    Private rotateControl180 As New RotateTransform

    Private transposePlayer1ReplayGameLabel As New TranslateTransform
    Private transposePlayer2ReplayGameLabel As New TranslateTransform

    Private Player1ReplayGameLabel As New TransformGroup
    Private Player2ReplayGameLabel As New TransformGroup

    Private transposePlayer1Replay_Game As New TranslateTransform
    Private transposePlayer2Replay_Game As New TranslateTransform

    Private Player1Replay_Game As New TransformGroup
    Private Player2Replay_Game As New TransformGroup

    Private transposePlayer1Save_Game As New TranslateTransform
    Private transposePlayer2Save_Game As New TranslateTransform

    Private Player1Save_Game As New TransformGroup
    Private Player2Save_Game As New TransformGroup

    Private transposePlayer1Replay_Forward As New TranslateTransform
    Private transposePlayer2Replay_Forward As New TranslateTransform

    Private Player1Replay_Forward As New TransformGroup
    Private Player2Replay_Forward As New TransformGroup

    Private transposePlayer1Replay_Reverse As New TranslateTransform
    Private transposePlayer2Replay_Reverse As New TranslateTransform

    Private Player1Replay_Reverse As New TransformGroup
    Private Player2Replay_Reverse As New TransformGroup


    Private transposePlayer1MoveNumberLabel As New TranslateTransform
    Private transposePlayer2MoveNumberLabel As New TranslateTransform

    Private Player1MoveNumberLabel As New TransformGroup
    Private Player2MoveNumberLabel As New TransformGroup

    Private transposePlayer1MoveNumberBox As New TranslateTransform
    Private transposePlayer2MoveNumberBox As New TranslateTransform

    Private Player1MoveNumberBox As New TransformGroup
    Private Player2MoveNumberBox As New TransformGroup

    Private transposePlayer1TxtCapturedPieces As New TranslateTransform
    Private transposePlayer2TxtCapturedPieces As New TranslateTransform

    Private Player1TxtCapturedPieces As New TransformGroup
    Private Player2TxtCapturedPieces As New TransformGroup

    Private transposePlayer1ChangeTurn As New TranslateTransform
    Private transposePlayer2ChangeTurn As New TranslateTransform

    Private Player1ChangeTurn As New TransformGroup
    Private Player2ChangeTurn As New TransformGroup

    Private transposePlayer1NewGame As New TranslateTransform
    Private transposePlayer2NewGame As New TranslateTransform

    Private Player1NewGame As New TransformGroup
    Private Player2NewGame As New TransformGroup

    Private transposePlayer1RestartGame As New TranslateTransform
    Private transposePlayer2RestartGame As New TranslateTransform

    Private Player1RestartGame As New TransformGroup
    Private Player2RestartGame As New TransformGroup


    Private transposePlayer1SelectColor As New TranslateTransform
    Private transposePlayer2SelectColor As New TranslateTransform

    Private Player1SelectColor As New TransformGroup
    Private Player2SelectColor As New TransformGroup


    Private transposePlayer1DisplayWhiteMoves As New TranslateTransform
    Private transposePlayer2DisplayWhiteMoves As New TranslateTransform

    Private Player1DisplayWhiteMoves As New TransformGroup
    Private Player2DisplayWhiteMoves As New TransformGroup


    Private transposePlayer1DisplayBlackMoves As New TranslateTransform
    Private transposePlayer2DisplayBlackMoves As New TranslateTransform

    Private Player1DisplayBlackMoves As New TransformGroup
    Private Player2DisplayBlackMoves As New TransformGroup


    Private transposePlayer1DisplayWhiteTurn As New TranslateTransform
    Private transposePlayer2DisplayWhiteTurn As New TranslateTransform

    Private Player1DisplayWhiteTurn As New TransformGroup
    Private Player2DisplayWhiteTurn As New TransformGroup


    Private transposePlayer1DisplayBlackTurn As New TranslateTransform
    Private transposePlayer2DisplayBlackTurn As New TranslateTransform

    Private Player1DisplayBlackTurn As New TransformGroup
    Private Player2DisplayBlackTurn As New TransformGroup


    Private transposePlayer1CapturedByMe As New TranslateTransform
    Private transposePlayer2CapturedByMe As New TranslateTransform

    Private Player1CapturedByMe As New TransformGroup
    Private Player2CapturedByMe As New TransformGroup


    Private transposePlayer1CapturedByOpponent As New TranslateTransform
    Private transposePlayer2CapturedByOpponent As New TranslateTransform

    Private Player1CapturedByOpponent As New TransformGroup
    Private Player2CapturedByOpponent As New TransformGroup


    Private ClockTime As DateTime

    Public Player1Clock As AlarmClocks.Clock
    Public Player2Clock As AlarmClocks.Clock

    Public isGameReset As Boolean = False

    Public ImageOn As Boolean = True

    Public startNewGame As Boolean = False
    Public restartGameHasClicked As Boolean = False
    Public startClock As Boolean = True
    Public GameHasBeenSaved As Boolean = True

    Public whiteMoveXML As XDocument
    Public blackMoveXML As XDocument
    Public MoveXML As XDocument
    Public ReplayMoveXML As XDocument

    Private blackReplayCount As Integer
    Private whiteReplayCount As Integer
    Private replayCount As Integer
    Private replayMoveCount As Integer = 1

    Private IsReplayForward As Boolean = True
    Private IsReplayReverse As Boolean = False

    Public CreateAppDataFile As Boolean = True

    Public localSettings As ApplicationDataContainer = ApplicationData.Current.LocalSettings
    Public localFolder As StorageFolder = ApplicationData.Current.LocalFolder

    Public StoragePath As String = "C:\Users\Larry\Documents\ChessGames\"
    Public nameOfFile As String

    Public openPicker As New FileOpenPicker
    Public savePicker As New FileSavePicker

    Public savePickerUI As FileSavePickerUI
    Public openPickerUI As FileOpenPickerUI

    Public replayFile As IStorageFile
    Public replayGameIsEnabled As Boolean = False

    Public argsSavePickerUI As FileSavePickerActivatedEventArgs
    Public argsOpenPickerUI As FileOpenPickerActivatedEventArgs

    Public alertPlayer As MediaElement

    Private Sub startGame_Click(sender As Object, e As RoutedEventArgs) Handles startGame.Click

        replayGameIsEnabled = False
        NewGame.Flyout.Hide()
        Save_Game.Visibility = Windows.UI.Xaml.Visibility.Visible
        StartGameSub()
    End Sub

    Private Sub StartGameSub()

        If startNewGame = True Then
            Exit Sub
        End If

        gameStampString = DateTime.Now.ToString

        If replayGameIsEnabled = True Then

            Dim queryReplayGameOptions = From GameDetails In ReplayMoveXML...<Moves>...<GameDetails>
            NamePlayer1.Text = queryReplayGameOptions.Elements("MyName").Value
            NamePlayer2.Text = queryReplayGameOptions.Elements("MyOpponentsName").Value

            If queryReplayGameOptions.Elements("WhitePlayer").Value = "0" Then
                insPlayers.WhitePlayer = Player.Player1
                selectWhitePlayer.IsOn = False
            ElseIf queryReplayGameOptions.Elements("WhitePlayer").Value = "1" Then
                insPlayers.WhitePlayer = Player.Player2
                selectWhitePlayer.IsOn = True
            End If

            changeTurnOnMoveLocationSelection.IsOn = queryReplayGameOptions.Elements("ChangeTurnOnMoveLocationSelectionIsOn").Value
            Use_Clock.IsOn = queryReplayGameOptions.Elements("UseClockIsOn").Value
        End If

        myName = NamePlayer1.Text
        myOpponentsName = NamePlayer2.Text

        gameTimeHours = CInt(GameClockHours.Text)
        gameTimeMinutes = CInt(GameClockMinutes.Text)

        gameTime = New TimeSpan(gameTimeHours, gameTimeMinutes, 0)
        gameName = myName + "Vs" + myOpponentsName '+ " " + gameStamp


        If isGameReset = True Then

            If timerPlayer1Clock.IsEnabled = True Then
                timerPlayer1Clock.Stop()
            End If
            If timerPlayer2Clock.IsEnabled = True Then
                timerPlayer2Clock.Stop()
            End If
        End If

        isGameReset = True
        boardRotation.IsOn = False

        If selectWhitePlayer.IsOn = False Then
            playernumber = Player.Player1
            boardRotation.IsOn = False
        Else
            playernumber = Player.Player2
            boardRotation.IsOn = True
        End If

        insTurn = New Turn(playernumber)
        insPlayers = New Players(insTurn)

        isPieceSelected = False
        isLocationToMoveSelected = False

        PieceCollection.Clear()
        PieceCollection3.Clear()
        PossiblePieceCollection.Clear()
        OldPieceCollection.Clear()
        BoardStatus.Clear()
        BoardImage.Clear()
        ImagesOfCapturedPieces.Clear()
        WhiteMove.Clear()
        BlackMove.Clear()
        WhiteCapturedPieces.Clear()
        BlackCapturedPieces.Clear()
        CapturedPieces.Clear()

        pieceSelected = New AnyPiece
        pieceSelectedOld = New AnyPiece


        insPieceCollection = New PieceCollection1(insTurn.PlayerNumber)
        insPieceCollection3 = New PieceCollection2(insTurn.PlayerNumber)
        insOldPieceCollection = New OldPieceCollection1

        insBoardStatus = New BoardStatus1

        whiteMoveNumber = 1
        blackMoveNumber = 1

        MoveNumberBox.DataContext = blackMoveNumber

        whiteMoveString = New ObservableCollection(Of String)
        blackMoveString = New ObservableCollection(Of String)

        displayWhiteMoves.ItemsSource = whiteMoveString
        displayBlackMoves.ItemsSource = blackMoveString

        insWhiteMove = New PossibleMove(whiteMoveNumber)
        insBlackMove = New PossibleMove(blackMoveNumber)

        ImageOn = True

        NewGame.IsEnabled = False
        boardRotation.IsEnabled = True
        changeTurn.IsEnabled = True

        NamePlayer1.DataContext = myName
        NamePlayer2.DataContext = myOpponentsName

        myName = NamePlayer1.Text
        myOpponentsName = NamePlayer2.Text

        txtMyName.DataContext = myName
        txtOpponentName.DataContext = myOpponentsName

        displayWhiteTurn.DataContext = "White's Turn"
        displayBlackTurn.DataContext = ""

        ClockTime = DateTime.Now

        timerBlinkSelectedPiece = New DispatcherTimer
        timerBlinkSelectedPiece.Interval = New TimeSpan(5000000)
        AddHandler timerBlinkSelectedPiece.Tick, AddressOf timerBlinkSelectedPiece_tick


        timerWhiteKingChecked = New DispatcherTimer
        timerWhiteKingChecked.Interval = New TimeSpan(2500000)
        AddHandler timerWhiteKingChecked.Tick, AddressOf timerWhiteKingChecked_tick

        timerBlackKingChecked = New DispatcherTimer
        timerBlackKingChecked.Interval = New TimeSpan(2500000)
        AddHandler timerBlackKingChecked.Tick, AddressOf timerBlackKingChecked_tick

        CalculatePossibleMoves()

        If insTurn.PlayerNumber = Player.Player1 Then
            rotateBoard.Angle = 0
            changeTurn.RenderTransform = Player1ChangeTurn
            displayWhiteTurn.RenderTransform = Player1DisplayWhiteTurn
            displayWhiteMoves.RenderTransform = Player1DisplayWhiteMoves
            displayBlackTurn.RenderTransform = Player1DisplayBlackTurn
            displayBlackMoves.RenderTransform = Player1DisplayBlackMoves
            boardRotation.RenderTransform = Player1SelectColor
            NewGame.RenderTransform = Player1NewGame
            Restart_Game.RenderTransform = Player1RestartGame
            CapturedByMe.RenderTransform = Player1CapturedByMe
            CapturedByOpponent.RenderTransform = Player1CapturedByOpponent
            txtCapturedPieces.RenderTransform = Player1TxtCapturedPieces
            MoveNumberLabel.RenderTransform = Player1MoveNumberLabel
            MoveNumberBox.RenderTransform = Player1MoveNumberBox
            Replay_Game.RenderTransform = Player1Replay_Game
            Save_Game.RenderTransform = Player1Save_Game
            ReplayGameLabel.RenderTransform = Player1ReplayGameLabel
            Replay_Forward.RenderTransform = Player1Replay_Forward
            Replay_Reverse.RenderTransform = Player1Replay_Reverse
        ElseIf insTurn.PlayerNumber = Player.Player2 Then
            rotateBoard.Angle = 180
            changeTurn.RenderTransform = Player2ChangeTurn
            displayWhiteTurn.RenderTransform = Player2DisplayWhiteTurn
            displayWhiteMoves.RenderTransform = Player2DisplayWhiteMoves
            displayBlackTurn.RenderTransform = Player2DisplayBlackTurn
            displayBlackMoves.RenderTransform = Player2DisplayBlackMoves
            boardRotation.RenderTransform = Player2SelectColor
            NewGame.RenderTransform = Player2NewGame
            Restart_Game.RenderTransform = Player2RestartGame
            CapturedByMe.RenderTransform = Player2CapturedByMe
            CapturedByOpponent.RenderTransform = Player2CapturedByOpponent
            txtCapturedPieces.RenderTransform = Player2TxtCapturedPieces
            MoveNumberLabel.RenderTransform = Player2MoveNumberLabel
            MoveNumberBox.RenderTransform = Player2MoveNumberBox
            Replay_Game.RenderTransform = Player2Replay_Game
            Save_Game.RenderTransform = Player2Save_Game
            ReplayGameLabel.RenderTransform = Player2ReplayGameLabel
            Replay_Forward.RenderTransform = Player2Replay_Forward
            Replay_Reverse.RenderTransform = Player2Replay_Reverse

        End If

        Player1ClockDisplay.RenderTransform = rotateBoard
        Player2ClockDisplay.RenderTransform = rotateBoard
        Player1Check.RenderTransform = rotateBoard
        Player2Check.RenderTransform = rotateBoard
        txtMyName.RenderTransform = rotateBoard
        txtOpponentName.RenderTransform = rotateBoard
        'txtCapturedPieces.RenderTransform = rotateBoard

        AddBoardImages()

        For Each image1 In BoardImage
            BoardImage(image1.Key).RenderTransform = rotateBoard
            BoardImage(image1.Key).Source = BoardLocation(image1.Key).Image
        Next

        For Each piece1 In PieceCollection
            BoardImage(piece1.Key).Source = PieceCollection(piece1.Key).Image
        Next

        For Each square In ImagesOfCapturedPieces
            ImagesOfCapturedPieces(square.Key).RenderTransform = rotateBoard
            ImagesOfCapturedPieces(square.Key).Source = CaptureBoardLocation(square.Key).Image
        Next

        NewGame.DataContext = "New Game"

        If Use_Clock.IsOn = True Then

            startClock = True

            timerStartClock = New DispatcherTimer
            timerStartClock.Interval = New TimeSpan(5000000)
            AddHandler timerStartClock.Tick, AddressOf timerStartClock_tick

            timerPlayer1Clock = New DispatcherTimer
            timerPlayer1Clock.Interval = New TimeSpan(0, 0, 1)
            AddHandler timerPlayer1Clock.Tick, AddressOf timerPlayer1Clock_tick

            timerPlayer2Clock = New DispatcherTimer
            timerPlayer2Clock.Interval = New TimeSpan(0, 0, 1)
            AddHandler timerPlayer2Clock.Tick, AddressOf timerPlayer2Clock_tick

            Player1Clock = New AlarmClocks.Clock(ClockTime, gameTime)
            Player1ClockDisplay.DataContext = Player1Clock.TimeRemaining

            Player2Clock = New AlarmClocks.Clock(ClockTime, gameTime)
            Player2ClockDisplay.DataContext = Player2Clock.TimeRemaining

            changeTurn.IsEnabled = True
            changeTurn.Visibility = Windows.UI.Xaml.Visibility.Visible
            changeTurn.DataContext = "Start Clock"
            timerStartClock.Start()

            Player1ClockDisplay.Visibility = Windows.UI.Xaml.Visibility.Visible
            Player2ClockDisplay.Visibility = Windows.UI.Xaml.Visibility.Visible

        Else
            startClock = False
            changeTurn.IsEnabled = False
            changeTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            changeTurn.DataContext = ""
            Player1ClockDisplay.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            Player2ClockDisplay.Visibility = Windows.UI.Xaml.Visibility.Collapsed

        End If

        If replayGameIsEnabled = True Then
            startClock = True
            ChangeTurnSub()
        End If

        MoveXML = New XDocument
        MoveXML = XDocument.Load("Assets\MoveXML.xml")

        Dim xeMyName As New XElement("MyName", myName)
        Dim xeMyOpponentsName As New XElement("MyOpponentsName", myOpponentsName)

        If insPlayers.WhitePlayer = Player.Player1 Then
            Dim xeWhitePlayer As New XElement("WhitePlayer", CStr(Player.Player1))
            MoveXML.Element("Moves").Element("GameDetails").Add(xeWhitePlayer)
        Else
            Dim xeWhitePlayer As New XElement("WhitePlayer", CStr(Player.Player2))
            MoveXML.Element("Moves").Element("GameDetails").Add(xeWhitePlayer)
        End If

        Dim xeChangeTurnOnMoveLocationSelectionIsOn As New XElement("ChangeTurnOnMoveLocationSelectionIsOn", changeTurnOnMoveLocationSelection.IsOn)
        Dim xeUseClockIsOn As New XElement("UseClockIsOn", Use_Clock.IsOn)



        MoveXML.Element("Moves").Element("GameDetails").Add(xeMyName)
        MoveXML.Element("Moves").Element("GameDetails").Add(xeMyOpponentsName)
        MoveXML.Element("Moves").Element("GameDetails").Add(xeChangeTurnOnMoveLocationSelectionIsOn)
        MoveXML.Element("Moves").Element("GameDetails").Add(xeUseClockIsOn)

        startNewGame = True
    End Sub

    Private Sub restartGame_Click(sender As Object, e As RoutedEventArgs) Handles Restart_Game.Click 'restartGame.Click,

        If restartGameHasClicked = True Then
            restartGameHasClicked = False
            Exit Sub
        End If

        If changeTurnOnMoveLocationSelection.IsOn = False Then
            changeTurn.Visibility = Windows.UI.Xaml.Visibility.Visible
        End If


        Restart_Game.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        ReplayGameLabel.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Replay_Forward.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Replay_Reverse.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Save_Game.Visibility = Windows.UI.Xaml.Visibility.Visible


        'restartGame.IsEnabled = False
        replayGameIsEnabled = False

        'Restart_Game.Flyout.Hide()

        whiteMoveNumber = WhiteMove.Count + 1
        blackMoveNumber = BlackMove.Count + 1


        restartGameHasClicked = True


    End Sub



    Private Sub MainPage_Load() Handles Me.Loaded

        If CreateAppDataFile = True Then
            whiteMoveXML = XDocument.Load("Assets\WhiteMoveXML.xml")
            blackMoveXML = XDocument.Load("Assets\BlackMoveXML.xml")
            MoveXML = XDocument.Load("Assets\MoveXML.xml")
        End If

        NamePlayer1.DataContext = "Larry"
        NamePlayer2.DataContext = "Mike"

        txtMyName.DataContext = "Larry"
        txtOpponentName.DataContext = "Mike"

        rotateControl0.Angle = 0
        rotateControl180.Angle = 180

        transposePlayer2DisplayWhiteTurn.X = -750
        transposePlayer2DisplayWhiteTurn.Y = -600

        transposePlayer1DisplayWhiteTurn.X = 0
        transposePlayer1DisplayWhiteTurn.Y = 0

        Player1DisplayWhiteTurn.Children.Add(rotateControl0)
        Player1DisplayWhiteTurn.Children.Add(transposePlayer1DisplayWhiteTurn)

        Player2DisplayWhiteTurn.Children.Add(rotateControl180)
        Player2DisplayWhiteTurn.Children.Add(transposePlayer2DisplayWhiteTurn)

        transposePlayer2DisplayWhiteMoves.X = -750
        transposePlayer2DisplayWhiteMoves.Y = 0

        transposePlayer1DisplayWhiteMoves.X = 0
        transposePlayer1DisplayWhiteMoves.Y = 0

        Player1DisplayWhiteMoves.Children.Add(rotateControl0)
        Player1DisplayWhiteMoves.Children.Add(transposePlayer1DisplayWhiteMoves)

        Player2DisplayWhiteMoves.Children.Add(rotateControl180)
        Player2DisplayWhiteMoves.Children.Add(transposePlayer2DisplayWhiteMoves)

        transposePlayer2DisplayBlackTurn.X = -1105
        transposePlayer2DisplayBlackTurn.Y = 600

        transposePlayer1DisplayBlackTurn.X = 0
        transposePlayer1DisplayBlackTurn.Y = 0

        Player1DisplayBlackTurn.Children.Add(rotateControl0)
        Player1DisplayBlackTurn.Children.Add(transposePlayer1DisplayBlackTurn)

        Player2DisplayBlackTurn.Children.Add(rotateControl180)
        Player2DisplayBlackTurn.Children.Add(transposePlayer2DisplayBlackTurn)

        transposePlayer2DisplayBlackMoves.X = -1105
        transposePlayer2DisplayBlackMoves.Y = 0

        transposePlayer1DisplayBlackMoves.X = 0
        transposePlayer1DisplayBlackMoves.Y = 0

        Player1DisplayBlackMoves.Children.Add(rotateControl0)
        Player1DisplayBlackMoves.Children.Add(transposePlayer1DisplayBlackMoves)

        Player2DisplayBlackMoves.Children.Add(rotateControl180)
        Player2DisplayBlackMoves.Children.Add(transposePlayer2DisplayBlackMoves)



        transposePlayer2MoveNumberLabel.X = 950
        transposePlayer2MoveNumberLabel.Y = 600

        transposePlayer1MoveNumberLabel.X = 0
        transposePlayer1MoveNumberLabel.Y = 0

        Player1MoveNumberLabel.Children.Add(rotateControl0)
        Player1MoveNumberLabel.Children.Add(transposePlayer1MoveNumberLabel)

        Player2MoveNumberLabel.Children.Add(rotateControl180)
        Player2MoveNumberLabel.Children.Add(transposePlayer2MoveNumberLabel)

        transposePlayer2MoveNumberBox.X = 730
        transposePlayer2MoveNumberBox.Y = 610

        transposePlayer1MoveNumberBox.X = 0
        transposePlayer1MoveNumberBox.Y = 0

        Player1MoveNumberBox.Children.Add(rotateControl0)
        Player1MoveNumberBox.Children.Add(transposePlayer1MoveNumberBox)

        Player2MoveNumberBox.Children.Add(rotateControl180)
        Player2MoveNumberBox.Children.Add(transposePlayer2MoveNumberBox)

        transposePlayer2CapturedByOpponent.X = 870
        transposePlayer2CapturedByOpponent.Y = 30

        transposePlayer1CapturedByOpponent.X = 0
        transposePlayer1CapturedByOpponent.Y = 0

        Player1CapturedByOpponent.Children.Add(transposePlayer1CapturedByOpponent)
        Player2CapturedByOpponent.Children.Add(transposePlayer2CapturedByOpponent)


        transposePlayer2NewGame.X = 972
        transposePlayer2NewGame.Y = 250

        transposePlayer1NewGame.X = 0
        transposePlayer1NewGame.Y = 0

        Player1NewGame.Children.Add(rotateControl0)
        Player1NewGame.Children.Add(transposePlayer1NewGame)

        Player2NewGame.Children.Add(rotateControl180)
        Player2NewGame.Children.Add(transposePlayer2NewGame)

        transposePlayer2RestartGame.X = 728
        transposePlayer2RestartGame.Y = 250

        transposePlayer1RestartGame.X = 0
        transposePlayer1RestartGame.Y = 0

        Player1RestartGame.Children.Add(rotateControl0)
        Player1RestartGame.Children.Add(transposePlayer1RestartGame)

        Player2RestartGame.Children.Add(rotateControl180)
        Player2RestartGame.Children.Add(transposePlayer2RestartGame)

        transposePlayer2Save_Game.X = 972
        transposePlayer2Save_Game.Y = 168

        transposePlayer1Save_Game.X = 0
        transposePlayer1Save_Game.Y = 0

        Player1Save_Game.Children.Add(rotateControl0)
        Player1Save_Game.Children.Add(transposePlayer1Save_Game)

        Player2Save_Game.Children.Add(rotateControl180)
        Player2Save_Game.Children.Add(transposePlayer2Save_Game)

        transposePlayer2Replay_Game.X = 728
        transposePlayer2Replay_Game.Y = 168

        transposePlayer1Replay_Game.X = 0
        transposePlayer1Replay_Game.Y = 0

        Player1Replay_Game.Children.Add(rotateControl0)
        Player1Replay_Game.Children.Add(transposePlayer1Replay_Game)

        Player2Replay_Game.Children.Add(rotateControl180)
        Player2Replay_Game.Children.Add(transposePlayer2Replay_Game)


        transposePlayer2ChangeTurn.X = 850
        transposePlayer2ChangeTurn.Y = 66

        transposePlayer1ChangeTurn.X = 0
        transposePlayer1ChangeTurn.Y = 0

        Player1ChangeTurn.Children.Add(rotateControl0)
        Player1ChangeTurn.Children.Add(transposePlayer1ChangeTurn)

        Player2ChangeTurn.Children.Add(rotateControl180)
        Player2ChangeTurn.Children.Add(transposePlayer2ChangeTurn)



        transposePlayer2SelectColor.X = 870
        transposePlayer2SelectColor.Y = -75

        transposePlayer1SelectColor.X = 0
        transposePlayer1SelectColor.Y = 0

        Player1SelectColor.Children.Add(rotateControl0)
        Player1SelectColor.Children.Add(transposePlayer1SelectColor)

        Player2SelectColor.Children.Add(rotateControl180)
        Player2SelectColor.Children.Add(transposePlayer2SelectColor)

        transposePlayer2ReplayGameLabel.X = 850
        transposePlayer2ReplayGameLabel.Y = -200

        transposePlayer1ReplayGameLabel.X = 0
        transposePlayer1ReplayGameLabel.Y = 0

        Player1ReplayGameLabel.Children.Add(rotateControl0)
        Player1ReplayGameLabel.Children.Add(transposePlayer1ReplayGameLabel)

        Player2ReplayGameLabel.Children.Add(rotateControl180)
        Player2ReplayGameLabel.Children.Add(transposePlayer2ReplayGameLabel)

        transposePlayer2Replay_Forward.X = 850
        transposePlayer2Replay_Forward.Y = -285

        transposePlayer1Replay_Forward.X = 0
        transposePlayer1Replay_Forward.Y = 0

        Player1Replay_Forward.Children.Add(rotateControl0)
        Player1Replay_Forward.Children.Add(transposePlayer1Replay_Forward)

        Player2Replay_Forward.Children.Add(rotateControl180)
        Player2Replay_Forward.Children.Add(transposePlayer2Replay_Forward)

        transposePlayer2Replay_Reverse.X = 850
        transposePlayer2Replay_Reverse.Y = -285

        transposePlayer1Replay_Reverse.X = 0
        transposePlayer1Replay_Reverse.Y = 0

        Player1Replay_Reverse.Children.Add(rotateControl0)
        Player1Replay_Reverse.Children.Add(transposePlayer1Replay_Reverse)

        Player2Replay_Reverse.Children.Add(rotateControl180)
        Player2Replay_Reverse.Children.Add(transposePlayer2Replay_Reverse)


        transposePlayer2CapturedByMe.X = 870
        transposePlayer2CapturedByMe.Y = 30

        transposePlayer1CapturedByMe.X = 0
        transposePlayer1CapturedByMe.Y = 0

        Player1CapturedByMe.Children.Add(transposePlayer1CapturedByMe)
        Player2CapturedByMe.Children.Add(transposePlayer2CapturedByMe)

        transposePlayer2TxtCapturedPieces.X = 833
        transposePlayer2TxtCapturedPieces.Y = -600

        transposePlayer1TxtCapturedPieces.X = 0
        transposePlayer1TxtCapturedPieces.Y = 0

        Player1TxtCapturedPieces.Children.Add(rotateControl0)
        Player1TxtCapturedPieces.Children.Add(transposePlayer1TxtCapturedPieces)

        Player2TxtCapturedPieces.Children.Add(rotateControl180)
        Player2TxtCapturedPieces.Children.Add(transposePlayer2TxtCapturedPieces)


        displayWhiteTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        displayBlackTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        changeTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Restart_Game.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        ReplayGameLabel.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Replay_Forward.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Replay_Reverse.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Save_Game.Visibility = Windows.UI.Xaml.Visibility.Collapsed


        NewGame.DataContext = "New Game"
        MoveNumberBox.DataContext = ""
        Player1Check.DataContext = ""
        Player2Check.DataContext = ""


        timerNewGame = New DispatcherTimer
        timerNewGame.Interval = New TimeSpan(5000000)
        AddHandler timerNewGame.Tick, AddressOf timerNewGame_tick
        timerNewGame.Start()

        If startNewGame = False Then
            NewGame.IsEnabled = True
            boardRotation.IsEnabled = False
            changeTurn.IsEnabled = False
            Exit Sub
        End If


    End Sub

    Public Async Sub createAppData()

        'Dim docGrid As XDocument = XDocument.Load(ApplicationData.Current.LocalFolder.Path + "\GridEntry.xml")
        'Dim path As String = 

        Dim whitelocalFile As IStorageFile = Await localFolder.CreateFileAsync("Assets\WhiteMoveXML.xml", CreationCollisionOption.ReplaceExisting)
        Dim blacklocalFile As IStorageFile = Await localFolder.CreateFileAsync("Assets\BlackMoveXML.xml", CreationCollisionOption.ReplaceExisting)
    End Sub


    Private Sub AddBoardImages()

        BoardImage.Add("Box_11", Box_11)
        BoardImage.Add("Box_12", Box_12)
        BoardImage.Add("Box_13", Box_13)
        BoardImage.Add("Box_14", Box_14)
        BoardImage.Add("Box_15", Box_15)
        BoardImage.Add("Box_16", Box_16)
        BoardImage.Add("Box_17", Box_17)
        BoardImage.Add("Box_18", Box_18)

        BoardImage.Add("Box_21", Box_21)
        BoardImage.Add("Box_22", Box_22)
        BoardImage.Add("Box_23", Box_23)
        BoardImage.Add("Box_24", Box_24)
        BoardImage.Add("Box_25", Box_25)
        BoardImage.Add("Box_26", Box_26)
        BoardImage.Add("Box_27", Box_27)
        BoardImage.Add("Box_28", Box_28)

        BoardImage.Add("Box_31", Box_31)
        BoardImage.Add("Box_32", Box_32)
        BoardImage.Add("Box_33", Box_33)
        BoardImage.Add("Box_34", Box_34)
        BoardImage.Add("Box_35", Box_35)
        BoardImage.Add("Box_36", Box_36)
        BoardImage.Add("Box_37", Box_37)
        BoardImage.Add("Box_38", Box_38)

        BoardImage.Add("Box_41", Box_41)
        BoardImage.Add("Box_42", Box_42)
        BoardImage.Add("Box_43", Box_43)
        BoardImage.Add("Box_44", Box_44)
        BoardImage.Add("Box_45", Box_45)
        BoardImage.Add("Box_46", Box_46)
        BoardImage.Add("Box_47", Box_47)
        BoardImage.Add("Box_48", Box_48)

        BoardImage.Add("Box_51", Box_51)
        BoardImage.Add("Box_52", Box_52)
        BoardImage.Add("Box_53", Box_53)
        BoardImage.Add("Box_54", Box_54)
        BoardImage.Add("Box_55", Box_55)
        BoardImage.Add("Box_56", Box_56)
        BoardImage.Add("Box_57", Box_57)
        BoardImage.Add("Box_58", Box_58)

        BoardImage.Add("Box_61", Box_61)
        BoardImage.Add("Box_62", Box_62)
        BoardImage.Add("Box_63", Box_63)
        BoardImage.Add("Box_64", Box_64)
        BoardImage.Add("Box_65", Box_65)
        BoardImage.Add("Box_66", Box_66)
        BoardImage.Add("Box_67", Box_67)
        BoardImage.Add("Box_68", Box_68)

        BoardImage.Add("Box_71", Box_71)
        BoardImage.Add("Box_72", Box_72)
        BoardImage.Add("Box_73", Box_73)
        BoardImage.Add("Box_74", Box_74)
        BoardImage.Add("Box_75", Box_75)
        BoardImage.Add("Box_76", Box_76)
        BoardImage.Add("Box_77", Box_77)
        BoardImage.Add("Box_78", Box_78)

        BoardImage.Add("Box_81", Box_81)
        BoardImage.Add("Box_82", Box_82)
        BoardImage.Add("Box_83", Box_83)
        BoardImage.Add("Box_84", Box_84)
        BoardImage.Add("Box_85", Box_85)
        BoardImage.Add("Box_86", Box_86)
        BoardImage.Add("Box_87", Box_87)
        BoardImage.Add("Box_88", Box_88)

        ImagesOfCapturedPieces.Add("Box_11C", Box_11C)
        ImagesOfCapturedPieces.Add("Box_12C", Box_12C)
        ImagesOfCapturedPieces.Add("Box_13C", Box_13C)
        ImagesOfCapturedPieces.Add("Box_14C", Box_14C)
        ImagesOfCapturedPieces.Add("Box_15C", Box_15C)
        ImagesOfCapturedPieces.Add("Box_16C", Box_16C)
        ImagesOfCapturedPieces.Add("Box_17C", Box_17C)
        ImagesOfCapturedPieces.Add("Box_18C", Box_18C)

        ImagesOfCapturedPieces.Add("Box_21C", Box_21C)
        ImagesOfCapturedPieces.Add("Box_22C", Box_22C)
        ImagesOfCapturedPieces.Add("Box_23C", Box_23C)
        ImagesOfCapturedPieces.Add("Box_24C", Box_24C)
        ImagesOfCapturedPieces.Add("Box_25C", Box_25C)
        ImagesOfCapturedPieces.Add("Box_26C", Box_26C)
        ImagesOfCapturedPieces.Add("Box_27C", Box_27C)
        ImagesOfCapturedPieces.Add("Box_28C", Box_28C)

        ImagesOfCapturedPieces.Add("Box_71C", Box_71C)
        ImagesOfCapturedPieces.Add("Box_72C", Box_72C)
        ImagesOfCapturedPieces.Add("Box_73C", Box_73C)
        ImagesOfCapturedPieces.Add("Box_74C", Box_74C)
        ImagesOfCapturedPieces.Add("Box_75C", Box_75C)
        ImagesOfCapturedPieces.Add("Box_76C", Box_76C)
        ImagesOfCapturedPieces.Add("Box_77C", Box_77C)
        ImagesOfCapturedPieces.Add("Box_78C", Box_78C)

        ImagesOfCapturedPieces.Add("Box_81C", Box_81C)
        ImagesOfCapturedPieces.Add("Box_82C", Box_82C)
        ImagesOfCapturedPieces.Add("Box_83C", Box_83C)
        ImagesOfCapturedPieces.Add("Box_84C", Box_84C)
        ImagesOfCapturedPieces.Add("Box_85C", Box_85C)
        ImagesOfCapturedPieces.Add("Box_86C", Box_86C)
        ImagesOfCapturedPieces.Add("Box_87C", Box_87C)
        ImagesOfCapturedPieces.Add("Box_88C", Box_88C)



    End Sub


    Private Sub Box_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles Box_11.Tapped, _
                Box_12.Tapped, Box_13.Tapped, Box_14.Tapped, Box_15.Tapped, Box_16.Tapped, Box_17.Tapped, _
                Box_18.Tapped, Box_21.Tapped, Box_22.Tapped, Box_23.Tapped, Box_24.Tapped, Box_25.Tapped, _
                Box_26.Tapped, Box_27.Tapped, Box_28.Tapped, Box_31.Tapped, Box_32.Tapped, Box_33.Tapped, _
                Box_34.Tapped, Box_35.Tapped, Box_36.Tapped, Box_37.Tapped, Box_38.Tapped, Box_41.Tapped, _
                Box_42.Tapped, Box_43.Tapped, Box_44.Tapped, Box_45.Tapped, Box_46.Tapped, Box_47.Tapped, _
                Box_48.Tapped, Box_51.Tapped, Box_52.Tapped, Box_53.Tapped, Box_54.Tapped, Box_55.Tapped, _
                Box_56.Tapped, Box_57.Tapped, Box_58.Tapped, Box_61.Tapped, Box_62.Tapped, Box_63.Tapped, _
                Box_64.Tapped, Box_65.Tapped, Box_66.Tapped, Box_67.Tapped, Box_68.Tapped, Box_71.Tapped, _
                Box_72.Tapped, Box_73.Tapped, Box_73.Tapped, Box_74.Tapped, Box_75.Tapped, Box_76.Tapped, _
                Box_77.Tapped, Box_78.Tapped, Box_81.Tapped, Box_82.Tapped, Box_83.Tapped, Box_84.Tapped, _
                Box_85.Tapped, Box_86.Tapped, Box_87.Tapped, Box_88.Tapped


        e.Handled = True
        DisableBox_Tapped()

        If startClock = True Or replayGameIsEnabled = True Then
            EnableBox_Tapped()
            Exit Sub
        End If

        sendername = sender.name
        MakeMove(sendername)
    End Sub

    Private Sub MakeMove(ByVal sendername As String)

        Dim kingCanCastle As Boolean = False

        'If PieceCollection.ContainsKey(sendername) = True Then
        'If PossiblePieceCollection.Contains(PieceCollection(sendername).Name) = False Then
        'isPieceSelected = False
        'EnableBox_Tapped()
        'Exit Sub

        'End If
        'End If

        If isPieceSelected = False Then

            If BoardStatus(sendername) = Status.Open Then
                EnableBox_Tapped()
                Exit Sub
            ElseIf insTurn.Color = PieceCollection(sendername).Color Then

                isLocationToMoveSelected = False
                boardRotation.IsEnabled = False
                changeTurn.IsEnabled = False
                NewGame.IsEnabled = False
                pieceSelected = New AnyPiece
                pieceSelectedOld = New AnyPiece
                SetPieceSelected(PieceCollection(sendername))
                SetPieceSelectedOld(pieceSelected)


                If pieceSelected.Piece = Pieces.King Then
                    kingCanCastle = insKingPossibleMoves.CanKingCastle(pieceSelected)
                End If

                If Not (PieceSelectedPossibleMoves.Count = 0) Then
                    PieceSelectedPossibleMoves.Clear()
                End If

                For Each move2 In AllPossibleMovesFromAllPossiblePieces
                    insPossibleMove = New PossibleMove
                    insPossibleMove = move2.Value
                    If insPossibleMove.Piece.Name = pieceSelected.Name Then
                        PieceSelectedPossibleMoves.Add(insPossibleMove.EndLocation, insPossibleMove)
                    End If
                Next
                If PieceSelectedPossibleMoves.Count > 0 Then
                    isPieceSelected = True
                    If timerBlinkSelectedPiece.IsEnabled = False Then
                        timerBlinkSelectedPiece.Start()
                    End If
                    HighlightSelectedPieceToMove(pieceSelected.BoxName)
                End If
                EnableBox_Tapped()
                Exit Sub
            ElseIf Not (insTurn.Color = PieceCollection(sendername).Color) Then
                isPieceSelected = False
                EnableBox_Tapped()
                Exit Sub
            End If

        ElseIf isPieceSelected = True Then

            If insTurn.Color = Color1.White Then
                If insWhiteMove.StartLocation = BoardLocation(sendername).Location Then

                    isPieceSelected = False
                    isLocationToMoveSelected = False
                    changeTurn.IsEnabled = False

                    If BoardLocation(BoardLocator(insWhiteMove.StartLocation)).SquareColor = Color1.White Then
                        pieceSelected.Image = pieceSelected.ImageOnWhiteSquare
                    Else
                        pieceSelected.Image = pieceSelected.ImageOnBlackSquare
                    End If

                    BoardImage(BoardLocator(insWhiteMove.StartLocation)).Source = pieceSelected.Image

                    BoardStatus.Remove(BoardLocator(insWhiteMove.StartLocation))
                    BoardStatus.Add(BoardLocator(insWhiteMove.StartLocation), Status.Occupied)

                    BoardStatus.Remove(pieceSelected.BoxName)
                    BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                    BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                    If timerBlinkSelectedPiece.IsEnabled = True Then
                        timerBlinkSelectedPiece.Stop()
                    End If
                    PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                    PieceCollection3.Remove(pieceSelected.Name)

                    pieceSelectedOld.BoxName = BoardLocator(insWhiteMove.StartLocation)
                    pieceSelectedOld.SquareLocation = BoardLocation(BoardLocator(insWhiteMove.StartLocation))

                    pieceSelected = New AnyPiece
                    SetPieceSelected(pieceSelectedOld)

                    PieceCollection.Add(pieceSelected.BoxName, pieceSelected)
                    PieceCollection3.Add(pieceSelected.Name, pieceSelected.BoxName)

                    If WhiteKingWasCastling = True Then

                        If BoardLocation(BoardLocator(insCastledRookMove.StartLocation)).SquareColor = Color1.White Then
                            pieceSelected2.Image = pieceSelected2.ImageOnWhiteSquare
                        Else
                            pieceSelected2.Image = pieceSelected2.ImageOnBlackSquare
                        End If

                        BoardImage(BoardLocator(insCastledRookMove.StartLocation)).Source = pieceSelected2.Image

                        BoardStatus.Remove(BoardLocator(insCastledRookMove.StartLocation))
                        BoardStatus.Add(BoardLocator(insCastledRookMove.StartLocation), Status.Occupied)

                        BoardStatus.Remove(pieceSelected2.BoxName)
                        BoardStatus.Add(pieceSelected2.BoxName, Status.Open)

                        BoardImage(pieceSelected2.BoxName).Source = BoardLocation(pieceSelected2.BoxName).Image

                        PieceCollection.Remove(PieceCollection3(pieceSelected2.Name))
                        PieceCollection3.Remove(pieceSelected2.Name)

                        pieceSelected2Old.BoxName = BoardLocator(insCastledRookMove.StartLocation)
                        pieceSelected2Old.SquareLocation = BoardLocation(BoardLocator(insCastledRookMove.StartLocation))

                        pieceSelected2 = New AnyPiece
                        SetPieceSelected2(pieceSelected2Old)

                        PieceCollection.Add(pieceSelected2.BoxName, pieceSelected2)
                        PieceCollection3.Add(pieceSelected2.Name, pieceSelected2.BoxName)

                        WhiteKingIsCastling = False

                    End If

                    BlackKingIsChecked = False
                    BlackKingWasChecked = True
                    If timerBlackKingChecked.IsEnabled = True Then
                        timerBlackKingChecked.Stop()
                    End If
                    If insPlayers.BlackPlayer = Player.Player1 Then
                        Player1Check.DataContext = ""
                    ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                        Player2Check.DataContext = ""
                    End If

                    If WhiteKingWasChecked = True Then
                        WhiteKingIsChecked = True
                        If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                            If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                            End If
                        End If
                        If timerWhiteKingChecked.IsEnabled = False Then
                            timerWhiteKingChecked.Start()
                        End If
                    End If

                    If isPieceCaptured = True Then

                        isPieceCaptured = False

                        BlackCapturedPieces.Remove(insBlackCapturedPiece.Piece.CapturedBoxName)
                        ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = CaptureBoardLocation(insBlackCapturedPiece.Piece.CapturedBoxName).Image

                        PieceCollection.Add(BoardLocator(insBlackCapturedPiece.CaptureLocation), insBlackCapturedPiece.Piece)
                        PieceCollection3.Add(insBlackCapturedPiece.Piece.name, BoardLocator(insBlackCapturedPiece.CaptureLocation))

                        BoardStatus.Remove(BoardLocator(insBlackCapturedPiece.CaptureLocation))
                        BoardStatus.Add(BoardLocator(insBlackCapturedPiece.CaptureLocation), Status.Occupied)

                        If BoardLocation(BoardLocator(insBlackCapturedPiece.CaptureLocation)).SquareColor = Color1.White Then
                            BoardImage(BoardLocator(insBlackCapturedPiece.CaptureLocation)).Source = PieceCollection(BoardLocator(insBlackCapturedPiece.CaptureLocation)).ImageOnWhiteSquare
                        ElseIf BoardLocation(BoardLocator(insBlackCapturedPiece.CaptureLocation)).SquareColor = Color1.Black Then
                            BoardImage(BoardLocator(insBlackCapturedPiece.CaptureLocation)).Source = PieceCollection(BoardLocator(insBlackCapturedPiece.CaptureLocation)).ImageOnBlackSquare
                        End If

                        If PieceSelectedPossibleMoves.Count > 0 Then
                            HighlightSelectedPieceToMove(pieceSelected.BoxName)
                        End If

                        EnableBox_Tapped()
                        Exit Sub

                    ElseIf isPieceCaptured = False Then

                        If PieceSelectedPossibleMoves.Count > 0 Then
                            HighlightSelectedPieceToMove(pieceSelected.BoxName)
                        End If

                        EnableBox_Tapped()
                        Exit Sub
                    End If
                    'Player Chooses Different piece to move
                ElseIf Not (insWhiteMove.StartLocation = BoardLocation(sendername).Location) Then

                    If PieceCollection.ContainsKey(sendername) Then
                        If PieceCollection(sendername).Color = insTurn.Color Then

                            isPieceSelected = False
                            isLocationToMoveSelected = False
                            changeTurn.IsEnabled = False

                            If BoardLocation(BoardLocator(insWhiteMove.StartLocation)).SquareColor = Color1.White Then
                                pieceSelected.Image = pieceSelected.ImageOnWhiteSquare
                            Else
                                pieceSelected.Image = pieceSelected.ImageOnBlackSquare
                            End If

                            BoardImage(BoardLocator(insWhiteMove.StartLocation)).Source = pieceSelected.Image

                            BoardStatus.Remove(BoardLocator(insWhiteMove.StartLocation))
                            BoardStatus.Add(BoardLocator(insWhiteMove.StartLocation), Status.Occupied)

                            If Not (insWhiteMove.EndLocation = "") Then
                                BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                                BoardStatus.Remove(pieceSelected.BoxName)
                                BoardStatus.Add(pieceSelected.BoxName, Status.Open)
                            End If

                            PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                            PieceCollection3.Remove(pieceSelected.Name)

                            pieceSelectedOld.BoxName = BoardLocator(insWhiteMove.StartLocation)
                            pieceSelectedOld.SquareLocation = BoardLocation(BoardLocator(insWhiteMove.StartLocation))

                            pieceSelected = New AnyPiece
                            SetPieceSelected(pieceSelectedOld)

                            PieceCollection.Add(pieceSelected.BoxName, pieceSelected)
                            PieceCollection3.Add(pieceSelected.Name, pieceSelected.BoxName)

                            pieceSelected = New AnyPiece
                            SetPieceSelected(PieceCollection(sendername))
                            pieceSelectedOld = New AnyPiece
                            SetPieceSelectedOld(pieceSelected)


                            If WhiteKingWasCastling = True Then

                                If BoardLocation(BoardLocator(insCastledRookMove.StartLocation)).SquareColor = Color1.White Then
                                    pieceSelected2.Image = pieceSelected2.ImageOnWhiteSquare
                                Else
                                    pieceSelected2.Image = pieceSelected2.ImageOnBlackSquare
                                End If

                                BoardImage(BoardLocator(insCastledRookMove.StartLocation)).Source = pieceSelected2.Image

                                BoardStatus.Remove(BoardLocator(insCastledRookMove.StartLocation))
                                BoardStatus.Add(BoardLocator(insCastledRookMove.StartLocation), Status.Occupied)

                                BoardStatus.Remove(pieceSelected2.BoxName)
                                BoardStatus.Add(pieceSelected2.BoxName, Status.Open)

                                BoardImage(pieceSelected2.BoxName).Source = BoardLocation(pieceSelected2.BoxName).Image

                                PieceCollection.Remove(PieceCollection3(pieceSelected2.Name))
                                PieceCollection3.Remove(pieceSelected2.Name)

                                pieceSelected2Old.BoxName = BoardLocator(insCastledRookMove.StartLocation)
                                pieceSelected2Old.SquareLocation = BoardLocation(BoardLocator(insCastledRookMove.StartLocation))

                                pieceSelected2 = New AnyPiece
                                SetPieceSelected2(pieceSelected2Old)

                                PieceCollection.Add(pieceSelected2.BoxName, pieceSelected2)
                                PieceCollection3.Add(pieceSelected2.Name, pieceSelected2.BoxName)

                                WhiteKingIsCastling = False

                            End If



                            'Need to select image Source if BoardStatus(sendername) is occupied

                            BoardImage(sendername).Source = BoardLocation(sendername).Image
                            If BoardStatus(sendername) = Status.Occupied Then

                                If BoardLocation(sendername).SquareColor = Color1.White Then
                                    BoardImage(sendername).Source = pieceSelected.ImageOnWhiteSquare
                                Else
                                    BoardImage(sendername).Source = pieceSelected.ImageOnBlackSquare
                                End If
                            End If
                            If timerBlinkSelectedPiece.IsEnabled = True Then
                                timerBlinkSelectedPiece.Stop()
                            End If

                            BlackKingIsChecked = False
                            BlackKingWasChecked = False
                            If timerBlackKingChecked.IsEnabled = True Then
                                timerBlackKingChecked.Stop()
                            End If
                            If insPlayers.BlackPlayer = Player.Player1 Then
                                Player1Check.DataContext = ""
                            ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                Player2Check.DataContext = ""
                            End If

                            If WhiteKingWasChecked = True Then
                                WhiteKingIsChecked = True
                                If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                    If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                        PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                                    End If
                                End If
                                If timerWhiteKingChecked.IsEnabled = False Then
                                    timerWhiteKingChecked.Start()
                                End If
                            End If

                            If PossiblePieceCollection.Contains(pieceSelected.Name) = True Then
                                HighlightSelectedPieceToMove(pieceSelected.BoxName)
                            End If

                            EnableBox_Tapped()
                            Exit Sub

                        End If
                    End If

                    'Player Chooses location to move piece to
                    If isLocationToMoveSelected = False Then
                        'Is it possible to move to selected location
                        PieceSelectedPossibleMoves.Clear()
                        For Each move2 In AllPossibleMovesFromAllPossiblePieces
                            insPossibleMove = New PossibleMove
                            insPossibleMove = move2.Value
                            If insPossibleMove.Piece.Name = pieceSelected.Name Then
                                If PieceSelectedPossibleMoves.ContainsKey(insPossibleMove.EndLocation) = False Then
                                    PieceSelectedPossibleMoves.Add(insPossibleMove.EndLocation, insPossibleMove)
                                End If

                            End If
                        Next

                        If PieceSelectedPossibleMoves.ContainsKey(BoardLocation(sendername).Location) Then

                            If BoardStatus(sendername) = Status.Occupied Then
                                If pieceSelected.Piece = Pieces.Pawn And pieceSelected.SquareLocation.column = PieceCollection(sendername).SquareLocation.column Then
                                    EnableBox_Tapped()
                                    Exit Sub
                                End If

                                isPieceCaptured = True

                                If PieceCollection(sendername).Color = Color1.White Then
                                    insWhiteCapturedPiece = New CapturedPiece(PieceCollection(sendername), PieceCollection(sendername).SquareLocation.Location, blackMoveNumber)
                                    If insWhiteCapturedPiece.Piece.CapturedBoxName IsNot Nothing Then
                                        If WhiteCapturedPieces.ContainsKey(insWhiteCapturedPiece.Piece.CapturedBoxName) = False Then
                                            WhiteCapturedPieces.Add(insWhiteCapturedPiece.Piece.CapturedBoxName, insWhiteCapturedPiece)
                                            If CaptureBoardLocation(insWhiteCapturedPiece.Piece.CapturedBoxName).SquareColor = Color1.White Then
                                                ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = insWhiteCapturedPiece.Piece.ImageOnWhiteSquare
                                            Else
                                                ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = insWhiteCapturedPiece.Piece.ImageOnBlackSquare
                                            End If
                                        End If
                                    End If
                                ElseIf PieceCollection(sendername).Color = Color1.Black Then
                                    insBlackCapturedPiece = New CapturedPiece(PieceCollection(sendername), PieceCollection(sendername).SquareLocation.Location, whiteMoveNumber)
                                    If insBlackCapturedPiece.Piece.CapturedBoxName IsNot Nothing Then
                                        If BlackCapturedPieces.ContainsKey(insBlackCapturedPiece.Piece.CapturedBoxName) = False Then
                                            BlackCapturedPieces.Add(insBlackCapturedPiece.Piece.CapturedBoxName, insBlackCapturedPiece)
                                            If CaptureBoardLocation(insBlackCapturedPiece.Piece.CapturedBoxName).SquareColor = Color1.White Then
                                                ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = insBlackCapturedPiece.Piece.ImageOnWhiteSquare
                                            Else
                                                ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = insBlackCapturedPiece.Piece.ImageOnBlackSquare
                                            End If
                                        End If
                                    End If
                                End If

                                BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                                BoardStatus.Remove(pieceSelected.BoxName)
                                BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                                BoardImage(sendername).Source = BoardLocation(sendername).Image

                                BoardStatus.Remove(sendername)
                                BoardStatus.Add(sendername, Status.Open)

                                If timerBlinkSelectedPiece.IsEnabled = True Then
                                    timerBlinkSelectedPiece.Stop()
                                End If

                                PieceCollection3.Remove(PieceCollection(sendername).Name)
                                PieceCollection.Remove(sendername)

                                PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                                PieceCollection3.Remove(pieceSelected.Name)


                                If WhiteKingWasChecked = True Then
                                    WhiteKingIsChecked = True
                                    If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerWhiteKingChecked.IsEnabled = False Then
                                        timerWhiteKingChecked.Start()
                                    End If

                                End If

                                If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = False Then
                                    BlackKingIsChecked = False
                                    BlackKingWasChecked = False
                                    If timerBlackKingChecked.IsEnabled = True Then
                                        timerBlackKingChecked.Stop()
                                    End If
                                    If insPlayers.BlackPlayer = Player.Player1 Then
                                        Player1Check.DataContext = ""
                                    ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                        Player2Check.DataContext = ""
                                    End If
                                ElseIf PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                    BlackKingWasChecked = True
                                    BlackKingIsChecked = True

                                    If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerBlackKingChecked.IsEnabled = False Then
                                        timerBlackKingChecked.Start()
                                    End If
                                End If

                                HightlightMoveLocation(sendername, pieceSelected)
                                EnableBox_Tapped()
                                Exit Sub

                            ElseIf BoardStatus(sendername) = Status.Open Then

                                changeTurn.IsEnabled = False
                                NewGame.IsEnabled = False

                                BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                                BoardStatus.Remove(pieceSelected.BoxName)
                                BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                                If timerBlinkSelectedPiece.IsEnabled = True Then
                                    timerBlinkSelectedPiece.Stop()
                                End If

                                PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                                PieceCollection3.Remove(pieceSelected.Name)

                                If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).PieceIsCaptured = True Then

                                    isPieceCaptured = True

                                    insBlackCapturedPiece = New CapturedPiece(PieceCollection(CapturedBlackPieces(BoardLocation(sendername).Location).BoxName), PieceCollection(CapturedBlackPieces(BoardLocation(sendername).Location).BoxName).SquareLocation.Location, whiteMoveNumber)
                                    If BlackCapturedPieces.ContainsKey(insBlackCapturedPiece.Piece.CapturedBoxName) = False Then
                                        BlackCapturedPieces.Add(insBlackCapturedPiece.Piece.CapturedBoxName, insBlackCapturedPiece)
                                        ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = insBlackCapturedPiece.Piece.OriginalImage
                                    End If
                                    BoardImage(CapturedBlackPieces(BoardLocation(sendername).Location).BoxName).Source = BoardLocation(CapturedBlackPieces(BoardLocation(sendername).Location).BoxName).Image

                                    BoardStatus.Remove(CapturedBlackPieces(BoardLocation(sendername).Location).BoxName)
                                    BoardStatus.Add(CapturedBlackPieces(BoardLocation(sendername).Location).BoxName, Status.Open)

                                End If

                                If pieceSelected.Piece = Pieces.King And pieceSelected.MoveNumber = 1 Then
                                    If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingCanCastleToQueenSide = True Then

                                        If insPlayers.WhitePlayer = Player.Player1 Then
                                            pieceSelected2 = New AnyPiece
                                            SetPieceSelected2(PieceCollection("Box_81"))
                                            pieceSelected2Old = New AnyPiece
                                            SetPieceSelected2Old(PieceCollection("Box_81"))
                                        ElseIf insPlayers.WhitePlayer = Player.Player2 Then
                                            pieceSelected2 = New AnyPiece
                                            SetPieceSelected2(PieceCollection("Box_18"))
                                            pieceSelected2Old = New AnyPiece
                                            SetPieceSelected2Old(PieceCollection("Box_18"))
                                        End If

                                        BoardImage(pieceSelected2.BoxName).Source = BoardLocation(pieceSelected2.BoxName).Image

                                        BoardStatus.Remove(pieceSelected2.BoxName)
                                        BoardStatus.Add(pieceSelected2.BoxName, Status.Open)

                                        PieceCollection.Remove(PieceCollection3(pieceSelected2.Name))
                                        PieceCollection3.Remove(pieceSelected2.Name)

                                        If AllPossibleCastledRookMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                            BlackKingWasChecked = True
                                        End If

                                        WhiteKingIsCastling = True
                                        WhiteKingWasCastling = True

                                    ElseIf PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingCanCastleToKingSide = True Then

                                        If insPlayers.WhitePlayer = Player.Player1 Then
                                            pieceSelected2 = New AnyPiece
                                            SetPieceSelected2(PieceCollection("Box_88"))
                                            pieceSelected2Old = New AnyPiece
                                            SetPieceSelected2Old(PieceCollection("Box_88"))
                                        ElseIf insPlayers.WhitePlayer = Player.Player2 Then
                                            pieceSelected2 = New AnyPiece
                                            SetPieceSelected2(PieceCollection("Box_11"))
                                            pieceSelected2Old = New AnyPiece
                                            SetPieceSelected2Old(PieceCollection("Box_11"))

                                        End If

                                        BoardImage(pieceSelected2.BoxName).Source = BoardLocation(pieceSelected2.BoxName).Image

                                        BoardStatus.Remove(pieceSelected2.BoxName)
                                        BoardStatus.Add(pieceSelected2.BoxName, Status.Open)

                                        PieceCollection.Remove(PieceCollection3(pieceSelected2.Name))
                                        PieceCollection3.Remove(pieceSelected2.Name)

                                        If AllPossibleCastledRookMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                            BlackKingWasChecked = True
                                        End If

                                        WhiteKingIsCastling = True
                                        WhiteKingWasCastling = True
                                    End If
                                End If

                                If WhiteKingWasChecked = True Then
                                    WhiteKingIsChecked = True
                                    If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerWhiteKingChecked.IsEnabled = False Then
                                        timerWhiteKingChecked.Start()
                                    End If
                                End If

                                If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = False Then
                                    BlackKingIsChecked = False
                                    BlackKingWasChecked = False
                                    If timerBlackKingChecked.IsEnabled = True Then
                                        timerBlackKingChecked.Stop()
                                    End If
                                    If insPlayers.BlackPlayer = Player.Player1 Then
                                        Player1Check.DataContext = ""
                                    ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                        Player2Check.DataContext = ""
                                    End If
                                ElseIf PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                    BlackKingWasChecked = True
                                    BlackKingIsChecked = True
                                    If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerBlackKingChecked.IsEnabled = False Then
                                        timerBlackKingChecked.Start()
                                    End If
                                End If

                                HightlightMoveLocation(sendername, pieceSelected)
                                EnableBox_Tapped()
                                Exit Sub
                            End If

                        ElseIf Not (PieceSelectedPossibleMoves.ContainsKey(BoardLocation(sendername).Location)) Then

                            EnableBox_Tapped()
                            Exit Sub

                        End If

                    ElseIf isLocationToMoveSelected = True Then

                        'Is it possible to move to selected location
                        PieceSelectedPossibleMoves.Clear()
                        For Each move2 In AllPossibleMovesFromAllPossiblePieces
                            insPossibleMove = New PossibleMove
                            insPossibleMove = move2.Value
                            If insPossibleMove.Piece.Name = pieceSelected.Name Then
                                If PieceSelectedPossibleMoves.ContainsKey(insPossibleMove.EndLocation) = False Then
                                    PieceSelectedPossibleMoves.Add(insPossibleMove.EndLocation, insPossibleMove)
                                End If

                            End If
                        Next

                        If PieceSelectedPossibleMoves.ContainsKey(BoardLocation(sendername).Location) = False Then

                            EnableBox_Tapped()
                            Exit Sub

                        ElseIf PieceSelectedPossibleMoves.ContainsKey(BoardLocation(sendername).Location) = True Then

                            isPieceSelected = False
                            isLocationToMoveSelected = False
                            changeTurn.IsEnabled = False



                            If BoardLocation(BoardLocator(insWhiteMove.StartLocation)).SquareColor = Color1.White Then
                                pieceSelected.Image = pieceSelected.ImageOnWhiteSquare
                            Else
                                pieceSelected.Image = pieceSelected.ImageOnBlackSquare
                            End If

                            BoardImage(BoardLocator(insWhiteMove.StartLocation)).Source = pieceSelected.Image

                            BoardStatus.Remove(BoardLocator(insWhiteMove.StartLocation))
                            BoardStatus.Add(BoardLocator(insWhiteMove.StartLocation), Status.Occupied)

                            BoardStatus.Remove(pieceSelected.BoxName)
                            BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                            BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                            If timerBlinkSelectedPiece.IsEnabled = True Then
                                timerBlinkSelectedPiece.Stop()
                            End If

                            PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                            PieceCollection3.Remove(pieceSelected.Name)

                            pieceSelectedOld.BoxName = BoardLocator(insWhiteMove.StartLocation)
                            pieceSelectedOld.SquareLocation = BoardLocation(BoardLocator(insWhiteMove.StartLocation))

                            pieceSelected = New AnyPiece
                            SetPieceSelected(pieceSelectedOld)

                            PieceCollection.Add(pieceSelected.BoxName, pieceSelected)
                            PieceCollection3.Add(pieceSelected.Name, pieceSelected.BoxName)


                            If WhiteKingWasCastling = True Then

                                If BoardLocation(BoardLocator(insCastledRookMove.StartLocation)).SquareColor = Color1.White Then
                                    pieceSelected2.Image = pieceSelected2.ImageOnWhiteSquare
                                Else
                                    pieceSelected2.Image = pieceSelected2.ImageOnBlackSquare
                                End If

                                BoardImage(BoardLocator(insCastledRookMove.StartLocation)).Source = pieceSelected2.Image

                                BoardStatus.Remove(BoardLocator(insCastledRookMove.StartLocation))
                                BoardStatus.Add(BoardLocator(insCastledRookMove.StartLocation), Status.Occupied)

                                BoardStatus.Remove(pieceSelected2.BoxName)
                                BoardStatus.Add(pieceSelected2.BoxName, Status.Open)

                                BoardImage(pieceSelected2.BoxName).Source = BoardLocation(pieceSelected2.BoxName).Image

                                PieceCollection.Remove(PieceCollection3(pieceSelected2.Name))
                                PieceCollection3.Remove(pieceSelected2.Name)

                                pieceSelected2Old.BoxName = BoardLocator(insCastledRookMove.StartLocation)
                                pieceSelected2Old.SquareLocation = BoardLocation(BoardLocator(insCastledRookMove.StartLocation))

                                pieceSelected2 = New AnyPiece
                                SetPieceSelected2(pieceSelected2Old)

                                PieceCollection.Add(pieceSelected2.BoxName, pieceSelected2)
                                PieceCollection3.Add(pieceSelected2.Name, pieceSelected2.BoxName)

                                WhiteKingIsCastling = False

                            End If

                            If WhiteKingWasChecked = True Then
                                WhiteKingIsChecked = True
                                If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                    If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                        PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                                    End If
                                End If
                                If timerWhiteKingChecked.IsEnabled = False Then
                                    timerWhiteKingChecked.Start()
                                End If
                            End If

                            BlackKingIsChecked = False
                            BlackKingWasChecked = False
                            If timerBlackKingChecked.IsEnabled = True Then
                                timerBlackKingChecked.Stop()
                            End If
                            If insPlayers.BlackPlayer = Player.Player1 Then
                                Player1Check.DataContext = ""
                            ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                Player2Check.DataContext = ""
                            End If


                            If isPieceCaptured = True Then

                                isPieceCaptured = False

                                BlackCapturedPieces.Remove(insBlackCapturedPiece.Piece.CapturedBoxName)
                                ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = CaptureBoardLocation(insBlackCapturedPiece.Piece.CapturedBoxName).Image

                                PieceCollection.Add(BoardLocator(insBlackCapturedPiece.CaptureLocation), insBlackCapturedPiece.Piece)
                                PieceCollection3.Add(insBlackCapturedPiece.Piece.name, BoardLocator(insBlackCapturedPiece.CaptureLocation))

                                BoardStatus.Remove(BoardLocator(insBlackCapturedPiece.CaptureLocation))
                                BoardStatus.Add(BoardLocator(insBlackCapturedPiece.CaptureLocation), Status.Occupied)

                                If BoardLocation(BoardLocator(insBlackCapturedPiece.CaptureLocation)).SquareColor = Color1.White Then
                                    BoardImage(BoardLocator(insBlackCapturedPiece.CaptureLocation)).Source = PieceCollection(BoardLocator(insBlackCapturedPiece.CaptureLocation)).ImageOnWhiteSquare
                                ElseIf BoardLocation(BoardLocator(insBlackCapturedPiece.CaptureLocation)).SquareColor = Color1.Black Then
                                    BoardImage(BoardLocator(insBlackCapturedPiece.CaptureLocation)).Source = PieceCollection(BoardLocator(insBlackCapturedPiece.CaptureLocation)).ImageOnBlackSquare
                                End If
                            End If

                            isPieceSelected = True

                            If BoardStatus(sendername) = Status.Occupied Then
                                If pieceSelected.Piece = Pieces.Pawn And pieceSelected.SquareLocation.column = PieceCollection(sendername).SquareLocation.column Then
                                    EnableBox_Tapped()
                                    Exit Sub
                                End If

                                isPieceCaptured = True

                                If PieceCollection(sendername).Color = Color1.White Then
                                    insWhiteCapturedPiece = New CapturedPiece(PieceCollection(sendername), PieceCollection(sendername).SquareLocation.Location, blackMoveNumber)
                                    If insWhiteCapturedPiece.Piece.CapturedBoxName IsNot Nothing Then
                                        If WhiteCapturedPieces.ContainsKey(insWhiteCapturedPiece.Piece.CapturedBoxName) = False Then
                                            WhiteCapturedPieces.Add(insWhiteCapturedPiece.Piece.CapturedBoxName, insWhiteCapturedPiece)
                                            If CaptureBoardLocation(insWhiteCapturedPiece.Piece.CapturedBoxName).SquareColor = Color1.White Then
                                                ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = insWhiteCapturedPiece.Piece.ImageOnWhiteSquare
                                            Else
                                                ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = insWhiteCapturedPiece.Piece.ImageOnBlackSquare
                                            End If
                                        End If
                                    End If
                                ElseIf PieceCollection(sendername).Color = Color1.Black Then
                                    insBlackCapturedPiece = New CapturedPiece(PieceCollection(sendername), PieceCollection(sendername).SquareLocation.Location, whiteMoveNumber)
                                    If insBlackCapturedPiece.Piece.CapturedBoxName IsNot Nothing Then
                                        If BlackCapturedPieces.ContainsKey(insBlackCapturedPiece.Piece.CapturedBoxName) = False Then
                                            BlackCapturedPieces.Add(insBlackCapturedPiece.Piece.CapturedBoxName, insBlackCapturedPiece)
                                            If CaptureBoardLocation(insBlackCapturedPiece.Piece.CapturedBoxName).SquareColor = Color1.White Then
                                                ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = insBlackCapturedPiece.Piece.ImageOnWhiteSquare
                                            Else
                                                ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = insBlackCapturedPiece.Piece.ImageOnBlackSquare
                                            End If
                                        End If
                                    End If
                                End If


                                BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                                BoardStatus.Remove(pieceSelected.BoxName)
                                BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                                BoardImage(sendername).Source = BoardLocation(sendername).Image

                                BoardStatus.Remove(sendername)
                                BoardStatus.Add(sendername, Status.Open)

                                If timerBlinkSelectedPiece.IsEnabled = True Then
                                    timerBlinkSelectedPiece.Stop()
                                End If

                                PieceCollection3.Remove(PieceCollection(sendername).Name)
                                PieceCollection.Remove(sendername)

                                PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                                PieceCollection3.Remove(pieceSelected.Name)


                                If WhiteKingWasChecked = True Then
                                    WhiteKingIsChecked = True
                                    If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerWhiteKingChecked.IsEnabled = False Then
                                        timerWhiteKingChecked.Start()
                                    End If
                                End If

                                If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = False Then
                                    BlackKingIsChecked = False
                                    BlackKingWasChecked = False
                                    If timerBlackKingChecked.IsEnabled = True Then
                                        timerBlackKingChecked.Stop()
                                    End If
                                    If insPlayers.BlackPlayer = Player.Player1 Then
                                        Player1Check.DataContext = ""
                                    ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                        Player2Check.DataContext = ""
                                    End If
                                ElseIf PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                    BlackKingWasChecked = True
                                    BlackKingIsChecked = True
                                    If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerBlackKingChecked.IsEnabled = False Then
                                        timerBlackKingChecked.Start()
                                    End If
                                End If

                                HightlightMoveLocation(sendername, pieceSelected)
                                EnableBox_Tapped()
                                Exit Sub

                            ElseIf BoardStatus(sendername) = Status.Open Then

                                changeTurn.IsEnabled = False
                                NewGame.IsEnabled = False

                                BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                                BoardStatus.Remove(pieceSelected.BoxName)
                                BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                                If timerBlinkSelectedPiece.IsEnabled = True Then
                                    timerBlinkSelectedPiece.Stop()
                                End If

                                PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                                PieceCollection3.Remove(pieceSelected.Name)

                                If WhiteKingWasChecked = True Then
                                    WhiteKingIsChecked = True
                                    If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerWhiteKingChecked.IsEnabled = False Then
                                        timerWhiteKingChecked.Start()
                                    End If
                                End If

                                If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = False Then
                                    BlackKingIsChecked = False
                                    BlackKingWasChecked = False
                                    If timerBlackKingChecked.IsEnabled = True Then
                                        timerBlackKingChecked.Stop()
                                    End If
                                    If insPlayers.BlackPlayer = Player.Player1 Then
                                        Player1Check.DataContext = ""
                                    ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                        Player2Check.DataContext = ""
                                    End If
                                ElseIf PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                    BlackKingWasChecked = True
                                    BlackKingIsChecked = True
                                    If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerBlackKingChecked.IsEnabled = False Then
                                        timerBlackKingChecked.Start()
                                    End If
                                End If

                                HightlightMoveLocation(sendername, pieceSelected)
                                EnableBox_Tapped()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            ElseIf insTurn.Color = Color1.Black Then

                If insBlackMove.StartLocation = BoardLocation(sendername).Location Then
                    isPieceSelected = False
                    isLocationToMoveSelected = False
                    changeTurn.IsEnabled = False

                    If BoardLocation(BoardLocator(insBlackMove.StartLocation)).SquareColor = Color1.White Then
                        pieceSelected.Image = pieceSelected.ImageOnWhiteSquare
                    Else
                        pieceSelected.Image = pieceSelected.ImageOnBlackSquare
                    End If

                    BoardImage(BoardLocator(insBlackMove.StartLocation)).Source = pieceSelected.Image

                    BoardStatus.Remove(BoardLocator(insBlackMove.StartLocation))
                    BoardStatus.Add(BoardLocator(insBlackMove.StartLocation), Status.Occupied)

                    BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                    BoardStatus.Remove(pieceSelected.BoxName)
                    BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                    If timerBlinkSelectedPiece.IsEnabled = True Then
                        timerBlinkSelectedPiece.Stop()
                    End If

                    PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                    PieceCollection3.Remove(pieceSelected.Name)

                    pieceSelectedOld.BoxName = BoardLocator(insBlackMove.StartLocation)
                    pieceSelectedOld.SquareLocation = BoardLocation(BoardLocator(insBlackMove.StartLocation))

                    pieceSelected = New AnyPiece
                    SetPieceSelected(pieceSelectedOld)

                    PieceCollection.Add(pieceSelected.BoxName, pieceSelected)
                    PieceCollection3.Add(pieceSelected.Name, pieceSelected.BoxName)

                    If BlackKingWasCastling = True Then

                        If BoardLocation(BoardLocator(insCastledRookMove.StartLocation)).SquareColor = Color1.White Then
                            pieceSelected2.Image = pieceSelected2.ImageOnWhiteSquare
                        Else
                            pieceSelected2.Image = pieceSelected2.ImageOnBlackSquare
                        End If

                        BoardImage(BoardLocator(insCastledRookMove.StartLocation)).Source = pieceSelected2.Image

                        BoardStatus.Remove(BoardLocator(insCastledRookMove.StartLocation))
                        BoardStatus.Add(BoardLocator(insCastledRookMove.StartLocation), Status.Occupied)

                        BoardStatus.Remove(pieceSelected2.BoxName)
                        BoardStatus.Add(pieceSelected2.BoxName, Status.Open)

                        BoardImage(pieceSelected2.BoxName).Source = BoardLocation(pieceSelected2.BoxName).Image

                        PieceCollection.Remove(PieceCollection3(pieceSelected2.Name))
                        PieceCollection3.Remove(pieceSelected2.Name)

                        pieceSelected2Old.BoxName = BoardLocator(insCastledRookMove.StartLocation)
                        pieceSelected2Old.SquareLocation = BoardLocation(BoardLocator(insCastledRookMove.StartLocation))

                        pieceSelected2 = New AnyPiece
                        SetPieceSelected2(pieceSelected2Old)

                        PieceCollection.Add(pieceSelected2.BoxName, pieceSelected2)
                        PieceCollection3.Add(pieceSelected2.Name, pieceSelected2.BoxName)

                        BlackKingIsCastling = False
                        BlackKingWasCastling = False


                    End If

                    WhiteKingIsChecked = False
                    WhiteKingWasChecked = False
                    If timerWhiteKingChecked.IsEnabled = True Then
                        timerWhiteKingChecked.Stop()
                    End If
                    If insPlayers.WhitePlayer = Player.Player1 Then
                        Player1Check.DataContext = ""
                    ElseIf insPlayers.WhitePlayer = Player.Player2 Then
                        Player2Check.DataContext = ""
                    End If


                    If BlackKingWasChecked = True Then
                        BlackKingIsChecked = True
                        If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                            If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                            End If
                        End If
                        If timerBlackKingChecked.IsEnabled = False Then
                            timerBlackKingChecked.Start()
                        End If
                    End If


                    If isPieceCaptured = True Then

                        isPieceCaptured = False

                        WhiteCapturedPieces.Remove(insWhiteCapturedPiece.Piece.CapturedBoxName)
                        ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = CaptureBoardLocation(insWhiteCapturedPiece.Piece.CapturedBoxName).Image

                        PieceCollection.Add(BoardLocator(insWhiteCapturedPiece.CaptureLocation), insWhiteCapturedPiece.Piece)
                        PieceCollection3.Add(insWhiteCapturedPiece.Piece.Name, BoardLocator(insWhiteCapturedPiece.CaptureLocation))

                        BoardStatus.Remove(BoardLocator(insWhiteCapturedPiece.CaptureLocation))
                        BoardStatus.Add(BoardLocator(insWhiteCapturedPiece.CaptureLocation), Status.Occupied)

                        If BoardLocation(BoardLocator(insWhiteCapturedPiece.CaptureLocation)).SquareColor = Color1.White Then
                            insImage = New Image
                            insImage = BoardImage(BoardLocator(insWhiteCapturedPiece.CaptureLocation))
                            insImage.Source = PieceCollection(BoardLocator(insWhiteCapturedPiece.CaptureLocation)).ImageOnWhiteSquare
                            BoardImage.Remove(BoardLocator(insWhiteCapturedPiece.CaptureLocation))
                            BoardImage.Add(BoardLocator(insWhiteCapturedPiece.CaptureLocation), insImage)
                        ElseIf BoardLocation(BoardLocator(insWhiteCapturedPiece.CaptureLocation)).SquareColor = Color1.Black Then
                            insImage = New Image
                            insImage = BoardImage(BoardLocator(insWhiteCapturedPiece.CaptureLocation))
                            insImage.Source = PieceCollection(BoardLocator(insWhiteCapturedPiece.CaptureLocation)).ImageOnBlackSquare
                            BoardImage.Remove(BoardLocator(insWhiteCapturedPiece.CaptureLocation))
                            BoardImage.Add(BoardLocator(insWhiteCapturedPiece.CaptureLocation), insImage)
                        End If

                        If PieceSelectedPossibleMoves.Count > 0 Then
                            HighlightSelectedPieceToMove(pieceSelected.BoxName)
                        End If

                        EnableBox_Tapped()
                        Exit Sub

                    ElseIf isPieceCaptured = False Then

                        If PieceSelectedPossibleMoves.Count > 0 Then
                            HighlightSelectedPieceToMove(pieceSelected.BoxName)
                        End If

                        EnableBox_Tapped()
                        Exit Sub
                    End If
                ElseIf Not (insBlackMove.StartLocation = BoardLocation(sendername).Location) Then
                    If BoardStatus(sendername) = Status.Occupied Then
                        If PieceCollection.ContainsKey(sendername) Then

                            If PieceCollection(sendername).Color = insTurn.Color Then

                                isPieceSelected = False
                                isLocationToMoveSelected = False
                                changeTurn.IsEnabled = False


                                If BoardLocation(BoardLocator(insBlackMove.StartLocation)).SquareColor = Color1.White Then
                                    pieceSelected.Image = pieceSelected.ImageOnWhiteSquare
                                Else
                                    pieceSelected.Image = pieceSelected.ImageOnBlackSquare
                                End If

                                BoardImage(BoardLocator(insBlackMove.StartLocation)).Source = pieceSelected.Image

                                BoardStatus.Remove(BoardLocator(insBlackMove.StartLocation))
                                BoardStatus.Add(BoardLocator(insBlackMove.StartLocation), Status.Occupied)

                                If Not (insBlackMove.EndLocation = "") Then
                                    BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                                    BoardStatus.Remove(pieceSelected.BoxName)
                                    BoardStatus.Add(pieceSelected.BoxName, Status.Open)
                                End If


                                PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                                PieceCollection3.Remove(pieceSelected.Name)

                                pieceSelectedOld.BoxName = BoardLocator(insBlackMove.StartLocation)
                                pieceSelectedOld.SquareLocation = BoardLocation(BoardLocator(insBlackMove.StartLocation))

                                pieceSelected = New AnyPiece
                                SetPieceSelected(pieceSelectedOld)

                                PieceCollection.Add(pieceSelected.BoxName, pieceSelected)
                                PieceCollection3.Add(pieceSelected.Name, pieceSelected.BoxName)

                                pieceSelected = New AnyPiece
                                SetPieceSelected(PieceCollection(sendername))
                                pieceSelectedOld = New AnyPiece
                                SetPieceSelectedOld(pieceSelected)

                                If BlackKingWasCastling = True Then

                                    If BoardLocation(BoardLocator(insCastledRookMove.StartLocation)).SquareColor = Color1.White Then
                                        pieceSelected2.Image = pieceSelected2.ImageOnWhiteSquare
                                    Else
                                        pieceSelected2.Image = pieceSelected2.ImageOnBlackSquare
                                    End If

                                    BoardImage(BoardLocator(insCastledRookMove.StartLocation)).Source = pieceSelected2.Image

                                    BoardStatus.Remove(BoardLocator(insCastledRookMove.StartLocation))
                                    BoardStatus.Add(BoardLocator(insCastledRookMove.StartLocation), Status.Occupied)

                                    BoardStatus.Remove(pieceSelected2.BoxName)
                                    BoardStatus.Add(pieceSelected2.BoxName, Status.Open)

                                    BoardImage(pieceSelected2.BoxName).Source = BoardLocation(pieceSelected2.BoxName).Image

                                    PieceCollection.Remove(PieceCollection3(pieceSelected2.Name))
                                    PieceCollection3.Remove(pieceSelected2.Name)

                                    pieceSelected2Old.BoxName = BoardLocator(insCastledRookMove.StartLocation)
                                    pieceSelected2Old.SquareLocation = BoardLocation(BoardLocator(insCastledRookMove.StartLocation))

                                    pieceSelected2 = New AnyPiece
                                    SetPieceSelected2(pieceSelected2Old)

                                    PieceCollection.Add(pieceSelected2.BoxName, pieceSelected2)
                                    PieceCollection3.Add(pieceSelected2.Name, pieceSelected2.BoxName)

                                    BlackKingIsCastling = False
                                    BlackKingWasCastling = False


                                End If

                                'Need to select image Source if BoardStatus(sendername) is occupied

                                BoardImage(sendername).Source = BoardLocation(sendername).Image
                                If BoardStatus(sendername) = Status.Occupied Then

                                    If BoardLocation(sendername).SquareColor = Color1.White Then
                                        BoardImage(sendername).Source = pieceSelected.ImageOnWhiteSquare
                                    Else
                                        BoardImage(sendername).Source = pieceSelected.ImageOnBlackSquare
                                    End If

                                End If

                                If timerBlinkSelectedPiece.IsEnabled = True Then
                                    timerBlinkSelectedPiece.Stop()
                                End If

                                WhiteKingIsChecked = False
                                WhiteKingWasChecked = False
                                If timerWhiteKingChecked.IsEnabled = True Then
                                    timerWhiteKingChecked.Stop()
                                End If
                                If insPlayers.WhitePlayer = Player.Player1 Then
                                    Player1Check.DataContext = ""
                                ElseIf insPlayers.WhitePlayer = Player.Player2 Then
                                    Player2Check.DataContext = ""
                                End If


                                If BlackKingWasChecked = True Then
                                    BlackKingIsChecked = True
                                    If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerBlackKingChecked.IsEnabled = False Then
                                        timerBlackKingChecked.Start()
                                    End If
                                End If

                                If PossiblePieceCollection.Contains(pieceSelected.Name) = True Then
                                    HighlightSelectedPieceToMove(pieceSelected.BoxName)
                                End If

                                EnableBox_Tapped()
                                Exit Sub

                            End If
                        End If
                    End If

                    If isLocationToMoveSelected = False Then
                        'Is it possible to move to selected location
                        PieceSelectedPossibleMoves.Clear()
                        For Each move2 In AllPossibleMovesFromAllPossiblePieces
                            insPossibleMove = New PossibleMove
                            insPossibleMove = move2.Value
                            If insPossibleMove.Piece.Name = pieceSelected.Name Then
                                If PieceSelectedPossibleMoves.ContainsKey(insPossibleMove.EndLocation) = False Then
                                    PieceSelectedPossibleMoves.Add(insPossibleMove.EndLocation, insPossibleMove)
                                End If

                            End If
                        Next

                        If PieceSelectedPossibleMoves.ContainsKey(BoardLocation(sendername).Location) Then

                            If BoardStatus(sendername) = Status.Occupied Then
                                If pieceSelected.Piece = Pieces.Pawn And pieceSelected.SquareLocation.column = PieceCollection(sendername).SquareLocation.column Then
                                    EnableBox_Tapped()
                                    Exit Sub
                                End If

                                isPieceCaptured = True


                                If PieceCollection(sendername).Color = Color1.White Then
                                    insWhiteCapturedPiece = New CapturedPiece(PieceCollection(sendername), PieceCollection(sendername).SquareLocation.Location, blackMoveNumber)
                                    If insWhiteCapturedPiece.Piece.CapturedBoxName IsNot Nothing Then
                                        If WhiteCapturedPieces.ContainsKey(insWhiteCapturedPiece.Piece.CapturedBoxName) = False Then
                                            WhiteCapturedPieces.Add(insWhiteCapturedPiece.Piece.CapturedBoxName, insWhiteCapturedPiece)
                                            If CaptureBoardLocation(insWhiteCapturedPiece.Piece.CapturedBoxName).SquareColor = Color1.White Then
                                                ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = insWhiteCapturedPiece.Piece.ImageOnWhiteSquare
                                            Else
                                                ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = insWhiteCapturedPiece.Piece.ImageOnBlackSquare
                                            End If
                                        End If
                                    End If
                                ElseIf PieceCollection(sendername).Color = Color1.Black Then
                                    insBlackCapturedPiece = New CapturedPiece(PieceCollection(sendername), PieceCollection(sendername).SquareLocation.Location, whiteMoveNumber)
                                    If insBlackCapturedPiece.Piece.CapturedBoxName IsNot Nothing Then
                                        If BlackCapturedPieces.ContainsKey(insBlackCapturedPiece.Piece.CapturedBoxName) = False Then
                                            BlackCapturedPieces.Add(insBlackCapturedPiece.Piece.CapturedBoxName, insBlackCapturedPiece)
                                            If CaptureBoardLocation(insBlackCapturedPiece.Piece.CapturedBoxName).SquareColor = Color1.White Then
                                                ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = insBlackCapturedPiece.Piece.ImageOnWhiteSquare
                                            Else
                                                ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = insBlackCapturedPiece.Piece.ImageOnBlackSquare
                                            End If
                                        End If
                                    End If
                                End If

                                BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                                BoardStatus.Remove(pieceSelected.BoxName)
                                BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                                BoardImage(sendername).Source = BoardLocation(sendername).Image

                                BoardStatus.Remove(sendername)
                                BoardStatus.Add(sendername, Status.Open)

                                If timerBlinkSelectedPiece.IsEnabled = True Then
                                    timerBlinkSelectedPiece.Stop()
                                End If

                                PieceCollection3.Remove(PieceCollection(sendername).Name)
                                PieceCollection.Remove(sendername)

                                PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                                PieceCollection3.Remove(pieceSelected.Name)

                                If BlackKingWasChecked = True Then
                                    BlackKingIsChecked = True
                                    If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerBlackKingChecked.IsEnabled = False Then
                                        timerBlackKingChecked.Start()
                                    End If
                                End If

                                If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = False Then
                                    WhiteKingIsChecked = False
                                    WhiteKingWasChecked = False
                                    If timerWhiteKingChecked.IsEnabled = True Then
                                        timerWhiteKingChecked.Stop()
                                    End If
                                    If insPlayers.BlackPlayer = Player.Player1 Then
                                        Player1Check.DataContext = ""
                                    ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                        Player2Check.DataContext = ""
                                    End If
                                ElseIf PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                    WhiteKingWasChecked = True
                                    WhiteKingIsChecked = True
                                    If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerWhiteKingChecked.IsEnabled = False Then
                                        timerWhiteKingChecked.Start()
                                    End If
                                End If




                                HightlightMoveLocation(sendername, pieceSelected)
                                EnableBox_Tapped()
                                Exit Sub

                            ElseIf BoardStatus(sendername) = Status.Open Then

                                changeTurn.IsEnabled = False
                                NewGame.IsEnabled = False


                                BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                                BoardStatus.Remove(pieceSelected.BoxName)
                                BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                                If timerBlinkSelectedPiece.IsEnabled = True Then
                                    timerBlinkSelectedPiece.Stop()
                                End If

                                PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                                PieceCollection3.Remove(pieceSelected.Name)

                                If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).PieceIsCaptured = True Then

                                    isPieceCaptured = True

                                    insWhiteCapturedPiece = New CapturedPiece(PieceCollection(CapturedWhitePieces(BoardLocation(sendername).Location).BoxName), PieceCollection(CapturedWhitePieces(BoardLocation(sendername).Location).BoxName).SquareLocation.Location, blackMoveNumber)
                                    If WhiteCapturedPieces.ContainsKey(insWhiteCapturedPiece.Piece.CapturedBoxName) Then
                                        WhiteCapturedPieces.Add(insWhiteCapturedPiece.Piece.CapturedBoxName, insWhiteCapturedPiece)
                                        ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = insWhiteCapturedPiece.Piece.OriginalImage
                                    End If
                                    BoardImage(CapturedWhitePieces(BoardLocation(sendername).Location).BoxName).Source = BoardLocation(CapturedWhitePieces(BoardLocation(sendername).Location).BoxName).Image

                                    BoardStatus.Remove(CapturedWhitePieces(BoardLocation(sendername).Location).BoxName)
                                    BoardStatus.Add(CapturedWhitePieces(BoardLocation(sendername).Location).BoxName, Status.Open)

                                End If

                                If pieceSelected.Piece = Pieces.King And pieceSelected.MoveNumber = 1 Then
                                    If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingCanCastleToQueenSide = True Then

                                        If insPlayers.BlackPlayer = Player.Player1 Then
                                            pieceSelected2 = New AnyPiece
                                            SetPieceSelected2(PieceCollection("Box_88"))
                                            pieceSelected2Old = New AnyPiece
                                            SetPieceSelected2Old(PieceCollection("Box_88"))
                                        ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                            pieceSelected2 = New AnyPiece
                                            SetPieceSelected2(PieceCollection("Box_11"))
                                            pieceSelected2Old = New AnyPiece
                                            SetPieceSelected2Old(PieceCollection("Box_11"))
                                        End If


                                        BoardImage(pieceSelected2.BoxName).Source = BoardLocation(pieceSelected2.BoxName).Image

                                        BoardStatus.Remove(pieceSelected2.BoxName)
                                        BoardStatus.Add(pieceSelected2.BoxName, Status.Open)

                                        PieceCollection.Remove(PieceCollection3(pieceSelected2.Name))
                                        PieceCollection3.Remove(pieceSelected2.Name)

                                        If AllPossibleCastledRookMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                            WhiteKingWasChecked = True
                                        End If

                                        BlackKingIsCastling = True
                                        BlackKingWasCastling = True

                                    ElseIf PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingCanCastleToKingSide = True Then

                                        If insPlayers.BlackPlayer = Player.Player1 Then
                                            pieceSelected2 = New AnyPiece
                                            SetPieceSelected2(PieceCollection("Box_81"))
                                            pieceSelected2Old = New AnyPiece
                                            SetPieceSelected2Old(PieceCollection("Box_81"))
                                        ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                            pieceSelected2 = New AnyPiece
                                            SetPieceSelected2(PieceCollection("Box_18"))
                                            pieceSelected2Old = New AnyPiece
                                            SetPieceSelected2Old(PieceCollection("Box_18"))
                                        End If

                                        BoardImage(pieceSelected2.BoxName).Source = BoardLocation(pieceSelected2.BoxName).Image

                                        BoardStatus.Remove(pieceSelected2.BoxName)
                                        BoardStatus.Add(pieceSelected2.BoxName, Status.Open)

                                        PieceCollection.Remove(PieceCollection3(pieceSelected2.Name))

                                        PieceCollection3.Remove(pieceSelected2.Name)
                                        If AllPossibleCastledRookMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                            WhiteKingWasChecked = True
                                        End If
                                        BlackKingIsCastling = True
                                        BlackKingWasCastling = True
                                    End If
                                End If

                                If BlackKingWasChecked = True Then
                                    BlackKingIsChecked = True
                                    If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerBlackKingChecked.IsEnabled = False Then
                                        timerBlackKingChecked.Start()
                                    End If
                                End If

                                If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = False Then
                                    WhiteKingIsChecked = False
                                    WhiteKingWasChecked = False
                                    If timerWhiteKingChecked.IsEnabled = True Then
                                        timerWhiteKingChecked.Stop()
                                    End If
                                    If insPlayers.BlackPlayer = Player.Player1 Then
                                        Player1Check.DataContext = ""
                                    ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                        Player2Check.DataContext = ""
                                    End If
                                ElseIf PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                    WhiteKingWasChecked = True
                                    WhiteKingIsChecked = True
                                    If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerWhiteKingChecked.IsEnabled = False Then
                                        timerWhiteKingChecked.Start()
                                    End If
                                End If

                                HightlightMoveLocation(sendername, pieceSelected)
                                EnableBox_Tapped()
                                Exit Sub
                            End If

                        ElseIf Not (PieceSelectedPossibleMoves.ContainsKey(BoardLocation(sendername).Location)) Then

                            EnableBox_Tapped()
                            Exit Sub
                        End If

                    ElseIf isLocationToMoveSelected = True Then

                        PieceSelectedPossibleMoves.Clear()
                        For Each move2 In AllPossibleMovesFromAllPossiblePieces
                            insPossibleMove = New PossibleMove
                            insPossibleMove = move2.Value
                            If insPossibleMove.Piece.Name = pieceSelected.Name Then
                                If PieceSelectedPossibleMoves.ContainsKey(insPossibleMove.EndLocation) = False Then
                                    PieceSelectedPossibleMoves.Add(insPossibleMove.EndLocation, insPossibleMove)
                                End If

                            End If
                        Next

                        If PieceSelectedPossibleMoves.ContainsKey(BoardLocation(sendername).Location) = False Then
                            EnableBox_Tapped()
                            Exit Sub

                        ElseIf PieceSelectedPossibleMoves.ContainsKey(BoardLocation(sendername).Location) = True Then
                            isPieceSelected = False
                            isLocationToMoveSelected = False
                            changeTurn.IsEnabled = False


                            If BoardLocation(PieceCollection3(pieceSelected.Name)).SquareColor = Color1.White Then
                                pieceSelected.Image = pieceSelected.ImageOnWhiteSquare
                            Else
                                pieceSelected.Image = pieceSelected.ImageOnBlackSquare
                            End If

                            BoardImage(BoardLocator(insBlackMove.StartLocation)).Source = pieceSelected.Image

                            BoardStatus.Remove(BoardLocator(insBlackMove.StartLocation))
                            BoardStatus.Add(BoardLocator(insBlackMove.StartLocation), Status.Occupied)

                            BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                            BoardStatus.Remove(pieceSelected.BoxName)
                            BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                            If timerBlinkSelectedPiece.IsEnabled = True Then
                                timerBlinkSelectedPiece.Stop()
                            End If

                            PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                            PieceCollection3.Remove(pieceSelected.Name)

                            pieceSelectedOld.BoxName = BoardLocator(insBlackMove.StartLocation)
                            pieceSelectedOld.SquareLocation = BoardLocation(BoardLocator(insBlackMove.StartLocation))

                            pieceSelected = New AnyPiece
                            SetPieceSelected(pieceSelectedOld)

                            PieceCollection.Add(pieceSelected.BoxName, pieceSelected)
                            PieceCollection3.Add(pieceSelected.Name, pieceSelected.BoxName)

                            If BlackKingWasCastling = True Then

                                If BoardLocation(BoardLocator(insCastledRookMove.StartLocation)).SquareColor = Color1.White Then
                                    pieceSelected2.Image = pieceSelected2.ImageOnWhiteSquare
                                Else
                                    pieceSelected2.Image = pieceSelected2.ImageOnBlackSquare
                                End If

                                BoardImage(BoardLocator(insCastledRookMove.StartLocation)).Source = pieceSelected2.Image

                                BoardStatus.Remove(BoardLocator(insCastledRookMove.StartLocation))
                                BoardStatus.Add(BoardLocator(insCastledRookMove.StartLocation), Status.Occupied)

                                BoardStatus.Remove(pieceSelected2.BoxName)
                                BoardStatus.Add(pieceSelected2.BoxName, Status.Open)

                                BoardImage(pieceSelected2.BoxName).Source = BoardLocation(pieceSelected2.BoxName).Image

                                PieceCollection.Remove(PieceCollection3(pieceSelected2.Name))
                                PieceCollection3.Remove(pieceSelected2.Name)

                                pieceSelected2Old.BoxName = BoardLocator(insCastledRookMove.StartLocation)
                                pieceSelected2Old.SquareLocation = BoardLocation(BoardLocator(insCastledRookMove.StartLocation))

                                pieceSelected2 = New AnyPiece
                                SetPieceSelected2(pieceSelected2Old)

                                PieceCollection.Add(pieceSelected2.BoxName, pieceSelected2)
                                PieceCollection3.Add(pieceSelected2.Name, pieceSelected2.BoxName)

                                BlackKingIsCastling = False
                                BlackKingWasCastling = False

                            End If

                            If BlackKingWasChecked = True Then
                                BlackKingIsChecked = True
                                If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                    If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                        PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                                    End If
                                End If
                                If timerBlackKingChecked.IsEnabled = False Then
                                    timerBlackKingChecked.Start()
                                End If
                            End If

                            WhiteKingIsChecked = False
                            WhiteKingWasChecked = False
                            If timerWhiteKingChecked.IsEnabled = True Then
                                timerWhiteKingChecked.Stop()
                            End If
                            If insPlayers.WhitePlayer = Player.Player1 Then
                                Player1Check.DataContext = ""
                            ElseIf insPlayers.WhitePlayer = Player.Player2 Then
                                Player2Check.DataContext = ""
                            End If
                            If isPieceCaptured = True Then

                                isPieceCaptured = False

                                WhiteCapturedPieces.Remove(insWhiteCapturedPiece.Piece.CapturedBoxName)
                                ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = CaptureBoardLocation(insWhiteCapturedPiece.Piece.CapturedBoxName).Image

                                PieceCollection.Add(BoardLocator(insWhiteCapturedPiece.CaptureLocation), insWhiteCapturedPiece.Piece)
                                PieceCollection3.Add(insWhiteCapturedPiece.Piece.Name, BoardLocator(insWhiteCapturedPiece.CaptureLocation))

                                BoardStatus.Remove(BoardLocator(insWhiteCapturedPiece.CaptureLocation))
                                BoardStatus.Add(BoardLocator(insWhiteCapturedPiece.CaptureLocation), Status.Occupied)

                                If BoardLocation(BoardLocator(insWhiteCapturedPiece.CaptureLocation)).SquareColor = Color1.White Then
                                    insImage = New Image
                                    insImage = BoardImage(BoardLocator(insWhiteCapturedPiece.CaptureLocation))
                                    insImage.Source = PieceCollection(BoardLocator(insWhiteCapturedPiece.CaptureLocation)).ImageOnWhiteSquare
                                    BoardImage.Remove(BoardLocator(insWhiteCapturedPiece.CaptureLocation))
                                    BoardImage.Add(BoardLocator(insWhiteCapturedPiece.CaptureLocation), insImage)
                                ElseIf BoardLocation(BoardLocator(insWhiteCapturedPiece.CaptureLocation)).SquareColor = Color1.Black Then
                                    insImage = New Image
                                    insImage = BoardImage(BoardLocator(insWhiteCapturedPiece.CaptureLocation))
                                    insImage.Source = PieceCollection(BoardLocator(insWhiteCapturedPiece.CaptureLocation)).ImageOnBlackSquare
                                    BoardImage.Remove(BoardLocator(insWhiteCapturedPiece.CaptureLocation))
                                    BoardImage.Add(BoardLocator(insWhiteCapturedPiece.CaptureLocation), insImage)
                                End If
                            End If

                            isPieceSelected = True

                            If BoardStatus(sendername) = Status.Occupied Then
                                If pieceSelected.Piece = Pieces.Pawn And pieceSelected.SquareLocation.column = PieceCollection(sendername).SquareLocation.column Then

                                    EnableBox_Tapped()
                                    Exit Sub
                                End If

                                isPieceCaptured = True


                                If PieceCollection(sendername).Color = Color1.White Then
                                    insWhiteCapturedPiece = New CapturedPiece(PieceCollection(sendername), PieceCollection(sendername).SquareLocation.Location, blackMoveNumber)
                                    If insWhiteCapturedPiece.Piece.CapturedBoxName IsNot Nothing Then
                                        If WhiteCapturedPieces.ContainsKey(insWhiteCapturedPiece.Piece.CapturedBoxName) = False Then
                                            WhiteCapturedPieces.Add(insWhiteCapturedPiece.Piece.CapturedBoxName, insWhiteCapturedPiece)
                                            If CaptureBoardLocation(insWhiteCapturedPiece.Piece.CapturedBoxName).SquareColor = Color1.White Then
                                                ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = insWhiteCapturedPiece.Piece.ImageOnWhiteSquare
                                            Else
                                                ImagesOfCapturedPieces(insWhiteCapturedPiece.Piece.CapturedBoxName).Source = insWhiteCapturedPiece.Piece.ImageOnBlackSquare
                                            End If
                                        End If
                                    End If
                                ElseIf PieceCollection(sendername).Color = Color1.Black Then
                                    insBlackCapturedPiece = New CapturedPiece(PieceCollection(sendername), PieceCollection(sendername).SquareLocation.Location, whiteMoveNumber)
                                    If insBlackCapturedPiece.Piece.CapturedBoxName IsNot Nothing Then
                                        If BlackCapturedPieces.ContainsKey(insBlackCapturedPiece.Piece.CapturedBoxName) = False Then
                                            BlackCapturedPieces.Add(insBlackCapturedPiece.Piece.CapturedBoxName, insBlackCapturedPiece)
                                            If CaptureBoardLocation(insBlackCapturedPiece.Piece.CapturedBoxName).SquareColor = Color1.White Then
                                                ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = insBlackCapturedPiece.Piece.ImageOnWhiteSquare
                                            Else
                                                ImagesOfCapturedPieces(insBlackCapturedPiece.Piece.CapturedBoxName).Source = insBlackCapturedPiece.Piece.ImageOnBlackSquare
                                            End If
                                        End If
                                    End If
                                End If

                                BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                                BoardStatus.Remove(pieceSelected.BoxName)
                                BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                                BoardImage(sendername).Source = BoardLocation(sendername).Image

                                BoardStatus.Remove(sendername)
                                BoardStatus.Add(sendername, Status.Open)

                                If timerBlinkSelectedPiece.IsEnabled = True Then
                                    timerBlinkSelectedPiece.Stop()
                                End If

                                PieceCollection3.Remove(PieceCollection(sendername).Name)
                                PieceCollection.Remove(sendername)

                                PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                                PieceCollection3.Remove(pieceSelected.Name)

                                If BlackKingWasChecked = True Then
                                    BlackKingIsChecked = True
                                    If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerBlackKingChecked.IsEnabled = False Then
                                        timerBlackKingChecked.Start()
                                    End If
                                End If

                                If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = False Then
                                    WhiteKingIsChecked = False
                                    WhiteKingWasChecked = False
                                    If timerWhiteKingChecked.IsEnabled = True Then
                                        timerWhiteKingChecked.Stop()
                                    End If
                                    If insPlayers.BlackPlayer = Player.Player1 Then
                                        Player1Check.DataContext = ""
                                    ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                        Player2Check.DataContext = ""
                                    End If
                                ElseIf PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                    WhiteKingWasChecked = True
                                    WhiteKingIsChecked = True
                                    If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerWhiteKingChecked.IsEnabled = False Then
                                        timerWhiteKingChecked.Start()
                                    End If
                                End If
                                HightlightMoveLocation(sendername, pieceSelected)
                                EnableBox_Tapped()
                                Exit Sub

                            ElseIf BoardStatus(sendername) = Status.Open Then

                                changeTurn.IsEnabled = False
                                NewGame.IsEnabled = False


                                BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image

                                BoardStatus.Remove(pieceSelected.BoxName)
                                BoardStatus.Add(pieceSelected.BoxName, Status.Open)

                                If timerBlinkSelectedPiece.IsEnabled = True Then
                                    timerBlinkSelectedPiece.Stop()
                                End If

                                PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
                                PieceCollection3.Remove(pieceSelected.Name)

                                If BlackKingWasChecked = True Then
                                    BlackKingIsChecked = True
                                    If PossiblePiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingBlackKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingBlackKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingBlackKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerBlackKingChecked.IsEnabled = False Then
                                        timerBlackKingChecked.Start()
                                    End If
                                End If

                                If PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = False Then
                                    WhiteKingIsChecked = False
                                    WhiteKingWasChecked = False
                                    If timerWhiteKingChecked.IsEnabled = True Then
                                        timerWhiteKingChecked.Stop()
                                    End If
                                    If insPlayers.BlackPlayer = Player.Player1 Then
                                        Player1Check.DataContext = ""
                                    ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                                        Player2Check.DataContext = ""
                                    End If
                                ElseIf PieceSelectedPossibleMoves(BoardLocation(sendername).Location).KingIsChecked = True Then
                                    WhiteKingWasChecked = True
                                    WhiteKingIsChecked = True
                                    If PossiblePiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = True Then
                                        If PiecesCheckingWhiteKing.ContainsKey(BoardLocation(sendername).Location) = False Then
                                            PiecesCheckingWhiteKing.Add(BoardLocation(sendername).Location, PossiblePiecesCheckingWhiteKing(BoardLocation(sendername).Location))
                                        End If
                                    End If
                                    If timerWhiteKingChecked.IsEnabled = False Then
                                        timerWhiteKingChecked.Start()
                                    End If
                                End If

                                HightlightMoveLocation(sendername, pieceSelected)
                                EnableBox_Tapped()
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If
        End If

    End Sub
    Private Sub DisableBox_Tapped()
        Box_11.IsTapEnabled = False
        Box_12.IsTapEnabled = False
        Box_13.IsTapEnabled = False
        Box_14.IsTapEnabled = False
        Box_11.IsTapEnabled = False
        Box_12.IsTapEnabled = False
        Box_13.IsTapEnabled = False
        Box_14.IsTapEnabled = False

        Box_21.IsTapEnabled = False
        Box_22.IsTapEnabled = False
        Box_23.IsTapEnabled = False
        Box_24.IsTapEnabled = False
        Box_21.IsTapEnabled = False
        Box_22.IsTapEnabled = False
        Box_23.IsTapEnabled = False
        Box_24.IsTapEnabled = False

        Box_31.IsTapEnabled = False
        Box_32.IsTapEnabled = False
        Box_33.IsTapEnabled = False
        Box_34.IsTapEnabled = False
        Box_31.IsTapEnabled = False
        Box_32.IsTapEnabled = False
        Box_33.IsTapEnabled = False
        Box_34.IsTapEnabled = False

        Box_41.IsTapEnabled = False
        Box_42.IsTapEnabled = False
        Box_43.IsTapEnabled = False
        Box_44.IsTapEnabled = False
        Box_41.IsTapEnabled = False
        Box_42.IsTapEnabled = False
        Box_43.IsTapEnabled = False
        Box_44.IsTapEnabled = False

        Box_51.IsTapEnabled = False
        Box_52.IsTapEnabled = False
        Box_53.IsTapEnabled = False
        Box_54.IsTapEnabled = False
        Box_51.IsTapEnabled = False
        Box_52.IsTapEnabled = False
        Box_53.IsTapEnabled = False
        Box_54.IsTapEnabled = False

        Box_61.IsTapEnabled = False
        Box_62.IsTapEnabled = False
        Box_63.IsTapEnabled = False
        Box_64.IsTapEnabled = False
        Box_61.IsTapEnabled = False
        Box_62.IsTapEnabled = False
        Box_63.IsTapEnabled = False
        Box_64.IsTapEnabled = False

        Box_71.IsTapEnabled = False
        Box_72.IsTapEnabled = False
        Box_73.IsTapEnabled = False
        Box_74.IsTapEnabled = False
        Box_71.IsTapEnabled = False
        Box_72.IsTapEnabled = False
        Box_73.IsTapEnabled = False
        Box_74.IsTapEnabled = False

        Box_81.IsTapEnabled = False
        Box_82.IsTapEnabled = False
        Box_83.IsTapEnabled = False
        Box_84.IsTapEnabled = False
        Box_81.IsTapEnabled = False
        Box_82.IsTapEnabled = False
        Box_83.IsTapEnabled = False
        Box_84.IsTapEnabled = False



    End Sub
    Private Sub EnableBox_Tapped()
        Box_11.IsTapEnabled = True
        Box_12.IsTapEnabled = True
        Box_13.IsTapEnabled = True
        Box_14.IsTapEnabled = True
        Box_11.IsTapEnabled = True
        Box_12.IsTapEnabled = True
        Box_13.IsTapEnabled = True
        Box_14.IsTapEnabled = True

        Box_21.IsTapEnabled = True
        Box_22.IsTapEnabled = True
        Box_23.IsTapEnabled = True
        Box_24.IsTapEnabled = True
        Box_21.IsTapEnabled = True
        Box_22.IsTapEnabled = True
        Box_23.IsTapEnabled = True
        Box_24.IsTapEnabled = True

        Box_31.IsTapEnabled = True
        Box_32.IsTapEnabled = True
        Box_33.IsTapEnabled = True
        Box_34.IsTapEnabled = True
        Box_31.IsTapEnabled = True
        Box_32.IsTapEnabled = True
        Box_33.IsTapEnabled = True
        Box_34.IsTapEnabled = True

        Box_41.IsTapEnabled = True
        Box_42.IsTapEnabled = True
        Box_43.IsTapEnabled = True
        Box_44.IsTapEnabled = True
        Box_41.IsTapEnabled = True
        Box_42.IsTapEnabled = True
        Box_43.IsTapEnabled = True
        Box_44.IsTapEnabled = True

        Box_51.IsTapEnabled = True
        Box_52.IsTapEnabled = True
        Box_53.IsTapEnabled = True
        Box_54.IsTapEnabled = True
        Box_51.IsTapEnabled = True
        Box_52.IsTapEnabled = True
        Box_53.IsTapEnabled = True
        Box_54.IsTapEnabled = True

        Box_61.IsTapEnabled = True
        Box_62.IsTapEnabled = True
        Box_63.IsTapEnabled = True
        Box_64.IsTapEnabled = True
        Box_61.IsTapEnabled = True
        Box_62.IsTapEnabled = True
        Box_63.IsTapEnabled = True
        Box_64.IsTapEnabled = True

        Box_71.IsTapEnabled = True
        Box_72.IsTapEnabled = True
        Box_73.IsTapEnabled = True
        Box_74.IsTapEnabled = True
        Box_71.IsTapEnabled = True
        Box_72.IsTapEnabled = True
        Box_73.IsTapEnabled = True
        Box_74.IsTapEnabled = True

        Box_81.IsTapEnabled = True
        Box_82.IsTapEnabled = True
        Box_83.IsTapEnabled = True
        Box_84.IsTapEnabled = True
        Box_81.IsTapEnabled = True
        Box_82.IsTapEnabled = True
        Box_83.IsTapEnabled = True
        Box_84.IsTapEnabled = True

    End Sub
    Private Sub SetPieceSelected(ByVal pieceselected1 As AnyPiece)

        pieceSelected.BoxName = pieceselected1.BoxName
        pieceSelected.CapturedBoxName = pieceselected1.CapturedBoxName
        pieceSelected.Color = pieceselected1.Color
        pieceSelected.Image = pieceselected1.Image
        pieceSelected.ImageOnBlackSquare = pieceselected1.ImageOnBlackSquare
        pieceSelected.ImageOnWhiteSquare = pieceselected1.ImageOnWhiteSquare
        pieceSelected.MoveNumber = pieceselected1.MoveNumber
        pieceSelected.Name = pieceselected1.Name
        pieceSelected.OriginalImage = pieceselected1.OriginalImage
        pieceSelected.Piece = pieceselected1.Piece
        pieceSelected.SquareLocation = pieceselected1.SquareLocation
        pieceSelected.Symbol = pieceselected1.Symbol

    End Sub

    Private Sub SetPieceSelectedOld(ByVal pieceSelected1 As AnyPiece)
        pieceSelectedOld.BoxName = pieceSelected1.BoxName
        pieceSelectedOld.CapturedBoxName = pieceSelected1.CapturedBoxName
        pieceSelectedOld.Color = pieceSelected1.Color
        pieceSelectedOld.Image = pieceSelected1.Image
        pieceSelectedOld.ImageOnBlackSquare = pieceSelected1.ImageOnBlackSquare
        pieceSelectedOld.ImageOnWhiteSquare = pieceSelected1.ImageOnWhiteSquare
        pieceSelectedOld.MoveNumber = pieceSelected1.MoveNumber
        pieceSelectedOld.Name = pieceSelected1.Name
        pieceSelectedOld.OriginalImage = pieceSelected1.OriginalImage
        pieceSelectedOld.Piece = pieceSelected1.Piece
        pieceSelectedOld.SquareLocation = pieceSelected1.SquareLocation
        pieceSelectedOld.Symbol = pieceSelected1.Symbol

    End Sub
    Private Sub SetPieceSelected2(ByVal pieceSelected1 As AnyPiece)
        pieceSelected2.BoxName = pieceSelected1.BoxName
        pieceSelected2.CapturedBoxName = pieceSelected1.CapturedBoxName
        pieceSelected2.Color = pieceSelected1.Color
        pieceSelected2.Image = pieceSelected1.Image
        pieceSelected2.ImageOnBlackSquare = pieceSelected1.ImageOnBlackSquare
        pieceSelected2.ImageOnWhiteSquare = pieceSelected1.ImageOnWhiteSquare
        pieceSelected2.MoveNumber = pieceSelected1.MoveNumber
        pieceSelected2.Name = pieceSelected1.Name
        pieceSelected2.OriginalImage = pieceSelected1.OriginalImage
        pieceSelected2.Piece = pieceSelected1.Piece
        pieceSelected2.SquareLocation = pieceSelected1.SquareLocation
        pieceSelected2.Symbol = pieceSelected1.Symbol

    End Sub

    Private Sub SetPieceSelected2Old(ByVal pieceSelected1 As AnyPiece)
        pieceSelected2Old.BoxName = pieceSelected1.BoxName
        pieceSelected2Old.CapturedBoxName = pieceSelected1.CapturedBoxName
        pieceSelected2Old.Color = pieceSelected1.Color
        pieceSelected2Old.Image = pieceSelected1.Image
        pieceSelected2Old.ImageOnBlackSquare = pieceSelected1.ImageOnBlackSquare
        pieceSelected2Old.ImageOnWhiteSquare = pieceSelected1.ImageOnWhiteSquare
        pieceSelected2Old.MoveNumber = pieceSelected1.MoveNumber
        pieceSelected2Old.Name = pieceSelected1.Name
        pieceSelected2Old.OriginalImage = pieceSelected1.OriginalImage
        pieceSelected2Old.Piece = pieceSelected1.Piece
        pieceSelected2Old.SquareLocation = pieceSelected1.SquareLocation
        pieceSelected2Old.Symbol = pieceSelected1.Symbol

    End Sub
    Private Function SetLocationPiece(ByVal pieceselected1 As AnyPiece) As AnyPiece

        locationpiece2 = New AnyPiece

        locationpiece2.BoxName = pieceselected1.BoxName
        locationpiece2.CapturedBoxName = pieceselected1.CapturedBoxName
        locationpiece2.Color = pieceselected1.Color
        locationpiece2.Image = pieceselected1.Image
        locationpiece2.ImageOnBlackSquare = pieceselected1.ImageOnBlackSquare
        locationpiece2.ImageOnWhiteSquare = pieceselected1.ImageOnWhiteSquare
        locationpiece2.MoveNumber = pieceselected1.MoveNumber
        locationpiece2.Name = pieceselected1.Name
        locationpiece2.OriginalImage = pieceselected1.OriginalImage
        locationpiece2.Piece = pieceselected1.Piece
        locationpiece2.SquareLocation = pieceselected1.SquareLocation
        locationpiece2.Symbol = pieceselected1.Symbol

        SetLocationPiece = locationpiece2

    End Function

    Private Function SetCapturedPieceForMove(ByVal pieceselected1 As AnyPiece) As AnyPiece

        locationpiece2 = New AnyPiece

        locationpiece2.BoxName = pieceselected1.BoxName
        locationpiece2.CapturedBoxName = pieceselected1.CapturedBoxName
        locationpiece2.Color = pieceselected1.Color
        locationpiece2.Image = pieceselected1.Image
        locationpiece2.ImageOnBlackSquare = pieceselected1.ImageOnBlackSquare
        locationpiece2.ImageOnWhiteSquare = pieceselected1.ImageOnWhiteSquare
        locationpiece2.MoveNumber = pieceselected1.MoveNumber
        locationpiece2.Name = pieceselected1.Name
        locationpiece2.OriginalImage = pieceselected1.OriginalImage
        locationpiece2.Piece = pieceselected1.Piece
        locationpiece2.SquareLocation = pieceselected1.SquareLocation
        locationpiece2.Symbol = pieceselected1.Symbol

        SetCapturedPieceForMove = locationpiece2

    End Function

    Private Sub CalculatePossibleMoves()
        HowManyPiecesAreCheckingKing()

        If insTurn.Color = Color1.White Then
            PossiblePiecesCheckingBlackKing.Clear()
            PiecesCheckingBlackKing.Clear()
            BlackKingIsChecked = False
            BlackKingWasChecked = False
            CapturedBlackPieces.Clear()
        ElseIf insTurn.Color = Color1.Black Then
            PossiblePiecesCheckingWhiteKing.Clear()
            PiecesCheckingWhiteKing.Clear()
            WhiteKingIsChecked = False
            WhiteKingWasChecked = False
            CapturedWhitePieces.Clear()
        End If

        PieceSelectedPossibleMoves.Clear()
        AllPossibleMovesFromAllPossiblePieces.Clear()
        AllOpenSpacesInBetweenKingAndPiecesCheckingKing.Clear()
        AllPossibleCastledRookMoves.Clear()

        For Each piece1 In PieceCollection

            If piece1.Value.Color = insTurn.Color Then

                If piece1.Value.Piece = Pieces.Pawn Then

                    PieceSelectedPossibleMoves.Clear()
                    insPawnPossibleMoves = New PawnPossibleMoves
                    PieceSelectedCanMove = CanPieceSelectedMove(piece1.Value)

                    For Each move In PieceSelectedPossibleMoves
                        insPossibleMove = New PossibleMove
                        insPossibleMove = move.Value
                        AllPossibleMovesFromAllPossiblePieces.Add(insPossibleMove.Piece.Name + insPossibleMove.EndLocation, insPossibleMove)

                    Next

                    If PieceSelectedPossibleMoves.Count > 0 Then
                        If PieceSelectedPossibleMoves.ContainsKey(insPossibleMove.EndLocation) = True Then
                            If PieceSelectedPossibleMoves(insPossibleMove.EndLocation).PieceIsCaptured = True Then
                                PieceIsCaptured = False
                                PieceSelectedCanMove = False
                            End If
                        End If
                    End If

                    If PieceSelectedPossibleMoves.ContainsKey(insPossibleMove.EndLocation) = False Then
                        If PieceSelectedCanMove = True Then
                            insPawnPossibleMoves = New PawnPossibleMoves(piece1.Value, insPlayers)
                        ElseIf PieceSelectedCanMove = False Then
                            If timerBlinkSelectedPiece.IsEnabled = True Then
                                timerBlinkSelectedPiece.Stop()
                            End If
                        End If
                    End If

                ElseIf piece1.Value.Piece = Pieces.Rook Then
                    PieceSelectedPossibleMoves.Clear()
                    insRookPossibleMoves = New RookPossibleMoves
                    PieceSelectedCanMove = CanPieceSelectedMove(piece1.Value)

                    For Each move In PieceSelectedPossibleMoves
                        insPossibleMove = New PossibleMove
                        insPossibleMove = move.Value
                        AllPossibleMovesFromAllPossiblePieces.Add(insPossibleMove.Piece.Name + insPossibleMove.EndLocation, insPossibleMove)

                    Next

                    If PieceSelectedPossibleMoves.Count > 0 Then
                        If PieceSelectedPossibleMoves.ContainsKey(insPossibleMove.EndLocation) = True Then
                            If PieceSelectedPossibleMoves(insPossibleMove.EndLocation).PieceIsCaptured = True Then
                                PieceIsCaptured = False
                                PieceSelectedCanMove = False
                            End If
                        End If
                    End If


                    If PieceSelectedCanMove = True Then
                        insRookPossibleMoves = New RookPossibleMoves(piece1.Value, insPlayers, WhiteKingIsChecked, BlackKingIsChecked)
                    ElseIf PieceSelectedCanMove = False Then
                        If timerBlinkSelectedPiece.IsEnabled = True Then
                            timerBlinkSelectedPiece.Stop()
                        End If
                    End If


                ElseIf piece1.Value.Piece = Pieces.Knight Then
                    PieceSelectedPossibleMoves.Clear()

                    PieceSelectedCanMove = CanPieceSelectedMove(piece1.Value)

                    If PieceSelectedCanMove = True Then
                        insKnightPossibleMoves = New KnightPossibleMoves(piece1.Value, insPlayers, WhiteKingIsChecked, BlackKingIsChecked)
                    Else
                        If timerBlinkSelectedPiece.IsEnabled = True Then
                            timerBlinkSelectedPiece.Stop()
                        End If
                    End If

                ElseIf piece1.Value.Piece = Pieces.Bishop Then
                    PieceSelectedPossibleMoves.Clear()
                    insBishopPossibleMoves = New BishopPossibleMoves
                    PieceSelectedCanMove = CanPieceSelectedMove(piece1.Value)

                    For Each move In PieceSelectedPossibleMoves
                        insPossibleMove = New PossibleMove
                        insPossibleMove = move.Value
                        AllPossibleMovesFromAllPossiblePieces.Add(insPossibleMove.Piece.Name + insPossibleMove.EndLocation, insPossibleMove)

                    Next

                    If PieceSelectedPossibleMoves.Count > 0 Then
                        If PieceSelectedPossibleMoves.ContainsKey(insPossibleMove.EndLocation) = True Then
                            If PieceSelectedPossibleMoves(insPossibleMove.EndLocation).PieceIsCaptured = True Then
                                PieceIsCaptured = False
                                PieceSelectedCanMove = False
                            End If
                        End If
                    End If

                    If PieceSelectedCanMove = True Then
                        insBishopPossibleMoves = New BishopPossibleMoves(piece1.Value, insPlayers, WhiteKingIsChecked, BlackKingIsChecked)
                    Else
                        If timerBlinkSelectedPiece.IsEnabled = True Then
                            timerBlinkSelectedPiece.Stop()
                        End If
                    End If

                ElseIf piece1.Value.Piece = Pieces.Queen Then
                    PieceSelectedPossibleMoves.Clear()
                    insQueenPossibleMoves = New QueenPossibleMoves
                    PieceSelectedCanMove = CanPieceSelectedMove(piece1.Value)

                    For Each move In PieceSelectedPossibleMoves
                        insPossibleMove = New PossibleMove
                        insPossibleMove = move.Value
                        AllPossibleMovesFromAllPossiblePieces.Add(insPossibleMove.Piece.Name + insPossibleMove.EndLocation, insPossibleMove)

                    Next

                    If PieceSelectedPossibleMoves.Count > 0 Then
                        If PieceSelectedPossibleMoves.ContainsKey(insPossibleMove.EndLocation) = True Then
                            If PieceSelectedPossibleMoves(insPossibleMove.EndLocation).PieceIsCaptured = True Then
                                PieceIsCaptured = False
                                PieceSelectedCanMove = False
                            End If
                        End If
                    End If

                    If PieceSelectedCanMove = True Then
                        insQueenPossibleMoves = New QueenPossibleMoves(piece1.Value, insPlayers, WhiteKingIsChecked, BlackKingIsChecked)
                    Else
                        If timerBlinkSelectedPiece.IsEnabled = True Then
                            timerBlinkSelectedPiece.Stop()
                        End If
                    End If

                ElseIf piece1.Value.Piece = Pieces.King Then
                    PieceSelectedPossibleMoves.Clear()
                    insKingPossibleMoves = New KingPossibleMoves(piece1.Value, insPlayers)

                End If
            End If
        Next

        If PieceSelectedPossibleMoves.Count = 0 Then
            'CheckMate!!!!!!!!!!
            If timerBlinkSelectedPiece.IsEnabled = True Then
                timerBlinkSelectedPiece.Stop()

            End If
        End If

        Exit Sub

    End Sub
    Private Sub HowManyPiecesAreCheckingKing()

        Dim row1 As Integer
        Dim column1 As Integer
        Dim number As Integer

        Dim rowdifference As Integer

        If insPlayers.WhitePlayer = Player.Player1 Then
            If insTurn.Color = Color1.White Then
                For Each direction1 In KingPlayer1
                    For number = 1 To 8
                        row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row + direction1.Value.RowIncrement * number
                        column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column + direction1.Value.ColumnIncrement * number
                        If Not ((row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8)) Then
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then

                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then

                                    If direction1.Key = Directions.Forward Or direction1.Key = Directions.Backward Or _
                                        direction1.Key = Directions.Right Or direction1.Key = Directions.Left Then

                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                            If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If

                                        End If
                                        Exit For
                                    ElseIf direction1.Key = Directions.ForwardToRight Or direction1.Key = Directions.BackwardToRight Or _
                                        direction1.Key = Directions.ForwardToLeft Or direction1.Key = Directions.BackwardToLeft Then

                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                        ElseIf PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Pawn Then
                                            rowdifference = PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).SquareLocation.row - PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row
                                            If rowdifference = 1 Then
                                                If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                    PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                                End If

                                            End If
                                        End If
                                        Exit For
                                    End If
                                ElseIf PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                    Exit For
                                End If
                            End If
                        Else
                            Exit For
                        End If
                    Next
                Next

                For Each direction1 In KnightPlayer1
                    row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row + direction1.Value.RowIncrement
                    column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column + direction1.Value.ColumnIncrement
                    If Not ((row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8)) Then
                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Knight Then
                                    If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                        PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                Exit Sub
            ElseIf insTurn.Color = Color1.Black Then
                For Each direction2 In KingPlayer2
                    For number = 1 To 8
                        row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row + direction2.Value.RowIncrement * number
                        column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column + direction2.Value.ColumnIncrement * number
                        If Not ((row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8)) Then
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                    If direction2.Key = Directions.Forward Or direction2.Key = Directions.Backward Or _
                                        direction2.Key = Directions.Right Or direction2.Key = Directions.Left Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                        End If
                                        Exit For
                                    ElseIf direction2.Key = Directions.ForwardToRight Or direction2.Key = Directions.BackwardToRight Or _
                                        direction2.Key = Directions.ForwardToLeft Or direction2.Key = Directions.BackwardToLeft Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                        ElseIf PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Pawn Then
                                            rowdifference = PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).SquareLocation.row - PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row
                                            If rowdifference = 1 Then
                                                If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                    PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                                End If

                                            End If

                                        End If
                                        Exit For
                                    End If
                                ElseIf PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                    Exit For
                                End If
                            End If
                        Else
                            Exit For
                        End If
                    Next
                Next

                For Each direction2 In KnightPlayer2
                    row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row + direction2.Value.RowIncrement
                    column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column + direction2.Value.ColumnIncrement
                    If Not ((row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8)) Then
                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Knight Then
                                    If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                        PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                Exit Sub

            End If
        ElseIf insPlayers.WhitePlayer = Player.Player2 Then
            If insTurn.Color = Color1.White Then
                For Each direction2 In KingPlayer2
                    For number = 1 To 8
                        row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row + direction2.Value.RowIncrement * number
                        column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column + direction2.Value.ColumnIncrement * number
                        If Not ((row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8)) Then
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                    If direction2.Key = Directions.Forward Or direction2.Key = Directions.Backward Or _
                                        direction2.Key = Directions.Right Or direction2.Key = Directions.Left Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                        End If
                                        Exit For
                                    ElseIf direction2.Key = Directions.ForwardToRight Or direction2.Key = Directions.BackwardToRight Or _
                                        direction2.Key = Directions.ForwardToLeft Or direction2.Key = Directions.BackwardToLeft Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                        ElseIf PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Pawn Then
                                            rowdifference = PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).SquareLocation.row - PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row
                                            If rowdifference = 1 Then
                                                If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                    PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                                End If
                                            End If
                                        End If
                                        Exit For
                                    End If
                                ElseIf PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                    Exit For
                                End If
                            End If
                        Else
                            Exit For
                        End If
                    Next
                Next

                For Each direction2 In KnightPlayer2
                    row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row + direction2.Value.RowIncrement
                    column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column + direction2.Value.ColumnIncrement
                    If Not ((row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8)) Then
                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Knight Then
                                    If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                        PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                Exit Sub
            ElseIf insTurn.Color = Color1.Black Then
                For Each direction1 In KingPlayer1
                    For number = 1 To 8
                        row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row + direction1.Value.RowIncrement * number
                        column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column + direction1.Value.ColumnIncrement * number
                        If Not ((row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8)) Then
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                    If direction1.Key = Directions.Forward Or direction1.Key = Directions.Backward Or _
                                        direction1.Key = Directions.Right Or direction1.Key = Directions.Left Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                        End If
                                        Exit For
                                    ElseIf direction1.Key = Directions.ForwardToRight Or direction1.Key = Directions.BackwardToRight Or _
                                        direction1.Key = Directions.ForwardToLeft Or direction1.Key = Directions.BackwardToLeft Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                            If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If

                                        ElseIf PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Pawn Then
                                            rowdifference = PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).SquareLocation.row - PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row
                                            If rowdifference = 1 Then
                                                If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                    PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                                End If

                                            End If
                                        End If
                                        Exit For
                                    End If
                                ElseIf PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                    Exit For
                                End If
                            End If
                        Else
                            Exit For
                        End If
                    Next
                Next

                For Each direction1 In KnightPlayer1
                    row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row + direction1.Value.RowIncrement
                    column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column + direction1.Value.ColumnIncrement
                    If Not ((row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8)) Then
                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Knight Then
                                    If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                        PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                Exit Sub
            End If
        End If
    End Sub

    Private Function CanPieceSelectedMove(ByVal pieceselected As AnyPiece) As Boolean

        Dim row1 As Integer
        Dim column1 As Integer
        Dim number As Integer

        Dim rowdifference2 As Integer
        Dim columndifference2 As Integer

        If insPlayers.WhitePlayer = Player.Player1 Then
            If insTurn.Color = Color1.White Then
                rowdifference = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row - pieceselected.SquareLocation.row
                columndifference = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column - pieceselected.SquareLocation.column
                row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row
                column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column
                If rowdifference = 0 Then
                    If columndifference > 0 Then
                        For column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column - 1 To 1 Step -1
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                rowdifference2 = pieceselected.SquareLocation.row - row1
                                columndifference2 = pieceselected.SquareLocation.column - column1
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                        CanPieceSelectedMove = True
                        Exit Function
                    ElseIf columndifference < 0 Then

                        For column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column + 1 To 8
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then

                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function

                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                        CanPieceSelectedMove = True
                        Exit Function
                    End If

                ElseIf columndifference = 0 Then

                    If pieceselected.Piece = Pieces.Pawn Then
                        CanPieceSelectedMove = True
                        Exit Function
                    End If

                    If rowdifference > 0 Then
                        For row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row + 1 To 8
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function

                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                    ElseIf rowdifference < 0 Then
                        For row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row - 1 To 1 Step -1
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                    End If
                    CanPieceSelectedMove = True
                    Exit Function
                ElseIf rowdifference / columndifference = 1 Then

                    For number = 1 To 7
                        If rowdifference > 0 And columndifference > 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row - number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column - number
                        ElseIf rowdifference < 0 And columndifference < 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row + number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column + number
                        End If

                        If (row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8) Then
                            Exit For
                        End If

                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                    rowdifference2 = pieceselected.SquareLocation.row - row1
                                    columndifference2 = pieceselected.SquareLocation.column - column1

                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                        PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                        CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                        If pieceselected.Piece = Pieces.Bishop Then
                                            CanBishopMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function

                                        ElseIf pieceselected.Piece = Pieces.Queen Then
                                            insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        End If
                                        CanPieceSelectedMove = False
                                        Exit Function
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                Else
                                    CanPieceSelectedMove = True
                                    Exit Function
                                End If
                            End If
                        End If
                    Next
                    CanPieceSelectedMove = True
                    Exit Function
                ElseIf rowdifference / columndifference = -1 Then
                    For number = 1 To 7

                        If rowdifference > 0 And columndifference < 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row - number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column + number
                        ElseIf rowdifference < 0 And columndifference > 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row + number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column - number
                        End If

                        If (row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8) Then
                            Exit For
                        End If

                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                    rowdifference2 = pieceselected.SquareLocation.row - row1
                                    columndifference2 = pieceselected.SquareLocation.column - column1

                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                        PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                        CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                        If pieceselected.Piece = Pieces.Bishop Then
                                            CanBishopMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function

                                        ElseIf pieceselected.Piece = Pieces.Queen Then
                                            insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        End If
                                        CanPieceSelectedMove = False
                                        Exit Function
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                Else
                                    CanPieceSelectedMove = True
                                    Exit Function
                                End If
                            End If
                        End If
                    Next
                    CanPieceSelectedMove = True
                    Exit Function
                Else
                    CanPieceSelectedMove = True
                    Exit Function
                End If

            ElseIf insTurn.Color = Color1.Black Then
                rowdifference = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row - pieceselected.SquareLocation.row
                columndifference = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column - pieceselected.SquareLocation.column
                row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row
                column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column
                If rowdifference = 0 Then
                    If columndifference > 0 Then
                        For column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column - 1 To 1 Step -1
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function

                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                        CanPieceSelectedMove = True
                        Exit Function
                    ElseIf columndifference < 0 Then

                        For column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column + 1 To 8
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function

                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                        CanPieceSelectedMove = True
                        Exit Function
                    End If

                ElseIf columndifference = 0 Then

                    If pieceselected.Piece = Pieces.Pawn Then
                        CanPieceSelectedMove = True
                        Exit Function
                    End If

                    If rowdifference > 0 Then
                        For row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row - 1 To 1 Step -1
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function

                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                    ElseIf rowdifference < 0 Then
                        For row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row + 1 To 8
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function

                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                    End If
                    CanPieceSelectedMove = True
                    Exit Function
                ElseIf rowdifference / columndifference = 1 Then

                    For number = 1 To 7
                        If rowdifference > 0 And columndifference > 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row - number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column - number
                        ElseIf rowdifference < 0 And columndifference < 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row + number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column + number
                        End If

                        If (row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8) Then
                            Exit For
                        End If

                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                        PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                        CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                        If pieceselected.Piece = Pieces.Bishop Then
                                            CanBishopMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function

                                        ElseIf pieceselected.Piece = Pieces.Queen Then
                                            insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        End If
                                        CanPieceSelectedMove = False
                                        Exit Function

                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                Else
                                    CanPieceSelectedMove = True
                                    Exit Function
                                End If
                            End If
                        End If
                    Next
                    CanPieceSelectedMove = True
                    Exit Function
                ElseIf rowdifference / columndifference = -1 Then
                    For number = 1 To 7

                        If rowdifference > 0 And columndifference < 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row - number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column + number
                        ElseIf rowdifference < 0 And columndifference > 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row + number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column - number
                        End If

                        If (row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8) Then
                            Exit For
                        End If

                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                        PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                        CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                        If pieceselected.Piece = Pieces.Bishop Then
                                            CanBishopMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        ElseIf pieceselected.Piece = Pieces.Queen Then
                                            insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        End If
                                        CanPieceSelectedMove = False
                                        Exit Function

                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                Else
                                    CanPieceSelectedMove = True
                                    Exit Function
                                End If
                            End If
                        End If
                    Next
                    CanPieceSelectedMove = True
                    Exit Function
                Else
                    CanPieceSelectedMove = True
                    Exit Function
                End If
            End If
        ElseIf insPlayers.WhitePlayer = Player.Player2 Then
            If insTurn.Color = Color1.White Then
                rowdifference = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row - pieceselected.SquareLocation.row
                columndifference = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column - pieceselected.SquareLocation.column
                row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row
                column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column
                If rowdifference = 0 Then
                    If columndifference > 0 Then
                        For column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column - 1 To 1 Step -1
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then

                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                        CanPieceSelectedMove = True
                        Exit Function
                    ElseIf columndifference < 0 Then

                        For column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column + 1 To 8
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                        CanPieceSelectedMove = True
                        Exit Function
                    End If

                ElseIf columndifference = 0 Then

                    If pieceselected.Piece = Pieces.Pawn Then
                        CanPieceSelectedMove = True
                        Exit Function
                    End If

                    If rowdifference > 0 Then
                        For row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row - 1 To 1 Step -1
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                    ElseIf rowdifference < 0 Then
                        For row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row + 1 To 8
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                    End If
                    CanPieceSelectedMove = True
                    Exit Function
                ElseIf rowdifference / columndifference = 1 Then

                    For number = 1 To 7
                        If rowdifference > 0 And columndifference > 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row - number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column - number
                        ElseIf rowdifference < 0 And columndifference < 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row + number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column + number
                        End If

                        If (row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8) Then
                            Exit For
                        End If

                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                        CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                        If pieceselected.Piece = Pieces.Bishop Then
                                            CanBishopMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        ElseIf pieceselected.Piece = Pieces.Queen Then
                                            insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        End If
                                        CanPieceSelectedMove = False
                                        Exit Function
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                Else
                                    CanPieceSelectedMove = True
                                    Exit Function
                                End If
                            End If
                        End If
                    Next
                    CanPieceSelectedMove = True
                    Exit Function
                ElseIf rowdifference / columndifference = -1 Then
                    For number = 1 To 7

                        If rowdifference > 0 And columndifference < 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row - number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column + number
                        ElseIf rowdifference < 0 And columndifference > 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row + number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column - number
                        End If

                        If (row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8) Then
                            Exit For
                        End If

                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                        CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                        If pieceselected.Piece = Pieces.Bishop Then
                                            CanBishopMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        ElseIf pieceselected.Piece = Pieces.Queen Then
                                            insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.WhiteKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        End If
                                        CanPieceSelectedMove = False
                                        Exit Function
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                Else
                                    CanPieceSelectedMove = True
                                    Exit Function
                                End If
                            End If
                        End If
                    Next
                    CanPieceSelectedMove = True
                    Exit Function
                Else
                    CanPieceSelectedMove = True
                    Exit Function
                End If

            ElseIf insTurn.Color = Color1.Black Then
                rowdifference = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row - pieceselected.SquareLocation.row
                columndifference = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column - pieceselected.SquareLocation.column
                row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row
                column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column
                If rowdifference = 0 Then
                    If columndifference > 0 Then
                        For column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column - 1 To 1 Step -1
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                        CanPieceSelectedMove = True
                        Exit Function
                    ElseIf columndifference < 0 Then

                        For column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column + 1 To 8
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                        CanPieceSelectedMove = True
                        Exit Function
                    End If

                ElseIf columndifference = 0 Then

                    If pieceselected.Piece = Pieces.Pawn Then
                        CanPieceSelectedMove = True
                        Exit Function
                    End If

                    If rowdifference > 0 Then
                        For row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row - 1 To 1 Step -1
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                    ElseIf rowdifference < 0 Then
                        For row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row + 1 To 8
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                            If pieceselected.Piece = Pieces.Rook Then
                                                CanRookMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function

                                            ElseIf pieceselected.Piece = Pieces.Queen Then
                                                insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                                CanPieceSelectedMove = False
                                                Exit Function
                                            End If
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        Else
                                            CanPieceSelectedMove = True
                                            Exit Function
                                        End If
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                    End If
                    CanPieceSelectedMove = True
                    Exit Function
                ElseIf rowdifference / columndifference = 1 Then

                    For number = 1 To 7
                        If rowdifference > 0 And columndifference > 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row - number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column - number
                        ElseIf rowdifference < 0 And columndifference < 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row + number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column + number
                        End If

                        If (row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8) Then
                            Exit For
                        End If

                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                        CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                        If pieceselected.Piece = Pieces.Bishop Then
                                            CanBishopMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        ElseIf pieceselected.Piece = Pieces.Queen Then
                                            insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        End If
                                        CanPieceSelectedMove = False
                                        Exit Function
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                Else
                                    CanPieceSelectedMove = True
                                    Exit Function
                                End If
                            End If
                        End If
                    Next
                    CanPieceSelectedMove = True
                    Exit Function
                ElseIf rowdifference / columndifference = -1 Then
                    For number = 1 To 7

                        If rowdifference > 0 And columndifference < 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row - number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column + number
                        ElseIf rowdifference < 0 And columndifference > 0 Then
                            row1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row + number
                            column1 = PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column - number
                        End If

                        If (row1 < 1 Or row1 > 8) Or (column1 < 1 Or column1 > 8) Then
                            Exit For
                        End If

                        If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                            If Not (row1 = pieceselected.SquareLocation.row And column1 = pieceselected.SquareLocation.column) Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                    If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                        CanPieceSelectedCapturePieceThreateningKing(pieceselected, PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))), row1, column1, rowdifference2, columndifference2)
                                        If pieceselected.Piece = Pieces.Bishop Then
                                            CanBishopMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        ElseIf pieceselected.Piece = Pieces.Queen Then
                                            insQueenPossibleMoves.CanQueenMoveTowardOwnKing(pieceselected, PieceCollection(PieceCollection2.PieceCollection3(PieceCollection1.BlackKing.Name)), row1, column1)
                                            CanPieceSelectedMove = False
                                            Exit Function
                                        End If
                                        CanPieceSelectedMove = False
                                        Exit Function
                                    Else
                                        CanPieceSelectedMove = True
                                        Exit Function
                                    End If
                                Else
                                    CanPieceSelectedMove = True
                                    Exit Function
                                End If
                            End If
                        End If
                    Next
                    CanPieceSelectedMove = True
                    Exit Function
                Else
                    CanPieceSelectedMove = True
                    Exit Function
                End If
            End If

        End If

        CanPieceSelectedMove = False
        Exit Function
    End Function

    Private Sub CanPieceSelectedCapturePieceThreateningKing(ByVal pieceselected1 As AnyPiece, ByVal piecethreatening As AnyPiece, ByVal row1 As Integer, ByVal column1 As Integer, ByVal rowdifference2 As Integer, ByVal columndifference2 As Integer)


        If pieceselected1.Piece = Pieces.Pawn Then
            CanPawnCapturePieceThreateningKing(pieceselected1, piecethreatening, row1, column1)
            Exit Sub
        ElseIf pieceselected1.Piece = Pieces.Rook Then
            CanRookCapturePieceThreateningKing(pieceselected1, piecethreatening, row1, column1)
            Exit Sub
        ElseIf pieceselected1.Piece = Pieces.Bishop Then
            CanBishopCapturePieceThreateningKing(pieceselected1, piecethreatening, row1, column1)
            Exit Sub
        ElseIf pieceselected1.Piece = Pieces.Queen Then
            insQueenPossibleMoves.CanQueenCapturePieceThreateningKing(pieceselected1, piecethreatening, row1, column1)
            Exit Sub
        End If

    End Sub
    Private Sub HighlightSelectedPieceToMove(ByVal location As String)

        If insTurn.Color = Color1.White Then
            insWhiteMove.Piece = pieceSelected
            insWhiteMove.StartLocation = pieceSelected.SquareLocation.location
            insWhiteMove.EndLocation = ""
            insWhiteMove.MoveString = ""
        Else
            insBlackMove.Piece = pieceSelected
            insBlackMove.StartLocation = pieceSelected.SquareLocation.location
            insBlackMove.EndLocation = ""
            insBlackMove.MoveString = ""

        End If
        If timerBlinkSelectedPiece.IsEnabled = False Then
            timerBlinkSelectedPiece.Start()
        End If
        isPieceSelected = True

    End Sub


    Private Sub HightlightMoveLocation(ByVal locationName As String, ByVal locationPiece As Object)

        isLocationToMoveSelected = True

        If insTurn.Color = Color1.White Then
            insWhiteMove = New PossibleMove

            insWhiteMove.MoveNumber = whiteMoveNumber
            insWhiteMove.MoveString = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).MoveString
            insWhiteMove.Piece = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).Piece
            insWhiteMove.StartLocation = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).StartLocation
            insWhiteMove.EndLocation = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).EndLocation
            insWhiteMove.PieceIsCaptured = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).PieceIsCaptured
            insWhiteMove.CapturedPiece = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).CapturedPiece
            insWhiteMove.KingIsChecked = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).KingIsChecked
            insWhiteMove.WhiteKingWasChecked = WhiteKingWasChecked
            insWhiteMove.BlackKingWasChecked = False
            insWhiteMove.KingCanCastleToQueenSide = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).KingCanCastleToQueenSide
            insWhiteMove.KingCanCastleToKingSide = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).KingCanCastleToKingSide
            insWhiteMove.WhiteKingWasCastling = WhiteKingWasCastling
            insWhiteMove.BlackKingWasCastling = False
            insWhiteMove.PawnCapturedByEnPassant = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).PawnCapturedByEnPassant
            insWhiteMove.PawnWasPromoted = False

            If insWhiteMove.Piece.Piece = Pieces.Pawn Then
                If insPlayers.WhitePlayer = Player.Player1 Then
                    If BoardLocation(BoardLocator(insWhiteMove.EndLocation)).Row - BoardLocation(BoardLocator(insWhiteMove.StartLocation)).Row = -2 Then
                        insWhiteMove.EnPassantPossibleOnMove = whiteMoveNumber
                    End If
                ElseIf insPlayers.WhitePlayer = Player.Player2 Then
                    If BoardLocation(BoardLocator(insWhiteMove.EndLocation)).Row - BoardLocation(BoardLocator(insWhiteMove.StartLocation)).Row = 2 Then
                        insWhiteMove.EnPassantPossibleOnMove = whiteMoveNumber
                    End If
                End If
            End If

            If Not (PossiblePiecesCheckingBlackKing.Count = 0) Then
                If PossiblePiecesCheckingBlackKing.ContainsKey(insWhiteMove.EndLocation) = True Then
                    If PossiblePiecesCheckingBlackKing(insWhiteMove.EndLocation).Piece = insWhiteMove.Piece.Piece Then
                        insWhiteMove.KingIsChecked = True
                        For Each piece1 In PossiblePiecesCheckingBlackKing
                            If piece1.Key = insWhiteMove.EndLocation Then
                                If PiecesCheckingBlackKing.ContainsKey(piece1.Key) = False Then
                                    PiecesCheckingBlackKing.Add(piece1.Key, piece1.Value)
                                End If
                                BlackKingIsChecked = True
                                If timerBlackKingChecked.IsEnabled = False Then
                                    timerBlackKingChecked.Start()
                                End If
                            End If

                        Next
                    ElseIf PiecesCheckingBlackKing.ContainsKey(insWhiteMove.StartLocation) = True Then
                        PiecesCheckingBlackKing.Remove(insWhiteMove.StartLocation)
                        If PiecesCheckingBlackKing.Count = 0 Then
                            BlackKingIsChecked = False
                            If timerBlackKingChecked.IsEnabled = True Then
                                timerBlackKingChecked.Stop()
                            End If
                            Player1Check.DataContext = ""
                            Player2Check.DataContext = ""
                        End If
                    End If
                End If

            End If
            If Not (CapturedBlackPieces.Count = 0) Then
                If CapturedBlackPieces.ContainsKey(insWhiteMove.EndLocation) = True Then
                    insWhiteMove.PieceIsCaptured = True
                    insWhiteMove.CapturedPiece = CapturedBlackPieces(insWhiteMove.EndLocation)
                    'insWhiteMove.CapturedPiece.BoxName = BoardLocator(insWhiteMove.EndLocation)
                    If PieceCollection3.ContainsKey(insWhiteMove.CapturedPiece.Name) = True Then
                        PieceCollection.Remove(PieceCollection3(insWhiteMove.CapturedPiece.Name))
                        PieceCollection3.Remove(insWhiteMove.CapturedPiece.Name)
                    End If
                    CapturedBlackPieces.Clear()
                End If
            End If

            If PieceSelectedPossibleMoves.ContainsKey(BoardLocation(locationName).Location) = True Then
                WhiteKingIsChecked = False
                PiecesCheckingWhiteKing.Clear()
                If timerWhiteKingChecked.IsEnabled = True Then
                    timerWhiteKingChecked.Stop()
                End If
                If insPlayers.WhitePlayer = Player.Player1 Then
                    Player1Check.DataContext = ""
                Else
                    Player2Check.DataContext = ""
                End If

                OpenSpacesInBetweenKingAndPieceCheckingKing.Clear()

            End If

            If WhiteKingIsCastling = True Then
                insCastledRookMove = New PossibleMove
                insCastledRookMove.MoveNumber = whiteMoveNumber
                insCastledRookMove.MoveString = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).MoveString
                insCastledRookMove.Piece = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).Piece
                insCastledRookMove.StartLocation = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).StartLocation
                insCastledRookMove.EndLocation = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).EndLocation
                insCastledRookMove.PieceIsCaptured = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).PieceIsCaptured
                insCastledRookMove.CapturedPiece = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).CapturedPiece
                insCastledRookMove.KingIsChecked = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).KingIsChecked
                insCastledRookMove.KingCanCastleToQueenSide = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).KingCanCastleToQueenSide
                insCastledRookMove.KingCanCastleToKingSide = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).KingCanCastleToKingSide



                If insCastledRookMove.KingIsChecked = True Then
                    If PiecesCheckingBlackKing.ContainsKey(insCastledRookMove.EndLocation) = False Then
                        PiecesCheckingBlackKing.Add(insCastledRookMove.EndLocation, insCastledRookMove.Piece)
                    End If
                    BlackKingIsChecked = True
                End If
            End If

            If insWhiteMove.Piece.Piece = Pieces.Pawn Then
                If insPlayers.WhitePlayer = Player.Player1 Then
                    If BoardLocation(BoardLocator(insWhiteMove.EndLocation)).Row = 1 Then

                        BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image
                        BoardStatus(pieceSelected.BoxName) = Status.Open

                        PieceCollection3.Remove(pieceSelected.BoxName)
                        PieceCollection.Remove(pieceSelected.BoxName)

                        insWhiteMove.PawnWasPromoted = True

                        promotedWhitePawnCount = promotedWhitePawnCount + 1
                        If promotedWhitePawnCount = 1 Then
                            promotedWhitePawn1 = New AnyPiece
                            promotedWhitePawn1.Piece = Pieces.Queen
                            promotedWhitePawn1.SquareLocation = BoardLocation(BoardLocator(insWhiteMove.EndLocation))
                            promotedWhitePawn1.Color = BoardLocation(BoardLocator(insWhiteMove.EndLocation)).SquareColor
                            If promotedWhitePawn1.Color = Color1.White Then
                                promotedWhitePawn1.OriginalImage = WQWS
                                promotedWhitePawn1.Image = WQWS
                            ElseIf promotedWhitePawn1.Color = Color1.Black Then
                                promotedWhitePawn1.OriginalImage = WQBS
                                promotedWhitePawn1.Image = WQBS
                            End If
                            promotedWhitePawn1.ImageOnWhiteSquare = WQWS
                            promotedWhitePawn1.ImageOnBlackSquare = WQBS
                            promotedWhitePawn1.MoveNumber = insWhiteMove.Piece.MoveNumber
                            promotedWhitePawn1.Name = "PromotedWhitePawn1"
                            promotedWhitePawn1.BoxName = BoardLocator(insWhiteMove.EndLocation)


                            insWhiteMove.PromotedPawnPiece = promotedWhitePawn1

                            BoardImage(promotedWhitePawn1.BoxName).Source = promotedWhitePawn1.Image
                            BoardStatus(promotedWhitePawn1.BoxName) = Status.Occupied

                            locationPiece = SetLocationPiece(promotedWhitePawn1)


                        ElseIf promotedWhitePawnCount = 2 Then
                            promotedWhitePawn2 = New AnyPiece
                            promotedWhitePawn2.Piece = Pieces.Queen
                            promotedWhitePawn2.SquareLocation = BoardLocation(BoardLocator(insWhiteMove.EndLocation))
                            promotedWhitePawn2.Color = BoardLocation(BoardLocator(insWhiteMove.EndLocation)).SquareColor
                            If promotedWhitePawn2.Color = Color1.White Then
                                promotedWhitePawn2.OriginalImage = WQWS
                                promotedWhitePawn2.Image = WQWS
                            ElseIf promotedWhitePawn2.Color = Color1.Black Then
                                promotedWhitePawn2.OriginalImage = WQBS
                                promotedWhitePawn2.Image = WQBS
                            End If
                            promotedWhitePawn2.ImageOnWhiteSquare = WQWS
                            promotedWhitePawn2.ImageOnBlackSquare = WQBS
                            promotedWhitePawn2.MoveNumber = insWhiteMove.Piece.MoveNumber
                            promotedWhitePawn2.Name = "PromotedWhitePawn2"
                            promotedWhitePawn2.BoxName = BoardLocator(insWhiteMove.EndLocation)

                            insWhiteMove.PromotedPawnPiece = promotedWhitePawn2

                            BoardImage(promotedWhitePawn2.BoxName).Source = promotedWhitePawn1.Image
                            BoardStatus(promotedWhitePawn2.BoxName) = Status.Occupied

                            locationPiece = SetLocationPiece(promotedWhitePawn2)

                        ElseIf promotedWhitePawnCount = 3 Then
                        ElseIf promotedWhitePawnCount = 4 Then
                        ElseIf promotedWhitePawnCount = 5 Then
                        ElseIf promotedWhitePawnCount = 6 Then
                        ElseIf promotedWhitePawnCount = 7 Then
                        ElseIf promotedWhitePawnCount = 8 Then

                        End If
                    End If
                ElseIf insPlayers.WhitePlayer = Player.Player2 Then
                    If BoardLocation(BoardLocator(insWhiteMove.EndLocation)).Row = 8 Then

                        BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image
                        BoardStatus(pieceSelected.BoxName) = Status.Open

                        PieceCollection3.Remove(pieceSelected.BoxName)
                        PieceCollection.Remove(pieceSelected.BoxName)

                        insWhiteMove.PawnWasPromoted = True


                        promotedWhitePawnCount += 1
                        If promotedWhitePawnCount = 1 Then
                            promotedWhitePawn1 = New AnyPiece
                            promotedWhitePawn1.Piece = Pieces.Queen
                            promotedWhitePawn1.SquareLocation = BoardLocation(BoardLocator(insWhiteMove.EndLocation))
                            promotedWhitePawn1.Color = BoardLocation(BoardLocator(insWhiteMove.EndLocation)).SquareColor
                            If promotedWhitePawn1.Color = Color1.White Then
                                promotedWhitePawn1.OriginalImage = WQWS
                                promotedWhitePawn1.Image = WQWS
                            ElseIf promotedWhitePawn1.Color = Color1.Black Then
                                promotedWhitePawn1.OriginalImage = WQBS
                                promotedWhitePawn1.Image = WQBS
                            End If
                            promotedWhitePawn1.ImageOnWhiteSquare = WQWS
                            promotedWhitePawn1.ImageOnBlackSquare = WQBS
                            promotedWhitePawn1.MoveNumber = insWhiteMove.Piece.MoveNumber
                            promotedWhitePawn1.Name = "PromotedWhitePawn1"
                            promotedWhitePawn1.BoxName = BoardLocator(insWhiteMove.EndLocation)

                            insWhiteMove.PromotedPawnPiece = promotedWhitePawn1

                            BoardImage(promotedWhitePawn1.BoxName).Source = promotedWhitePawn1.Image
                            BoardStatus(promotedWhitePawn1.BoxName) = Status.Occupied



                            locationPiece = SetLocationPiece(promotedWhitePawn1)

                        ElseIf promotedWhitePawnCount = 2 Then
                            promotedWhitePawn2 = New AnyPiece
                            promotedWhitePawn2.Piece = Pieces.Queen
                            promotedWhitePawn2.SquareLocation = BoardLocation(BoardLocator(insWhiteMove.EndLocation))
                            promotedWhitePawn2.Color = BoardLocation(BoardLocator(insWhiteMove.EndLocation)).SquareColor
                            If promotedWhitePawn2.Color = Color1.White Then
                                promotedWhitePawn2.OriginalImage = WQWS
                                promotedWhitePawn2.Image = WQWS
                            ElseIf promotedWhitePawn2.Color = Color1.Black Then
                                promotedWhitePawn2.OriginalImage = WQBS
                                promotedWhitePawn2.Image = WQBS
                            End If
                            promotedWhitePawn2.ImageOnWhiteSquare = WQWS
                            promotedWhitePawn2.ImageOnBlackSquare = WQBS
                            promotedWhitePawn2.MoveNumber = 1
                            promotedWhitePawn2.Name = "PromotedWhitePawn2"
                            promotedWhitePawn2.BoxName = BoardLocator(insWhiteMove.EndLocation)

                            insWhiteMove.PromotedPawnPiece = promotedWhitePawn2

                            BoardImage(promotedWhitePawn2.BoxName).Source = promotedWhitePawn1.Image
                            BoardStatus(promotedWhitePawn2.BoxName) = Status.Occupied

                            locationPiece = SetLocationPiece(promotedWhitePawn2)

                        ElseIf promotedWhitePawnCount = 3 Then
                        ElseIf promotedWhitePawnCount = 4 Then
                        ElseIf promotedWhitePawnCount = 5 Then
                        ElseIf promotedWhitePawnCount = 6 Then
                        ElseIf promotedWhitePawnCount = 7 Then
                        ElseIf promotedWhitePawnCount = 8 Then

                        End If
                    End If
                End If
            End If

        ElseIf insTurn.Color = Color1.Black Then

            insBlackMove = New PossibleMove
            insBlackMove.MoveNumber = blackMoveNumber
            insBlackMove.MoveString = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).MoveString
            insBlackMove.Piece = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).Piece
            insBlackMove.StartLocation = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).StartLocation
            insBlackMove.EndLocation = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).EndLocation
            insBlackMove.PieceIsCaptured = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).PieceIsCaptured
            insBlackMove.CapturedPiece = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).CapturedPiece
            insBlackMove.KingIsChecked = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).KingIsChecked
            insBlackMove.BlackKingWasChecked = BlackKingWasChecked
            insBlackMove.WhiteKingWasChecked = False
            insBlackMove.KingCanCastleToQueenSide = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).KingCanCastleToQueenSide
            insBlackMove.KingCanCastleToKingSide = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).KingCanCastleToKingSide
            insBlackMove.BlackKingWasCastling = BlackKingWasCastling
            insBlackMove.WhiteKingWasCastling = False
            insBlackMove.PawnCapturedByEnPassant = PieceSelectedPossibleMoves(BoardLocation(locationName).Location).PawnCapturedByEnPassant
            insBlackMove.PawnWasPromoted = False

            If insBlackMove.Piece.Piece = Pieces.Pawn Then
                If insPlayers.BlackPlayer = Player.Player1 Then
                    If BoardLocation(BoardLocator(insBlackMove.EndLocation)).Row - BoardLocation(BoardLocator(insBlackMove.StartLocation)).Row = -2 Then
                        insBlackMove.EnPassantPossibleOnMove = blackMoveNumber + 1
                    End If
                ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                    If BoardLocation(BoardLocator(insBlackMove.EndLocation)).Row - BoardLocation(BoardLocator(insBlackMove.StartLocation)).Row = 2 Then
                        insBlackMove.EnPassantPossibleOnMove = blackMoveNumber + 1
                    End If
                End If
            End If


            If Not (PossiblePiecesCheckingWhiteKing.Count = 0) Then
                If PossiblePiecesCheckingWhiteKing.ContainsKey(insBlackMove.EndLocation) = True Then
                    If PossiblePiecesCheckingWhiteKing(insBlackMove.EndLocation).Piece = insBlackMove.Piece.Piece Then
                        insBlackMove.KingIsChecked = True
                        For Each piece1 In PossiblePiecesCheckingWhiteKing
                            If piece1.Key = insBlackMove.EndLocation Then
                                If PiecesCheckingWhiteKing.ContainsKey(piece1.Key) = False Then
                                    PiecesCheckingWhiteKing.Add(piece1.Key, piece1.Value)
                                End If
                                WhiteKingIsChecked = True
                                If timerWhiteKingChecked.IsEnabled = False Then
                                    timerWhiteKingChecked.Start()
                                End If
                            End If
                        Next

                        If PiecesCheckingWhiteKing.ContainsKey(insBlackMove.StartLocation) = True Then
                            PiecesCheckingWhiteKing.Remove(insBlackMove.StartLocation)
                            If PiecesCheckingWhiteKing.Count = 0 Then
                                WhiteKingIsChecked = False
                                If timerWhiteKingChecked.IsEnabled = True Then
                                    timerWhiteKingChecked.Stop()
                                End If
                                Player1Check.DataContext = ""
                                Player2Check.DataContext = ""
                            End If
                        End If
                    End If
                End If
                'PossiblePiecesCheckingWhiteKing.Clear()
            End If
            If Not (CapturedWhitePieces.Count = 0) Then
                If CapturedWhitePieces.ContainsKey(insBlackMove.EndLocation) = True Then
                    insBlackMove.PieceIsCaptured = True
                    insBlackMove.CapturedPiece = CapturedWhitePieces(insBlackMove.EndLocation)
                    'insBlackMove.CapturedPiece.BoxName = BoardLocator(insBlackMove.EndLocation)
                    If PieceCollection3.ContainsKey(insBlackMove.CapturedPiece.Name) = True Then
                        PieceCollection.Remove(PieceCollection3(insBlackMove.CapturedPiece.Name))
                        PieceCollection3.Remove(insBlackMove.CapturedPiece.Name)
                    End If

                End If

                CapturedWhitePieces.Clear()

            End If
            If PieceSelectedPossibleMoves.ContainsKey(BoardLocation(locationName).Location) = True Then
                BlackKingIsChecked = False
                PiecesCheckingBlackKing.Clear()
                If timerBlackKingChecked.IsEnabled = True Then
                    timerBlackKingChecked.Stop()
                End If
                If insPlayers.BlackPlayer = Player.Player1 Then
                    Player1Check.DataContext = ""
                Else
                    Player2Check.DataContext = ""
                End If
                OpenSpacesInBetweenKingAndPieceCheckingKing.Clear()
            End If

            If BlackKingIsCastling = True Then
                insCastledRookMove = New PossibleMove
                insCastledRookMove.MoveNumber = whiteMoveNumber
                insCastledRookMove.MoveString = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).MoveString
                insCastledRookMove.Piece = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).Piece
                insCastledRookMove.StartLocation = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).StartLocation
                insCastledRookMove.EndLocation = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).EndLocation
                insCastledRookMove.PieceIsCaptured = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).PieceIsCaptured
                insCastledRookMove.CapturedPiece = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).CapturedPiece
                insCastledRookMove.KingIsChecked = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).KingIsChecked
                insCastledRookMove.KingCanCastleToQueenSide = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).KingCanCastleToQueenSide
                insCastledRookMove.KingCanCastleToKingSide = AllPossibleCastledRookMoves(BoardLocation(locationName).Location).KingCanCastleToKingSide


                If insCastledRookMove.KingIsChecked = True Then
                    If PiecesCheckingWhiteKing.ContainsKey(insCastledRookMove.EndLocation) = False Then
                        PiecesCheckingWhiteKing.Add(insCastledRookMove.EndLocation, insCastledRookMove.Piece)
                    End If
                    WhiteKingIsChecked = True
                End If
            End If

            If insBlackMove.Piece.Piece = Pieces.Pawn Then
                If insPlayers.BlackPlayer = Player.Player1 Then
                    If BoardLocation(BoardLocator(insBlackMove.EndLocation)).Row = 1 Then
                        BoardImage(pieceSelected.BoxName).Source = BoardLocation(pieceSelected.BoxName).Image
                        BoardStatus(pieceSelected.BoxName) = Status.Open

                        PieceCollection3.Remove(pieceSelected.BoxName)
                        PieceCollection.Remove(pieceSelected.BoxName)

                        insBlackMove.PawnWasPromoted = True

                        promotedBlackPawnCount = promotedBlackPawnCount + 1

                        If promotedBlackPawnCount = 1 Then
                            promotedBlackPawn1 = New AnyPiece
                            promotedBlackPawn1.Piece = Pieces.Queen
                            promotedBlackPawn1.SquareLocation = BoardLocation(BoardLocator(insBlackMove.EndLocation))
                            promotedBlackPawn1.Color = BoardLocation(BoardLocator(insBlackMove.EndLocation)).SquareColor
                            If promotedBlackPawn1.Color = Color1.White Then
                                promotedBlackPawn1.OriginalImage = BQWS
                                promotedBlackPawn1.Image = BQWS
                            ElseIf promotedBlackPawn1.Color = Color1.Black Then
                                promotedBlackPawn1.OriginalImage = BQBS
                                promotedBlackPawn1.Image = BQBS
                            End If
                            promotedBlackPawn1.ImageOnWhiteSquare = BQWS
                            promotedBlackPawn1.ImageOnBlackSquare = BQBS
                            promotedBlackPawn1.MoveNumber = insBlackMove.Piece.MoveNumber
                            promotedBlackPawn1.Name = "PromotedBlackPawn1"
                            promotedBlackPawn1.BoxName = BoardLocator(insBlackMove.EndLocation)

                            insBlackMove.PromotedPawnPiece = promotedBlackPawn1

                            BoardImage(promotedBlackPawn1.BoxName).Source = promotedBlackPawn1.Image
                            BoardStatus(promotedBlackPawn1.BoxName) = Status.Occupied

                            locationPiece = SetLocationPiece(promotedBlackPawn1)

                        ElseIf promotedBlackPawnCount = 2 Then
                            promotedBlackPawn2 = New AnyPiece
                            promotedBlackPawn2.Piece = Pieces.Queen
                            promotedBlackPawn2.SquareLocation = BoardLocation(BoardLocator(insBlackMove.EndLocation))
                            promotedBlackPawn2.Color = BoardLocation(BoardLocator(insBlackMove.EndLocation)).SquareColor

                            If promotedBlackPawn2.Color = Color1.White Then
                                promotedBlackPawn2.OriginalImage = BQWS
                                promotedBlackPawn2.Image = BQWS
                            ElseIf promotedBlackPawn2.Color = Color1.Black Then
                                promotedBlackPawn2.OriginalImage = BQBS
                                promotedBlackPawn2.Image = BQBS
                            End If
                            promotedBlackPawn2.ImageOnWhiteSquare = BQWS
                            promotedBlackPawn2.ImageOnBlackSquare = BQBS
                            promotedBlackPawn2.MoveNumber = insBlackMove.Piece.MoveNumber
                            promotedBlackPawn2.Name = "PromotedBlackPawn2"
                            promotedBlackPawn2.BoxName = BoardLocator(insBlackMove.EndLocation)

                            insBlackMove.PromotedPawnPiece = promotedBlackPawn1

                            BoardImage(promotedBlackPawn2.BoxName).Source = promotedBlackPawn2.Image
                            BoardStatus(promotedBlackPawn2.BoxName) = Status.Occupied

                            locationPiece = SetLocationPiece(promotedBlackPawn2)

                        ElseIf promotedBlackPawnCount = 3 Then
                        ElseIf promotedBlackPawnCount = 4 Then
                        ElseIf promotedBlackPawnCount = 5 Then
                        ElseIf promotedBlackPawnCount = 6 Then
                        ElseIf promotedBlackPawnCount = 7 Then
                        ElseIf promotedBlackPawnCount = 8 Then

                        End If

                    End If
                ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                    If BoardLocation(BoardLocator(insBlackMove.EndLocation)).Row = 8 Then
                        promotedBlackPawnCount += 1
                        If promotedBlackPawnCount = 1 Then
                            promotedBlackPawn1 = New AnyPiece
                            promotedBlackPawn1.Piece = Pieces.Queen
                            promotedBlackPawn1.SquareLocation = BoardLocation(BoardLocator(insBlackMove.EndLocation))
                            promotedBlackPawn1.Color = BoardLocation(BoardLocator(insBlackMove.EndLocation)).SquareColor
                            If promotedBlackPawn1.Color = Color1.White Then
                                promotedBlackPawn1.OriginalImage = BQWS
                                promotedBlackPawn1.Image = BQWS
                            ElseIf promotedBlackPawn1.Color = Color1.Black Then
                                promotedBlackPawn1.OriginalImage = BQBS
                                promotedBlackPawn1.Image = BQBS
                            End If
                            promotedBlackPawn1.ImageOnWhiteSquare = BQWS
                            promotedBlackPawn1.ImageOnBlackSquare = BQBS
                            promotedBlackPawn1.MoveNumber = insBlackMove.Piece.MoveNumber
                            promotedBlackPawn1.Name = "PromotedBlackPawn1"
                            promotedBlackPawn1.BoxName = BoardLocator(insBlackMove.EndLocation)

                            BoardImage(promotedBlackPawn1.BoxName).Source = promotedBlackPawn1.Image
                            BoardStatus(promotedBlackPawn1.BoxName) = Status.Occupied

                            locationPiece = SetLocationPiece(promotedBlackPawn1)


                        ElseIf promotedBlackPawnCount = 2 Then
                            promotedBlackPawn2 = New AnyPiece
                            promotedBlackPawn2.Piece = Pieces.Queen
                            promotedBlackPawn2.SquareLocation = BoardLocation(BoardLocator(insBlackMove.EndLocation))
                            promotedBlackPawn2.Color = BoardLocation(BoardLocator(insBlackMove.EndLocation)).SquareColor
                            If promotedBlackPawn2.Color = Color1.White Then
                                promotedBlackPawn2.OriginalImage = BQWS
                                promotedBlackPawn2.Image = BQWS
                            ElseIf promotedBlackPawn2.Color = Color1.Black Then
                                promotedBlackPawn2.OriginalImage = BQBS
                                promotedBlackPawn2.Image = BQBS
                            End If
                            promotedBlackPawn2.ImageOnWhiteSquare = BQWS
                            promotedBlackPawn2.ImageOnBlackSquare = BQBS
                            promotedBlackPawn2.MoveNumber = insBlackMove.Piece.MoveNumber
                            promotedBlackPawn2.Name = "PromotedBlackPawn2"
                            promotedBlackPawn2.BoxName = BoardLocator(insBlackMove.EndLocation)

                            BoardImage(promotedBlackPawn2.BoxName).Source = promotedBlackPawn2.Image
                            BoardStatus(promotedBlackPawn2.BoxName) = Status.Occupied

                            locationPiece = SetLocationPiece(promotedBlackPawn2)

                        ElseIf promotedBlackPawnCount = 3 Then
                        ElseIf promotedBlackPawnCount = 4 Then
                        ElseIf promotedBlackPawnCount = 5 Then
                        ElseIf promotedBlackPawnCount = 6 Then
                        ElseIf promotedBlackPawnCount = 7 Then
                        ElseIf promotedBlackPawnCount = 8 Then


                        End If
                    End If
                End If
            End If
        End If


        locationPiece.BoxName = locationName
        locationPiece.SquareLocation = BoardLocation(locationName)

        If BoardLocation(locationName).SquareColor = Color1.White Then
            locationPiece.Image = locationPiece.ImageOnWhiteSquare
        Else
            locationPiece.Image = locationPiece.ImageOnBlackSquare
        End If

        pieceSelected = New AnyPiece
        SetPieceSelected(locationPiece)

        PieceCollection.Add(pieceSelected.BoxName, pieceSelected)
        PieceCollection3.Add(pieceSelected.Name, pieceSelected.BoxName)

        BoardImage(pieceSelected.BoxName).Source = pieceSelected.Image

        BoardStatus.Remove(pieceSelected.BoxName)
        BoardStatus.Add(pieceSelected.BoxName, Status.Occupied)

        Dim PromotedWhiteQueenCheckedBlackKingLikeRook As Boolean
        Dim PromotedWhiteQueenCheckedBlackKingLikeBishop As Boolean
        Dim PromotedBlackQueenCheckedWhiteKingLikeRook As Boolean
        Dim PromotedBlackQueenCheckedWhiteKingLikeBishop As Boolean


        If pieceSelected.Color = Color1.White Then
            If insWhiteMove.PawnWasPromoted = True Then


                If pieceSelected.Piece = Pieces.Queen Then
                    rownumber = pieceSelected.SquareLocation.row
                    columnnumber = pieceSelected.SquareLocation.column
                    KingIsChecked = False
                    PieceIsCaptured = False
                    rowdifference = PieceCollection(PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.row - rownumber
                    columndifference = PieceCollection(PieceCollection3(PieceCollection1.BlackKing.Name)).SquareLocation.column - columnnumber
                    PromotedWhiteQueenCheckedBlackKingLikeRook = CanPromotedWhiteQueenCheckBlackKingLikeRook(pieceSelected, rownumber, columnnumber, KingIsChecked, PieceIsCaptured)
                    PromotedWhiteQueenCheckedBlackKingLikeBishop = CanPromotedWhiteQueenCheckBlackKingLikeBishop(pieceSelected, rownumber, columnnumber, KingIsChecked, PieceIsCaptured)
                    If PromotedWhiteQueenCheckedBlackKingLikeRook = True Or PromotedWhiteQueenCheckedBlackKingLikeBishop = True Then
                        If PossiblePiecesCheckingBlackKing.ContainsKey(insWhiteMove.EndLocation) = False Then
                            PossiblePiecesCheckingBlackKing.Add(insWhiteMove.EndLocation, pieceSelected)
                        End If
                        If PiecesCheckingBlackKing.ContainsKey(insWhiteMove.EndLocation) = False Then
                            PiecesCheckingBlackKing.Add(insWhiteMove.EndLocation, pieceSelected)
                        End If
                        BlackKingIsChecked = True
                        If timerBlackKingChecked.IsEnabled = False Then
                            timerBlackKingChecked.Start()
                        End If
                    End If
                End If
            End If
        ElseIf pieceSelected.Color = Color1.Black Then
            If insBlackMove.PawnWasPromoted = True Then
                If pieceSelected.Piece = Pieces.Queen Then
                    rownumber = pieceSelected.SquareLocation.row
                    columnnumber = pieceSelected.SquareLocation.column
                    KingIsChecked = False
                    PieceIsCaptured = False
                    rowdifference = PieceCollection(PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.row - rownumber
                    columndifference = PieceCollection(PieceCollection3(PieceCollection1.WhiteKing.Name)).SquareLocation.column - columnnumber
                    PromotedBlackQueenCheckedWhiteKingLikeRook = CanPromotedBlackQueenCheckWhiteKingLikeRook(pieceSelected, rownumber, columnnumber, KingIsChecked, PieceIsCaptured)
                    PromotedBlackQueenCheckedWhiteKingLikeBishop = CanPromotedBlackQueenCheckWhiteKingLikeBishop(pieceSelected, rownumber, columnnumber, KingIsChecked, PieceIsCaptured)
                    If PromotedBlackQueenCheckedWhiteKingLikeRook = True Or PromotedBlackQueenCheckedWhiteKingLikeBishop = True Then
                        If PossiblePiecesCheckingWhiteKing.ContainsKey(insBlackMove.EndLocation) = False Then
                            PossiblePiecesCheckingWhiteKing.Add(insBlackMove.EndLocation, pieceSelected)
                        End If
                        If PiecesCheckingWhiteKing.ContainsKey(insBlackMove.EndLocation) = False Then
                            PiecesCheckingWhiteKing.Add(insBlackMove.EndLocation, pieceSelected)
                        End If
                        WhiteKingIsChecked = True
                        If timerWhiteKingChecked.IsEnabled = False Then
                            timerWhiteKingChecked.Start()
                        End If
                    End If
                End If
            End If
        End If


        If WhiteKingIsCastling = True Or BlackKingIsCastling = True Then
            pieceSelected2.BoxName = BoardLocator(insCastledRookMove.EndLocation)
            pieceSelected2.SquareLocation = BoardLocation(BoardLocator(insCastledRookMove.EndLocation))

            If BoardLocation(BoardLocator(insCastledRookMove.EndLocation)).SquareColor = Color1.White Then
                pieceSelected2.Image = pieceSelected2.ImageOnWhiteSquare
            Else
                pieceSelected2.Image = pieceSelected2.ImageOnBlackSquare
            End If


            PieceCollection.Add(pieceSelected2.BoxName, pieceSelected2)
            PieceCollection3.Add(pieceSelected2.Name, pieceSelected2.BoxName)

            BoardImage(pieceSelected2.BoxName).Source = pieceSelected2.Image

            BoardStatus.Remove(pieceSelected2.BoxName)
            BoardStatus.Add(pieceSelected2.BoxName, Status.Occupied)

        ElseIf WhiteKingWasCastling = True Or BlackKingWasCastling = True Then
            pieceSelected2.BoxName = BoardLocator(insCastledRookMove.StartLocation)
            pieceSelected2.SquareLocation = BoardLocation(BoardLocator(insCastledRookMove.StartLocation))

            If BoardLocation(BoardLocator(insCastledRookMove.StartLocation)).SquareColor = Color1.White Then
                pieceSelected2.Image = pieceSelected2.ImageOnWhiteSquare
            Else
                pieceSelected2.Image = pieceSelected2.ImageOnBlackSquare
            End If

            If PieceCollection.ContainsKey(pieceSelected2.BoxName) = False Then
                PieceCollection.Add(pieceSelected2.BoxName, pieceSelected2)
                PieceCollection3.Add(pieceSelected2.Name, pieceSelected2.BoxName)
            End If

            BoardImage(pieceSelected2.BoxName).Source = pieceSelected2.Image

            BoardStatus.Remove(pieceSelected2.BoxName)
            BoardStatus.Add(pieceSelected2.BoxName, Status.Occupied)

            WhiteKingWasCastling = False
            BlackKingWasCastling = False

        End If

        Dim row1 As Integer
        Dim column1 As Integer
        Dim opposingKingIsInDiscoveredCheck As Boolean = False

        If insPlayers.WhitePlayer = Player.Player1 Then
            If insTurn.Color = Color1.White Then
                For Each direction2 In KingPlayer2
                    For number1 = 1 To 8
                        row1 = PieceCollection(PieceCollection3("BlackKing")).SquareLocation.row + direction2.Value.RowIncrement * number1
                        column1 = PieceCollection(PieceCollection3("BlackKing")).SquareLocation.column + direction2.Value.ColumnIncrement * number1
                        If Not (row1 < 1 Or row1 > 8 Or column1 < 1 Or column1 > 8) Then
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                    If direction2.Key = Directions.Forward Or direction2.Key = Directions.Backward Or _
                                        direction2.Key = Directions.Left Or direction2.Key = Directions.Right Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            opposingKingIsInDiscoveredCheck = True
                                            If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                            Exit For
                                        Else
                                            Exit For
                                        End If
                                    ElseIf direction2.Key = Directions.ForwardToLeft Or direction2.Key = Directions.ForwardToRight Or _
                                        direction2.Key = Directions.BackwardToLeft Or direction2.Key = Directions.KnBackwardToRight Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            opposingKingIsInDiscoveredCheck = True
                                            If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                            Exit For
                                        Else
                                            Exit For
                                        End If
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                        Else
                            Exit For
                        End If
                    Next
                    If opposingKingIsInDiscoveredCheck = True Then
                        insWhiteMove.KingIsChecked = True
                        BlackKingIsChecked = True
                        If timerBlackKingChecked.IsEnabled = False Then
                            timerBlackKingChecked.Start()
                        End If
                        Exit For
                    End If
                Next
            ElseIf insTurn.Color = Color1.Black Then
                For Each direction1 In KingPlayer1
                    For number1 = 1 To 8
                        row1 = PieceCollection(PieceCollection3("WhiteKing")).SquareLocation.row + direction1.Value.RowIncrement * number1
                        column1 = PieceCollection(PieceCollection3("WhiteKing")).SquareLocation.column + direction1.Value.ColumnIncrement * number1
                        If Not (row1 < 1 Or row1 > 8 Or column1 < 1 Or column1 > 8) Then
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                    If direction1.Key = Directions.Forward Or direction1.Key = Directions.Backward Or _
                                        direction1.Key = Directions.Left Or direction1.Key = Directions.Right Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            opposingKingIsInDiscoveredCheck = True
                                            If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                            Exit For
                                        Else
                                            Exit For
                                        End If
                                    ElseIf direction1.Key = Directions.ForwardToLeft Or direction1.Key = Directions.ForwardToRight Or _
                                        direction1.Key = Directions.BackwardToLeft Or direction1.Key = Directions.KnBackwardToRight Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            opposingKingIsInDiscoveredCheck = True
                                            If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                            Exit For
                                        Else
                                            Exit For
                                        End If
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                        Else
                            Exit For
                        End If
                    Next
                    If opposingKingIsInDiscoveredCheck = True Then
                        insBlackMove.KingIsChecked = True
                        WhiteKingIsChecked = True
                        If timerWhiteKingChecked.IsEnabled = False Then
                            timerWhiteKingChecked.Start()
                        End If
                    End If
                    Exit For
                Next


            End If
        ElseIf insPlayers.WhitePlayer = Player.Player2 Then
            If insTurn.Color = Color1.White Then
                For Each direction1 In KingPlayer1
                    For number1 = 1 To 8
                        row1 = PieceCollection(PieceCollection3("BlackKing")).SquareLocation.row + direction1.Value.RowIncrement * number1
                        column1 = PieceCollection(PieceCollection3("BlackKing")).SquareLocation.column + direction1.Value.ColumnIncrement * number1
                        If Not (row1 < 1 Or row1 > 8 Or column1 < 1 Or column1 > 8) Then
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.White Then
                                    If direction1.Key = Directions.Forward Or direction1.Key = Directions.Backward Or _
                                        direction1.Key = Directions.Left Or direction1.Key = Directions.Right Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            opposingKingIsInDiscoveredCheck = True
                                            If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                            Exit For
                                        Else
                                            Exit For
                                        End If
                                    ElseIf direction1.Key = Directions.ForwardToLeft Or direction1.Key = Directions.ForwardToRight Or _
                                        direction1.Key = Directions.BackwardToLeft Or direction1.Key = Directions.KnBackwardToRight Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            opposingKingIsInDiscoveredCheck = True
                                            If PiecesCheckingBlackKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingBlackKing.Add(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                            Exit For
                                        Else
                                            Exit For
                                        End If
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                        Else
                            Exit For
                        End If
                    Next
                    If opposingKingIsInDiscoveredCheck = True Then
                        insWhiteMove.KingIsChecked = True
                        BlackKingIsChecked = True
                        If timerBlackKingChecked.IsEnabled = False Then
                            timerBlackKingChecked.Start()
                        End If
                        Exit For
                    End If
                Next

            ElseIf insTurn.Color = Color1.Black Then
                For Each direction2 In KingPlayer2
                    For number1 = 1 To 8
                        row1 = PieceCollection(PieceCollection3("WhiteKing")).SquareLocation.row + direction2.Value.RowIncrement * number1
                        column1 = PieceCollection(PieceCollection3("WhiteKing")).SquareLocation.column + direction2.Value.ColumnIncrement * number1
                        If Not (row1 < 1 Or row1 > 8 Or column1 < 1 Or column1 > 8) Then
                            If BoardStatus(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))) = Status.Occupied Then
                                If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Color = Color1.Black Then
                                    If direction2.Key = Directions.Forward Or direction2.Key = Directions.Backward Or _
                                        direction2.Key = Directions.Left Or direction2.Key = Directions.Right Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Rook Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            opposingKingIsInDiscoveredCheck = True
                                            If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                            Exit For
                                        Else
                                            Exit For
                                        End If
                                    ElseIf direction2.Key = Directions.ForwardToLeft Or direction2.Key = Directions.ForwardToRight Or _
                                        direction2.Key = Directions.BackwardToLeft Or direction2.Key = Directions.KnBackwardToRight Then
                                        If PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Bishop Or _
                                            PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))).Piece = Pieces.Queen Then
                                            opposingKingIsInDiscoveredCheck = True
                                            If PiecesCheckingWhiteKing.ContainsKey(BoardLocatorRowColumnToChessNotation(CStr(row1) + CStr(column1))) = False Then
                                                PiecesCheckingWhiteKing.Add(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1)), PieceCollection(BoardLocatorRowColumnToBoxName(CStr(row1) + CStr(column1))))
                                            End If
                                            Exit For
                                        Else
                                            Exit For
                                        End If
                                    End If
                                Else
                                    Exit For
                                End If
                            End If
                        Else
                            Exit For
                        End If
                    Next
                    If opposingKingIsInDiscoveredCheck = True Then
                        insBlackMove.KingIsChecked = True
                        WhiteKingIsChecked = True
                        If timerWhiteKingChecked.IsEnabled = False Then
                            timerWhiteKingChecked.Start()
                        End If
                    End If
                    Exit For
                Next
            End If
        End If


        If timerBlinkSelectedPiece.IsEnabled = False Then
            timerBlinkSelectedPiece.Start()
        End If
        changeTurn.IsEnabled = True

        If changeTurnOnMoveLocationSelection.IsOn = True Then
            ChangeTurnSub()
        End If

    End Sub

    Private Sub changeTurn_Click(sender As Object, e As RoutedEventArgs) Handles changeTurn.Click
        ChangeTurnSub()
    End Sub
    Private Sub Start_Clock()

        startClock = False
        ChessBoard.IsTapEnabled = True

        insTurn.Color = Color1.White

        If insTurn.Color = Color1.White Then
            If insTurn.PlayerNumber = Player.Player1 Then
                If timerPlayer1Clock.IsEnabled = False And Use_Clock.IsOn = True Then
                    timerPlayer1Clock.Start()
                End If
            Else
                If timerPlayer2Clock.IsEnabled = False And Use_Clock.IsOn = True Then
                    timerPlayer2Clock.Start()
                End If
            End If
        End If

        startClock = False

        ChessBoard.IsTapEnabled = True

        changeTurn.DataContext = "Change Turn"
        If changeTurnOnMoveLocationSelection.IsOn = True Then
            changeTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        End If

    End Sub
    Private Sub ChangeTurnSub()

        If startClock = True Then

            startClock = False
            startNewGame = False
            ChessBoard.IsTapEnabled = True

            insTurn.Color = Color1.White

            If insTurn.Color = Color1.White Then
                If insTurn.PlayerNumber = Player.Player1 Then
                    If timerPlayer1Clock.IsEnabled = False And Use_Clock.IsOn = True Then
                        timerPlayer1Clock.Start()
                    End If
                Else
                    If timerPlayer2Clock.IsEnabled = False And Use_Clock.IsOn = True Then
                        timerPlayer2Clock.Start()
                    End If
                End If
            End If


            changeTurn.DataContext = "Change Turn"
            If changeTurnOnMoveLocationSelection.IsOn = True Then
                changeTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            Else
                changeTurn.IsEnabled = True
                changeTurn.Visibility = Windows.UI.Xaml.Visibility.Visible
            End If

            Exit Sub
        End If

        If timerBlinkSelectedPiece.IsEnabled = True Then
            timerBlinkSelectedPiece.Stop()
        End If

        BoardImage(pieceSelected.BoxName).Source = pieceSelected.Image

        isPieceSelected = False
        isLocationToMoveSelected = False
        isPieceCaptured = False
        PossiblePieceCollection.Clear()

        If insTurn.Color = Color1.White Then

            If WhiteKingIsCastling = True Then
                insWhiteMove.KingHasCastled = True
                insWhiteMove.CastledRookMove = insCastledRookMove
            End If

            Dim xeMove As New XElement("Move")
            Dim xaWhiteMoveNumber As New XAttribute("WhiteMoveNumber", whiteMoveNumber)
            Dim xePieceMoveNumber As New XElement("PieceMoveNumber", PieceCollection(pieceSelected.BoxName).MoveNumber.ToString)
            Dim xeMoveString As New XElement("MoveString", insWhiteMove.MoveString)
            Dim xePieceName As New XElement("PieceName", insWhiteMove.Piece.Name)
            Dim xeStartLocation As New XElement("StartLocation", insWhiteMove.StartLocation)
            Dim xeEndLocation As New XElement("EndLocation", insWhiteMove.EndLocation)
            Dim xePieceIsCaptured As New XElement("PieceIsCaptured", insWhiteMove.PieceIsCaptured)
            Dim xeKingIsChecked As New XElement("KingIsChecked", insWhiteMove.KingIsChecked)
            Dim xeWhiteKingWasChecked As New XElement("WhiteKingWasChecked", insWhiteMove.WhiteKingWasChecked)
            Dim xeKingCanCastleToQueenSide As New XElement("KingCanCastleToQueenSide", insWhiteMove.KingCanCastleToQueenSide)
            Dim xeKingCanCastleToKingSide As New XElement("KingCanCastleToKingSide", insWhiteMove.KingCanCastleToKingSide)
            Dim xeWhiteKingWasCastling As New XElement("WhiteKingWasCastling", insWhiteMove.WhiteKingWasCastling)
            Dim xeKingHasCastled As New XElement("KingHasCastled", insWhiteMove.KingHasCastled)
            Dim xePawnWasPromoted As New XElement("PawnWasPromoted", insWhiteMove.PawnWasPromoted)

            If whiteMoveNumber = 1 Then

                MoveXML.Element("Moves").Element("WhiteMoves").AddFirst(xeMove)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xaWhiteMoveNumber)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xePieceMoveNumber)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeMoveString)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xePieceName)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeStartLocation)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeEndLocation)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xePieceIsCaptured)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeKingIsChecked)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeWhiteKingWasChecked)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeKingCanCastleToQueenSide)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeKingCanCastleToKingSide)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeWhiteKingWasCastling)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeKingHasCastled)
                MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xePawnWasPromoted)

                If insWhiteMove.PieceIsCaptured = True Then

                    Dim xeCapturedPieceName As New XElement("CapturedPieceName", insWhiteMove.CapturedPiece.Name)
                    Dim xeCapturedMoveNumber As New XElement("CapturedMoveNumber", insWhiteMove.CapturedPiece.MoveNumber)
                    MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeCapturedPieceName)
                    MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeCapturedMoveNumber)

                    If insWhiteMove.PawnCapturedByEnPassant Is Nothing Then
                        Dim xeCapturedBoxName As New XElement("CapturedBoxName", insWhiteMove.CapturedPiece.BoxName)
                        MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeCapturedBoxName)
                    ElseIf insWhiteMove.PawnCapturedByEnPassant IsNot Nothing Then
                        Dim xeCapturedBoxName As New XElement("CapturedBoxName", insWhiteMove.CapturedPiece.BoxName)
                        MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeCapturedBoxName)

                        Dim xePawnCapturedByEnPassant As New XElement("PawnCapturedByEnPassant", insWhiteMove.PawnCapturedByEnPassant)
                        MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xePawnCapturedByEnPassant)

                    End If

                End If

                If insWhiteMove.KingHasCastled = True Then
                    Dim xeCastledRookName As New XElement("CastledRookName", insWhiteMove.CastledRookMove.Piece.Name)
                    Dim xeCastledRookStartLocation As New XElement("CastledRookStartLocation", insWhiteMove.CastledRookMove.StartLocation)
                    Dim xeCastledRookEndLocation As New XElement("CastledRookEndLocation", insWhiteMove.CastledRookMove.EndLocation)
                    MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeCastledRookName)
                    MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeCastledRookStartLocation)
                    MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeCastledRookEndLocation)

                End If

                If insWhiteMove.EnPassantPossibleOnMove IsNot Nothing Then
                    Dim xeEnPassantPossibleOnMove As New XElement("EnPassantPossibleOnMove", insWhiteMove.EnPassantPossibleOnMove)
                    MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xeEnPassantPossibleOnMove)
                End If

                If insWhiteMove.PawnWasPromoted = True Then
                    Dim xePromotedPawnName As New XElement("PromotedPawnName", insWhiteMove.PromotedPawnPiece.Name)
                    Dim xePromotedPawnBoxName As New XElement("PromotedPawnBoxName", insWhiteMove.PromotedPawnPiece.BoxName)
                    MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xePromotedPawnName)
                    MoveXML.Element("Moves").Element("WhiteMoves").Element("Move").Add(xePromotedPawnBoxName)
                End If

            Else
                xeMove.Add(xaWhiteMoveNumber)
                xeMove.Add(xePieceMoveNumber)
                xeMove.Add(xeMoveString)
                xeMove.Add(xePieceName)
                xeMove.Add(xeStartLocation)
                xeMove.Add(xeEndLocation)
                xeMove.Add(xePieceIsCaptured)
                xeMove.Add(xeKingIsChecked)
                xeMove.Add(xeWhiteKingWasChecked)
                xeMove.Add(xeKingCanCastleToQueenSide)
                xeMove.Add(xeKingCanCastleToKingSide)
                xeMove.Add(xeWhiteKingWasCastling)
                xeMove.Add(xeKingHasCastled)
                xeMove.Add(xePawnWasPromoted)

                If insWhiteMove.PieceIsCaptured = True Then
                    Dim xeCapturedPieceName As New XElement("CapturedPieceName", insWhiteMove.CapturedPiece.Name)
                    Dim xeCapturedBoxName As New XElement("CapturedBoxName", insWhiteMove.CapturedPiece.BoxName)
                    Dim xeCapturedMoveNumber As New XElement("CapturedMoveNumber", insWhiteMove.CapturedPiece.MoveNumber)
                    xeMove.Add(xeCapturedPieceName)
                    xeMove.Add(xeCapturedBoxName)
                    xeMove.Add(xeCapturedMoveNumber)
                    If insWhiteMove.PawnCapturedByEnPassant IsNot Nothing Then
                        Dim xePawnCapturedByEnPassant As New XElement("PawnCapturedByEnPassant", insWhiteMove.PawnCapturedByEnPassant)
                        xeMove.Add(xePawnCapturedByEnPassant)
                    End If

                End If

                If insWhiteMove.KingHasCastled = True Then
                    Dim xeCastledRookName As New XElement("CastledRookName", insWhiteMove.CastledRookMove.Piece.Name)
                    Dim xeCastledRookStartLocation As New XElement("CastledRookStartLocation", insWhiteMove.CastledRookMove.StartLocation)
                    Dim xeCastledRookEndLocation As New XElement("CastledRookEndLocation", insWhiteMove.CastledRookMove.EndLocation)
                    xeMove.Add(xeCastledRookName)
                    xeMove.Add(xeCastledRookStartLocation)
                    xeMove.Add(xeCastledRookEndLocation)

                End If

                If insWhiteMove.EnPassantPossibleOnMove IsNot Nothing Then
                    Dim xeEnPassantPossibleOnMove As New XElement("EnPassantPossibleOnMove", insWhiteMove.EnPassantPossibleOnMove)
                    xeMove.Add(xeEnPassantPossibleOnMove)
                End If

                If insWhiteMove.PawnWasPromoted = True Then
                    Dim xePromotedPawnName As New XElement("PromotedPawnName", insWhiteMove.PromotedPawnPiece.Name)
                    Dim xePromotedPawnBoxName As New XElement("PromotedPawnBoxName", insWhiteMove.PromotedPawnPiece.BoxName)
                    xeMove.Add(xePromotedPawnName)
                    xeMove.Add(xePromotedPawnBoxName)
                End If
                MoveXML.Element("Moves").Element("WhiteMoves").Add(xeMove)
            End If

            'If whiteMoveNumber = 1 Then
            'MoveXML.Element("Moves").Element("WhiteMoves").AddFirst(xeMove)
            'Else
            'MoveXML.Element("Moves").Element("WhiteMoves").Add(xeMove)
            'End If

            WhiteKingIsChecked = False
            WhiteKingWasChecked = False

            If WhiteKingIsCastling = True Then
                PieceCollection(pieceSelected2.BoxName).MoveNumber = PieceCollection(pieceSelected2.BoxName).MoveNumber + 1
            End If

            WhiteKingIsCastling = False
            WhiteKingWasCastling = False

            PieceCollection(pieceSelected.BoxName).MoveNumber = PieceCollection(pieceSelected.BoxName).MoveNumber + 1

            If insWhiteMove.PieceIsCaptured = True Then
                If CapturedPieceCollection.ContainsKey(insWhiteMove.CapturedPiece.Name) = False Then
                    CapturedPieceCollection.Add(insWhiteMove.CapturedPiece.Name, insWhiteMove.CapturedPiece)
                End If
            End If

            If WhiteMove.ContainsKey(whiteMoveNumber) = False Then
                WhiteMove.Add(whiteMoveNumber, insWhiteMove)
                whiteMoveString.Add(insWhiteMove.MoveString)
            End If
            displayWhiteMoves.ItemsSource = whiteMoveString

            insWhiteMove = New PossibleMove(whiteMoveNumber)
            whiteMoveNumber = whiteMoveNumber + 1


            insTurn.Color = Color1.Black

            If insTurn.PlayerNumber = Player.Player1 Then
                insTurn.PlayerNumber = Player.Player2
                If timerPlayer1Clock.IsEnabled = True Then
                    timerPlayer1Clock.Stop()
                End If
                If timerPlayer2Clock.IsEnabled = False Then
                    timerPlayer2Clock.Start()
                End If
            ElseIf insTurn.PlayerNumber = Player.Player2 Then
                insTurn.PlayerNumber = Player.Player1
                If timerPlayer2Clock.IsEnabled = True Then
                    timerPlayer2Clock.Stop()
                End If
                If timerPlayer1Clock.IsEnabled = False Then
                    timerPlayer1Clock.Start()
                End If
            End If

            CalculatePossibleMoves()

            If AllPossibleMovesFromAllPossiblePieces.Count = 0 Then
                If timerBlackKingChecked.IsEnabled = True Then
                    timerBlackKingChecked.Stop()
                End If
                CheckMate = True
                If timerBlackKingChecked.IsEnabled = False Then
                    timerBlackKingChecked.Start()
                End If
                Exit Sub
                changeTurn.Visibility = Windows.UI.Xaml.Visibility.Visible
                changeTurn.IsEnabled = True
            End If

            If boardRotation.IsOn = True Then
                If insTurn.PlayerNumber = Player.Player1 Then
                    rotateBoard.Angle = 0
                    changeTurn.RenderTransform = Player1ChangeTurn
                    displayWhiteTurn.RenderTransform = Player1DisplayWhiteTurn
                    displayWhiteMoves.RenderTransform = Player1DisplayWhiteMoves
                    displayBlackTurn.RenderTransform = Player1DisplayBlackTurn
                    displayBlackMoves.RenderTransform = Player1DisplayBlackMoves
                    boardRotation.RenderTransform = Player1SelectColor
                    NewGame.RenderTransform = Player1NewGame
                    Restart_Game.RenderTransform = Player1RestartGame
                    CapturedByMe.RenderTransform = Player1CapturedByMe
                    CapturedByOpponent.RenderTransform = Player1CapturedByOpponent
                    txtCapturedPieces.RenderTransform = Player1TxtCapturedPieces
                    MoveNumberLabel.RenderTransform = Player1MoveNumberLabel
                    MoveNumberBox.RenderTransform = Player1MoveNumberBox
                    Replay_Game.RenderTransform = Player1Replay_Game
                    Save_Game.RenderTransform = Player1Save_Game
                    ReplayGameLabel.RenderTransform = Player1ReplayGameLabel
                    Replay_Forward.RenderTransform = Player1Replay_Forward
                    Replay_Reverse.RenderTransform = Player1Replay_Reverse

                ElseIf insTurn.PlayerNumber = Player.Player2 Then
                    rotateBoard.Angle = 180
                    changeTurn.RenderTransform = Player2ChangeTurn
                    displayWhiteTurn.RenderTransform = Player2DisplayWhiteTurn
                    displayWhiteMoves.RenderTransform = Player2DisplayWhiteMoves
                    displayBlackTurn.RenderTransform = Player2DisplayBlackTurn
                    displayBlackMoves.RenderTransform = Player2DisplayBlackMoves
                    boardRotation.RenderTransform = Player2SelectColor
                    NewGame.RenderTransform = Player2NewGame
                    Restart_Game.RenderTransform = Player2RestartGame
                    CapturedByMe.RenderTransform = Player2CapturedByMe
                    CapturedByOpponent.RenderTransform = Player2CapturedByOpponent
                    txtCapturedPieces.RenderTransform = Player2TxtCapturedPieces
                    MoveNumberLabel.RenderTransform = Player2MoveNumberLabel
                    MoveNumberBox.RenderTransform = Player2MoveNumberBox
                    Replay_Game.RenderTransform = Player2Replay_Game
                    Save_Game.RenderTransform = Player2Save_Game
                    ReplayGameLabel.RenderTransform = Player2ReplayGameLabel
                    Replay_Forward.RenderTransform = Player2Replay_Forward
                    Replay_Reverse.RenderTransform = Player2Replay_Reverse

                End If
                Player1ClockDisplay.RenderTransform = rotateBoard
                Player2ClockDisplay.RenderTransform = rotateBoard
                txtMyName.RenderTransform = rotateBoard
                txtOpponentName.RenderTransform = rotateBoard
                'txtCapturedPieces.RenderTransform = rotateBoard
            End If

            For Each image1 In BoardImage
                BoardImage(image1.Key).RenderTransform = rotateBoard
            Next

            For Each piece1 In PieceCollection
                If BoardLocation(piece1.Key).SquareColor = Color1.White Then
                    PieceCollection(piece1.Key).Image = PieceCollection(piece1.Key).ImageOnWhiteSquare
                Else
                    PieceCollection(piece1.Key).Image = PieceCollection(piece1.Key).ImageOnBlackSquare
                End If
                BoardImage(piece1.Key).Source = PieceCollection(piece1.Key).Image
            Next



            displayWhiteTurn.DataContext = ""
            displayBlackTurn.DataContext = "Black's Turn"

            displayWhiteTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed
            displayBlackTurn.Visibility = Windows.UI.Xaml.Visibility.Visible


            boardRotation.IsEnabled = True
            changeTurn.IsEnabled = False
            NewGame.IsEnabled = True
            Exit Sub

        ElseIf insTurn.Color = Color1.Black Then

            If BlackKingIsCastling = True Then
                insBlackMove.KingHasCastled = True
                insBlackMove.CastledRookMove = insCastledRookMove
            End If

            Dim xeMove As New XElement("Move")
            Dim xaBlackMoveNumber As New XAttribute("BlackMoveNumber", blackMoveNumber)
            Dim xePieceMoveNumber As New XElement("PieceMoveNumber", PieceCollection(pieceSelected.BoxName).MoveNumber.ToString)
            Dim xeMoveString As New XElement("MoveString", insBlackMove.MoveString)
            Dim xePieceName As New XElement("PieceName", insBlackMove.Piece.Name)
            Dim xeStartLocation As New XElement("StartLocation", insBlackMove.StartLocation)
            Dim xeEndLocation As New XElement("EndLocation", insBlackMove.EndLocation)
            Dim xePieceIsCaptured As New XElement("PieceIsCaptured", insBlackMove.PieceIsCaptured)
            Dim xeKingIsChecked As New XElement("KingIsChecked", insBlackMove.KingIsChecked)
            Dim xeBlackKingWasChecked As New XElement("BlackKingWasChecked", insBlackMove.BlackKingWasChecked)
            Dim xeKingCanCastleToQueenSide As New XElement("KingCanCastleToQueenSide", insBlackMove.KingCanCastleToQueenSide)
            Dim xeKingCanCastleToKingSide As New XElement("KingCanCastleToKingSide", insBlackMove.KingCanCastleToKingSide)
            Dim xeBlackKingWasCastling As New XElement("BlackKingWasCastling", insBlackMove.BlackKingWasCastling)
            Dim xeKingHasCastled As New XElement("KingHasCastled", insBlackMove.KingHasCastled)
            Dim xePawnWasPromoted As New XElement("PawnWasPromoted", insBlackMove.PawnWasPromoted)

            If blackMoveNumber = 1 Then
                MoveXML.Element("Moves").Element("BlackMoves").AddFirst(xeMove)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xaBlackMoveNumber)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xePieceMoveNumber)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeMoveString)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xePieceName)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeStartLocation)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeEndLocation)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xePieceIsCaptured)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeKingIsChecked)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeBlackKingWasChecked)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeKingCanCastleToQueenSide)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeKingCanCastleToKingSide)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeBlackKingWasCastling)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeKingHasCastled)
                MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xePawnWasPromoted)

                If insBlackMove.PieceIsCaptured = True Then
                    Dim xeCapturedPieceName As New XElement("CapturedPieceName", insBlackMove.CapturedPiece.Name)
                    Dim xeCapturedBoxName As New XElement("CapturedBoxName", insBlackMove.CapturedPiece.BoxName)
                    Dim xeCapturedMoveNumber As New XElement("CapturedMoveNumber", insBlackMove.CapturedPiece.MoveNumber)
                    MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeCapturedPieceName)
                    MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeCapturedBoxName)
                    MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeCapturedMoveNumber)
                    If insBlackMove.PawnCapturedByEnPassant IsNot Nothing Then

                        Dim xePawnCapturedByEnPassant As New XElement("PawnCapturedByEnPassant", insBlackMove.PawnCapturedByEnPassant)
                        MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xePawnCapturedByEnPassant)

                    End If

                End If

                If insBlackMove.KingHasCastled = True Then
                    Dim xeCastledRookName As New XElement("CastledRookName", insBlackMove.CastledRookMove.Piece.Name)
                    Dim xeCastledRookStartLocation As New XElement("CastledRookStartLocation", insBlackMove.CastledRookMove.StartLocation)
                    Dim xeCastledRookEndLocation As New XElement("CastledRookEndLocation", insBlackMove.CastledRookMove.EndLocation)
                    MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeCastledRookName)
                    MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeCastledRookStartLocation)
                    MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeCastledRookEndLocation)

                End If

                If insBlackMove.EnPassantPossibleOnMove IsNot Nothing Then
                    Dim xeEnPassantPossibleOnMove As New XElement("EnPassantPossibleOnMove", insBlackMove.EnPassantPossibleOnMove)
                    MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xeEnPassantPossibleOnMove)
                End If

                If insBlackMove.PawnWasPromoted = True Then
                    Dim xePromotedPawnName As New XElement("PromotedPawnName", insBlackMove.PromotedPawnPiece.Name)
                    Dim xePromotedPawnBoxName As New XElement("PromotedPawnBoxName", insBlackMove.PromotedPawnPiece.BoxName)
                    MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xePromotedPawnName)
                    MoveXML.Element("Moves").Element("BlackMoves").Element("Move").Add(xePromotedPawnBoxName)
                End If

            Else
                xeMove.Add(xaBlackMoveNumber)
                xeMove.Add(xePieceMoveNumber)
                xeMove.Add(xeMoveString)
                xeMove.Add(xePieceName)
                xeMove.Add(xeStartLocation)
                xeMove.Add(xeEndLocation)
                xeMove.Add(xePieceIsCaptured)
                xeMove.Add(xeKingIsChecked)
                xeMove.Add(xeBlackKingWasChecked)
                xeMove.Add(xeKingCanCastleToQueenSide)
                xeMove.Add(xeKingCanCastleToKingSide)
                xeMove.Add(xeBlackKingWasCastling)
                xeMove.Add(xeKingHasCastled)
                xeMove.Add(xePawnWasPromoted)

                If insBlackMove.PieceIsCaptured = True Then
                    Dim xeCapturedPieceName As New XElement("CapturedPieceName", insBlackMove.CapturedPiece.Name)
                    Dim xeCapturedBoxName As New XElement("CapturedBoxName", insBlackMove.CapturedPiece.BoxName)
                    Dim xeCapturedMoveNumber As New XElement("CapturedMoveNumber", insBlackMove.CapturedPiece.MoveNumber)
                    xeMove.Add(xeCapturedPieceName)
                    xeMove.Add(xeCapturedBoxName)
                    xeMove.Add(xeCapturedMoveNumber)
                    If insBlackMove.PawnCapturedByEnPassant IsNot Nothing Then

                        Dim xePawnCapturedByEnPassant As New XElement("PawnCapturedByEnPassant", insBlackMove.PawnCapturedByEnPassant)
                        xeMove.Add(xePawnCapturedByEnPassant)

                    End If
                End If

                If insBlackMove.KingHasCastled = True Then
                    Dim xeCastledRookName As New XElement("CastledRookName", insBlackMove.CastledRookMove.Piece.Name)
                    Dim xeCastledRookStartLocation As New XElement("CastledRookStartLocation", insBlackMove.CastledRookMove.StartLocation)
                    Dim xeCastledRookEndLocation As New XElement("CastledRookEndLocation", insBlackMove.CastledRookMove.EndLocation)
                    xeMove.Add(xeCastledRookName)
                    xeMove.Add(xeCastledRookStartLocation)
                    xeMove.Add(xeCastledRookEndLocation)
                End If

                If insBlackMove.EnPassantPossibleOnMove IsNot Nothing Then
                    Dim xeEnPassantPossibleOnMove As New XElement("EnPassantPossibleOnMove", insBlackMove.EnPassantPossibleOnMove)
                    xeMove.Add(xeEnPassantPossibleOnMove)
                End If

                If insBlackMove.PawnWasPromoted = True Then
                    Dim xePromotedPawnName As New XElement("PromotedPawnName", insBlackMove.PromotedPawnPiece.Name)
                    Dim xePromotedPawnBoxName As New XElement("PromotedPawnBoxName", insBlackMove.PromotedPawnPiece.BoxName)
                    xeMove.Add(xePromotedPawnName)
                    xeMove.Add(xePromotedPawnBoxName)
                End If

                MoveXML.Element("Moves").Element("BlackMoves").Add(xeMove)
            End If

            BlackKingIsChecked = False
            BlackKingWasChecked = False

            If BlackKingIsCastling = True Then
                PieceCollection(pieceSelected2.BoxName).MoveNumber = PieceCollection(pieceSelected2.BoxName).MoveNumber + 1
            End If

            BlackKingIsCastling = False
            BlackKingWasCastling = False

            PieceCollection(pieceSelected.BoxName).MoveNumber = PieceCollection(pieceSelected.BoxName).MoveNumber + 1

            If insBlackMove.PieceIsCaptured = True Then
                If CapturedPieceCollection.ContainsKey(insBlackMove.CapturedPiece.Name) = False Then
                    CapturedPieceCollection.Add(insBlackMove.CapturedPiece.Name, insBlackMove.CapturedPiece)
                End If
            End If

            If BlackMove.ContainsKey(blackMoveNumber) = False Then
                BlackMove.Add(blackMoveNumber, insBlackMove)
                blackMoveString.Add(insBlackMove.MoveString)
            End If
            displayBlackMoves.ItemsSource = blackMoveString

            blackMoveNumber = blackMoveNumber + 1
            insBlackMove = New PossibleMove(blackMoveNumber)
            MoveNumberBox.DataContext = blackMoveNumber
            insTurn.Color = Color1.White

            If insTurn.PlayerNumber = Player.Player1 Then
                insTurn.PlayerNumber = Player.Player2
                If timerPlayer1Clock.IsEnabled = True Then
                    timerPlayer1Clock.Stop()
                End If
                If timerPlayer2Clock.IsEnabled = False Then
                    timerPlayer2Clock.Start()
                End If
            Else
                insTurn.PlayerNumber = Player.Player1
                If timerPlayer2Clock.IsEnabled = True Then
                    timerPlayer2Clock.Stop()
                End If
                If timerPlayer1Clock.IsEnabled = False Then
                    timerPlayer1Clock.Start()
                End If
            End If

            CalculatePossibleMoves()

            If AllPossibleMovesFromAllPossiblePieces.Count = 0 Then
                If timerWhiteKingChecked.IsEnabled = True Then
                    timerWhiteKingChecked.Stop()
                End If
                CheckMate = True
                If timerWhiteKingChecked.IsEnabled = False Then
                    timerWhiteKingChecked.Start()
                End If
                Exit Sub
                changeTurn.Visibility = Windows.UI.Xaml.Visibility.Visible
                changeTurn.IsEnabled = True
            End If

            If boardRotation.IsOn = True Then
                If insTurn.PlayerNumber = Player.Player1 Then
                    rotateBoard.Angle = 0
                    changeTurn.RenderTransform = Player1ChangeTurn
                    displayWhiteTurn.RenderTransform = Player1DisplayWhiteTurn
                    displayWhiteMoves.RenderTransform = Player1DisplayWhiteMoves
                    displayBlackTurn.RenderTransform = Player1DisplayBlackTurn
                    displayBlackMoves.RenderTransform = Player1DisplayBlackMoves
                    boardRotation.RenderTransform = Player1SelectColor
                    NewGame.RenderTransform = Player1NewGame
                    Restart_Game.RenderTransform = Player1RestartGame
                    CapturedByMe.RenderTransform = Player1CapturedByMe
                    CapturedByOpponent.RenderTransform = Player1CapturedByOpponent
                    txtCapturedPieces.RenderTransform = Player1TxtCapturedPieces
                    MoveNumberLabel.RenderTransform = Player1MoveNumberLabel
                    MoveNumberBox.RenderTransform = Player1MoveNumberBox
                    Replay_Game.RenderTransform = Player1Replay_Game
                    Save_Game.RenderTransform = Player1Save_Game
                    ReplayGameLabel.RenderTransform = Player1ReplayGameLabel
                    Replay_Forward.RenderTransform = Player1Replay_Forward
                    Replay_Reverse.RenderTransform = Player1Replay_Reverse

                Else
                    rotateBoard.Angle = 180
                    changeTurn.RenderTransform = Player2ChangeTurn
                    displayWhiteTurn.RenderTransform = Player2DisplayWhiteTurn
                    displayWhiteMoves.RenderTransform = Player2DisplayWhiteMoves
                    displayBlackTurn.RenderTransform = Player2DisplayBlackTurn
                    displayBlackMoves.RenderTransform = Player2DisplayBlackMoves
                    boardRotation.RenderTransform = Player2SelectColor
                    NewGame.RenderTransform = Player2NewGame
                    Restart_Game.RenderTransform = Player2RestartGame
                    CapturedByMe.RenderTransform = Player2CapturedByMe
                    CapturedByOpponent.RenderTransform = Player2CapturedByOpponent
                    txtCapturedPieces.RenderTransform = Player2TxtCapturedPieces
                    MoveNumberLabel.RenderTransform = Player2MoveNumberLabel
                    MoveNumberBox.RenderTransform = Player2MoveNumberBox
                    Replay_Game.RenderTransform = Player2Replay_Game
                    Save_Game.RenderTransform = Player2Save_Game
                    ReplayGameLabel.RenderTransform = Player2ReplayGameLabel
                    Replay_Forward.RenderTransform = Player2Replay_Forward
                    Replay_Reverse.RenderTransform = Player2Replay_Reverse

                End If
                Player1ClockDisplay.RenderTransform = rotateBoard
                Player2ClockDisplay.RenderTransform = rotateBoard
                txtMyName.RenderTransform = rotateBoard
                txtOpponentName.RenderTransform = rotateBoard
                'txtCapturedPieces.RenderTransform = rotateBoard
            End If

            For Each image1 In BoardImage
                BoardImage(image1.Key).RenderTransform = rotateBoard
            Next

            For Each piece1 In PieceCollection
                If BoardLocation(piece1.Key).SquareColor = Color1.White Then
                    PieceCollection(piece1.Key).Image = PieceCollection(piece1.Key).ImageOnWhiteSquare
                Else
                    PieceCollection(piece1.Key).Image = PieceCollection(piece1.Key).ImageOnBlackSquare
                End If
                BoardImage(piece1.Key).Source = PieceCollection(piece1.Key).Image
            Next

            displayWhiteTurn.DataContext = "White's Turn"
            displayBlackTurn.DataContext = ""

            displayWhiteTurn.Visibility = Windows.UI.Xaml.Visibility.Visible
            displayBlackTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed

            boardRotation.IsEnabled = True
            changeTurn.IsEnabled = False
            NewGame.IsEnabled = True

            Exit Sub

        End If

    End Sub

    Private Sub rotateControl80()

        CapturedPieces.Clear()
        For Each capturedpiece In WhiteCapturedPieces
            CapturedPieceOld = capturedpiece.Value
            CapturedPieceOld.Piece.SquareLocation = CaptureBoardLocation(TransposeCaptureBoard(capturedpiece.Key))
            CapturedPieceOld.Piece.BoxName = TransposeCaptureBoard(capturedpiece.Key)
            CapturedPieceOld.Piece.CapturedBoxName = TransposeCaptureBoard(capturedpiece.Key)
            CapturedPieces.Add(CapturedPieceOld.Piece.BoxName, CapturedPieceOld)
        Next

        WhiteCapturedPieces.Clear()
        For Each capturedPiece In CapturedPieces
            WhiteCapturedPieces.Add(capturedPiece.Key, capturedPiece.Value)
        Next

        CapturedPieces.Clear()
        For Each capturedpiece In BlackCapturedPieces
            CapturedPieceOld = capturedpiece.Value
            CapturedPieceOld.Piece.SquareLocation = CaptureBoardLocation(TransposeCaptureBoard(capturedpiece.Key))
            CapturedPieceOld.Piece.BoxName = TransposeCaptureBoard(capturedpiece.Key)
            CapturedPieceOld.Piece.CapturedBoxName = TransposeCaptureBoard(capturedpiece.Key)
            CapturedPieces.Add(CapturedPieceOld.Piece.BoxName, CapturedPieceOld)

        Next

        BlackCapturedPieces.Clear()
        For Each capturedPiece In CapturedPieces
            BlackCapturedPieces.Add(capturedPiece.Key, capturedPiece.Value)
        Next

        CapturedPieces.Clear()
        For Each square In ImagesOfCapturedPieces
            ImagesOfCapturedPieces(square.Key).Source = CaptureBoardLocation(square.Key).Image
        Next

        For Each capturedPiece In BlackCapturedPieces
            ImagesOfCapturedPieces(capturedPiece.Key).Source() = capturedPiece.Value.Piece.OriginalImage
        Next

        For Each capturedPiece In WhiteCapturedPieces
            ImagesOfCapturedPieces(capturedPiece.Key).Source() = capturedPiece.Value.Piece.OriginalImage
        Next

        OldPieceCollection.Clear()
        For Each piece1 In PieceCollection
            pieceSelectedOld = piece1.Value
            pieceSelectedOld.SquareLocation = BoardLocation(TransposeBoard(piece1.Key))
            pieceSelectedOld.BoxName = TransposeBoard(piece1.Key)
            pieceSelectedOld.CapturedBoxName = TransposeCaptureBoard(piece1.Value.CapturedBoxName)
            OldPieceCollection.Add(pieceSelectedOld.BoxName, pieceSelectedOld)
        Next


        PieceCollection.Clear()
        PieceCollection3.Clear()
        BoardStatus.Clear()

        For Each square In BoardLocation
            BoardStatus.Add(square.Key, Status.Open)
        Next

        For Each piece1 In OldPieceCollection
            PieceCollection.Add(piece1.Key, piece1.Value)
            PieceCollection3.Add(piece1.Value.Name, piece1.Key)
            BoardStatus.Remove(piece1.Key)
            BoardStatus.Add(piece1.Key, Status.Occupied)
        Next


        For Each square In BoardLocation
            If BoardStatus(square.Key) = Status.Open Then
                BoardImage(square.Key).Source = BoardLocation(square.Key).Image
            Else
                If BoardLocation(square.Key).SquareColor = Color1.White Then
                    PieceCollection(square.Key).Image = PieceCollection(square.Key).ImageOnWhiteSquare
                Else
                    PieceCollection(square.Key).Image = PieceCollection(square.Key).ImageOnBlackSquare
                End If
                BoardImage(square.Key).Source = PieceCollection(square.Key).Image
            End If
        Next

        For Each insWhiteMove1 In WhiteMove
            insWhiteMove1.Value.StartLocation = TransposeLocation(insWhiteMove1.Value.StartLocation)
            insWhiteMove1.Value.EndLocation = TransposeLocation(insWhiteMove1.Value.EndLocation)
            insWhiteMove1.Value.MoveString = insWhiteMove1.Value.Piece.Symbol + " " + insWhiteMove1.Value.StartLocation + ":" + insWhiteMove1.Value.EndLocation
            WhiteMoveOld.Add(insWhiteMove1.Key, insWhiteMove1.Value)
        Next

        WhiteMove.Clear()
        whiteMoveString.Clear()

        For Each insWhiteMove1 In WhiteMoveOld
            WhiteMove.Add(insWhiteMove1.Key, insWhiteMove1.Value)
            whiteMoveString.Add(insWhiteMove1.Value.MoveString)
        Next

        WhiteMoveOld.Clear()
        displayWhiteMoves.ItemsSource = whiteMoveString

        For Each insBlackMove1 In BlackMove
            insBlackMove1.Value.StartLocation = TransposeLocation(insBlackMove1.Value.StartLocation)
            insBlackMove1.Value.EndLocation = TransposeLocation(insBlackMove1.Value.EndLocation)
            insBlackMove1.Value.MoveString = insBlackMove1.Value.Piece.Symbol + " " + insBlackMove1.Value.StartLocation + ":" + insBlackMove1.Value.EndLocation
            BlackMoveOld.Add(insBlackMove1.Key, insBlackMove1.Value)
        Next

        BlackMove.Clear()
        blackMoveString.Clear()


        For Each insBlackMove1 In BlackMoveOld
            BlackMove.Add(insBlackMove1.Key, insBlackMove1.Value)
            blackMoveString.Add(insBlackMove1.Value.MoveString)
        Next

        BlackMoveOld.Clear()
        displayBlackMoves.ItemsSource = blackMoveString

    End Sub



    Private Sub timerBlinkSelectedPiece_tick()

        Dim insImage1 As Image

        If pieceSelected.Color = insTurn.Color Then
            If ImageOn = True Then
                BoardImage(pieceSelected.BoxName).Source = BoardLocation(PieceCollection3(pieceSelected.Name)).Image
                ImageOn = False
            Else
                insImage1 = New Image
                If BoardLocation(PieceCollection3(pieceSelected.Name)).SquareColor = Color1.White Then
                    pieceSelected.Image = pieceSelected.ImageOnWhiteSquare
                Else
                    pieceSelected.Image = pieceSelected.ImageOnBlackSquare
                End If
                BoardImage(pieceSelected.BoxName).Source = pieceSelected.Image
                ImageOn = True
            End If
        End If

    End Sub
    Public Sub timerWhiteKingChecked_tick()


        If insPlayers.WhitePlayer = Player.Player1 Then
            If WhiteKingCheckedImageOn = True Then
                If WhiteKingCheckedImageOn = True Then
                    If CheckMate = False Then
                        Player1Check.DataContext = "Check"
                    Else
                        Player1Check.DataContext = "CheckMate"
                    End If
                End If

                WhiteKingCheckedImageOn = False
            Else
                Player1Check.DataContext = ""
                WhiteKingCheckedImageOn = True
            End If
        Else
            If WhiteKingCheckedImageOn = True Then

                If CheckMate = False Then
                    Player2Check.DataContext = "Check"
                Else
                    Player2Check.DataContext = "CheckMate"
                End If

                WhiteKingCheckedImageOn = False
            Else
                Player2Check.DataContext = ""
                WhiteKingCheckedImageOn = True
            End If

        End If

    End Sub
    Public Sub timerBlackKingChecked_tick()


        If insPlayers.WhitePlayer = Player.Player1 Then
            If BlackKingCheckedImageOn = True Then
                If CheckMate = False Then
                    Player2Check.DataContext = "Check"
                Else
                    Player2Check.DataContext = "CheckMate"
                End If

                BlackKingCheckedImageOn = False
            Else
                Player2Check.DataContext = ""
                BlackKingCheckedImageOn = True
            End If
        Else
            If BlackKingCheckedImageOn = True Then

                If CheckMate = False Then
                    Player1Check.DataContext = "Check"
                Else
                    Player1Check.DataContext = "CheckMate"
                End If

                BlackKingCheckedImageOn = False
            Else
                Player1Check.DataContext = ""
                BlackKingCheckedImageOn = True
            End If

        End If
    End Sub

    Private Sub timerPlayer1Clock_tick()
        Player1Clock.TimeRemaining = Player1Clock.TimeRemaining.Subtract(timerPlayer1Clock.Interval)
        Player1ClockDisplay.DataContext = Player1Clock.TimeRemaining


    End Sub

    Private Sub timerPlayer2Clock_tick()
        Player2Clock.TimeRemaining = Player2Clock.TimeRemaining.Subtract(timerPlayer2Clock.Interval)
        Player2ClockDisplay.DataContext = Player2Clock.TimeRemaining

    End Sub
    Private Sub timerNewGame_tick()
        If NewGameImageOn = True Then
            NewGame.DataContext = ""
            NewGameImageOn = False
        Else
            NewGame.DataContext = "New Game"
            NewGameImageOn = True

        End If
    End Sub
    Private Sub timerStartClock_tick()
        If StartClockImageOn = True Then
            changeTurn.DataContext = ""
            StartClockImageOn = False
        Else
            changeTurn.DataContext = "Start Clock"
            StartClockImageOn = True

        End If
    End Sub



    Private Sub NewGame_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles NewGame.Tapped

        startNewGame = False

        displayWhiteTurn.Visibility = Windows.UI.Xaml.Visibility.Visible
        displayBlackTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        changeTurn.Visibility = Windows.UI.Xaml.Visibility.Visible
        Restart_Game.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        ReplayGameLabel.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Replay_Forward.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Replay_Reverse.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Save_Game.Visibility = Windows.UI.Xaml.Visibility.Collapsed

        changeTurn.DataContext = "Start Clock"
        If timerBlinkSelectedPiece.IsEnabled = True Then
            timerBlinkSelectedPiece.Stop()
        End If
        If timerPlayer1Clock.IsEnabled = True Then
            timerPlayer1Clock.Stop()
        End If
        If timerPlayer2Clock.IsEnabled = True Then
            timerPlayer2Clock.Stop()
        End If
        If timerWhiteKingChecked.IsEnabled = True Then
            timerWhiteKingChecked.Stop()
        End If
        If timerBlackKingChecked.IsEnabled = True Then
            timerBlackKingChecked.Stop()
        End If

        timerNewGame.Stop()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        If timerBlinkSelectedPiece.IsEnabled = True And replayGameIsEnabled = False Then
            timerBlinkSelectedPiece.Stop()
        End If
        If timerPlayer1Clock.IsEnabled = True Then
            timerPlayer1Clock.Stop()
        End If
        If timerPlayer2Clock.IsEnabled = True Then
            timerPlayer2Clock.Stop()
        End If
        If timerWhiteKingChecked.IsEnabled = True Then
            timerWhiteKingChecked.Stop()
        End If
        If timerBlackKingChecked.IsEnabled = True Then
            timerBlackKingChecked.Stop()
        End If

    End Sub

    Private Sub Save_Game_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles Save_Game.Tapped
        SaveMoveXML()
    End Sub

    Private Async Sub SaveMoveXML()

        savePicker = New FileSavePicker

        savePicker.SuggestedFileName = "ChessGame"
        savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary

        savePicker.FileTypeChoices.Add("XML", New String() {".xml"})
        Dim file1 As IStorageFile = Await savePicker.PickSaveFileAsync()

        If file1 IsNot Nothing Then
            ' At this point, the app can begin writing to the provided save file
            Await FileIO.WriteTextAsync(file1, MoveXML.ToString)
        End If

    End Sub


    Private Async Sub Replay_Game_Click(sender As Object, e As RoutedEventArgs) Handles Replay_Game.Click



        whiteMoveXML = New XDocument
        blackMoveXML = New XDocument
        MoveXML = New XDocument

        whiteMoveXML = XDocument.Load("Assets\WhiteMoveXML.xml")
        blackMoveXML = XDocument.Load("Assets\BlackMoveXML.xml")
        MoveXML = XDocument.Load("Assets\MoveXML.xml")


        Dim fileOpenPicker As New FileOpenPicker()

        fileOpenPicker.FileTypeFilter.Add(".xml")
        Dim file As IStorageFile = Await fileOpenPicker.PickSingleFileAsync()

        If file Is Nothing Then Exit Sub

        Dim contents As StringBuilder = New StringBuilder()
        Dim nextLine As String
        Dim counter As Integer


        Dim openPicker = New FileOpenPicker()
        openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary

        openPicker.FileTypeFilter.Add(".txt")

        Dim reader As StreamReader = New StreamReader(Await file.OpenStreamForReadAsync())
        nextLine = Await reader.ReadLineAsync()
        While (nextLine <> Nothing)
            contents.Append(nextLine)
            nextLine = Await reader.ReadLineAsync()
        End While

        MoveCollection.Clear()
        PieceCollection.Clear()
        PieceCollection3.Clear()
        OldPieceCollection.Clear()
        BoardStatus.Clear()

        changeTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Restart_Game.Visibility = Windows.UI.Xaml.Visibility.Visible
        ReplayGameLabel.Visibility = Windows.UI.Xaml.Visibility.Visible
        Replay_Forward.Visibility = Windows.UI.Xaml.Visibility.Visible
        Replay_Reverse.Visibility = Windows.UI.Xaml.Visibility.Visible
        Save_Game.Visibility = Windows.UI.Xaml.Visibility.Collapsed


        If timerBlinkSelectedPiece.IsEnabled = True Then
            timerBlinkSelectedPiece.Stop()
        End If
        If timerPlayer1Clock.IsEnabled = True Then
            timerPlayer1Clock.Stop()
        End If
        If timerPlayer2Clock.IsEnabled = True Then
            timerPlayer2Clock.Stop()
        End If
        If timerWhiteKingChecked.IsEnabled = True Then
            timerWhiteKingChecked.Stop()
        End If
        If timerBlackKingChecked.IsEnabled = True Then
            timerBlackKingChecked.Stop()
        End If


        ReplayMoveXML = New XDocument
        ReplayMoveXML = XDocument.Parse(contents.ToString)

        Dim queryReplayWhite = From Move In ReplayMoveXML...<Moves>...<WhiteMoves>...<Move>
        Dim queryReplayBlack = From Move In ReplayMoveXML...<Moves>...<BlackMoves>...<Move>
        Dim queryReplayGameOptions = From GameDetails In ReplayMoveXML...<Moves>...<GameDetails>


        If queryReplayGameOptions.Elements("WhitePlayer").Value = "0" Then
            insTurn = New Turn(Player.Player1)

        ElseIf queryReplayGameOptions.Elements("WhitePlayer").Value = "1" Then
            insTurn = New Turn(Player.Player1)

        End If

        insPlayers = New Players(insTurn)

        insPieceCollection = New PieceCollection1(insTurn.PlayerNumber)
        insPieceCollection3 = New PieceCollection2(insTurn.PlayerNumber)
        insOldPieceCollection = New OldPieceCollection1

        insBoardStatus = New BoardStatus1


        If queryReplayWhite.Count = queryReplayBlack.Count Then
            replayCount = queryReplayWhite.Count
        ElseIf queryReplayWhite.Count > queryReplayBlack.Count Then
            replayCount = queryReplayWhite.Count
        ElseIf queryReplayWhite.Count < queryReplayBlack.Count Then
            replayCount = queryReplayBlack.Count
        End If

        replayMoveCount = 1

        For counter = 1 To 2 * replayCount
            If replayMoveCount / 2 + 1 / 2 <= queryReplayWhite.Count And replayMoveCount Mod 2 <> 0 Then
                insReplayWhiteMove = New PossibleMove
                Dim queryReplayWhiteMove = From Move In queryReplayWhite _
                                           Where Move.Attribute("WhiteMoveNumber") = CStr(replayMoveCount / 2 + 1 / 2)

                insReplayWhiteMove.MoveNumber = CInt(replayMoveCount / 2 + 1 / 2)
                Dim piecename As String = queryReplayWhiteMove.Elements("PieceName").Value
                pieceSelected2 = New AnyPiece
                If PieceCollection3.ContainsKey(piecename) Then
                    pieceSelected = PieceCollection(PieceCollection3(piecename))
                Else
                    pieceSelected = PieceCollection(PieceCollection3("WhiteQueen"))
                End If

                insReplayWhiteMove.Piece = pieceSelected2
                insReplayWhiteMove.Piece.MoveNumber = CInt(queryReplayWhiteMove.Elements("PieceMoveNumber").Value)
                insReplayWhiteMove.Piece.Name = queryReplayWhiteMove.Elements("PieceName").Value


                insReplayWhiteMove.StartLocation = queryReplayWhiteMove.Elements("StartLocation").Value
                insReplayWhiteMove.EndLocation = queryReplayWhiteMove.Elements("EndLocation").Value

                insReplayWhiteMove.Piece.SquareLocation = BoardLocation(BoardLocator(insReplayWhiteMove.StartLocation))
                insReplayWhiteMove.Piece.BoxName = BoardLocator(insReplayWhiteMove.StartLocation)

                If insReplayWhiteMove.Piece.SquareLocation.SquareColor = Color1.White Then
                    insReplayWhiteMove.Piece.Image = insReplayWhiteMove.Piece.ImageOnWhiteSquare
                Else
                    insReplayWhiteMove.Piece.Image = insReplayWhiteMove.Piece.ImageOnBlackSquare
                End If

                insReplayWhiteMove.MoveString = queryReplayWhiteMove.Elements("MoveString").Value
                insReplayWhiteMove.PieceIsCaptured = queryReplayWhiteMove.Elements("PieceIsCaptured").Value

                If insReplayWhiteMove.PieceIsCaptured = True Then

                    Dim name As String
                    Dim boxname As String
                    Dim capturedmovenumber As String

                    name = queryReplayWhiteMove.Elements("CapturedPieceName").Value
                    boxname = queryReplayWhiteMove.Elements("CapturedBoxName").Value
                    capturedmovenumber = queryReplayWhiteMove.Elements("CapturedMoveNumber").Value

                    pieceSelected2 = New AnyPiece
                    pieceSelected2 = NewPiece(boxname, name)


                    insReplayWhiteMove.CapturedPiece = pieceSelected2
                    insReplayWhiteMove.CapturedPiece.MoveNumber = capturedmovenumber

                    If queryReplayWhiteMove.Elements("PawnCapturedByEnPassant").Value IsNot Nothing Then
                        insReplayWhiteMove.PawnCapturedByEnPassant = queryReplayWhiteMove.Elements("PawnCapturedByEnPassant").Value
                    End If

                End If

                insReplayWhiteMove.KingIsChecked = queryReplayWhiteMove.Elements("KingIsChecked").Value
                insReplayWhiteMove.WhiteKingWasChecked = queryReplayWhiteMove.Elements("WhiteKingWasChecked").Value
                insReplayWhiteMove.KingCanCastleToQueenSide = queryReplayWhiteMove.Elements("KingCanCastleToQueenSide").Value
                insReplayWhiteMove.KingCanCastleToKingSide = queryReplayWhiteMove.Elements("KingCanCastleToKingSide").Value
                insReplayWhiteMove.WhiteKingWasCastling = queryReplayWhiteMove.Elements("WhiteKingWasCastling").Value
                insReplayWhiteMove.KingHasCastled = queryReplayWhiteMove.Elements("KingHasCastled").Value

                If insReplayWhiteMove.KingHasCastled = True Then

                    Dim name As String
                    Dim startlocation As String
                    Dim endlocation As String

                    name = queryReplayWhiteMove.Elements("CastledRookName").Value
                    startlocation = queryReplayWhiteMove.Elements("CastledRookStartLocation").Value
                    endlocation = queryReplayWhiteMove.Elements("CastledRookEndLocation").Value

                    Dim startboxname As String = BoardLocator(startlocation)

                    pieceSelected2 = New AnyPiece
                    pieceSelected2 = NewPiece(startboxname, name)

                    insReplayWhiteMove.CastledRookMove = New PossibleMove

                    insReplayWhiteMove.CastledRookMove.Piece = pieceSelected2

                    insReplayWhiteMove.CastledRookMove.StartLocation = startlocation
                    insReplayWhiteMove.CastledRookMove.EndLocation = endlocation

                End If

                insReplayWhiteMove.PawnWasPromoted = queryReplayWhiteMove.Elements("PawnWasPromoted").Value
                If insReplayWhiteMove.PawnWasPromoted = True Then

                    Dim name As String = queryReplayWhiteMove.Elements("PromotedPawnName").Value
                    Dim boxname As String = queryReplayWhiteMove.Elements("PromotedPawnBoxName").Value
                    Dim movenumber As Integer = queryReplayWhiteMove.Elements("PieceMoveNumber").Value

                    pieceSelected2 = New AnyPiece
                    pieceSelected2 = NewPiece(boxname, name)

                    insReplayWhiteMove.PromotedPawnPiece = pieceSelected2
                    insReplayWhiteMove.PromotedPawnPiece.MoveNumber = movenumber - 1
                End If

                insReplayWhiteMove.EnPassantPossibleOnMove = queryReplayWhiteMove.Elements("EnPassantPossibleOnMove").Value

                MoveCollection.Add(CStr(replayMoveCount), insReplayWhiteMove)
                replayMoveCount = replayMoveCount + 1
            ElseIf replayMoveCount / 2 <= queryReplayBlack.Count And replayMoveCount Mod 2 = 0 Then
                insReplayBlackMove = New PossibleMove
                Dim queryReplayBlackMove = From Move In queryReplayBlack _
                                          Where Move.Attribute("BlackMoveNumber") = CStr(replayMoveCount / 2)

                insReplayBlackMove.MoveNumber = CInt(replayMoveCount / 2)
                Dim piecename As String = queryReplayBlackMove.Elements("PieceName").Value
                pieceSelected = New AnyPiece
                If PieceCollection3.ContainsKey(piecename) Then
                    pieceSelected = PieceCollection(PieceCollection3(piecename))
                Else
                    pieceSelected = PieceCollection(PieceCollection3("BlackQueen"))
                End If
                insReplayBlackMove.Piece = pieceSelected

                insReplayBlackMove.Piece.MoveNumber = CInt(queryReplayBlackMove.Elements("PieceMoveNumber").Value)
                insReplayBlackMove.Piece.Name = queryReplayBlackMove.Elements("PieceName").Value

                insReplayBlackMove.StartLocation = queryReplayBlackMove.Elements("StartLocation").Value
                insReplayBlackMove.EndLocation = queryReplayBlackMove.Elements("EndLocation").Value

                insReplayBlackMove.Piece.SquareLocation = BoardLocation(BoardLocator(insReplayBlackMove.StartLocation))
                insReplayBlackMove.Piece.BoxName = BoardLocator(insReplayBlackMove.StartLocation)

                If insReplayBlackMove.Piece.SquareLocation.SquareColor = Color1.White Then
                    insReplayBlackMove.Piece.Image = insReplayBlackMove.Piece.ImageOnWhiteSquare
                Else
                    insReplayBlackMove.Piece.Image = insReplayBlackMove.Piece.ImageOnBlackSquare
                End If

                insReplayBlackMove.MoveString = queryReplayBlackMove.Elements("MoveString").Value

                insReplayBlackMove.PieceIsCaptured = queryReplayBlackMove.Elements("PieceIsCaptured").Value
                If insReplayBlackMove.PieceIsCaptured = True Then

                    Dim name As String
                    Dim boxname As String
                    Dim capturedmovenumber As String

                    name = queryReplayBlackMove.Elements("CapturedPieceName").Value
                    boxname = queryReplayBlackMove.Elements("CapturedBoxName").Value
                    capturedmovenumber = queryReplayBlackMove.Elements("CapturedMoveNumber").Value

                    pieceSelected2 = New AnyPiece
                    pieceSelected2 = NewPiece(boxname, name)

                    insReplayBlackMove.CapturedPiece = New AnyPiece

                    insReplayBlackMove.CapturedPiece = pieceSelected2
                    insReplayBlackMove.CapturedPiece.MoveNumber = capturedmovenumber

                    If queryReplayBlackMove.Elements("PawnCapturedByEnPassant").Value IsNot Nothing Then
                        insReplayBlackMove.PawnCapturedByEnPassant = queryReplayBlackMove.Elements("PawnCapturedByEnPassant").Value
                    End If
                End If

                insReplayBlackMove.KingIsChecked = queryReplayBlackMove.Elements("KingIsChecked").Value
                insReplayBlackMove.WhiteKingWasChecked = queryReplayBlackMove.Elements("BlackKingWasChecked").Value
                insReplayBlackMove.KingCanCastleToQueenSide = queryReplayBlackMove.Elements("KingCanCastleToQueenSide").Value
                insReplayBlackMove.KingCanCastleToKingSide = queryReplayBlackMove.Elements("KingCanCastleToKingSide").Value
                insReplayBlackMove.WhiteKingWasCastling = queryReplayBlackMove.Elements("BlackKingWasCastling").Value
                insReplayBlackMove.KingHasCastled = queryReplayBlackMove.Elements("KingHasCastled").Value

                If insReplayBlackMove.KingHasCastled = True Then

                    Dim name As String
                    Dim startlocation As String
                    Dim endlocation As String

                    name = queryReplayBlackMove.Elements("CastledRookName").Value
                    startlocation = queryReplayBlackMove.Elements("CastledRookStartLocation").Value
                    endlocation = queryReplayBlackMove.Elements("CastledRookEndLocation").Value

                    Dim startboxname As String = BoardLocator(startlocation)

                    pieceSelected2 = New AnyPiece
                    pieceSelected2 = NewPiece(startboxname, name)

                    insReplayBlackMove.CastledRookMove = New PossibleMove

                    insReplayBlackMove.CastledRookMove.Piece = pieceSelected2
                    insReplayBlackMove.CastledRookMove.StartLocation = startlocation
                    insReplayBlackMove.CastledRookMove.EndLocation = endlocation
                End If

                insReplayBlackMove.PawnWasPromoted = queryReplayBlackMove.Elements("PawnWasPromoted").Value
                If insReplayBlackMove.PawnWasPromoted = True Then

                    Dim name As String = queryReplayBlackMove.Elements("PromotedPawnName").Value
                    Dim boxname As String = queryReplayBlackMove.Elements("PromotedPawnBoxName").Value
                    Dim movenumber As Integer = queryReplayBlackMove.Elements("PieceMoveNumber").Value

                    pieceSelected2 = New AnyPiece
                    pieceSelected2 = NewPiece(boxname, name)

                    insReplayBlackMove.PromotedPawnPiece = pieceSelected2
                    insReplayBlackMove.PromotedPawnPiece.MoveNumber = movenumber - 1
                End If


                insReplayBlackMove.EnPassantPossibleOnMove = queryReplayBlackMove.Elements("EnPassantPossibleOnMove").Value

                MoveCollection.Add(CStr(replayMoveCount), insReplayBlackMove)
                replayMoveCount = replayMoveCount + 1
            End If
        Next


        replayMoveCount = 1

        replayGameIsEnabled = True

        displayWhiteTurn.Visibility = Windows.UI.Xaml.Visibility.Visible
        displayBlackTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed

        Player1ClockDisplay.Visibility = Windows.UI.Xaml.Visibility.Collapsed
        Player2ClockDisplay.Visibility = Windows.UI.Xaml.Visibility.Collapsed


        If timerNewGame.IsEnabled = True Then
            timerNewGame.Stop()
        End If

        If timerStartClock.IsEnabled = True Then
            timerStartClock.Stop()
        End If

        startNewGame = False

        StartGameSub()

    End Sub


    Private Sub Replay_Forward_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles Replay_Forward.Tapped

        e.Handled = True
        Replay_Forward.IsEnabled = False

        If GameHasBeenSaved = True Then
            GameHasBeenSaved = False
            whiteMoveNumber = 1
            blackMoveNumber = 1
        End If

        If replayMoveCount > MoveCollection.Count Then
            Replay_Forward.IsEnabled = True
            Exit Sub
        End If
        If IsReplayForward = False Then
            replayMoveCount = replayMoveCount + 1
            IsReplayForward = True
        End If

        Dim insReplayMove3 As New PossibleMove
        insReplayMove3 = MoveCollection(replayMoveCount)


        If insTurn.Color = Color1.White Then
            whiteMoveNumber = CInt(Truncate(0.5 + replayMoveCount / 2))
            Dim queryReplayWhite = From Move In ReplayMoveXML...<Moves>...<WhiteMoves>...<Move>
            Dim queryReplayWhiteMove = From Move In queryReplayWhite _
                       Where Move.Attribute("WhiteMoveNumber") = CStr(whiteMoveNumber)

            insReplayMove3.Piece.MoveNumber = CInt(queryReplayWhiteMove.Elements("PieceMoveNumber").Value)
            PieceCollection(BoardLocator(insReplayMove3.StartLocation)).MoveNumber = insReplayMove3.Piece.MoveNumber
        Else
            Dim queryReplayBlack = From Move In ReplayMoveXML...<Moves>...<BlackMoves>...<Move>
            blackMoveNumber = CInt(Truncate(replayMoveCount / 2))
            Dim queryReplayBlackMove = From Move In queryReplayBlack _
                      Where Move.Attribute("BlackMoveNumber") = CStr(blackMoveNumber)

            insReplayMove3.Piece.MoveNumber = CInt(queryReplayBlackMove.Elements("PieceMoveNumber").Value)
            PieceCollection(BoardLocator(insReplayMove3.StartLocation)).MoveNumber = insReplayMove3.Piece.MoveNumber

        End If


        PossiblePieceCollection.Clear()

        CalculatePossibleMoves()

        sendername = BoardLocator(insReplayMove3.StartLocation)
        MakeMove(sendername)
        sendername = BoardLocator(insReplayMove3.EndLocation)
        MakeMove(sendername)

        replayMoveCount = replayMoveCount + 1

        IsReplayReverse = False
        IsReplayForward = True

        Replay_Forward.IsEnabled = True
    End Sub


    Private Sub Replay_Reverse_Tapped(sender As Object, e As TappedRoutedEventArgs) Handles Replay_Reverse.Tapped

        e.Handled = True
        Replay_Reverse.IsEnabled = False

        If replayMoveCount = 0 And IsReplayReverse = True Then
            replayMoveCount = 1
            Replay_Reverse.IsEnabled = True
            Exit Sub
        End If

        If replayMoveCount >= 1 Then

            If IsReplayForward = True Then
                If replayMoveCount = 1 Then
                    IsReplayReverse = False
                    Replay_Reverse.IsEnabled = True
                    Exit Sub
                End If

                replayMoveCount = replayMoveCount - 1
            End If



            Dim insReplayMove1 As New PossibleMove
            insReplayMove1 = MoveCollection(replayMoveCount)


            If insTurn.Color = Color1.White Then
                insTurn.Color = Color1.Black
                If insTurn.PlayerNumber = Player.Player1 Then
                    insTurn.PlayerNumber = Player.Player2
                Else
                    insTurn.PlayerNumber = Player.Player1
                End If
                If timerWhiteKingChecked.IsEnabled = True Then
                    timerWhiteKingChecked.Stop()
                End If

                MoveXML.Element("Moves").Element("BlackMoves").LastNode.Remove()
                BlackMove.Remove(blackMoveNumber - 1)
                blackMoveString.RemoveAt(blackMoveNumber - 2)
                displayBlackMoves.ItemsSource = blackMoveString
                blackMoveNumber = blackMoveNumber - 1
                displayWhiteTurn.DataContext = ""
                displayBlackTurn.DataContext = "Black's Turn"
                displayWhiteTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed
                displayBlackTurn.Visibility = Windows.UI.Xaml.Visibility.Visible

                MoveNumberBox.DataContext = blackMoveNumber
            ElseIf insTurn.Color = Color1.Black Then
                insTurn.Color = Color1.White
                If insTurn.PlayerNumber = Player.Player1 Then
                    insTurn.PlayerNumber = Player.Player2
                Else
                    insTurn.PlayerNumber = Player.Player1
                End If
                If timerBlackKingChecked.IsEnabled = True Then
                    timerBlackKingChecked.Stop()
                End If

                MoveXML.Element("Moves").Element("WhiteMoves").LastNode.Remove()
                WhiteMove.Remove(whiteMoveNumber - 1)
                whiteMoveString.RemoveAt(whiteMoveNumber - 2)
                displayWhiteMoves.ItemsSource = whiteMoveString
                whiteMoveNumber = whiteMoveNumber - 1
                displayWhiteTurn.DataContext = "White's Turn"
                displayBlackTurn.DataContext = ""
                displayWhiteTurn.Visibility = Windows.UI.Xaml.Visibility.Visible
                displayBlackTurn.Visibility = Windows.UI.Xaml.Visibility.Collapsed

                MoveNumberBox.DataContext = blackMoveNumber
            End If

            If boardRotation.IsOn = True Then
                If insTurn.PlayerNumber = Player.Player1 Then
                    rotateBoard.Angle = 0
                    changeTurn.RenderTransform = Player1ChangeTurn
                    displayWhiteTurn.RenderTransform = Player1DisplayWhiteTurn
                    displayWhiteMoves.RenderTransform = Player1DisplayWhiteMoves
                    displayBlackTurn.RenderTransform = Player1DisplayBlackTurn
                    displayBlackMoves.RenderTransform = Player1DisplayBlackMoves
                    boardRotation.RenderTransform = Player1SelectColor
                    NewGame.RenderTransform = Player1NewGame
                    Restart_Game.RenderTransform = Player1RestartGame
                    CapturedByMe.RenderTransform = Player1CapturedByMe
                    CapturedByOpponent.RenderTransform = Player1CapturedByOpponent
                    txtCapturedPieces.RenderTransform = Player1TxtCapturedPieces
                    MoveNumberLabel.RenderTransform = Player1MoveNumberLabel
                    MoveNumberBox.RenderTransform = Player1MoveNumberBox
                    Replay_Game.RenderTransform = Player1Replay_Game
                    Save_Game.RenderTransform = Player1Save_Game
                    ReplayGameLabel.RenderTransform = Player1ReplayGameLabel
                    Replay_Forward.RenderTransform = Player1Replay_Forward
                    Replay_Reverse.RenderTransform = Player1Replay_Reverse

                ElseIf insTurn.PlayerNumber = Player.Player2 Then
                    rotateBoard.Angle = 180
                    changeTurn.RenderTransform = Player2ChangeTurn
                    displayWhiteTurn.RenderTransform = Player2DisplayWhiteTurn
                    displayWhiteMoves.RenderTransform = Player2DisplayWhiteMoves
                    displayBlackTurn.RenderTransform = Player2DisplayBlackTurn
                    displayBlackMoves.RenderTransform = Player2DisplayBlackMoves
                    boardRotation.RenderTransform = Player2SelectColor
                    NewGame.RenderTransform = Player2NewGame
                    Restart_Game.RenderTransform = Player2RestartGame
                    CapturedByMe.RenderTransform = Player2CapturedByMe
                    CapturedByOpponent.RenderTransform = Player2CapturedByOpponent
                    txtCapturedPieces.RenderTransform = Player2TxtCapturedPieces
                    MoveNumberLabel.RenderTransform = Player2MoveNumberLabel
                    MoveNumberBox.RenderTransform = Player2MoveNumberBox
                    Replay_Game.RenderTransform = Player2Replay_Game
                    Save_Game.RenderTransform = Player2Save_Game
                    ReplayGameLabel.RenderTransform = Player2ReplayGameLabel
                    Replay_Forward.RenderTransform = Player2Replay_Forward
                    Replay_Reverse.RenderTransform = Player2Replay_Reverse

                End If
                Player1ClockDisplay.RenderTransform = rotateBoard
                Player2ClockDisplay.RenderTransform = rotateBoard
                txtMyName.RenderTransform = rotateBoard
                txtOpponentName.RenderTransform = rotateBoard
                'txtCapturedPieces.RenderTransform = rotateBoard
            End If



            MakeMoveBackwards(insReplayMove1)

            If insReplayMove1.BlackKingWasChecked = True Then
                BlackKingWasChecked = True
                BlackKingIsChecked = True
                If timerBlackKingChecked.IsEnabled = False Then
                    timerBlackKingChecked.Start()
                End If
            End If

            If insReplayMove1.WhiteKingWasChecked = True Then
                WhiteKingWasChecked = True
                WhiteKingIsChecked = True

                If timerWhiteKingChecked.IsEnabled = False Then
                    timerWhiteKingChecked.Start()
                End If
            End If

            If insReplayMove1.BlackKingWasChecked = False Then
                BlackKingWasChecked = False
                BlackKingIsChecked = False
            End If
            If insReplayMove1.WhiteKingWasChecked = False Then
                WhiteKingWasChecked = False
                WhiteKingIsChecked = False

            End If

            CalculatePossibleMoves()

            If replayMoveCount > 1 Then
                replayMoveCount = replayMoveCount - 1


            ElseIf replayMoveCount = 1 Then
                IsReplayReverse = True
                IsReplayForward = True
                Replay_Reverse.IsEnabled = True
                Exit Sub
            End If
            IsReplayForward = False
            IsReplayReverse = True
            Replay_Reverse.IsEnabled = True
            Exit Sub

        End If

    End Sub

    Private Sub MakeMoveBackwards(ByVal insReplayMove2 As PossibleMove)
        pieceSelected = New AnyPiece
        pieceSelected = PieceCollection(PieceCollection3(insReplayMove2.Piece.Name))

        Dim startBoxName As String = BoardLocator(insReplayMove2.EndLocation)
        Dim endBoxName As String = BoardLocator(insReplayMove2.StartLocation)

        pieceSelected.MoveNumber = insReplayMove2.Piece.MoveNumber
        BoardImage(startBoxName).Source = BoardLocation(startBoxName).Image

        BoardStatus.Remove(startBoxName)
        BoardStatus.Add(startBoxName, Status.Open)

        If PieceCollection3.ContainsKey(pieceSelected.Name) = True Then
            PieceCollection.Remove(PieceCollection3(pieceSelected.Name))
            PieceCollection3.Remove(pieceSelected.Name)
        End If

        pieceSelected.BoxName = endBoxName
        pieceSelected.SquareLocation = BoardLocation(endBoxName)

        If pieceSelected.MoveNumber > 1 Then
            pieceSelected.MoveNumber = pieceSelected.MoveNumber - 1
        End If

        If pieceSelected.SquareLocation.SquareColor = Color1.White Then
            pieceSelected.Image = pieceSelected.ImageOnWhiteSquare
        Else
            pieceSelected.Image = pieceSelected.ImageOnBlackSquare
        End If
        BoardImage(endBoxName).Source = pieceSelected.Image

        If insReplayMove2.PieceIsCaptured = True Then

            pieceSelected2 = New AnyPiece
            pieceSelected2 = insReplayMove2.CapturedPiece


            If ImagesOfCapturedPieces.ContainsKey(insReplayMove2.CapturedPiece.CapturedBoxName) = True Then
                If CaptureBoardLocation(insReplayMove2.CapturedPiece.CapturedBoxName).SquareColor = Color1.White Then
                    ImagesOfCapturedPieces(insReplayMove2.CapturedPiece.CapturedBoxName).Source = WOS
                ElseIf CaptureBoardLocation(insReplayMove2.CapturedPiece.CapturedBoxName).SquareColor = Color1.Black Then
                    ImagesOfCapturedPieces(insReplayMove2.CapturedPiece.CapturedBoxName).Source = BOS
                End If
            End If

            If WhiteCapturedPieces.ContainsKey(insReplayMove2.CapturedPiece.CapturedBoxName) = True Then
                WhiteCapturedPieces.Remove(insReplayMove2.CapturedPiece.CapturedBoxName)
            ElseIf BlackCapturedPieces.ContainsKey(insReplayMove2.CapturedPiece.CapturedBoxName) = True Then
                BlackCapturedPieces.Remove(insReplayMove2.CapturedPiece.CapturedBoxName)
            End If


            BoardImage(insReplayMove2.CapturedPiece.BoxName).Source = pieceSelected2.Image

            BoardStatus.Remove(insReplayMove2.CapturedPiece.BoxName)
            BoardStatus.Add(insReplayMove2.CapturedPiece.BoxName, Status.Occupied)

            If PieceCollection.ContainsKey(pieceSelected2.BoxName) = False Then
                PieceCollection.Add(pieceSelected2.BoxName, pieceSelected2)
                PieceCollection3.Add(pieceSelected2.Name, pieceSelected2.BoxName)
            End If
        End If

        If insReplayMove2.KingHasCastled = True Then

            pieceSelected2 = New AnyPiece
            pieceSelected2 = insReplayMove2.CastledRookMove.Piece

            Dim startBoxName1 As String = BoardLocator(insReplayMove2.CastledRookMove.EndLocation)
            Dim endBoxName1 As String = BoardLocator(insReplayMove2.CastledRookMove.StartLocation)

            BoardImage(startBoxName1).Source = BoardLocation(startBoxName1).Image
            BoardImage(endBoxName1).Source = pieceSelected2.Image

            BoardStatus.Remove(startBoxName1)
            BoardStatus.Add(startBoxName1, Status.Open)

            BoardStatus.Remove(endBoxName1)
            BoardStatus.Add(endBoxName1, Status.Occupied)

            PieceCollection.Remove(startBoxName1)
            PieceCollection3.Remove(pieceSelected2.Name)

            PieceCollection.Add(endBoxName1, pieceSelected2)
            PieceCollection3.Add(pieceSelected2.Name, pieceSelected2.BoxName)

        End If

        If insReplayMove2.PawnWasPromoted = True Then

            If insReplayMove2.Piece.Color = Color1.White Then
                promotedWhitePawnCount = promotedWhitePawnCount - 1
            ElseIf insReplayMove2.Piece.Color = Color1.Black Then
                promotedBlackPawnCount = promotedBlackPawnCount - 1
            End If



            BoardImage(insReplayMove2.PromotedPawnPiece.BoxName).Source = BoardLocation(insReplayMove2.PromotedPawnPiece.BoxName).Image

            BoardStatus.Remove(insReplayMove2.PromotedPawnPiece.BoxName)
            BoardStatus.Add(insReplayMove2.PromotedPawnPiece.BoxName, Status.Open)

            If PieceCollection3.ContainsKey(insReplayMove2.PromotedPawnPiece.Name) = True Then
                PieceCollection.Remove(PieceCollection3(insReplayMove2.PromotedPawnPiece.Name))
                PieceCollection3.Remove(insReplayMove2.PromotedPawnPiece.Name)
            End If

            pieceSelected = New AnyPiece
            pieceSelected = insReplayMove2.Piece

            Dim endBoxName1 As String = BoardLocator(insReplayMove2.StartLocation)

            BoardImage(pieceSelected.BoxName).Source = pieceSelected.Image

            BoardStatus.Remove(endBoxName1)
            BoardStatus.Add(endBoxName1, Status.Occupied)

            PieceCollection.Add(pieceSelected.BoxName, pieceSelected)
            PieceCollection3.Add(pieceSelected.Name, pieceSelected.BoxName)

            Exit Sub
        End If

        If insReplayMove2.KingIsChecked = True Then
            If pieceSelected.Color = Color1.White Then
                If timerBlackKingChecked.IsEnabled = True Then
                    timerBlackKingChecked.Stop()
                End If
                If insPlayers.BlackPlayer = Player.Player1 Then
                    Player1Check.DataContext = ""
                ElseIf insPlayers.BlackPlayer = Player.Player2 Then
                    Player2Check.DataContext = ""
                End If

            Else
                If timerWhiteKingChecked.IsEnabled = True Then
                    timerWhiteKingChecked.Stop()
                End If
                If insPlayers.WhitePlayer = Player.Player1 Then
                    Player1Check.DataContext = ""
                ElseIf insPlayers.WhitePlayer = Player.Player2 Then
                    Player2Check.DataContext = ""
                End If


            End If
        End If


        BoardImage(pieceSelected.BoxName).Source = pieceSelected.Image

        BoardStatus.Remove(endBoxName)
        BoardStatus.Add(endBoxName, Status.Occupied)

        PieceCollection.Add(pieceSelected.BoxName, pieceSelected)
        PieceCollection3.Add(pieceSelected.Name, pieceSelected.BoxName)


    End Sub
End Class
