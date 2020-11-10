﻿Public Class frmExtEccEeengineerCall

#Region "変数定義"

    Private mudtSetExtAlmNew As gTypSetExtCommon = Nothing

#End Region

#Region "画面表示関数"

    '--------------------------------------------------------------------
    ' 機能      : 画面表示関数
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 本画面を表示する
    ' 備考      : 
    '--------------------------------------------------------------------
    Friend Sub gShow(ByRef frmOwner As Form)

        Try

            ''本画面表示
            Call gShowFormModelessForCloseWait2(Me, frmOwner)

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
    Private Sub frmExtEccEeengineerCall_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            ''画面設定
            Call mSetDisplay(gudt.SetExtAlarm.udtExtAlarmCommon)

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： chkAutoEeengineerCallOutputチェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub chkAutoEeengineerCallOutputAcc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoEeengineerCallOutputAcc.CheckedChanged

        Try

            ''両方チェック無しにはしない
            If Not chkAutoEeengineerCallOutputAcc.Checked Then

                If Not chkAutoEeengineerCallOutputExt.Checked Then

                    chkAutoEeengineerCallOutputAcc.Checked = True

                End If

            End If

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '----------------------------------------------------------------------------
    ' 機能説明  ： chkAutoEeengineerCallOutputExtチェック
    ' 引数      ： なし
    ' 戻値      ： なし
    '----------------------------------------------------------------------------
    Private Sub chkAutoEeengineerCallOutputExt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoEeengineerCallOutputExt.CheckedChanged

        Try

            ''両方チェック無しにはしない
            If Not chkAutoEeengineerCallOutputExt.Checked Then

                If Not chkAutoEeengineerCallOutputAcc.Checked Then

                    chkAutoEeengineerCallOutputExt.Checked = True

                End If

            End If

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

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetExtAlmNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetExtAlarm.udtExtAlarmCommon, mudtSetExtAlmNew) Then

                ''変更された場合は設定を更新する
                Call mCopyStructure(mudtSetExtAlmNew, gudt.SetExtAlarm.udtExtAlarmCommon)

                ''メッセージ表示
                Call MessageBox.Show("It saved.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)

                ''更新フラグ設定
                gblnUpdateAll = True
                gudt.SetEditorUpdateInfo.udtSave.bytExtAlarm = 1
                gudt.SetEditorUpdateInfo.udtCompile.bytExtAlarm = 1

            End If

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

            Me.Close()

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : Printボタンクリック
    ' 返り値    : なし
    ' 引き数    : なし
    ' 機能説明  : 画面印刷を行う
    '--------------------------------------------------------------------
    Private Sub cmdPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrint.Click

        Try

            Call gPrintScreen(True)

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
    Private Sub frmExtEccEeengineerCall_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Try

            ''設定値を比較用構造体に格納
            Call mSetStructure(mudtSetExtAlmNew)

            ''データが変更されているかチェック
            If Not mChkStructureEquals(gudt.SetExtAlarm.udtExtAlarmCommon, mudtSetExtAlmNew) Then

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
                        Call mCopyStructure(mudtSetExtAlmNew, gudt.SetExtAlarm.udtExtAlarmCommon)

                        ''更新フラグ設定
                        gblnUpdateAll = True
                        gudt.SetEditorUpdateInfo.udtSave.bytExtAlarm = 1
                        gudt.SetEditorUpdateInfo.udtCompile.bytExtAlarm = 1

                    Case Windows.Forms.DialogResult.No

                        ''何もしない

                    Case Windows.Forms.DialogResult.Cancel

                        ''画面を閉じない
                        e.Cancel = True

                End Select

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
    Private Sub frmExtEccEeengineerCall_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Try

            Me.Dispose()

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

            ''この画面は入力チェックなし
            Return True

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Function

    '--------------------------------------------------------------------
    ' 機能      : 設定値格納
    ' 返り値    : なし
    ' 引き数    : ARG1 - ( O) システム設定構造体
    ' 機能説明  : 構造体に設定を格納する
    '--------------------------------------------------------------------
    Private Sub mSetStructure(ByRef udtSet As gTypSetExtCommon)

        Try

            With udtSet

                ''機能 有無
                .shtEeengineerCall = IIf(optEeengineerCallFunction0.Checked, gBitSet(.shtEeengineerCall, 0, True), gBitSet(.shtEeengineerCall, 0, False))

                ''選択SW 有無
                .shtEeengineerCall = IIf(optEeengineerSelectSW0.Checked, gBitSet(.shtEeengineerCall, 1, True), gBitSet(.shtEeengineerCall, 1, False))

                ''Accept 機能有無
                .shtEeengineerCall = IIf(optAcceptFunction0.Checked, gBitSet(.shtEeengineerCall, 2, True), gBitSet(.shtEeengineerCall, 2, False))

                ''自動エンジニアコール出力先
                If chkAutoEeengineerCallOutputAcc.Checked = True And _
                   chkAutoEeengineerCallOutputExt.Checked = False Then

                    .shtEeengineerCall = gBitSet(.shtEeengineerCall, 3, False)
                    .shtEeengineerCall = gBitSet(.shtEeengineerCall, 4, False)

                ElseIf chkAutoEeengineerCallOutputAcc.Checked = True And _
                       chkAutoEeengineerCallOutputExt.Checked = True Then

                    .shtEeengineerCall = gBitSet(.shtEeengineerCall, 3, True)
                    .shtEeengineerCall = gBitSet(.shtEeengineerCall, 4, False)

                ElseIf chkAutoEeengineerCallOutputAcc.Checked = False And _
                       chkAutoEeengineerCallOutputExt.Checked = True Then

                    .shtEeengineerCall = gBitSet(.shtEeengineerCall, 3, False)
                    .shtEeengineerCall = gBitSet(.shtEeengineerCall, 4, True)

                End If

                ''Accept 鳴動パターン
                .shtEeengineerCall = IIf(optAcceptPattern0.Checked, gBitSet(.shtEeengineerCall, 5, False), gBitSet(.shtEeengineerCall, 5, True))

            End With

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
        End Try

    End Sub

    '--------------------------------------------------------------------
    ' 機能      : 設定値表示
    ' 返り値    : なし
    ' 引き数    : ARG1 - (I ) システム設定構造体
    ' 機能説明  : 構造体の設定を画面に表示する
    '--------------------------------------------------------------------
    Private Sub mSetDisplay(ByVal udtSet As gTypSetExtCommon)

        Try

            With udtSet

                ''機能 有無
                optEeengineerCallFunction0.Checked = IIf(gBitCheck(.shtEeengineerCall, 0), True, False)
                optEeengineerCallFunction1.Checked = IIf(gBitCheck(.shtEeengineerCall, 0), False, True)

                ''選択SW 有無
                optEeengineerSelectSW0.Checked = IIf(gBitCheck(.shtEeengineerCall, 1), True, False)
                optEeengineerSelectSW1.Checked = IIf(gBitCheck(.shtEeengineerCall, 1), False, True)

                ''Accept 機能有無
                optAcceptFunction0.Checked = IIf(gBitCheck(.shtEeengineerCall, 2), True, False)
                optAcceptFunction1.Checked = IIf(gBitCheck(.shtEeengineerCall, 2), False, True)

                ''自動エンジニアコール出力先
                If gBitCheck(.shtEeengineerCall, 3) = False And _
                   gBitCheck(.shtEeengineerCall, 4) = False Then

                    chkAutoEeengineerCallOutputAcc.Checked = True
                    chkAutoEeengineerCallOutputExt.Checked = False

                ElseIf gBitCheck(.shtEeengineerCall, 3) = True And _
                       gBitCheck(.shtEeengineerCall, 4) = False Then

                    chkAutoEeengineerCallOutputAcc.Checked = True
                    chkAutoEeengineerCallOutputExt.Checked = True

                ElseIf gBitCheck(.shtEeengineerCall, 3) = False And _
                       gBitCheck(.shtEeengineerCall, 4) = True Then

                    chkAutoEeengineerCallOutputAcc.Checked = False
                    chkAutoEeengineerCallOutputExt.Checked = True

                End If

                ''Accept 鳴動パターン
                optAcceptPattern0.Checked = IIf(gBitCheck(.shtEeengineerCall, 5), False, True)
                optAcceptPattern1.Checked = IIf(gBitCheck(.shtEeengineerCall, 5), True, False)

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
    Private Sub mCopyStructure(ByVal udtSource As gTypSetExtCommon, _
                               ByRef udtTarget As gTypSetExtCommon)

        Try

            udtTarget.shtEeengineerCall = udtSource.shtEeengineerCall

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
    Private Function mChkStructureEquals(ByVal udt1 As gTypSetExtCommon, _
                                         ByVal udt2 As gTypSetExtCommon) As Boolean

        Try

            If udt1.shtEeengineerCall <> udt2.shtEeengineerCall Then Return False

        Catch ex As Exception
            Call gOutputErrorLog(gMakeExceptionInfo(System.Reflection.MethodBase.GetCurrentMethod, ex.Message))
            Return False
        End Try

        Return True

    End Function

#End Region

End Class