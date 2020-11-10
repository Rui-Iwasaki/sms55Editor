﻿Module modPrint




#Region "定数定義"

    'Ver2.0.4.1 Font種類変更 英文、和文問わずMS明朝とする
    'Ver2.0.4.2 Fontは英文は「CurierNew」和文はMS明朝とする
    'フォント設定
    Public gFnt16 As New Font("Courier New", 16)
    Public gFnt14 As New Font("Courier New", 14)
    Public gFnt13 As New Font("Courier New", 13)
    Public gFnt12 As New Font("Courier New", 12)
    Public gFnt11 As New Font("Courier New", 11)
    Public gFnt10 As New Font("Courier New", 10)
    Public gFnt9 As New Font("Courier New", 9)
    Public gFnt8 As New Font("Courier New", 8)
    Public gFnt7 As New Font("Courier New", 7)

    'Ver2.0.3.6 Font種類変更
    ''日本語用  2014.05.19
    Public gFnt10j As New Font("ＭＳ 明朝", 10) ''和文仕様 20200217 hori
    Public gFnt9j As New Font("ＭＳ 明朝", 9)
    Public gFnt8j As New Font("ＭＳ 明朝", 8)
    Public gFnt7j As New Font("ＭＳ 明朝", 7)   ''和文仕様 20200217 hori


    ''1文字あたりの大きさ（計算用）
    Public gFntScale14 As Double = 14
    Public gFntScale13 As Double = 13
    Public gFntScale12 As Double = 12
    Public gFntScale10 As Double = 10
    Public gFntScale9 As Double = 7.5     ' 実際の表示サイズにて調整  2013.07.24
    Public gFntScale8 As Double = 8
    Public gFntScale7 As Double = 7.5
    Public gFntScale6 As Double = 6

    Public gFntScale9j As Double = 6.5      ' 日本語ﾌｫﾝﾄ用追加　Ver1.8.2 2015.11.19 追加

    ''文字色
    Public gFntColorBlack As Brush = Brushes.Black      ''黒
    Public gFntColorWhite As Brush = Brushes.White      ''白



#Region "ページフレーム（フッター）"

    ''ライン描画開始位置  2015/4/10 高さ調整 T.Ueki
    Private Const mCstFooterStart As Single = 1075 '1070

    ''サブタイトル名
    Private Const mCstFooterNameSubShipNo As String = "SHIP NO."
    Private Const mCstFooterNameSubDrawingNo As String = "DRAWING NO."
    Private Const mCstFooterNameSubRevNo As String = "REV."
    Private Const mCstFooterNameSubPageNo As String = "PAGE"

    ''折返し文字数長
    Private Const mCstFooterMaxLengthShipNo As Integer = 20

    Private Const mCstFooterRowHight As Single = 40 '45

    ''ラベル描画開始位置(メイン)
    Private Const mCstFooterStartLabel As Single = 1080 '1085

    ''画面左端からのX位置
    Private Const mCstFooterCol1 As Single = 240    ''Name
    Private Const mCstFooterCol2 As Single = 445 '460    ''Ship No.     2013.11.20
    Private Const mCstFooterCol3 As Single = 585 '600    ''Drawing No.  2013.11.20
    Private Const mCstFooterCol4 As Single = 685 '700    ''Revision No. 2013.11.20

#End Region

#Region "端子台"

    ''フレームサイズ指定
    'Public Const gCstFrameTerminalLeft As Single = 40  '40     ''画面左端からのX位置
    'Public Const gCstFrameTerminalLeft As Single = 25  '40     ''画面左端からのX位置     ' 2015.10.16
    Public gCstFrameTerminalLeft As Single ' 2015.10.22 Ver1.7.5 CH/ﾀｸﾞ印字切り替えのため変数に変更
    '2015/4/10 T.Ueki 高さ調整
    Public Const gCstFrameTerminalUp As Single = 55 '35         ''画面上端からのY位置
    'Public Const gCstFrameTerminalWidth As Single = 710 '725     ''幅    2013.11.20
    'Public Const gCstFrameTerminalWidth As Single = 725 '725     ''幅    2015.10.16
    Public gCstFrameTerminalWidth As Single ' 2015.10.22 Ver1.7.5 CH/ﾀｸﾞ印字切り替えのため変数に変更
    '2015/4/10 高さ調整 T.Ueki
    Public Const gCstFrameTerminalHight As Single = 1060    ''高さ 1080

#End Region

#Region "Local Unit"

    ''サイズ指定
    'Ver2.0.6.5 Local Unitの帳票の左端を他とそろえるために、40へ変更
    Public Const gCstFrameLocalUnitLeft As Single = 40 '45   '60    '画面左端からのX位置 Ver2.0.0.2 60へ変更

    '2015/4/10 T.Ueki 高さ調整
    Public Const gCstFrameLocalUnitUp As Single = 55 '35        ''画面上端からのY位置
    Public Const gCstFrameLocalUnitWidth As Single = 700    ''幅
    Public Const gCstFrameLocalUnitHight As Single = 1060   ''高さ

    ''ページ区分
    'Public Const gCstPageKind_A As String = "A"
    'Public Const gCstPageKind_B As String = "B"

#End Region

#Region "グラフ印字"

    '-------------------
    ''アナログメーター
    '-------------------
    'グラフ枠
    Public gCstPathPrtGraphExhBar As String = "\GraphBmp\FrameExhBarAnalog.bmp"

    'Ver2.0.7.M (新デザイン)背景画像（タイトルに線無し版）
    Public gCstPathPrtGraphExhBar_NEW As String = "\GraphBmp\FrameExhBarAnalog_NEW.bmp"

    'BMPサイズ 1/8
    Public gCstPathPrtGraphAnalog8_3 As String = "\GraphBmp\AnalogMeter8_3.bmp"     '3分割
    Public gCstPathPrtGraphAnalog8_4 As String = "\GraphBmp\AnalogMeter8_4.bmp"     '4分割
    Public gCstPathPrtGraphAnalog8_5 As String = "\GraphBmp\AnalogMeter8_5.bmp"     '5分割
    Public gCstPathPrtGraphAnalog8_6 As String = "\GraphBmp\AnalogMeter8_6.bmp"     '6分割
    Public gCstPathPrtGraphAnalog8_7 As String = "\GraphBmp\AnalogMeter8_7.bmp"     '7分割

    'Ver2.0.7.M (新デザイン)アナログメーター印字新デザイン対応
    'BMPサイズ 1/8 
    Public gCstPathPrtGraphAnalog8_3_NEW As String = "\GraphBmp\AnalogMeter8_3_NEW.bmp"     '3分割
    Public gCstPathPrtGraphAnalog8_4_NEW As String = "\GraphBmp\AnalogMeter8_4_NEW.bmp"     '4分割
    Public gCstPathPrtGraphAnalog8_5_NEW As String = "\GraphBmp\AnalogMeter8_5_NEW.bmp"     '5分割
    Public gCstPathPrtGraphAnalog8_6_NEW As String = "\GraphBmp\AnalogMeter8_6_NEW.bmp"     '6分割
    Public gCstPathPrtGraphAnalog8_7_NEW As String = "\GraphBmp\AnalogMeter8_7_NEW.bmp"     '7分割


    'BMPサイズ 1/2
    Public gCstPathPrtGraphAnalog2_3 As String = "\GraphBmp\AnalogMeter2_3.bmp"     '3分割
    Public gCstPathPrtGraphAnalog2_4 As String = "\GraphBmp\AnalogMeter2_4.bmp"     '4分割
    Public gCstPathPrtGraphAnalog2_5 As String = "\GraphBmp\AnalogMeter2_5.bmp"     '5分割
    Public gCstPathPrtGraphAnalog2_6 As String = "\GraphBmp\AnalogMeter2_6.bmp"     '6分割
    Public gCstPathPrtGraphAnalog2_7 As String = "\GraphBmp\AnalogMeter2_7.bmp"     '7分割

    '------------------
    ''フリーグラフ
    '------------------
    Public gCstPathPrtGraphFreeFrame As String = "\GraphBmp\FrameFree.bmp"              'グラフ枠
    'Public gCstPathPrtGraphFreeFramePrint As String = "\GraphBmp\FrameFree.bmp"         'グラフ枠（印字用）

    'Counter
    Public gCstPathPrtGraphFreeCounter As String = "\GraphBmp\FreeCounter.bmp"

    'Indicator
    Public gCstPathPrtGraphFreeIndicatorData As String = "\GraphBmp\FreeIndicatorData.bmp"
    Public gCstPathPrtGraphFreeIndicatorAlarm As String = "\GraphBmp\FreeIndicatorAlarm.bmp"
    Public gCstPathPrtGraphFreeIndicatorRepose As String = "\GraphBmp\FreeIndicatorRepose.bmp"
    Public gCstPathPrtGraphFreeIndicatorSensorFail As String = "\GraphBmp\FreeIndicatorSensorFail.bmp"

    'AnalogMeter
    Public gCstPathPrtGraphFreeAnalogMeterScale3 As String = "\GraphBmp\FreeAnalogMeter3.bmp"
    Public gCstPathPrtGraphFreeAnalogMeterScale4 As String = "\GraphBmp\FreeAnalogMeter4.bmp"
    Public gCstPathPrtGraphFreeAnalogMeterScale5 As String = "\GraphBmp\FreeAnalogMeter5.bmp"
    Public gCstPathPrtGraphFreeAnalogMeterScale6 As String = "\GraphBmp\FreeAnalogMeter6.bmp"
    Public gCstPathPrtGraphFreeAnalogMeterScale7 As String = "\GraphBmp\FreeAnalogMeter7.bmp"

    'Bar
    Public gCstPathPrtGraphFreeBar3 As String = "\GraphBmp\FreeBar3.bmp"
    Public gCstPathPrtGraphFreeBar4 As String = "\GraphBmp\FreeBar4.bmp"
    Public gCstPathPrtGraphFreeBar5 As String = "\GraphBmp\FreeBar5.bmp"
    Public gCstPathPrtGraphFreeBar6 As String = "\GraphBmp\FreeBar6.bmp"
    Public gCstPathPrtGraphFreeBar7 As String = "\GraphBmp\FreeBar7.bmp"

#End Region


#Region "端子表印刷"     '' Ver1.9.3 2016.01.12 追加

    Public gPrtDigData() As gDigData

#End Region


#End Region

#Region "列挙体定義"

    Public Enum gEnmPrintTerminalPageType
        tptFuList
        tptDigital
        tptAnalog2
        tptAnalog3
        tptFU       'Ver2.0.0.2 LU統合用
    End Enum

#End Region

#Region "構造体定義"

#Region "FU情報"

    Public Structure gTypFuInfo

        Dim strFuName As String             ''FCU/FU名称
        Dim strFuType As String             ''FCU/FU種類
        Dim strNamePlate As String          ''FCU/FU盤名
        Dim strRemarks As String            ''コメント
        Dim udtFuPort() As gTypFuInfoPort   ''Port情報詳細

    End Structure

    Public Structure gTypFuInfoPort

        Dim intPortType As Integer          ''ポートタイプ（0x0001:DO ～ 0x0008:AI_K）
        Dim intTerinf As Integer            ''端子台設定　ver1.4.0 2011.08.17
        Dim udtFuPin() As gTypFuInfoPin     ''ポートの設定情報詳細

    End Structure

    Public Structure gTypFuInfoPin

        ''仮設定フラグ
        Dim blnDmyStatusIn As Boolean       ''ステータス名称（入力側）
        Dim blnDmyStatusOut As Boolean      ''ステータス名称（出力側）
        Dim blnDmyScaleRange As Boolean     ''スケール値（上下限で１つ）
        Dim blnDmyFUadress As Boolean       'Ver2.0.2.4 該当FUｱﾄﾞﾚｽﾀﾞﾐｰ

        ''CH共通
        Dim blnChComDmy As Boolean          ''ダミーCHフラグ
        Dim blnChComSc As Boolean           ''隠しCHフラグ
        Dim blnChComSc2 As Boolean          'Ver2.0.2.6 隠しCHフラグ2
        Dim blnChComSc3 As Boolean          'Ver2.0.2.6 隠しCHフラグ3
        Dim strGroupNo As String            ''グループNO
        Dim intChId As Integer              ''CH_ID
        Dim strChNo As String               ''CH_NO
        Dim strChNo2 As String              ''CH_NO (同一端子CH表示用)     2013.11.23
        Dim strChNo3 As String              ''CH_NO (同一端子CH表示用)     2014.09.18
        Dim intChType As Integer            ''CH種別
        Dim intDataType As Integer          ''データ種別
        Dim intSignal As Short              ''入力信号
        Dim strItemName As String           ''CHアイテム名称
        Dim strStatus As String             ''ステータス種別
        Dim strSignal As String             ''Signal

        ''CH個別設定
        Dim strRangeHigh As String          ''スケール値（上限）
        Dim strRangeLow As String           ''スケール値（下限）
        Dim intDecimalPosition As Integer   ''小数点位置
        Dim intRangeType As Integer         ''

        ''出力設定
        Dim bytOutput As Byte               ''CH OUT Type Setup
        Dim bytOutStatus As Byte            ''Output Movement
        Dim intOutMask As Integer           ''Output Movement マスクデータ（ビットパターン）
        Dim strOutStatus As String          ''OutputStatus  Ver1.9.3 2016.01.16 追加

        ''端子台
        Dim strWireMark() As String         ''WireMark
        Dim strWireMarkClass() As String    ''WireMarkClass
        Dim strCoreNoIn() As String         ''CoreNoIn
        Dim strCoreNoCom() As String        ''CoreNoCom
        Dim strDist() As String             ''DEST
        Dim intTerminalNo As Integer        ''端子台番号

    End Structure

#End Region

#Region "印刷情報"

    Public Structure gTypPrintTerminalInfo

        ''最初のページに表示する情報（21固定の配列）
        Dim udtRowInfoPage1() As gTypPrintTerminalPage1

        ''１ページに表示する行情報（デジタルなら32、アナログなら16や8の配列）
        Dim udtRowInfo() As gTypFuInfoPin

        Dim intPageNo As Integer                        ''ページ番号
        Dim udtPageType As gEnmPrintTerminalPageType    ''ページタイプ  （tptFuListなど）
        Dim intPortType As Integer                      ''ポートタイプ  （0x01:DO ～ 0x08:AI_K）
        Dim intTerinf As Integer                        ''端子台設定　ver1.4.0 2011.08.17
        Dim intStartRowIndex As Integer                 ''スタート行番号

        'Ver2.0.0.2 Terminal Print 基板指定印刷用
        Dim intFuNo As Integer
        Dim intSlotNo As Integer

        Dim strBoxFuInfo As String                      ''スロット情報①（印刷右上に表示する文字列：FUタイプ）
        Dim strBoxSlotInfo As String                    ''スロット情報②（印刷右上に表示する文字列：ポートタイプ）
        Dim strBoxRemarks As String                     ''Remarks情報　 （印刷右上に表示する文字列：Remarks）
        Dim strFrameInfoLine1 As String                 ''フレーム情報①（印刷左下に表示する文字列：１行目）
        Dim strFrameInfoLine2 As String                 ''フレーム情報②（印刷左下に表示する文字列：２行目）
        Dim strDrawinfNoInfo As String                  ''フレーム情報③（印刷右下に表示する文字列：図番）


    End Structure

    Public Structure gTypPrintTerminalPage1

        Dim strNo As String
        Dim strFuName As String
        Dim strNamePlate As String
        Dim strFuType As String
        Dim strCodeNo As String
        Dim strRemarks As String

    End Structure

#End Region

#Region "端子表印刷 ﾃﾞｼﾞﾀﾙ"     '' Ver1.9.3 2016.01.12 追加

    Public Structure gDigStrSet
        Dim strWireMark1 As String
        Dim strWireMark2 As String
        Dim strCoreNo1 As String
        Dim strCoreNo2 As String
        Dim strTermRem As String
    End Structure


    '' ﾃﾞｼﾞﾀﾙ設定構造体
    Public Structure gDigData
        Dim nCHNo As UShort         '' CHNo.
        Dim strItemName As String   '' CH名称
        Dim strStatus As String     '' ｽﾃｰﾀｽ
        Dim CHType As Integer       '' CH種別
        Dim DataType As Integer
        Dim FCUNo As Integer        '' FCUNo
        Dim PortNo As Integer       '' 基板No
        Dim TermNo As Integer       '' 端子No
        Dim bSCFg As Boolean        '' 隠しCHﾌﾗｸﾞ
        Dim bWKFg As Boolean        '' ﾜｰｸCHﾌﾗｸﾞ
        Dim bDummyFg As Boolean     '' ﾀﾞﾐｰCHﾌﾗｸﾞ
        Dim gDigStrSet() As gDigStrSet
    End Structure



#End Region

#End Region

#Region "内部関数"

    '----------------------------------------------------------------------------
    ' 機能説明  ： ページフレーム作成（Terminal）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Title1行目
    '           ： ARG3 - (I ) Title2行目
    '           ： ARG4 - (I ) Page No
    '           ： ARG5 - (I ) Part選択（True:Machinery、False:Cargo）
    ' 戻値      ： なし
    ' 履歴      ： パート選択の引数削除　ver.1.4.0 2011.08.17
    '----------------------------------------------------------------------------
    Public Sub gPrtDrawOutFrameTerminal(ByVal objGraphics As System.Drawing.Graphics, _
                                        ByVal hstrTitleLine1 As String, _
                                        ByVal hstrTitleLine2 As String, _
                                        ByVal hstrDrawingNo As String, _
                                        ByVal hintPageNo As Integer, _
                                        ByVal hintPagePrint As Integer)

        Try

            Dim strShipNo As String = ""
            Dim strDrawingNo As String = ""
            Dim strRev As String = ""
            Dim p2 As New Pen(Color.Black, 2)

            '' 印刷フォーム調整　ver.1.4.0 2011.08.17
            '' 枠線(区切り線)を太くする

            ''----------------------------
            '' ライン描画
            ''----------------------------
            ''アウトフレーム
            objGraphics.DrawRectangle(p2, gCstFrameTerminalLeft, gCstFrameTerminalUp, gCstFrameTerminalWidth, gCstFrameTerminalHight)

            ''フッターの横線
            objGraphics.DrawLine(p2, gCstFrameTerminalLeft, mCstFooterStart, gCstFrameTerminalLeft + gCstFrameTerminalWidth, mCstFooterStart)

            ''フッターの縦線
            objGraphics.DrawLine(p2, mCstFooterCol1, mCstFooterStart, mCstFooterCol1, mCstFooterStart + mCstFooterRowHight)
            objGraphics.DrawLine(p2, mCstFooterCol2, mCstFooterStart, mCstFooterCol2, mCstFooterStart + mCstFooterRowHight)
            objGraphics.DrawLine(p2, mCstFooterCol3, mCstFooterStart, mCstFooterCol3, mCstFooterStart + mCstFooterRowHight)
            objGraphics.DrawLine(p2, mCstFooterCol4, mCstFooterStart, mCstFooterCol4, mCstFooterStart + mCstFooterRowHight)

            ''----------------------------
            '' 文字列描画
            ''----------------------------
            ''SelectPart削除(船名、図番はパート区別なし)　ver.1.4.0 2011.08.17
            'If hblnSelectMach Then
            ''Machinery
            strShipNo = gGetString(gudt.SetChGroupSetM.udtGroup.strShipNo)      ''スペース削除    2013.11.20
            ''DrawingNo変更   2013.10.18
            'strDrawingNo = gudt.SetChGroupSetM.udtGroup.strDrawNo
            'Ver2.0.5.9 DrawNoの前後のスペースを解除
            strDrawingNo = gudt.SetChGroupSetM.udtGroup.strDrawNo.Trim & "-" & hstrDrawingNo
            'Else
            ' ''Carg
            'strShipNo = gudt.SetChGroupSetC.udtGroup.strShipName
            'strDrawingNo = gudt.SetChGroupSetC.udtGroup.strDrawNo
            'End If

            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                ''Title
                objGraphics.DrawString(hstrTitleLine1, gFnt10, gFntColorBlack, gCstFrameTerminalLeft + 10, mCstFooterStart + 6)
                '高さ調整 2015/4/10 T.Ueki
                objGraphics.DrawString(hstrTitleLine2, gFnt10j, gFntColorBlack, gCstFrameTerminalLeft + 10, mCstFooterStartLabel + 16)

                ''Ship No 
                objGraphics.DrawString(mCstFooterNameSubShipNo, gFnt8, gFntColorBlack, mCstFooterCol1 + 5, mCstFooterStart + 3)             ''1行目]
                '高さ調整 2015/4/10 T.Ueki
                Call mDrawStringsLine2(objGraphics, strShipNo, mCstFooterMaxLengthShipNo, mCstFooterCol1, mCstFooterStartLabel + 10)             ''2行目

            Else
                ''Title
                objGraphics.DrawString(hstrTitleLine1, gFnt10, gFntColorBlack, gCstFrameTerminalLeft + 10, mCstFooterStart + 6)
                '高さ調整 2015/4/10 T.Ueki
                objGraphics.DrawString(hstrTitleLine2, gFnt10, gFntColorBlack, gCstFrameTerminalLeft + 10, mCstFooterStartLabel + 16)

                ''Ship No 
                objGraphics.DrawString(mCstFooterNameSubShipNo, gFnt8, gFntColorBlack, mCstFooterCol1 + 5, mCstFooterStart + 3)             ''1行目]
                '高さ調整 2015/4/10 T.Ueki
                Call mDrawStringsLine2(objGraphics, strShipNo, mCstFooterMaxLengthShipNo, mCstFooterCol1, mCstFooterStartLabel + 10)             ''2行目

            End If
            ''Drawing No
            ''表示変更  2013.10.18
            objGraphics.DrawString(mCstFooterNameSubDrawingNo, gFnt8, gFntColorBlack, mCstFooterCol2 + 5, mCstFooterStart + 3)          ''1行目
            ''objGraphics.DrawString(strDrawingNo, gFnt10, gFntColorBlack, mCstFooterCol2 + 20, mCstFooterStartLabel + 6)                 ''2行目
            '高さ調整 2015/4/10 T.Ueki
            objGraphics.DrawString(strDrawingNo, gFnt10, gFntColorBlack, mCstFooterCol2 + 5, mCstFooterStartLabel + 16)                 ''2行目    2013.10.18

            ''Revision No
            '' バージョン追加 ver1.4.0 2011.08.17
            'T.Ueki ファイル管理仕様変更
            strRev = gudtFileInfo.strFileName
            'strRev = gudtFileInfo.strFileName & "-" & gudtFileInfo.strFileVersion
            objGraphics.DrawString(mCstFooterNameSubRevNo, gFnt8, gFntColorBlack, mCstFooterCol3 + 5, mCstFooterStart + 3)              ''1行目
            If Len(strRev) <= 11 Then
                'T.Ueki ファイル管理仕様変更
                'objGraphics.DrawString(strRev, gFnt10, gFntColorBlack, mCstFooterCol3 + 5, mCstFooterStartLabel + 6)                    ''2行目
                '高さ調整 2015/4/10 T.Ueki
                objGraphics.DrawString(strRev, gFnt10, gFntColorBlack, mCstFooterCol3 + 27, mCstFooterStartLabel + 16)                    ''2行目
            Else
                objGraphics.DrawString(strRev, gFnt10, gFntColorBlack, mCstFooterCol3 - 2, mCstFooterStartLabel + 16)                    ''2行目
            End If

            ''Page No
            objGraphics.DrawString(mCstFooterNameSubPageNo, gFnt8, gFntColorBlack, mCstFooterCol4 + 5, mCstFooterStart + 3)             ''1行目
            ''objGraphics.DrawString("No." & hintPageNo.ToString, gFnt10, gFntColorBlack, mCstFooterCol4 + 20, mCstFooterStartLabel + 6)  ''2行目
            If hintPagePrint = 1 Then   ''ページ印刷有りの場合のみ   2013.10.18
                '高さ調整 2015/4/10 T.Ueki
                objGraphics.DrawString(hintPageNo.ToString("000"), gFnt10, gFntColorBlack, mCstFooterCol4 + 20, mCstFooterStartLabel + 16)  ''2行目    2013.10.18
            End If

            p2.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ページフレーム作成（LocalUnit）
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) Title1行目
    '           ： ARG3 - (I ) Title2行目
    '           ： ARG4 - (I ) Page No
    '           ： ARG5 - (I ) Part選択（True:Machinery、False:Cargo）
    ' 戻値      ： なし
    ' 履歴      ： パート選択の引数削除　ver.1.4.0 2011.08.02
    '----------------------------------------------------------------------------
    Public Sub gPrtDrawOutFrameLocalUnit(ByVal objGraphics As System.Drawing.Graphics, _
                                         ByVal hstrTitleLine1 As String, _
                                         ByVal hstrTitleLine2 As String, _
                                         ByVal hstrDrawingNo As String, _
                                         ByVal hintPageNo As Integer, _
                                         ByVal hintPagePrint As Integer)

        Try

            Dim strShipNo As String = ""
            Dim strDrawingNo As String = ""
            Dim strRev As String = ""
            Dim p2 As New Pen(Color.Black, 2)
            Dim unit_x As Integer = 0
            Dim unit_y As Integer = 0

            '' 印刷フォーム調整　ver.1.4.0 2011.08.10
            '' 枠線(区切り線)を太くする

            ''----------------------------
            '' ライン描画
            ''----------------------------
            ''アウトフレーム
            objGraphics.DrawRectangle(p2, gCstFrameLocalUnitLeft, gCstFrameTerminalUp, gCstFrameLocalUnitWidth, gCstFrameTerminalHight)

            ''フッターの横線
            objGraphics.DrawLine(p2, gCstFrameLocalUnitLeft, mCstFooterStart, gCstFrameLocalUnitLeft + gCstFrameLocalUnitWidth, mCstFooterStart)

            ''フッターの縦線
            objGraphics.DrawLine(p2, mCstFooterCol1, mCstFooterStart, mCstFooterCol1, mCstFooterStart + mCstFooterRowHight)
            objGraphics.DrawLine(p2, mCstFooterCol2, mCstFooterStart, mCstFooterCol2, mCstFooterStart + mCstFooterRowHight)
            objGraphics.DrawLine(p2, mCstFooterCol3, mCstFooterStart, mCstFooterCol3, mCstFooterStart + mCstFooterRowHight)
            objGraphics.DrawLine(p2, mCstFooterCol4, mCstFooterStart, mCstFooterCol4, mCstFooterStart + mCstFooterRowHight)

            ''----------------------------
            '' 文字列描画
            ''----------------------------
            ''SelectPart削除(船名、図番はパート区別なし)　ver.1.4.0 2011.08.02
            'If hblnSelectMach Then
            ''Machinery
            'strShipNo = gudt.SetChGroupSetM.udtGroup.strShipNo       ''スペース削除    2015.4.22 T.Ueki
            strShipNo = gGetString(gudt.SetChGroupSetM.udtGroup.strShipNo)
            ''DrawingNo変更   2013.10.18
            'strDrawingNo = gudt.SetChGroupSetM.udtGroup.strDrawNo
            'Ver2.0.2.3 端子表印字と統一されたため、A,B,Cは -1した数となる
            'Ver2.0.5.9 DrawNoの前後のスペースを解除
            Select Case (hintPageNo - 1)
                Case 1
                    strDrawingNo = gudt.SetChGroupSetM.udtGroup.strDrawNo.Trim & "-" & hstrDrawingNo & "A"
                Case 2
                    strDrawingNo = gudt.SetChGroupSetM.udtGroup.strDrawNo.Trim & "-" & hstrDrawingNo & "B"
                Case 3
                    strDrawingNo = gudt.SetChGroupSetM.udtGroup.strDrawNo.Trim & "-" & hstrDrawingNo & "C"
            End Select

            'Else
            ' ''Carg
            'strShipNo = gudt.SetChGroupSetC.udtGroup.strShipName
            'strDrawingNo = gudt.SetChGroupSetC.udtGroup.strDrawNo
            'End If

            If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200306 hori
                ''Title
                objGraphics.DrawString(hstrTitleLine1, gFnt10j, gFntColorBlack, gCstFrameLocalUnitLeft + 10, mCstFooterStart + 8)
                '高さ調整 2015/4/22 T.Ueki
                objGraphics.DrawString(hstrTitleLine2, gFnt10j, gFntColorBlack, gCstFrameLocalUnitLeft + 10, mCstFooterStartLabel + 16)
            Else
                ''Title
                objGraphics.DrawString(hstrTitleLine1, gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + 10, mCstFooterStart + 8)
                '高さ調整 2015/4/22 T.Ueki
                objGraphics.DrawString(hstrTitleLine2, gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + 10, mCstFooterStartLabel + 16)
            End If

            ''Ship No
            objGraphics.DrawString(mCstFooterNameSubShipNo, gFnt8, gFntColorBlack, mCstFooterCol1 + 5, mCstFooterStart + 3)             ''サブ
            '高さ調整 2015/4/22 T.Ueki
            Call mDrawStringsLine2(objGraphics, strShipNo, mCstFooterMaxLengthShipNo, mCstFooterCol1, mCstFooterStartLabel + 10)             ''メイン

            ''Drawing No
            ''表示変更  2013.10.18
            objGraphics.DrawString(mCstFooterNameSubDrawingNo, gFnt8, gFntColorBlack, mCstFooterCol2 + 5, mCstFooterStart + 3)          ''サブ
            ''objGraphics.DrawString(strDrawingNo, gFnt10, gFntColorBlack, mCstFooterCol2 + 20, mCstFooterStartLabel + 6)                 ''メイン
            '高さ調整 2015/4/22 T.Ueki
            objGraphics.DrawString(strDrawingNo, gFnt10, gFntColorBlack, mCstFooterCol2 + 5, mCstFooterStartLabel + 16)                  ''メイン

            ''Revision No
            '' バージョン追加 ver1.4.0
            'T.Ueki ファイル管理仕様変更
            strRev = gudtFileInfo.strFileName
            'strRev = gudtFileInfo.strFileName & "-" & gudtFileInfo.strFileVersion
            objGraphics.DrawString(mCstFooterNameSubRevNo, gFnt8, gFntColorBlack, mCstFooterCol3 + 5, mCstFooterStart + 3)              ''サブ
            If Len(strRev) <= 11 Then
                'T.Ueki ファイル管理仕様変更
                '高さ調整 2015/4/22 T.Ueki
                objGraphics.DrawString(strRev, gFnt10, gFntColorBlack, mCstFooterCol3 + 27, mCstFooterStartLabel + 16)     ''メイン
                'objGraphics.DrawString(strRev, gFnt10, gFntColorBlack, mCstFooterCol3 + 5, mCstFooterStartLabel + 6)     ''メイン
            Else
                '高さ調整 2015/4/22 T.Ueki
                objGraphics.DrawString(strRev, gFnt10, gFntColorBlack, mCstFooterCol3 - 2, mCstFooterStartLabel + 16)     ''メイン
            End If

            ''Page No
            objGraphics.DrawString(mCstFooterNameSubPageNo, gFnt8, gFntColorBlack, mCstFooterCol4 + 5, mCstFooterStart + 3)             ''サブ
            ''objGraphics.DrawString("No." & hintPageNo.ToString, gFnt10, gFntColorBlack, mCstFooterCol4 + 20, mCstFooterStartLabel + 6)  ''メイン
            If hintPagePrint = 1 Then   ''ページ印刷有りの場合のみ   2013.10.18
                objGraphics.DrawString(hintPageNo.ToString("000"), gFnt10, gFntColorBlack, mCstFooterCol4 + 20, mCstFooterStartLabel + 16)  ''メイン    2013.10.18
            End If


            ''ユニット型式    型式名変更　2013.10.21
            ''ユニット型式追加　2015.03.17
            unit_x = 50
            '高さ調整 2015/4/22 T.Ueki
            'unit_y = 940
            unit_y = 960
            objGraphics.DrawString("U5650-2                      BP02", gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y)          ''ユニット型式
            objGraphics.DrawString("     -3                      BP03", gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + 13)     ''ユニット型式
            objGraphics.DrawString("     -5                      BP05", gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + 26)     ''ユニット型式
            objGraphics.DrawString("     -8                      BP08", gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + 39)     ''ユニット型式
            objGraphics.DrawString("     -12/-12P,-32            BP02", gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + 52)     ''ユニット型式    2013.10.21型名追加
            objGraphics.DrawString("     -13/-13P,-23,-33/-33P   BP03", gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + 65)     ''ユニット型式
            objGraphics.DrawString("     -15/-15P,-35/-35P       BP05", gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + 78)     ''ユニット型式
            objGraphics.DrawString("     -18/-18P                BP08", gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + 91)     ''ユニット型式
            'T.Ueki 10スロット削除
            'objGraphics.DrawString("     -10P  BP03", gFnt10, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + 104)    ''ユニット型式

            'Ver2.0.7.4 基板ﾊﾞｰｼﾞｮﾝ印刷対応 備考欄右側に注意書き出力
            If g_bytTerVer = 1 Then
                unit_x = 50 + 290
                unit_y = 960

                Dim intYhosei As Integer
                Dim ExplanatoryEJ As Byte   ''説明文の和英設定 0:英文 1:和文 2:全和文　ver2.0.8.I 2019.02.21 
                ExplanatoryEJ = gudt.SetSystem.udtSysSystem.shtLanguage ''システムの言語を反映
                ''逆転フラグ有り
                If g_bytExoTxtEtoJ = 1 Then
                    ''英なら和にする,和なら英にする
                    If ExplanatoryEJ = 0 Then
                        ExplanatoryEJ = 1
                    ElseIf ExplanatoryEJ = 1 Then
                        ExplanatoryEJ = 0
                    End If
                End If

                If ExplanatoryEJ = 1 Then
                    intYhosei = 10
                    '和文
                    objGraphics.DrawString("MODULE TYPE", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y)
                    objGraphics.DrawString(" Mxxx + revision + (-S,-C)", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 1))
                    objGraphics.DrawString(" 基板型式の末尾のｱﾙﾌｧﾍﾞｯﾄは基板のﾘﾋﾞｼﾞｮﾝです。", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 2))
                    objGraphics.DrawString(" ", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 3))
                    objGraphics.DrawString(" 基板の改版等により異なる場合がありますが問題ありません。", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 4))
                    objGraphics.DrawString(" ", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 5))
                    objGraphics.DrawString(" FCU-M001のみﾘﾋﾞｼﾞｮﾝの後に基板種類を表す文字がある場合が", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 6))
                    objGraphics.DrawString(" あります。", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 7))

                ElseIf ExplanatoryEJ = 2 Then
                    intYhosei = 10
                    '全和文
                    objGraphics.DrawString(" 基板型式", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y)    ''和文仕様 20200217 hori
                    objGraphics.DrawString(" Mxxx + revision + (-S,-C)", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 1))
                    objGraphics.DrawString(" 基板型式の末尾のｱﾙﾌｧﾍﾞｯﾄは基板のﾘﾋﾞｼﾞｮﾝです。", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 2))
                    objGraphics.DrawString(" ", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 3))
                    objGraphics.DrawString(" 基板の改版等により異なる場合がありますが問題ありません。", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 4))
                    objGraphics.DrawString(" ", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 5))
                    objGraphics.DrawString(" FCU-M001のみﾘﾋﾞｼﾞｮﾝの後に基板種類を表す文字がある場合が", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 6))
                    objGraphics.DrawString(" あります。", gFnt8j, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 7))
                Else
                    intYhosei = 10
                    '英文 
                    objGraphics.DrawString("MODULE TYPE", gFnt8, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y)
                    objGraphics.DrawString(" Mxxx + revision + (-S,-C)", gFnt8, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 1))
                    objGraphics.DrawString(" The alphabet at the end of MODULE TYPE shows a", gFnt8, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 2))
                    objGraphics.DrawString(" revision.", gFnt8, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 3))
                    objGraphics.DrawString(" There are cases where it is different depending", gFnt8, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 4))
                    objGraphics.DrawString(" on the change of MODULE, but there is no problem.", gFnt8, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 5))
                    objGraphics.DrawString(" Characters which shows board type may be added", gFnt8, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 6))
                    objGraphics.DrawString(" only after FCU-M001 revision.", gFnt8, gFntColorBlack, gCstFrameLocalUnitLeft + unit_x, unit_y + (intYhosei * 7))
                End If
            End If
            '-


            p2.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ポートタイプより、列挙体定義値を返す
    ' 引数      ： ARG1 - (I ) ポートタイプ
    ' 戻値      ： gEnmPrintTerminalPageType の値
    '----------------------------------------------------------------------------
    Public Function gConvPortType(ByVal intPortType As Integer) As gEnmPrintTerminalPageType

        Try

            Dim udtRtn As String = ""

            Select Case intPortType

                Case gCstCodeFuSlotTypeDO, gCstCodeFuSlotTypeDI

                    udtRtn = gEnmPrintTerminalPageType.tptDigital

                Case gCstCodeFuSlotTypeAO, gCstCodeFuSlotTypeAI_2, gCstCodeFuSlotTypeAI_1_5, gCstCodeFuSlotTypeAI_4_20, gCstCodeFuSlotTypeAI_K

                    udtRtn = gEnmPrintTerminalPageType.tptAnalog2

                Case gCstCodeFuSlotTypeAI_3

                    udtRtn = gEnmPrintTerminalPageType.tptAnalog3

            End Select

            Return udtRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 2行用の文字描写関数
    ' 引数      ： ARG1 - (I ) Graphicsオブジェクト
    '           ： ARG2 - (I ) 描く文字
    '           ： ARG3 - (I ) １行あたりの最大文字数
    '           ： ARG4 - (I ) 文字を書き始める場所（X軸）
    '           ： ARG5 - (I ) 文字を書き始める場所（Y軸）
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub mDrawStringsLine2(ByVal hobjGraphics As Object, _
                                  ByVal hstrTargetStrings As String, _
                                  ByVal hintLineLength As Integer, _
                                  ByVal hintX As Integer, _
                                  ByVal hsngY As Single)

        Try
            'Ver2.0.4.9 「^」が入った場合に改行。またﾌｫﾝﾄは1行時よりも小さくする。10→8、2段目のY軸を+13から+10へ変更
            'Ver2.0.5.2 引数文字超えたら改行を復活。「^」で改行も有効。改行になってもﾌｫﾝﾄは小さくしない。
            Dim strLine As String = ""
            Dim strSplit() As String = Nothing

            If hstrTargetStrings.Length > hintLineLength Or _
                hstrTargetStrings.IndexOf("^") >= 0 Then

                If hstrTargetStrings.IndexOf("^") >= 0 Then
                    '「^」
                    strSplit = hstrTargetStrings.Split("^")
                Else
                    '文字数超
                    ReDim strSplit(1)
                    strSplit(0) = hstrTargetStrings.Substring(0, hintLineLength)
                    strSplit(1) = hstrTargetStrings.Substring(hintLineLength)
                End If

                If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                    '1行目
                    'strLine = hstrTargetStrings.Substring(0, hintLineLength)
                    strLine = strSplit(0)
                    hobjGraphics.DrawString(strLine, gFnt10j, gFntColorBlack, hintX + 10, hsngY)         '' 2013.11.20
                    '2行目
                    'strLine = hstrTargetStrings.Substring(hintLineLength)
                    strLine = strSplit(1)
                    hobjGraphics.DrawString(strLine, gFnt10j, gFntColorBlack, hintX + 10, hsngY + 13)    '' 2013.11.20
                Else
                    '1行目
                    'strLine = hstrTargetStrings.Substring(0, hintLineLength)
                    strLine = strSplit(0)
                    hobjGraphics.DrawString(strLine, gFnt10, gFntColorBlack, hintX + 10, hsngY)         '' 2013.11.20
                    '2行目
                    'strLine = hstrTargetStrings.Substring(hintLineLength)
                    strLine = strSplit(1)
                    hobjGraphics.DrawString(strLine, gFnt10, gFntColorBlack, hintX + 10, hsngY + 13)    '' 2013.11.20
                End If
            Else
                If gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then     '和文仕様 20200217 hori
                    ''1行表示
                    hobjGraphics.DrawString(hstrTargetStrings, gFnt10j, gFntColorBlack, hintX + 10, hsngY + 6)   '' 2013.11.20
                Else
                    hobjGraphics.DrawString(hstrTargetStrings, gFnt10, gFntColorBlack, hintX + 10, hsngY + 6)   '' 2013.11.20
                End If
            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub



    '----------------------------------------------------------------------------
    ' 機能説明  ： 端子表印刷　ﾃﾞｼﾞﾀﾙﾃﾞｰﾀ初期化
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 履歴      ： Ver1.9.3 2016.01.12 追加
    '----------------------------------------------------------------------------
    Public Sub gInitDigData()

        Try

            Dim wkI As Integer

            ReDim gPrtDigData(gCstChannelIdMax)

            For wkI = 0 To gCstChannelIdMax
                ReDim gPrtDigData(wkI).gDigStrSet(5)
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    '--------------------------------------------------------------------
    ' 機能      : ﾃﾞｼﾞﾀﾙ配列に取り込むかどうかをﾁｪｯｸ
    ' 返り値    : True：ﾃﾞｼﾞﾀﾙ基板ﾃﾞｰﾀ
    ' 引き数    : udtCH   
    ' 機能説明  : 
    ' 履歴    　: Ver1.9.3 2016.01.12
    '--------------------------------------------------------------------
    Public Function ChkDISetting(udtCH As gTypSetChRec) As Boolean

        Try

            Dim bRet As Boolean

            If udtCH.udtChCommon.shtChno = 0 Then   '' CHNo.が0ならば処理しない
                Return False
            End If

            Select Case (udtCH.udtChCommon.shtChType)
                Case gCstCodeChTypeAnalog   '' ｱﾅﾛｸﾞ
                    bRet = False
                Case gCstCodeChTypeDigital  '' ﾃﾞｼﾞﾀﾙ　FU入力のみOKとする
                    Select Case udtCH.udtChCommon.shtData
                        Case gCstCodeChDataTypeDigitalNC, _
                            gCstCodeChDataTypeDigitalNO, _
                            gCstCodeChDataTypeDigitalExt
                            bRet = True
                        Case Else
                            bRet = False
                    End Select
                Case gCstCodeChTypeMotor    '' ﾓｰﾀｰ
                    'Ver2.0.7.S JACOMより大きい＝通信CHは非表示
                    'If udtCH.udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom Then  '' JACOMﾃﾞｰﾀなら追加しない
                    If udtCH.udtChCommon.shtData >= gCstCodeChDataTypeMotorDeviceJacom Then
                        bRet = False
                    Else
                        bRet = True
                    End If
                Case gCstCodeChTypeValve    '' ﾊﾞﾙﾌﾞ
                    If udtCH.udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Then        '' DI/DOならば追加
                        bRet = True
                    Else
                        bRet = False
                    End If
                Case gCstCodeChTypeComposite    '' ｺﾝﾎﾟｼﾞｯﾄ
                    bRet = True
                Case gCstCodeChTypePulse    '' ﾊﾟﾙｽ
                    '' Ver1.11.8.3 2016.11.08 運転積算 通信CH追加
                    '' Ver1.12.0.1 2017.01.13 運転積算種類追加
                    If udtCH.udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Or _
                        udtCH.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDev Or udtCH.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDevTotalMin Or _
                        udtCH.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDevDayHour Or udtCH.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDevDayMin Or _
                        udtCH.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDevLapHour Or udtCH.udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDevLapMin Then  '' 通信CHなら追加しない
                        bRet = False
                    Else
                        bRet = True
                    End If
            End Select

            If bRet = True Then
                If udtCH.udtChCommon.shtFuno = gCstCodeChCommonFuNoNothing Then     '' FU設定なしならば追加しない
                    bRet = False
                End If
            End If

            Return bRet

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 端子表印刷　ﾃﾞｼﾞﾀﾙﾃﾞｰﾀ作成
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 履歴      ： Ver1.9.3 2016.01.12 追加
    '----------------------------------------------------------------------------
    Public Sub gGetDigData()
        Dim wkI As Integer
        Dim j As Integer
        Dim nIndex As Integer = 0
        Dim digData As gDigData

        Try
            digData = New gDigData

            Call gInitDigData()    '' ｸﾞﾛｰﾊﾞﾙ変数　初期化

            For wkI = 0 To gCstChannelIdMax - 1
                With gudt.SetChInfo.udtChannel(wkI).udtChCommon
                    If ChkDISetting(gudt.SetChInfo.udtChannel(wkI)) = False Then  '' DI基板使用CHならば設定
                        Continue For
                    End If

                    digData.nCHNo = .shtChno   '' CHNo.
                    digData.strItemName = .strChitem        '' CH名称

                    digData.strStatus = GetStatus(gudt.SetChInfo.udtChannel(wkI))     '' ｽﾃｰﾀｽ

                    digData.CHType = .shtChType     '' CH種別
                    digData.DataType = .shtData     '' ﾃﾞｰﾀ種別
                    digData.FCUNo = .shtFuno        '' FCUNo
                    digData.PortNo = .shtPortno      '' 基板No
                    digData.bSCFg = IIf(gBitCheck(.shtFlag1, 1), True, False)   '' 隠しCHﾌﾗｸﾞ
                    digData.bWKFg = IIf(gBitCheck(.shtFlag1, 2), True, False)   '' ﾜｰｸCHﾌﾗｸﾞ
                    digData.bDummyFg = IIf(gBitCheck(.shtFlag1, 4), True, False)   '' ﾀﾞﾐｰCHﾌﾗｸﾞ
                    If digData.bDummyFg = False Then   '' FUｱﾄﾞﾚｽのﾀﾞﾐｰをﾁｪｯｸ
                        digData.bDummyFg = gudt.SetChInfo.udtChannel(wkI).DummyCommonFuAddress
                    End If

                    ''Dim gDigStrSet() As gDigStrSet

                    For j = 0 To .shtPinNo - 1
                        digData.TermNo = .shtPin + j      '' 端子No

                        If .shtChType = gCstCodeChTypeMotor Then    '' ﾓｰﾀｰならば端子ごとにｽﾃｰﾀｽを設定
                            digData.strStatus = GetMotorStatus(.shtData, j)
                        End If

                        nIndex = nIndex + 1

                        Call DigSort(nIndex, digData)
                    Next



                End With
            Next

            If nIndex = 0 Then      '' ﾃﾞｼﾞﾀﾙﾃﾞｰﾀが存在しない場合は処理を抜ける
                Return
            End If

            'For wkI = 0 To nIndex - 1
            '    Debug.Print("CHNo:" & gPrtDigData(wkI).nCHNo.ToString & _
            '                    "  FU:" & gPrtDigData(wkI).FCUNo & "-" & gPrtDigData(wkI).PortNo & "-" & gPrtDigData(wkI).TermNo)
            'Next


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 端子表印刷　ﾓｰﾀｰ　端子ｽﾃｰﾀｽ名称取得
    ' 引数      ： ﾃﾞｰﾀ種別
    ' 戻値      ： ｽﾃｰﾀｽ文字
    ' 履歴      ： Ver1.9.3 2016.01.15 追加
    '----------------------------------------------------------------------------
    Private Function GetMotorStatus(intDataType As Integer, nTermNo As Integer) As String
        Dim strStatus As String = ""

        'Ver2.0.0.2 モーター種別増加 START
        'Ver2.0.7.M (保安庁)
        If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
            Select Case intDataType
                Case gCstCodeChDataTypeMotorManRun, gCstCodeChDataTypeMotorAbnorRun, gCstCodeChDataTypeMotorRManRun, gCstCodeChDataTypeMotorRAbnorRun '' RUN
                    If nTermNo = 0 Then
                        strStatus = "運転"
                    Else
                        strStatus = "停止"
                    End If
                Case gCstCodeChDataTypeMotorManRunA, gCstCodeChDataTypeMotorAbnorRunA, gCstCodeChDataTypeMotorRManRunA, gCstCodeChDataTypeMotorRAbnorRunA   '' RUN-A
                    If nTermNo = 0 Then
                        strStatus = "運転"
                    ElseIf nTermNo = 1 Then
                        strStatus = "停止"
                    Else
                        strStatus = "ｽﾀﾝﾊﾞｲ"
                    End If
                Case gCstCodeChDataTypeMotorManRunB, gCstCodeChDataTypeMotorAbnorRunB, gCstCodeChDataTypeMotorRManRunB, gCstCodeChDataTypeMotorRAbnorRunB   '' RUN-B
                    If nTermNo = 0 Then
                        strStatus = "低速運転"
                    ElseIf nTermNo = 1 Then
                        strStatus = "高速運転"
                    Else
                        strStatus = "停止"
                    End If
                Case gCstCodeChDataTypeMotorManRunC, gCstCodeChDataTypeMotorAbnorRunC, gCstCodeChDataTypeMotorRManRunC, gCstCodeChDataTypeMotorRAbnorRunC   '' RUN-C
                    If nTermNo = 0 Then
                        strStatus = "給気"
                    ElseIf nTermNo = 1 Then
                        strStatus = "排気"
                    Else
                        strStatus = "停止"
                    End If
                Case gCstCodeChDataTypeMotorManRunD, gCstCodeChDataTypeMotorAbnorRunD, gCstCodeChDataTypeMotorRManRunD, gCstCodeChDataTypeMotorRAbnorRunD   '' RUN-D
                    If nTermNo = 0 Then
                        strStatus = "低速給気"
                    ElseIf nTermNo = 1 Then
                        strStatus = "高速給気"
                    ElseIf nTermNo = 2 Then
                        strStatus = "低速排気"
                    ElseIf nTermNo = 3 Then
                        strStatus = "高速排気"
                    Else
                        strStatus = "停止"
                    End If
                Case gCstCodeChDataTypeMotorManRunE, gCstCodeChDataTypeMotorAbnorRunE, gCstCodeChDataTypeMotorRManRunE, gCstCodeChDataTypeMotorRAbnorRunE   '' RUN-E
                    If nTermNo = 0 Then
                        strStatus = "正転"
                    ElseIf nTermNo = 1 Then
                        strStatus = "逆転"
                    Else
                        strStatus = "停止"
                    End If
                Case gCstCodeChDataTypeMotorManRunF, gCstCodeChDataTypeMotorAbnorRunF, gCstCodeChDataTypeMotorRManRunF, gCstCodeChDataTypeMotorRAbnorRunF   '' RUN-F
                    If nTermNo = 0 Then
                        strStatus = "低速正転"
                    ElseIf nTermNo = 1 Then
                        strStatus = "高速正転"
                    ElseIf nTermNo = 2 Then
                        strStatus = "低速逆転"
                    ElseIf nTermNo = 3 Then
                        strStatus = "高速逆転"
                    Else
                        strStatus = "停止"
                    End If
                Case gCstCodeChDataTypeMotorManRunG, gCstCodeChDataTypeMotorAbnorRunG, gCstCodeChDataTypeMotorRManRunG, gCstCodeChDataTypeMotorRAbnorRunG   '' RUN-G
                    If nTermNo = 0 Then
                        strStatus = "自動"
                    Else
                        strStatus = "停止"
                    End If
                Case gCstCodeChDataTypeMotorManRunH, gCstCodeChDataTypeMotorAbnorRunH, gCstCodeChDataTypeMotorRManRunH, gCstCodeChDataTypeMotorRAbnorRunH   '' RUN-H
                    If nTermNo = 0 Then
                        strStatus = "運転"
                    ElseIf nTermNo = 1 Then
                        strStatus = "停止"
                    Else
                        strStatus = "自動"
                    End If
                Case gCstCodeChDataTypeMotorManRunI, gCstCodeChDataTypeMotorAbnorRunI, gCstCodeChDataTypeMotorRManRunI, gCstCodeChDataTypeMotorRAbnorRunI   '' RUN-I
                    If nTermNo = 0 Then
                        strStatus = "自動"
                    ElseIf nTermNo = 1 Then
                        strStatus = "停止"
                    Else
                        strStatus = "ｽﾀﾝﾊﾞｲ"
                    End If
                Case gCstCodeChDataTypeMotorManRunJ, gCstCodeChDataTypeMotorAbnorRunJ, gCstCodeChDataTypeMotorRManRunJ, gCstCodeChDataTypeMotorRAbnorRunJ   '' RUN-J
                    If nTermNo = 0 Then
                        strStatus = "低速運転"
                    ElseIf nTermNo = 1 Then
                        strStatus = "高速運転"
                    ElseIf nTermNo = 2 Then
                        strStatus = "停止"
                    Else
                        strStatus = "ｽﾀﾝﾊﾞｲ"
                    End If
                Case gCstCodeChDataTypeMotorManRunK, gCstCodeChDataTypeMotorAbnorRunK, gCstCodeChDataTypeMotorRManRunK, gCstCodeChDataTypeMotorRAbnorRunK   '' RUN-J
                    If nTermNo = 0 Then
                        strStatus = "ECO-RUN"   '' Ver1.12.0.0 2016.12.22  '-'追加
                    ElseIf nTermNo = 1 Then
                        strStatus = "BYPS-RUN"  '' Ver1.12.0.0 2016.12.22  '-'追加
                    ElseIf nTermNo = 2 Then
                        strStatus = "STOP"
                    Else
                        strStatus = "ST/BY"
                    End If
                Case Else
                    strStatus = "運転"
            End Select
        Else
            Select Case intDataType
                Case gCstCodeChDataTypeMotorManRun, gCstCodeChDataTypeMotorAbnorRun, gCstCodeChDataTypeMotorRManRun, gCstCodeChDataTypeMotorRAbnorRun '' RUN
                    If nTermNo = 0 Then
                        strStatus = "RUN"
                    Else
                        strStatus = "STOP"
                    End If
                Case gCstCodeChDataTypeMotorManRunA, gCstCodeChDataTypeMotorAbnorRunA, gCstCodeChDataTypeMotorRManRunA, gCstCodeChDataTypeMotorRAbnorRunA   '' RUN-A
                    If nTermNo = 0 Then
                        strStatus = "RUN"
                    ElseIf nTermNo = 1 Then
                        strStatus = "STOP"
                    Else
                        strStatus = "ST/BY"
                    End If
                Case gCstCodeChDataTypeMotorManRunB, gCstCodeChDataTypeMotorAbnorRunB, gCstCodeChDataTypeMotorRManRunB, gCstCodeChDataTypeMotorRAbnorRunB   '' RUN-B
                    If nTermNo = 0 Then
                        strStatus = "RUN-L"
                    ElseIf nTermNo = 1 Then
                        strStatus = "RUN-H"
                    Else
                        strStatus = "STOP"
                    End If
                Case gCstCodeChDataTypeMotorManRunC, gCstCodeChDataTypeMotorAbnorRunC, gCstCodeChDataTypeMotorRManRunC, gCstCodeChDataTypeMotorRAbnorRunC   '' RUN-C
                    If nTermNo = 0 Then
                        strStatus = "SUP"
                    ElseIf nTermNo = 1 Then
                        strStatus = "EXH"
                    Else
                        strStatus = "STOP"
                    End If
                Case gCstCodeChDataTypeMotorManRunD, gCstCodeChDataTypeMotorAbnorRunD, gCstCodeChDataTypeMotorRManRunD, gCstCodeChDataTypeMotorRAbnorRunD   '' RUN-D
                    If nTermNo = 0 Then
                        strStatus = "SUP-L"
                    ElseIf nTermNo = 1 Then
                        strStatus = "SUP-H"
                    ElseIf nTermNo = 2 Then
                        strStatus = "EXH-L"
                    ElseIf nTermNo = 3 Then
                        strStatus = "EXH-H"
                    Else
                        strStatus = "STOP"
                    End If
                Case gCstCodeChDataTypeMotorManRunE, gCstCodeChDataTypeMotorAbnorRunE, gCstCodeChDataTypeMotorRManRunE, gCstCodeChDataTypeMotorRAbnorRunE   '' RUN-E
                    If nTermNo = 0 Then
                        strStatus = "FWD"
                    ElseIf nTermNo = 1 Then
                        strStatus = "REV"
                    Else
                        strStatus = "STOP"
                    End If
                Case gCstCodeChDataTypeMotorManRunF, gCstCodeChDataTypeMotorAbnorRunF, gCstCodeChDataTypeMotorRManRunF, gCstCodeChDataTypeMotorRAbnorRunF   '' RUN-F
                    If nTermNo = 0 Then
                        strStatus = "FWD-L"
                    ElseIf nTermNo = 1 Then
                        strStatus = "FWD-H"
                    ElseIf nTermNo = 2 Then
                        strStatus = "REV-L"
                    ElseIf nTermNo = 3 Then
                        strStatus = "REV-H"
                    Else
                        strStatus = "STOP"
                    End If
                Case gCstCodeChDataTypeMotorManRunG, gCstCodeChDataTypeMotorAbnorRunG, gCstCodeChDataTypeMotorRManRunG, gCstCodeChDataTypeMotorRAbnorRunG   '' RUN-G
                    If nTermNo = 0 Then
                        strStatus = "AUTO"
                    Else
                        strStatus = "STOP"
                    End If
                Case gCstCodeChDataTypeMotorManRunH, gCstCodeChDataTypeMotorAbnorRunH, gCstCodeChDataTypeMotorRManRunH, gCstCodeChDataTypeMotorRAbnorRunH   '' RUN-H
                    If nTermNo = 0 Then
                        strStatus = "RUN"
                    ElseIf nTermNo = 1 Then
                        strStatus = "STOP"
                    Else
                        strStatus = "AUTO"
                    End If
                Case gCstCodeChDataTypeMotorManRunI, gCstCodeChDataTypeMotorAbnorRunI, gCstCodeChDataTypeMotorRManRunI, gCstCodeChDataTypeMotorRAbnorRunI   '' RUN-I
                    If nTermNo = 0 Then
                        strStatus = "AUTO"
                    ElseIf nTermNo = 1 Then
                        strStatus = "STOP"
                    Else
                        strStatus = "ST/BY"
                    End If
                Case gCstCodeChDataTypeMotorManRunJ, gCstCodeChDataTypeMotorAbnorRunJ, gCstCodeChDataTypeMotorRManRunJ, gCstCodeChDataTypeMotorRAbnorRunJ   '' RUN-J
                    If nTermNo = 0 Then
                        strStatus = "RUN-L"
                    ElseIf nTermNo = 1 Then
                        strStatus = "RUN-H"
                    ElseIf nTermNo = 2 Then
                        strStatus = "STOP"
                    Else
                        strStatus = "ST/BY"
                    End If
                Case gCstCodeChDataTypeMotorManRunK, gCstCodeChDataTypeMotorAbnorRunK, gCstCodeChDataTypeMotorRManRunK, gCstCodeChDataTypeMotorRAbnorRunK   '' RUN-J
                    If nTermNo = 0 Then
                        strStatus = "ECO-RUN"   '' Ver1.12.0.0 2016.12.22  '-'追加
                    ElseIf nTermNo = 1 Then
                        strStatus = "BYPS-RUN"  '' Ver1.12.0.0 2016.12.22  '-'追加
                    ElseIf nTermNo = 2 Then
                        strStatus = "STOP"
                    Else
                        strStatus = "ST/BY"
                    End If
                Case Else
                    strStatus = "RUN"
            End Select
        End If
        'Ver2.0.0.2 モーター種別増加 END

        '' M2ならば STOP → NORMAL に置き換え
        'Ver2.0.0.2 モーター種別増加 START
        If (intDataType >= gCstCodeChDataTypeMotorAbnorRun) And (intDataType <= gCstCodeChDataTypeMotorAbnorRunK) _
            Or _
            (intDataType >= gCstCodeChDataTypeMotorRAbnorRun) And (intDataType <= gCstCodeChDataTypeMotorRAbnorRunK) _
            Then
            'Ver2.0.0.2 モーター種別増加 END

            'Ver2.0.7.M (保安庁)
            If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                If strStatus = "停止" Then
                    strStatus = "正常" '' ver.2.0.8.A 2018.10.12 通常→正常
                End If
            Else
                If strStatus = "STOP" Then
                    strStatus = "NORMAL"
                End If
            End If

        End If

        Return strStatus

    End Function

    '----------------------------------------------------------------------------
    ' 機能説明  ： 端子表印刷　ﾃﾞｼﾞﾀﾙﾃﾞｰﾀFUの順に並べかえ
    ' 引数      ： なし
    ' 戻値      ： なし
    ' 履歴      ： Ver1.9.3 2016.01.12 追加
    '----------------------------------------------------------------------------
    Private Sub DigSort(ByVal nCount As Integer, ByVal DigData As gDigData)
        Dim wkI As Integer
        Dim digTemp() As gDigData
        Dim bSetFg As Boolean = False

        Try

            If nCount = 1 Then      '' 最初のﾃﾞｰﾀならばｺﾋﾟｰする
                gPrtDigData(0) = DigData
            Else
                '' 配列初期化
                ReDim digTemp(gCstChannelIdMax)
                For wkI = 0 To gCstChannelIdMax
                    ReDim digTemp(wkI).gDigStrSet(5)
                Next

                digTemp = gPrtDigData   '' 配列を保存

                For wkI = 0 To nCount - 2
                    If gPrtDigData(wkI).FCUNo < DigData.FCUNo Then  '' FU番号が自分の方が大きいので次へ
                        Continue For
                    ElseIf gPrtDigData(wkI).FCUNo > DigData.FCUNo Then      '' FU番号が自分の方が小さいので割り込む
                        Array.Copy(digTemp, wkI, gPrtDigData, wkI + 1, nCount - wkI - 1)      '' 残りをｺﾋﾟｰ
                        gPrtDigData(wkI) = DigData
                        bSetFg = True
                        Exit For
                    Else        '' FU番号が同じ
                        If gPrtDigData(wkI).PortNo < DigData.PortNo Then    '' 基板番号が自分の方が大きいので次へ
                            Continue For
                        ElseIf gPrtDigData(wkI).PortNo > DigData.PortNo Then      '' 基板番号が自分の方が小さいので割り込む
                            Array.Copy(digTemp, wkI, gPrtDigData, wkI + 1, nCount - wkI - 1)      '' 残りをｺﾋﾟｰ
                            gPrtDigData(wkI) = DigData
                            bSetFg = True
                            Exit For
                        Else    '' 基板番号が同じ
                            If gPrtDigData(wkI).TermNo < DigData.TermNo Then    '' 端子番号が自分の方が大きいので次へ
                                Continue For
                            ElseIf gPrtDigData(wkI).TermNo > DigData.TermNo Then      '' 端子番号が自分の方が小さいので割り込む
                                Array.Copy(digTemp, wkI, gPrtDigData, wkI + 1, nCount - wkI - 1)      '' 残りをｺﾋﾟｰ
                                gPrtDigData(wkI) = DigData
                                bSetFg = True
                                Exit For
                            Else    '' 端子番号が同じ
                                If (gPrtDigData(wkI).CHType = gCstCodeChTypePulse And _
                                    ((gPrtDigData(wkI).DataType >= gCstCodeChDataTypePulseRevoTotalHour) <= _
                                     (gPrtDigData(wkI).DataType <= gCstCodeChDataTypePulseRevoLapMin))) Or _
                                    gPrtDigData(wkI).nCHNo > DigData.nCHNo Then    '' 運転積算 またはCHNo.が小さい場合は割り込む
                                    Array.Copy(digTemp, wkI, gPrtDigData, wkI + 1, nCount - wkI - 1)      '' 残りをｺﾋﾟｰ
                                    gPrtDigData(wkI) = DigData
                                    bSetFg = True
                                    Exit For
                                End If
                            End If

                        End If
                    End If
                Next

                If bSetFg = False Then      '' 最終ﾃﾞｰﾀとして追加
                    gPrtDigData(wkI) = DigData
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : CH詳細 ｽﾃｰﾀｽ取得処理　(ｱﾅﾛｸﾞ以外)
    ' 返り値    : ｽﾃｰﾀｽ文字列
    ' 引き数    : udtCH   
    ' 機能説明  : 
    ' 履歴    　: Ver1.9.3 2016.01.12 Publicに移動
    '--------------------------------------------------------------------
    Public Function GetStatus(udtCH As gTypSetChRec) As String
        Dim strStatus As String

        If udtCH.udtChCommon.shtStatus = &HFF Then
            strStatus = SetStatusData(udtCH.udtChCommon.strStatus)
        Else
            'Ver2.0.7.M (保安庁)
            If g_bytHOAN = 1 Or gudt.SetSystem.udtSysSystem.shtLanguage = 2 Then '全和文仕様 hori
                Select Case udtCH.udtChCommon.shtStatus
                    Case &H0
                        strStatus = "*"
                    Case &H1
                        strStatus = "入/切"
                    Case &H2
                        strStatus = "遮断/開"
                    Case &H3
                        strStatus = "閉/開"
                    Case &H4
                        strStatus = "正常/異常"
                    Case &H5
                        strStatus = "正常/故障"
                    Case &H6
                        strStatus = "正常/解除"
                    Case &H8
                        strStatus = "正常/高"
                    Case &H9
                        strStatus = "正常/低"
                    Case &HA
                        strStatus = "正常/低低"
                    Case &HB
                        strStatus = "正常/高高"
                    Case &HC
                        strStatus = "正常/閉"
                    Case &HD
                        strStatus = "正常/喪失"
                    Case &HE
                        strStatus = "USE/NOT USE"
                    Case &HF
                        strStatus = "DO/HFO"
                    Case &H10
                        strStatus = "DISEN/ENGA"
                    Case &H11
                        strStatus = "実行/停止"
                    Case &H12
                        strStatus = "始動/停止"
                    Case &H13
                        strStatus = "開/閉"
                    Case &H14
                        strStatus = "運転/停止"
                    Case &HA0
                        strStatus = "入/*"
                    Case &HA1
                        strStatus = "開/*"
                    Case &HA2
                        strStatus = "遮断/*"
                    Case &HA3
                        strStatus = "喪失/*"
                    Case &HA4
                        strStatus = "正常/*"
                    Case &HA5
                        strStatus = "異常/*"
                    Case &HA6
                        strStatus = "FAIL/*"
                    Case &HA7
                        strStatus = "USE/*"
                    Case &HA8
                        strStatus = "自動/*"
                    Case &HA9
                        strStatus = "当直/*"
                    Case &HAA
                        strStatus = "解除/*"
                    Case &HAB
                        strStatus = "準備/*"
                    Case &HAC
                        strStatus = "COND/*"
                    Case &HAD
                        strStatus = "消去/*"
                    Case &HAE
                        strStatus = "運転/*"
                    Case &HAF
                        strStatus = "BLOCK/SERV"
                    Case &HB0
                        strStatus = "*/低"
                    Case &HB1
                        strStatus = "*/高"
                    Case &HB2
                        strStatus = "PASS/*"
                    Case &HB3
                        strStatus = "OK/*"
                    Case &HB4
                        strStatus = "停止/*"
                    Case &HB5
                        strStatus = "閉/*"

                    Case &H40
                        strStatus = "*"
                    Case &H41
                        strStatus = "正常/高"

                    Case Else
                        strStatus = ""
                End Select
            Else
                Select Case udtCH.udtChCommon.shtStatus
                    Case &H0
                        strStatus = "*"
                    Case &H1
                        strStatus = "ON/OFF"
                    Case &H2
                        strStatus = "SHUT/OPEN"
                    Case &H3
                        strStatus = "CLOSE/OPEN"
                    Case &H4
                        strStatus = "NOR/ABNOR"
                    Case &H5
                        strStatus = "NOR/TRBL"
                    Case &H6
                        strStatus = "NOR/CANCL"
                    Case &H8
                        strStatus = "NOR/HIGH"
                    Case &H9
                        strStatus = "NOR/LOW"
                    Case &HA
                        strStatus = "NOR/LL"
                    Case &HB
                        strStatus = "NOR/HH"
                    Case &HC
                        strStatus = "NOR/CLOSE"
                    Case &HD
                        strStatus = "NOR/TRIP"
                    Case &HE
                        strStatus = "USE/NOT USE"
                    Case &HF
                        strStatus = "DO/HFO"
                    Case &H10
                        strStatus = "DISEN/ENGA"
                    Case &H11
                        strStatus = "ACT/STOP"
                    Case &H12
                        strStatus = "START/STOP"
                    Case &H13
                        strStatus = "OPEN/CLOSE"
                    Case &H14
                        strStatus = "RUN/STOP"
                    Case &HA0
                        strStatus = "ON/*"
                    Case &HA1
                        strStatus = "OPEN/*"
                    Case &HA2
                        strStatus = "SHUT/*"
                    Case &HA3
                        strStatus = "TRIP/*"
                    Case &HA4
                        strStatus = "NOR/*"
                    Case &HA5
                        strStatus = "ABNOR/*"
                    Case &HA6
                        strStatus = "FAIL/*"
                    Case &HA7
                        strStatus = "USE/*"
                    Case &HA8
                        strStatus = "AUTO/*"
                    Case &HA9
                        strStatus = "DUTY/*"
                    Case &HAA
                        strStatus = "CANCL/*"
                    Case &HAB
                        strStatus = "READY/*"
                    Case &HAC
                        strStatus = "COND/*"
                    Case &HAD
                        strStatus = "CLEAR/*"
                    Case &HAE
                        strStatus = "RUN/*"
                    Case &HAF
                        strStatus = "BLOCK/SERV"
                    Case &HB0
                        strStatus = "*/LOW"
                    Case &HB1
                        strStatus = "*/HIGH"
                    Case &HB2
                        strStatus = "PASS/*"
                    Case &HB3
                        strStatus = "OK/*"
                    Case &HB4
                        strStatus = "STOP/*"
                    Case &HB5
                        strStatus = "CLOSE/*"

                    Case &H40
                        strStatus = "*"
                    Case &H41
                        strStatus = "NOR/HIGH"

                    Case Else
                        strStatus = ""
                End Select
            End If
        End If

        Return strStatus

    End Function

    '--------------------------------------------------------------------
    ' 機能      : CH詳細設定ﾁｪｸ処理
    ' 返り値    : /を付加した後のｽﾃｰﾀｽ文字列
    ' 引き数    : strStatus  ｽﾃｰﾀｽ
    ' 機能説明  : 
    ' 履歴    　: Ver1.9.3 2016.01.12 Publicに移動
    '--------------------------------------------------------------------
    Public Function SetStatusData(strStatus As String) As String

        Dim strStatus1 As String
        Dim strStatus2 As String
        Dim strSetStatus As String
        Dim nLen As Integer

        nLen = LenB(strStatus)
        'Ver2.0.7.L
        'strStatus1 = strStatus.Substring(0, 8)
        strStatus1 = MidB(strStatus, 0, 8)
        'strStatus2 = strStatus.Substring(8, nLen - 8)
        strStatus2 = MidB(strStatus, 8, nLen - 8)

        strSetStatus = Trim(strStatus1) & "/" & Trim(strStatus2)

        Return strSetStatus

    End Function

#End Region

End Module