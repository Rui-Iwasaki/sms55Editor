﻿Imports System.Runtime.InteropServices
Imports System.Threading
Public Class frmCmpCompier

#Region "GetMimCH DLL Call"
    <System.Runtime.InteropServices.DllImport("GetMimCH", CharSet:=Runtime.InteropServices.CharSet.Ansi)> _
    Shared Function mainProc(pstrPath As String, pintCHNo As Integer) As IntPtr
    End Function
#End Region


#Region "定数定義"

    Private Const mCstButtonTextCmpStart As String = "Compile Start"
    Private Const mCstButtonTextCmpCancel As String = "Compile Cancel"
    Private Const mCstButtonTextErrStart As String = "Error Check Start"
    Private Const mCstButtonTextErrCancel As String = "Error Check Cancel"
    Private Const mCstButtonTextErrMPStart As String = "Error Check Start"
    Private Const mCstButtonTextErrMPCancel As String = "Error Check Cancel"

#End Region

#Region "変数定義"

    Private mintRtn As Integer
    Private mudtFileInfo As gTypFileInfo
    Private mudtCompileType As gEnmCompileType
    Private mblnCancelFlg As Boolean
    Private mblnChkBlank As Boolean

    Private mintErrCnt As Integer
    Private mstrErrMsg() As String

    'Ver2.0.3.1 Warning
    Private mintWarCnt As Integer
    Private mstrWarMsg() As String

    Private mintDummyCnt As Integer
    Private mstrDummyMsg() As String

    Private mstrPrintingText As String
    Private mstrPrintingPosition As Integer
    Private mstrPrintFont As Font

    Private mblnEnglish As Boolean

    Private mblFireAlmMimic As Boolean

    'Ver2.0.3.1 システムチェック高速化
    Private prListSysCH As ArrayList
    Private prListAllCH As ArrayList
#Region "保存用出力構造体"

    Private mudtSetSystem As gTypSetSystem                                  ''システム設定
    Private mudtSetFuChannel As gTypSetFu                                   ''FU設定
    Private mudtSetChannelDisp As gTypSetChDisp                             ''チャンネル情報データ（表示名設定データ）
    Private mudtSetChannel As gTypSetChInfo                                 ''チャンネル情報
    Private mudtSetComposite As gTypSetChComposite                          ''コンポジット設定
    Private mudtSetChOutput As gTypSetChOutput                              ''出力チャンネル設定
    Private mudtSetChAndOr As gTypSetChAndOr                                ''論理出力設定
    Private mudtSetGroupM As gTypSetChGroupSet                              ''グループ設定
    Private mudtSetGroupC As gTypSetChGroupSet                              ''グループ設定
    Private mudtSetRepose As gTypSetChGroupRepose                           ''リポーズ入力設定
    Private mudtSetChRunHour As gTypSetChRunHour                            ''積算データ設定ファイル
    Private mudtSetExhGus As gTypSetChExhGus                                ''排ガス演算処理設定
    Private mudtSetCtrlUseNotuseM As gTypSetChCtrlUse                       ''コントロール使用可／不可設定
    Private mudtSetCtrlUseNotuseC As gTypSetChCtrlUse                       ''コントロール使用可／不可設定
    Private mudtSetSIO As gTypSetChSio                                      ''SIO設定
    Private mudtSetSIOCh() As gTypSetChSioCh                                ''SIO通信チャンネル設定
    Private mudtSetSIOExt() As gTypSetChSioExt                              'SIO通信拡張設定
    Private mudtSetChDataSaveTable As gTypSetChDataSave                     ''データ保存テーブル
    Private mudtSetChDataForwardTableSet As gTypSetChDataForward            ''データ転送テーブル設定
    Private mudtSetExtAlarm As gTypSetExtAlarm                              ''延長警報設定
    Private mudtSetTimer As gTypSetExtTimerSet                              ''タイマ設定
    Private mudtSetTimerName As gTypSetExtTimerName                         ''タイマ表示名称設定
    Private mudtSetSeqSequenceID As gTypSetSeqID                            ''シーケンスID
    Private mudtSetSeqSequenceSet As gTypSetSeqSet                          ''シーケンス設定
    Private mudtSetSeqLinear As gTypSetSeqLinear                            ''リニアライズテーブル
    Private mudtSetSeqOpeExp As gTypSetSeqOperationExpression               ''演算式テーブル
    Private mudtSetOpsScreenTitleM As gTypSetOpsScreenTitle                 ''OPS画面タイトル
    Private mudtSetOpsScreenTitleC As gTypSetOpsScreenTitle                 ''OPS画面タイトル
    Private mudtSetOpsPulldownMenuM As gTypSetOpsPulldownMenu               ''プルダウンメニュー
    Private mudtSetOpsPulldownMenuC As gTypSetOpsPulldownMenu               ''プルダウンメニュー
    Private mudtSetOpsGraphM As gTypSetOpsGraph                             ''OPSグラフ設定
    Private mudtSetOpsGraphC As gTypSetOpsGraph                             ''OPSグラフ設定
    Private mudtSetOpsLogFormatM As gTypSetOpsLogFormat                     ''ログフォーマット
    Private mudtSetOpsLogFormatC As gTypSetOpsLogFormat                     ''ログフォーマット
    Private mudtSetOpsLogIdDataM As gTypSetOpsLogIdData                     ''ログフォーマットCHID ☆2012/10/26 K.Tanigawa
    Private mudtSetOpsLogIdDataC As gTypSetOpsLogIdData                     ''ログフォーマットCHID ☆2012/10/26 K.Tanigawa
    Private mudtSetOpsGwsCh As gTypSetOpsGwsCh                              ''GWS送信CH 2014.02.04

    Private mudtSetOpsTrendGraphPID As gTypSetOpsTrendGraph                 'PID専用ﾄﾚﾝﾄﾞ

#End Region

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 0:キャンセル、1:実行、-1:失敗
    ' 引き数    : ARG1 - (I ) ファイル情報構造体
    ' 機能説明  : 画面表示を行い戻り値を返す
    '--------------------------------------------------------------------
    Friend Function gShow(ByVal udtFileInfo As gTypFileInfo, _
                          ByVal udtCompileType As gEnmCompileType) As Integer

        Try

            ''戻り値初期化
            mintRtn = 0

            ''引数保存
            mudtFileInfo = udtFileInfo
            mudtCompileType = udtCompileType

            ''画面表示
            Call Me.ShowDialog()

            ''戻り値設定
            udtFileInfo = mudtFileInfo
            Return mintRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmCmpCompier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''ヘッダーメッセージ出力
            Call mDispHeaderMessage()

            Select Case mudtCompileType
                Case gEnmCompileType.cpCompile
                    cmdCompile.Text = mCstButtonTextCmpStart
                Case gEnmCompileType.cpErrorCheck
                    cmdCompile.Text = mCstButtonTextErrStart
                    optDefaultCopy.Enabled = False
                    optDefaultNotCopy.Enabled = False
                Case gEnmCompileType.cpMeasuringCheck
                    '' 2019.03.12 計測点コンパイル追加
                    cmdCompile.Text = mCstButtonTextErrMPStart
                    optDefaultCopy.Enabled = False
                    optDefaultNotCopy.Enabled = False
            End Select

            ''文字フラグ
            mblnEnglish = optEnglish.Checked

            ''行間チェック
            mblnChkBlank = optChkBlank.Checked

            'Ver2.0.6.5 logging1.cnf,logging2.cnfが既に存在している場合は、DefaultDataをNotCopyへ
            With mudtFileInfo
                Dim strPathBase As String = ""
                Dim strLoging1 As String = ""
                Dim strLoging2 As String = ""
                strPathBase = System.IO.Path.Combine(.strFilePath, .strFileName)
                strPathBase = strPathBase & "\Temp"
                strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameCompile)
                strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameLog)
                '
                strLoging1 = System.IO.Path.Combine(strPathBase, gCstFileOtherLogTimeM)
                strLoging2 = System.IO.Path.Combine(strPathBase, gCstFileOtherLogTimeC)
                '
                If (System.IO.File.Exists(strLoging1) = True) And (System.IO.File.Exists(strLoging2) = True) Then
                    'NotCopyへ
                    optDefaultNotCopy.Checked = True
                End If
            End With


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Englishクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optEnglish_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optEnglish.CheckedChanged, optDefaultCopy.CheckedChanged, optChkBlank.CheckedChanged

        Try

            ''文字フラグ
            mblnEnglish = optEnglish.Checked
            mblnChkBlank = optChkBlank.Checked

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Japaneseクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optJapanese_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optJapanese.CheckedChanged, optDefaultNotCopy.CheckedChanged

        Try

            ''文字フラグ
            mblnEnglish = optEnglish.Checked
            mblnChkBlank = optChkBlank.Checked

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： コンパイルボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdCompile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCompile.Click

        Try

            Dim strWaitMsg1 As String = ""
            Dim strWaitMsg2 As String = ""
            Dim udtMsgResult As DialogResult
            'Dim udtMimicMsgResult As DialogResult

            If cmdCompile.Text = mCstButtonTextCmpCancel _
            Or cmdCompile.Text = mCstButtonTextErrCancel Then
                mblnCancelFlg = True
                Return
            End If

            'Ver2.0.7.T
            '未セーブは強制ｾｰﾌﾞさす
            If fnPrintBfSave() = False Then
                Exit Sub
            End If
            '-

            Select Case mudtCompileType
                Case gEnmCompileType.cpCompile

                    If mblnEnglish Then
                        udtMsgResult = MessageBox.Show("Do you start compiling?", "Compiler", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        strWaitMsg1 = "Now Compiling"
                        strWaitMsg2 = "Please wait..."
                    Else
                        udtMsgResult = MessageBox.Show("コンパイルを開始します。よろしいですか？", "コンパイラ", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        strWaitMsg1 = "コンパイル中です。"
                        strWaitMsg2 = "しばらくお待ち下さい..."
                    End If

                Case gEnmCompileType.cpErrorCheck

                    If mblnEnglish Then
                        udtMsgResult = MessageBox.Show("Do you start error check?", "Compiler", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        strWaitMsg1 = "Now Error Checking"
                        strWaitMsg2 = "Please wait..."
                    Else
                        udtMsgResult = MessageBox.Show("エラーチェックを開始します。よろしいですか？", "コンパイラ", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        strWaitMsg1 = "エラーチェック中です。"
                        strWaitMsg2 = "しばらくお待ち下さい..."
                    End If

                Case gEnmCompileType.cpMeasuringCheck
                    '' 2019.03.12 追加
                    If mblnEnglish Then
                        udtMsgResult = MessageBox.Show("Do you start error check(measuring point only)?", "Compiler", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        strWaitMsg1 = "Now Error Checking"
                        strWaitMsg2 = "Please wait..."
                    Else
                        udtMsgResult = MessageBox.Show("エラーチェック(計測点のみ)を開始します。よろしいですか？", "コンパイラ", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        strWaitMsg1 = "エラーチェック中です。"
                        strWaitMsg2 = "しばらくお待ち下さい..."
                    End If

            End Select

            If udtMsgResult = Windows.Forms.DialogResult.Yes Then

                ''画面設定
                Call mSetDisplayEnable(False, strWaitMsg1, strWaitMsg2)

                ''テキストクリア
                txtMsg.Text = ""

                ''エラー情報初期化
                mintErrCnt = 0
                Erase mstrErrMsg

                'Ver2.0.3.1
                'ワーニング情報初期化
                mintWarCnt = 0
                Erase mstrWarMsg

                ''仮設定情報初期化
                mintDummyCnt = 0
                Erase mstrDummyMsg

                ''ヘッダーメッセージ出力
                Call mDispHeaderMessage()

                ''開始 ''2019.03.12 場合分け追加
                Select Case mudtCompileType
                    Case gEnmCompileType.cpCompile : Call mAddMsgText("Compile start", "コンパイルを開始します。")
                    Case gEnmCompileType.cpErrorCheck : Call mAddMsgText("Error check start", "エラーチェックを開始します。")
                    Case gEnmCompileType.cpMeasuringCheck : Call mAddMsgText("Error check start", "エラーチェックを開始します。")
                End Select
                Call mAddMsgText("", "")

                '' Ver1.9.2 2015.12.22 ｺﾒﾝﾄ
                ''Call SetDataProc()     ' 2015.10.24 Ver1.7.5  ｺﾝﾊﾟｲﾙ前にﾃﾞｰﾀを設定する


                ''初期化ファイル存在確認
                Call mAddMsgText("[Check IniFile Exist]", "[初期化ファイル存在確認]")
                Call mChkIniFileExist()
                If mblnCancelFlg Then Return

                ''コンバイン設定確認
                Call mAddMsgText("[Check Combine Setting]", "[コンバイン設定確認]")
                Call mChkCombine()
                If mblnCancelFlg Then Return

                ''チャンネル情報チェック
                Call mChkChannelInfo()
                If mblnCancelFlg Then Return

                ''ターミナル情報チェック
                Call mChkTerminalInfo()
                If mblnCancelFlg Then Return

                '' 2019.03.12 計測点チェックのみの場合は飛ばす
                If mudtCompileType <> gEnmCompileType.cpMeasuringCheck Then
                    ''その他チャンネル情報チェック
                    Call mChkOtherChannelInfo()
                    If mblnCancelFlg Then Return

                    ''シーケンス設定チェック
                    Call mChkSequenceInfo()
                    If mblnCancelFlg Then Return

                    ''OPS設定チェック
                    Call mChkOPSInfo()
                    If mblnCancelFlg Then Return

                    ''仮設定チェック
                    Call mChkDummyInfo()
                    If mblnCancelFlg Then Return

                    'Ver2.0.7.E メニューのHC印刷設定と、プリンタの設定の矛盾チェック
                    Call mChkPrinterSetting()
                    If mblnCancelFlg Then Return

                    'Ver2.0.8.2 ログタイムセッティングと、プリンタ設定等の矛盾チェック
                    Call mChkLogTimePrinterSetting()
                    If mblnCancelFlg Then Return

                    'Ver2.0.8.4 デマンドCSV保存があった場合のチェック
                    Call mChkDemandCSVSetting()
                    If mblnCancelFlg Then Return

                End If
                '' 2019.03.12 計測点チェックのみの場合は飛ばす(END)

                ''ここからはキャンセル禁止
                cmdCompile.Enabled = False

                '' 2019.03.12 計測点チェックのみの場合は飛ばす
                If mudtCompileType <> gEnmCompileType.cpMeasuringCheck Then

                    'Ver2.0.0.7
                    'デジタルCHのDataType=Device Statusの場合の詳細ﾌﾗｸﾞ自動設定
                    Call subSetDeviceStatusFLG()


                    'Ver2.0.0.7
                    'システム設定チェック
                    Call mChkChSystem()

                    '■外販
                    '外販の場合、延長警報パネル設定の一部ﾃﾞｰﾀに自動設定
                    If gintNaiGai = 1 Then
                        Call subSetExtPnlAuto()
                        gudt.SetEditorUpdateInfo.udtSave.bytExtAlarm = 1
                    End If


                    '■一旦ｺﾒﾝﾄ
                    ''Ver2.0.0.2 南日本M761対応 2017.02.27追加
                    ''Fire Alarm Mimic のチェック
                    'Call mAddMsgText("[Check Alarm Mimic Setting]", "[火災警報設定]")
                    'mblFireAlmMimic = False
                    'Call mChkFireAlmMimic()
                    'Call mAddMsgText("", "")
                    'If mblnCancelFlg Then Return



                    '■Share対応       '' Ver1.11.8.4 2016.11.09 一旦ｺﾒﾝﾄ
                    ''Call subShareAITE()
                    ''If mblnCancelFlg Then
                    ''    '処理途中終了となる
                    ''    Call mSetDisplayEnable(True)
                    ''    Call SaveErrLog()
                    ''    cmdCompile.Enabled = True
                    ''    Return
                    ''End If

                    '■ｽﾃｰﾀｽ名の末尾0x00対応 Ver1.11.9.0 2016.11.21
                    Call subPatchData()




                    ''チャンネル情報データ（表示名称設定データ）のCHID(CHNO)を設定
                    Call mAddMsgText("[CH NO Re-setting]", "[チャンネルNO再設定]")
                    Call mSetChDispChId()

                    ''ID変更前に現在の構造体を保存
                    Call mAddMsgText("[Save Structure Info]", "[構造体情報保存]")
                    Call mSaveStructure()
                    If mblnCancelFlg Then Return


                    'ID振り直し前にIDを取得　T.Ueki
                    ''チャンネルID、チャンネルNo.、システムNo変換  
                    Call mSetChannelInfo(1)
                    If mblnCancelFlg Then Return

                    'ID変換場所
                    ''チャンネルID振りなおし
                    Call mAddMsgText("[CH ID Renumber]", "[チャンネルID再付番]")
                    Call mSetChidRenumber()
                    If mblnCancelFlg Then Return

                    'チャンネルID、チャンネルNo.、システムNo変換  
                    Call mAddMsgText("[Set CH ID and SYSTEM NO]", "[チャンネルID、システムNo設定]")
                    Call mSetChannelInfo(0)          ''　☆ 2012/10/26 K.Tanigawa Log CHID 変換を追加
                    If mblnCancelFlg Then Return

                    'ID比較
                    Call mSetChannelInfoCompair()

                    'Ver2.0.4.9
                    'ShipNoに入っている「^」を削除
                    gudt.SetChGroupSetM.udtGroup.strShipNo = gudt.SetChGroupSetM.udtGroup.strShipNo.Replace("^", "")
                    gudt.SetChGroupSetC.udtGroup.strShipNo = gudt.SetChGroupSetC.udtGroup.strShipNo.Replace("^", "")


                    ''コンパイルファイル出力
                    If mudtCompileType = gEnmCompileType.cpCompile Then

                        Call mAddMsgText("[Complie File Output]", "[コンパイルファイル出力]")
                        Call mOutputCompileFile()

                        ''バージョンアップだった場合はバージョンアップフラグを落とす
                        ''gudtFileInfo.blnVersionUP = False     2013.11.29 コメント
                        ''gudtFileInfo.strFileVersionPrev = 0   2013.12.18

                        ''更新フラグ設定　ver.1.4.0 2011.09.29
                        ''CH変換テーブルはコンパイル時に設定されるのでコンパイル後に保存必要
                        gblnUpdateAll = True
                        'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1    '2015.10.23 ｺﾒﾝﾄ
                        gudt.SetEditorUpdateInfo.udtSave.bytChConvNow = 1
                        gudt.SetEditorUpdateInfo.udtSave.bytChConvPrev = 1

                        'Ver2.0.2.0
                        gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1

                    End If
                    If mblnCancelFlg Then Return

                    ''保存した構造体を元に戻す
                    Call mLoadStructure()
                    If mblnCancelFlg Then Return

                End If
                '' 2019.03.12 計測点チェックのみの場合は飛ばす(END)

                Select Case mudtCompileType
                    Case gEnmCompileType.cpCompile
                        Call mAddMsgText("Finished compiling.", "コンパイルが終了しました。")
                        Call mAddMsgText("Error count : " & mintErrCnt, "エラー数 : " & mintErrCnt)
                        Call mAddMsgText("Dummy count : " & mintDummyCnt, "仮設定数 : " & mintDummyCnt)
                        Call mAddMsgText("", "")
                    Case gEnmCompileType.cpErrorCheck
                        Call mAddMsgText("Finished checking.", "エラーチェックが終了しました。")
                        Call mAddMsgText("Error count : " & mintErrCnt, "エラー数 : " & mintErrCnt)
                        Call mAddMsgText("Dummy count : " & mintDummyCnt, "仮設定数 : " & mintDummyCnt)
                        Call mAddMsgText("", "")
                    Case gEnmCompileType.cpMeasuringCheck
                        ''2019.03.12 追加
                        Call mAddMsgText("Finished checking.", "エラーチェックが終了しました。")
                        Call mAddMsgText("Error count : " & mintErrCnt, "エラー数 : " & mintErrCnt)
                        Call mAddMsgText("Dummy count : " & mintDummyCnt, "仮設定数 : " & mintDummyCnt)
                        Call mAddMsgText("", "")
                End Select

                '' 2019.03.12 計測点チェックのみの場合は飛ばす
                If mudtCompileType <> gEnmCompileType.cpMeasuringCheck Then

                    'Ver2.0.5.9 Mimicﾌｧｲﾙが存在しない場合は、ミミックｺﾝﾊﾟｲﾗ起動させない
                    Dim blMimic As Boolean = False
                    Dim strPathBaseMimic As String = ""
                    strPathBaseMimic = System.IO.Path.Combine(mudtFileInfo.strFilePath, mudtFileInfo.strFileName)
                    strPathBaseMimic = System.IO.Path.Combine(strPathBaseMimic, gCstFolderNameSave)
                    strPathBaseMimic = System.IO.Path.Combine(strPathBaseMimic, gCstFolderNameMimic)
                    strPathBaseMimic = System.IO.Path.Combine(strPathBaseMimic, "Mimic1")
                    If System.IO.Directory.Exists(strPathBaseMimic) = True Then
                        Dim strMimicFiles As String() = System.IO.Directory.GetFiles(strPathBaseMimic, "*.mim", System.IO.SearchOption.AllDirectories)
                        If strMimicFiles.Length > 0 Then
                            blMimic = True
                        End If
                    End If

                    If blMimic = True Then
                        'Ver2.0.7.T
                        'カラーパレットファイル(T151.com)を標準か新デザインか判断し
                        'Mimicのフォルダへ上書き格納する。
                        'コピー元は、INIﾌｫﾙﾀﾞへ格納しておくこと。
                        Dim strMotoPath As String = ""
                        Dim strSakiPath As String = ""
                        '>>>コピー元パス作成
                        strMotoPath = gGetAppPath()
                        strMotoPath = strMotoPath & "\iniFile"
                        If g_bytNEWDES = 0 Then
                            '標準
                            strMotoPath = strMotoPath & "\T151\BASE\T151.com"
                        Else
                            '新デザイン
                            strMotoPath = strMotoPath & "\T151\NEW\T151.com"
                        End If
                        '>>>コピー先パス作成
                        strSakiPath = System.IO.Path.Combine(mudtFileInfo.strFilePath, mudtFileInfo.strFileName)
                        strSakiPath = System.IO.Path.Combine(strSakiPath, gCstFolderNameSave)
                        strSakiPath = System.IO.Path.Combine(strSakiPath, gCstFolderNameMimic)
                        strSakiPath = strSakiPath & "\T151.com"
                        'コピー元が存在しないならコピー処理しない
                        If System.IO.File.Exists(strMotoPath) = True Then
                            System.IO.File.Copy(strMotoPath, strSakiPath, True)
                        End If

                        'Ver2.0.2.0 ミミックコンパイラ強制起動へ変更
                        ''ミミックコンパイラー起動確認
                        'If mblnEnglish Then
                        '    udtMimicMsgResult = MessageBox.Show("Do you start Mimic compiling?", "Mimic Compiler", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        'Else
                        '    udtMimicMsgResult = MessageBox.Show("ミミックのコンパイルを開始します。よろしいですか？", "コンパイラ", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        'End If

                        'If udtMimicMsgResult = Windows.Forms.DialogResult.Yes Then
                        '    Dim Ret As Long
                        '    Ret = Shell(AppPass + "\CompileMimic.exe", vbNormalFocus)
                        'End If

                        'Ver2.0.3.6 ｺﾝﾊﾟｲﾗは終了まで待つように変更
                        'Dim Ret As Long
                        'Ret = Shell(AppPass + "\CompileMimic.exe", vbNormalFocus)
                        Dim p As System.Diagnostics.Process = System.Diagnostics.Process.Start(AppPass + "\CompileMimic.exe")
                        p.WaitForExit()

                        'T311.com 負荷曲線 hori 20200317
                        Dim strSourcePath As String = ""
                        Dim strTargetPath As String = ""
                        '>>>コピー元パス作成
                        strSourcePath = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion)   '選択中のファイル情報
                        strSourcePath = System.IO.Path.Combine(strSourcePath, gCstFolderNameSave)                       'Saveパス
                        strSourcePath = System.IO.Path.Combine(strSourcePath, gCstFolderNameMimic)                      'mimicパス
                        strSourcePath = System.IO.Path.Combine(strSourcePath, "Mimic1\")                                'Mimic1パス
                        strSourcePath = strSourcePath & "T311.com"

                        '>>>コピー先パス作成
                        strTargetPath = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion)   '選択中のファイル情報
                        strTargetPath = System.IO.Path.Combine(strTargetPath, gCstFolderNameCompile)                    'Compileパス
                        strTargetPath = System.IO.Path.Combine(strTargetPath, gCstFolderNameMimic)                      'mimicパス
                        strTargetPath = System.IO.Path.Combine(strTargetPath, "Mimic1\")                                'Mimic1パス
                        strTargetPath = strTargetPath & "T311.com"
                        'コピー元が存在しないならコピー処理しない
                        If System.IO.File.Exists(strSourcePath) = True Then
                            System.IO.File.Copy(strSourcePath, strTargetPath, True)
                        End If

                        'Mimicｺﾝﾊﾟｲﾙ結果パス
                        Dim strFileLine() As String = Nothing
                        Dim strCompileErrLogPath As String = mudtFileInfo.strFilePath & "\" & mudtFileInfo.strFileName & "\Temp\MimicErr.log"
                        If System.IO.File.Exists(strCompileErrLogPath) = True Then
                            Call mAddMsgText("", "")
                            'ｺﾝﾊﾟｲﾙ結果ﾌｧｲﾙが存在すれば読み込んでｴﾃﾞｨﾀのﾛｸﾞへ書き出し
                            Dim sr As IO.StreamReader
                            sr = New IO.StreamReader(strCompileErrLogPath)
                            Dim strFileData As String = sr.ReadToEnd()
                            strFileLine = Split(strFileData, vbCrLf)
                            sr.Close()
                            For z As Integer = LBound(strFileLine) To UBound(strFileLine) Step 1
                                Call mAddMsgText(strFileLine(z), strFileLine(z))
                            Next z
                            'Mimicｺﾝﾊﾟｲﾗが吐き出したエラーﾛｸﾞﾌｧｲﾙは削除する
                            'System.IO.File.Delete(strCompileErrLogPath)
                        End If
                    End If

                End If
                '' 2019.03.12 計測点チェックのみの場合は飛ばす(END)

                ''画面設定
                Call mSetDisplayEnable(True)

                Call SaveErrLog()       ' 2015.10.23 Ver1.7.5 ｴﾗｰﾛｸﾞは自動保存とする

                ''コンパイルボタン使用可
                cmdCompile.Enabled = True

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'Ver2.0.7.T コンパイル前に保存して再読込処理（印刷前処理と同じ）
    Private Function fnPrintBfSave() As Boolean
        Dim bRet As Boolean = False

        '保存対象がなければ保存せずに印刷処理続行
        If gudt.SetEditorUpdateInfo.udtSave.bytChannel <> 1 Then
            Return True
        End If

        '保存ダイアログ表示
        If frmFileVersion.gShow(gEnmFileMode.fmEdit, gudtFileInfo, False) <> 0 Then
            '>>>設定値保存
            My.Settings.SelectVersion = gudtFileInfo.strFileVersion
            Call My.Settings.Save()

            'Ver2.0.5.9 全体更新フラグ初期化
            gblnUpdateAll = False

            '>>>保存したファイルを再読込
            frmFileAccess.gShow(gudtFileInfo, gEnmAccessMode.amLoad, False, False, False, gudt, False, False)

            bRet = True
        End If

        Return bRet
    End Function


#Region "■Share対応"
    '----------------------------------------------------------------------------
    ' 機能説明  ： Share対応。相手へCH保存する機能
    ' 引数      ： なし
    ' 戻値      ： なし
    '
    '----------------------------------------------------------------------------
    Private Sub subShareAITE()
        '>>>ShareUseでなければ処理抜け
        If gudt.SetSystem.udtSysFcu.shtShareChUse <> 1 Then
            Return
        End If


        '>>>計測点リストを全点みてShare(3)のCHが無いなら処理抜け
        Dim bShareCHK As Boolean = False
        For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)
            With gudt.SetChInfo.udtChannel(i).udtChCommon
                If .shtShareType = 3 Then
                    bShareCHK = True
                    Exit For
                End If
            End With
        Next i
        If bShareCHK = False Then
            Return
        End If


        Call mAddMsgText("[Share CH Start]", "[共有チャンネル設定、開始]")


        '>>>相手ファイルを指定させる
        Dim strPath As String = ""
        Dim strFile As String = ""
        If System.IO.Directory.Exists(gudtFileInfo.strFilePath) Then
            fdgFolder.SelectedPath = gudtFileInfo.strFilePath
        Else
            fdgFolder.SelectedPath = "C:\"
        End If
        'フォルダ選択ダイアログ表示
        If fdgFolder.ShowDialog = Windows.Forms.DialogResult.OK Then
            '読込成功
            Call mGetPathAndFileName(fdgFolder.SelectedPath, strPath, strFile)
        Else
            'キャンセル時は処理抜け
            Call mAddMsgText("-Share CH Cancel END", "-共有チャンネル設定、キャンセル。コンパイル終了")
            mblnCancelFlg = True
            Return
        End If


        '>>>channel.cfgファイルの存在チェック
        Dim strPathBase As String = ""
        strPathBase = System.IO.Path.Combine(strPath, strFile)
        strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameSave)
        strPathBase = System.IO.Path.Combine(strPathBase, gCstPathChannel)
        strPathBase = System.IO.Path.Combine(strPathBase, strFile & "_" & gCstFileChannel)
        If System.IO.File.Exists(strPathBase) = False Then
            'ファイルが存在しない場合は処理抜け(エラーメッセージは出すがｺﾝﾊﾟｲﾙそのものは続行)
            Call mAddMsgText("-Share CH Cancel File Nothing", "-共有チャンネル設定、チャンネル設定ファイル無し。")
            Return
        End If


        '>>>相手計測点リストをメモリ上に読み込み
        Dim aiteCHList As gTypSetChInfo = Nothing
        If mLoadChannel(aiteCHList, strPathBase) = -1 Then
            '読込失敗の場合は処理抜け(エラーメッセージは出すがｺﾝﾊﾟｲﾙそのものは続行)
            Call mAddMsgText("-Share CH Cancel File Read Error", "-共有チャンネル設定、チャンネル設定ファイル読込失敗。")
            Return
        End If

        '>>>自計測点リストループ（該当レコード探し）
        Dim intCHNo As Integer = 0
        Dim bAri As Boolean = False
        Dim intAri As Integer = 0
        For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)
            With gudt.SetChInfo.udtChannel(i).udtChCommon
                If .shtShareType = 3 Then
                    '自レコードの指定したCHNoが相手に存在するかチェック
                    intCHNo = .shtShareChid
                    ' ゼロなら自レコードのCHNoそのもの
                    If intCHNo <= 0 Then
                        intCHNo = .shtChno
                    End If
                    bAri = False
                    intAri = 0
                    For j As Integer = 0 To UBound(aiteCHList.udtChannel) Step 1
                        '存在チェック
                        If intCHNo = aiteCHList.udtChannel(j).udtChCommon.shtChno Then
                            '存在する
                            bAri = True
                            intAri = j
                            Exit For
                        End If
                    Next j
                    If bAri = True Then
                        '存在するなら該当相手レコードのShareTypeを「Loacal」へ、ShareCHNoを自CHNoに。
                        aiteCHList.udtChannel(intAri).udtChCommon.shtShareType = 1
                        aiteCHList.udtChannel(intAri).udtChCommon.shtShareChid = .shtChno
                        '★★さらに自レコードのShareTypeが「Share」になっているが、それを「Remoto」へ
                        .shtShareType = 2
                        .shtShareChid = 0
                    Else
                        '存在しないなら、次へ
                        Call mAddMsgText("-Share CH Nothing " & intCHNo.ToString & "", "-共有チャンネル設定、共有CH無し " & intCHNo.ToString & "")
                    End If
                End If
            End With
        Next i

        '>>>相手計測点リストを上書き保存
        'ファイルが存在する場合を消す
        If System.IO.File.Exists(strPathBase) Then Call My.Computer.FileSystem.DeleteFile(strPathBase)

        'ファイルへ書き込み処理
        Dim intFileNo As Integer = FreeFile()
        FileOpen(intFileNo, strPathBase, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

        Try
            ''ファイル書き込み
            FilePut(intFileNo, aiteCHList)
            Call mAddMsgText("-Share CH File Save OK " & strPathBase & "", "-共有チャンネル設定、ファイル保存成功 " & strPathBase & "")
        Catch ex As Exception
            Call mAddMsgText("-Share CH File Save NG " & strPathBase & "", "-共有チャンネル設定、ファイル保存失敗 " & strPathBase & "")
        Finally
            FileClose(intFileNo)
        End Try
        Call mAddMsgText("-Share CH SET OK", "-共有チャンネル設定、正常終了")
    End Sub
    '--------------------------------------------------------------------
    ' 機能      : ファイルパス、ファイル名取得
    ' 返り値    : 0:OK、-1:選択パス不正
    ' 引き数    : ARG1 - (I ) 選択パス
    ' 　　　    : ARG2 - ( O) ファイルパス
    ' 　　　    : ARG3 - ( O) ファイル名
    ' 機能説明  : 選択パスからファイルパスとファイル名を取得する
    '--------------------------------------------------------------------
    Private Function mGetPathAndFileName(ByVal strSelectPath As String, _
                                         ByRef strFilePath As String, _
                                         ByRef strFileName As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim strwk() As String = Nothing

            strFilePath = ""
            strFileName = ""

            strwk = strSelectPath.Split("\")

            If strwk Is Nothing Then
                intRtn = -1
            Else

                For i As Integer = LBound(strwk) To UBound(strwk)

                    If i <> UBound(strwk) Then
                        strFilePath &= strwk(i) & "\"
                    Else
                        strFileName = strwk(i)
                    End If

                Next

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function
#End Region


#Region "■ｽﾃｰﾀｽ名末尾0x00対応 Ver1.11.9.0 2016.11.21"
    'Ver1.11.9.0 2016.11.21
    Private Sub subPatchData()
        Dim i As Integer = 0
        Dim strStatus As String = ""
        Dim intStatusLen As Integer

        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1

                strStatus = .udtChannel(i).udtChCommon.strStatus

                'もし何も入力されていない場合　T.Ueki
                intStatusLen = Len(strStatus)


                If intStatusLen <> 0 Then
                    'ステータスが255=マニュアル入力ステータス
                    '1文字目が0x00ではない=何か入力されている
                    '16文字目が0x00である=対象データである
                    If .udtChannel(i).udtChCommon.shtStatus = 255 Then
                        'Ver2.0.7.H 保安庁対応
                        'If Asc(strStatus.Substring(0, 1)) <> 0 And Asc(strStatus.Substring(15, 1)) = 0 Then
                        If Asc(MidB(strStatus, 0, 1)) <> 0 And Asc(MidB(strStatus, 15, 1)) = 0 Then
                            '末尾0x00を削り代わりに0x20を付けてデータを書き換える
                            'Ver2.0.7.H 保安庁対応
                            '.udtChannel(i).udtChCommon.strStatus = .udtChannel(i).udtChCommon.strStatus.Substring(0, 15) & " "
                            .udtChannel(i).udtChCommon.strStatus = MidB(.udtChannel(i).udtChCommon.strStatus, 0, 15) & " "
                            'データが変わったため更新ﾌﾗｸﾞを立てる
                            gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        End If
                    End If
                End If
            Next
        End With

    End Sub

#End Region

#Region "■デジタルCH,DeviceStatusの詳細フラグ自動設定 Ver2.0.0.7"
    Private Sub subSetDeviceStatusFLG()
        '処理速度向上のため、ChInfo3000点を一回のみﾙｰﾌﾟして、そのﾙｰﾌﾟ内で随時処理分岐とする。

        Dim i As Integer = 0

        Dim intDeviceCode As Integer = 0
        'Dim cmbStatus As ComboBox

        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1
                '>>>システムCHのみ対象
                If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    'デバイスｺｰﾄﾞの取得
                    'cmbStatus = New ComboBox
                    intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                    'cmbStatus = Nothing

                    Select Case intDeviceCode
                        Case 1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                            '>>>OPS1～OPS10
                            Call subSetDeviceStatusFLG_OPS(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 11, 12
                            '>>>GWS1～GWS2
                            Call subSetDeviceStatusFLG_GWS(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 13, 14
                            '>>>FCU A、FCU B
                            Call subSetDeviceStatusFLG_FCU(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 15, 16
                            '>>>FCU I/O A、FCU I/O B
                            Call subSetDeviceStatusFLG_FCUIO(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 17
                            '>>>FCU 1-2 COMMUNICATION
                            Call subSetDeviceStatusFLG_FCU_COMMU(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 18 To 37
                            '>>>FU1～FU20
                            Call subSetDeviceStatusFLG_FU(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 38
                            '>>>FU LINE
                            Call subSetDeviceStatusFLG_FU_LINE(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 39, 40
                            '>>>LOG PRINTER(Machi/Cargo)
                            Call subSetDeviceStatusFLG_PRINTERs(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 41, 42
                            '>>>ALARM PRINTER(Machi/Cargo)
                            Call subSetDeviceStatusFLG_PRINTERs(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 43, 44
                            '>>>HARD COPY PRINTER(Machi/Cargo)
                            Call subSetDeviceStatusFLG_PRINTERs(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 83, 84
                            '>>>COM1～COM18
                            Call subSetDeviceStatusFLG_COM(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 61
                            '>>>EXT ALARM PANEL ALL
                            Call subSetDeviceStatusFLG_EXT_ALM(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 62 To 81
                            '>>>EXT ALARM PANEL 1～EXT ALARM PANEL 20
                            Call subSetDeviceStatusFLG_EXT_ALM(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 82
                            '>>>SENSOR
                            Call subSetDeviceStatusFLG_EXT_ALM(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 85, 86
                            '>>>COM.EXT FCU A、COM.EXT FCU B
                            Call subSetDeviceStatusFLG_EXT_FCU(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                        Case 87
                            '>>>MANUAL REPOSE
                            Call subSetDeviceStatusFLG_MREP(i, intDeviceCode)
                            'gudt.SetEditorUpdateInfo.udtSave.bytChannel = 1
                    End Select

                    '↓システムCHのみ判定if
                End If
                '↓全CHﾙｰﾌﾟ
            Next i
        End With

    End Sub

    'Sys01～15を一旦ｸﾘｱする関数
    Private Sub subSetSysClear(pintI As Integer)
        With gudt.SetChInfo.udtChannel(pintI)
            '01
            .SystemInfoStatusUse01 = 0
            .SystemInfoKikiCode01 = 0
            .SystemInfoStatusName01 = ""
            '02
            .SystemInfoStatusUse02 = 0
            .SystemInfoKikiCode02 = 0
            .SystemInfoStatusName02 = ""
            '03
            .SystemInfoStatusUse03 = 0
            .SystemInfoKikiCode03 = 0
            .SystemInfoStatusName03 = ""
            '04
            .SystemInfoStatusUse04 = 0
            .SystemInfoKikiCode04 = 0
            .SystemInfoStatusName04 = ""
            '05
            .SystemInfoStatusUse05 = 0
            .SystemInfoKikiCode05 = 0
            .SystemInfoStatusName05 = ""
            '06
            .SystemInfoStatusUse06 = 0
            .SystemInfoKikiCode06 = 0
            .SystemInfoStatusName06 = ""
            '07
            .SystemInfoStatusUse07 = 0
            .SystemInfoKikiCode07 = 0
            .SystemInfoStatusName07 = ""
            '08
            .SystemInfoStatusUse08 = 0
            .SystemInfoKikiCode08 = 0
            .SystemInfoStatusName08 = ""
            '09
            .SystemInfoStatusUse09 = 0
            .SystemInfoKikiCode09 = 0
            .SystemInfoStatusName09 = ""
            '10
            .SystemInfoStatusUse10 = 0
            .SystemInfoKikiCode10 = 0
            .SystemInfoStatusName10 = ""
            '11
            .SystemInfoStatusUse11 = 0
            .SystemInfoKikiCode11 = 0
            .SystemInfoStatusName11 = ""
            '12
            .SystemInfoStatusUse12 = 0
            .SystemInfoKikiCode12 = 0
            .SystemInfoStatusName12 = ""
            '13
            .SystemInfoStatusUse13 = 0
            .SystemInfoKikiCode13 = 0
            .SystemInfoStatusName13 = ""
            '14
            .SystemInfoStatusUse14 = 0
            .SystemInfoKikiCode14 = 0
            .SystemInfoStatusName14 = ""
            '15
            .SystemInfoStatusUse15 = 0
            .SystemInfoKikiCode15 = 0
            .SystemInfoStatusName15 = ""
            '16
            .SystemInfoStatusUse16 = 0
            .SystemInfoKikiCode16 = 0
            .SystemInfoStatusName16 = ""
        End With
    End Sub

    'OPS1～OPS20の詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_OPS(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'ETH A
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName
            'ETH B
            .SystemInfoStatusUse02 = 1
            .SystemInfoKikiCode02 = udtKiki(1).shtCode
            .SystemInfoStatusName02 = udtKiki(1).strName
            'CONFIG
            .SystemInfoStatusUse03 = 1
            .SystemInfoKikiCode03 = udtKiki(2).shtCode
            .SystemInfoStatusName03 = udtKiki(2).strName
            'DATA
            .SystemInfoStatusUse04 = 1
            .SystemInfoKikiCode04 = udtKiki(3).shtCode
            .SystemInfoStatusName04 = udtKiki(3).strName

            '条件でﾌﾗｸﾞ落とし
            '>>>ETH Bは、OPS Setの該当OPSでEthernet line A only有り(=1)の場合ﾌﾗｸﾞ落とし
            '※該当OPSは、DeviceCodeが1～10であるため、マイナス１でＯＫ
            If gudt.SetSystem.udtSysOps.udtOpsDetail(pintDeviceCode - 1).shtEtherA = 1 Then
                .SystemInfoStatusUse02 = 0
            End If
        End With
    End Sub

    'GWS1～GWS20の詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_GWS(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'ETH A
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName
            'ETH B
            .SystemInfoStatusUse02 = 1
            .SystemInfoKikiCode02 = udtKiki(1).shtCode
            .SystemInfoStatusName02 = udtKiki(1).strName
            'CONFIG
            .SystemInfoStatusUse03 = 1
            .SystemInfoKikiCode03 = udtKiki(2).shtCode
            .SystemInfoStatusName03 = udtKiki(2).strName
            'DATA
            .SystemInfoStatusUse04 = 1
            .SystemInfoKikiCode04 = udtKiki(3).shtCode
            .SystemInfoStatusName04 = udtKiki(3).strName

            '条件でﾌﾗｸﾞ落とし
            '>>>ETH Bは、System Setの該当GWSでEthernet line A onlyの場合ﾌﾗｸﾞ落とし
            Dim bONOFF As Boolean = False
            Select Case pintDeviceCode
                Case 11
                    'GWS1
                    bONOFF = gBitCheck(gudt.SetSystem.udtSysSystem.shtGWS1, 1)
                Case 12
                    'GWS2
                    bONOFF = gBitCheck(gudt.SetSystem.udtSysSystem.shtGWS2, 1)
                Case Else
                    bONOFF = False
            End Select
            If bONOFF = True Then
                .SystemInfoStatusUse02 = 0
            End If
        End With
    End Sub

    'FCU A～FCU Bの詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_FCU(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'RUN
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName
            'SUB
            .SystemInfoStatusUse02 = 1
            .SystemInfoKikiCode02 = udtKiki(1).shtCode
            .SystemInfoStatusName02 = udtKiki(1).strName
            'CF
            .SystemInfoStatusUse03 = 1
            .SystemInfoKikiCode03 = udtKiki(2).shtCode
            .SystemInfoStatusName03 = udtKiki(2).strName
            'MEMORY
            .SystemInfoStatusUse04 = 1
            .SystemInfoKikiCode04 = udtKiki(3).shtCode
            .SystemInfoStatusName04 = udtKiki(3).strName
            'SUB MEM
            .SystemInfoStatusUse05 = 1
            .SystemInfoKikiCode05 = udtKiki(4).shtCode
            .SystemInfoStatusName05 = udtKiki(4).strName
            'ETH A
            .SystemInfoStatusUse06 = 1
            .SystemInfoKikiCode06 = udtKiki(5).shtCode
            .SystemInfoStatusName06 = udtKiki(5).strName
            'ETH B
            .SystemInfoStatusUse07 = 1
            .SystemInfoKikiCode07 = udtKiki(6).shtCode
            .SystemInfoStatusName07 = udtKiki(6).strName
            'CONFIG
            .SystemInfoStatusUse08 = 1
            .SystemInfoKikiCode08 = udtKiki(7).shtCode
            .SystemInfoStatusName08 = udtKiki(7).strName
            'SYNCHRO
            .SystemInfoStatusUse09 = 1
            .SystemInfoKikiCode09 = udtKiki(8).shtCode
            .SystemInfoStatusName09 = udtKiki(8).strName
            'EXT SIO
            .SystemInfoStatusUse10 = 1
            .SystemInfoKikiCode10 = udtKiki(9).shtCode
            .SystemInfoStatusName10 = udtKiki(9).strName
            'EX.SIO1
            .SystemInfoStatusUse11 = 1
            .SystemInfoKikiCode11 = udtKiki(10).shtCode
            .SystemInfoStatusName11 = udtKiki(10).strName
            'EX.SIO2
            .SystemInfoStatusUse12 = 1
            .SystemInfoKikiCode12 = udtKiki(11).shtCode
            .SystemInfoStatusName12 = udtKiki(11).strName
            'EX.ETH A
            .SystemInfoStatusUse13 = 1
            .SystemInfoKikiCode13 = udtKiki(12).shtCode
            .SystemInfoStatusName13 = udtKiki(12).strName
            'EX.ETH B
            .SystemInfoStatusUse14 = 1
            .SystemInfoKikiCode14 = udtKiki(13).shtCode
            .SystemInfoStatusName14 = udtKiki(13).strName

            '条件でﾌﾗｸﾞ落とし
            '>>>EX.ETH A,Bは、FCU SetのFCU Extend Boardが0の場合ﾌﾗｸﾞ落とし 2019.02.06
            If gudt.SetSystem.udtSysFcu.shtFcuExtendBord = 0 Then
                .SystemInfoStatusUse13 = 0
                .SystemInfoStatusUse14 = 0
            End If

            '>>>EX SIO,1,2は、FCU SetのFCU Extend Boardが0の場合ﾌﾗｸﾞ落とし
            If gudt.SetSystem.udtSysFcu.shtFcuExtendBord = 0 Then
                'Ver2.0.2.0
                .SystemInfoStatusUse10 = 0

                .SystemInfoStatusUse11 = 0
                .SystemInfoStatusUse12 = 0
            End If
        End With
    End Sub

    'FCU IO A～FCU IO Bの詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_FCUIO(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'SLOT1
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName
            'SLOT2
            .SystemInfoStatusUse02 = 1
            .SystemInfoKikiCode02 = udtKiki(1).shtCode
            .SystemInfoStatusName02 = udtKiki(1).strName
            'SLOT3
            .SystemInfoStatusUse03 = 1
            .SystemInfoKikiCode03 = udtKiki(2).shtCode
            .SystemInfoStatusName03 = udtKiki(2).strName
            'SLOT4
            .SystemInfoStatusUse04 = 1
            .SystemInfoKikiCode04 = udtKiki(3).shtCode
            .SystemInfoStatusName04 = udtKiki(3).strName
            'SLOT5
            .SystemInfoStatusUse05 = 1
            .SystemInfoKikiCode05 = udtKiki(4).shtCode
            .SystemInfoStatusName05 = udtKiki(4).strName

            '条件でﾌﾗｸﾞ落とし
            '>>>SLOT1～5は、FU設定のFCU(0盤目)の該当SLOTの種別が0ならﾌﾗｸﾞ落とし
            If gudt.SetFu.udtFu(0).udtSlotInfo(0).shtType = 0 Then
                .SystemInfoStatusUse01 = 0
            End If
            If gudt.SetFu.udtFu(0).udtSlotInfo(1).shtType = 0 Then
                .SystemInfoStatusUse02 = 0
            End If
            If gudt.SetFu.udtFu(0).udtSlotInfo(2).shtType = 0 Then
                .SystemInfoStatusUse03 = 0
            End If
            If gudt.SetFu.udtFu(0).udtSlotInfo(3).shtType = 0 Then
                .SystemInfoStatusUse04 = 0
            End If
            If gudt.SetFu.udtFu(0).udtSlotInfo(4).shtType = 0 Then
                .SystemInfoStatusUse05 = 0
            End If

        End With
    End Sub

    'FCU 1-2 COMMUNICATIONの詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_FCU_COMMU(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'A-ETH A
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName
            'A-ETH B
            .SystemInfoStatusUse02 = 1
            .SystemInfoKikiCode02 = udtKiki(1).shtCode
            .SystemInfoStatusName02 = udtKiki(1).strName
            'B-ETH A
            .SystemInfoStatusUse03 = 1
            .SystemInfoKikiCode03 = udtKiki(2).shtCode
            .SystemInfoStatusName03 = udtKiki(2).strName
            'B-ETH B
            .SystemInfoStatusUse04 = 1
            .SystemInfoKikiCode04 = udtKiki(3).shtCode
            .SystemInfoStatusName04 = udtKiki(3).strName

            '条件でﾌﾗｸﾞ落とし
            '全部ON ﾌﾗｸﾞ落とし無し
        End With
    End Sub

    'FU 1～FU 20の詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_FU(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'CPU
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName
            'MEMORY
            .SystemInfoStatusUse02 = 1
            .SystemInfoKikiCode02 = udtKiki(1).shtCode
            .SystemInfoStatusName02 = udtKiki(1).strName
            'LINE A
            .SystemInfoStatusUse03 = 1
            .SystemInfoKikiCode03 = udtKiki(2).shtCode
            .SystemInfoStatusName03 = udtKiki(2).strName
            'LINE B
            .SystemInfoStatusUse04 = 1
            .SystemInfoKikiCode04 = udtKiki(3).shtCode
            .SystemInfoStatusName04 = udtKiki(3).strName
            'CANBUS
            .SystemInfoStatusUse05 = 1
            .SystemInfoKikiCode05 = udtKiki(4).shtCode
            .SystemInfoStatusName05 = udtKiki(4).strName
            'SLOT1
            .SystemInfoStatusUse06 = 1
            .SystemInfoKikiCode06 = udtKiki(5).shtCode
            .SystemInfoStatusName06 = udtKiki(5).strName
            'SLOT2
            .SystemInfoStatusUse07 = 1
            .SystemInfoKikiCode07 = udtKiki(6).shtCode
            .SystemInfoStatusName07 = udtKiki(6).strName
            'SLOT3
            .SystemInfoStatusUse08 = 1
            .SystemInfoKikiCode08 = udtKiki(7).shtCode
            .SystemInfoStatusName08 = udtKiki(7).strName
            'SLOT4
            .SystemInfoStatusUse09 = 1
            .SystemInfoKikiCode09 = udtKiki(8).shtCode
            .SystemInfoStatusName09 = udtKiki(8).strName
            'SLOT5
            .SystemInfoStatusUse10 = 1
            .SystemInfoKikiCode10 = udtKiki(9).shtCode
            .SystemInfoStatusName10 = udtKiki(9).strName
            'SLOT6
            .SystemInfoStatusUse11 = 1
            .SystemInfoKikiCode11 = udtKiki(10).shtCode
            .SystemInfoStatusName11 = udtKiki(10).strName
            'SLOT7
            .SystemInfoStatusUse12 = 1
            .SystemInfoKikiCode12 = udtKiki(11).shtCode
            .SystemInfoStatusName12 = udtKiki(11).strName
            'SLOT8
            .SystemInfoStatusUse13 = 1
            .SystemInfoKikiCode13 = udtKiki(12).shtCode
            .SystemInfoStatusName13 = udtKiki(12).strName

            '条件でﾌﾗｸﾞ落とし
            '※該当FU番号は、DeviceStatusが、18から37のため、17を引けば、1～20となる。(注)0は、FCU
            '>>>CANBUSは、FU設定の該当FUのCANBUSが0ならﾌﾗｸﾞ落とし
            If gudt.SetFu.udtFu(pintDeviceCode - 17).shtCanBus = 0 Then
                .SystemInfoStatusUse05 = 0
            End If
            '>>>SLOT1～8は、FU設定の該当FUの該当SLOTの種別が0ならﾌﾗｸﾞ落とし()
            If gudt.SetFu.udtFu(pintDeviceCode - 17).udtSlotInfo(0).shtType = 0 Then
                .SystemInfoStatusUse06 = 0
            End If
            If gudt.SetFu.udtFu(pintDeviceCode - 17).udtSlotInfo(1).shtType = 0 Then
                .SystemInfoStatusUse07 = 0
            End If
            If gudt.SetFu.udtFu(pintDeviceCode - 17).udtSlotInfo(2).shtType = 0 Then
                .SystemInfoStatusUse08 = 0
            End If
            If gudt.SetFu.udtFu(pintDeviceCode - 17).udtSlotInfo(3).shtType = 0 Then
                .SystemInfoStatusUse09 = 0
            End If
            If gudt.SetFu.udtFu(pintDeviceCode - 17).udtSlotInfo(4).shtType = 0 Then
                .SystemInfoStatusUse10 = 0
            End If
            If gudt.SetFu.udtFu(pintDeviceCode - 17).udtSlotInfo(5).shtType = 0 Then
                .SystemInfoStatusUse11 = 0
            End If
            If gudt.SetFu.udtFu(pintDeviceCode - 17).udtSlotInfo(6).shtType = 0 Then
                .SystemInfoStatusUse12 = 0
            End If
            If gudt.SetFu.udtFu(pintDeviceCode - 17).udtSlotInfo(7).shtType = 0 Then
                .SystemInfoStatusUse13 = 0
            End If

        End With
    End Sub

    'FU LINEの詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_FU_LINE(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'LINE A
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName
            'LINE B
            .SystemInfoStatusUse02 = 1
            .SystemInfoKikiCode02 = udtKiki(1).shtCode
            .SystemInfoStatusName02 = udtKiki(1).strName

            '条件でﾌﾗｸﾞ落とし
            '全部ON ﾌﾗｸﾞ落とし無し
        End With
    End Sub

    'PRINTERs(LOG,ALARM,HARD COPY)の詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_PRINTERs(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'ERROR
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName
            'OFFLINE
            .SystemInfoStatusUse02 = 1
            .SystemInfoKikiCode02 = udtKiki(1).shtCode
            .SystemInfoStatusName02 = udtKiki(1).strName

            '条件でﾌﾗｸﾞ落とし
            '全部ON ﾌﾗｸﾞ落とし無し
        End With
    End Sub

    'COM1～COM18の詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_COM(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'TIMEOUT
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName
            'FRAME
            .SystemInfoStatusUse02 = 1
            .SystemInfoKikiCode02 = udtKiki(1).shtCode
            .SystemInfoStatusName02 = udtKiki(1).strName
            'CHK ERR
            .SystemInfoStatusUse03 = 1
            .SystemInfoKikiCode03 = udtKiki(2).shtCode
            .SystemInfoStatusName03 = udtKiki(2).strName
            'PARITY
            .SystemInfoStatusUse04 = 1
            .SystemInfoKikiCode04 = udtKiki(3).shtCode
            .SystemInfoStatusName04 = udtKiki(3).strName
            'FRAMING
            .SystemInfoStatusUse05 = 1
            .SystemInfoKikiCode05 = udtKiki(4).shtCode
            .SystemInfoStatusName05 = udtKiki(4).strName

            '条件でﾌﾗｸﾞ落とし
            '全部ON ﾌﾗｸﾞ落とし無し

        End With
    End Sub

    'EXT ALARM PANEL ALLと1～20,SENSORの詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_EXT_ALM(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'FAIL
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName

            '条件でﾌﾗｸﾞ落とし
            '全部ON ﾌﾗｸﾞ落とし無し
        End With
    End Sub

    'COM.EXT FCU A～COM.EXT FCU Bの詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_EXT_FCU(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'RUN
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName
            'SUB
            .SystemInfoStatusUse02 = 1
            .SystemInfoKikiCode02 = udtKiki(1).shtCode
            .SystemInfoStatusName02 = udtKiki(1).strName
            'CF
            .SystemInfoStatusUse03 = 1
            .SystemInfoKikiCode03 = udtKiki(2).shtCode
            .SystemInfoStatusName03 = udtKiki(2).strName
            'MEMORY
            .SystemInfoStatusUse04 = 1
            .SystemInfoKikiCode04 = udtKiki(3).shtCode
            .SystemInfoStatusName04 = udtKiki(3).strName
            'SUB MEM
            .SystemInfoStatusUse05 = 1
            .SystemInfoKikiCode05 = udtKiki(4).shtCode
            .SystemInfoStatusName05 = udtKiki(4).strName
            'ETH A
            .SystemInfoStatusUse06 = 1
            .SystemInfoKikiCode06 = udtKiki(5).shtCode
            .SystemInfoStatusName06 = udtKiki(5).strName
            'ETH B
            .SystemInfoStatusUse07 = 1
            .SystemInfoKikiCode07 = udtKiki(6).shtCode
            .SystemInfoStatusName07 = udtKiki(6).strName
            'CONFIG
            .SystemInfoStatusUse08 = 1
            .SystemInfoKikiCode08 = udtKiki(7).shtCode
            .SystemInfoStatusName08 = udtKiki(7).strName
            'SYNCHRO
            .SystemInfoStatusUse09 = 1
            .SystemInfoKikiCode09 = udtKiki(8).shtCode
            .SystemInfoStatusName09 = udtKiki(8).strName
            'EXT SIO
            .SystemInfoStatusUse10 = 1
            .SystemInfoKikiCode10 = udtKiki(9).shtCode
            .SystemInfoStatusName10 = udtKiki(9).strName
            'EX.SIO1
            .SystemInfoStatusUse11 = 1
            .SystemInfoKikiCode11 = udtKiki(10).shtCode
            .SystemInfoStatusName11 = udtKiki(10).strName
            'EX.SIO2
            .SystemInfoStatusUse12 = 1
            .SystemInfoKikiCode12 = udtKiki(11).shtCode
            .SystemInfoStatusName12 = udtKiki(11).strName
            'EX.ETH A
            .SystemInfoStatusUse13 = 1
            .SystemInfoKikiCode13 = udtKiki(12).shtCode
            .SystemInfoStatusName13 = udtKiki(12).strName
            'EX.ETH B
            .SystemInfoStatusUse14 = 1
            .SystemInfoKikiCode14 = udtKiki(13).shtCode
            .SystemInfoStatusName14 = udtKiki(13).strName

            '条件でﾌﾗｸﾞ落とし
            '>>>EX.ETH A,Bは常にOFF
            .SystemInfoStatusUse13 = 0
            .SystemInfoStatusUse14 = 0
            '>>>EX SIO1,2は、FCU SetのFCU Extend Boardが0の場合ﾌﾗｸﾞ落とし
            If gudt.SetSystem.udtSysFcu.shtFcuExtendBord = 0 Then
                .SystemInfoStatusUse11 = 0
                .SystemInfoStatusUse12 = 0
            End If
        End With
    End Sub

    'MANUAL REPORTの詳細ﾌﾗｸﾞを設定する
    Private Sub subSetDeviceStatusFLG_MREP(pintI As Integer, pintDeviceCode As Integer)
        Dim udtKiki() As gTypCodeName = Nothing

        With gudt.SetChInfo.udtChannel(pintI)
            Call gGetComboCodeName(udtKiki, gEnmComboType.ctChListChannelListDeviceStatus, _
                                               pintDeviceCode.ToString("00"))

            '一旦、全てのSYSをｸﾘｱ
            Call subSetSysClear(pintI)

            '全てONで詳細をひとまず格納
            'M REP
            .SystemInfoStatusUse01 = 1
            .SystemInfoKikiCode01 = udtKiki(0).shtCode
            .SystemInfoStatusName01 = udtKiki(0).strName

            '条件でﾌﾗｸﾞ落とし
            '全部ON ﾌﾗｸﾞ落とし無し
        End With
    End Sub
#End Region

#Region "■外販 延長警報パネル設定に自動設定処理 Ver2.0.6.5"
    Private Sub subSetExtPnlAuto()
        Try
            Dim i As Integer = 0
            Dim intEngNo As Integer = -1
            Dim blEngNo As Boolean = False
            Dim intEcc() As Integer = Nothing

            With gudt.SetExtAlarm
                '>>>Duty Function 常に１
                .udtExtAlarmCommon.shtDutyFunc = 1

                '>>>Engineer All Call SW No(全体)は、詳細を見て決定する
                intEngNo = -1
                blEngNo = False
                For i = 0 To UBound(.udtExtAlarm) Step 1
                    '詳細が0なら次の処理へ
                    If .udtExtAlarm(i).shtEngNo = 0 Then
                        Continue For
                    End If

                    If intEngNo = -1 Then
                        '最初は普通に詳細のNoを格納
                        intEngNo = .udtExtAlarm(i).shtEngNo
                    Else
                        '1件でも他と異なる詳細Noがあれば処理抜け
                        If intEngNo <> .udtExtAlarm(i).shtEngNo Then
                            blEngNo = True
                            Exit For
                        End If
                    End If
                Next i

                '詳細がバラバラなら0を格納 同じならその番号を格納
                If intEngNo = -1 Or blEngNo = True Then
                    .udtExtAlarmCommon.shtEngCall = 0
                Else
                    .udtExtAlarmCommon.shtEngCall = intEngNo
                End If

                '>>>Engineer Call機能 bit操作
                '0bit 常にON
                .udtExtAlarmCommon.shtEeengineerCall = gBitSet(.udtExtAlarmCommon.shtEeengineerCall, 0, True)
                '1bit Engineer All Call SW 処理が0ならONそうでないならOFF
                If .udtExtAlarmCommon.shtEngCall = 0 Then
                    .udtExtAlarmCommon.shtEeengineerCall = gBitSet(.udtExtAlarmCommon.shtEeengineerCall, 1, True)
                Else
                    .udtExtAlarmCommon.shtEeengineerCall = gBitSet(.udtExtAlarmCommon.shtEeengineerCall, 1, False)
                End If
                '2bit ↓が計測点にあるならON
                'ACCEPT,ACCEPT RESET,ACCEPT LAMP OUT DO,ACCEPT BUZZER OUT DO（55,56,146,147）
                ReDim intEcc(3)
                intEcc(0) = 55
                intEcc(1) = 56
                intEcc(2) = 146
                intEcc(3) = 147
                .udtExtAlarmCommon.shtEeengineerCall = gBitSet(.udtExtAlarmCommon.shtEeengineerCall, 2, fnGetDI(intEcc))
                '3bit 常にON
                .udtExtAlarmCommon.shtEeengineerCall = gBitSet(.udtExtAlarmCommon.shtEeengineerCall, 3, True)
                '4bit 常にOFF
                .udtExtAlarmCommon.shtEeengineerCall = gBitSet(.udtExtAlarmCommon.shtEeengineerCall, 4, False)

                '>>>Patrol man call 常に0
                .udtExtAlarmCommon.shtPatrolCall = 0

                '>>>Dead man alarm
                '↓が計測点にあるならON
                'DEADMAN START,DEADMAN RESET,DEADMAN ALARM（60,61,75）
                ReDim intEcc(2)
                intEcc(0) = 60
                intEcc(1) = 61
                intEcc(2) = 75
                If fnGetDI(intEcc) = True Then
                    .udtExtAlarmCommon.shtDeadAlarm = 1
                Else
                    .udtExtAlarmCommon.shtDeadAlarm = 0
                End If

                '>>>D/L EXT ALARM GROUP LED OUTPUT
                For i = 0 To UBound(.udtExtAlarmCommon.intGroupType) Step 1
                    If fnGetEXTGR(i + 1) = True Then
                        .udtExtAlarmCommon.intGroupType(i) = gBitSet(.udtExtAlarmCommon.intGroupType(i), i, True)
                    Else
                        .udtExtAlarmCommon.intGroupType(i) = gBitSet(.udtExtAlarmCommon.intGroupType(i), i, False)
                    End If
                Next i

            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    '計測点を検索して該当DIがあればTrue
    Private Function fnGetDI(pintFuncNo() As Integer) As Boolean
        Try
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim bRet As Boolean = False

            With gudt.SetChInfo
                For i = 0 To UBound(.udtChannel) Step 1
                    With .udtChannel(i)
                        'デジタルかつ、ﾃﾞｰﾀﾀｲﾌﾟ=0x40であること
                        If .udtChCommon.shtChType = gCstCodeChTypeDigital And _
                            .udtChCommon.shtData = gCstCodeChDataTypeDigitalExt Then
                            '引数分ﾙｰﾌﾟ
                            For j = 0 To UBound(pintFuncNo) Step 1
                                If pintFuncNo(j) = .udtChCommon.shtEccFunc Then
                                    bRet = True
                                    Exit For
                                End If
                            Next j
                            If bRet = True Then
                                Exit For
                            End If
                        End If
                    End With
                Next i

                Return bRet
            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Function
    '計測点を検索して該当EXT.GRがあればTrue
    Private Function fnGetEXTGR(pintEX As Integer) As Boolean
        Try
            Dim bRet As Boolean = False
            Dim i As Integer = 0
            Dim j As Integer = 0


            Dim intExt(6) As Integer
            For i = 0 To UBound(intExt) Step 1
                intExt(i) = 0
            Next i


            With gudt.SetChInfo
                For i = 0 To UBound(.udtChannel) Step 1
                    With .udtChannel(i)
                        'CH Typeで分岐してEXT GRを格納
                        Select Case .udtChCommon.shtChType
                            Case gCstCodeChTypeAnalog
                                intExt(0) = .AnalogHiHiExtGroup
                                intExt(1) = .AnalogHiExtGroup
                                intExt(2) = .AnalogLoExtGroup
                                intExt(3) = .AnalogLoLoExtGroup
                                intExt(4) = .AnalogSensorFailExtGroup
                            Case gCstCodeChTypeDigital
                                intExt(0) = .udtChCommon.shtExtGroup
                            Case gCstCodeChTypeMotor
                                intExt(0) = .udtChCommon.shtExtGroup
                                intExt(1) = .MotorAlarmExtGroup
                            Case gCstCodeChTypeValve
                                'バルブはバルブタイプで分岐
                                Select Case .udtChCommon.shtData
                                    Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55, gCstCodeChDataTypeValveExt
                                        'DIDO,DO
                                        intExt(0) = .udtChCommon.shtExtGroup
                                        intExt(1) = .ValveDiDoAlarmExtGroup
                                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
                                        'AIDO
                                        intExt(0) = .ValveAiDoHiHiExtGroup
                                        intExt(1) = .ValveAiDoHiExtGroup
                                        intExt(2) = .ValveAiDoLoExtGroup
                                        intExt(3) = .ValveAiDoLoLoExtGroup
                                        intExt(4) = .ValveAiDoSensorFailExtGroup
                                        intExt(5) = .ValveAiDoAlarmExtGroup
                                        intExt(6) = .udtChCommon.shtExtGroup
                                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2
                                        'AIAO
                                        intExt(0) = .ValveAiAoHiHiExtGroup
                                        intExt(1) = .ValveAiAoHiExtGroup
                                        intExt(2) = .ValveAiAoLoExtGroup
                                        intExt(3) = .ValveAiAoLoLoExtGroup
                                        intExt(4) = .ValveAiAoSensorFailExtGroup
                                        intExt(5) = .ValveAiAoAlarmExtGroup
                                        intExt(6) = .udtChCommon.shtExtGroup
                                    Case gCstCodeChDataTypeValveAO_4_20
                                        'AO
                                        intExt(0) = .ValveAiAoAlarmExtGroup
                                End Select
                            Case gCstCodeChTypeComposite
                                intExt(0) = .udtChCommon.shtExtGroup
                            Case gCstCodeChTypePulse
                                intExt(0) = .udtChCommon.shtExtGroup
                        End Select

                        '引数の数値がEXTに含まれてるならTrueでexit for
                        For j = 0 To UBound(intExt) Step 1
                            If intExt(j) = pintEX Then
                                bRet = True
                                Exit For
                            End If
                        Next j
                        If bRet = True Then
                            Exit For
                        End If
                    End With
                Next i
            End With

            Return bRet
        Catch ex As Exception

        End Try
    End Function
#End Region


    '----------------------------------------------------------------------------
    ' 機能説明  ： ファイル出力ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '
    '   2015.10.23 Ver1.7.5 出力ﾌｧｲﾙ名は自動作成するように変更
    '----------------------------------------------------------------------------
    Private Sub cmdOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOutput.Click

        Call SaveErrLog()

        'Try
        '    Dim dlgOutput As New SaveFileDialog()

        '    With dlgOutput

        '        .FileName = Format(Now, "yyyyMMddHHmm") & ".txt"
        '        .InitialDirectory = gGetAppPath()
        '        .Filter = "Text File(*.txt)|*.txt"
        '        .FilterIndex = 1
        '        .RestoreDirectory = True
        '        .OverwritePrompt = True
        '        .CheckPathExists = True

        '        ''ダイアログを表示する
        '        If .ShowDialog() = DialogResult.OK Then
        '        End If

        '    End With

        'Catch ex As Exception
        '    Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        'End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ｴﾗｰﾛｸﾞ 出力
    ' 引数      ： なし
    ' 戻値      ： なし
    '
    '  2015.10.23 Ver1.7.5 追加
    '  Ver2.0.1.3 エラーのみﾌｧｲﾙも生成
    '  Ver2.0.3.1 警告対応
    '----------------------------------------------------------------------------
    Private Sub SaveErrLog()
        Dim strErrLogPath As String
        Dim strErrOnly As String
        Dim intFileNum As Integer

        'strErrLogPath = mudtFileInfo.strFilePath & mudtFileInfo.strFileName & "\Temp\" & mudtFileInfo.strFileName & "_err.txt"
        strErrLogPath = mudtFileInfo.strFilePath & "\" & mudtFileInfo.strFileName & "\Temp\" & mudtFileInfo.strFileName & "_compile.txt"
        strErrOnly = mudtFileInfo.strFilePath & "\" & mudtFileInfo.strFileName & "\Temp\" & mudtFileInfo.strFileName & "_err.txt"

        Try
            '>>>ｺﾝﾊﾟｲﾙﾌｧｲﾙ
            If System.IO.File.Exists(strErrLogPath) Then    ' 前回のｴﾗｰﾛｸﾞが存在する場合は削除
                System.IO.File.Delete(strErrLogPath)
            End If

            ''ファイル出力
            intFileNum = FreeFile()
            Call FileOpen(intFileNum, strErrLogPath, OpenMode.Append)
            Call Print(intFileNum, txtMsg.Text & vbNewLine)
            Call FileClose(intFileNum)

            ' ﾃｷｽﾄを開くのは保留
            'If mblnEnglish Then

            '    If MessageBox.Show("The file is output successfully." & vbNewLine & _
            '                       "Do you open the file?" & vbNewLine & vbNewLine & strErrLogPath, _
            '                       "Compiler", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            '        Call Process.Start(strErrLogPath)

            '    End If

            'Else

            '    If MessageBox.Show("ファイルの出力が完了しました。" & vbNewLine & _
            '                       "出力したファイルを開きますか？" & vbNewLine & vbNewLine & strErrLogPath, _
            '                       "コンパイラ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

            '        Call Process.Start(strErrLogPath)

            '    End If

            'End If


            '>>>エラーオンリーﾌｧｲﾙ
            If System.IO.File.Exists(strErrOnly) Then    ' 前回のｴﾗｰﾛｸﾞが存在する場合は削除
                System.IO.File.Delete(strErrOnly)
            End If

            ''ファイル出力
            intFileNum = FreeFile()
            Call FileOpen(intFileNum, strErrOnly, OpenMode.Append)

            Dim strMsgError As String = ""
            Dim strMsgDummy As String = ""
            Dim strMsgWar As String = ""
            Dim strMsgOut As String = ""

            If Not mstrErrMsg Is Nothing Then
                strMsgError &= IIf(mblnEnglish, "[Error Description]", "[エラー詳細]") & vbCrLf

                For i As Integer = 0 To UBound(mstrErrMsg)
                    strMsgError &= "  " & mstrErrMsg(i) & vbCrLf
                Next
            End If

            If Not mstrDummyMsg Is Nothing Then
                strMsgDummy &= IIf(mblnEnglish, "[Dummy Description]", "[仮設定]") & vbCrLf

                For i As Integer = 0 To UBound(mstrDummyMsg)
                    strMsgDummy &= "  " & mstrDummyMsg(i) & vbCrLf
                Next
            End If

            If Not mstrWarMsg Is Nothing Then
                strMsgWar &= IIf(mblnEnglish, "[Warning Description]", "[警告詳細]") & vbCrLf

                For i As Integer = 0 To UBound(mstrWarMsg)
                    strMsgWar &= "  " & mstrWarMsg(i) & vbCrLf
                Next
            End If


            strMsgOut = strMsgError & vbCrLf & vbCrLf & strMsgDummy & vbCrLf & vbCrLf & strMsgWar
            Call Print(intFileNum, strMsgOut & vbNewLine)
            Call FileClose(intFileNum)


        Catch ex As Exception


        End Try
    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 印刷ボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try

            Dim strMsg As String
            Dim strTitle As String

            If mblnEnglish Then
                strMsg = "May I print compile massage?"
                strTitle = "Compile Print"
            Else
                strMsg = "コンパイルメッセージを印字します。よろしいですか？"
                strTitle = "コンパイル印字"
            End If

            If MessageBox.Show(strMsg, strTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then

                '印刷する文字列と位置を設定する
                mstrPrintingText = txtMsg.Text
                mstrPrintingPosition = 0

                '印刷に使うフォントを指定する
                mstrPrintFont = New Font("Arial", 10)

                'PrintDocumentオブジェクトの作成
                Dim pd As New System.Drawing.Printing.PrintDocument

                'PrintPageイベントハンドラの追加
                AddHandler pd.PrintPage, AddressOf pd_PrintPage

                '印刷を開始する
                pd.Print()

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： 印刷処理
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub pd_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs)

        Try

            If mstrPrintingPosition = 0 Then
                '改行記号を'\n'に統一する
                mstrPrintingText = mstrPrintingText.Replace(vbCrLf, vbLf)
                '  mstrPrintingText = mstrPrintingText.Replace(vbCr, vbLf)
            End If

            '印刷する初期位置を決定
            Dim x As Integer = e.MarginBounds.Left
            Dim y As Integer = e.MarginBounds.Top

            '1ページに収まらなくなるか、印刷する文字がなくなるかまでループ
            While e.MarginBounds.Bottom > y + mstrPrintFont.Height AndAlso _
                    mstrPrintingPosition < mstrPrintingText.Length
                Dim line As String = ""

                While True
                    '印刷する文字がなくなるか、
                    '改行の時はループから抜けて印刷する
                    If mstrPrintingPosition >= mstrPrintingText.Length OrElse _
                            mstrPrintingText.Chars(mstrPrintingPosition) = vbLf Then
                        mstrPrintingPosition += 1
                        Exit While
                    End If
                    '一文字追加し、印刷幅を超えるか調べる
                    line += mstrPrintingText.Chars(mstrPrintingPosition)
                    If e.Graphics.MeasureString(line, mstrPrintFont).Width _
                            > e.MarginBounds.Width Then
                        '印刷幅を超えたため、折り返す
                        line = line.Substring(0, line.Length - 1)
                        Exit While
                    End If
                    '印刷文字位置を次へ
                    mstrPrintingPosition += 1
                End While
                '一行書き出す
                e.Graphics.DrawString(line, mstrPrintFont, Brushes.Black, x, y)
                '次の行の印刷位置を計算
                y += mstrPrintFont.GetHeight(e.Graphics)
            End While

            '次のページがあるか調べる
            If mstrPrintingPosition >= mstrPrintingText.Length Then
                e.HasMorePages = False
                '初期化する
                mstrPrintingPosition = 0
            Else
                e.HasMorePages = True
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： キャンセルボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            mblnCancelFlg = True
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： エラー表示
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdErrorDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdErrorDisplay.Click
        'Ver2.0.3.1 Warning対応
        Try

            Dim strMsgError As String = ""
            Dim strMsgDummy As String = ""
            Dim strMsgWar As String = ""

            If Not mstrErrMsg Is Nothing Then

                mblnCancelFlg = False

                Call mAddMsgText("[Error Description]", "[エラー詳細]")

                For i As Integer = 0 To UBound(mstrErrMsg)
                    strMsgError &= "  (" & i + 1 & ") " & mstrErrMsg(i) & vbCrLf
                Next

                Call mAddMsgText(strMsgError, strMsgError)

                mblnCancelFlg = True

            End If

            If Not mstrDummyMsg Is Nothing Then

                mblnCancelFlg = False

                Call mAddMsgText("[Dummy Description]", "[仮設定]")

                For i As Integer = 0 To UBound(mstrDummyMsg)
                    strMsgDummy &= "  (" & i + 1 & ") " & mstrDummyMsg(i) & vbCrLf
                Next

                Call mAddMsgText(strMsgDummy, strMsgDummy)

                mblnCancelFlg = True

            End If

            If Not mstrWarMsg Is Nothing Then

                mblnCancelFlg = False

                Call mAddMsgText("[Warning Description]", "[警告詳細]")

                For i As Integer = 0 To UBound(mstrWarMsg)
                    strMsgWar &= "  (" & i + 1 & ") " & mstrWarMsg(i) & vbCrLf
                Next

                Call mAddMsgText(strMsgWar, strMsgWar)

                mblnCancelFlg = True

            End If

            'If Not mstrErrMsg Is Nothing Then

            '    mblnCancelFlg = False

            '    Call mAddMsgText("[Error Description]", "[エラー詳細]")

            '    For i As Integer = 0 To UBound(mstrErrMsg)
            '        Call mAddMsgText("  (" & i + 1 & ") " & mstrErrMsg(i), "  (" & i + 1 & ") " & mstrErrMsg(i))
            '    Next

            '    mblnCancelFlg = True

            'End If

            'If Not mstrDummyMsg Is Nothing Then

            '    mblnCancelFlg = False

            '    Call mAddMsgText("[Dummy Description]", "[仮設定]")

            '    For i As Integer = 0 To UBound(mstrDummyMsg)
            '        Call mAddMsgText("  (" & i + 1 & ") " & mstrDummyMsg(i), "  (" & i + 1 & ") " & mstrDummyMsg(i))
            '    Next

            '    mblnCancelFlg = True

            'End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： フォームクローズ
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub frmCmpCompier_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Exitボタンクリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Me.Dispose()
            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "内部関数"

#Region "ヘッダーメッセージ表示"

    '--------------------------------------------------------------------
    ' 機能      : ヘッダーメッセージ出力
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : ヘッダーメッセージを出力する
    '--------------------------------------------------------------------
    Private Sub mDispHeaderMessage()

        Try

            Select Case mudtCompileType
                Case gEnmCompileType.cpCompile

                    Call mAddMsgText(" --- EDITOR COMPILER Ver " & gGetVersionChar() & " for Win --- ", _
                                     " --- エディタ コンパイラ Ver " & gGetVersionChar() & " for Win --- ")

                    Call mAddMsgText(" File Name : [ FILE : " & mudtFileInfo.strFileName & " ] ", " ファイル名 : [ FILE : " & mudtFileInfo.strFileName & " ] ")
                    Call mAddMsgText("", "")

                Case gEnmCompileType.cpErrorCheck

                    Call mAddMsgText(" --- EDITOR ERROR CHECKER Ver " & gGetVersionChar() & " for Win --- ", _
                                     " --- エディタ エラーチェック Ver " & gGetVersionChar() & " for Win --- ")

                    Call mAddMsgText(" File Name : [ FILE : " & mudtFileInfo.strFileName & " ] ", " ファイル名 : [ FILE : " & mudtFileInfo.strFileName & " ] ")
                    Call mAddMsgText("", "")

                Case gEnmCompileType.cpMeasuringCheck
                    ''2019.03.12 追加
                    Call mAddMsgText(" --- EDITOR ERROR CHECKER Ver " & gGetVersionChar() & " for Win --- ", _
                                     " --- エディタ エラーチェック Ver " & gGetVersionChar() & " for Win --- ")

                    Call mAddMsgText(" File Name : [ FILE : " & mudtFileInfo.strFileName & " ] ", " ファイル名 : [ FILE : " & mudtFileInfo.strFileName & " ] ")
                    Call mAddMsgText("", "")

            End Select

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "初期化ファイル存在確認"

    '--------------------------------------------------------------------
    ' 機能      : 初期化ファイル存在確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 初期化ファイルの存在確認を行う
    '--------------------------------------------------------------------
    Private Sub mChkIniFileExist()

        Try

            Dim strPath As String
            Dim strFileName(37) As String   '' 和文仕様追加 20200603 hori

            ''iniファイル名を配列に格納
            strFileName(0) = gCstIniFileNameComboSysSystem
            strFileName(1) = gCstIniFileNameComboSysFcu
            strFileName(2) = gCstIniFileNameComboSysOps
            strFileName(3) = gCstIniFileNameComboSysGws         '' GWS処理追加  2014.02.03
            strFileName(4) = gCstIniFileNameComboSysPrinter
            strFileName(5) = gCstIniFileNameComboExtPnlSystem
            strFileName(6) = gCstIniFileNameComboExtPnlList
            strFileName(7) = gCstIniFileNameComboExtPnlLed
            strFileName(8) = gCstIniFileNameComboExtPnlLcdGroup
            strFileName(9) = gCstIniFileNameComboExtTimer
            strFileName(10) = gCstIniFileNameComboSeqOpe
            strFileName(11) = gCstIniFileNameComboSeqLine
            strFileName(12) = gCstIniFileNameComboSeqSetDetail
            strFileName(13) = gCstIniFileNameComboOpsPulldown
            strFileName(14) = gCstIniFileNameComboOpsPulldownJpn
            strFileName(15) = gCstIniFileNameComboOpsGraphList
            strFileName(16) = gCstIniFileNameComboOpsGraphAnalogMeter
            strFileName(17) = gCstIniFileNameComboOpsGraphAnalogMeterDetail
            strFileName(18) = gCstIniFileNameComboOpsLogFormat
            strFileName(19) = gCstIniFileNameComboChList
            strFileName(20) = gCstIniFileNameComboChListJpn
            strFileName(21) = gCstIniFileNameComboChTerminalList
            strFileName(22) = gCstIniFileNameComboChTerminalFunction
            strFileName(23) = gCstIniFileNameComboChOutputDo
            strFileName(24) = gCstIniFileNameComboChGroupReposeList
            strFileName(25) = gCstIniFileNameComboChRunHour
            strFileName(26) = gCstIniFileNameComboChExhGusGroup
            strFileName(27) = gCstIniFileNameComboChDataForwardTableList
            strFileName(28) = gCstIniFileNameComboChDataSaveTableList
            strFileName(29) = gCstIniFileNameComboChSioDetail
            strFileName(30) = gCstIniFileNameComboChCtrlUseNotuseDetail
            strFileName(31) = gCstIniFileNameListSeqLogic
            strFileName(32) = gCstIniFileNameListOpsScreenTitle
            strFileName(33) = gCstIniFileNameListOpsScreenTitleJpn
            strFileName(34) = gCstIniFileNameListOpsSelectionMenu
            strFileName(35) = gCstIniFileNameListOpsSelectionMenuJpn
            strFileName(36) = gCstIniFileNameListChSystem
            strFileName(37) = gCstIniFileNameListChSystemJpn

            For i As Integer = 0 To UBound(strFileName)

                ''フルパス作成
                strPath = System.IO.Path.Combine(gGetDirNameIniFile, strFileName(i))

                ''ファイル存在確認
                If System.IO.File.Exists(strPath) Then
                    Call mAddMsgText(" -" & strFileName(i) & " ... Exist", " -" & strFileName(i) & " ... OK")
                Else
                    Call mAddMsgText(" -" & strFileName(i) & " ... Not Exist", " -" & strFileName(i) & " ... 存在しません")
                    Call mAddMsgText("  -Inifile doesn't exist. [Path] " & strPath, "  -初期化ファイルが存在しません。[パス] " & strPath)
                    Call mSetErrString("Inifile doesn't exist. [Path] " & strPath, "初期化ファイルが存在しません。[パス] " & strPath)
                End If

                If mblnCancelFlg Then Return

            Next

            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "コンバイン設定確認"

    '--------------------------------------------------------------------
    ' 機能      : コンバイン設定確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : コンバイン設定の確認を行う
    '--------------------------------------------------------------------
    Private Sub mChkCombine()

        Try

            ''FCU台数２台でコンバインありの場合
            If gudt.SetSystem.udtSysSystem.shtCombineUse <> gCstCodeSysCombineNone And _
               gudt.SetSystem.udtSysFcu.shtFcuNo >= 2 Then
                Call mAddMsgText(" -Checking Combine Setting ... Failure", " -コンバイン設定確認 ... エラー")
                Call mSetErrString("Set Combine and FCU Count = 2 is inconsistent setting.", "コンバイン有りでFCU２台の設定は不整合です。")
            Else
                Call mAddMsgText(" -Checking Combine Setting ... Success", " -コンバイン設定確認 ... OK")
            End If

            Call mAddMsgText("", "")
            If mblnCancelFlg Then Return

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "Fire Alarm Mimicチェック"
    Private Sub mChkFireAlmMimic()
        Try
            Dim strMimicNo As String = ""

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            With gudt.SetChInfo

                'AlmMimicが入っているか
                For i As Integer = 0 To UBound(.udtChannel)
                    strMimicNo = GetMimicAlm(.udtChannel(i))
                    'AlmMimicが入っていないなら次のレコード
                    If strMimicNo <> "" Then
                        Call mAddMsgText(" CH No=" & .udtChannel(i).udtChCommon.shtChno & " --- Mimic=" & strMimicNo, _
                                         " CH No=" & .udtChannel(i).udtChCommon.shtChno & " --- Mimic=" & strMimicNo)

                        'AlmMimicアリ　フラグON
                        mblFireAlmMimic = True

                        'Mimicファイル存在チェック
                        Dim strPathBase As String = ""
                        'パス生成
                        strPathBase = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion)
                        strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameSave)
                        strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameMimic)
                        strPathBase = System.IO.Path.Combine(strPathBase, "Mimic1")
                        Dim strMimicFiles As String() = System.IO.Directory.GetFiles(strPathBase, "S0" & strMimicNo & ".mim", System.IO.SearchOption.AllDirectories)

                        If strMimicFiles.Length = 0 Then
                            '該当Mimicが存在しないというエラー
                            Call mSetErrString("Alarm Mimic File is not Exitst." & _
                                           "[Info]Group=" & .udtChannel(i).udtChCommon.shtGroupNo & _
                                           " , CH NO=" & .udtChannel(i).udtChCommon.shtChno & " , AlarmMimicNo=" & strMimicNo, _
                                           "火災警報設定Mimicファイルは存在しません。" & _
                                           "[情報]グループ=" & .udtChannel(i).udtChCommon.shtGroupNo & _
                                           " , チャンネル番号=" & .udtChannel(i).udtChCommon.shtChno & " , AlarmMimicNo=" & strMimicNo, _
                                           intErrCnt, strErrMsg)
                        Else
                            'Mimic内に該当CHnoがあるかチェック
                            Dim strPath As String = strMimicFiles(0)
                            Dim strFileName As String = System.IO.Path.GetFileName(strPath)
                            Dim bytesData As Byte()
                            Dim pRet As IntPtr
                            Dim strRet As String = ""
                            'Shift JISとして文字列に変換
                            bytesData = System.Text.Encoding.GetEncoding(932).GetBytes(strPath)
                            strPath = System.Text.Encoding.GetEncoding(932).GetString(bytesData)
                            'DLLコール
                            pRet = mainProc(strPath, CInt(.udtChannel(i).udtChCommon.shtChno))
                            strRet = Marshal.PtrToStringAnsi(pRet)

                            If strRet.Trim = "" Then
                                'Mimic内に該当CHno存在しない
                                Call mSetErrString("Alarm MimicNo is not Exitst." & _
                                           "[Info]Group=" & .udtChannel(i).udtChCommon.shtGroupNo & _
                                           " , CH NO=" & .udtChannel(i).udtChCommon.shtChno & " , AlarmMimicNo=" & strMimicNo, _
                                           "火災警報設定Mimic内にCH Noは存在しません。" & _
                                           "[情報]グループ=" & .udtChannel(i).udtChCommon.shtGroupNo & _
                                           " , チャンネル番号=" & .udtChannel(i).udtChCommon.shtChno & " , AlarmMimicNo=" & strMimicNo, _
                                           intErrCnt, strErrMsg)
                            End If
                        End If
                    End If
                Next

                ''結果表示
                If mblFireAlmMimic = True Then
                    '1点でもMimicNoが入力されているならば、その成否にかかわらず
                    'OPS設定にﾌﾗｸﾞをｾｯﾄ
                    gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 3, True)

                    'MimicNoが1件でも入力されていること
                    If intErrCnt = 0 Then
                        Call mAddMsgText(" -Checking Alarm MimicNo Setting ... Success", " -火災警報設定確認 ... OK")
                    Else

                        Call mAddMsgText(" -Checking Alarm MimicNo Setting ... Failure", " -火災警報設定確認 ... エラー")
                        ''失敗時はエラー内容を追記
                        For i As Integer = 0 To UBound(strErrMsg)
                            Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                            Call mSetErrString(strErrMsg(i), strErrMsg(i))
                            If mblnCancelFlg Then Return
                        Next
                    End If
                Else
                    'MimicNoが入力されていないならば
                    'OPS設定のﾌﾗｸﾞを落とす
                    gudt.SetSystem.udtSysOps.shtSystem = gBitSet(gudt.SetSystem.udtSysOps.shtSystem, 3, False)

                    Call mAddMsgText(" -Checking Alarm MimicNo Setting ... NoData OK", " -火災警報設定確認 ... 設定無し OK")
                End If


            End With
        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub

#End Region


#Region "チャンネル情報チェック"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : チャンネル情報のチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChannelInfo()

        Try

            ''チャンネル情報チェック開始
            Call mAddMsgText("[Check Channel Info]", "[チャンネル設定確認]")


            ''チャンネルNo重複チェック
            Call mChkChNoOverlap()
            If mblnCancelFlg Then Return


            ''FUアドレスチェック
            Call mChkFuAddress()
            If mblnCancelFlg Then Return

            'Ver2.0.0.4
            'チャンネル情報共通部チェック
            Call mChkChCommon()
            If mblnCancelFlg Then Return


            ''デジタル設定チェック 2015/5/13 T.Ueki
            Call mChkChDigital()
            If mblnCancelFlg Then Return


            ''アナログ設定チェック
            Call mChkChAnalog()
            If mblnCancelFlg Then Return

            '' Ver1.10.5 2016.05.09 追加
            '' ﾓｰﾀｰ設定チェック 
            Call mChkChMotor()
            If mblnCancelFlg Then Return

            '' ﾊﾟﾙｽ設定ﾁｪｯｸ
            Call mChkChPulse2()
            If mblnCancelFlg Then Return
            ''//

            ''チャンネル名称チェック
            Call mChkChName()
            If mblnCancelFlg Then Return


            ''手動入力項目チェック
            Call mChkChManualInput()
            If mblnCancelFlg Then Return


            ''延長警報グループチェック
            Call mChkChExtGroup()
            If mblnCancelFlg Then Return


            'Ver2.0.0.2 南日本M761対応 2017.02.27追加
            '運転積算データ設定ファイルのTriggerCHをChInfoのTriggerCHへ格納
            Call mSetChRunHour()
            If mblnCancelFlg Then Return


            ''運転積算CH設定数チェック
            Call mChkChRunHourCnt()
            If mblnCancelFlg Then Return


            '▼▼▼ 20110215 アナログCHとパルスCHのRLフラグ確認を行わない ▼▼▼▼▼▼▼▼▼▼
            '（下記関数ではアナログCHとパルスCHのチェックしか行っていないので関数ごとコメント）
            ''RLフラグチェック
            'Call mChkChFlagRL()
            'If mblnCancelFlg Then Return
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            '▼▼▼ 20110308 パルス積算設定（８端子以内）確認は行わない ▼▼▼▼▼▼▼▼▼▼▼▼
            ' ''パルス積算設定チェック
            'Call mChkChPulse()
            'If mblnCancelFlg Then Return
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            ' ''延長警報グループ設定時の遅延タイマーチェック
            'Call mChkExtGDelayTimer()
            'If mblnCancelFlg Then Return

            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "チャンネルNo重複チェック"

    '--------------------------------------------------------------------
    ' 機能      : チャンネルNo重複チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : チャンネルNoの重複チェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChNoOverlap()

        Try

            Dim aryCheck As New ArrayList
            Dim strwk1() As String
            Dim strwk2() As String
            Dim strItem1 As String
            Dim strItem2 As String
            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            Dim strMsg1 As String       '' Ver1.12.0.8 2017.02.22
            Dim strMsg2 As String       '' Ver1.12.0.8 2017.02.22


            With gudt.SetChInfo

                ''設定されているチャンネル番号のみチャンネル番号と配列番号を配列化
                For i As Integer = 0 To UBound(.udtChannel)
                    If .udtChannel(i).udtChCommon.shtChno <> 0 Then
                        aryCheck.Add(.udtChannel(i).udtChCommon.shtChno & "," & i)
                    End If
                Next

                ''チャンネル番号が存在する場合
                If Not aryCheck Is Nothing Then

                    ''チャンネル番号順に並べ替え
                    Call aryCheck.Sort()

                    '' Ver1.12.0.8 2017.02.22 CH数表示追加
                    strMsg1 = "***** Channel Count = " + aryCheck.Count.ToString
                    strMsg2 = "***** チャンネル数 = " + aryCheck.Count.ToString
                    Call mAddMsgText(strMsg1, strMsg2)
                    ''//

                    ''上から順に１つ下と番号が同じかチェック
                    For i As Integer = 0 To aryCheck.Count - 1

                        ''最後の１つはチェックしない
                        If i = aryCheck.Count - 1 Then Exit For

                        ''チャンネル番号とチャンネル配列番号を分割
                        strwk1 = aryCheck(i).ToString.Split(",")
                        strwk2 = aryCheck(i + 1).ToString.Split(",")

                        ''チャンネル番号が同じ場合
                        If strwk1(0) = strwk2(0) Then

                            ''メッセージ作成
                            strItem1 = IIf(mblnEnglish, "[Info1]Group=" & .udtChannel(strwk1(1)).udtChCommon.shtGroupNo & _
                                                        " , CH Name=" & gGetString(.udtChannel(strwk1(1)).udtChCommon.strChitem), _
                                                        "[情報１]グループ=" & .udtChannel(strwk1(1)).udtChCommon.shtGroupNo & _
                                                        " , チャンネル名称=" & gGetString(.udtChannel(strwk1(1)).udtChCommon.strChitem))
                            strItem2 = IIf(mblnEnglish, "[Info2]Group=" & .udtChannel(strwk2(1)).udtChCommon.shtGroupNo & _
                                                        " , CH Name=" & gGetString(.udtChannel(strwk2(1)).udtChCommon.strChitem), _
                                                        "[情報２]グループ=" & .udtChannel(strwk2(1)).udtChCommon.shtGroupNo & _
                                                        " , チャンネル名称=" & gGetString(.udtChannel(strwk2(1)).udtChCommon.strChitem))

                            ''メッセージ追加
                            ReDim Preserve strErrMsg(intErrCnt)
                            strErrMsg(intErrCnt) = IIf(mblnEnglish, "CH NO [" & strwk1(0) & "] overlaps. " & strItem1 & " " & strItem2, _
                                                                    "チャンネル番号 " & strwk1(0) & "は重複しています。" & strItem1 & " " & strItem2)

                            intErrCnt += 1

                        End If

                    Next
                End If
            End With

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking CH NO overlaps ... Success", " -チャンネル番号重複確認 ... OK")
            Else

                Call mAddMsgText(" -Checking CH NO overlaps ... Failure", " -チャンネル番号重複確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "FUアドレスチェック"

    '--------------------------------------------------------------------
    ' 機能      : FUアドレスチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : FUアドレスのチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkFuAddress()

        Try

            Dim aryFuAddressALL As New ArrayList
            Dim aryFuAddressOut As New ArrayList

            ''FUアドレスが設定されていること
            Call mChkFuAddressSet(aryFuAddressALL, aryFuAddressOut)
            If mblnCancelFlg Then Return

            '▼▼▼ 20110215 出力側のみFUアドレスの重複チェックを行う ▼▼▼▼▼▼
            ''FUアドレスが重複していないこと
            Call mChkFuAddressOverlap(aryFuAddressOut)
            If mblnCancelFlg Then Return
            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

            ''CH情報とFUボード種別が一致していること
            Call mChkFuBoardType(aryFuAddressALL)
            If mblnCancelFlg Then Return

            'Ver2.0.3.1
            'CH情報が端子からオーバーしてないか確認
            Call mChkFuTermCount()
            If mblnCancelFlg Then Return

            'Ver2.0.6.0
            'DO,AO基板でないFuｱﾄﾞﾚｽが計測点のOUT_FUｱﾄﾞﾚｽに入力されている場合ｴﾗｰﾒｯｾｰｼﾞ
            '(上記エラーは他でひっかかる)
            'Call mChkFuOutAdr()
            'If mblnCancelFlg Then Return


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス設定確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) FUアドレス情報（入力側、出力側 両方）
    ' 　　　    : ARG2 - ( O) FUアドレス情報（出力側のみ）
    ' 機能説明  : FUアドレスが設定されているか確認する
    '--------------------------------------------------------------------
    Private Sub mChkFuAddressSet(ByRef aryFuAddressALL As ArrayList, _
                                 ByRef aryFuAddressOut As ArrayList)

        Try

            Dim intFuNo As Integer
            Dim intPortNo As Integer
            Dim intPin As Integer
            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            With gudt.SetChInfo

                ''チャンネルが設定されている場合、FUアドレスが設定されているか確認
                For i As Integer = 0 To UBound(.udtChannel)

                    With .udtChannel(i)
                        Dim aaa As Integer

                        If i = 238 Then
                            aaa = 1
                        End If

                        If .udtChCommon.shtChno = 945 Then
                            aaa = 1
                        End If
                        If .udtChCommon.shtChno <> 0 Then

                            '==========
                            '' INPUT
                            '==========
                            With .udtChCommon

                                '▼▼▼ 20110308 出力のみの場合は入力側のチェックを行わない ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                                If .shtChType = gCstCodeChTypeValve And _
                                  (.shtData = gCstCodeChDataTypeValveDO Or _
                                   .shtData = gCstCodeChDataTypeValveAO_4_20 Or _
                                   .shtData = gCstCodeChDataTypeValveJacom Or _
                                   .shtData = gCstCodeChDataTypeValveJacom55 Or _
                                   .shtData = gCstCodeChDataTypeValveExt) Then

                                    ''出力のみの場合は何もしない
                                    '' Ver1.12.1.0 2017.03.06 追加
                                ElseIf .shtChType = gCstCodeChTypeMotor And _
                                            (.shtData >= gCstCodeChDataTypeMotorRManRun And .shtData <= gCstCodeChDataTypeMotorRDevice) Then
                                    '' ﾓｰﾀｰ通信CHの場合は何もしない
                                Else

                                    If .shtShareType = 2 Or .shtShareType = 3 Then      '' Ver1.11.8.5 2016.11.09  Remote/Shareの場合はﾁｪｯｸしない

                                    Else

                                        ''FU番号、ポート、端子番号が全て 0 か 65535 の場合
                                        If (.shtFuno = 0 And .shtPortno = 0 And .shtPin = 0) _
                                        Or (.shtFuno = gCstCodeChNotSetFuNo And .shtPortno = gCstCodeChNotSetFuPort And .shtPin = gCstCodeChNotSetFuPin) _
                                        Or .shtPortno = gCstCodeChNotSetFuPort Then     ' 2015.10.23 Ver1.7.5 端子番号が65535の場合

                                            ''システムCH、通信CH、ワークCHの場合はチェックしない
                                            'If (.shtChType <> gCstCodeChTypeSystem) And _
                                            '  ((.shtOutPort = 0) And (.shtGwsPort = 0)) And _
                                            '   (gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk) = False) Then
                                            If (Not (.shtChType = gCstCodeChTypeDigital And .shtData = gCstCodeChDataTypeDigitalDeviceStatus)) And _
                                              ((.shtOutPort = 0) And (.shtGwsPort = 0)) And _
                                               (gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk) = False) Then

                                                ''アナログCHでデータタイプが排ガスの場合はチェックしない
                                                If Not ((.shtChType = gCstCodeChTypeAnalog) And _
                                                        (.shtData = gCstCodeChDataTypeAnalogExhAve Or _
                                                         .shtData = gCstCodeChDataTypeAnalogExhRepose Or _
                                                         .shtData = gCstCodeChDataTypeAnalogExtDev)) Then

                                                    ''メッセージ追加
                                                    ReDim Preserve strErrMsg(intErrCnt)
                                                    strErrMsg(intErrCnt) = IIf(mblnEnglish, "FU Address is not set. " & _
                                                                                            "[Info]Group=" & .shtGroupNo & _
                                                                                            " , CH NO=" & .shtChno & _
                                                                                            " , CH NAME=" & Trim(gGetString(.strChitem)) & _
                                                                                            " [Type]Input", _
                                                                                            "FUアドレスが設定されていません。" & _
                                                                                            "[情報]グループ=" & .shtGroupNo & _
                                                                                            " , チャンネル番号=" & .shtChno & _
                                                                                            " , チャンネル名称=" & Trim(gGetString(.strChitem)) & _
                                                                                            " [タイプ]入力側")
                                                    intErrCnt += 1

                                                End If

                                            End If

                                        Else

                                            ''FUアドレスが設定されている場合は一時保存
                                            Call aryFuAddressALL.Add(.shtFuno & "," & .shtPortno & "," & .shtPin & "," & i & "," & gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioInput, gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk)))
                                            'Call aryFuAddressALL.Add(.shtFuno & "," & .shtPortno & "," & .shtPin & "," & i & "," &  gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioInput, gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk)))
                                        End If

                                    End If
                                End If

                            End With

                            '==========
                            '' OUTPUT
                            '==========

                            Select Case .udtChCommon.shtChType
                                Case gCstCodeChTypeMotor

                                    '▼▼▼ 20110215 モーターCHの出力側はFUアドレスが設定されていなくてもエラーとしない ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                                    intFuNo = .MotorFuNo        ''Fu No
                                    intPortNo = .MotorPortNo    ''Port No
                                    intPin = .MotorPin          ''Pin

                                    With .udtChCommon

                                        ''FU番号、ポート、端子番号が全て 0 か 65535 の場合
                                        If (intFuNo = 0 And intPortNo = 0 And intPin = 0) _
                                        Or (intFuNo = gCstCodeChNotSetFuNo And intPortNo = gCstCodeChNotSetFuPort And intPin = gCstCodeChNotSetFuPin) Then

                                            ' ''メッセージ追加
                                            'ReDim Preserve strErrMsg(intErrCnt)
                                            'strErrMsg(intErrCnt) = IIf(mblnEnglish, "FU Address is not set. " & _
                                            '                                         "[Info]Group=" & .shtGroupNo & _
                                            '                                         " , CH NO=" & .shtChno & _
                                            '                                         " , CH NAME=" & Trim(gGetString(.strChitem)) & _
                                            '                                         " [Type]Output", _
                                            '                                         "FUアドレスが設定されていません。" & _
                                            '                                         "[情報]グループ=" & .shtGroupNo & _
                                            '                                         " , チャンネル番号=" & .shtChno & _
                                            '                                         " , チャンネル名称=" & Trim(gGetString(.strChitem)) & _
                                            '                                         " [タイプ]出力側")
                                            'intErrCnt += 1

                                        Else

                                            ''FUアドレスが設定されている場合は一時保存
                                            Call aryFuAddressALL.Add(intFuNo & "," & intPortNo & "," & intPin & "," & i & "," & gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioOutput, gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk)))
                                            Call aryFuAddressOut.Add(intFuNo & "," & intPortNo & "," & intPin & "," & i & "," & gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioOutput, gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk)))

                                        End If

                                    End With
                                    '-------------------------------------------------------------------------------------------------------------------------------------
                                    'intFuNo = .MotorFuNo        ''Fu No
                                    'intPortNo = .MotorPortNo    ''Port No
                                    'intPin = .MotorPin          ''Pin

                                    'With .udtChCommon

                                    '    ''FU番号、ポート、端子番号が全て 0 か 65535 の場合
                                    '    If (intFuNo = 0 And intPortNo = 0 And intPin = 0) _
                                    '    Or (intFuNo = gCstCodeChNotSetFuNo And intPortNo = gCstCodeChNotSetFuPort And intPin = gCstCodeChNotSetFuPin) Then

                                    '        ''メッセージ追加
                                    '        ReDim Preserve strErrMsg(intErrCnt)
                                    '        strErrMsg(intErrCnt) = IIf(mblnEnglish, "FU Address is not set. " & _
                                    '                                                 "[Info]Group=" & .shtGroupNo & _
                                    '                                                 " , CH NO=" & .shtChno & _
                                    '                                                 " , CH NAME=" & Trim(gGetString(.strChitem)) & _
                                    '                                                 " [Type]Output", _
                                    '                                                 "FUアドレスが設定されていません。" & _
                                    '                                                 "[情報]グループ=" & .shtGroupNo & _
                                    '                                                 " , チャンネル番号=" & .shtChno & _
                                    '                                                 " , チャンネル名称=" & Trim(gGetString(.strChitem)) & _
                                    '                                                 " [タイプ]出力側")
                                    '        intErrCnt += 1

                                    '    Else

                                    '        ''FUアドレスが設定されている場合は一時保存
                                    '        Call aryFuAddress.Add(gConvFuAddress(intFuNo, intPortNo, intPin) & "," & i & "," & gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioOutput))

                                    '    End If

                                    'End With
                                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                                Case gCstCodeChTypeValve

                                    With .udtChCommon

                                        Select Case .shtData

                                            Case gCstCodeChDataTypeValveDI_DO, _
                                                 gCstCodeChDataTypeValveDO

                                                intFuNo = gudt.SetChInfo.udtChannel(i).ValveDiDoFuNo        ''Fu No
                                                intPortNo = gudt.SetChInfo.udtChannel(i).ValveDiDoPortNo    ''Port No
                                                intPin = gudt.SetChInfo.udtChannel(i).ValveDiDoPin          ''Pin


                                                If .shtShareType = 2 Or .shtShareType = 3 Then      '' Ver1.11.8.5 2016.11.09  Remote/Shareの場合はﾁｪｯｸしない
                                                Else
                                                    ''FU番号、ポート、端子番号が全て 0 か 65535 の場合
                                                    If (intFuNo = 0 And intPortNo = 0 And intPin = 0) _
                                                    Or (intFuNo = gCstCodeChNotSetFuNo And intPortNo = gCstCodeChNotSetFuPort And intPin = gCstCodeChNotSetFuPin) Then

                                                        ''メッセージ追加
                                                        ReDim Preserve strErrMsg(intErrCnt)
                                                        strErrMsg(intErrCnt) = IIf(mblnEnglish, "FU Address is not set. " & _
                                                                                                "[Info]Group=" & .shtGroupNo & _
                                                                                                " , CH NO=" & .shtChno & _
                                                                                                " , CH NAME=" & Trim(gGetString(.strChitem)) & _
                                                                                                " [Type]Output", _
                                                                                                "FUアドレスが設定されていません。" & _
                                                                                                "[情報]グループ=" & .shtGroupNo & _
                                                                                                " , チャンネル番号=" & .shtChno & _
                                                                                                " , チャンネル名称=" & Trim(gGetString(.strChitem)) & _
                                                                                                " [タイプ]出力側")
                                                        intErrCnt += 1

                                                    Else

                                                        ''FUアドレスが設定されている場合は一時保存
                                                        Call aryFuAddressALL.Add(intFuNo & "," & intPortNo & "," & intPin & "," & i & "," & gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioOutput, gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk)))
                                                        Call aryFuAddressOut.Add(intFuNo & "," & intPortNo & "," & intPin & "," & i & "," & gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioOutput, gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk)))

                                                    End If
                                                End If

                                            Case gCstCodeChDataTypeValveAI_DO1, _
                                                 gCstCodeChDataTypeValveAI_DO2

                                                intFuNo = gudt.SetChInfo.udtChannel(i).ValveAiDoFuNo        ''Fu No
                                                intPortNo = gudt.SetChInfo.udtChannel(i).ValveAiDoPortNo    ''Port No
                                                intPin = gudt.SetChInfo.udtChannel(i).ValveAiDoPin          ''Pin

                                                If .shtShareType = 2 Or .shtShareType = 3 Then      '' Ver1.11.8.5 2016.11.09  Remote/Shareの場合はﾁｪｯｸしない
                                                Else

                                                    ''FU番号、ポート、端子番号が全て 0 か 65535 の場合
                                                    If (intFuNo = 0 And intPortNo = 0 And intPin = 0) _
                                                    Or (intFuNo = gCstCodeChNotSetFuNo And intPortNo = gCstCodeChNotSetFuPort And intPin = gCstCodeChNotSetFuPin) Then

                                                        ''メッセージ追加
                                                        ReDim Preserve strErrMsg(intErrCnt)
                                                        strErrMsg(intErrCnt) = IIf(mblnEnglish, "FU Address is not set. " & _
                                                                                                "[Info]Group=" & .shtGroupNo & _
                                                                                                " , CH NO=" & .shtChno & _
                                                                                                " , CH NAME=" & Trim(gGetString(.strChitem)) & _
                                                                                                " [Type]Output", _
                                                                                                "FUアドレスが設定されていません。" & _
                                                                                                "[情報]グループ=" & .shtGroupNo & _
                                                                                                " , チャンネル番号=" & .shtChno & _
                                                                                                " , チャンネル名称=" & Trim(gGetString(.strChitem)) & _
                                                                                                " [タイプ]出力側")
                                                        intErrCnt += 1

                                                    Else

                                                        ''FUアドレスが設定されている場合は一時保存
                                                        Call aryFuAddressALL.Add(intFuNo & "," & intPortNo & "," & intPin & "," & i & "," & gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioOutput, gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk)))
                                                        Call aryFuAddressOut.Add(intFuNo & "," & intPortNo & "," & intPin & "," & i & "," & gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioOutput, gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk)))

                                                    End If
                                                End If

                                            Case gCstCodeChDataTypeValveAI_AO1, _
                                                 gCstCodeChDataTypeValveAO_4_20

                                                intFuNo = gudt.SetChInfo.udtChannel(i).ValveAiAoFuNo        ''Fu No
                                                intPortNo = gudt.SetChInfo.udtChannel(i).ValveAiAoPortNo    ''Port No
                                                intPin = gudt.SetChInfo.udtChannel(i).ValveAiAoPin          ''Pin

                                                If .shtShareType = 2 Or .shtShareType = 3 Then      '' Ver1.11.8.5 2016.11.09  Remote/Shareの場合はﾁｪｯｸしない
                                                Else

                                                    ''FU番号、ポート、端子番号が全て 0 か 65535 の場合
                                                    If (intFuNo = 0 And intPortNo = 0 And intPin = 0) _
                                                    Or (intFuNo = gCstCodeChNotSetFuNo And intPortNo = gCstCodeChNotSetFuPort And intPin = gCstCodeChNotSetFuPin) Then

                                                        ''メッセージ追加
                                                        ReDim Preserve strErrMsg(intErrCnt)
                                                        strErrMsg(intErrCnt) = IIf(mblnEnglish, "FU Address is not set. " & _
                                                                                                "[Info]Group=" & .shtGroupNo & _
                                                                                                " , CH NO=" & .shtChno & _
                                                                                                " , CH NAME=" & Trim(gGetString(.strChitem)) & _
                                                                                                " [Type]Output", _
                                                                                                "FUアドレスが設定されていません。" & _
                                                                                                "[情報]グループ=" & .shtGroupNo & _
                                                                                                " , チャンネル番号=" & .shtChno & _
                                                                                                " , チャンネル名称=" & Trim(gGetString(.strChitem)) & _
                                                                                                " [タイプ]出力側")
                                                        intErrCnt += 1

                                                    Else

                                                        ''FUアドレスが設定されている場合は一時保存
                                                        Call aryFuAddressALL.Add(intFuNo & "," & intPortNo & "," & intPin & "," & i & "," & gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioOutput, gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk)))
                                                        Call aryFuAddressOut.Add(intFuNo & "," & intPortNo & "," & intPin & "," & i & "," & gGetFuBordType(gudt.SetChInfo.udtChannel(i), gEnmIOType.ioOutput, gBitCheck(.shtFlag1, gCstCodeChCommonFlagBitPosWk)))

                                                    End If
                                                End If

                                        End Select

                                    End With

                            End Select


                        End If

                    End With

                    If mblnCancelFlg Then Return

                Next

            End With

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking FU Address Set ... Success", " -FUアドレス設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking FU Address Set ... Failure", " -FUアドレス設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス重複確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : FUアドレスが重複していないか確認する
    '--------------------------------------------------------------------
    Private Sub mChkFuAddressOverlap(ByVal aryCheck As ArrayList)

        Try

            Dim strwk1() As String
            Dim strwk2() As String
            Dim strItem1 As String
            Dim strItem2 As String
            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            With gudt.SetChInfo

                ''FUアドレスが存在する場合
                If Not aryCheck Is Nothing Then

                    ''FUアドレス順に並べ替え
                    Call aryCheck.Sort()

                    ''上から順に１つ下と番号が同じかチェック
                    For i As Integer = 0 To aryCheck.Count - 1

                        ''最後の１つはチェックしない
                        If i = aryCheck.Count - 1 Then Exit For

                        ''FUアドレスとチャンネル配列番号を分割
                        strwk1 = aryCheck(i).ToString.Split(",")
                        strwk2 = aryCheck(i + 1).ToString.Split(",")

                        ''FUアドレスが同じ場合
                        If strwk1(0) = strwk2(0) And strwk1(1) = strwk2(1) And strwk1(2) = strwk2(2) Then

                            ''メッセージ作成
                            strItem1 = IIf(mblnEnglish, "[Info1]Group=" & .udtChannel(strwk1(3)).udtChCommon.shtGroupNo & _
                                                        " , CH NO=" & gGetString(.udtChannel(strwk1(3)).udtChCommon.shtChno), _
                                                        "[情報１]グループ=" & .udtChannel(strwk1(3)).udtChCommon.shtGroupNo & _
                                                        " , チャンネル番号=" & gGetString(.udtChannel(strwk1(3)).udtChCommon.shtChno))
                            strItem2 = IIf(mblnEnglish, "[Info2]Group=" & .udtChannel(strwk2(3)).udtChCommon.shtGroupNo & _
                                                        " , CH NO=" & gGetString(.udtChannel(strwk2(3)).udtChCommon.shtChno), _
                                                        "[情報２]グループ=" & .udtChannel(strwk2(3)).udtChCommon.shtGroupNo & _
                                                        " , チャンネル番号=" & gGetString(.udtChannel(strwk2(3)).udtChCommon.shtChno))

                            ' ''メッセージ作成
                            'strItem1 = IIf(mblnEnglish, "[Info1]Group=" & .udtChannel(strwk1(1)).udtChCommon.shtGroupNo & _
                            '                            " , CH NO=" & gGetString(.udtChannel(strwk1(1)).udtChCommon.shtChno), _
                            '                            "[情報１]グループ=" & .udtChannel(strwk1(1)).udtChCommon.shtGroupNo & _
                            '                            " , チャンネル番号=" & gGetString(.udtChannel(strwk1(1)).udtChCommon.shtChno))
                            'strItem2 = IIf(mblnEnglish, "[Info2]Group=" & .udtChannel(strwk2(1)).udtChCommon.shtGroupNo & _
                            '                            " , CH NO=" & gGetString(.udtChannel(strwk2(1)).udtChCommon.shtChno), _
                            '                            "[情報２]グループ=" & .udtChannel(strwk2(1)).udtChCommon.shtGroupNo & _
                            '                            " , チャンネル番号=" & gGetString(.udtChannel(strwk2(1)).udtChCommon.shtChno))

                            ''メッセージ追加
                            ReDim Preserve strErrMsg(intErrCnt)
                            strErrMsg(intErrCnt) = IIf(mblnEnglish, "Output FU Address [" & strwk1(0) & strwk1(1) & strwk1(2) & "] is overlaps. " & strItem1 & " " & strItem2, _
                                                                    "出力側のFUアドレス [" & strwk1(0) & strwk1(1) & strwk1(2) & "] は重複しています。" & strItem1 & " " & strItem2)
                            intErrCnt += 1

                        End If

                    Next
                End If
            End With

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking FU Address overlaps ... Success", " -FUアドレス重複確認 ... OK")
            Else

                Call mAddMsgText(" -Checking FU Address overlaps ... Failure", " -FUアドレス重複確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : FUアドレス情報確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : FUアドレスが情報が正しいか確認する
    '--------------------------------------------------------------------
    Private Sub mChkFuBoardType(ByVal aryCheck As ArrayList)

        Try

            Dim intFuNo As Integer
            Dim intFuPort As Integer
            Dim intFuPin As Integer
            Dim strwk() As String
            Dim strItem As String
            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            With gudt.SetChInfo

                ''FUアドレスが存在する場合
                If Not aryCheck Is Nothing Then

                    ''FUアドレスとボード種類が正しいかチェック
                    For i As Integer = 0 To aryCheck.Count - 1

                        ''FUアドレスとチャンネル配列番号を分割
                        strwk = aryCheck(i).ToString.Split(",")

                        intFuNo = strwk(0)
                        intFuPort = strwk(1)
                        intFuPin = strwk(2)



                        ' ''FUアドレスを分割
                        'Call gSeparateFuAddress(strwk(0), intFuNo, intFuPort, intFuPin)

                        ''対応ボードなしの場合はチェックしない
                        If strwk(4) = gCstCodeFuSlotTypeNothing Then
                            'If strwk(2) = gCstCodeFuSlotTypeNothing Then
                            'OK
                        Else

                            ''対応ボードと実際に設定されているボードが一致しない場合
                            If strwk(4) <> gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intFuPort - 1).shtType Then
                                'If strwk(2) <> gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intFuPort - 1).shtType Then

                                strItem = IIf(mblnEnglish, "[Info]Group=" & .udtChannel(strwk(3)).udtChCommon.shtGroupNo & _
                                                           " , CH NO=" & .udtChannel(strwk(3)).udtChCommon.shtChno & _
                                                           " , FU Address=" & strwk(0) & strwk(1) & strwk(2) & _
                                                           " , ChType=" & .udtChannel(strwk(3)).udtChCommon.shtChType & _
                                                           " , ChDataType=" & .udtChannel(strwk(3)).udtChCommon.shtData & _
                                                           " , FuBoardType=" & gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intFuPort - 1).shtType, _
                                                           "[情報]グループ=" & .udtChannel(strwk(3)).udtChCommon.shtGroupNo & _
                                                           " , チャンネル番号=" & .udtChannel(strwk(3)).udtChCommon.shtChno & _
                                                           " , FUアドレス=" & strwk(0) & strwk(1) & strwk(2) & _
                                                           " , チャンネルタイプ=" & .udtChannel(strwk(3)).udtChCommon.shtChType & _
                                                           " , チャンネルデータタイプ=" & .udtChannel(strwk(3)).udtChCommon.shtData & _
                                                           " , FUボード種別=" & gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intFuPort - 1).shtType)

                                'strItem = IIf(mblnEnglish, "[Info]Group=" & .udtChannel(strwk(1)).udtChCommon.shtGroupNo & _
                                '                           " , CH NO=" & .udtChannel(strwk(1)).udtChCommon.shtChno & _
                                '                           " , FU Address=" & strwk(0) & _
                                '                           " , ChType=" & .udtChannel(strwk(1)).udtChCommon.shtChType & _
                                '                           " , ChDataType=" & .udtChannel(strwk(1)).udtChCommon.shtData & _
                                '                           " , FuBoardType=" & gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intFuPort - 1).shtType, _
                                '                           "[情報]グループ=" & .udtChannel(strwk(1)).udtChCommon.shtGroupNo & _
                                '                           " , チャンネル番号=" & .udtChannel(strwk(1)).udtChCommon.shtChno & _
                                '                           " , FUアドレス=" & strwk(0) & _
                                '                           " , チャンネルタイプ=" & .udtChannel(strwk(1)).udtChCommon.shtChType & _
                                '                           " , チャンネルデータタイプ=" & .udtChannel(strwk(1)).udtChCommon.shtData & _
                                '                           " , FUボード種別=" & gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intFuPort - 1).shtType)

                                ''メッセージ追加
                                Call mSetErrString("FU Board type unmatched. " & strItem, "FUボード種別が一致しません。" & strItem, intErrCnt, strErrMsg)

                            End If

                        End If
                    Next
                End If
            End With

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking FU Board Type ... Success", " -FUボード種別確認 ... OK")
            Else

                Call mAddMsgText(" -Checking FU Board Type ... Failure", " -FUボード種別確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'Ver2.0.3.1
    '--------------------------------------------------------------------
    ' 機能      : FUアドレス端子配置確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 配置された端子がオーバーしてないか確認する
    '--------------------------------------------------------------------
    Private Sub mChkFuTermCount()

        Try
            Dim aryCheck As New ArrayList

            Dim strChNo As String = ""
            Dim strListIndex As String = ""
            Dim x As Integer
            Dim i As Integer
            Dim intFuNo, intPortNo, intPin, intPinNo As Integer

            Dim CHViewFlg As Boolean

            Dim row As Integer

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing


            'Chinfoをチャンネル番号順に並べ替え
            gMakeChNoOrderSort(aryCheck)

            For x = 0 To aryCheck.Count - 1 Step 1
                gGetChNoOrder(aryCheck, x, strChNo, strListIndex)

                i = Val(strListIndex)
                With gudt.SetChInfo.udtChannel(i)

                    CHViewFlg = False

                    Select Case .udtChCommon.shtChType
                        Case gCstCodeChTypeAnalog       'アナログ
                            If .udtChCommon.shtData = gCstCodeChDataTypeAnalogModbus Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogExtDev _
                                   Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhAve Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogJacom Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypeAnalogLatitude Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogLongitude Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogJacom55 Then
                                CHViewFlg = True
                            End If
                        Case gCstCodeChTypeDigital      'デジタル
                            If .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacomNC _
                                  Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacomNO Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalModbusNC Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalModbusNO Or _
                                  .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacom55NC Or .udtChCommon.shtData = gCstCodeChDataTypeDigitalJacom55NO Then
                                CHViewFlg = True
                            End If
                        Case gCstCodeChTypeMotor        'モーター
                            If .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom Or .udtChCommon.shtData = gCstCodeChDataTypeMotorDeviceJacom55 Then
                                CHViewFlg = True
                            End If
                            'Ver2.0.5.9
                            '通信も除外
                            If _
                                (.udtChCommon.shtData >= gCstCodeChDataTypeMotorRManRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorRManRunK) Or _
                                (.udtChCommon.shtData >= gCstCodeChDataTypeMotorRAbnorRun And .udtChCommon.shtData <= gCstCodeChDataTypeMotorRAbnorRunK) Or _
                                (.udtChCommon.shtData = gCstCodeChDataTypeMotorRDevice) _
                            Then
                                CHViewFlg = True
                            End If

                        Case gCstCodeChTypeValve        'バルブ
                            If .udtChCommon.shtData = gCstCodeChDataTypeValveJacom Or .udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Then
                                CHViewFlg = True
                            End If
                        Case gCstCodeChTypeComposite    'コンポジット
                            '処理無し
                        Case gCstCodeChTypePulse        'パルス
                            If .udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Or _
                                .udtChCommon.shtData >= gCstCodeChDataTypePulseRevoExtDev Then
                                CHViewFlg = True
                            End If
                        Case Else
                            '処理無し
                    End Select


                    If CHViewFlg = False Then
                        For j As Integer = 0 To 1   'InとOutの２回分 LOOP

                            If j = 1 Then
                                If (.udtChCommon.shtChType = gCstCodeChTypeMotor) _
                                Or (.udtChCommon.shtChType = gCstCodeChTypeValve) Then
                                    'Motor、Valve は 外部出力FU Addressがある
                                Else
                                    Exit For
                                End If
                            End If

                            'FU番号, FUポート番号
                            row = 65535
                            If j = 0 Then
                                '入力
                                If (.udtChCommon.shtFuno <> gCstCodeChCommonFuNoNothing) And (.udtChCommon.shtPortno <> gCstCodeChCommonPortNoNothing) And (.udtChCommon.shtPortno <> 0) Then
                                    row = gGet2Byte(.udtChCommon.shtPin)    '計測点番号
                                    intFuNo = .udtChCommon.shtFuno
                                    intPortNo = .udtChCommon.shtPortno
                                    intPin = .udtChCommon.shtPin
                                    intPinNo = .udtChCommon.shtPinNo        '計測点個数
                                End If

                            ElseIf j = 1 Then
                                ''出力
                                If .udtChCommon.shtChType = gCstCodeChTypeMotor Then        'モーターCH
                                    intFuNo = .MotorFuNo            'Fu No
                                    intPortNo = .MotorPortNo        'Port No
                                    intPin = .MotorPin              'Pin
                                    intPinNo = .MotorPinNo          '計測点個数
                                ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then    'バルブCH
                                    If .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveDO Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveJacom Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveJacom55 Or _
                                       .udtChCommon.shtData = gCstCodeChDataTypeValveExt Then
                                        'DI->DO
                                        intFuNo = .ValveDiDoFuNo        'Fu No
                                        intPortNo = .ValveDiDoPortNo    'Port No
                                        intPin = .ValveDiDoPin          'Pin
                                        intPinNo = .ValveDiDoPinNo      '計測点個数
                                    ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                           .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then
                                        'AI->DO
                                        intFuNo = .ValveAiDoFuNo        'Fu No
                                        intPortNo = .ValveAiDoPortNo    'Port No
                                        intPin = .ValveAiDoPin          'Pin
                                        intPinNo = .ValveAiDoPinNo      '計測点個数
                                    ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                           .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Or _
                                           .udtChCommon.shtData = gCstCodeChDataTypeValveAO_4_20 Then
                                        'AI->AO
                                        intFuNo = .ValveAiAoFuNo        'Fu No
                                        intPortNo = .ValveAiAoPortNo    'Port No
                                        intPin = .ValveAiAoPin          'Pin
                                        intPinNo = .ValveAiAoPinNo      '計測点個数
                                    End If
                                End If

                                'Ver2.0.4.0
                                'Portに０が入っている場合も対象外
                                If (intFuNo <> gCstCodeChCommonFuNoNothing) And (intPortNo <> gCstCodeChCommonPortNoNothing) And (intPortNo <> 0) Then
                                    row = intPin    ''計測点番号
                                End If
                            End If

                            '複数Pin使用CHだが基板MAXを超えるならエラー
                            If row <> 65535 Then
                                Dim intHanPin As Integer = 0
                                Dim intHanMax As Integer = 0
                                '>>>基板MAX取得
                                Dim intType As Integer = gudt.SetFu.udtFu(intFuNo).udtSlotInfo(intPortNo - 1).shtType
                                Select Case intType
                                    Case gCstCodeFuSlotTypeDO, gCstCodeFuSlotTypeDI     'DO, DI
                                        intHanMax = gCstFuSlotMaxDO
                                    Case gCstCodeFuSlotTypeAO                           'AO
                                        intHanMax = gCstFuSlotMaxAO
                                    Case gCstCodeFuSlotTypeAI_2                         '2線式
                                        intHanMax = gCstFuSlotMaxAI_2Line
                                    Case gCstCodeFuSlotTypeAI_3                         '3線式(3行毎)
                                        intHanMax = gCstFuSlotMaxAI_3Line * 3
                                    Case gCstCodeFuSlotTypeAI_1_5, gCstCodeFuSlotTypeAI_K   '1-5V, K
                                        intHanMax = gCstFuSlotMaxAI_1_5
                                    Case gCstCodeFuSlotTypeAI_4_20                      '4-20mA
                                        intHanMax = gCstFuSlotMaxAI_4_20
                                End Select
                                '>>>PIN数取得 該当外は初期値のまま＝０となる
                                Select Case .udtChCommon.shtChType
                                    Case gCstCodeChTypeMotor        'モーター
                                        If intType = gCstCodeFuSlotTypeDI Then
                                            'DI
                                            intHanPin = .udtChCommon.shtPinNo
                                        ElseIf intType = gCstCodeFuSlotTypeDO Then
                                            'DO
                                            intHanPin = .MotorPinNo
                                        End If
                                    Case gCstCodeChTypeValve        'バルブ
                                        If intType = gCstCodeFuSlotTypeDI Then
                                            'DI
                                            intHanPin = .udtChCommon.shtPinNo
                                        ElseIf intType = gCstCodeFuSlotTypeAI_1_5 Or _
                                               intType = gCstCodeFuSlotTypeAI_4_20 Then
                                            'AI
                                            intHanPin = .udtChCommon.shtPinNo
                                        ElseIf intType = gCstCodeFuSlotTypeDO Then
                                            'DO
                                            If .udtChCommon.shtData = gCstCodeChDataTypeValveDI_DO Or _
                                               .udtChCommon.shtData = gCstCodeChDataTypeValveDO Then
                                                intHanPin = .ValveDiDoPinNo
                                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then
                                                intHanPin = .ValveAiDoPinNo
                                            End If
                                        ElseIf intType = gCstCodeFuSlotTypeAO Then
                                            'AO
                                            intHanPin = .ValveAiAoPinNo
                                        End If
                                    Case gCstCodeChTypeComposite    'コンポジット
                                        intHanPin = .udtChCommon.shtPinNo
                                End Select
                                '>>>判定
                                '指定行+(PIN数-1) > MAX行-1 ならエラー
                                If row + (intHanPin - 1) > intHanMax Then
                                    'Error
                                    'メッセージ追加
                                    Call mSetErrString( _
                                        "FU Pin count over. " & vbCrLf _
                                        & "    [FUadr]" & intFuNo.ToString & "-" & intPortNo.ToString & "-" & intPin & vbCrLf _
                                        & "    [CHno]" & .udtChCommon.shtChno, _
                                        "FU Pin数がオーバーしています。" & vbCrLf _
                                        & "    [FUadr]" & intFuNo.ToString & "-" & intPortNo.ToString & "-" & intPin & vbCrLf _
                                        & "    [CHno]" & .udtChCommon.shtChno, _
                                        intErrCnt, strErrMsg)
                                    '該当無しと同じ処置とする
                                    row = 65535
                                End If
                            End If
                        Next j
                    End If
                End With
            Next x


            '結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking FU Pin Count ... Success", " -FU Pin数 ... OK")
            Else
                Call mAddMsgText(" -Checking FU Pin Count ... Failure", " -FU Pin数 ... エラー")
                ''失敗時はエラー内容を追記
                For i = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'Ver2.0.6.0
    '--------------------------------------------------------------------
    ' 機能      : FUアドレス基板種類確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : DO,AO基板でないFuｱﾄﾞﾚｽが計測点のOUT_FUｱﾄﾞﾚｽに入力されていないか確認する
    '--------------------------------------------------------------------
    Private Sub mChkFuOutAdr()

        Try
            Dim aryFUout As ArrayList = New ArrayList
            Dim strFUadr As String = ""
            Dim i As Integer
            Dim j As Integer
            Dim blOUTch As Boolean
            Dim blDel As Boolean = False

            Dim strTerFUadr() As String = Nothing
            Dim intFU As Integer = 0
            Dim intSlot As Integer = 0
            Dim intTerFU As Integer = 0
            Dim intTerSlot As Integer = 0

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing


            '>>>OUT基板のFUｱﾄﾞﾚｽを格納(DO基板とAO基板)
            With gudt.SetFu
                For i = 0 To UBound(.udtFu) Step 1
                    For j = 0 To UBound(.udtFu(i).udtSlotInfo) Step 1
                        If .udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeDO Or _
                            .udtFu(i).udtSlotInfo(j).shtType = gCstCodeFuSlotTypeAO Then
                            strFUadr = i.ToString & "," & (j + 1).ToString
                            aryFUout.Add(strFUadr)
                        End If
                    Next j
                Next i
            End With

            'Ver2.0.6.0 DEL ｺﾝﾊﾟｲﾙ時にエラーでひっかけるのみにとどめる
            '>>>計測点リスト全点でOUT設定があるCHのFU判定
            With gudt.SetChInfo
                '計測点リスト全点ﾙｰﾌﾟ
                For i = 0 To UBound(.udtChannel) Step 1
                    '計測点リストが
                    ' モーター
                    ' バルブ DIDO,AIAO,DIAO,AO,DO
                    'の場合のみ対象。FUｱﾄﾞﾚｽを取得しておく
                    blOUTch = False
                    Select Case .udtChannel(i).udtChCommon.shtChType
                        Case gCstCodeChTypeMotor
                            'モーター
                            blOUTch = True
                            intFU = .udtChannel(i).MotorFuNo
                            intSlot = .udtChannel(i).MotorPortNo
                        Case gCstCodeChTypeValve
                            'バルブ
                            Select Case .udtChannel(i).udtChCommon.shtData
                                Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO
                                    'DIDO
                                    blOUTch = True
                                    intFU = .udtChannel(i).ValveDiDoFuNo
                                    intSlot = .udtChannel(i).ValveDiDoPortNo
                                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
                                    'AIDO
                                    blOUTch = True
                                    intFU = .udtChannel(i).ValveAiDoFuNo
                                    intSlot = .udtChannel(i).ValveAiDoPortNo
                                Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2, gCstCodeChDataTypeValveAO_4_20
                                    'AIAO
                                    blOUTch = True
                                    intFU = .udtChannel(i).ValveAiAoFuNo
                                    intSlot = .udtChannel(i).ValveAiAoPortNo
                            End Select
                    End Select
                    'アドレスがFFなら何もしない
                    If blOUTch = True Then
                        If intFU = &HFFFF And intSlot = &HFFFF Then
                            blOUTch = False
                        End If
                    End If

                    If blOUTch = True Then
                        '基板に同じFUがあるならOK。そうでないならFUをｸﾘｱ
                        blDel = False
                        For j = 0 To aryFUout.Count - 1 Step 1
                            '基板のFuｱﾄﾞﾚｽを1件取り出し
                            strFUadr = aryFUout(j)
                            strTerFUadr = strFUadr.Split(",")
                            intTerFU = CInt(strTerFUadr(0))
                            intTerSlot = CInt(strTerFUadr(1))
                            If intFU = intTerFU And intSlot = intTerSlot Then
                                blDel = True
                                Exit For
                            End If
                        Next j

                        If blDel = False Then
                            '該当基板が無いためメッセージ
                            'Error
                            'メッセージ追加
                            Call mSetErrString( _
                                "Not Out Term. " & vbCrLf _
                                & "    [FUadr]" & intFU.ToString & "-" & intSlot.ToString & vbCrLf _
                                & "    [CHno]" & .udtChannel(i).udtChCommon.shtChno, _
                                "OUT基板ではありません。" & vbCrLf _
                                & "    [FUadr]" & intFU.ToString & "-" & intSlot.ToString & vbCrLf _
                                & "    [CHno]" & .udtChannel(i).udtChCommon.shtChno, _
                                intErrCnt, strErrMsg)
                        End If
                    End If
                Next i
            End With


            '結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking FU Out Term ... Success", " -FU OUT基板 ... OK")
            Else
                Call mAddMsgText(" -Checking FU Out Term ... Failure", " -FU OUT基板 ... エラー")
                ''失敗時はエラー内容を追記
                For i = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#End Region

#Region "パルス設定チェック"

    'Ver1.10.5 2016.05.09 追加
    '--------------------------------------------------------------------
    ' 機能      : パルス設定確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : パルス設定が情報が正しいか確認する
    '--------------------------------------------------------------------
    Private Sub mChkChPulse2()

        Try

            ''各設定値
            Dim intDifilter As Integer = 0

            'Ver2.0.0.9
            Dim intALMlevel As Integer = 0

            ''各設定有無フラグ
            Dim blnPulse As Boolean = False

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim blnErrFlg As Boolean = False
            Dim strMsgEng As String = ""
            Dim strMsgJpn As String = ""

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                blnPulse = False

                With gudt.SetChInfo.udtChannel(i)

                    ''チャンネルが設定されている場合
                    If .udtChCommon.shtChno <> 0 Then

                        ''==========================
                        ''先に比較用の値を取得する
                        ''==========================
                        Select Case .udtChCommon.shtChType
                            Case gCstCodeChTypePulse

                                blnPulse = True

                                '' ﾊﾟﾙｽCH
                                If .udtChCommon.shtData < gCstCodeChDataTypePulseRevoTotalHour Or _
                                    .udtChCommon.shtData = gCstCodeChDataTypePulseExtDev Then
                                    intDifilter = .PulseDiFilter

                                    'Ver2.0.0.9
                                    intALMlevel = .PulseLRMode
                                Else
                                    intDifilter = .RevoDiFilter

                                    'Ver2.0.0.9
                                    intALMlevel = .RevoLRMode
                                End If

                        End Select

                        If blnPulse = True Then

                            ' DIﾌｨﾙﾀｰ値ﾁｪｯｸ
                            If intDifilter <> 2 Then
                                Call mSetErrString("PULSE FILTER setting is not 2." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "PULSE FILTER 設定値が2ではありません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)
                            End If

                            'Ver2.0.0.9
                            'AlarmLevelチェック
                            If gudt.SetSystem.udtSysOps.shtLRMode = 0 Then
                                'AlarmLevel=NONEの場合、0であること
                                If intALMlevel <> 0 Then
                                    Call mSetErrString("Alarm Level is not 0. " & _
                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                    "Alarm Levelが0になっていません。" & _
                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                       intErrCnt, strErrMsg)
                                End If
                            Else
                                'Flag2の1bitがonならば、チェック
                                If gBitCheck(.udtChCommon.shtFlag2, 1) = True Then 'AL
                                    If intALMlevel = 0 Then
                                        Call mSetErrString("Alarm Level is 0. " & _
                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                    "Alarm Levelが0になっています。" & _
                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                       intErrCnt, strErrMsg)
                                    End If
                                End If
                            End If


                        End If

                    End If

                End With
            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Pulse Setting ... Success", " -パルス設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Pulse Setting ... Failure", " -パルス設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


#Region "チャンネル共通部チェック"
    'Ver2.0.0.4
    '--------------------------------------------------------------------
    ' 機能      : チャンネル共通部チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : チャンネル共通部のチェックをここで行う
    '--------------------------------------------------------------------
    Private Sub mChkChCommon()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing


            ''本関数内全てのチェックはチャンネルが設定されている場合のみ
            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then
                        'SC ONだが、RLもONならエラー
                        If gBitCheck(.udtChCommon.shtFlag1, 1) = True And _
                            gBitCheck(.udtChCommon.shtFlag2, 0) = True Then
                            Call mSetErrString("SC ON and RL ON" & _
                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                               " , CH NO=" & .udtChCommon.shtChno, _
                                               "SCとRLが両方ともONです。" & _
                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                               intErrCnt, strErrMsg)
                        End If
                    End If

                End With

            Next


            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Channel Common ... Success", " -チャンネル共通部確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Channel Common ... Failure", " -チャンネル共通部確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region



#Region "モーター設定チェック"

    'Ver1.10.5 2016.05.09 追加
    '--------------------------------------------------------------------
    ' 機能      : モーター設定確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : モーター設定が情報が正しいか確認する
    '--------------------------------------------------------------------
    Private Sub mChkChMotor()

        Try

            ''各設定値
            Dim intDifilter As Integer = 0

            ''各設定有無フラグ
            Dim blnMotor As Boolean = False

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim blnErrFlg As Boolean = False
            Dim strMsgEng As String = ""
            Dim strMsgJpn As String = ""

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                blnMotor = False

                With gudt.SetChInfo.udtChannel(i)

                    ''チャンネルが設定されている場合
                    If .udtChCommon.shtChno <> 0 Then

                        ''==========================
                        ''先に比較用の値を取得する
                        ''==========================
                        Select Case .udtChCommon.shtChType
                            Case gCstCodeChTypeMotor

                                blnMotor = True
                                intDifilter = .MotorDiFilter
                        End Select

                        If blnMotor = True Then

                            ' DIﾌｨﾙﾀｰ値ﾁｪｯｸ
                            If intDifilter <> 12 Then
                                Call mSetErrString("MOTOR FILTER setting is not 12." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "MOTOR FILTER 設定値が12ではありません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)
                            End If

                            'Ver2.0.0.9
                            'AlarmLevelチェック
                            If gudt.SetSystem.udtSysOps.shtLRMode = 0 Then
                                'AlarmLevel=NONEの場合、0であること
                                If .MotorLRMode <> 0 Then
                                    Call mSetErrString("Alarm Level is not 0. " & _
                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                    "Alarm Levelが0になっていません。" & _
                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                       intErrCnt, strErrMsg)
                                End If
                            Else
                                'Flag2の1bitがonならば、チェック
                                If gBitCheck(.udtChCommon.shtFlag2, 1) = True Then 'AL
                                    If .MotorLRMode = 0 Then
                                        Call mSetErrString("Alarm Level is 0. " & _
                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                    "Alarm Levelが0になっています。" & _
                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                       intErrCnt, strErrMsg)
                                    End If
                                End If
                            End If

                            
                        End If

                    End If

                End With
            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Motor Setting ... Success", " -モーター設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Motor Setting ... Failure", " -モーター設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "デジタル設定チェック"

    '2015/5/13 T.Ueki 新規追加
    '--------------------------------------------------------------------
    ' 機能      : デジタル設定確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : デジタル設定が情報が正しいか確認する
    '--------------------------------------------------------------------
    Private Sub mChkChDigital()

        Try

            ''各設定値
            Dim intUse As Integer = 0
            Dim intDifilter As Integer = 0  '未使用
            Dim intExtG As Integer = 0
            Dim intDly As Integer = 0
            Dim intGRep1 As Integer = 0
            Dim intGRep2 As Integer = 0
            Dim intmRepSet As Integer = 0

            Dim intAlarmLevel As Integer = 0


            ''各設定有無フラグ
            Dim blnDigital As Boolean = False
            Dim blnUse As Boolean = False
            Dim blnExtG As Boolean = False
            Dim blnDly As Boolean = False
            Dim blnGRep1 As Boolean = False
            Dim blnGRep2 As Boolean = False
            Dim blnmRepSet As Boolean = False

            Dim DlySetFlg As Boolean = False

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim blnErrFlg As Boolean = False
            Dim strMsgEng As String = ""
            Dim strMsgJpn As String = ""

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                blnDigital = False
                blnExtG = False
                blnDly = False
                blnGRep1 = False
                blnGRep2 = False
                blnmRepSet = False

                With gudt.SetChInfo.udtChannel(i)


                    ''チャンネルが設定されている場合   '' Ver1.11.9.8 2016.12.15 
                    ''If .udtChCommon.shtChno <> 0 Then
                    If .udtChCommon.shtChno = 0 Then
                        Continue For
                    End If

                    

                    ''==========================
                    ''先に比較用の値を取得する
                    ''==========================
                    Select Case .udtChCommon.shtChType
                        Case gCstCodeChTypeDigital

                            blnDigital = True

                            ''アラーム有無
                            intUse = .DigitalUse
                            intDifilter = .DigitalDiFilter
                            blnUse = IIf(.DigitalUse = 0, False, True)

                            intExtG = .udtChCommon.shtExtGroup
                            intDly = .udtChCommon.shtDelay
                            intGRep1 = .udtChCommon.shtGRepose1
                            intGRep2 = .udtChCommon.shtGRepose2
                            intmRepSet = .udtChCommon.shtM_ReposeSet

                            If .udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then
                                intAlarmLevel = .SystemLRMode
                            Else
                                intAlarmLevel = .DigitalLRMode
                            End If

                    End Select

                    ' Ver1.11.9.8 2016.12.15 
                    ''If blnDigital = True Then
                    If blnDigital = False Then
                        Continue For
                    End If

                    '' Ver1.10.5 2016.05.09
                    '' DIﾌｨﾙﾀｰ値ﾁｪｯｸ
                    If .udtChCommon.shtData <> gCstCodeChDataTypeDigitalDeviceStatus And intDifilter <> 12 Then
                        Call mSetErrString("DIGITAL FILTER setting is not 12." & _
                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                           " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                           "DIGITAL FILTER 設定値が12ではありません" & _
                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                           " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                           intErrCnt, strErrMsg)

                    End If
                    ''//

                    'Ext_G が設定されている場合
                    If blnUse = True Then
                        'Ver2.0.3.9 全部使われてないならﾁｪｯｸしない
                        'Ver2.0.4.1 前へ戻す 何かが入ってないならエラー
                        If intDly = gCstCodeChCommonExtGroupNothing Or intGRep1 = gCstCodeChCommonExtGroupNothing Or intGRep2 = gCstCodeChCommonExtGroupNothing Or intmRepSet = 0 Then
                            'If intDly = gCstCodeChCommonExtGroupNothing And intGRep1 = gCstCodeChCommonExtGroupNothing And intGRep2 = gCstCodeChCommonExtGroupNothing And intmRepSet = 0 Then
                            'Else
                            If intDly = gCstCodeChCommonExtGroupNothing Or intGRep1 = gCstCodeChCommonExtGroupNothing Or intGRep2 = gCstCodeChCommonExtGroupNothing Or intmRepSet = 0 Then
                                Call mSetErrString("EXT GROUP setting is incorrect." & _
                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                               " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                               "EXT GROUPが正しく設定されていません" & _
                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                               " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                               intErrCnt, strErrMsg)
                            End If
                        End If

                        'Ver2.0.0.9 チェック方法変更
                        ''ﾛｲﾄﾞ表示　ｱﾗｰﾑﾚﾍﾞﾙ追加     2015.11.12 Ver1.7.8
                        '' 参照するﾌﾗｸﾞを間違えていたので修正　　2015.11.18  Ver1.8.1
                        ''If gudt.SetSystem.udtSysSystem.shtLanguage = 1 And .DigitalLRMode = 0 Then
                        'If gudt.SetSystem.udtSysOps.shtLRMode = 1 And .DigitalLRMode = 0 Then
                        '    Call mSetErrString("ALM LEVEL setting is nothing." & _
                        '                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                        '                   "ALM LEVELが設定されていません" & _
                        '                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                        '                   intErrCnt, strErrMsg)
                        'End If
                        'AlarmLevelチェック
                        If gudt.SetSystem.udtSysOps.shtLRMode = 0 Then
                            'AlarmLevel=NONEの場合、0であること
                            If intAlarmLevel <> 0 Then
                                Call mSetErrString("Alarm Level is not 0. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                "Alarm Levelが0になっていません。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                   intErrCnt, strErrMsg)
                            End If
                        Else
                            'Flag2の1bitがonならば、チェック
                            If gBitCheck(.udtChCommon.shtFlag2, 1) = True Then 'AL
                                If intAlarmLevel = 0 Then
                                    Call mSetErrString("Alarm Level is 0. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                "Alarm Levelが0になっています。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                   intErrCnt, strErrMsg)
                                End If
                            End If
                        End If





                        '' Ver1.10.7 2016.06.14  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                        Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                            intErrCnt, strErrMsg)
                    Else

                        ' Ver1.10.8 2016.06.28 Ext.G が255(設定なし)以外の場合  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                        If intExtG <> gCstCodeChCommonExtGroupNothing Then
                            Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                            intErrCnt, strErrMsg)

                            '' Ver1.11.9.8 2016.12.15 警報有無ﾌﾗｸﾞがｾｯﾄされない場合が存在するので表示
                            Call mSetErrString("INFO Digital AlmUse Flag Set" & _
                                           " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                           "情報：ﾃﾞｼﾞﾀﾙ AlmUseﾌﾗｸﾞ 設定" & _
                                           " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                           intErrCnt, strErrMsg)
                        End If
                        ''//

                        'Ext_G が未設定の場合
                        If intDly <> gCstCodeChCommonExtGroupNothing Or intGRep1 <> gCstCodeChCommonExtGroupNothing Or intGRep2 <> gCstCodeChCommonExtGroupNothing Or intmRepSet <> 0 Then

                            '例外　Delayのみ設定されている場合、グループリポーズテーブルに設定されている場合は除外　T.Ueki 2015/6/3
                            If intDly <> gCstCodeChCommonExtGroupNothing And intGRep1 = gCstCodeChCommonExtGroupNothing And intGRep2 = gCstCodeChCommonExtGroupNothing And intmRepSet = 0 Then

                                For j As Integer = 0 To UBound(gudt.SetChGroupRepose.udtRepose)

                                    If .udtChCommon.shtChid = gConvChNoToChId(gudt.SetChGroupRepose.udtRepose(j).shtChId) Then
                                        DlySetFlg = True
                                        Exit For
                                    End If

                                    ''チェックマークがついていないかつ47行目の格納時にFor文を抜ける 2018.12.13 倉重
                                    If g_bytGREPNUM = 0 And j = 47 Then
                                        Exit For
                                    End If

                                Next

                                If DlySetFlg = False Then
                                    Call mSetErrString("EXT ALARM setting is incorrect." & _
                                          "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                          " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                          "警報設定が正しくが設定されていません" & _
                                          "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                          " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                          intErrCnt, strErrMsg)
                                End If

                                DlySetFlg = False

                                'Ver2.0.0.9 チェック方法変更
                                ''ﾛｲﾄﾞ表示　ｱﾗｰﾑﾚﾍﾞﾙ追加     2015.11.12 Ver1.7.8
                                '' 参照するﾌﾗｸﾞを間違えていたので修正　　2015.11.18  Ver1.8.1
                                ''If gudt.SetSystem.udtSysSystem.shtLanguage = 1 And .DigitalLRMode = 0 Then
                                'If gudt.SetSystem.udtSysOps.shtLRMode = 1 And .DigitalLRMode = 0 Then
                                '    Call mSetErrString("ALM LEVEL setting is nothing." & _
                                '                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                '                   "ALM LEVELが設定されていません" & _
                                '                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                '                   intErrCnt, strErrMsg)
                                'End If
                                'AlarmLevelチェック
                                If gudt.SetSystem.udtSysOps.shtLRMode = 0 Then
                                    'AlarmLevel=NONEの場合、0であること
                                    If .DigitalLRMode <> 0 Then
                                        Call mSetErrString("Alarm Level is not 0. " & _
                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                        "Alarm Levelが0になっていません。" & _
                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                           intErrCnt, strErrMsg)
                                    End If
                                Else
                                    'Flag2の1bitがonならば、チェック
                                    If gBitCheck(.udtChCommon.shtFlag2, 1) = True Then 'AL
                                        If .DigitalLRMode = 0 Then
                                            Call mSetErrString("Alarm Level is 0. " & _
                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                        "Alarm Levelが0になっています。" & _
                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                           intErrCnt, strErrMsg)
                                        End If
                                    End If
                                End If

                            End If

                        End If

                    End If




                End With
            Next

        ''結果表示
        If intErrCnt = 0 Then
            Call mAddMsgText(" -Checking Digital Setting ... Success", " -デジタル設定確認 ... OK")
        Else

            Call mAddMsgText(" -Checking Digital Setting ... Failure", " -デジタル設定確認 ... エラー")

            ''失敗時はエラー内容を追記
            For i As Integer = 0 To UBound(strErrMsg)
                Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                Call mSetErrString(strErrMsg(i), strErrMsg(i))
                If mblnCancelFlg Then Return
            Next

        End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "システム設定チェック"

    'Ver2.0.0.7
    '--------------------------------------------------------------------
    ' 機能      : システム設定確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : システム設定が情報が正しいか確認する
    '--------------------------------------------------------------------
    
    Private Sub mChkChSystem()
        Dim intErrCnt As Integer = 0
        Dim strErrMsg() As String = Nothing

        Try
            'エラーメッセージ用変数

            'Ver2.0.3.1 加速化
            'システムCHのみのArrayと３０００件のArray作成
            '各処理では、ｼｽﾃﾑCHのArrayをﾙｰﾌﾟさせ、該当CHnoを３０００Arrayに渡し添え字Getで処理
            With gudt.SetChInfo
                '>>>システムCH取得
                prListSysCH = New ArrayList
                For i As Integer = 0 To UBound(.udtChannel)
                    If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                        .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then
                        prListSysCH.Add(.udtChannel(i).udtChCommon.shtChno.ToString)
                    End If
                Next i
                '>>>全CH取得
                prListAllCH = New ArrayList
                'チャンネル番号を配列化
                For i As Integer = 0 To UBound(.udtChannel)
                    prListAllCH.Add(.udtChannel(i).udtChCommon.shtChno.ToString)
                Next i
            End With

            'Ver2.0.3.6 高速化
            Call gSetKikiAry(cmbStatus)


            '■OPS1～10のチェック
            Call mAddMsgText("[System OPS CHK]", "[System OPS CHK]")
            Call subCHKsysOPS(intErrCnt, strErrMsg)
            
            '■GWS1,2のチェック
            Call mAddMsgText("[System GWS CHK]", "[System GWS CHK]")
            Call subCHKsysGWS(intErrCnt, strErrMsg)

            '■FCU A,Bのチェック
            Call mAddMsgText("[System FCU CHK]", "[System FCU CHK]")
            Call subCHKsysFCU(intErrCnt, strErrMsg)

            '■FCU IO A,Bのチェック
            Call mAddMsgText("[System FCU IO CHK]", "[System FCU IO CHK]")
            Call subCHKsysFCUIO(intErrCnt, strErrMsg)

            '■FCU 1-2 COMMNICATIONのチェック
            Call mAddMsgText("[System FCU COMMUNICATION CHK]", "[System FCU COMMUNICATION CHK]")
            Call subCHKsysFCUCOMMU(intErrCnt, strErrMsg)

            '■FU1～20のチェック
            Call mAddMsgText("[System FU CHK]", "[System FU CHK]")
            Call subCHKsysFU(intErrCnt, strErrMsg)

            '■FU LINEのチェック
            Call mAddMsgText("[System FU LINE CHK]", "[System FU LINE CHK]")
            Call subCHKsysFULINE(intErrCnt, strErrMsg)

            '■PRINTERsのチェック
            Call mAddMsgText("[System PRINTER CHK]", "[System PRINTER CHK]")
            Call subCHKsysPRINTERs(intErrCnt, strErrMsg)

            '■COM1～18のチェック
            Call mAddMsgText("[System COM CHK]", "[System COM CHK]")
            Call subCHKsysCOM(intErrCnt, strErrMsg)

            '■EXT ALARM PANEL ALLのチェック
            Call mAddMsgText("[System EXT ALARM PANEL ALL CHK]", "[System EXT ALARM PANEL ALL CHK]")
            Call subCHKsysEXTPANELALL(intErrCnt, strErrMsg)

            '■EXT ALARM PANEL 1～20のチェック
            Call mAddMsgText("[System EXT ALARM PANEL CHK]", "[System EXT ALARM PANEL CHK]")
            Call subCHKsysEXTPANEL(intErrCnt, strErrMsg)


            '下記はチェックしない
            '■SENSOR
            '■COM.EXT FCU A,B
            '■MANUAL REPOSE




            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Device Status(System) Setting ... Success", " -システム(Device Status)設定確認 ... OK")
                Call mAddMsgText("", "")
            Else

                Call mAddMsgText(" -Checking Device Status(System) Setting ... Failure", " -システム(Device Status)設定確認 ... エラー")
                ''失敗時はエラー内容を追記
                For i = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                Next
                Call mAddMsgText("", "")
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'OPS
    Private Sub subCHKsysOPS(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Ver2.0.3.1 処理高速化

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False

        'システム側にOPS設定があるのに、CH側に存在しないならエラー
        With gudt.SetSystem.udtSysOps
            For i = 0 To UBound(gudt.SetSystem.udtSysOps.udtOpsDetail) Step 1
                'Exist=1：OPS設定有り
                If .udtOpsDetail(i).shtExist = 1 Then
                    blOK = False
                    'For j = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                    '    '>>>システムCHのみ対象
                    '    If gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    '        gudt.SetChInfo.udtChannel(j).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    '        'デバイスｺｰﾄﾞの取得
                    '        'cmbStatus = New ComboBox
                    '        intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01)
                    '        'cmbStatus = Nothing

                    '        'デバイスｺｰﾄﾞ-1と、iが一致すれば良い
                    '        If intDeviceCode - 1 = i Then
                    '            blOK = True
                    '            Exit For
                    '        End If
                    '    End If
                    'Next j
                    For j = 0 To prListSysCH.Count - 1 Step 1
                        '>>>システムCHのみ対象
                        Dim iDX As Integer = prListAllCH.IndexOf(prListSysCH(j).ToString)
                        'デバイスｺｰﾄﾞの取得
                        'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(iDX).SystemInfoKikiCode01)
                        'Ver2.0.3.6 高速化
                        intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(iDX).SystemInfoKikiCode01.ToString)
                        If intDeviceCode >= 0 Then
                            intDeviceCode = gAryKiki(intDeviceCode)
                        End If


                        'デバイスｺｰﾄﾞ-1と、iが一致すれば良い
                        If intDeviceCode - 1 = i Then
                            blOK = True
                            Exit For
                        End If
                    Next j

                    If blOK = False Then
                        'システムに設定されているが、CH側には設定されていない
                        Call mSetErrString( _
                                "CH List No Data OPS." & _
                                    "[Info]OPS No=" & i + 1, _
                                "計測点リストにOPSが設定されていません。" & _
                                    "[情報]OPS番号=" & i + 1, _
                            intErrCnt, strErrMsg)
                    End If
                End If
                'Call mAddMsgText("|", "|")
            Next i
        End With
        'CH側でOPS設定があるのに、システム側に存在しないならエラー
        With gudt.SetChInfo
            'For i = 0 To UBound(.udtChannel) Step 1
            For i = 0 To prListSysCH.Count - 1 Step 1
                Dim iDX As Integer = prListAllCH.IndexOf(prListSysCH(i).ToString)
                'システムCHのみ対象
                'If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                '    .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                'デバイスｺｰﾄﾞの取得
                'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(iDX).SystemInfoKikiCode01)
                'Ver2.0.3.6 高速化
                intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(iDX).SystemInfoKikiCode01.ToString)
                If intDeviceCode >= 0 Then
                    intDeviceCode = gAryKiki(intDeviceCode)
                End If

                'デバイスｺｰﾄﾞがOPS=1～10
                blOK = False
                Select Case intDeviceCode
                    Case 1 To 10
                        'デバイスｺｰﾄﾞ-1にシステムのOPS設定あればOK
                        If gudt.SetSystem.udtSysOps.udtOpsDetail(intDeviceCode - 1).shtExist = 1 Then
                            blOK = True
                        End If
                        If blOK = False Then
                            'CHに設定されているが、システム側には設定されていない
                            Call mSetErrString( _
                                    "SYSTEM No Data OPS." & _
                                        "[Info]OPS No=" & intDeviceCode & ",CH=" & .udtChannel(iDX).udtChCommon.shtChno, _
                                    "システム設定にOPSが設定されていません。" & _
                                        "[情報]OPS番号=" & intDeviceCode & ",CH=" & .udtChannel(iDX).udtChCommon.shtChno, _
                                intErrCnt, strErrMsg)
                        End If
                End Select
                'End If
            Next i
        End With
    End Sub
    'GWS
    Private Sub subCHKsysGWS(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False

        'システム側にGWS設定があるのに、CH側に存在しないならエラー
        With gudt.SetSystem.udtSysSystem
            'GWS1
            If gBitCheck(gudt.SetSystem.udtSysSystem.shtGWS1, 0) = True Then
                blOK = False
                For j = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                    '>>>システムCHのみ対象
                    If gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                        gudt.SetChInfo.udtChannel(j).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                        'デバイスｺｰﾄﾞの取得
                        'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01)
                        'Ver2.0.3.6 高速化
                        intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01.ToString)
                        If intDeviceCode >= 0 Then
                            intDeviceCode = gAryKiki(intDeviceCode)
                        End If

                        'デバイスｺｰﾄﾞが11ならばGWS1で良い
                        If intDeviceCode = 11 Then
                            blOK = True
                            Exit For
                        End If
                    End If
                Next j
                If blOK = False Then
                    'システムに設定されているが、CH側には設定されていない
                    Call mSetErrString( _
                            "CH List No Data GWS1.", _
                            "計測点リストにGWS1が設定されていません。", _
                        intErrCnt, strErrMsg)
                End If
            End If
            'GWS2
            If gBitCheck(gudt.SetSystem.udtSysSystem.shtGWS2, 0) = True Then
                blOK = False
                For j = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                    '>>>システムCHのみ対象
                    If gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                        gudt.SetChInfo.udtChannel(j).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                        'デバイスｺｰﾄﾞの取得
                        'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01)
                        'Ver2.0.3.6 高速化
                        intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01.ToString)
                        If intDeviceCode >= 0 Then
                            intDeviceCode = gAryKiki(intDeviceCode)
                        End If

                        'デバイスｺｰﾄﾞが12ならばGWS2で良い
                        If intDeviceCode = 12 Then
                            blOK = True
                            Exit For
                        End If
                    End If
                Next j
                If blOK = False Then
                    'システムに設定されているが、CH側には設定されていない
                    Call mSetErrString( _
                            "CH List No Data GWS2.", _
                            "計測点リストにGWS2が設定されていません。", _
                        intErrCnt, strErrMsg)
                End If
            End If
        End With
        'CH側でGWS設定があるのに、システム側に存在しないならエラー
        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1
                'システムCHのみ対象
                If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    'デバイスｺｰﾄﾞの取得
                    'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                    'Ver2.0.3.6 高速化
                    intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(i).SystemInfoKikiCode01.ToString)
                    If intDeviceCode >= 0 Then
                        intDeviceCode = gAryKiki(intDeviceCode)
                    End If

                    'デバイスｺｰﾄﾞがGWS=11～12
                    blOK = False
                    Select Case intDeviceCode
                        Case 11
                            'GWS1にシステムのGWS設定あればOK
                            If gBitCheck(gudt.SetSystem.udtSysSystem.shtGWS1, 0) = True Then
                                blOK = True
                            End If
                            If blOK = False Then
                                'CHに設定されているが、システム側には設定されていない
                                Call mSetErrString( _
                                        "SYSTEM No Data GWS1." & _
                                            "[Info]CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                        "システム設定にGWS1が設定されていません。" & _
                                            "[情報]CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                    intErrCnt, strErrMsg)
                            End If
                        Case 12
                            'GWS2にシステムのGWS設定あればOK
                            If gBitCheck(gudt.SetSystem.udtSysSystem.shtGWS2, 0) = True Then
                                blOK = True
                            End If
                            If blOK = False Then
                                'CHに設定されているが、システム側には設定されていない
                                Call mSetErrString( _
                                        "SYSTEM No Data GWS2." & _
                                            "[Info]CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                        "システム設定にGWS2が設定されていません。" & _
                                            "[情報]CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                    intErrCnt, strErrMsg)
                            End If
                    End Select
                End If
            Next i
        End With
    End Sub
    'FCU
    Private Sub subCHKsysFCU(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False

        'CHにFCU A,Bどちらも存在しない場合はエラー
        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1
                'システムCHのみ対象
                If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    'デバイスｺｰﾄﾞの取得
                    'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                    'Ver2.0.3.6 高速化
                    intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(i).SystemInfoKikiCode01.ToString)
                    If intDeviceCode >= 0 Then
                        intDeviceCode = gAryKiki(intDeviceCode)
                    End If

                    'デバイスｺｰﾄﾞがFCU=13,14
                    blOK = False
                    Select Case intDeviceCode
                        Case 13, 14
                            blOK = True
                            Exit For
                    End Select
                End If
            Next i
            If blOK = False Then
                Call mSetErrString( _
                            "CH List No Data FCU.", _
                            "計測点リストにFCUが設定されていません。", _
                        intErrCnt, strErrMsg)
            End If
        End With

        'FCU Extend Boardがないのに、SIOのﾎﾟｰﾄ5～9を使用(portが０でない)してればエラー
        blOK = False
        With gudt.SetChSio
            For i = 4 To 8 Step 1
                If .udtVdr(i).shtPort <> 0 Then
                    blOK = True
                    Exit For
                End If
            Next i

            '拡張LAN設定も確認する
            For i = 14 To 15 Step 1
                If .udtVdr(i).shtPort <> 0 Then
                    blOK = True
                    Exit For
                End If
            Next i

            If blOK = True Then
                '5-9のportをいずれか使用している
                If gudt.SetSystem.udtSysFcu.shtFcuExtendBord = 0 Then
                    Call mSetErrString( _
                            "No Data FCU Extend Board.", _
                            "FCU Extend Boardが設定されていません。", _
                        intErrCnt, strErrMsg)
                End If
            End If
        End With
    End Sub
    'FCU IO
    Private Sub subCHKsysFCUIO(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False

        'FCU設定ありなのに、CHに存在しない場合エラー
        If gudt.SetFu.udtFu(0).shtUse = 1 Then
            With gudt.SetChInfo
                blOK = False
                For i = 0 To UBound(.udtChannel) Step 1
                    'システムCHのみ対象
                    If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                        .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                        'デバイスｺｰﾄﾞの取得
                        'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                        'Ver2.0.3.6 高速化
                        intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(i).SystemInfoKikiCode01.ToString)
                        If intDeviceCode >= 0 Then
                            intDeviceCode = gAryKiki(intDeviceCode)
                        End If

                        'デバイスｺｰﾄﾞがFCU IO=15,16
                        blOK = False
                        Select Case intDeviceCode
                            Case 15, 16
                                blOK = True
                                Exit For
                        End Select
                    End If
                Next i
                If blOK = False Then
                    Call mSetErrString( _
                                "CH List No Data FCU-IO.", _
                                "計測点リストにFCU-IOが設定されていません。", _
                            intErrCnt, strErrMsg)
                Else
                    '16=FCU IO Bがある場合はエラー
                    If intDeviceCode = 16 Then
                        Call mSetErrString( _
                                "CH List Data FCU I/O B." & _
                                    "[Info]CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                "計測点リストにFCU I/O Bが設定されています。" & _
                                    "[情報]CH=" & .udtChannel(i).udtChCommon.shtChno, _
                            intErrCnt, strErrMsg)
                    End If
                End If
            End With
        End If

    End Sub
    'FCU 1-2 COMMUNICATION
    Private Sub subCHKsysFCUCOMMU(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False



        'FCU COUNTが1で、該当CHがあるならエラー 17
        If gudt.SetSystem.udtSysFcu.shtFcuCnt = 1 Then
            With gudt.SetChInfo
                blOK = False
                For i = 0 To UBound(.udtChannel) Step 1
                    'システムCHのみ対象
                    If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                        .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                        'デバイスｺｰﾄﾞの取得
                        'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                        'Ver2.0.3.6 高速化
                        intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(i).SystemInfoKikiCode01.ToString)
                        If intDeviceCode >= 0 Then
                            intDeviceCode = gAryKiki(intDeviceCode)
                        End If

                        'デバイスｺｰﾄﾞがFCU COMMUNICATIOM 17
                        blOK = False
                        Select Case intDeviceCode
                            Case 17
                                blOK = True
                                Exit For
                        End Select
                    End If
                Next i
                If blOK = True Then
                    Call mSetErrString( _
                            "FCU COUNT = 0 Bat CH List Data FCU 1-2 COMMUNICATION" & _
                                "[Info]CH=" & .udtChannel(i).udtChCommon.shtChno, _
                            "FCU COUNTが０だが、FCU 1-2 COMMUNUCATIONが設定されています。" & _
                                "[情報]CH=" & .udtChannel(i).udtChCommon.shtChno, _
                            intErrCnt, strErrMsg)
                End If
            End With
        End If

    End Sub
    'FU
    Private Sub subCHKsysFU(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False

        'システム側にFU設定があるのに、CH側に存在しないならエラー
        With gudt.SetFu
            For i = 1 To UBound(.udtFu) Step 1
                '1：FU設定有り
                If .udtFu(i).shtUse = 1 Then
                    blOK = False
                    'For j = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                    '    '>>>システムCHのみ対象
                    '    If gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    '        gudt.SetChInfo.udtChannel(j).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    '        'デバイスｺｰﾄﾞの取得
                    '        'cmbStatus = New ComboBox
                    '        intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01)
                    '        'cmbStatus = Nothing

                    '        'デバイスｺｰﾄﾞ(18～37なので、-17する)と、i(1～20)が一致すれば良い
                    '        If intDeviceCode - 17 = i Then
                    '            blOK = True
                    '            Exit For
                    '        End If
                    '    End If
                    'Next j
                    For j = 0 To prListSysCH.Count - 1 Step 1
                        Dim iDX As Integer = prListAllCH.IndexOf(prListSysCH(j).ToString)
                        'デバイスｺｰﾄﾞの取得
                        'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(iDX).SystemInfoKikiCode01)
                        'Ver2.0.3.6 高速化
                        intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(iDX).SystemInfoKikiCode01.ToString)
                        If intDeviceCode >= 0 Then
                            intDeviceCode = gAryKiki(intDeviceCode)
                        End If

                        'デバイスｺｰﾄﾞ(18～37なので、-17する)と、i(1～20)が一致すれば良い
                        If intDeviceCode - 17 = i Then
                            blOK = True
                            Exit For
                        End If
                    Next j
                    If blOK = False Then
                        'システムに設定されているが、CH側には設定されていない
                        Call mSetErrString( _
                                "CH List No Data FU." & _
                                    "[Info]FU No=" & i, _
                                "計測点リストにFUが設定されていません。" & _
                                    "[情報]FU番号=" & i, _
                            intErrCnt, strErrMsg)
                    End If
                End If
                'Call mAddMsgText("|", "|")
            Next i
        End With
        'CH側でFU設定があるのに、システム側に存在しないならエラー
        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1
                'システムCHのみ対象
                If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    'デバイスｺｰﾄﾞの取得
                    'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                    'Ver2.0.3.6 高速化
                    intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(i).SystemInfoKikiCode01.ToString)
                    If intDeviceCode >= 0 Then
                        intDeviceCode = gAryKiki(intDeviceCode)
                    End If

                    'デバイスｺｰﾄﾞがFU=18～37
                    blOK = False
                    Select Case intDeviceCode
                        Case 18 To 37
                            'デバイスｺｰﾄﾞ-17にシステムのFU設定あればOK
                            If gudt.SetFu.udtFu(intDeviceCode - 17).shtUse = 1 Then
                                blOK = True
                            End If
                            If blOK = False Then
                                'CHに設定されているが、システム側には設定されていない
                                Call mSetErrString( _
                                        "SYSTEM No Data FU." & _
                                            "[Info]FU No=" & intDeviceCode & ",CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                        "システム設定にFUが設定されていません。" & _
                                            "[情報]FU番号=" & intDeviceCode & ",CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                    intErrCnt, strErrMsg)
                            End If
                    End Select
                End If
            Next i
        End With
    End Sub
    'FU LINE
    Private Sub subCHKsysFULINE(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False

        'システム側にFU設定があるのに、CH側に存在しないならエラー
        With gudt.SetFu
            blOK = False
            For i = 1 To UBound(.udtFu) Step 1
                '1：FU設定有り
                If .udtFu(i).shtUse = 1 Then
                    blOK = True
                    Exit For
                End If
            Next i

            If blOK = True Then
                blOK = False
                For j = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                    '>>>システムCHのみ対象
                    If gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                        gudt.SetChInfo.udtChannel(j).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                        'デバイスｺｰﾄﾞの取得
                        'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01)
                        'Ver2.0.3.6 高速化
                        intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01.ToString)
                        If intDeviceCode >= 0 Then
                            intDeviceCode = gAryKiki(intDeviceCode)
                        End If
                        'デバイスｺｰﾄﾞ(３８)なら良い
                        If intDeviceCode = 38 Then
                            blOK = True
                            Exit For
                        End If
                    End If
                Next j
                If blOK = False Then
                    'システムに設定されているが、CH側には設定されていない
                    Call mSetErrString( _
                            "CH List No Data FU LINE.", _
                            "計測点リストにFU LINEが設定されていません。", _
                        intErrCnt, strErrMsg)
                End If
            End If

        End With
    End Sub
    'LOG PRINTER
    Private Sub subCHKsysPRINTERs(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False
        Dim strRet As String = ""

        Dim strCHNo As String = ""

        '0=LOG PRINTER(Mac)
        '1=LOG PRINTER(Cargo)
        '2=ALM PRINTER(Mac)
        '3=ALM PRINTER(Cargo)
        '4=HARD COPY(Mac)
        '  HARD COPY(Cargo)は原則存在しない
        For i = 0 To 4 Step 1
            'システム側にPRINTER設定があるのに、CH側に存在しないならエラー
            With gudt.SetSystem.udtSysPrinter
                If .udtPrinterDetail(i).bytPrinter <> 0 Then
                    blOK = False
                    For j = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                        '>>>システムCHのみ対象
                        If gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                            gudt.SetChInfo.udtChannel(j).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                            'デバイスｺｰﾄﾞの取得
                            'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01)
                            'Ver2.0.3.6 高速化
                            intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01.ToString)
                            If intDeviceCode >= 0 Then
                                intDeviceCode = gAryKiki(intDeviceCode)
                            End If
                            Select Case intDeviceCode
                                Case 39
                                    If i = 0 Then
                                        blOK = True
                                        Exit For
                                    End If
                                Case 40
                                    If i = 1 Then
                                        blOK = True
                                        Exit For
                                    End If
                                Case 41
                                    If i = 2 Then
                                        blOK = True
                                        Exit For
                                    End If
                                Case 42
                                    If i = 3 Then
                                        blOK = True
                                        Exit For
                                    End If
                                Case 43
                                    If i = 4 Then
                                        blOK = True
                                        Exit For
                                    End If
                            End Select
                        End If
                    Next j
                    If blOK = False Then
                        Select Case i
                            Case 0
                                strRet = "LOG PRINTER(Machi)"
                            Case 1
                                strRet = "LOG PRINTER(Cargo)"
                            Case 2
                                strRet = "ALARM PRINTER(Machi)"
                            Case 3
                                strRet = "ALARM PRINTER(Cargo)"
                            Case 4
                                strRet = "HARD COPY PRINTER(Machi)"
                        End Select
                        'システムに設定されているが、CH側には設定されていない
                        Call mSetErrString( _
                                "CH List No Data " & strRet & ".", _
                                "計測点リストに" & strRet & "が設定されていません。", _
                            intErrCnt, strErrMsg)
                    End If
                End If
            End With
            'Call mAddMsgText("|", "|")
        Next i


        'CH側でPRINTER設定があるのに、システム側に存在しないならエラー
        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1
                'システムCHのみ対象
                If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    strCHNo = .udtChannel(i).udtChCommon.shtChno
                    'デバイスｺｰﾄﾞの取得
                    'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                    'Ver2.0.3.6 高速化
                    intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(i).SystemInfoKikiCode01.ToString)
                    If intDeviceCode >= 0 Then
                        intDeviceCode = gAryKiki(intDeviceCode)
                    End If
                    'デバイスｺｰﾄﾞが39～44
                    blOK = False
                    Select Case intDeviceCode
                        Case 39 'LOG PRINTER(M)
                            If gudt.SetSystem.udtSysPrinter.udtPrinterDetail(0).bytPrinter = 0 Then
                                Call mSetErrString( _
                                    "No Data LOG PRINTER(Machi). CH=" & strCHNo, _
                                    "LOG PRINTER(Machi)が設定されていません。CH=" & strCHNo, _
                                intErrCnt, strErrMsg)
                            End If
                        Case 40 'LOG PRINTER(C)
                            If gudt.SetSystem.udtSysPrinter.udtPrinterDetail(1).bytPrinter = 0 Then
                                Call mSetErrString( _
                                    "No Data LOG PRINTER(Cargo). CH=" & strCHNo, _
                                    "LOG PRINTER(Cargo)が設定されていません。CH=" & strCHNo, _
                                intErrCnt, strErrMsg)
                            End If
                        Case 41 'ALM PRINTER(M)
                            If gudt.SetSystem.udtSysPrinter.udtPrinterDetail(2).bytPrinter = 0 Then
                                Call mSetErrString( _
                                    "No Data ALARM PRINTER(Machi).CH=" & strCHNo, _
                                    "ALARM PRINTER(Machi)が設定されていません。CH=" & strCHNo, _
                                intErrCnt, strErrMsg)
                            End If
                        Case 42 'ALM PRINTER(C)
                            If gudt.SetSystem.udtSysPrinter.udtPrinterDetail(3).bytPrinter = 0 Then
                                Call mSetErrString( _
                                    "No Data ALARM PRINTER(Cargo).CH=" & strCHNo, _
                                    "ALARM PRINTER(Cargo)が設定されていません。CH=" & strCHNo, _
                                intErrCnt, strErrMsg)
                            End If
                        Case 43 'HARD COPY(M)
                            If gudt.SetSystem.udtSysPrinter.udtPrinterDetail(4).bytPrinter = 0 Then
                                Call mSetErrString( _
                                    "No Data HARD COPY PRINTER(Machi).CH=" & strCHNo, _
                                    "HARD COPY PRINTER(Machi)が設定されていません。CH=" & strCHNo, _
                                intErrCnt, strErrMsg)
                            End If
                        Case 44 'HARD COPY(C)
                            'HARD Cargoは設定そのものがエラー
                            Call mSetErrString( _
                                    "SETTING HARD COPY PRINTER(Cargo).CH=" & strCHNo, _
                                    "HARD COPY PRINTER(Cargo)が設定されています。CH=" & strCHNo, _
                                intErrCnt, strErrMsg)
                    End Select
                End If
            Next i
        End With

        
    End Sub
    'COM
    Private Sub subCHKsysCOM(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False

        'SIO Set側に、ポート1～16(0～15)しかないためCOM1～16(45～60)のみ対象

        'システム側にSIO設定があるのに、CH側に存在しないならエラー
        With gudt.SetChSio
            For i = 0 To UBound(.udtVdr) Step 1
                'Port<>0：SIO設定有り
                If .udtVdr(i).shtPort <> 0 And ((.udtVdr(i).shtCommType1 >= 1 And .udtVdr(i).shtCommType1 <= 4) or (.udtVdr(i).shtCommType1 >= 6 And .udtVdr(i).shtCommType1 <= 9)) Then
                    blOK = False
                    'For j = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                    '    '>>>システムCHのみ対象
                    '    If gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    '        gudt.SetChInfo.udtChannel(j).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    '        'デバイスｺｰﾄﾞの取得
                    '        'cmbStatus = New ComboBox
                    '        intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01)
                    '        'cmbStatus = Nothing

                    '        'デバイスｺｰﾄﾞ-45と、iが一致すれば良い
                    '        If intDeviceCode - 45 = i Then
                    '            blOK = True
                    '            Exit For
                    '        End If
                    '    End If
                    'Next j
                    For j = 0 To prListSysCH.Count - 1 Step 1
                        '>>>システムCHのみ対象
                        Dim iDX As Integer = prListAllCH.IndexOf(prListSysCH(j).ToString)
                        'デバイスｺｰﾄﾞの取得
                        'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(iDX).SystemInfoKikiCode01)
                        'Ver2.0.3.6 高速化
                        intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(iDX).SystemInfoKikiCode01.ToString)
                        If intDeviceCode >= 0 Then
                            intDeviceCode = gAryKiki(intDeviceCode)
                        End If
                        'デバイスｺｰﾄﾞ-45と、iが一致すれば良い
                        If intDeviceCode - 45 = i Then
                            blOK = True
                            Exit For
                        End If
                        '拡張LANシステムCH
                        If intDeviceCode - 76 = i Then
                            blOK = True
                            Exit For
                        End If
                    Next j
                    If blOK = False Then
                        'システムに設定されているが、CH側には設定されていない
                        If i < 14 Then

                            Call mSetErrString( _
                                    "CH List No Data SIO COM." & _
                                        "[Info]SIO COM No=" & i + 1, _
                                    "計測点リストにSIO COMが設定されていません。" & _
                                        "[情報]SIO COM番号=" & i + 1, _
                                intErrCnt, strErrMsg)
                        Else
                            blOK = True '2019/04/01   拡張LAN　ｽﾃｰﾀｽ未入力でも　compileでｴﾗｰ検出されないように変更
                            ' Call mSetErrString( _
                            '        "CH List No Data EXT LAN." & _
                            '           "[Info]EXT LAN PORT No=" & (i - 14) + 1, _
                            '      "計測点リストにSIO COMが設定されていません。" & _
                            '         "[情報]EXT LAN PORT番号=" & (i - 14) + 1, _
                            '  intErrCnt, strErrMsg)
                        End If
                    End If
                End If
                'Call mAddMsgText("|", "|")
            Next i
        End With
        'CH側でSIO設定があるのに、システム側に存在しないならエラー
        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1
                'システムCHのみ対象
                If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    'デバイスｺｰﾄﾞの取得
                    'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                    'Ver2.0.3.6 高速化
                    intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(i).SystemInfoKikiCode01.ToString)
                    If intDeviceCode >= 0 Then
                        intDeviceCode = gAryKiki(intDeviceCode)
                    End If

                    'デバイスｺｰﾄﾞがCOM=45～60
                    blOK = False
                    Select Case intDeviceCode
                        Case 45 To 60
                            'デバイスｺｰﾄﾞ-45にシステムのSIO設定あればOK
                            If gudt.SetChSio.udtVdr(intDeviceCode - 45).shtPort <> 0 And _
                                    (gudt.SetChSio.udtVdr(intDeviceCode - 45).shtCommType1 >= 1 And gudt.SetChSio.udtVdr(intDeviceCode - 45).shtCommType1 <= 4) Then
                                blOK = True
                            End If
                            If blOK = False Then
                                'CHに設定されているが、システム側には設定されていない
                                Call mSetErrString( _
                                        "SYSTEM No Data SIO COM." & _
                                            "[Info]SIO COM No=" & intDeviceCode - 44 & ",CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                        "システム設定にSIO COMが設定されていません。" & _
                                            "[情報]SIO COM番号=" & intDeviceCode - 44 & ",CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                    intErrCnt, strErrMsg)
                            End If

                        Case 90 To 91 '拡張LAN
                            'デバイスｺｰﾄﾞ-45にシステムのSIO設定あればOK
                            If gudt.SetChSio.udtVdr(intDeviceCode - 76).shtPort <> 0 And _
                                    (gudt.SetChSio.udtVdr(intDeviceCode - 76).shtCommType1 >= 6 And gudt.SetChSio.udtVdr(intDeviceCode - 76).shtCommType1 <= 9) Then
                                blOK = True
                            End If
                            If blOK = False Then
                                'CHに設定されているが、システム側には設定されていない
                                Call mSetErrString( _
                                        "SYSTEM No Data Ext LAN Port." & _
                                            "[Info]EXT LAN PORT No=" & intDeviceCode - 89 & ",CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                        "システム設定に拡張LANポートが設定されていません。" & _
                                            "[情報]EXT LAN PORT番号=" & intDeviceCode - 89 & ",CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                    intErrCnt, strErrMsg)
                            End If

                    End Select
                End If
            Next i
        End With
    End Sub
    'EXT ALARM PANEL ALL
    Private Sub subCHKsysEXTPANELALL(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False

        'EXT ALARM側に、データが存在するかチェック
        blOK = False
        With gudt.SetExtAlarm
            For i = 0 To UBound(.udtExtAlarm) Step 1
                If .udtExtAlarm(i).shtNo <> 0 Then
                    blOK = True
                    Exit For
                End If
            Next i
        End With

        '1件でも存在する場合、CH側に存在しないとエラー
        '1件も存在しないのに、CH側に存在するとエラー
        Dim intCH As Integer = -1
        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1
                'システムCHのみ対象
                If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    'デバイスｺｰﾄﾞの取得
                    'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                    'Ver2.0.3.6 高速化
                    intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(i).SystemInfoKikiCode01.ToString)
                    If intDeviceCode >= 0 Then
                        intDeviceCode = gAryKiki(intDeviceCode)
                    End If
                    'デバイスｺｰﾄﾞがEXT PANEL ALARM ALL=61
                    Select Case intDeviceCode
                        Case 61
                            intCH = .udtChannel(i).udtChCommon.shtChno
                            Exit For
                        Case 62 To 81
                            'Ver2.0.1.9 EXT PANEL 個別が存在すればﾁｪｯｸしない
                            intCH = .udtChannel(i).udtChCommon.shtChno
                    End Select
                End If
            Next i

            If blOK = True Then
                If intCH < 0 Then
                    Call mSetErrString( _
                        "CH List No Data EXT ALARM PANEL ALL.", _
                        "計測点リストにEXT ALARM PANEL ALLが設定されていません。", _
                    intErrCnt, strErrMsg)
                End If
            Else
                If intCH > 0 Then
                    Call mSetErrString( _
                        "EXT ALARM PANEL No Data." & _
                            "[Info]CH=" & .udtChannel(i).udtChCommon.shtChno, _
                        "システム設定にEXT ALARM PANEL が設定されていません。" & _
                            "[情報]CH=" & .udtChannel(i).udtChCommon.shtChno, _
                    intErrCnt, strErrMsg)
                End If
            End If

        End With

    End Sub
    'EXT ALARM PANEL
    Private Sub subCHKsysEXTPANEL(ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim i As Integer = 0
        Dim j As Integer = 0

        'Dim cmbStatus As ComboBox
        Dim intDeviceCode As Integer = 0

        Dim blOK As Boolean = False

        'EXT ALARM PANEL ALLがあるかチェック
        Dim intAllCH As Integer = -1
        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1
                'システムCHのみ対象
                If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    'デバイスｺｰﾄﾞの取得
                    'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                    'Ver2.0.3.6 高速化
                    intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(i).SystemInfoKikiCode01.ToString)
                    If intDeviceCode >= 0 Then
                        intDeviceCode = gAryKiki(intDeviceCode)
                    End If
                    'デバイスｺｰﾄﾞがEXT PANEL ALARM ALL=61
                    Select Case intDeviceCode
                        Case 61
                            intAllCH = .udtChannel(i).udtChCommon.shtChno
                            Exit For
                    End Select
                End If
            Next i
        End With

        ''■EXT ALARM ALLがある場合はチェックは別処理
        'If intAllCH > 0 Then
        '    blOK = False
        '    'CHにEXT ALARM PANEL1～20全て存在しなければOKとして処理抜け
        '    With gudt.SetChInfo
        '        For i = 0 To UBound(.udtChannel) Step 1
        '            'システムCHのみ対象
        '            If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
        '                .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

        '                'デバイスｺｰﾄﾞの取得
        '                'cmbStatus = New ComboBox
        '                intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
        '                'cmbStatus = Nothing

        '                'デバイスｺｰﾄﾞがEXT PANEL ALARM 1～20=62～81
        '                Select Case intDeviceCode
        '                    Case 62 To 81
        '                        blOK = True
        '                        Exit For
        '                End Select
        '            End If
        '        Next i

        '        If blOK = True Then
        '            Call mSetErrString( _
        '                    "EXT ALARM PANEL ALL is set but EXT ALARM PANEL is also set." & _
        '                        "[Info]CH=" & .udtChannel(i).udtChCommon.shtChno, _
        '                    "EXT ALARM PANEL ALLが設定されているがEXT ALARM PANELも設定されている。" & _
        '                        "[情報]CH=" & .udtChannel(i).udtChCommon.shtChno, _
        '                intErrCnt, strErrMsg)
        '        End If
        '    End With
        '    Exit Sub
        '    '↓■EXT ALARM ALLがある場合はチェックは別処理if
        'End If


        '■システムと計測点の個別不整合チェック
        'システム側にEXT ALARM PANEL設定があるのに、CH側に存在しないならエラー
        'Ver2.0.1.9 EXT PANEL ALLがあるときは、CH側に個別設定が無い警告を出さない 
        If intAllCH <= 0 Then
            With gudt.SetExtAlarm
                For i = 0 To UBound(.udtExtAlarm) Step 1
                    '<>0：設定有り
                    If .udtExtAlarm(i).shtNo <> 0 Then
                        blOK = False
                        'For j = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                        '    '>>>システムCHのみ対象
                        '    If gudt.SetChInfo.udtChannel(j).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                        '        gudt.SetChInfo.udtChannel(j).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                        '        'デバイスｺｰﾄﾞの取得
                        '        'cmbStatus = New ComboBox
                        '        intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(j).SystemInfoKikiCode01)
                        '        'cmbStatus = Nothing

                        '        'デバイスｺｰﾄﾞ(62～81)と、i(0～19)が一致すれば良い
                        '        If intDeviceCode - 62 = i Then
                        '            blOK = True
                        '            Exit For
                        '        End If
                        '    End If
                        'Next j
                        For j = 0 To prListSysCH.Count - 1 Step 1
                            Dim iDX As Integer = prListAllCH.IndexOf(prListSysCH(j).ToString)
                            '>>>システムCHのみ対象
                            'デバイスｺｰﾄﾞの取得
                            'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, gudt.SetChInfo.udtChannel(iDX).SystemInfoKikiCode01)
                            'Ver2.0.3.6 高速化
                            intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(iDX).SystemInfoKikiCode01.ToString)
                            If intDeviceCode >= 0 Then
                                intDeviceCode = gAryKiki(intDeviceCode)
                            End If
                            'デバイスｺｰﾄﾞ(62～81)と、i(0～19)が一致すれば良い
                            If intDeviceCode - 62 = i Then
                                blOK = True
                                Exit For
                            End If
                        Next j
                        If blOK = False Then
                            'システムに設定されているが、CH側には設定されていない
                            Call mSetErrString( _
                                    "CH List No Data EXT ALARM PANEL." & _
                                        "[Info]EXT ALARM PANEL=" & i + 1, _
                                    "計測点リストにEXT ALARM PANELが設定されていません。" & _
                                        "[情報EXT ALARM PANEL=" & i + 1, _
                                intErrCnt, strErrMsg)
                        End If
                    End If
                    'Call mAddMsgText("|", "|")
                Next i
            End With
        End If
        'CH側で設定があるのに、システム側に存在しないならエラー
        With gudt.SetChInfo
            For i = 0 To UBound(.udtChannel) Step 1
                'システムCHのみ対象
                If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeDigital And _
                    .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeDigitalDeviceStatus Then

                    'デバイスｺｰﾄﾞの取得
                    'intDeviceCode = gGetChannelSystemDeviceStatus(cmbStatus, .udtChannel(i).SystemInfoKikiCode01)
                    'Ver2.0.3.6 高速化
                    intDeviceCode = gAryKikiDtl.IndexOf(gudt.SetChInfo.udtChannel(i).SystemInfoKikiCode01.ToString)
                    If intDeviceCode >= 0 Then
                        intDeviceCode = gAryKiki(intDeviceCode)
                    End If
                    'デバイスｺｰﾄﾞがEXT ALARM PANEL=1～20
                    blOK = False
                    Select Case intDeviceCode
                        Case 62 To 81
                            'デバイスｺｰﾄﾞ-62にシステムの設定あればOK
                            If gudt.SetExtAlarm.udtExtAlarm(intDeviceCode - 62).shtNo <> 0 Then
                                blOK = True
                            End If
                            If blOK = False Then
                                'CHに設定されているが、システム側には設定されていない
                                Call mSetErrString( _
                                        "SYSTEM No Data EXT ALARM PANEL." & _
                                            "[Info]EXT ALARM PANEL=" & intDeviceCode - 61 & ",CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                        "システム設定にEXT ALARM PANELが設定されていません。" & _
                                            "[情報]EXT ALARM PANEL=" & intDeviceCode - 61 & ",CH=" & .udtChannel(i).udtChCommon.shtChno, _
                                    intErrCnt, strErrMsg)
                            End If
                    End Select
                End If
            Next i
        End With


    End Sub

#End Region


#Region "アナログ設定チェック"

    '--------------------------------------------------------------------
    ' 機能      : アナログ設定確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : アナログ設定が情報が正しいか確認する
    '--------------------------------------------------------------------
    Private Sub mChkChAnalog()

        Try

            ''各設定値
            Dim intHi As Integer = 0
            Dim intLo As Integer = 0
            Dim intHiHi As Integer = 0
            Dim intLoLo As Integer = 0
            Dim intRangeHi As Integer = 0
            Dim intRangeLo As Integer = 0
            Dim intNormalHi As Integer = 0
            Dim intNormalLo As Integer = 0

            Dim intExtGHi As Integer = 0
            Dim intExtGLo As Integer = 0
            Dim intExtGHiHi As Integer = 0
            Dim intExtGLoLo As Integer = 0
            Dim intExtGSen As Integer = 0

            Dim intDlyHi As Integer = 0
            Dim intDlyLo As Integer = 0
            Dim intDlyHiHi As Integer = 0
            Dim intDlyLoLo As Integer = 0
            Dim intDlySen As Integer = 0

            'Ver2.0.0.9 ｺﾝﾊﾟｲﾙ時チェック
            Dim intString As Integer = 0
            Dim intOFFset As Integer = 0
            Dim intALMlevel As Integer = 0

            'Ver2.0.1.7 ｾﾝﾀｰﾊﾞｰｸﾞﾗﾌﾁｪｯｸ
            Dim blCenterBarFlg As Boolean = False

            ''各設定有無フラグ
            Dim blnAnalog As Boolean = False
            Dim blnHi As Boolean = False
            Dim blnLo As Boolean = False
            Dim blnHiHi As Boolean = False
            Dim blnLoLo As Boolean = False
            Dim blnSen As Boolean = False       '' 2015.03.16
            Dim blnRangeHi As Boolean = False
            Dim blnRangeLo As Boolean = False
            Dim blnNormalHi As Boolean = False
            Dim blnNormalLo As Boolean = False

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim blnErrFlg As Boolean = False
            Dim strMsgEng As String = ""
            Dim strMsgJpn As String = ""
            'Dim blnCheckExecuteFlg As Boolean = False

            Dim bAlmLevelChk As Boolean     '' Ver1.11.8.5 2016.11.10  ｱﾗｰﾑﾚﾍﾞﾙﾁｪｯｸ追加

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                blnAnalog = False
                blnHi = False
                blnLo = False
                blnHiHi = False
                blnLoLo = False
                blnSen = False      '' 2015.03.16
                blnRangeHi = False
                blnRangeLo = False
                blnNormalHi = False
                blnNormalLo = False

                With gudt.SetChInfo.udtChannel(i)

                    ''チャンネルが設定されている場合
                    If .udtChCommon.shtChno <> 0 Then

                        bAlmLevelChk = False        '' Ver1.11.8.5 2016.11.10  ｱﾗｰﾑﾚﾍﾞﾙﾁｪｯｸ 初期値設定

                        ''==========================
                        ''先に比較用の値を取得する
                        ''==========================
                        Select Case .udtChCommon.shtChType
                            Case gCstCodeChTypeAnalog

                                'Ver2.0.0.9
                                intString = .AnalogString
                                intOFFset = .AnalogOffsetValue
                                intALMlevel = .AnalogLRMode

                                'Ver2.0.1.7
                                blCenterBarFlg = gBitCheck(.AnalogDisplay3, 0)


                                ''レンジがあるデータタイプの場合
                                '' Ver1.10.9 2016.07.08 緯度・経度追加
                                ' Ver2.0.1.9 全部対象
                                'If .udtChCommon.shtData = gCstCodeChDataTypeAnalogK _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog2Pt _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog2Jpt _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog3Pt _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog3Jpt _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog1_5v _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypeAnalog4_20mA _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogLatitude _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypeAnalogLongitude Then
                                If .udtChCommon.shtData >= gCstCodeChDataTypeAnalogK Then

                                    ''アナログ
                                    blnAnalog = True

                                    ''レンジ
                                    intRangeHi = .AnalogRangeHigh                       ''レンジ上限値
                                    intRangeLo = .AnalogRangeLow                        ''レンジ下限値
                                    blnRangeHi = True                                   ''レンジ上限使用有無
                                    blnRangeLo = True                                   ''レンジ下限使用有無


                                    'Ver2.0.7.D
                                    'データが温度基板(PT系)の場合は、レンジ上下下限値処理
                                    If .udtChCommon.shtData >= gCstCodeChDataTypeAnalog2Pt And _
                                        .udtChCommon.shtData <= gCstCodeChDataTypeAnalog3Jpt Then

                                        Dim strDec As String = ""
                                        strDec = "1".PadRight(.AnalogDecimalPosition + 1, "0"c)

                                        intRangeHi = intRangeHi * CInt(strDec)
                                        intRangeLo = intRangeLo * CInt(strDec)
                                    End If



                                    ''標準範囲
                                    intNormalHi = .AnalogNormalHigh                     ''標準範囲上限値
                                    intNormalLo = .AnalogNormalLow                      ''標準範囲下限値
                                    blnNormalHi = IIf(.AnalogNormalHigh = gCstCodeChAlalogNormalRangeNothingHi, False, True)    ''標準範囲上限使用有無
                                    blnNormalLo = IIf(.AnalogNormalLow = gCstCodeChAlalogNormalRangeNothingLo, False, True)     ''標準範囲下限使用有無

                                    ''設定値
                                    intHiHi = .AnalogHiHiValue                          ''Value HH
                                    intHi = .AnalogHiValue                              ''Value H
                                    intLo = .AnalogLoValue                              ''Value L
                                    intLoLo = .AnalogLoLoValue                          ''Value LL
                                    blnHiHi = IIf(.AnalogHiHiUse = 1, True, False)      ''上上限使用有無
                                    blnHi = IIf(.AnalogHiUse = 1, True, False)          ''上限使用有無
                                    blnLo = IIf(.AnalogLoUse = 1, True, False)          ''下限使用有無
                                    blnLoLo = IIf(.AnalogLoLoUse = 1, True, False)      ''下下限使用有無
                                    blnSen = IIf(.AnalogSensorFailUse = 1, True, False) ''センサフェイル使用有無   2015.03.16

                                    '' Ext.G    2015.03.25
                                    intExtGHiHi = .AnalogHiHiExtGroup                   '' Ext.G HH
                                    intExtGHi = .AnalogHiExtGroup                       '' Ext.G H
                                    intExtGLo = .AnalogLoExtGroup                       '' Ext.G L
                                    intExtGLoLo = .AnalogLoLoExtGroup                   '' Ext.G LL
                                    intExtGSen = .AnalogSensorFailExtGroup              '' Ext.G Sensor

                                    ''DLY   2015.03.25
                                    intDlyHiHi = .AnalogHiHiDelay                       '' DLY HH
                                    intDlyHi = .AnalogHiDelay                           '' DLY H
                                    intDlyLo = .AnalogLoDelay                           '' DLY L
                                    intDlyLoLo = .AnalogLoLoDelay                       '' DLY LL
                                    intDlySen = .AnalogSensorFailDelay                  '' DLY Sensor

                                    ' ''チェック実行フラグ
                                    'blnCheckExecuteFlg = True

                                End If

                            Case gCstCodeChTypeValve

                                ''AI-DOの場合
                                If .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 _
                                Or .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then
                                    'Ver2.0.0.9
                                    intString = .ValveAiDoString
                                    intOFFset = .ValveAiDoOffsetValue
                                    intALMlevel = .ValveAiDoLRMode

                                    'Ver2.0.1.7
                                    blCenterBarFlg = gBitCheck(.ValveAiDoDisplay3, 0)


                                    ''アナログ
                                    blnAnalog = True

                                    ''レンジ
                                    intRangeHi = .ValveAiDoRangeHigh                        ''レンジ上限値
                                    intRangeLo = .ValveAiDoRangeLow                         ''レンジ下限値
                                    blnRangeHi = True                                       ''レンジ上限使用有無
                                    blnRangeLo = True                                       ''レンジ下限使用有無

                                    ''標準範囲
                                    intNormalHi = .ValveAiDoNormalHigh                      ''標準範囲上限値
                                    intNormalLo = .ValveAiDoNormalLow                       ''標準範囲下限値
                                    blnNormalHi = IIf(.ValveAiDoNormalHigh = gCstCodeChAlalogNormalRangeNothingHi, False, True)    ''標準範囲上限使用有無
                                    blnNormalLo = IIf(.ValveAiDoNormalLow = gCstCodeChAlalogNormalRangeNothingLo, False, True)     ''標準範囲下限使用有無

                                    ''設定値
                                    intHiHi = .ValveAiDoHiHiValue                           ''Value HH
                                    intHi = .ValveAiDoHiValue                               ''Value H
                                    intLo = .ValveAiDoLoValue                               ''Value L
                                    intLoLo = .ValveAiDoLoLoValue                           ''Value LL
                                    blnHiHi = IIf(.ValveAiDoHiHiUse = 1, True, False)       ''上上限使用有無
                                    blnHi = IIf(.ValveAiDoHiUse = 1, True, False)           ''上限使用有無
                                    blnLo = IIf(.ValveAiDoLoUse = 1, True, False)           ''下限使用有無
                                    blnLoLo = IIf(.ValveAiDoLoLoUse = 1, True, False)       ''下下限使用有無
                                    blnSen = IIf(.ValveAiDoSensorFailUse = 1, True, False) ''センサフェイル使用有無   2015.03.16

                                    '' Ext.G    2015.03.25
                                    intExtGHiHi = .ValveAiDoHiHiExtGroup                   '' Ext.G HH
                                    intExtGHi = .ValveAiDoHiExtGroup                       '' Ext.G H
                                    intExtGLo = .ValveAiDoLoExtGroup                       '' Ext.G L
                                    intExtGLoLo = .ValveAiDoLoLoExtGroup                   '' Ext.G LL
                                    intExtGSen = .ValveAiDoSensorFailExtGroup              '' Ext.G Sensor

                                    ''DLY   2015.03.25
                                    intDlyHiHi = .ValveAiDoHiHiDelay                       '' DLY HH
                                    intDlyHi = .ValveAiDoHiDelay                           '' DLY H
                                    intDlyLo = .ValveAiDoLoDelay                           '' DLY L
                                    intDlyLoLo = .ValveAiDoLoLoDelay                       '' DLY LL
                                    intDlySen = .ValveAiDoSensorFailDelay                  '' DLY Sensor

                                    ' ''チェック実行フラグ
                                    'blnCheckExecuteFlg = True

                                End If

                                ''AI-AOの場合
                                If .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 _
                                Or .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Then

                                    'Ver2.0.0.9
                                    intString = .ValveAiAoString
                                    intOFFset = .ValveAiAoOffsetValue
                                    intALMlevel = .ValveAiAoLRMode

                                    'Ver2.0.1.7
                                    blCenterBarFlg = gBitCheck(.ValveAiAoDisplay3, 0)

                                    ''アナログ
                                    blnAnalog = True

                                    ''レンジ
                                    intRangeHi = .ValveAiAoRangeHigh                        ''レンジ上限値
                                    intRangeLo = .ValveAiAoRangeLow                         ''レンジ下限値
                                    blnRangeHi = True                                       ''レンジ上限使用有無
                                    blnRangeLo = True                                       ''レンジ下限使用有無

                                    ''標準範囲
                                    intNormalHi = .ValveAiAoNormalHigh                      ''標準範囲上限値
                                    intNormalLo = .ValveAiAoNormalLow                       ''標準範囲下限値
                                    blnNormalHi = IIf(.ValveAiAoNormalHigh = gCstCodeChAlalogNormalRangeNothingHi, False, True)    ''標準範囲上限使用有無
                                    blnNormalLo = IIf(.ValveAiAoNormalLow = gCstCodeChAlalogNormalRangeNothingLo, False, True)     ''標準範囲下限使用有無

                                    ''設定値
                                    intHiHi = .ValveAiAoHiHiValue                           ''Value HH
                                    intHi = .ValveAiAoHiValue                               ''Value H
                                    intLo = .ValveAiAoLoValue                               ''Value L
                                    intLoLo = .ValveAiAoLoLoValue                           ''Value LL
                                    blnHiHi = IIf(.ValveAiAoHiHiUse = 1, True, False)       ''上上限使用有無
                                    blnHi = IIf(.ValveAiAoHiUse = 1, True, False)           ''上限使用有無
                                    blnLo = IIf(.ValveAiAoLoUse = 1, True, False)           ''下限使用有無
                                    blnLoLo = IIf(.ValveAiAoLoLoUse = 1, True, False)       ''下下限使用有無
                                    blnSen = IIf(.ValveAiAoSensorFailUse = 1, True, False) ''センサフェイル使用有無   2015.03.16

                                    '' Ext.G    2015.03.25
                                    intExtGHiHi = .ValveAiAoHiHiExtGroup                   '' Ext.G HH
                                    intExtGHi = .ValveAiAoHiExtGroup                       '' Ext.G H
                                    intExtGLo = .ValveAiAoLoExtGroup                       '' Ext.G L
                                    intExtGLoLo = .ValveAiAoLoLoExtGroup                   '' Ext.G LL
                                    intExtGSen = .ValveAiAoSensorFailExtGroup              '' Ext.G Sensor

                                    ''DLY   2015.03.25
                                    intDlyHiHi = .ValveAiAoHiHiDelay                       '' DLY HH
                                    intDlyHi = .ValveAiAoHiDelay                           '' DLY H
                                    intDlyLo = .ValveAiAoLoDelay                           '' DLY L
                                    intDlyLoLo = .ValveAiAoLoLoDelay                       '' DLY LL
                                    intDlySen = .ValveAiAoSensorFailDelay                  '' DLY Sensor


                                    ' ''チェック実行フラグ
                                    'blnCheckExecuteFlg = True

                                End If

                        End Select

                        'Ver2.0.1.2
                        If blnAnalog = False Then
                            Continue For
                        End If


                        'Ver2.0.0.9 STRING
                        'STRINGが０以外ならエラー
                        If intString <> 0 Then
                            Call mSetErrString("String is not 0. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                "STRINGが0になっていません。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                   intErrCnt, strErrMsg)
                        End If
                        'OFF SETが０以外ならエラー
                        If intOFFset <> 0 Then
                            Call mSetErrString("OFF SET is not 0. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                "OFF SETが0になっていません。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                   intErrCnt, strErrMsg)
                        End If
                        'AlarmLevelチェック
                        If gudt.SetSystem.udtSysOps.shtLRMode = 0 Then
                            'AlarmLevel=NONEの場合、0であること
                            If intALMlevel <> 0 Then
                                Call mSetErrString("Alarm Level is not 0. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                "Alarm Levelが0になっていません。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                   intErrCnt, strErrMsg)
                            End If
                        Else
                            'Flag2の1bitがonならば、チェック
                            If gBitCheck(.udtChCommon.shtFlag2, 1) = True Then 'AL
                                If intALMlevel = 0 Then
                                    Call mSetErrString("Alarm Level is 0. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                "Alarm Levelが0になっています。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                   intErrCnt, strErrMsg)
                                End If
                            End If
                        End If



                        '===============================================
                        ''レンジ上限＞レンジ下限になっているか
                        '===============================================
                        If blnRangeHi And blnRangeLo Then

                            If Not (intRangeHi >= intRangeLo) Then
                                Call mSetErrString("Analog range is inconsistent. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & _
                                                   " , RangeHigh=" & intRangeHi & _
                                                   " , RangeLow=" & intRangeLo, _
                                                   "レンジ上限＞レンジ下限になっていません。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                   " , レンジ上限=" & intRangeHi & _
                                                   " , レンジ下限=" & intRangeLo, _
                                                   intErrCnt, strErrMsg)
                            End If


                            'Ver2.0.1.7 ｾﾝﾀｰﾊﾞｰｸﾞﾗﾌﾁｪｯｸ
                            'ﾚﾝｼﾞHiとﾚﾝｼﾞLOの絶対値が同じならBarCenterはON
                            'ﾚﾝｼﾞHiとﾚﾝｼﾞLOの絶対値が違うならBarCenterはOFF
                            '上記でないならﾒｯｾｰｼﾞ
                            If intRangeLo <> intRangeHi Then
                                Dim intChLow As Integer = Math.Abs(CInt(intRangeLo))
                                Dim intChHi As Integer = Math.Abs(CInt(intRangeHi))
                                If intChLow = intChHi Then
                                    'CenterONが正
                                    If blCenterBarFlg = False Then
                                        Call mSetErrString("CenterBarGraph is not ON. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & _
                                                   " , RangeHigh=" & intRangeHi & _
                                                   " , RangeLow=" & intRangeLo, _
                                                   "センター表示フラグがにONなっていません。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                   " , レンジ上限=" & intRangeHi & _
                                                   " , レンジ下限=" & intRangeLo, _
                                                   intErrCnt, strErrMsg)
                                    End If
                                Else
                                    'CenterOFFが正
                                    If blCenterBarFlg = True Then
                                        Call mSetErrString("CenterBarGraph is ON. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & _
                                                   " , RangeHigh=" & intRangeHi & _
                                                   " , RangeLow=" & intRangeLo, _
                                                   "センター表示フラグがにONなっています。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                   " , レンジ上限=" & intRangeHi & _
                                                   " , レンジ下限=" & intRangeLo, _
                                                   intErrCnt, strErrMsg)
                                    End If
                                End If
                            Else
                                'CenterOFFが正
                                If blCenterBarFlg = True Then
                                    Call mSetErrString("CenterBarGraph is ON. " & _
                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                               " , CH NO=" & .udtChCommon.shtChno & _
                                               " , RangeHigh=" & intRangeHi & _
                                               " , RangeLow=" & intRangeLo, _
                                               "センター表示フラグがにONなっています。" & _
                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                               " , チャンネル番号=" & .udtChCommon.shtChno & _
                                               " , レンジ上限=" & intRangeHi & _
                                               " , レンジ下限=" & intRangeLo, _
                                               intErrCnt, strErrMsg)
                                End If
                            End If

                        End If


                        '' Ver1.10.6.1 2016.06.07  ｱﾅﾛｸﾞCH条件 追加
                        If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then
                            '' Ver1.10.6 2016.06.06  緯度・経度 追加
                            If .udtChCommon.shtData = gCstCodeChDataTypeAnalogLatitude Then     '' 緯度
                                If intRangeHi <> 9000 Then     '' Ver1.10.9 2016.07.08 緯度・経度間違いのため修正  18000 → 9000
                                    Call mSetErrString("Latitude H range is inconsistent. " & _
                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                       "緯度 レンジ上限値 エラーです。" & _
                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                       intErrCnt, strErrMsg)
                                End If

                                If intRangeLo <> -9000 Then    '' Ver1.10.9 2016.07.08 緯度・経度間違いのため修正  -18000 → -9000
                                    Call mSetErrString("Latitude L range is inconsistent. " & _
                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                       "緯度 レンジ下限値 エラーです。" & _
                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                       intErrCnt, strErrMsg)
                                End If
                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeAnalogLongitude Then     '' 経度
                                If intRangeHi <> 18000 Then      '' Ver1.10.9 2016.07.08 緯度・経度間違いのため修正  9000 → 18000
                                    Call mSetErrString("Longitude H range is inconsistent. " & _
                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                       "経度 レンジ上限値 エラーです。" & _
                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                       intErrCnt, strErrMsg)
                                End If

                                If intRangeLo <> -18000 Then     '' Ver1.10.9 2016.07.08 緯度・経度間違いのため修正  -9000 → -18000
                                    Call mSetErrString("Longitude L range is inconsistent. " & _
                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                       "経度 レンジ下限値 エラーです。" & _
                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                       intErrCnt, strErrMsg)
                                End If
                            End If

                            'Ver2.0.2.6 アナログCHのステータスとセット値関係処理
                            Dim intRet As Integer = fnAnalogStatusAlarmRel(i)
                            'HH 10,H 20,L 30,LL 40
                            'ｽﾃｰﾀｽあり、値なし+1
                            'ｽﾃｰﾀｽなし、値あり+2
                            Select Case intRet
                                Case 11
                                    Call mSetErrString("Error Analog(HH) STATUS ON ALARM OFF. " & _
                                            "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            " , CH NO=" & .udtChCommon.shtChno, _
                                            "アナログ(HH) ステータス値あり アラーム値なし エラー。" & _
                                            "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            " , チャンネル番号=" & .udtChCommon.shtChno, _
                                            intErrCnt, strErrMsg)
                                Case 12
                                    Call mSetErrString("Error Analog(HH) STATUS OFF ALARM ON. " & _
                                            "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            " , CH NO=" & .udtChCommon.shtChno, _
                                            "アナログ(HH) ステータス値なし アラーム値あり エラー。" & _
                                            "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            " , チャンネル番号=" & .udtChCommon.shtChno, _
                                            intErrCnt, strErrMsg)
                                Case 21
                                    Call mSetErrString("Error Analog(H) STATUS ON ALARM OFF. " & _
                                            "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            " , CH NO=" & .udtChCommon.shtChno, _
                                            "アナログ(H) ステータス値あり アラーム値なし エラー。" & _
                                            "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            " , チャンネル番号=" & .udtChCommon.shtChno, _
                                            intErrCnt, strErrMsg)
                                Case 22
                                    Call mSetErrString("Error Analog(H) STATUS OFF ALARM ON. " & _
                                            "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            " , CH NO=" & .udtChCommon.shtChno, _
                                            "アナログ(H) ステータス値なし アラーム値あり エラー。" & _
                                            "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            " , チャンネル番号=" & .udtChCommon.shtChno, _
                                            intErrCnt, strErrMsg)
                                Case 31
                                    Call mSetErrString("Error Analog(L) STATUS ON ALARM OFF. " & _
                                            "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            " , CH NO=" & .udtChCommon.shtChno, _
                                            "アナログ(L) ステータス値あり アラーム値なし エラー。" & _
                                            "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            " , チャンネル番号=" & .udtChCommon.shtChno, _
                                            intErrCnt, strErrMsg)
                                Case 32
                                    Call mSetErrString("Error Analog(L) STATUS OFF ALARM ON. " & _
                                            "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            " , CH NO=" & .udtChCommon.shtChno, _
                                            "アナログ(L) ステータス値なし アラーム値あり エラー。" & _
                                            "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            " , チャンネル番号=" & .udtChCommon.shtChno, _
                                            intErrCnt, strErrMsg)
                                Case 41
                                    Call mSetErrString("Error Analog(LL) STATUS ON ALARM OFF. " & _
                                            "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            " , CH NO=" & .udtChCommon.shtChno, _
                                            "アナログ(L) ステータス値あり アラーム値なし エラー。" & _
                                            "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            " , チャンネル番号=" & .udtChCommon.shtChno, _
                                            intErrCnt, strErrMsg)
                                Case 42
                                    Call mSetErrString("Error Analog(LL) STATUS OFF ALARM ON. " & _
                                            "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            " , CH NO=" & .udtChCommon.shtChno, _
                                            "アナログ(L) ステータス値なし アラーム値あり エラー。" & _
                                            "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            " , チャンネル番号=" & .udtChCommon.shtChno, _
                                            intErrCnt, strErrMsg)
                                Case 99
                                    'Ver2.0.6.3 ステータスが無い場合はエラー
                                    Call mSetErrString("Error Analog STATUS OFF. " & _
                                            "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            " , CH NO=" & .udtChCommon.shtChno, _
                                            "アナログ ステータス値なし エラー。" & _
                                            "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            " , チャンネル番号=" & .udtChCommon.shtChno, _
                                            intErrCnt, strErrMsg)
                                Case Else
                            End Select

                        End If

                        ''//

                        '===============================================
                        ''標準範囲が設定されているか
                        '===============================================
                        '▼▼▼ 20110308 標準範囲設定なし対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                        ' ''標準範囲上限が設定されていない場合
                        'If blnNormalHi Then
                        '    Call mSetErrString("Analog HiNormal is not set. " & _
                        '                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                        '                       " , CH NO=" & .udtChCommon.shtChno, _
                        '                       "標準範囲上限が設定されていません。" & _
                        '                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                        '                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                        '                       intErrCnt, strErrMsg)
                        'End If

                        ' ''標準範囲下限が設定されていない場合
                        'If blnNormalLo Then
                        '    Call mSetErrString("Analog LoNormal is not set. " & _
                        '                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                        '                       " , CH NO=" & .udtChCommon.shtChno, _
                        '                       "標準範囲下限が設定されていません。" & _
                        '                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                        '                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                        '                       intErrCnt, strErrMsg)
                        'End If
                        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                        '===============================================
                        ''標準範囲上限＞標準範囲下限になっているか
                        '===============================================
                        If blnNormalHi And blnNormalLo Then

                            If Not (intNormalHi >= intNormalLo) Then
                                Call mSetErrString("Analog normal range is inconsistent. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & _
                                                   " , HiNormal=" & intNormalHi & _
                                                   " , LoNormal=" & intNormalLo, _
                                                   "標準範囲上限＞標準範囲下限になっていません。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                   " , 標準範囲上限=" & intNormalHi & _
                                                   " , 標準範囲下限=" & intNormalLo, _
                                                   intErrCnt, strErrMsg)
                            End If

                        End If

                        '===============================================
                        ''選択ステータスに対応した値が設定されていること
                        '===============================================
                        ' ''ステータスがなし以外の場合
                        'If .udtChCommon.shtStatus <> gCstCodeChStatusAnalogNothing Then

                        '▼▼▼ 20110308 アラーム値入力確認は不要（入力しない場合もある）▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                        ' ''===============================================
                        '' ''上上限、上限、下限、下下限が入力されているか
                        ' ''===============================================
                        ' ''上上限チェック
                        'If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorHiHi _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLoLoNorHiHi Then

                        '    ''上上限使用なしの場合
                        '    If Not blnHiHi Then
                        '        Call mSetErrString("Analog Alarm HiHi Value is not set. " & _
                        '                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                        '                           " , CH NO=" & .udtChCommon.shtChno, _
                        '                           "上上限アラーム値が設定されていません。 " & _
                        '                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                        '                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                        '                           intErrCnt, strErrMsg)
                        '    End If

                        'End If

                        ' ''上限チェック
                        'If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorHiHi _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorHigh _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLowNorHigh _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLoLoNorHiHi _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorEHigh _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogELowNorEHigh Then

                        '    ''上限使用なしの場合
                        '    If Not blnHi = 0 Then
                        '        Call mSetErrString("Analog Alarm Hi Value is not set. " & _
                        '                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                        '                           " , CH NO=" & .udtChCommon.shtChno, _
                        '                           "上限アラーム値が設定されていません。 " & _
                        '                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                        '                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                        '                           intErrCnt, strErrMsg)
                        '    End If

                        'End If

                        ' ''下限チェック
                        'If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorLoLo _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorLow _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLowNorHigh _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLoLoNorHiHi _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorELow _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogELowNorEHigh Then

                        '    ''下限使用なしの場合
                        '    If Not blnLo = 0 Then
                        '        Call mSetErrString("Analog Alarm Lo Value is not set. " & _
                        '                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                        '                           " , CH NO=" & .udtChCommon.shtChno, _
                        '                             "下限アラーム値が設定されていません。 " & _
                        '                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                        '                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                        '                         intErrCnt, strErrMsg)
                        '    End If

                        'End If

                        ' ''下下限チェック
                        'If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorLoLo _
                        'Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLoLoNorHiHi Then

                        '    ''下下限使用なしの場合
                        '    If Not blnLoLo = 0 Then
                        '        Call mSetErrString("Analog Alarm LoLo Value is not set. " & _
                        '                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                        '                           " , CH NO=" & .udtChCommon.shtChno, _
                        '                           "下下限アラーム値が設定されていません。 " & _
                        '                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                        '                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                        '                           intErrCnt, strErrMsg)
                        '    End If

                        'End If
                        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲


                        '===============================================
                        ''各設定値の大小関係は正しいか
                        '===============================================
                        blnErrFlg = gChkAnalogRangeSetValue(intRangeHi, intHiHi, intHi, intNormalHi, intNormalLo, intLo, intLoLo, intRangeLo, _
                                                            blnRangeHi, blnHiHi, blnHi, blnNormalHi, blnNormalLo, blnLo, blnLoLo, blnRangeLo, _
                                                            strMsgEng, strMsgJpn)

                        If blnErrFlg Then
                            Call mSetErrString("Order in alarm set value incorrect." & _
                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                               " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                               "アラーム設定値の大小関係が正しくありません。" & _
                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                               " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                               intErrCnt, strErrMsg)
                        End If

                        If blnAnalog = True Then

                            '' Ver1.10.5 2016.05.09  単位入力ﾁｪｯｸ追加
                            If .udtChCommon.strUnit.IndexOf("℃") > 0 Or .udtChCommon.strUnit.IndexOf("％") > 0 Then
                                Call mSetErrString("Double-byte character is included in unit." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "単位に全角文字が含まれます" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)
                            End If
                            ''//

                            '' Ver1.11.5 2016.09.06  mmHgﾌﾗｸﾞ
                            If .udtChCommon.shtUnit = &H5 Then
                                .udtChCommon.shtFlag1 = gBitSet(.udtChCommon.shtFlag1, 6, True)
                            Else
                                .udtChCommon.shtFlag1 = gBitSet(.udtChCommon.shtFlag1, 6, False)
                            End If

                            '' cmHgﾌﾗｸﾞ
                            If .udtChCommon.shtUnit = &HFF And .udtChCommon.strUnit = "cmHg" Then
                                .udtChCommon.shtFlag1 = gBitSet(.udtChCommon.shtFlag1, 7, True)
                            Else
                                .udtChCommon.shtFlag1 = gBitSet(.udtChCommon.shtFlag1, 7, False)
                            End If
                            ''//

                            If blnHiHi = True Then      '' HH ALARM あり  2015.03.25
                                If intExtGHiHi = 255 And fnAnalogStatusAlarmRel_V2(i, 0) = False Then
                                    Call mSetErrString("EXT GROUP is not set in HH ALARM." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "HH ALARMにEXT GROUPが設定されていません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)
                                End If

                                If intDlyHiHi = 255 And fnAnalogStatusAlarmRel_V2(i, 0) = False Then
                                    Call mSetErrString("DELAY is not set in HH ALARM." & _
                                                  "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                  " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                  "HH ALARMにDELAYが設定されていません" & _
                                                  "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                  " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                  intErrCnt, strErrMsg)
                                End If

                                '' 関数取りやめ　　2015.11.18  Ver1.8.1
                                '' Ver1.11.8.5 2016.11.10  ｱﾗｰﾑﾚﾍﾞﾙ設定無の場合は一括で表示するように変更
                                ''Call mChkChAnalogLRFlg(gudt.SetChInfo.udtChannel(i), intErrCnt)     '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                                If gudt.SetSystem.udtSysOps.shtLRMode = 1 And .AnalogLRMode = 0 Then
                                    bAlmLevelChk = True
                                    'Call mSetErrString("ALM HH LEVEL setting is nothing." & _
                                    '               " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                    '               "ALM HH LEVELが設定されていません" & _
                                    '               " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                    '               intErrCnt, strErrMsg)
                                End If
                                ''//

                                '' Ver1.10.7 2016.06.14  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                                'Ver2.0.7.T アナログExGusRP(RP)の場合、隠しACChkしない
                                If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                Else
                                    Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                    intErrCnt, strErrMsg)
                                End If
                                
                            Else
                                If intExtGHiHi <> 255 Or intDlyHiHi <> 255 Then
                                    Call mSetErrString("Set Value is not set in HH ALARM." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "HH ALARMに設定値が設定されていません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)

                                    '' Ver1.10.8 2016.06.28  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                                    'Ver2.0.7.T アナログExGusRP(RP)の場合、隠しACChkしない
                                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                    Else
                                        Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                         intErrCnt, strErrMsg)
                                    End If
                                    
                                End If
                            End If

                            If blnHi = True Then      '' H ALARM あり  2015.03.25
                                If intExtGHi = 255 And fnAnalogStatusAlarmRel_V2(i, 1) = False Then
                                    Call mSetErrString("EXT GROUP is not set in H ALARM." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "H ALARMにEXT GROUPが設定されていません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)
                                End If

                                If intDlyHi = 255 And fnAnalogStatusAlarmRel_V2(i, 1) = False Then
                                    Call mSetErrString("DELAY is not set in H ALARM." & _
                                                  "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                  " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                  "H ALARMにDELAYが設定されていません" & _
                                                  "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                  " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                  intErrCnt, strErrMsg)
                                End If

                                '' 関数取りやめ　　2015.11.18  Ver1.8.1
                                '' Ver1.11.8.5 2016.11.10  ｱﾗｰﾑﾚﾍﾞﾙ設定無の場合は一括で表示するように変更
                                ''Call mChkChAnalogLRFlg(gudt.SetChInfo.udtChannel(i), intErrCnt)     '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                                If gudt.SetSystem.udtSysOps.shtLRMode = 1 And .AnalogLRMode = 0 Then
                                    bAlmLevelChk = True
                                    'Call mSetErrString("ALM H LEVEL setting is nothing." & _
                                    '               " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                    '               "ALM H LEVELが設定されていません" & _
                                    '               " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                    '               intErrCnt, strErrMsg)
                                End If
                                ''//

                                '' Ver1.10.7 2016.06.14  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                                If blnHiHi = False Then
                                    'Ver2.0.7.T アナログExGusRP(RP)の場合、隠しACChkしない
                                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                    Else
                                        Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                        intErrCnt, strErrMsg)
                                    End If
                                    
                                End If

                            Else
                                If intExtGHi <> 255 Or intDlyHi <> 255 Then
                                    Call mSetErrString("Set Value is not set in H ALARM." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "H ALARMに設定値が設定されていません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)

                                    '' Ver1.10.8 2016.06.28  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                                    'Ver2.0.7.T アナログExGusRP(RP)の場合、隠しACChkしない
                                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                    Else
                                        Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                            intErrCnt, strErrMsg)
                                    End If
                                    

                                End If
                            End If

                            If blnLo = True Then      '' L ALARM あり  2015.03.25
                                If intExtGLo = 255 And fnAnalogStatusAlarmRel_V2(i, 2) = False Then
                                    Call mSetErrString("EXT GROUP is not set in L ALARM." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "L ALARMにEXT GROUPが設定されていません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)
                                End If

                                If intDlyLo = 255 And fnAnalogStatusAlarmRel_V2(i, 2) = False Then
                                    Call mSetErrString("DELAY is not set in L ALARM." & _
                                                  "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                  " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                  "L ALARMにDELAYが設定されていません" & _
                                                  "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                  " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                  intErrCnt, strErrMsg)
                                End If

                                '' 関数取りやめ　　2015.11.18  Ver1.8.1
                                '' Ver1.11.8.5 2016.11.10  ｱﾗｰﾑﾚﾍﾞﾙ設定無の場合は一括で表示するように変更
                                ''Call mChkChAnalogLRFlg(gudt.SetChInfo.udtChannel(i), intErrCnt)     '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                                If gudt.SetSystem.udtSysOps.shtLRMode = 1 And .AnalogLRMode = 0 Then
                                    bAlmLevelChk = True
                                    'Call mSetErrString("ALM L LEVEL setting is nothing." & _
                                    '               " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                    '               "ALM L LEVELが設定されていません" & _
                                    '               " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                    '               intErrCnt, strErrMsg)
                                End If
                                ''//

                                '' Ver1.10.7 2016.06.14  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                                If blnHiHi = False And blnHi = False Then
                                    'Ver2.0.7.T アナログExGusRP(RP)の場合、隠しACChkしない
                                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                    Else
                                        Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                        intErrCnt, strErrMsg)
                                    End If
                                    
                                End If

                            Else
                                If intExtGLo <> 255 Or intDlyLo <> 255 Then
                                    Call mSetErrString("Set Value is not set in L ALARM." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "L ALARMに設定値が設定されていません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)

                                    '' Ver1.10.8 2016.06.28  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                                    'Ver2.0.7.T アナログExGusRP(RP)の場合、隠しACChkしない
                                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                    Else
                                        Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                        intErrCnt, strErrMsg)
                                    End If
                                    
                                End If
                            End If

                            If blnLoLo = True Then      '' LL ALARM あり  2015.03.25
                                If intExtGLoLo = 255 And fnAnalogStatusAlarmRel_V2(i, 3) = False Then
                                    Call mSetErrString("EXT GROUP is not set in LL ALARM." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "LL ALARMにEXT GROUPが設定されていません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)
                                End If

                                If intDlyLoLo = 255 And fnAnalogStatusAlarmRel_V2(i, 3) = False Then
                                    Call mSetErrString("DELAY is not set in LL ALARM." & _
                                                  "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                  " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                  "LL ALARMにDELAYが設定されていません" & _
                                                  "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                  " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                  intErrCnt, strErrMsg)
                                End If

                                '' 関数取りやめ　　2015.11.18  Ver1.8.1
                                '' Ver1.11.8.5 2016.11.10  ｱﾗｰﾑﾚﾍﾞﾙ設定無の場合は一括で表示するように変更
                                ''Call mChkChAnalogLRFlg(gudt.SetChInfo.udtChannel(i), intErrCnt)     '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                                If gudt.SetSystem.udtSysOps.shtLRMode = 1 And .AnalogLRMode = 0 Then
                                    bAlmLevelChk = True
                                    'Call mSetErrString("ALM LL LEVEL setting is nothing." & _
                                    '               " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                    '               "ALM LL LEVELが設定されていません" & _
                                    '               " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                    '               intErrCnt, strErrMsg)
                                End If
                                ''//

                                '' Ver1.10.7 2016.06.14  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                                If blnHiHi = False And blnHi = False And blnLo = False Then
                                    'Ver2.0.7.T アナログExGusRP(RP)の場合、隠しACChkしない
                                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                    Else
                                        Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                        intErrCnt, strErrMsg)
                                    End If
                                End If
                            Else
                                If intExtGLoLo <> 255 Or intDlyLoLo <> 255 Then
                                    Call mSetErrString("Set Value is not set in LL ALARM." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "LL ALARMに設定値が設定されていません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)

                                    '' Ver1.10.8 2016.06.28  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                                    'Ver2.0.7.T アナログExGusRP(RP)の場合、隠しACChkしない
                                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                    Else
                                        Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                        intErrCnt, strErrMsg)
                                    End If
                                    
                                End If
                            End If

                            '' センサのExt.G、DLYのチェック   2015.03.16
                            If blnSen = True Then   '' センサ異常あり
                                If intExtGSen = 255 Then
                                    Call mSetErrString("EXT GROUP is not set in SENSOR FAIL." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                   "SENSOR FAILにEXT GROUPが設定されていません" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                   intErrCnt, strErrMsg)
                                End If

                                If intDlySen = 255 Then
                                    Call mSetErrString("DELAY is not set in SENSOR FAIL." & _
                                                  "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                  " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                                  "SENSOR FAILにDELAYが設定されていません" & _
                                                  "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                  " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                                  intErrCnt, strErrMsg)
                                End If

                                '' 関数取りやめ　　2015.11.18  Ver1.8.1
                                '' Ver1.11.8.5 2016.11.10  ｱﾗｰﾑﾚﾍﾞﾙ設定無の場合は一括で表示するように変更
                                ''Call mChkChAnalogLRFlg(gudt.SetChInfo.udtChannel(i), intErrCnt)     '' 2015.11.12  Ver1.7.8  ﾛｲﾄﾞ対応
                                If gudt.SetSystem.udtSysOps.shtLRMode = 1 And .AnalogLRMode = 0 Then
                                    bAlmLevelChk = True
                                    'Call mSetErrString("ALM SENSOR LEVEL setting is nothing." & _
                                    '               " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                    '               "ALM SENSOR LEVELが設定されていません" & _
                                    '               " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                    '               intErrCnt, strErrMsg)
                                End If
                                ''//

                                '' Ver1.10.7 2016.06.14  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                                If blnHiHi = False And blnHi = False And blnLo = False And blnLoLo = False Then
                                    'Ver2.0.7.T アナログExGusRP(RP)の場合、隠しACChkしない
                                    If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                    Else
                                        Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                        intErrCnt, strErrMsg)
                                    End If
                                    
                                End If

                            ElseIf intExtGSen <> 255 Then       '' Ver1.10.8 2016.06.28  隠しCHにｱﾗｰﾑ設定がある場合はACのﾁｪｯｸを行う
                                'Ver2.0.7.T アナログExGusRP(RP)の場合、隠しACChkしない
                                If .udtChCommon.shtChType = gCstCodeChTypeAnalog And .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                                Else
                                    Call ChkSCAlm(.udtChCommon.shtGroupNo, .udtChCommon.shtChno, .udtChCommon.shtFlag1, .udtChCommon.shtFlag2, _
                                    intErrCnt, strErrMsg)
                                End If
                                
                            End If

                            '' Ver1.11.8.5 2016.11.10  ｱﾗｰﾑﾚﾍﾞﾙ設定無の場合は一括で表示するように変更
                            If bAlmLevelChk = True Then
                                'Call mSetErrString("ALM LEVEL setting is nothing." & _
                                '               " , CH NO=" & .udtChCommon.shtChno & strMsgEng, _
                                '               "ALM LEVELが設定されていません" & _
                                '               " , チャンネル番号=" & .udtChCommon.shtChno & strMsgJpn, _
                                '               intErrCnt, strErrMsg)
                            End If
                        End If

                    Else
                        'Debug.Print("Index = " & i)

                    End If

                    'End If

                End With
            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Analog ... Success", " -アナログ確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Analog ... Failure", " -アナログ確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'Ver2.0.2.6 アナログCHのステータスとセット値関係処理関数
    Private Function fnAnalogStatusAlarmRel(pintI As Integer) As Integer
        Dim intValue As Integer = 0
        Dim strCboText As String = ""
        Dim strStatusS() As String

        Dim strHHstatus As String = ""
        Dim strHstatus As String = ""
        Dim strLstatus As String = ""
        Dim strLLstatus As String = ""


        With gudt.SetChInfo.udtChannel(pintI)
            '>>>ステータスの格納
            'ｽﾃｰﾀｽ名称を取得
            If .udtChCommon.shtStatus <> gCstCodeChManualInputStatus Then
                '既存ならｺｰﾄﾞから名称取得
                Call gSetComboBox(cmbStatusAnalog, gEnmComboType.ctChListChannelListStatusAnalog)
                cmbStatusAnalog.SelectedValue = .udtChCommon.shtStatus.ToString
                strCboText = cmbStatusAnalog.Text
                '「*」ならそのまま戻る
                If strCboText = "*" Then
                    Return 0
                End If
                'ｽﾃｰﾀｽを「/」で区切る
                strStatusS = strCboText.Split("/")

                'STATUSを該当箇所へ格納
                For i As Integer = 0 To UBound(strStatusS) Step 1
                    'Ver2.0.8.2 保安庁対応
                    Select Case strStatusS(i)
                        Case "HH", "高高"
                            strHHstatus = strStatusS(i)
                        Case "HIGH", "H", "EH", "高"
                            strHstatus = strStatusS(i)
                        Case "LOW", "L", "EL", "低"
                            strLstatus = strStatusS(i)
                        Case "LL", "低低"
                            strLLstatus = strStatusS(i)
                    End Select
                Next i
            Else
                'ﾏﾆｭｱﾙならマニュアル値取得
                strHHstatus = NZf(.AnalogHiHiStatusInput).Trim
                If strHHstatus.Length > 0 Then
                    If Asc(strHHstatus(0)) = 0 Then
                        strHHstatus = ""
                    End If
                End If
                strHstatus = NZf(.AnalogHiStatusInput).Trim
                If strHstatus.Length > 0 Then
                    If Asc(strHstatus(0)) = 0 Then
                        strHstatus = ""
                    End If
                End If
                strLstatus = NZf(.AnalogLoStatusInput).Trim
                If strLstatus.Length > 0 Then
                    If Asc(strLstatus(0)) = 0 Then
                        strLstatus = ""
                    End If
                End If
                strLLstatus = NZf(.AnalogLoLoStatusInput).Trim
                If strLLstatus.Length > 0 Then
                    If Asc(strLLstatus(0)) = 0 Then
                        strLLstatus = ""
                    End If
                End If
            End If



            '>>>アラームの有り無し格納
            Dim blHHuse As Boolean = False
            Dim blHuse As Boolean = False
            Dim blLuse As Boolean = False
            Dim blLLuse As Boolean = False

            Dim strEx As String = ""
            Dim strDly As String = ""
            Dim strGR1 As String = ""
            Dim strGR2 As String = ""

            '>>HH
            strEx = IIf(.AnalogHiHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiHiExtGroup))
            strDly = IIf(.AnalogHiHiDelay = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiHiDelay))
            strGR1 = IIf(.AnalogHiHiGroupRepose1 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiHiGroupRepose1))
            strGR2 = IIf(.AnalogHiHiGroupRepose2 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiHiGroupRepose2))
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blHHuse = True
            End If
            '>>H
            strEx = IIf(.AnalogHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiExtGroup))
            strDly = IIf(.AnalogHiDelay = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiDelay))
            strGR1 = IIf(.AnalogHiGroupRepose1 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiGroupRepose1))
            strGR2 = IIf(.AnalogHiGroupRepose2 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiGroupRepose2))
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blHuse = True
            End If
            '>>L
            strEx = IIf(.AnalogLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoExtGroup))
            strDly = IIf(.AnalogLoDelay = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoDelay))
            strGR1 = IIf(.AnalogLoGroupRepose1 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoGroupRepose1))
            strGR2 = IIf(.AnalogLoGroupRepose2 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoGroupRepose2))
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blLuse = True
            End If
            '>>LL
            strEx = IIf(.AnalogLoLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoLoExtGroup))
            strDly = IIf(.AnalogLoLoDelay = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoLoDelay))
            strGR1 = IIf(.AnalogLoLoGroupRepose1 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoLoGroupRepose1))
            strGR2 = IIf(.AnalogLoLoGroupRepose2 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoLoGroupRepose2))
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blLLuse = True
            End If


            '>>>判定
            'Ver2.0.6.3
            'ｱﾗｰﾑ値、ｽﾃｰﾀｽ、全て無いならエラー「99」
            If (blHHuse = False And blHuse = False And blLuse = False And blLLuse = False) And _
                (strHHstatus = "" And strHstatus = "" And strLstatus = "" And strLLstatus = "") Then
                Return 99
            End If


            'ｱﾗｰﾑ値が全て無いなら、そのまま
            If blHHuse = False And blHuse = False And blLuse = False And blLLuse = False Then
                Return 0
            End If

            '1件でも矛盾があれば各種エラー番号で戻る
            'HH 10,H 20,L 30,LL 40
            'ｽﾃｰﾀｽあり、値なし+1
            'ｽﾃｰﾀｽなし、値あり+2
            '>>HH 
            If strHHstatus = "" And blHHuse = True Then
                Return 12
            End If
            If strHHstatus <> "" And blHHuse = False Then
                Return 11
            End If
            '>>H
            If strHstatus = "" And blHuse = True Then
                Return 22
            End If
            If strHstatus <> "" And blHuse = False Then
                Return 21
            End If
            '>>L
            If strLstatus = "" And blLuse = True Then
                Return 32
            End If
            If strLstatus <> "" And blLuse = False Then
                Return 31
            End If
            '>>LL
            If strLLstatus = "" And blLLuse = True Then
                Return 42
            End If
            If strLLstatus <> "" And blLLuse = False Then
                Return 41
            End If
        End With

        Return 0
    End Function
    Private Function fnAnalogStatusAlarmRel_V2(pintI As Integer, pintFlg As Integer) As Boolean
        'pintFlg：0=HH,1=H,2=L,3=LL
        Dim strHHstatus As String = ""
        Dim strHstatus As String = ""
        Dim strLstatus As String = ""
        Dim strLLstatus As String = ""

        With gudt.SetChInfo.udtChannel(pintI)

            'Ver2.0.3.2
            'アナログCHでﾃﾞｰﾀタイプがgCstCodeChDataTypeAnalogExhRepose[49,Exhaust Gas Repose]なら常にtrueを戻す
            If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then
                If .udtChCommon.shtData = gCstCodeChDataTypeAnalogExhRepose Then
                    Return True
                End If
            End If

            '>>>アラームの有り無し格納
            Dim blHHuse As Boolean = False
            Dim blHuse As Boolean = False
            Dim blLuse As Boolean = False
            Dim blLLuse As Boolean = False

            Dim strEx As String = ""
            Dim strDly As String = ""
            Dim strGR1 As String = ""
            Dim strGR2 As String = ""

            '>>HH
            strEx = IIf(.AnalogHiHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiHiExtGroup))
            strDly = IIf(.AnalogHiHiDelay = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiHiDelay))
            strGR1 = IIf(.AnalogHiHiGroupRepose1 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiHiGroupRepose1))
            strGR2 = IIf(.AnalogHiHiGroupRepose2 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiHiGroupRepose2))
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blHHuse = True
            End If
            '>>H
            strEx = IIf(.AnalogHiExtGroup = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiExtGroup))
            strDly = IIf(.AnalogHiDelay = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiDelay))
            strGR1 = IIf(.AnalogHiGroupRepose1 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiGroupRepose1))
            strGR2 = IIf(.AnalogHiGroupRepose2 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogHiGroupRepose2))
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blHuse = True
            End If
            '>>L
            strEx = IIf(.AnalogLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoExtGroup))
            strDly = IIf(.AnalogLoDelay = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoDelay))
            strGR1 = IIf(.AnalogLoGroupRepose1 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoGroupRepose1))
            strGR2 = IIf(.AnalogLoGroupRepose2 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoGroupRepose2))
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blLuse = True
            End If
            '>>LL
            strEx = IIf(.AnalogLoLoExtGroup = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoLoExtGroup))
            strDly = IIf(.AnalogLoLoDelay = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoLoDelay))
            strGR1 = IIf(.AnalogLoLoGroupRepose1 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoLoGroupRepose1))
            strGR2 = IIf(.AnalogLoLoGroupRepose2 = gCstCodeChAnalogExtGroupNothing, "", NZf(.AnalogLoLoGroupRepose2))
            If strEx = "" And strDly = "" And strGR1 = "" And strGR2 = "" Then
            Else
                blLLuse = True
            End If


            '>>>判定
            Select Case pintFlg
                Case 0
                    'HH
                    Return blHHuse
                Case 1
                    'H
                    Return blHuse
                Case 2
                    'L
                    Return blLuse
                Case 3
                    'LL
                    Return blLLuse
                Case Else
                    Return True
            End Select
        End With

        Return True
    End Function

    '--------------------------------------------------------------------
    ' 機能      : アナログCH ﾛｲﾄﾞ設定対応　ﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub mChkChAnalogLRFlg(ByVal udtChannel As gTypSetChRec, ByRef intErrCnt As Integer)
        Try
            Dim strErrMsg() As String = Nothing
            Dim strMsgEng As String = ""
            Dim strMsgJpn As String = ""

            ''ﾛｲﾄﾞ表示　ｱﾗｰﾑﾚﾍﾞﾙ追加     2015.11.12 Ver1.7.8
            '' 参照するﾌﾗｸﾞを間違えていたので修正　　2015.11.18  Ver1.8.1
            ''If gudt.SetSystem.udtSysSystem.shtLanguage = 1 And udtChannel.AnalogLRMode = 0 Then
            If gudt.SetSystem.udtSysOps.shtLRMode = 1 And udtChannel.AnalogLRMode = 0 Then
                Call mSetErrString("ALM LEVEL setting is nothing." & _
                               " , CH NO=" & udtChannel.udtChCommon.shtChno & strMsgEng, _
                               "ALM LEVELが設定されていません" & _
                               " , チャンネル番号=" & udtChannel.udtChCommon.shtChno & strMsgJpn, _
                               intErrCnt, strErrMsg)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region

#Region "チャンネル名称チェック"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル名称確認
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : チャンネル名称を確認する
    '--------------------------------------------------------------------
    Private Sub mChkChName()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intByteCount As Integer     '' Ver1.11.8.2 2016.11.01 ｺﾒﾝﾄ解除

            ''チャンネルが設定されている場合
            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then

                        ''名称がない場合
                        If gGetString(.udtChCommon.strChitem) = "" Then
                            Call mSetErrString("Channel name is not set." & _
                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                               " , CH NO=" & .udtChCommon.shtChno, _
                                               "チャンネル名称が設定されていません。" & _
                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                               intErrCnt, strErrMsg)
                        End If

                        '' Ver1.10.5 2016.05.09
                        If .udtChCommon.strChitem.IndexOf("　") > 0 Then
                            Call mSetErrString("Double-byte space is included in the ch name." & _
                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                               " , CH NO=" & .udtChCommon.shtChno, _
                                               "チャンネル名称に全角スペースが含まれています。" & _
                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                               intErrCnt, strErrMsg)
                        End If
                        ''//

                        '' Ver1.11.8.2 2016.11.01 英文仕様時の日本語ﾁｪｯｸ
                        If gudt.SetSystem.udtSysSystem.shtLanguage = 0 Then
                            intByteCount = System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(gGetString(.udtChCommon.strChitem))
                            If intByteCount <> Len(gGetString(.udtChCommon.strChitem)) Then
                                Call mSetErrString("Double-byte char is included in the ch name." & _
                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                               " , CH NO=" & .udtChCommon.shtChno, _
                                               "チャンネル名称に全角文字が含まれています。" & _
                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                               intErrCnt, strErrMsg)
                            End If
                        End If

                        ' ''文字のバイト数取得
                        'intByteCount = System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(gGetString(.udtChCommon.strChitem))

                        ' ''バイト数と文字数が異なる場合
                        'If intByteCount <> Len(gGetString(.udtChCommon.strChitem)) Then
                        '    Call mSetErrString("The channel name is not correct. " & _
                        '                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                        '                       " , CH NO=" & .udtChCommon.shtChno, _
                        '                       intErrCnt, strErrMsg)
                        'End If

                    End If

                End With

            Next


            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Channel Name ... Success", " -チャンネル名称確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Channel Name ... Failure", " -チャンネル名称確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "手動入力項目チェック"

    '--------------------------------------------------------------------
    ' 機能      : 手動入力項目チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 手動入力項目を確認する
    '--------------------------------------------------------------------
    Private Sub mChkChManualInput()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intFuNo As Integer
            Dim intPortNo As Integer
            Dim intPin As Integer

            ''チャンネルが設定されている場合
            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then

                        Select Case .udtChCommon.shtChType
                            Case gCstCodeChTypeAnalog

                                '================
                                ''アナログ
                                '================
                                ''ステータス手動入力の場合
                                If .udtChCommon.shtStatus = gCstCodeChManualInputStatus Then

                                    ''HiHi使用の場合
                                    If .AnalogHiHiUse <> 0 Then

                                        ''ステータス入力チェック
                                        If gGetString(.AnalogHiHiStatusInput) = "" Then
                                            Call mSetErrString("Status name is not set." & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno & _
                                                               " , Position=HiHi", _
                                                               "ステータス名が設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                               " , 未設定箇所=上上限", _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    End If

                                    ''Hi使用の場合
                                    If .AnalogHiUse <> 0 Then

                                        ''ステータス入力チェック
                                        If gGetString(.AnalogHiStatusInput) = "" Then
                                            Call mSetErrString("Status name is not set." & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno & _
                                                               " , Position=Hi", _
                                                               "ステータス名が設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                               " , 未設定箇所=上限", _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    End If

                                    ''Lo使用の場合
                                    If .AnalogLoUse <> 0 Then

                                        ''ステータス入力チェック
                                        If gGetString(.AnalogLoStatusInput) = "" Then
                                            Call mSetErrString("Status name is not set." & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno & _
                                                               " , Position=Lo", _
                                                               "ステータス名が設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                               " , 未設定箇所=下限", _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    End If

                                    ''LoLo使用の場合
                                    If .AnalogLoLoUse <> 0 Then

                                        ''ステータス入力チェック
                                        If gGetString(.AnalogLoLoStatusInput) = "" Then
                                            Call mSetErrString("Status name is not set." & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno & _
                                                               " , Position=LoLo", _
                                                               "ステータス名が設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                               " , 未設定箇所=下下限", _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    End If

                                    '▼▼▼ 20110311 センサーフェイルの手動入力チェックは行わない ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                                    ' ''SensorFail使用の場合
                                    'If .AnalogSensorFailUse <> 0 Then

                                    '    ''ステータス入力チェック
                                    '    If gGetString(.AnalogSensorFailStatusInput) = "" Then
                                    '        Call mSetErrString("Status name is not set." & _
                                    '                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                    '                           " , CH NO=" & .udtChCommon.shtChno & _
                                    '                           " , Position=SensorFail", _
                                    '                           "ステータス名が設定されていません。" & _
                                    '                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                    '                           " , チャンネル番号=" & .udtChCommon.shtChno & _
                                    '                           " , 未設定箇所=センサーフェイル", _
                                    '                           intErrCnt, strErrMsg)
                                    '    End If

                                    'End If
                                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                                End If

                            Case gCstCodeChTypeDigital

                                If .udtChCommon.shtData <> gCstCodeChDataTypeDigitalDeviceStatus Then

                                    '================
                                    ''デジタル
                                    '================
                                    ''ステータス手動入力の場合
                                    If .udtChCommon.shtStatus = gCstCodeChManualInputStatus Then

                                        ''ステータス入力チェック
                                        If gGetString(.udtChCommon.strStatus) = "" Then
                                            Call mSetErrString("Status name is not set." & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno, _
                                                               "ステータス名が設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    End If

                                Else

                                    '================
                                    ''システム
                                    '================
                                    ''システムはステータス手動入力なし

                                End If

                            Case gCstCodeChTypeMotor
                                '================
                                ''モーター
                                '================
                                ''（入力側）ステータス手動入力の場合
                                If .udtChCommon.shtStatus = gCstCodeChManualInputStatus Then

                                    ''ステータス入力チェック
                                    If gGetString(.udtChCommon.strStatus) = "" Then
                                        Call mSetErrString("Status name is not set." & _
                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                           "ステータス名が設定されていません。" & _
                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                           intErrCnt, strErrMsg)
                                    End If

                                End If

                                ''（出力側）ステータス手動入力の場合
                                If .MotorStatus = gCstCodeChManualInputStatus Then

                                    ''ステータス入力チェック
                                    If gGetString(.MotorOutStatus1) = "" And _
                                       gGetString(.MotorOutStatus2) = "" And _
                                       gGetString(.MotorOutStatus3) = "" And _
                                       gGetString(.MotorOutStatus4) = "" And _
                                       gGetString(.MotorOutStatus5) = "" And _
                                       gGetString(.MotorOutStatus6) = "" And _
                                       gGetString(.MotorOutStatus7) = "" And _
                                       gGetString(.MotorOutStatus8) = "" Then

                                        ''FUアドレス情報を取得
                                        intFuNo = .MotorFuNo        ''Fu No
                                        intPortNo = .MotorPortNo    ''Port No
                                        intPin = .MotorPin          ''Pin

                                        ''FU番号、ポート、端子番号が全て 0 か 65535 の場合は入力なしでもOK
                                        If Not ((intFuNo = 0 And intPortNo = 0 And intPin = 0) Or _
                                                (intFuNo = gCstCodeChNotSetFuNo And intPortNo = gCstCodeChNotSetFuPort And intPin = gCstCodeChNotSetFuPin)) Then

                                            Call mSetErrString("Status name is not set." & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno, _
                                                               "ステータス名が設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    End If

                                End If

                            Case gCstCodeChTypeValve
                                '================
                                ''バルブ
                                '================
                                Select Case .udtChCommon.shtData
                                    Case gCstCodeChDataTypeValveDI_DO

                                        '----------------
                                        ''バルブDI-DO
                                        '----------------
                                        ''（入力側）ステータス手動入力の場合
                                        If .udtChCommon.shtStatus = gCstCodeChManualInputStatus Then

                                            ''ステータス入力チェック
                                            If gGetString(.udtChCommon.strStatus) = "" Then
                                                Call mSetErrString("Status name is not set." & _
                                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                                   "ステータス名が設定されていません。" & _
                                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                   intErrCnt, strErrMsg)
                                            End If

                                        End If

                                        ''（出力側）なし

                                    Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2

                                        '----------------
                                        ''バルブAI-DO
                                        '----------------
                                        ''（入力側）ステータス手動入力の場合
                                        If .udtChCommon.shtStatus = gCstCodeChManualInputStatus Then

                                            ''HiHi使用の場合
                                            If .ValveAiDoHiHiUse <> 0 Then

                                                ''ステータス入力チェック
                                                If gGetString(.ValveAiDoHiHiStatusInput) = "" Then
                                                    Call mSetErrString("Status name is not set." & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno & _
                                                                       " , Position=HiHi", _
                                                                       "ステータス名が設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                                       " , 未設定箇所=上上限", _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            End If

                                            ''Hi使用の場合
                                            If .ValveAiDoHiUse <> 0 Then

                                                ''ステータス入力チェック
                                                If gGetString(.ValveAiDoHiStatusInput) = "" Then
                                                    Call mSetErrString("Status name is not set." & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno & _
                                                                       " , Position=Hi", _
                                                                       "ステータス名が設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                                       " , 未設定箇所=上限", _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            End If

                                            ''Lo使用の場合
                                            If .ValveAiDoLoUse <> 0 Then

                                                ''ステータス入力チェック
                                                If gGetString(.ValveAiDoLoStatusInput) = "" Then
                                                    Call mSetErrString("Status name is not set." & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno & _
                                                                       " , Position=Lo", _
                                                                       "ステータス名が設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                                       " , 未設定箇所=下限", _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            End If

                                            ''LoLo使用の場合
                                            If .ValveAiDoLoLoUse <> 0 Then

                                                ''ステータス入力チェック
                                                If gGetString(.ValveAiDoLoLoStatusInput) = "" Then
                                                    Call mSetErrString("Status name is not set." & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno & _
                                                                       " , Position=LoLo", _
                                                                       "ステータス名が設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                                       " , 未設定箇所=下下限", _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            End If

                                            '▼▼▼ 20110311 センサーフェイルの手動入力チェックは行わない ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                                            ' ''SensorFail使用の場合
                                            'If .ValveAiDoSensorFailUse <> 0 Then

                                            '    ''ステータス入力チェック
                                            '    If gGetString(.ValveAiDoSensorFailStatusInput) = "" Then
                                            '        Call mSetErrString("Status name is not set." & _
                                            '                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            '                           " , CH NO=" & .udtChCommon.shtChno & _
                                            '                           " , Position=SensorFail", _
                                            '                           "ステータス名が設定されていません。" & _
                                            '                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            '                           " , チャンネル番号=" & .udtChCommon.shtChno & _
                                            '                           " , 未設定箇所=センサーフェイル", _
                                            '                           intErrCnt, strErrMsg)
                                            '    End If

                                            'End If
                                            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                                        End If

                                        ''（出力側）なし

                                    Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2

                                        '----------------
                                        ''バルブAI-AO
                                        '----------------
                                        ''（入力側）ステータス手動入力の場合
                                        If .udtChCommon.shtStatus = gCstCodeChManualInputStatus Then

                                            ''HiHi使用の場合
                                            If .ValveAiAoHiHiUse <> 0 Then

                                                ''ステータス入力チェック
                                                If gGetString(.ValveAiAoHiHiStatusInput) = "" Then
                                                    Call mSetErrString("Status name is not set." & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno & _
                                                                       " , Position=HiHi", _
                                                                       "ステータス名が設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                                       " , 未設定箇所=上上限", _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            End If

                                            ''Hi使用の場合
                                            If .ValveAiAoHiUse <> 0 Then

                                                ''ステータス入力チェック
                                                If gGetString(.ValveAiAoHiStatusInput) = "" Then
                                                    Call mSetErrString("Status name is not set." & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno & _
                                                                       " , Position=Hi", _
                                                                       "ステータス名が設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                                       " , 未設定箇所=上限", _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            End If

                                            ''Lo使用の場合
                                            If .ValveAiAoLoUse <> 0 Then

                                                ''ステータス入力チェック
                                                If gGetString(.ValveAiAoLoStatusInput) = "" Then
                                                    Call mSetErrString("Status name is not set." & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno & _
                                                                       " , Position=Lo", _
                                                                       "ステータス名が設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                                       " , 未設定箇所=下限", _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            End If

                                            ''LoLo使用の場合
                                            If .ValveAiAoLoLoUse <> 0 Then

                                                ''ステータス入力チェック
                                                If gGetString(.ValveAiAoLoLoStatusInput) = "" Then
                                                    Call mSetErrString("Status name is not set." & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno & _
                                                                       " , Position=LoLo", _
                                                                       "ステータス名が設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno & _
                                                                       " , 未設定箇所=下下限", _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            End If

                                            '▼▼▼ 20110311 センサーフェイルの手動入力チェックは行わない ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                                            ' ''SensorFail使用の場合
                                            'If .ValveAiAoSensorFailUse <> 0 Then

                                            '    ''ステータス入力チェック
                                            '    If gGetString(.ValveAiAoSensorFailStatusInput) = "" Then
                                            '        Call mSetErrString("Status name is not set." & _
                                            '                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                            '                           " , CH NO=" & .udtChCommon.shtChno & _
                                            '                           " , Position=SensorFail", _
                                            '                           "ステータス名が設定されていません。" & _
                                            '                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                            '                           " , チャンネル番号=" & .udtChCommon.shtChno & _
                                            '                           " , 未設定箇所=センサーフェイル", _
                                            '                           intErrCnt, strErrMsg)
                                            '    End If

                                            'End If
                                            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                                        End If

                                        ''（出力側）ステータス手動入力の場合
                                        If .ValveAiAoOutStatus = gCstCodeChManualInputStatus Then

                                            ''ステータス入力チェック
                                            If gGetString(.ValveAiAoOutStatus1) = "" Then
                                                Call mSetErrString("Status name is not set." & _
                                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                                   "ステータス名が設定されていません。" & _
                                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                   intErrCnt, strErrMsg)
                                            End If

                                        End If

                                    Case gCstCodeChDataTypeValveDO

                                        '----------------
                                        ''バルブDO
                                        '----------------
                                        ''（入力側）なし
                                        ''（出力側）なし

                                    Case gCstCodeChDataTypeValveAO_4_20

                                        '----------------
                                        ''バルブAO
                                        '----------------
                                        ''（入力側）なし
                                        ''（出力側）なし

                                End Select

                            Case gCstCodeChTypeComposite
                                '================
                                ''コンポジット
                                '================
                                ''コンポジットはコンポジットテーブルに設定が入っているので後でやる

                            Case gCstCodeChTypePulse
                                '================
                                ''パルス
                                '================
                                ''パルスはステータス手動入力なし

                        End Select

                        ''単位手動入力の場合
                        If .udtChCommon.shtUnit = gCstCodeChManualInputUnit Then

                            ''単位入力チェック
                            If gGetString(.udtChCommon.shtUnit) = "" Then
                                Call mSetErrString("Unit name is not set." & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                   "単位名が設定されていません。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                   intErrCnt, strErrMsg)
                            End If

                        End If

                    End If

                End With

            Next

            ''コンポジットテーブル確認
            For i As Integer = 0 To UBound(gudt.SetChComposite.udtComposite)

                ''チャンネルが設定されている場合
                If gudt.SetChComposite.udtComposite(i).shtChid <> 0 Then

                    For j As Integer = 0 To UBound(gudt.SetChComposite.udtComposite(i).udtCompInf)

                        ''Bit使用の場合
                        If gBitCheck(gudt.SetChComposite.udtComposite(i).udtCompInf(j).bytAlarmUse, 0) Then

                            ''ステータス名称が設定されていない場合
                            If gGetString(gudt.SetChComposite.udtComposite(i).udtCompInf(j).strStatusName) = "" Then

                                Call mSetErrString("Status name is not set." & _
                                                   "[Info]CompositeTableNo=" & i + 1 & _
                                                   " , BitPos=" & j + 1, _
                                                   "ステータス名が設定されていません。" & _
                                                   "[情報]コンポジットテーブル番号=" & i + 1 & _
                                                   " , ビット位置=" & j + 1, _
                                                   intErrCnt, strErrMsg)
                            End If

                            If gudt.SetChComposite.udtComposite(i).shtDiFilter <> 12 Then
                                Call mSetErrString("Composite FILTER setting is not 12." & _
                                                   "[Info]CompositeTableNo=" & i + 1 & _
                                                   " , BitPos=" & j + 1, _
                                                   "Composite FILTER 設定値が12ではありません" & _
                                                   "[情報]コンポジットテーブル番号=" & i + 1 & _
                                                   " , ビット位置=" & j + 1, _
                                                   intErrCnt, strErrMsg)
                            End If

                        End If
                    Next
                End If
            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking ManualInput Name ... Success", " -手動入力確認 ... OK")
            Else

                Call mAddMsgText(" -Checking ManualInput Name ... Failure", " -手動入力確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Exit Sub

    End Sub

#End Region

#Region "延長警報グループチェック"

    '--------------------------------------------------------------------
    ' 機能      : 延長警報グループチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 延長警報グループを確認する
    '--------------------------------------------------------------------
    Private Sub mChkChExtGroup()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            'Dim intExtGroupCommon As Integer
            'Dim intExtGroupChTypeHH As Integer
            'Dim intExtGroupChTypeH As Integer
            'Dim intExtGroupChTypeL As Integer
            'Dim intExtGroupChTypeLL As Integer
            'Dim intStatusOutput As Integer

            ''チャンネルが設定されている場合
            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then

                        '▼▼▼ 20110215 警報が無い場合は延長警報グループの確認を行わない ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                        If gChkAlarmUse(.udtChCommon.shtChno) Then

                            Select Case .udtChCommon.shtChType
                                Case gCstCodeChTypeAnalog

                                    '==================
                                    ''アナログCH
                                    '==================
                                    ''上上限チェック
                                    If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorHiHi _
                                    Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLoLoNorHiHi Then

                                        ''上上限の延長警報グループが設定なしの場合
                                        If .AnalogHiHiExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                            Call mSetErrString("Analog Alarm HiHi Ext.G is not set. " & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno, _
                                                               "上上限の延長警報グループが設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                               intErrCnt, strErrMsg)
                                        Else

                                            ''上上限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                            If .AnalogHiHiDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                Call mSetErrString("Analog Alarm HiHi Delay Timer is not set. " & _
                                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                                   "上上限の遅延タイマー値が設定されていません。" & _
                                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                   intErrCnt, strErrMsg)
                                            End If

                                        End If

                                    End If

                                    ''上限チェック
                                    If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorHigh _
                                    Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLowNorHigh _
                                    Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorEHigh _
                                    Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogELowNorEHigh Then

                                        ''上限の延長警報グループが設定なしの場合
                                        If .AnalogHiExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                            Call mSetErrString("Analog Alarm Hi Ext.G is not set. " & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno, _
                                                               "上限の延長警報グループが設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                               intErrCnt, strErrMsg)
                                        Else

                                            ''上限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                            If .AnalogHiDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                Call mSetErrString("Analog Alarm Hi Delay Timer is not set. " & _
                                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                                   "上限の遅延タイマー値が設定されていません。" & _
                                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                   intErrCnt, strErrMsg)
                                            End If

                                        End If

                                    End If

                                    ''下限チェック
                                    If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorLow _
                                    Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLowNorHigh _
                                    Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorELow _
                                    Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogELowNorEHigh Then

                                        ''下限の延長警報グループが設定なしの場合
                                        If .AnalogLoExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                            Call mSetErrString("Analog Alarm Lo Ext.G is not set. " & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno, _
                                                               "下限の延長警報グループが設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                               intErrCnt, strErrMsg)
                                        Else

                                            ''下限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                            If .AnalogLoDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                Call mSetErrString("Analog Alarm Lo Delay Timer is not set. " & _
                                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                                   "下限の遅延タイマー値が設定されていません。" & _
                                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                   intErrCnt, strErrMsg)
                                            End If

                                        End If

                                    End If

                                    ''下下限チェック
                                    If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorLoLo _
                                    Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLoLoNorHiHi Then

                                        ''下下限の延長警報グループが設定なしの場合
                                        If .AnalogLoLoExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                            Call mSetErrString("Analog Alarm LoLo Ext.G is not set. " & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno, _
                                                               "下下限の延長警報グループが設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                               intErrCnt, strErrMsg)
                                        Else

                                            ''下下限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                            If .AnalogLoLoDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                Call mSetErrString("Analog Alarm LoLo Delay Timer is not set. " & _
                                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                                   "下下限の遅延タイマー値が設定されていません。" & _
                                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                   intErrCnt, strErrMsg)
                                            End If

                                        End If

                                    End If

                                Case gCstCodeChTypeDigital

                                    If .udtChCommon.shtData <> gCstCodeChDataTypeDigitalDeviceStatus Then

                                        '==================
                                        ''デジタルCH
                                        '==================
                                        ''ステータスがなし以外で延長警報グループが設定されていない場合
                                        If .udtChCommon.shtStatus <> gCstCodeChStatusDigitalNothing And _
                                           .udtChCommon.shtExtGroup = gCstCodeChCommonExtGroupNothing Then
                                            '全部なしならﾁｪｯｸしない
                                            If .udtChCommon.shtDelay = gCstCodeChCommonExtGroupNothing Or .udtChCommon.shtGRepose1 = gCstCodeChCommonExtGroupNothing Or .udtChCommon.shtGRepose2 = gCstCodeChCommonExtGroupNothing Or .udtChCommon.shtM_ReposeSet = 0 Then
                                            Else
                                                Call mSetErrString("Digital Alarm Ext.G is not set. " & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno, _
                                                               "デジタルの延長警報グループが設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                               intErrCnt, strErrMsg)
                                            End If

                                        Else

                                            ''延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                            If .udtChCommon.shtDelay = gCstCodeChCommonDelayTimerNothing Then
                                                Call mSetErrString("Digital Alarm Delay Timer is not set. " & _
                                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                                   "遅延タイマー値が設定されていません。" & _
                                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                   intErrCnt, strErrMsg)
                                            End If

                                        End If

                                    Else

                                        '==================
                                        ''システムCH
                                        '==================
                                        ''延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                        If .udtChCommon.shtExtGroup <> gCstCodeChCommonExtGroupNothing And _
                                           .udtChCommon.shtDelay = gCstCodeChCommonDelayTimerNothing Then

                                            Call mSetErrString("System Alarm Delay Timer is not set. " & _
                                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                                   "遅延タイマー値が設定されていません。" & _
                                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                   intErrCnt, strErrMsg)
                                        End If

                                    End If

                                Case gCstCodeChTypeValve

                                    '==================
                                    ''バルブCH
                                    '==================
                                    Select Case .udtChCommon.shtData

                                        Case gCstCodeChDataTypeValveDI_DO

                                            ''Input側とOutput側のどちらかが警報なし以外だった場合
                                            If .udtChCommon.shtStatus <> gCstCodeChStatusDigitalNothing _
                                            Or .ValveDiDoStatus <> gCstCodeChStatusDigitalNothing Then

                                                ''延長警報が設定されていない場合
                                                If .udtChCommon.shtExtGroup = gCstCodeChCommonExtGroupNothing Then
                                                    'Call mSetErrString("Valve Alarm Ext.G is not set. " & _
                                                    '                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                    '                   " , CH NO=" & .udtChCommon.shtChno, _
                                                    '                   "バルブの延長警報グループが設定されていません。" & _
                                                    '                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                    '                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                    '                   intErrCnt, strErrMsg)
                                                Else

                                                    ''延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                                    'If .udtChCommon.shtDelay = gCstCodeChCommonDelayTimerNothing Then
                                                    '    Call mSetErrString("Valve Alarm Delay Timer is not set. " & _
                                                    '                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                    '                       " , CH NO=" & .udtChCommon.shtChno, _
                                                    '                       "遅延タイマー値が設定されていません。" & _
                                                    '                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                    '                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                    '                       intErrCnt, strErrMsg)
                                                    'End If

                                                End If

                                            End If

                                        Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2

                                            ''上上限チェック
                                            If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorHiHi _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLoLoNorHiHi Then

                                                ''上上限の延長警報グループが設定なしの場合
                                                If .ValveAiDoHiHiExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                                    Call mSetErrString("Valve AI Alarm HiHi Ext.G is not set. " & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                                       "上上限の延長警報グループが設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                       intErrCnt, strErrMsg)
                                                Else

                                                    ''上上限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                                    If .ValveAiDoHiHiDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                        Call mSetErrString("Valve AI Alarm HiHi Delay Timer is not set. " & _
                                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                                           "上上限の遅延タイマー値が設定されていません。" & _
                                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                           intErrCnt, strErrMsg)
                                                    End If

                                                End If

                                            End If

                                            ''上限チェック
                                            If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorHigh _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLowNorHigh _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorEHigh _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogELowNorEHigh Then

                                                ''上限の延長警報グループが設定なしの場合
                                                If .ValveAiDoHiExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                                    Call mSetErrString("Valve AI Alarm Hi Ext.G is not set. " & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                                       "上限の延長警報グループが設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                       intErrCnt, strErrMsg)
                                                Else

                                                    ''上限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                                    If .ValveAiDoHiDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                        Call mSetErrString("Valve AI Alarm Hi Delay Timer is not set. " & _
                                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                                           "上限の遅延タイマー値が設定されていません。" & _
                                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                           intErrCnt, strErrMsg)
                                                    End If

                                                End If

                                            End If

                                            ''下限チェック
                                            If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorLow _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLowNorHigh _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorELow _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogELowNorEHigh Then

                                                ''下限の延長警報グループが設定なしの場合
                                                If .ValveAiDoLoExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                                    Call mSetErrString("Valve AI Alarm Lo Ext.G is not set. " & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                                       "下限の延長警報グループが設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                       intErrCnt, strErrMsg)
                                                Else

                                                    ''下限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                                    If .ValveAiDoLoDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                        Call mSetErrString("Valve AI Alarm Lo Delay Timer is not set. " & _
                                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                                           "下限の遅延タイマー値が設定されていません。" & _
                                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                           intErrCnt, strErrMsg)
                                                    End If

                                                End If

                                            End If

                                            ''下下限チェック
                                            If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorLoLo _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLoLoNorHiHi Then

                                                ''下下限の延長警報グループが設定なしの場合
                                                If .ValveAiDoLoLoExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                                    Call mSetErrString("Valve AI Alarm LoLo Ext.G is not set. " & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                                       "下下限の延長警報グループが設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                       intErrCnt, strErrMsg)
                                                Else

                                                    ''下下限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                                    If .ValveAiDoLoLoDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                        Call mSetErrString("Valve AI Alarm LoLo Delay Timer is not set. " & _
                                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                                           "下下限の遅延タイマー値が設定されていません。" & _
                                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                           intErrCnt, strErrMsg)
                                                    End If

                                                End If

                                            End If

                                        Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2

                                            ''上上限チェック
                                            If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorHiHi _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLoLoNorHiHi Then

                                                ''上上限の延長警報グループが設定なしの場合
                                                If .ValveAiAoHiHiExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                                    Call mSetErrString("Valve AI Alarm HiHi Ext.G is not set. " & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                                       "上上限の延長警報グループが設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                       intErrCnt, strErrMsg)
                                                Else

                                                    ''上上限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                                    If .ValveAiAoHiHiDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                        Call mSetErrString("Valve AI Alarm HiHi Delay Timer is not set. " & _
                                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                                           "上上限の遅延タイマー値が設定されていません。" & _
                                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                           intErrCnt, strErrMsg)
                                                    End If

                                                End If

                                            End If

                                            ''上限チェック
                                            If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorHigh _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLowNorHigh _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorEHigh _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogELowNorEHigh Then

                                                ''上限の延長警報グループが設定なしの場合
                                                If .ValveAiAoHiExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                                    Call mSetErrString("Valve AI Alarm Hi Ext.G is not set. " & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                                       "上限の延長警報グループが設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                       intErrCnt, strErrMsg)
                                                Else

                                                    ''上限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                                    If .ValveAiAoHiDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                        Call mSetErrString("Valve AI Alarm Hi Delay Timer is not set. " & _
                                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                                           "上限の遅延タイマー値が設定されていません。" & _
                                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                           intErrCnt, strErrMsg)
                                                    End If

                                                End If

                                            End If

                                            ''下限チェック
                                            If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorLow _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLowNorHigh _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorELow _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogELowNorEHigh Then

                                                ''下限の延長警報グループが設定なしの場合
                                                If .ValveAiAoLoExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                                    Call mSetErrString("Valve AI Alarm Lo Ext.G is not set. " & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                                       "下限の延長警報グループが設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                       intErrCnt, strErrMsg)
                                                Else

                                                    ''下限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                                    If .ValveAiAoLoDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                        Call mSetErrString("Valve AI Alarm Lo Delay Timer is not set. " & _
                                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                                           "下限の遅延タイマー値が設定されていません。" & _
                                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                           intErrCnt, strErrMsg)
                                                    End If

                                                End If

                                            End If

                                            ''下下限チェック
                                            If .udtChCommon.shtStatus = gCstCodeChStatusAnalogNorLoLo _
                                            Or .udtChCommon.shtStatus = gCstCodeChStatusAnalogLoLoNorHiHi Then

                                                ''下下限の延長警報グループが設定なしの場合
                                                If .ValveAiAoLoLoExtGroup = gCstCodeChAnalogExtGroupNothing Then
                                                    Call mSetErrString("Valve AI Alarm LoLo Ext.G is not set. " & _
                                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                                       "下下限の延長警報グループが設定されていません。" & _
                                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                       intErrCnt, strErrMsg)
                                                Else

                                                    ''下下限の延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                                    If .ValveAiAoLoLoDelay = gCstCodeChAnalogDelayTimerNothing Then
                                                        Call mSetErrString("Valve AI Alarm LoLo Delay Timer is not set. " & _
                                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                                           "下下限の遅延タイマー値が設定されていません。" & _
                                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                                           intErrCnt, strErrMsg)
                                                    End If

                                                End If

                                            End If

                                        Case gCstCodeChDataTypeValveAO_4_20
                                        Case gCstCodeChDataTypeValveDO
                                        Case gCstCodeChDataTypeValveJacom
                                        Case gCstCodeChDataTypeValveJacom55
                                        Case gCstCodeChDataTypeValveExt

                                    End Select


                                Case gCstCodeChTypeMotor

                                    '==================
                                    ''モーターCH
                                    '==================
                                    '▼▼▼ 20110127 モーターCHは除外 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                                    ' ''延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                    'If .udtChCommon.shtExtGroup <> gCstCodeChCommonExtGroupNothing And _
                                    '   .udtChCommon.shtDelay = gCstCodeChCommonDelayTimerNothing Then

                                    '    Call mSetErrString("Motor Alarm Delay Timer is not set. " & _
                                    '                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                    '                           " , CH NO=" & .udtChCommon.shtChno, _
                                    '                           "遅延タイマー値が設定されていません。" & _
                                    '                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                    '                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                    '                           intErrCnt, strErrMsg)
                                    'End If
                                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                                Case gCstCodeChTypeComposite

                                    '==================
                                    ''コンポジットCH
                                    '==================
                                    ''延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                    If .udtChCommon.shtExtGroup <> gCstCodeChCommonExtGroupNothing And _
                                       .udtChCommon.shtDelay = gCstCodeChCommonDelayTimerNothing Then

                                        Call mSetErrString("Composite Alarm Delay Timer is not set. " & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno, _
                                                               "遅延タイマー値が設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                               intErrCnt, strErrMsg)
                                    End If

                                Case gCstCodeChTypePulse

                                    '==================
                                    ''パルスCH
                                    '==================
                                    ''延長警報グループが設定されていて遅延タイマー値が設定されていない場合
                                    If .udtChCommon.shtExtGroup <> gCstCodeChCommonExtGroupNothing And _
                                       .udtChCommon.shtDelay = gCstCodeChCommonDelayTimerNothing Then

                                        Call mSetErrString("Pulse Alarm Delay Timer is not set. " & _
                                                               "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                               " , CH NO=" & .udtChCommon.shtChno, _
                                                               "遅延タイマー値が設定されていません。" & _
                                                               "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                               " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                               intErrCnt, strErrMsg)
                                    End If

                            End Select

                        End If

                    End If

                End With

            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Ext.G Setting ... Success", " -延長警報グループ確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Ext.G Setting ... Failure", " -延長警報グループ確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "運転積算CH設定数チェック"

    '--------------------------------------------------------------------
    ' 機能      : 運転積算CH設定数チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 運転積算CH設定数が256以内かチェックする
    '--------------------------------------------------------------------
    Private Sub mChkChRunHourCnt()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intCnt As Integer = 0

            ''チャンネルが設定されている場合
            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno <> 0 Then

                        ''パルス積算CHの場合
                        If .udtChCommon.shtChType = gCstCodeChTypePulse Then

                            ''データ種別が運転積算の場合
                            '' Ver1.11.8.3 2016.11.08  運転積算 通信CH追加
                            'If .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalHour _
                            'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalMin _
                            'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayHour _
                            'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayMin _
                            'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapHour _
                            'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapMin _
                            'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDev Then
                            '' Ver1.12.0.1 2017.01.13  関数に変更
                            If gChkRunHourCH(.udtChCommon._shtChno) Then

                                intCnt += 1

                                ''トリガCHが設定されているか
                                If gudt.SetChInfo.udtChannel(i).RevoTrigerChid = 0 Then

                                    '▼▼▼ 20110308 トリガCHが設定されていなくてもワークCHならエラーとしない ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                                    If Not gBitCheck(.udtChCommon.shtFlag1, gCstCodeChCommonFlagBitPosWk) Then

                                        Call mSetErrString("Pulse revolution trriger channel is not set. " & _
                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                           "運転積算チャンネルにトリガチャンネルが設定されていません。" & _
                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                           intErrCnt, strErrMsg)

                                    End If
                                    '-------------------------------------------------------------------------------------------------------
                                    'Call mSetErrString("Pulse revolution trriger channel is not set. " & _
                                    '                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                    '                   " , CH NO=" & .udtChCommon.shtChno, _
                                    '                   "運転積算チャンネルにトリガチャンネルが設定されていません。" & _
                                    '                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                    '                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                    '                   intErrCnt, strErrMsg)
                                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
                                Else

                                    Dim intChType As Integer = 0
                                    Dim intDataType As Integer = 0
                                    Dim intFuNo As Integer = 0
                                    Dim intFuPort As Integer = 0
                                    Dim intFuPin As Integer = 0

                                    ''トリガCHがチャンネルリストに存在するか
                                    If Not gExistChNo(gudt.SetChInfo.udtChannel(i).RevoTrigerChid, intChType, intDataType, intFuNo, intFuPort, intFuPin) Then

                                        Call mSetErrString("Trigger channel set CH NO [" & gudt.SetChInfo.udtChannel(i).RevoTrigerChid & "] doesn't exist in channel setting. " & _
                                                           "[Info]Revo CH NO=" & .udtChCommon.shtChno, _
                                                           "運転積算トリガで設定したチャンネル番号 [" & gudt.SetChInfo.udtChannel(i).RevoTrigerChid & "] はチャンネルリストに登録されていません。" & _
                                                           "[情報]運転積算チャンネル番号=" & .udtChCommon.shtChno, _
                                                           intErrCnt, strErrMsg)

                                    Else

                                        ''トリガCHのチャンネルタイプはデジタルorモーターか
                                        If intChType <> gCstCodeChTypeDigital And _
                                           intChType <> gCstCodeChTypeMotor Then

                                            Call mSetErrString("Trigger channel type is not Digital or Moter. " & _
                                                               "[Info]CH NO=" & gudt.SetChInfo.udtChannel(i).RevoTrigerChid, _
                                                               "運転積算トリガで設定したチャンネルがデジタルチャンネル又はモーターチャンネルではありません。" & _
                                                               "[情報]チャンネル番号=" & gudt.SetChInfo.udtChannel(i).RevoTrigerChid, _
                                                               intErrCnt, strErrMsg)

                                        End If

                                        If (intChType = gCstCodeChTypeMotor And intDataType = gCstCodeChDataTypeMotorDeviceJacom) Or _
                                           (intChType = gCstCodeChTypeMotor And intDataType = gCstCodeChDataTypeMotorDeviceJacom55) Then
                                            'JACOMの場合アドレスチェック無し 2013.07.25 K.Fujimoto
                                        Else
                                            ''運転積算CHとトリガCHのFUアドレスが同一か
                                            If .udtChCommon.shtFuno <> intFuNo _
                                            Or .udtChCommon.shtPortno <> intFuPort _
                                            Or .udtChCommon.shtPin <> intFuPin Then

                                                Call mSetErrString("Trigger channel fu address is not the same as revo channel fu address. " & _
                                                                   "[Info]Trigger channel fu address=" & gConvFuAddress(intFuNo, intFuPort, intFuPin) & _
                                                                   " , Revo channel fu address=" & gConvFuAddress(.udtChCommon.shtFuno, .udtChCommon.shtPortno, .udtChCommon.shtPin), _
                                                                   "運転積算チャンネルとトリガチャンネルのFUアドレスが一致しません。" & _
                                                                   "[情報]運転積算トリガチャンネルFUアドレス=" & gConvFuAddress(intFuNo, intFuPort, intFuPin) & _
                                                                   " , 運転積算チャンネルFUアドレス=" & gConvFuAddress(.udtChCommon.shtFuno, .udtChCommon.shtPortno, .udtChCommon.shtPin), _
                                                                   intErrCnt, strErrMsg)

                                            End If

                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End With
            Next

            ''運転積算が設定されている数をチェック
            If intCnt > gCstCntChPulseRevoMax Then
                Call mSetErrString("Run Hour CH set count over " & gCstCntChPulseRevoMax & ".", _
                                   "運転積算チャンネルの設定数が " & gCstCntChPulseRevoMax & " を超えています。", intErrCnt, strErrMsg)

            End If

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Run Hour CH Count ... Success", " -運転積算チャンネル確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Run Hour CH Count ... Failure", " -運転積算チャンネル確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "運転積算CH設定"
    'Ver2.0.0.2 南日本M761対応 2017.02.27追加
    '--------------------------------------------------------------------
    ' 機能      : 運転積算CH設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 運転積算テーブルのTriggerCHをChInfoのTriggerCHへ設定する
    '--------------------------------------------------------------------
    Private Sub mSetChRunHour()
        Try
            ''チャンネルが設定されている場合
            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)
                With gudt.SetChInfo.udtChannel(i)
                    If .udtChCommon.shtChno <> 0 Then
                        ''パルス積算CHの場合
                        If .udtChCommon.shtChType = gCstCodeChTypePulse Then
                            ''データ種別が運転積算の場合
                            If gChkRunHourCH(.udtChCommon._shtChno) Then
                                '運転積算テーブルのTriggerCHを設定
                                For j As Integer = 0 To UBound(gudt.SetChRunHour.udtDetail) Step 1
                                    'CHnoが一致
                                    If gudt.SetChRunHour.udtDetail(j).shtChid = .udtChCommon.shtChno Then
                                        'TrigerSysNoとCHnoをChInfoのTrigerへ格納
                                        .RevoTrigerSysno = gudt.SetChRunHour.udtDetail(j).shtTrgSysno
                                        .RevoTrigerChid = gudt.SetChRunHour.udtDetail(j).shtTrgChid 'Ver2.0.0.5
                                        Exit For
                                    End If
                                Next j
                            End If
                        End If
                    End If
                End With
            Next i

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "RLフラグチェック"

    '--------------------------------------------------------------------
    ' 機能      : RLフラグチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : レギュラーログイン時フラグがアナログCHと運転積算CHに立っているか確認する
    '--------------------------------------------------------------------
    Private Sub mChkChFlagRL()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intCnt As Integer = 0


            'Ver2.0.8.4 デマンドCSV保存がある場合はRLチェックしない
            Dim j As Integer = 0
            Dim x As Integer = 0
            Dim z As Integer = 0
            Dim blCSV As Boolean = False
            With gudt.SetOpsPulldownMenuM
                For j = 0 To UBound(.udtDetail) Step 1
                    For x = 0 To UBound(.udtDetail(j).udtGroup) Step 1
                        For z = 0 To UBound(.udtDetail(j).udtGroup(x).udtSub) Step 1
                            '30-15---があるならHCプリンタが必須
                            If _
                                .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType1 = 30 And _
                                .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType2 = 15 _
                            Then
                                blCSV = True
                                Exit For
                            End If
                        Next z
                    Next x
                Next j
            End With
            '-

            'Ver2.0.8.4 デマンドCSV保存がある場合はRLチェックしない
            If blCSV = False Then

                ''チャンネルが設定されている場合
                For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                    With gudt.SetChInfo.udtChannel(i)

                        If .udtChCommon.shtChno <> 0 Then

                            ''アナログCHの場合
                            If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then

                                ''RLフラグを確認
                                If Not gBitCheck(.udtChCommon.shtFlag2, gCstCodeChCommonFlagBitPosRL) Then
                                    Call mSetErrString("RL Flag is not set. " & _
                                                       "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                       " , CH NO=" & .udtChCommon.shtChno, _
                                                       "レギュラーログフラグが設定されていません。" & _
                                                       "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                       " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                       intErrCnt, strErrMsg)
                                End If

                            End If

                            ''パルス積算CHの場合
                            If .udtChCommon.shtChType = gCstCodeChTypePulse Then

                                ''データ種別が運転積算の場合
                                '' Ver1.11.8.3 2016.11.08  運転積算 通信CH追加
                                'If .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalHour _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoTotalMin _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayHour _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoDayMin _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapHour _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoLapMin _
                                'Or .udtChCommon.shtData = gCstCodeChDataTypePulseRevoExtDev Then
                                '' Ver1.12.0.1 関数に変更
                                If gChkRunHourCH(.udtChCommon._shtChno) Then

                                    ''RLフラグを確認
                                    If Not gBitCheck(.udtChCommon.shtFlag2, gCstCodeChCommonFlagBitPosRL) Then
                                        Call mSetErrString("RL Flag is not set. " & _
                                                           "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                           " , CH NO=" & .udtChCommon.shtChno, _
                                                           "レギュラーログフラグが設定されていません。" & _
                                                           "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                           " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                           intErrCnt, strErrMsg)
                                    End If
                                End If
                            End If
                        End If
                    End With
                Next

                'Ver2.0.8.4 デマンドCSV保存がある場合はRLチェックしない
            End If


            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking RL Flag ... Success", " -レギュラーログフラグ確認 ... OK")
            Else

                Call mAddMsgText(" -Checking RL Flag ... Failure", " -レギュラーログフラグ確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "パルス積算設定チェック"

    '--------------------------------------------------------------------
    ' 機能      : パルス積算設定チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : パルス積算チャンネルの設定をチェックする
    '--------------------------------------------------------------------
    Private Sub mChkChPulse()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intCnt As Integer = 0
            Dim udtFuInfo() As gTypFuInfo = Nothing

            ''FU情報取得
            Call gMakeFuInfoStructure(udtFuInfo)

            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i).udtChCommon

                    ''チャンネルが設定されている場合
                    If .shtChno <> 0 Then

                        ''パルス積算CHの場合
                        If .shtChType = gCstCodeChTypePulse Then

                            ''設定数カウントアップ
                            intCnt += 1

                            '▼▼▼ 20110215 パルス積算CHの端子位置は１以外でもOK ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼

                            ''パルス積算CHが設定されている端子から8端子以内にCHが設定されていないこと
                            If .shtPin <> gCstCodeChNotSetFuPin Then

                                For j As Integer = .shtPin To .shtPin + 8 - 1 - 1

                                    If j <= UBound(udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin) Then

                                        If udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin(j).strChNo <> 0 Then

                                            Call mSetErrString("Other channel are set between 1-8. " & _
                                                               "[PulseCH]Group=" & .shtGroupNo & _
                                                               " , CH NO=" & .shtChno & _
                                                               " , FuAddress=" & gConvFuAddress(.shtFuno, .shtPortno, .shtPin) & _
                                                               "[OtherCH]Group=" & udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin(j).strGroupNo & _
                                                               " , CH NO=" & udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin(j).strChNo & _
                                                               " , FuAddress=" & gConvFuAddress(.shtFuno, .shtPortno, j + 1), _
                                                               "パルス積算チャンネルが設定されている端子から8端子以内にチャンネルが設定されています。" & _
                                                               "[パルス積算チャンネル]グループ=" & .shtGroupNo & _
                                                               " , チャンネル番号=" & .shtChno & _
                                                               " , FUアドレス=" & gConvFuAddress(.shtFuno, .shtPortno, .shtPin) & _
                                                               "[重複チャンネル]グループ=" & udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin(j).strGroupNo & _
                                                               " , チャンネル番号=" & udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin(j).strChNo & _
                                                               " , FUアドレス=" & gConvFuAddress(.shtFuno, .shtPortno, j + 1), _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    End If

                                Next

                            End If
                            '-------------------------------------------------------------------------------------------------------------------------------------
                            ' ''FUアドレスは１から設定してあるか
                            'If .shtPin > 1 Then
                            '    Call mSetErrString("Pulse channel FU Address Pin No is not 1. " & _
                            '                       "[Info]Group=" & .shtGroupNo & _
                            '                       " , CH NO=" & .shtChno, _
                            '                       "パルス積算チャンネルのFUアドレスの計測点番号に 1 以外が設定されています。" & _
                            '                       "[情報]グループ=" & .shtGroupNo & _
                            '                       " , チャンネル番号=" & .shtChno, _
                            '                       intErrCnt, strErrMsg)
                            'Else

                            '    If .shtPin = 1 Then

                            '        ''パルス積算CHが設定されている端子から8端子以内にCHが設定されていないこと
                            '        For j As Integer = 0 + 1 To 8 - 1
                            '            If udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin(j).strChNo <> 0 Then

                            '                Call mSetErrString("Other channel are set between 1-8. " & _
                            '                                   "[PulseCH]Group=" & .shtGroupNo & _
                            '                                   " , CH NO=" & .shtChno & _
                            '                                   " , FuAddress=" & gConvFuAddress(.shtFuno, .shtPortno, .shtPin) & _
                            '                                   "[OtherCH]Group=" & udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin(j).strGroupNo & _
                            '                                   " , CH NO=" & udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin(j).strChNo & _
                            '                                   " , FuAddress=" & gConvFuAddress(.shtFuno, .shtPortno, j + 1), _
                            '                                   "パルス積算チャンネルが設定されている端子から8端子以内にチャンネルが設定されています。" & _
                            '                                   "[パルス積算チャンネル]グループ=" & .shtGroupNo & _
                            '                                   " , チャンネル番号=" & .shtChno & _
                            '                                   " , FUアドレス=" & gConvFuAddress(.shtFuno, .shtPortno, .shtPin) & _
                            '                                   "[重複チャンネル]グループ=" & udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin(j).strGroupNo & _
                            '                                   " , チャンネル番号=" & udtFuInfo(.shtFuno).udtFuPort(.shtPortno - 1).udtFuPin(j).strChNo & _
                            '                                   " , FUアドレス=" & gConvFuAddress(.shtFuno, .shtPortno, j + 1), _
                            '                                   intErrCnt, strErrMsg)
                            '            End If
                            '        Next
                            '    End If
                            'End If
                            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                        End If
                    End If
                End With
            Next

            ''パルス積算チャンネル数をチェック
            If intCnt > gCstCntChPulseMax Then
                Call mSetErrString("Pulse CH set count over " & gCstCntChPulseMax & ".", _
                                   "パルス積算チャンネルの設定数が " & gCstCntChPulseRevoMax & " を超えています。", intErrCnt, strErrMsg)
            End If

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Pulse CH Setting ... Success", " -パルスチャンネル設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Pulse CH Setting ... Failure", " -パルスチャンネル設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "延長警報グループ設定時の遅延タイマーチェック"

    ''--------------------------------------------------------------------
    '' 機能      : 延長警報グループ設定時の遅延タイマーチェック
    '' 返り値    : なし
    '' 引き数    : ARG1 - (I ) なし
    '' 機能説明  : 延長警報グループ設定時のみ、遅延タイマーが設定されているかチェックする
    ''--------------------------------------------------------------------
    'Private Sub mChkExtGDelayTimer()

    '    Try

    '        Dim intErrCnt As Integer = 0
    '        Dim strErrMsg() As String = Nothing

    '        For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

    '            With gudt.SetChInfo.udtChannel(i).udtChCommon

    '                ''チャンネルが設定されている場合
    '                If .shtChno <> 0 Then

    '                    ''延長警報グループが設定されている場合
    '                    If .shtExtGroup <> gCstCodeChCommonExtGroupNothing Then

    '                        ''遅延タイマーが設定されていない場合
    '                        If .shtDelay = gCstCodeChCommonExtGroupNothing Then
    '                            Call mSetErrString("Delay Timer Value is not set. " & _
    '                                               "[Info]Group=" & .shtGroupNo & _
    '                                               " , CH NO=" & .shtChno, _
    '                                               "遅延タイマー値が設定されていません。 " & _
    '                                               "[情報]グループ=" & .shtGroupNo & _
    '                                               " , チャンネル番号=" & .shtChno, _
    '                                               intErrCnt, strErrMsg)
    '                        End If

    '                    End If
    '                End If
    '            End With
    '        Next

    '        ''結果表示
    '        If intErrCnt = 0 Then
    '            Call mAddMsgText(" -Checking Ext.G and Delay Timer ... Success", " -延長警報グループ、遅延タイマー設定確認 ... OK")
    '        Else

    '            Call mAddMsgText(" -Checking Ext.G and Delay Timer ... Failure", " -延長警報グループ、遅延タイマー設定確認 ... エラー")

    '            ''失敗時はエラー内容を追記
    '            For i As Integer = 0 To UBound(strErrMsg)
    '                Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
    '                Call mSetErrString(strErrMsg(i), strErrMsg(i))
    '                If mblnCancelFlg Then Return
    '            Next

    '        End If

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

#End Region

#End Region

#Region "ターミナル情報チェック"

    '--------------------------------------------------------------------
    ' 機能      : ターミナル情報チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : ターミナル情報のチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkTerminalInfo()

        Try

            ''ターミナル情報チェック開始
            Call mAddMsgText("[Check Terminal Info]", "[ターミナル設定確認]")

            ''使用設定チェック
            Call mChkFuUse()
            If mblnCancelFlg Then Return

            ''出力設定(DO)チェック
            Call mChkFuOutputDO()
            If mblnCancelFlg Then Return

            ' ''出力設定(AO)チェック
            'Call mChkFuOutputAO()
            'If mblnCancelFlg Then Return

            'Ver2.0.4.0
            '基板と計測点アナログCHのタイプが一致しているかﾁｪｯｸ
            Call mChkFuType()
            If mblnCancelFlg Then Return


            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "使用設定チェック"

    '--------------------------------------------------------------------
    ' 機能      : RLフラグチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : レギュラーログイン時フラグがアナログCHと運転積算CHに立っているか確認する
    '--------------------------------------------------------------------
    Private Sub mChkFuUse()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim blnFlg As Boolean

            For i As Integer = 0 To UBound(gudt.SetFu.udtFu)

                ''FU使用の場合
                If gudt.SetFu.udtFu(i).shtUse = gCstCodeFuUse Then

                    ''名称が設定されていない場合
                    If gGetString(gudt.SetChDisp.udtChDisp(i).strFuName) = "" Then
                        Call mSetErrString("FU name is not set. " & _
                                           "[Info]FuNo=" & i, _
                                           "FU名称が設定されていません。" & _
                                           "[情報]FU番号=" & i, _
                                           intErrCnt, strErrMsg)
                    End If

                    ''タイプが設定されていない場合
                    If gGetString(gudt.SetChDisp.udtChDisp(i).strFuType) = "" Then
                        Call mSetErrString("FU type is not set. " & _
                                           "[Info]FuNo=" & i, _
                                           "FUタイプが設定されていません" & _
                                           "[情報]FU番号=" & i, _
                                           intErrCnt, strErrMsg)
                    End If

                    ''TB1～8の中で１つ以上スロット種別が設定されているか
                    blnFlg = False
                    For j As Integer = 0 To UBound(gudt.SetFu.udtFu(i).udtSlotInfo)
                        If gudt.SetFu.udtFu(i).udtSlotInfo(j).shtType <> gCstCodeFuSlotTypeNothing Then
                            blnFlg = True
                        End If
                    Next

                    If Not blnFlg Then
                        Call mSetErrString("FU slot type is not set to all slots. " & "[Info]FuNo=" & i, _
                                           "全てのスロットにFUスロット種別が設定されていません。" & "[情報]FU番号=" & i, intErrCnt, strErrMsg)
                    End If

                End If

            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking FU use setting ... Success", " -FU使用設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking FU use setting ... Failure", " -FU使用設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "出力設定(DO)チェック"

    '--------------------------------------------------------------------
    ' 機能      : 出力設定(DO)チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 出力設定(DO)のチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkFuOutputDO()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intArrNo As String = 0
            Dim intSetCnt As Integer = 0
            Dim intChNo() As Integer = Nothing
            Dim intStatus() As Integer = Nothing

            For i As Integer = 0 To gCstCntFuOutputDoMax - 1

                With gudt.SetChOutput.udtCHOutPut(i)

                    ''出力タイプが設定されている場合
                    If .bytOutput <> gCstCodeFuOutputTypeInvalid And _
                       .bytOutput <> 255 Then

                        ''CH出力タイプがCHデータの場合
                        If .bytType = gCstCodeFuOutputChTypeCh Then

                            ''CylinderCH1が設定されていない場合
                            If .shtChid = 0 Then
                                Call mSetErrString("CH output set (DO) Cylinder CH not set. " & _
                                                   "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                   "出力チャンネル設定（DO）でシリンダチャンネルが設定されていません。" & _
                                                   "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                   intErrCnt, strErrMsg)
                            Else

                                ''チャンネル番号がチャンネル設定に存在すること
                                If Not gExistChNo(.shtChid) Then
                                    Call mSetErrString("CH output set (DO) Cylinder CH NO [" & .shtChid & "] doesn't exist in channel setting. " & _
                                                       "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                       "出力チャンネル設定（DO）でシリンダチャンネルに設定したチャンネル番号 [" & .shtChid & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                       intErrCnt, strErrMsg)
                                End If

                                ''出力タイプが「CH(-,-)」「RUN(-,LT)」の場合
                                If .bytOutput = gCstCodeFuOutputTypeCh____ Or _
                                   .bytOutput = gCstCodeFuOutputTypeRun__LT Then

                                    Select Case .bytStatus
                                        Case gCstCodeFuOutputStatusAlarm

                                            ''設定CHにアラームが設定すること
                                            If Not gChkAlarmUse(.shtChid) Then
                                                Call mSetErrString("CH output set (DO) Cylinder CH NO [" & .shtChid & "] is not alarm setting. " & _
                                                                   "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                   "出力チャンネル設定（DO）でシリンダチャンネルに設定したチャンネル番号 [" & .shtChid & "] はアラームが設定されていません。" & _
                                                                   "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                   intErrCnt, strErrMsg)
                                            End If

                                        Case gCstCodeFuOutputStatusMotor

                                            ''設定CHがモーター or コンポジットであること
                                            If Not (gGetChannelType(.shtChid) = gCstCodeChTypeMotor _
                                            Or gGetChannelType(.shtChid) = gCstCodeChTypeComposite _
                                            Or gGetChannelType(.shtChid) = gCstCodeChTypeValve) Then
                                                Call mSetErrString("CH output set (DO) Cylinder CH NO [" & .shtChid & "] is not motor or composite cannel. " & _
                                                                   "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                   "出力チャンネル設定（DO）でシリンダチャンネルに設定したチャンネル番号 [" & .shtChid & "] はモーター又はコンポジットチャンネルではありません。" & _
                                                                   "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                   intErrCnt, strErrMsg)
                                            End If

                                        Case gCstCodeFuOutputStatusOnOff

                                            ''設定CHがデジタルCHであること
                                            If gGetChannelType(.shtChid) <> gCstCodeChTypeDigital Then
                                                '' MOTOR CH RUN/STOP(MO)の条件追加　ver1.4.0 2011.09.02
                                                If gGetChannelType(.shtChid) = gCstCodeChTypeMotor Then
                                                    'Ver2.0.0.2 モーター種別増加 R Device ADD
                                                    If gGetChNoToDataTypeCode(.shtChid) <> gCstCodeChDataTypeMotorDevice And gGetChNoToDataTypeCode(.shtChid) <> gCstCodeChDataTypeMotorDeviceJacom And gGetChNoToDataTypeCode(.shtChid) <> gCstCodeChDataTypeMotorRDevice And _
                                                       gGetChNoToDataTypeCode(.shtChid) <> gCstCodeChDataTypeMotorDeviceJacom55 Then
                                                        Call mSetErrString("CH output set (DO) Cylinder CH NO [" & .shtChid & "] is not ON/OFF status. " & _
                                                                       "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                       "出力チャンネル設定（DO）でシリンダチャンネルに設定したチャンネル番号 [" & .shtChid & "] はON/OFFステータスではありません。" & _
                                                                       "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                       intErrCnt, strErrMsg)
                                                    End If
                                                Else
                                                    Call mSetErrString("CH output set (DO) Cylinder CH NO [" & .shtChid & "] is not digital cannel. " & _
                                                                       "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                       "出力チャンネル設定（DO）でシリンダチャンネルに設定したチャンネル番号 [" & .shtChid & "] はデジタルチャンネルではありません。" & _
                                                                       "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                       intErrCnt, strErrMsg)
                                                End If
                                            End If

                                    End Select

                                End If

                            End If

                        Else

                            ''論理出力設定ファイルの対象配列番号を取得
                            'Select Case .bytType
                            '    Case gCstCodeFuOutputChTypeOr

                            '        ''論理出力Orの場合はCH IDがそのまま配列番号となる
                            '        intArrNo = .shtChid - 1

                            '    Case gCstCodeFuOutputChTypeAnd

                            '        ''論理出力Andの場合はOrレコードの後ろにAndレコードの
                            '        ''情報が入るのでOrレコード数が配列番号となる
                            '        intArrNo = gCstCntFuAndOrRecCntOr - 1

                            'End Select

                            ''CH IDがそのまま配列番号となる
                            intArrNo = .shtChid - 1

                            ''２つ以上のチャンネルが設定されていること
                            intSetCnt = 0
                            Erase intChNo
                            Erase intStatus     '' ver.1.4.5 2012.07.03
                            For j As Integer = 0 To UBound(gudt.SetChAndOr.udtCHOut(intArrNo).udtCHAndOr)

                                ''チャンネルが設定されている場合はチャンネル番号を保存してカウントアップ
                                If gudt.SetChAndOr.udtCHOut(intArrNo).udtCHAndOr(j).shtChid <> 0 Then
                                    ReDim Preserve intChNo(intSetCnt)
                                    ReDim Preserve intStatus(intSetCnt)
                                    intChNo(intSetCnt) = gudt.SetChAndOr.udtCHOut(intArrNo).udtCHAndOr(j).shtChid
                                    intStatus(intSetCnt) = gudt.SetChAndOr.udtCHOut(intArrNo).udtCHAndOr(j).bytStatus
                                    intSetCnt += 1
                                End If

                            Next

                            ''設定チャンネル数チェック
                            If Not (intSetCnt >= 2) Then
                                Call mSetErrString("CH output set (DO) Cylinder CH set count under 2. " & _
                                                   "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                   "出力チャンネル設定（DO）に２つ以上のシリンダチャンネルが設定されていません。" & _
                                                   "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                   intErrCnt, strErrMsg)
                            Else

                                For k As Integer = 0 To UBound(intChNo)

                                    ''チャンネル番号がチャンネル設定に存在すること
                                    If Not gExistChNo(intChNo(k)) Then
                                        Call mSetErrString("CH output set (DO) Cylinder CH NO [" & .shtChid & "] doesn't exist in channel setting. " & _
                                                           "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                           "出力チャンネル設定（DO）でシリンダチャンネルに設定したチャンネル番号 [" & intChNo(k) & "] はチャンネルリストに登録されていません。" & _
                                                           "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                           intErrCnt, strErrMsg)
                                    End If

                                    ''出力タイプが「CH(-,-)」「RUN(-,LT)」の場合
                                    If .bytOutput = gCstCodeFuOutputTypeCh____ Or _
                                       .bytOutput = gCstCodeFuOutputTypeRun__LT Then

                                        Select Case intStatus(k)    '' ver.1.4.5 2012.07.03
                                            Case gCstCodeFuOutputStatusAlarm

                                                ''設定CHにアラームが設定すること
                                                If Not gChkAlarmUse(intChNo(k)) Then
                                                    Call mSetErrString("CH output set (DO) Cylinder CH NO [" & .shtChid & "] is not alarm setting. " & _
                                                                       "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                       "出力チャンネル設定（DO）でシリンダチャンネルに設定したチャンネル番号 [" & intChNo(k) & "] はアラームが設定されていません。" & _
                                                                       "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            Case gCstCodeFuOutputStatusMotor

                                                ''設定CHがモーター or コンポジットであること      '' ver.1.4.5 2012.07.03 条件変更
                                                If Not (gGetChannelType(intChNo(k)) = gCstCodeChTypeMotor _
                                                Or gGetChannelType(intChNo(k)) = gCstCodeChTypeComposite _
                                                Or gGetChannelType(intChNo(k)) = gCstCodeChTypeValve) Then

                                                    Call mSetErrString("CH output set (DO) Cylinder CH NO [" & .shtChid & "] is not motor or composite cannel. " & _
                                                                       "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                       "出力チャンネル設定（DO）でシリンダチャンネルに設定したチャンネル番号 [" & intChNo(k) & "] はモーター又はコンポジットチャンネルではありません。" & _
                                                                       "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            Case gCstCodeFuOutputStatusOnOff

                                                ''設定CHがデジタルCHであること
                                                If gGetChannelType(intChNo(k)) <> gCstCodeChTypeDigital Then
                                                    Call mSetErrString("CH output set (DO) Cylinder CH NO [" & .shtChid & "] is not digital cannel. " & _
                                                                       "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                       "出力チャンネル設定（DO）でシリンダチャンネルに設定したチャンネル番号 [" & intChNo(k) & "] はデジタルチャンネルではありません。" & _
                                                                       "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                        End Select

                                    End If

                                Next
                            End If
                        End If
                    End If
                End With
            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Output DO setting ... Success", " -出力設定（DO）確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Output DO setting ... Failure", " -出力設定（DO）確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "出力設定(AO)チェック"

    ''--------------------------------------------------------------------
    '' 機能      : 出力設定(AO)チェック
    '' 返り値    : なし
    '' 引き数    : ARG1 - (I ) なし
    '' 機能説明  : 出力設定(AO)のチェックを行う
    ''--------------------------------------------------------------------
    'Private Sub mChkFuOutputAO()

    '    Try

    '        Dim intErrCnt As Integer = 0
    '        Dim strErrMsg() As String = Nothing
    '        Dim intArrNo As String = 0
    '        Dim intSetCnt As Integer = 0
    '        Dim intChNo() As Integer = Nothing

    '        For i As Integer = gCstCntFuOutputDoMax To gCstCntFuOutputDoMax + gCstCntFuOutputAoMax - 1

    '            With gudt.SetChOutput.udtCHOutPut(i)

    '                ''チャンネルが設定されていない場合
    '                If .shtChid = 0 Then

    '                    ''AO側はDOと違いCH,And,OrのタイプがないのでCH=0がチャンネル番号の
    '                    ''設定忘れなのか単に空のレコードなのかがわからない
    '                    ''そのため、CH番号の設定確認は行わない（行えない）

    '                    'Call mSetErrString("CH output set (AO) CH NO not set. " & _
    '                    '                   "[Info]FU Address=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
    '                    '                   intErrCnt, strErrMsg)

    '                Else

    '                    ''チャンネル番号がチャンネル設定に存在すること
    '                    If Not gExistChNo(.shtChid) Then
    '                        Call mSetErrString("CH output set (AO) CH NO [" & .shtChid & "] doesn't exist in channel setting. " & _
    '                                           "[Info]FuAddress=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
    '                                           "出力チャンネル設定（AO）で設定したチャンネル番号 [" & .shtChid & "] はチャンネルリストに登録されていません。" & _
    '                                           "[情報]FUアドレス=" & gConvFuAddress(.bytFuno, .bytPortno, .bytPin), _
    '                                           intErrCnt, strErrMsg)
    '                    End If

    '                End If
    '            End With
    '        Next

    '        ''結果表示
    '        If intErrCnt = 0 Then
    '            Call mAddMsgText(" -Checking Output AO setting ... Success", " -出力設定（AO）確認 ... OK")
    '        Else

    '            Call mAddMsgText(" -Checking Output AO setting ... Failure", " -出力設定（AO）確認 ... エラー")

    '            ''失敗時はエラー内容を追記
    '            For i As Integer = 0 To UBound(strErrMsg)
    '                Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
    '                Call mSetErrString(strErrMsg(i), strErrMsg(i))
    '                If mblnCancelFlg Then Return
    '            Next

    '        End If

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

#End Region

#Region "基板と計測点チェック"
    '--------------------------------------------------------------------
    ' 機能      : 基板と計測点チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 基板タイプと計測点アナログCHのタイプが一致しているかﾁｪｯｸする
    '--------------------------------------------------------------------
    Private Sub mChkFuType()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            Dim i As Integer

            Dim blPTorJPT As Boolean = False

            Dim intFUno As Integer
            Dim intPortNo As Integer
            Dim intPinNo As Integer
            Dim blFUerr As Boolean

            Dim blKekka As Boolean = False

            'FCU設定からPT,JPTを取得
            If gudt.SetSystem.udtSysFcu.shtPtJptFlg <> 0 Then
                blPTorJPT = True
            End If

            With gudt.SetChInfo
                For i = 0 To UBound(.udtChannel) Step 1
                    blKekka = False
                    'CHNoが入っていること
                    If .udtChannel(i).udtChCommon.shtChno <> 0 Then
                        'アナログCHであること
                        If .udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeAnalog Then
                            'ﾃﾞｰﾀﾀｲﾌﾟが2PT,2JPT,3PT,3JPTのいずれかであること
                            If .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog2Pt Or _
                                .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog2Jpt Or _
                                .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog3Pt Or _
                                .udtChannel(i).udtChCommon.shtData = gCstCodeChDataTypeAnalog3Jpt Then

                                '該当CHのFUｱﾄﾞﾚｽ取得(Pinは不要だが取得しておく)
                                intFUno = .udtChannel(i).udtChCommon.shtFuno
                                intPortNo = .udtChannel(i).udtChCommon.shtPortno
                                intPinNo = .udtChannel(i).udtChCommon.shtPin

                                'FUｱﾄﾞﾚｽが範囲内であること
                                blFUerr = False
                                'FU=0-20,Port=1-8,Pinはﾁｪｯｸ不要
                                If intFUno < 0 Or intFUno > 20 Then
                                    blFUerr = True
                                End If
                                If intPortNo < 1 Or intPortNo > 8 Then
                                    blFUerr = True
                                End If
                                If blFUerr = False Then
                                    '該当FUの基板によって処理分岐してChInfoのﾃﾞｰﾀﾀｲﾌﾟ書き換え
                                    Select Case gudt.SetFu.udtFu(intFUno).udtSlotInfo(intPortNo - 1).shtType
                                        Case 4
                                            'M100A=2線式
                                            If blPTorJPT = True Then
                                                'JPT
                                                If .udtChannel(i).udtChCommon.shtData <> gCstCodeChDataTypeAnalog2Jpt Then
                                                    blKekka = True
                                                End If
                                            Else
                                                'PT
                                                If .udtChannel(i).udtChCommon.shtData <> gCstCodeChDataTypeAnalog2Pt Then
                                                    blKekka = True
                                                End If
                                            End If
                                        Case 5
                                            'M110A=3線式
                                            If blPTorJPT = True Then
                                                'JPT
                                                If .udtChannel(i).udtChCommon.shtData <> gCstCodeChDataTypeAnalog3Jpt Then
                                                    blKekka = True
                                                End If
                                            Else
                                                'PT
                                                If .udtChannel(i).udtChCommon.shtData <> gCstCodeChDataTypeAnalog3Pt Then
                                                    blKekka = True
                                                End If
                                            End If
                                        Case Else
                                            '該当外は何もしない
                                    End Select

                                    If blKekka = True Then
                                        Call mSetErrString( _
                                            "FU Type and CH Type Mismatch. " & "[Info]ChNo=" & .udtChannel(i).udtChCommon.shtChno, _
                                            "基板と計測点が一致しません。" & "[情報]ChNo=" & .udtChannel(i).udtChCommon.shtChno, _
                                           intErrCnt, strErrMsg)
                                    End If
                                End If
                            End If
                        End If
                    End If

                    'Ver2.0.4.9
                    'OUT_FUで指定されている基板がOUTじゃないならエラー
                    'モーターCHとDIDO,AIDO,AIAOが対象
                    Dim intOchk As Integer = 0
                    Select Case .udtChannel(i).udtChCommon.shtChType
                        Case gCstCodeChTypeMotor
                            intOchk = 1
                        Case gCstCodeChTypeValve
                            'DIDO,AIDO,AIDOのみ
                            Select Case .udtChannel(i).udtChCommon.shtData
                                Case gCstCodeChDataTypeValveDI_DO, gCstCodeChDataTypeValveDO, gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55, gCstCodeChDataTypeValveExt
                                    'DIDO
                                    intOchk = 2
                                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2, gCstCodeChDataTypeValvePT_DO2
                                    'AIDO
                                    intOchk = 3
                                Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2, gCstCodeChDataTypeValvePT_AO2, gCstCodeChDataTypeValveAO_4_20
                                    'AIAO
                                    intOchk = 4
                            End Select
                    End Select
                    'OUTがある可能性のCH以外は処理しない
                    If intOchk <> 0 Then
                        'FUｱﾄﾞﾚｽ取得
                        Select Case intOchk
                            Case 1
                                'Motor
                                intFUno = .udtChannel(i).MotorFuNo
                                intPortNo = .udtChannel(i).MotorPortNo
                                intPinNo = .udtChannel(i).MotorPin
                            Case 2
                                'DIDO
                                intFUno = .udtChannel(i).ValveDiDoFuNo
                                intPortNo = .udtChannel(i).ValveDiDoPortNo
                                intPinNo = .udtChannel(i).ValveDiDoPin
                            Case 3
                                'AIDO
                                intFUno = .udtChannel(i).ValveAiDoFuNo
                                intPortNo = .udtChannel(i).ValveAiDoPortNo
                                intPinNo = .udtChannel(i).ValveAiDoPin
                            Case 4
                                'AIAO
                                intFUno = .udtChannel(i).ValveAiAoFuNo
                                intPortNo = .udtChannel(i).ValveAiAoPortNo
                                intPinNo = .udtChannel(i).ValveAiAoPin
                        End Select
                        'FUｱﾄﾞﾚｽが範囲内であること
                        blFUerr = False
                        'FU=0-20,Port=1-8,Pinはﾁｪｯｸ不要
                        If intFUno < 0 Or intFUno > 20 Then
                            blFUerr = True
                        End If
                        If intPortNo < 1 Or intPortNo > 8 Then
                            blFUerr = True
                        End If
                        If blFUerr = False Then
                            Select Case gudt.SetFu.udtFu(intFUno).udtSlotInfo(intPortNo - 1).shtType
                                Case gCstCodeFuSlotTypeDO, gCstCodeFuSlotTypeAO
                                Case Else
                                    'OUT_FUがOUT基板じゃない場合エラー
                                    Call mSetErrString( _
                                            "CH OUT Adress is not OUT Terminal. " & "[Info]ChNo=" & .udtChannel(i).udtChCommon.shtChno, _
                                            "計測点出力アドレスがOUT基板ではありません。" & "[情報]ChNo=" & .udtChannel(i).udtChCommon.shtChno, _
                                           intErrCnt, strErrMsg)
                            End Select
                        End If
                    End If
                Next i
            End With




            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking FU and CH Type setting ... Success", " -基板タイプと計測点タイプ確認 ... OK")
            Else

                Call mAddMsgText(" -Checking FU and CH Type setting ... Failure", " -FU基板タイプと計測点タイプ確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region

#End Region

#Region "その他チャンネル情報チェック"

    '--------------------------------------------------------------------
    ' 機能      : その他チャンネル情報チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : その他チャンネル情報のチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkOtherChannelInfo()

        Try

            ''その他チャンネル情報チェック開始
            Call mAddMsgText("[Check Other Channel Info]", "[その他チャンネル情報確認]")

            ''グループリポーズチェック
            Call mChkChGroupRepose()
            If mblnCancelFlg Then Return

            ''運転積算チェック
            Call mChkChRunHour()
            If mblnCancelFlg Then Return

            ''排ガスグループチェック
            Call mChkChExhGus()
            If mblnCancelFlg Then Return

            ''コントロール使用可/不可チェック
            Call mChkChControlUseNotuse(gudt.SetChCtrlUseM, True)
            Call mChkChControlUseNotuse(gudt.SetChCtrlUseC, False)
            If mblnCancelFlg Then Return

            ''SIO設定チェック
            Call mChkChSIO()
            If mblnCancelFlg Then Return

            ''データ保存テーブルチェック
            Call mChkChDataSaveTable()
            If mblnCancelFlg Then Return

            ''チャンネルリスト行間チェック
            Call mChkChBlankRow(mblnChkBlank)
            If mblnCancelFlg Then Return

            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "グループリポーズチェック"

    '--------------------------------------------------------------------
    ' 機能      : グループリポーズチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : グループリポーズチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChGroupRepose()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            For i As Integer = 0 To UBound(gudt.SetChGroupRepose.udtRepose)

                With gudt.SetChGroupRepose.udtRepose(i)

                    ''チャンネルが設定されている場合
                    If .shtChId <> 0 Then
                        Dim intChT As Integer = -1
                        Dim intDataT As Integer = -1
                        Dim intFu As Integer = -1
                        Dim intPort As Integer = -1
                        Dim intPin As Integer = -1
                        ''チャンネル番号がチャンネル設定に存在すること
                        If Not gExistChNo(.shtChId, intChT, intDataT, intFu, intPort, intPin) Then
                            Call mSetErrString("Group Repose set CH NO [" & .shtChId & "] doesn't exist in channel setting. " & _
                                               "[Info]Group=" & i + 1, _
                                               "グループリポーズで設定したチャンネル番号 [" & .shtChId & "] はチャンネルリストに登録されていません。" & _
                                               "[情報]グループ=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        Else
                            'Ver2.0.1.9 ﾃﾞｼﾞﾀﾙCH以外はﾒｯｾｰｼﾞ
                            If intChT <> gCstCodeChTypeDigital Then
                                Call mSetErrString("Group Repose set CH NO [" & .shtChId & "] doesn't DigitalCH. " & _
                                               "[Info]Group=" & i + 1, _
                                               "グループリポーズで設定したチャンネル番号 [" & .shtChId & "] はデジタルチャンネルではありません。" & _
                                               "[情報]グループ=" & i + 1, _
                                               intErrCnt, strErrMsg)
                            End If
                        End If

                    End If

                    For j As Integer = 0 To UBound(.udtReposeInf)

                        With .udtReposeInf(j)

                            ''チャンネルが設定されている場合
                            If .shtChId <> 0 Then

                                ''チャンネル番号がチャンネル設定に存在すること
                                If Not gExistChNo(.shtChId) Then
                                    Call mSetErrString("Group Repose set CH NO [" & .shtChId & "] doesn't exist in channel setting. " & _
                                                       "[Info]Group=" & i + 1 & _
                                                       " , CH SetNo=" & j + 1, _
                                                       "グループリポーズで設定したチャンネル番号 [" & .shtChId & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]グループ=" & i + 1 & _
                                                       " , 設定番号=" & j + 1, _
                                                       intErrCnt, strErrMsg)

                                End If

                            End If

                        End With

                    Next

                End With

                ''チェックマークがついていないかつ47行目の格納時にFor文を抜ける 2018.12.13 倉重
                If g_bytGREPNUM = 0 And i = 47 Then
                    Exit For
                End If

            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Group Repose setting ... Success", " -グループリポーズ設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Group Repose setting ... Failure", " -グループリポーズ設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "運転積算チェック"

    '--------------------------------------------------------------------
    ' 機能      : 運転積算チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 運転積算チェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChRunHour()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            Dim intMaskCnt As Integer = 0
            Dim blMaskCnt As Boolean = False

            For i As Integer = 0 To UBound(gudt.SetChRunHour.udtDetail)

                With gudt.SetChRunHour.udtDetail(i)
                    If .shtChid <> 0 Then
                        blMaskCnt = True
                    End If


                    ''運転積算チャンネルが設定されていてトリガーチャンネルが設定されていない場合
                    If .shtChid <> 0 And _
                       .shtTrgChid = 0 Then
                        Call mSetErrString("Run Hour set Trigger CH is not set. " & _
                                           "[Info]SetNo=" & i + 1, _
                                           "運転積算でトリガーチャンネルが設定されていません。" & _
                                           "[情報]設定番号=" & i + 1, _
                                           intErrCnt, strErrMsg)
                    End If

                    ''運転積算チャンネル番号がチャンネル設定に存在すること
                    If .shtChid <> 0 Then
                        If Not gExistChNo(.shtChid) Then
                            Call mSetErrString("Run Hour set CH NO [" & .shtChid & "] doesn't exist in channel setting. " & _
                                               "[Info]SetNo=" & i + 1, _
                                               "運転積算で設定したチャンネル番号 [" & .shtChid & "] はチャンネルリストに登録されていません。" & _
                                               "[情報]設定番号=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        Else

                            ''運転積算CHかチェック
                            If Not gChkRunHourCH(.shtChid) Then

                                Call mSetErrString("Run Hour set CH NO [" & .shtChid & "] is not Run Hour channel. " & _
                                                   "[Info]SetNo=" & i + 1, _
                                                   "運転積算で設定したチャンネル番号 [" & .shtChid & "] は運転積算のチャンネルではありません。" & _
                                                   "[情報]設定番号=" & i + 1, _
                                                   intErrCnt, strErrMsg)

                            End If
                        End If
                    End If

                    ''トリガーチャンネル番号がチャンネル設定に存在すること
                    If .shtTrgChid <> 0 Then
                        If Not gExistChNo(.shtTrgChid) Then
                            Call mSetErrString("Run Hour set CH NO [" & .shtTrgChid & "] doesn't exist in channel setting. " & _
                                               "[Info]SetNo=" & i + 1, _
                                               "運転積算で設定したチャンネル番号 [" & .shtTrgChid & "] はチャンネルリストに登録されていません。" & _
                                               "[情報]設定番号=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        Else
                            'Ver2.0.3.1
                            'トリガーCHのステータスと設定のｽﾃｰﾀｽが異なる場合ﾒｯｾｰｼﾞ
                            Dim intIndex As Integer = gConvChNoToChArrayId(.shtTrgChid)
                            If intIndex >= 0 Then
                                '設定ｽﾃｰﾀｽと比較
                                Dim intStatus As Integer = .shtStatus
                                Dim blHikaku As Boolean = False
                                'Ver2.0.3.2 DigitalはCHタイプがﾃﾞｼﾞﾀﾙで一致すればOKとする
                                If intStatus > 256 Then
                                    'ﾃﾞｼﾞﾀﾙと比較すればOK
                                    If gudt.SetChInfo.udtChannel(intIndex).udtChCommon.shtChType = gCstCodeChTypeDigital Then
                                        blHikaku = True
                                    End If
                                Else
                                    If gudt.SetChInfo.udtChannel(intIndex).udtChCommon.shtData = intStatus Then
                                        blHikaku = True
                                    End If
                                End If
                                If blHikaku = False Then
                                    Call mSetErrString("Trigger CH NO [" & .shtTrgChid & "] and status are different." & _
                                               "[Info]SetNo=" & i + 1, _
                                               "トリガーチャンネル [" & .shtTrgChid & "] とステータスが異なります。" & _
                                               "[情報]設定番号=" & i + 1, _
                                               intErrCnt, strErrMsg)
                                End If
                            End If

                            'Ver2.0.3.2
                            'RH CHのFUadrとTriggerCHのFUadrが不一致ならﾒｯｾｰｼﾞ。ただしRHCHのWがONならﾁｪｯｸしない
                            Dim intRHindex As Integer = gConvChNoToChArrayId(.shtChid)
                            Dim intTRindex As Integer = gConvChNoToChArrayId(.shtTrgChid)
                            If intRHindex >= 0 And intTRindex >= 0 Then
                                'RHCHのWがONなら無視
                                If Not gBitCheck(gudt.SetChInfo.udtChannel(intRHindex).udtChCommon.shtFlag1, gCstCodeChCommonFlagBitPosWk) Then
                                    'FU比較
                                    Dim intFuNo_RH As Integer = gudt.SetChInfo.udtChannel(intRHindex).udtChCommon.shtFuno
                                    Dim intPort_RH As Integer = gudt.SetChInfo.udtChannel(intRHindex).udtChCommon.shtPortno
                                    Dim intPin_RH As Integer = gudt.SetChInfo.udtChannel(intRHindex).udtChCommon.shtPin
                                    Dim intFuNo_TR As Integer = gudt.SetChInfo.udtChannel(intTRindex).udtChCommon.shtFuno
                                    Dim intPort_TR As Integer = gudt.SetChInfo.udtChannel(intTRindex).udtChCommon.shtPortno
                                    Dim intPin_TR As Integer = gudt.SetChInfo.udtChannel(intTRindex).udtChCommon.shtPin
                                    If _
                                        intFuNo_RH <> intFuNo_TR Or _
                                        intPort_RH <> intPort_TR Or _
                                        intPin_RH <> intPin_TR _
                                    Then
                                        Call mSetErrString("RH CH No [" & .shtChid & "] and Trigger CH NO [" & .shtTrgChid & "]  adress is different." & _
                                               "[FUadr]RH=" & intFuNo_RH & "-" & intPort_RH & "-" & intPin_RH & " Trigger=" & intFuNo_TR & "-" & intPort_TR & "-" & intPin_TR, _
                                               "運転積算チャンネル[" & .shtChid & "]とトリガーチャンネル [" & .shtTrgChid & "] でアドレスが異なります。" & _
                                               "[FUadr]RH=" & intFuNo_RH & "-" & intPort_RH & "-" & intPin_RH & " Trigger=" & intFuNo_TR & "-" & intPort_TR & "-" & intPin_TR, _
                                               intErrCnt, strErrMsg)
                                    End If
                                End If
                            End If

                        End If
                    End If

                    'Ver2.0.3.1
                    'ｽﾃｰﾀｽをﾁｪｯｸしている件数取得
                    If .shtMask <> 0 Then
                        intMaskCnt = intMaskCnt + 1
                    End If

                End With

            Next

            'Ver2.0.3.1
            'ｽﾃｰﾀｽをﾁｪｯｸしている件数がゼロ件ならﾒｯｾｰｼﾞ
            'Ver2.0.3.4 ランアワー設定がゼロ件なら本ﾁｪｯｸは行わない
            If intMaskCnt <= 0 And blMaskCnt = True Then
                Call mSetErrString("The status of running totalization is not checked.", _
                        "運転積算のステータスがチェックされていません。", _
                        intErrCnt, strErrMsg)
            End If


            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Run Hour setting ... Success", " -運転積算設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Run Hour setting ... Failure", " -運転積算設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "排ガスグループチェック"

    '--------------------------------------------------------------------
    ' 機能      : 排ガスグループチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 排ガスグループチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChExhGus()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            For i As Integer = 0 To UBound(gudt.SetChExhGus.udtExhGusRec)

                Dim intCntAve As Integer = 0
                Dim intCntRep As Integer = 0
                Dim intCntCyl As Integer = 0
                Dim intCntDev As Integer = 0

                With gudt.SetChExhGus.udtExhGusRec(i)

                    ''最初に全ての種類のチャンネル設定数を取得
                    If .shtAveChid <> 0 Then intCntAve = 1
                    If .shtRepChid <> 0 Then intCntRep = 1
                    For j As Integer = 0 To UBound(.udtExhGusCyl)
                        If .udtExhGusCyl(j).shtChid <> 0 Then intCntCyl += 1
                    Next
                    For j As Integer = 0 To UBound(.udtExhGusDev)
                        If .udtExhGusDev(j).shtChid <> 0 Then intCntDev += 1
                    Next

                    ''どこか１つでもチャンネルが設定されていたらこれより下のチェックを行う
                    If intCntAve <> 0 Or intCntRep <> 0 Or intCntCyl <> 0 Or intCntDev <> 0 Then

                        '======================
                        ''平均チャンネル
                        '======================
                        ''チャンネルが設定されていない場合
                        If .shtAveChid = 0 Then

                            '▼▼▼ 20110127 平均チャンネルとリポーズCHはチェックなし ▼▼▼▼▼▼▼▼▼▼▼▼
                            'Call mSetErrString("Exh Gas set Ave CH is not set. " & _
                            '                   "[Info]GroupNo=" & i + 1, _
                            '                   "排ガスグループで平均チャンネルが設定されていません。" & _
                            '                   "[情報]グループ=" & i + 1, _
                            '                   intErrCnt, strErrMsg)
                            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                        Else

                            ''チャンネル番号がチャンネル設定に存在すること
                            If Not gExistChNo(.shtAveChid) Then
                                Call mSetErrString("Exh Gas set CH NO [" & .shtAveChid & "] doesn't exist in channel setting. " & _
                                                   "[Info]GroupNo=" & i + 1, _
                                                   "排ガスグループで設定した平均チャンネルの番号 [" & .shtAveChid & "] はチャンネルリストに登録されていません。" & _
                                                   "[情報]グループ=" & i + 1, _
                                                   intErrCnt, strErrMsg)
                            End If

                        End If

                        '======================
                        ''リポーズチャンネル
                        '======================
                        ''チャンネルが設定されていない場合
                        If .shtRepChid = 0 Then

                            '▼▼▼ 20110127 平均チャンネルとリポーズCHはチェックなし ▼▼▼▼▼▼▼▼▼▼▼▼
                            'Call mSetErrString("Exh Gas set Rep CH is not set. " & _
                            '                   "[Info]GroupNo=" & i + 1, _
                            '                   "排ガスグループでリポーズチャンネルが設定されていません。" & _
                            '                   "[情報]グループ=" & i + 1, _
                            '                   intErrCnt, strErrMsg)
                            '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                        Else

                            ''チャンネル番号がチャンネル設定に存在すること
                            If Not gExistChNo(.shtRepChid) Then
                                Call mSetErrString("Exh Gas set CH NO [" & .shtRepChid & "] doesn't exist in channel setting. " & _
                                                   "[Info]GroupNo=" & i + 1, _
                                                   "排ガスグループで設定したリポーズチャンネルの番号 [" & .shtRepChid & "] はチャンネルリストに登録されていません。" & _
                                                   "[情報]グループ=" & i + 1, _
                                                   intErrCnt, strErrMsg)
                            End If

                        End If

                        '======================
                        ''シリンダチャンネル
                        '======================
                        For j As Integer = 0 To UBound(.udtExhGusCyl)

                            ''チャンネルが設定されている場合
                            If .udtExhGusCyl(j).shtChid <> 0 Then

                                ''チャンネル番号がチャンネル設定に存在すること
                                If Not gExistChNo(.udtExhGusCyl(j).shtChid) Then
                                    Call mSetErrString("Exh Gas set Cylinder CH NO [" & .udtExhGusCyl(j).shtChid & "] doesn't exist in channel setting. " & _
                                                       "[Info]GroupNo=" & i + 1, _
                                                       "排ガスグループで設定したシリンダチャンネルの番号 [" & .udtExhGusCyl(j).shtChid & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]グループ=" & i + 1, _
                                                       intErrCnt, strErrMsg)
                                End If

                            End If
                        Next

                        ''チャンネルが１つも設定されていない場合
                        If intCntCyl = 0 Then
                            Call mSetErrString("Exh Gas set Cylinder CH is not set. " & _
                                               "[Info]GroupNo=" & i + 1, _
                                               "排ガスグループでシリンダチャンネルが設定されていません。" & _
                                               "[情報]グループ=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        End If

                        '======================
                        ''Devisionチャンネル
                        '======================
                        For j As Integer = 0 To UBound(.udtExhGusDev)

                            ''Devisionチャンネルが設定されている場合
                            If .udtExhGusDev(j).shtChid <> 0 Then

                                ''Devisionチャンネル番号がチャンネル設定に存在すること
                                If Not gExistChNo(.udtExhGusDev(j).shtChid) Then
                                    Call mSetErrString("Exh Gas set CH NO [" & .udtExhGusDev(j).shtChid & "] doesn't exist in channel setting. " & _
                                                       "[Info]GroupNo=" & i + 1, _
                                                       "排ガスグループで設定した偏差チャンネルの番号 [" & .udtExhGusDev(j).shtChid & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]グループ=" & i + 1, _
                                                       intErrCnt, strErrMsg)
                                End If

                            End If
                        Next

                        '' 偏差CHの設定がない場合もあるためコメント　ver1.4.0 2011.09.02
                        ' ''チャンネルが１つも設定されていない場合
                        'If intCntDev = 0 Then
                        '    Call mSetErrString("Exh Gas set Devision CH is not set. " & _
                        '                       "[Info]GroupNo=" & i + 1, _
                        '                       "排ガスグループで偏差チャンネルが設定されていません。" & _
                        '                       "[情報]グループ=" & i + 1, _
                        '                       intErrCnt, strErrMsg)
                        'End If

                        '▼▼▼ 20110516 作成仕様書のコメント返却で削除になっていたのでコメントアウト ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                        ' ''シリンダチャンネルとDivisionチャンネルの数が一致しない場合
                        'If intCntCyl <> intCntDev Then
                        '    Call mSetErrString("Exh Gas set Cylinder CH is not the same as a set number of Devision. " & _
                        '                       "[Info]GroupNo=" & i + 1, _
                        '                       "排ガスグループでシリンダチャンネルと偏差チャンネルの設定数が一致しません。" & _
                        '                       "[情報]グループ=" & i + 1, _
                        '                       intErrCnt, strErrMsg)
                        'End If
                        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲


                        'Ver2.0.3.1
                        'シリンダ－CHとDEV CHの設定数が一致していない場合エラー。ただしDevCH０件ならﾁｪｯｸしない
                        If intCntDev <> 0 Then
                            If intCntCyl <> intCntDev Then
                                Call mSetErrString("Deviation CH and Cylinder CH do not match in number." & _
                                            "[Info]GroupNo=" & i + 1, _
                                            "偏差チャンネルとシリンダチャンネル数が不一致です。" & _
                                            "[情報]グループ=" & i + 1, _
                                            intErrCnt, strErrMsg)
                            End If
                        End If

                    End If

                End With

            Next


            

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Exh Gas setting ... Success", " -排ガスグループ設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Exh Gas setting ... Failure", " -排ガスグループ設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "コントロール使用可/不可チェック"

    '--------------------------------------------------------------------
    ' 機能      : コントロール使用可/不可チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : コントロール使用可/不可チェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChControlUseNotuse(ByVal udtSetChCtrlUse As gTypSetChCtrlUse, ByVal blnMachinery As Boolean)

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intCnt As Integer = 0

            For i As Integer = 0 To UBound(udtSetChCtrlUse.udtCtrlUseNotuseRec)

                With udtSetChCtrlUse.udtCtrlUseNotuseRec(i)

                    ''条件数が設定されている場合
                    If .shtCount >= 1 Then

                        ''フラグが設定されていない場合
                        If .bytFlg = gCstCodeChCtrlUseCondTypeNothing Then
                            Call mSetErrString("Ctrl Use set Flg is not set. " & _
                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                               " , Group=" & i + 1, _
                                               "コントロール使用可/不可でフラグ（Flg）が設定されていません。" & _
                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                               " , グループ=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        Else

                            ''チャンネル設定数をチェック
                            intCnt = 0
                            For j As Integer = 0 To UBound(.udtUseNotuseDetails)

                                With .udtUseNotuseDetails(j)

                                    ''チャンネルが設定されている場合
                                    If .shtChno <> 0 Then

                                        ''チャンネル番号がチャンネル設定に存在すること
                                        If Not gExistChNo(.shtChno) Then
                                            Call mSetErrString("Ctrl Use set CH NO [" & .shtChno & "] doesn't exist in channel setting. " & _
                                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                               " , Group=" & i + 1 & _
                                                               " , ChSetNo=" & j + 1, _
                                                               "コントロール使用可/不可で設定したチャンネル番号 [" & .shtChno & "] はチャンネルリストに登録されていません。" & _
                                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                               " , グループ=" & i + 1 & _
                                                               " , 設定番号=" & j + 1, _
                                                               intErrCnt, strErrMsg)
                                        End If

                                        ''CH条件タイプが設定されていること
                                        If .bytType = gCstCodeChCtrlUseCondTypeChNothing Then
                                            Call mSetErrString("Ctrl Use set CH flg type is not set. " & _
                                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                               " , Group=" & i + 1 & _
                                                               " , ChSetNo=" & j + 1, _
                                                               "コントロール使用可/不可でフラグタイプ（FlgType）が設定されていません。" & _
                                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                               " , グループ=" & i + 1 & _
                                                               " , 設定番号=" & j + 1, _
                                                               intErrCnt, strErrMsg)
                                        End If

                                        ''カウントアップ
                                        intCnt += 1

                                    End If

                                End With
                            Next

                            ''条件数とCH設定数が一致しない場合
                            If .shtCount <> intCnt Then
                                Call mSetErrString("Ctrl Use set count is not corresponding to CH set count. " & _
                                                   "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                   " , Group=" & i + 1, _
                                                   "コントロール使用可/不可で条件数とチャンネル設定数が一致しません。" & _
                                                   "[情報]グループ=" & mGetPartName(blnMachinery) & _
                                                   " , グループ=" & i + 1, _
                                                   intErrCnt, strErrMsg)
                            End If
                        End If
                    End If

                    'Ver2.0.6.1
                    '条件数、条件ﾌﾗｸﾞが未設定だがCHは設定されている過去ﾃﾞｰﾀ用チェック
                    If .shtCount <= 0 Or .bytFlg = 0 Then
                        intCnt = 0
                        For j As Integer = 0 To UBound(.udtUseNotuseDetails)

                            With .udtUseNotuseDetails(j)

                                ''チャンネルが設定されている場合
                                If .shtChno <> 0 Then

                                    ''チャンネル番号がチャンネル設定に存在すること
                                    If Not gExistChNo(.shtChno) Then
                                        Call mSetErrString("Ctrl Use set CH NO [" & .shtChno & "] doesn't exist in channel setting. " & _
                                                           "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                           " , Group=" & i + 1 & _
                                                           " , ChSetNo=" & j + 1, _
                                                           "コントロール使用可/不可で設定したチャンネル番号 [" & .shtChno & "] はチャンネルリストに登録されていません。" & _
                                                           "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                           " , グループ=" & i + 1 & _
                                                           " , 設定番号=" & j + 1, _
                                                           intErrCnt, strErrMsg)
                                    End If

                                    ''CH条件タイプが設定されていること
                                    If .bytType = gCstCodeChCtrlUseCondTypeChNothing Then
                                        Call mSetErrString("Ctrl Use set CH flg type is not set. " & _
                                                           "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                           " , Group=" & i + 1 & _
                                                           " , ChSetNo=" & j + 1, _
                                                           "コントロール使用可/不可でフラグタイプ（FlgType）が設定されていません。" & _
                                                           "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                           " , グループ=" & i + 1 & _
                                                           " , 設定番号=" & j + 1, _
                                                           intErrCnt, strErrMsg)
                                    End If

                                    ''カウントアップ
                                    intCnt += 1

                                End If

                            End With
                        Next

                        ''条件数とCH設定数が一致しない場合
                        If .shtCount <> intCnt Then
                            Call mSetErrString("Ctrl Use set count is not corresponding to CH set count. " & _
                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                               " , Group=" & i + 1, _
                                               "コントロール使用可/不可で条件数とチャンネル設定数が一致しません。" & _
                                               "[情報]グループ=" & mGetPartName(blnMachinery) & _
                                               " , グループ=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        End If
                    End If
                End With
            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Ctrl Use setting [" & mGetPartName(blnMachinery) & "] ... Success", _
                                 " -コントロール使用可／不可設定確認 [" & mGetPartName(blnMachinery) & "] ... OK")
            Else

                Call mAddMsgText(" -Checking Ctrl Use setting [" & mGetPartName(blnMachinery) & "] ... Failure", _
                                 " -コントロール使用可／不可設定確認 [" & mGetPartName(blnMachinery) & "] ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "SIO設定チェック"

    '--------------------------------------------------------------------
    ' 機能      : SIO設定チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : SIO設定チェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChSIO()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            For i As Integer = 0 To UBound(gudt.SetChSio.udtVdr)

                With gudt.SetChSio.udtVdr(i)

                    '▼▼▼ 20110516 Number of CH はチャンネル数なので存在チェックはしない ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    ' ''チャンネルが設定されている場合
                    'If .shtSendCH <> 0 Then

                    '    ''チャンネル番号がチャンネル設定に存在すること
                    '    If Not gExistChNo(.shtSendCH) Then
                    '        Call mSetErrString("SIO set Send CH NO [" & .shtSendCH & "] doesn't exist in channel setting. " & _
                    '                           "[Info]Port=" & i + 1, _
                    '                           "SIO設定の送信チャンネル番号 [" & .shtSendCH & "] はチャンネルリストに登録されていません。" & _
                    '                           "[情報]ポート番号=" & i + 1, _
                    '                           intErrCnt, strErrMsg)
                    '    End If

                    'End If
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    ''Useにチェックが付いている場合
                    If .shtPort <> 0 Then ''ポート番号が設定されていれば使用（0が未使用）

                        ''タイムアウトの0チェック不要　ver1.4.0 2011.09.02
                        ' ''受信タイムアウト（起動時）が入力されていること
                        'If .shtReceiveInit = 0 Then
                        '    Call mSetErrString("SIO set ReceiveInit is not set. [Info]Port=" & i + 1, _
                        '                       "SIO設定の受信タイムアウト（起動時）が設定されていません。[情報]ポート番号=" & i + 1, intErrCnt, strErrMsg)
                        'End If

                        ' ''受信タイムアウト（起動後）が入力されていること
                        'If .shtReceiveUseally = 0 Then
                        '    Call mSetErrString("SIO set ReceiveUsually is not set. [Info]Port=" & i + 1, _
                        '                       "SIO設定の受信タイムアウト（起動後）が設定されていません。[情報]ポート番号=" & i + 1, intErrCnt, strErrMsg)
                        'End If

                        ' ''送信間隔（起動時）が入力されていること
                        'If .shtSendInit = 0 Then
                        '    Call mSetErrString("SIO set SendInit is not set. [Info]Port=" & i + 1, _
                        '                       "SIO設定の送信間隔（起動時）が設定されていません。[情報]ポート番号=" & i + 1, intErrCnt, strErrMsg)
                        'End If

                        ' ''送信間隔（起動後）が入力されていること
                        'If .shtSendUseally = 0 Then
                        '    Call mSetErrString("SIO set SendUsually is not set. [Info]Port=" & i + 1, _
                        '                       "SIO設定の送信間隔（起動後）が設定されていません。[情報]ポート番号=" & i + 1, intErrCnt, strErrMsg)
                        'End If

                        ''CommType1が設定されていること
                        If .shtCommType1 = gCstCodeChSioCommType1Nothing Then
                            If i < 14 Then 'SIO
                                Call mSetErrString("SIO set CommType1 is not set. " & _
                                                   "[Info]Port=" & i + 1, _
                                                   "SIO設定の通信種類１が設定されていません。" & _
                                                   "[情報]ポート番号=" & i + 1, _
                                                   intErrCnt, strErrMsg)
                            Else '拡張LAN
                                Call mSetErrString("EXT LAN set CommType1 is not set. " & _
                                                  "[Info]Port=" & (i - 14) + 1, _
                                                  "拡張LAN設定の通信種類１が設定されていません。" & _
                                                  "[情報]ポート番号=" & (i - 14) + 1, _
                                                  intErrCnt, strErrMsg)
                            End If

                        End If
                    End If
                End With
            Next

            ''通信CHの存在確認
            For i As Integer = 0 To UBound(gudt.SetChSioCh)
                For j As Integer = 0 To UBound(gudt.SetChSioCh(i).udtSioChRec)

                    With gudt.SetChSioCh(i).udtSioChRec(j)

                        ''チャンネルが設定されている場合
                        If .shtChNo <> 0 Then

                            ''チャンネル番号がチャンネル設定に存在すること
                            If Not gExistChNo(.shtChNo) Then
                                If i < 14 Then
                                    Call mSetErrString("SIO set Transmission CH NO [" & .shtChNo & "] doesn't exist in channel setting. " & _
                                                       "[Info]Port=" & i + 1 & _
                                                       " , SetNo=" & j + 1, _
                                                       "SIO設定の通信チャンネル番号 [" & .shtChNo & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]ポート番号=" & i + 1 & _
                                                       " , 設定番号=" & j + 1, _
                                                       intErrCnt, strErrMsg)
                                Else
                                    Call mSetErrString("EXT LAN set Transmission CH NO [" & .shtChNo & "] doesn't exist in channel setting. " & _
                                                       "[Info]Port=" & (i - 14) + 1 & _
                                                       " , SetNo=" & j + 1, _
                                                       "拡張LAN設定の通信チャンネル番号 [" & .shtChNo & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]ポート番号=" & (i - 14) + 1 & _
                                                       " , 設定番号=" & j + 1, _
                                                       intErrCnt, strErrMsg)
                                End If
                            End If
                        End If
                    End With
                Next
            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking SIO setting ... Success", " -SIO設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking SIO setting ... Failure", " -SIO設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "データ保存テーブルチェック"

    '--------------------------------------------------------------------
    ' 機能      : データ保存テーブルチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : データ保存テーブルチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChDataSaveTable()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            For i As Integer = 0 To UBound(gudt.SetChDataSave.udtDetail)

                With gudt.SetChDataSave.udtDetail(i)

                    ''チャンネルが設定されている場合
                    If .shtChid <> 0 Then

                        ''チャンネル番号がチャンネル設定に存在すること
                        If Not gExistChNo(.shtChid) Then
                            Call mSetErrString("Data Save Table set CH NO [" & .shtChid & "] doesn't exist in channel setting. " & _
                                               "[Info]SetNo=" & i + 1, _
                                               "データ保存テーブルで設定したチャンネル番号 [" & .shtChid & "] はチャンネルリストに登録されていません。" & _
                                               "[情報]設定番号=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        End If

                    End If

                End With
            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Data Save Table setting ... Success", " -データ保存テーブル確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Data Save Table setting ... Failure", " -データ保存テーブル確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "行間空白チェック"

    '--------------------------------------------------------------------
    ' 機能      : 行間空白チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 行間の空白チェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChBlankRow(ByVal blnFillBlk As Boolean)

        Try
            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing


            Dim shtPageIdx As Short
            Dim shtPosIdx As Short
            Dim arrBlank(35)()() As Boolean   'グループ×ページ×CH表示位置 の3段階配列(CH設定の有無をフラグとして格納)
            Dim arrWrongPosCH(35)() As String   'グループ(1~36)×CH表示位置(1~100) の2段階配列(表示位置と一致していないCHNoの値を格納)

            For i As Short = 0 To UBound(arrBlank)                  'Group
                ReDim arrBlank(i)(4)
                ReDim arrWrongPosCH(i)(99)
                For j As Short = 0 To UBound(arrBlank(i))           'Page
                    ReDim arrBlank(i)(j)(19)
                    For k As Short = 0 To UBound(arrBlank(i)(j))    'Position
                        arrBlank(i)(j)(k) = False   '初期化
                        arrWrongPosCH(i)((j * 20) + k) = 0
                    Next
                Next
            Next

            If blnFillBlk = True Then       '行間チェック「する」(空白がある場合、その位置を表示)

                For shtCH As Short = 0 To UBound(gudt.SetChInfo.udtChannel) 'CH設定の有無を格納
                    With gudt.SetChInfo.udtChannel(shtCH).udtChCommon
                        shtPageIdx = 0
                        shtPosIdx = .shtDispPos
                        If .shtGroupNo <> 0 And .shtDispPos <> 0 Then       'CHが存在する　かつ↓
                            If Not gBitCheck(.shtFlag1, 1) = True Then      '隠しCHでない場合
                                If shtPosIdx <= 20 Then                         'CH位置が20以下(=1ページ目)の場合、位置をそのまま格納
                                    shtPageIdx = 0
                                Else                                            'CH位置が21以上(=2ページ目以降)の場合、ページ内の位置を格納したいので
                                    While shtPosIdx > 20                        '20以下になるまで
                                        shtPosIdx -= 20                         '20ずつ引いていき、
                                        shtPageIdx += 1                         '引いた回数をページ数とする
                                    End While
                                End If

                                arrBlank(.shtGroupNo - 1)(shtPageIdx)(shtPosIdx - 1) = True

                            End If
                        End If
                    End With
                Next

                ''行間チェック開始
                For i = 0 To UBound(arrBlank)                                       'Group
                    Dim shtGroupNo As Integer = modStructureConst.gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtGroupNo

                    For j = 0 To UBound(arrBlank(i))                                'Page
                        For k As Short = 0 To UBound(arrBlank(i)(j)) - 1            'Position　ページの末尾の空白は検出しない
                            If arrBlank(i)(j)(k) = False Then                       '空白を検出した場合
                                For l As Short = k + 1 To UBound(arrBlank(i)(j))    '検出した空白より後ろのCHの有無を確認
                                    If arrBlank(i)(j)(l) = True Then                'あった場合、その場所をエラー文として保存
                                        Call mSetErrString("Channel List　Group No：[" & shtGroupNo & "] has blank space.　" & _
                                                           "[Info]Position：" & k + (20 * j) + 1, _
                                                           "計測点リスト　グループ番号：[" & shtGroupNo & "] の行間に空欄があります。　" & _
                                                           "[情報]位置：" & k + (20 * j) + 1, _
                                       intErrCnt, strErrMsg)
                                        Exit For
                                    End If
                                Next
                            End If
                        Next
                    Next
                Next

            Else       '行間チェック「しない」(空白がある場合、その位置を表示)
                'For shtCH As Short = 0 To UBound(gudt.SetChInfo.udtChannel) 'CH設定の有無を格納
                '    With gudt.SetChInfo.udtChannel(shtCH).udtChCommon
                '        If Not gBitCheck(.shtFlag1, 1) = True Then      '隠しCHでない場合
                '            If .shtChno <> 0 Then   'CHが存在する＆CHNoの下2桁と表示位置が一致していない場合、エラー文を出力

                '                Dim strL2DigCHNo As String = Strings.Right(.shtChno.ToString("D4"), 2)      'CHNoの下2桁
                '                Dim strL2DigPos As String = Strings.Right(.shtDispPos.ToString("D4"), 2)    '表示位置の下2桁

                '                If strL2DigCHNo <> strL2DigPos Then
                '                    Call mSetErrString("Channel List　Group No：[" & .shtGroupNo & "] あ＝～～～～～～～.　" & _
                '                                               "[Info]Position：" & .shtDispPos, _
                '                                               "計測点リスト　グループ番号：[" & .shtGroupNo & "] あああああの行間に空欄があります。　" & _
                '                                               "[情報]位置：" & .shtDispPos, _
                '                           intErrCnt, strErrMsg)
                '                End If
                '            End If
                '        End If
                '    End With
                'Next

                For shtCH As Short = 0 To UBound(gudt.SetChInfo.udtChannel) 'CH設定の有無を格納
                    With gudt.SetChInfo.udtChannel(shtCH).udtChCommon
                        If .shtGroupNo <> 0 And .shtDispPos <> 0 Then       'CHが存在する　かつ↓
                            If Not gBitCheck(.shtFlag1, 1) = True Then      '隠しCHでない場合
                                Dim strL2DigCHNo As String = Strings.Right(.shtChno.ToString("D4"), 2)      'CHNoの下2桁
                                Dim strL2DigPos As String = Strings.Right(.shtDispPos.ToString("D4"), 2)    '表示位置の下2桁

                                If strL2DigCHNo <> strL2DigPos Then 'CHが存在する＆CHNoの下2桁と表示位置が一致していない場合、CHNoを格納

                                    arrWrongPosCH(.shtGroupNo - 1)(.shtDispPos - 1) = .shtChno.ToString("D4")

                                End If
                            End If
                        End If
                    End With
                Next
            End If

            For i = 0 To UBound(arrWrongPosCH)                                       'Group
                Dim shtGroupNo As Integer = modStructureConst.gudt.SetChGroupSetM.udtGroup.udtGroupInfo(i).shtGroupNo

                For j As Short = 0 To UBound(arrWrongPosCH(i))      'Position
                    If arrWrongPosCH(i)(j) <> 0 Then                'CHNoが格納されていた場合
                        Call mSetErrString("Channel List　CH No：[" & arrWrongPosCH(i)(j) & "] does not match Display-Position.　" & _
                                           "[Info]Group No：[" & shtGroupNo & "], Position：[" & j + 1 & "]", _
                                           "計測点リスト　CH番号：[" & arrWrongPosCH(i)(j) & "] CHNo.と表示位置の番号が一致していません。　" & _
                                           "[情報]グループ番号：[" & shtGroupNo & "], 位置：[" & j + 1 & "]", _
                       intErrCnt, strErrMsg)
                    End If
                Next
            Next


            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking blank space of CH List ... Success", " -チャンネルリスト行間確認 ... OK")
            Else

                Call mAddMsgText(" -Checking blank space of CH List ... Failure", " -チャンネルリスト行間確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region


 
#End Region

#Region "シーケンス情報チェック"

    '--------------------------------------------------------------------
    ' 機能      : シーケンス情報チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : シーケンス情報のチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkSequenceInfo()

        Try

            ''シーケンス情報チェック開始
            Call mAddMsgText("[Check Sequence Info]", "[シーケンス設定確認]")

            ''シーケンス設定チェック
            Call mChkChSequence()
            If mblnCancelFlg Then Return

            ''演算式・定数設定チェック
            Call mChkChOperationFixed()
            If mblnCancelFlg Then Return

            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "シーケンス設定チェック"

    '--------------------------------------------------------------------
    ' 機能      : シーケンス設定チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : シーケンス設定チェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChSequence()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim udtSequenceLogicSub() As gTypCodeName = Nothing

            For i As Integer = 0 To UBound(gudt.SetSeqSet.udtDetail)

                With gudt.SetSeqSet.udtDetail(i)

                    ''出力チャンネルが設定されている場合は存在チェック
                    If .shtOutChid <> 0 Then
                        If Not gExistChNo(.shtOutChid) Then
                            Call mSetErrString("Sequence set Output CH NO [" & .shtOutChid & "] doesn't exist in channel setting. " & _
                                               "[Info]ID=" & i + 1, _
                                               "シーケンスで設定した出力チャンネル番号 [" & .shtOutChid & "] はチャンネルリストに登録されていません。" & _
                                               "[情報]シーケンスID=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        End If
                    End If

                    ''入力チャンネルが設定されている場合は存在チェック
                    For j As Integer = 0 To UBound(.udtInput)
                        With .udtInput(j)
                            If .shtChid <> 0 Then
                                If ((.shtChSelect <> gCstCodeSeqChSelectCalc) And (.shtChSelect <> gCstCodeSeqChSelectFixed)) And (.shtChSelect <> gCstCodeSeqChSelectManual) Then '' 定数6 追加　ver1.4.4 2012.05.07
                                    If Not gExistChNo(.shtChid) Then
                                        Call mSetErrString("Sequence set Input CH NO [" & .shtChid & "] doesn't exist in channel setting. " & _
                                                           "[Info]ID=" & i + 1 & _
                                                           " , SetNo=" & j + 1, _
                                                           "シーケンスで設定した入力チャンネル番号 [" & .shtChid & "] はチャンネルリストに登録されていません。" & _
                                                           "[情報]シーケンスID=" & i + 1 & _
                                                           " , 設定番号=" & j + 1, _
                                                           intErrCnt, strErrMsg)
                                    End If
                                ElseIf ((.shtChSelect = gCstCodeSeqChSelectManual) And (.bytStatus < 49 Or .bytStatus > 54)) Then '' 定数5 0x31～0x36は無効 Ver1.4.6 2012.07.31 K.Tanigawa
                                    If Not gExistChNo(.shtChid) Then
                                        Call mSetErrString("Sequence set Input CH NO [" & .shtChid & "] doesn't exist in channel setting. " & _
                                                           "[Info]ID=" & i + 1 & _
                                                           " , SetNo=" & j + 1, _
                                                           "シーケンスで設定した入力チャンネル番号 [" & .shtChid & "] はチャンネルリストに登録されていません。" & _
                                                           "[情報]シーケンスID=" & i + 1 & _
                                                           " , 設定番号=" & j + 1, _
                                                           intErrCnt, strErrMsg)
                                    End If
                                End If
                            End If
                        End With
                    Next

                    ''Useチェックが付いている場合
                    If gudt.SetSeqID.shtID(i) <> 0 Then ''ID番号が設定されていれば使用（0が未使用）

                        ''出力CHが入力されている場合
                        If .shtOutChid <> 0 Then

                            ''出力ステータスが選択されていない場合
                            If .bytOutStatus = gCstCodeSeqOutputStatusNothing Then
                                Call mSetErrString("Sequence set CH Output Status is not set. " & _
                                                   "[Info]ID=" & i + 1, _
                                                   "シーケンスで出力ステータスが選択されていません。" & _
                                                   "[情報]シーケンスID=" & i + 1, _
                                                   intErrCnt, strErrMsg)
                            End If

                            ''出力タイプが選択されていない場合
                            If .bytOutDataType = gCstCodeSeqOutputTypeChNothing Then
                                Call mSetErrString("Sequence set CH Output Type is not set. " & _
                                                   "[Info]ID=" & i + 1, _
                                                   "シーケンスで出力タイプが設定されていません。" & _
                                                   "[情報]シーケンスID=" & i + 1, _
                                                   intErrCnt, strErrMsg)
                            End If

                        End If

                        ''FUアドレスが入力されている場合
                        If .bytFuno <> gCstCodeChNotSetFuNoByte And _
                           .bytPort <> gCstCodeChNotSetFuPortByte And _
                           .bytPin <> gCstCodeChNotSetFuPinByte Then

                            ''出力タイプが選択されていない場合
                            If .bytOutType = gCstCodeSeqOutputTypeFuNothing Then
                                Call mSetErrString("Sequence set FU Output Type is not set. " & _
                                                   "[Info]ID=" & i + 1, _
                                                   "シーケンスで出力タイプが設定されていません。" & _
                                                   "[情報]シーケンスID=" & i + 1, _
                                                   intErrCnt, strErrMsg)
                            End If

                        End If

                        ''ロジックが選択されていない場合
                        If .shtLogicType = 0 Then
                            Call mSetErrString("Seqence set Logic is not set. " & _
                                               "[Info]ID=" & i + 1, _
                                               "シーケンスでロジックが設定されていません。" & _
                                               "[情報]シーケンスID=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        Else

                            ''シーケンスロジックサブ設定取得
                            Call gGetComboCodeName(udtSequenceLogicSub, gEnmComboType.ctSeqSetDetailLogic, Format(.shtLogicType, "00"))

                            For j As Integer = 0 To UBound(udtSequenceLogicSub)

                                ''サブ設定のデータ種別によってチェック内容が異なる
                                Select Case udtSequenceLogicSub(j).shtCode
                                    Case 0

                                        '===============
                                        ''設定なし
                                        '===============
                                        ''チェック終了
                                        'Exit For

                                    Case gCstCodeSeqLogicSubDataTypeChNo

                                        '===============
                                        ''CH No
                                        '===============
                                        ''チャンネル存在確認
                                        If Not gExistChNo(.shtLogicItem(j)) Then
                                            Call mSetErrString("Sequence set LogicItem CH NO [" & .shtLogicItem(j) & "] doesn't exist in channel setting. " & _
                                                               "[Info]ID=" & i + 1 & _
                                                               " , Name=" & udtSequenceLogicSub(j).strName, _
                                                               "シーケンスで設定したロジック項目のチャンネル番号 [" & .shtLogicItem(j) & "] はチャンネルリストに登録されていません。" & _
                                                               "[情報]シーケンスID=" & i + 1 & _
                                                               " , ロジック名=" & udtSequenceLogicSub(j).strName, _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    Case gCstCodeSeqLogicSubDataTypeBit

                                        '===============
                                        ''ビット
                                        '===============
                                        ''チェックは特に無し

                                    Case gCstCodeSeqLogicSubDataTypeLinear

                                        '===============
                                        ''リニアテーブルNo
                                        '===============
                                        ''リニアテーブル設定確認
                                        Try
                                            If gudt.SetSeqLinear.udtPoints(.shtLogicItem(j) - 1).shtPoint = 0 Then
                                                Call mSetErrString("Sequence set LogicItem LinearTableNo [" & .shtLogicItem(j) & "] is not set. " & _
                                                                   "[Info]ID=" & i + 1 & _
                                                                   " , Name=" & udtSequenceLogicSub(j).strName, _
                                                                   "シーケンスで設定したロジック項目のリニアテーブル番号 [" & .shtLogicItem(j) & "] にはデータが設定されていません。" & _
                                                                   "[情報]シーケンスID=" & i + 1 & _
                                                                   " , ロジック名=" & udtSequenceLogicSub(j).strName, _
                                                                   intErrCnt, strErrMsg)
                                            End If
                                        Catch ex As Exception
                                            Call mSetErrString("Sequence set LogicItem LinearTableNo [" & .shtLogicItem(j) & "] is Error No!! " & _
                                                               "[Info]ID=" & i + 1 & _
                                                               " , Name=" & udtSequenceLogicSub(j).strName, _
                                                               "シーケンスで設定したロジック項目のリニアテーブル番号 [" & .shtLogicItem(j) & "] は正しくありません。" & _
                                                               "[情報]シーケンスID=" & i + 1 & _
                                                               " , ロジック名=" & udtSequenceLogicSub(j).strName, _
                                                               intErrCnt, strErrMsg)
                                        End Try

                                    Case gCstCodeSeqLogicSubDataTypeExpresion

                                        '===============
                                        ''式テーブルNo
                                        '===============
                                        ''式テーブル設定確認
                                        Try
                                            If gGetString(gudt.SetSeqOpeExp.udtTables(.shtLogicItem(j) - 1).strExp) = "" Then
                                                Call mSetErrString("Sequence set LogicItem ExpresionTableNo [" & .shtLogicItem(j) & "] is not set. " & _
                                                                   "[Info]ID=" & i + 1 & _
                                                                   " , Name=" & udtSequenceLogicSub(j).strName, _
                                                                   "シーケンスで設定したロジック項目の式テーブル番号 [" & .shtLogicItem(j) & "] にはデータが設定されていません。" & _
                                                                   "[情報]シーケンスID=" & i + 1 & _
                                                                   " , ロジック名=" & udtSequenceLogicSub(j).strName, _
                                                                   intErrCnt, strErrMsg)
                                            End If
                                        Catch ex As Exception
                                            Call mSetErrString("Sequence set LogicItem ExpresionTableNo [" & .shtLogicItem(j) & "] is Error No!! " & _
                                                               "[Info]ID=" & i + 1 & _
                                                               " , Name=" & udtSequenceLogicSub(j).strName, _
                                                               "シーケンスで設定したロジック項目の式テーブル番号 [" & .shtLogicItem(j) & "] は正しくありません。" & _
                                                               "[情報]シーケンスID=" & i + 1 & _
                                                               " , ロジック名=" & udtSequenceLogicSub(j).strName, _
                                                               intErrCnt, strErrMsg)
                                        End Try

                                    Case gCstCodeSeqLogicSubDataTypeFixed

                                        '===============
                                        ''定数テーブルNo
                                        '===============
                                        ''チェックは特に無し

                                End Select

                            Next

                        End If

                    End If

                End With

            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Seqence setting ... Success", " -シーケンス設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Seqence setting ... Failure", " -シーケンス設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "演算式・定数設定チェック"

    '--------------------------------------------------------------------
    ' 機能      : 演算式・定数設定チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 演算式・定数設定チェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChOperationFixed()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intChNo As Integer

            For i As Integer = 0 To UBound(gudt.SetSeqOpeExp.udtTables)

                For j As Integer = 0 To UBound(gudt.SetSeqOpeExp.udtTables(i).udtAryInf)

                    With gudt.SetSeqOpeExp.udtTables(i).udtAryInf(j)

                        '定数種類がCHデータの場合
                        If .shtType = gCstCodeSeqFixTypeChData _
                        Or .shtType = gCstCodeSeqFixTypeLowSet _
                        Or .shtType = gCstCodeSeqFixTypeHighSet _
                        Or .shtType = gCstCodeSeqFixTypeLLSet _
                        Or .shtType = gCstCodeSeqFixTypeHHSet Then

                            ''チャンネル番号取得
                            intChNo = BitConverter.ToUInt16(.bytInfo, 2)

                            ''チャンネルが設定されている場合
                            If intChNo <> 0 Then

                                ''チャンネル存在チェック
                                If Not gExistChNo(intChNo) Then
                                    Call mSetErrString("Operation Fixed Number Table set CH NO [" & intChNo & "] doesn't exist in channel setting. " & _
                                                       "[Info]Table=" & i + 1, _
                                                       "演算式・定数テーブルで設定したチャンネル番号 [" & intChNo & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]テーブル番号=" & i + 1, _
                                                       intErrCnt, strErrMsg)
                                End If

                            End If

                        End If

                    End With

                Next

            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Operation Fixed Number Table setting ... Success", " -演算式・定数テーブル設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking Operation Fixed Number Table setting ... Failure", " -演算式・定数テーブル設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region


#Region "OPS グラフ設定チェック"

    '--------------------------------------------------------------------
    ' 機能      : OPS グラフ設定 更新フラグセット
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : グラフ設定 更新フラグセット
    '               Ver1.10.5 2016.05.09 追加
    '--------------------------------------------------------------------
    Private Sub mSetGraphFlg(ByVal blnMachinery As Boolean)

        gblnUpdateAll = True
        If blnMachinery = True Then
            gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphM = 1
            gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphM = 1
        Else
            gudt.SetEditorUpdateInfo.udtSave.bytOpsGraphC = 1
            gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphC = 1
        End If
    End Sub

    '--------------------------------------------------------------------
    ' 機能      : OPS グラフ設定チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 偏差グラフチェックを行う
    '               Ver1.10.5 2016.05.09 追加
    '--------------------------------------------------------------------
    Private Sub mChkOPSGraph(ByRef udtSetOpsGraph As gTypSetOpsGraph, _
                             ByVal blnMachinery As Boolean)

        Try

            ''ｸﾞﾗﾌ番号が未設定ならば追加
            For i = LBound(udtSetOpsGraph.udtGraphTitleRec) To UBound(udtSetOpsGraph.udtGraphTitleRec)
                If udtSetOpsGraph.udtGraphTitleRec(i).bytNo = 0 Then
                    udtSetOpsGraph.udtGraphTitleRec(i).bytNo = i + 1
                    mSetGraphFlg(blnMachinery)  ''更新フラグ設定
                End If
            Next


            ''偏差グラフチェック
            Call mChkGraphExt(udtSetOpsGraph, blnMachinery)
            If mblnCancelFlg Then Return

            ''棒グラフチェック
            Call mChkGraphBar(udtSetOpsGraph, blnMachinery)
            If mblnCancelFlg Then Return

            ''アナログメーターチェック
            Call mChkGraphAnalogMeter(udtSetOpsGraph, blnMachinery)
            If mblnCancelFlg Then Return


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region


#Region "OPS情報チェック"

    '--------------------------------------------------------------------
    ' 機能      : OPS情報チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : OPS情報のチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkOPSInfo()

        Try

            ''OPS情報チェック開始
            Call mAddMsgText("[Check OPS Info]", "[OPS設定確認]")

            '' Ver1.10.5 2016.05.09 ｸﾞﾗﾌﾁｪｯｸを関数に移行
            mChkOPSGraph(gudt.SetOpsGraphM, True)
            mChkOPSGraph(gudt.SetOpsGraphC, False)
            If mblnCancelFlg Then Return

            '' ''偏差グラフチェック
            ''Call mChkGraphExt(gudt.SetOpsGraphM, True)
            ''Call mChkGraphExt(gudt.SetOpsGraphC, False)
            ''If mblnCancelFlg Then Return

            '' ''棒グラフチェック
            ''Call mChkGraphBar(gudt.SetOpsGraphM, True)
            ''Call mChkGraphBar(gudt.SetOpsGraphC, False)
            ''If mblnCancelFlg Then Return

            '' ''アナログメーターチェック
            ''Call mChkGraphAnalogMeter(gudt.SetOpsGraphM, True)
            ''Call mChkGraphAnalogMeter(gudt.SetOpsGraphC, False)
            ''If mblnCancelFlg Then Return
            ''//

            ''フリーグラフチェック    ' 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
            Call mChkGraphFree(gudt.SetOpsFreeGraphM, True)
            Call mChkGraphFree(gudt.SetOpsFreeGraphC, False)
            If mblnCancelFlg Then Return

            ''ログフォーマットチェック
            Call mChkLogFormat(gudt.SetOpsLogFormatM, True)
            Call mChkLogFormat(gudt.SetOpsLogFormatC, False)
            If mblnCancelFlg Then Return

            '' Ver1.11.2  2016.08.01  ﾛｸﾞｵﾌﾟｼｮﾝﾁｪｯｸ
            If gudt.SetSystem.udtSysPrinter.shtLogDrawNo <> 0 Then
                Call mChkLogOption(gudt.SetOpsLogOption, gudt.SetOpsLogFormatM, True)
                If mblnCancelFlg Then Return
            End If
            ''

            ''GWS CH設定チェック
            Call mChkChGws()
            If mblnCancelFlg Then Return

            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "偏差グラフチェック"

    '--------------------------------------------------------------------
    ' 機能      : 偏差グラフチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    '                '' Ver1.10.5 2016.05.09    第一引数 ByVal → ByRef
    ' 機能説明  : 偏差グラフチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkGraphExt(ByRef udtSetOpsGraph As gTypSetOpsGraph, _
                             ByVal blnMachinery As Boolean)

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intCylinderCnt As Integer = 0
            Dim intDevisionCnt As Integer = 0
            Dim intTurboChargerCnt As Integer = 0

            'Ver2.0.3.1
            Dim blCylPer As Boolean = False
            Dim blDevPer As Boolean = False
            Dim strCylRange() As String = Nothing
            Dim strDevRange() As String = Nothing

            For i As Integer = 0 To UBound(udtSetOpsGraph.udtGraphExhaustRec)

                With udtSetOpsGraph.udtGraphExhaustRec(i)

                    '' Ver1.10.5 2016.05.09  ｸﾞﾗﾌ番号がｾｯﾄされていなければ追加
                    If .bytNo = 0 Then
                        .bytNo = i + 1
                        mSetGraphFlg(blnMachinery)  ''更新フラグ設定
                    End If
                    ''//

                    ''平均チャンネルが設定してある場合は存在チェック
                    If .shtAveCh <> 0 Then
                        If Not gExistChNo(.shtAveCh) Then
                            Call mSetErrString("Exhaust Gas Graph set Ave CH NO [" & .shtAveCh & "] doesn't exist in channel setting. " & _
                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                               " , GroupNo=" & i + 1, _
                                               "偏差グラフで設定した平均チャンネル番号 [" & .shtAveCh & "] はチャンネルリストに登録されていません。" & _
                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                               " , グループ番号=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        End If
                    End If

                    intCylinderCnt = 0
                    intDevisionCnt = 0

                    blCylPer = False
                    blDevPer = False
                    For j As Integer = 0 To UBound(.udtCylinder)

                        With .udtCylinder(j)

                            ''シリンダチャンネルが設定してある場合
                            If .shtChCylinder <> 0 Then

                                ''チャンネル存在チェック
                                If Not gExistChNo(.shtChCylinder) Then
                                    Call mSetErrString("Exhaust Gas Graph set Cylinder CH NO [" & .shtChCylinder & "] doesn't exist in channel setting. " & _
                                                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                       " , GroupNo=" & i + 1 & _
                                                       " , SetNo=" & j + 1, _
                                                       "偏差グラフで設定したシリンダチャンネル番号 [" & .shtChCylinder & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                       " , グループ番号=" & i + 1 & _
                                                       " , 設定番号=" & j + 1, _
                                                       intErrCnt, strErrMsg)
                                End If

                                ''タイトル入力チェック
                                If gGetString(.strTitle) = "" Then
                                    Call mSetErrString("Exhaust Gas Graph set Cylinder Deviation CH Title is not set. " & _
                                                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                       " , GroupNo=" & i + 1 & _
                                                       " , SetNo=" & j + 1, _
                                                       "偏差グラフでシリンダ及び偏差チャンネルにタイトルが設定されていません。" & _
                                                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                       " , グループ番号=" & i + 1 & _
                                                       " , 設定番号=" & j + 1, _
                                                       intErrCnt, strErrMsg)
                                End If


                                'Ver2.0.3.1
                                'Unitに「%」が存在すればﾌﾗｸﾞON
                                Dim intCylIndex As Integer = gConvChNoToChArrayId(.shtChCylinder)
                                If intCylIndex >= 0 Then
                                    '単位が「%」
                                    If gudt.SetChInfo.udtChannel(intCylIndex).udtChCommon.shtUnit = &H18 Then
                                        blCylPer = True
                                    End If
                                End If
                                'レンジを格納
                                Dim intRangeHi As Integer = -1
                                Dim intRangeLo As Integer = -1
                                Call gGetChannelRange(.shtChCylinder, intRangeHi, intRangeLo)
                                '取得したレンジを文字列として保存
                                ReDim Preserve strCylRange(intCylinderCnt)
                                strCylRange(intCylinderCnt) = intRangeLo & "," & intRangeHi


                                ''シリンダチャンネル設定数カウントアップ
                                intCylinderCnt += 1
                            End If

                            ''偏差チャンネルが設定してある場合
                            If .shtChDeviation <> 0 Then

                                ''チャンネル存在チェック
                                If Not gExistChNo(.shtChDeviation) Then
                                    Call mSetErrString("Exhaust Gas Graph set Deviation CH NO [" & .shtChCylinder & "] doesn't exist in channel setting. " & _
                                                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                       " , GroupNo=" & i + 1 & _
                                                       " , SetNo=" & j + 1, _
                                                       "偏差グラフで設定した偏差チャンネル番号 [" & .shtChCylinder & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                       " , グループ番号=" & i + 1 & _
                                                       " , 設定番号=" & j + 1, _
                                                       intErrCnt, strErrMsg)
                                End If

                                ''タイトル入力チェック
                                If gGetString(.strTitle) = "" Then
                                    Call mSetErrString("Exhaust Gas Graph set Cylinder Deviation CH Title is not set. " & _
                                                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                       " , GroupNo=" & i + 1 & _
                                                       " , SetNo=" & j + 1, _
                                                       "偏差グラフでシリンダ及び偏差チャンネルにタイトルが設定されていません。" & _
                                                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                       " , グループ番号=" & i + 1 & _
                                                       " , 設定番号=" & j + 1, _
                                                       intErrCnt, strErrMsg)
                                End If


                                'Ver2.0.3.1
                                'Unitに「%」が存在すればﾌﾗｸﾞON
                                Dim intDevIndex As Integer = gConvChNoToChArrayId(.shtChDeviation)
                                If intDevIndex >= 0 Then
                                    '単位が「%」
                                    If gudt.SetChInfo.udtChannel(intDevIndex).udtChCommon.shtUnit = &H18 Then
                                        blDevPer = True
                                    End If
                                End If
                                'レンジを格納
                                Dim intRangeHi As Integer = -1
                                Dim intRangeLo As Integer = -1
                                Call gGetChannelRange(.shtChDeviation, intRangeHi, intRangeLo)
                                '取得したレンジを文字列として保存
                                ReDim Preserve strDevRange(intDevisionCnt)
                                strDevRange(intDevisionCnt) = intRangeLo & "," & intRangeHi


                                ''偏差チャンネル設定数カウントアップ
                                intDevisionCnt += 1

                            End If

                        End With

                    Next


                    'Ver2.0.3.1
                    'シリンダー、偏差それぞれRangeが不統一だったらエラーとする
                    ' ただし単位に%がある場合はﾁｪｯｸしない
                    '>>>シリンダー
                    Dim strNowRange As String = ""
                    If blCylPer <> True And intCylinderCnt > 0 Then
                        strNowRange = strCylRange(0)
                        For x As Integer = 1 To UBound(strCylRange) Step 1
                            If strNowRange <> strCylRange(x) Then
                                Call mSetErrString("Exhaust Gas Graph set CylinderCH Range is inconsistent." & _
                                           "[Info]Part=" & mGetPartName(blnMachinery) & _
                                           " , GroupNo=" & i + 1, _
                                           "偏差グラフで設定したシリンダチャンネルは、Rangeが不統一です。" & _
                                           "[情報]パート=" & mGetPartName(blnMachinery) & _
                                           " , グループ番号=" & i + 1, _
                                           intErrCnt, strErrMsg)
                                Exit For
                            End If
                        Next x
                    End If
                    '>>>偏差
                    strNowRange = ""
                    If blDevPer <> True And intDevisionCnt > 0 Then
                        strNowRange = strDevRange(0)
                        For x As Integer = 1 To UBound(strDevRange) Step 1
                            If strNowRange <> strDevRange(x) Then
                                Call mSetErrString("Exhaust Gas Graph set DevCH Range is inconsistent." & _
                                           "[Info]Part=" & mGetPartName(blnMachinery) & _
                                           " , GroupNo=" & i + 1, _
                                           "偏差グラフで設定した偏差チャンネルは、Rangeが不統一です。" & _
                                           "[情報]パート=" & mGetPartName(blnMachinery) & _
                                           " , グループ番号=" & i + 1, _
                                           intErrCnt, strErrMsg)
                                Exit For
                            End If
                        Next x
                    End If


                    intTurboChargerCnt = 0
                    For j As Integer = 0 To UBound(.udtTurboCharger)

                        With .udtTurboCharger(j)

                            ''ターボチャージャーチャンネルが設定してある場合
                            If .shtChTurboCharger <> 0 Then

                                ''チャンネル存在チェック
                                If Not gExistChNo(.shtChTurboCharger) Then

                                    Call mSetErrString("Exhaust Gas Graph set TurboCharger CH NO [" & .shtChTurboCharger & "] doesn't exist in channel setting. " & _
                                                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                       " , GroupNo=" & i + 1 & _
                                                       " , SetNo=" & j + 1, _
                                                       "偏差グラフで設定したターボチャージャーチャンネル番号 [" & .shtChTurboCharger & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                       " , グループ番号=" & i + 1 & _
                                                       " , 設定番号=" & j + 1, _
                                                       intErrCnt, strErrMsg)


                                End If

                                ''T/Cチャンネル設定数カウントアップ
                                intTurboChargerCnt += 1

                            End If

                        End With

                    Next

                    '▼▼▼ 20110516 設定数は以下であればOK ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    ''シリンダ設定数が Setup Cylinder Count より大きい場合
                    If .bytCyCnt < intCylinderCnt Then
                        Call mSetErrString("Exhaust Gas Graph set CylinderCH of numbers that are more than [Setup Cylinder Count] are set. " & _
                                           "[Info]Part=" & mGetPartName(blnMachinery) & _
                                           " , GroupNo=" & i + 1, _
                                           "偏差グラフで[Setup Cylinder Count]より多い数のシリンダチャンネルが設定されています。" & _
                                           "[情報]パート=" & mGetPartName(blnMachinery) & _
                                           " , グループ番号=" & i + 1, _
                                           intErrCnt, strErrMsg)
                    End If

                    ''偏差設定数が Setup Cylinder Count より大きい場合
                    If .bytCyCnt < intDevisionCnt Then
                        Call mSetErrString("Exhaust Gas Graph set DevisionCH of numbers that are more than [Setup Cylinder Count] are set. " & _
                                           "[Info]Part=" & mGetPartName(blnMachinery) & _
                                           " , GroupNo=" & i + 1, _
                                           "偏差グラフで[Setup Cylinder Count]より多い数の偏差チャンネルが設定されています。" & _
                                           "[情報]パート=" & mGetPartName(blnMachinery) & _
                                           " , グループ番号=" & i + 1, _
                                           intErrCnt, strErrMsg)
                    End If

                    ''ターボチャージャー設定数が Setup T/C Count より大きい場合
                    If .bytTcCnt < intTurboChargerCnt Then
                        Call mSetErrString("Exhaust Gas Graph set T/C CH of numbers that are more than [Setup T/C Count] are set. " & _
                                           "[Info]Part=" & mGetPartName(blnMachinery) & _
                                           " , GroupNo=" & i + 1, _
                                           "偏差グラフで[Setup T/C Count]より多い数のターボチャージャーチャンネルが設定されています。" & _
                                           "[情報]パート=" & mGetPartName(blnMachinery) & _
                                           " , グループ番号=" & i + 1, _
                                           intErrCnt, strErrMsg)
                    End If
                    '------------------------------------------------------------------------------------------------------------------------------------
                    ' ''シリンダ設定数が異なる場合
                    'If .bytCyCnt <> intCylinderCnt Then
                    '    Call mSetErrString("Exhaust Gas Graph set Cylinder CH is not the same as a set number of Cylinder Set Count. " & _
                    '                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                    '                       " , GroupNo=" & i + 1, _
                    '                       "偏差グラフでシリンダチャンネルの設定数が一致しません。" & _
                    '                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                    '                       " , グループ番号=" & i + 1, _
                    '                       intErrCnt, strErrMsg)
                    'End If

                    ' ''シリンダ設定数と偏差設定数が異なる場合
                    'If .bytCyCnt <> intDevisionCnt Then
                    '    Call mSetErrString("Exhaust Gas Graph set Cylinder CH is not the same as a set number of Devision CH. " & _
                    '                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                    '                       " , GroupNo=" & i + 1, _
                    '                       "偏差グラフでシリンダチャンネルと偏差チャンネルの設定数が一致しません。" & _
                    '                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                    '                       " , グループ番号=" & i + 1, _
                    '                       intErrCnt, strErrMsg)
                    'End If

                    ' ''ターボチャージャー設定数が異なる場合
                    'If .bytTcCnt <> intTurboChargerCnt Then
                    '    Call mSetErrString("Exhaust Gas Graph set T/C CH is not the same as a set number of T/C Set Count. " & _
                    '                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                    '                       " , GroupNo=" & i + 1, _
                    '                       "偏差グラフでターボチャージャーチャンネルの設定数が一致しません。" & _
                    '                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                    '                       " , グループ番号=" & i + 1, _
                    '                       intErrCnt, strErrMsg)
                    'End If
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    ''ターボチャージャー設定数が１以上の場合はタイトルが入力されていること
                    If .bytTcCnt >= 1 Then

                        If gGetString(.strTcTitle) = "" Then
                            Call mSetErrString("Exhaust Gas Graph set Turbo Charger Title is not set. " & _
                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                               " , GroupNo=" & i + 1, _
                                               "偏差グラフでターボチャージャーチャンネルのタイトルが設定されていません。" & _
                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                               " , グループ番号=" & i + 1, _
                                               intErrCnt, strErrMsg)
                        End If

                    End If

                End With


            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Exhaust Gas Graph setting [" & mGetPartName(blnMachinery) & "] ... Success", _
                                 " -偏差グラフ設定確認 [" & mGetPartName(blnMachinery) & "] ... OK")
            Else

                Call mAddMsgText(" -Checking Exhaust Gas Graph setting [" & mGetPartName(blnMachinery) & "] ... Failure", _
                                 " -偏差グラフ設定確認 [" & mGetPartName(blnMachinery) & "] ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "棒グラフチェック"

    '--------------------------------------------------------------------
    ' 機能      : 棒グラフチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    '                '' Ver1.10.5 2016.05.09    第一引数 ByVal → ByRef
    ' 機能説明  : 棒グラフチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkGraphBar(ByRef udtSetOpsGraph As gTypSetOpsGraph, _
                             ByVal blnMachinery As Boolean)

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intRangeHi As Integer = 0
            Dim intRangeLo As Integer = 0
            Dim intRangeCnt As Integer = 0
            Dim strRange() As String = Nothing
            Dim strwk() As String = Nothing
            Dim intRangeValue As Integer
            Dim intDivisionCnt As Integer = 0

            Dim blCylPer As Boolean = False

            For i As Integer = 0 To UBound(udtSetOpsGraph.udtGraphBarRec)

                With udtSetOpsGraph.udtGraphBarRec(i)

                    '' Ver1.10.5 2016.05.09  ｸﾞﾗﾌ番号がｾｯﾄされていなければ追加
                    If .bytNo = 0 Then
                        .bytNo = i + 1
                        mSetGraphFlg(blnMachinery)  ''更新フラグ設定
                    End If
                    ''//

                    intRangeCnt = 0
                    For j As Integer = 0 To UBound(.udtCylinder)

                        intRangeHi = 0
                        intRangeLo = 0

                        With .udtCylinder(j)

                            ''シリンダチャンネルが設定してある場合は存在チェック
                            If .shtChCylinder <> 0 Then
                                If Not gExistChNo(.shtChCylinder) Then
                                    Call mSetErrString("Bar Graph set Cylinder CH NO [" & .shtChCylinder & "] doesn't exist in channel setting. " & _
                                                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                       " , GroupNo=" & i + 1, _
                                                       "棒グラフで設定したシリンダチャンネル番号 [" & .shtChCylinder & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                       " , グループ番号=" & i + 1, _
                                                       intErrCnt, strErrMsg)
                                Else

                                    ''レンジ取得
                                    Call gGetChannelRange(.shtChCylinder, intRangeHi, intRangeLo)

                                    ''取得したレンジを文字列として保存
                                    ReDim Preserve strRange(intRangeCnt)
                                    strRange(intRangeCnt) = intRangeLo & "," & intRangeHi


                                    'Ver2.0.3.2
                                    'Unitに「%」が存在すればﾌﾗｸﾞON
                                    Dim intCylIndex As Integer = gConvChNoToChArrayId(.shtChCylinder)
                                    If intCylIndex >= 0 Then
                                        '単位が「%」
                                        If gudt.SetChInfo.udtChannel(intCylIndex).udtChCommon.shtUnit = &H18 Then
                                            blCylPer = True
                                        End If
                                    End If


                                    intRangeCnt += 1

                                End If


                            End If

                        End With

                    Next

                    '▼▼▼ 20110127 レンジは同一でなくでもOK（%表示する） ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    ' ''シリンダ設定数が２以上の場合はレンジが全て同じかチェック
                    'If intRangeCnt >= 2 Then

                    '    For j As Integer = 0 To UBound(strRange) - 1

                    '        If strRange(j) <> strRange(j + 1) Then
                    '            Call mSetErrString("Bar Graph set CH Range is not all the same. " & _
                    '                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                    '                               " , GroupNo=" & i + 1, _
                    '                               "棒グラフで設定したチャンネルにレンジの異なるチャンネルが設定されています。" & _
                    '                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                    '                               " , グループ番号=" & i + 1, _
                    '                               intErrCnt, strErrMsg)
                    '        End If

                    '    Next

                    'End If
                    '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                    ''シリンダ設定数が１以上の場合はレンジと分割数が正しいかチェック
                    If intRangeCnt >= 1 Then

                        ''レンジを取得
                        strwk = Split(strRange(0), ",")

                        ''ver1.4.0 2011.09.02 百分率追加
                        ''レンジ幅を計算
                        If .bytDisplay = 0 Then     '' 計測レンジ
                            intRangeValue = CInt(strwk(1)) - CInt(strwk(0))
                        Else                        '' 百分率
                            intRangeValue = 100
                        End If


                        ''分割数を取得
                        Select Case .bytDevision
                            Case gCstCodeOpsBarGraphDivision4 : intDivisionCnt = 2 '4   ver1.4.0 2011.09.02 偶数値はOKとする
                            Case gCstCodeOpsBarGraphDivision6 : intDivisionCnt = 6
                            Case gCstCodeOpsBarGraphDivision3_5 : intDivisionCnt = 3
                            Case gCstCodeOpsBarGraphDivisionNoting : intDivisionCnt = 0
                        End Select

                        ''分割数０以外の場合
                        If intDivisionCnt <> 0 Then

                            ''レンジ幅が分割数で割り切れない場合
                            If intRangeValue Mod intDivisionCnt <> 0 Then
                                Call mSetErrString("Bar Graph set Neither RangeSpan nor DivisionCount are correct. " & _
                                                   "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                   " , GroupNo=" & i + 1, _
                                                   "棒グラフで設定されたチャンネルのレンジが分割数で割り切れません。" & _
                                                   "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                   " , グループ番号=" & i + 1, _
                                                   intErrCnt, strErrMsg)
                            End If

                        End If


                        'Ver2.0.3.2
                        'シリンダーRangeが不統一だったらエラーとする
                        ' ただし単位に%がある場合はﾁｪｯｸしない
                        '>>>シリンダー
                        Dim strNowRange As String = ""

                        'Ver2.0.4.1 パーセント表示とは、ｸﾞﾗﾌ設定のパーセント表示のこと。(UNITではない)
                        'If blCylPer <> True Then
                        If .bytDisplay <> 1 Then
                            strNowRange = strRange(0)
                            For x As Integer = 1 To UBound(strRange) Step 1
                                If strNowRange <> strRange(x) Then
                                    Call mSetErrString("Bar Graph set CylinderCH Range is inconsistent." & _
                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                               " , GroupNo=" & i + 1, _
                                               "棒グラフで設定したシリンダチャンネルは、Rangeが不統一です。" & _
                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                               " , グループ番号=" & i + 1, _
                                               intErrCnt, strErrMsg)
                                    Exit For
                                End If
                            Next x
                        End If


                    End If

                End With

            Next



            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Bar Graph setting [" & mGetPartName(blnMachinery) & "] ... Success", _
                                 " -棒グラフ設定確認 [" & mGetPartName(blnMachinery) & "] ... OK")
            Else

                Call mAddMsgText(" -Checking Bar Graph setting [" & mGetPartName(blnMachinery) & "] ... Failure", _
                                 " -棒グラフ設定確認 [" & mGetPartName(blnMachinery) & "] ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "アナログメーターチェック"

    '--------------------------------------------------------------------
    ' 機能      : アナログメーターチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    '                '' Ver1.10.5 2016.05.09    第一引数 ByVal → ByRef
    ' 機能説明  : アナログメーターチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkGraphAnalogMeter(ByRef udtSetOpsGraph As gTypSetOpsGraph, _
                                     ByVal blnMachinery As Boolean)

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intRangeHi As Integer = 0
            Dim intRangeLo As Integer = 0
            Dim intRangeCnt As Integer = 0
            Dim strRange() As String = Nothing
            Dim strwk() As String = Nothing
            Dim intRangeValue As Integer
            Dim intDivisionCnt As Integer = 0

            For i As Integer = 0 To UBound(udtSetOpsGraph.udtGraphAnalogMeterRec)

                With udtSetOpsGraph.udtGraphAnalogMeterRec(i)

                    '' Ver1.10.5 2016.05.09  ｸﾞﾗﾌ番号がｾｯﾄされていなければ追加
                    If .bytNo = 0 Then
                        .bytNo = i + 1
                        mSetGraphFlg(blnMachinery)  ''更新フラグ設定
                    End If
                    ''//

                    For j As Integer = 0 To UBound(.udtDetail)

                        intRangeHi = 0
                        intRangeLo = 0

                        With .udtDetail(j)

                            ''チャンネルが設定してある場合は存在チェック
                            If .shtChNo <> 0 Then
                                If Not gExistChNo(.shtChNo) Then
                                    Call mSetErrString("Analog Meter Graph set CH NO [" & .shtChNo & "] doesn't exist in channel setting. " & _
                                                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                       " , GroupNo=" & i + 1, _
                                                       "アナログメーターグラフで設定したチャンネル番号 [" & .shtChNo & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                       " , グループ番号=" & i + 1, _
                                                       intErrCnt, strErrMsg)
                                Else

                                    ''レンジ取得
                                    Call gGetChannelRange(.shtChNo, intRangeHi, intRangeLo)

                                    ''レンジ幅を計算
                                    intRangeValue = intRangeHi - intRangeLo

                                    ''分割数０以外の場合
                                    If .bytScale <> 0 Then

                                        ''レンジ幅が分割数で割り切れない場合
                                        If intRangeValue Mod .bytScale <> 0 Then
                                            Call mSetErrString("Analog Meter Graph set CH NO [" & .shtChNo & "] Neither RangeSpan nor ScaleCount are correct. " & _
                                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                               " , GroupNo=" & i + 1 & " , Range=" & intRangeLo & "-" & intRangeHi, _
                                                               "アナログメーターグラフで設定されたチャンネルのレンジが分割数で割り切れません。" & _
                                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                               " , グループ番号=" & i + 1 & " , Range=" & intRangeLo & "-" & intRangeHi, _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    End If

                                End If
                            End If

                        End With

                    Next

                End With

            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Analog Meter Graph setting [" & mGetPartName(blnMachinery) & "] ... Success", _
                                 " -アナログメーターグラフ設定確認 [" & mGetPartName(blnMachinery) & "] ... OK")
            Else

                Call mAddMsgText(" -Checking Analog Meter Graph setting [" & mGetPartName(blnMachinery) & "] ... Failure", _
                                 " -アナログメーターグラフ設定確認 [" & mGetPartName(blnMachinery) & "] ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "フリーグラフチェック"

    '--------------------------------------------------------------------
    ' 機能      : フリーグラフチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : フリーグラフチェックを行う
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Sub mChkGraphFree(ByVal udtSetOpsFreeGraph As gTypSetOpsFreeGraph, _
                              ByVal blnMachinery As Boolean)

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim intChType As Integer
            Dim intRangeHi As Integer = 0
            Dim intRangeLo As Integer = 0
            Dim intRangeValue As Integer
            Dim intOpsNo As Integer
            Dim intGraphNo As Integer

            For i As Integer = 0 To UBound(udtSetOpsFreeGraph.udtFreeGraphRec)

                For j As Integer = 0 To UBound(udtSetOpsFreeGraph.udtFreeGraphRec(i).udtFreeDetail)

                    With udtSetOpsFreeGraph.udtFreeGraphRec(i).udtFreeDetail(j)

                        intOpsNo = (i \ 16) + 1
                        intGraphNo = (i Mod 16) + 1

                        If .bytTopPos = j + 1 Then

                            ''チャンネルが設定してある場合は存在チェック
                            If .shtChNo <> 0 Then

                                If Not gExistChNo(.shtChNo) Then
                                    Call mSetErrString("Free Graph set CH NO [" & .shtChNo & "] doesn't exist in channel setting. " & _
                                                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                       " , OPS=" & intOpsNo & _
                                                       " , GraphNo=" & intGraphNo & _
                                                       " , SetNo=" & j + 1, _
                                                       "フリーグラフで設定したチャンネル番号 [" & .shtChNo & "] はチャンネルリストに登録されていません。" & _
                                                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                       " , OPS番号=" & intOpsNo & _
                                                       " , グラフ番号=" & intGraphNo & _
                                                       " , 設定番号=" & j + 1, _
                                                       intErrCnt, strErrMsg)
                                End If

                                ''チャンネルタイプ取得
                                intChType = gGetChannelType(.shtChNo)

                                Select Case intChType
                                    Case gCstCodeChTypeDigital

                                        '-------------------
                                        ''デジタルCH
                                        '-------------------
                                        If .bytType = gCstCodeOpsFreeGrapTypeBar _
                                        Or .bytType = gCstCodeOpsFreeGrapTypeAnalog Then
                                            Call mSetErrString("Free Graph set CH NO [" & .shtChNo & "] is ChType and GraphType are unmatched. " & _
                                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                               " , OPS=" & intOpsNo & _
                                                               " , GraphNo=" & intGraphNo & _
                                                               " , SetNo=" & j + 1 & _
                                                               " , ChType=" & gGetNameChannelType(intChType) & _
                                                               " , GraphType=" & gGetNameFreeGraphType(.bytType), _
                                                               "フリーグラフで設定したチャンネル [" & .shtChNo & "] に設定することのできないグラフタイプが設定されています。" & _
                                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                               " , OPS番号=" & intOpsNo & _
                                                               " , グラフ番号=" & intGraphNo & _
                                                               " , 設定番号=" & j + 1 & _
                                                               " , チャンネルタイプ=" & gGetNameChannelType(intChType) & _
                                                               " , グラフタイプ=" & gGetNameFreeGraphType(.bytType), _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    Case gCstCodeChTypeAnalog

                                        '-------------------
                                        ''アナログCH
                                        '-------------------
                                        ''全グラフタイプ設定可能なため、チェックなし

                                    Case gCstCodeChTypeMotor

                                        '-------------------
                                        ''モーターCH
                                        '-------------------
                                        If .bytType = gCstCodeOpsFreeGrapTypeBar _
                                        Or .bytType = gCstCodeOpsFreeGrapTypeAnalog Then
                                            Call mSetErrString("Free Graph set CH NO [" & .shtChNo & "] is ChType and GraphType are unmatched. " & _
                                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                               " , OPS=" & intOpsNo & _
                                                               " , GraphNo=" & intGraphNo & _
                                                               " , SetNo=" & j + 1 & _
                                                               " , ChType=" & gGetNameChannelType(intChType) & _
                                                               " , GraphType=" & gGetNameFreeGraphType(.bytType), _
                                                               "フリーグラフで設定したチャンネル [" & .shtChNo & "] に設定することのできないグラフタイプが設定されています。" & _
                                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                               " , OPS番号=" & intOpsNo & _
                                                               " , グラフ番号=" & intGraphNo & _
                                                               " , 設定番号=" & j + 1 & _
                                                               " , チャンネルタイプ=" & gGetNameChannelType(intChType) & _
                                                               " , グラフタイプ=" & gGetNameFreeGraphType(.bytType), _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    Case gCstCodeChTypeValve

                                        Select Case gGetChNoToDataTypeCode(.shtChNo)
                                            Case gCstCodeChDataTypeValveDI_DO

                                                '-------------------
                                                ''バルブ（DI-DO）CH
                                                '-------------------
                                                If .bytType = gCstCodeOpsFreeGrapTypeBar _
                                                Or .bytType = gCstCodeOpsFreeGrapTypeAnalog Then
                                                    Call mSetErrString("Free Graph set CH NO [" & .shtChNo & "] is ChType and GraphType are unmatched. " & _
                                                                       "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                                       " , OPS=" & intOpsNo & _
                                                                       " , GraphNo=" & intGraphNo & _
                                                                       " , SetNo=" & j + 1 & _
                                                                       " , ChType=" & gGetNameChannelType(intChType) & _
                                                                       " , GraphType=" & gGetNameFreeGraphType(.bytType), _
                                                                       "フリーグラフで設定したチャンネル [" & .shtChNo & "] に設定することのできないグラフタイプが設定されています。" & _
                                                                       "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                                       " , OPS番号=" & intOpsNo & _
                                                                       " , グラフ番号=" & intGraphNo & _
                                                                       " , 設定番号=" & j + 1 & _
                                                                       " , チャンネルタイプ=" & gGetNameChannelType(intChType) & _
                                                                       " , グラフタイプ=" & gGetNameFreeGraphType(.bytType), _
                                                                       intErrCnt, strErrMsg)
                                                End If

                                            Case gCstCodeChDataTypeValveAI_DO1, _
                                                 gCstCodeChDataTypeValveAI_DO2

                                                '-------------------
                                                ''バルブ（AI-DO）CH
                                                '-------------------
                                                ''全グラフタイプ設定可能なため、チェックなし

                                            Case gCstCodeChDataTypeValveAI_AO1, _
                                                 gCstCodeChDataTypeValveAI_AO2

                                                '-------------------
                                                ''バルブ（AI-DO）CH
                                                '-------------------
                                                ''全グラフタイプ設定可能なため、チェックなし

                                            Case gCstCodeChDataTypeValveAO_4_20, _
                                                 gCstCodeChDataTypeValveDO, _
                                                 gCstCodeChDataTypeValveJacom, _
                                                 gCstCodeChDataTypeValveJacom55, _
                                                 gCstCodeChDataTypeValveExt

                                                '-------------------
                                                ''バルブ（その他）CH
                                                '-------------------
                                                ''全てのグラフタイプ設定不可
                                                Call mSetErrString("Free Graph set CH NO [" & .shtChNo & "] is ChType and GraphType are unmatched. " & _
                                                                   "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                                   " , OPS=" & intOpsNo & _
                                                                   " , GraphNo=" & intGraphNo & _
                                                                   " , SetNo=" & j + 1 & _
                                                                   " , ChType=" & gGetNameChannelType(intChType) & _
                                                                   " , GraphType=" & gGetNameFreeGraphType(.bytType), _
                                                                   "フリーグラフで設定したチャンネル [" & .shtChNo & "] に設定することのできないグラフタイプが設定されています。" & _
                                                                   "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                                   " , OPS番号=" & intOpsNo & _
                                                                   " , グラフ番号=" & intGraphNo & _
                                                                   " , 設定番号=" & j + 1 & _
                                                                   " , チャンネルタイプ=" & gGetNameChannelType(intChType) & _
                                                                   " , グラフタイプ=" & gGetNameFreeGraphType(.bytType), _
                                                                   intErrCnt, strErrMsg)

                                        End Select

                                    Case gCstCodeChTypeComposite

                                        '-------------------
                                        ''コンポジットCH
                                        '-------------------
                                        If .bytType = gCstCodeOpsFreeGrapTypeBar _
                                        Or .bytType = gCstCodeOpsFreeGrapTypeAnalog Then
                                            Call mSetErrString("Free Graph set CH NO [" & .shtChNo & "] is ChType and GraphType are unmatched. " & _
                                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                               " , OPS=" & intOpsNo & _
                                                               " , GraphNo=" & intGraphNo & _
                                                               " , SetNo=" & j + 1 & _
                                                               " , ChType=" & gGetNameChannelType(intChType) & _
                                                               " , GraphType=" & gGetNameFreeGraphType(.bytType), _
                                                               "フリーグラフで設定したチャンネル [" & .shtChNo & "] に設定することのできないグラフタイプが設定されています。" & _
                                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                               " , OPS番号=" & intOpsNo & _
                                                               " , グラフ番号=" & intGraphNo & _
                                                               " , 設定番号=" & j + 1 & _
                                                               " , チャンネルタイプ=" & gGetNameChannelType(intChType) & _
                                                               " , グラフタイプ=" & gGetNameFreeGraphType(.bytType), _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    Case gCstCodeChTypePulse

                                        '-------------------
                                        ''パルスCH
                                        '-------------------
                                        ''全グラフタイプ設定可能なため、チェックなし

                                End Select


                                ''バー or アナログメーターの場合、レンジ分割数チェック
                                If .bytType = gCstCodeOpsFreeGrapTypeBar _
                                Or .bytType = gCstCodeOpsFreeGrapTypeAnalog Then

                                    ''レンジ取得
                                    Call gGetChannelRange(.shtChNo, intRangeHi, intRangeLo)

                                    ''レンジ幅を計算
                                    intRangeValue = intRangeHi - intRangeLo

                                    ''分割数０以外の場合
                                    If .bytScale <> 0 Then

                                        ''レンジ幅が分割数で割り切れない場合
                                        If intRangeValue Mod .bytScale <> 0 Then

                                            Call mSetErrString("Free Graph Analog set Neither RangeSpan nor ScaleCount are correct. " & _
                                                               "[Info]Part=" & mGetPartName(blnMachinery) & _
                                                               " , OPS=" & intOpsNo & _
                                                               " , GraphNo=" & intGraphNo & _
                                                               " , SetNo=" & j + 1 & _
                                                               " , CH NO=" & .shtChNo & _
                                                               " , Range Hi=" & intRangeHi & _
                                                               " , Range Lo=" & intRangeLo & _
                                                               " , Scale=" & .bytScale, _
                                                               "フリーグラフのアナログで設定されたチャンネルのレンジが分割数で割り切れません。" & _
                                                               "[情報]パート=" & mGetPartName(blnMachinery) & _
                                                               " , OPS番号=" & intOpsNo & _
                                                               " , グラフ番号=" & intGraphNo & _
                                                               " , 設定番号=" & j + 1 & _
                                                               " , チャンネル番号=" & .shtChNo & _
                                                               " , レンジ上限=" & intRangeHi & _
                                                               " , レンジ下限=" & intRangeLo & _
                                                               " , 分割数=" & .bytScale, _
                                                               intErrCnt, strErrMsg)
                                        End If

                                    End If
                                End If
                            End If
                        End If
                    End With
                Next
            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Free Graph setting [" & mGetPartName(blnMachinery) & "] ... Success", _
                                 " -フリーグラフ設定確認 [" & mGetPartName(blnMachinery) & "] ... OK")
            Else

                Call mAddMsgText(" -Checking Free Graph setting [" & mGetPartName(blnMachinery) & "] ... Failure", _
                                 " -フリーグラフ設定確認 [" & mGetPartName(blnMachinery) & "] ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ログフォーマットチェック"

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマットチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : ログフォーマットチェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkLogFormat(ByVal udtSetOpsLogFormat As gTypSetOpsLogFormat, _
                              ByVal blnMachinery As Boolean)

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim strChNo As String

            Dim LogGRNo As Integer

            Dim nPage As Integer        '' Ver1.12.0.8  2017.02.22  ﾍﾟｰｼﾞ番号ﾁｪｯｸ追加
            Dim bytErrFg As Byte = 0    '' Ver1.12.0.8  2017.02.22  ﾍﾟｰｼﾞ番号ﾁｪｯｸ追加

            nPage = 0       '' Ver1.12.0.8  2017.02.22  ﾍﾟｰｼﾞ番号ﾁｪｯｸ追加

            For i As Integer = 0 To UBound(udtSetOpsLogFormat.strCol1)

                ''CH番号設定の場合
                If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 2) = "CH" Then

                    ''CH番号取得
                    strChNo = Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 3, Len(Trim(udtSetOpsLogFormat.strCol1(i))))

                    ''CH番号がCHリストに存在しない場合
                    If Not gExistChNo(strChNo) Then
                        Call mSetErrString("LogFormat set CH NO [" & strChNo & "] doesn't exist in channel setting. " & _
                                           "[Info]Part=" & mGetPartName(blnMachinery) & _
                                           " , Col=" & 1 & _
                                           " , Row=" & i + 1, _
                                           "ログフォーマットで設定したチャンネル番号 [" & strChNo & "] はチャンネルリストに登録されていません。" & _
                                           "[情報]パート=" & mGetPartName(blnMachinery) & _
                                           " , 列番号=" & 1 & _
                                           " , 行番号=" & i + 1, _
                                           intErrCnt, strErrMsg)
                    Else        '' Ver1.9.2 2015.12.22  RLﾌﾗｸﾞﾁｪｯｸ追加
                        Call ChkLogCHSet(strChNo, intErrCnt, strErrMsg)
                    End If

                Else
                    ''[ CNTTITLE ] 設定の場合
                    If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 8) = "CNTTITLE" Then
                    End If

                    ''[ ANATITLE ] 設定の場合
                    If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 8) = "ANATITLE" Then
                    End If

                    ''[ SPACE ] 設定の場合
                    If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 5) = "SPACE" Then
                    End If
                    ''[ DATA ] 設定の場合
                    '' Ver1.12.0.8  2017.02.22  [ PAGE ] 設定を追加し、MAXﾍﾟｰｼﾞﾁｪｯｸ
                    If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 4) = "DATA" Or Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 4) = "PAGE" Then
                        nPage = nPage + 1

                        If nPage = gCstLogMaxPage Then
                            bytErrFg = 1
                            Call mSetErrString("LogFormat more than 10 pages. ", _
                                           "ログフォーマットでが10ページ以上設定されています。", _
                                           intErrCnt, strErrMsg)
                        End If
                    End If


                    ''[ GR ] 設定の場合
                    If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 2) = "GR" Then

                        ''GROUP番号取得
                        LogGRNo = Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 3, Len(Trim(udtSetOpsLogFormat.strCol1(i))))

                    End If


                End If


            Next

            nPage = 0       '' Ver1.12.0.8  2017.02.22  ﾍﾟｰｼﾞ番号ﾁｪｯｸ追加

            For i As Integer = 0 To UBound(udtSetOpsLogFormat.strCol2)

                ''CH番号設定の場合
                If Mid(Trim(udtSetOpsLogFormat.strCol2(i)), 1, 2) = "CH" Then

                    ''CH番号取得
                    strChNo = Mid(Trim(udtSetOpsLogFormat.strCol2(i)), 3, Len(Trim(udtSetOpsLogFormat.strCol2(i))))

                    ''CH番号がCHリストに存在しない場合
                    If Not gExistChNo(strChNo) Then
                        Call mSetErrString("LogFormat set CH NO [" & strChNo & "] doesn't exist in channel setting. " & _
                                           "[Info]Part=" & mGetPartName(blnMachinery) & _
                                           " , Col=" & 2 & _
                                           " , Row=" & i + 1, _
                                           "ログフォーマットで設定したチャンネル番号 [" & strChNo & "] はチャンネルリストに登録されていません。" & _
                                           "[情報]パート=" & mGetPartName(blnMachinery) & _
                                           " , 列番号=" & 2 & _
                                           " , 行番号=" & i + 1, _
                                           intErrCnt, strErrMsg)
                    Else        '' Ver1.9.2 2015.12.22  RLﾌﾗｸﾞﾁｪｯｸ追加
                        Call ChkLogCHSet(strChNo, intErrCnt, strErrMsg)
                    End If

                Else
                    ''[ CNTTITLE ] 設定の場合
                    If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 8) = "CNTTITLE" Then
                    End If

                    ''[ ANATITLE ] 設定の場合
                    If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 8) = "ANATITLE" Then
                    End If

                    ''[ SPACE ] 設定の場合
                    If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 5) = "SPACE" Then
                    End If
                    ''[ DATA ] 設定の場合
                    '' Ver1.12.0.8  2017.02.22  [ PAGE ] 設定を追加し、MAXﾍﾟｰｼﾞﾁｪｯｸ
                    If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 4) = "DATA" Or Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 4) = "PAGE" Then
                        nPage = nPage + 1

                        If bytErrFg = 0 And nPage = gCstLogMaxPage Then
                            Call mSetErrString("LogFormat more than 10 pages. ", _
                                           "ログフォーマットでが10ページ以上設定されています。", _
                                           intErrCnt, strErrMsg)
                        End If
                    End If

                    ''[ GR ] 設定の場合
                    If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 2) = "GR" Then

                        ''GROUP番号取得
                        LogGRNo = Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 3, Len(Trim(udtSetOpsLogFormat.strCol1(i))))

                    End If

                End If

            Next


            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking LogFormat setting [" & mGetPartName(blnMachinery) & "] ... Success", _
                                 " -ログフォーマット設定確認 [" & mGetPartName(blnMachinery) & "] ... OK")
            Else

                Call mAddMsgText(" -Checking LogFormat setting [" & mGetPartName(blnMachinery) & "] ... Failure", _
                                 " -ログフォーマット設定確認 [" & mGetPartName(blnMachinery) & "] ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマットチェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : ログフォーマットに設定されているCHのRLフラグチェック
    '               '' Ver1.9.2 2015.12.22 追加
    '--------------------------------------------------------------------
    Public Function ChkLogCHSet(ByVal strCH As String, ByRef intErrCnt As Integer, ByRef strErrMsg() As String) As Boolean

        Try
            Dim tempCH As Integer
            Dim bytChkFg As Byte


            'Ver2.0.8.4 デマンドCSV保存がある場合はRLチェックしない
            Dim j As Integer = 0
            Dim x As Integer = 0
            Dim z As Integer = 0
            Dim blCSV As Boolean = False
            With gudt.SetOpsPulldownMenuM
                For j = 0 To UBound(.udtDetail) Step 1
                    For x = 0 To UBound(.udtDetail(j).udtGroup) Step 1
                        For z = 0 To UBound(.udtDetail(j).udtGroup(x).udtSub) Step 1
                            '30-15---があるならHCプリンタが必須
                            If _
                                .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType1 = 30 And _
                                .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType2 = 15 _
                            Then
                                blCSV = True
                                Exit For
                            End If
                        Next z
                    Next x
                Next j
            End With


            tempCH = CInt(strCH)        '' CHNo.取得
            bytChkFg = 0

            ''チャンネルが設定されている場合
            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    If .udtChCommon.shtChno = tempCH Then       '' 探しているCHならば設定を確認

                        bytChkFg = 1

                        ''RLフラグを確認
                        If Not gBitCheck(.udtChCommon.shtFlag2, gCstCodeChCommonFlagBitPosRL) Then
                            If blCSV = False Then
                                Call mSetErrString("RL Flag is not set. " & _
                                                   "[Info]Group=" & .udtChCommon.shtGroupNo & _
                                                   " , CH NO=" & .udtChCommon.shtChno, _
                                                   "レギュラーログフラグが設定されていません。" & _
                                                   "[情報]グループ=" & .udtChCommon.shtGroupNo & _
                                                   " , チャンネル番号=" & .udtChCommon.shtChno, _
                                                   intErrCnt, strErrMsg)
                            End If
                        End If

                        Exit For
                    End If
                End With
            Next

            '' CHが存在しなかった場合
            If bytChkFg = 0 Then
                Call mSetErrString("LogFormat set CH NO [" & strCH & "] doesn't exist in channel setting. ", _
                                           "ログフォーマットで設定したチャンネル番号 [" & strCH & "] はチャンネルリストに登録されていません。", _
                                           intErrCnt, strErrMsg)
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function
#End Region

#Region "ﾛｸﾞｵﾌﾟｼｮﾝﾁｪｯｸ"

    ' Ver1.11.2 2016.08.01 追加
    '--------------------------------------------------------------------
    ' 機能      : ﾛｸﾞｵﾌﾟｼｮﾝﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : udtSetOpsLogOption   ﾛｸﾞｵﾌﾟｼｮﾝ設定
    '             udtSetOpsLogFormat   ﾛｸﾞ設定
    '             blnMachinery         ﾏｼﾅﾘﾌﾗｸﾞ
    ' 機能説明  : ﾛｸﾞｵﾌﾟｼｮﾝ設定ﾁｪｯｸを行う
    '--------------------------------------------------------------------
    Private Sub mChkLogOption(ByVal udtSetOpsLogOption As gTypLogOption, ByVal udtSetOpsLogFormat As gTypSetOpsLogFormat, _
                              ByVal blnMachinery As Boolean)

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing
            Dim strChNo As String


            '' ﾛｸﾞﾌｫｰﾏｯﾄ設定 が ｵﾌﾟｼｮﾝ設定に入っているか確認
            For i As Integer = 0 To UBound(udtSetOpsLogFormat.strCol1)

                ''CH番号設定の場合
                If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 2) = "CH" Then

                    ''CH番号取得
                    strChNo = Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 3, Len(Trim(udtSetOpsLogFormat.strCol1(i))))

                    '' CHを検索してｵﾌﾟｼｮﾝ設定がなければｴﾗｰを表示
                    If mChkLogOptionCH(udtSetOpsLogOption, CInt(strChNo)) = False Then

                        Call mSetErrString("LogFormat set CH NO [" & strChNo & "] doesn't exist in log option setting. ", _
                                               "ログフォーマットで設定したチャンネル番号 [" & strChNo & "] はログオプション設定に登録されていません。", _
                                               intErrCnt, strErrMsg)
                    End If
                End If

            Next

            For i As Integer = 0 To UBound(udtSetOpsLogFormat.strCol2)

                ''CH番号設定の場合
                If Mid(Trim(udtSetOpsLogFormat.strCol2(i)), 1, 2) = "CH" Then

                    ''CH番号取得
                    strChNo = Mid(Trim(udtSetOpsLogFormat.strCol2(i)), 3, Len(Trim(udtSetOpsLogFormat.strCol2(i))))

                    '' CHを検索してｵﾌﾟｼｮﾝ設定がなければｴﾗｰを表示
                    If mChkLogOptionCH(udtSetOpsLogOption, CInt(strChNo)) = False Then

                        Call mSetErrString("LogFormat set CH NO [" & strChNo & "] doesn't exist in log option setting. ", _
                                           "ログフォーマットで設定したチャンネル番号 [" & strChNo & "] はログオプション設定に登録されていません。", _
                                           intErrCnt, strErrMsg)
                    End If

                End If

            Next


            '' ｵﾌﾟｼｮﾝ設定がﾛｸﾞﾌｫｰﾏｯﾄ設定に入っているか確認
            For j As Integer = 0 To UBound(udtSetOpsLogOption.udtLogOption)
                '' CH設定が入っていなければ処理を抜ける
                If udtSetOpsLogOption.udtLogOption(j).shtCHNo = 0 Then
                    Exit For
                End If

                If mChkLogCH(udtSetOpsLogFormat, udtSetOpsLogOption.udtLogOption(j).shtCHNo) = False Then
                    strChNo = udtSetOpsLogOption.udtLogOption(j).shtCHNo.ToString
                    Call mSetErrString("LogOption set CH NO [" & strChNo & "] doesn't exist in log format setting. ", _
                                           "ログオプションで設定したチャンネル番号 [" & strChNo & "] はログフォーマット設定に登録されていません。", _
                                           intErrCnt, strErrMsg)
                End If
            Next

            ''結果表示
            If intErrCnt = 0 Then
                ''    Call mAddMsgText(" -Checking LogOption setting [" & mGetPartName(blnMachinery) & "] ... Success", _
                ''                     " -ログオプション設定確認 [" & mGetPartName(blnMachinery) & "] ... OK")
            Else

                Call mAddMsgText(" -Checking LogOption setting [" & mGetPartName(blnMachinery) & "] ... Failure", _
                                 " -ログオプション設定確認 [" & mGetPartName(blnMachinery) & "] ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


    ' Ver1.11.2 2016.08.01 追加
    '--------------------------------------------------------------------
    ' 機能      : ﾛｸﾞｵﾌﾟｼｮﾝﾁｪｯｸ
    ' 返り値    : True CH設定あり    False CH設定なし 
    ' 引き数    : udtSetOpsLogOption   ﾛｸﾞｵﾌﾟｼｮﾝ設定
    '             nCHNo   検索CHNo.
    ' 機能説明  : ﾛｸﾞｵﾌﾟｼｮﾝ設定ﾁｪｯｸを行う
    '--------------------------------------------------------------------
    Private Function mChkLogOptionCH(ByVal udtSetOpsLogOption As gTypLogOption, ByVal nCHNo As Integer) As Boolean
        Dim bRet As Boolean

        bRet = False

        For j As Integer = 0 To UBound(udtSetOpsLogOption.udtLogOption)
            '' CH設定が入っていなければ処理を抜ける
            If udtSetOpsLogOption.udtLogOption(j).shtCHNo = 0 Then
                Exit For
            End If

            If udtSetOpsLogOption.udtLogOption(j).shtCHNo = nCHNo Then
                bRet = True
                Exit For
            End If
        Next

        Return bRet

    End Function

    ' Ver1.11.2 2016.08.01 追加
    '--------------------------------------------------------------------
    ' 機能      : ﾛｸﾞｵﾌﾟｼｮﾝﾁｪｯｸ
    ' 返り値    : True CH設定あり    False CH設定なし 
    ' 引き数    : udtSetOpsLogOption   ﾛｸﾞｵﾌﾟｼｮﾝ設定
    '             nCHNo   検索CHNo.
    ' 機能説明  : ﾛｸﾞｵﾌﾟｼｮﾝ設定ﾁｪｯｸを行う
    '--------------------------------------------------------------------
    Private Function mChkLogCH(ByVal udtSetOpsLogFormat As gTypSetOpsLogFormat, ByVal nCHNo As Integer) As Boolean
        Dim bRet As Boolean
        Dim strChNo As String

        bRet = False

        For i As Integer = 0 To UBound(udtSetOpsLogFormat.strCol1)

            ''CH番号設定の場合
            If Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 1, 2) = "CH" Then
                ''CH番号取得
                strChNo = Mid(Trim(udtSetOpsLogFormat.strCol1(i)), 3, Len(Trim(udtSetOpsLogFormat.strCol1(i))))

                If CInt(strChNo) = nCHNo Then
                    bRet = True
                    Exit For
                End If

            End If

        Next

        If bRet = False Then
            For i As Integer = 0 To UBound(udtSetOpsLogFormat.strCol2)

                ''CH番号設定の場合
                If Mid(Trim(udtSetOpsLogFormat.strCol2(i)), 1, 2) = "CH" Then
                    ''CH番号取得
                    strChNo = Mid(Trim(udtSetOpsLogFormat.strCol2(i)), 3, Len(Trim(udtSetOpsLogFormat.strCol2(i))))

                    If CInt(strChNo) = nCHNo Then
                        bRet = True
                        Exit For
                    End If

                End If
            Next
        End If

        Return bRet

    End Function

#End Region


#Region "GWS CH設定チェック"

    '--------------------------------------------------------------------
    ' 機能      : GWS CH設定チェック
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : SIO設定チェックを行う
    '--------------------------------------------------------------------
    Private Sub mChkChGws()

        Try

            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing

            ''通信CHの存在確認
            For i As Integer = 0 To UBound(gudt.SetOpsGwsCh.udtGwsFileRec)
                For j As Integer = 0 To UBound(gudt.SetOpsGwsCh.udtGwsFileRec(i).udtGwsChRec)

                    With gudt.SetOpsGwsCh.udtGwsFileRec(i).udtGwsChRec(j)

                        ''チャンネルが設定されている場合
                        If .shtChNo <> 0 Then

                            ''チャンネル番号がチャンネル設定に存在すること
                            If Not gExistChNo(.shtChNo) Then
                                Call mSetErrString("GWS set Transmission CH NO [" & .shtChNo & "] doesn't exist in channel setting. " & _
                                                   "[Info]File=" & i + 1 & _
                                                   " , SetNo=" & j + 1, _
                                                   "GWS設定の通信チャンネル番号 [" & .shtChNo & "] はチャンネルリストに登録されていません。" & _
                                                   "[情報]ファイル番号=" & i + 1 & _
                                                   " , 設定番号=" & j + 1, _
                                                   intErrCnt, strErrMsg)
                            End If

                        End If
                    End With
                Next
            Next

            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking GWS setting ... Success", " -GWS CH設定確認 ... OK")
            Else

                Call mAddMsgText(" -Checking GWS setting ... Failure", " -GWS CH設定確認 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#End Region

#Region "仮設定チェック"

    Private Sub mChkDummyInfo()

        Dim intDummyCnt As Integer = 0
        Dim strDummyMsg() As String = Nothing

        ''仮設定チェック開始
        Call mAddMsgText("[Check Dummy Info]", "[仮設定確認]")

        ''チャンネルが設定されている場合
        For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

            With gudt.SetChInfo.udtChannel(i)

                If .udtChCommon.shtChno <> 0 Then

                    Select Case .udtChCommon.shtChType
                        Case gCstCodeChTypeAnalog

                            '================
                            ''アナログ
                            '================
                            Call mChkDummyCommon(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 0, 0, 0, 0, 1, 0, 1, 1)    ''共通項目チェック
                            Call mChkDummyRange(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 0, 0, 0, 0, 0, 0)     ''レンジチェック   2014.11.25
                            Call mChkDummyAlarmHH(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1)         ''警報設定チェック（上上限）
                            Call mChkDummyAlarmHi(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1)         ''警報設定チェック（上限）
                            Call mChkDummyAlarmLo(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1)         ''警報設定チェック（下限）
                            Call mChkDummyAlarmLL(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1)         ''警報設定チェック（下下限）
                            Call mChkDummyAlarmSF(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1)         ''警報設定チェック（センサーフェイル）

                        Case gCstCodeChTypeDigital

                            '================
                            ''デジタル
                            '================
                            Call mChkDummyCommon(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 0, 0, 1)    ''共通項目チェック

                        Case gCstCodeChTypeMotor

                            '================
                            ''モーター
                            '================
                            Call mChkDummyCommon(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 0, 0, 0)    ''共通項目チェック
                            Call mChkDummyOutput(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1)                   ''出力情報チェック
                            Call mChkDummyOutSta(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 0, 0, 0)    ''出力ステータス情報チェック
                            Call mChkDummyFedAlm(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1, 1)       ''フィードバックアラーム情報チェック


                        Case gCstCodeChTypeValve

                            Select Case .udtChCommon.shtData
                                Case gCstCodeChDataTypeValveDI_DO

                                    '================
                                    ''バルブDiDo
                                    '================
                                    Call mChkDummyCommon(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1, 0, 1)    ''共通項目チェック
                                    Call mChkDummyOutput(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1)                   ''出力情報チェック
                                    Call mChkDummyOutSta(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1, 1, 1)    ''出力ステータス情報チェック
                                    Call mChkDummyFedAlm(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1, 1)       ''フィードバックアラーム情報チェック

                                Case gCstCodeChDataTypeValveAI_DO1, gCstCodeChDataTypeValveAI_DO2

                                    '================
                                    ''バルブAiDo
                                    '================
                                    Call mChkDummyCommon(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 0, 0, 0, 0, 1, 0, 1, 1)    ''共通項目チェック
                                    Call mChkDummyRange(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 0, 0, 0, 0, 0, 0)     ''レンジチェック   2014.11.25
                                    Call mChkDummyOutput(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1)                   ''出力情報チェック
                                    Call mChkDummyOutSta(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1, 1, 1)    ''出力ステータス情報チェック
                                    Call mChkDummyValve1(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 0)          ''バルブ関連情報チェック
                                    Call mChkDummyFedAlm(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1, 1)       ''フィードバックアラーム情報チェック


                                Case gCstCodeChDataTypeValveAI_AO1, gCstCodeChDataTypeValveAI_AO2

                                    '================
                                    ''バルブAiAo
                                    '================
                                    Call mChkDummyCommon(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 0, 0, 0, 0, 1, 0, 1, 1)    ''共通項目チェック
                                    Call mChkDummyRange(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 0, 0, 0, 0, 0, 0)     ''レンジチェック   2014.11.25
                                    Call mChkDummyOutput(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1)                   ''出力情報チェック
                                    Call mChkDummyOutSta(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 0, 0, 0, 0, 0, 0)    ''出力ステータス情報チェック
                                    Call mChkDummyValve1(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1)          ''バルブ関連情報チェック
                                    Call mChkDummyFedAlm(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1, 1)       ''フィードバックアラーム情報チェック


                                Case gCstCodeChDataTypeValveDO

                                    '================
                                    ''バルブDo
                                    '================
                                    Call mChkDummyOutput(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1)                   ''出力情報チェック
                                    Call mChkDummyOutSta(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1, 1, 1)    ''出力ステータス情報チェック
                                    Call mChkDummyFedAlm(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1, 1)       ''フィードバックアラーム情報チェック
                                    Call mChkDummyCmpSt1(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス１チェック
                                    Call mChkDummyCmpSt2(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス２チェック
                                    Call mChkDummyCmpSt3(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス３チェック
                                    Call mChkDummyCmpSt4(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス４チェック
                                    Call mChkDummyCmpSt5(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス５チェック
                                    Call mChkDummyCmpSt6(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス６チェック
                                    Call mChkDummyCmpSt7(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス７チェック
                                    Call mChkDummyCmpSt8(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス８チェック
                                    Call mChkDummyCmpSt9(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス９チェック

                                Case gCstCodeChDataTypeValveAO_4_20

                                    '================
                                    ''バルブAo
                                    '================
                                    Call mChkDummyOutput(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1)                   ''出力情報チェック
                                    Call mChkDummyOutSta(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 0, 0, 0, 0, 0, 0)    ''出力ステータス情報チェック
                                    Call mChkDummyValve1(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1)          ''バルブ関連情報チェック
                                    Call mChkDummyFedAlm(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1, 1)       ''フィードバックアラーム情報チェック


                                Case gCstCodeChDataTypeValveJacom, gCstCodeChDataTypeValveJacom55

                                    '================
                                    ''バルブJacom
                                    '================
                                    Call mChkDummyFedAlm(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1, 1)       ''フィードバックアラーム情報チェック

                                Case gCstCodeChDataTypeValveExt

                                    '================
                                    ''バルブExt
                                    '================
                                    Call mChkDummyOutput(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1)                   ''出力情報チェック
                                    Call mChkDummyFedAlm(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1, 1)       ''フィードバックアラーム情報チェック

                            End Select

                        Case gCstCodeChTypeComposite

                            '================
                            ''コンポジット
                            '================
                            Call mChkDummyCommon(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 1, 0, 1)    ''共通項目チェック
                            Call mChkDummyCmpSt1(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス１チェック
                            Call mChkDummyCmpSt2(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス２チェック
                            Call mChkDummyCmpSt3(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス３チェック
                            Call mChkDummyCmpSt4(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス４チェック
                            Call mChkDummyCmpSt5(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス５チェック
                            Call mChkDummyCmpSt6(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス６チェック
                            Call mChkDummyCmpSt7(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス７チェック
                            Call mChkDummyCmpSt8(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス８チェック
                            Call mChkDummyCmpSt9(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 0, 1, 1, 1, 1)          ''コンポジットステータス９チェック

                        Case gCstCodeChTypePulse

                            '================
                            ''パルス積算
                            '================
                            Call mChkDummyCommon(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 1, 1, 1, 1, 1, 0, 1, 1)    ''共通項目チェック
                            Call mChkDummyAlarmHi(gudt.SetChInfo.udtChannel(i), intDummyCnt, strDummyMsg, 0, 1, 0, 0, 0, 1)         ''警報設定チェック（上限）

                    End Select

                End If

            End With

        Next

        ''結果表示
        Call mAddMsgText(" -Checking Dummy setting ... Success", " -仮設定確認 ... OK")
        Call mAddMsgText("  Dummy Set Count : " & intDummyCnt, "  仮設定件数 : " & intDummyCnt)

        If intDummyCnt <> 0 Then

            ''仮設定内容を追記
            Dim strMsg As String = ""
            For i As Integer = 0 To UBound(strDummyMsg)

                strMsg &= "  -" & strDummyMsg(i) & vbCrLf

                Call mSetDummyString(strDummyMsg(i), strDummyMsg(i))

                If mblnCancelFlg Then Return

            Next

            Call mAddMsgText(strMsg, strMsg)

            ' ''仮設定内容を追記
            'For i As Integer = 0 To UBound(strDummyMsg)
            '    Call mAddMsgText("  -" & strDummyMsg(i), "  -" & strDummyMsg(i))
            '    Call mSetDummyString(strDummyMsg(i), strDummyMsg(i))
            '    If mblnCancelFlg Then Return
            'Next

        End If

        Call mAddMsgText("", "")

    End Sub

#Region "共通項目チェック"

    Private Sub mChkDummyCommon(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer, ByVal intCheckBit7 As Integer, ByVal intCheckBit8 As Integer)

        With udtChannel

            ''共通項目：延長警報グループ
            If intCheckBit1 = 1 Then
                If .DummyCommonExtGroup Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " ExtGroup Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の延長警報グループ（ExtGroup）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''共通項目：ディレイタイマ値
            If intCheckBit2 = 1 Then
                'Ver2.0.0.5 Dummy修正
                'If .DummyCommonExtGroup Then
                If .DummyCommonDelay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のディレイタイマ値（Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''共通項目：グループリポーズ１
            If intCheckBit3 = 1 Then
                'Ver2.0.0.5 Dummy修正
                'If .DummyCommonExtGroup Then
                If .DummyCommonGroupRepose1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " GroupRepose1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のグループリポーズ１（GroupRepose1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''共通項目：グループリポーズ２
            If intCheckBit4 = 1 Then
                'Ver2.0.0.5 Dummy修正
                'If .DummyCommonExtGroup Then
                If .DummyCommonGroupRepose2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " GroupRepose2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のグループリポーズ２（GroupRepose2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''共通項目：入力FUアドレス
            If intCheckBit5 = 1 Then
                'Ver2.0.0.5 Dummy修正
                If .DummyCommonFuAddress Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Input FuAddress Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の入力FUアドレス（Input FuAddress）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''共通項目：計測点個数
            If intCheckBit6 = 1 Then
                'Ver2.0.0.5 Dummy修正
                'If .DummyCommonExtGroup Then
                If .DummyCommonPinNo Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " TerminalCount Dummy.", _
                                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の計測点個数（TerminalCount）は仮設定です。", _
                                           intDummyCnt, strDummyMsg)
                End If
            End If

            ''共通項目：単位種別
            If intCheckBit7 = 1 Then
                'Ver2.0.0.5 Dummy修正
                'If .DummyCommonExtGroup Then
                If .DummyCommonUnitName Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Unit Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の単位種別（Unit）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''共通項目：ステータス種別
            If intCheckBit8 = 1 Then
                'Ver2.0.0.5 Dummy修正
                'If .DummyCommonExtGroup Then
                If .DummyCommonStatusName Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Input Status Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のステータス（Input Status）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region

#Region "レンジチェック"

    Private Sub mChkDummyRange(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer, ByVal intCheckBit7 As Integer, ByVal intCheckBit8 As Integer)

        With udtChannel

            ''レンジ
            If intCheckBit1 = 1 Then
                If .DummyRangeScale Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Range Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のレンジ（Range）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region

#Region "警報設定チェック"

#Region " HiHi "

    Private Sub mChkDummyAlarmHH(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                 ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                 ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyDelayHH Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " HiHi Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上上限のディレイタイマ値（HiHi Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''アラームセット値
            If intCheckBit2 = 1 Then
                If .DummyDelayHH Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " HiHi Value Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上上限のアラームセット値（HiHi Value）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyDelayHH Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " HiHi Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上上限の延長警報グループ（HiHi Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyDelayHH Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " HiHi G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上上限のグループリポーズ１（HiHi G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyDelayHH Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " HiHi G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上上限のグループリポーズ２（HiHi G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyDelayHH Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " HiHi StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上上限のステータス名称（HiHi StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With


    End Sub


#End Region
#Region " Hi "

    Private Sub mChkDummyAlarmHi(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                 ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                 ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyDelayH Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Hi Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上限のディレイタイマ値（Hi Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''アラームセット値
            If intCheckBit2 = 1 Then
                If .DummyValueH Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Hi Value Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上限のアラームセット値（Hi Value）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyExtGrH Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Hi Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上限の延長警報グループ（Hi Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyGRep1H Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Hi G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上限のグループリポーズ１（Hi G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyGRep2H Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Hi G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上限のグループリポーズ２（Hi G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyStaNmH Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Hi StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の上限のステータス名称（Hi StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With


    End Sub


#End Region
#Region " Lo "

    Private Sub mChkDummyAlarmLo(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                 ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                 ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyDelayL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Lo Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下限のディレイタイマ値（Lo Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''アラームセット値
            If intCheckBit2 = 1 Then
                If .DummyDelayL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Lo Value Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下限のアラームセット値（Lo Value）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyDelayL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Lo Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下限の延長警報グループ（Lo Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyDelayL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Lo G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下限のグループリポーズ１（Lo G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyDelayL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Lo G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下限のグループリポーズ２（Lo G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyDelayL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Lo StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下限のステータス名称（Lo StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With


    End Sub


#End Region
#Region " LoLo "

    Private Sub mChkDummyAlarmLL(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                 ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                 ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyDelayLL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " LoLo Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下下限のディレイタイマ値（LoLo Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''アラームセット値
            If intCheckBit2 = 1 Then
                If .DummyDelayLL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " LoLo Value Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下下限のアラームセット値（LoLo Value）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyDelayLL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " LoLo Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下下限の延長警報グループ（LoLo Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyDelayLL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " LoLo G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下下限のグループリポーズ１（LoLo G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyDelayLL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " LoLo G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下下限のグループリポーズ２（LoLo G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyDelayLL Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " LoLo StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の下下限のステータス名称（LoLo StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With


    End Sub


#End Region
#Region " SensorFailure "

    Private Sub mChkDummyAlarmSF(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                 ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                 ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyDelaySF Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " SensorFailure Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のセンサーフェイルのディレイタイマ値（SensorFailure Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''アラームセット値
            If intCheckBit2 = 1 Then
                If .DummyDelaySF Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " SensorFailure Value Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のセンサーフェイルのアラームセット値（SensorFailure Value）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyDelaySF Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " SensorFailure Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のセンサーフェイルの延長警報グループ（SensorFailure Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyDelaySF Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " SensorFailure G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のセンサーフェイルのグループリポーズ１（SensorFailure G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyDelaySF Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " SensorFailure G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のセンサーフェイルのグループリポーズ２（SensorFailure G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyDelaySF Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " SensorFailure StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のセンサーフェイルのステータス名称（SensorFailure StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With


    End Sub


#End Region

#End Region

#Region "出力情報チェック"

    Private Sub mChkDummyOutput(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer)

        With udtChannel

            ''出力FUアドレス
            If intCheckBit1 = 1 Then
                If .DummyOutFuAddress Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out FuAddress Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力FUアドレス（Out FuAddress）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''出力点数
            If intCheckBit2 = 1 Then
                If .DummyOutBitCount Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out TerminalCount Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力点数（Out TerminalCount）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''出力ステータス
            If intCheckBit3 = 1 Then
                If .DummyOutStatusType Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out Status Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力ステータス（Out Status）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With


    End Sub

#End Region

#Region "出力ステータス情報チェック"

    Private Sub mChkDummyOutSta(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer, ByVal intCheckBit7 As Integer, ByVal intCheckBit8 As Integer)

        With udtChannel

            ''ステータス名称１
            If intCheckBit1 = 1 Then
                If .DummyOutStatus1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out StatusName1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力ステータス名称１（Out StatusName1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称２
            If intCheckBit2 = 1 Then
                If .DummyOutStatus2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out StatusName2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力ステータス名称２（Out StatusName2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称３
            If intCheckBit3 = 1 Then
                If .DummyOutStatus3 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out StatusName3 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力ステータス名称３（Out StatusName3）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称４
            If intCheckBit4 = 1 Then
                If .DummyOutStatus4 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out StatusName4 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力ステータス名称４（Out StatusName4）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称５
            If intCheckBit5 = 1 Then
                If .DummyOutStatus5 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out StatusName5 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力ステータス名称５（Out StatusName5）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称６
            If intCheckBit6 = 1 Then
                If .DummyOutStatus6 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out StatusName6 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力ステータス名称６（Out StatusName6）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称７
            If intCheckBit7 = 1 Then
                If .DummyOutStatus7 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out StatusName7 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力ステータス名称７（Out StatusName7）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称８
            If intCheckBit8 = 1 Then
                If .DummyOutStatus8 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Out StatusName8 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の出力ステータス名称８（Out StatusName8）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region

#Region "バルブ関連項チェック"

    Private Sub mChkDummyValve1(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''規定値１
            If intCheckBit1 = 1 Then
                If .DummySp1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " ControlValue1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の規定値１（ControlValue1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''規定値２
            If intCheckBit2 = 1 Then
                If .DummySp2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " ControlValue2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の規定値２ControlValue2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ヒステリシス値（開処理）
            If intCheckBit3 = 1 Then
                If .DummyHysOpen Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Hysteresis(Open) Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のヒステリシス値（開処理）（Hysteresis(Open)）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ヒステリシス値（閉処理）
            If intCheckBit4 = 1 Then
                If .DummyHysClose Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Hysteresis(Close) Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のヒステリシス値（閉処理）（Hysteresis(Close)）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''サンプリング時間
            If intCheckBit5 = 1 Then
                If .DummySmpTime Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " SmplingTime Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のサンプリング時間（SmplingTime）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''変化量
            If intCheckBit6 = 1 Then
                If .DummyVar Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " Variation Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " の変化量（Variation）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region

#Region "フィードバックアラーム情報チェック"

    Private Sub mChkDummyFedAlm(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer, ByVal intCheckBit7 As Integer)

        With udtChannel

            ''FAディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyFaDelay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " FA Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のフィードバックアラームのディレイタイマ値（FA Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ' ''FAアラームセット値
            'If intCheckBit2 = 1 Then
            '    If .Dummy Then
            '        Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " FA Value Dummy.", _
            '                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のフィードバックアラームのアラームセット値（FA Value）は仮設定です。", _
            '                           intDummyCnt, strDummyMsg)
            '    End If
            'End If

            ''FA延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyFaExtGr Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " FA Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のフィードバックアラームの延長警報グループ（FA Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''FAグループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyFaGrep1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " FA G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のフィードバックアラームのグループリポーズ１（FA G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''FAグループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyFaGrep2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " FA G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のフィードバックアラームのグループリポーズ２（FA G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''FAステータス名称
            If intCheckBit6 = 1 Then
                If .DummyFaStaNm Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " FA StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のフィードバックアラームのステータス名称（FA StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''FAアラームタイマ値
            If intCheckBit7 = 1 Then
                If .DummyFaTimeV Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " FA TimerValue Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のフィードバックアラームのアラームタイマ値（FA TimerValue）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region

#Region "コンポジット関連項目チェック"

#Region " Status1 "

    Private Sub mChkDummyCmpSt1(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyCmpStatus1Delay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus1 Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス１のディレイタイマ値（CompositeStatus1 Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ' ''アラームセット値
            'If intCheckBit2 = 1 Then
            '    If .DummyCmpStatus Then
            '        Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus1 Value Dummy.", _
            '                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス１のアラームセット値（CompositeStatus1 Value）は仮設定です。", _
            '                           intDummyCnt, strDummyMsg)
            '    End If
            'End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyCmpStatus1ExtGr Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus1 Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス１の延長警報グループ（CompositeStatus1 Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyCmpStatus1GRep1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus1 G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス１のグループリポーズ１（CompositeStatus1 G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyCmpStatus1GRep2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus1 G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス１のグループリポーズ２（CompositeStatus1 G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyCmpStatus1StaNm Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus1 StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス１のステータス名称（CompositeStatus1 StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region
#Region " Status2 "

    Private Sub mChkDummyCmpSt2(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyCmpStatus2Delay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus2 Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス２のディレイタイマ値（CompositeStatus2 Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ' ''アラームセット値
            'If intCheckBit2 = 1 Then
            '    If .DummyCmpStatus Then
            '        Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus2 Value Dummy.", _
            '                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス２のアラームセット値（CompositeStatus2 Value）は仮設定です。", _
            '                           intDummyCnt, strDummyMsg)
            '    End If
            'End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyCmpStatus2ExtGr Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus2 Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス２の延長警報グループ（CompositeStatus2 Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyCmpStatus2GRep1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus2 G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス２のグループリポーズ１（CompositeStatus2 G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyCmpStatus2GRep2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus2 G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス２のグループリポーズ２（CompositeStatus2 G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyCmpStatus2StaNm Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus2 StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス２のステータス名称（CompositeStatus2 StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region
#Region " Status3 "

    Private Sub mChkDummyCmpSt3(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyCmpStatus3Delay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus3 Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス３のディレイタイマ値（CompositeStatus3 Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ' ''アラームセット値
            'If intCheckBit2 = 1 Then
            '    If .DummyCmpStatus Then
            '        Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus3 Value Dummy.", _
            '                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス３のアラームセット値（CompositeStatus3 Value）は仮設定です。", _
            '                           intDummyCnt, strDummyMsg)
            '    End If
            'End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyCmpStatus3ExtGr Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus3 Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス３の延長警報グループ（CompositeStatus3 Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyCmpStatus3GRep1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus3 G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス３のグループリポーズ１（CompositeStatus3 G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyCmpStatus3GRep2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus3 G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス３のグループリポーズ２（CompositeStatus3 G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyCmpStatus3StaNm Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus3 StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス３のステータス名称（CompositeStatus3 StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region
#Region " Status4 "

    Private Sub mChkDummyCmpSt4(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyCmpStatus4Delay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus4 Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス４のディレイタイマ値（CompositeStatus4 Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ' ''アラームセット値
            'If intCheckBit2 = 1 Then
            '    If .DummyCmpStatus Then
            '        Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus4 Value Dummy.", _
            '                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス４のアラームセット値（CompositeStatus4 Value）は仮設定です。", _
            '                           intDummyCnt, strDummyMsg)
            '    End If
            'End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyCmpStatus4ExtGr Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus4 Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス４の延長警報グループ（CompositeStatus4 Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyCmpStatus4GRep1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus4 G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス４のグループリポーズ１（CompositeStatus4 G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyCmpStatus4GRep2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus4 G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス４のグループリポーズ２（CompositeStatus4 G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyCmpStatus4StaNm Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus4 StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス４のステータス名称（CompositeStatus4 StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region
#Region " Status5 "

    Private Sub mChkDummyCmpSt5(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyCmpStatus5Delay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus5 Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス５のディレイタイマ値（CompositeStatus5 Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ' ''アラームセット値
            'If intCheckBit2 = 1 Then
            '    If .DummyCmpStatus Then
            '        Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus5 Value Dummy.", _
            '                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス５のアラームセット値（CompositeStatus5 Value）は仮設定です。", _
            '                           intDummyCnt, strDummyMsg)
            '    End If
            'End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyCmpStatus5ExtGr Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus5 Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス５の延長警報グループ（CompositeStatus5 Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyCmpStatus5GRep1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus5 G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス５のグループリポーズ１（CompositeStatus5 G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyCmpStatus5GRep2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus5 G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス５のグループリポーズ２（CompositeStatus5 G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyCmpStatus5StaNm Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus5 StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス５のステータス名称（CompositeStatus5 StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region
#Region " Status6 "

    Private Sub mChkDummyCmpSt6(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyCmpStatus6Delay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus6 Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス６のディレイタイマ値（CompositeStatus6 Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ' ''アラームセット値
            'If intCheckBit2 = 1 Then
            '    If .DummyCmpStatus Then
            '        Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus6 Value Dummy.", _
            '                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス６のアラームセット値（CompositeStatus6 Value）は仮設定です。", _
            '                           intDummyCnt, strDummyMsg)
            '    End If
            'End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyCmpStatus6ExtGr Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus6 Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス６の延長警報グループ（CompositeStatus6 Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyCmpStatus6GRep1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus6 G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス６のグループリポーズ１（CompositeStatus6 G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyCmpStatus6GRep2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus6 G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス６のグループリポーズ２（CompositeStatus6 G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyCmpStatus6StaNm Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus6 StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス６のステータス名称（CompositeStatus6 StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region
#Region " Status7 "

    Private Sub mChkDummyCmpSt7(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyCmpStatus7Delay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus7 Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス７のディレイタイマ値（CompositeStatus7 Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ' ''アラームセット値
            'If intCheckBit2 = 1 Then
            '    If .DummyCmpStatus Then
            '        Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus7 Value Dummy.", _
            '                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス７のアラームセット値（CompositeStatus7 Value）は仮設定です。", _
            '                           intDummyCnt, strDummyMsg)
            '    End If
            'End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyCmpStatus7ExtGr Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus7 Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス７の延長警報グループ（CompositeStatus7 Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyCmpStatus7GRep1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus7 G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス７のグループリポーズ１（CompositeStatus7 G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyCmpStatus7GRep2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus7 G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス７のグループリポーズ２（CompositeStatus7 G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyCmpStatus7StaNm Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus7 StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス７のステータス名称（CompositeStatus7 StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region
#Region " Status8 "

    Private Sub mChkDummyCmpSt8(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyCmpStatus8Delay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus8 Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス８のディレイタイマ値（CompositeStatus8 Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ' ''アラームセット値
            'If intCheckBit2 = 1 Then
            '    If .DummyCmpStatus Then
            '        Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus8 Value Dummy.", _
            '                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス８のアラームセット値（CompositeStatus8 Value）は仮設定です。", _
            '                           intDummyCnt, strDummyMsg)
            '    End If
            'End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyCmpStatus8ExtGr Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus8 Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス８の延長警報グループ（CompositeStatus8 Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyCmpStatus8GRep1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus8 G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス８のグループリポーズ１（CompositeStatus8 G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyCmpStatus8GRep2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus8 G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス８のグループリポーズ２（CompositeStatus8 G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyCmpStatus8StaNm Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus8 StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス８のステータス名称（CompositeStatus8 StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region
#Region " Status9 "

    Private Sub mChkDummyCmpSt9(ByVal udtChannel As gTypSetChRec, ByRef intDummyCnt As Integer, ByRef strDummyMsg() As String, _
                                ByVal intCheckBit1 As Integer, ByVal intCheckBit2 As Integer, ByVal intCheckBit3 As Integer, ByVal intCheckBit4 As Integer, _
                                ByVal intCheckBit5 As Integer, ByVal intCheckBit6 As Integer)

        With udtChannel

            ''ディレイタイマ値
            If intCheckBit1 = 1 Then
                If .DummyCmpStatus9Delay Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus9 Delay Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス９のディレイタイマ値（CompositeStatus9 Delay）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ' ''アラームセット値
            'If intCheckBit2 = 1 Then
            '    If .DummyCmpStatus Then
            '        Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus9 Value Dummy.", _
            '                           "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス９のアラームセット値（CompositeStatus9 Value）は仮設定です。", _
            '                           intDummyCnt, strDummyMsg)
            '    End If
            'End If

            ''延長警報グループ
            If intCheckBit3 = 1 Then
                If .DummyCmpStatus9ExtGr Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus9 Ext.G Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス９の延長警報グループ（CompositeStatus9 Ext.G）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ１
            If intCheckBit4 = 1 Then
                If .DummyCmpStatus9GRep1 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus9 G.Rep1 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス９のグループリポーズ１（CompositeStatus9 G.Rep1）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''グループリポーズ２
            If intCheckBit5 = 1 Then
                If .DummyCmpStatus9GRep2 Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus9 G.Rep2 Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス９のグループリポーズ２（CompositeStatus9 G.Rep2）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

            ''ステータス名称
            If intCheckBit6 = 1 Then
                If .DummyCmpStatus9StaNm Then
                    Call mSetErrString("(Check) CH" & .udtChCommon.shtChno.ToString("0000") & " CompositeStatus9 StatusName Dummy.", _
                                       "(確認) CH番号 " & .udtChCommon.shtChno.ToString("0000") & " のコンポジットステータス９のステータス名称（CompositeStatus9 StatusName）は仮設定です。", _
                                       intDummyCnt, strDummyMsg)
                End If
            End If

        End With

    End Sub

#End Region

#End Region

#End Region

#Region "メニューの印刷設定とプリンタの設定の矛盾チェック"
    'Ver2.0.7.E 追加関数
    Private Sub mChkPrinterSetting()
        Dim intErrCnt As Integer = 0
        Dim strErrMsg() As String = Nothing

        Dim j As Integer
        Dim x As Integer
        Dim z As Integer

        Try

            'HCチェック
            'メニューにカラーHC印字があるか？
            Dim blHC As Boolean = False
            With gudt.SetOpsPulldownMenuM
                For j = 0 To UBound(.udtDetail) Step 1
                    For x = 0 To UBound(.udtDetail(j).udtGroup) Step 1
                        For z = 0 To UBound(.udtDetail(j).udtGroup(x).udtSub) Step 1
                            '30-17---4,6があるならHCプリンタが必須
                            If _
                                .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType1 = 30 And _
                                .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType2 = 17 And _
                                (.udtDetail(j).udtGroup(x).udtSub(z).ViewNo1 = data_exchange(4) Or .udtDetail(j).udtGroup(x).udtSub(z).ViewNo1 = data_exchange(6)) _
                            Then
                                blHC = True
                                Exit For
                            End If
                        Next z
                    Next x
                Next j
            End With

            'メニューにHCカラー印字機能が存在する場合のみチェック
            If blHC = True Then
                Dim blLog1 As Boolean = False
                Dim blLog2 As Boolean = False

                If gudt.SetSystem.udtSysPrinter.udtPrinterDetail(0).bytPrinter <> 0 And _
                    gBitCheck(gudt.SetSystem.udtSysPrinter.udtPrinterDetail(0).shtPrintUse, 3) = True Then
                    blLog1 = True
                End If
                If gudt.SetSystem.udtSysPrinter.udtPrinterDetail(1).bytPrinter <> 0 And _
                    gBitCheck(gudt.SetSystem.udtSysPrinter.udtPrinterDetail(1).shtPrintUse, 3) = True Then
                    blLog2 = True
                End If


                'HCプリンタ設定が無い　かつ
                'LOGプリンタにカラーフラグが立ってない
                If gudt.SetSystem.udtSysPrinter.udtPrinterDetail(4).bytPrinter = 0 And _
                    blLog1 = False And blLog2 = False Then
                    Call mSetErrString( _
                                    "There is Color HARD COPY printer in the menu, but COLOR PRINTER is not set.", _
                                    "メニューでカラーHARD COPYプリンタがあるが、カラーPRINTERが設定されていません。", _
                                intErrCnt, strErrMsg)
                End If
            End If



            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Printer Setting ... Success", " -プリンタ設定確認 ... OK")
                Call mAddMsgText("", "")
            Else

                Call mAddMsgText(" -Checking Printer Setting ... Failure", " -プリンタ設定確認 ... エラー")
                ''失敗時はエラー内容を追記
                For i = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                Next
                Call mAddMsgText("", "")
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region

#Region "ログタイムセッティングと、プリンタ設定等の矛盾チェック"
    'Ver2.0.8.2 ログタイムセッティングと、プリンタ設定等の矛盾チェック 追加関数
    Private Sub mChkLogTimePrinterSetting()
        'プリンタを使用する(Log or Alarm)
        ' AND
        '計測点リストRL○が一点でもある
        '上記を満たす時
        'メニューにログタイムセッティングが無いならばメッセージ出力

        Dim intErrCnt As Integer = 0
        Dim strErrMsg() As String = Nothing

        Dim j As Integer
        Dim x As Integer
        Dim z As Integer

        Dim blPrinter As Boolean = False
        Dim blRLlist As Boolean = False
        Dim blLogTime As Boolean = False

        Try

            'プリンタチェック 一種類でもあればON
            '>>>Log Printer=0,1 Alarm Printer=2,3
            For i = 0 To 3 Step 1
                If gudt.SetSystem.udtSysPrinter.udtPrinterDetail(i).bytPrinter <> 0 And _
                                    gBitCheck(gudt.SetSystem.udtSysPrinter.udtPrinterDetail(i).shtPrintUse, 0) = True Then
                    blPrinter = True
                    Exit For
                End If
            Next i

            'RLチェック 計測点にRL○が一点でもあればON
            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)
                With gudt.SetChInfo.udtChannel(i)
                    If .udtChCommon.shtChno <> 0 Then
                        If gBitCheck(.udtChCommon.shtFlag2, 0) = True Then
                            blRLlist = True
                            Exit For
                        End If
                    End If
                End With
            Next i


            '上記２チェックが両方ONの場合のみﾁｪｯｸ
            If blPrinter = True And blRLlist = True Then
                With gudt.SetOpsPulldownMenuM
                    For j = 0 To UBound(.udtDetail) Step 1
                        For x = 0 To UBound(.udtDetail(j).udtGroup) Step 1
                            For z = 0 To UBound(.udtDetail(j).udtGroup(x).udtSub) Step 1
                                'LOG TIME SETTINGがある
                                '0-1-0-0-93
                                If _
                                    .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType1 = 0 And _
                                    .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType2 = 1 And _
                                    .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType3 = 0 And _
                                    .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType4 = 0 And _
                                    (.udtDetail(j).udtGroup(x).udtSub(z).ViewNo1 = data_exchange(93)) _
                                Then
                                    blLogTime = True
                                    Exit For
                                End If
                            Next z
                        Next x
                    Next j
                    'LOG TIME SETTINGが無いならメッセージ
                    If blLogTime = False Then
                        Call mSetErrString( _
                                            "There is no LOG TIME SETTING in the menu", _
                                            "メニューにLOG TIME SETTINGがありません。", _
                            intErrCnt, strErrMsg)
                    End If
                End With
            End If





            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Menu LOG TIME SETTING Setting ... Success", " -LOG TIME SETTING設定確認 ... OK")
                Call mAddMsgText("", "")
            Else

                Call mAddMsgText(" -Checking Menu LOG TIME SETTING Setting ... Failure", " -LOG TIME SETTING設定確認 ... エラー")
                ''失敗時はエラー内容を追記
                For i = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                Next
                Call mAddMsgText("", "")
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region

#Region "デマンドCSV保存があった場合のチェック"
    'Ver2.0.8.4 デマンドCSV保存があった場合のチェック 追加関数
    Private Sub mChkDemandCSVSetting()
        'デマンドCSV保存がある場合(メニュー30-15あり)
        '計測点全点とﾛｸﾞﾌｫｰﾏｯﾄ設定したCHNo全点をチェック
        '相違があればメッセージ出力
        '※ただしSC（隠しCH）はﾁｪｯｸ対象外とする
        Try
            Dim intErrCnt As Integer = 0
            Dim strErrMsg() As String = Nothing


            Dim aryCHLIST As ArrayList    '計測点LogCHno
            Dim aryDataLIST As ArrayList  '計測点CHno

            'デマンドCSV保存があるか否か
            Dim j As Integer = 0
            Dim x As Integer = 0
            Dim z As Integer = 0
            Dim blCSV As Boolean = False
            With gudt.SetOpsPulldownMenuM
                For j = 0 To UBound(.udtDetail) Step 1
                    For x = 0 To UBound(.udtDetail(j).udtGroup) Step 1
                        For z = 0 To UBound(.udtDetail(j).udtGroup(x).udtSub) Step 1
                            '30-15---があるならHCプリンタが必須
                            If _
                                .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType1 = 30 And _
                                .udtDetail(j).udtGroup(x).udtSub(z).SubbytMenuType2 = 15 _
                            Then
                                blCSV = True
                                Exit For
                            End If
                        Next z
                    Next x
                Next j
            End With

            Dim strChNo As String = ""
            If blCSV = True Then
                'ﾛｸﾞﾌｫｰﾏｯﾄのCHNoを格納
                aryCHLIST = New ArrayList
                For j = 0 To UBound(gudt.SetOpsLogFormatM.strCol1) Step 1
                    If Mid(Trim(gudt.SetOpsLogFormatM.strCol1(j)), 1, 2) = "CH" Then
                        strChNo = Mid(Trim(gudt.SetOpsLogFormatM.strCol1(j)), 3, Len(Trim(gudt.SetOpsLogFormatM.strCol1(j))))
                        aryCHLIST.Add(strChNo)
                    End If
                Next j
                For j = 0 To UBound(gudt.SetOpsLogFormatM.strCol2) Step 1
                    If Mid(Trim(gudt.SetOpsLogFormatM.strCol2(j)), 1, 2) = "CH" Then
                        strChNo = Mid(Trim(gudt.SetOpsLogFormatM.strCol2(j)), 3, Len(Trim(gudt.SetOpsLogFormatM.strCol2(j))))
                        aryCHLIST.Add(strChNo)
                    End If
                Next j
                '計測点のCHNoを格納
                aryDataLIST = New ArrayList
                'チャンネル番号を配列化
                For j = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                    With gudt.SetChInfo.udtChannel(j)
                        If .udtChCommon.shtChno <> 0 Then
                            If gBitCheck(.udtChCommon.shtFlag1, 1) <> True Then
                                aryDataLIST.Add(.udtChCommon.shtChno.ToString("0000"))
                            End If
                        End If
                    End With
                Next j

                '計測点のCHNoがﾛｸﾞﾌｫｰﾏｯﾄの計測点に含まれているならばOK
                Dim idx As Integer = 0
                For j = 0 To aryDataLIST.Count - 1 Step 1
                    idx = aryCHLIST.IndexOf(aryDataLIST(j))
                    If idx < 0 Then
                        '該当CHnoが存在しないならメッセージ
                        Call mSetErrString( _
                                            "Nothing LOG FORMAT. CHNo=" & aryDataLIST(j).ToString, _
                                            "ﾛｸﾞﾌｫｰﾏｯﾄに含まれていません。CHNo=" & aryDataLIST(j).ToString, _
                            intErrCnt, strErrMsg)
                    End If
                Next j

            End If





            ''結果表示
            If intErrCnt = 0 Then
                Call mAddMsgText(" -Checking Demand CSV CHNo Setting ... Success", " -デマンドCSV CHNo設定確認 ... OK")
                Call mAddMsgText("", "")
            Else

                Call mAddMsgText(" -Checking Demand CSV CHNo Setting ... Failure", " -デマンドCSV CHNo設定確認 ... エラー")
                ''失敗時はエラー内容を追記
                For i = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                Next
                Call mAddMsgText("", "")
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region

#Region "チャンネルID再設定（旧）"

    ''--------------------------------------------------------------------
    '' 機能      : チャンネルID再設定
    '' 返り値    : なし
    '' 引き数    : ARG1 - (I ) なし
    '' 機能説明  : チャンネル設定構造体のCH IDが連番になるように再設定する
    ''--------------------------------------------------------------------
    'Private Sub mSetChidRenumber()

    '    Try

    '        Dim intLastIndex As Integer
    '        Dim intNullIndex As Integer

    '        Do

    '            ''最終レコード位置を取得
    '            For i As Integer = UBound(gudt.SetChInfo.udtChannel) To 0 Step -1

    '                ''ID、No、Typeが 0 以外の場合（チャンネルが設定されているレコード）
    '                If gudt.SetChInfo.udtChannel(i).udtChCommon.shtChid <> 0 And _
    '                   gudt.SetChInfo.udtChannel(i).udtChCommon.shtChno <> 0 And _
    '                   gudt.SetChInfo.udtChannel(i).udtChCommon.shtChType <> gCstCodeChTypeNothing Then

    '                    intLastIndex = i
    '                    Exit For

    '                End If

    '            Next

    '            ''空きレコード位置を取得
    '            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

    '                ''ID、No、Typeが 0 の場合（チャンネルが設定されていないレコード）
    '                If gudt.SetChInfo.udtChannel(i).udtChCommon.shtChid = 0 And _
    '                   gudt.SetChInfo.udtChannel(i).udtChCommon.shtChno = 0 And _
    '                   gudt.SetChInfo.udtChannel(i).udtChCommon.shtChType = gCstCodeChTypeNothing Then

    '                    intNullIndex = i
    '                    Exit For

    '                End If

    '            Next

    '            ''空きレコード位置と最終レコード位置を比較
    '            If intNullIndex >= intLastIndex Then

    '                ''最終レコード位置より空きレコード位置が大きい場合はループを抜ける
    '                Exit Do

    '            Else

    '                ''空きレコード位置に最終レコードをセットする
    '                gudt.SetChInfo.udtChannel(intNullIndex) = gudt.SetChInfo.udtChannel(intLastIndex)
    '                Call gInitSetChannelDispOne(gudt.SetChInfo.udtChannel(intLastIndex))

    '            End If

    '            If mblnCancelFlg Then Return

    '        Loop

    '        ''IDを振り直し
    '        For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

    '            With gudt.SetChInfo.udtChannel(i).udtChCommon

    '                If .shtChid <> 0 And _
    '                   .shtChno <> 0 And _
    '                   .shtChType <> gCstCodeChTypeNothing Then

    '                    .shtChid = i + 1

    '                Else

    '                    .shtChid = 0

    '                End If

    '            End With

    '            If mblnCancelFlg Then Return

    '        Next

    '        Call mAddMsgText(" -CH ID Renumber ... Success", " -チャンネルID再付番 ... OK")
    '        Call mAddMsgText("", "")

    '    Catch ex As Exception
    '        Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
    '    End Try

    'End Sub

#End Region

#Region "チャンネルID再設定（新）"

    '--------------------------------------------------------------------
    ' 機能      : チャンネルID再設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : チャンネル設定構造体のCH IDが連番になるように再設定する
    '--------------------------------------------------------------------
    Private Sub mSetChidRenumber()

        Dim blnFlg As Boolean = False
        Dim intChid As Integer = 0
        Dim intChidNew As Integer = 0
        Dim intChConvTblPrevLastIndex As Integer
        Dim udtSetChInfo As gTypSetChInfo = Nothing
        Dim udtSetChInfoBackup As gTypSetChInfo = Nothing
        Dim intNextSearchStartIndex As Integer = 0
        Dim intErrCnt As Integer = 0
        Dim strErrMsg() As String = Nothing

        Dim udtSetChConvPrevBack As gTypSetChConv = Nothing

        Try

            Call gInitSetChConv(gudt.SetChConvNow)

            ''現在のCH設定テーブルをコピー
            udtSetChInfo = DeepCopyHelper.DeepCopy(gudt.SetChInfo)

            'Ver2.0.1.8 現在のConvテーブルをコポー（追加エラーが発生した時に元に戻す用）
            udtSetChConvPrevBack = DeepCopyHelper.DeepCopy(gudt.SetChConvPrev)

            'Ver2.0.6.3 CHID initｺﾝﾊﾟｲﾙの場合、Convテーブルを初期化
            If chkCHIDinit.Checked = True Then
                Call gInitSetChConv(gudt.SetChConvPrev)
            End If

            ''現在のCH設定テーブルをコピー（追加エラーが発生した時に元に戻す用）
            udtSetChInfoBackup = DeepCopyHelper.DeepCopy(gudt.SetChInfo)

            '■Excel取込データCHID対応処理 START EXCEL取込の計測点リストを反映する
            'Call subSetExcelChList()
            '■Excel取込データCHID対応処理 END   EXCEL取込の計測点リストを反映する

            ''前VerのCH変換テーブルにデータが設定されているかチェック
            If Not mChkChConvTable(gudt.SetChConvPrev) Then

                '=====================================================
                ''前VerのCH変換テーブルにデータが設定されていない場合
                '=====================================================
                ''現VerのCH変換テーブルに１から順にCHIDを設定
                intChid = 1
                For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                    With gudt.SetChInfo.udtChannel(i).udtChCommon

                        If .shtChno <> 0 And .shtChType <> gCstCodeChTypeNothing Then

                            gudt.SetChConvNow.udtChConv(i).shtChid = intChid
                            .shtChid = intChid
                            intChid += 1

                        Else

                            .shtChid = 0

                        End If

                    End With

                    If mblnCancelFlg Then Return

                Next

                ''現在のCH設定テーブルをコピー
                udtSetChInfo = DeepCopyHelper.DeepCopy(gudt.SetChInfo)

            Else

                '=====================================================
                ''前VerのCH変換テーブルにデータが設定されている場合
                '=====================================================
                ''現在のCH設定テーブルをコピー
                udtSetChInfo = DeepCopyHelper.DeepCopy(gudt.SetChInfo)

                ''現在のCH設定テーブルを初期化
                Call gInitSetChannelDisp(gudt.SetChInfo)

                ''前VerのCH変換テーブルの最終インデックスを取得
                For i As Integer = gCstChannelIdMax To 0 Step -1
                    If gudt.SetChConvPrev.udtChConv(i).shtChid <> 0 Then
                        intChConvTblPrevLastIndex = i
                        Exit For
                    End If
                Next

                ''前VerのCH変換テーブルのループ
                ''（前VerのCH変換テーブルと現VerのCH設定テーブルで同一のIDを追加する処理）
                For i As Integer = 0 To intChConvTblPrevLastIndex

                    With gudt.SetChConvPrev.udtChConv(i)

                        ''CHIDが設定されている場合
                        If .shtChid <> 0 Then

                            ''コピーしたCH設定テーブルに同一IDがあるか検索
                            blnFlg = False
                            For j As Integer = 0 To UBound(udtSetChInfo.udtChannel)

                                ''同一CHIDが見つかった場合
                                If .shtChid = udtSetChInfo.udtChannel(j).udtChCommon.shtChid Then

                                    If udtSetChInfo.udtChannel(j).udtChCommon.shtChno = 2528 Then
                                        Dim a As Integer = 0
                                    End If


                                    ''前VerのCH変換テーブルの配列位置と同じ位置のCH設定テーブルにCH情報をセットする
                                    gudt.SetChInfo.udtChannel(i) = udtSetChInfo.udtChannel(j)

                                    ''現VerのCH変換テーブルにCHIDをセットする
                                    gudt.SetChConvNow.udtChConv(i).shtChid = udtSetChInfo.udtChannel(j).udtChCommon.shtChid

                                    blnFlg = True
                                    Exit For

                                End If

                            Next

                            ''CH情報がセットされなかった場合
                            If Not blnFlg Then

                                ''現VerのCH変換テーブルに 0 をセットする
                                gudt.SetChConvNow.udtChConv(i).shtChid = 0
                                'Ver2.0.1.8 前Verの変換ﾃｰﾌﾞﾙにも０をｾｯﾄする
                                gudt.SetChConvPrev.udtChConv(i).shtChid = 0
                            End If

                        End If
                    End With
                Next
            End If

            ''現VerのCH設定テーブルのループ
            ''現VerのCH変換テーブルでCHID = 0 かつ CHNO <> 0 のチャンネル（追加したチャンネル）にIDを振り、CH変換テーブルに登録する処理
            intNextSearchStartIndex = 0
            For i As Integer = 0 To UBound(udtSetChInfo.udtChannel)

                With udtSetChInfo.udtChannel(i).udtChCommon

                    If udtSetChInfo.udtChannel(i).udtChCommon.shtChno = 2528 Then
                        Dim a As Integer = 0
                    End If

                    ''CHNO <> 0 かつ CHID = 0 の場合（追加したチャンネル）
                    If .shtChno <> 0 And .shtChid = 0 _
                    Or Not mChkChIdExist(.shtChid, gudt.SetChConvPrev, intChConvTblPrevLastIndex) Then

                        ''前VerのCH変換テーブルを検索し、CHID = 0 の位置にチャンネル情報をセット
                        blnFlg = False
                        For j As Integer = intNextSearchStartIndex To gCstChannelIdMax - 1

                            If gudt.SetChConvPrev.udtChConv(j).shtChid = 0 Then

                                ''新規CHID取得
                                intChidNew += 1
                                Call mGetNewChid(gudt.SetChConvPrev, intChConvTblPrevLastIndex, intChidNew)

                                ''現VerのCH変換テーブルに新規IDを振る
                                gudt.SetChConvNow.udtChConv(j).shtChid = intChidNew


                                ''現VerのCH変換テーブルの配列位置と同じ位置のCH設定テーブルにCH情報をセットする
                                gudt.SetChInfo.udtChannel(j) = udtSetChInfo.udtChannel(i)
                                gudt.SetChInfo.udtChannel(j).udtChCommon.shtChid = intChidNew

                                intNextSearchStartIndex = j + 1

                                blnFlg = True

                                Exit For

                            End If

                        Next

                        If Not blnFlg Then

                            '=======================================================================================
                            ''追加したチャンネルを保存する場所がない場合
                            '=======================================================================================
                            ''例１）3000CH設定されている状態で3001CH目を登録した場合
                            ''例２）前Verで3000CH設定をコンパイルして、現Verでチャンネルを削除して追加した場合
                            ''↑チャンネルを削除した場合は、対象位置を欠番(0)で保持する必要があるため、追加できない
                            Call mSetErrString("It is not possible to save it, because the maximum number of channels is exceeded. [Info]CH NO=" & .shtChno, _
                                               "チャンネル最大設定数を超えるため、チャンネルを保存できません。[情報]チャンネル番号=" & .shtChno, intErrCnt, strErrMsg)

                            ''CH設定を元に戻す
                            gudt.SetChInfo = DeepCopyHelper.DeepCopy(udtSetChInfoBackup)

                            'Ver2.0.1.8 Convももとに戻す
                            gudt.SetChConvPrev = DeepCopyHelper.DeepCopy(udtSetChConvPrevBack)

                        End If

                    End If

                End With

            Next

            ''変更前と変更後の構造体を比較し、変更前にあって変更後にないCH（CHID重複等による追加漏れ）を追加する処理
            For i As Integer = 0 To UBound(udtSetChInfo.udtChannel)

                blnFlg = False
                For j As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                    ''変更前の構造体にあるCH番号が、変更後の構造体に見つかった場合はフラグを立てる
                    If udtSetChInfo.udtChannel(i).udtChCommon.shtChno = gudt.SetChInfo.udtChannel(j).udtChCommon.shtChno Then
                        blnFlg = True
                        Exit For
                    End If

                Next

                ''フラグが立っていない場合（変更前の構造体にあるCH番号が、変更後の構造体にない場合）
                If Not blnFlg Then

                    ''変更後のチャンネル構造体で CHID=0,CHNO=0 かつ、CH変換テーブルの該当配列番号の CHID=0 の位置にチャンネル情報をセット
                    For k As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)

                        If gudt.SetChInfo.udtChannel(k).udtChCommon.shtChid = 0 And _
                           gudt.SetChInfo.udtChannel(k).udtChCommon.shtChno = 0 And _
                           gudt.SetChConvPrev.udtChConv(k).shtChid = 0 Then

                            ''新規CHID取得
                            intChidNew += 1
                            Call mGetNewChid(gudt.SetChConvPrev, intChConvTblPrevLastIndex, intChidNew)

                            ''現VerのCH変換テーブルに新規IDを振る
                            gudt.SetChConvNow.udtChConv(k).shtChid = intChidNew


                            ''現VerのCH変換テーブルの配列位置と同じ位置のCH設定テーブルにCH情報をセットする
                            gudt.SetChInfo.udtChannel(k) = udtSetChInfo.udtChannel(i)
                            gudt.SetChInfo.udtChannel(k).udtChCommon.shtChid = intChidNew

                            Exit For

                        End If

                    Next

                End If

            Next

            'Ver2.0.6.2 この段階でエラーが無い場合は、現VerのCHIDコンバートファイルを前Verへｺﾋﾟｰ
            If intErrCnt = 0 Then
                gudt.SetChConvPrev = DeepCopyHelper.DeepCopy(gudt.SetChConvNow)

                'Ver2.0.6.2 ｺﾝﾊﾟｲﾙ前のCHIDと不一致ならばﾒｯｾｰｼﾞを出す
                ' 前VerにCHIDが無い＝新規ならばﾁｪｯｸは行わない
                If mChkChConvTable(udtSetChConvPrevBack) Then
                    '該当場所のCHIDが不一致ならばﾒｯｾｰｼﾞ
                    Dim z As Integer = 0
                    For z = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                        With gudt.SetChInfo.udtChannel(z).udtChCommon
                            '
                            If .shtChid <> udtSetChConvPrevBack.udtChConv(z).shtChid Then
                                'CHno=0,CHID=0の場合はCH削除のためメッセージは出さない
                                If .shtChno <> 0 Or .shtChid <> 0 Then
                                    Call mSetErrString("CHID changed by compilation. [Info]CH NO=" & .shtChno & " CHID=" & udtSetChConvPrevBack.udtChConv(z).shtChid & "->" & .shtChid, _
                                                   "コンパイルによりCHIDが変わりました。[情報]CH NO=" & .shtChno & " CHID=" & udtSetChConvPrevBack.udtChConv(z).shtChid & "->" & .shtChid, intErrCnt, strErrMsg)
                                End If
                            End If
                        End With
                    Next z
                End If
            End If


            ''結果表示
            If intErrCnt = 0 Then

                Call mAddMsgText(" -CH ID Renumber ... Success", " -チャンネルID再付番 ... OK")
                Call mAddMsgText("", "")
            Else

                Call mAddMsgText(" -CH ID Renumber ... Failure", " -チャンネルID再付番 ... エラー")

                ''失敗時はエラー内容を追記
                For i As Integer = 0 To UBound(strErrMsg)
                    Call mAddMsgText("  -" & strErrMsg(i), "  -" & strErrMsg(i))
                    Call mSetErrString(strErrMsg(i), strErrMsg(i))
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#Region "■Excel取込データCHID対応処理"
    '--------------------------------------------------------------------
    ' 機能      : EXCEL取込のデータを使ってデータ再設定
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : EXCEL取込のチャンネル設定のデータを従来のチャンネル設定へ取り込む
    '--------------------------------------------------------------------
    Private Sub subSetExcelChList()
        Try
            Dim i As Integer
            Dim j As Integer
            Dim k As Integer
            Dim intCHID As Integer
            Dim blnFlg As Boolean   'CHNoが見つかったらTrue
            Dim strPathBase As String = ""

            Dim ExcelSetChInfo As gTypSetChInfo = Nothing

            '■従来の計測点リストは、Compileフォルダに保存されているものとする。
            '■Excel取込の計測点リストは、gudt変数に読み込まれているものとする。

            'Excel取込の計測点リストを専用変数へ格納
            ExcelSetChInfo = DeepCopyHelper.DeepCopy(gudt.SetChInfo)

            '従来の計測点リストを変数へ読込(コンパイラフォルダから)
            'パス生成
            strPathBase = System.IO.Path.Combine(gudtFileInfo.strFilePath, gudtFileInfo.strFileVersion)
            strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameCompile)
            strPathBase = System.IO.Path.Combine(strPathBase, gCstPathChannel)
            strPathBase = System.IO.Path.Combine(strPathBase, gCstFileChannel)
            gudt.SetChInfo = Nothing
            If mLoadChannel(gudt.SetChInfo, strPathBase) = -1 Then
                'コンパイルﾌｧｲﾙが無い＝取込ではない場合は処理抜け
                Exit Sub
            End If


            '従来の計測点リストと比較してデータ変更
            With ExcelSetChInfo
                '従来の計測点リストを更新
                For i = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1

                    blnFlg = False
                    For j = 0 To UBound(.udtChannel) Step 1
                        ''同一CHNoが見つかった場合
                        If .udtChannel(j).udtChCommon.shtChno = gudt.SetChInfo.udtChannel(i).udtChCommon.shtChno Then
                            'CHIDを退避（CHIDが消えるため）
                            intCHID = gudt.SetChInfo.udtChannel(i).udtChCommon.shtChid
                            '従来の計測点リストへ、値をセット
                            gudt.SetChInfo.udtChannel(i) = .udtChannel(j)
                            'CHIDを戻す
                            gudt.SetChInfo.udtChannel(i).udtChCommon.shtChid = intCHID

                            blnFlg = True
                            Exit For
                        End If
                    Next j
                    If blnFlg = False Then
                        'ﾌﾗｸﾞがFalseの場合
                        'Excelに無い＝削除されたとみなす
                        Call gInitSetChannelDispOne(gudt.SetChInfo.udtChannel(i))
                    End If
                Next i

                'Excel取込の計測点リストにしかない＝新規を取り込み
                For i = 0 To UBound(.udtChannel) Step 1
                    blnFlg = False
                    For j = 0 To UBound(gudt.SetChInfo.udtChannel) Step 1
                        '同一CHNoが見つかった場合
                        If .udtChannel(i).udtChCommon.shtChno = gudt.SetChInfo.udtChannel(j).udtChCommon.shtChno Then
                            blnFlg = True
                            Exit For
                        End If
                    Next j
                    If blnFlg = False Then
                        'ﾌﾗｸﾞがFalse=Excel取込にしかない＝新規
                        '従来の計測点リストに新規追加
                        For k = 0 To UBound(gudt.SetChInfo.udtChannel)
                            'CHID,CHNO,convTblのCHIDが全て0のﾚｺｰﾄﾞへ追加
                            If gudt.SetChInfo.udtChannel(k).udtChCommon.shtChid = 0 And
                               gudt.SetChInfo.udtChannel(k).udtChCommon.shtChno = 0 And
                               gudt.SetChConvPrev.udtChConv(k).shtChid = 0 Then
                                'CH情報をセットする。CHIDは0とする
                                gudt.SetChInfo.udtChannel(k) = .udtChannel(i)
                                gudt.SetChInfo.udtChannel(k).udtChCommon.shtChid = 0

                                Exit For
                            End If
                        Next k
                    End If
                Next i
            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try
    End Sub
    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報読込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - ( O) チャンネル情報構造体
    ' 　　　    : ARG3 - (I ) ファイルパス
    ' 機能説明  : CHInfo読込処理を行う
    '--------------------------------------------------------------------
    Private Function mLoadChannel(ByRef udtSetChannel As gTypSetChInfo,
             ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strFullPath As String = strPathBase



            ''ファイル存在確認
            If Not System.IO.File.Exists(strFullPath) Then
                intRtn = -1
            Else
                Call mRemakeChannelFileLoad(strFullPath)
                ''ファイルオープン
                intFileNo = FreeFile()
                FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared)

                Try
                    ''ファイル読込み
                    FileGet(intFileNo, udtSetChannel)
                    FileClose(intFileNo)

                    '構造体読み用に作成されたファイルを削除する
                    Call System.IO.File.Delete(strFullPath)
                Catch ex As Exception
                    intRtn = -1
                End Try

            End If

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return -1
        End Try

    End Function
#Region "チャンネル設定ファイルの可変長出力対応"
    Private Sub mRemakeChannelFileLoad(ByRef strFullPath As String)

        Try

            Dim intFileNo As Integer
            Dim udtSetChInfo As gTypSetChInfo = Nothing
            Dim lngByteCntAll As Integer = gCstByteCntHeader + (gCstByteCntChannelOne * gCstChannelIdMax)
            Dim bytInit(lngByteCntAll - 1) As Byte
            Dim bytChCmp() As Byte
            Dim strFilePathTemp1 As String = ""
            Dim strFilePathTemp2 As String = ""

            ''初期化したチャンネル情報構造体を作成する
            Call gInitSetChannelDisp(udtSetChInfo)

            ''tmp1出力パスを作成する（既に存在する場合は削除する）
            strFilePathTemp1 = strFullPath & ".tmp1"
            If System.IO.File.Exists(strFilePathTemp1) Then Call My.Computer.FileSystem.DeleteFile(strFilePathTemp1)

            ''初期化したチャンネル情報構造体を出力する
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFilePathTemp1, OpenMode.Binary, OpenAccess.Write, OpenShare.LockWrite)
            FilePut(intFileNo, udtSetChInfo)
            FileClose(intFileNo)

            ''初期化したチャンネル情報構造体をバイト配列で読み込む
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFilePathTemp1, OpenMode.Binary, OpenAccess.Read, OpenShare.LockWrite)
            FileGet(intFileNo, bytInit)
            FileClose(intFileNo)

            ''可変長で出力された channel.cfg のファイルサイズ（バイト数）を取得する
            Dim objFileInfo As New System.IO.FileInfo(strFullPath)
            Dim lngByteCnt As Long = objFileInfo.Length

            ''可変長で出力された channel.cfg の読み込み用バイト配列を再定義する
            ReDim bytChCmp(lngByteCnt - 1)

            ''可変長で出力された channel.cfg をバイト配列で読み込む
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.LockWrite)
            FileGet(intFileNo, bytChCmp)
            FileClose(intFileNo)

            ''初期化したチャンネル情報を読み込んだバイト配列に、可変長で出力された channel.cfg を読み込んだバイト配列を上書きする
            Call Array.Copy(bytChCmp, bytInit, lngByteCnt)

            ''上記の情報を統合したバイト配列を出力する
            strFilePathTemp2 = strFullPath & ".tmp2"
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFilePathTemp2, OpenMode.Binary, OpenAccess.Write, OpenShare.LockWrite)
            FilePut(intFileNo, bytInit)
            FileClose(intFileNo)

            ''読み込むべきファイルフルパスをout引数に設定する
            strFullPath = strFilePathTemp2

            ''tmp1を削除する（tmp2は読み込んだ後に削除する）
            If System.IO.File.Exists(strFilePathTemp1) Then Call My.Computer.FileSystem.DeleteFile(strFilePathTemp1)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region
#End Region

    Private Function mChkChIdExist(ByVal intChID As Integer, _
                                   ByVal udtChConvPrev As gTypSetChConv, _
                                   ByVal intLastIndex As Integer) As Boolean

        If intChID = 0 Then Return True

        For i As Integer = 0 To intLastIndex

            With udtChConvPrev.udtChConv(i)

                If intChID = .shtChid Then
                    Return True
                End If

            End With

        Next

        Return False

    End Function

    Private Sub mGetNewChid(ByVal udtSetChConvPrev As gTypSetChConv, _
                            ByVal intChConvTblPrevLastIndex As Integer, _
                            ByRef intNewID As Integer)

        Dim blnFlg As Boolean = False

        Do

            blnFlg = False
            For i As Integer = 0 To intChConvTblPrevLastIndex

                If udtSetChConvPrev.udtChConv(i).shtChid = intNewID Then
                    blnFlg = True
                    Exit For
                End If

            Next

            If Not blnFlg Then
                Exit Do
            End If

            intNewID += 1

        Loop

    End Sub

#End Region

#Region "チャンネルID-No変換"


    '--------------------------------------------------------------------
    ' 機能      : チャンネルID、チャンネルNo、システムNo変換
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub mSetChannelInfo(ByVal PreNow As Integer)

        Try

            Dim intErrCnt As Integer
            Dim strMsg() As String = Nothing

            '********************************************************
            '========================================================
            ''注意！！
            ''ここに No-ID 変換を追加した場合、ファイル読み込みフォームの
            ''コンパイルファイル読み込みでID-No変換の追加が必要になります！！
            ''追加箇所：frmFileAccess - mConvChidToChno
            '========================================================
            '********************************************************

            'ID変換前
            If PreNow = 1 Then

                ''出力チャンネル設定
                Call gConvChannelChOutput(gudt.SetChOutputPrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Output Set Channel Setting", "出力チャンネル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''グループリポーズ設定
                Call gConvChannelChGroupRepose(gudt.SetChGroupReposePrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Group Repose Channel Setting", "グループリポーズ設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''論理出力設定
                Call gConvChannelChAndOr(gudt.SetChAndOrPrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "And Or Channel Setting", "論理出力設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''SIO通信チャンネル設定
                For i As Integer = 0 To UBound(gudt.SetChSioChPrev)
                    Call gConvChannelChSIOCh(gudt.SetChSioChPrev(i), intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                    If i < 14 Then
                        Call mOutputChannelConvMsg(IIf(mblnEnglish, "SIO Channel Port " & i + 1 & " Setting", "SIO通信チャンネル設定 ポート" & i + 1), intErrCnt, strMsg)
                    Else
                        Call mOutputChannelConvMsg(IIf(mblnEnglish, "EXT LAN Channel Port " & (i - 14) + 1 & " Setting", "EXT LAN通信チャンネル設定 ポート" & (i - 14) + 1), intErrCnt, strMsg)
                    End If
                    If mblnCancelFlg Then Return
                Next

                ''運転積算トリガチャンネル設定
                Call gConvChannelChRevoTrriger(gudt.SetChInfoPrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Pulse Revolution Trigger Channnel Setting", "運転積算トリガチャンネル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''積算データ設定
                Call gConvChannelChRunHour(gudt.SetChRunHourPrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "RUN HOUR Channel Setting", "積算チャンネル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''シーケンス設定
                Call gConvChannelSeqSequence(gudt.SetSeqSetPrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Sequence Set Channel Setting", "シーケンス設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''排ガス演算設定
                Call gConvChannelExhGus(gudt.SetChExhGusPrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Exh Gas Channel Setting", "排ガス演算設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''データ保存テーブル設定
                Call gConvChannelDataSaveTable(gudt.SetChDataSavePrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Data SaveTable Channel Setting", "データ保存テーブル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''コンポジット設定
                Call gConvChannelComposite(gudt.SetChCompositePrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Composite Channel Setting", "コンポジット設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''演算式テーブル
                Call gConvChannelOpeExp(gudt.SetSeqOpeExpPrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Operation Expression Setting", "演算式テーブル"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''チャンネル情報データ（表示名設定データ）
                Call gConvChannelChDisp(gudt.SetChDispPrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Slot Information(CH Disp) Channel Setting", "チャンネル情報データ(表示名設定データ)設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''Log format CHID・コード テーブル
                Call gConvChannelLogIDChg(gudt.SetOpsLogFormatMPrev, gudt.SetOpsLogIdDataM, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Log Format（ChannelID/Code）Setting", "LogFormat設定テーブル"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return
                gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogIdDataM = gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogFormatM         '' 変更有り：１

                Call gConvChannelLogIDChg(gudt.SetOpsLogFormatCPrev, gudt.SetOpsLogIdDataC, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Log Format（ChannelID/Code）Setting", "LogFormat設定テーブル"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return
                gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogIdDataC = gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogFormatC         '' 変更有り：１

                ''GWS通信チャンネル設定  2014.02.04
                Call gConvChannelChGwsCh(gudt.SetOpsGwsChPrev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "GWS Channel" & " Setting", "GWS通信チャンネル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                'PID用ﾄﾚﾝﾄﾞ設定
                Call gConvChannelChTrendPID(gudt.SetOpsTrendGraphPIDprev, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "PID TREND Channel" & " Setting", "PIDトレンドチャンネル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return



                Call mAddMsgText("", "")

                'ID変換後
            Else

                ''出力チャンネル設定
                Call gConvChannelChOutput(gudt.SetChOutput, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Output Set Channel Setting", "出力チャンネル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''グループリポーズ設定
                Call gConvChannelChGroupRepose(gudt.SetChGroupRepose, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Group Repose Channel Setting", "グループリポーズ設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''論理出力設定
                Call gConvChannelChAndOr(gudt.SetChAndOr, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "And Or Channel Setting", "論理出力設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ' ''SIO設定
                'Call gConvChannelChSIO(gudt.SetChSio, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                'Call mOutputChannelConvMsg(IIf(mblnEnglish, "SIO Channel Setting", "SIO設定"), intErrCnt, strMsg)
                'If mblnCancelFlg Then Return

                ''SIO通信チャンネル設定
                For i As Integer = 0 To UBound(gudt.SetChSioCh)
                    Call gConvChannelChSIOCh(gudt.SetChSioCh(i), intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                    If i < 14 Then
                        Call mOutputChannelConvMsg(IIf(mblnEnglish, "SIO Channel Port " & i + 1 & " Setting", "SIO通信チャンネル設定 ポート" & i + 1), intErrCnt, strMsg)
                    Else
                        Call mOutputChannelConvMsg(IIf(mblnEnglish, "EXT LAN Channel Port " & (i - 14) + 1 & " Setting", "EXT LAN通信チャンネル設定 ポート" & (i - 14) + 1), intErrCnt, strMsg)
                    End If
                    If mblnCancelFlg Then Return
                Next

                ''運転積算トリガチャンネル設定
                Call gConvChannelChRevoTrriger(gudt.SetChInfo, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Pulse Revolution Trigger Channnel Setting", "運転積算トリガチャンネル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''積算データ設定
                Call gConvChannelChRunHour(gudt.SetChRunHour, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "RUN HOUR Channel Setting", "積算チャンネル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''シーケンス設定
                Call gConvChannelSeqSequence(gudt.SetSeqSet, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Sequence Set Channel Setting", "シーケンス設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''排ガス演算設定
                Call gConvChannelExhGus(gudt.SetChExhGus, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Exh Gas Channel Setting", "排ガス演算設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''データ保存テーブル設定
                Call gConvChannelDataSaveTable(gudt.SetChDataSave, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Data SaveTable Channel Setting", "データ保存テーブル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''コンポジット設定
                Call gConvChannelComposite(gudt.SetChComposite, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Composite Channel Setting", "コンポジット設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''演算式テーブル
                Call gConvChannelOpeExp(gudt.SetSeqOpeExp, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Operation Expression Setting", "演算式テーブル"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''チャンネル情報データ（表示名設定データ）
                Call gConvChannelChDisp(gudt.SetChDisp, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Slot Information(CH Disp) Channel Setting", "チャンネル情報データ(表示名設定データ)設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''☆2012.10.26 K.Tanigawa
                ''Log format CHID・コード テーブル
                Call gConvChannelLogIDChg(gudt.SetOpsLogFormatM, gudt.SetOpsLogIdDataM, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Log Format（ChannelID/Code）Setting", "LogFormat設定テーブル"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return
                gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogIdDataM = gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogFormatM         '' 変更有り：１

                Call gConvChannelLogIDChg(gudt.SetOpsLogFormatC, gudt.SetOpsLogIdDataC, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "Log Format（ChannelID/Code）Setting", "LogFormat設定テーブル"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return
                gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogIdDataC = gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogFormatC         '' 変更有り：１

                ''GWS通信チャンネル設定  2014.02.04
                Call gConvChannelChGwsCh(gudt.SetOpsGwsCh, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "GWS Channel" & " Setting", "GWS通信チャンネル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                'PID用ﾄﾚﾝﾄﾞチャンネル設定
                Call gConvChannelChTrendPID(gudt.SetOpsTrendGraphPID, intErrCnt, strMsg, gEnmConvMode.cmNO_ID, mblnEnglish)
                Call mOutputChannelConvMsg(IIf(mblnEnglish, "PID TREND Channel" & " Setting", "PIDトレンドチャンネル設定"), intErrCnt, strMsg)
                If mblnCancelFlg Then Return

                ''OPSのグラフ関連はチャンネルNoでの保存なので何もしない
                'gTypSetOpsGraphExhaust- shtAveCh                         ''平均CH
                'gTypSetOpsGraphExhaustCylinder - shtChCylinder           ''シリンダのCH番号
                'gTypSetOpsGraphExhaustCylinder - shtChDeviation          ''偏差のCH番号
                'gTypSetOpsGraphExhaustTurboCharger - shtChTurboCharger   ''T/CのCH番号"
                'gTypSetOpsGraphBarCylinder - shtChCylinder               ''シリンダのCH番号"
                'gTypSetOpsGraphAnalogMeterDetail - shtChNo               ''CH番号"
                'gTypSetOpsGraphFreeDetail - shtChNo                      ''CH番号"

                ''コントロール使用可/不可の詳細はチャンネルNoでの保存なので何もしない
                'gTypSetChCtrlUseRecDetail - shtChno              ''CH_NO.

                Call mAddMsgText("", "")

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    'T.Ueki
    '--------------------------------------------------------------------
    ' 機能      : チャンネルID各テーブルのID変換比較
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Private Sub mSetChannelInfoCompair()

        Dim i As Integer
        Dim j As Integer
        Dim z As Integer

        ' チャンネル情報   2014.02.08
        For i = 0 To UBound(gudt.SetChInfo.udtChannel)
            If gudt.SetChInfoPrev.udtChannel(i).udtChCommon.shtChid <> gudt.SetChInfo.udtChannel(i).udtChCommon.shtChid Then
                gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphM = 1        '' GRAPHはCHNO保存だが、CHIDが変わるとOPSで再作成必要につき更新
                If gudt.SetSystem.udtSysSystem.shtCombineUse <> 0 Then      '' コンバイン時のみCARGO更新
                    gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphC = 1
                End If
            End If
        Next


        '出力チャンネル
        For i = 0 To UBound(gudt.SetChOutput.udtCHOutPut)
            If gudt.SetChOutputPrev.udtCHOutPut(i).shtChid <> gudt.SetChOutput.udtCHOutPut(i).shtChid Then
                gudt.SetEditorUpdateInfo.udtCompile.bytOutPut = 1
            End If
        Next

        'グループリポーズ
        For i = 0 To UBound(gudt.SetChGroupRepose.udtRepose)
            If gudt.SetChGroupReposePrev.udtRepose(i).shtChId <> gudt.SetChGroupRepose.udtRepose(i).shtChId Then
                gudt.SetEditorUpdateInfo.udtCompile.bytRepose = 1
            End If

            ''チェックマークがついていないかつ47行目の格納時にFor文を抜ける 2018.12.13 倉重
            If g_bytGREPNUM = 0 And i = 47 Then
                Exit For
            End If
        Next


        ''論理出力設定
        For i = 0 To UBound(gudt.SetChAndOr.udtCHOut)
            For j = 0 To UBound(gudt.SetChAndOr.udtCHOut(i).udtCHAndOr)
                If gudt.SetChAndOrPrev.udtCHOut(i).udtCHAndOr(j).shtChid <> gudt.SetChAndOr.udtCHOut(i).udtCHAndOr(j).shtChid Then
                    gudt.SetEditorUpdateInfo.udtCompile.bytOrAnd = 1
                End If
            Next

        Next

        ''SIO通信チャンネル設定
        For i = 0 To UBound(gudt.SetChSioCh)
            For j = 0 To UBound(gudt.SetChSioChPrev(i).udtSioChRec)
                If gudt.SetChSioChPrev(i).udtSioChRec(j).shtChId <> gudt.SetChSioCh(i).udtSioChRec(j).shtChId Then
                    gudt.SetEditorUpdateInfo.udtCompile.bytChSioCh(i) = 1
                End If
            Next

        Next

        ''運転積算トリガチャンネル設定
        For i = 0 To UBound(gudt.SetChInfo.udtChannel)
            If gudt.SetChInfoPrev.udtChannel(i).RevoTrigerChid <> gudt.SetChInfo.udtChannel(i).RevoTrigerChid Then
                gudt.SetEditorUpdateInfo.udtCompile.bytChRunHour = 1    '' 2014.01.15
            End If
        Next

        ''積算データ設定
        For i = 0 To UBound(gudt.SetChRunHour.udtDetail)
            If gudt.SetChRunHourPrev.udtDetail(i).shtChid <> gudt.SetChRunHour.udtDetail(i).shtChid Then
                gudt.SetEditorUpdateInfo.udtCompile.bytChRunHour = 1
            End If
        Next

        ''シーケンス設定
        For i = 0 To UBound(gudt.SetSeqSetPrev.udtDetail)
            If gudt.SetSeqSetPrev.udtDetail(i).shtOutChid <> gudt.SetSeqSet.udtDetail(i).shtOutChid Then
                gudt.SetEditorUpdateInfo.udtCompile.bytSeqSequenceSet = 1
            End If
        Next

        ''排ガス演算設定
        For i = 0 To UBound(gudt.SetChExhGusPrev.udtExhGusRec)
            If gudt.SetChExhGusPrev.udtExhGusRec(i).shtAveChid <> gudt.SetChExhGus.udtExhGusRec(i).shtAveChid And _
                               gudt.SetChExhGusPrev.udtExhGusRec(i).shtRepChid <> gudt.SetChExhGus.udtExhGusRec(i).shtRepChid Then
                gudt.SetEditorUpdateInfo.udtCompile.bytExhGus = 1
            End If
        Next

        ''データ保存テーブル設定
        For i = 0 To UBound(gudt.SetChDataSavePrev.udtDetail)
            If gudt.SetChDataSavePrev.udtDetail(i).shtChid <> gudt.SetChDataSave.udtDetail(i).shtChid Then
                gudt.SetEditorUpdateInfo.udtCompile.bytChDataSaveTable = 1
            End If
        Next

        ''コンポジット設定
        For i = 0 To UBound(gudt.SetChCompositePrev.udtComposite)
            If gudt.SetChCompositePrev.udtComposite(i).shtChid <> gudt.SetChComposite.udtComposite(i).shtChid Then
                gudt.SetEditorUpdateInfo.udtCompile.bytComposite = 1
            End If
        Next

        ''演算式テーブル
        For i = 0 To UBound(gudt.SetSeqOpeExpPrev.udtTables)
            For j = 0 To 15
                For z = 2 To 3
                    If gudt.SetSeqOpeExpPrev.udtTables(i).udtAryInf(j).bytInfo(z) <> gudt.SetSeqOpeExp.udtTables(i).udtAryInf(j).bytInfo(z) Then
                        gudt.SetEditorUpdateInfo.udtCompile.bytSeqOperationExpression = 1
                    End If
                Next

            Next
        Next

        ''チャンネル情報データ（表示名設定データ）
        For i = 0 To UBound(gudt.SetChDispPrev.udtChDisp)
            For j = 0 To UBound(gudt.SetChDispPrev.udtChDisp(i).udtSlotInfo)
                For z = 0 To UBound(gudt.SetChDispPrev.udtChDisp(i).udtSlotInfo(j).udtPinInfo)
                    If gudt.SetChDispPrev.udtChDisp(i).udtSlotInfo(j).udtPinInfo(z).shtChid <> gudt.SetChDisp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(z).shtChid Then
                        gudt.SetEditorUpdateInfo.udtCompile.bytChDisp = 1
                    End If
                Next

            Next

        Next

        ''GWS通信チャンネル設定
        For i = 0 To UBound(gudt.SetOpsGwsCh.udtGwsFileRec)
            For j = 0 To UBound(gudt.SetOpsGwsCh.udtGwsFileRec(i).udtGwsChRec)
                If gudt.SetOpsGwsChPrev.udtGwsFileRec(i).udtGwsChRec(j).shtChId <> gudt.SetOpsGwsCh.udtGwsFileRec(i).udtGwsChRec(j).shtChId Then
                    gudt.SetEditorUpdateInfo.udtCompile.bytOpsGwsCh = 1
                End If
            Next

        Next


        'PID用ﾄﾚﾝﾄﾞ
        For i = 0 To UBound(gudt.SetOpsTrendGraphPID.udtTrendGraphRec) Step 1
            For j = 0 To UBound(gudt.SetOpsTrendGraphPID.udtTrendGraphRec(i).udtTrendGraphRecChno)
                If gudt.SetOpsTrendGraphPIDprev.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno <> gudt.SetOpsTrendGraphPID.udtTrendGraphRec(i).udtTrendGraphRecChno(j).shtChno Then
                    gudt.SetEditorUpdateInfo.udtCompile.bytOpsTrendGraphPID = 1
                End If
            Next j
        Next i


    End Sub





    '--------------------------------------------------------------------
    ' 機能      : チャンネル変換メッセージ出力
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 名称
    ' 　　　    : ARG2 - (I ) エラー数
    ' 　　　    : ARG3 - (I ) メッセージ
    ' 機能説明  : チャンネル変換メッセージを出力する
    '--------------------------------------------------------------------
    Private Sub mOutputChannelConvMsg(ByVal strCurName As String, _
                                      ByVal intErrCnt As Integer, _
                                      ByVal strMsg() As String)

        Try

            ''メッセージ
            If intErrCnt = 0 Then

                ''全て成功
                Call mAddMsgText(" -" & strCurName & " ... Success", " -" & strCurName & " ... OK")

            Else

                ''失敗
                Call mAddMsgText(" -" & strCurName & " ... Failure", " -" & strCurName & " ... エラー")

                ''エラー内容を追記
                For i As Integer = 0 To UBound(strMsg)
                    Call mAddMsgText("  -" & strMsg(i), "  -" & strMsg(i))
                    Call mSetErrString(strMsg(i) & " [Info] " & strCurName, strMsg(i) & " [情報] " & strCurName)
                    If mblnCancelFlg Then Return
                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region



#Region "コンパイルファイル出力"

#Region "全体出力関数"

    '--------------------------------------------------------------------
    ' 機能      : コンパイルファイル出力
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : コンパイルファイルを出力する
    '--------------------------------------------------------------------
    Private Sub mOutputCompileFile()

        Try

            Dim intRtn As Integer
            Dim strPathBase As String = ""

            Dim strPathBaseEditor As String

            Dim strPathVerInfoPrev As String = ""
            Dim strPathUpdateInfo As String = ""

            '2015/5/8 T.Ueki
            Dim strPathFUprog As String = ""
            Dim strPathFUnew As String = ""
            Dim strPathFUsave As String = ""
            Dim strPathorg As String = ""
            Dim strPathmimic As String = ""

            '2015/6/10 T.Ueki
            Dim strPathlog As String = ""

            Dim strPathSaveNow As String = ""

            ''バージョン番号までのファイルパス作成
            With mudtFileInfo

                ''ファイル名までのパス
                strPathBase = System.IO.Path.Combine(.strFilePath, .strFileName)

                ''バージョン番号までのパス（コピー用）
                strPathSaveNow = strPathBase

                ''バージョン番号までのパス T.Ueki Tempフォルダに出力先変更
                strPathBaseEditor = .strFileVersion
                strPathBase = strPathBase & "\Temp"

                ''EditorInfo配下のフォルダパス
                strPathUpdateInfo = System.IO.Path.Combine(System.IO.Path.Combine(strPathBaseEditor, gCstFolderNameEditorInfo), gCstFolderNameUpdateInfo)

                ''Compile までのパス
                strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameCompile)

                ''Compile までのパス（コピー用）
                strPathSaveNow = System.IO.Path.Combine(strPathSaveNow, gCstFolderNameSave)

                ''[org][prog][new][save] までのパス 2015/5/8 T.Ueki
                strPathFUprog = System.IO.Path.Combine(strPathBase, gCstFolderNameFUProg)
                strPathFUnew = System.IO.Path.Combine(strPathBase, gCstFolderNameFUNew)
                strPathFUsave = System.IO.Path.Combine(strPathBase, gCstFolderNameFUSave)
                strPathorg = System.IO.Path.Combine(strPathBase, gCstFolderNameOrg)
                strPathmimic = System.IO.Path.Combine(strPathBase, gCstFolderNameMimic)
                strPathlog = System.IO.Path.Combine(strPathBase, gCstFolderNameLog)

                Call mAddMsgText("Make output path ... Success", " -出力パス作成 ... OK")

            End With

            ''Compileフォルダ作成
            If gMakeFolder(strPathBase) <> 0 Then
                Call mAddMsgText("Make output folder ... Failure", " -出力フォルダ作成 ... エラー")
                Call mAddMsgText(" -It failed in making the output folder. [" & strPathBase & "]", "出力フォルダの作成に失敗しました。[" & strPathBase & "]")
                Call mAddMsgText(" -The compile file is not output.", "コンパイルファイルは出力されません。")
                Return
            Else
                Call mAddMsgText("Make output folder [" & strPathBase & "] ... Success", " -出力フォルダ作成 [" & strPathBase & "] ... OK")
            End If

            ''progフォルダ作成 2015/5/8 T.Ueki
            If gMakeFolder(strPathFUprog) <> 0 Then
                Call mAddMsgText("Make prog folder ... Failure", " -FU用Progフォルダ作成 ... エラー")
                Call mAddMsgText(" -It failed in making the prog folder. [" & strPathFUprog & "]", "FU用Progフォルダの作成に失敗しました。[" & strPathFUprog & "]")
                Return
            Else
                Call mAddMsgText("Make prog folder [" & strPathFUprog & "] ... Success", " -FU用Progフォルダ作成 [" & strPathFUprog & "] ... OK")
            End If

            ''newフォルダ作成 2015/5/8 T.Ueki
            If gMakeFolder(strPathFUnew) <> 0 Then
                Call mAddMsgText("Make new folder ... Failure", " -FU用Newフォルダ作成 ... エラー")
                Call mAddMsgText(" -It failed in making the new folder. [" & strPathFUnew & "]", "FU用Newフォルダの作成に失敗しました。[" & strPathFUnew & "]")
                Return
            Else
                Call mAddMsgText("Make new folder [" & strPathFUnew & "] ... Success", " -FU用Newフォルダ作成 [" & strPathFUnew & "] ... OK")
            End If

            ''saveフォルダ作成 2015/5/8 T.Ueki
            If gMakeFolder(strPathFUsave) <> 0 Then
                Call mAddMsgText("Make save folder ... Failure", " -FU用Saveフォルダ作成 ... エラー")
                Call mAddMsgText(" -It failed in making the save folder. [" & strPathFUsave & "]", "FU用Saveフォルダの作成に失敗しました。[" & strPathFUsave & "]")
                Return
            Else
                Call mAddMsgText("Make save folder [" & strPathFUsave & "] ... Success", " -FU用Saveフォルダ作成 [" & strPathFUsave & "] ... OK")
            End If

            ''logフォルダ作成 2015/6/10 T.Ueki
            If gMakeFolder(strPathlog) <> 0 Then
                Call mAddMsgText("Make Log folder ... Failure", " -Logフォルダ作成 ... エラー")
                Call mAddMsgText(" -It failed in making the Log folder. [" & strPathlog & "]", "Logフォルダの作成に失敗しました。[" & strPathlog & "]")
                Return
            Else
                Call mAddMsgText("Make Log folder [" & strPathlog & "] ... Success", " -Logフォルダ作成 [" & strPathlog & "] ... OK")
            End If

            ''orgフォルダ作成 2015/5/8 T.Ueki
            If gMakeFolder(strPathorg) <> 0 Then
                Call mAddMsgText("Make org folder ... Failure", " -Orgフォルダ作成 ... エラー")
                Call mAddMsgText(" -It failed in making the org folder. [" & strPathorg & "]", "Orgフォルダの作成に失敗しました。[" & strPathorg & "]")
                Return
            Else
                Call mAddMsgText("Make org folder [" & strPathorg & "] ... Success", " -Orgフォルダ作成 [" & strPathorg & "] ... OK")
            End If



            'With mudtFileInfo

            '    ''ファイル名までのパス
            '    strPathBase = System.IO.Path.Combine(.strFilePath, .strFileName)

            '    ''バージョン番号までのパス（コピー用）
            '    strPathSaveNow = strPathBase
            '    strPathSavePrev = .strFileVersionPrev
            '    strPathCompileNow = .strFileVersion
            '    strPathCompilePrev = .strFileVersionPrev

            '    ''バージョン番号までのパス T.Ueki Tempフォルダに出力先変更
            '    strPathBaseEditor = .strFileVersion
            '    strPathBase = strPathBase & "\Temp"

            '    ' ''バージョン番号までのパス（コピー用）
            '    'strPathSaveNow = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersion), "000"))
            '    'strPathSavePrev = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersionPrev), "000"))
            '    'strPathCompileNow = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersion), "000"))
            '    'strPathCompilePrev = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersionPrev), "000"))

            '    ' ''バージョン番号までのパス T.Ueki Tempフォルダに出力先変更
            '    'strPathBaseEditor = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersion), "000"))
            '    'strPathBase = System.IO.Path.Combine(strPathBase, gCstVersionPrefix & Format(CInt(.strFileVersion), "000") & "\Temp")

            '    ''EditorInfo配下のフォルダパス
            '    ''strPathVerInfoPrev = System.IO.Path.Combine(System.IO.Path.Combine(strPathBaseEditor, gCstFolderNameEditorInfo), gCstFolderNameVerInfoPre)
            '    strPathUpdateInfo = System.IO.Path.Combine(System.IO.Path.Combine(strPathBaseEditor, gCstFolderNameEditorInfo), gCstFolderNameUpdateInfo)

            '    ''Compile までのパス
            '    strPathBase = System.IO.Path.Combine(strPathBase, gCstFolderNameCompile)

            '    ''Compile までのパス（コピー用）
            '    strPathSaveNow = System.IO.Path.Combine(strPathSaveNow, gCstFolderNameSave)
            '    strPathSavePrev = System.IO.Path.Combine(strPathSavePrev, gCstFolderNameSave)
            '    strPathCompileNow = System.IO.Path.Combine(strPathCompileNow, gCstFolderNameCompile)
            '    strPathCompilePrev = System.IO.Path.Combine(strPathCompilePrev, gCstFolderNameCompile)

            '    Call mAddMsgText("Make output path ... Success", " -出力パス作成 ... OK")

            'End With

            ' ''Compileフォルダ作成
            'If gMakeFolder(strPathBase) <> 0 Then
            '    Call mAddMsgText("Make output folder ... Failure", " -出力フォルダ作成 ... エラー")
            '    Call mAddMsgText(" -It failed in making the output folder. [" & strPathBase & "]", _
            '                     "出力フォルダの作成に失敗しました。[" & strPathBase & "]")
            '    Call mAddMsgText(" -The compile file is not output.", "コンパイルファイルは出力されません。")
            '    Return
            'Else
            '    Call mAddMsgText("Make output folder [" & strPathBase & "] ... Success", _
            '                     " -出力フォルダ作成 [" & strPathBase & "] ... OK")
            'End If

            '' ''VerInfoPrevフォルダ作成
            ''If gMakeFolder(strPathVerInfoPrev) <> 0 Then
            ''    Call mAddMsgText("Make output folder ... Failure", " -出力フォルダ作成 ... エラー")
            ''    Call mAddMsgText(" -It failed in making the output folder. [" & strPathVerInfoPrev & "]", _
            ''                     "出力フォルダの作成に失敗しました。[" & strPathVerInfoPrev & "]")
            ''    Call mAddMsgText(" -The compile file is not output.", "コンパイルファイルは出力されません。")
            ''    Return
            ''Else
            ''    Call mAddMsgText("Make output folder [" & strPathVerInfoPrev & "] ... Success", _
            ''                     " -出力フォルダ作成 [" & strPathVerInfoPrev & "] ... OK")
            ''End If

            '' ''UpdateInfoフォルダ作成
            ''If gMakeFolder(strPathUpdateInfo) <> 0 Then
            ''    Call mAddMsgText("Make output folder ... Failure", " -出力フォルダ作成 ... エラー")
            ''    Call mAddMsgText(" -It failed in making the output folder. [" & strPathUpdateInfo & "]", _
            ''                     "出力フォルダの作成に失敗しました。[" & strPathUpdateInfo & "]")
            ''    Call mAddMsgText(" -The compile file is not output.", "コンパイルファイルは出力されません。")
            ''    Return
            ''Else
            ''    Call mAddMsgText("Make output folder [" & strPathUpdateInfo & "] ... Success", _
            ''                     " -出力フォルダ作成 [" & strPathUpdateInfo & "] ... OK")
            ''End If

            ' ''バージョンアップ保存の場合は旧バージョンの設定ファイルをコピーする
            'If gudtFileInfo.blnVersionUP Then

            '    ' ''Saveフォルダ
            '    'If System.IO.Directory.Exists(strPathSavePrev) Then
            '    '    If gCopyDirectory(strPathSavePrev, strPathSaveNow, gudtFileInfo.strFileVersionPrev, gudtFileInfo.strFileVersion) <> 0 Then
            '    '        Call mAddMsgText("Copy prev ver save files ... Failure", " -前バージョンデータ（Saveフォルダ）コピー ... エラー")
            '    '        Call mAddMsgText(" -It failed in copy the prev ver save files. [" & strPathSaveNow & "]", _
            '    '                         "前バージョン設定ファイル（Saveフォルダ）のコピーに失敗しました。[" & strPathSaveNow & "]")
            '    '        Call mAddMsgText(" -The compile file is not output.", "コンパイルファイルは出力されません。")
            '    '        Return
            '    '    Else
            '    '        Call mAddMsgText("Copy prev ver save files ... Success", _
            '    '                         " -前バージョンデータ（Saveフォルダ）コピー ... OK")
            '    '    End If
            '    'End If

            '    ' ''Compileフォルダ
            '    'If System.IO.Directory.Exists(strPathCompilePrev) Then
            '    '    If gCopyDirectory(strPathCompilePrev, strPathCompileNow) <> 0 Then
            '    '        Call mAddMsgText("Copy prev ver compile files ... Failure", " -前バージョンデータ（compileフォルダ）コピー ... エラー")
            '    '        Call mAddMsgText(" -It failed in copy the prev ver compile files. [" & strPathCompileNow & "]", _
            '    '                         "前バージョン設定ファイル（compileフォルダ）のコピーに失敗しました。[" & strPathCompileNow & "]")
            '    '        Call mAddMsgText(" -The compile file is not output.", "コンパイルファイルは出力されません。")
            '    '        Return
            '    '    Else
            '    '        Call mAddMsgText("Copy prev ver compile files ... Success", _
            '    '                         " -前バージョンデータ（compileフォルダ）コピー ... OK")
            '    '    End If
            '    'End If

            'End If

            Call mAddMsgText("", "")
            Call mAddMsgText("Complie file output start", "コンパイルファイル出力開始")
            Call mAddMsgText("Output path : " & strPathBase, "出力パス : " & strPathBase)
            Call mAddMsgText("", "")

            Call SetCompileSaveFg()     '' Ver1.10.8 2016.06.28  全ﾃﾞｰﾀを保存するように変更


            'システム設定データ書き込み
            intRtn += mSaveSystem(gudt.SetSystem, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytSystem)
            If mblnCancelFlg Then Return

            ''FU チャンネル情報書き込み
            intRtn += mSaveFuChannel(gudt.SetFu, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytFuChannel)
            If mblnCancelFlg Then Return

            ''チャンネル情報データ（表示名設定データ）書き込み
            intRtn += mSaveChDisp(gudt.SetChDisp, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytChDisp)
            If mblnCancelFlg Then Return

            ''チャンネル情報書き込み
            intRtn += mSaveChannel(gudt.SetChInfo, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytChannel)
            If mblnCancelFlg Then Return

            ''コンポジット情報書き込み
            intRtn += mSaveComposite(gudt.SetChComposite, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytComposite)
            If mblnCancelFlg Then Return

            ''グループ設定書き込み
            intRtn += mSaveGroup(gudt.SetChGroupSetM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytGroupM)
            intRtn += mSaveGroup(gudt.SetChGroupSetC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytGroupC)
            If mblnCancelFlg Then Return

            ''リポーズ入力設定書き込み
            intRtn += mSaveRepose(gudt.SetChGroupRepose, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytRepose)
            If mblnCancelFlg Then Return

            ''出力チャンネル設定書き込み
            intRtn += mSaveOutPut(gudt.SetChOutput, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytOutPut)
            If mblnCancelFlg Then Return

            ''論理出力設定書き込み
            intRtn += mSaveOrAnd(gudt.SetChAndOr, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytOrAnd)
            If mblnCancelFlg Then Return

            ''積算データ設定書き込み
            intRtn += mSaveChRunHour(gudt.SetChRunHour, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytChRunHour)
            If mblnCancelFlg Then Return

            ''コントロール使用可／不可設定書き込み
            intRtn += mSaveCtrlUseNotuse(gudt.SetChCtrlUseM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytCtrlUseNotuseM)
            intRtn += mSaveCtrlUseNotuse(gudt.SetChCtrlUseC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytCtrlUseNotuseC)
            If mblnCancelFlg Then Return

            ''SIO設定書き込み
            intRtn += mSaveChSio(gudt.SetChSio, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytChSio)
            If mblnCancelFlg Then Return

            ''SIO通信チャンネル設定書き込み
            For i As Integer = 0 To UBound(gudt.SetChSioCh)
                intRtn += mSaveChSioCh(gudt.SetChSioCh(i), mudtFileInfo, strPathBase, i + 1, gudt.SetEditorUpdateInfo.udtCompile.bytChSioCh(i))
                If mblnCancelFlg Then Return
            Next

            'Ver2.0.5.8
            'SIO拡張設定書き込み
            For i As Integer = 0 To UBound(gudt.SetChSioExt)
                intRtn += mSaveChSioExt(gudt.SetChSio.udtVdr(i).shtKakuTbl, gudt.SetChSioExt(i), mudtFileInfo, strPathBase, i + 1, gudt.SetEditorUpdateInfo.udtCompile.bytChSioExt(i))
                If mblnCancelFlg Then Return
            Next


            ''排ガス処理演算設定書き込み
            intRtn += mSaveExhGus(gudt.SetChExhGus, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytExhGus)
            If mblnCancelFlg Then Return

            ''延長警報書き込み
            intRtn += mSaveExtAlarm(gudt.SetExtAlarm, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytExtAlarm)
            If mblnCancelFlg Then Return

            ''タイマ設定書き込み
            intRtn += mSaveTimer(gudt.SetExtTimerSet, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytTimer)
            If mblnCancelFlg Then Return

            ''タイマ表示名称設定書き込み
            intRtn += mSaveTimerName(gudt.SetExtTimerName, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytTimerName)
            If mblnCancelFlg Then Return

            ''シーケンスID書き込み
            intRtn += mSaveSeqSequenceID(gudt.SetSeqID, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytSeqSequenceID)
            If mblnCancelFlg Then Return

            ''シーケンス設定書き込み
            intRtn += mSaveSeqSequenceSet(gudt.SetSeqSet, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytSeqSequenceSet)
            If mblnCancelFlg Then Return

            'リニアライズテーブル書き込み
            intRtn += mSaveSeqLinear(gudt.SetSeqLinear, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytSeqLinear)
            If mblnCancelFlg Then Return

            '演算式テーブル書き込み
            intRtn += mSaveSeqOperationExpression(gudt.SetSeqOpeExp, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytSeqOperationExpression)
            If mblnCancelFlg Then Return

            ''データ保存テーブル設定
            intRtn += mSaveChDataSaveTable(gudt.SetChDataSave, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytChDataSaveTable)
            If mblnCancelFlg Then Return

            ''データ転送テーブル設定
            intRtn += mSaveChDataForwardTableSet(gudt.SetChDataForward, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytChDataForwardTableSet)
            If mblnCancelFlg Then Return

            'OPSスクリーンタイトル書き込み
            intRtn += mSaveOpsScreenTitle(gudt.SetOpsScreenTitleM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsScreenTitleM)
            intRtn += mSaveOpsScreenTitle(gudt.SetOpsScreenTitleC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsScreenTitleC)
            If mblnCancelFlg Then Return

            ''プルダウンメニュー
            intRtn += mSaveManuMain(gudt.SetOpsPulldownMenuM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainM)
            intRtn += mSaveManuMain(gudt.SetOpsPulldownMenuC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainC)
            If mblnCancelFlg Then Return

            ''セレクションメニュー
            intRtn += mSaveSelectionMenu(gudt.SetOpsSelectionMenuM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsSelectionMenuM)
            intRtn += mSaveSelectionMenu(gudt.SetOpsSelectionMenuC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsSelectionMenuC)
            If mblnCancelFlg Then Return

            'intRtn += mSaveSelectionMenu(gudt.SetOpsSelectionMenuM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainM)
            'intRtn += mSaveSelectionMenu(gudt.SetOpsSelectionMenuC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsManuMainC)
            'If mblnCancelFlg Then Return

            ''OPSグラフ設定
            intRtn += mSaveOpsGraph(gudt.SetOpsGraphM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphM)
            intRtn += mSaveOpsGraph(gudt.SetOpsGraphC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsGraphC)
            If mblnCancelFlg Then Return

            ''フリーグラフ    2013.07.22 グラフとフリーグラフを分離  K.Fujimoto
            intRtn += mSaveOpsFreeGraph(gudt.SetOpsFreeGraphM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsFreeGraphM)
            intRtn += mSaveOpsFreeGraph(gudt.SetOpsFreeGraphC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsFreeGraphC)
            If mblnCancelFlg Then Return

            ''フリーディスプレイ
            intRtn += mSaveOpsFreeDisplay(gudt.SetOpsFreeDisplayM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsFreeDisplayM)
            intRtn += mSaveOpsFreeDisplay(gudt.SetOpsFreeDisplayC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsFreeDisplayC)
            If mblnCancelFlg Then Return

            ''トレンドグラフ
            intRtn += mSaveOpsTrendGraph(gudt.SetOpsTrendGraphM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsTrendGraphM)
            intRtn += mSaveOpsTrendGraph(gudt.SetOpsTrendGraphC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsTrendGraphC)

            If fnChkPID() = True Then
                intRtn += mSaveOpsTrendGraph_PID(gudt.SetOpsTrendGraphPID, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsTrendGraphPID)
                intRtn += mSaveOpsTrendGraph_PID(gudt.SetOpsTrendGraphPID, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsTrendGraphPID2)
            End If
            If mblnCancelFlg Then Return

            ''ログフォーマット
            ''            intRtn += mSaveOpsLogFormat(gudt.SetOpsLogFormatM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogFormatM)
            ''          intRtn += mSaveOpsLogFormat(gudt.SetOpsLogFormatC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogFormatC)
            ''        If mblnCancelFlg Then Return

            ''ログフォーマットCHID ☆2012/10/26 K.Tanigawa
            intRtn += mSaveOpsLogIDData(gudt.SetOpsLogIdDataM, mudtFileInfo, strPathBase, True, gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogIdDataM)
            intRtn += mSaveOpsLogIDData(gudt.SetOpsLogIdDataC, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogIdDataC)
            If mblnCancelFlg Then Return

            ''Ver1.9.6 2016.02.08  ﾛｸﾞ印字特殊設定
            intRtn += mSaveOpsLogOptionSet(gudt.SetOpsLogOption, mudtFileInfo, strPathBase, False, gudt.SetEditorUpdateInfo.udtCompile.bytOpsLogOption)
            If mblnCancelFlg Then Return
            ''//

            ''GWS通信チャンネル設定書き込み  2014.02.04
            intRtn += mSaveOpsGwsCh(gudt.SetOpsGwsCh, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytOpsGwsCh)
            If mblnCancelFlg Then Return

            ''CH変換テーブル（現VerのCH変換テーブルをコンパイルフォルダに保存する）
            intRtn += mSaveChConv(gudt.SetChConvNow, mudtFileInfo, strPathBase, gudt.SetEditorUpdateInfo.udtCompile.bytChConvNow, gudt.SetChInfo)   '' K.Tanigawa 2013/02/14 gudt.SetChinfo 追加
            If mblnCancelFlg Then Return

            'コンパイル時は出力しない。
            ' ''CH変換テーブル（前VerのCH変換テーブルをVerInfoPrevフォルダに保存する）
            'intRtn += mSaveChConv(gudt.SetChConvPrev, mudtFileInfo, strPathVerInfoPrev, gudt.SetEditorUpdateInfo.udtCompile.bytChConvPrev, gudt.SetChInfo)  '' K.Tanigawa 2013/02/14 gudt.SetChinfo 追加
            'If mblnCancelFlg Then Return

            ''デフォルトデータ作成用
            ''ログ印字設定
            'intRtn += mSaveOtherLogTime(gudt.SetOtherLogTime, mudtFileInfo, strPathBase, True)
            'intRtn += mSaveOtherLogTime(gudt.SetOtherLogTime, mudtFileInfo, strPathBase, False)
            'If mblnCancelFlg Then Return

            'コンパイル時は出力しない。
            ' ''ファイル更新情報
            'intRtn += mSaveEditorUpdateInfo(gudt.SetEditorUpdateInfo, mudtFileInfo, strPathUpdateInfo)
            'If mblnCancelFlg Then Return

            ''デフォルトデータ
            If optDefaultCopy.Checked Then
                intRtn += mCopyDefaultData(strPathBase)
                If mblnCancelFlg Then Return
            ElseIf gudt.SetSystem.udtSysPrinter.shtLogDrawNo <> 0 Then      '' Ver1.11.8.1 2016.10.28  ﾛｸﾞ設定ﾌｧｲﾙｺﾋﾟ処理追加
                intRtn += mCopyLogDefaultData(strPathBase, True)
            End If
            If mblnCancelFlg Then Return

            ''メッセージ表示
            If intRtn <> 0 Then

                ''失敗
                Call mAddMsgText("", "")
                Call mAddMsgText("Failed compiling.", "コンパイルファイルの出力でエラーが発生しました。")

            Else

                ''成功
                Call mAddMsgText("", "")
                Call mAddMsgText("Succeeded compiling.", "コンパイルファイルを出力しました。")

                '2015/5/8 T.Ueki
                'コンパイル出力完了後、orgフォルダにCompile内の[log][meas][set][mimic]結果をコピーする。

                If gCopyDirectory(strPathBase & "\log", strPathorg & "\log") <> 0 Then
                    Call mAddMsgText("It failed in org file. [log folder Copy]", "orgフォルダ内にlogフォルダをコピー出来ません。")
                    Return
                End If

                'Ver2.0.5.8
                Call subCompileSIOext(strPathorg)

                If gCopyDirectory(strPathBase & "\set", strPathorg & "\set") <> 0 Then
                    Call mAddMsgText("It failed in org file. [set folder Copy]", "orgフォルダ内にsetフォルダをコピー出来ません。")
                    Return
                End If

                If gCopyDirectory(strPathBase & "\meas", strPathorg & "\meas") <> 0 Then
                    Call mAddMsgText("It failed in org file. [meas folder Copy]", "orgフォルダ内にmeasフォルダをコピー出来ません。")
                    Return
                End If

                'Mimicフォルダが存在しない場合は処理しない
                If Not System.IO.Directory.Exists(strPathmimic) Then

                Else

                    If gCopyDirectory(strPathBase & "\mimic", strPathorg & "\mimic") <> 0 Then
                        Call mAddMsgText("It failed in org file. [mimic folder Copy]", "orgフォルダ内にmimicフォルダをコピー出来ません。")
                        Return
                    End If

                End If

            End If

            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "SIO拡張用特殊ﾌｧｲﾙ処理"
    Private Sub subCompileSIOext(pstrOrgPath As String)
        'orgﾌｫﾙﾀﾞのset\extのSIO拡張ﾌｧｲﾙを削除する
        '※SIO拡張ﾌｧｲﾙは増えたり減ったりするため。
        Dim intPortNo As Integer = 1
        Dim strCpath As String = ""
        Dim strFullPath As String = ""

        strCpath = System.IO.Path.Combine(pstrOrgPath, gCstPathChSioExt)

        '2019.03.18 ファイル数変更
        Dim strCurFileName As String
        For intPortNo = 1 To 16 Step 1
            strCurFileName = gCstFileChSioExtName & Format(intPortNo, "00") & gCstFileChSioExtExt
            strFullPath = System.IO.Path.Combine(strCpath, strCurFileName)

            If System.IO.File.Exists(strFullPath) = True Then
                Call System.IO.File.Delete(strFullPath)
            End If
        Next

    End Sub
#End Region

#Region "個別出力関数"

#Region "システム設定"

    '--------------------------------------------------------------------
    ' 機能      : システム設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) システム設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSystem(ByVal udtSetSystem As gTypSetSystem, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSystem
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSystem)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSystem.udtHeader, udtFileInfo.strFileVersion, gCstRecsSystem, gCstSizeSystem, , , , gCstFnumSystem)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSystem)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "ＦＵチャンネル情報"

    '--------------------------------------------------------------------
    ' 機能      : ＦＵチャンネル情報保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveFuChannel(ByVal udtSetFuChannel As gTypSetFu, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String, _
                                    ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathFuChannel
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileFuChannel)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetFuChannel.udtHeader, udtFileInfo.strFileVersion, gCstRecsFuChannel, gCstSizeFuChannel, , , , gCstFnumFuChannel)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetFuChannel)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報データ"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報データ保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChDisp(ByVal udtSetFuChannel As gTypSetChDisp, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDisp
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDisp)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetFuChannel.udtHeader, udtFileInfo.strFileVersion, gCstRecsChDisp, gCstSizeChDisp, , , , gCstFnumChDisp)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)


            'Ver2.0.1.5 Remarksに「^」が入っている場合消す
            'Ver2.0.2.5 和文の場合消す。英文の場合半角スペースへ置き換え
            With udtSetFuChannel
                Dim i As Integer = 0
                For i = 0 To UBound(.udtChDisp) Step 1
                    If gudt.SetSystem.udtSysSystem.shtLanguage = 1 Then
                        .udtChDisp(i).strRemarks = .udtChDisp(i).strRemarks.Replace("^", "")
                    Else
                        .udtChDisp(i).strRemarks = .udtChDisp(i).strRemarks.Replace("^", " ")
                    End If

                Next i
            End With

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetFuChannel)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "チャンネル情報"

    '--------------------------------------------------------------------
    ' 機能      : チャンネル情報保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) チャンネル情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChannel(ByVal udtSetChannel As gTypSetChInfo, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChannel
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChannel)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChannel.udtHeader, udtFileInfo.strFileVersion, gGetRecCntChannel(udtSetChannel), gCstSizeChannel, , , , gCstFnumChannel)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try

                Try
                    ''ファイル書き込み
                    FilePut(intFileNo, udtSetChannel)
                Catch ex As Exception
                    intRtn = -1
                Finally
                    FileClose(intFileNo)
                End Try

                If intRtn <> 0 Then
                    Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                    Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                    Return intRtn
                End If

                '▼▼▼ 20110214 チャンネル設定ファイルの可変長出力対応 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                Call mRemakeChannelFileSave(strFullPath)
                '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "コンポジット情報"

    '--------------------------------------------------------------------
    ' 機能      : コンポジット情報保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) コンポジット情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveComposite(ByVal udtSetComposite As gTypSetChComposite, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String, _
                                    ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathComposite
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileComposite)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetComposite.udtHeader, udtFileInfo.strFileVersion, gGetRecCntComposite(udtSetComposite), gCstSizeComposite, , , , gCstFnumComposite)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetComposite)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "グループ設定"

    '--------------------------------------------------------------------
    ' 機能      : グループ設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) グループ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveGroup(ByVal udtSetGroup As gTypSetChGroupSet, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String, _
                                ByVal blnMachinery As Boolean, _
                                ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathGroup
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileGroupM, gCstFileGroupC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetGroup.udtHeader, udtFileInfo.strFileVersion, gCstRecsGroup, gCstSizeGroup, , , , IIf(blnMachinery, gCstFnumGroupM, gCstFnumGroupC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetGroup)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "リポーズ入力設定"

    '--------------------------------------------------------------------
    ' 機能      : リポーズ入力設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) リポーズ入力設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : リポーズ入力設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveRepose(ByVal udtSetGroupRepose As gTypSetChGroupRepose, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathRepose
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileRepose)
            Dim udt48 As gTypSetChGroupRepose48 = Nothing ''2018.12.13 倉重 グループリポーズが48の場合に使用する配列
            Dim intListRow As Integer = 0       ''2018.12.13 倉重 カウンタ変数
            Dim intListDetailRow As Integer = 0 ''2018.12.13 倉重 カウンタ変数

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)



            '更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetGroupRepose.udtHeader, udtFileInfo.strFileVersion, gCstRecsRepose, gCstSizeRepose, , , , gCstFnumRepose)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                ''ADDのチェックボックスがチェックでない場合に格納する2018.12.13 倉重
                If g_bytGREPNUM = 0 Then
                    udt48.InitArray()
                    For intListRow = LBound(udt48.udtRepose) To UBound(udt48.udtRepose)
                        udt48.udtRepose(intListRow).InitArray()
                    Next

                    ''ヘッダの書き込み
                    udt48.udtHeader.strVersion = udtSetGroupRepose.udtHeader.strVersion
                    udt48.udtHeader.strDate = udtSetGroupRepose.udtHeader.strDate
                    udt48.udtHeader.strTime = udtSetGroupRepose.udtHeader.strTime
                    udt48.udtHeader.shtRecs = udtSetGroupRepose.udtHeader.shtRecs
                    udt48.udtHeader.shtSize1 = udtSetGroupRepose.udtHeader.shtSize1
                    udt48.udtHeader.shtSize2 = udtSetGroupRepose.udtHeader.shtSize2
                    udt48.udtHeader.shtSize3 = udtSetGroupRepose.udtHeader.shtSize3
                    udt48.udtHeader.shtSize4 = udtSetGroupRepose.udtHeader.shtSize4
                    udt48.udtHeader.shtSize5 = udtSetGroupRepose.udtHeader.shtSize5


                    ''リポーズデータの書き込み
                    For intListRow = LBound(udt48.udtRepose) To UBound(udt48.udtRepose)
                        ''CH ID
                        udt48.udtRepose(intListRow).shtChId = udtSetGroupRepose.udtRepose(intListRow).shtChId

                        ''データ種別コード
                        udt48.udtRepose(intListRow).shtData = udtSetGroupRepose.udtRepose(intListRow).shtData

                        For intListDetailRow = 0 To UBound(udt48.udtRepose(intListRow).udtReposeInf)

                            ''CH ID
                            udt48.udtRepose(intListRow).udtReposeInf(intListDetailRow).shtChId = udtSetGroupRepose.udtRepose(intListRow).udtReposeInf(intListDetailRow).shtChId

                            ''マスク値
                            udt48.udtRepose(intListRow).udtReposeInf(intListDetailRow).bytMask = udtSetGroupRepose.udtRepose(intListRow).udtReposeInf(intListDetailRow).bytMask

                        Next
                    Next
                    FilePut(intFileNo, udt48)
                Else
                    FilePut(intFileNo, udtSetGroupRepose)
                End If

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "出力チャンネル設定"

    '--------------------------------------------------------------------
    ' 機能      : 出力チャンネル設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 出力チャンネル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 出力チャンネル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOutPut(ByVal udtSetCHOutPut As gTypSetChOutput, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOutPut
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOutPut)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetCHOutPut.udtHeader, udtFileInfo.strFileVersion, gCstRecsOutPut, gCstSizeOutPut, , , , gCstFnumOutPut)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetCHOutPut)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "論理出力設定"

    '--------------------------------------------------------------------
    ' 機能      : 論理出力設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 論理出力設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 論理出力設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOrAnd(ByVal udtSetCHAndOr As gTypSetChAndOr, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String, _
                                ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOrAnd
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOrAnd)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetCHAndOr.udtHeader, udtFileInfo.strFileVersion, gCstRecsOrAnd, gCstSizeOrAnd, , , , gCstFnumOrAnd)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetCHAndOr)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "積算データ設定"

    '--------------------------------------------------------------------
    ' 機能      : 積算データ設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 積算データ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 積算データ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChRunHour(ByVal udtSetChRunHour As gTypSetChRunHour, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String, _
                                    ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChAdd
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChAdd)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChRunHour.udtHeader, udtFileInfo.strFileVersion, gCstRecsChAdd, gCstSizeChAdd, , , , gCstFnumChAdd)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChRunHour)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "排ガス処理演算設定"

    '--------------------------------------------------------------------
    ' 機能      : 排ガス処理演算設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 排ガス処理演算設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 排ガス処理演算設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveExhGus(ByVal udtSetExhGus As gTypSetChExhGus, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathExhGus
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileExhGus)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetExhGus.udtHeader, udtFileInfo.strFileVersion, gCstRecsExhGus, gCstSizeExhGus, , , , gCstFnumExhGus)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetExhGus)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "コントロール使用可／不可設定"

    '--------------------------------------------------------------------
    ' 機能      : コントロール使用可／不可設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) コントロール使用可／不可設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : コントロール使用可／不可設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveCtrlUseNotuse(ByVal udtSetTimer As gTypSetChCtrlUse, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean, _
                                        ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathCtrlUseNouse
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileCtrlUseNouseM, gCstFileCtrlUseNouseC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetTimer.udtHeader, udtFileInfo.strFileVersion, gCstRecsCtrlUseNouse, gCstSizeCtrlUseNouse, , , , IIf(blnMachinery, gCstFnumCtrlUseNouseM, gCstFnumCtrlUseNouseC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetTimer)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "SIO設定"

    '--------------------------------------------------------------------
    ' 機能      : SIO設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) SIO設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChSio(ByVal udtSetSio As gTypSetChSio, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String, _
                                ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSio
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSio)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSio.udtHeader, udtFileInfo.strFileVersion, gCstRecsChSio, gCstSizeChSio1, gCstSizeChSio2, , , gCstFnumChSio)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSio)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "SIO設定"

    '--------------------------------------------------------------------
    ' 機能      : SIO設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) SIO設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChSioCh(ByVal udtSetSioCh As gTypSetChSioCh, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByVal intPortNo As Integer, _
                                  ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSio
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSioChName) & Format(intPortNo, "00") & gCstFileChSioChExt

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSioCh.udtHeader, udtFileInfo.strFileVersion, gCstRecsChSioCh, gCstSizeChSioCh, , , , gCstFnumChSioChStart + (intPortNo - 1))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSioCh)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


#Region "SIO設定拡張"

    '--------------------------------------------------------------------
    ' 機能      : SIO拡張設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) SIO拡張設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChSioExt(ByVal pintKakuTbl As Integer, _
                                  ByVal udtSetSioExt As gTypSetChSioExt, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByVal intPortNo As Integer, _
                                  ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChSioExt
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChSioExtName) & Format(intPortNo, "00") & gCstFileChSioExtExt

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) And pintKakuTbl = 1 Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成しない1
            'Call gMakeHeader(udtSetSioCh.udtHeader, udtFileInfo.strFileVersion, gCstRecsChSioCh, gCstSizeChSioCh, , , , gCstFnumChSioChStart + (intPortNo - 1))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            '使用フラグが立ってない場合生成しない
            If pintKakuTbl <> 1 Then
                Return 0
            End If

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSioExt)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "延長警報設定"

    '--------------------------------------------------------------------
    ' 機能      : 延長警報設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 延長警報設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 延長警報設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveExtAlarm(ByVal udtSetExAlm As gTypSetExtAlarm, _
                                   ByVal udtFileInfo As gTypFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathExtAlarm
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileExtAlarm)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetExAlm.udtHeader, udtFileInfo.strFileVersion, gCstRecsExtAlarm, gCstSizeExtAlarm1, gCstSizeExtAlarm2, , , gCstFnumExtAlarm)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetExAlm)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "タイマ設定"

    '--------------------------------------------------------------------
    ' 機能      : タイマ設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) タイマ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : タイマ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveTimer(ByVal udtSetTimer As gTypSetExtTimerSet, _
                                ByVal udtFileInfo As gTypFileInfo, _
                                ByVal strPathBase As String, _
                                ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathTimer
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileTimer)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetTimer.udtHeader, udtFileInfo.strFileVersion, gCstRecsTimer, gCstSizeTimer, , , , gCstFnumTimer)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetTimer)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "タイマ表示名称設定"

    '--------------------------------------------------------------------
    ' 機能      : タイマ表示名称設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) タイマ表示名称設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : タイマ表示名称設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveTimerName(ByVal udtSetTimerName As gTypSetExtTimerName, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String, _
                                    ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathTimerName
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileTimerName)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetTimerName.udtHeader, udtFileInfo.strFileVersion, gCstRecsTimerName, gCstSizeTimerName, , , , gCstFnumTimerName)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetTimerName)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "シーケンスID"

    '--------------------------------------------------------------------
    ' 機能      : シーケンスID保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) シーケンスID構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : シーケンスID保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSeqSequenceID(ByVal udtSetSeqSequenceID As gTypSetSeqID, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqSequenceID
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqSequenceID)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSeqSequenceID.udtHeader, udtFileInfo.strFileVersion, gCstRecsSeqSequenceID, gCstSizeSeqSequenceID, , , , gCstFnumSeqSequenceID)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSeqSequenceID)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "シーケンス設定"

    '--------------------------------------------------------------------
    ' 機能      : シーケンス設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) シーケンス設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : シーケンス設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSeqSequenceSet(ByVal udtSetSeqSequenceSet As gTypSetSeqSet, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqSequenceSet
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqSequenceSet)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSeqSequenceSet.udtHeader, udtFileInfo.strFileVersion, gCstRecsSeqSequenceSet, gCstSizeSeqSequenceSet, , , , gCstFnumSeqSequenceSet)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSeqSequenceSet)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "リニアライズテーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : リニアライズテーブル設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) リニアライズテーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : リニアライズテーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSeqLinear(ByVal udtSetSeqLinear As gTypSetSeqLinear, _
                                    ByVal udtFileInfo As gTypFileInfo, _
                                    ByVal strPathBase As String, _
                                    ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqLinear
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqLinear)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSeqLinear.udtHeader, udtFileInfo.strFileVersion, gCstRecsSeqLinear, gCstSizeSeqLinear1, gCstSizeSeqLinear2, , , gCstFnumSeqLinear)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSeqLinear)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "演算式テーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : 演算式テーブル設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) 演算式テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : 演算式テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSeqOperationExpression(ByVal udtSetSeqOpeExp As gTypSetSeqOperationExpression, _
                                                 ByVal udtFileInfo As gTypFileInfo, _
                                                 ByVal strPathBase As String, _
                                                 ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathSeqOperationExpression
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileSeqOperationExpression)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetSeqOpeExp.udtHeader, udtFileInfo.strFileVersion, gCstRecsSeqOperationExpression, gCstSizeSeqOperationExpression, , , , gCstFnumSeqOperationExpression)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetSeqOpeExp)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "データ保存テーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : データ保存テーブル設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) データ保存テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ保存テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChDataSaveTable(ByVal udtSetChDataTable As gTypSetChDataSave, _
                                          ByVal udtFileInfo As gTypFileInfo, _
                                          ByVal strPathBase As String, _
                                          ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDataSaveTable
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDataSaveTable)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChDataTable.udtHeader, udtFileInfo.strFileVersion, gCstRecsChDataSaveTable, gCstSizeChDataSaveTable, , , , gCstFnumChDataSaveTable)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChDataTable)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "データ転送テーブル設定"

    '--------------------------------------------------------------------
    ' 機能      : データ転送テーブル設定
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : データ転送テーブル設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChDataForwardTableSet(ByVal udtSetChDataForwardTableSet As gTypSetChDataForward, _
                                                ByVal udtFileInfo As gTypFileInfo, _
                                                ByVal strPathBase As String, _
                                                ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChDataForwardTableSet
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChDataForwardTableSet)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChDataForwardTableSet.udtHeader, udtFileInfo.strFileVersion, gCstRecsChDataForwardTableSet, gCstSizeChDataForwardTableSet, , , , gCstFnumChDataForwardTableSet)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChDataForwardTableSet)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "OPSスクリーンタイトル"

    '--------------------------------------------------------------------
    ' 機能      : OPSスクリーンタイトル保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) OPSスクリーンタイトル構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : システム設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsScreenTitle(ByVal udtSetOpsScreenTitle As gTypSetOpsScreenTitle, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByVal blnMachinery As Boolean, _
                                         ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsScreenTitle
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsScreenTitleM, gCstFileOpsScreenTitleC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetOpsScreenTitle.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsScreenTitle, gCstSizeOpsScreenTitle, , , , IIf(blnMachinery, gCstFnumOpsScreenTitleM, gCstFnumOpsScreenTitleC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetOpsScreenTitle)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "プルダウンメニュー"

    '--------------------------------------------------------------------
    ' 機能      : プルダウンメニュー
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : プルダウンメニュー設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveManuMain(ByVal udtManuMain As gTypSetOpsPulldownMenu, _
                                   ByVal udtFileInfo As gTypFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByVal blnMachinery As Boolean, _
                                   ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsPulldownMenu
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsPulldownMenuM, gCstFileOpsPulldownMenuC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtManuMain.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsPulldownMenu, gCstSizeOpsPulldownMenu, , , , IIf(blnMachinery, gCstFnumOpsPulldownMenuM, gCstFnumOpsPulldownMenuC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtManuMain)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "セレクションメニュー"

    '--------------------------------------------------------------------
    ' 機能      : セレクションメニュー
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) データ転送テーブル設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : セレクションメニュー設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveSelectionMenu(ByVal udtSelectionMenu As gTypSetOpsSelectionMenu, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean, _
                                        ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsSelectionMenu
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsSelectionMenuM, gCstFileOpsSelectionMenuC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSelectionMenu.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsSelectionMenu, gCstSizeOpsSelectionMenu, , , , IIf(blnMachinery, gCstFnumOpsSelectionMenuM, gCstFnumOpsSelectionMenuC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSelectionMenu)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "OPSグラフ設定"

    '--------------------------------------------------------------------
    ' 機能      : OPSグラフ設定書込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) OPSグラフ設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : OPSグラフ設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsGraph(ByVal udtOpsGraph As gTypSetOpsGraph, _
                                   ByVal udtFileInfo As gTypFileInfo, _
                                   ByVal strPathBase As String, _
                                   ByVal blnMachinery As Boolean, _
                                   ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsGraphM, gCstFileOpsGraphC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtOpsGraph.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsGraph, , , , , IIf(blnMachinery, gCstFnumOpsGraphM, gCstFnumOpsGraphC))

            'メータ分割数自動計算 20200305 hori
            For i As Integer = 0 To UBound(udtOpsGraph.udtGraphAnalogMeterRec)                  'アナログメータのレコード範囲
                For j As Integer = 0 To UBound(udtOpsGraph.udtGraphAnalogMeterRec(i).udtDetail) 'アナログメータ設定数
                    If udtOpsGraph.udtGraphAnalogMeterRec(i).udtDetail(j).bytScale = 0 Then '分割数に"0"が入っていた場合、自動計算
                        If frmOpsGraphAnalogMaterList.fnSetScale(udtOpsGraph.udtGraphAnalogMeterRec(i).udtDetail(j)._shtChNo) > 0 Then
                            udtOpsGraph.udtGraphAnalogMeterRec(i).udtDetail(j).bytScale = frmOpsGraphAnalogMaterList.fnSetScale(udtOpsGraph.udtGraphAnalogMeterRec(i).udtDetail(j)._shtChNo)
                        End If
                    End If
                Next
            Next

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtOpsGraph)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "フリーグラフ"

    '--------------------------------------------------------------------
    ' 機能      : フリーグラフ書込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) フリーグラフ構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : フリーグラフ保存処理を行う
    '             2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
    '--------------------------------------------------------------------
    Private Function mSaveOpsFreeGraph(ByVal udtOpsFreeGraph As gTypSetOpsFreeGraph, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByVal blnMachinery As Boolean, _
                                         ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsFreeGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsFreeGraphM, gCstFileOpsFreeGraphC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtOpsFreeGraph.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsFreeGraph, gCstSizeOpsFreeGraph, , , , IIf(blnMachinery, gCstFnumOpsFreeGraphM, gCstFnumOpsFreeGraphC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtOpsFreeGraph)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


#Region "フリーディスプレイ"

    '--------------------------------------------------------------------
    ' 機能      : フリーディスプレイ書込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) フリーディスプレイ構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : フリーディスプレイ保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsFreeDisplay(ByVal udtOpsFreeDisplay As gTypSetOpsFreeDisplay, _
                                         ByVal udtFileInfo As gTypFileInfo, _
                                         ByVal strPathBase As String, _
                                         ByVal blnMachinery As Boolean, _
                                         ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsFreeDisplay
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsFreeDisplayM, gCstFileOpsFreeDisplayC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtOpsFreeDisplay.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsFreeDisplay, gCstSizeOpsFreeDisplay, , , , IIf(blnMachinery, gCstFnumOpsFreeDisplayM, gCstFnumOpsFreeDisplayC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtOpsFreeDisplay)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "トレンドグラフ"

    '--------------------------------------------------------------------
    ' 機能      : トレンドグラフ書込
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) トレンドグラフ構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : トレンドグラフ保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsTrendGraph(ByVal udtOpsTrendGraph As gTypSetOpsTrendGraph, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean, _
                                        ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsTrendGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsTrendGraphM, gCstFileOpsTrendGraphC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtOpsTrendGraph.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsTrendGraph, gCstSizeOpsTrendGraph, , , , IIf(blnMachinery, gCstFnumOpsTrendGraphM, gCstFnumOpsTrendGraphC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtOpsTrendGraph)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    'PIDﾄﾚﾝﾄﾞ専用
    Private Function mSaveOpsTrendGraph_PID(ByVal udtOpsTrendGraph As gTypSetOpsTrendGraph, _
                                        ByVal udtFileInfo As gTypFileInfo, _
                                        ByVal strPathBase As String, _
                                        ByVal blnMachinery As Boolean, _
                                        ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsTrendGraph
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOpsTrendGraphPID)
            If blnMachinery = True Then
                strCurFileName = mGetOutputFileName(udtFileInfo, gCstFileOpsTrendGraphPID)
            Else
                strCurFileName = mGetOutputFileName(udtFileInfo, gCstFileOpsTrendGraphPID2)
            End If

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            If blnMachinery = True Then
                Call gMakeHeader(udtOpsTrendGraph.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsTrendGraph, gCstSizeOpsTrendGraph, , , , gCstFnumOpsTrendGraphPID)
            Else
                Call gMakeHeader(udtOpsTrendGraph.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsTrendGraph, gCstSizeOpsTrendGraph, , , , gCstFnumOpsTrendGraphPID2)
            End If


            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtOpsTrendGraph)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function



#End Region

#Region "ログフォーマット"

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマット保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ログフォーマット構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログフォーマット保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsLogFormat(ByVal udtSetOpsLogFormat As gTypSetOpsLogFormat, _
                                       ByVal udtFileInfo As gTypFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByVal blnMachinery As Boolean, _
                                       ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogFormat
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsLogFormatM, gCstFileOpsLogFormatC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetOpsLogFormat.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsLogFormat, gCstSizeOpsLogFormat, , , , IIf(blnMachinery, gCstFnumOpsLogFormatM, gCstFnumOpsLogFormatC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetOpsLogFormat)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


#Region "ログフォーマットCHID"  '' ☆2012/10/26 K.Tanigawa

    '--------------------------------------------------------------------
    ' 機能      : ログフォーマットCHIDテーブル保存 
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ログフォーマット構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログフォーマット保存処理を行う
    ' ☆ 2012/10/26 K.Tanigawa
    '--------------------------------------------------------------------
    Private Function mSaveOpsLogIDData(ByVal udtSetOpsLogIDData As gTypSetOpsLogIdData, _
                                       ByVal udtFileInfo As gTypFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByVal blnMachinery As Boolean, _
                                       ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogIdData
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOpsLogIdDataM, gCstFileOpsLogIdDataC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成 ☆2012/10/26 K.Tanigawa
            Call gMakeHeader(udtSetOpsLogIDData.udtheader, udtFileInfo.strFileVersion, gCstRecsOpsLogIdData, gCstSizeOpsLogIdData, , , , IIf(blnMachinery, gCstFnumOpsLogIdDataM, gCstFnumOpsLogIdDataC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetOpsLogIDData)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


#Region "ログ特殊設定"  ''Ver1.9.6 2016.02.08 追加

    '--------------------------------------------------------------------
    ' 機能      : ログ特殊設定テーブル保存 
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ログフォーマット構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログ特殊設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsLogOptionSet(ByVal udtLogOption As gTypLogOption, _
                                       ByVal udtFileInfo As gTypFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByVal blnMachinery As Boolean, _
                                       ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsLogOption
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOpsLogOption)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtLogOption.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsLogOption, gCstSizeOpsLogOption, , , , gCstFnumOpsLogOption)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtLogOption)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region


#Region "GWS CH設定"      '' 2014.02.04

    '--------------------------------------------------------------------
    ' 機能      : GWS CH設定保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) SIO設定構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : SIO設定保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOpsGwsCh(ByVal udtSetGwsCh As gTypSetOpsGwsCh, _
                                  ByVal udtFileInfo As gTypFileInfo, _
                                  ByVal strPathBase As String, _
                                  ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOpsGwsCh
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileOpsGwsChName) & gCstFileOpsGwsChExt

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetGwsCh.udtHeader, udtFileInfo.strFileVersion, gCstRecsOpsGwsCh, gCstSizeOpsGwsCh, , , , gCstFnumOpsGwsChStart)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetGwsCh)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "CH変換テーブル"

    '--------------------------------------------------------------------
    ' 機能      : CH変換テーブル保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) CH変換テーブル構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : CH変換テーブル保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveChConv(ByVal udtSetChConv As gTypSetChConv, _
                                 ByVal udtFileInfo As gTypFileInfo, _
                                 ByVal strPathBase As String, _
                                 ByRef bytOutputFlg As Byte,
                                 ByRef udtSetChannel As gTypSetChInfo) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathChConv
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileChConv)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChConv.udtHeader, udtFileInfo.strFileVersion, gCstRecsChConv, gCstSizeChConv, gGetChConvPacketSize(udtSetChannel), , , gCstFnumChConv)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChConv)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "ログ印字時刻"

    '--------------------------------------------------------------------
    ' 機能      : ログ印字時刻保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ログ印字時刻構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ログ印字時刻保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveOtherLogTime(ByVal udtSetOtherLogTime As gTypSetOtherLogTime, _
                                       ByVal udtFileInfo As gTypFileInfo, _
                                       ByVal strPathBase As String, _
                                       ByVal blnMachinery As Boolean, _
                                       ByRef bytOutputFlg As Byte) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathOtherLogTime
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, IIf(blnMachinery, gCstFileOtherLogTimeM, gCstFileOtherLogTimeC))

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''更新されてなく、ファイルが存在する場合
            If bytOutputFlg = 0 And System.IO.File.Exists(strFullPath) Then
                If gCstOutputFileMsgDisplay Then Call mAddMsgText(" -Output file [" & strFullPath & "] ... not output. because it is not updated.", " -ファイル出力 [" & strFullPath & "] ... 未出力（更新なし）")
                Return 0
            End If

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetOtherLogTime.udtHeader, udtFileInfo.strFileVersion, gCstRecsOtherLogTime, gCstSizeOtherLogTime, , , , IIf(blnMachinery, gCstFnumOtherLogTimeM, gCstFnumOtherLogTimeC))

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetOtherLogTime)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

                ''出力フラグを 0 にする
                bytOutputFlg = 0

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "ファイル更新情報"

    '--------------------------------------------------------------------
    ' 機能      : ファイル更新情報保存
    ' 返り値    : 0:成功、<>0失敗
    ' 引き数    : ARG1 - (I ) ファイル更新情報構造体
    ' 　　　    : ARG2 - (I ) ファイル情報構造体
    ' 　　　    : ARG3 - (I ) ベースパス
    ' 機能説明  : ファイル更新情報保存処理を行う
    '--------------------------------------------------------------------
    Private Function mSaveEditorUpdateInfo(ByVal udtSetChDataTable As gTypSetEditorUpdateInfo, _
                                           ByVal udtFileInfo As gTypFileInfo, _
                                           ByVal strPathBase As String) As Integer

        Try

            Dim intRtn As Integer = 0
            Dim intFileNo As Integer
            Dim strPathSave As String
            Dim strFullPath As String
            Dim strCurPathName As String = gCstPathEditorUpdateInfo
            Dim strCurFileName As String = mGetOutputFileName(udtFileInfo, gCstFileEditorUpdateInfo)

            ''保存パスを作成
            strPathSave = System.IO.Path.Combine(strPathBase, strCurPathName)
            strFullPath = System.IO.Path.Combine(strPathSave, strCurFileName)

            ''フォルダ作成
            If gMakeFolder(strPathSave) <> 0 Then
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -It failed in making the folder. [Path]" & strPathBase, "  -フォルダの作成に失敗しました。[Path]" & strPathBase)
                Return -1
            End If

            ''ヘッダレコード作成
            Call gMakeHeader(udtSetChDataTable.udtHeader, udtFileInfo.strFileVersion, gCstRecsEditorUpdateInfo, gCstSizeEditorUpdateInfo, , , , gCstFnumEditorUpdateInfo)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''ファイルオープン
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.Shared)

            Try
                ''ファイル書き込み
                FilePut(intFileNo, udtSetChDataTable)

                ''メッセージ出力
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Success", " -ファイル出力 [" & strFullPath & "] ... OK")

            Catch ex As Exception
                Call mAddMsgText(" -Output file [" & strFullPath & "] ... Failure", " -ファイル出力 [" & strFullPath & "] ... エラー")
                Call mAddMsgText("  -" & Err.Description, "  -" & Err.Description)
                intRtn = -1
            Finally
                FileClose(intFileNo)
            End Try

            Return intRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "デフォルトデータ"

    Private Function mCopyDefaultData(ByVal strPathBase As String) As Integer

        Dim intRtn As Integer = 0

        Try

            Dim strPathDefaultData As String

            ''デフォルトデータ保存フォルダのパスを取得
            strPathDefaultData = gGetDirNameDefaultData()

            ''デフォルトデータを出力フォルダにコピー
            If gCopyDirectory(strPathDefaultData, strPathBase) <> 0 Then
                Call mAddMsgText(" -Default Data Copy ... Failure", " デフォルトデータコピー ... エラー")
                Call mAddMsgText("  -[Default Data] folder copy error. ", "  -[Default Data] フォルダのコピーに失敗しました。 ")
                intRtn = -1
            Else

                ''ログ印字設定（logging1.cnf、logging2.cnf）の操作を行う
                'If mChangeLogTimeFile(strPathBase) <> 0 Then
                '    Call mAddMsgText(" -Default Data Copy ... Failure", " デフォルトデータコピー ... エラー")
                '    Call mAddMsgText("  -[logging.cnf] operation error. ", "  -[logging.cnf] の操作に失敗しました。")
                '    intRtn = -1
                'Else

                '    ''メッセージ出力
                '    Call mAddMsgText(" -Default Data Copy ... Success", " -デフォルトデータコピー ... ... OK")

                'End If

                ''ログ印字設定（logging1.cnf、logging2.cnf）の操作を行う   '' Ver1.11.8.1 2016.10.28 関数に変更
                If (mCopyLogDefaultData(strPathBase, False) = -1) Then
                    intRtn = -1
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            intRtn = -1
        End Try

        Return intRtn

    End Function

    '' Ver1.11.8.1 2016.10.28  ﾛｸﾞﾃﾞｰﾀｺﾋﾟｰ処理分離
    Private Function mCopyLogDefaultData(ByVal strPathBase As String, ByVal bCopyFlg As Boolean) As Integer

        Dim intRtn As Integer = 0
        Dim strPathDefaultData As String
        Dim strTempLog As String

        Try
            If bCopyFlg Then        '' Defaultｺﾋﾟｰが必要な場合
                strPathDefaultData = gGetDirNameDefaultData()

                ''デフォルトデータを出力フォルダにコピー
                strPathDefaultData = strPathDefaultData + "\Log"
                strTempLog = strPathBase + "\Log"
                If gCopyDirectory(strPathDefaultData, strTempLog) <> 0 Then
                    Call mAddMsgText(" -Default Data Copy ... Failure", " デフォルトデータコピー ... エラー")
                    Call mAddMsgText("  -[Default Data] folder copy error. ", "  -[Default Data] フォルダのコピーに失敗しました。 ")
                    intRtn = -1
                    Return intRtn
                End If

            End If

            ''ログ印字設定（logging1.cnf、logging2.cnf）の操作を行う
            If mChangeLogTimeFile(strPathBase) <> 0 Then
                Call mAddMsgText(" -Default Data Copy ... Failure", " デフォルトデータコピー ... エラー")
                Call mAddMsgText("  -[logging.cnf] operation error. ", "  -[logging.cnf] の操作に失敗しました。")
                intRtn = -1
            Else

                ''メッセージ出力
                Call mAddMsgText(" -Default Data Copy ... Success", " -デフォルトデータコピー ... ... OK")

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            intRtn = -1
        End Try

        Return intRtn

    End Function

    Private Function mChangeLogTimeFile(ByVal strPathBase As String) As Integer

        Dim intRtn As Integer = 0

        Try

            ''デフォルトデータの log フォルダから下記４ファイルがコピーされる。

            ''logging1_Set.cnf      プリンタあり時のマシナリ用ファイル
            ''logging2_Set.cnf      プリンタあり時のカーゴ用ファイル
            ''logging1_Unset.cnf    プリンタなし時のマシナリ用ファイル
            ''logging2_Unset.cnf    プリンタなし時のカーゴ用ファイル
            ''logging1_Set_Opt1.cnf プリンタオプションありのマシナリ用ファイル       '' Ver1.11.2 2016.08.01 追加
            ''logging2_Set_Opt1.cnf プリンタオプションあり時のカーゴ用ファイル       '' Ver1.11.2 2016.08.01 追加

            ''プリンタ設定状態を参照して上記４ファイルから必要なファイルの名前を
            ''logging1.cnf、logging2.cnfに変更して、不要なファイルは削除する

            Dim blnMachinery As Boolean = False
            Dim blnCargo As Boolean = False
            Dim strCurPath As String = System.IO.Path.Combine(strPathBase, gCstPathOtherLogTime)

            Dim strFileLogTimeM As String = System.IO.Path.Combine(strCurPath, gCstFileOtherLogTimeM)
            Dim strFileLogTimeC As String = System.IO.Path.Combine(strCurPath, gCstFileOtherLogTimeC)
            Dim strFileLogTimeM_Set As String = System.IO.Path.Combine(strCurPath, gCstFileOtherLogTimeM_Set)
            Dim strFileLogTimeC_Set As String = System.IO.Path.Combine(strCurPath, gCstFileOtherLogTimeC_Set)
            Dim strFileLogTimeM_Unset As String = System.IO.Path.Combine(strCurPath, gCstFileOtherLogTimeM_Unset)
            Dim strFileLogTimeC_Unset As String = System.IO.Path.Combine(strCurPath, gCstFileOtherLogTimeC_Unset)
            Dim strFileLogTimeM_Set_Opt1 As String = System.IO.Path.Combine(strCurPath, gCstFileOtherLogTimeM_Set_Opt1)     '' Ver1.11.2 2016.08.02
            Dim strFileLogTimeC_Set_Opt1 As String = System.IO.Path.Combine(strCurPath, gCstFileOtherLogTimeC_Set_Opt1)     '' Ver1.11.2 2016.08.02

            '' Ver1.11.2 2016.08.02  ﾃﾞﾌｫﾙﾄﾌｧｲﾙを作成するためｺﾒﾝﾄ
            '' Ver1.10.2 2016.03.03  ﾛｸﾞｵﾌﾟｼｮﾝありの場合、既にlogging1.cnfﾌｧｲﾙが存在していればﾃﾞﾌｫﾙﾄﾃﾞｰﾀはｺﾋﾟｰしない
            ''If gudt.SetSystem.udtSysPrinter.shtLogDrawNo Then
            ''    If System.IO.File.Exists(strFileLogTimeM) Then
            ''        If System.IO.File.Exists(strFileLogTimeM_Set) Then Call System.IO.File.Delete(strFileLogTimeM_Set)
            ''        If System.IO.File.Exists(strFileLogTimeC_Set) Then Call System.IO.File.Delete(strFileLogTimeC_Set)
            ''        If System.IO.File.Exists(strFileLogTimeM_Unset) Then Call System.IO.File.Delete(strFileLogTimeM_Unset)
            ''        If System.IO.File.Exists(strFileLogTimeC_Unset) Then Call System.IO.File.Delete(strFileLogTimeC_Unset)

            ''        Return intRtn
            ''    End If
            ''End If
            ''//

            ''プリンタ使用設定になっているかチェック
            If mChkPrinterUse(gudt.SetSystem.udtSysPrinter, blnMachinery, blnCargo) Then
                '=========================
                ''プリンタ使用ありの場合
                '=========================
                '' Ver1.11.2 2016.08.02  ﾃﾞﾌｫﾙﾄﾌｧｲﾙを作成するためｺﾒﾝﾄ
                ''前回のファイルが残っている場合は削除する
                If System.IO.File.Exists(strFileLogTimeM) Then Call System.IO.File.Delete(strFileLogTimeM)
                If System.IO.File.Exists(strFileLogTimeC) Then Call System.IO.File.Delete(strFileLogTimeC)
                ''


                ''マシナリで使用のフラグが立っている場合
                If blnMachinery Then

                    'If gudt.SetSystem.udtSysPrinter.shtLogDrawNo = 1 Then       '' Ver1.11.2 2016.08.02
                    If gudt.SetSystem.udtSysPrinter.shtLogDrawNo <> 0 Then       '' Ver1.11.8.1 2016.10.27  設定が入っている場合に変更
                        '' Ver1.11.8.1 2016.10.28 ﾌｧｲﾙ削除追加
                        If System.IO.File.Exists(strFileLogTimeM_Unset) Then Call System.IO.File.Delete(strFileLogTimeM_Unset)
                        If System.IO.File.Exists(strFileLogTimeM_Set) Then Call System.IO.File.Delete(strFileLogTimeM_Set)
                        '
                        If System.IO.File.Exists(strFileLogTimeM_Set_Opt1) Then Call System.IO.File.Move(strFileLogTimeM_Set_Opt1, strFileLogTimeM)
                    Else
                        ''プリンタ使用なしのファイルを削除して、プリンタ設定ありのファイルをリネームする
                        If System.IO.File.Exists(strFileLogTimeM_Unset) Then Call System.IO.File.Delete(strFileLogTimeM_Unset)
                        If System.IO.File.Exists(strFileLogTimeM_Set) Then Call System.IO.File.Move(strFileLogTimeM_Set, strFileLogTimeM)
                    End If

                Else
                    If System.IO.File.Exists(strFileLogTimeM_Set_Opt1) Then Call System.IO.File.Delete(strFileLogTimeM_Set_Opt1)

                    ''プリンタ使用ありのファイルを削除して、プリンタ設定なしのファイルをリネームする
                    If System.IO.File.Exists(strFileLogTimeM_Set) Then Call System.IO.File.Delete(strFileLogTimeM_Set)
                    If System.IO.File.Exists(strFileLogTimeM_Unset) Then Call System.IO.File.Move(strFileLogTimeM_Unset, strFileLogTimeM)

                End If

                ''カーゴで使用のフラグが立っている場合
                If blnCargo Then

                    'If gudt.SetSystem.udtSysPrinter.shtLogDrawNo = 1 Then       '' Ver1.11.2 2016.08.02
                    If gudt.SetSystem.udtSysPrinter.shtLogDrawNo <> 0 Then       '' Ver1.11.8.1 2016.10.27  設定が入っている場合に変更
                        '' Ver1.11.8.1 2016.10.28 ﾌｧｲﾙ削除追加
                        If System.IO.File.Exists(strFileLogTimeC_Unset) Then Call System.IO.File.Delete(strFileLogTimeC_Unset)
                        If System.IO.File.Exists(strFileLogTimeC_Set) Then Call System.IO.File.Delete(strFileLogTimeC_Set)
                        '
                        If System.IO.File.Exists(strFileLogTimeC_Set_Opt1) Then Call System.IO.File.Move(strFileLogTimeC_Set_Opt1, strFileLogTimeC)
                    Else
                        ''プリンタ使用なしのファイルを削除して、プリンタ設定ありのファイルをリネームする
                        ''If System.IO.File.Exists(strFileLogTimeC_Unset) Then Call System.IO.File.Delete(strFileLogTimeC_Unset)
                        If System.IO.File.Exists(strFileLogTimeC_Set) Then Call System.IO.File.Move(strFileLogTimeC_Set, strFileLogTimeC)
                    End If

                Else
                    If System.IO.File.Exists(strFileLogTimeC_Set_Opt1) Then Call System.IO.File.Delete(strFileLogTimeC_Set_Opt1)

                    ''プリンタ使用ありのファイルを削除して、プリンタ設定なしのファイルをリネームする
                    If System.IO.File.Exists(strFileLogTimeC_Set) Then Call System.IO.File.Delete(strFileLogTimeC_Set)
                    If System.IO.File.Exists(strFileLogTimeC_Unset) Then Call System.IO.File.Move(strFileLogTimeC_Unset, strFileLogTimeC)

                End If

            Else

                '=========================
                ''プリンタ使用なしの場合
                '=========================
                '' Ver1.11.2 2016.08.02  ﾃﾞﾌｫﾙﾄﾌｧｲﾙを作成するためｺﾒﾝﾄ
                ''前回のファイルが残っている場合は削除する
                If System.IO.File.Exists(strFileLogTimeM) Then Call System.IO.File.Delete(strFileLogTimeM)
                If System.IO.File.Exists(strFileLogTimeC) Then Call System.IO.File.Delete(strFileLogTimeC)
                ''

                ''プリンタ使用ありのファイルは削除する
                If System.IO.File.Exists(strFileLogTimeM_Set) Then Call System.IO.File.Delete(strFileLogTimeM_Set)
                If System.IO.File.Exists(strFileLogTimeC_Set) Then Call System.IO.File.Delete(strFileLogTimeC_Set)

                ''プリンタ使用なしのファイルをリネームする
                If System.IO.File.Exists(strFileLogTimeM_Unset) Then Call System.IO.File.Move(strFileLogTimeM_Unset, strFileLogTimeM)
                If System.IO.File.Exists(strFileLogTimeC_Unset) Then Call System.IO.File.Move(strFileLogTimeC_Unset, strFileLogTimeC)

            End If

            '' Ver1.11.2 2016.08.02  ﾛｸﾞｵﾌﾟｼｮﾝが存在しなければﾃﾞｰﾀは削除する
            'If gudt.SetSystem.udtSysPrinter.shtLogDrawNo <> 1 Then
            If gudt.SetSystem.udtSysPrinter.shtLogDrawNo = 0 Then      '' Ver1.11.8.1 2016.10.27  変更
                If System.IO.File.Exists(strFileLogTimeM_Set_Opt1) Then Call System.IO.File.Delete(strFileLogTimeM_Set_Opt1)
                If System.IO.File.Exists(strFileLogTimeC_Set_Opt1) Then Call System.IO.File.Delete(strFileLogTimeC_Set_Opt1)
            End If
            ''

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            intRtn = -1
        End Try

        Return intRtn

    End Function
    'ファイルバイナリレベル比較関数
    Private Function BinComp(ByVal f1 As String, ByVal f2 As String) As Boolean
        Dim fi1 As New System.IO.FileInfo(f1)
        Dim fi2 As New System.IO.FileInfo(f2)

        ' ファイルサイズの比較
        If fi1.Length <> fi2.Length Then Return False

        Dim sr1 As System.IO.Stream = Nothing
        Dim sr2 As System.IO.Stream = Nothing
        Try
            ' バイナリファイルの読込
            sr1 = System.IO.File.Open(f1, System.IO.FileMode.Open, System.IO.FileAccess.Read)
            sr2 = System.IO.File.Open(f2, System.IO.FileMode.Open, System.IO.FileAccess.Read)

            ' バイナリファイルの1バイト比較
            Dim b1 As Integer = -1
            Dim b2 As Integer = -1
            Do
                b1 = sr1.ReadByte()
                b2 = sr2.ReadByte()
                If b1 <> b2 Then Return False
                If b1 = -1 Then Exit Do
            Loop While (b1 = b2)

        Catch ex As Exception
            Throw ex
        Finally
            ' バイナリファイルクローズ
            If sr1 Is Nothing = False Then sr1.Close()
            If sr2 Is Nothing = False Then sr2.Close()
        End Try
        Return True
    End Function



    Private Function mChkPrinterUse(ByVal udtSysPrinter As gTypSetSysPrinter, _
                                    ByRef blnMachinery As Boolean, _
                                    ByRef blnCargo As Boolean) As Boolean

        ''ログプリンタ１、２とアラームプリンタ１、２でどれか一つでも
        ''プリンタなし以外だったらプリンタ使用設定あり

        Dim blnRtn As Boolean = False

        Try

            ''ログプリンタ１
            With udtSysPrinter.udtPrinterDetail(0)

                If .bytPrinter <> 0 Then
                    blnRtn = True
                    If gBitCheck(.shtPart, 0) Then blnMachinery = True
                    If gBitCheck(.shtPart, 1) Then blnCargo = True
                End If

            End With

            ''ログプリンタ２
            With udtSysPrinter.udtPrinterDetail(1)

                If .bytPrinter <> 0 Then
                    blnRtn = True
                    If gBitCheck(.shtPart, 0) Then blnMachinery = True
                    If gBitCheck(.shtPart, 1) Then blnCargo = True
                End If

            End With

            ''アラームプリンタ１
            With udtSysPrinter.udtPrinterDetail(2)

                If .bytPrinter <> 0 Then
                    blnRtn = True
                    If gBitCheck(.shtPart, 0) Then blnMachinery = True
                    If gBitCheck(.shtPart, 1) Then blnCargo = True
                End If

            End With

            ''アラームプリンタ２
            With udtSysPrinter.udtPrinterDetail(3)

                If .bytPrinter <> 0 Then
                    blnRtn = True
                    If gBitCheck(.shtPart, 0) Then blnMachinery = True
                    If gBitCheck(.shtPart, 1) Then blnCargo = True
                End If

            End With

            'プリンタ有りで、パートフラグがない場合は強制でマシナリパートとする  2013.11.08
            If blnRtn = True Then
                If blnMachinery = False And blnCargo = False Then
                    blnMachinery = True
                End If
            End If

            Return blnRtn

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#End Region

#End Region

#Region "構造体保存"

    '--------------------------------------------------------------------
    ' 機能      : 構造体保存
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 現在の構造体をモジュール変数にDeepCopyで保存する
    '--------------------------------------------------------------------
    Private Sub mSaveStructure()

        Try

            ''システム設定
            mudtSetSystem = DeepCopyHelper.DeepCopy(gudt.SetSystem)
            Call mAddMsgText(" -System Set ... Success", " -システム設定 ... OK")
            If mblnCancelFlg Then Return

            ''FU設定
            mudtSetFuChannel = DeepCopyHelper.DeepCopy(gudt.SetFu)
            Call mAddMsgText(" -FCU Set ... Success", " -FU設定 ... OK")
            If mblnCancelFlg Then Return

            ''チャンネル情報データ（表示名設定データ）
            mudtSetChannelDisp = DeepCopyHelper.DeepCopy(gudt.SetChDisp)
            gudt.SetChDispPrev = DeepCopyHelper.DeepCopy(gudt.SetChDisp)
            Call mAddMsgText(" -Channel Disp ... Success", " -チャンネル表示名設定データ ... OK")
            If mblnCancelFlg Then Return

            ''チャンネル情報
            mudtSetChannel = DeepCopyHelper.DeepCopy(gudt.SetChInfo)
            gudt.SetChInfoPrev = DeepCopyHelper.DeepCopy(gudt.SetChInfo)
            Call mAddMsgText(" -Channel Set ... Success", " -チャンネル情報 ... OK")
            If mblnCancelFlg Then Return

            ''コンポジット設定
            mudtSetComposite = DeepCopyHelper.DeepCopy(gudt.SetChComposite)
            gudt.SetChCompositePrev = DeepCopyHelper.DeepCopy(gudt.SetChComposite)
            Call mAddMsgText(" -Composite Set ... Success", " -コンポジット設定 ... OK")
            If mblnCancelFlg Then Return

            ''出力チャンネル設定
            mudtSetChOutput = DeepCopyHelper.DeepCopy(gudt.SetChOutput)
            gudt.SetChOutputPrev = DeepCopyHelper.DeepCopy(gudt.SetChOutput)
            Call mAddMsgText(" -Channel Output Set ... Success", " -出力チャンネル設定 ... OK")
            If mblnCancelFlg Then Return

            ''論理出力設定
            mudtSetChAndOr = DeepCopyHelper.DeepCopy(gudt.SetChAndOr)
            gudt.SetChAndOrPrev = DeepCopyHelper.DeepCopy(gudt.SetChAndOr)
            Call mAddMsgText(" -Channel AndOr Set ... Success", " -論理出力設定 ... OK")
            If mblnCancelFlg Then Return

            ''グループ設定
            mudtSetGroupM = DeepCopyHelper.DeepCopy(gudt.SetChGroupSetM)
            mudtSetGroupC = DeepCopyHelper.DeepCopy(gudt.SetChGroupSetC)
            Call mAddMsgText(" -Group Set ... Success", " -グループ設定 ... OK")
            If mblnCancelFlg Then Return

            ''リポーズ入力設定
            mudtSetRepose = DeepCopyHelper.DeepCopy(gudt.SetChGroupRepose)
            gudt.SetChGroupReposePrev = DeepCopyHelper.DeepCopy(gudt.SetChGroupRepose)

            Call mAddMsgText(" -Repose Set ... Success", " -リポーズ入力設定 ... OK")
            If mblnCancelFlg Then Return

            ''積算データ設定ファイル
            mudtSetChRunHour = DeepCopyHelper.DeepCopy(gudt.SetChRunHour)
            gudt.SetChRunHourPrev = DeepCopyHelper.DeepCopy(gudt.SetChRunHour)
            Call mAddMsgText(" -Run Hour Set ... Success", " -積算データ設定ファイル ... OK")
            If mblnCancelFlg Then Return

            ''排ガス演算処理設定
            mudtSetExhGus = DeepCopyHelper.DeepCopy(gudt.SetChExhGus)
            gudt.SetChExhGusPrev = DeepCopyHelper.DeepCopy(gudt.SetChExhGus)
            Call mAddMsgText(" -Exh Gas Set ... Success", " -排ガス演算処理設定 ... OK")
            If mblnCancelFlg Then Return

            ''コントロール使用可／不可設定
            mudtSetCtrlUseNotuseM = DeepCopyHelper.DeepCopy(gudt.SetChCtrlUseM)
            mudtSetCtrlUseNotuseC = DeepCopyHelper.DeepCopy(gudt.SetChCtrlUseC)
            Call mAddMsgText(" -Control Use/NotUse set... Success", " -コントロール使用可／不可設定 ... OK")
            If mblnCancelFlg Then Return

            ''SIO設定
            mudtSetSIO = DeepCopyHelper.DeepCopy(gudt.SetChSio)
            Call mAddMsgText(" -SIO set... Success", " -SIO設定 ... OK")
            If mblnCancelFlg Then Return

            ''SIO通信チャンネル設定
            ReDim mudtSetSIOCh(gCstCntChSioPort - 1)

            gudt.SetChSioChPrev = DeepCopyHelper.DeepCopy(gudt.SetChSioCh)

            For i As Integer = 0 To UBound(gudt.SetChSioCh)
                mudtSetSIOCh(i) = DeepCopyHelper.DeepCopy(gudt.SetChSioCh(i))
                'gudt.SetChSioChPrev(i) = DeepCopyHelper.DeepCopy(gudt.SetChSioCh(i))
                If i < 14 Then
                    Call mAddMsgText(" -SIO CH Port " & i + 1 & " set... Success", " -SIO通信チャンネル設定 ポート" & i + 1 & " ... OK")
                Else
                    Call mAddMsgText(" -EXT LAN CH Port " & (i - 14) + 1 & " set... Success", " -EXT LAN通信チャンネル設定 ポート" & (i - 14) + 1 & " ... OK")
                End If
                If mblnCancelFlg Then Return
            Next

            'SIO通信拡張設定
            ReDim mudtSetSIOExt(gCstCntChSioVDRPort - 1)
            For i As Integer = 0 To UBound(gudt.SetChSioExt)
                mudtSetSIOExt(i) = DeepCopyHelper.DeepCopy(gudt.SetChSioExt(i))
                Call mAddMsgText(" -SIO Ext Port " & i + 1 & " set... Success", " -SIO通信拡張設定 ポート" & i + 1 & " ... OK")
                If mblnCancelFlg Then Return
            Next

            ''データ保存テーブル
            mudtSetChDataSaveTable = DeepCopyHelper.DeepCopy(gudt.SetChDataSave)
            gudt.SetChDataSavePrev = DeepCopyHelper.DeepCopy(gudt.SetChDataSave)
            Call mAddMsgText(" -Data Save Table ... Success", " -データ保存テーブル ... OK")
            If mblnCancelFlg Then Return

            ''データ転送テーブル設定
            mudtSetChDataForwardTableSet = DeepCopyHelper.DeepCopy(gudt.SetChDataForward)
            Call mAddMsgText(" -Data Forward Table ... Success", " -データ転送テーブル設定 ... OK")
            If mblnCancelFlg Then Return

            ''延長警報設定
            mudtSetExtAlarm = DeepCopyHelper.DeepCopy(gudt.SetExtAlarm)
            Call mAddMsgText(" -Ext Alarm Set ... Success", " -延長警報設定 ... OK")
            If mblnCancelFlg Then Return

            ''タイマ設定
            mudtSetTimer = DeepCopyHelper.DeepCopy(gudt.SetExtTimerSet)
            Call mAddMsgText(" -Timer Set ... Success", " -タイマ設定 ... OK")
            If mblnCancelFlg Then Return

            ''タイマ表示名称設定
            mudtSetTimerName = DeepCopyHelper.DeepCopy(gudt.SetExtTimerName)
            Call mAddMsgText(" -Timer Name Set ... Success", " -タイマ表示名称設定 ... OK")
            If mblnCancelFlg Then Return

            ''シーケンスID
            mudtSetSeqSequenceID = DeepCopyHelper.DeepCopy(gudt.SetSeqID)
            Call mAddMsgText(" -Sequence ID Set ... Success", " -シーケンスID ... OK")
            If mblnCancelFlg Then Return

            ''シーケンス設定
            mudtSetSeqSequenceSet = DeepCopyHelper.DeepCopy(gudt.SetSeqSet)
            gudt.SetSeqSetPrev = DeepCopyHelper.DeepCopy(gudt.SetSeqSet)
            Call mAddMsgText(" -Sequence Set ... Success", " -シーケンス設定 ... OK")
            If mblnCancelFlg Then Return

            ''リニアライズテーブル
            mudtSetSeqLinear = DeepCopyHelper.DeepCopy(gudt.SetSeqLinear)
            Call mAddMsgText(" -Linear Set ... Success", " -リニアライズテーブル ... OK")
            If mblnCancelFlg Then Return

            ''演算式テーブル
            mudtSetSeqOpeExp = DeepCopyHelper.DeepCopy(gudt.SetSeqOpeExp)
            gudt.SetSeqOpeExpPrev = DeepCopyHelper.DeepCopy(gudt.SetSeqOpeExp)
            Call mAddMsgText(" -Operation Fixed Number Set ... Success", " -演算式テーブル ... OK")
            If mblnCancelFlg Then Return

            ''OPS画面タイトル
            mudtSetOpsScreenTitleM = DeepCopyHelper.DeepCopy(gudt.SetOpsScreenTitleM)
            mudtSetOpsScreenTitleC = DeepCopyHelper.DeepCopy(gudt.SetOpsScreenTitleC)
            Call mAddMsgText(" -OPS Screen Title Set ... Success", " -OPS画面タイトル ... OK")
            If mblnCancelFlg Then Return

            ''プルダウンメニュー
            mudtSetOpsPulldownMenuM = DeepCopyHelper.DeepCopy(gudt.SetOpsPulldownMenuM)
            mudtSetOpsPulldownMenuC = DeepCopyHelper.DeepCopy(gudt.SetOpsPulldownMenuC)
            Call mAddMsgText(" -OPS Pulldown Menu Set ... Success", " -プルダウンメニュー ... OK")
            If mblnCancelFlg Then Return

            ''OPSグラフ設定
            mudtSetOpsGraphM = DeepCopyHelper.DeepCopy(gudt.SetOpsGraphM)
            mudtSetOpsGraphC = DeepCopyHelper.DeepCopy(gudt.SetOpsGraphC)
            Call mAddMsgText(" -OPS Graph Set ... Success", " -OPSグラフ設定 ... OK")
            If mblnCancelFlg Then Return

            ''ログフォーマット
            mudtSetOpsLogFormatM = DeepCopyHelper.DeepCopy(gudt.SetOpsLogFormatM)
            mudtSetOpsLogFormatC = DeepCopyHelper.DeepCopy(gudt.SetOpsLogFormatC)

            gudt.SetOpsLogFormatMPrev = DeepCopyHelper.DeepCopy(gudt.SetOpsLogFormatM)
            gudt.SetOpsLogFormatCPrev = DeepCopyHelper.DeepCopy(gudt.SetOpsLogFormatC)

            mudtSetOpsLogIdDataM = DeepCopyHelper.DeepCopy(gudt.SetOpsLogIdDataM)  ''ログフォーマットCHID ☆2012/10/26 K.Tanigawa
            mudtSetOpsLogIdDataC = DeepCopyHelper.DeepCopy(gudt.SetOpsLogIdDataC)  ''ログフォーマットCHID ☆2012/10/26 K.Tanigawa
            Call mAddMsgText(" -OPS Log Format Set ... Success", " -ログフォーマット ... OK")
            If mblnCancelFlg Then Return

            'PID用ﾄﾚﾝﾄﾞ設定
            mudtSetOpsTrendGraphPID = DeepCopyHelper.DeepCopy(gudt.SetOpsTrendGraphPID)
            gudt.SetOpsTrendGraphPIDprev = DeepCopyHelper.DeepCopy(gudt.SetOpsTrendGraphPID)
            Call mAddMsgText(" -OPS PID TREND Graph Set ... Success", " -PIDグラフ設定 ... OK")
            If mblnCancelFlg Then Return


            ''GWS通信チャンネル設定
            ReDim mudtSetOpsGwsCh.udtGwsFileRec(gCstCntOpsGwsPort - 1)
            For i As Integer = 0 To UBound(gudt.SetOpsGwsCh.udtGwsFileRec)
                ReDim mudtSetOpsGwsCh.udtGwsFileRec(i).udtGwsChRec(2999)
            Next
            mudtSetOpsGwsCh = DeepCopyHelper.DeepCopy(gudt.SetOpsGwsCh)
            gudt.SetOpsGwsChPrev = DeepCopyHelper.DeepCopy(gudt.SetOpsGwsCh)
            Call mAddMsgText(" -GWS CH Set " & " set... Success", " -GWS通信チャンネル設定" & " ... OK")
            If mblnCancelFlg Then Return

            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "構造体戻し"

    '--------------------------------------------------------------------
    ' 機能      : 構造体戻し
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) なし
    ' 機能説明  : 保存したモジュール変数の構造体をグローバル構造体に戻す
    '--------------------------------------------------------------------
    Private Sub mLoadStructure()

        Try

            ''チャンネル情報はこの前のID並べ替え後のデータが必要なので構造体ごと戻せない
            ''但し、共通項目にある運転積算トリガCHIDは戻す必要があるのでここで処理する
            For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)
                If gudt.SetChInfo.udtChannel(i).udtChCommon.shtChType = gCstCodeChTypePulse Then
                    For j As Integer = 0 To UBound(mudtSetChannel.udtChannel)
                        If gudt.SetChInfo.udtChannel(i).udtChCommon.shtChno = mudtSetChannel.udtChannel(j).udtChCommon.shtChno Then
                            gudt.SetChInfo.udtChannel(i).RevoTrigerChid = mudtSetChannel.udtChannel(j).RevoTrigerChid
                        End If
                    Next
                End If
            Next

            'For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)
            '    gudt.SetChInfo.udtChannel(i).RevoTrigerChid = mudtSetChannel.udtChannel(i).RevoTrigerChid
            'Next


            gudt.SetSystem = mudtSetSystem                                                       ''システム設定
            gudt.SetFu = mudtSetFuChannel                                                        ''FU設定
            gudt.SetChDisp = mudtSetChannelDisp                                                  ''チャンネル情報データ（表示名設定データ）
            gudt.SetChComposite = mudtSetComposite                                               ''コンポジット設定
            gudt.SetChOutput = mudtSetChOutput                                                   ''出力チャンネル設定
            gudt.SetChAndOr = mudtSetChAndOr                                                     ''論理出力設定
            gudt.SetChGroupSetM = mudtSetGroupM                                                  ''グループ設定
            gudt.SetChGroupSetC = mudtSetGroupC                                                  ''グループ設定
            gudt.SetChGroupRepose = mudtSetRepose                                                ''リポーズ入力設定
            gudt.SetChRunHour = mudtSetChRunHour                                                 ''積算データ設定ファイル
            gudt.SetChExhGus = mudtSetExhGus                                                     ''排ガス演算処理設定
            gudt.SetChCtrlUseM = mudtSetCtrlUseNotuseM                                           ''コントロール使用可／不可設定
            gudt.SetChCtrlUseC = mudtSetCtrlUseNotuseC                                           ''コントロール使用可／不可設定
            gudt.SetChSio = mudtSetSIO                                                           ''SIO設定
            gudt.SetChSioCh = mudtSetSIOCh                                                       ''SIO通信チャンネル設定
            gudt.SetChSioExt = mudtSetSIOext                                                     'SIO通信拡張設定
            gudt.SetChDataSave = mudtSetChDataSaveTable                                          ''データ保存テーブル
            gudt.SetChDataForward = mudtSetChDataForwardTableSet                                 ''データ転送テーブル設定
            gudt.SetExtAlarm = mudtSetExtAlarm                                                   ''延長警報設定
            gudt.SetExtTimerSet = mudtSetTimer                                                   ''タイマ設定
            gudt.SetExtTimerName = mudtSetTimerName                                              ''タイマ表示名称設定
            gudt.SetSeqID = mudtSetSeqSequenceID                                                 ''シーケンスID
            gudt.SetSeqSet = mudtSetSeqSequenceSet                                               ''シーケンス設定
            gudt.SetSeqLinear = mudtSetSeqLinear                                                 ''リニアライズテーブル
            gudt.SetSeqOpeExp = mudtSetSeqOpeExp                                                 ''演算式テーブル
            gudt.SetOpsScreenTitleM = mudtSetOpsScreenTitleM                                     ''OPS画面タイトル
            gudt.SetOpsScreenTitleC = mudtSetOpsScreenTitleC                                     ''OPS画面タイトル
            gudt.SetOpsPulldownMenuM = mudtSetOpsPulldownMenuM                                   ''プルダウンメニュー
            gudt.SetOpsPulldownMenuC = mudtSetOpsPulldownMenuC                                   ''プルダウンメニュー
            gudt.SetOpsGraphM = mudtSetOpsGraphM                                                 ''OPSグラフ設定
            gudt.SetOpsGraphC = mudtSetOpsGraphC                                                 ''OPSグラフ設定
            gudt.SetOpsLogFormatM = mudtSetOpsLogFormatM                                         ''ログフォーマット
            gudt.SetOpsLogFormatC = mudtSetOpsLogFormatC                                         ''ログフォーマット
            gudt.SetOpsLogIdDataM = mudtSetOpsLogIdDataM                                         ''ログフォーマットCHID ☆2012/10/26 K.Tanigawa
            gudt.SetOpsLogIdDataC = mudtSetOpsLogIdDataC                                         ''ログフォーマットCHID ☆2012/10/26 K.Tanigawa
            gudt.SetOpsGwsCh = mudtSetOpsGwsCh                                                   ''GWS通信CH

            gudt.SetOpsTrendGraphPID = mudtSetOpsTrendGraphPID


            ''構造体保存時にDeepCopyしているので構造体戻し時のDeepCopyは必要なし（時間かかる）
            'gudt.SetSystem = DeepCopyHelper.DeepCopy(mudtSetSystem)                             ''システム設定
            'gudt.SetFu = DeepCopyHelper.DeepCopy(mudtSetFuChannel)                              ''FU設定
            'gudt.SetChDisp = DeepCopyHelper.DeepCopy(mudtSetChannelDisp)                        ''チャンネル情報データ（表示名設定データ）
            'gudt.SetChInfo = DeepCopyHelper.DeepCopy(mudtSetChannel)                            ''チャンネル情報
            'gudt.SetChComposite = DeepCopyHelper.DeepCopy(mudtSetComposite)                     ''コンポジット設定
            'gudt.SetChGroupSet = DeepCopyHelper.DeepCopy(mudtSetGroup)                          ''グループ設定
            'gudt.SetChGroupRepose = DeepCopyHelper.DeepCopy(mudtSetRepose)                      ''リポーズ入力設定
            'gudt.SetChRunHour = DeepCopyHelper.DeepCopy(mudtSetChRunHour)                       ''積算データ設定ファイル
            'gudt.SetChExhGus = DeepCopyHelper.DeepCopy(mudtSetExhGus)                           ''排ガス演算処理設定
            'gudt.SetChCtrlUse = DeepCopyHelper.DeepCopy(mudtSetCtrlUseNotuse)                   ''コントロール使用可／不可設定
            'gudt.SetChDataSave = DeepCopyHelper.DeepCopy(mudtSetChDataSaveTable)                ''データ保存テーブル
            'gudt.SetChDataForward = DeepCopyHelper.DeepCopy(mudtSetChDataForwardTableSet)       ''データ転送テーブル設定
            'gudt.SetExtAlarm = DeepCopyHelper.DeepCopy(mudtSetExtAlarm)                         ''延長警報設定
            'gudt.SetExtTimerSet = DeepCopyHelper.DeepCopy(mudtSetTimer)                         ''タイマ設定
            'gudt.SetExtTimerName = DeepCopyHelper.DeepCopy(mudtSetTimerName)                    ''タイマ表示名称設定
            'gudt.SetSeqID = DeepCopyHelper.DeepCopy(mudtSetSeqSequenceID)                       ''シーケンスID
            'gudt.SetSeqSet = DeepCopyHelper.DeepCopy(mudtSetSeqSequenceSet)                     ''シーケンス設定
            'gudt.SetSeqLinear = DeepCopyHelper.DeepCopy(mudtSetSeqLinear)                       ''リニアライズテーブル
            'gudt.SetSeqOpeExp = DeepCopyHelper.DeepCopy(mudtSetSeqOpeExp)                       ''演算式テーブル
            'gudt.SetOpsScreenTitle = DeepCopyHelper.DeepCopy(mudtSetOpsScreenTitle)             ''OPS画面タイトル
            'gudt.SetOpsPulldownMenu = DeepCopyHelper.DeepCopy(mudtSetOpsPulldownMenu)           ''プルダウンメニュー
            'gudt.SetOpsPulldownMenuGroup = DeepCopyHelper.DeepCopy(mudtSetOpsPulldownMenuGroup) ''プルダウンメニュー消す
            'gudt.SetOpsGraph = DeepCopyHelper.DeepCopy(mudtSetOpsGraph)                         ''OPSグラフ設定
            'gudt.SetOpsLogFormat = DeepCopyHelper.DeepCopy(mudtSetOpsLogFormat)                 ''ログフォーマット

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "エラーメッセージ追加"

    '--------------------------------------------------------------------
    ' 機能      : エラーメッセージ追加
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) エラーメッセージ
    ' 機能説明  : エラーメッセージをモジュール変数にセットして、
    ' 　　　　    エラーカウントをカウントアップする
    '--------------------------------------------------------------------
    Private Sub mSetErrString(ByVal strMsgEng As String, _
                              ByVal strMsgJpn As String)

        Try

            Dim intCurNo As Integer = 0

            If mstrErrMsg Is Nothing Then
                ReDim mstrErrMsg(0)
                intCurNo = 0
            Else
                intCurNo = UBound(mstrErrMsg) + 1
                ReDim Preserve mstrErrMsg(intCurNo)
            End If

            mintErrCnt += 1
            mstrErrMsg(intCurNo) = IIf(mblnEnglish, strMsgEng, strMsgJpn)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetErrString(ByVal strMsgEng As String, _
                              ByVal strMsgJpn As String, _
                              ByRef intErrCnt As Integer, _
                              ByRef strErrMsg() As String)

        Try

            Dim intCurNo As Integer = 0

            If strErrMsg Is Nothing Then
                ReDim strErrMsg(0)
                intCurNo = 0
            Else
                intCurNo = UBound(strErrMsg) + 1
                ReDim Preserve strErrMsg(intCurNo)
            End If

            intErrCnt += 1
            strErrMsg(intCurNo) = IIf(mblnEnglish, strMsgEng, strMsgJpn)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : ワーニングメッセージ追加
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) ワーニングメッセージ
    ' 機能説明  : メッセージをモジュール変数にセットして、
    ' 　　　　    カウントをカウントアップする
    '--------------------------------------------------------------------
    Private Sub mSetWarString(ByVal strMsgEng As String, _
                              ByVal strMsgJpn As String)

        Try

            Dim intCurNo As Integer = 0

            If mstrWarMsg Is Nothing Then
                ReDim mstrWarMsg(0)
                intCurNo = 0
            Else
                intCurNo = UBound(mstrWarMsg) + 1
                ReDim Preserve mstrWarMsg(intCurNo)
            End If

            mintWarCnt += 1
            mstrWarMsg(intCurNo) = IIf(mblnEnglish, strMsgEng, strMsgJpn)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetWarString(ByVal strMsgEng As String, _
                              ByVal strMsgJpn As String, _
                              ByRef intWarCnt As Integer, _
                              ByRef strWarMsg() As String)

        Try

            Dim intCurNo As Integer = 0

            If strWarMsg Is Nothing Then
                ReDim strWarMsg(0)
                intCurNo = 0
            Else
                intCurNo = UBound(strWarMsg) + 1
                ReDim Preserve strWarMsg(intCurNo)
            End If

            intWarCnt += 1
            strWarMsg(intCurNo) = IIf(mblnEnglish, strMsgEng, strMsgJpn)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub
#End Region

#Region "仮設定メッセージ追加"

    '--------------------------------------------------------------------
    ' 機能      : 仮設定メッセージ追加
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 仮設定メッセージ
    ' 機能説明  : 仮設定メッセージをモジュール変数にセットして、
    ' 　　　　    仮設定カウントをカウントアップする
    '--------------------------------------------------------------------
    Private Sub mSetDummyString(ByVal strMsgEng As String, _
                                ByVal strMsgJpn As String)

        Try

            Dim intCurNo As Integer = 0

            If mstrDummyMsg Is Nothing Then
                ReDim mstrDummyMsg(0)
                intCurNo = 0
            Else
                intCurNo = UBound(mstrDummyMsg) + 1
                ReDim Preserve mstrDummyMsg(intCurNo)
            End If

            mintDummyCnt += 1
            mstrDummyMsg(intCurNo) = IIf(mblnEnglish, strMsgEng, strMsgJpn)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    Private Sub mSetDummyString(ByVal strMsgEng As String, _
                                ByVal strMsgJpn As String, _
                                ByRef intDummyCnt As Integer, _
                                ByRef strDummyMsg() As String)

        Try

            Dim intCurNo As Integer = 0

            If strDummyMsg Is Nothing Then
                ReDim strDummyMsg(0)
                intCurNo = 0
            Else
                intCurNo = UBound(strDummyMsg) + 1
                ReDim Preserve strDummyMsg(intCurNo)
            End If

            intDummyCnt += 1
            strDummyMsg(intCurNo) = IIf(mblnEnglish, strMsgEng, strMsgJpn)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "ログテキスト追加"

    '--------------------------------------------------------------------
    ' 機能      : テキスト表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) メッセージ
    ' 機能説明  : 処理メッセージをテキストに表示する
    '--------------------------------------------------------------------
    Private Sub mAddMsgText(ByVal strMsgEng As String, _
                            ByVal strMsgJpn As String)

        Try

            ''メッセージ追加
            txtMsg.Text &= IIf(mblnEnglish, strMsgEng, strMsgJpn) & vbCrLf '  vbNewLine

            ''表示位置設定
            txtMsg.SelectionStart = Len(txtMsg.Text)
            txtMsg.Focus()
            txtMsg.ScrollToCaret()

            ''テキスト更新
            'Call txtMsg.Refresh()
            Call Application.DoEvents()
            'Call System.Threading.Thread.Sleep(1)
            'Call Application.DoEvents()

            ''キャンセル時
            If mblnCancelFlg Then

                Select Case mudtCompileType
                    Case gEnmCompileType.cpCompile
                        txtMsg.Text &= IIf(mblnEnglish, "Compile Canceled.", "コンパイルをキャンセルしました。") & vbNewLine
                    Case gEnmCompileType.cpErrorCheck
                        txtMsg.Text &= IIf(mblnEnglish, "Error Check Canceled.", "エラーチェックをキャンセルしました。") & vbNewLine
                    Case gEnmCompileType.cpMeasuringCheck
                        ''2019.03.12 追加
                        txtMsg.Text &= IIf(mblnEnglish, "Error Check Canceled.", "エラーチェックをキャンセルしました。") & vbNewLine
                End Select

                txtMsg.SelectionStart = Len(txtMsg.Text)
                txtMsg.Focus()
                txtMsg.ScrollToCaret()

                Call mSetDisplayEnable(True)

            End If



        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "画面設定"

    Private Sub mSetDisplayEnable(ByVal blnFlg As Boolean, _
                         Optional ByVal strWaitMsg1 As String = "", _
                         Optional ByVal strWaitMsg2 As String = "")

        If blnFlg Then

            ''Waitフレーム
            fraWait.Visible = False
            cmdErrorDisplay.Enabled = True
            cmdPrint.Enabled = True
            cmdOutput.Enabled = True
            cmdExit.Enabled = True

            mblnCancelFlg = True

            Select Case mudtCompileType
                Case gEnmCompileType.cpCompile
                    cmdCompile.Text = mCstButtonTextCmpStart
                Case gEnmCompileType.cpErrorCheck
                    cmdCompile.Text = mCstButtonTextErrStart
                Case gEnmCompileType.cpMeasuringCheck
                    '' 2019.03.12 追加
                    cmdCompile.Text = mCstButtonTextErrStart
            End Select

        Else

            ''Waitフレーム
            lblWait1.Text = strWaitMsg1
            lblWait2.Text = strWaitMsg2
            fraWait.Visible = True
            fraWait.Refresh()

            cmdErrorDisplay.Enabled = False
            cmdPrint.Enabled = False
            cmdOutput.Enabled = False
            cmdExit.Enabled = False

            mblnCancelFlg = False

            Select Case mudtCompileType
                Case gEnmCompileType.cpCompile
                    cmdCompile.Text = mCstButtonTextCmpCancel
                Case gEnmCompileType.cpErrorCheck
                    cmdCompile.Text = mCstButtonTextErrCancel
                Case gEnmCompileType.cpMeasuringCheck
                    ''2019.03.12 追加
                    cmdCompile.Text = mCstButtonTextErrCancel
            End Select

        End If


    End Sub

#End Region

#Region "出力ファイル名取得"

    '--------------------------------------------------------------------
    ' 機能      : 出力ファイル名取得
    ' 返り値    : 出力ファイル名取得
    ' 引き数    : ARG1 - (I ) ファイル情報構造体
    ' 　　　    : ARG2 - (I ) 基本ファイル名
    ' 機能説明  : コンパイルファイルなのでそのまま返す
    '--------------------------------------------------------------------
    Private Function mGetOutputFileName(ByVal udtFileInfo As gTypFileInfo, ByVal strFileName As String) As String

        Try

            Return strFileName

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strFileName

    End Function

#End Region

#Region "CH変換テーブルチェック"

    Private Function mChkChConvTable(ByVal udtChConv As gTypSetChConv) As Boolean

        For i As Integer = 0 To UBound(udtChConv.udtChConv)

            With udtChConv.udtChConv(i)

                If .shtChid <> 0 Then
                    Return True
                End If

            End With

        Next

        Return False

    End Function

#End Region

#Region "M/C名称取得"

    Private Function mGetPartName(ByVal blnMachinery As Boolean) As String

        Dim strRtn As String = ""

        Try

            If blnMachinery Then

                If mblnEnglish Then
                    strRtn = "Machinery"
                Else
                    strRtn = "マシナリ"
                End If

            Else

                If mblnEnglish Then
                    strRtn = "Cargo"
                Else
                    strRtn = "カーゴ"
                End If

            End If


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

        Return strRtn

    End Function

#End Region

#Region "チャンネル設定ファイルの可変長出力"

    Private Sub mRemakeChannelFileSave(ByVal strFullPath As String)

        Try

            Dim intCnt As Integer = 0
            Dim lngByteCntOut As Integer = 0
            Dim lngByteCntAll As Integer = gCstByteCntHeader + (gCstByteCntChannelOne * gCstChannelIdMax)
            Dim bytInput(lngByteCntAll - 1) As Byte
            Dim bytOutput() As Byte = Nothing
            Dim intFileNo As Integer
            Dim intLastSetIndex As Integer = -1

            ''出力したファイルをバイト配列で読み込む
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Read, OpenShare.LockWrite)

            Try
                FileGet(intFileNo, bytInput)
            Catch ex As Exception
                Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Finally
                FileClose(intFileNo)
            End Try

            ''チャンネル設定構造体を下から上へ検索し、最後にチャンネルが設定されている位置を保存する
            For i As Integer = UBound(gudt.SetChInfo.udtChannel) To 0 Step -1
                With gudt.SetChInfo.udtChannel(i).udtChCommon
                    If .shtChid <> 0 And .shtChno <> 0 And .shtChType <> gCstCodeChTypeNothing Then
                        intLastSetIndex = i
                        Exit For
                    End If
                End With
            Next

            ''出力するバイト数を算出する
            lngByteCntOut = gCstByteCntHeader + ((intLastSetIndex + 1) * gCstByteCntChannelOne)

            ''出力用のバイト配列を定義する
            ReDim bytOutput(lngByteCntOut - 1)

            ''出力用のバイト配列に出力データをコピーする
            Call Array.Copy(bytInput, bytOutput, lngByteCntOut)

            ''ファイルが存在する場合を消す
            If System.IO.File.Exists(strFullPath) Then Call My.Computer.FileSystem.DeleteFile(strFullPath)

            ''出力用のバイト配列からファイルを出力する
            intFileNo = FreeFile()
            FileOpen(intFileNo, strFullPath, OpenMode.Binary, OpenAccess.Write, OpenShare.LockWrite)

            Try
                FilePut(intFileNo, bytOutput)
            Catch ex As Exception
                Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Finally
                FileClose(intFileNo)
            End Try

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "チャンネル情報データ（表示名称設定データ）のCHID(CHNO)を設定"

    Private Sub mSetChDispChId()

        Try

            Dim intCurFuNo As Integer
            Dim intCurSlot As Integer
            Dim intCurPin As Integer
            Dim intCurPinNo As Integer
            Dim aryCheck As New ArrayList
            Dim strChNo As String = ""
            Dim strListIndex As String = ""
            Dim i As Integer
            Dim x As Integer

            Dim intFuNo As Integer
            Dim intPortNo As Integer
            Dim intPin As Integer

            Dim intChkChNo As Integer
            Dim intChkindex As Integer

            ''現在設定されているCHNOを初期化
            'For i As Integer = 0 To UBound(gudt.SetChDisp.udtChDisp)
            For i = 0 To UBound(gudt.SetChDisp.udtChDisp)
                For j As Integer = 0 To UBound(gudt.SetChDisp.udtChDisp(i).udtSlotInfo)
                    For k As Integer = 0 To UBound(gudt.SetChDisp.udtChDisp(i).udtSlotInfo(j).udtPinInfo)
                        With gudt.SetChDisp.udtChDisp(i).udtSlotInfo(j).udtPinInfo(k)
                            .shtChid = 0
                        End With
                    Next
                Next
            Next

            '端子DO処理追加　2015.07.14 T.Ueki
            For i = 0 To UBound(gudt.SetChOutput.udtCHOutPut)

                With gudt.SetChOutput.udtCHOutPut(i)
                    intFuNo = .bytFuno
                    intPortNo = .bytPortno
                    intPin = .bytPin
                End With

                ''各設定値が範囲内のものだけ追加する
                If (intFuNo >= 0 And intFuNo <= gCstCountFuNo - 1) And _
                   (intPortNo >= 1 And intPortNo <= gCstCountFuPort) And _
                   (intPin >= 1 And intPin <= gCstCountFuPin) Then

                    If gudt.SetChOutput.udtCHOutPut(i).bytType = 0 Then
                        ''出力チャンネル構造体からFU構造体に情報を移す
                        If gudt.SetChDisp.udtChDisp(intFuNo).udtSlotInfo(intPortNo - 1).udtPinInfo(intPin - 1).shtChid = 0 Then
                            gudt.SetChDisp.udtChDisp(intFuNo).udtSlotInfo(intPortNo - 1).udtPinInfo(intPin - 1).shtChid = gudt.SetChOutput.udtCHOutPut(i).shtChid
                        End If
                    End If

                End If

            Next i

            ''チャンネル番号順に並べ替え 2015.07.14 T.Ueki
            gMakeChNoOrderSort(aryCheck)

            'For i As Integer = 0 To UBound(gudt.SetChInfo.udtChannel)
            For x = 0 To aryCheck.Count - 1     '' CH順ソートでループ

                ''ソート結果からリストのインデックス取得   2015.07.14 T.Ueki
                gGetChNoOrder(aryCheck, x, strChNo, strListIndex)

                i = Val(strListIndex)   '' リストインデックスをセット    2015.07.14 T.Ueki

                With gudt.SetChInfo.udtChannel(i).udtChCommon
                    If .shtChno = 707 Then
                        Dim debugA As Integer = 0
                    End If


                    ''チャンネル番号が設定してある場合
                    If .shtChno <> 0 Then

                        Select Case .shtChType
                            Case gCstCodeChTypeAnalog

                                intCurFuNo = .shtFuno
                                intCurSlot = .shtPortno
                                intCurPin = .shtPin
                                intCurPinNo = .shtPinNo

                                ''FUアドレスが設定してある場合
                                If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                    ''端子台に割り付くFUアドレスを設定するデータタイプの場合
                                    If .shtData = gCstCodeChDataTypeAnalogK _
                                    Or .shtData = gCstCodeChDataTypeAnalog2Pt _
                                    Or .shtData = gCstCodeChDataTypeAnalog2Jpt _
                                    Or .shtData = gCstCodeChDataTypeAnalog3Pt _
                                    Or .shtData = gCstCodeChDataTypeAnalog3Jpt _
                                    Or .shtData = gCstCodeChDataTypeAnalog1_5v _
                                    Or .shtData = gCstCodeChDataTypeAnalog4_20mA _
                                    Or .shtData = gCstCodeChDataTypeAnalogExhAve _
                                    Or .shtData = gCstCodeChDataTypeAnalogExhRepose _
                                    Or .shtData = gCstCodeChDataTypeAnalogExtDev Then
                                        If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = 0 Then
                                            gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = .shtChno
                                        End If
                                    End If

                                End If

                            Case gCstCodeChTypeDigital

                                intCurFuNo = .shtFuno
                                intCurSlot = .shtPortno
                                intCurPin = .shtPin
                                intCurPinNo = .shtPinNo

                                ''FUアドレスが設定してある場合
                                If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                    ''端子台に割り付くFUアドレスを設定するデータタイプの場合
                                    If .shtData = gCstCodeChDataTypeDigitalNC _
                                    Or .shtData = gCstCodeChDataTypeDigitalNO _
                                    Or .shtData = gCstCodeChDataTypeDigitalExt Then
                                        If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = 0 Then
                                            gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = .shtChno
                                        Else
                                            'ver2.0.8.7 RHよりDIGITAL CHを優先とする
                                            intChkChNo = gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid
                                            intChkindex = gConvChNoToChArrayId(intChkChNo)
                                            If gudt.SetChInfo.udtChannel(intChkindex).udtChCommon.shtChType = gCstCodeChTypePulse Then
                                                gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = .shtChno
                                            End If
                                        End If
                                    End If

                                End If

                            Case gCstCodeChTypeMotor

                                '==========
                                '' Input
                                '==========
                                intCurFuNo = .shtFuno
                                intCurSlot = .shtPortno
                                intCurPin = .shtPin
                                intCurPinNo = .shtPinNo

                                ''FUアドレスが設定してある場合
                                If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                    ''端子台に割り付かないデータタイプではない場合
                                    'Ver1.12.1 通信ﾓｰﾀｰCHは除く
                                    If .shtData <> gCstCodeChDataTypeMotorDeviceJacom And _
                                        .shtData <> gCstCodeChDataTypeMotorDeviceJacom55 And _
                                        .shtData < gCstCodeChDataTypeMotorRManRun Then

                                        For l As Integer = intCurPin - 1 To (intCurPin - 1) + (intCurPinNo - 1)
                                            If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = 0 Then
                                                gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = .shtChno
                                            Else
                                                'ver2.0.8.7 RHよりMOTOR CHを優先とする
                                                intChkChNo = gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid
                                                intChkindex = gConvChNoToChArrayId(intChkChNo)
                                                If gudt.SetChInfo.udtChannel(intChkindex).udtChCommon.shtChType = gCstCodeChTypePulse Then
                                                    gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = .shtChno
                                                End If
                                            End If
                                        Next

                                    End If

                                End If

                                '==========
                                '' Output
                                '==========
                                intCurFuNo = gudt.SetChInfo.udtChannel(i).MotorFuNo
                                intCurSlot = gudt.SetChInfo.udtChannel(i).MotorPortNo
                                intCurPin = gudt.SetChInfo.udtChannel(i).MotorPin
                                intCurPinNo = gudt.SetChInfo.udtChannel(i).MotorPinNo

                                ''FUアドレスが設定してある場合
                                If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                    ''端子台に割り付かないデータタイプではない場合
                                    If .shtData <> gCstCodeChDataTypeMotorDeviceJacom Or _
                                       .shtData <> gCstCodeChDataTypeMotorDevice Or _
                                       .shtData <> gCstCodeChDataTypeMotorDeviceJacom55 Then
                                        For l As Integer = intCurPin - 1 To (intCurPin - 1) + (intCurPinNo - 1)
                                            If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = 0 Then
                                                gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = .shtChno
                                            End If
                                        Next
                                    End If

                                End If

                            Case gCstCodeChTypeValve

                                '==========
                                '' Input
                                '==========
                                intCurFuNo = .shtFuno
                                intCurSlot = .shtPortno
                                intCurPin = .shtPin
                                intCurPinNo = .shtPinNo

                                Select Case .shtData
                                    Case gCstCodeChDataTypeValveDI_DO

                                        ''FUアドレスが設定してある場合
                                        If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                            For l As Integer = intCurPin - 1 To (intCurPin - 1) + (intCurPinNo - 1)
                                                If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = 0 Then
                                                    gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = .shtChno
                                                End If
                                            Next

                                        End If

                                    Case gCstCodeChDataTypeValveAI_DO1, _
                                         gCstCodeChDataTypeValveAI_DO2, _
                                         gCstCodeChDataTypeValveAI_AO1, _
                                         gCstCodeChDataTypeValveAI_AO2

                                        ''FUアドレスが設定してある場合
                                        If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                            If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = 0 Then
                                                gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = .shtChno
                                            End If

                                        End If

                                    Case gCstCodeChDataTypeValveAO_4_20
                                    Case gCstCodeChDataTypeValveDO
                                    Case gCstCodeChDataTypeValveJacom
                                    Case gCstCodeChDataTypeValveJacom55
                                    Case gCstCodeChDataTypeValveExt
                                End Select

                                '==========
                                '' Output
                                '==========
                                Select Case .shtData
                                    Case gCstCodeChDataTypeValveDI_DO, _
                                         gCstCodeChDataTypeValveDO, _
                                         gCstCodeChDataTypeValveExt

                                        intCurFuNo = gudt.SetChInfo.udtChannel(i).ValveDiDoFuNo
                                        intCurSlot = gudt.SetChInfo.udtChannel(i).ValveDiDoPortNo
                                        intCurPin = gudt.SetChInfo.udtChannel(i).ValveDiDoPin
                                        intCurPinNo = gudt.SetChInfo.udtChannel(i).ValveDiDoPinNo

                                        ''FUアドレスが設定してある場合
                                        If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                            For l As Integer = intCurPin - 1 To (intCurPin - 1) + (intCurPinNo - 1)
                                                If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = 0 Then
                                                    gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = .shtChno
                                                End If
                                            Next

                                        End If

                                    Case gCstCodeChDataTypeValveAI_DO1, _
                                         gCstCodeChDataTypeValveAI_DO2

                                        intCurFuNo = gudt.SetChInfo.udtChannel(i).ValveAiDoFuNo
                                        intCurSlot = gudt.SetChInfo.udtChannel(i).ValveAiDoPortNo
                                        intCurPin = gudt.SetChInfo.udtChannel(i).ValveAiDoPin
                                        intCurPinNo = gudt.SetChInfo.udtChannel(i).ValveAiDoPinNo

                                        ''FUアドレスが設定してある場合
                                        If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                            For l As Integer = intCurPin - 1 To (intCurPin - 1) + (intCurPinNo - 1)
                                                If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = 0 Then
                                                    gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = .shtChno
                                                End If
                                            Next

                                        End If

                                    Case gCstCodeChDataTypeValveAI_AO1, _
                                         gCstCodeChDataTypeValveAI_AO2, _
                                         gCstCodeChDataTypeValveAO_4_20

                                        intCurFuNo = gudt.SetChInfo.udtChannel(i).ValveAiAoFuNo
                                        intCurSlot = gudt.SetChInfo.udtChannel(i).ValveAiAoPortNo
                                        intCurPin = gudt.SetChInfo.udtChannel(i).ValveAiAoPin
                                        intCurPinNo = gudt.SetChInfo.udtChannel(i).ValveAiAoPinNo

                                        ''FUアドレスが設定してある場合
                                        If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                            If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = 0 Then
                                                gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = .shtChno
                                            End If

                                        End If

                                End Select

                            Case gCstCodeChTypeComposite

                                intCurFuNo = .shtFuno
                                intCurSlot = .shtPortno
                                intCurPin = .shtPin
                                intCurPinNo = .shtPinNo

                                ''FUアドレスが設定してある場合
                                If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then
                                    For l As Integer = intCurPin - 1 To (intCurPin - 1) + (intCurPinNo - 1)
                                        If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = 0 Then
                                            gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = .shtChno
                                        End If
                                    Next
                                End If

                            Case gCstCodeChTypePulse

                                intCurFuNo = .shtFuno
                                intCurSlot = .shtPortno
                                intCurPin = .shtPin
                                intCurPinNo = .shtPinNo

                                '' Ver1.12.0.2 2016.01.23  運転積算 通信ﾀｲﾌﾟは除く
                                'Ver2.0.3.8 通信タイプ追加
                                'If .shtData >= gCstCodeChDataTypePulseRevoExtDev And .shtData <= gCstCodeChDataTypePulseRevoExtDevLapMin Then
                                If .shtData >= gCstCodeChDataTypePulseExtDev And .shtData <= gCstCodeChDataTypePulseRevoExtDevLapMin Then
                                Else
                                    ''FUアドレスが設定してある場合
                                    If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then
                                        If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = 0 Then
                                            gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = .shtChno
                                        End If
                                    End If
                                End If

                                'Ver2.0.7.D PID
                            Case gCstCodeChTypePID

                                intCurFuNo = .shtFuno
                                intCurSlot = .shtPortno
                                intCurPin = .shtPin
                                intCurPinNo = .shtPinNo

                                ''FUアドレスが設定してある場合
                                If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                    ''端子台に割り付くFUアドレスを設定するデータタイプの場合
                                    If .shtData = gCstCodeChDataTypePID_1_AI1_5 _
                                    Or .shtData = gCstCodeChDataTypePID_2_AI4_20 _
                                    Or .shtData = gCstCodeChDataTypePID_3_Pt100_2 _
                                    Or .shtData = gCstCodeChDataTypePID_4_Pt100_3 _
                                    Or .shtData = gCstCodeChDataTypePID_5_AI_K Then
                                        If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = 0 Then
                                            gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(intCurPin - 1).shtChid = .shtChno
                                        End If
                                    End If

                                End If

                                '==========
                                '' Output
                                '==========
                                intCurFuNo = gudt.SetChInfo.udtChannel(i).PidOutFuNo
                                intCurSlot = gudt.SetChInfo.udtChannel(i).PidOutPortNo
                                intCurPin = gudt.SetChInfo.udtChannel(i).PidOutPin
                                intCurPinNo = gudt.SetChInfo.udtChannel(i).PidOutPinNo

                                ''FUアドレスが設定してある場合
                                If gChkFuAddressSet(intCurFuNo, intCurSlot, intCurPin) Then

                                    ''端子台に割り付かないデータタイプではない場合
                                    If .shtData <> gCstCodeChDataTypeMotorDeviceJacom Or _
                                        .shtData <> gCstCodeChDataTypeMotorDeviceJacom Or _
                                        .shtData <> gCstCodeChDataTypeMotorDevice Then
                                        For l As Integer = intCurPin - 1 To (intCurPin - 1) + (intCurPinNo - 1)
                                            If gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = 0 Then
                                                gudt.SetChDisp.udtChDisp(intCurFuNo).udtSlotInfo(intCurSlot - 1).udtPinInfo(l).shtChid = .shtChno
                                            End If
                                        Next
                                    End If

                                End If
                        End Select



                    End If

                End With

            Next x

            Call mAddMsgText(" -CH NO Re-setting ... Success", " -チャンネルNO再設定 ... OK")
            Call mAddMsgText("", "")

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try


    End Sub

#End Region

#Region "隠しCH 警報設定ﾁｪｯｸ"

    '--------------------------------------------------------------------
    ' 機能      : 隠しCHのAC設定をﾁｪｯｸ
    ' 返り値    : なし
    ' 引き数    : 
    ' 機能説明  : 
    '--------------------------------------------------------------------
    Public Sub ChkSCAlm(ByVal GNo As Short, ByVal CHNo As Short, ByVal Flag1 As Short, ByVal Flag2 As Short, _
                        ByRef intErrCnt As Integer, ByRef strErrMsg() As String)
        Dim strMsgEng As String = ""
        Dim strMsgJpn As String = ""

        ''SC(隠しCH設定)のみ処理を行う
        If gBitCheck(Flag1, 1) = False Then
            Return
        End If

        '' AC 設定有りの場合はOK
        If gBitCheck(Flag2, 3) = True Then
            Return
        End If

        '' 警告
        Call mSetErrString("Check AC Setting of SC CH" & _
                       "[Info]Group=" & GNo & _
                       " , CH NO=" & CHNo & strMsgEng, _
                       "隠しCHのACが設定されていません" & _
                       "[情報]グループ=" & GNo & _
                       " , チャンネル番号=" & CHNo & strMsgJpn, _
                       intErrCnt, strErrMsg)

    End Sub

#End Region


#Region "ｺﾝﾊﾟｲﾙﾃﾞｰﾀ 保存ﾌﾗｸﾞｾｯﾄ"

    '--------------------------------------------------------------------
    ' 機能      : ｺﾝﾊﾟｲﾙﾃﾞｰﾀ 保存ﾌﾗｸﾞｾｯﾄ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  :   '' Ver1.10.8 2016.06.28  全ﾃﾞｰﾀを保存するように変更
    '--------------------------------------------------------------------
    Private Sub SetCompileSaveFg()

        Dim i As Integer

        With gudt.SetEditorUpdateInfo.udtCompile
            .bytSystem = 1   ''システム設定データ
            .bytFuChannel = 1   ''FU チャンネル情報
            .bytChDisp = 1   ''チャンネル情報データ（表示名設定
            .bytChannel = 1   ''チャンネル情報
            .bytComposite = 1   ''コンポジット情報
            .bytGroupM = 1   ''グループ設定
            .bytGroupC = 1   ''グループ設定
            .bytRepose = 1   ''リポーズ入力設定
            .bytOutPut = 1   ''出力チャンネル設定
            .bytOrAnd = 1    ''論理出力設定
            .bytChRunHour = 1   ''積算データ設定
            .bytCtrlUseNotuseM = 1   ''コントロール使用可／不可設定
            .bytCtrlUseNotuseC = 1   ''コントロール使用可／不可設定
            .bytChSio = 1   ''SIO設定

            For i = 0 To UBound(.bytChSioCh)        ''SIO通信チャンネル設定1～16
                .bytChSioCh(i) = 1
            Next

            .bytExhGus = 1   ''排ガス処理演算設定
            .bytExtAlarm = 1   ''延長警報
            .bytTimer = 1   ''タイマ設定
            .bytTimerName = 1   ''タイマ表示名称設定
            .bytSeqSequenceID = 1   ''シーケンスID
            .bytSeqSequenceSet = 1   ''シーケンス設定
            .bytSeqLinear = 1   ''リニアライズテーブル
            .bytSeqOperationExpression = 1   ''演算式テーブル
            .bytChDataSaveTable = 1   ''データ保存テーブル設定
            .bytChDataForwardTableSet = 1   ''データ転送テーブル設定

            .bytOpsScreenTitleM = 1   ''OPSスクリーンタイトル
            .bytOpsScreenTitleC = 1   ''OPSスクリーンタイトル
            .bytOpsManuMainM = 1   ''プルダウンメニュー
            .bytOpsManuMainC = 1   ''プルダウンメニュー
            .bytOpsSelectionMenuM = 1   ''セレクションメニュー
            .bytOpsSelectionMenuC = 1   ''セレクションメニュー
            .bytOpsGraphM = 1   ''OPSグラフ設定
            .bytOpsGraphC = 1   ''OPSグラフ設定
            ''.bytOpsFreeGraphM = 1   ''フリーグラフ
            ''.bytOpsFreeGraphC = 1   ''フリーグラフ
            ''.bytOpsFreeDisplayM = 1   ''フリーディスプレイ
            ''.bytOpsFreeDisplayC = 1   ''フリーディスプレイ
            ''.bytOpsTrendGraphM = 1   ''トレンドグラフ
            ''.bytOpsTrendGraphC = 1   ''トレンドグラフ
            .bytOpsLogFormatM = 1   ''ログフォーマット
            .bytOpsLogFormatC = 1   ''ログフォーマット
            .bytChConvNow = 1   ''CH変換テーブル
            .bytChConvPrev = 1   ''CH変換テーブル
            .bytOpsLogIdDataM = 1   ''ログフォーマットCHID
            .bytOpsLogIdDataC = 1   ''ログフォーマットCHID
            .bytOpsGwsCh = 1   ''GWS通信チャンネル設定
            .bytOpsLogOption = 1   ''ﾛｸﾞｵﾌﾟｼｮﾝ設定
        End With
    End Sub

#End Region


#End Region

  
End Class
