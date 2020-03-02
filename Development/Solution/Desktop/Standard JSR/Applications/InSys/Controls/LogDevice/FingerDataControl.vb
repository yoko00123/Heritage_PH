Public Class FingerDataControl



    Dim FDefColor() As Color = { _
  Color.FromArgb(236, 222, 201) _
, Color.FromArgb(236, 223, 200) _
, Color.FromArgb(237, 222, 200) _
, Color.FromArgb(236, 223, 201) _
, Color.FromArgb(237, 222, 201) _
, Color.FromArgb(237, 223, 200) _
, Color.FromArgb(237, 223, 201) _
, Color.FromArgb(237, 223, 202) _
, Color.FromArgb(237, 224, 201) _
, Color.FromArgb(238, 223, 201) _
}

    Dim FHiColor() As Color = { _
  Color.FromArgb(90, 200, 1) _
, Color.FromArgb(90, 201, 0) _
, Color.FromArgb(91, 200, 0) _
, Color.FromArgb(90, 201, 1) _
, Color.FromArgb(91, 200, 1) _
, Color.FromArgb(91, 201, 0) _
, Color.FromArgb(91, 201, 1) _
, Color.FromArgb(91, 201, 2) _
, Color.FromArgb(92, 202, 1) _
, Color.FromArgb(92, 201, 1) _
}

    Dim FRegColor() As Color = { _
  Color.FromArgb(0, 91, 200) _
, Color.FromArgb(0, 90, 201) _
, Color.FromArgb(1, 90, 200) _
, Color.FromArgb(0, 91, 201) _
, Color.FromArgb(1, 91, 200) _
, Color.FromArgb(1, 90, 201) _
, Color.FromArgb(1, 91, 201) _
, Color.FromArgb(1, 92, 201) _
, Color.FromArgb(2, 91, 202) _
, Color.FromArgb(2, 91, 201) _
}

    Dim FRegHiColor() As Color = { _
  Color.FromArgb(40, 151, 100) _
, Color.FromArgb(40, 150, 101) _
, Color.FromArgb(41, 150, 100) _
, Color.FromArgb(40, 151, 101) _
, Color.FromArgb(41, 151, 100) _
, Color.FromArgb(41, 150, 101) _
, Color.FromArgb(41, 151, 101) _
, Color.FromArgb(41, 152, 101) _
, Color.FromArgb(42, 151, 102) _
, Color.FromArgb(42, 151, 101) _
}



    Public Fingers(9) As FingerClass

    Private Sub Init()
        Me.BackgroundImage = CreateNonIndexedImage(Me.BackgroundImage)
        For i As Integer = Fingers.GetLowerBound(0) To Fingers.GetUpperBound(0)
            Fingers(i) = New FingerClass
        Next
        Fingers(0).FloodPoint = New Point(18, 32)
        Fingers(1).FloodPoint = New Point(43, 29)
        Fingers(2).FloodPoint = New Point(71, 23)
        Fingers(3).FloodPoint = New Point(94, 34)
        Fingers(4).FloodPoint = New Point(103, 96)
        Fingers(5).FloodPoint = New Point(161, 98)
        Fingers(6).FloodPoint = New Point(182, 52)
        Fingers(7).FloodPoint = New Point(197, 44)
        Fingers(8).FloodPoint = New Point(222, 26)
        Fingers(9).FloodPoint = New Point(249, 34)

    End Sub

    Private mSelectedIndex As Integer = -1

    Public Property SelectedIndex() As Integer
        Get
            Return mSelectedIndex
        End Get
        Set(ByVal value As Integer)
            mSelectedIndex = value
            For i As Integer = 0 To 9
                Fingers(i).Selected = (i = mSelectedIndex)
            Next
        End Set
    End Property



    Public Sub Render()
        Dim vNewColor As Color
        Dim f As FingerClass
        Dim img As Bitmap
        img = CType(Me.BackgroundImage, Bitmap)
        For i As Integer = Fingers.GetLowerBound(0) To Fingers.GetUpperBound(0)
            f = Fingers(i)
            If f.Selected And f.Registered Then
                vNewColor = FRegHiColor(i)
            ElseIf f.Selected Then
                vNewColor = FHiColor(i)
            ElseIf f.Registered Then
                vNewColor = FRegColor(i)
            Else
                vNewColor = FDefColor(i)
            End If
            SafeFloodFill(img, f.FloodPoint.X, f.FloodPoint.Y, vNewColor)
        Next
        Me.Refresh()
    End Sub

    'Private Function AverageColor(ByVal c1 As Color, ByVal c2 As Color) As Color
    '    Dim r1, r2, g1, g2, b1, b2 As Integer
    '    r1 = c1.R
    '    r2 = c2.R
    '    g1 = c1.G
    '    g2 = c2.G
    '    b1 = c1.B
    '    b2 = c2.B

    '    'Return Color.FromArgb((c1.R + c2.R) \ 2, (c1.G + c2.G) \ 2, (c1.B + c2.B) \ 2)
    '    'Return Color.FromArgb((r1 + r2) \ 2, (g1 + g2) \ 2, (b1 + b2) \ 2)
    '    Return Color.Gold
    'End Function

    Function CreateNonIndexedImage(ByVal src As Image) As Bitmap
        Dim newBmp As New Bitmap(src.Width, src.Height, Imaging.PixelFormat.Format32bppArgb)

        Using gfx As Graphics = Graphics.FromImage(newBmp)
            gfx.DrawImage(src, 0, 0)
        End Using

        Return newBmp
    End Function

#Region "SafeFloodFill"
    Public Sub SafeFloodFill(ByVal bm As Bitmap, ByVal x As Integer, ByVal y As Integer, ByVal new_color As Color)
        Dim old_color As Color = bm.GetPixel(x, y)
        If old_color.ToArgb <> new_color.ToArgb Then
            Dim pts As New Stack()
            pts.Push(New Point(x, y))
            bm.SetPixel(x, y, new_color)
            Do While pts.Count > 0
                Dim pt As Point = DirectCast(pts.Pop(), Point)
                If pt.X > 0 Then SafeCheckPoint(bm, pts, pt.X - 1, pt.Y, old_color, new_color)
                If pt.Y > 0 Then SafeCheckPoint(bm, pts, pt.X, pt.Y - 1, old_color, new_color)
                If pt.X < bm.Width - 1 Then SafeCheckPoint(bm, pts, pt.X + 1, pt.Y, old_color, new_color)
                If pt.Y < bm.Height - 1 Then SafeCheckPoint(bm, pts, pt.X, pt.Y + 1, old_color, new_color)
            Loop
        End If
    End Sub

    Private Sub SafeCheckPoint(ByVal bm As Bitmap, ByVal pts As Stack, ByVal x As Integer, ByVal y As Integer, ByVal old_color As Color, ByVal new_color As Color)
        Dim clr As Color = bm.GetPixel(x, y)
        If clr.Equals(old_color) Then
            pts.Push(New Point(x, y))
            bm.SetPixel(x, y, new_color)
        End If
    End Sub
#End Region ' SafeFloodFill

    Private Sub FingerDataControl_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        Dim img As Bitmap = CType(Me.BackgroundImage, Bitmap)
        Dim c As Color = img.GetPixel(e.X, e.Y)
        Me.SelectedIndex = -1
        For i As Integer = Fingers.GetLowerBound(0) To Fingers.GetUpperBound(0)
            If c = FDefColor(i) Or c = FRegColor(i) Or c = FHiColor(i) Or c = FRegHiColor(i) Then
                Me.SelectedIndex = i
                Exit For
            End If
        Next
        Render()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Init()
    End Sub
End Class
