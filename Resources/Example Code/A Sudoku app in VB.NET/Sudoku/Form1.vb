Public Class Form1
    Const cellWidth As Integer = 32
    Const cellHeight As Integer = 32

    Const xOffset As Integer = -20
    Const yoffset As Integer = 25

    Private DEFAULT_BACKCOLOR As Color = Color.White

    Private FIXED_FORECOLOR As Color = Color.Blue
    Private FIXED_BACKCOLOR As Color = Color.LightSteelBlue

    Private USER_FORECOLOR As Color = Color.Black
    Private USER_BACKCOLOR As Color = Color.LightYellow

    Private SelectedNumber As Integer

    Private Moves As Stack(Of String)
    Private RedoMoves As Stack(Of String)

    Private saveFileName As String = String.Empty

    Private actual(9, 9) As Integer

    Private seconds As Integer

    Private gameStarted As Boolean = False

    Private possible(9, 9) As String
    Private HintMode As Boolean

    Public Sub DrawBoard()
        ToolStripButton1.Checked = True
        SelectedNumber = 1

        Dim location As New Point

        For row As Integer = 1 To 9
            For col As Integer = 1 To 9
                location.X = col * (cellWidth + 1) + xOffset
                location.Y = row * (cellHeight + 1) + yoffset
                Dim lbl As New Label
                With lbl
                    .Name = col.ToString() & row.ToString()
                    .BorderStyle = BorderStyle.Fixed3D
                    .Location = location
                    .Width = cellWidth
                    .Height = cellHeight
                    .TextAlign = ContentAlignment.MiddleCenter
                    .BackColor = DEFAULT_BACKCOLOR
                    .Font = New Font(.Font, .Font.Style Or FontStyle.Bold)
                    .Tag = 1
                    AddHandler lbl.Click, AddressOf Cell_Click
                End With
                Me.Controls.Add(lbl)
            Next
        Next
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToolStripStatusLabel1.Text = String.Empty
        ToolStripStatusLabel2.Text = String.Empty

        DrawBoard()
    End Sub

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim x1, y1, x2, y2 As Integer
        x1 = 1 * (cellWidth + 1) + xOffset - 1
        x2 = 9 * (cellWidth + 1) + xOffset + cellWidth
        For r As Integer = 1 To 10 Step 3
            y1 = r * (cellHeight + 1) + yoffset - 1
            y2 = y1
            e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2)
        Next
        y1 = 1 * (cellHeight + 1) + yoffset - 1
        y2 = 9 * (cellHeight + 1) + yoffset + cellHeight
        For c As Integer = 1 To 10 Step 3
            x1 = c * (cellWidth + 1) + xOffset - 1
            x2 = x1
            e.Graphics.DrawLine(Pens.Black, x1, y1, x2, y2)
        Next
    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click
        If gameStarted Then
            Dim response As MsgBoxResult =
            MessageBox.Show("Do you want to save current game?",
            "Save current game",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question)
            If response = MsgBoxResult.Yes Then
                SaveGameToDisk(False)
            ElseIf response = MsgBoxResult.Cancel Then
                Return
            End If
        End If
        StartNewGame()
    End Sub

    Public Sub SaveGameToDisk(ByVal saveAs As Boolean)
        '---if saveFileName is empty, means game has not been saved
        ' before---
        If saveFileName = String.Empty OrElse saveAs Then
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter =
            "SDO files (*.sdo)|*.sdo|All files (*.*)|*.*"
            saveFileDialog1.FilterIndex = 1
            saveFileDialog1.RestoreDirectory = False
            If saveFileDialog1.ShowDialog() =
            Windows.Forms.DialogResult.OK Then
                '---store the filename first---
                saveFileName = saveFileDialog1.FileName
            Else
                Return
            End If
        End If
        '---formulate the string representing the values to store---
        Dim str As New System.Text.StringBuilder()
        For row As Integer = 1 To 9
            For col As Integer = 1 To 9
                str.Append(actual(col, row).ToString())
            Next
        Next
        '---save the values to file---
        Try
            Dim fileExists As Boolean
            fileExists =
            My.Computer.FileSystem.FileExists(saveFileName)
            If fileExists Then _
            My.Computer.FileSystem.DeleteFile(saveFileName)
            My.Computer.FileSystem.WriteAllText(saveFileName,
            str.ToString(), True)
            ToolStripStatusLabel1.Text = "Puzzle saved in " &
            saveFileName
        Catch ex As Exception
            MsgBox("Error saving game. Please try again.")
        End Try
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(
            ByVal sender As System.Object,
            ByVal e As System.EventArgs) _
            Handles SaveAsToolStripMenuItem.Click
        If Not gameStarted Then
            DisplayActivity("Game not started yet.", True)
            Return
        End If
        SaveGameToDisk(True)
    End Sub

    Private Sub SaveToolStripMenuItem_Click(
            ByVal sender As System.Object,
            ByVal e As System.EventArgs) _
            Handles SaveToolStripMenuItem.Click
        If Not gameStarted Then
            DisplayActivity("Game not started yet.", True)
            Return
        End If
        SaveGameToDisk(False)
    End Sub

    Private Sub OpenToolStripMenuItem_Click(
            ByVal sender As System.Object,
            ByVal e As System.EventArgs) _
            Handles OpenToolStripMenuItem.Click
        If gameStarted Then
            Dim response As MsgBoxResult =
            MessageBox.Show("Do you want to save current game?",
            "Save current game",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question)
            If response = MsgBoxResult.Yes Then
                SaveGameToDisk(False)
            ElseIf response = MsgBoxResult.Cancel Then
                Return
            End If
        End If
        '---load the game from disk---
        Dim fileContents As String
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.Filter = "SDO files (*.sdo)|*.sdo|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        openFileDialog1.RestoreDirectory = False
        If openFileDialog1.ShowDialog() =
        Windows.Forms.DialogResult.OK Then
            fileContents =
            My.Computer.FileSystem.ReadAllText(
            openFileDialog1.FileName)
            ToolStripStatusLabel1.Text = openFileDialog1.FileName
            saveFileName = openFileDialog1.FileName
        Else
            Return
        End If
        StartNewGame()
        '---initialize the board---
        Dim counter As Short = 0
        For row As Integer = 1 To 9
            For col As Integer = 1 To 9
                Try
                    If CInt(fileContents(counter).ToString()) <> 0 Then
                        SetCell(col, row, CInt(fileContents(counter).ToString()), 0)
                    End If
                Catch ex As Exception
                    MsgBox("File does not contain a valid Sudoku puzzle")
                    Exit Sub
                End Try
                counter += 1
            Next
        Next
    End Sub

    Private Sub ExitToolStripMenuItem_Click(
            ByVal sender As System.Object,
            ByVal e As System.EventArgs) _
            Handles ExitToolStripMenuItem.Click
        If gameStarted Then
            Dim response As MsgBoxResult =
            MsgBox("Do you want to save current game?",
            MsgBoxStyle.YesNoCancel, "Save current game")
            If response = MsgBoxResult.Yes Then
                SaveGameToDisk(False)
            ElseIf response = MsgBoxResult.Cancel Then
                Return
            End If
        End If
        '---exit the application---
        End
    End Sub

    Public Sub StartNewGame()
        saveFileName = String.Empty

        txtActivities.Text = String.Empty
        seconds = 0
        ClearBoard()
        gameStarted = True
        Timer1.Enabled = True
        ToolStripStatusLabel1.Text = "New game started"

        ToolTip1.RemoveAll()
    End Sub

    Public Function CalculatePossibleValues(
            ByVal col As Integer,
            ByVal row As Integer) As String
        '---get the current possible values for the cell
        Dim str As String
        If possible(col, row) = String.Empty Then
            str = "123456789"
        Else
            str = possible(col, row)
        End If
        '---Step (1) check by column
        Dim r, c As Integer
        For r = 1 To 9
            If actual(col, r) <> 0 Then
                '---that means there is an actual value in it---
                str = str.Replace(actual(col, r).ToString(), String.Empty)
            End If
        Next
        '---Step (2) check by row
        For c = 1 To 9
            If actual(c, row) <> 0 Then
                '---that means there is an actual value in it---
                str = str.Replace(actual(c, row).ToString(), String.Empty)
            End If
        Next
        '---Step (3) check within the minigrid---
        Dim startC, startR As Integer
        startC = col - ((col - 1) Mod 3)
        startR = row - ((row - 1) Mod 3)
        For rr As Integer = startR To startR + 2
            For cc As Integer = startC To startC + 2
                If actual(cc, rr) <> 0 Then
                    '---that means there is a actual value in it---
                    str = str.Replace(actual(cc, rr).ToString(), String.Empty)
                End If
            Next
        Next
        '---if possible value is an empty string then error because of
        ' invalid move---
        If str = String.Empty Then
            Throw New Exception("Invalid Move")
        End If
        Return str
    End Function

    Public Function CheckColumnsAndRows() As Boolean
        Dim changes As Boolean = False
        '---check all cells
        For row As Integer = 1 To 9
            For col As Integer = 1 To 9
                If actual(col, row) = 0 Then
                    Try
                        possible(col, row) = CalculatePossibleValues(col, row)
                    Catch ex As Exception
                        DisplayActivity("Invalid placement, please undo move", False)
                        Throw New Exception("Invalid Move")
                    End Try
                    '---display the possible values in the ToolTip
                    SetToolTip(col, row, possible(col, row))
                    If possible(col, row).Length = 1 Then
                        '---that means a number is confirmed---
                        SetCell(col, row, CInt(possible(col, row)), 1)
                        '----Number is confirmed
                        actual(col, row) = CInt(possible(col, row))
                        DisplayActivity("Col/Row and Minigrid Elimination", False)
                        DisplayActivity("=========================", False)
                        DisplayActivity("Inserted value " & actual(col, row) & " in " & "(" & col & "," & row & ")", False)
                        '---get the UI of the application to refresh
                        ' with the newly confirmed number---
                        Application.DoEvents()
                        '---saves the move into the stack
                        Moves.Push(col & row & possible(col, row))
                        '---if user only asks for a hint, stop at this point---
                        changes = True
                        If HintMode Then Return True
                    End If
                End If
            Next
        Next
        Return changes
    End Function

    Public Sub ClearBoard()
        Moves = New Stack(Of String)
        RedoMoves = New Stack(Of String)
        For row As Integer = 1 To 9
            For col As Integer = 1 To 9
                SetCell(col, row, 0, 1)
            Next
        Next
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ToolStripStatusLabel2.Text = "Elapsed time: " &
        seconds & " second(s)"
        seconds += 1
    End Sub

    Private Sub ToolStripButton_Click(
            ByVal sender As System.Object,
            ByVal e As System.EventArgs) _
            Handles _
            ToolStripButton1.Click,
            ToolStripButton2.Click,
            ToolStripButton3.Click,
            ToolStripButton4.Click,
            ToolStripButton5.Click,
            ToolStripButton6.Click,
            ToolStripButton7.Click,
            ToolStripButton8.Click,
            ToolStripButton9.Click,
            ToolStripButton10.Click
        Dim selectedButton As ToolStripButton = CType(sender, ToolStripButton)
        '---uncheck all the Button controls in the ToolStrip---
        '---ToolStrip1.Items.Item(0) is "Select Number"
        '---ToolStrip1.Items.Item(1) is "1"
        '---ToolStrip1.Items.Item(2) is "2", etc
        '---ToolStrip1.Items.Item(10) is "Erase", etc
        For i As Integer = 1 To 10
            CType(ToolStrip1.Items.Item(i), ToolStripButton).Checked = False
        Next

        selectedButton.Checked = True

        If selectedButton.Text = "Erase" Then
            SelectedNumber = 0
        Else
            SelectedNumber = CInt(selectedButton.Text)
        End If
    End Sub

    Private Sub Cell_Click(
            ByVal sender As System.Object,
            ByVal e As System.EventArgs)
        '---check to see if game has even started or not---
        If Not gameStarted Then
            DisplayActivity("Click File->New to start a new" &
            " game or File->Open to load an existing game", True)
            Return
        End If
        Dim cellLabel As Label = CType(sender, Label)
        '---if cell is not erasable then exit---
        If cellLabel.Tag.ToString() = "0" Then
            DisplayActivity("Selected cell is not empty", False)
            Return
        End If
        '---determine the col and row of the selected cell---
        Dim col As Integer = cellLabel.Name.Substring(0, 1)
        Dim row As Integer = cellLabel.Name.ToString().Substring(1, 1)
        '---If erasing a cell---
        If SelectedNumber = 0 Then
            '---if cell is empty then no need to erase---
            If actual(col, row) = 0 Then Return
            '---save the value in the array---
            SetCell(col, row, SelectedNumber, 1)
            DisplayActivity("Number erased at (" &
            col & "," & row & ")", False)
        ElseIf cellLabel.Text = String.Empty Then
            '---else set a value; check if move is valid---
            If Not IsMoveValid(col, row, SelectedNumber) Then
                DisplayActivity("Invalid move at (" & col &
                "," & row & ")", False)
                Return
            End If
            '---save the value in the array---
            SetCell(col, row, SelectedNumber, 1)
            DisplayActivity("Number placed at (" & col & "," & row & ")", False)
            '---saves the move into the stack---
            Moves.Push(cellLabel.Name.ToString() & SelectedNumber)
            '---check if the puzzle is solved---
            If IsPuzzleSolved() Then
                Timer1.Enabled = False
                Beep()
                ToolStripStatusLabel1.Text = "*****Puzzle Solved*****"
            End If
        End If
    End Sub

    Public Function IsMoveValid(
            ByVal col As Integer,
            ByVal row As Integer,
            ByVal value As Integer) As Boolean
        Dim puzzleSolved As Boolean = True
        '---scan through column
        For r As Integer = 1 To 9
            If actual(col, r) = value Then '---duplicate---
                Return False
            End If
        Next
        '---scan through row
        For c As Integer = 1 To 9
            If actual(c, row) = value Then '---duplicate---
                Return False
            End If
        Next
        '---scan through minigrid
        Dim startC, startR As Integer
        startC = col - ((col - 1) Mod 3)
        startR = row - ((row - 1) Mod 3)
        For rr As Integer = 0 To 2
            For cc As Integer = 0 To 2
                If actual(startC + cc, startR + rr) = value Then
                    '---duplicate---
                    Return False
                End If
            Next
        Next
        Return True
    End Function

    Public Function IsPuzzleSolved() As Boolean
        '---check row by row---
        Dim pattern As String
        Dim r, c As Integer
        For r = 1 To 9
            pattern = "123456789"
            For c = 1 To 9
                pattern = pattern.Replace(actual(c, r).ToString(), String.Empty)
            Next
            If pattern.Length > 0 Then
                Return False
            End If
        Next
        '---check col by col---
        For c = 1 To 9
            pattern = "123456789"
            For r = 1 To 9
                pattern = pattern.Replace(actual(c, r).ToString(), String.Empty)
            Next
            If pattern.Length > 0 Then
                Return False
            End If
        Next
        '---check by minigrid---
        For c = 1 To 9 Step 3
            pattern = "123456789"
            For r = 1 To 9 Step 3
                For cc As Integer = 0 To 2
                    For rr As Integer = 0 To 2
                        pattern = pattern.Replace(
                        actual(c + cc, r + rr).ToString(), String.Empty)
                    Next
                Next
            Next
            If pattern.Length > 0 Then
                Return False
            End If
        Next
        Return True
    End Function

    Public Sub SetCell(
            ByVal col As Integer, ByVal row As Integer,
            ByVal value As Integer, ByVal erasable As Short)
        '---Locate the particular Label control---
        Dim lbl() As Control =
        Me.Controls.Find(col.ToString() & row.ToString(), True)
        Dim cellLabel As Label = CType(lbl(0), Label)
        '---save the value in the array---
        actual(col, row) = value

        If value = 0 Then
            For r As Integer = 1 To 9
                For c As Integer = 1 To 9
                    If actual(c, r) = 0 Then possible(c, r) = String.Empty
                Next
            Next
        Else
            possible(col, row) = value.ToString()
        End If

        '---set the appearance for the Label control---
        If value = 0 Then '---erasing the cell---
            cellLabel.Text = String.Empty
            cellLabel.Tag = erasable
            cellLabel.BackColor = DEFAULT_BACKCOLOR
        Else
            If erasable = 0 Then '---means default puzzle values---
                cellLabel.BackColor = FIXED_BACKCOLOR
                cellLabel.ForeColor = FIXED_FORECOLOR
            Else '---means user-set value---
                cellLabel.BackColor = USER_BACKCOLOR
                cellLabel.ForeColor = USER_FORECOLOR
            End If
            cellLabel.Text = value
            cellLabel.Tag = erasable
        End If
    End Sub

    Public Sub DisplayActivity(
            ByVal str As String,
            ByVal soundBeep As Boolean)
        If soundBeep Then Beep()
        txtActivities.Text &= str & Environment.NewLine

    End Sub

    Private Sub UndoToolStripMenuItem_Click(
            ByVal sender As System.Object,
            ByVal e As System.EventArgs) _
            Handles UndoToolStripMenuItem.Click
        '---if no previous moves, then exit---
        If Moves.Count = 0 Then Return
        '---remove from the Moves stack and push into
        ' the RedoMoves stack---
        Dim str As String = Moves.Pop()
        RedoMoves.Push(str)
        '---save the value in the array---
        SetCell(Integer.Parse(str(0)), Integer.Parse(str(1)), 0, 1)
        DisplayActivity("Value removed at (" &
        Integer.Parse(str(0)) & "," &
        Integer.Parse(str(1)) & ")", False)
    End Sub

    Public Sub SetToolTip(
            ByVal col As Integer, ByVal row As Integer,
            ByVal possiblevalues As String)
        '---Locate the particular Label control---
        Dim lbl() As Control = Me.Controls.Find(col.ToString() & row.ToString(), True)
        ToolTip1.SetToolTip(CType(lbl(0), Label), possiblevalues)
    End Sub

    Private Sub RedoToolStripMenuItem_Click(
            ByVal sender As System.Object,
            ByVal e As System.EventArgs) _
            Handles RedoToolStripMenuItem.Click
        '---if RedoMove stack is empty, then exit---
        If RedoMoves.Count = 0 Then Return
        '---remove from the RedoMoves stack and push into the
        ' Moves stack---
        Dim str As String = RedoMoves.Pop()
        Moves.Push(str)
        '---save the value in the array---
        SetCell(Integer.Parse(str(0)), Integer.Parse(str(1)),
        Integer.Parse(str(2)), 1)
        DisplayActivity("Value reinserted at (" &
        Integer.Parse(str(0)) & "," &
        Integer.Parse(str(1)) & ")", False)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHint.Click
        '---show hints one cell at a time
        HintMode = True
        Try
            SolvePuzzle()
        Catch ex As Exception
            MessageBox.Show("Please undo your move", "Invalid Move",
            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSolvePuzzle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSolvePuzzle.Click
        '---solve the puzzle
        HintMode = False
        Try
            SolvePuzzle()
        Catch ex As Exception
            MessageBox.Show("Please undo your move", "Invalid Move",
            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function SolvePuzzle() As Boolean
        Dim changes As Boolean
        Dim ExitLoop As Boolean = False
        Try
            Do
                Do
                    Do
                        Do
                            '---Perform Col/Row and Minigrid Elimination----
                            changes = CheckColumnsAndRows()
                            If (HintMode AndAlso changes) OrElse IsPuzzleSolved() Then
                                ExitLoop = True
                                Exit Do
                            End If
                        Loop Until Not changes
                        If ExitLoop Then Exit Do
                        '---Look for Lone Ranger in Minigrids----
                        changes = LookForLoneRangersinMinigrids()
                        If (HintMode AndAlso changes) OrElse IsPuzzleSolved() Then
                            ExitLoop = True
                            Exit Do
                        End If
                    Loop Until Not changes
                    If ExitLoop Then Exit Do
                    '---Look for Lone Ranger in Rows----
                    changes = LookForLoneRangersinRows()
                    If (HintMode AndAlso changes) OrElse IsPuzzleSolved() Then
                        ExitLoop = True
                        Exit Do
                    End If
                Loop Until Not changes
                If ExitLoop Then Exit Do
                '---Look for Lone Ranger in Columns----
                changes = LookForLoneRangersinColumns()
                If (HintMode AndAlso changes) OrElse IsPuzzleSolved() Then
                    ExitLoop = True
                    Exit Do
                End If
            Loop Until Not changes
        Catch ex As Exception
            Throw New Exception("Invalid Move")
        End Try
        If IsPuzzleSolved() Then
            Timer1.Enabled = False
            Beep()
            ToolStripStatusLabel1.Text = "*****Puzzle Solved*****"
            MsgBox("Puzzle solved")
            Return True
        Else
            Return False
        End If
    End Function

    Public Function LookForLoneRangersinMinigrids() As Boolean
        Dim changes As Boolean = False
        Dim NextMiniGrid As Boolean
        Dim occurrence As Integer
        Dim cPos, rPos As Integer
        '---check for each number from 1 to 9---
        For n As Integer = 1 To 9
            '---check the 9 minigrids---
            For r As Integer = 1 To 9 Step 3
                For c As Integer = 1 To 9 Step 3
                    NextMiniGrid = False
                    '---check within the minigrid---
                    occurrence = 0
                    For rr As Integer = 0 To 2
                        For cc As Integer = 0 To 2
                            If actual(c + cc, r + rr) = 0 AndAlso
                            possible(c + cc, r + rr).Contains(n.ToString()) Then
                                occurrence += 1
                                cPos = c + cc
                                rPos = r + rr
                                If occurrence > 1 Then
                                    NextMiniGrid = True
                                    Exit For
                                End If
                            End If
                        Next
                        If NextMiniGrid Then Exit For
                    Next
                    If (Not NextMiniGrid) AndAlso occurrence = 1 Then
                        '---that means number is confirmed---
                        SetCell(cPos, rPos, n, 1)
                        SetToolTip(cPos, rPos, n.ToString())
                        '---saves the move into the stack
                        Moves.Push(cPos & rPos & n.ToString())
                        DisplayActivity("Look for Lone Rangers in Minigrids", False)
                        DisplayActivity("===========================", False)
                        DisplayActivity("Inserted value " & n.ToString() &
                        " in " & "(" & cPos & "," & rPos & ")", False)
                        Application.DoEvents()
                        changes = True
                        '---if user clicks the Hint button, exit the function---
                        If HintMode Then Return True
                    End If
                Next
            Next
        Next
        Return changes
    End Function

    Public Function LookForLoneRangersinRows() As Boolean
        Dim changes As Boolean = False
        Dim occurrence As Integer
        Dim cPos, rPos As Integer
        '---check by row----
        For r As Integer = 1 To 9
            For n As Integer = 1 To 9
                occurrence = 0
                For c As Integer = 1 To 9
                    If actual(c, r) = 0 AndAlso possible(c, r).Contains(n.ToString()) Then
                        occurrence += 1
                        '---if multiple occurrences, not a lone ranger anymore
                        If occurrence > 1 Then Exit For
                        cPos = c
                        rPos = r
                    End If
                Next
                If occurrence = 1 Then
                    '--number is confirmed---
                    SetCell(cPos, rPos, n, 1)
                    SetToolTip(cPos, rPos, n.ToString())
                    '---saves the move into the stack---
                    Moves.Push(cPos & rPos & n.ToString())
                    DisplayActivity("Look for Lone Rangers in Rows", False)
                    DisplayActivity("=========================", False)
                    DisplayActivity("Inserted value " & n.ToString() & " in " & "(" & cPos & "," & rPos & ")",
                    False)
                    Application.DoEvents()
                    changes = True
                    '---if user clicks the Hint button, exit the function---
                    If HintMode Then Return True
                End If
            Next
        Next
        Return changes
    End Function

    Public Function LookForLoneRangersinColumns() As Boolean
        Dim changes As Boolean = False
        Dim occurrence As Integer
        Dim cPos, rPos As Integer
        '----check by column----
        For c As Integer = 1 To 9
            For n As Integer = 1 To 9
                occurrence = 0
                For r As Integer = 1 To 9
                    If actual(c, r) = 0 AndAlso possible(c, r).Contains(n.ToString()) Then
                        occurrence += 1
                        '---if multiple occurrences, not a lone ranger anymore
                        If occurrence > 1 Then Exit For
                        cPos = c
                        rPos = r
                    End If
                Next
                If occurrence = 1 Then
                    '--number is confirmed---
                    SetCell(cPos, rPos, n, 1)
                    SetToolTip(cPos, rPos, n.ToString())
                    '---saves the move into the stack
                    Moves.Push(cPos & rPos & n.ToString())
                    DisplayActivity("Look for Lone Rangers in Columns", False)
                    DisplayActivity("===========================", False)
                    DisplayActivity("Inserted value " & n.ToString() & " in " & "(" & cPos & "," & rPos & ")", False)
                    Application.DoEvents()
                    changes = True
                    '---if user clicks the Hint button, exit the function---
                    If HintMode Then Return True
                End If
            Next
        Next
        Return changes
    End Function

    Public Function LookForTwinsinMinigrids() As Boolean
        Dim changes As Boolean = False
        '---look for twins in each cell---
        For r As Integer = 1 To 9
            For c As Integer = 1 To 9
                '---if two possible values, check for twins---
                If actual(c, r) = 0 AndAlso possible(c, r).Length = 2 Then
                    '---scan by the minigrid that the current cell is in
                    Dim startC, startR As Integer
                    startC = c - ((c - 1) Mod 3)
                    startR = r - ((r - 1) Mod 3)
                    For rr As Integer = startR To startR + 2
                        For cc As Integer = startC To startC + 2
                            '---for cells other than the pair of twins---
                            If (Not ((cc = c) AndAlso (rr = r))) AndAlso
                            possible(cc, rr) = possible(c, r) Then
                                '---twins found---
                                DisplayActivity("Twins found in minigrid at: (" & c & "," & r & ") and (" & cc & "," & rr & ")", False)
                                '---remove the twins from all the other possible
                                ' values in the minigrid---
                                For rrr As Integer = startR To startR + 2
                                    For ccc As Integer = startC To startC + 2
                                        '---only check for empty cells---
                                        If actual(ccc, rrr) = 0 AndAlso
                                        possible(ccc, rrr) <> possible(c, r) _
                                        Then
                                            '---save a copy of the original
                                            ' possible values (twins)---
                                            Dim original_possible As String =
                                            possible(ccc, rrr)
                                            '---remove first twin number from
                                            ' possible values---
                                            possible(ccc, rrr) =
                                            possible(ccc, rrr).Replace(
                                            possible(c, r)(0), String.Empty)
                                            '---remove second twin number from
                                            ' possible values---
                                            possible(ccc, rrr) =
                                            possible(ccc, rrr).Replace(
                                            possible(c, r)(1), String.Empty)
                                            '---set the ToolTip---
                                            SetToolTip(
                                            ccc, rrr, possible(ccc, rrr))
                                            '---if the possible values are
                                            ' modified, then set the changes
                                            ' variable to True to indicate that
                                            ' the possible values of cells in the
                                            ' minigrid have been modified---
                                            If original_possible <>
                                            possible(ccc, rrr) Then
                                                changes = True
                                            End If
                                            '---if possible value reduces to empty
                                            ' string, then the user has placed a
                                            ' move that results in the puzzle being
                                            ' not solvable---
                                            If possible(ccc, rrr) = String.Empty _
                                            Then
                                                Throw New Exception("Invalid Move")
                                            End If
                                            '---if left with 1 possible value for
                                            ' the current cell, cell is
                                            ' confirmed---
                                            If possible(ccc, rrr).Length = 1 Then
                                                SetCell(ccc, rrr,
                                                CInt(possible(ccc, rrr)), 1)
                                                SetToolTip(
                                                ccc, rrr, possible(ccc, rrr))
                                                '---saves the move into the stack
                                                Moves.Push(
                                                ccc & rrr & possible(ccc, rrr))
                                                DisplayActivity(
                                                "Look For Twins in Minigrids",
                                                False)
                                                DisplayActivity(
                                                "===========================",
                                                False)
                                                DisplayActivity(
                                                "Inserted value " &
                                                actual(ccc, rrr) &
                                                " in " & "(" & ccc & "," &
                                                rrr & ")", False)
                                                Application.DoEvents()
                                                '---if user clicks the Hint button,
                                                'exit the function---
                                                If HintMode Then Return True
                                            End If
                                        End If
                                    Next
                                Next
                            End If
                        Next
                    Next
                End If
            Next
        Next
        Return changes
    End Function

    Public Function LookForTwinsinRows() As Boolean
        Dim changes As Boolean = False
        '---for each row, check each column in the row---
        For r As Integer = 1 To 9
            For c As Integer = 1 To 9
                '---if two possible values, check for twins---
                If actual(c, r) = 0 AndAlso possible(c, r).Length = 2 Then
                    '--scan columns in this row---
                    For cc As Integer = c + 1 To 9
                        If (possible(cc, r) = possible(c, r)) Then
                            '--twins found---
                            DisplayActivity("Twins found in row at: (" &
                            c & "," & r & ") and (" & cc & "," & r & ")",
                            False)
                            '---remove the twins from all the other possible
                            ' values in the column---
                            For ccc As Integer = 1 To 9
                                If (actual(ccc, r) = 0) AndAlso (ccc <> c) _
                                AndAlso (ccc <> cc) Then
                                    '---save a copy of the original possible
                                    ' values (twins)---
                                    Dim original_possible As String =
                                    possible(ccc, r)
                                    '---remove first twin number from possible
                                    ' values---
                                    possible(ccc, r) = possible(ccc, r).Replace(
                                    possible(c, r)(0), String.Empty)
                                    '---remove second twin number from possible
                                    ' values---
                                    possible(ccc, r) = possible(ccc, r).Replace(
                                    possible(c, r)(1), String.Empty)
                                    '---set the ToolTip---
                                    SetToolTip(ccc, r, possible(ccc, r))
                                    '---if the possible values are modified, then
                                    ' set the changes variable to True to indicate
                                    ' that the possible values of cells in the
                                    ' minigrid have been modified---
                                    If original_possible <> possible(ccc, r) Then
                                        changes = True
                                    End If
                                    '---if possible value reduces to empty string,
                                    ' then the user has placed a move that results
                                    ' in the puzzle being not solvable---
                                    If possible(ccc, r) = String.Empty Then
                                        Throw New Exception("Invalid Move")
                                    End If
                                    '---if left with 1 possible value for the
                                    ' current cell, cell is confirmed---
                                    If possible(ccc, r).Length = 1 Then
                                        SetCell(ccc, r, CInt(possible(ccc, r)), 1)
                                        SetToolTip(ccc, r, possible(ccc, r))
                                        '---saves the move into the stack
                                        Moves.Push(ccc & r & possible(ccc, r))
                                        DisplayActivity("Look For Twins in Rows)",
                                        False)
                                        DisplayActivity("=======================",
                                        False)
                                        DisplayActivity("Inserted value " &
                                        actual(ccc, r) & " in " & "(" &
                                        ccc & "," & r & ")", False)
                                        Application.DoEvents()
                                        '---if user clicks the Hint button, exit
                                        ' the function---
                                        If HintMode Then Return True
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        Next
        Return changes
    End Function

    Public Function LookForTwinsinColumns() As Boolean
        Dim changes As Boolean = False
        '---for each column, check each row in the column---
        For c As Integer = 1 To 9
            For r As Integer = 1 To 9
                '---if two possible values, check for twins---
                If actual(c, r) = 0 AndAlso possible(c, r).Length = 2 Then
                    '--scan rows in this column---
                    For rr As Integer = r + 1 To 9
                        If (possible(c, rr) = possible(c, r)) Then
                            '--twins found---
                            DisplayActivity("Twins found in column at: (" &
                            c & "," & r & ") and (" & c & "," & rr & ")", False)
                            '---remove the twins from all the other possible
                            ' values in the row---
                            For rrr As Integer = 1 To 9
                                If (actual(c, rrr) = 0) AndAlso (rrr <> r) _
                                AndAlso (rrr <> rr) Then
                                    '---save a copy of the original possible
                                    ' values (twins)---
                                    Dim original_possible As String =
                                    possible(c, rrr)
                                    '---remove first twin number from possible
                                    ' values---
                                    possible(c, rrr) = possible(c, rrr).Replace(
                                    possible(c, r)(0), String.Empty)
                                    '---remove second twin number from possible
                                    ' values---
                                    possible(c, rrr) = possible(c, rrr).Replace(
                                    possible(c, r)(1), String.Empty)
                                    '---set the ToolTip---
                                    SetToolTip(c, rrr, possible(c, rrr))
                                    '---if the possible values are modified, then
                                    ' set the changes variable to True to indicate
                                    ' that the possible values of cells in the
                                    ' minigrid have been modified---
                                    If original_possible <> possible(c, rrr) Then
                                        changes = True
                                    End If
                                    '---if possible value reduces to empty string,
                                    ' then the user has placed a move that results
                                    ' in the puzzle being not solvable---
                                    If possible(c, rrr) = String.Empty Then
                                        Throw New Exception("Invalid Move")
                                    End If
                                    '---if left with 1 possible value for the
                                    ' current cell, cell is confirmed---
                                    If possible(c, rrr).Length = 1 Then
                                        SetCell(c, rrr, CInt(possible(c, rrr)), 1)
                                        SetToolTip(c, rrr, possible(c, rrr))
                                        '---saves the move into the stack
                                        Moves.Push(c & rrr & possible(c, rrr))
                                        DisplayActivity(
                                        "Looking for twins (by column)", False)
                                        DisplayActivity(
                                        "============================", False)
                                        DisplayActivity("Inserted value " & actual(c, rrr) & " in " & "(" & c & "," & rrr & ")", False)
                                        Application.DoEvents()
                                        '---if user clicks the Hint button,
                                        'exit the function---
                                        If HintMode Then Return True
                                    End If
                                End If
                            Next
                        End If
                    Next
                End If
            Next
        Next
        Return changes
    End Function

End Class
