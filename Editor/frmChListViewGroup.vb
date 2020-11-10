﻿Public Class frmChListViewGroup

#Region "変数定義"

    ''インスタンス用配列 ----------------------------------

    ''Colorボタン配列
    Private mCmdColor() As System.Windows.Forms.Button

    ''グループ枠配列
    Private mFrame() As System.Windows.Forms.GroupBox

    ''構造体配列用インデックス（非表示）
    Private mtxtGroupIndex() As System.Windows.Forms.TextBox

    ''グループ番号
    Private mtxtGroupNo() As System.Windows.Forms.TextBox

    ''グループ名称配列
    Private mtxtGroupName() As System.Windows.Forms.TextBox

    ''CH登録画面展開ボタン配列
    Private mCmdList() As System.Windows.Forms.Button
    ''------------------------------------------------------

    'T.Ueki 色情報変更　2014/5/27
    ''色情報
    Private mColorInfo() As Color = {Color.Black, Color.Blue, Color.Green, _
                                     Color.Cyan, Color.Red, Color.Purple}

    'Private mColorInfo() As Color = {Color.WhiteSmoke, Color.Silver, Color.GreenYellow, _
    '                                 Color.MediumPurple, Color.Yellow, Color.CornflowerBlue, _
    '                                 Color.PaleTurquoise, Color.Red, Color.Gold}

    ''グループ情報格納
    Public Structure GroupInfo
        Public intGroupNo As Integer    ''グループ番号
        Public intColorNo As Integer    ''グループ枠色番号：0～8
        Public intDispIndex As Integer  ''グループ表示位置
        Public strName As String        ''グループ名称
    End Structure
    Public mGroupInfo(gCstChannelGroupMax - 1) As GroupInfo

    ''グループ情報複製用
    Private mudtSetGroupNew As gTypSetChGroupSet

    ''グループ情報(マシナリー)作業用
    Private mudtWorkGroupM As gTypSetChGroupSet

    ''グループ情報(カーゴ)作業用
    Private mudtWorkGroupC As gTypSetChGroupSet

    ''Print Setting画面からの情報
    Private Structure PrintSettingInfo
        Public strDrawNo As String
        Public strComment As String
        Public strShip As String
    End Structure
    Private mPrintSetting(1) As PrintSettingInfo

    ''フラグ
    Private mflgSkip As Boolean = False

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : 1:OK  0:キャンセル
    ' 引き数    : ARG1 - (IO) シーケンス設定構造体
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow()

        Try

            ''本画面表示
            Call gShowFormModelessForCloseWaitChannel1(Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

#Region "画面イベント"

    '--------------------------------------------------------------------
    ' 機能      : フォームロード
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面表示初期処理を行う
    '--------------------------------------------------------------------
    Private Sub frmChListViewGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''参照モードの設定
            Call gSetChListDispOnly(Me, cmdSave)
            RemoveHandler Me.FormClosing, AddressOf frmSysSystem_FormClosing

            ''グループ構造体インデックス用　インスタンス作成
            mtxtGroupIndex = New System.Windows.Forms.TextBox(gCstChannelGroupMax - 1) { _
                txtIndex01, txtIndex02, txtIndex03, txtIndex04, txtIndex05, txtIndex06, _
                txtIndex07, txtIndex08, txtIndex09, txtIndex10, txtIndex11, txtIndex12, _
                txtIndex13, txtIndex14, txtIndex15, txtIndex16, txtIndex17, txtIndex18, _
                txtIndex19, txtIndex20, txtIndex21, txtIndex22, txtIndex23, txtIndex24, _
                txtIndex25, txtIndex26, txtIndex27, txtIndex28, txtIndex29, txtIndex30, _
                txtIndex31, txtIndex32, txtIndex33, txtIndex34, txtIndex35, txtIndex36}

            ''グループ番号用　インスタンス作成
            mtxtGroupNo = New System.Windows.Forms.TextBox(gCstChannelGroupMax - 1) { _
                txtNo01, txtNo02, txtNo03, txtNo04, txtNo05, txtNo06, _
                txtNo07, txtNo08, txtNo09, txtNo10, txtNo11, txtNo12, _
                txtNo13, txtNo14, txtNo15, txtNo16, txtNo17, txtNo18, _
                txtNo19, txtNo20, txtNo21, txtNo22, txtNo23, txtNo24, _
                txtNo25, txtNo26, txtNo27, txtNo28, txtNo29, txtNo30, _
                txtNo31, txtNo32, txtNo33, txtNo34, txtNo35, txtNo36}

            ''Colorボタン用　インスタンス作成
            mCmdColor = New System.Windows.Forms.Button(gCstChannelGroupMax - 1) { _
                cmdColor01, cmdColor02, cmdColor03, cmdColor04, cmdColor05, cmdColor06, _
                cmdColor07, cmdColor08, cmdColor09, cmdColor10, cmdColor11, cmdColor12, _
                cmdColor13, cmdColor14, cmdColor15, cmdColor16, cmdColor17, cmdColor18, _
                cmdColor19, cmdColor20, cmdColor21, cmdColor22, cmdColor23, cmdColor24, _
                cmdColor25, cmdColor26, cmdColor27, cmdColor28, cmdColor29, cmdColor30, _
                cmdColor31, cmdColor32, cmdColor33, cmdColor34, cmdColor35, cmdColor36}

            ''グループ背景色用　インスタンス作成
            mFrame = New System.Windows.Forms.GroupBox(gCstChannelGroupMax - 1) { _
                Frame01, Frame02, Frame03, Frame04, Frame05, Frame06, _
                Frame07, Frame08, Frame09, Frame10, Frame11, Frame12, _
                Frame13, Frame14, Frame15, Frame16, Frame17, Frame18, _
                Frame19, Frame20, Frame21, Frame22, Frame23, Frame24, _
                Frame25, Frame26, Frame27, Frame28, Frame29, Frame30, _
                Frame31, Frame32, Frame33, Frame34, Frame35, Frame36}

            ''グループ名称用　インスタンス作成
            mtxtGroupName = New System.Windows.Forms.TextBox(gCstChannelGroupMax - 1) { _
                txtGroupName01, txtGroupName02, txtGroupName03, txtGroupName04, txtGroupName05, txtGroupName06, _
                txtGroupName07, txtGroupName08, txtGroupName09, txtGroupName10, txtGroupName11, txtGroupName12, _
                txtGroupName13, txtGroupName14, txtGroupName15, txtGroupName16, txtGroupName17, txtGroupName18, _
                txtGroupName19, txtGroupName20, txtGroupName21, txtGroupName22, txtGroupName23, txtGroupName24, _
                txtGroupName25, txtGroupName26, txtGroupName27, txtGroupName28, txtGroupName29, txtGroupName30, _
                txtGroupName31, txtGroupName32, txtGroupName33, txtGroupName34, txtGroupName35, txtGroupName36}

            ''CH登録画面展開用ボタン　インスタンス作成
            mCmdList = New System.Windows.Forms.Button(gCstChannelGroupMax - 1) { _
                cmdList01, cmdList02, cmdList03, cmdList04, cmdList05, cmdList06, _
                cmdList07, cmdList08, cmdList09, cmdList10, cmdList11, cmdList12, _
                cmdList13, cmdList14, cmdList15, cmdList16, cmdList17, cmdList18, _
                cmdList19, cmdList20, cmdList21, cmdList22, cmdList23, cmdList24, _
                cmdList25, cmdList26, cmdList27, cmdList28, cmdList29, cmdList30, _
                cmdList31, cmdList32, cmdList33, cmdList34, cmdList35, cmdList36}

            ''イベントハンドラに関連付け
            For i = 0 To gCstChannelGroupMax - 1
                AddHandler mCmdColor(i).MouseClick, AddressOf Me.cmdColor_Click
                AddHandler mCmdList(i).MouseClick, AddressOf Me.cmdList_Click
                AddHandler mtxtGroupName(i).MouseDoubleClick, AddressOf Me.txtGroupName_DoubleClick
                AddHandler mtxtGroupName(i).KeyPress, AddressOf Me.txtGroupName_KeyPress
                AddHandler mtxtGroupNo(i).KeyPress, AddressOf Me.txtGroupNo_KeyPress
                AddHandler mtxtGroupNo(i).Validating, AddressOf Me.txtGroupNo_Validating
                AddHandler mtxtGroupNo(i).Validated, AddressOf Me.txtGroupNo_Validated
            Next

            ''配列再定義
            mudtSetGroupNew.udtGroup.InitArray()
            mudtWorkGroupM.udtGroup.InitArray()
            mudtWorkGroupC.udtGroup.InitArray()

            ''Work構造体にコピー
            Call mCopyStructure(gudt.SetChGroupSetM, mudtWorkGroupM)
            Call mCopyStructure(gudt.SetChGroupSetC, mudtWorkGroupC)

            ''画面グループ番号　初期値セット
            For i = 1 To gCstChannelGroupMax
                mtxtGroupNo(i - 1).Text = i.ToString("00")
            Next

            ''画面設定 ---------------------------------------------------
            Call mSetDisplay(mudtWorkGroupM, 0)    ''Machinery
            ''------------------------------------------------------------

            mflgSkip = True

            ''Machinery/Cargoボタン設定
            Call gSetCombineControl(optMachinery, optCargo)

            mflgSkip = False

            ''構造体インデックスは非表示
            For i As Integer = 1 To gCstChannelGroupMax
                mtxtGroupIndex(i - 1).Visible = False
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Saveボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 保存処理を行う
    '--------------------------------------------------------------------
    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try

            ''入力チェック
            If Not mChkInput() Then Return

            Dim intFlag As Integer = 0
            Dim intKubun As Integer = IIf(optMachinery.Checked, 0, 1)    ''0:Machinery　1:Cargo

            ''現在の画面の情報を作業用構造体に退避
            If intKubun = 0 Then
                Call mSetWork(mudtWorkGroupM, intKubun)

                ''図番、船番、コメントは区別はパート区別なし　ver.1.4.0 2011.09.21
                mudtWorkGroupC.udtGroup.strDrawNo = mudtWorkGroupM.udtGroup.strDrawNo
                mudtWorkGroupC.udtGroup.strShipNo = mudtWorkGroupM.udtGroup.strShipNo
                mudtWorkGroupC.udtGroup.strComment = mudtWorkGroupM.udtGroup.strComment
            ElseIf intKubun = 1 Then
                Call mSetWork(mudtWorkGroupC, intKubun)

                ''図番、船番、コメントは区別はパート区別なし　ver.1.4.0 2011.09.21
                mudtWorkGroupM.udtGroup.strDrawNo = mudtWorkGroupC.udtGroup.strDrawNo
                mudtWorkGroupM.udtGroup.strShipNo = mudtWorkGroupC.udtGroup.strShipNo
                mudtWorkGroupM.udtGroup.strComment = mudtWorkGroupC.udtGroup.strComment
            End If

            ''Machinery -----------------------------------------------------------------

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetChGroupSetM, mudtWorkGroupM) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtWorkGroupM, gudt.SetChGroupSetM)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                intFlag = 1

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytGroupM = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytGroupM = 1

            End If

            ''Cargo ---------------------------------------------------------------------

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetChGroupSetC, mudtWorkGroupC) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtWorkGroupC, gudt.SetChGroupSetC)

                ''メッセージ表示
                If intFlag = 0 Then
                    Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytGroupC = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytGroupC = 1

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ中
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 設定が変更されている場合は確認メッセージを表示する
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''現在の画面の情報を作業用構造体に退避
            Dim intKubun As Integer = IIf(optMachinery.Checked, 0, 1)    ''0:Machinery　1:Cargo

            If intKubun = 0 Then
                ''Machinery
                Call mSetWork(mudtWorkGroupM, intKubun)

                ''図番、船番、コメントは区別はパート区別なし　ver.1.4.0 2011.09.21
                mudtWorkGroupC.udtGroup.strDrawNo = mudtWorkGroupM.udtGroup.strDrawNo
                mudtWorkGroupC.udtGroup.strShipNo = mudtWorkGroupM.udtGroup.strShipNo
                mudtWorkGroupC.udtGroup.strComment = mudtWorkGroupM.udtGroup.strComment
            Else
                ''Cargo
                Call mSetWork(mudtWorkGroupC, intKubun)

                ''図番、船番、コメントは区別はパート区別なし　ver.1.4.0 2011.09.21
                mudtWorkGroupM.udtGroup.strDrawNo = mudtWorkGroupC.udtGroup.strDrawNo
                mudtWorkGroupM.udtGroup.strShipNo = mudtWorkGroupC.udtGroup.strShipNo
                mudtWorkGroupM.udtGroup.strComment = mudtWorkGroupC.udtGroup.strComment
            End If


            If intKubun = 0 Then

                ''データが変更されているかチェック
                If Not mChkStructureEquals(gudt.SetChGroupSetM, mudtWorkGroupM) Then

                    ''変更されている場合はメッセージ表示
                    Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                                "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                        Case Windows.Forms.DialogResult.Yes

                            ''入力チェック
                            If Not mChkInput() Then
                                e.Cancel = True
                                Return
                            End If

                            ''変更されている場合は設定を更新する
                            Call mCopyStructure(mudtWorkGroupM, gudt.SetChGroupSetM)

                            ''更新フラグ設定
                            gblnUpdateAll = True
                            gudt.SetEditorUpdateInfo.udtSave.bytGroupM = 1
                            gudt.SetEditorUpdateInfo.udtCompile.bytGroupM = 1

                        Case Windows.Forms.DialogResult.No

                            ''何もしない

                        Case Windows.Forms.DialogResult.Cancel

                            ''画面を閉じない
                            e.Cancel = True

                    End Select

                End If

            ElseIf intKubun = 1 Then

                ''データが変更されているかチェック
                If Not mChkStructureEquals(gudt.SetChGroupSetC, mudtWorkGroupC) Then

                    ''変更されている場合はメッセージ表示
                    Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                                "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                        Case Windows.Forms.DialogResult.Yes

                            ''入力チェック
                            If Not mChkInput() Then
                                e.Cancel = True
                                Return
                            End If

                            ''変更されている場合は設定を更新する
                            Call mCopyStructure(mudtWorkGroupC, gudt.SetChGroupSetC)

                            ''更新フラグ設定
                            gblnUpdateAll = True
                            gudt.SetEditorUpdateInfo.udtSave.bytGroupC = 1
                            gudt.SetEditorUpdateInfo.udtCompile.bytGroupC = 1

                        Case Windows.Forms.DialogResult.No

                            ''何もしない

                        Case Windows.Forms.DialogResult.Cancel

                            ''画面を閉じない
                            e.Cancel = True

                    End Select

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : フォームクローズ
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームのインスタンスを破棄する
    '--------------------------------------------------------------------
    Private Sub frmSysSystem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Exitボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : フォームを閉じる
    '--------------------------------------------------------------------
    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click

        Try

            Dim blnMach As Boolean = False
            Dim blnCarg As Boolean = False

            Dim intKubun As Integer = IIf(optMachinery.Checked, 0, 1)    ''0:Machinery　1:Cargo

            If cmdSave.Enabled = True Then

                ''現在の画面の情報を作業用構造体に退避
                If intKubun = 0 Then
                    Call mSetWork(mudtWorkGroupM, intKubun)

                    ''図番、船番、コメントは区別はパート区別なし　ver.1.4.0 2011.09.21
                    mudtWorkGroupC.udtGroup.strDrawNo = mudtWorkGroupM.udtGroup.strDrawNo
                    mudtWorkGroupC.udtGroup.strShipNo = mudtWorkGroupM.udtGroup.strShipNo
                    mudtWorkGroupC.udtGroup.strComment = mudtWorkGroupM.udtGroup.strComment
                ElseIf intKubun = 1 Then
                    Call mSetWork(mudtWorkGroupC, intKubun)

                    ''図番、船番、コメントは区別はパート区別なし　ver.1.4.0 2011.09.21
                    mudtWorkGroupM.udtGroup.strDrawNo = mudtWorkGroupC.udtGroup.strDrawNo
                    mudtWorkGroupM.udtGroup.strShipNo = mudtWorkGroupC.udtGroup.strShipNo
                    mudtWorkGroupM.udtGroup.strComment = mudtWorkGroupC.udtGroup.strComment
                End If

                ''データが変更されているかチェック
                blnMach = mChkStructureEquals(mudtWorkGroupM, gudt.SetChGroupSetM)
                blnCarg = mChkStructureEquals(mudtWorkGroupC, gudt.SetChGroupSetC)

                ''データが変更されている場合
                If (Not blnMach) Or (Not blnCarg) Then

                    ''変更されている場合はメッセージ表示
                    Select Case MessageBox.Show("Setting has been changed." & vbNewLine & _
                                                "Do you save the changes?", Me.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                        Case Windows.Forms.DialogResult.Yes

                            ''入力チェック
                            If Not mChkInput() Then
                                Return
                            End If

                            ''変更されている場合は設定を更新する
                            If Not blnMach Then Call mCopyStructure(mudtWorkGroupM, gudt.SetChGroupSetM)
                            If Not blnCarg Then Call mCopyStructure(mudtWorkGroupC, gudt.SetChGroupSetC)

                            ''全体更新フラグ設定
                            gblnUpdateAll = True
                            If Not blnMach Then gudt.SetEditorUpdateInfo.udtSave.bytGroupM = 1
                            If Not blnCarg Then gudt.SetEditorUpdateInfo.udtSave.bytGroupC = 1
                            If Not blnMach Then gudt.SetEditorUpdateInfo.udtCompile.bytGroupM = 1
                            If Not blnCarg Then gudt.SetEditorUpdateInfo.udtCompile.bytGroupC = 1

                        Case Windows.Forms.DialogResult.No

                            ''何もしない

                        Case Windows.Forms.DialogResult.Cancel

                            ''画面を閉じない
                            Return

                    End Select

                End If

            End If

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： Colorボタン クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try

            Dim getColor As Color, intColorNo As Integer
            Dim strName As String, intNo As Integer

            strName = CType(sender, System.Windows.Forms.Button).Name
            intNo = Integer.Parse(strName.Substring(8, 2))

            ''カラー選択画面　表示
            intColorNo = frmChListViewColorPallet.gShow(mGroupInfo(CCInt(mtxtGroupIndex(intNo - 1).Text)).intColorNo, _
                                                        getColor, Me)

            If intColorNo >= 0 Then

                ''背景色 変更
                'mFrame(intNo - 1).BackColor = getColor

                'T.Ueki 背景色から文字色に変更 2014/5/27
                mtxtGroupName(intNo - 1).ForeColor = getColor
                ''色番号　退避
                mGroupInfo(CCInt(mtxtGroupIndex(intNo - 1).Text)).intColorNo = intColorNo

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： PrintSettingsボタン クリック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdPrintSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrintSettings.Click

        Try

            Dim intKubun As Integer = IIf(optMachinery.Checked, 0, 1)    ''0:Machinery　1:Cargo

            Dim strDrawNo As String = mPrintSetting(intKubun).strDrawNo
            Dim strComment As String = mPrintSetting(intKubun).strComment
            Dim strShip As String = mPrintSetting(intKubun).strShip

            If frmChListPrintSetting.gShow(strDrawNo, strComment, strShip, Me) = 0 Then

                ''図番、船番、コメントは区別はパート区別なし　ver.1.4.0 2011.09.21
                ''Machinery、Cargo両方にセット
                mPrintSetting(0).strDrawNo = strDrawNo
                mPrintSetting(0).strComment = strComment
                mPrintSetting(0).strShip = strShip

                mPrintSetting(1).strDrawNo = strDrawNo
                mPrintSetting(1).strComment = strComment
                mPrintSetting(1).strShip = strShip

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： ---ボタン クリック(チャンネル登録画面へ展開する)
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub cmdList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Try
            Dim strName As String, intNo As Integer, iAns As Integer
            Dim intIndex As Integer
            Dim strGroupName1(gCstChannelGroupMax - 1) As String
            Dim strGroupName2(gCstChannelGroupMax - 1) As String
            Dim intGroupNo1(gCstChannelGroupMax - 1) As Integer
            Dim intGroupNo2(gCstChannelGroupMax - 1) As Integer
            Dim intGroupIdx(gCstChannelGroupMax - 1) As Integer

            Dim intKubun As Integer = IIf(optMachinery.Checked, 0, 1)    ''0:Machinery　1:Cargo

            strName = CType(sender, System.Windows.Forms.Button).Name
            intNo = Integer.Parse(strName.Substring(7, 2))

            ''グループ名称, 番号
            For i As Integer = 0 To gCstChannelGroupMax - 1

                intIndex = CCInt(mtxtGroupIndex(i).Text)
                intGroupIdx(i) = intIndex

                If optMachinery.Checked Then
                    ''Machinery
                    intGroupNo1(i) = CCInt(mtxtGroupNo(i).Text)

                    intGroupNo2(i) = mudtWorkGroupC.udtGroup.udtGroupInfo(intIndex).shtGroupNo

                    strGroupName1(i) = mtxtGroupName(i).Text.Trim
                    'strGroupName1(i) = mtxtGroupName(i).Text    '2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更
                    strGroupName1(i) = strGroupName1(i).Replace(vbCrLf, "")

                    strGroupName2(i) = gGetString(mudtWorkGroupC.udtGroup.udtGroupInfo(intIndex).strName1)
                    strGroupName2(i) += " "
                    strGroupName2(i) += gGetString(mudtWorkGroupC.udtGroup.udtGroupInfo(intIndex).strName2)
                    strGroupName2(i) += " "
                    strGroupName2(i) += gGetString(mudtWorkGroupC.udtGroup.udtGroupInfo(intIndex).strName3)
                    strGroupName2(i) = strGroupName2(i).Replace(vbCrLf, "")
                Else
                    ''Cargo
                    intGroupNo1(i) = mudtWorkGroupM.udtGroup.udtGroupInfo(intIndex).shtGroupNo

                    intGroupNo2(i) = CCInt(mtxtGroupNo(i).Text)

                    strGroupName1(i) = gGetString(mudtWorkGroupM.udtGroup.udtGroupInfo(intIndex).strName1)
                    strGroupName1(i) += " "
                    strGroupName1(i) += gGetString(mudtWorkGroupM.udtGroup.udtGroupInfo(intIndex).strName2)
                    strGroupName1(i) += " "
                    strGroupName1(i) += gGetString(mudtWorkGroupM.udtGroup.udtGroupInfo(intIndex).strName3)
                    strGroupName1(i) = strGroupName1(i).Replace(vbCrLf, "")

                    strGroupName2(i) = mtxtGroupName(i).Text.Trim
                    'strGroupName2(i) = mtxtGroupName(i).Text    '2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更
                    strGroupName2(i) = strGroupName2(i).Replace(vbCrLf, "")
                End If

            Next

            ''チャンネル一覧画面表示 ----------------------------------------------------
            iAns = frmChListChannelList.gShow(CCInt(mtxtGroupNo(intNo - 1).Text), _
                                              CCInt(mtxtGroupIndex(intNo - 1).Text), _
                                              strGroupName1, strGroupName2, _
                                              intGroupNo1, intGroupNo2, _
                                              intGroupIdx, intKubun, Me)


        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループ名称エリア ダブルクリック(チャンネル登録画面へ展開する)
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtGroupName_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            Dim strName As String, intNo As Integer, iAns As Integer
            Dim intIndex As Integer
            Dim strGroupName1(gCstChannelGroupMax - 1) As String
            Dim strGroupName2(gCstChannelGroupMax - 1) As String
            Dim intGroupNo1(gCstChannelGroupMax - 1) As Integer
            Dim intGroupNo2(gCstChannelGroupMax - 1) As Integer
            Dim intGroupIdx(gCstChannelGroupMax - 1) As Integer

            Dim intKubun As Integer = IIf(optMachinery.Checked, 0, 1)    ''0:Machinery　1:Cargo

            strName = CType(sender, System.Windows.Forms.TextBox).Name
            intNo = Integer.Parse(strName.Substring(12, 2))

            ''グループ名称, 番号
            For i As Integer = 0 To gCstChannelGroupMax - 1

                intIndex = CCInt(mtxtGroupIndex(i).Text)
                intGroupIdx(i) = intIndex

                If optMachinery.Checked Then
                    ''Machinery
                    intGroupNo1(i) = CCInt(mtxtGroupNo(i).Text)
                    intGroupNo2(i) = mudtWorkGroupM.udtGroup.udtGroupInfo(intIndex).shtGroupNo


                    strGroupName1(i) = mtxtGroupName(i).Text.Trim
                    'strGroupName1(i) = mtxtGroupName(i).Text     '2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更
                    strGroupName1(i) = strGroupName1(i).Replace(vbCrLf, "")

                    strGroupName2(i) = gGetString(mudtWorkGroupM.udtGroup.udtGroupInfo(intIndex).strName1)
                    strGroupName2(i) += " "
                    strGroupName2(i) += gGetString(mudtWorkGroupM.udtGroup.udtGroupInfo(intIndex).strName2)
                    strGroupName2(i) += " "
                    strGroupName2(i) += gGetString(mudtWorkGroupM.udtGroup.udtGroupInfo(intIndex).strName3)
                    strGroupName2(i) = strGroupName2(i).Replace(vbCrLf, "")
                Else
                    ''Cargo
                    intGroupNo1(i) = mudtWorkGroupC.udtGroup.udtGroupInfo(intIndex).shtGroupNo

                    intGroupNo2(i) = CCInt(mtxtGroupNo(i).Text)

                    strGroupName1(i) = gGetString(mudtWorkGroupC.udtGroup.udtGroupInfo(intIndex).strName1)
                    strGroupName1(i) += " "
                    strGroupName1(i) += gGetString(mudtWorkGroupC.udtGroup.udtGroupInfo(intIndex).strName2)
                    strGroupName1(i) += " "
                    strGroupName1(i) += gGetString(mudtWorkGroupC.udtGroup.udtGroupInfo(intIndex).strName3)
                    strGroupName1(i) = strGroupName1(i).Replace(vbCrLf, "")

                    strGroupName2(i) = mtxtGroupName(i).Text.Trim
                    'strGroupName2(i) = mtxtGroupName(i).Text    '2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更
                    strGroupName2(i) = strGroupName2(i).Replace(vbCrLf, "")
                End If

            Next

            ''チャンネル一覧画面表示 ----------------------------------------------------
            iAns = frmChListChannelList.gShow(CCInt(mtxtGroupNo(intNo - 1).Text), _
                                              CCInt(mtxtGroupIndex(intNo - 1).Text), _
                                              strGroupName1, strGroupName2, _
                                              intGroupNo1, intGroupNo2, _
                                              intGroupIdx, intKubun, Me)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループ番号エリア 入力チェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtGroupNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

        Try

            Dim strName As String, intNo As Integer

            strName = CType(sender, System.Windows.Forms.TextBox).Name
            intNo = Integer.Parse(strName.Substring(5, 2))

            ' '' ↓↓↓ K.Tanigawa 2012/01/12 チェック範囲を 1-99 → 0-99 に修正。グループ番号'0'はグループ無し（データは0xFF)
            If Val(mtxtGroupNo(intNo - 1).Text) < 0 Or Val(mtxtGroupNo(intNo - 1).Text) > 99 Then

                MsgBox("Please set Group No. '1'-'99'.", MsgBoxStyle.Exclamation, "Channel List")
                e.Cancel = True

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループ番号エリア フォーマット
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtGroupNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)

        Try

            Dim strName As String, intNo As Integer

            strName = CType(sender, System.Windows.Forms.TextBox).Name
            intNo = Integer.Parse(strName.Substring(5, 2))

            If mtxtGroupNo(intNo - 1).Text <> "" Then

                mtxtGroupNo(intNo - 1).Text = CCInt(mtxtGroupNo(intNo - 1).Text).ToString("00")

                mGroupInfo(CCInt(mtxtGroupIndex(intNo - 1).Text)).intGroupNo = CCInt(mtxtGroupNo(intNo - 1).Text)
                ' '' ↓↓↓ K.Tanigawa 2012/01/12 グループ無し処理追加。グループ番号'0'はグループ無し（データは0xFFFF)
                If mGroupInfo(CCInt(mtxtGroupIndex(intNo - 1).Text)).intGroupNo = 0 Then
                    mGroupInfo(CCInt(mtxtGroupIndex(intNo - 1).Text)).intGroupNo = &HFFFF
                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループ番号エリア 入力制限
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtGroupNo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Try

            If gCheckTextInput(2, sender, e.KeyChar, True) Then
                e.Handled = True
                Exit Sub
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： グループ名称エリア 入力制限
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub txtGroupName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        Try

            If Asc(e.KeyChar) >= 0 And Asc(e.KeyChar) <= 31 And Asc(e.KeyChar) <> 13 Then Exit Sub

            Dim strName As String, intNo As Integer
            Dim strValue As String, strLine As String = ""
            Dim p1 As Integer, p2 As Integer
            Dim SelectionStart As Integer, idx As Integer

            strName = CType(sender, System.Windows.Forms.TextBox).Name
            intNo = Integer.Parse(strName.Substring(12, 2))
            strValue = mtxtGroupName(intNo - 1).Text
            SelectionStart = CType(sender, System.Windows.Forms.TextBox).SelectionStart

            ''------------------------------------------------
            p1 = strValue.IndexOf(vbCrLf)               ''p1 <-- 1番目のvbCrLfのポインタ
            If p1 >= 0 Then
                p2 = strValue.IndexOf(vbCrLf, p1 + 2)   ''p2 <-- 2番目のvbCrLfのポインタ
            End If

            ''どの行を編集中かを見極める
            If p1 < 0 Then
                idx = 1 ''1行目
            Else
                If SelectionStart <= p1 Then
                    idx = 1 ''1行目
                Else
                    If p2 < 0 Then
                        idx = 2 ''2行目

                    Else
                        If SelectionStart <= p2 Then
                            idx = 2 ''2行目
                        Else
                            idx = 3 ''3行目
                        End If

                    End If
                End If
            End If

            ''編集中の行をGETする
            If idx = 1 Then
                If p1 >= 0 Then
                    strLine = strValue.Substring(0, p1)
                Else
                    strLine = strValue
                End If

            ElseIf idx = 2 Then
                If p2 >= 0 Then
                    strLine = strValue.Substring(p1 + 2, p2 - (p1 + 2))
                Else
                    strLine = strValue.Substring(p1 + 2)
                End If

            ElseIf idx = 3 Then
                strLine = strValue.Substring(p2 + 2)
            End If

            If Asc(e.KeyChar) = 13 Then
                ''改行コード
            Else
                If LenB(strLine) >= 16 Then
                    e.Handled = True
                    Exit Sub
                End If
            End If

            ''------------------------------------------------
            strValue = strValue.Replace(vbCrLf, "")

            If LenB(strValue) >= 32 Then
                e.Handled = True
                Exit Sub
            End If

            ''------------------------------------------------
            If Asc(e.KeyChar) = 13 Then     ''改行コード
                If mtxtGroupName(intNo - 1).Lines.Count > 2 Then
                    e.Handled = True
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： マシナリー/カーゴ　変更時
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub optMachinery_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optMachinery.CheckedChanged

        Try
            If mflgSkip Then Exit Sub

            If optMachinery.Checked Then      ''マシナリー　選択

                ''入力チェック
                If Not mChkInput() Then Return

                ''カーゴの情報を退避
                Call mSetWork(mudtWorkGroupC, 1)

                ''図番、船番、コメントは区別はパート区別なし　ver.1.4.0 2011.09.21
                mudtWorkGroupM.udtGroup.strDrawNo = mudtWorkGroupC.udtGroup.strDrawNo
                mudtWorkGroupM.udtGroup.strShipNo = mudtWorkGroupC.udtGroup.strShipNo
                mudtWorkGroupM.udtGroup.strComment = mudtWorkGroupC.udtGroup.strComment

                ''画面再設定
                Call mSetDisplay(mudtWorkGroupM, 0)

            ElseIf optCargo.Checked Then    ''カーゴ選択

                ''入力チェック
                If Not mChkInput() Then Return

                ''マシナリーの情報を退避
                Call mSetWork(mudtWorkGroupM, 0)

                ''図番、船番、コメントは区別はパート区別なし　ver.1.4.0 2011.09.21
                mudtWorkGroupC.udtGroup.strDrawNo = mudtWorkGroupM.udtGroup.strDrawNo
                mudtWorkGroupC.udtGroup.strShipNo = mudtWorkGroupM.udtGroup.strShipNo
                mudtWorkGroupC.udtGroup.strComment = mudtWorkGroupM.udtGroup.strComment

                ''画面再設定
                Call mSetDisplay(mudtWorkGroupC, 1)

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Over Viewボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 表示位置設定画面を開く
    '--------------------------------------------------------------------
    Private Sub cmdOverView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOverView.Click

        Try

            Dim intKubun As Integer = IIf(optMachinery.Checked, 0, 1)    ''0:Machinery　1:Cargo
            Dim intCnt As Integer = 0
            Dim intDispIndex(35) As Integer

            ''グループNoの整合性をチェックする
            If Not mChkInput() Then Return

            ''グループ名称退避
            For i As Integer = 0 To gCstChannelGroupMax - 1

                mGroupInfo(CCInt(mtxtGroupIndex(i).Text)).strName = mtxtGroupName(i).Text

            Next

            ''表示位置設定画面 ---------------------------------------------------
            If frmChListViewGroupDispIndex.gShow(mGroupInfo, Me) = 0 Then

                If intKubun = 0 Then
                    ''Machinery
                    For i As Integer = 0 To gCstChannelGroupMax - 1

                        mudtWorkGroupM.udtGroup.udtGroupInfo(i).shtDisplayPosition = intDispIndex(i)

                    Next

                ElseIf intKubun = 1 Then
                    ''Cargo
                    For i As Integer = 0 To gCstChannelGroupMax - 1

                        mudtWorkGroupC.udtGroup.udtGroupInfo(i).shtDisplayPosition = intDispIndex(i)

                    Next

                End If

                ''再表示(OPS表示順に並び替え）

                ''グループ表示位置GET
                For i As Integer = 0 To UBound(intDispIndex)
                    intDispIndex(i) = IIf(mGroupInfo(i).intDispIndex = gCstCodeChGroupDisplayPositionNothing, -1, mGroupInfo(i).intDispIndex - 1)
                Next

                ''グループ表示位置が設定なしの場合、昇順に割り振る
                Call SetGroupDispIndex(intDispIndex)

                For i As Integer = 0 To gCstChannelGroupMax - 1

                    ''グループ構造体インデックス（非表示）
                    mtxtGroupIndex(intDispIndex(i)).Text = i.ToString

                    ''グループ番号
                    ' '' ↓↓↓ K.Tanigawa 2012/01/12 グループ無し処理追加。グループ番号'0'はグループ無し（データは0xFF)
                    If mGroupInfo(i).intGroupNo = &HFFFF Then
                        mtxtGroupNo(intDispIndex(i)).Text = "00"
                    Else
                        mtxtGroupNo(intDispIndex(i)).Text = mGroupInfo(i).intGroupNo.ToString("00")
                    End If

                    ''グループ名称
                    mtxtGroupName(intDispIndex(i)).Text = mGroupInfo(i).strName

                    ''カラー設定     文字色に変更          2014.11.18
                    'mFrame(intDispIndex(i)).BackColor = mColorInfo(mGroupInfo(i).intColorNo)
                    mFrame(intDispIndex(i)).ForeColor = mColorInfo(mGroupInfo(i).intColorNo)

                Next

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub


#End Region

#Region "内部関数"

    '--------------------------------------------------------------------
    ' 機能      : 入力チェック
    ' 返り値    : True:入力OK、False:入力NG
    ' 引き数    : なし
    ' 機能説明  : 入力チェックを行う
    '--------------------------------------------------------------------
    Private Function mChkInput() As Boolean

        Try

            ''グループ番号の抜けをチェック
            For i As Integer = LBound(mtxtGroupNo) To UBound(mtxtGroupNo)

                ''共通数値入力チェック
                If Not gChkInputNum(mtxtGroupNo(i), 0, 99, "Group No", False, True) Then Return False ' '' K.Tanigawa 2012/01/12 グループNo.1-99 を 0 -99 に変更。0はグループ無し。0xFF

            Next

            ''グループ番号のダブりをチェック
            For i As Integer = LBound(mtxtGroupNo) To UBound(mtxtGroupNo) - 1

                For j As Integer = i + 1 To UBound(mtxtGroupNo)

                    If mtxtGroupNo(i).Text = mtxtGroupNo(j).Text And mtxtGroupNo(j).Text <> 0 Then   ' '' K.Tanigawa 2012/01/12 グループNo.0の重複許可。0はグループ無し。0xFF


                        Call MessageBox.Show("The Group No overlaps. " & vbCrLf & _
                                             CStr(i + 1) & "th and " & CStr(j + 1) & "th", _
                                             "Input error", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False

                    End If

                Next

            Next

            ''入力不可文字置き換え
            For i As Integer = LBound(mtxtGroupName) To UBound(mtxtGroupName)

                ''共通テキスト入力チェック
                If Not gChkInputText(mtxtGroupName(i), "GROUP Name", True, True) Then Return False

            Next

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納(作業領域）
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    '           : ARG2 - (I ) 0:Machinery　1:Cargo
    ' 機能説明  : 作業用構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetWork(ByRef udtSet As gTypSetChGroupSet, ByVal intKubun As Integer)

        Try
            Dim intIndex As Integer = 0
            Dim intPoint1 As Integer, intPoint2 As Integer

            With udtSet.udtGroup

                For i As Integer = 0 To UBound(mtxtGroupName)

                    ''Draw No.
                    .strDrawNo = mPrintSetting(intKubun).strDrawNo

                    ''船名
                    .strShipNo = mPrintSetting(intKubun).strShip

                    ''コメント
                    .strComment = mPrintSetting(intKubun).strComment

                    ''構造体インデックス
                    intIndex = CCInt(mtxtGroupIndex(i).Text)

                    ''グループ名称
                    intPoint1 = mtxtGroupName(i).Text.IndexOf(vbCrLf)

                    If intPoint1 >= 0 Then

                        ''1行目
                        .udtGroupInfo(intIndex).strName1 = mtxtGroupName(i).Text.Substring(0, intPoint1)

                        If .udtGroupInfo(intIndex).strName1.Length > 16 Then
                            .udtGroupInfo(intIndex).strName1 = .udtGroupInfo(intIndex).strName1.Substring(0, 16)
                        End If

                        intPoint2 = mtxtGroupName(i).Text.Substring(intPoint1 + 2).IndexOf(vbCrLf)
                        If intPoint2 >= 0 Then

                            ''2行目
                            .udtGroupInfo(intIndex).strName2 = mtxtGroupName(i).Text.Substring(intPoint1 + 2, intPoint1 + intPoint2 - intPoint1)

                            If .udtGroupInfo(intIndex).strName2.Length > 16 Then
                                .udtGroupInfo(intIndex).strName2 = .udtGroupInfo(intIndex).strName2.Substring(0, 16)
                            End If

                            ''3行目
                            .udtGroupInfo(intIndex).strName3 = mtxtGroupName(i).Text.Substring(intPoint1 + intPoint2 + 2 + 2, mtxtGroupName(i).Text.Length - intPoint1 - intPoint2 - 2 - 2)

                            If .udtGroupInfo(intIndex).strName3.Length > 16 Then
                                .udtGroupInfo(intIndex).strName3 = .udtGroupInfo(intIndex).strName3.Substring(0, 16)
                            End If

                        Else
                            ''2行目
                            .udtGroupInfo(intIndex).strName2 = mtxtGroupName(i).Text.Substring(intPoint1 + 2, mtxtGroupName(i).Text.Length - intPoint1 - 2)

                            If .udtGroupInfo(intIndex).strName2.Length > 16 Then
                                .udtGroupInfo(intIndex).strName2 = .udtGroupInfo(intIndex).strName2.Substring(0, 16)
                            End If

                            ''3行目
                            .udtGroupInfo(intIndex).strName3 = ""
                        End If

                    Else
                        ''改行コードがない時
                        If mtxtGroupName(i).Text.Trim.Length <= 16 Then
                            .udtGroupInfo(intIndex).strName1 = mtxtGroupName(i).Text.Trim
                            .udtGroupInfo(intIndex).strName2 = ""
                            .udtGroupInfo(intIndex).strName3 = ""

                        ElseIf mtxtGroupName(i).Text.Trim.Length >= 17 And mtxtGroupName(i).Text.Trim.Length <= 32 Then
                            .udtGroupInfo(intIndex).strName1 = mtxtGroupName(i).Text.Trim.Substring(0, 16) & vbCrLf
                            .udtGroupInfo(intIndex).strName2 = mtxtGroupName(i).Text.Trim.Substring(16)
                            .udtGroupInfo(intIndex).strName3 = ""
                        Else
                            .udtGroupInfo(intIndex).strName1 = mtxtGroupName(i).Text.Trim.Substring(0, 16) & vbCrLf
                            .udtGroupInfo(intIndex).strName2 = mtxtGroupName(i).Text.Trim.Substring(16, 16) & vbCrLf
                            .udtGroupInfo(intIndex).strName3 = mtxtGroupName(i).Text.Trim.Substring(32)
                        End If

                        If .udtGroupInfo(intIndex).strName3.Length > 16 Then
                            .udtGroupInfo(intIndex).strName3 = .udtGroupInfo(intIndex).strName3.Substring(0, 16)
                        End If

                    End If

                    ''カラー設定
                    .udtGroupInfo(intIndex).shtColor = mGroupInfo(intIndex).intColorNo

                    ''グループ番号
                    .udtGroupInfo(intIndex).shtGroupNo = CCInt(mtxtGroupNo(i).Text)
                    '' ↓↓↓ K.Tanigawa 2012/01/12 グループ無し処理を追加
                    If .udtGroupInfo(intIndex).shtGroupNo = 0 Then
                        .udtGroupInfo(intIndex).shtGroupNo = &H0
                    End If

                    ''表示位置インデックス
                    .udtGroupInfo(intIndex).shtDisplayPosition = mGroupInfo(intIndex).intDispIndex

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : グループ表示位置セット
    ' 返り値    : なし
    ' 引き数    : ARG1 - (IO) グループ表示位置
    ' 機能説明  : グループ表示位置が設定なしの場合、昇順に割り振る
    '--------------------------------------------------------------------
    Private Sub SetGroupDispIndex(ByRef DispIndex() As Integer)

        Dim iCnt As Integer = 0, iFlag As Boolean = False

        For i As Integer = 0 To UBound(DispIndex)

            If DispIndex(i) = -1 Then

                iFlag = False : iCnt = 0
                Do Until iFlag

                    iFlag = True
                    For j As Integer = 0 To UBound(DispIndex)

                        If DispIndex(j) = iCnt Then
                            iCnt += 1
                            iFlag = False
                            Exit For
                        End If

                    Next

                Loop

                DispIndex(i) = iCnt
                iCnt += 1

            End If

        Next

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示(グループ）
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) システム設定構造体
    '           : ARG2 - (I ) 0:Machinery　1:Cargo
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetChGroupSet, ByVal intKubun As Integer)

        Try

            Dim intDispIndex(gCstChannelGroupMax - 1) As Integer

            With udtSet.udtGroup

                ''グループ表示位置GET ------------------------------------
                For i As Integer = 0 To UBound(.udtGroupInfo)

                    If .udtGroupInfo(i).shtDisplayPosition = gCstCodeChGroupDisplayPositionNothing Or _
                       .udtGroupInfo(i).shtDisplayPosition = gCstCodeChGroupDisplayPositionNothingConvert Then
                        intDispIndex(i) = -1
                    Else
                        intDispIndex(i) = .udtGroupInfo(i).shtDisplayPosition - 1
                    End If

                Next

                ''グループ表示位置が設定なしの場合、昇順に割り振る(0～35)
                Call SetGroupDispIndex(intDispIndex)
                ''---------------------------------------------------------

                ''Draw No.
                mPrintSetting(intKubun).strDrawNo = gGetString(.strDrawNo)

                ''船名
                mPrintSetting(intKubun).strShip = gGetString(.strShipNo)

                ''コメント
                mPrintSetting(intKubun).strComment = gGetString(.strComment)

                For i As Integer = 0 To UBound(.udtGroupInfo)

                    ''グループ情報 退避 ----------------------------------
                    mGroupInfo(i).intGroupNo = .udtGroupInfo(i).shtGroupNo
                    mGroupInfo(i).intColorNo = .udtGroupInfo(i).shtColor
                    mGroupInfo(i).intDispIndex = .udtGroupInfo(i).shtDisplayPosition
                    ''----------------------------------------------------

                    ''グループ構造体インデックス（非表示）
                    mtxtGroupIndex(intDispIndex(i)).Text = i.ToString

                    ''グループ番号
                    '' ↓↓↓ K.Tanigawa 2012/01/12 グループ無し処理を追加
                    If .udtGroupInfo(i).shtGroupNo = 0 Then
                        mtxtGroupNo(intDispIndex(i)).Text = "00"             ' Group無し："00"
                    Else
                        mtxtGroupNo(intDispIndex(i)).Text = .udtGroupInfo(i).shtGroupNo.ToString("00")  ' '1-99'
                    End If

                    ''グループ名称
                    .udtGroupInfo(i).strName1 = gGetString(.udtGroupInfo(i).strName1)
                    .udtGroupInfo(i).strName2 = gGetString(.udtGroupInfo(i).strName2)
                    .udtGroupInfo(i).strName3 = gGetString(.udtGroupInfo(i).strName3)

                    'Ver2.0.2.8 スペースを削除しないように変更
                    If .udtGroupInfo(i).strName1 <> "" Then
                        mtxtGroupName(intDispIndex(i)).Text = .udtGroupInfo(i).strName1.Trim
                        'mtxtGroupName(intDispIndex(i)).Text = .udtGroupInfo(i).strName1     ' 2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更

                        If .udtGroupInfo(i).strName2 <> "" Then

                            mtxtGroupName(intDispIndex(i)).Text += vbCrLf & .udtGroupInfo(i).strName2.Trim
                            'mtxtGroupName(intDispIndex(i)).Text += vbCrLf & .udtGroupInfo(i).strName2 ' 2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更

                            If .udtGroupInfo(i).strName3 <> "" Then

                                mtxtGroupName(intDispIndex(i)).Text += vbCrLf & .udtGroupInfo(i).strName3.Trim
                                'mtxtGroupName(intDispIndex(i)).Text += vbCrLf & .udtGroupInfo(i).strName3   '2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更

                            End If

                        Else

                            If .udtGroupInfo(i).strName3 <> "" Then

                                mtxtGroupName(intDispIndex(i)).Text += vbCrLf & vbCrLf & .udtGroupInfo(i).strName3.Trim
                                'mtxtGroupName(intDispIndex(i)).Text += vbCrLf & vbCrLf & .udtGroupInfo(i).strName3  '2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更

                            End If

                        End If

                    Else

                        If .udtGroupInfo(i).strName2 <> "" Then

                            mtxtGroupName(intDispIndex(i)).Text = vbCrLf & .udtGroupInfo(i).strName2.Trim
                            'mtxtGroupName(intDispIndex(i)).Text = vbCrLf & .udtGroupInfo(i).strName2    '2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更

                            If .udtGroupInfo(i).strName3 <> "" Then

                                mtxtGroupName(intDispIndex(i)).Text += vbCrLf & .udtGroupInfo(i).strName3.Trim
                                'mtxtGroupName(intDispIndex(i)).Text += vbCrLf & .udtGroupInfo(i).strName3   '2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更

                            End If

                        Else

                            If .udtGroupInfo(i).strName3 <> "" Then

                                mtxtGroupName(intDispIndex(i)).Text = vbCrLf & vbCrLf & .udtGroupInfo(i).strName3.Trim
                                'mtxtGroupName(intDispIndex(i)).Text = vbCrLf & vbCrLf & .udtGroupInfo(i).strName3   '2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更

                            Else
                                mtxtGroupName(intDispIndex(i)).Text = ""
                            End If

                        End If

                    End If

                    ' ''カラー設定
                    'mFrame(intDispIndex(i)).BackColor = mColorInfo(mGroupInfo(i).intColorNo)

                    '背景色から文字色に変更 T.Ueki 2014/5/27
                    ''カラー設定
                    mtxtGroupName(intDispIndex(i)).ForeColor = mColorInfo(mGroupInfo(i).intColorNo)

                Next

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体複製
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) 複製元
    ' 　　　    : ARG1 - ( O) 複製先
    ' 機能説明  : 構造体を複製する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると単純に = では複製できないため関数を用意
    ' 　　　　  : ↑ = でやると配列部分が参照渡しになり（？）値更新時に両方更新されてしまう
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、この関数を使わずに = で処理しても良い
    '--------------------------------------------------------------------
    Private Sub mCopyStructure(ByVal udtSource As gTypSetChGroupSet, _
                               ByRef udtTarget As gTypSetChGroupSet)

        Try
            udtTarget.udtGroup.strDrawNo = udtSource.udtGroup.strDrawNo
            udtTarget.udtGroup.strShipNo = udtSource.udtGroup.strShipNo
            udtTarget.udtGroup.strComment = udtSource.udtGroup.strComment

            For j As Integer = LBound(udtSource.udtGroup.udtGroupInfo) To UBound(udtSource.udtGroup.udtGroupInfo)

                udtTarget.udtGroup.udtGroupInfo(j).shtGroupNo = udtSource.udtGroup.udtGroupInfo(j).shtGroupNo
                udtTarget.udtGroup.udtGroupInfo(j).strName1 = udtSource.udtGroup.udtGroupInfo(j).strName1
                udtTarget.udtGroup.udtGroupInfo(j).strName2 = udtSource.udtGroup.udtGroupInfo(j).strName2
                udtTarget.udtGroup.udtGroupInfo(j).strName3 = udtSource.udtGroup.udtGroupInfo(j).strName3
                udtTarget.udtGroup.udtGroupInfo(j).shtColor = udtSource.udtGroup.udtGroupInfo(j).shtColor
                udtTarget.udtGroup.udtGroupInfo(j).shtDisplayPosition = udtSource.udtGroup.udtGroupInfo(j).shtDisplayPosition
            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 構造体比較
    ' 返り値    : True:相違なし、False:相違あり
    ' 引き数    : ARG1 - (I ) 構造体１
    ' 　　　    : ARG1 - (I ) 構造体２
    ' 機能説明  : 構造体の設定値を比較する
    ' 備考　　  : 構造体メンバの中に構造体配列がいると Equals メソッドで正しい結果が得られないため関数を用意
    ' 　　　　  : 構造体メンバの中に構造体配列がいない場合は、 Equals メソッドで処理しても良いが一応これを使うこと
    ' 　　　　  : String文字列の比較には gCompareString を使用すること（単純な = だとNULL文字の有り無しで結果が変わってしまう）
    '--------------------------------------------------------------------
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetChGroupSet, _
                                         ByVal udt2 As gTypSetChGroupSet) As Boolean

        Try

            If Not gCompareString(Trim(udt1.udtGroup.strDrawNo), _
                                  Trim(udt2.udtGroup.strDrawNo)) Then Return False

            If Not gCompareString(Trim(udt1.udtGroup.strShipNo), _
                                  Trim(udt2.udtGroup.strShipNo)) Then Return False

            If Not gCompareString(Trim(udt1.udtGroup.strComment), _
                                  Trim(udt2.udtGroup.strComment)) Then Return False

            For j As Integer = LBound(udt1.udtGroup.udtGroupInfo) To UBound(udt1.udtGroup.udtGroupInfo)

                If udt1.udtGroup.udtGroupInfo(j).shtGroupNo <> udt2.udtGroup.udtGroupInfo(j).shtGroupNo Then Return False

                '2015.10.30 Ver1.7.5 ｽﾍﾟｰｽを削除しないように変更
                '      ｽﾍﾟｰｽの追加も変更とみなすため、ｽﾍﾟｰｽを削除しないﾌﾗｸﾞを追加
                'If Not gCompareString(udt1.udtGroup.udtGroupInfo(j).strName1, _
                '                  udt2.udtGroup.udtGroupInfo(j).strName1, False) Then Return False

                'If Not gCompareString(udt1.udtGroup.udtGroupInfo(j).strName2, _
                '                  udt2.udtGroup.udtGroupInfo(j).strName2, False) Then Return False

                'If Not gCompareString(udt1.udtGroup.udtGroupInfo(j).strName3, _
                '                  udt2.udtGroup.udtGroupInfo(j).strName3, False) Then Return False
                If Not gCompareString(Trim(udt1.udtGroup.udtGroupInfo(j).strName1), _
                                  Trim(udt2.udtGroup.udtGroupInfo(j).strName1)) Then Return False

                If Not gCompareString(Trim(udt1.udtGroup.udtGroupInfo(j).strName2), _
                                  Trim(udt2.udtGroup.udtGroupInfo(j).strName2)) Then Return False

                If Not gCompareString(Trim(udt1.udtGroup.udtGroupInfo(j).strName3), _
                                  Trim(udt2.udtGroup.udtGroupInfo(j).strName3)) Then Return False
                '//

                If udt1.udtGroup.udtGroupInfo(j).shtColor <> udt2.udtGroup.udtGroupInfo(j).shtColor Then Return False

                If udt1.udtGroup.udtGroupInfo(j).shtDisplayPosition <> udt2.udtGroup.udtGroupInfo(j).shtDisplayPosition Then Return False

            Next j

            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

#End Region

#Region "その他"

    '--------------------------------------------------------------------
    ' 機能      : ノーマルレンジ　クリア
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : AIチャンネルにて、ノーマルレンジにスケール値と同じ値が
    ' 　　　　　　設定されている場合、設定無しに書き換える
    ' 備考　　  : 隠しボタン
    '--------------------------------------------------------------------
    Private Sub cmdRangeClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRangeClear.Click

        Try

            For i As Integer = LBound(gudt.SetChInfo.udtChannel) To UBound(gudt.SetChInfo.udtChannel)

                With gudt.SetChInfo.udtChannel(i)

                    ''チャンネル有り
                    If .udtChCommon.shtChno <> 0 Then

                        If .udtChCommon.shtChType = gCstCodeChTypeAnalog Then

                            ''アナログCH
                            If .AnalogRangeHigh = .AnalogNormalHigh And .AnalogRangeLow = .AnalogNormalLow Then

                                .AnalogNormalHigh = gCstCodeChAlalogNormalRangeNothingHi
                                .AnalogNormalLow = gCstCodeChAlalogNormalRangeNothingLo

                            End If

                        ElseIf .udtChCommon.shtChType = gCstCodeChTypeValve Then

                            ''バルブCH
                            If .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO1 Or _
                               .udtChCommon.shtData = gCstCodeChDataTypeValveAI_DO2 Then

                                If .ValveAiDoRangeHigh = .ValveAiDoNormalHigh And .ValveAiDoRangeLow = .ValveAiDoNormalLow Then

                                    .ValveAiDoNormalHigh = gCstCodeChAlalogNormalRangeNothingHi
                                    .ValveAiDoNormalLow = gCstCodeChAlalogNormalRangeNothingLo

                                End If

                            ElseIf .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO1 Or _
                                   .udtChCommon.shtData = gCstCodeChDataTypeValveAI_AO2 Then

                                If .ValveAiAoRangeHigh = .ValveAiAoNormalHigh And .ValveAiAoRangeLow = .ValveAiAoNormalLow Then

                                    .ValveAiAoNormalHigh = gCstCodeChAlalogNormalRangeNothingHi
                                    .ValveAiAoNormalLow = gCstCodeChAlalogNormalRangeNothingLo

                                End If

                            End If

                        End If

                    End If

                End With

            Next

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

#End Region

   
    Private Sub txtGroupName02_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGroupName02.TextChanged

    End Sub

    Private Sub cmdList02_Click(sender As System.Object, e As System.EventArgs) Handles cmdList02.Click

    End Sub

    Private Sub Frame02_Enter(sender As System.Object, e As System.EventArgs) Handles Frame02.Enter

    End Sub

    Private Sub cmdList01_Click(sender As System.Object, e As System.EventArgs) Handles cmdList01.Click

    End Sub

    Private Sub cmdList03_Click(sender As System.Object, e As System.EventArgs) Handles cmdList03.Click

    End Sub

    Private Sub cmdList11_Click(sender As System.Object, e As System.EventArgs) Handles cmdList11.Click

    End Sub
End Class