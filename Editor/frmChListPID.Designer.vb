﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChListPID
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

    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更できます。
    'コード エディタを使用して、変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdNextCH = New System.Windows.Forms.Button()
        Me.cmdBeforeCH = New System.Windows.Forms.Button()
        Me.lblDummy = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.cmbRanDumy = New System.Windows.Forms.ComboBox()
        Me.lblRangeCaution = New System.Windows.Forms.Label()
        Me.chkPSDisp = New System.Windows.Forms.CheckBox()
        Me.chkPowerFactor = New System.Windows.Forms.CheckBox()
        Me.lblDecPoint = New System.Windows.Forms.Label()
        Me.fraHiHi0 = New System.Windows.Forms.GroupBox()
        Me.chkSover = New System.Windows.Forms.CheckBox()
        Me.chkSunder = New System.Windows.Forms.CheckBox()
        Me.txtStatusSF = New System.Windows.Forms.TextBox()
        Me.txtStatusLoLo = New System.Windows.Forms.TextBox()
        Me.txtStatusLo = New System.Windows.Forms.TextBox()
        Me.txtStatusHi = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.txtStatusHiHi = New System.Windows.Forms.TextBox()
        Me.txtValueHiHi = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbValueSensorFailure = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtExtGSensorFailure = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtGRep2SensorFailure = New System.Windows.Forms.TextBox()
        Me.txtGRep2LoLo = New System.Windows.Forms.TextBox()
        Me.txtGRep2Lo = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.txtGRep1SensorFailure = New System.Windows.Forms.TextBox()
        Me.txtGRep1LoLo = New System.Windows.Forms.TextBox()
        Me.txtExtGLoLo = New System.Windows.Forms.TextBox()
        Me.txtGRep2Hi = New System.Windows.Forms.TextBox()
        Me.txtDelaySensorFailure = New System.Windows.Forms.TextBox()
        Me.txtDelayLoLo = New System.Windows.Forms.TextBox()
        Me.txtGRep1Lo = New System.Windows.Forms.TextBox()
        Me.txtDelayLo = New System.Windows.Forms.TextBox()
        Me.txtValueLoLo = New System.Windows.Forms.TextBox()
        Me.txtGRep1Hi = New System.Windows.Forms.TextBox()
        Me.txtExtGLo = New System.Windows.Forms.TextBox()
        Me.txtValueLo = New System.Windows.Forms.TextBox()
        Me.txtExtGHi = New System.Windows.Forms.TextBox()
        Me.txtDelayHiHi = New System.Windows.Forms.TextBox()
        Me.txtDelayHi = New System.Windows.Forms.TextBox()
        Me.txtGRep2HiHi = New System.Windows.Forms.TextBox()
        Me.txtExtGHiHi = New System.Windows.Forms.TextBox()
        Me.txtValueHi = New System.Windows.Forms.TextBox()
        Me.txtGRep1HiHi = New System.Windows.Forms.TextBox()
        Me.Label128 = New System.Windows.Forms.Label()
        Me.Label129 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.lblHyphen = New System.Windows.Forms.Label()
        Me.txtRangeTo = New System.Windows.Forms.TextBox()
        Me.txtRangeFrom = New System.Windows.Forms.TextBox()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.cmbExtDevice = New System.Windows.Forms.ComboBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.txtPin = New System.Windows.Forms.TextBox()
        Me.txtPortNo = New System.Windows.Forms.TextBox()
        Me.txtFuNo = New System.Windows.Forms.TextBox()
        Me.cmbRangeType = New System.Windows.Forms.ComboBox()
        Me.lblRange = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbDataType = New System.Windows.Forms.ComboBox()
        Me.lblFuAddress = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtOffset = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.lblNorHyphen = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.txtDecimal = New System.Windows.Forms.TextBox()
        Me.txtLowNormal = New System.Windows.Forms.TextBox()
        Me.chkCenterGraph = New System.Windows.Forms.CheckBox()
        Me.txtHighNormal = New System.Windows.Forms.TextBox()
        Me.Label123 = New System.Windows.Forms.Label()
        Me.txtString = New System.Windows.Forms.TextBox()
        Me.lblDecimal = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.cmbAlmLvl = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTag = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbTime = New System.Windows.Forms.ComboBox()
        Me.chkMrepose = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtShareChid = New System.Windows.Forms.TextBox()
        Me.lblShareChid = New System.Windows.Forms.Label()
        Me.lblShareType = New System.Windows.Forms.Label()
        Me.cmbShareType = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblBitSet = New System.Windows.Forms.Label()
        Me.txtSP = New System.Windows.Forms.TextBox()
        Me.txtPLC = New System.Windows.Forms.TextBox()
        Me.txtEP = New System.Windows.Forms.TextBox()
        Me.txtAC = New System.Windows.Forms.TextBox()
        Me.txtRL = New System.Windows.Forms.TextBox()
        Me.txtWK = New System.Windows.Forms.TextBox()
        Me.txtGWS = New System.Windows.Forms.TextBox()
        Me.txtSio = New System.Windows.Forms.TextBox()
        Me.txtSC = New System.Windows.Forms.TextBox()
        Me.txtDmy = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.fraAlarm = New System.Windows.Forms.GroupBox()
        Me.txtDelayTimer = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtExtGroup = New System.Windows.Forms.TextBox()
        Me.txtR1 = New System.Windows.Forms.TextBox()
        Me.txtR2 = New System.Windows.Forms.TextBox()
        Me.cmbSysNo = New System.Windows.Forms.ComboBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtChNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chkSpTracking = New System.Windows.Forms.CheckBox()
        Me.chkCasMode = New System.Windows.Forms.CheckBox()
        Me.chkOutMode = New System.Windows.Forms.CheckBox()
        Me.txtPinNoDo = New System.Windows.Forms.TextBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtPidExtParaUnit4 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaUnit3 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaUnit2 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaUnit1 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaName4 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaName3 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaName2 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaName1 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaLow4 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaLow3 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaLow2 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaLow1 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaHigh4 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaHigh3 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaHigh2 = New System.Windows.Forms.TextBox()
        Me.txtPidExtParaHigh1 = New System.Windows.Forms.TextBox()
        Me.txtPidExtPara4 = New System.Windows.Forms.TextBox()
        Me.txtPidExtPara3 = New System.Windows.Forms.TextBox()
        Me.txtPidExtPara2 = New System.Windows.Forms.TextBox()
        Me.txtPidExtPara1 = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtPidDefGAP = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtPidDefTD = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtPidDefTI = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.txtPidDefPB = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtPidDefMvLow = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtPidDefMvHigh = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtPidDefSpLow = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtPidDefSpHigh = New System.Windows.Forms.TextBox()
        Me.txtPinDo = New System.Windows.Forms.TextBox()
        Me.txtPortNoDo = New System.Windows.Forms.TextBox()
        Me.txtFuNoDo = New System.Windows.Forms.TextBox()
        Me.lblDoStart = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.fraHiHi0.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.fraAlarm.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Location = New System.Drawing.Point(724, 539)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.Size = New System.Drawing.Size(113, 33)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.Text = "OK"
        Me.cmdOK.UseVisualStyleBackColor = True
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(847, 539)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(113, 33)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = True
        '
        'cmdNextCH
        '
        Me.cmdNextCH.BackColor = System.Drawing.SystemColors.Control
        Me.cmdNextCH.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdNextCH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdNextCH.Location = New System.Drawing.Point(138, 539)
        Me.cmdNextCH.Name = "cmdNextCH"
        Me.cmdNextCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdNextCH.Size = New System.Drawing.Size(113, 33)
        Me.cmdNextCH.TabIndex = 3
        Me.cmdNextCH.Text = "Next CH"
        Me.cmdNextCH.UseVisualStyleBackColor = True
        '
        'cmdBeforeCH
        '
        Me.cmdBeforeCH.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBeforeCH.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBeforeCH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBeforeCH.Location = New System.Drawing.Point(14, 539)
        Me.cmdBeforeCH.Name = "cmdBeforeCH"
        Me.cmdBeforeCH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBeforeCH.Size = New System.Drawing.Size(113, 33)
        Me.cmdBeforeCH.TabIndex = 2
        Me.cmdBeforeCH.Text = "Before CH"
        Me.cmdBeforeCH.UseVisualStyleBackColor = True
        '
        'lblDummy
        '
        Me.lblDummy.AutoSize = True
        Me.lblDummy.Location = New System.Drawing.Point(869, 3)
        Me.lblDummy.Name = "lblDummy"
        Me.lblDummy.Size = New System.Drawing.Size(95, 12)
        Me.lblDummy.TabIndex = 11
        Me.lblDummy.Text = "F5:DummySetting"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(952, 530)
        Me.TabControl1.TabIndex = 12
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(944, 504)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Common"
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox5.Controls.Add(Me.cmbRanDumy)
        Me.GroupBox5.Controls.Add(Me.lblRangeCaution)
        Me.GroupBox5.Controls.Add(Me.chkPSDisp)
        Me.GroupBox5.Controls.Add(Me.chkPowerFactor)
        Me.GroupBox5.Controls.Add(Me.lblDecPoint)
        Me.GroupBox5.Controls.Add(Me.fraHiHi0)
        Me.GroupBox5.Controls.Add(Me.lblHyphen)
        Me.GroupBox5.Controls.Add(Me.txtRangeTo)
        Me.GroupBox5.Controls.Add(Me.txtRangeFrom)
        Me.GroupBox5.Controls.Add(Me.txtUnit)
        Me.GroupBox5.Controls.Add(Me.txtStatus)
        Me.GroupBox5.Controls.Add(Me.cmbExtDevice)
        Me.GroupBox5.Controls.Add(Me.cmbUnit)
        Me.GroupBox5.Controls.Add(Me.txtPin)
        Me.GroupBox5.Controls.Add(Me.txtPortNo)
        Me.GroupBox5.Controls.Add(Me.txtFuNo)
        Me.GroupBox5.Controls.Add(Me.cmbRangeType)
        Me.GroupBox5.Controls.Add(Me.lblRange)
        Me.GroupBox5.Controls.Add(Me.cmbStatus)
        Me.GroupBox5.Controls.Add(Me.Label14)
        Me.GroupBox5.Controls.Add(Me.cmbDataType)
        Me.GroupBox5.Controls.Add(Me.lblFuAddress)
        Me.GroupBox5.Controls.Add(Me.Label38)
        Me.GroupBox5.Controls.Add(Me.Label8)
        Me.GroupBox5.Controls.Add(Me.txtOffset)
        Me.GroupBox5.Controls.Add(Me.Label39)
        Me.GroupBox5.Controls.Add(Me.lblNorHyphen)
        Me.GroupBox5.Controls.Add(Me.Label45)
        Me.GroupBox5.Controls.Add(Me.txtDecimal)
        Me.GroupBox5.Controls.Add(Me.txtLowNormal)
        Me.GroupBox5.Controls.Add(Me.chkCenterGraph)
        Me.GroupBox5.Controls.Add(Me.txtHighNormal)
        Me.GroupBox5.Controls.Add(Me.Label123)
        Me.GroupBox5.Controls.Add(Me.txtString)
        Me.GroupBox5.Controls.Add(Me.lblDecimal)
        Me.GroupBox5.Location = New System.Drawing.Point(448, 6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(475, 490)
        Me.GroupBox5.TabIndex = 114
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "PID"
        '
        'cmbRanDumy
        '
        Me.cmbRanDumy.FormattingEnabled = True
        Me.cmbRanDumy.Location = New System.Drawing.Point(360, 29)
        Me.cmbRanDumy.Name = "cmbRanDumy"
        Me.cmbRanDumy.Size = New System.Drawing.Size(36, 20)
        Me.cmbRanDumy.TabIndex = 128
        Me.cmbRanDumy.Visible = False
        '
        'lblRangeCaution
        '
        Me.lblRangeCaution.Location = New System.Drawing.Point(264, 30)
        Me.lblRangeCaution.Name = "lblRangeCaution"
        Me.lblRangeCaution.Size = New System.Drawing.Size(205, 106)
        Me.lblRangeCaution.TabIndex = 127
        Me.lblRangeCaution.Text = "^"
        '
        'chkPSDisp
        '
        Me.chkPSDisp.AutoSize = True
        Me.chkPSDisp.Location = New System.Drawing.Point(360, 237)
        Me.chkPSDisp.Name = "chkPSDisp"
        Me.chkPSDisp.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkPSDisp.Size = New System.Drawing.Size(90, 16)
        Me.chkPSDisp.TabIndex = 19
        Me.chkPSDisp.Text = "P/S Display"
        Me.chkPSDisp.UseVisualStyleBackColor = True
        '
        'chkPowerFactor
        '
        Me.chkPowerFactor.BackColor = System.Drawing.SystemColors.Control
        Me.chkPowerFactor.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPowerFactor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPowerFactor.Location = New System.Drawing.Point(200, 236)
        Me.chkPowerFactor.Name = "chkPowerFactor"
        Me.chkPowerFactor.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkPowerFactor.Size = New System.Drawing.Size(121, 19)
        Me.chkPowerFactor.TabIndex = 18
        Me.chkPowerFactor.Text = "Power Factor"
        Me.chkPowerFactor.UseVisualStyleBackColor = True
        '
        'lblDecPoint
        '
        Me.lblDecPoint.BackColor = System.Drawing.SystemColors.Control
        Me.lblDecPoint.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDecPoint.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDecPoint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDecPoint.Location = New System.Drawing.Point(360, 15)
        Me.lblDecPoint.Name = "lblDecPoint"
        Me.lblDecPoint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDecPoint.Size = New System.Drawing.Size(164, 19)
        Me.lblDecPoint.TabIndex = 124
        Me.lblDecPoint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblDecPoint.Visible = False
        '
        'fraHiHi0
        '
        Me.fraHiHi0.BackColor = System.Drawing.SystemColors.Control
        Me.fraHiHi0.Controls.Add(Me.chkSover)
        Me.fraHiHi0.Controls.Add(Me.chkSunder)
        Me.fraHiHi0.Controls.Add(Me.txtStatusSF)
        Me.fraHiHi0.Controls.Add(Me.txtStatusLoLo)
        Me.fraHiHi0.Controls.Add(Me.txtStatusLo)
        Me.fraHiHi0.Controls.Add(Me.txtStatusHi)
        Me.fraHiHi0.Controls.Add(Me.lblStatus)
        Me.fraHiHi0.Controls.Add(Me.txtStatusHiHi)
        Me.fraHiHi0.Controls.Add(Me.txtValueHiHi)
        Me.fraHiHi0.Controls.Add(Me.Label4)
        Me.fraHiHi0.Controls.Add(Me.cmbValueSensorFailure)
        Me.fraHiHi0.Controls.Add(Me.Label6)
        Me.fraHiHi0.Controls.Add(Me.Label5)
        Me.fraHiHi0.Controls.Add(Me.txtExtGSensorFailure)
        Me.fraHiHi0.Controls.Add(Me.Label1)
        Me.fraHiHi0.Controls.Add(Me.txtGRep2SensorFailure)
        Me.fraHiHi0.Controls.Add(Me.txtGRep2LoLo)
        Me.fraHiHi0.Controls.Add(Me.txtGRep2Lo)
        Me.fraHiHi0.Controls.Add(Me.Label64)
        Me.fraHiHi0.Controls.Add(Me.txtGRep1SensorFailure)
        Me.fraHiHi0.Controls.Add(Me.txtGRep1LoLo)
        Me.fraHiHi0.Controls.Add(Me.txtExtGLoLo)
        Me.fraHiHi0.Controls.Add(Me.txtGRep2Hi)
        Me.fraHiHi0.Controls.Add(Me.txtDelaySensorFailure)
        Me.fraHiHi0.Controls.Add(Me.txtDelayLoLo)
        Me.fraHiHi0.Controls.Add(Me.txtGRep1Lo)
        Me.fraHiHi0.Controls.Add(Me.txtDelayLo)
        Me.fraHiHi0.Controls.Add(Me.txtValueLoLo)
        Me.fraHiHi0.Controls.Add(Me.txtGRep1Hi)
        Me.fraHiHi0.Controls.Add(Me.txtExtGLo)
        Me.fraHiHi0.Controls.Add(Me.txtValueLo)
        Me.fraHiHi0.Controls.Add(Me.txtExtGHi)
        Me.fraHiHi0.Controls.Add(Me.txtDelayHiHi)
        Me.fraHiHi0.Controls.Add(Me.txtDelayHi)
        Me.fraHiHi0.Controls.Add(Me.txtGRep2HiHi)
        Me.fraHiHi0.Controls.Add(Me.txtExtGHiHi)
        Me.fraHiHi0.Controls.Add(Me.txtValueHi)
        Me.fraHiHi0.Controls.Add(Me.txtGRep1HiHi)
        Me.fraHiHi0.Controls.Add(Me.Label128)
        Me.fraHiHi0.Controls.Add(Me.Label129)
        Me.fraHiHi0.Controls.Add(Me.Label20)
        Me.fraHiHi0.Controls.Add(Me.Label30)
        Me.fraHiHi0.Controls.Add(Me.Label40)
        Me.fraHiHi0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraHiHi0.Location = New System.Drawing.Point(27, 259)
        Me.fraHiHi0.Name = "fraHiHi0"
        Me.fraHiHi0.Padding = New System.Windows.Forms.Padding(0)
        Me.fraHiHi0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraHiHi0.Size = New System.Drawing.Size(440, 224)
        Me.fraHiHi0.TabIndex = 123
        Me.fraHiHi0.TabStop = False
        Me.fraHiHi0.Text = "Alarm"
        '
        'chkSover
        '
        Me.chkSover.BackColor = System.Drawing.SystemColors.Control
        Me.chkSover.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSover.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSover.Location = New System.Drawing.Point(198, 177)
        Me.chkSover.Name = "chkSover"
        Me.chkSover.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkSover.Size = New System.Drawing.Size(97, 19)
        Me.chkSover.TabIndex = 51
        Me.chkSover.Text = "Sensor Over"
        Me.chkSover.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkSover.UseVisualStyleBackColor = True
        Me.chkSover.Visible = False
        '
        'chkSunder
        '
        Me.chkSunder.BackColor = System.Drawing.SystemColors.Control
        Me.chkSunder.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSunder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSunder.Location = New System.Drawing.Point(84, 178)
        Me.chkSunder.Name = "chkSunder"
        Me.chkSunder.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkSunder.Size = New System.Drawing.Size(97, 19)
        Me.chkSunder.TabIndex = 50
        Me.chkSunder.Text = "Sensor Under"
        Me.chkSunder.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkSunder.UseVisualStyleBackColor = True
        Me.chkSunder.Visible = False
        '
        'txtStatusSF
        '
        Me.txtStatusSF.AcceptsReturn = True
        Me.txtStatusSF.Enabled = False
        Me.txtStatusSF.Location = New System.Drawing.Point(344, 152)
        Me.txtStatusSF.MaxLength = 0
        Me.txtStatusSF.Name = "txtStatusSF"
        Me.txtStatusSF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusSF.Size = New System.Drawing.Size(72, 19)
        Me.txtStatusSF.TabIndex = 49
        '
        'txtStatusLoLo
        '
        Me.txtStatusLoLo.AcceptsReturn = True
        Me.txtStatusLoLo.Location = New System.Drawing.Point(344, 125)
        Me.txtStatusLoLo.MaxLength = 0
        Me.txtStatusLoLo.Name = "txtStatusLoLo"
        Me.txtStatusLoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusLoLo.Size = New System.Drawing.Size(72, 19)
        Me.txtStatusLoLo.TabIndex = 43
        '
        'txtStatusLo
        '
        Me.txtStatusLo.AcceptsReturn = True
        Me.txtStatusLo.Location = New System.Drawing.Point(344, 98)
        Me.txtStatusLo.MaxLength = 0
        Me.txtStatusLo.Name = "txtStatusLo"
        Me.txtStatusLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusLo.Size = New System.Drawing.Size(72, 19)
        Me.txtStatusLo.TabIndex = 37
        '
        'txtStatusHi
        '
        Me.txtStatusHi.AcceptsReturn = True
        Me.txtStatusHi.Location = New System.Drawing.Point(344, 71)
        Me.txtStatusHi.MaxLength = 0
        Me.txtStatusHi.Name = "txtStatusHi"
        Me.txtStatusHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusHi.Size = New System.Drawing.Size(72, 19)
        Me.txtStatusHi.TabIndex = 31
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Location = New System.Drawing.Point(356, 26)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.Size = New System.Drawing.Size(41, 12)
        Me.lblStatus.TabIndex = 115
        Me.lblStatus.Text = "Status"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStatusHiHi
        '
        Me.txtStatusHiHi.AcceptsReturn = True
        Me.txtStatusHiHi.Location = New System.Drawing.Point(344, 44)
        Me.txtStatusHiHi.MaxLength = 0
        Me.txtStatusHiHi.Name = "txtStatusHiHi"
        Me.txtStatusHiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusHiHi.Size = New System.Drawing.Size(72, 19)
        Me.txtStatusHiHi.TabIndex = 25
        '
        'txtValueHiHi
        '
        Me.txtValueHiHi.AcceptsReturn = True
        Me.txtValueHiHi.Location = New System.Drawing.Point(63, 44)
        Me.txtValueHiHi.MaxLength = 0
        Me.txtValueHiHi.Name = "txtValueHiHi"
        Me.txtValueHiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValueHiHi.Size = New System.Drawing.Size(72, 19)
        Me.txtValueHiHi.TabIndex = 20
        Me.txtValueHiHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(12, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(47, 24)
        Me.Label4.TabIndex = 112
        Me.Label4.Text = "Sensor" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Failure"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbValueSensorFailure
        '
        Me.cmbValueSensorFailure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbValueSensorFailure.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbValueSensorFailure.FormattingEnabled = True
        Me.cmbValueSensorFailure.Location = New System.Drawing.Point(84, 152)
        Me.cmbValueSensorFailure.Name = "cmbValueSensorFailure"
        Me.cmbValueSensorFailure.Size = New System.Drawing.Size(48, 20)
        Me.cmbValueSensorFailure.TabIndex = 44
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(27, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 70
        Me.Label6.Text = "LOLO"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(38, 101)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 69
        Me.Label5.Text = "LO"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtExtGSensorFailure
        '
        Me.txtExtGSensorFailure.AcceptsReturn = True
        Me.txtExtGSensorFailure.Location = New System.Drawing.Point(138, 152)
        Me.txtExtGSensorFailure.MaxLength = 0
        Me.txtExtGSensorFailure.Name = "txtExtGSensorFailure"
        Me.txtExtGSensorFailure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGSensorFailure.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGSensorFailure.TabIndex = 45
        Me.txtExtGSensorFailure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(40, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 68
        Me.Label1.Text = "HI"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGRep2SensorFailure
        '
        Me.txtGRep2SensorFailure.AcceptsReturn = True
        Me.txtGRep2SensorFailure.Enabled = False
        Me.txtGRep2SensorFailure.Location = New System.Drawing.Point(291, 152)
        Me.txtGRep2SensorFailure.MaxLength = 0
        Me.txtGRep2SensorFailure.Name = "txtGRep2SensorFailure"
        Me.txtGRep2SensorFailure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2SensorFailure.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2SensorFailure.TabIndex = 48
        Me.txtGRep2SensorFailure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2LoLo
        '
        Me.txtGRep2LoLo.AcceptsReturn = True
        Me.txtGRep2LoLo.Location = New System.Drawing.Point(291, 125)
        Me.txtGRep2LoLo.MaxLength = 0
        Me.txtGRep2LoLo.Name = "txtGRep2LoLo"
        Me.txtGRep2LoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2LoLo.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2LoLo.TabIndex = 42
        Me.txtGRep2LoLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2Lo
        '
        Me.txtGRep2Lo.AcceptsReturn = True
        Me.txtGRep2Lo.Location = New System.Drawing.Point(291, 98)
        Me.txtGRep2Lo.MaxLength = 0
        Me.txtGRep2Lo.Name = "txtGRep2Lo"
        Me.txtGRep2Lo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2Lo.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2Lo.TabIndex = 36
        Me.txtGRep2Lo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.BackColor = System.Drawing.SystemColors.Control
        Me.Label64.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label64.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label64.Location = New System.Drawing.Point(28, 47)
        Me.Label64.Name = "Label64"
        Me.Label64.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label64.Size = New System.Drawing.Size(29, 12)
        Me.Label64.TabIndex = 59
        Me.Label64.Text = "HIHI"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGRep1SensorFailure
        '
        Me.txtGRep1SensorFailure.AcceptsReturn = True
        Me.txtGRep1SensorFailure.Enabled = False
        Me.txtGRep1SensorFailure.Location = New System.Drawing.Point(239, 152)
        Me.txtGRep1SensorFailure.MaxLength = 0
        Me.txtGRep1SensorFailure.Name = "txtGRep1SensorFailure"
        Me.txtGRep1SensorFailure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1SensorFailure.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1SensorFailure.TabIndex = 47
        Me.txtGRep1SensorFailure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1LoLo
        '
        Me.txtGRep1LoLo.AcceptsReturn = True
        Me.txtGRep1LoLo.Location = New System.Drawing.Point(239, 125)
        Me.txtGRep1LoLo.MaxLength = 0
        Me.txtGRep1LoLo.Name = "txtGRep1LoLo"
        Me.txtGRep1LoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1LoLo.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1LoLo.TabIndex = 41
        Me.txtGRep1LoLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGLoLo
        '
        Me.txtExtGLoLo.AcceptsReturn = True
        Me.txtExtGLoLo.Location = New System.Drawing.Point(138, 125)
        Me.txtExtGLoLo.MaxLength = 0
        Me.txtExtGLoLo.Name = "txtExtGLoLo"
        Me.txtExtGLoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGLoLo.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGLoLo.TabIndex = 39
        Me.txtExtGLoLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2Hi
        '
        Me.txtGRep2Hi.AcceptsReturn = True
        Me.txtGRep2Hi.Location = New System.Drawing.Point(291, 71)
        Me.txtGRep2Hi.MaxLength = 0
        Me.txtGRep2Hi.Name = "txtGRep2Hi"
        Me.txtGRep2Hi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2Hi.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2Hi.TabIndex = 30
        Me.txtGRep2Hi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelaySensorFailure
        '
        Me.txtDelaySensorFailure.AcceptsReturn = True
        Me.txtDelaySensorFailure.Location = New System.Drawing.Point(188, 152)
        Me.txtDelaySensorFailure.MaxLength = 0
        Me.txtDelaySensorFailure.Name = "txtDelaySensorFailure"
        Me.txtDelaySensorFailure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelaySensorFailure.Size = New System.Drawing.Size(48, 19)
        Me.txtDelaySensorFailure.TabIndex = 46
        Me.txtDelaySensorFailure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayLoLo
        '
        Me.txtDelayLoLo.AcceptsReturn = True
        Me.txtDelayLoLo.Location = New System.Drawing.Point(188, 125)
        Me.txtDelayLoLo.MaxLength = 0
        Me.txtDelayLoLo.Name = "txtDelayLoLo"
        Me.txtDelayLoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayLoLo.Size = New System.Drawing.Size(48, 19)
        Me.txtDelayLoLo.TabIndex = 40
        Me.txtDelayLoLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1Lo
        '
        Me.txtGRep1Lo.AcceptsReturn = True
        Me.txtGRep1Lo.Location = New System.Drawing.Point(239, 98)
        Me.txtGRep1Lo.MaxLength = 0
        Me.txtGRep1Lo.Name = "txtGRep1Lo"
        Me.txtGRep1Lo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1Lo.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1Lo.TabIndex = 35
        Me.txtGRep1Lo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayLo
        '
        Me.txtDelayLo.AcceptsReturn = True
        Me.txtDelayLo.Location = New System.Drawing.Point(188, 98)
        Me.txtDelayLo.MaxLength = 0
        Me.txtDelayLo.Name = "txtDelayLo"
        Me.txtDelayLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayLo.Size = New System.Drawing.Size(48, 19)
        Me.txtDelayLo.TabIndex = 34
        Me.txtDelayLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValueLoLo
        '
        Me.txtValueLoLo.AcceptsReturn = True
        Me.txtValueLoLo.Location = New System.Drawing.Point(63, 125)
        Me.txtValueLoLo.MaxLength = 0
        Me.txtValueLoLo.Name = "txtValueLoLo"
        Me.txtValueLoLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValueLoLo.Size = New System.Drawing.Size(72, 19)
        Me.txtValueLoLo.TabIndex = 38
        Me.txtValueLoLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1Hi
        '
        Me.txtGRep1Hi.AcceptsReturn = True
        Me.txtGRep1Hi.Location = New System.Drawing.Point(239, 71)
        Me.txtGRep1Hi.MaxLength = 0
        Me.txtGRep1Hi.Name = "txtGRep1Hi"
        Me.txtGRep1Hi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1Hi.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1Hi.TabIndex = 29
        Me.txtGRep1Hi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGLo
        '
        Me.txtExtGLo.AcceptsReturn = True
        Me.txtExtGLo.Location = New System.Drawing.Point(138, 98)
        Me.txtExtGLo.MaxLength = 0
        Me.txtExtGLo.Name = "txtExtGLo"
        Me.txtExtGLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGLo.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGLo.TabIndex = 33
        Me.txtExtGLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValueLo
        '
        Me.txtValueLo.AcceptsReturn = True
        Me.txtValueLo.Location = New System.Drawing.Point(63, 98)
        Me.txtValueLo.MaxLength = 0
        Me.txtValueLo.Name = "txtValueLo"
        Me.txtValueLo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValueLo.Size = New System.Drawing.Size(72, 19)
        Me.txtValueLo.TabIndex = 32
        Me.txtValueLo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGHi
        '
        Me.txtExtGHi.AcceptsReturn = True
        Me.txtExtGHi.Location = New System.Drawing.Point(138, 71)
        Me.txtExtGHi.MaxLength = 0
        Me.txtExtGHi.Name = "txtExtGHi"
        Me.txtExtGHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGHi.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGHi.TabIndex = 27
        Me.txtExtGHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayHiHi
        '
        Me.txtDelayHiHi.AcceptsReturn = True
        Me.txtDelayHiHi.Location = New System.Drawing.Point(188, 44)
        Me.txtDelayHiHi.MaxLength = 0
        Me.txtDelayHiHi.Name = "txtDelayHiHi"
        Me.txtDelayHiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayHiHi.Size = New System.Drawing.Size(48, 19)
        Me.txtDelayHiHi.TabIndex = 22
        Me.txtDelayHiHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDelayHi
        '
        Me.txtDelayHi.AcceptsReturn = True
        Me.txtDelayHi.Location = New System.Drawing.Point(188, 71)
        Me.txtDelayHi.MaxLength = 0
        Me.txtDelayHi.Name = "txtDelayHi"
        Me.txtDelayHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayHi.Size = New System.Drawing.Size(48, 19)
        Me.txtDelayHi.TabIndex = 28
        Me.txtDelayHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep2HiHi
        '
        Me.txtGRep2HiHi.AcceptsReturn = True
        Me.txtGRep2HiHi.Location = New System.Drawing.Point(291, 44)
        Me.txtGRep2HiHi.MaxLength = 0
        Me.txtGRep2HiHi.Name = "txtGRep2HiHi"
        Me.txtGRep2HiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep2HiHi.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep2HiHi.TabIndex = 24
        Me.txtGRep2HiHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtExtGHiHi
        '
        Me.txtExtGHiHi.AcceptsReturn = True
        Me.txtExtGHiHi.Location = New System.Drawing.Point(138, 44)
        Me.txtExtGHiHi.MaxLength = 0
        Me.txtExtGHiHi.Name = "txtExtGHiHi"
        Me.txtExtGHiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExtGHiHi.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGHiHi.TabIndex = 21
        Me.txtExtGHiHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValueHi
        '
        Me.txtValueHi.AcceptsReturn = True
        Me.txtValueHi.Location = New System.Drawing.Point(63, 71)
        Me.txtValueHi.MaxLength = 0
        Me.txtValueHi.Name = "txtValueHi"
        Me.txtValueHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValueHi.Size = New System.Drawing.Size(72, 19)
        Me.txtValueHi.TabIndex = 26
        Me.txtValueHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGRep1HiHi
        '
        Me.txtGRep1HiHi.AcceptsReturn = True
        Me.txtGRep1HiHi.Location = New System.Drawing.Point(239, 44)
        Me.txtGRep1HiHi.MaxLength = 0
        Me.txtGRep1HiHi.Name = "txtGRep1HiHi"
        Me.txtGRep1HiHi.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRep1HiHi.Size = New System.Drawing.Size(48, 19)
        Me.txtGRep1HiHi.TabIndex = 23
        Me.txtGRep1HiHi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label128
        '
        Me.Label128.AutoSize = True
        Me.Label128.BackColor = System.Drawing.SystemColors.Control
        Me.Label128.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label128.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label128.Location = New System.Drawing.Point(196, 26)
        Me.Label128.Name = "Label128"
        Me.Label128.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label128.Size = New System.Drawing.Size(35, 12)
        Me.Label128.TabIndex = 60
        Me.Label128.Text = "Delay"
        Me.Label128.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label129
        '
        Me.Label129.AutoSize = True
        Me.Label129.BackColor = System.Drawing.SystemColors.Control
        Me.Label129.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label129.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label129.Location = New System.Drawing.Point(80, 26)
        Me.Label129.Name = "Label129"
        Me.Label129.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label129.Size = New System.Drawing.Size(35, 12)
        Me.Label129.TabIndex = 59
        Me.Label129.Text = "Value"
        Me.Label129.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(144, 26)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(35, 12)
        Me.Label20.TabIndex = 58
        Me.Label20.Text = "EXT.G"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(244, 26)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(41, 12)
        Me.Label30.TabIndex = 57
        Me.Label30.Text = "G REP1"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(296, 26)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(41, 12)
        Me.Label40.TabIndex = 56
        Me.Label40.Text = "G REP2"
        '
        'lblHyphen
        '
        Me.lblHyphen.AutoSize = True
        Me.lblHyphen.BackColor = System.Drawing.SystemColors.Control
        Me.lblHyphen.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHyphen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHyphen.Location = New System.Drawing.Point(172, 86)
        Me.lblHyphen.Name = "lblHyphen"
        Me.lblHyphen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHyphen.Size = New System.Drawing.Size(11, 12)
        Me.lblHyphen.TabIndex = 122
        Me.lblHyphen.Text = "-"
        Me.lblHyphen.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtRangeTo
        '
        Me.txtRangeTo.AcceptsReturn = True
        Me.txtRangeTo.Location = New System.Drawing.Point(183, 84)
        Me.txtRangeTo.MaxLength = 0
        Me.txtRangeTo.Name = "txtRangeTo"
        Me.txtRangeTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRangeTo.Size = New System.Drawing.Size(72, 19)
        Me.txtRangeTo.TabIndex = 5
        Me.txtRangeTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtRangeFrom
        '
        Me.txtRangeFrom.AcceptsReturn = True
        Me.txtRangeFrom.Location = New System.Drawing.Point(97, 84)
        Me.txtRangeFrom.MaxLength = 0
        Me.txtRangeFrom.Name = "txtRangeFrom"
        Me.txtRangeFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRangeFrom.Size = New System.Drawing.Size(72, 19)
        Me.txtRangeFrom.TabIndex = 4
        Me.txtRangeFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtUnit
        '
        Me.txtUnit.AcceptsReturn = True
        Me.txtUnit.Location = New System.Drawing.Point(257, 175)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(116, 19)
        Me.txtUnit.TabIndex = 15
        '
        'txtStatus
        '
        Me.txtStatus.AcceptsReturn = True
        Me.txtStatus.Location = New System.Drawing.Point(259, 112)
        Me.txtStatus.MaxLength = 0
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatus.Size = New System.Drawing.Size(164, 19)
        Me.txtStatus.TabIndex = 8
        Me.txtStatus.Visible = False
        '
        'cmbExtDevice
        '
        Me.cmbExtDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbExtDevice.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbExtDevice.FormattingEnabled = True
        Me.cmbExtDevice.Location = New System.Drawing.Point(259, 53)
        Me.cmbExtDevice.Name = "cmbExtDevice"
        Me.cmbExtDevice.Size = New System.Drawing.Size(265, 20)
        Me.cmbExtDevice.TabIndex = 6
        '
        'cmbUnit
        '
        Me.cmbUnit.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUnit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbUnit.Location = New System.Drawing.Point(97, 175)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbUnit.Size = New System.Drawing.Size(142, 20)
        Me.cmbUnit.TabIndex = 14
        '
        'txtPin
        '
        Me.txtPin.AcceptsReturn = True
        Me.txtPin.Location = New System.Drawing.Point(182, 26)
        Me.txtPin.MaxLength = 0
        Me.txtPin.Name = "txtPin"
        Me.txtPin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPin.Size = New System.Drawing.Size(40, 19)
        Me.txtPin.TabIndex = 2
        '
        'txtPortNo
        '
        Me.txtPortNo.AcceptsReturn = True
        Me.txtPortNo.Location = New System.Drawing.Point(140, 26)
        Me.txtPortNo.MaxLength = 0
        Me.txtPortNo.Name = "txtPortNo"
        Me.txtPortNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNo.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNo.TabIndex = 1
        '
        'txtFuNo
        '
        Me.txtFuNo.AcceptsReturn = True
        Me.txtFuNo.Location = New System.Drawing.Point(98, 26)
        Me.txtFuNo.MaxLength = 0
        Me.txtFuNo.Name = "txtFuNo"
        Me.txtFuNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNo.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNo.TabIndex = 0
        '
        'cmbRangeType
        '
        Me.cmbRangeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRangeType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbRangeType.FormattingEnabled = True
        Me.cmbRangeType.Location = New System.Drawing.Point(360, 83)
        Me.cmbRangeType.Name = "cmbRangeType"
        Me.cmbRangeType.Size = New System.Drawing.Size(164, 20)
        Me.cmbRangeType.TabIndex = 4
        '
        'lblRange
        '
        Me.lblRange.AutoSize = True
        Me.lblRange.BackColor = System.Drawing.SystemColors.Control
        Me.lblRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRange.Location = New System.Drawing.Point(21, 87)
        Me.lblRange.Name = "lblRange"
        Me.lblRange.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblRange.Size = New System.Drawing.Size(65, 12)
        Me.lblRange.TabIndex = 108
        Me.lblRange.Text = "Range Type"
        Me.lblRange.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbStatus
        '
        Me.cmbStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cmbStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbStatus.Location = New System.Drawing.Point(98, 112)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbStatus.Size = New System.Drawing.Size(157, 20)
        Me.cmbStatus.TabIndex = 7
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(46, 115)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(41, 12)
        Me.Label14.TabIndex = 90
        Me.Label14.Text = "Status"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDataType
        '
        Me.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDataType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbDataType.FormattingEnabled = True
        Me.cmbDataType.Location = New System.Drawing.Point(97, 53)
        Me.cmbDataType.Name = "cmbDataType"
        Me.cmbDataType.Size = New System.Drawing.Size(157, 20)
        Me.cmbDataType.TabIndex = 3
        '
        'lblFuAddress
        '
        Me.lblFuAddress.AutoSize = True
        Me.lblFuAddress.BackColor = System.Drawing.SystemColors.Control
        Me.lblFuAddress.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFuAddress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFuAddress.Location = New System.Drawing.Point(20, 29)
        Me.lblFuAddress.Name = "lblFuAddress"
        Me.lblFuAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFuAddress.Size = New System.Drawing.Size(65, 12)
        Me.lblFuAddress.TabIndex = 88
        Me.lblFuAddress.Text = "FU Address"
        Me.lblFuAddress.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(55, 178)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(29, 12)
        Me.Label38.TabIndex = 85
        Me.Label38.Text = "Unit"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(27, 58)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 81
        Me.Label8.Text = "Data Type"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtOffset
        '
        Me.txtOffset.AcceptsReturn = True
        Me.txtOffset.Location = New System.Drawing.Point(320, 144)
        Me.txtOffset.MaxLength = 0
        Me.txtOffset.Name = "txtOffset"
        Me.txtOffset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOffset.Size = New System.Drawing.Size(76, 19)
        Me.txtOffset.TabIndex = 13
        Me.txtOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(7, 147)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(77, 12)
        Me.Label39.TabIndex = 84
        Me.Label39.Text = "Normal Range"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNorHyphen
        '
        Me.lblNorHyphen.AutoSize = True
        Me.lblNorHyphen.BackColor = System.Drawing.SystemColors.Control
        Me.lblNorHyphen.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNorHyphen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNorHyphen.Location = New System.Drawing.Point(172, 147)
        Me.lblNorHyphen.Name = "lblNorHyphen"
        Me.lblNorHyphen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNorHyphen.Size = New System.Drawing.Size(11, 12)
        Me.lblNorHyphen.TabIndex = 83
        Me.lblNorHyphen.Text = "-"
        Me.lblNorHyphen.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(43, 210)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(41, 12)
        Me.Label45.TabIndex = 82
        Me.Label45.Text = "String"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDecimal
        '
        Me.txtDecimal.AcceptsReturn = True
        Me.txtDecimal.Location = New System.Drawing.Point(314, 15)
        Me.txtDecimal.MaxLength = 0
        Me.txtDecimal.Name = "txtDecimal"
        Me.txtDecimal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDecimal.Size = New System.Drawing.Size(48, 19)
        Me.txtDecimal.TabIndex = 6
        Me.txtDecimal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtDecimal.Visible = False
        '
        'txtLowNormal
        '
        Me.txtLowNormal.AcceptsReturn = True
        Me.txtLowNormal.Location = New System.Drawing.Point(97, 144)
        Me.txtLowNormal.MaxLength = 0
        Me.txtLowNormal.Name = "txtLowNormal"
        Me.txtLowNormal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLowNormal.Size = New System.Drawing.Size(71, 19)
        Me.txtLowNormal.TabIndex = 10
        Me.txtLowNormal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkCenterGraph
        '
        Me.chkCenterGraph.BackColor = System.Drawing.SystemColors.Control
        Me.chkCenterGraph.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCenterGraph.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCenterGraph.Location = New System.Drawing.Point(73, 236)
        Me.chkCenterGraph.Name = "chkCenterGraph"
        Me.chkCenterGraph.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkCenterGraph.Size = New System.Drawing.Size(121, 19)
        Me.chkCenterGraph.TabIndex = 17
        Me.chkCenterGraph.Text = "Center Graph"
        Me.chkCenterGraph.UseVisualStyleBackColor = True
        '
        'txtHighNormal
        '
        Me.txtHighNormal.AcceptsReturn = True
        Me.txtHighNormal.Location = New System.Drawing.Point(183, 144)
        Me.txtHighNormal.MaxLength = 0
        Me.txtHighNormal.Name = "txtHighNormal"
        Me.txtHighNormal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHighNormal.Size = New System.Drawing.Size(71, 19)
        Me.txtHighNormal.TabIndex = 11
        Me.txtHighNormal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label123
        '
        Me.Label123.AutoSize = True
        Me.Label123.BackColor = System.Drawing.SystemColors.Control
        Me.Label123.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label123.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label123.Location = New System.Drawing.Point(273, 147)
        Me.Label123.Name = "Label123"
        Me.Label123.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label123.Size = New System.Drawing.Size(41, 12)
        Me.Label123.TabIndex = 81
        Me.Label123.Text = "Offset"
        Me.Label123.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtString
        '
        Me.txtString.AcceptsReturn = True
        Me.txtString.Location = New System.Drawing.Point(97, 207)
        Me.txtString.MaxLength = 0
        Me.txtString.Name = "txtString"
        Me.txtString.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtString.Size = New System.Drawing.Size(48, 19)
        Me.txtString.TabIndex = 16
        Me.txtString.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblDecimal
        '
        Me.lblDecimal.AutoSize = True
        Me.lblDecimal.BackColor = System.Drawing.SystemColors.Control
        Me.lblDecimal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDecimal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDecimal.Location = New System.Drawing.Point(261, 18)
        Me.lblDecimal.Name = "lblDecimal"
        Me.lblDecimal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDecimal.Size = New System.Drawing.Size(47, 12)
        Me.lblDecimal.TabIndex = 34
        Me.lblDecimal.Text = "Decimal"
        Me.lblDecimal.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblDecimal.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.cmbAlmLvl)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtTag)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cmbTime)
        Me.GroupBox1.Controls.Add(Me.chkMrepose)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.fraAlarm)
        Me.GroupBox1.Controls.Add(Me.cmbSysNo)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.Label36)
        Me.GroupBox1.Controls.Add(Me.txtItemName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtChNo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label37)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(427, 465)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Common"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(230, 241)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(71, 12)
        Me.Label26.TabIndex = 110
        Me.Label26.Text = "Alarm Level"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbAlmLvl
        '
        Me.cmbAlmLvl.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAlmLvl.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbAlmLvl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAlmLvl.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbAlmLvl.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbAlmLvl.Location = New System.Drawing.Point(307, 236)
        Me.cmbAlmLvl.Name = "cmbAlmLvl"
        Me.cmbAlmLvl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbAlmLvl.Size = New System.Drawing.Size(97, 20)
        Me.cmbAlmLvl.TabIndex = 109
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(181, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(47, 12)
        Me.Label2.TabIndex = 108
        Me.Label2.Text = "Tag No."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTag
        '
        Me.txtTag.AcceptsReturn = True
        Me.txtTag.Location = New System.Drawing.Point(228, 55)
        Me.txtTag.MaxLength = 16
        Me.txtTag.Name = "txtTag"
        Me.txtTag.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTag.Size = New System.Drawing.Size(115, 19)
        Me.txtTag.TabIndex = 107
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(12, 240)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(119, 12)
        Me.Label9.TabIndex = 106
        Me.Label9.Text = "Unit of Delay Timer"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbTime
        '
        Me.cmbTime.BackColor = System.Drawing.SystemColors.Window
        Me.cmbTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbTime.Location = New System.Drawing.Point(136, 236)
        Me.cmbTime.Name = "cmbTime"
        Me.cmbTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbTime.Size = New System.Drawing.Size(64, 20)
        Me.cmbTime.TabIndex = 6
        '
        'chkMrepose
        '
        Me.chkMrepose.BackColor = System.Drawing.SystemColors.Control
        Me.chkMrepose.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMrepose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMrepose.Location = New System.Drawing.Point(280, 384)
        Me.chkMrepose.Name = "chkMrepose"
        Me.chkMrepose.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkMrepose.Size = New System.Drawing.Size(120, 19)
        Me.chkMrepose.TabIndex = 100
        Me.chkMrepose.Text = "Manual Repose"
        Me.chkMrepose.UseVisualStyleBackColor = True
        Me.chkMrepose.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtShareChid)
        Me.GroupBox3.Controls.Add(Me.lblShareChid)
        Me.GroupBox3.Controls.Add(Me.lblShareType)
        Me.GroupBox3.Controls.Add(Me.cmbShareType)
        Me.GroupBox3.Location = New System.Drawing.Point(16, 272)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(213, 80)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        '
        'txtShareChid
        '
        Me.txtShareChid.AcceptsReturn = True
        Me.txtShareChid.Location = New System.Drawing.Point(108, 48)
        Me.txtShareChid.MaxLength = 0
        Me.txtShareChid.Name = "txtShareChid"
        Me.txtShareChid.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtShareChid.Size = New System.Drawing.Size(48, 19)
        Me.txtShareChid.TabIndex = 1
        Me.txtShareChid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblShareChid
        '
        Me.lblShareChid.AutoSize = True
        Me.lblShareChid.BackColor = System.Drawing.SystemColors.Control
        Me.lblShareChid.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShareChid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShareChid.Location = New System.Drawing.Point(12, 52)
        Me.lblShareChid.Name = "lblShareChid"
        Me.lblShareChid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShareChid.Size = New System.Drawing.Size(83, 12)
        Me.lblShareChid.TabIndex = 106
        Me.lblShareChid.Text = "Remote CH No."
        Me.lblShareChid.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblShareType
        '
        Me.lblShareType.AutoSize = True
        Me.lblShareType.BackColor = System.Drawing.SystemColors.Control
        Me.lblShareType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShareType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShareType.Location = New System.Drawing.Point(32, 24)
        Me.lblShareType.Name = "lblShareType"
        Me.lblShareType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShareType.Size = New System.Drawing.Size(65, 12)
        Me.lblShareType.TabIndex = 105
        Me.lblShareType.Text = "Share Type"
        Me.lblShareType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbShareType
        '
        Me.cmbShareType.BackColor = System.Drawing.SystemColors.Window
        Me.cmbShareType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmbShareType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbShareType.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbShareType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbShareType.Location = New System.Drawing.Point(108, 20)
        Me.cmbShareType.Name = "cmbShareType"
        Me.cmbShareType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbShareType.Size = New System.Drawing.Size(88, 20)
        Me.cmbShareType.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.lblBitSet)
        Me.GroupBox2.Controls.Add(Me.txtSP)
        Me.GroupBox2.Controls.Add(Me.txtPLC)
        Me.GroupBox2.Controls.Add(Me.txtEP)
        Me.GroupBox2.Controls.Add(Me.txtAC)
        Me.GroupBox2.Controls.Add(Me.txtRL)
        Me.GroupBox2.Controls.Add(Me.txtWK)
        Me.GroupBox2.Controls.Add(Me.txtGWS)
        Me.GroupBox2.Controls.Add(Me.txtSio)
        Me.GroupBox2.Controls.Add(Me.txtSC)
        Me.GroupBox2.Controls.Add(Me.txtDmy)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox2.Location = New System.Drawing.Point(16, 144)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox2.Size = New System.Drawing.Size(388, 84)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Flag"
        '
        'lblBitSet
        '
        Me.lblBitSet.AutoSize = True
        Me.lblBitSet.BackColor = System.Drawing.SystemColors.Control
        Me.lblBitSet.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBitSet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBitSet.Location = New System.Drawing.Point(72, 66)
        Me.lblBitSet.Name = "lblBitSet"
        Me.lblBitSet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBitSet.Size = New System.Drawing.Size(101, 12)
        Me.lblBitSet.TabIndex = 82
        Me.lblBitSet.Text = "Enter the bitset"
        Me.lblBitSet.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblBitSet.Visible = False
        '
        'txtSP
        '
        Me.txtSP.AcceptsReturn = True
        Me.txtSP.Location = New System.Drawing.Point(340, 44)
        Me.txtSP.MaxLength = 0
        Me.txtSP.Name = "txtSP"
        Me.txtSP.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSP.Size = New System.Drawing.Size(28, 19)
        Me.txtSP.TabIndex = 9
        Me.txtSP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPLC
        '
        Me.txtPLC.AcceptsReturn = True
        Me.txtPLC.Location = New System.Drawing.Point(304, 44)
        Me.txtPLC.MaxLength = 0
        Me.txtPLC.Name = "txtPLC"
        Me.txtPLC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPLC.Size = New System.Drawing.Size(28, 19)
        Me.txtPLC.TabIndex = 8
        Me.txtPLC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtEP
        '
        Me.txtEP.AcceptsReturn = True
        Me.txtEP.Location = New System.Drawing.Point(268, 44)
        Me.txtEP.MaxLength = 0
        Me.txtEP.Name = "txtEP"
        Me.txtEP.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtEP.Size = New System.Drawing.Size(28, 19)
        Me.txtEP.TabIndex = 7
        Me.txtEP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtAC
        '
        Me.txtAC.AcceptsReturn = True
        Me.txtAC.Location = New System.Drawing.Point(232, 44)
        Me.txtAC.MaxLength = 0
        Me.txtAC.Name = "txtAC"
        Me.txtAC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtAC.Size = New System.Drawing.Size(28, 19)
        Me.txtAC.TabIndex = 6
        Me.txtAC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRL
        '
        Me.txtRL.AcceptsReturn = True
        Me.txtRL.Location = New System.Drawing.Point(196, 44)
        Me.txtRL.MaxLength = 0
        Me.txtRL.Name = "txtRL"
        Me.txtRL.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtRL.Size = New System.Drawing.Size(28, 19)
        Me.txtRL.TabIndex = 5
        Me.txtRL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWK
        '
        Me.txtWK.AcceptsReturn = True
        Me.txtWK.Location = New System.Drawing.Point(160, 44)
        Me.txtWK.MaxLength = 0
        Me.txtWK.Name = "txtWK"
        Me.txtWK.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtWK.Size = New System.Drawing.Size(28, 19)
        Me.txtWK.TabIndex = 4
        Me.txtWK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtGWS
        '
        Me.txtGWS.AcceptsReturn = True
        Me.txtGWS.Location = New System.Drawing.Point(124, 44)
        Me.txtGWS.MaxLength = 0
        Me.txtGWS.Name = "txtGWS"
        Me.txtGWS.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtGWS.Size = New System.Drawing.Size(28, 19)
        Me.txtGWS.TabIndex = 3
        Me.txtGWS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSio
        '
        Me.txtSio.AcceptsReturn = True
        Me.txtSio.Location = New System.Drawing.Point(88, 44)
        Me.txtSio.MaxLength = 0
        Me.txtSio.Name = "txtSio"
        Me.txtSio.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSio.Size = New System.Drawing.Size(28, 19)
        Me.txtSio.TabIndex = 2
        Me.txtSio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSC
        '
        Me.txtSC.AcceptsReturn = True
        Me.txtSC.Location = New System.Drawing.Point(52, 44)
        Me.txtSC.MaxLength = 0
        Me.txtSC.Name = "txtSC"
        Me.txtSC.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtSC.Size = New System.Drawing.Size(28, 19)
        Me.txtSC.TabIndex = 1
        Me.txtSC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDmy
        '
        Me.txtDmy.AcceptsReturn = True
        Me.txtDmy.Location = New System.Drawing.Point(16, 44)
        Me.txtDmy.MaxLength = 0
        Me.txtDmy.Name = "txtDmy"
        Me.txtDmy.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtDmy.Size = New System.Drawing.Size(28, 19)
        Me.txtDmy.TabIndex = 0
        Me.txtDmy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(344, 24)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(29, 12)
        Me.Label25.TabIndex = 81
        Me.Label25.Text = "LOCK"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(304, 24)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(23, 12)
        Me.Label22.TabIndex = 80
        Me.Label22.Text = "PLC"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(268, 24)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(17, 12)
        Me.Label23.TabIndex = 79
        Me.Label23.Text = "EP"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(232, 24)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(17, 12)
        Me.Label24.TabIndex = 78
        Me.Label24.Text = "AC"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(196, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(17, 12)
        Me.Label15.TabIndex = 77
        Me.Label15.Text = "RL"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(160, 24)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(17, 12)
        Me.Label19.TabIndex = 76
        Me.Label19.Text = "WK"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(124, 24)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(23, 12)
        Me.Label21.TabIndex = 75
        Me.Label21.Text = "GWS"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(88, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(23, 12)
        Me.Label12.TabIndex = 74
        Me.Label12.Text = "SIO"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(52, 24)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(17, 12)
        Me.Label11.TabIndex = 73
        Me.Label11.Text = "SC"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(16, 24)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(23, 12)
        Me.Label10.TabIndex = 72
        Me.Label10.Text = "Dmy"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraAlarm
        '
        Me.fraAlarm.BackColor = System.Drawing.SystemColors.Control
        Me.fraAlarm.Controls.Add(Me.txtDelayTimer)
        Me.fraAlarm.Controls.Add(Me.Label13)
        Me.fraAlarm.Controls.Add(Me.Label16)
        Me.fraAlarm.Controls.Add(Me.Label17)
        Me.fraAlarm.Controls.Add(Me.Label18)
        Me.fraAlarm.Controls.Add(Me.txtExtGroup)
        Me.fraAlarm.Controls.Add(Me.txtR1)
        Me.fraAlarm.Controls.Add(Me.txtR2)
        Me.fraAlarm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAlarm.Location = New System.Drawing.Point(16, 376)
        Me.fraAlarm.Name = "fraAlarm"
        Me.fraAlarm.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAlarm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAlarm.Size = New System.Drawing.Size(256, 79)
        Me.fraAlarm.TabIndex = 4
        Me.fraAlarm.TabStop = False
        Me.fraAlarm.Text = "Alarm"
        Me.fraAlarm.Visible = False
        '
        'txtDelayTimer
        '
        Me.txtDelayTimer.AcceptsReturn = True
        Me.txtDelayTimer.Location = New System.Drawing.Point(76, 45)
        Me.txtDelayTimer.MaxLength = 0
        Me.txtDelayTimer.Name = "txtDelayTimer"
        Me.txtDelayTimer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelayTimer.Size = New System.Drawing.Size(48, 19)
        Me.txtDelayTimer.TabIndex = 1
        Me.txtDelayTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(76, 27)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(35, 12)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "Delay"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(24, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(35, 12)
        Me.Label16.TabIndex = 58
        Me.Label16.Text = "EXT.G"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(132, 27)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(41, 12)
        Me.Label17.TabIndex = 57
        Me.Label17.Text = "G REP1"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(184, 27)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(41, 12)
        Me.Label18.TabIndex = 56
        Me.Label18.Text = "G REP2"
        '
        'txtExtGroup
        '
        Me.txtExtGroup.AcceptsReturn = True
        Me.txtExtGroup.Location = New System.Drawing.Point(22, 45)
        Me.txtExtGroup.MaxLength = 0
        Me.txtExtGroup.Name = "txtExtGroup"
        Me.txtExtGroup.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtExtGroup.Size = New System.Drawing.Size(48, 19)
        Me.txtExtGroup.TabIndex = 0
        Me.txtExtGroup.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtR1
        '
        Me.txtR1.AcceptsReturn = True
        Me.txtR1.Location = New System.Drawing.Point(130, 45)
        Me.txtR1.MaxLength = 0
        Me.txtR1.Name = "txtR1"
        Me.txtR1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtR1.Size = New System.Drawing.Size(48, 19)
        Me.txtR1.TabIndex = 2
        Me.txtR1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtR2
        '
        Me.txtR2.AcceptsReturn = True
        Me.txtR2.Location = New System.Drawing.Point(184, 45)
        Me.txtR2.MaxLength = 0
        Me.txtR2.Name = "txtR2"
        Me.txtR2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtR2.Size = New System.Drawing.Size(48, 19)
        Me.txtR2.TabIndex = 3
        Me.txtR2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbSysNo
        '
        Me.cmbSysNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSysNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cmbSysNo.FormattingEnabled = True
        Me.cmbSysNo.Location = New System.Drawing.Point(79, 26)
        Me.cmbSysNo.Name = "cmbSysNo"
        Me.cmbSysNo.Size = New System.Drawing.Size(110, 20)
        Me.cmbSysNo.TabIndex = 0
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.Location = New System.Drawing.Point(79, 109)
        Me.txtRemarks.MaxLength = 16
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(265, 19)
        Me.txtRemarks.TabIndex = 3
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(20, 112)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label36.Size = New System.Drawing.Size(47, 12)
        Me.Label36.TabIndex = 99
        Me.Label36.Text = "Remarks"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.Location = New System.Drawing.Point(79, 82)
        Me.txtItemName.MaxLength = 30
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(265, 19)
        Me.txtItemName.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 71
        Me.Label3.Text = "Item Name"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtChNo
        '
        Me.txtChNo.AcceptsReturn = True
        Me.txtChNo.Location = New System.Drawing.Point(79, 55)
        Me.txtChNo.MaxLength = 0
        Me.txtChNo.Name = "txtChNo"
        Me.txtChNo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtChNo.Size = New System.Drawing.Size(48, 19)
        Me.txtChNo.TabIndex = 1
        Me.txtChNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(26, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 69
        Me.Label7.Text = "CH No."
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(24, 29)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(47, 12)
        Me.Label37.TabIndex = 67
        Me.Label37.Text = "Sys No."
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.chkSpTracking)
        Me.TabPage2.Controls.Add(Me.chkCasMode)
        Me.TabPage2.Controls.Add(Me.chkOutMode)
        Me.TabPage2.Controls.Add(Me.txtPinNoDo)
        Me.TabPage2.Controls.Add(Me.Label52)
        Me.TabPage2.Controls.Add(Me.GroupBox6)
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.txtPinDo)
        Me.TabPage2.Controls.Add(Me.txtPortNoDo)
        Me.TabPage2.Controls.Add(Me.txtFuNoDo)
        Me.TabPage2.Controls.Add(Me.lblDoStart)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(944, 504)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "PID"
        '
        'chkSpTracking
        '
        Me.chkSpTracking.AutoSize = True
        Me.chkSpTracking.Location = New System.Drawing.Point(191, 47)
        Me.chkSpTracking.Name = "chkSpTracking"
        Me.chkSpTracking.Size = New System.Drawing.Size(90, 16)
        Me.chkSpTracking.TabIndex = 107
        Me.chkSpTracking.Text = "SP Tracking"
        Me.chkSpTracking.UseVisualStyleBackColor = True
        '
        'chkCasMode
        '
        Me.chkCasMode.AutoSize = True
        Me.chkCasMode.Location = New System.Drawing.Point(101, 47)
        Me.chkCasMode.Name = "chkCasMode"
        Me.chkCasMode.Size = New System.Drawing.Size(72, 16)
        Me.chkCasMode.TabIndex = 106
        Me.chkCasMode.Text = "Cas Mode"
        Me.chkCasMode.UseVisualStyleBackColor = True
        '
        'chkOutMode
        '
        Me.chkOutMode.AutoSize = True
        Me.chkOutMode.Location = New System.Drawing.Point(18, 47)
        Me.chkOutMode.Name = "chkOutMode"
        Me.chkOutMode.Size = New System.Drawing.Size(72, 16)
        Me.chkOutMode.TabIndex = 105
        Me.chkOutMode.Text = "Out Mode"
        Me.chkOutMode.UseVisualStyleBackColor = True
        '
        'txtPinNoDo
        '
        Me.txtPinNoDo.AcceptsReturn = True
        Me.txtPinNoDo.Location = New System.Drawing.Point(376, 15)
        Me.txtPinNoDo.MaxLength = 0
        Me.txtPinNoDo.Name = "txtPinNoDo"
        Me.txtPinNoDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinNoDo.Size = New System.Drawing.Size(40, 19)
        Me.txtPinNoDo.TabIndex = 104
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.BackColor = System.Drawing.SystemColors.Control
        Me.Label52.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label52.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label52.Location = New System.Drawing.Point(305, 18)
        Me.Label52.Name = "Label52"
        Me.Label52.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label52.Size = New System.Drawing.Size(65, 12)
        Me.Label52.TabIndex = 22
        Me.Label52.Text = "Out Pin No"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaUnit4)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaUnit3)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaUnit2)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaUnit1)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaName4)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaName3)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaName2)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaName1)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaLow4)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaLow3)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaLow2)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaLow1)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaHigh4)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaHigh3)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaHigh2)
        Me.GroupBox6.Controls.Add(Me.txtPidExtParaHigh1)
        Me.GroupBox6.Controls.Add(Me.txtPidExtPara4)
        Me.GroupBox6.Controls.Add(Me.txtPidExtPara3)
        Me.GroupBox6.Controls.Add(Me.txtPidExtPara2)
        Me.GroupBox6.Controls.Add(Me.txtPidExtPara1)
        Me.GroupBox6.Controls.Add(Me.Label51)
        Me.GroupBox6.Controls.Add(Me.Label50)
        Me.GroupBox6.Controls.Add(Me.Label49)
        Me.GroupBox6.Controls.Add(Me.Label48)
        Me.GroupBox6.Controls.Add(Me.Label47)
        Me.GroupBox6.Controls.Add(Me.Label46)
        Me.GroupBox6.Controls.Add(Me.Label44)
        Me.GroupBox6.Controls.Add(Me.Label43)
        Me.GroupBox6.Controls.Add(Me.Label42)
        Me.GroupBox6.Location = New System.Drawing.Point(18, 188)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(527, 173)
        Me.GroupBox6.TabIndex = 21
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "PID EXT Param"
        '
        'txtPidExtParaUnit4
        '
        Me.txtPidExtParaUnit4.Location = New System.Drawing.Point(403, 131)
        Me.txtPidExtParaUnit4.MaxLength = 0
        Me.txtPidExtParaUnit4.Name = "txtPidExtParaUnit4"
        Me.txtPidExtParaUnit4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaUnit4.Size = New System.Drawing.Size(116, 19)
        Me.txtPidExtParaUnit4.TabIndex = 140
        '
        'txtPidExtParaUnit3
        '
        Me.txtPidExtParaUnit3.Location = New System.Drawing.Point(403, 106)
        Me.txtPidExtParaUnit3.MaxLength = 0
        Me.txtPidExtParaUnit3.Name = "txtPidExtParaUnit3"
        Me.txtPidExtParaUnit3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaUnit3.Size = New System.Drawing.Size(116, 19)
        Me.txtPidExtParaUnit3.TabIndex = 135
        '
        'txtPidExtParaUnit2
        '
        Me.txtPidExtParaUnit2.Location = New System.Drawing.Point(403, 81)
        Me.txtPidExtParaUnit2.MaxLength = 0
        Me.txtPidExtParaUnit2.Name = "txtPidExtParaUnit2"
        Me.txtPidExtParaUnit2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaUnit2.Size = New System.Drawing.Size(116, 19)
        Me.txtPidExtParaUnit2.TabIndex = 130
        '
        'txtPidExtParaUnit1
        '
        Me.txtPidExtParaUnit1.Location = New System.Drawing.Point(403, 56)
        Me.txtPidExtParaUnit1.MaxLength = 0
        Me.txtPidExtParaUnit1.Name = "txtPidExtParaUnit1"
        Me.txtPidExtParaUnit1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaUnit1.Size = New System.Drawing.Size(116, 19)
        Me.txtPidExtParaUnit1.TabIndex = 125
        '
        'txtPidExtParaName4
        '
        Me.txtPidExtParaName4.Location = New System.Drawing.Point(275, 131)
        Me.txtPidExtParaName4.MaxLength = 0
        Me.txtPidExtParaName4.Name = "txtPidExtParaName4"
        Me.txtPidExtParaName4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaName4.Size = New System.Drawing.Size(116, 19)
        Me.txtPidExtParaName4.TabIndex = 139
        '
        'txtPidExtParaName3
        '
        Me.txtPidExtParaName3.Location = New System.Drawing.Point(275, 106)
        Me.txtPidExtParaName3.MaxLength = 0
        Me.txtPidExtParaName3.Name = "txtPidExtParaName3"
        Me.txtPidExtParaName3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaName3.Size = New System.Drawing.Size(116, 19)
        Me.txtPidExtParaName3.TabIndex = 134
        '
        'txtPidExtParaName2
        '
        Me.txtPidExtParaName2.Location = New System.Drawing.Point(275, 81)
        Me.txtPidExtParaName2.MaxLength = 0
        Me.txtPidExtParaName2.Name = "txtPidExtParaName2"
        Me.txtPidExtParaName2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaName2.Size = New System.Drawing.Size(116, 19)
        Me.txtPidExtParaName2.TabIndex = 129
        '
        'txtPidExtParaName1
        '
        Me.txtPidExtParaName1.Location = New System.Drawing.Point(275, 56)
        Me.txtPidExtParaName1.MaxLength = 0
        Me.txtPidExtParaName1.Name = "txtPidExtParaName1"
        Me.txtPidExtParaName1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaName1.Size = New System.Drawing.Size(116, 19)
        Me.txtPidExtParaName1.TabIndex = 124
        '
        'txtPidExtParaLow4
        '
        Me.txtPidExtParaLow4.Location = New System.Drawing.Point(210, 131)
        Me.txtPidExtParaLow4.MaxLength = 0
        Me.txtPidExtParaLow4.Name = "txtPidExtParaLow4"
        Me.txtPidExtParaLow4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaLow4.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtParaLow4.TabIndex = 138
        Me.txtPidExtParaLow4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtParaLow3
        '
        Me.txtPidExtParaLow3.Location = New System.Drawing.Point(210, 106)
        Me.txtPidExtParaLow3.MaxLength = 0
        Me.txtPidExtParaLow3.Name = "txtPidExtParaLow3"
        Me.txtPidExtParaLow3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaLow3.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtParaLow3.TabIndex = 133
        Me.txtPidExtParaLow3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtParaLow2
        '
        Me.txtPidExtParaLow2.Location = New System.Drawing.Point(210, 81)
        Me.txtPidExtParaLow2.MaxLength = 0
        Me.txtPidExtParaLow2.Name = "txtPidExtParaLow2"
        Me.txtPidExtParaLow2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaLow2.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtParaLow2.TabIndex = 128
        Me.txtPidExtParaLow2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtParaLow1
        '
        Me.txtPidExtParaLow1.Location = New System.Drawing.Point(210, 56)
        Me.txtPidExtParaLow1.MaxLength = 0
        Me.txtPidExtParaLow1.Name = "txtPidExtParaLow1"
        Me.txtPidExtParaLow1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaLow1.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtParaLow1.TabIndex = 123
        Me.txtPidExtParaLow1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtParaHigh4
        '
        Me.txtPidExtParaHigh4.Location = New System.Drawing.Point(139, 131)
        Me.txtPidExtParaHigh4.MaxLength = 0
        Me.txtPidExtParaHigh4.Name = "txtPidExtParaHigh4"
        Me.txtPidExtParaHigh4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaHigh4.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtParaHigh4.TabIndex = 137
        Me.txtPidExtParaHigh4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtParaHigh3
        '
        Me.txtPidExtParaHigh3.Location = New System.Drawing.Point(139, 106)
        Me.txtPidExtParaHigh3.MaxLength = 0
        Me.txtPidExtParaHigh3.Name = "txtPidExtParaHigh3"
        Me.txtPidExtParaHigh3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaHigh3.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtParaHigh3.TabIndex = 132
        Me.txtPidExtParaHigh3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtParaHigh2
        '
        Me.txtPidExtParaHigh2.Location = New System.Drawing.Point(139, 81)
        Me.txtPidExtParaHigh2.MaxLength = 0
        Me.txtPidExtParaHigh2.Name = "txtPidExtParaHigh2"
        Me.txtPidExtParaHigh2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaHigh2.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtParaHigh2.TabIndex = 127
        Me.txtPidExtParaHigh2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtParaHigh1
        '
        Me.txtPidExtParaHigh1.Location = New System.Drawing.Point(139, 56)
        Me.txtPidExtParaHigh1.MaxLength = 0
        Me.txtPidExtParaHigh1.Name = "txtPidExtParaHigh1"
        Me.txtPidExtParaHigh1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtParaHigh1.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtParaHigh1.TabIndex = 122
        Me.txtPidExtParaHigh1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtPara4
        '
        Me.txtPidExtPara4.Location = New System.Drawing.Point(83, 131)
        Me.txtPidExtPara4.MaxLength = 0
        Me.txtPidExtPara4.Name = "txtPidExtPara4"
        Me.txtPidExtPara4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtPara4.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtPara4.TabIndex = 136
        Me.txtPidExtPara4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtPara3
        '
        Me.txtPidExtPara3.Location = New System.Drawing.Point(83, 106)
        Me.txtPidExtPara3.MaxLength = 0
        Me.txtPidExtPara3.Name = "txtPidExtPara3"
        Me.txtPidExtPara3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtPara3.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtPara3.TabIndex = 131
        Me.txtPidExtPara3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtPara2
        '
        Me.txtPidExtPara2.Location = New System.Drawing.Point(83, 81)
        Me.txtPidExtPara2.MaxLength = 0
        Me.txtPidExtPara2.Name = "txtPidExtPara2"
        Me.txtPidExtPara2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtPara2.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtPara2.TabIndex = 126
        Me.txtPidExtPara2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPidExtPara1
        '
        Me.txtPidExtPara1.Location = New System.Drawing.Point(83, 56)
        Me.txtPidExtPara1.MaxLength = 0
        Me.txtPidExtPara1.Name = "txtPidExtPara1"
        Me.txtPidExtPara1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidExtPara1.Size = New System.Drawing.Size(48, 19)
        Me.txtPidExtPara1.TabIndex = 121
        Me.txtPidExtPara1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.SystemColors.Control
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(401, 39)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(65, 12)
        Me.Label51.TabIndex = 74
        Me.Label51.Text = "Param Unit"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(273, 39)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(65, 12)
        Me.Label50.TabIndex = 73
        Me.Label50.Text = "Param Name"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.BackColor = System.Drawing.SystemColors.Control
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(208, 39)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(59, 12)
        Me.Label49.TabIndex = 72
        Me.Label49.Text = "Param Low"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(137, 39)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(65, 12)
        Me.Label48.TabIndex = 71
        Me.Label48.Text = "Param High"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.SystemColors.Control
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label47.Location = New System.Drawing.Point(81, 39)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(35, 12)
        Me.Label47.TabIndex = 70
        Me.Label47.Text = "Param"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(10, 134)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(65, 12)
        Me.Label46.TabIndex = 69
        Me.Label46.Text = "Ext Param4"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(10, 109)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(65, 12)
        Me.Label44.TabIndex = 68
        Me.Label44.Text = "Ext Param3"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(10, 84)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(65, 12)
        Me.Label43.TabIndex = 67
        Me.Label43.Text = "Ext Param2"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(10, 56)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(65, 12)
        Me.Label42.TabIndex = 66
        Me.Label42.Text = "Ext Param1"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label41)
        Me.GroupBox4.Controls.Add(Me.txtPidDefGAP)
        Me.GroupBox4.Controls.Add(Me.Label35)
        Me.GroupBox4.Controls.Add(Me.txtPidDefTD)
        Me.GroupBox4.Controls.Add(Me.Label34)
        Me.GroupBox4.Controls.Add(Me.txtPidDefTI)
        Me.GroupBox4.Controls.Add(Me.Label33)
        Me.GroupBox4.Controls.Add(Me.txtPidDefPB)
        Me.GroupBox4.Controls.Add(Me.Label31)
        Me.GroupBox4.Controls.Add(Me.txtPidDefMvLow)
        Me.GroupBox4.Controls.Add(Me.Label32)
        Me.GroupBox4.Controls.Add(Me.txtPidDefMvHigh)
        Me.GroupBox4.Controls.Add(Me.Label29)
        Me.GroupBox4.Controls.Add(Me.txtPidDefSpLow)
        Me.GroupBox4.Controls.Add(Me.Label28)
        Me.GroupBox4.Controls.Add(Me.txtPidDefSpHigh)
        Me.GroupBox4.Location = New System.Drawing.Point(18, 81)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(562, 101)
        Me.GroupBox4.TabIndex = 20
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "PID DEF Param"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(458, 21)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(23, 12)
        Me.Label41.TabIndex = 75
        Me.Label41.Text = "GAP"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPidDefGAP
        '
        Me.txtPidDefGAP.AcceptsReturn = True
        Me.txtPidDefGAP.Location = New System.Drawing.Point(487, 18)
        Me.txtPidDefGAP.MaxLength = 0
        Me.txtPidDefGAP.Name = "txtPidDefGAP"
        Me.txtPidDefGAP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidDefGAP.Size = New System.Drawing.Size(48, 19)
        Me.txtPidDefGAP.TabIndex = 118
        Me.txtPidDefGAP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(349, 75)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(17, 12)
        Me.Label35.TabIndex = 73
        Me.Label35.Text = "TD"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPidDefTD
        '
        Me.txtPidDefTD.AcceptsReturn = True
        Me.txtPidDefTD.Location = New System.Drawing.Point(378, 72)
        Me.txtPidDefTD.MaxLength = 0
        Me.txtPidDefTD.Name = "txtPidDefTD"
        Me.txtPidDefTD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidDefTD.Size = New System.Drawing.Size(48, 19)
        Me.txtPidDefTD.TabIndex = 117
        Me.txtPidDefTD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(349, 50)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(17, 12)
        Me.Label34.TabIndex = 71
        Me.Label34.Text = "TI"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPidDefTI
        '
        Me.txtPidDefTI.AcceptsReturn = True
        Me.txtPidDefTI.Location = New System.Drawing.Point(378, 47)
        Me.txtPidDefTI.MaxLength = 0
        Me.txtPidDefTI.Name = "txtPidDefTI"
        Me.txtPidDefTI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidDefTI.Size = New System.Drawing.Size(48, 19)
        Me.txtPidDefTI.TabIndex = 116
        Me.txtPidDefTI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(349, 25)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(17, 12)
        Me.Label33.TabIndex = 69
        Me.Label33.Text = "PB"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPidDefPB
        '
        Me.txtPidDefPB.AcceptsReturn = True
        Me.txtPidDefPB.Location = New System.Drawing.Point(378, 22)
        Me.txtPidDefPB.MaxLength = 0
        Me.txtPidDefPB.Name = "txtPidDefPB"
        Me.txtPidDefPB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidDefPB.Size = New System.Drawing.Size(48, 19)
        Me.txtPidDefPB.TabIndex = 115
        Me.txtPidDefPB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(163, 60)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(41, 12)
        Me.Label31.TabIndex = 67
        Me.Label31.Text = "MV Low"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPidDefMvLow
        '
        Me.txtPidDefMvLow.AcceptsReturn = True
        Me.txtPidDefMvLow.Location = New System.Drawing.Point(252, 57)
        Me.txtPidDefMvLow.MaxLength = 0
        Me.txtPidDefMvLow.Name = "txtPidDefMvLow"
        Me.txtPidDefMvLow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidDefMvLow.Size = New System.Drawing.Size(48, 19)
        Me.txtPidDefMvLow.TabIndex = 114
        Me.txtPidDefMvLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(10, 60)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(47, 12)
        Me.Label32.TabIndex = 65
        Me.Label32.Text = "MV High"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPidDefMvHigh
        '
        Me.txtPidDefMvHigh.AcceptsReturn = True
        Me.txtPidDefMvHigh.Location = New System.Drawing.Point(99, 57)
        Me.txtPidDefMvHigh.MaxLength = 0
        Me.txtPidDefMvHigh.Name = "txtPidDefMvHigh"
        Me.txtPidDefMvHigh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidDefMvHigh.Size = New System.Drawing.Size(48, 19)
        Me.txtPidDefMvHigh.TabIndex = 113
        Me.txtPidDefMvHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(163, 27)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(77, 12)
        Me.Label29.TabIndex = 63
        Me.Label29.Text = "SetPoint Low"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPidDefSpLow
        '
        Me.txtPidDefSpLow.AcceptsReturn = True
        Me.txtPidDefSpLow.Location = New System.Drawing.Point(252, 24)
        Me.txtPidDefSpLow.MaxLength = 0
        Me.txtPidDefSpLow.Name = "txtPidDefSpLow"
        Me.txtPidDefSpLow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidDefSpLow.Size = New System.Drawing.Size(48, 19)
        Me.txtPidDefSpLow.TabIndex = 112
        Me.txtPidDefSpLow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(10, 27)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(83, 12)
        Me.Label28.TabIndex = 61
        Me.Label28.Text = "SetPoint High"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPidDefSpHigh
        '
        Me.txtPidDefSpHigh.AcceptsReturn = True
        Me.txtPidDefSpHigh.Location = New System.Drawing.Point(99, 24)
        Me.txtPidDefSpHigh.MaxLength = 0
        Me.txtPidDefSpHigh.Name = "txtPidDefSpHigh"
        Me.txtPidDefSpHigh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPidDefSpHigh.Size = New System.Drawing.Size(48, 19)
        Me.txtPidDefSpHigh.TabIndex = 111
        Me.txtPidDefSpHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPinDo
        '
        Me.txtPinDo.AcceptsReturn = True
        Me.txtPinDo.Location = New System.Drawing.Point(233, 15)
        Me.txtPinDo.MaxLength = 0
        Me.txtPinDo.Name = "txtPinDo"
        Me.txtPinDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinDo.Size = New System.Drawing.Size(40, 19)
        Me.txtPinDo.TabIndex = 103
        '
        'txtPortNoDo
        '
        Me.txtPortNoDo.AcceptsReturn = True
        Me.txtPortNoDo.Location = New System.Drawing.Point(191, 15)
        Me.txtPortNoDo.MaxLength = 0
        Me.txtPortNoDo.Name = "txtPortNoDo"
        Me.txtPortNoDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPortNoDo.Size = New System.Drawing.Size(40, 19)
        Me.txtPortNoDo.TabIndex = 102
        '
        'txtFuNoDo
        '
        Me.txtFuNoDo.AcceptsReturn = True
        Me.txtFuNoDo.Location = New System.Drawing.Point(149, 15)
        Me.txtFuNoDo.MaxLength = 0
        Me.txtFuNoDo.Name = "txtFuNoDo"
        Me.txtFuNoDo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFuNoDo.Size = New System.Drawing.Size(40, 19)
        Me.txtFuNoDo.TabIndex = 101
        '
        'lblDoStart
        '
        Me.lblDoStart.AutoSize = True
        Me.lblDoStart.BackColor = System.Drawing.SystemColors.Control
        Me.lblDoStart.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDoStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDoStart.Location = New System.Drawing.Point(16, 18)
        Me.lblDoStart.Name = "lblDoStart"
        Me.lblDoStart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDoStart.Size = New System.Drawing.Size(119, 12)
        Me.lblDoStart.TabIndex = 19
        Me.lblDoStart.Text = "Out Start FU Adress"
        Me.lblDoStart.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmChListPID
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(972, 584)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lblDummy)
        Me.Controls.Add(Me.cmdBeforeCH)
        Me.Controls.Add(Me.cmdNextCH)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.cmdCancel)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(4, 30)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmChListPID"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "CHANNEL LIST PID"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.fraHiHi0.ResumeLayout(False)
        Me.fraHiHi0.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.fraAlarm.ResumeLayout(False)
        Me.fraAlarm.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cmdNextCH As System.Windows.Forms.Button
    Public WithEvents cmdBeforeCH As System.Windows.Forms.Button
    Friend WithEvents lblDummy As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbRanDumy As System.Windows.Forms.ComboBox
    Friend WithEvents lblRangeCaution As System.Windows.Forms.Label
    Public WithEvents chkPSDisp As System.Windows.Forms.CheckBox
    Public WithEvents chkPowerFactor As System.Windows.Forms.CheckBox
    Public WithEvents lblDecPoint As System.Windows.Forms.Label
    Public WithEvents fraHiHi0 As System.Windows.Forms.GroupBox
    Public WithEvents chkSover As System.Windows.Forms.CheckBox
    Public WithEvents chkSunder As System.Windows.Forms.CheckBox
    Public WithEvents txtStatusSF As System.Windows.Forms.TextBox
    Public WithEvents txtStatusLoLo As System.Windows.Forms.TextBox
    Public WithEvents txtStatusLo As System.Windows.Forms.TextBox
    Public WithEvents txtStatusHi As System.Windows.Forms.TextBox
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Public WithEvents txtStatusHiHi As System.Windows.Forms.TextBox
    Public WithEvents txtValueHiHi As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbValueSensorFailure As System.Windows.Forms.ComboBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtExtGSensorFailure As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtGRep2SensorFailure As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2LoLo As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2Lo As System.Windows.Forms.TextBox
    Public WithEvents Label64 As System.Windows.Forms.Label
    Public WithEvents txtGRep1SensorFailure As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1LoLo As System.Windows.Forms.TextBox
    Public WithEvents txtExtGLoLo As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2Hi As System.Windows.Forms.TextBox
    Public WithEvents txtDelaySensorFailure As System.Windows.Forms.TextBox
    Public WithEvents txtDelayLoLo As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1Lo As System.Windows.Forms.TextBox
    Public WithEvents txtDelayLo As System.Windows.Forms.TextBox
    Public WithEvents txtValueLoLo As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1Hi As System.Windows.Forms.TextBox
    Public WithEvents txtExtGLo As System.Windows.Forms.TextBox
    Public WithEvents txtValueLo As System.Windows.Forms.TextBox
    Public WithEvents txtExtGHi As System.Windows.Forms.TextBox
    Public WithEvents txtDelayHiHi As System.Windows.Forms.TextBox
    Public WithEvents txtDelayHi As System.Windows.Forms.TextBox
    Public WithEvents txtGRep2HiHi As System.Windows.Forms.TextBox
    Public WithEvents txtExtGHiHi As System.Windows.Forms.TextBox
    Public WithEvents txtValueHi As System.Windows.Forms.TextBox
    Public WithEvents txtGRep1HiHi As System.Windows.Forms.TextBox
    Public WithEvents Label128 As System.Windows.Forms.Label
    Public WithEvents Label129 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents lblHyphen As System.Windows.Forms.Label
    Public WithEvents txtRangeTo As System.Windows.Forms.TextBox
    Public WithEvents txtRangeFrom As System.Windows.Forms.TextBox
    Public WithEvents txtUnit As System.Windows.Forms.TextBox
    Public WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents cmbExtDevice As System.Windows.Forms.ComboBox
    Public WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Public WithEvents txtPin As System.Windows.Forms.TextBox
    Public WithEvents txtPortNo As System.Windows.Forms.TextBox
    Public WithEvents txtFuNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbRangeType As System.Windows.Forms.ComboBox
    Public WithEvents lblRange As System.Windows.Forms.Label
    Public WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbDataType As System.Windows.Forms.ComboBox
    Public WithEvents lblFuAddress As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtOffset As System.Windows.Forms.TextBox
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents lblNorHyphen As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents txtDecimal As System.Windows.Forms.TextBox
    Public WithEvents txtLowNormal As System.Windows.Forms.TextBox
    Public WithEvents chkCenterGraph As System.Windows.Forms.CheckBox
    Public WithEvents txtHighNormal As System.Windows.Forms.TextBox
    Public WithEvents Label123 As System.Windows.Forms.Label
    Public WithEvents txtString As System.Windows.Forms.TextBox
    Public WithEvents lblDecimal As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents cmbAlmLvl As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtTag As System.Windows.Forms.TextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents cmbTime As System.Windows.Forms.ComboBox
    Public WithEvents chkMrepose As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Public WithEvents txtShareChid As System.Windows.Forms.TextBox
    Public WithEvents lblShareChid As System.Windows.Forms.Label
    Public WithEvents lblShareType As System.Windows.Forms.Label
    Public WithEvents cmbShareType As System.Windows.Forms.ComboBox
    Public WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents lblBitSet As System.Windows.Forms.Label
    Public WithEvents txtSP As System.Windows.Forms.TextBox
    Public WithEvents txtPLC As System.Windows.Forms.TextBox
    Public WithEvents txtEP As System.Windows.Forms.TextBox
    Public WithEvents txtAC As System.Windows.Forms.TextBox
    Public WithEvents txtRL As System.Windows.Forms.TextBox
    Public WithEvents txtWK As System.Windows.Forms.TextBox
    Public WithEvents txtGWS As System.Windows.Forms.TextBox
    Public WithEvents txtSio As System.Windows.Forms.TextBox
    Public WithEvents txtSC As System.Windows.Forms.TextBox
    Public WithEvents txtDmy As System.Windows.Forms.TextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents fraAlarm As System.Windows.Forms.GroupBox
    Public WithEvents txtDelayTimer As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents txtExtGroup As System.Windows.Forms.TextBox
    Public WithEvents txtR1 As System.Windows.Forms.TextBox
    Public WithEvents txtR2 As System.Windows.Forms.TextBox
    Friend WithEvents cmbSysNo As System.Windows.Forms.ComboBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtChNo As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents txtPidDefSpLow As System.Windows.Forms.TextBox
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents txtPidDefSpHigh As System.Windows.Forms.TextBox
    Public WithEvents txtPinDo As System.Windows.Forms.TextBox
    Public WithEvents txtPortNoDo As System.Windows.Forms.TextBox
    Public WithEvents txtFuNoDo As System.Windows.Forms.TextBox
    Public WithEvents lblDoStart As System.Windows.Forms.Label
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Public WithEvents txtPidExtParaUnit4 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaUnit3 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaUnit2 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaUnit1 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaName4 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaName3 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaName2 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaName1 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaLow4 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaLow3 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaLow2 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaLow1 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaHigh4 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaHigh3 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaHigh2 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtParaHigh1 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtPara4 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtPara3 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtPara2 As System.Windows.Forms.TextBox
    Public WithEvents txtPidExtPara1 As System.Windows.Forms.TextBox
    Public WithEvents Label51 As System.Windows.Forms.Label
    Public WithEvents Label50 As System.Windows.Forms.Label
    Public WithEvents Label49 As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents Label47 As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents txtPidDefGAP As System.Windows.Forms.TextBox
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents txtPidDefTD As System.Windows.Forms.TextBox
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents txtPidDefTI As System.Windows.Forms.TextBox
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents txtPidDefPB As System.Windows.Forms.TextBox
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents txtPidDefMvLow As System.Windows.Forms.TextBox
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents txtPidDefMvHigh As System.Windows.Forms.TextBox
    Public WithEvents txtPinNoDo As System.Windows.Forms.TextBox
    Public WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents chkSpTracking As System.Windows.Forms.CheckBox
    Friend WithEvents chkCasMode As System.Windows.Forms.CheckBox
    Friend WithEvents chkOutMode As System.Windows.Forms.CheckBox
#End Region

End Class