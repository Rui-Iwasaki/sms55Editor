﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOpsScreenTitle
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

#Region "Windows フォーム デザイナによって生成されたコード "
    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.grdScreen = New Editor.clsDataGridViewPlus()
        Me.optCargo = New System.Windows.Forms.RadioButton()
        Me.optMachinery = New System.Windows.Forms.RadioButton()
        CType(Me.grdScreen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdExit
        '
        Me.cmdExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Location = New System.Drawing.Point(503, 487)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(113, 33)
        Me.cmdExit.TabIndex = 1
        Me.cmdExit.Text = "Exit"
        Me.cmdExit.UseVisualStyleBackColor = True
        '
        'cmdSave
        '
        Me.cmdSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(384, 486)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(113, 33)
        Me.cmdSave.TabIndex = 2
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'grdScreen
        '
        Me.grdScreen.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdScreen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdScreen.Location = New System.Drawing.Point(20, 23)
        Me.grdScreen.Name = "grdScreen"
        Me.grdScreen.RowTemplate.Height = 21
        Me.grdScreen.Size = New System.Drawing.Size(596, 451)
        Me.grdScreen.TabIndex = 3
        '
        'optCargo
        '
        Me.optCargo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optCargo.Appearance = System.Windows.Forms.Appearance.Button
        Me.optCargo.BackColor = System.Drawing.SystemColors.Control
        Me.optCargo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCargo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCargo.Location = New System.Drawing.Point(139, 487)
        Me.optCargo.Name = "optCargo"
        Me.optCargo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCargo.Size = New System.Drawing.Size(113, 33)
        Me.optCargo.TabIndex = 150
        Me.optCargo.TabStop = True
        Me.optCargo.Text = "Cargo"
        Me.optCargo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optCargo.UseVisualStyleBackColor = True
        '
        'optMachinery
        '
        Me.optMachinery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.optMachinery.Appearance = System.Windows.Forms.Appearance.Button
        Me.optMachinery.BackColor = System.Drawing.SystemColors.Control
        Me.optMachinery.Cursor = System.Windows.Forms.Cursors.Default
        Me.optMachinery.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMachinery.Location = New System.Drawing.Point(20, 487)
        Me.optMachinery.Name = "optMachinery"
        Me.optMachinery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optMachinery.Size = New System.Drawing.Size(113, 33)
        Me.optMachinery.TabIndex = 149
        Me.optMachinery.TabStop = True
        Me.optMachinery.Text = "Machinery"
        Me.optMachinery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optMachinery.UseVisualStyleBackColor = True
        '
        'frmOpsScreenTitle
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdExit
        Me.ClientSize = New System.Drawing.Size(636, 531)
        Me.ControlBox = False
        Me.Controls.Add(Me.optCargo)
        Me.Controls.Add(Me.optMachinery)
        Me.Controls.Add(Me.grdScreen)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmdSave)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Location = New System.Drawing.Point(4, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOpsScreenTitle"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SCREEN TITLE SET"
        CType(Me.grdScreen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdScreen As Editor.clsDataGridViewPlus
    Public WithEvents optCargo As System.Windows.Forms.RadioButton
    Public WithEvents optMachinery As System.Windows.Forms.RadioButton
#End Region

End Class