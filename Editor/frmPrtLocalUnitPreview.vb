﻿Public Class frmPrtLocalUnitPreview
    '2017/6/16 もはや同期はとれていないため、本画面を使用するのは禁止
#Region "定数定義"

    ''ライン描画開始位置
    Private Const mCstCodeFooterStartPosition As Single = 1070

    ''PictureBoxのサイズ指定
    Private Const mCstCodeImgHeight As Integer = 1150

    '印刷モード
    Private Const mCstCodePrintModePrintAll As Integer = 0
    Private Const mCstCodePrintModePrintPages As Integer = 1
    Private Const mCstCodePrintModePreview As Integer = 2

#End Region

#Region "変数定義"

    Private mudtFuInfo() As gTypFuInfo = Nothing
    Private mudtSetFuNew As gTypSetFu
    Private mudtSetChDispNew As gTypSetChDisp

    Private mintPageCount As Integer = 1
    Private mblnPrintSingle As Boolean = False
    Private mintPageTo As Integer = 0
    Private mintPageFrom As Integer = 0
    Private mintPagePrint As Integer                   ''ページ印刷     　 （True:ページ印刷する  False:ページ印刷しない）2013.10.18

    ''端子台毎に割りつけられているチャンネルの数を格納する
    Dim mintRecCntSlot(gCstCountFuNo - 1, gCstCountFuPort - 1) As Integer

    ''印刷モード
    Private mintPrintMode As Integer

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 印刷モード（0:Print(All)、1:Print(Pages)、2:Preview）
    '           : ARG2 - (I ) 印刷開始ページ 
    '           : ARG3 - (I ) 印刷終了ページ
    '           : ARG4 - (I ) Part選択（True:Machinery、False:Cargo）
    ' 備考      : 
    ' 履歴      : パート選択の引数削除　ver.1.4.0 2011.08.02
    '--------------------------------------------------------------------
    Friend Function gShow(ByRef hudtFuInfo() As gTypFuInfo, _
                          ByVal hintPrintMode As Integer, _
                          ByVal hintPageFrom As Integer, _
                          ByVal hintPageTo As Integer, _
                          ByVal hintPagePrint As Integer) As Integer

        Try

            ''引数保存
            mudtFuInfo = hudtFuInfo
            mintPrintMode = hintPrintMode
            mintPageFrom = hintPageFrom
            mintPageTo = hintPageTo
            mintPagePrint = hintPagePrint

            ''本画面表示
            Me.ShowDialog()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームロード
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmPrtLocalUnitPreview_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim i As Integer
            Dim intTemp As Integer

            ''PictureBoxのサイズ設定
            imgPreview.Height = mCstCodeImgHeight

            ''Page総数の取得 
            Dim mintRecCntSlot(gCstCountFuNo - 1, gCstCountFuPort - 1) As Integer

            ''端子台毎に割りつけられているチャンネルの数をGETする
            Call GetRecCountSlot()

            ''ページ数の取得
            For i = 0 To UBound(gudt.SetFu.udtFu)
                If gudt.SetFu.udtFu(i).shtUse = 1 Then
                    intTemp = i
                End If
            Next
            ''For i = 0 To gCstCountFuNo - 1
            ''    For j = 0 To gCstCountFuPort - 1
            ''        If mintRecCntSlot(i, j) > 0 Then
            ''            intTemp = i
            ''        End If
            ''    Next
            ''Next

            ''Page総数
            If intTemp = 0 Then
                '何も設定されていない場合は、ページ数「1」を表示
                intTemp = 1
            Else
                intTemp = CInt((intTemp) / 7 + 0.5)
            End If

            ''Page総数set
            lblMaxPage.Text = (intTemp).ToString

            '-------------------
            ''ページ指定印刷
            '-------------------
            Select Case mintPrintMode
                Case mCstCodePrintModePrintAll
                    ''全ページ印刷
                    Call cmdAllPrint_Click(cmdAllPrint, New EventArgs)
                    Me.Close()
                Case mCstCodePrintModePrintPages
                    ''部分印刷
                    Call cmdPagesPrint_Click(cmdAllPrint, New EventArgs)
                    Me.Close()
            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： プレビュー画面表示
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub imgPreview_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles imgPreview.Paint

        Try

            'ページフレーム作成
            ''Call gPrtDrawOutFrameLocalUnit(e.Graphics, "", " FIELD UNIT", mintPageCount)
            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                If g_bytFUSet = 0 Then      '' Ver1.8.6.1 2015.12.03 出荷済みｵｰﾀﾞｰ用
                    Call gPrtDrawOutFrameLocalUnit(e.Graphics, "", " フィールドユニット", "FU001", mintPageCount, mintPagePrint)  '' DrawingNo変更　2013.10.1
                Else        '' Ver1.8.6.1 2015.12.03  2桁表示
                    Call gPrtDrawOutFrameLocalUnit(e.Graphics, "", " フィールドユニット", "FU01", mintPageCount, mintPagePrint)  '' DrawingNo変更
                End If

            Else
                If g_bytFUSet = 0 Then      '' Ver1.8.6.1 2015.12.03 出荷済みｵｰﾀﾞｰ用
                    Call gPrtDrawOutFrameLocalUnit(e.Graphics, "", " FIELD UNIT", "FU001", mintPageCount, mintPagePrint)  '' DrawingNo変更　2013.10.1
                Else        '' Ver1.8.6.1 2015.12.03  2桁表示
                    Call gPrtDrawOutFrameLocalUnit(e.Graphics, "", " FIELD UNIT", "FU01", mintPageCount, mintPagePrint)  '' DrawingNo変更
                End If
            End If

            'グラフ作成
            Call DrawLines(e.Graphics)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： CLOSE ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click

        Try

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub frmPrtLocalUnitPreview_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： [>>] クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdNextPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNextPage.Click

        Try

            If CInt(txtPage.Text) < CInt(lblMaxPage.Text) Then
                txtPage.Text = Str(CInt(txtPage.Text) + 1)
                mintPageCount = CInt(txtPage.Text)
            End If

            Call imgPreview.Refresh()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： [<<] クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdBeforePage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBeforePage.Click

        Try

            If CInt(txtPage.Text) > 1 Then
                txtPage.Text = Str(CInt(txtPage.Text) - 1)
                mintPageCount = CInt(txtPage.Text)
            End If

            Call imgPreview.Refresh()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ALL PRINT クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdAllPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAllPrint.Click

        Try

            ''PrintDialogオブジェクトの作成
            Dim PrintDialog1 As New PrintDialog()

            PrintDialog1.AllowPrintToFile = False   ''ファイルへ出力 チェックボックスを無効にする 
            PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
            PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

            ''印刷ダイアログを表示
            If PrintDialog1.ShowDialog() = DialogResult.OK Then

                mblnPrintSingle = False

                'PrintDocumentオブジェクト作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                'PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage

                pd.OriginAtMargins = True
                pd.DefaultPageSettings.Landscape = False
                pd.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                pd.PrinterSettings.Copies = PrintDialog1.PrinterSettings.Copies

                ''余白設定
                pd.DefaultPageSettings.Margins.Top = 20
                pd.DefaultPageSettings.Margins.Left = 20 '10    2013.10.19
                pd.DefaultPageSettings.Margins.Right = 20
                pd.DefaultPageSettings.Margins.Bottom = 20

                '印刷を開始する
                pd.Print()

                'PrintDocumentオブジェクト破棄
                pd.Dispose()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： PAGES PRINT クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPagesPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPagesPrint.Click

        Try

            ''PrintDialogオブジェクトの作成
            Dim PrintDialog1 As New PrintDialog()

            PrintDialog1.AllowPrintToFile = False   ''ファイルへ出力 チェックボックスを無効にする 
            PrintDialog1.PrinterSettings = New System.Drawing.Printing.PrinterSettings()
            PrintDialog1.UseEXDialog = True         '' 64bit版対応 2014.09.18

            ''印刷ダイアログを表示
            If PrintDialog1.ShowDialog() = DialogResult.OK Then

                mblnPrintSingle = True

                ''PrintDocumentオブジェクトの作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                ''PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage

                pd.OriginAtMargins = True
                pd.DefaultPageSettings.Landscape = False
                pd.PrinterSettings.PrinterName = PrintDialog1.PrinterSettings.PrinterName
                pd.PrinterSettings.Copies = PrintDialog1.PrinterSettings.Copies

                ''余白設定
                pd.DefaultPageSettings.Margins.Top = 20
                pd.DefaultPageSettings.Margins.Left = 20 '10    2013.10.19
                pd.DefaultPageSettings.Margins.Right = 20
                pd.DefaultPageSettings.Margins.Bottom = 20


                '印刷方法の切替え
                If mintPageFrom + mintPageTo <> 0 Then '========================================================================

                    '------------------
                    ''ページ指定印刷 
                    '------------------
                    For i As Integer = mintPageFrom To mintPageTo

                        txtPage.Text = Str(i)
                        mintPageCount = CInt(txtPage.Text)

                        Call imgPreview.Refresh()

                        ''印刷を開始する
                        pd.Print()

                    Next

                Else

                    '------------------
                    ''全画面印刷
                    '------------------
                    mintPageCount = CInt(txtPage.Text)

                    ''印刷を開始する
                    pd.Print()

                End If '========================================================================================================


                'PrintDocumentオブジェクト破棄
                pd.Dispose()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ページ印刷（ページ指定印刷と全印刷）
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 1ページ分印刷する
    '--------------------------------------------------------------------
    Private Sub pd_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        Try

            'Call gPrtDrawOutFrameLocalUnit(e.Graphics, "FIELD UNIT", " FIELD UNIT", mintPageCount)
            'Call gPrtDrawOutFrameLocalUnit(e.Graphics, "", " FIELD UNIT", mintPageCount)
            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                If g_bytFUSet = 0 Then      '' Ver1.8.6.1 2015.12.03 出荷済みｵｰﾀﾞｰ用
                    Call gPrtDrawOutFrameLocalUnit(e.Graphics, "", " フィールドユニット", "FU001", mintPageCount, mintPagePrint)  '' DrawingNo変更　2013.10.18
                Else        '' Ver1.8.6.1 2015.12.03  2桁表示
                    Call gPrtDrawOutFrameLocalUnit(e.Graphics, "", " フィールドユニット", "FU01", mintPageCount, mintPagePrint)  '' DrawingNo変更
                End If

            Else
                If g_bytFUSet = 0 Then      '' Ver1.8.6.1 2015.12.03 出荷済みｵｰﾀﾞｰ用
                    Call gPrtDrawOutFrameLocalUnit(e.Graphics, "", " FIELD UNIT", "FU001", mintPageCount, mintPagePrint)  '' DrawingNo変更　2013.10.18
                Else        '' Ver1.8.6.1 2015.12.03  2桁表示
                    Call gPrtDrawOutFrameLocalUnit(e.Graphics, "", " FIELD UNIT", "FU01", mintPageCount, mintPagePrint)  '' DrawingNo変更
                End If

            End If

            Call DrawLines(e.Graphics)

            If mblnPrintSingle = False Then

                If mintPageCount >= CInt(lblMaxPage.Text) Then
                    mintPageCount = 1
                    e.HasMorePages = False

                Else
                    mintPageCount += 1
                    e.HasMorePages = True

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#End Region

#Region "内部関数"

    '**********************************************************
    '* 描画部分　ライン＋文字
    '**********************************************************
    Private Sub DrawLines(ByVal e As System.Drawing.Graphics)

        Try

            Dim p1 As New Pen(Color.Black, 1)
            Dim p2 As New Pen(Color.Black, 2)
            Dim p1d As New Pen(Color.Black, 1)
            Dim i As Integer, j As Integer
            Dim intTemp As Integer, intTempH As Integer, intTemp2 As Integer
            Dim frmLeft As Integer, frmUp As Integer, frmWidth As Integer, frmHeight As Integer
            Dim wNO As Integer, wCPU As Integer, wREMA As Integer
            Dim hGrp As Integer, hBaseHeader As Integer, hBaseGrp As Integer
            Dim strTemp As String = ""
            Dim strTemp2 As String = ""
            Dim strRL As String = ""
            Dim strRYtype As String = ""
            Dim intRYNo As Integer = 0
            Dim intPage As Integer, intCnt As Integer = 0
            Dim funo As Integer = 0

            '2015/4/9 T.Ueki 斜線書込み対応
            Dim PrintFUNo As String
            Dim PrintSlotType As String
            Dim PrintSlotTypeLen As Integer
            Dim PrintPLCSW As Integer

            intPage = mintPageCount

            ''フレームサイズ取得
            frmLeft = gCstFrameLocalUnitLeft
            frmUp = gCstFrameLocalUnitUp
            frmWidth = gCstFrameLocalUnitWidth
            frmHeight = gCstFrameLocalUnitHight

            wNO = 50
            wCPU = 80
            wREMA = 80

            hGrp = 110
            hBaseHeader = 30
            hBaseGrp = hGrp / 5

            ''端子台のページNoの初期値を獲得する************
            'intCnt = mGetPageIndex(mintPageCount)
            'intCnt += 1     ''端子台の帳票の開始ページは2ページ目からなので＋1

            '' 印刷フォーム調整　ver.1.4.0 2011.08.10
            '**HEADER LINE***********************************
            p1d.DashStyle = Drawing.Drawing2D.DashStyle.Dash

            e.DrawLine(p2, frmLeft, frmUp + hBaseHeader, frmLeft + frmWidth, frmUp + hBaseHeader)     'H2

            e.DrawLine(p1, frmLeft + wNO, frmUp + hBaseHeader, frmLeft + wNO, frmUp + hBaseHeader * 4 + hGrp * 7)     'V left2
            e.DrawLine(p1, frmLeft + wNO * 2, frmUp + hBaseHeader, frmLeft + wNO * 2, frmUp + hBaseHeader * 4 + hGrp * 7)   'V left3

            intTemp = ((frmLeft + frmWidth - (wCPU + wREMA)) - (frmLeft + wNO * 2)) / 8

            For j = 1 To 8
                e.DrawLine(p1, (frmLeft + wNO * 2) + intTemp * j, frmUp + hBaseHeader * 2, (frmLeft + wNO * 2) + intTemp * j, frmUp + hBaseHeader * 4)

                e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j - intTemp / 2), frmUp + hBaseHeader * 3, CInt((frmLeft + wNO * 2) + intTemp * j - intTemp / 2), frmUp + hBaseHeader * 4)
            Next

            e.DrawLine(p1, frmLeft + wNO * 2, frmUp + hBaseHeader * 2, frmLeft + frmWidth - wCPU - wREMA, frmUp + hBaseHeader * 2)  'H3
            e.DrawLine(p1, frmLeft, frmUp + hBaseHeader * 3, frmLeft + frmWidth - wCPU - wREMA, frmUp + hBaseHeader * 3)  'H4

            e.DrawLine(p1, frmLeft + wNO, frmUp + hBaseHeader, frmLeft + wNO * 2, frmUp + hBaseHeader * 3)  'H4
            e.DrawLine(p1, frmLeft + wNO, frmUp + hBaseHeader * 3, frmLeft + wNO * 2, frmUp + hBaseHeader * 4)  'H4

            '*********************************************


            '**BODY LINE**********************************
            For i = 0 To 6

                'Grp段頭の高さ
                intTempH = frmUp + hBaseHeader * 4 + hGrp * i

                e.DrawLine(p2, frmLeft, intTempH, frmLeft + frmWidth, intTempH)  '段の区切り線

                e.DrawLine(p1, frmLeft, intTempH + hBaseGrp, (frmLeft + wNO * 2), intTempH + hBaseGrp)  'Grp横線1-1
                e.DrawLine(p1d, (frmLeft + wNO * 2), intTempH + hBaseGrp, frmLeft + frmWidth - wREMA, intTempH + hBaseGrp)  'Grp横線1-2

                e.DrawLine(p1, frmLeft, intTempH + hBaseGrp * 2, (frmLeft + wNO * 2), intTempH + hBaseGrp * 2)  'Grp横線2-1
                e.DrawLine(p1d, (frmLeft + wNO * 2), intTempH + hBaseGrp * 2, frmLeft + frmWidth - (wREMA + wCPU), intTempH + hBaseGrp * 2)  'Grp横線2-2
                e.DrawLine(p1, frmLeft + frmWidth - (wREMA + wCPU), intTempH + hBaseGrp * 2, frmLeft + frmWidth - wREMA, intTempH + hBaseGrp * 2)  'Grp横線2-3

                e.DrawLine(p1, frmLeft + wNO, intTempH + hBaseGrp * 3, frmLeft + wNO * 2, intTempH + hBaseGrp * 3)  'Grp横線3-1

                e.DrawLine(p1, frmLeft + frmWidth - (wREMA + wCPU), intTempH + hBaseGrp * 3, frmLeft + frmWidth - wREMA, intTempH + hBaseGrp * 3)  'Grp横線3-2

                'e.DrawLine(p1, frmLeft + wNO, intTempH + hBaseGrp * 4, frmLeft + frmWidth - wREMA, intTempH + hBaseGrp * 4)  'Grp横線4
                e.DrawLine(p1, frmLeft + wNO, intTempH + hBaseGrp * 4, frmLeft + wNO * 2, intTempH + hBaseGrp * 4)  'Grp横線4-1
                e.DrawLine(p1d, (frmLeft + wNO * 2), intTempH + hBaseGrp * 4, frmLeft + frmWidth - (wREMA + wCPU), intTempH + hBaseGrp * 4)  'Grp横線4-2
                e.DrawLine(p1, frmLeft + frmWidth - (wREMA + wCPU), intTempH + hBaseGrp * 4, frmLeft + frmWidth - wREMA, intTempH + hBaseGrp * 4)  'Grp横線4-3

                'Grp内縦線描画
                For j = 1 To 8
                    e.DrawLine(p1, (frmLeft + wNO * 2) + intTemp * j, intTempH, (frmLeft + wNO * 2) + intTemp * j, intTempH + hGrp)
                    e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j - intTemp / 2), intTempH + hBaseGrp, CInt((frmLeft + wNO * 2) + intTemp * j - intTemp / 2), intTempH + hGrp)
                Next

                For j = 1 To 5
                    e.DrawLine(p1, frmLeft + frmWidth - (wREMA + wCPU) + 7 + 12 * j, intTempH + hBaseGrp * 2, frmLeft + frmWidth - (wREMA + wCPU) + 7 + 12 * j, intTempH + hBaseGrp * 4)
                Next

            Next

            e.DrawLine(p2, frmLeft, frmUp + hBaseHeader * 4 + hGrp * 7, frmLeft + frmWidth, frmUp + hBaseHeader * 4 + hGrp * 7)  '段の区切り線

            e.DrawLine(p1, frmLeft, frmUp + frmHeight, frmLeft + frmWidth, frmUp + frmHeight)

            e.DrawLine(p1, (frmLeft + wNO * 2) + intTemp * 8, frmUp + hBaseHeader, (frmLeft + wNO * 2) + intTemp * 8, mCstCodeFooterStartPosition + 3)  'V
            e.DrawLine(p1, frmLeft + frmWidth - wREMA, frmUp + hBaseHeader, frmLeft + frmWidth - wREMA, frmUp + hBaseHeader * 4 + hGrp * 7)         'V r2
            '************************************************************************************


            '**TEXT PRINT************************************************************************
            Dim fnt As New Font("Courier New", 14)
            '' FU → FIELD UNITに変更　ver.1.4.0 2011.08.02
            e.DrawString("FIELD UNIT MODULE & TERMINAL ARRANGEMENT", fnt, Brushes.Black, frmLeft + frmWidth / 100 * 20, frmUp + 5)

            Dim fnt1 As New Font("Courier New", 10)
            Dim fnt1j As New Font("ＭＳ 明朝", 10)      '' 2014.05.19

            e.DrawString("MODULE TYPE", fnt1, Brushes.Black, frmLeft + wNO * 2 + 150, frmUp + hBaseHeader + 1)
            e.DrawString("TERMINAL TYPE (TB*A/TB*B)", fnt1, Brushes.Black, frmLeft + wNO * 2 + 120, frmUp + hBaseHeader + 12)

            e.DrawString("NO", fnt1, Brushes.Black, frmLeft + 10, frmUp + hBaseHeader + 10)
            e.DrawString("TYPE", fnt1, Brushes.Black, frmLeft + 5, frmUp + hBaseHeader * 3 + 10)

            Dim fnt2 As New Font("Courier New", 8)
            Dim fnt3 As New Font("Courier New", 7)

            For i = 0 To 7
                e.DrawString("TB" & Str(i + 1), fnt1, Brushes.Black, frmLeft + wNO * 2 + intTemp * i + 6, frmUp + hBaseHeader * 2 + 6)
                e.DrawString("A", fnt1, Brushes.Black, frmLeft + wNO * 2 + intTemp * i + 7, frmUp + hBaseHeader * 3 + 6)
                e.DrawString("B", fnt1, Brushes.Black, frmLeft + wNO * 2 + intTemp * i + CInt(intTemp / 2) + 7, frmUp + hBaseHeader * 3 + 6)
            Next

            '' CPU追加　ver.1.4.0 2011.08.02
            e.DrawString("CPU", fnt1, Brushes.Black, frmLeft + wNO * 2 + 466, frmUp + hBaseHeader * 2 + 6)

            e.DrawString("Remarks", fnt1, Brushes.Black, frmLeft + wNO * 2 + 528, frmUp + hBaseHeader * 2 + 6)

            'SMS-U650-A-
            For i = 0 To 6  '行ループ
                '' LCU    → FCUに変更　ver.1.4.0 2011.08.02
                '' LCU-TM → -TM
                e.DrawString("FCU-", fnt2, Brushes.Black, frmLeft + wNO + 7, frmUp + hBaseHeader * 4 + hGrp * i + 6)
                e.DrawString("-TM", fnt2, Brushes.Black, frmLeft + wNO + 4, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6)
                e.DrawString("CABLE", fnt2, Brushes.Black, frmLeft + wNO + 5, frmUp + hBaseHeader * 4 + hBaseGrp * 2 + hGrp * i + 6)
                e.DrawString("TERM", fnt2, Brushes.Black, frmLeft + wNO + 7, frmUp + hBaseHeader * 4 + hBaseGrp * 3 + hGrp * i + 6)

                funo = i + ((mintPageCount - 1) * 7)
                If gudt.SetFu.udtFu(funo).shtUse = 1 Then
                    '**TYPE ALPH ******************
                    Select Case funo
                        Case 0 : strTemp = "FCU"
                        Case 1 : strTemp = "FU1"
                        Case 2 : strTemp = "FU2"
                        Case 3 : strTemp = "FU3"
                        Case 4 : strTemp = "FU4"
                        Case 5 : strTemp = "FU5"
                        Case 6 : strTemp = "FU6"
                        Case 7 : strTemp = "FU7"
                        Case 8 : strTemp = "FU8"
                        Case 9 : strTemp = "FU9"
                        Case 10 : strTemp = "FU10"
                        Case 11 : strTemp = "FU11"
                        Case 12 : strTemp = "FU12"
                        Case 13 : strTemp = "FU13"
                        Case 14 : strTemp = "FU14"
                        Case 15 : strTemp = "FU15"
                        Case 16 : strTemp = "FU16"
                        Case 17 : strTemp = "FU17"
                        Case 18 : strTemp = "FU18"
                        Case 19 : strTemp = "FU19"
                        Case 20 : strTemp = "FU20"
                    End Select

                    If funo <= 9 Then
                        e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + 10, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                    Else
                        e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + 5, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                    End If

                    'FU番号保持 T.Ueki 斜線書込み対応
                    PrintFUNo = strTemp

                    '******************************

                    '**CPU ************************
                    If gudt.SetFu.udtFu(funo).shtCanBus = 1 Then    ' CANBUS有
                        strTemp = "M001A-C"
                    Else
                        If funo = 0 Then                            ' FCU SUB
                            strTemp = "M001A-S"
                        Else                                        ' FU
                            strTemp = "M001A"
                        End If
                    End If
                    e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + wNO * 2 + 450, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                    '******************************

                    '**COM ************************
                    If funo = 0 Then                            ' FCU SUB
                        strTemp = ""
                    Else                                        ' FU
                        strTemp = "COM"
                    End If
                    e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + wNO * 2 + 466, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6)
                    '******************************

                    '**DIP SW *********************
                    e.DrawString("ON", fnt3, Brushes.Black, frmLeft + wNO * 2 + 440, frmUp + hBaseHeader * 4 + hBaseGrp * 2 + hGrp * i + 6)
                    e.DrawString("OFF", fnt3, Brushes.Black, frmLeft + wNO * 2 + 440, frmUp + hBaseHeader * 4 + hBaseGrp * 3 + hGrp * i + 6)

                    For j = 1 To 5
                        If funo And (&H1 << (j - 1)) Then
                            e.DrawString("o", fnt1, Brushes.Black, frmLeft + frmWidth - (wREMA + wCPU) + 7 + 12 * j, frmUp + hBaseHeader * 4 + hBaseGrp * 2 + hGrp * i + 2)
                        Else
                            e.DrawString("o", fnt1, Brushes.Black, frmLeft + frmWidth - (wREMA + wCPU) + 7 + 12 * j, frmUp + hBaseHeader * 4 + hBaseGrp * 3 + hGrp * i + 2)
                        End If
                        e.DrawString(j.ToString, fnt2, Brushes.Black, frmLeft + frmWidth - (wREMA + wCPU) + 7 + 12 * j, frmUp + hBaseHeader * 4 + hBaseGrp * 4 + hGrp * i)
                    Next
                    '******************************

                    'T.Ueki Reamrksの2行表示処理変更
                    '**Remarks*********************
                    'Ver2.0.0.2 Remarksのﾌｫﾝﾄｻｲｽﾞ
                    Dim fnt_remark As New Font("Courier New", 10)
                    Dim fnt_remarkj As New Font("ＭＳ 明朝", 10)

                    '改行するバイト数
                    Dim KaigyoByte As Integer = 8

                    Dim LenMoji As Integer      '文字の長さ
                    Dim SearchMoji As String    '検索対象文字(1文字)
                    Dim LineMoji1 As String     '1行目
                    Dim LineMoji2 As String     '2行目

                    Dim k As Long

                    strTemp = gGetString(gudt.SetChDisp.udtChDisp(funo).strRemarks)
                    'Ver2.0.1.5 「^」をｽﾍﾟｰｽと置き換える
                    'Ver2.0.1.7 「^」は改行文字とする
                    'strTemp = strTemp.Replace("^", " ")
                    '初期化
                    SearchMoji = ""
                    LineMoji1 = ""
                    LineMoji2 = ""
                    LenMoji = 0

                    For k = 1 To Len(strTemp)
                        SearchMoji = Mid(strTemp, k, 1)

                        Select Case SearchMoji
                            Case " " To "z"     '0x20 - 0x7A
                                LenMoji = LenMoji + 1
                                'Case "A" To "Z"
                                '    LenMoji = LenMoji + 1
                                'Case "0" To "9"
                                '    LenMoji = LenMoji + 1
                            Case "｡" To "ﾟ"   '0xA1 - 0xDF (｡ - ﾟ)半角ｶﾀｶﾅ  2014.11.17
                                LenMoji = LenMoji + 1
                            Case "^"
                                LenMoji = KaigyoByte + 1
                            Case Else
                                LenMoji = LenMoji + 2
                        End Select

                        '8ﾊﾞｲﾄ以下なら1行目
                        If LenMoji >= KaigyoByte + 1 Then
                            LineMoji2 = LineMoji2 + SearchMoji
                        Else
                            LineMoji1 = LineMoji1 + SearchMoji
                        End If
                    Next k

                    'Ver2.0.0.2 Reamrksのﾌｫﾝﾄは他と変更可能にしておく
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then     '' 和文表示の場合  2014.05.19
                        If LenMoji <= KaigyoByte Then
                            e.DrawString(strTemp, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                        Else
                            e.DrawString(LineMoji1, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                            e.DrawString(LineMoji2, fnt_remarkj, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16)
                        End If
                    Else
                        If LenMoji <= KaigyoByte Then
                            e.DrawString(strTemp, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                        Else
                            e.DrawString(LineMoji1, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                            e.DrawString(LineMoji2, fnt_remark, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 16)
                        End If
                    End If
                   

                    'strTemp = gGetString(gudt.SetChDisp.udtChDisp(funo).strRemarks)
                    'If strTemp.Length <= 8 Then
                    '    e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                    'Else
                    '    e.DrawString(strTemp.Substring(0, 8), fnt1, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5)
                    '    e.DrawString(strTemp.Substring(8), fnt1, Brushes.Black, frmLeft + 622, frmUp + hBaseHeader * 4 + hGrp * i + 5 + 12)
                    'End If

                    '******************************

                    '**TYPE NUMBER*****************
                    ''表示文字数変更　ver.1.4.0 2011.08.02
                    ''型名変更  2013.10.21
                    strTemp = Trim(Mid(gudt.SetChDisp.udtChDisp(funo).strFuType, _
                                       Len("SMS-U5650") + 1, _
                                       Len(gudt.SetChDisp.udtChDisp(funo).strFuType) - Len("SMS-U5650")))

                    'If Len(strTemp) = 2 Then
                    '    intTemp2 = 10
                    'Else
                    intTemp2 = 5
                    'End If

                    e.DrawString(strTemp, fnt1, Brushes.Black, frmLeft + intTemp2, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 5)
                    '******************************

                    'SLOTﾀｲﾌﾟ保持 T.Ueki 斜線書込み対応
                    PrintSlotTypeLen = Len(strTemp)

                    Select Case PrintSlotTypeLen
                        Case 0
                            PrintSlotType = ""
                        Case 2
                            PrintSlotType = Mid(strTemp, 2, 1)
                        Case Else
                            PrintSlotType = Mid(strTemp, 3, 1)
                    End Select


                    '**INPUT TYPE******************
                    For j = 0 To 7  '列ループ

                        'If mintRecCntSlot(i + ((mintPageCount - 1) * 7), j) > 0 Then

                        strTemp = ""
                        strTemp2 = ""


                        If j = 0 Then
                            ''型名変更  2013.10.21
                            If Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-12" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-13" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-15" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-18" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-12P" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-13P" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-15P" Or _
                               Trim(gudt.SetChDisp.udtChDisp(funo).strFuType) = "SMS-U5650-18P" Then

                                strRL = "-R"
                            Else
                                strRL = "-L"
                            End If
                        Else
                            strRL = "-L"
                        End If

                        Select Case gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType
                            Case 1
                                strTemp = "M003A"   ''DO

                                If (gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf > 1) Then    'TMRY
                                    strTemp2 = "RY"
                                Else
                                    strTemp2 = "DO"
                                End If

                                'If gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf = 2 Then
                                '    strTemp2 = "RY"
                                'ElseIf gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf = 3 Then
                                '    strTemp2 = "RY1"
                                'ElseIf gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf = 4 Then
                                '    strTemp2 = "RY2"
                                'Else
                                '    strTemp2 = "DO"
                                'End If

                            Case 2
                                strTemp = "M002A"   ''DI
                                strTemp2 = "DI"

                            Case 3
                                strTemp = "M030A"   ''AO
                                strTemp2 = "AO"

                            Case 4
                                strTemp = "M100A"   ''AI(2線式)
                                strTemp2 = "15"

                            Case 5
                                strTemp = "M110A"   ''AI(3線式)
                                strTemp2 = "TEMP"   ''型名変更  2013.10.21

                            Case 6
                                strTemp = "M500A"   ''AI(1-5V)
                                strTemp2 = "15"

                            Case 7
                                strTemp = "M400A"   ''AI(4-20mA)
                                strTemp2 = "42"

                            Case 8
                                strTemp = "M200A"   ''AI(K)
                                strTemp2 = "K"      ''型名変更  2013.10.21

                        End Select

                        If strTemp <> "" Then

                            e.DrawString(strTemp, fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 6, frmUp + hBaseHeader * 4 + hGrp * i + 5)                     '基板型式

                            If gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType = 1 Then   ' DO
                                '' TMRYの場合は4分割
                                If (gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf > 1) Then    'TMRY
                                    '' 4分割ライン
                                    intTempH = frmUp + hBaseHeader * 4 + hGrp * i
                                    e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j + intTemp / 4), intTempH + hBaseGrp, CInt((frmLeft + wNO * 2) + intTemp * j + intTemp / 4), intTempH + hGrp)
                                    e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j + (intTemp / 4 * 3)), intTempH + hBaseGrp, CInt((frmLeft + wNO * 2) + intTemp * j + (intTemp / 4 * 3)), intTempH + hGrp)

                                    '' 2013.10.25
                                    If 2 <= gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf And gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf <= 5 Then
                                        strRYtype = Mid(strRL, 2, 1) & " "
                                        'strRYtype = " " & Mid(strRL, 2, 1)
                                        intRYNo = gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf - 1
                                    ElseIf 6 <= gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf And gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf <= 9 Then
                                        strRYtype = Mid(strRL, 2, 1) & "1"
                                        'strRYtype = "1" & Mid(strRL, 2, 1)
                                        intRYNo = gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf - 5
                                    ElseIf 10 <= gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf And gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf <= 13 Then
                                        strRYtype = "2"
                                        intRYNo = gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtTerinf - 9
                                    Else
                                        strRYtype = ""
                                    End If


                                    e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)               '端子台型式(A)-1
                                    e.DrawString(strRYtype, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)         '端子台型式(A)-2
                                    e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j, frmUp + hBaseHeader * 4 + hGrp * i + 60)           'CABLE(A)

                                    'If ChkTerminalCount(gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType, 2, funo, j) Then
                                    If intRYNo > 1 Then
                                        e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)          '端子台型式(B)-1
                                        e.DrawString(strRYtype, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)    '端子台型式(B)-2
                                        e.DrawString((j + 1).ToString & "B", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 13, frmUp + hBaseHeader * 4 + hGrp * i + 60)  'CABLE(B)
                                    End If

                                    'If ChkTerminalCount(gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType, 3, funo, j) Then
                                    If intRYNo > 2 Then
                                        e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)          '端子台型式(C)-1
                                        e.DrawString(strRYtype, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)    '端子台型式(C)-2
                                        e.DrawString((j + 1).ToString & "C", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 26, frmUp + hBaseHeader * 4 + hGrp * i + 60)  'CABLE(C)
                                    End If

                                    'If ChkTerminalCount(gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType, 4, funo, j) Then
                                    If intRYNo > 3 Then
                                        e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)          '端子台型式(D)-1
                                        e.DrawString(strRYtype, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)    '端子台型式(D)-2
                                        e.DrawString((j + 1).ToString & "D", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 40, frmUp + hBaseHeader * 4 + hGrp * i + 60)  'CABLE(D)
                                    End If

                                Else
                                    e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6)      '端子台型式(A)
                                    e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 5, frmUp + hBaseHeader * 4 + hGrp * i + 60)          'CABLE(A)

                                    If ChkTerminalCount(mudtFuInfo(funo), 3, funo, j) Or ChkTerminalCount(mudtFuInfo(funo), 4, funo, j) Then    '' DOの場合、33-64をチェックするように変更  2015.01.26
                                        e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 28, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式(B)
                                        e.DrawString((j + 1).ToString & "B", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 33, frmUp + hBaseHeader * 4 + hGrp * i + 60)     'CABLE(B)
                                    End If

                                End If

                            ElseIf gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType = 2 Then   ' DI
                                e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式(A)
                                e.DrawString((j + 1).ToString & "A", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 5, frmUp + hBaseHeader * 4 + hGrp * i + 60)     'CABLE(A)

                                If ChkTerminalCount(mudtFuInfo(funo), 2, funo, j) Then
                                    e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 28, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式(B)
                                    e.DrawString((j + 1).ToString & "B", fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 33, frmUp + hBaseHeader * 4 + hGrp * i + 60) 'CABLE(B)
                                End If

                            ElseIf gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType = 5 Then   'AI(3線式)    2013.10.21
                                e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i)               '端子台型式(A)-1
                                e.DrawString(strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)         '端子台型式(A)-2
                                e.DrawString((j + 1).ToString, fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 9, frmUp + hBaseHeader * 4 + hGrp * i + 60)           'CABLE

                            ElseIf gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType = 3 Or gudt.SetFu.udtFu(funo).udtSlotInfo(j).shtType = 7 Then
                                e.DrawString(strTemp2, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i) '端子台型式

                                'T.Ueki 端子R L表示位置修正
                                'e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式
                                e.DrawString(strRL & "-1", fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 11)
                                e.DrawString((j + 1).ToString, fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 9, frmUp + hBaseHeader * 4 + hGrp * i + 60)           'CABLE

                            Else
                                e.DrawString(strTemp2 & strRL, fnt3, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 1, frmUp + hBaseHeader * 4 + hBaseGrp + hGrp * i + 6) '端子台型式
                                e.DrawString((j + 1).ToString, fnt2, Brushes.Black, frmLeft + wNO * 2 + intTemp * j + 9, frmUp + hBaseHeader * 4 + hGrp * i + 60)           'CABLE

                            End If


                        Else

                            '2015.4.9 T.Ueki 斜線書込み対応
                            intTempH = frmUp + hBaseHeader * 4 + hGrp * i   '高さ指定

                            If PrintFUNo = "FCU" Then

                                '5ｽﾛｯﾄ目から斜線
                                If j > 4 Then
                                    e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j), intTempH, CInt((frmLeft + wNO * 2) + intTemp * (j + 1)), intTempH + hGrp)
                                End If
                            Else

                                Select Case PrintSlotType

                                    Case "2"
                                        PrintPLCSW = 1
                                    Case "3"
                                        PrintPLCSW = 2
                                    Case "5"
                                        PrintPLCSW = 4
                                    Case "8"
                                        PrintPLCSW = 7
                                    Case Else
                                        '処理無し

                                End Select

                                If j > PrintPLCSW Then
                                    e.DrawLine(p1, CInt((frmLeft + wNO * 2) + intTemp * j), intTempH, CInt((frmLeft + wNO * 2) + intTemp * (j + 1)), intTempH + hGrp)


                                End If

                            End If

                        End If

                    Next j
                    '********************
                End If
            Next i

            p1.Dispose()
            p2.Dispose()
            p1d.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    ''端子台のページNoの初期値(指定ページ)を獲得する
    Private Function mGetPageIndex(ByVal intPageNo As Integer) As Integer

        Dim i As Integer, j As Integer
        Dim intCnt As Integer = 0

        If intPageNo = 1 Then Return 0

        For i = 0 To UBound(gudt.SetFu.udtFu)

            If i = (intPageNo - 1) * 7 Then Exit For

            For j = 0 To UBound(gudt.SetFu.udtFu(i).udtSlotInfo)

                'If mintRecCntSlot(i, j) > 0 Then

                Select Case gudt.SetFu.udtFu(i).udtSlotInfo(j).shtType

                    Case 1, 2  ''デジタル
                        intCnt += 2

                    Case 3, 4, 5, 6, 7, 8   ''アナログ
                        intCnt += 1

                End Select

                'End If

            Next

        Next

        Return intCnt

    End Function

    ''端子台毎に割りつけられているチャンネルの数を獲得する
    Private Sub GetRecCountSlot()

        Dim intFuNo As Integer, intPortNo As Integer
        Dim intFlag As Integer = 0

        ''チャンネル設定構造体
        For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

            intFlag = 0

            With gudt.SetChInfo.udtChannel(i)

                If .udtChCommon.shtChid > 0 Then

                    If .udtChCommon.shtChType = gCstCodeChTypeDigital And _
                       .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then
                        ''システムチャンネル(データ種別コードが機器状態)は端子台に表示しない
                    Else

                        If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then
                            If .udtChCommon.shtData <> gCstCodeChDataTypeAnalogJacom And _
                               .udtChCommon.shtData <> gCstCodeChDataTypeAnalogJacom55 Then
                                intFlag = 1
                            End If
                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeDigital Then
                            If .udtChCommon.shtData <> gCstCodeChDataTypeDigitalJacomNC And _
                               .udtChCommon.shtData <> gCstCodeChDataTypeDigitalJacomNO And _
                               .udtChCommon.shtData <> gCstCodeChDataTypeDigitalJacom55NC And _
                               .udtChCommon.shtData <> gCstCodeChDataTypeDigitalJacom55NO And _
                               .udtChCommon.shtData <> gCstCodeChDataTypeDigitalModbusNC And _
                               .udtChCommon.shtData <> gCstCodeChDataTypeDigitalModbusNO Then
                                intFlag = 1
                            End If
                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeMotor Then
                            If (.udtChCommon.shtData <> gCstCodeChDataTypeMotorDeviceJacom) Or _
                               (.udtChCommon.shtData <> gCstCodeChDataTypeMotorDeviceJacom55) Then
                                intFlag = 1
                            End If
                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then
                            If (.udtChCommon.shtData <> gCstCodeChDataTypeValveJacom) And (.udtChCommon.shtData <> gCstCodeChDataTypeValveJacom55) Then
                                intFlag = 1
                            End If
                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeComposite Then
                            intFlag = 1
                        ElseIf .udtChCommon.shtChType = gCstCodeChTypePulse Then
                            intFlag = 1
                        End If

                        If intFlag = 1 Then

                            For j As Integer = 0 To 1   'InとOutの２回分 LOOP

                                If j = 1 Then
                                    If (.udtChCommon.shtChType = gCstCodeChTypeMotor) _
                                    Or (.udtChCommon.shtChType = gCstCodeChTypeValve) Then
                                        ''Motor、Valve は 外部出力FU Addressがある
                                    Else
                                        Exit For
                                    End If
                                End If

                                If j = 0 Then
                                    ''入力
                                    intFuNo = .udtChCommon.shtFuno
                                    intPortNo = .udtChCommon.shtPortno

                                ElseIf j = 1 Then
                                    ''出力
                                    If .udtChCommon.shtChType = gCstCodeChTypeMotor Then
                                        intFuNo = .MotorFuNo            ''Fu No
                                        intPortNo = .MotorPortNo        ''Port No

                                    ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then
                                        intFuNo = .ValveDiDoFuNo        ''Fu No
                                        intPortNo = .ValveDiDoPortNo    ''Port No

                                    End If

                                End If

                                If intFuNo <> gCstCodeChCommonFuNoNothing And _
                                   (intFuNo >= 0 And intFuNo <= 20) And _
                                   (intPortNo >= 1 And intPortNo <= 8) Then
                                    mintRecCntSlot(intFuNo, intPortNo - 1) += 1 ''カウントアップ
                                End If

                            Next j
                        End If

                    End If

                End If

            End With

        Next i

        ''出力チャンネル設定構造体
        For i As Integer = LBound(gudt.SetChOutput.udtCHOutPut) To UBound(gudt.SetChOutput.udtCHOutPut)

            With gudt.SetChOutput.udtCHOutPut(i)

                intFuNo = .bytFuno
                intPortNo = .bytPortno

                If intFuNo <> 255 And _
                   (intFuNo >= 0 And intFuNo <= 20) And _
                   (intPortNo >= 1 And intPortNo <= 8) Then

                    intFuNo = .bytFuno
                    intPortNo = .bytPortno

                    mintRecCntSlot(intFuNo, intPortNo - 1) += 1 ''カウントアップ

                End If

            End With

        Next i

    End Sub


    ''D/I、D/Oの点数確認
    Private Function ChkTerminalCount(ByVal hudtFuInfo As gTypFuInfo, ByVal pos As Integer, ByVal funo As Integer, ByVal Slotno As Integer) As Boolean

        Dim i As Integer
        Dim exist_flg As Boolean = False
        Dim mtype As Integer = 0

        mtype = hudtFuInfo.udtFuPort(Slotno).intPortType

        Select Case mtype
            Case 1 ''DO
                If pos = 2 Then
                    For i = 16 To 31
                        '' 参照変数変更   2013.11.02
                        'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                        ' 2015.09.16 M.Kaihara
                        ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                        ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                        'If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                        If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                            exist_flg = True
                            Exit For
                        End If
                    Next
                ElseIf pos = 3 Then
                    For i = 32 To 47
                        '' 参照変数変更   2013.11.02
                        'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                        ' 2015.09.16 M.Kaihara
                        ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                        ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                        'If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                        If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                            exist_flg = True
                            Exit For
                        End If
                    Next
                ElseIf pos = 4 Then
                    For i = 48 To 63
                        '' 参照変数変更   2013.11.02
                        'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                        ' 2015.09.16 M.Kaihara
                        ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                        ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                        'If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                        If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                            exist_flg = True
                            Exit For
                        End If
                    Next
                End If

            Case 2 ''DI
                For i = 32 To 63
                    '' 参照変数変更   2013.11.02
                    'If gudt.SetChDisp.udtChDisp(funo).udtSlotInfo(Slotno).udtPinInfo(i).shtChid <> 0 Then

                    ' 2015.09.16 M.Kaihara
                    ' 端子リスト印刷において2枚目のページが印刷されない不具合を修正。
                    ' 2枚目のページリストを数える際、strChNoだけでなくstrChNo2, strChNo3も見るように修正。
                    'If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Then
                    If hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo2 <> "" Or hudtFuInfo.udtFuPort(Slotno).udtFuPin(i).strChNo3 <> "" Then
                        exist_flg = True
                        Exit For
                    End If
                Next
        End Select

        Return exist_flg

    End Function

#End Region

End Class