Public Class SudokuPuzzle
    Private actual(9, 9) As Integer
    Private possible(9, 9) As String
    Private BruteForceStop As Boolean = False
    Private ActualStack As New Stack(Of Integer(,))()
    Private PossibleStack As New Stack(Of String(,))()
    '---store the total score accumulated---
    Private totalscore As Integer

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



End Class
