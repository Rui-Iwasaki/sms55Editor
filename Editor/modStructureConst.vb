﻿Imports Microsoft.VisualBasic
Imports System.Runtime.InteropServices

Module modStructureConst

    'Ver2.0.7.H
    'System.Text.Encoding.UTF8をSystem.Text.Encoding.GetEncoding("shift_jis")へ変換
    '保安庁日本語対策

#Region "変数定義"

    ''出力構造体クラス
    Public gudt As New clsStructure

    ' ''システム設定
    'Public gudt.SetSystem As gTypSetSystem

    ' ''FU設定
    'Public gudt.SetFu As gTypSetFu

    ' ''チャンネル情報データ（表示名設定データ）
    'Public gudt.SetChDisp As gTypSetChDisp

    ' ''チャンネル情報
    'Public gudt.SetChInfo As gTypSetChInfo

    ' ''コンポジット設定
    'Public gudt.SetChComposite As gTypSetChComposite

    ' ''出力チャンネル設定
    'Public gudt.SetChOutput As gTypSetChOutput

    ' ''論理出力設定
    'Public gudt.SetChAndOr As gTypSetChAndOr

    ' ''グループ設定
    'Public gudt.SetChGroupSetM As gTypSetChGroupSet
    'Public gudt.SetChGroupSetC As gTypSetChGroupSet

    ' ''リポーズ入力設定
    'Public gudt.SetChGroupRepose As gTypSetChGroupRepose

    ' ''積算データ設定ファイル
    'Public gudt.SetChRunHour As gTypSetChRunHour

    ' ''排ガス演算処理設定
    'Public gudt.SetChExhGus As gTypSetChExhGus

    ' ''コントロール使用可／不可設定
    'Public gudt.SetChCtrlUseM As gTypSetChCtrlUse
    'Public gudt.SetChCtrlUseC As gTypSetChCtrlUse

    ' ''SIO設定
    'Public gudt.SetChSio As gTypSetChSio

    ' ''SIO設定CH設定
    'Public gudt.SetChSioCh() As gTypSetChSioCh

    ' ''データ保存テーブル
    'Public gudt.SetChDataSave As gTypSetChDataSave

    ' ''データ転送テーブル設定
    'Public gudt.SetChDataForward As gTypSetChDataForward

    ' ''延長警報設定
    'Public gudt.SetExtAlarm As gTypSetExtAlarm

    ' ''タイマ設定
    'Public gudt.SetExtTimerSet As gTypSetExtTimerSet

    ' ''タイマ表示名称設定
    'Public gudt.SetExtTimerName As gTypSetExtTimerName

    ' ''シーケンスID
    'Public gudt.SetSeqID As gTypSetSeqID

    ' ''シーケンス設定
    'Public gudt.SetSeqSet As gTypSetSeqSet

    ' ''リニアライズテーブル
    'Public gudt.SetSeqLinear As gTypSetSeqLinear

    ' ''演算式テーブル
    'Public gudt.SetSeqOpeExp As gTypSetSeqOperationExpression

    ' ''OPS画面タイトル
    'Public gudt.SetOpsScreenTitleM As gTypSetOpsScreenTitle
    'Public gudt.SetOpsScreenTitleC As gTypSetOpsScreenTitle

    ' ''プルダウンメニュー
    'Public gudt.SetOpsPulldownMenuM As gTypSetOpsPulldownMenu
    'Public gudt.SetOpsPulldownMenuC As gTypSetOpsPulldownMenu

    ' ''OPSグラフ設定
    'Public gudt.SetOpsGraphM As gTypSetOpsGraph
    'Public gudt.SetOpsGraphC As gTypSetOpsGraph

    ' ''ログフォーマット
    'Public gudt.SetOpsLogFormatM As gTypSetOpsLogFormat
    'Public gudt.SetOpsLogFormatC As gTypSetOpsLogFormat

    ' ''CH変換テーブル
    'Public gudt.SetChConvNow As gTypSetChConv    ''現Ver
    'Public gudt.SetChConvPrev As gTypSetChConv   ''前Ver

    ' ''デフォルトデータ作成用
    ' ''ログ印字設定
    ''Public gudt.SetOtherLogTime As gTypSetOtherLogTime

    ' ''フリーディスプレイ
    'Public gudt.SetOpsFreeDisplayM As gTypSetOpsFreeDisplay
    'Public gudt.SetOpsFreeDisplayC As gTypSetOpsFreeDisplay

    ' ''トレンドグラフ
    'Public gudt.SetOpsTrendGraphM As gTypSetOpsTrendGraph
    'Public gudt.SetOpsTrendGraphC As gTypSetOpsTrendGraph


#End Region

#Region "ヘッダー"

    ''ヘッダー構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetHeader

        '''<summary>
        '''バージョン
        '''半角8文字
        '''</summary>
        <VBFixedStringAttribute(8)> _
        Dim strVersion As String

        '''<summary>
        '''年月日
        '''半角8文字
        '''</summary>
        <VBFixedStringAttribute(8)> _
        Dim strDate As String

        '''<summary>
        '''時分
        '''半角4文字
        '''</summary>
        <VBFixedStringAttribute(4)> _
        Dim strTime As String

        ''データレコード数
#Region "        Dim shtRecs As Short"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtRecs() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtRecs), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtRecs = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtRecs As Short

#End Region

        ''データレコードサイズ
#Region "        Dim shtSize1 As Short"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSize1() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSize1), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSize1 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSize1 As Short

#End Region
#Region "        Dim shtSize2 As Short"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSize2() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSize2), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSize2 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSize2 As Short

#End Region
#Region "        Dim shtSize3 As Short"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSize3() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSize3), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSize3 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSize3 As Short

#End Region
#Region "        Dim shtSize4 As Short"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSize4() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSize4), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSize4 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSize4 As Short

#End Region
#Region "        Dim shtSize5 As Short"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSize5() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSize5), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSize5 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSize5 As Short

#End Region

    End Structure

#End Region

#Region "システム設定"

    ''システム設定構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSystem

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        '''<summary>
        '''システム設定レコード
        '''</summary>
        Dim udtSysSystem As gTypSetSysSystem

        '''<summary>
        '''FCU設定レコード
        '''</summary>
        Dim udtSysFcu As gTypSetSysFcu

        '''<summary>
        '''OPS設定レコード
        '''</summary>
        Dim udtSysOps As gTypSetSysOps

        '''<summary>
        '''GWS設定レコード
        '''</summary>
        Dim udtSysGws As gTypSetSysGws

        '''<summary>
        '''プリンタ設定レコード
        '''</summary>
        Dim udtSysPrinter As gTypSetSysPrinter

    End Structure

#Region "システム設定"

    ''システム設定構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSysSystem

        '''<summary>
        '''日本語対応
        '''半角32文字
        '''</summary>
        <VBFixedStringAttribute(32)> _
        Dim strShipName As String

        '''<summary>
        '''システムクロック
        '''0:外部
        '''1:内部
        '''</summary>
        Dim shtClock As Short

        '''<summary>
        '''日付フォーマット 
        '''01H:dd/mm/yy
        '''02H:mm/dd/yy
        '''03H:yy/mm/dd
        '''11H:ff/mm/'yy
        '''12H:mm/dd/'yy
        '''13H:'yy/mm/dd
        '''</summary>
        Dim shtDate As Short

        '''<summary>
        '''日本語対応
        '''0:英文
        '''1:和文
        '''2:全和文 
        '''</summary>
        Dim shtLanguage As Short

        '''<summary>
        '''コンバイン有無
        '''0:なし
        '''1:Machinery/Cargo
        '''2:Machinery/HULL
        '''</summary>
        Dim shtCombineUse As Short

        '''<summary>
        '''コンバインセパレート
        '''Bit0:fs/bsセパレート
        '''Bit1～Bit7:予備
        '''</summary>
        Dim shtCombineSeparate As Short

        '''<summary>
        '''予備
        '''</summary>
        Dim shtSpare2 As Short

        '''<summary>
        '''GWS1設定
        '''Bit0:GWS有無
        '''Bit1:Ethernet Line A Only
        '''Bit2:Ethernet Line A and B
        '''Bit3～Bit7:予備
        '''</summary>
        Dim shtGWS1 As Short

        '''<summary>
        '''GWS2設定
        '''Bit0:GWS2有無
        '''Bit1:Ethernet Line A Only
        '''Bit2:Ethernet Line A and B
        '''Bit3～Bit7:予備
        '''</summary>
        Dim shtGWS2 As Short

        '''<summary>
        '''取扱説明書(言語) 日本語対応 2015.02.05
        '''0:英文
        '''1:和文
        '''</summary>
        Dim shtManual As Short

        '''<summary>
        '''GL船級仕様
        ''' 0:通常
        ''' 1:GL
        '''</summary>
        Dim shtgl_spec As Short

        '''<summary>
        '''保安庁　初期表示画面
        '''</summary>
        Dim shthoan_gno As Short    'Ver2.0.7.H 保安庁対応


        '''<summary>
        '''予備
        '''</summary>
        Dim shtSpare As Short
        'Ver2.0.7.H 保安庁対応
        '<VBFixedArray(1)> _
        'Dim shtSpare() As Short


        '''<summary>
        '''配列数初期化
        '''</summary>
        Public Sub InitArray()
            'Ver2.0.7.H 保安庁対応　コメント化
            'ReDim shtSpare(1)
        End Sub

    End Structure

#End Region

#Region "FCU設定"

    ''FCU設定
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSysFcu

        ' '''<summary>
        ' '''予備 2011.12.13 K.Tanigawa 予備キャンセル 
        ' '''</summary>
        ' Dim shtSpare2 As Short

        ''''<summary>
        ''''CANBUS対応
        ''''0:無し
        ''''1:有り
        ''''</summary>
        'Dim shtCanbus As Short

        ''''<summary>
        ''''MODBUS対応
        ''''0:無し
        ''''1:有り
        ''''</summary>
        'Dim shtModbus As Short

        '''<summary>
        '''FCU台数　ver.1.4.0 2011.12.13 K.Tanigawa
        '''1～2（台）
        '''</summary>
        Dim shtFcuCnt As Short

        '''<summary>
        '''FCU番号　台数→番号に変更　ver.1.4.0 2011.09.29
        '''1～2（台）
        '''</summary>
        Dim shtFcuNo As Short

        '''<summary>
        '''収集周期
        '''1～255（秒）
        '''</summary>
        Dim shtCrrectTime As Short

        '''<summary>
        '''FCU拡張ボード
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtFcuExtendBord As Short

        '''<summary>
        '''FCU拡張ボード２（現状使用していない）
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtFcuExtendBord2 As Short

        '''<summary>
        '''共有CH有無
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtShareChUse As Short

        '' Ver1.9.3 2016.01.21 追加
        ''ｼｽﾃﾑ設定 ﾋﾞｯﾄ単位で参照のこと
        ''1:FCU通信用拡張あり
#Region "        Dim FCUOption As Short                                      ''FCUｵﾌﾟｼｮﾝ設定"
        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtFCUOption() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtFCUOption), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtFCUOption = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtFCUOption As Short

#End Region


        'Ver2.0.3.6
        'PT,PTフラグ 0=PT 1=JPT
#Region "        Dim PtJptFlg As Short                                      'PT,JPTフラグ"
        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtPtJptFlg() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtPtJptFlg), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtPtJptFlg = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtPtJptFlg As Short

#End Region

        'Ver2.0.7.V
        'FCU設定フラグ BIT単位
#Region "        Dim FCU2Flg As Short                              'FCU設定フラグ"
        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtFCU2Flg() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtFCU2Flg), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtFCU2Flg = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtFCU2Flg As Short

#End Region


        '''<summary>
        '''予備
        ''' Ver1.9.3 2016.01.21 要素数　21 → 20
        ''' Ver2.0.3.6 要素数 20→19
        ''' Ver2.0.7.V 要素数 19→18
        '''</summary>
        <VBFixedArray(18)> _
        Dim shtSpare() As Short

        '''<summary>
        '''配列数初期化
        ''' Ver1.9.3 2016.01.21 要素数　21 → 20
        ''' Ver2.0.3.6 要素数 20→19
        ''' Ver2.0.7.V 要素数 19→18
        '''</summary>
        Public Sub InitArray()
            ReDim shtSpare(18)
        End Sub

    End Structure

#End Region

#Region "OPS設定"

#Region "OPS共通"

    ''OPS共通設定
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSysOps

        '''<summary>
        '''各機器遠隔操作許可
        '''0:禁止
        '''1:許可
        '''</summary>
        Dim shtControl As Short

        '''<summary>
        '''EXTグループ,グループリポーズ変更禁止
        '''0:全て許可
        '''1:禁止（EXT.GR）
        '''2:禁止（G.REP）
        '''3:禁止（EXT.GR + G.REP）
        '''</summary>
        Dim shtProhibition As Short

        '''<summary>
        '''チャンネルデータ変更禁止
        '''0:変更禁止
        '''1:変更許可
        '''</summary>
        Dim shtChannelEdit As Short

        '''<summary>
        '''アラーム表示方法
        '''1:Active Only
        '''2:System Or
        '''</summary>
        Dim shtAlarm As Short

        '''<summary>
        '''Duty設定 可/不可フラグ
        '''0:不可
        '''1:可
        '''</summary>
        Dim shtDuty As Short

        '''<summary>
        '''コントロール1台のみのインターロック
        '''0:インターロック無し
        '''1:インターロック有り
        '''</summary>
        Dim shtContOnlyFlag As Short

        '''<summary>
        '''Auto Alarm 表示順序　2015/5/27 T.Ueki
        '''</summary>
        Dim shtAlarm_Order As Short

        '''<summary>
        ''' ﾀｸﾞ番号 表示ﾓｰﾄﾞ　2015.10.22 Ver1.7.5
        '''</summary>
        Dim shtTagMode As Short


        '''<summary>
        '''ﾛｲﾄﾞ対応 表示ﾓｰﾄﾞ　2015.11.12 Ver1.7.8
        '''</summary>
        Dim shtLRMode As Short


        ''ｼｽﾃﾑ動作ﾌﾗｸﾞ　ﾋﾞｯﾄ単位で参照すること　2016.01.21 Ver1.9.3
        '' 0x01:ﾋｽﾄﾘ　自動更新あり
        '' 0x02:Mach/Hull FCU2台仕様時の設定       '' Ver1.11.8.2 2016.11.01
        '' 0x03:SET権レベル対応   Ver1.11.8.8 2016.11.17
        '' 0x04:Auto Alm 火災警報特殊仕様フラグ    Ver2.0.0.2 南日本M761対応 2017.02.27追加
#Region "        Dim shtSystem As Short                 ''OPSｼｽﾃﾑ設定"
        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSystem() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSystem), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSystem = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSystem As Short

#End Region


#Region "        Dim shtBS_CHNo As Short                          ''BS CH出力 CHNo. 2016.01.21 Ver1.9.3"
        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtBS_CHNo() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtBS_CHNo), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtBS_CHNo = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtBS_CHNo As Short

#End Region


#Region "        Dim shtFS_CHNo As Short                          ''FS CH出力 CHNo. 2016.01.21 Ver1.9.3"
        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtFS_CHNo() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtFS_CHNo), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtFS_CHNo = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtFS_CHNo As Short

#End Region

#Region "        Dim shtTerVer As Short                           '基板ﾊﾞｰｼﾞｮﾝﾌﾗｸﾞ Ver2.0.7.0 基板ﾊﾞｰｼﾞｮﾝ印刷対応"
        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtTerVer() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtTerVer), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtTerVer = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtTerVer As Short

#End Region


        '''<summary>
        '''予備
        ''' 2015.10.22 Ver1.7.5  ﾀｸﾞ表示ﾓｰﾄﾞ追加のため要素数変更　　20 → 19
        ''' 2015.11.12 Ver1.7.4  ﾀｸﾞ表示ﾓｰﾄﾞ追加のため要素数変更　　20 → 18
        ''' 2016.01.21 Ver1.9.3  ため要素数変更　　18 → 15
        ''' 2017.10.20 Ver2.0.7.0 基板ﾊﾞｰｼﾞｮﾝﾌﾗｸﾞ追加のため要素数変更  15→14
        '''</summary>
        <VBFixedArray(14)> _
        Dim shtSpare() As Short

        '''<summary>
        '''OPS個別設定
        '''全１０台分設定
        '''</summary>
        <VBFixedArray(9)> _
        Dim udtOpsDetail() As gTypSetSysOpsDetail

        '''<summary>
        '''配列数初期化
        ''' Ver1.8.9 2015.12.12 要素数変更　　20 → 18
        ''' Ver1.9.3 2016.01.21 要素数変更　　18 → 15]
        ''' Ver2.0.7.1 2017.10.26 要素数変更　　15 → 14]
        '''</summary>
        Public Sub InitArray()
            ReDim shtSpare(14)
            ReDim udtOpsDetail(9)
        End Sub

    End Structure

#End Region

#Region "OPS個別"

    ''OPS個別設定
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSysOpsDetail

        '''<summary>
        '''OPS接続有無
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtExist As Short

        '''<summary>
        '''アラーム表示モード
        '''0:INHIBIT
        '''1:AUTO
        '''2:WINDOW
        '''</summary>
        Dim shtAlarmDisp As Short

        '''<summary>
        '''OPS設定変更
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtEnable As Short

        '''<summary>
        '''遠隔操作
        '''Bit0:Func
        '''Bit1:Initial
        '''Bit2～Bit7:予備
        '''</summary>
        Dim shtControl As Short

        '''<summary>
        '''コントロール制限
        '''Bit0:１チェック
        '''Bit1:２チェック
        '''Bit2:４チェック
        '''Bit3:８チェック
        '''Bit4～Bit7:予備
        '''例：[1:ECC][2:EMC][4:WCC]
        '''</summary>
        Dim shtControlFlag As Short

        ''コントロール入力禁止
        ''0x0000～0xFFFF
        ''例：[1:FO禁止][2:FW系禁止]
#Region "        Dim shtControlProhFlag As Short"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtControlProhFlag() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtControlProhFlag), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtControlProhFlag = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtControlProhFlag As Short

#End Region

        '''<summary>
        '''オペレーションパネル
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtOperaionPanel As Short

        '''<summary>
        '''調光機能
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtAdjustLight As Short

        '''<summary>
        '''HATTELAND製液晶接続
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtHatteland As Short

        '''<summary>
        '''印字パート
        '''Bit0:Machinery
        '''Bit1:Cargo
        '''Bit2～Bit7:予備
        '''</summary>
        Dim shtPrintPart As Short

        '''<summary>
        '''OPS表示モード（コンバイン時のみ）
        '''Bit0:Machinery
        '''Bit1:Cargo
        '''Bit2～Bit7:予備
        '''</summary>
        Dim shtOpsType As Short

        '''<summary>
        '''起動モード（OPSType連動）
        '''Bit0:Machinery
        '''Bit1:Cargo
        '''Bit2～Bit7:予備
        '''</summary>
        Dim shtBootMode As Short

        '''<summary>
        '''リポーズサマリ
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtRepSum As Short

        '''<summary>
        '''通信Aラインのみ使用
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtEtherA As Short

        '''<summary>
        '''OPS解像度
        '''1:1024x768
        '''2:1280x768
        '''</summary>
        Dim shtResolution As Short


        '' Ver1.9.3 2016.01.21 追加
        ''ｼｽﾃﾑ設定　ﾋﾞｯﾄ単位で参照のこと
        ''1:BS/FSあり　ECCのみ

#Region "        Dim shtSysSet As Short"
        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSysSet() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSysSet), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSysSet = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSysSet As Short

#End Region


        '''<summary>
        '''予備
        ''' Ver1.9.3 2016.01.21 要素数　12 → 11
        '''</summary>
        <VBFixedArray(11)> _
        Dim shtSpare() As Short

        '''<summary>
        '''配列数初期化
        ''' Ver1.9.3 2016.01.21 要素数　12 → 11
        '''</summary>
        Public Sub InitArray()
            ReDim shtSpare(11)
        End Sub

    End Structure

#End Region

#End Region

#Region "GWS設定"

#Region "GWS共通"

    ''GWS共通設定
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSysGws

        '''<summary>
        '''GWS個別設定
        '''全２台分設定
        '''</summary>
        <VBFixedArray(1)> _
        Dim udtGwsDetail() As gTypSetSysGwsDetail

        '''<summary>
        '''配列数初期化
        '''</summary>
        Public Sub InitArray()
            ReDim udtGwsDetail(1)
        End Sub

    End Structure

#End Region

#Region "GWS個別"

    ''GWS個別設定
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSysGwsDetail

        '''<summary>
        '''OPS接続有無
        '''0:無し
        '''1:共有ファイル、2:Ext.VDU通信
        '''</summary>
        Dim shtGwsType As Short

        '''<summary>
        '''IPアドレス
        '''</summary>
        Dim bytIP1 As Byte
        Dim bytIP2 As Byte
        Dim bytIP3 As Byte
        Dim bytIP4 As Byte

        '''<summary>
        '''予備
        '''</summary>
        <VBFixedArray(4)> _
        Dim shtSpare() As Short

        '''<summary>
        '''GWS共有ファイル設定
        '''4ファイル分
        '''</summary>
        <VBFixedArray(3)> _
        Dim udtGwsFileInfo() As gTypGwsFileInfo

        '''<summary>
        '''予備
        '''</summary>
        <VBFixedArray(7)> _
        Dim shtSpare2() As Short

        '''<summary>
        '''配列数初期化
        '''</summary>
        Public Sub InitArray()
            ReDim shtSpare(4)
            ReDim udtGwsFileInfo(3)
            ReDim shtSpare2(7)
        End Sub

    End Structure

    ''共有ファイル情報
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypGwsFileInfo

        Dim bytType As Byte         '' ファイルタイプ
        Dim bytSetFlg As Byte       '' 設定フラグ    0x01:全チャンネル保存
        Dim bytBkupCnt As Byte      '' バックアップ有無
        Dim bytspare As Byte        '' 予備
        Dim shtInterval As Short    '' インターバル周期

    End Structure


#End Region

#End Region

#Region "プリンタ設定"

#Region "プリンタ共通"

    ''プリンタ共通設定
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSysPrinter

        '''<summary>
        '''用紙サイズA3（ログプリンタ）
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtAutoCnt As Short     ' 2013.07.22 自動印字最大数追加  K.Fujimoto

        '''<summary>
        '''英数・日本語設定（アラームプリンタ）
        '''1:SingleSize
        '''2:DoubleSize
        '''3:Double(Reduction)
        '''</summary>
        Dim shtPrintType As Short

        '''<summary>
        '''イベントプリント（アラームプリンタ）
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtEventPrint As Short

        '''<summary>
        '''ヌーンログ下線
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtNoonUnder As Short

        '''<summary>
        '''デマンドログ改ページ
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtDemandPage As Short

        '''<summary>
        '''Machinery/Cargo印字
        '''0:無し
        '''1:有り
        '''</summary>
        Dim shtMachineryCargoPrint As Short

        '' Ver1.9.3 2016.01.21 ﾛｸﾞ印字特殊設定
        ''下絵の上にﾃﾞｰﾀを印字
        ''0:無し
        ''0以外:下絵ﾌｧｲﾙ番号

#Region "        Dim shtLogDrawNo As Short"
        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtLogDrawNo() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtLogDrawNo), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtLogDrawNo = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtLogDrawNo As Short

#End Region


        '''<summary>
        '''自動印字する場合のデータ個数(Cargo用)
        '''範囲：1-100
        '''</summary>
        Dim shtAutoCntCargo As Short     ' 2019.02.05 倉重


        '''<summary>
        '''予備B
        ''' Ver1.9.3 2016.01.21 要素数 21 → 20
        ''' Ver2.0.8.H 2019.02.05 要素数 20 → 19
        '''</summary>
        <VBFixedArray(19)> _
        Dim shtSpare() As Short

        '''<summary>
        '''プリンタ個別設定
        '''全６台分設定（予備１台）
        '''</summary>
        <VBFixedArray(5)> _
        Dim udtPrinterDetail() As gTypSetSysPrinterDetail

        '''<summary>
        '''配列数初期化
        '''</summary>
        Public Sub InitArray()
            ReDim shtSpare(19)
            ReDim udtPrinterDetail(5)
        End Sub

    End Structure

#End Region

#Region "プリンタ個別"

    ''プリンタ個別設定
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSysPrinterDetail

        '''<summary>
        '''プリンタ有無
        '''【ログプリンタ】0:無し、1:ドットプリンタ[NADA]、2:ドットプリンタ[NEC]、3:ネットワークプリンタ
        '''【アラームプリンタ】0:無し、1:有り
        '''【HCプリンタ】0:無し、1:有り
        '''</summary>
        Dim bytPrinter As Byte

        '''<summary>
        '''印字有無 
        '''Bit0:印字有無
        '''Bit1:バックアップ印字有無
        '''Bit2～Bit7:予備
        '''</summary>
        Dim shtPrintUse As Byte

        '''<summary>
        '''印字パート
        '''Bit0:Machinery
        '''Bit1:Cargo
        '''Bit2～Bit7:予備
        '''</summary>
        Dim shtPart As Short

        '''<summary>
        '''ドライバ
        '''半角16文字
        '''</summary>
        <VBFixedStringAttribute(16)> _
        Dim strDriver As String

        '''<summary>
        '''デバイス
        '''半角32文字
        '''</summary>
        <VBFixedStringAttribute(32)> _
        Dim strDevice As String

        '''<summary>
        '''IPアドレス
        '''</summary>
        Dim bytIP1 As Byte
        Dim bytIP2 As Byte
        Dim bytIP3 As Byte
        Dim bytIP4 As Byte

        ''''<summary>
        ''''予備
        ''''半角13文字
        ''''</summary>
        '<VBFixedArray(5)> _
        'Dim shtSpare() As Short

        ''''<summary>
        ''''配列数初期化
        ''''</summary>
        'Public Sub InitArray()
        '    ReDim shtSpare(5)
        'End Sub

    End Structure

#End Region

#End Region

#End Region

#Region "OPS設定"

#Region "画面タイトル構造体"

    ''画面タイトル構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsScreenTitle

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''OPSスクリーンタイトル
        <VBFixedArray(99)> Dim udtOpsScreenTitle() As gTypSetOpsScreenTitleRec

        '''配列数初期化
        Public Sub InitArray()
            ReDim udtOpsScreenTitle(99)
        End Sub

    End Structure

    ''画面タイトル構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsScreenTitleRec

        Dim bytScreenNo As Byte                                     ''番号
        Dim bytSpare As Byte                                        ''予備
        <VBFixedStringAttribute(30)> Dim strScreenName As String    ''名称

    End Structure

#End Region

#Region "プルダウンメニュー構造体"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsPulldownMenu

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''ポイント数
        <VBFixedArray(11)> Dim udtDetail() As gTypSetOpsPulldownMenuRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtDetail(11)
        End Sub

    End Structure

    ''メインメニュー構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsPulldownMenuRec

        <VBFixedStringAttribute(12)> Dim strName As String                  ''メインメニュー名称
        Dim tx As Short                                                   ''メインメニューの動作開始地点（左上X座標）
        Dim ty As Short                                                  ''メインメニューの動作開始地点（左上Y座標)
        Dim bx As Short                                                   ''メインメニューの動作開始地点（右下X座標）
        Dim by As Short                                                   ''メインメニューの動作開始地点（右下Y座標）
        Dim OPSSTFLG1 As Byte                                               ''OPS禁止フラグ
        Dim OPSSTFLG2 As Byte                                               ''OPS禁止フラグ2
        Dim bytMenuNo1 As Byte                                              ''メニュー番号（エディター用）
        Dim Spare1 As Byte                                                  ''予備1
        Dim Spare2 As Byte                                                  ''予備2
        Dim Spare3 As Byte                                                  ''予備3
        Dim Spare4 As Byte                                                  ''予備4
        Dim Spare5 As Byte                                                  ''予備5
        Dim bytMenuType As Byte                                             ''メニュータイプ
        Dim Yobi1 As Byte                                                   ''セレクトされているグループメニュー番号(未使用)
        Dim Yobi2 As Byte                                                   ''セレクトされているグループメニュー番号(保持型)(未使用)
        Dim bytMenuSet As Byte                                              ''グループメニューセット数
        Dim groupviewx As Short                                           ''グループメニューの表示位置X
        Dim groupviewy As Short                                           ''グループメニューの表示位置Y
        Dim groupsizex As Short                                           ''グループメニューの横サイズ位置
        Dim groupsizey As Short                                           ''グループメニューの縦サイズ位置
        <VBFixedArray(11)> Dim udtGroup() As gTypSetOpsPulldownMenuGroup    ''サブグループ

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtGroup(11)
        End Sub

    End Structure

    ''サブグループ構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsPulldownMenuGroup

        <VBFixedStringAttribute(24)> Dim strName As String              ''サブグループ名称
        Dim grouptx As Short                                            ''グループメニューの動作開始地点（左上X座標）
        Dim groupty As Short                                            ''グループメニューの動作開始地点（左上Y座標)
        Dim groupbx As Short                                            ''グループメニューの動作開始地点（右下X座標）
        Dim groupby As Short                                            ''グループメニューの動作開始地点（右下Y座標）
        Dim groupSpare1 As Byte                                         ''予備1
        Dim groupSpare2 As Byte                                         ''予備2
        Dim groupSpare3 As Byte                                         ''予備3
        Dim groupSpare4 As Byte                                         ''予備4
        Dim groupbytMenuType As Byte                                    ''メニュータイプ(処理項目・未使用))
        Dim SubgroupYobi1 As Byte                                       ''セレクトされているサブメニュー番号(未使用)
        Dim SubgroupYobi2 As Byte                                       ''セレクトされているサブメニュー番号(保持型)(未使用)
        Dim bytCount As Byte                                            ''サブメニュー設定数
        Dim Subviewx As Short                                           ''サブメニューの表示位置X
        Dim Subviewy As Short                                           ''サブメニューの表示位置Y
        Dim Subsizex As Short                                           ''サブメニューの横サイズ位置
        Dim Subsizey As Short                                           ''サブメニューの縦サイズ位置
        <VBFixedArray(16)> Dim udtSub() As gTypSetOpsPulldownMenuSub    ''サブメニュー

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtSub(16)
        End Sub

    End Structure

    ''サブメニュー構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsPulldownMenuSub

        <VBFixedStringAttribute(32)> Dim strName As String      ''サブメニュー名称
        Dim SubbytMenuType1 As Byte                             ''メニュータイプ(Bラベル処理項目1)
        Dim SubbytMenuType2 As Byte                             ''メニュータイプ(Bラベル処理項目2)
        Dim SubbytMenuType3 As Byte                             ''メニュータイプ(Bラベル処理項目3)
        Dim SubbytMenuType4 As Byte                             ''メニュータイプ(Bラベル処理項目4)
        Dim SubYobi1 As Byte                                    ''画面モード（未使用）
        Dim SubYobi2 As Byte                                    ''現在操作可能な画面の表示位置（未使用）
        Dim bytKeyCode As Byte                                  ''キーコード（未使用）
        Dim SubYobi4 As Byte                                    ''予備
        Dim ViewNo1 As Short                                    ''画面番号0
        Dim ViewNo2 As Short                                    ''画面番号1（未使用）
        Dim ViewNo3 As Short                                    ''画面番号2（未使用）
        Dim ViewNo4 As Short                                    ''画面番号3（未使用）
        Dim SubMenutx As Short                                  ''サブメニューの動作開始地点（左上X座標）
        Dim SubMenuty As Short                                  ''サブメニューの動作開始地点（左上Y座標)
        Dim SubMenubx As Short                                  ''サブメニューの動作開始地点（右下X座標）
        Dim SubMenuby As Short                                  ''サブメニューの動作開始地点（右下Y座標）

    End Structure

#End Region

#Region "動作表示位置"

    Public Const Main_Menu_Left As Integer = 8       'ボタン間幅
    Public Const Main_Menu_DX As Integer = 84        'メニューボタン幅
    Public Const Main_Menu_DY As Integer = 40        'メニューボタン高さ
    Public Const Group_Menu_DX As Integer = 150      'グループメニュー幅
    Public Const Group_Menu_DY As Integer = 40       'グループメニュー高さ
    Public Const Sub_Menu_DX As Integer = 300        'サブメニュー幅
    Public Const Sub_Menu_DY As Integer = 40         'サブメニュー高さ

    Public Const TitleDY As Integer = 40            'メニュー上部（幅）

    Public Const Win7XPOS As Integer = 0            'Windows表示開始地点（X）
    Public Const Win7YPOS As Integer = 0            'Windows表示開始地点（Y）
    Public Const Win7CXPOS As Integer = 1024        'Windows表示幅（横）
    Public Const Win7CYPOS As Integer = 768         'Windows表示幅（縦）

    Public Const Main_Menu_Max As Integer = 12      'メインメニュー数（最大値）
    Public Const Group_Menu_Max As Integer = 12     'グループメニュー数（最大値）
    Public Const Sub_Menu_Max As Integer = 17       'サブメニュー数（最大値）

    Public Const Main_Menu_Byte As Integer = 12      'メインメニュー文字バイト数
    Public Const Group_Menu_Byte As Integer = 24     'グループメニュー文字バイト数
    Public Const Sub_Menu_Byte As Integer = 32       'サブメニュー文字バイト数

#End Region

#Region "セレクションメニュー"

    ''セレクションメニュー構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsSelectionMenu

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        <VBFixedArray(200)> Dim udtOpsSelectionOffSetRec() As gTypSetOpsSelectionMenuOffSetRec
        <VBFixedArray(79)> Dim udtOpsSelectionSetViewRec() As gTypSetOpsSelectionMenuSetViewRec
        <VBFixedArray(99)> Dim udtOpsSelectionMenuNameKeyRec() As gTypSetOpsSelectionMenuNameKeyRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtOpsSelectionOffSetRec(200)
            ReDim udtOpsSelectionSetViewRec(79)
            ReDim udtOpsSelectionMenuNameKeyRec(99)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsSelectionMenuOffSetRec

        Dim ViewNo As Short                           '画面番号(～201)

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsSelectionMenuSetViewRec

        <VBFixedStringAttribute(8)> Dim SelectName As String                          'セレクション名称
        <VBFixedArray(9)> Dim udtKey() As gTypSetOpsSelectionMenuKey       'キー

        ''配列数初期化
        Public Sub InitArray()

            ReDim udtKey(9)

        End Sub

    End Structure

    ''キー構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
   Public Structure gTypSetOpsSelectionMenuKey

        Dim BytNameType1 As Byte                                            '処理項目1(Bラベル処理項目1)
        Dim BytNameType2 As Byte                                            '処理項目2(Bラベル処理項目2)
        Dim BytNameType3 As Byte                                            '処理項目3(Bラベル処理項目3)
        Dim BytNameType4 As Byte                                            '処理項目4(Bラベル処理項目4)
        Dim BytSelectName As Short                                          '画面番号
        Dim NameCode As Byte                                                '名称コード(未使用)
        Dim Yobi1 As Byte                                                   '予備

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Public Structure gTypSetOpsSelectionMenuNameKeyRec

        <VBFixedStringAttribute(16)> Dim SelectMenuKeyName As String                'セレクションメニューキー名称

    End Structure

    ''セレクションメニュー構造体(エディター用)
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsSelectionMenuEdit

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        <VBFixedArray(200)> Dim udtOpsSelectionOffSetRecEdit() As gTypSetOpsSelectionMenuOffSetRecEdit
        <VBFixedArray(200)> Dim udtOpsSelectionSetViewRecEdit() As gTypSetOpsSelectionMenuSetViewRecEdit
        <VBFixedArray(99)> Dim udtOpsSelectionMenuNameKeyRecEdit() As gTypSetOpsSelectionMenuNameKeyRecEdit

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtOpsSelectionOffSetRecEdit(200)
            ReDim udtOpsSelectionSetViewRecEdit(200)
            ReDim udtOpsSelectionMenuNameKeyRecEdit(99)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsSelectionMenuOffSetRecEdit

        Dim EditViewNo As Short                           '画面番号(～201)

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsSelectionMenuSetViewRecEdit

        <VBFixedStringAttribute(8)> Dim EditSelectName As String                          'セレクション名称
        <VBFixedArray(9)> Dim EditudtKey() As gTypSetOpsSelectionMenuKeyEdit       'キー

        ''配列数初期化
        Public Sub InitArray()
            ReDim EditudtKey(9)
        End Sub

    End Structure

    ''キー構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsSelectionMenuKeyEdit

        Dim EditBytNameType1 As Byte                                            '処理項目1(Bラベル処理項目1)
        Dim EditBytNameType2 As Byte                                            '処理項目2(Bラベル処理項目2)
        Dim EditBytNameType3 As Byte                                            '処理項目3(Bラベル処理項目3)
        Dim EditBytNameType4 As Byte                                            '処理項目4(Bラベル処理項目4)
        Dim EditBytSelectName As Short                                          '画面番号
        Dim EditNameCode As Byte                                                '名称コード(未使用)
        Dim EditYobi1 As Byte                                                   '予備

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsSelectionMenuNameKeyRecEdit

        <VBFixedStringAttribute(16)> Dim EditSelectMenuKeyName As String                'セレクションメニューキー名称

    End Structure

#End Region

#Region "グラフ設定構造体"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGraph

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''３種用グラフタイトル設定
        <VBFixedArray(15)> Dim udtGraphTitleRec() As gTypSetOpsGraphTitle

        ''偏差グラフ（排気ガスグラフ）設定
        <VBFixedArray(15)> Dim udtGraphExhaustRec() As gTypSetOpsGraphExhaust

        ''バーグラフ設定
        <VBFixedArray(15)> Dim udtGraphBarRec() As gTypSetOpsGraphBar

        ''アナログメーター
        <VBFixedArray(15)> Dim udtGraphAnalogMeterRec() As gTypSetOpsGraphAnalogMeter

        ''フリーグラフ
        '<VBFixedArray(9)> Dim udtGraphFreeRec() As gTypSetOpsGraphFree     2013.07.22 グラフとフリーグラフを分離  K.Fujimoto

        ''アナログメーター設定
        Dim udtGraphAnalogMeterSettingRec As gTypSetOpsGraphAnalogMeterSetting

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtGraphTitleRec(15)
            ReDim udtGraphExhaustRec(15)
            ReDim udtGraphBarRec(15)
            ReDim udtGraphAnalogMeterRec(15)
            'ReDim udtGraphFreeRec(9)       2013.07.22 グラフとフリーグラフを分離  K.Fujimoto
        End Sub

    End Structure

#Region "タイトル"

    ''------------------------------------------
    '' グラフタイトル構造体
    ''------------------------------------------
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGraphTitle

        Dim bytNo As Byte                                       ''グラフ番号(1～16)
        Dim bytType As Byte                                     ''グラフタイプ(1:Exhaust、2:Bar、3:AnalogMeter)
        <VBFixedStringAttribute(2)> Dim strSpare As String      ''予備
        <VBFixedStringAttribute(32)> Dim strName As String      ''グラフ名称(半角32文字)

    End Structure

#End Region

#Region "排気ガス"

    ''------------------------------------------
    '' 排気ガスグラフ（偏差グラフ）構造体
    ''------------------------------------------
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGraphExhaust

        Dim bytNo As Byte                                                       ''グラフ番号(1～16)
        <VBFixedStringAttribute(3)> Dim strSpare As String                      ''予備
        <VBFixedStringAttribute(32)> Dim strTitle As String                     ''グラフタイトル(半角32文字)
        <VBFixedStringAttribute(4)> Dim strItemUp As String                     ''グラフデータ名称（上段）(半角4文字)
        <VBFixedStringAttribute(4)> Dim strItemDown As String                   ''グラフデータ名称（下段）(半角4文字)
#Region "        Dim shtAveCh As Short                                                   ''平均CH"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtAveCh() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtAveCh), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtAveCh = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtAveCh As Short

#End Region
        Dim bytDevMark As Byte                                                  ''偏差目盛の上下限値(0～255)
        '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph削除 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        Dim bytSpare As Byte                                                    ''予備
        '---------------------------------------------------------------------------------------------------
        'Dim byt20Graph As Byte                                                  ''グラフ20本区切り（0:OFF、1：ON）
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        Dim bytLine As Byte                                                     ''数値の分け方（0:設定なし、1:1Line、2:2LINE）
        Dim bytCyCnt As Byte                                                    ''シリンダの数(1～24)
        <VBFixedStringAttribute(2)> Dim strSpare2 As String                     ''予備
        <VBFixedArray(23)> Dim udtCylinder() As gTypSetOpsGraphExhaustCylinder  ''シリンダグラフ情報
        <VBFixedStringAttribute(16)> Dim strTcTitle As String                   ''T/Cグラフのタイトル(半角16文字)
        <VBFixedStringAttribute(4)> Dim strTcComm1 As String                    ''T/Cグラフのコメント1(半角4文字)
        <VBFixedStringAttribute(4)> Dim strTcComm2 As String                    ''T/Cグラフのコメント2(半角4文字)
        Dim bytTcCnt As Byte                                                    ''T/Cの数(1～8)
        <VBFixedStringAttribute(3)> Dim strSpare3 As String                     ''予備
        <VBFixedArray(7)> Dim udtTurboCharger() As gTypSetOpsGraphExhaustTurboCharger ''T/Cグラフ情報

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtCylinder(23)
            ReDim udtTurboCharger(7)
        End Sub

    End Structure

    ''排気ガスグラフのシリンダ情報
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGraphExhaustCylinder
#Region "        Dim shtChCylinder As Short                                          ''シリンダのCH番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChCylinder() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChCylinder), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChCylinder = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChCylinder As Short

#End Region
#Region "        Dim shtChDeviation As Short                                         ''偏差のCH番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChDeviation() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChDeviation), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChDeviation = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChDeviation As Short

#End Region
        <VBFixedStringAttribute(8)> Dim strTitle As String                  ''シリンダのCH番号に対する名称(半角5文字)
    End Structure

    ''排気ガスグラフのT/C情報
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGraphExhaustTurboCharger
#Region "        Dim shtChTurboCharger As Short                                      ''T/CのCH番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChTurboCharger() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChTurboCharger), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChTurboCharger = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChTurboCharger As Short

#End Region
        <VBFixedStringAttribute(5)> Dim strTitle As String                  ''T/CのCH番号に対する名称(半角5文字)
        Dim bytSplitLine As Byte                                            ''区切り線
    End Structure

#End Region

#Region "バー"

    ''------------------------------------------
    '' バーグラフ構造体
    ''------------------------------------------
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGraphBar

        Dim bytNo As Byte                                                   ''グラフ番号(1～16)
        <VBFixedStringAttribute(3)> Dim strSpare As String                  ''予備
        <VBFixedStringAttribute(32)> Dim strTitle As String                 ''グラフタイトル(半角32文字)
        <VBFixedStringAttribute(4)> Dim strItemUp As String                 ''グラフデータ名称（上段）(半角4文字)
        <VBFixedStringAttribute(4)> Dim strItemDown As String               ''グラフデータ名称（下段）(半角4文字)
        '▼▼▼ 20110330 ファイルデータ１７版対応 20Graph→表示切替に変更 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
        Dim bytDisplay As Byte                                              ''表示切替指定（0:計測点レンジ、1:百分率）
        '---------------------------------------------------------------------------------------------------
        'Dim byt20Graph As Byte                                              ''グラフ20本区切り（0:OFF、1：ON）
        '▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
        Dim bytLine As Byte                                                 ''数値の分け方（0:設定なし、1:1Line、2:2LINE）
        Dim bytDevision As Byte                                             ''分割数（1:４分割、2:６分割、3:３ｘ５分割）
        Dim bytCyCnt As Byte                                                ''シリンダの数(1～24)
        <VBFixedArray(23)> Dim udtCylinder() As gTypSetOpsGraphBarCylinder  ''シリンダグラフ情報

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtCylinder(23)
        End Sub

    End Structure

    ''バーグラフのシリンダ情報
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGraphBarCylinder
#Region "        Dim shtChCylinder As Short                                          ''シリンダのCH番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChCylinder() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChCylinder), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChCylinder = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChCylinder As Short

#End Region
        <VBFixedStringAttribute(6)> Dim strTitle As String                  ''シリンダのCH番号に対する名称(半角5文字)
    End Structure

#End Region

#Region "アナログメーター"

    ''------------------------------------------
    '' アナログメーター
    ''------------------------------------------
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGraphAnalogMeter

        Dim bytNo As Byte                                                       ''グラフ番号(1～16)
        Dim bytMeterType As Byte                                                ''表示タイプ
        <VBFixedStringAttribute(2)> Dim strSpare As String                      ''予備
        <VBFixedStringAttribute(32)> Dim strTitle As String                     ''グラフタイトル(半角32文字)
        <VBFixedArray(7)> Dim udtDetail() As gTypSetOpsGraphAnalogMeterDetail   ''アナログメーター詳細

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtDetail(7)
        End Sub

    End Structure

    ''アナログメーター詳細
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGraphAnalogMeterDetail
#Region "        Dim shtChNo As Short                    ''CH番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChNo() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChNo), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChNo = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChNo As Short

#End Region
        Dim bytScale As Byte                    ''目盛り分割数(3～7)
        Dim bytColor As Byte                    ''表示色(0～255)
    End Structure

#End Region

#Region "フリー"

    ' ''------------------------------------------
    ' '' フリーグラフ構造体
    ' ''------------------------------------------
    '<Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    'Public Structure gTypSetOpsGraphFree

    '    <VBFixedArray(15)> Dim udtFreeGraphTitle() As gTypSetOpsGraphFreeTitle

    '    ''配列数初期化
    '    Public Sub InitArray()
    '        ReDim udtFreeGraphTitle(15)
    '    End Sub

    'End Structure

    ' ''フリーグラフタイトル
    '<Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    'Public Structure gTypSetOpsGraphFreeTitle

    '    Dim bytOpsNo As Byte                                                ''OPS番号(1～10)
    '    Dim bytGraphNo As Byte                                              ''グラフ番号(1～16)
    '    <VBFixedStringAttribute(2)> Dim strSpare As String                  ''予備
    '    <VBFixedStringAttribute(32)> Dim strGraphTitle As String            ''グラフタイトル(半角32文字)
    '    <VBFixedArray(31)> Dim udtFreeDetail() As gTypSetOpsGraphFreeDetail ''フリーグラフ詳細

    '    ''配列数初期化
    '    Public Sub InitArray()
    '        ReDim udtFreeDetail(31)
    '    End Sub

    'End Structure

    ''フリーグラフ詳細情報
    '    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    '    Public Structure gTypSetOpsGraphFreeDetail
    '        Dim bytType As Byte                                                 ''グラフタイプ
    '        Dim bytTopPos As Byte                                               ''グラフ先頭位置
    '#Region "        Dim shtChNo As Short                                                ''CH番号"

    '        ''UInt16が構造体メンバとして使えないためプロパティとして定義
    '        Public Property shtChNo() As UInt16
    '            Get
    '                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChNo), 0)
    '            End Get
    '            Set(ByVal value As UInt16)
    '                _shtChNo = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
    '            End Set
    '        End Property

    '        ''内部的に使用されるメンバ
    '        ''外部からの値設定は行わない事
    '        Dim _shtChNo As Short

    '#End Region
    '        Dim bytIndicatorKind As Byte                                        ''表示種類
    '#Region "        Dim shtIndicatorPattern As Short                                    ''表示マスク"

    '        ''UInt16が構造体メンバとして使えないためプロパティとして定義
    '        Public Property shtIndicatorPattern() As UInt16
    '            Get
    '                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtIndicatorPattern), 0)
    '            End Get
    '            Set(ByVal value As UInt16)
    '                _shtIndicatorPattern = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
    '            End Set
    '        End Property

    '        ''内部的に使用されるメンバ
    '        ''外部からの値設定は行わない事
    '        Dim _shtIndicatorPattern As Short

    '#End Region
    '        Dim bytScale As Byte                                                ''目盛分割数
    '        Dim bytColor As Byte                                                ''表示色
    '        <VBFixedStringAttribute(3)> Dim strSpare As String                  ''予備
    '    End Structure

#End Region

#Region "アナログメーター設定"

    ''------------------------------------------
    '' アナログメーター設定
    ''------------------------------------------
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGraphAnalogMeterSetting

        Dim bytChNameDisplayPoint As Byte                       ''CH名称表示位置    (1:Left、2:Center、3:Right)
        Dim bytMarkNumericalValue As Byte                       ''目盛数値表示方法  (1:Normal、2:Short)
        Dim bytPointerFrame As Byte                             ''指針の縁取り      (0:No、1:Yes)
        Dim bytPointerColorChange As Byte                       ''指針の色変更      (0:No、1:Yes)
        'Dim bytSideColorSymbol As Byte                          ''シンボル表示有無  (0:No、1:Yes)  2011.06.09 17版削除
        <VBFixedStringAttribute(4)> Dim strSpare As String      ''予備
    End Structure

#End Region

#End Region

    '2013.07.22 グラフとフリーグラフを分離  K.Fujimoto
#Region "フリーグラフ"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsFreeGraph

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''フリーディスプレイ情報
        <VBFixedArray(159)> Dim udtFreeGraphRec() As gTypSetOpsFreeGraphRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtFreeGraphRec(159)
        End Sub

    End Structure

    ''フリーグラフタイトル
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsFreeGraphRec

        Dim bytOpsNo As Byte                                                ''OPS番号(1～10)
        Dim bytGraphNo As Byte                                              ''グラフ番号(1～16)
        <VBFixedStringAttribute(2)> Dim strSpare As String                  ''予備
        <VBFixedStringAttribute(32)> Dim strGraphTitle As String            ''グラフタイトル(半角32文字)
        <VBFixedArray(31)> Dim udtFreeDetail() As gTypSetOpsFreeGraphDetail ''フリーグラフ詳細

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtFreeDetail(31)
        End Sub

    End Structure

    ''フリーグラフ詳細情報
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsFreeGraphDetail
        Dim bytType As Byte                                                 ''グラフタイプ
        Dim bytTopPos As Byte                                               ''グラフ先頭位置
#Region "        Dim shtChNo As Short                                                ''CH番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChNo() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChNo), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChNo = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChNo As Short

#End Region
        Dim bytIndicatorKind As Byte                                        ''表示種類
#Region "        Dim shtIndicatorPattern As Short                                    ''表示マスク"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtIndicatorPattern() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtIndicatorPattern), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtIndicatorPattern = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtIndicatorPattern As Short

#End Region
        Dim bytScale As Byte                                                ''目盛分割数
        Dim bytColor As Byte                                                ''表示色
        <VBFixedStringAttribute(3)> Dim strSpare As String                  ''予備
    End Structure

#End Region

#Region "ログフォーマット構造体"

    ''ログフォーマット構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsLogFormat

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        <VBFixedArray(599)> <VBFixedStringAttribute(10)> Dim strCol1() As String    ''列１
        <VBFixedArray(599)> <VBFixedStringAttribute(10)> Dim strCol2() As String    ''列２

        ''配列数初期化
        Public Sub InitArray()
            ReDim strCol1(599)
            ReDim strCol2(599)
        End Sub

    End Structure

#End Region

#Region "ログフォーマットCHID格納用構造体"   '' ☆ 2012/10/26 K.Tanigawa

    '' ログフォーマットCHID格納用構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsLogIdData

        Dim udtheader As gTypSetHeader

        <VBFixedArray(5999)> Dim shtLogChTbl() As Short
        ''        <VBFixedArray(5999)> Dim shtLogChTbl() As gTypSetOpsLogIdDataDetail

        ''配列数初期化
        Public Sub InitArray()
            ReDim shtLogChTbl(5999)
        End Sub

    End Structure

    ''ログフォーマット詳細情報
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsLogIdDataDetail
#Region "        Dim shtLogIdData As UShort                        ''CH番号/Code番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtLogIdData() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtLogIdData), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtLogIdData = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtLogIdData As UShort

#End Region
    End Structure

#End Region


#End Region

#Region "FU設定"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetFu

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        '''<summary>
        '''FU設定データ
        '''</summary>
        <VBFixedArray(20)> Dim udtFu() As gTypSetFuRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtFu(20)
        End Sub

    End Structure

    ''FU設定データ
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetFuRec

        Dim shtUse As Short                                             ''ＦＵ 使用／未使用フラグ
        Dim shtCanBus As Short                                          ''CanBus

        ''スロット情報
        <VBFixedArray(7)> Dim udtSlotInfo() As gTypSetFuRecSlot

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtSlotInfo(7)
        End Sub

    End Structure

    ''スロット情報
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetFuRecSlot

        Dim shtType As Short                                            ''スロット種別
        Dim shtTerinf As Short                                          ''端子台設定　ver1.4.0 2011.07.29

    End Structure

#End Region

#Region "チャンネル情報データ（FU表示名称設定データ）"

    ''チャンネル情報データ（表示名設定データ）
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChDisp

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        '''<summary>
        '''表示名設定データ
        '''</summary>
        <VBFixedArray(20)> Dim udtChDisp() As gTypSetChDispRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtChDisp(20)
        End Sub

    End Structure

    '''表示名設定データ
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChDispRec

        <VBFixedStringAttribute(16)> Dim strFuName As String    ''FCU/FU名称
        <VBFixedStringAttribute(16)> Dim strFuType As String    ''FCU/FU種類
        <VBFixedStringAttribute(16)> Dim strNamePlate As String ''FCU/FU盤名
        <VBFixedStringAttribute(16)> Dim strRemarks As String   ''コメント

        ''スロット情報
        <VBFixedArray(7)> Dim udtSlotInfo() As gTypSetChDispRecSlot

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtSlotInfo(7)
        End Sub

    End Structure

    ''スロット情報
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChDispRecSlot

        '<VBFixedStringAttribute(24)> Dim strCableMark As String     ''CableMark
        '<VBFixedStringAttribute(24)> Dim strCableClass As String    ''CableClass
        '<VBFixedStringAttribute(24)> Dim strDestination As String   ''Destination

        ' ''端子台名称情報
        '<VBFixedArray(3)> Dim udtTerminalInfo() As gTypSetChDispRecSlotTerminal

        ''計測点情報
        <VBFixedArray(63)> Dim udtPinInfo() As gTypSetChDispRecSlotPin

        ''配列数初期化
        Public Sub InitArray()
            'ReDim udtTerminalInfo(3)
            ReDim udtPinInfo(63)
        End Sub

    End Structure

    ' ''端子台名称情報
    '<Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    'Public Structure gTypSetChDispRecSlotTerminal

    '    <VBFixedStringAttribute(12)> Dim strCableMark As String     ''CableMark
    '    <VBFixedStringAttribute(12)> Dim strCableClass As String    ''CableClass
    '    <VBFixedStringAttribute(12)> Dim strDestination As String   ''Destination
    '    Dim shtPinCnt As Short                                      ''計測点設定数
    '    Dim shtSpare As Short                                       ''予備

    'End Structure

    ''計測点情報
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChDispRecSlotPin

        <VBFixedStringAttribute(4)> Dim strCoreNoIn As String       ''CoreNoIn
        <VBFixedStringAttribute(4)> Dim strCoreNoCom As String      ''CoreNoCom

        <VBFixedStringAttribute(24)> Dim strWireMark As String      ''WireMark
        <VBFixedStringAttribute(24)> Dim strWireMarkClass As String ''WireMarkClass
        <VBFixedStringAttribute(24)> Dim strDest As String          ''Destination

        Dim shtTerminalNo As Short                                  ''端子台番号
#Region "        Dim shtChid As Short          　　　                        ''CH ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChid As Short

#End Region

    End Structure

#End Region

#Region "延長警報盤設定"

    ''延長警報設定構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetExtAlarm

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        '''<summary>
        '''延長警報盤共通設定
        '''</summary>
        Dim udtExtAlarmCommon As gTypSetExtCommon

        ''' <summary>
        ''' 各延長警報盤設定
        ''' </summary>
        ''' <remarks>各延長警報盤設定</remarks>
        <VBFixedArray(19)> Dim udtExtAlarm() As gTypSetExtRec

        '''配列数初期化
        Public Sub InitArray()
            ReDim udtExtAlarm(19)
        End Sub

    End Structure

#Region "延長警報盤共通設定"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
     Public Structure gTypSetExtCommon

        <VBFixedArray(19)> Dim shtUse() As Short                    ''延長警報盤使用有無
        Dim shtCombineSet As Short                                  ''コンバイン設定
        Dim shtDutyFunc As Short                                    ''Duty機能有無
        Dim shtDutyMethod As Short                                  ''川汽仕様(特殊仕様)設定
        Dim shtEffect As Short                                      ''Group Effect 機能
        Dim shtNv As Short                                          ''NVルール
        Dim shtPart1 As Short                                       ''Duty part選択
        Dim shtPart2 As Short                                       ''Duty part選択
        Dim shtEeengineerCall As Short                                ''Eeengineer Call機能 
        Dim shtPatrolCall As Short                                  ''Patrol Man Call機能有無
        Dim shtDeadAlarm As Short                                   ''Dead Man Alarm 使用有無
        Dim shtLamps As Short                                       ''アラームランプ数
        Dim shtBuzzer As Short                                      ''ブザーパターン
        Dim shtGrpOut As Short                                      ''グループ出力パターン
        Dim shtGrpEffct As Short                                    ''Group Effect 設定（12個）
        Dim shtGrpFire As Short                                     ''Fire Sound Group 設定（12個）
        Dim shtGrpAlarm As Short                                    ''ｸﾞﾙｰﾌﾟｱﾗｰﾑﾗﾝﾌﾟ出力　選択
        Dim shtFireBuzzer As Short                                  ''Fire ブザーパターン
        Dim shtRsv As Short                                         ''予備    '' Ver1.11.5 2016.09.06 F.T Output にて使用
        <VBFixedArray(11)> Dim intGroupType() As Integer            ''EXTグループアラーム出力設定
        Dim shtSpecialWh As Short                                   ''特殊(川汽)仕様 (W/H)
        Dim shtSpecialPr As Short                                   ''特殊(川汽)仕様 (P/R)
        Dim shtSpecialCe As Short                                   ''特殊(川汽)仕様 (C/E)
        Dim shtEngCall As Short                                     ''Eeengineer Call 設定
        <VBFixedArray(11)> Dim udtExtGroup() As gTypSetExtCommonLcdGroup        ''LCD EXT グループ表示設定
        <VBFixedArray(29)> Dim udtExtDuty() As gTypSetExtCommonLcdDuty          ''LCD EXT Duty表示名称設定
        '' Ver1.8.7 2015.12.10  ｵﾌﾟｼｮﾝ設定追加
        Dim Option1 As Short                                        '' 延長警報設定　特殊設定1
        Dim Option2 As Short                                        '' 延長警報設定　特殊設定2
        Dim Option3 As Short                                        '' 延長警報設定　特殊設定3
        <VBFixedArray(2)> Dim shtSpare() As Short                   ''予備        '' Ver1.8.7 2015.12.10  5 → 2


        ''配列数初期化
        Public Sub InitArray()
            ReDim shtUse(19)
            ReDim intGroupType(11)
            ReDim udtExtGroup(11)
            ReDim udtExtDuty(29)
            ReDim shtSpare(2)       '' Ver1.8.9 2015.12.12 5 → 2 に変更
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetExtCommonLcdGroup

        Dim shtGroup As Short                                       ''LCD 警報ｸﾞﾙｰﾌﾟ番号
        Dim shtMark As Short                                        ''LCD マーク番号
        <VBFixedStringAttribute(16)> Dim strGroupName As String     ''LCD ｸﾞﾙｰﾌﾟ名称

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetExtCommonLcdDuty

        <VBFixedStringAttribute(8)> Dim strDutyName As String       ''LCD Duty 名称

    End Structure

#End Region

#Region "延長警報盤個別設定"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetExtRec

        <VBFixedStringAttribute(16)> Dim strPlace As String         ''設置場所　
        Dim shtNo As Short                                          ''延長警報パネル通信ID番号
        Dim shtReAlarm As Short                                     ''Re Alarm設定有無
        Dim shtBuzzCut As Short                                     ''ブザーカット有無
        Dim shtFreeEng As Short                                     ''フリーエンジニア有無
        Dim shtOption As Short                                      ''オプション
        Dim shtPanel As Short                                       ''パネルタイプ
        Dim shtPart As Short                                        ''パート設定
        Dim shtEngNo As Short                                       ''Eeengineer Call No 設定
        Dim shtDuty As Short                                        ''Duty 番号
        Dim shtDutyBuzz As Short                                    ''Duty ブザーストップ　動作設定
        Dim shtWatchLed As Short                                    ''Watch LED 表示方法選択
        Dim shtLedOut As Short                                      ''LED表示方法選択
        <VBFixedArray(11)> Dim shtLedTimer() As Short               ''LED12個分の遅延タイマ値
        <VBFixedArray(7)> Dim shtPosition() As Short                ''表示位置(LCD設定)


        ''配列数初期化
        Public Sub InitArray()
            ReDim shtLedTimer(11)
            ReDim shtPosition(7)
        End Sub

    End Structure

#End Region

#End Region

#Region "チャンネル情報"

    ''チャンネル情報構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChInfo

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        '''<summary>
        '''チャンネル個別データ
        '''全3000個分設定
        '''</summary>
        <VBFixedArray(2999)> Dim udtChannel() As gTypSetChRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtChannel(2999)
        End Sub

    End Structure

    ''チャンネル個別データ
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChRec

        '''<summary>
        '''CH共通項目
        '''</summary>
        Dim udtChCommon As gTypSetChRecCommon

        '''<summary>
        '''CH個別項目
        '''</summary>
        <VBFixedArray(gCstByteCntChannelType - 1)> Dim udtChTypeData() As Byte

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtChTypeData(gCstByteCntChannelType - 1)
        End Sub

#Region "個別項目プロパティ"

        ''仮設定保存開始位置
        Private Const mCstDummyStartIndex As Integer = 353

#Region "テンプレート"

        ' ''
        'Public Property Analog() As Byte
        '    Get
        '        Return udtChTypeData()
        '    End Get
        '    Set(ByVal value As Byte)
        '        udtChTypeData() = value
        '    End Set
        'End Property

        ' ''
        'Public Property Analog() As Short
        '    Get
        '        Return gConnect2Byte(udtChTypeData(), udtChTypeData())
        '    End Get
        '    Set(ByVal value As Short)
        '        Call gSeparat2Byte(value, udtChTypeData(), udtChTypeData())
        '    End Set
        'End Property

        ' ''
        'Public Property Analog() As Uint16
        '    Get
        '        Return gConnect2Byte(udtChTypeData(), udtChTypeData())
        '    End Get
        '    Set(ByVal value As Uint16)
        '        Call gSeparat2Byte(value, udtChTypeData(), udtChTypeData())
        '    End Set
        'End Property

        ' ''
        'Public Property Analog() As Integer
        '    Get
        '        Return gConnect4Byte(udtChTypeData(), udtChTypeData(), udtChTypeData(), udtChTypeData())
        '    End Get
        '    Set(ByVal value As Integer)
        '        Call gSeparat4Byte(value, udtChTypeData(), udtChTypeData(), udtChTypeData(), udtChTypeData())
        '    End Set
        'End Property

#End Region

#Region "仮設定：共通項目"

        ''DummyCommonExtGroup
        Public Property DummyCommonExtGroup() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex) = gBitSet(udtChTypeData(mCstDummyStartIndex), 0, value)
            End Set
        End Property

        ''DummyCommonDelay
        Public Property DummyCommonDelay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex), 1)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex) = gBitSet(udtChTypeData(mCstDummyStartIndex), 1, value)
            End Set
        End Property

        ''DummyCommonGroupRepose1
        Public Property DummyCommonGroupRepose1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex) = gBitSet(udtChTypeData(mCstDummyStartIndex), 2, value)
            End Set
        End Property

        ''DummyCommonGroupRepose2
        Public Property DummyCommonGroupRepose2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex) = gBitSet(udtChTypeData(mCstDummyStartIndex), 3, value)
            End Set
        End Property

        ''DummyCommonFuAddress
        Public Property DummyCommonFuAddress() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex) = gBitSet(udtChTypeData(mCstDummyStartIndex), 4, value)
            End Set
        End Property

        ''DummyCommonPinNo
        Public Property DummyCommonPinNo() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex) = gBitSet(udtChTypeData(mCstDummyStartIndex), 5, value)
            End Set
        End Property

        ''DummyCommonUnitName
        Public Property DummyCommonUnitName() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex), 6)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex) = gBitSet(udtChTypeData(mCstDummyStartIndex), 6, value)
            End Set
        End Property

        ''DummyCommonStatusName
        Public Property DummyCommonStatusName() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex), 7)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex) = gBitSet(udtChTypeData(mCstDummyStartIndex), 7, value)
            End Set
        End Property

#End Region

#Region "仮設定：警報設定"

#Region " HiHi "

        ''DummyDelayHH
        Public Property DummyDelayHH() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 1), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 1) = gBitSet(udtChTypeData(mCstDummyStartIndex + 1), 0, value)
            End Set
        End Property

        ''DummyValueHH
        Public Property DummyValueHH() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 1), 1)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 1) = gBitSet(udtChTypeData(mCstDummyStartIndex + 1), 1, value)
            End Set
        End Property

        ''DummyExtGrHH
        Public Property DummyExtGrHH() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 1), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 1) = gBitSet(udtChTypeData(mCstDummyStartIndex + 1), 2, value)
            End Set
        End Property

        ''DummyGRep1HH
        Public Property DummyGRep1HH() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 1), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 1) = gBitSet(udtChTypeData(mCstDummyStartIndex + 1), 3, value)
            End Set
        End Property

        ''DummyGRep2HH
        Public Property DummyGRep2HH() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 1), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 1) = gBitSet(udtChTypeData(mCstDummyStartIndex + 1), 4, value)
            End Set
        End Property

        ''DummyStaNmHH
        Public Property DummyStaNmHH() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 1), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 1) = gBitSet(udtChTypeData(mCstDummyStartIndex + 1), 5, value)
            End Set
        End Property

#End Region
#Region " Hi "

        ''DummyDelayH
        Public Property DummyDelayH() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 2), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 2) = gBitSet(udtChTypeData(mCstDummyStartIndex + 2), 0, value)
            End Set
        End Property

        ''DummyValueH
        Public Property DummyValueH() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 2), 1)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 2) = gBitSet(udtChTypeData(mCstDummyStartIndex + 2), 1, value)
            End Set
        End Property

        ''DummyExtGrH
        Public Property DummyExtGrH() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 2), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 2) = gBitSet(udtChTypeData(mCstDummyStartIndex + 2), 2, value)
            End Set
        End Property

        ''DummyGRep1H
        Public Property DummyGRep1H() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 2), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 2) = gBitSet(udtChTypeData(mCstDummyStartIndex + 2), 3, value)
            End Set
        End Property

        ''DummyGRep2H
        Public Property DummyGRep2H() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 2), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 2) = gBitSet(udtChTypeData(mCstDummyStartIndex + 2), 4, value)
            End Set
        End Property

        ''DummyStaNmH
        Public Property DummyStaNmH() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 2), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 2) = gBitSet(udtChTypeData(mCstDummyStartIndex + 2), 5, value)
            End Set
        End Property

#End Region
#Region " Lo "

        ''DummyDelayL
        Public Property DummyDelayL() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 3), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 3) = gBitSet(udtChTypeData(mCstDummyStartIndex + 3), 0, value)
            End Set
        End Property

        ''DummyValueL
        Public Property DummyValueL() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 3), 1)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 3) = gBitSet(udtChTypeData(mCstDummyStartIndex + 3), 1, value)
            End Set
        End Property

        ''DummyExtGrL
        Public Property DummyExtGrL() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 3), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 3) = gBitSet(udtChTypeData(mCstDummyStartIndex + 3), 2, value)
            End Set
        End Property

        ''DummyGRep1L
        Public Property DummyGRep1L() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 3), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 3) = gBitSet(udtChTypeData(mCstDummyStartIndex + 3), 3, value)
            End Set
        End Property

        ''DummyGRep2L
        Public Property DummyGRep2L() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 3), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 3) = gBitSet(udtChTypeData(mCstDummyStartIndex + 3), 4, value)
            End Set
        End Property

        ''DummyStaNmL
        Public Property DummyStaNmL() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 3), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 3) = gBitSet(udtChTypeData(mCstDummyStartIndex + 3), 5, value)
            End Set
        End Property

#End Region
#Region " LoLo "

        ''DummyDelayLL
        Public Property DummyDelayLL() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 4), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 4) = gBitSet(udtChTypeData(mCstDummyStartIndex + 4), 0, value)
            End Set
        End Property

        ''DummyValueLL
        Public Property DummyValueLL() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 4), 1)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 4) = gBitSet(udtChTypeData(mCstDummyStartIndex + 4), 1, value)
            End Set
        End Property

        ''DummyExtGrLL
        Public Property DummyExtGrLL() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 4), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 4) = gBitSet(udtChTypeData(mCstDummyStartIndex + 4), 2, value)
            End Set
        End Property

        ''DummyGRep1LL
        Public Property DummyGRep1LL() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 4), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 4) = gBitSet(udtChTypeData(mCstDummyStartIndex + 4), 3, value)
            End Set
        End Property

        ''DummyGRep2LL
        Public Property DummyGRep2LL() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 4), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 4) = gBitSet(udtChTypeData(mCstDummyStartIndex + 4), 4, value)
            End Set
        End Property

        ''DummyStaNmLL
        Public Property DummyStaNmLL() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 4), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 4) = gBitSet(udtChTypeData(mCstDummyStartIndex + 4), 5, value)
            End Set
        End Property

#End Region
#Region " SF "

        ''DummyDelaySF
        Public Property DummyDelaySF() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 5), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 5) = gBitSet(udtChTypeData(mCstDummyStartIndex + 5), 0, value)
            End Set
        End Property

        ''DummyValueSF
        Public Property DummyValueSF() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 5), 1)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 5) = gBitSet(udtChTypeData(mCstDummyStartIndex + 5), 1, value)
            End Set
        End Property

        ''DummyExtGrSF
        Public Property DummyExtGrSF() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 5), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 5) = gBitSet(udtChTypeData(mCstDummyStartIndex + 5), 2, value)
            End Set
        End Property

        ''DummyGRep1SF
        Public Property DummyGRep1SF() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 5), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 5) = gBitSet(udtChTypeData(mCstDummyStartIndex + 5), 3, value)
            End Set
        End Property

        ''DummyGRep2SF
        Public Property DummyGRep2SF() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 5), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 5) = gBitSet(udtChTypeData(mCstDummyStartIndex + 5), 4, value)
            End Set
        End Property

        ''DummyStaNmSF
        Public Property DummyStaNmSF() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 5), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 5) = gBitSet(udtChTypeData(mCstDummyStartIndex + 5), 5, value)
            End Set
        End Property

#End Region

#End Region

#Region "仮設定：レンジ設定"

#Region " Range "

        ''DummyRangeScale
        Public Property DummyRangeScale() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 6), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 6) = gBitSet(udtChTypeData(mCstDummyStartIndex + 6), 0, value)
            End Set
        End Property

        ''DummyRangeNormalHi
        Public Property DummyRangeNormalHi() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 6), 1)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 6) = gBitSet(udtChTypeData(mCstDummyStartIndex + 6), 1, value)
            End Set
        End Property

        ''DummyRangeNormalLo
        Public Property DummyRangeNormalLo() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 6), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 6) = gBitSet(udtChTypeData(mCstDummyStartIndex + 6), 2, value)
            End Set
        End Property

#End Region

#End Region

#Region "仮設定：出力情報"

        ''DummyOutFuAddress
        Public Property DummyOutFuAddress() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 7), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 7) = gBitSet(udtChTypeData(mCstDummyStartIndex + 7), 0, value)
            End Set
        End Property

        ''DummyOutBitCount
        Public Property DummyOutBitCount() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 7), 1)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 7) = gBitSet(udtChTypeData(mCstDummyStartIndex + 7), 1, value)
            End Set
        End Property

        ''DummyOutStatusType
        Public Property DummyOutStatusType() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 7), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 7) = gBitSet(udtChTypeData(mCstDummyStartIndex + 7), 2, value)
            End Set
        End Property

#End Region

#Region "仮設定：出力ステータス情報"

        ''DummyOutStatus1
        Public Property DummyOutStatus1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 8), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 8) = gBitSet(udtChTypeData(mCstDummyStartIndex + 8), 0, value)
            End Set
        End Property

        ''DummyOutStatus2
        Public Property DummyOutStatus2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 8), 1)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 8) = gBitSet(udtChTypeData(mCstDummyStartIndex + 8), 1, value)
            End Set
        End Property

        ''DummyOutStatus3
        Public Property DummyOutStatus3() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 8), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 8) = gBitSet(udtChTypeData(mCstDummyStartIndex + 8), 2, value)
            End Set
        End Property

        ''DummyOutStatus4
        Public Property DummyOutStatus4() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 8), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 8) = gBitSet(udtChTypeData(mCstDummyStartIndex + 8), 3, value)
            End Set
        End Property

        ''DummyOutStatus5
        Public Property DummyOutStatus5() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 8), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 8) = gBitSet(udtChTypeData(mCstDummyStartIndex + 8), 4, value)
            End Set
        End Property

        ''DummyOutStatus6
        Public Property DummyOutStatus6() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 8), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 8) = gBitSet(udtChTypeData(mCstDummyStartIndex + 8), 5, value)
            End Set
        End Property

        ''DummyOutStatus7
        Public Property DummyOutStatus7() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 8), 6)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 8) = gBitSet(udtChTypeData(mCstDummyStartIndex + 8), 6, value)
            End Set
        End Property

        ''DummyOutStatus8
        Public Property DummyOutStatus8() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 8), 7)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 8) = gBitSet(udtChTypeData(mCstDummyStartIndex + 8), 7, value)
            End Set
        End Property

#End Region

#Region "仮設定：バルブ関連項目"

        ''DummySp1
        Public Property DummySp1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 9), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 9) = gBitSet(udtChTypeData(mCstDummyStartIndex + 9), 0, value)
            End Set
        End Property

        ''DummySp2
        Public Property DummySp2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 9), 1)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 9) = gBitSet(udtChTypeData(mCstDummyStartIndex + 9), 1, value)
            End Set
        End Property

        ''DummyHysOpen
        Public Property DummyHysOpen() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 9), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 9) = gBitSet(udtChTypeData(mCstDummyStartIndex + 9), 2, value)
            End Set
        End Property

        ''DummyHysClose
        Public Property DummyHysClose() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 9), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 9) = gBitSet(udtChTypeData(mCstDummyStartIndex + 9), 3, value)
            End Set
        End Property

        ''DummySmpTime
        Public Property DummySmpTime() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 9), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 9) = gBitSet(udtChTypeData(mCstDummyStartIndex + 9), 4, value)
            End Set
        End Property

        ''DummyVar
        Public Property DummyVar() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 9), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 9) = gBitSet(udtChTypeData(mCstDummyStartIndex + 9), 5, value)
            End Set
        End Property



#End Region

#Region "仮設定：フィードバックアラーム情報"

        ''DummyFaDelay
        Public Property DummyFaDelay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 10), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 10) = gBitSet(udtChTypeData(mCstDummyStartIndex + 10), 0, value)
            End Set
        End Property

        ''DummyFaExtGr
        Public Property DummyFaExtGr() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 10), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 10) = gBitSet(udtChTypeData(mCstDummyStartIndex + 10), 2, value)
            End Set
        End Property

        ''DummyFaGrep1
        Public Property DummyFaGrep1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 10), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 10) = gBitSet(udtChTypeData(mCstDummyStartIndex + 10), 3, value)
            End Set
        End Property

        ''DummyFaGrep2
        Public Property DummyFaGrep2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 10), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 10) = gBitSet(udtChTypeData(mCstDummyStartIndex + 10), 4, value)
            End Set
        End Property

        ''DummyFaStaNm
        Public Property DummyFaStaNm() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 10), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 10) = gBitSet(udtChTypeData(mCstDummyStartIndex + 10), 5, value)
            End Set
        End Property

        ''DummyFaTimeV
        Public Property DummyFaTimeV() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 10), 6)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 10) = gBitSet(udtChTypeData(mCstDummyStartIndex + 10), 6, value)
            End Set
        End Property

#End Region

#Region "仮設定：コンポジット関連項目"

#Region " Status1 "

        ''DummyCmpStatus1Delay
        Public Property DummyCmpStatus1Delay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 11), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 11) = gBitSet(udtChTypeData(mCstDummyStartIndex + 11), 0, value)
            End Set
        End Property

        ' ''DummyCmpStatus1Value
        'Public Property DummyCmpStatus1Value() As Boolean
        '    Get
        '        Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 11), 1)
        '    End Get
        '    Set(ByVal value As Boolean)
        '        udtChTypeData(mCstDummyStartIndex + 11) = gBitSet(udtChTypeData(mCstDummyStartIndex + 11), 1, value)
        '    End Set
        'End Property

        ''DummyCmpStatus1ExtGr
        Public Property DummyCmpStatus1ExtGr() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 11), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 11) = gBitSet(udtChTypeData(mCstDummyStartIndex + 11), 2, value)
            End Set
        End Property

        ''DummyCmpStatus1GRep1
        Public Property DummyCmpStatus1GRep1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 11), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 11) = gBitSet(udtChTypeData(mCstDummyStartIndex + 11), 3, value)
            End Set
        End Property

        ''DummyCmpStatus1GRep2
        Public Property DummyCmpStatus1GRep2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 11), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 11) = gBitSet(udtChTypeData(mCstDummyStartIndex + 11), 4, value)
            End Set
        End Property

        ''DummyCmpStatus1StaNm
        Public Property DummyCmpStatus1StaNm() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 11), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 11) = gBitSet(udtChTypeData(mCstDummyStartIndex + 11), 5, value)
            End Set
        End Property

#End Region
#Region " Status2 "

        ''DummyCmpStatus2Delay
        Public Property DummyCmpStatus2Delay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 12), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 12) = gBitSet(udtChTypeData(mCstDummyStartIndex + 12), 0, value)
            End Set
        End Property

        ' ''DummyCmpStatus2Value
        'Public Property DummyCmpStatus2Value() As Boolean
        '    Get
        '        Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 12), 1)
        '    End Get
        '    Set(ByVal value As Boolean)
        '        udtChTypeData(mCstDummyStartIndex + 12) = gBitSet(udtChTypeData(mCstDummyStartIndex + 12), 1, value)
        '    End Set
        'End Property

        ''DummyCmpStatus2ExtGr
        Public Property DummyCmpStatus2ExtGr() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 12), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 12) = gBitSet(udtChTypeData(mCstDummyStartIndex + 12), 2, value)
            End Set
        End Property

        ''DummyCmpStatus2GRep1
        Public Property DummyCmpStatus2GRep1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 12), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 12) = gBitSet(udtChTypeData(mCstDummyStartIndex + 12), 3, value)
            End Set
        End Property

        ''DummyCmpStatus2GRep2
        Public Property DummyCmpStatus2GRep2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 12), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 12) = gBitSet(udtChTypeData(mCstDummyStartIndex + 12), 4, value)
            End Set
        End Property

        ''DummyCmpStatus2StaNm
        Public Property DummyCmpStatus2StaNm() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 12), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 12) = gBitSet(udtChTypeData(mCstDummyStartIndex + 12), 5, value)
            End Set
        End Property

#End Region
#Region " Status3 "

        ''DummyCmpStatus3Delay
        Public Property DummyCmpStatus3Delay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 13), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 13) = gBitSet(udtChTypeData(mCstDummyStartIndex + 13), 0, value)
            End Set
        End Property

        ' ''DummyCmpStatus3Value
        'Public Property DummyCmpStatus3Value() As Boolean
        '    Get
        '        Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 13), 1)
        '    End Get
        '    Set(ByVal value As Boolean)
        '        udtChTypeData(mCstDummyStartIndex + 13) = gBitSet(udtChTypeData(mCstDummyStartIndex + 13), 1, value)
        '    End Set
        'End Property

        ''DummyCmpStatus3ExtGr
        Public Property DummyCmpStatus3ExtGr() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 13), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 13) = gBitSet(udtChTypeData(mCstDummyStartIndex + 13), 2, value)
            End Set
        End Property

        ''DummyCmpStatus3GRep1
        Public Property DummyCmpStatus3GRep1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 13), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 13) = gBitSet(udtChTypeData(mCstDummyStartIndex + 13), 3, value)
            End Set
        End Property

        ''DummyCmpStatus3GRep2
        Public Property DummyCmpStatus3GRep2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 13), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 13) = gBitSet(udtChTypeData(mCstDummyStartIndex + 13), 4, value)
            End Set
        End Property

        ''DummyCmpStatus3StaNm
        Public Property DummyCmpStatus3StaNm() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 13), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 13) = gBitSet(udtChTypeData(mCstDummyStartIndex + 13), 5, value)
            End Set
        End Property

#End Region
#Region " Status4 "

        ''DummyCmpStatus4Delay
        Public Property DummyCmpStatus4Delay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 14), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 14) = gBitSet(udtChTypeData(mCstDummyStartIndex + 14), 0, value)
            End Set
        End Property

        ' ''DummyCmpStatus4Value
        'Public Property DummyCmpStatus4Value() As Boolean
        '    Get
        '        Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 14), 1)
        '    End Get
        '    Set(ByVal value As Boolean)
        '        udtChTypeData(mCstDummyStartIndex + 14) = gBitSet(udtChTypeData(mCstDummyStartIndex + 14), 1, value)
        '    End Set
        'End Property

        ''DummyCmpStatus4ExtGr
        Public Property DummyCmpStatus4ExtGr() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 14), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 14) = gBitSet(udtChTypeData(mCstDummyStartIndex + 14), 2, value)
            End Set
        End Property

        ''DummyCmpStatus4GRep1
        Public Property DummyCmpStatus4GRep1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 14), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 14) = gBitSet(udtChTypeData(mCstDummyStartIndex + 14), 3, value)
            End Set
        End Property

        ''DummyCmpStatus4GRep2
        Public Property DummyCmpStatus4GRep2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 14), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 14) = gBitSet(udtChTypeData(mCstDummyStartIndex + 14), 4, value)
            End Set
        End Property

        ''DummyCmpStatus4StaNm
        Public Property DummyCmpStatus4StaNm() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 14), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 14) = gBitSet(udtChTypeData(mCstDummyStartIndex + 14), 5, value)
            End Set
        End Property

#End Region
#Region " Status5 "

        ''DummyCmpStatus5Delay
        Public Property DummyCmpStatus5Delay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 15), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 15) = gBitSet(udtChTypeData(mCstDummyStartIndex + 15), 0, value)
            End Set
        End Property

        ' ''DummyCmpStatus5Value
        'Public Property DummyCmpStatus5Value() As Boolean
        '    Get
        '        Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 15), 1)
        '    End Get
        '    Set(ByVal value As Boolean)
        '        udtChTypeData(mCstDummyStartIndex + 15) = gBitSet(udtChTypeData(mCstDummyStartIndex + 15), 1, value)
        '    End Set
        'End Property

        ''DummyCmpStatus5ExtGr
        Public Property DummyCmpStatus5ExtGr() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 15), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 15) = gBitSet(udtChTypeData(mCstDummyStartIndex + 15), 2, value)
            End Set
        End Property

        ''DummyCmpStatus5GRep1
        Public Property DummyCmpStatus5GRep1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 15), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 15) = gBitSet(udtChTypeData(mCstDummyStartIndex + 15), 3, value)
            End Set
        End Property

        ''DummyCmpStatus5GRep2
        Public Property DummyCmpStatus5GRep2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 15), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 15) = gBitSet(udtChTypeData(mCstDummyStartIndex + 15), 4, value)
            End Set
        End Property

        ''DummyCmpStatus5StaNm
        Public Property DummyCmpStatus5StaNm() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 15), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 15) = gBitSet(udtChTypeData(mCstDummyStartIndex + 15), 5, value)
            End Set
        End Property

#End Region
#Region " Status6 "

        ''DummyCmpStatus6Delay
        Public Property DummyCmpStatus6Delay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 16), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 16) = gBitSet(udtChTypeData(mCstDummyStartIndex + 16), 0, value)
            End Set
        End Property

        ' ''DummyCmpStatus6Value
        'Public Property DummyCmpStatus6Value() As Boolean
        '    Get
        '        Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 16), 1)
        '    End Get
        '    Set(ByVal value As Boolean)
        '        udtChTypeData(mCstDummyStartIndex + 16) = gBitSet(udtChTypeData(mCstDummyStartIndex + 16), 1, value)
        '    End Set
        'End Property

        ''DummyCmpStatus6ExtGr
        Public Property DummyCmpStatus6ExtGr() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 16), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 16) = gBitSet(udtChTypeData(mCstDummyStartIndex + 16), 2, value)
            End Set
        End Property

        ''DummyCmpStatus6GRep1
        Public Property DummyCmpStatus6GRep1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 16), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 16) = gBitSet(udtChTypeData(mCstDummyStartIndex + 16), 3, value)
            End Set
        End Property

        ''DummyCmpStatus6GRep2
        Public Property DummyCmpStatus6GRep2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 16), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 16) = gBitSet(udtChTypeData(mCstDummyStartIndex + 16), 4, value)
            End Set
        End Property

        ''DummyCmpStatus6StaNm
        Public Property DummyCmpStatus6StaNm() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 16), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 16) = gBitSet(udtChTypeData(mCstDummyStartIndex + 16), 5, value)
            End Set
        End Property

#End Region
#Region " Status7 "

        ''DummyCmpStatus7Delay
        Public Property DummyCmpStatus7Delay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 17), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 17) = gBitSet(udtChTypeData(mCstDummyStartIndex + 17), 0, value)
            End Set
        End Property

        ' ''DummyCmpStatus7Value
        'Public Property DummyCmpStatus7Value() As Boolean
        '    Get
        '        Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 17), 1)
        '    End Get
        '    Set(ByVal value As Boolean)
        '        udtChTypeData(mCstDummyStartIndex + 17) = gBitSet(udtChTypeData(mCstDummyStartIndex + 17), 1, value)
        '    End Set
        'End Property

        ''DummyCmpStatus7ExtGr
        Public Property DummyCmpStatus7ExtGr() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 17), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 17) = gBitSet(udtChTypeData(mCstDummyStartIndex + 17), 2, value)
            End Set
        End Property

        ''DummyCmpStatus7GRep1
        Public Property DummyCmpStatus7GRep1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 17), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 17) = gBitSet(udtChTypeData(mCstDummyStartIndex + 17), 3, value)
            End Set
        End Property

        ''DummyCmpStatus7GRep2
        Public Property DummyCmpStatus7GRep2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 17), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 17) = gBitSet(udtChTypeData(mCstDummyStartIndex + 17), 4, value)
            End Set
        End Property

        ''DummyCmpStatus7StaNm
        Public Property DummyCmpStatus7StaNm() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 17), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 17) = gBitSet(udtChTypeData(mCstDummyStartIndex + 17), 5, value)
            End Set
        End Property

#End Region
#Region " Status8 "

        ''DummyCmpStatus8Delay
        Public Property DummyCmpStatus8Delay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 18), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 18) = gBitSet(udtChTypeData(mCstDummyStartIndex + 18), 0, value)
            End Set
        End Property

        ' ''DummyCmpStatus8Value
        'Public Property DummyCmpStatus8Value() As Boolean
        '    Get
        '        Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 18), 1)
        '    End Get
        '    Set(ByVal value As Boolean)
        '        udtChTypeData(mCstDummyStartIndex + 18) = gBitSet(udtChTypeData(mCstDummyStartIndex + 18), 1, value)
        '    End Set
        'End Property

        ''DummyCmpStatus8ExtGr
        Public Property DummyCmpStatus8ExtGr() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 18), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 18) = gBitSet(udtChTypeData(mCstDummyStartIndex + 18), 2, value)
            End Set
        End Property

        ''DummyCmpStatus8GRep1
        Public Property DummyCmpStatus8GRep1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 18), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 18) = gBitSet(udtChTypeData(mCstDummyStartIndex + 18), 3, value)
            End Set
        End Property

        ''DummyCmpStatus8GRep2
        Public Property DummyCmpStatus8GRep2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 18), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 18) = gBitSet(udtChTypeData(mCstDummyStartIndex + 18), 4, value)
            End Set
        End Property

        ''DummyCmpStatus8StaNm
        Public Property DummyCmpStatus8StaNm() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 18), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 18) = gBitSet(udtChTypeData(mCstDummyStartIndex + 18), 5, value)
            End Set
        End Property

#End Region
#Region " Status9 "

        ''DummyCmpStatus9Delay
        Public Property DummyCmpStatus9Delay() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 19), 0)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 19) = gBitSet(udtChTypeData(mCstDummyStartIndex + 19), 0, value)
            End Set
        End Property

        ' ''DummyCmpStatus9Value
        'Public Property DummyCmpStatus9Value() As Boolean
        '    Get
        '        Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 19), 1)
        '    End Get
        '    Set(ByVal value As Boolean)
        '        udtChTypeData(mCstDummyStartIndex + 19) = gBitSet(udtChTypeData(mCstDummyStartIndex + 19), 1, value)
        '    End Set
        'End Property

        ''DummyCmpStatus9ExtGr
        Public Property DummyCmpStatus9ExtGr() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 19), 2)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 19) = gBitSet(udtChTypeData(mCstDummyStartIndex + 19), 2, value)
            End Set
        End Property

        ''DummyCmpStatus9GRep1
        Public Property DummyCmpStatus9GRep1() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 19), 3)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 19) = gBitSet(udtChTypeData(mCstDummyStartIndex + 19), 3, value)
            End Set
        End Property

        ''DummyCmpStatus9GRep2
        Public Property DummyCmpStatus9GRep2() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 19), 4)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 19) = gBitSet(udtChTypeData(mCstDummyStartIndex + 19), 4, value)
            End Set
        End Property

        ''DummyCmpStatus9StaNm
        Public Property DummyCmpStatus9StaNm() As Boolean
            Get
                Return gBitCheck(udtChTypeData(mCstDummyStartIndex + 19), 5)
            End Get
            Set(ByVal value As Boolean)
                udtChTypeData(mCstDummyStartIndex + 19) = gBitSet(udtChTypeData(mCstDummyStartIndex + 19), 5, value)
            End Set
        End Property

#End Region

#End Region

#Region "アナログ"

#Region "アラーム情報"

#Region " HiHi "

        Private Const mCstAnalogStartIndexHiHi As Integer = 0

        ''Use
        Public Property AnalogHiHiUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexHiHi), udtChTypeData(mCstAnalogStartIndexHiHi + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexHiHi), udtChTypeData(mCstAnalogStartIndexHiHi + 1))
            End Set
        End Property

        ''Delay
        Public Property AnalogHiHiDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexHiHi + 2), udtChTypeData(mCstAnalogStartIndexHiHi + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexHiHi + 2), udtChTypeData(mCstAnalogStartIndexHiHi + 3))
            End Set
        End Property

        ''Value
        Public Property AnalogHiHiValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstAnalogStartIndexHiHi + 4), _
                                     udtChTypeData(mCstAnalogStartIndexHiHi + 5), _
                                     udtChTypeData(mCstAnalogStartIndexHiHi + 6), _
                                     udtChTypeData(mCstAnalogStartIndexHiHi + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexHiHi + 4), _
                                   udtChTypeData(mCstAnalogStartIndexHiHi + 5), _
                                   udtChTypeData(mCstAnalogStartIndexHiHi + 6), _
                                   udtChTypeData(mCstAnalogStartIndexHiHi + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property AnalogHiHiExtGroup() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexHiHi + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexHiHi + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property AnalogHiHiGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexHiHi + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexHiHi + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property AnalogHiHiGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexHiHi + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexHiHi + 10) = value
            End Set
        End Property

        ''Spare
        Public Property AnalogHiHiSpare() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexHiHi + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexHiHi + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property AnalogHiHiStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstAnalogStartIndexHiHi + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstAnalogStartIndexHiHi + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property AnalogHiHiManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexHiHi + 20), udtChTypeData(mCstAnalogStartIndexHiHi + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexHiHi + 20), udtChTypeData(mCstAnalogStartIndexHiHi + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property AnalogHiHiManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexHiHi + 22), udtChTypeData(mCstAnalogStartIndexHiHi + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexHiHi + 22), udtChTypeData(mCstAnalogStartIndexHiHi + 23))
            End Set
        End Property

#End Region

#Region " Hi "

        Private Const mCstAnalogStartIndexHi As Integer = 24

        ''Use
        Public Property AnalogHiUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexHi), udtChTypeData(mCstAnalogStartIndexHi + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexHi), udtChTypeData(mCstAnalogStartIndexHi + 1))
            End Set
        End Property

        ''Delay
        Public Property AnalogHiDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexHi + 2), udtChTypeData(mCstAnalogStartIndexHi + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexHi + 2), udtChTypeData(mCstAnalogStartIndexHi + 3))
            End Set
        End Property

        ''Value
        Public Property AnalogHiValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstAnalogStartIndexHi + 4), _
                                     udtChTypeData(mCstAnalogStartIndexHi + 5), _
                                     udtChTypeData(mCstAnalogStartIndexHi + 6), _
                                     udtChTypeData(mCstAnalogStartIndexHi + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexHi + 4), _
                                   udtChTypeData(mCstAnalogStartIndexHi + 5), _
                                   udtChTypeData(mCstAnalogStartIndexHi + 6), _
                                   udtChTypeData(mCstAnalogStartIndexHi + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property AnalogHiExtGroup() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexHi + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexHi + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property AnalogHiGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexHi + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexHi + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property AnalogHiGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexHi + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexHi + 10) = value
            End Set
        End Property

        ''Spare
        Public Property AnalogHiSpare() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexHi + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexHi + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property AnalogHiStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstAnalogStartIndexHi + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstAnalogStartIndexHi + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property AnalogHiManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexHi + 20), udtChTypeData(mCstAnalogStartIndexHi + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexHi + 20), udtChTypeData(mCstAnalogStartIndexHi + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property AnalogHiManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexHi + 22), udtChTypeData(mCstAnalogStartIndexHi + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexHi + 22), udtChTypeData(mCstAnalogStartIndexHi + 23))
            End Set
        End Property

#End Region

#Region " Lo "

        Private Const mCstAnalogStartIndexLo As Integer = 48

        ''Use
        Public Property AnalogLoUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexLo), udtChTypeData(mCstAnalogStartIndexLo + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexLo), udtChTypeData(mCstAnalogStartIndexLo + 1))
            End Set
        End Property

        ''Delay
        Public Property AnalogLoDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexLo + 2), udtChTypeData(mCstAnalogStartIndexLo + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexLo + 2), udtChTypeData(mCstAnalogStartIndexLo + 3))
            End Set
        End Property

        ''Value
        Public Property AnalogLoValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstAnalogStartIndexLo + 4), _
                                     udtChTypeData(mCstAnalogStartIndexLo + 5), _
                                     udtChTypeData(mCstAnalogStartIndexLo + 6), _
                                     udtChTypeData(mCstAnalogStartIndexLo + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexLo + 4), _
                                   udtChTypeData(mCstAnalogStartIndexLo + 5), _
                                   udtChTypeData(mCstAnalogStartIndexLo + 6), _
                                   udtChTypeData(mCstAnalogStartIndexLo + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property AnalogLoExtGroup() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexLo + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexLo + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property AnalogLoGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexLo + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexLo + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property AnalogLoGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexLo + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexLo + 10) = value
            End Set
        End Property

        ''Spare
        Public Property AnalogLoSpare() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexLo + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexLo + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property AnalogLoStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstAnalogStartIndexLo + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstAnalogStartIndexLo + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property AnalogLoManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexLo + 20), udtChTypeData(mCstAnalogStartIndexLo + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexLo + 20), udtChTypeData(mCstAnalogStartIndexLo + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property AnalogLoManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexLo + 22), udtChTypeData(mCstAnalogStartIndexLo + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexLo + 22), udtChTypeData(mCstAnalogStartIndexLo + 23))
            End Set
        End Property

#End Region

#Region " LoLo "

        Private Const mCstAnalogStartIndexLoLo As Integer = 72

        ''Use
        Public Property AnalogLoLoUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexLoLo), udtChTypeData(mCstAnalogStartIndexLoLo + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexLoLo), udtChTypeData(mCstAnalogStartIndexLoLo + 1))
            End Set
        End Property

        ''Delay
        Public Property AnalogLoLoDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexLoLo + 2), udtChTypeData(mCstAnalogStartIndexLoLo + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexLoLo + 2), udtChTypeData(mCstAnalogStartIndexLoLo + 3))
            End Set
        End Property

        ''Value
        Public Property AnalogLoLoValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstAnalogStartIndexLoLo + 4), _
                                     udtChTypeData(mCstAnalogStartIndexLoLo + 5), _
                                     udtChTypeData(mCstAnalogStartIndexLoLo + 6), _
                                     udtChTypeData(mCstAnalogStartIndexLoLo + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexLoLo + 4), _
                                   udtChTypeData(mCstAnalogStartIndexLoLo + 5), _
                                   udtChTypeData(mCstAnalogStartIndexLoLo + 6), _
                                   udtChTypeData(mCstAnalogStartIndexLoLo + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property AnalogLoLoExtGroup() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexLoLo + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexLoLo + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property AnalogLoLoGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexLoLo + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexLoLo + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property AnalogLoLoGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexLoLo + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexLoLo + 10) = value
            End Set
        End Property

        ''Spare
        Public Property AnalogLoLoSpare() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexLoLo + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexLoLo + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property AnalogLoLoStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstAnalogStartIndexLoLo + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstAnalogStartIndexLoLo + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property AnalogLoLoManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexLoLo + 20), udtChTypeData(mCstAnalogStartIndexLoLo + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexLoLo + 20), udtChTypeData(mCstAnalogStartIndexLoLo + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property AnalogLoLoManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexLoLo + 22), udtChTypeData(mCstAnalogStartIndexLoLo + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexLoLo + 22), udtChTypeData(mCstAnalogStartIndexLoLo + 23))
            End Set
        End Property

#End Region

#Region " SensorFail "

        Private Const mCstAnalogStartIndexSensorFail As Integer = 96

        ''Use
        Public Property AnalogSensorFailUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexSensorFail), udtChTypeData(mCstAnalogStartIndexSensorFail + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexSensorFail), udtChTypeData(mCstAnalogStartIndexSensorFail + 1))
            End Set
        End Property

        ''Delay
        Public Property AnalogSensorFailDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexSensorFail + 2), udtChTypeData(mCstAnalogStartIndexSensorFail + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexSensorFail + 2), udtChTypeData(mCstAnalogStartIndexSensorFail + 3))
            End Set
        End Property

        ''Value
        Public Property AnalogSensorFailValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstAnalogStartIndexSensorFail + 4), _
                                     udtChTypeData(mCstAnalogStartIndexSensorFail + 5), _
                                     udtChTypeData(mCstAnalogStartIndexSensorFail + 6), _
                                     udtChTypeData(mCstAnalogStartIndexSensorFail + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexSensorFail + 4), _
                                   udtChTypeData(mCstAnalogStartIndexSensorFail + 5), _
                                   udtChTypeData(mCstAnalogStartIndexSensorFail + 6), _
                                   udtChTypeData(mCstAnalogStartIndexSensorFail + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property AnalogSensorFailExtGroup() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexSensorFail + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexSensorFail + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property AnalogSensorFailGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexSensorFail + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexSensorFail + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property AnalogSensorFailGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexSensorFail + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexSensorFail + 10) = value
            End Set
        End Property

        ''Spare
        Public Property AnalogSensorFailSpare() As Byte
            Get
                Return udtChTypeData(mCstAnalogStartIndexSensorFail + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstAnalogStartIndexSensorFail + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property AnalogSensorFailStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstAnalogStartIndexSensorFail + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstAnalogStartIndexSensorFail + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property AnalogSensorFailManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexSensorFail + 20), udtChTypeData(mCstAnalogStartIndexSensorFail + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexSensorFail + 20), udtChTypeData(mCstAnalogStartIndexSensorFail + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property AnalogSensorFailManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexSensorFail + 22), udtChTypeData(mCstAnalogStartIndexSensorFail + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstAnalogStartIndexSensorFail + 22), udtChTypeData(mCstAnalogStartIndexSensorFail + 23))
            End Set
        End Property

#End Region

#End Region

#Region "アナログ項目"

        Private Const mCstAnalogStartIndexItem As Integer = 120

        ''RangeHigh
        Public Property AnalogRangeHigh() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstAnalogStartIndexItem + 0), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 1), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 2), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 3))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 0), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 1), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 2), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 3))
            End Set
        End Property

        ''RangeLow
        Public Property AnalogRangeLow() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstAnalogStartIndexItem + 4), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 5), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 6), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 4), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 5), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 6), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 7))
            End Set
        End Property

        ''NormalHigh
        Public Property AnalogNormalHigh() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstAnalogStartIndexItem + 8), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 9), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 10), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 11))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 8), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 9), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 10), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 11))
            End Set
        End Property

        ''NormalLow
        Public Property AnalogNormalLow() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstAnalogStartIndexItem + 12), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 13), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 14), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 15))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 12), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 13), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 14), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 15))
            End Set
        End Property

        ''OffsetValue
        Public Property AnalogOffsetValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstAnalogStartIndexItem + 16), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 17), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 18), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 19))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 16), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 17), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 18), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 19))
            End Set
        End Property

        ''String
        Public Property AnalogString() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexItem + 20), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 21))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 20), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 21))
            End Set
        End Property

        ''DecimalPosition
        Public Property AnalogDecimalPosition() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexItem + 22), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 23))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 22), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 23))
            End Set
        End Property

        ''Display3
        Public Property AnalogDisplay3() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexItem + 24), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 25))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 24), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 25))
            End Set
        End Property

        ''RangeType
        Public Property AnalogRangeType() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexItem + 26), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 27))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 26), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 27))
            End Set
        End Property

        ''TagNo
        '' 2015.10.22 Verf1.7.5 追加
        Public Property AnalogTagNo() As String

            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstAnalogStartIndexItem + 28 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstAnalogStartIndexItem + 28 + j) = bytArray(j)
                Next j

            End Set
        End Property


        ''LRNo
        '' 2015.11.12 Verf1.7.8 追加
        Public Property AnalogLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexItem + 44), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 45))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 44), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 45))
            End Set
        End Property

        ''fire_Alm_mimic
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Public Property AnalogAlmMimic() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexItem + 46), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 47))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 46), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 47))
            End Set
        End Property

        'adjst_psw
        'Ver2.0.7.C ｵﾌｾｯﾄ調整ﾊﾟｽﾜｰﾄﾞ有無
        Public Property AnalogAdjstPsw() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexItem + 48), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 49))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 48), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 49))
            End Set
        End Property

        'Ver2.0.8.5
        'mmhg_flg mmHgレンジ下限小数点変更フラグ
        Public Property AnalogMmHgFlg() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexItem + 50), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 51))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 50), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 51))
            End Set
        End Property
        'mmhg_dec mmHgレンジ下限小数点桁数
        Public Property AnalogMmHgDec() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstAnalogStartIndexItem + 52), _
                                     udtChTypeData(mCstAnalogStartIndexItem + 53))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstAnalogStartIndexItem + 52), _
                                   udtChTypeData(mCstAnalogStartIndexItem + 53))
            End Set
        End Property
        '-
#End Region

#End Region

#Region "デジタル"

#Region "デジタル項目"

        ''Use
        Public Property DigitalUse() As Integer
            Get
                Return gConnect2Byte(udtChTypeData(0), udtChTypeData(1))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat2Byte(value, udtChTypeData(0), udtChTypeData(1))
            End Set
        End Property

        ''DiFilter
        Public Property DigitalDiFilter() As Integer
            Get
                Return gConnect2Byte(udtChTypeData(2), udtChTypeData(3))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat2Byte(value, udtChTypeData(2), udtChTypeData(3))
            End Set
        End Property


        ''TagNo
        '' 2015.10.22 Verf1.7.5 追加
        Public Property DigitalTagNo() As String

            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(4 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(4 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''LRNo
        '' 2015.11.12 Verf1.7.8 追加
        Public Property DigitalLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(20), _
                                     udtChTypeData(21))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(20), _
                                   udtChTypeData(21))
            End Set
        End Property

        ''fire_Alm_mimic
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Public Property DigitalAlmMimic() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(22), _
                                     udtChTypeData(23))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(22), _
                                   udtChTypeData(23))
            End Set
        End Property
#End Region

#End Region

#Region "モーター"

#Region "モーター項目"

        ''Use
        Public Property MotorUse() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(0), udtChTypeData(1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(0), udtChTypeData(1))
            End Set
        End Property

        ''DiFilter
        Public Property MotorDiFilter() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(2), udtChTypeData(3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(2), udtChTypeData(3))
            End Set
        End Property

        ''Feedback
        Public Property MotorFeedback() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(4), udtChTypeData(5))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(4), udtChTypeData(5))
            End Set
        End Property

        ''FuNo
        Public Property MotorFuNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(6), udtChTypeData(7))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(6), udtChTypeData(7))
            End Set
        End Property

        ''PortNo
        Public Property MotorPortNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(8), udtChTypeData(9))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(8), udtChTypeData(9))
            End Set
        End Property

        ''Pin
        Public Property MotorPin() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(10), udtChTypeData(11))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(10), udtChTypeData(11))
            End Set
        End Property

        ''PinNo
        Public Property MotorPinNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(12), udtChTypeData(13))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(12), udtChTypeData(13))
            End Set
        End Property

        ''Control
        Public Property MotorControl() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(14), udtChTypeData(15))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(14), udtChTypeData(16))
            End Set
        End Property

        ''Width
        Public Property MotorWidth() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(16), udtChTypeData(17))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(16), udtChTypeData(17))
            End Set
        End Property

        ''Status
        Public Property MotorStatus() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(18), udtChTypeData(19))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(18), udtChTypeData(19))
            End Set
        End Property

#End Region

#Region "出力ステータス"

        Private Const mCstMotorStartIndexOutStatus As Integer = 20

        ''OutStatus1
        Public Property MotorOutStatus1() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstMotorStartIndexOutStatus + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstMotorStartIndexOutStatus + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus2
        Public Property MotorOutStatus2() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstMotorStartIndexOutStatus + 8 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstMotorStartIndexOutStatus + 8 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus3
        Public Property MotorOutStatus3() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstMotorStartIndexOutStatus + 16 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstMotorStartIndexOutStatus + 16 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus4
        Public Property MotorOutStatus4() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstMotorStartIndexOutStatus + 24 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstMotorStartIndexOutStatus + 24 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus5
        Public Property MotorOutStatus5() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstMotorStartIndexOutStatus + 32 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstMotorStartIndexOutStatus + 32 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus6
        Public Property MotorOutStatus6() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstMotorStartIndexOutStatus + 40 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstMotorStartIndexOutStatus + 40 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus7
        Public Property MotorOutStatus7() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstMotorStartIndexOutStatus + 48 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstMotorStartIndexOutStatus + 48 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus8
        Public Property MotorOutStatus8() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstMotorStartIndexOutStatus + 56 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstMotorStartIndexOutStatus + 56 + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region

#Region "フィードバックアラーム情報"

        Private Const mCstMotorAlarmStartIndex As Integer = 84

        ''Use
        Public Property MotorAlarmUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex), udtChTypeData(mCstMotorAlarmStartIndex + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstMotorAlarmStartIndex), udtChTypeData(mCstMotorAlarmStartIndex + 1))
            End Set
        End Property

        ''Delay
        Public Property MotorAlarmDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 2), udtChTypeData(mCstMotorAlarmStartIndex + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstMotorAlarmStartIndex + 2), udtChTypeData(mCstMotorAlarmStartIndex + 3))
            End Set
        End Property

        ''Sp1
        Public Property MotorAlarmSp1() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 4), udtChTypeData(mCstMotorAlarmStartIndex + 5))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstMotorAlarmStartIndex + 4), udtChTypeData(mCstMotorAlarmStartIndex + 5))
            End Set
        End Property

        ''Sp2
        Public Property MotorAlarmSp2() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 6), udtChTypeData(mCstMotorAlarmStartIndex + 7))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstMotorAlarmStartIndex + 6), udtChTypeData(mCstMotorAlarmStartIndex + 7))
            End Set
        End Property

        ''Hys1
        Public Property MotorAlarmHys1() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 8), udtChTypeData(mCstMotorAlarmStartIndex + 9))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstMotorAlarmStartIndex + 8), udtChTypeData(mCstMotorAlarmStartIndex + 9))
            End Set
        End Property

        ''Hys2
        Public Property MotorAlarmHys2() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 10), udtChTypeData(mCstMotorAlarmStartIndex + 11))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstMotorAlarmStartIndex + 10), udtChTypeData(mCstMotorAlarmStartIndex + 11))
            End Set
        End Property

        ''St
        Public Property MotorAlarmSt() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 12), udtChTypeData(mCstMotorAlarmStartIndex + 13))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstMotorAlarmStartIndex + 12), udtChTypeData(mCstMotorAlarmStartIndex + 13))
            End Set
        End Property

        ''Spare1
        Public Property MotorAlarmSpare1() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 14), udtChTypeData(mCstMotorAlarmStartIndex + 15))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstMotorAlarmStartIndex + 14), udtChTypeData(mCstMotorAlarmStartIndex + 15))
            End Set
        End Property

        ''Var
        Public Property MotorAlarmVar() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstMotorAlarmStartIndex + 16), _
                                     udtChTypeData(mCstMotorAlarmStartIndex + 17), _
                                     udtChTypeData(mCstMotorAlarmStartIndex + 18), _
                                     udtChTypeData(mCstMotorAlarmStartIndex + 19))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, udtChTypeData(mCstMotorAlarmStartIndex + 16), _
                                          udtChTypeData(mCstMotorAlarmStartIndex + 17), _
                                          udtChTypeData(mCstMotorAlarmStartIndex + 18), _
                                          udtChTypeData(mCstMotorAlarmStartIndex + 19))
            End Set
        End Property

        ''ExtGroup
        Public Property MotorAlarmExtGroup() As Byte
            Get
                Return udtChTypeData(mCstMotorAlarmStartIndex + 20)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstMotorAlarmStartIndex + 20) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property MotorAlarmGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstMotorAlarmStartIndex + 21)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstMotorAlarmStartIndex + 21) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property MotorAlarmGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstMotorAlarmStartIndex + 22)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstMotorAlarmStartIndex + 22) = value
            End Set
        End Property

        ''Spare
        Public Property MotorAlarmSpare2() As Byte
            Get
                Return udtChTypeData(mCstMotorAlarmStartIndex + 23)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstMotorAlarmStartIndex + 23) = value
            End Set
        End Property

        ''StatusInput
        Public Property MotorAlarmStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstMotorAlarmStartIndex + 24) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstMotorAlarmStartIndex + 24) + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualReposeState
        Public Property MotorAlarmManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 32), udtChTypeData(mCstMotorAlarmStartIndex + 33))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstMotorAlarmStartIndex + 32), udtChTypeData(mCstMotorAlarmStartIndex + 33))
            End Set
        End Property

        ''ManualReposeState
        Public Property MotorAlarmManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 34), udtChTypeData(mCstMotorAlarmStartIndex + 35))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstMotorAlarmStartIndex + 34), udtChTypeData(mCstMotorAlarmStartIndex + 35))
            End Set
        End Property


#End Region

#Region "その他"
        ''TagNo
        '' 2015.10.22 Verf1.7.5 追加
        Public Property MotorTagNo() As String

            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstMotorAlarmStartIndex + 36 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstMotorAlarmStartIndex + 36 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''LRNo
        '' 2015.11.12 Verf1.7.8 追加
        Public Property MotorLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 52), _
                                     udtChTypeData(mCstMotorAlarmStartIndex + 53))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstMotorAlarmStartIndex + 52), _
                                   udtChTypeData(mCstMotorAlarmStartIndex + 53))
            End Set
        End Property

        ''fire_Alm_mimic
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Public Property MotorAlmMimic() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstMotorAlarmStartIndex + 54), _
                                     udtChTypeData(mCstMotorAlarmStartIndex + 55))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstMotorAlarmStartIndex + 54), _
                                   udtChTypeData(mCstMotorAlarmStartIndex + 55))
            End Set
        End Property
#End Region

#End Region

#Region "バルブ"

#Region "バルブDI-DO"

#Region "バルブDiDo項目"

        ''Use
        Public Property ValveDiDoUse() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(0), udtChTypeData(1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(0), udtChTypeData(1))
            End Set
        End Property

        ''TableIndex
        Public Property ValveCompositeTableIndex() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(2), udtChTypeData(3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(2), udtChTypeData(3))
            End Set
        End Property

        ''Feedback
        Public Property ValveDiDoFeedback() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(4), udtChTypeData(5))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(4), udtChTypeData(5))
            End Set
        End Property

        ''FuNo
        Public Property ValveDiDoFuNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(6), udtChTypeData(7))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(6), udtChTypeData(7))
            End Set
        End Property

        ''PortNo
        Public Property ValveDiDoPortNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(8), udtChTypeData(9))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(8), udtChTypeData(9))
            End Set
        End Property

        ''Pin
        Public Property ValveDiDoPin() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(10), udtChTypeData(11))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(10), udtChTypeData(11))
            End Set
        End Property

        ''PinNo
        Public Property ValveDiDoPinNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(12), udtChTypeData(13))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(12), udtChTypeData(13))
            End Set
        End Property

        ''Control
        Public Property ValveDiDoControl() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(14), udtChTypeData(15))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(14), udtChTypeData(15))
            End Set
        End Property

        ''Width
        Public Property ValveDiDoWidth() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(16), udtChTypeData(17))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(16), udtChTypeData(17))
            End Set
        End Property

        ''Status
        Public Property ValveDiDoStatus() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(18), udtChTypeData(19))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(18), udtChTypeData(19))
            End Set
        End Property

#End Region

#Region "出力ステータス"

        ''OutStatus1
        Public Property ValveDiDoOutStatus1() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(20 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(20 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus2
        Public Property ValveDiDoOutStatus2() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(28 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(28 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus3
        Public Property ValveDiDoOutStatus3() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(36 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(36 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus4
        Public Property ValveDiDoOutStatus4() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(44 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(44 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus5
        Public Property ValveDiDoOutStatus5() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(52 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(52 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus6
        Public Property ValveDiDoOutStatus6() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(60 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(60 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus7
        Public Property ValveDiDoOutStatus7() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(68 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(68 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus8
        Public Property ValveDiDoOutStatus8() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(76 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(76 + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region

#Region "フィードバックアラーム情報"

        Private Const mCstValveDiDoAlarmStartIndex As Integer = 84

        ''Use
        Public Property ValveDiDoAlarmUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex), udtChTypeData(mCstValveDiDoAlarmStartIndex + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex), udtChTypeData(mCstValveDiDoAlarmStartIndex + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveDiDoAlarmDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 2), udtChTypeData(mCstValveDiDoAlarmStartIndex + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex + 2), udtChTypeData(mCstValveDiDoAlarmStartIndex + 3))
            End Set
        End Property

        ''Sp1
        Public Property ValveDiDoAlarmSp1() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 4), udtChTypeData(mCstValveDiDoAlarmStartIndex + 5))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex + 4), udtChTypeData(mCstValveDiDoAlarmStartIndex + 5))
            End Set
        End Property

        ''Sp2
        Public Property ValveDiDoAlarmSp2() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 6), udtChTypeData(mCstValveDiDoAlarmStartIndex + 7))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex + 6), udtChTypeData(mCstValveDiDoAlarmStartIndex + 7))
            End Set
        End Property

        ''Hys1
        Public Property ValveDiDoAlarmHys1() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 8), udtChTypeData(mCstValveDiDoAlarmStartIndex + 9))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex + 8), udtChTypeData(mCstValveDiDoAlarmStartIndex + 9))
            End Set
        End Property

        ''Hys2
        Public Property ValveDiDoAlarmHys2() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 10), udtChTypeData(mCstValveDiDoAlarmStartIndex + 11))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex + 10), udtChTypeData(mCstValveDiDoAlarmStartIndex + 11))
            End Set
        End Property

        ''St
        Public Property ValveDiDoAlarmSt() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 12), udtChTypeData(mCstValveDiDoAlarmStartIndex + 13))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex + 12), udtChTypeData(mCstValveDiDoAlarmStartIndex + 13))
            End Set
        End Property

        ''Spare1
        Public Property ValveDiDoAlarmSpare1() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 14), udtChTypeData(mCstValveDiDoAlarmStartIndex + 15))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex + 14), udtChTypeData(mCstValveDiDoAlarmStartIndex + 15))
            End Set
        End Property

        ''Var
        Public Property ValveDiDoAlarmVar() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 16), _
                                     udtChTypeData(mCstValveDiDoAlarmStartIndex + 17), _
                                     udtChTypeData(mCstValveDiDoAlarmStartIndex + 18), _
                                     udtChTypeData(mCstValveDiDoAlarmStartIndex + 19))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex + 16), _
                                          udtChTypeData(mCstValveDiDoAlarmStartIndex + 17), _
                                          udtChTypeData(mCstValveDiDoAlarmStartIndex + 18), _
                                          udtChTypeData(mCstValveDiDoAlarmStartIndex + 19))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveDiDoAlarmExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveDiDoAlarmStartIndex + 20)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveDiDoAlarmStartIndex + 20) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveDiDoAlarmGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveDiDoAlarmStartIndex + 21)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveDiDoAlarmStartIndex + 21) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveDiDoAlarmGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveDiDoAlarmStartIndex + 22)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveDiDoAlarmStartIndex + 22) = value
            End Set
        End Property

        ''Spare
        Public Property ValveDiDoAlarmSpare2() As Byte
            Get
                Return udtChTypeData(mCstValveDiDoAlarmStartIndex + 23)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveDiDoAlarmStartIndex + 23) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveDiDoAlarmStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstValveDiDoAlarmStartIndex + 24) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstValveDiDoAlarmStartIndex + 24) + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualReposeState
        Public Property ValveDiDoAlarmManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 32), udtChTypeData(mCstValveDiDoAlarmStartIndex + 33))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex + 32), udtChTypeData(mCstValveDiDoAlarmStartIndex + 33))
            End Set
        End Property

        ''ManualReposeState
        Public Property ValveDiDoAlarmManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 34), udtChTypeData(mCstValveDiDoAlarmStartIndex + 35))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveDiDoAlarmStartIndex + 34), udtChTypeData(mCstValveDiDoAlarmStartIndex + 35))
            End Set
        End Property

#End Region

#Region "その他"
        ''TagNo
        '' 2015.10.22 Verf1.7.5 追加
        Public Property ValveDiDoTagNo() As String

            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveDiDoAlarmStartIndex + 36 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveDiDoAlarmStartIndex + 36 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''LRNo
        '' 2015.11.12 Verf1.7.8 追加
        Public Property ValveDiDoLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 52), _
                                     udtChTypeData(mCstValveDiDoAlarmStartIndex + 53))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveDiDoAlarmStartIndex + 52), _
                                   udtChTypeData(mCstValveDiDoAlarmStartIndex + 53))
            End Set
        End Property

        ''fire_Alm_mimic
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Public Property ValveDiDoAlmMimic() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveDiDoAlarmStartIndex + 54), _
                                     udtChTypeData(mCstValveDiDoAlarmStartIndex + 55))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveDiDoAlarmStartIndex + 54), _
                                   udtChTypeData(mCstValveDiDoAlarmStartIndex + 55))
            End Set
        End Property
#End Region

#End Region

#Region "バルブAI-DO"

#Region "アラーム情報"

#Region " HiHi "

        Private Const mCstValveAiDoStartIndexHiHi As Integer = 0

        ''Use
        Public Property ValveAiDoHiHiUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexHiHi), udtChTypeData(mCstValveAiDoStartIndexHiHi + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexHiHi), udtChTypeData(mCstValveAiDoStartIndexHiHi + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiDoHiHiDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexHiHi + 2), udtChTypeData(mCstValveAiDoStartIndexHiHi + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexHiHi + 2), udtChTypeData(mCstValveAiDoStartIndexHiHi + 3))
            End Set
        End Property

        ''Value
        Public Property ValveAiDoHiHiValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoStartIndexHiHi + 4), _
                                     udtChTypeData(mCstValveAiDoStartIndexHiHi + 5), _
                                     udtChTypeData(mCstValveAiDoStartIndexHiHi + 6), _
                                     udtChTypeData(mCstValveAiDoStartIndexHiHi + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexHiHi + 4), _
                                   udtChTypeData(mCstValveAiDoStartIndexHiHi + 5), _
                                   udtChTypeData(mCstValveAiDoStartIndexHiHi + 6), _
                                   udtChTypeData(mCstValveAiDoStartIndexHiHi + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiDoHiHiExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexHiHi + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexHiHi + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiDoHiHiGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexHiHi + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexHiHi + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiDoHiHiGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexHiHi + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexHiHi + 10) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiDoHiHiSpare() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexHiHi + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexHiHi + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiDoHiHiStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexHiHi + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexHiHi + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property ValveAiDoHiHiManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexHiHi + 20), udtChTypeData(mCstValveAiDoStartIndexHiHi + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexHiHi + 20), udtChTypeData(mCstValveAiDoStartIndexHiHi + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property ValveAiDoHiHiManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexHiHi + 22), udtChTypeData(mCstValveAiDoStartIndexHiHi + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexHiHi + 22), udtChTypeData(mCstValveAiDoStartIndexHiHi + 23))
            End Set
        End Property

#End Region

#Region " Hi "

        Private Const mCstValveAiDoStartIndexHi As Integer = 24

        ''Use
        Public Property ValveAiDoHiUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexHi), udtChTypeData(mCstValveAiDoStartIndexHi + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexHi), udtChTypeData(mCstValveAiDoStartIndexHi + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiDoHiDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexHi + 2), udtChTypeData(mCstValveAiDoStartIndexHi + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexHi + 2), udtChTypeData(mCstValveAiDoStartIndexHi + 3))
            End Set
        End Property

        ''Value
        Public Property ValveAiDoHiValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoStartIndexHi + 4), _
                                     udtChTypeData(mCstValveAiDoStartIndexHi + 5), _
                                     udtChTypeData(mCstValveAiDoStartIndexHi + 6), _
                                     udtChTypeData(mCstValveAiDoStartIndexHi + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexHi + 4), _
                                   udtChTypeData(mCstValveAiDoStartIndexHi + 5), _
                                   udtChTypeData(mCstValveAiDoStartIndexHi + 6), _
                                   udtChTypeData(mCstValveAiDoStartIndexHi + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiDoHiExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexHi + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexHi + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiDoHiGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexHi + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexHi + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiDoHiGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexHi + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexHi + 10) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiDoHiSpare() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexHi + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexHi + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiDoHiStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexHi + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexHi + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property ValveAiDoHiManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexHi + 20), udtChTypeData(mCstValveAiDoStartIndexHi + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexHi + 20), udtChTypeData(mCstValveAiDoStartIndexHi + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property ValveAiDoHiManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexHi + 22), udtChTypeData(mCstValveAiDoStartIndexHi + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexHi + 22), udtChTypeData(mCstValveAiDoStartIndexHi + 23))
            End Set
        End Property

#End Region

#Region " Lo "

        Private Const mCstValveAiDoStartIndexLo As Integer = 48

        ''Use
        Public Property ValveAiDoLoUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexLo), udtChTypeData(mCstValveAiDoStartIndexLo + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexLo), udtChTypeData(mCstValveAiDoStartIndexLo + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiDoLoDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexLo + 2), udtChTypeData(mCstValveAiDoStartIndexLo + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexLo + 2), udtChTypeData(mCstValveAiDoStartIndexLo + 3))
            End Set
        End Property

        ''Value
        Public Property ValveAiDoLoValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoStartIndexLo + 4), _
                                     udtChTypeData(mCstValveAiDoStartIndexLo + 5), _
                                     udtChTypeData(mCstValveAiDoStartIndexLo + 6), _
                                     udtChTypeData(mCstValveAiDoStartIndexLo + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexLo + 4), _
                                   udtChTypeData(mCstValveAiDoStartIndexLo + 5), _
                                   udtChTypeData(mCstValveAiDoStartIndexLo + 6), _
                                   udtChTypeData(mCstValveAiDoStartIndexLo + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiDoLoExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexLo + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexLo + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiDoLoGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexLo + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexLo + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiDoLoGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexLo + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexLo + 10) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiDoLoSpare() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexLo + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexLo + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiDoLoStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexLo + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexLo + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property ValveAiDoLoManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexLo + 20), udtChTypeData(mCstValveAiDoStartIndexLo + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexLo + 20), udtChTypeData(mCstValveAiDoStartIndexLo + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property ValveAiDoLoManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexLo + 22), udtChTypeData(mCstValveAiDoStartIndexLo + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexLo + 22), udtChTypeData(mCstValveAiDoStartIndexLo + 23))
            End Set
        End Property

#End Region

#Region " LoLo "

        Private Const mCstValveAiDoStartIndexLoLo As Integer = 72

        ''Use
        Public Property ValveAiDoLoLoUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexLoLo), udtChTypeData(mCstValveAiDoStartIndexLoLo + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexLoLo), udtChTypeData(mCstValveAiDoStartIndexLoLo + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiDoLoLoDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexLoLo + 2), udtChTypeData(mCstValveAiDoStartIndexLoLo + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexLoLo + 2), udtChTypeData(mCstValveAiDoStartIndexLoLo + 3))
            End Set
        End Property

        ''Value
        Public Property ValveAiDoLoLoValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoStartIndexLoLo + 4), _
                                     udtChTypeData(mCstValveAiDoStartIndexLoLo + 5), _
                                     udtChTypeData(mCstValveAiDoStartIndexLoLo + 6), _
                                     udtChTypeData(mCstValveAiDoStartIndexLoLo + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexLoLo + 4), _
                                   udtChTypeData(mCstValveAiDoStartIndexLoLo + 5), _
                                   udtChTypeData(mCstValveAiDoStartIndexLoLo + 6), _
                                   udtChTypeData(mCstValveAiDoStartIndexLoLo + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiDoLoLoExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexLoLo + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexLoLo + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiDoLoLoGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexLoLo + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexLoLo + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiDoLoLoGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexLoLo + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexLoLo + 10) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiDoLoLoSpare() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexLoLo + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexLoLo + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiDoLoLoStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexLoLo + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexLoLo + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property ValveAiDoLoLoManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexLoLo + 20), udtChTypeData(mCstValveAiDoStartIndexLoLo + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexLoLo + 20), udtChTypeData(mCstValveAiDoStartIndexLoLo + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property ValveAiDoLoLoManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexLoLo + 22), udtChTypeData(mCstValveAiDoStartIndexLoLo + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexLoLo + 22), udtChTypeData(mCstValveAiDoStartIndexLoLo + 23))
            End Set
        End Property

#End Region

#Region " SensorFail "

        Private Const mCstValveAiDoStartIndexSensorFail As Integer = 96

        ''Use
        Public Property ValveAiDoSensorFailUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexSensorFail), udtChTypeData(mCstValveAiDoStartIndexSensorFail + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexSensorFail), udtChTypeData(mCstValveAiDoStartIndexSensorFail + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiDoSensorFailDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexSensorFail + 2), udtChTypeData(mCstValveAiDoStartIndexSensorFail + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexSensorFail + 2), udtChTypeData(mCstValveAiDoStartIndexSensorFail + 3))
            End Set
        End Property

        ''Value
        Public Property ValveAiDoSensorFailValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoStartIndexSensorFail + 4), _
                                     udtChTypeData(mCstValveAiDoStartIndexSensorFail + 5), _
                                     udtChTypeData(mCstValveAiDoStartIndexSensorFail + 6), _
                                     udtChTypeData(mCstValveAiDoStartIndexSensorFail + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexSensorFail + 4), _
                                   udtChTypeData(mCstValveAiDoStartIndexSensorFail + 5), _
                                   udtChTypeData(mCstValveAiDoStartIndexSensorFail + 6), _
                                   udtChTypeData(mCstValveAiDoStartIndexSensorFail + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiDoSensorFailExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexSensorFail + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexSensorFail + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiDoSensorFailGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexSensorFail + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexSensorFail + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiDoSensorFailGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexSensorFail + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexSensorFail + 10) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiDoSensorFailSpare() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoStartIndexSensorFail + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoStartIndexSensorFail + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiDoSensorFailStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexSensorFail + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexSensorFail + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property ValveAiDoSensorFailManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexSensorFail + 20), udtChTypeData(mCstValveAiDoStartIndexSensorFail + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexSensorFail + 20), udtChTypeData(mCstValveAiDoStartIndexSensorFail + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property ValveAiDoSensorFailManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexSensorFail + 22), udtChTypeData(mCstValveAiDoStartIndexSensorFail + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexSensorFail + 22), udtChTypeData(mCstValveAiDoStartIndexSensorFail + 23))
            End Set
        End Property

#End Region

#End Region

#Region "バルブAiDo項目"

        Private Const mCstValveAiDoStartIndexItem As Integer = 120

        ''RangeHigh
        Public Property ValveAiDoRangeHigh() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 0), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 1), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 2), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 3))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 0), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 1), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 2), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 3))
            End Set
        End Property

        ''RangeLow
        Public Property ValveAiDoRangeLow() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 4), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 5), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 6), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 4), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 5), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 6), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 7))
            End Set
        End Property

        ''NormalHigh
        Public Property ValveAiDoNormalHigh() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 8), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 9), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 10), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 11))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 8), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 9), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 10), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 11))
            End Set
        End Property

        ''NormalLow
        Public Property ValveAiDoNormalLow() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 12), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 13), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 14), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 15))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 12), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 13), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 14), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 15))
            End Set
        End Property

        ''OffsetValue
        Public Property ValveAiDoOffsetValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 16), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 17), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 18), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 19))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 16), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 17), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 18), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 19))
            End Set
        End Property

        ''String
        Public Property ValveAiDoString() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 20), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 21))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 20), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 21))
            End Set
        End Property

        ''DecimalPosition
        Public Property ValveAiDoDecimalPosition() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 22), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 23))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 22), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 23))
            End Set
        End Property

        ''Display3
        Public Property ValveAiDoDisplay3() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 24), _
                                     udtChTypeData(mCstValveAiDoStartIndexItem + 25))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 24), _
                                   udtChTypeData(mCstValveAiDoStartIndexItem + 25))
            End Set
        End Property

        ''Feedback
        Public Property ValveAiDoFeedback() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 26), udtChTypeData(mCstValveAiDoStartIndexItem + 27))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexItem + 26), udtChTypeData(mCstValveAiDoStartIndexItem + 27))
            End Set
        End Property

        ''FuNo
        Public Property ValveAiDoFuNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 28), udtChTypeData(mCstValveAiDoStartIndexItem + 29))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexItem + 28), udtChTypeData(mCstValveAiDoStartIndexItem + 29))
            End Set
        End Property

        ''PortNo
        Public Property ValveAiDoPortNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 30), udtChTypeData(mCstValveAiDoStartIndexItem + 31))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexItem + 30), udtChTypeData(mCstValveAiDoStartIndexItem + 31))
            End Set
        End Property

        ''Pin
        Public Property ValveAiDoPin() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 32), udtChTypeData(mCstValveAiDoStartIndexItem + 33))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexItem + 32), udtChTypeData(mCstValveAiDoStartIndexItem + 33))
            End Set
        End Property

        ''PinNo
        Public Property ValveAiDoPinNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 34), udtChTypeData(mCstValveAiDoStartIndexItem + 35))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexItem + 34), udtChTypeData(mCstValveAiDoStartIndexItem + 35))
            End Set
        End Property

        ''OutControl
        Public Property ValveAiDoOutControl() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 36), udtChTypeData(mCstValveAiDoStartIndexItem + 37))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexItem + 36), udtChTypeData(mCstValveAiDoStartIndexItem + 37))
            End Set
        End Property

        ''Width
        Public Property ValveAiDoWidth() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 38), udtChTypeData(mCstValveAiDoStartIndexItem + 39))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexItem + 38), udtChTypeData(mCstValveAiDoStartIndexItem + 39))
            End Set
        End Property

        ''Spare
        Public Property ValveAiDoSpare() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 40), udtChTypeData(mCstValveAiDoStartIndexItem + 41))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexItem + 40), udtChTypeData(mCstValveAiDoStartIndexItem + 41))
            End Set
        End Property

        ''OutStatus
        Public Property ValveAiDoOutStatus() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoStartIndexItem + 42), udtChTypeData(mCstValveAiDoStartIndexItem + 43))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoStartIndexItem + 42), udtChTypeData(mCstValveAiDoStartIndexItem + 43))
            End Set
        End Property

#End Region

#Region "出力ステータス"

        Private Const mCstValveAiDoStartIndexOutStatus As Integer = mCstValveAiDoStartIndexItem + 44

        ''OutStatus1
        Public Property ValveAiDoOutStatus1() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexOutStatus + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexOutStatus + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus2
        Public Property ValveAiDoOutStatus2() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexOutStatus + 8 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexOutStatus + 8 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus3
        Public Property ValveAiDoOutStatus3() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexOutStatus + 16 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexOutStatus + 16 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus4
        Public Property ValveAiDoOutStatus4() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexOutStatus + 24 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexOutStatus + 24 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus5
        Public Property ValveAiDoOutStatus5() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexOutStatus + 32 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexOutStatus + 32 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus6
        Public Property ValveAiDoOutStatus6() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexOutStatus + 40 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexOutStatus + 40 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus7
        Public Property ValveAiDoOutStatus7() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexOutStatus + 48 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexOutStatus + 48 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''OutStatus8
        Public Property ValveAiDoOutStatus8() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoStartIndexOutStatus + 56 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoStartIndexOutStatus + 56 + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region

#Region "フィードバックアラーム情報"

        Private Const mCstValveAiDoAlarmStartIndex As Integer = mCstValveAiDoStartIndexOutStatus + 64

        ''Use
        Public Property ValveAiDoAlarmUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex), udtChTypeData(mCstValveAiDoAlarmStartIndex + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex), udtChTypeData(mCstValveAiDoAlarmStartIndex + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiDoAlarmDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 2), udtChTypeData(mCstValveAiDoAlarmStartIndex + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex + 2), udtChTypeData(mCstValveAiDoAlarmStartIndex + 3))
            End Set
        End Property

        ''Sp1
        Public Property ValveAiDoAlarmSp1() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 4), udtChTypeData(mCstValveAiDoAlarmStartIndex + 5))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex + 4), udtChTypeData(mCstValveAiDoAlarmStartIndex + 5))
            End Set
        End Property

        ''Sp2
        Public Property ValveAiDoAlarmSp2() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 6), udtChTypeData(mCstValveAiDoAlarmStartIndex + 7))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex + 6), udtChTypeData(mCstValveAiDoAlarmStartIndex + 7))
            End Set
        End Property

        ''Hys1
        Public Property ValveAiDoAlarmHys1() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 8), udtChTypeData(mCstValveAiDoAlarmStartIndex + 9))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex + 8), udtChTypeData(mCstValveAiDoAlarmStartIndex + 9))
            End Set
        End Property

        ''Hys2
        Public Property ValveAiDoAlarmHys2() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 10), udtChTypeData(mCstValveAiDoAlarmStartIndex + 11))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex + 10), udtChTypeData(mCstValveAiDoAlarmStartIndex + 11))
            End Set
        End Property

        ''St
        Public Property ValveAiDoAlarmSt() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 12), udtChTypeData(mCstValveAiDoAlarmStartIndex + 13))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex + 12), udtChTypeData(mCstValveAiDoAlarmStartIndex + 13))
            End Set
        End Property

        ''Spare1
        Public Property ValveAiDoAlarmSpare1() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 14), udtChTypeData(mCstValveAiDoAlarmStartIndex + 15))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex + 14), udtChTypeData(mCstValveAiDoAlarmStartIndex + 15))
            End Set
        End Property

        ''Var
        Public Property ValveAiDoAlarmVar() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 16), _
                                     udtChTypeData(mCstValveAiDoAlarmStartIndex + 17), _
                                     udtChTypeData(mCstValveAiDoAlarmStartIndex + 18), _
                                     udtChTypeData(mCstValveAiDoAlarmStartIndex + 19))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex + 16), _
                                          udtChTypeData(mCstValveAiDoAlarmStartIndex + 17), _
                                          udtChTypeData(mCstValveAiDoAlarmStartIndex + 18), _
                                          udtChTypeData(mCstValveAiDoAlarmStartIndex + 19))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiDoAlarmExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoAlarmStartIndex + 20)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoAlarmStartIndex + 20) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiDoAlarmGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoAlarmStartIndex + 21)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoAlarmStartIndex + 21) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiDoAlarmGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoAlarmStartIndex + 22)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoAlarmStartIndex + 22) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiDoAlarmSpare2() As Byte
            Get
                Return udtChTypeData(mCstValveAiDoAlarmStartIndex + 23)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiDoAlarmStartIndex + 23) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiDoAlarmStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstValveAiDoAlarmStartIndex + 24) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstValveAiDoAlarmStartIndex + 24) + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualReposeState
        Public Property ValveAiDoAlarmManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 32), udtChTypeData(mCstValveAiDoAlarmStartIndex + 33))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex + 32), udtChTypeData(mCstValveAiDoAlarmStartIndex + 33))
            End Set
        End Property

        ''ManualReposeState
        Public Property ValveAiDoAlarmManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 34), udtChTypeData(mCstValveAiDoAlarmStartIndex + 35))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiDoAlarmStartIndex + 34), udtChTypeData(mCstValveAiDoAlarmStartIndex + 35))
            End Set
        End Property

#End Region

#Region "その他"
        ''TagNo
        '' 2015.10.22 Verf1.7.5 追加
        Public Property ValveAiDoTagNo() As String

            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiDoAlarmStartIndex + 36 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiDoAlarmStartIndex + 36 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''LRNo
        '' 2015.11.12 Verf1.7.8 追加
        Public Property ValveAiDoLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 52), _
                                     udtChTypeData(mCstValveAiDoAlarmStartIndex + 53))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiDoAlarmStartIndex + 52), _
                                   udtChTypeData(mCstValveAiDoAlarmStartIndex + 53))
            End Set
        End Property

        ''fire_Alm_mimic
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Public Property ValveAiDoAlmMimic() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiDoAlarmStartIndex + 54), _
                                     udtChTypeData(mCstValveAiDoAlarmStartIndex + 55))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiDoAlarmStartIndex + 54), _
                                   udtChTypeData(mCstValveAiDoAlarmStartIndex + 55))
            End Set
        End Property
#End Region

#End Region

#Region "バルブAI-AO"

#Region "アラーム情報"

#Region " HiHi "

        Private Const mCstValveAiAoStartIndexHiHi As Integer = 0

        ''Use
        Public Property ValveAiAoHiHiUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexHiHi), udtChTypeData(mCstValveAiAoStartIndexHiHi + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexHiHi), udtChTypeData(mCstValveAiAoStartIndexHiHi + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiAoHiHiDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexHiHi + 2), udtChTypeData(mCstValveAiAoStartIndexHiHi + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexHiHi + 2), udtChTypeData(mCstValveAiAoStartIndexHiHi + 3))
            End Set
        End Property

        ''Value
        Public Property ValveAiAoHiHiValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoStartIndexHiHi + 4), _
                                     udtChTypeData(mCstValveAiAoStartIndexHiHi + 5), _
                                     udtChTypeData(mCstValveAiAoStartIndexHiHi + 6), _
                                     udtChTypeData(mCstValveAiAoStartIndexHiHi + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexHiHi + 4), _
                                   udtChTypeData(mCstValveAiAoStartIndexHiHi + 5), _
                                   udtChTypeData(mCstValveAiAoStartIndexHiHi + 6), _
                                   udtChTypeData(mCstValveAiAoStartIndexHiHi + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiAoHiHiExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexHiHi + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexHiHi + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiAoHiHiGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexHiHi + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexHiHi + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiAoHiHiGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexHiHi + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexHiHi + 10) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiAoHiHiSpare() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexHiHi + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexHiHi + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiAoHiHiStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexHiHi + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiAoStartIndexHiHi + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property ValveAiAoHiHiManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexHiHi + 20), udtChTypeData(mCstValveAiAoStartIndexHiHi + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexHiHi + 20), udtChTypeData(mCstValveAiAoStartIndexHiHi + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property ValveAiAoHiHiManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexHiHi + 22), udtChTypeData(mCstValveAiAoStartIndexHiHi + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexHiHi + 22), udtChTypeData(mCstValveAiAoStartIndexHiHi + 23))
            End Set
        End Property

#End Region

#Region " Hi "

        Private Const mCstValveAiAoStartIndexHi As Integer = 24

        ''Use
        Public Property ValveAiAoHiUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexHi), udtChTypeData(mCstValveAiAoStartIndexHi + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexHi), udtChTypeData(mCstValveAiAoStartIndexHi + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiAoHiDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexHi + 2), udtChTypeData(mCstValveAiAoStartIndexHi + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexHi + 2), udtChTypeData(mCstValveAiAoStartIndexHi + 3))
            End Set
        End Property

        ''Value
        Public Property ValveAiAoHiValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoStartIndexHi + 4), _
                                     udtChTypeData(mCstValveAiAoStartIndexHi + 5), _
                                     udtChTypeData(mCstValveAiAoStartIndexHi + 6), _
                                     udtChTypeData(mCstValveAiAoStartIndexHi + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexHi + 4), _
                                   udtChTypeData(mCstValveAiAoStartIndexHi + 5), _
                                   udtChTypeData(mCstValveAiAoStartIndexHi + 6), _
                                   udtChTypeData(mCstValveAiAoStartIndexHi + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiAoHiExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexHi + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexHi + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiAoHiGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexHi + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexHi + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiAoHiGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexHi + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexHi + 10) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiAoHiSpare() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexHi + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexHi + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiAoHiStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexHi + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiAoStartIndexHi + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property ValveAiAoHiManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexHi + 20), udtChTypeData(mCstValveAiAoStartIndexHi + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexHi + 20), udtChTypeData(mCstValveAiAoStartIndexHi + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property ValveAiAoHiManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexHi + 22), udtChTypeData(mCstValveAiAoStartIndexHi + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexHi + 22), udtChTypeData(mCstValveAiAoStartIndexHi + 23))
            End Set
        End Property

#End Region

#Region " Lo "

        Private Const mCstValveAiAoStartIndexLo As Integer = 48

        ''Use
        Public Property ValveAiAoLoUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexLo), udtChTypeData(mCstValveAiAoStartIndexLo + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexLo), udtChTypeData(mCstValveAiAoStartIndexLo + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiAoLoDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexLo + 2), udtChTypeData(mCstValveAiAoStartIndexLo + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexLo + 2), udtChTypeData(mCstValveAiAoStartIndexLo + 3))
            End Set
        End Property

        ''Value
        Public Property ValveAiAoLoValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoStartIndexLo + 4), _
                                     udtChTypeData(mCstValveAiAoStartIndexLo + 5), _
                                     udtChTypeData(mCstValveAiAoStartIndexLo + 6), _
                                     udtChTypeData(mCstValveAiAoStartIndexLo + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexLo + 4), _
                                   udtChTypeData(mCstValveAiAoStartIndexLo + 5), _
                                   udtChTypeData(mCstValveAiAoStartIndexLo + 6), _
                                   udtChTypeData(mCstValveAiAoStartIndexLo + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiAoLoExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexLo + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexLo + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiAoLoGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexLo + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexLo + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiAoLoGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexLo + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexLo + 10) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiAoLoSpare() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexLo + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexLo + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiAoLoStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexLo + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiAoStartIndexLo + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property ValveAiAoLoManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexLo + 20), udtChTypeData(mCstValveAiAoStartIndexLo + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexLo + 20), udtChTypeData(mCstValveAiAoStartIndexLo + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property ValveAiAoLoManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexLo + 22), udtChTypeData(mCstValveAiAoStartIndexLo + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexLo + 22), udtChTypeData(mCstValveAiAoStartIndexLo + 23))
            End Set
        End Property

#End Region

#Region " LoLo "

        Private Const mCstValveAiAoStartIndexLoLo As Integer = 72

        ''Use
        Public Property ValveAiAoLoLoUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexLoLo), udtChTypeData(mCstValveAiAoStartIndexLoLo + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexLoLo), udtChTypeData(mCstValveAiAoStartIndexLoLo + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiAoLoLoDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexLoLo + 2), udtChTypeData(mCstValveAiAoStartIndexLoLo + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexLoLo + 2), udtChTypeData(mCstValveAiAoStartIndexLoLo + 3))
            End Set
        End Property

        ''Value
        Public Property ValveAiAoLoLoValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoStartIndexLoLo + 4), _
                                     udtChTypeData(mCstValveAiAoStartIndexLoLo + 5), _
                                     udtChTypeData(mCstValveAiAoStartIndexLoLo + 6), _
                                     udtChTypeData(mCstValveAiAoStartIndexLoLo + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexLoLo + 4), _
                                   udtChTypeData(mCstValveAiAoStartIndexLoLo + 5), _
                                   udtChTypeData(mCstValveAiAoStartIndexLoLo + 6), _
                                   udtChTypeData(mCstValveAiAoStartIndexLoLo + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiAoLoLoExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexLoLo + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexLoLo + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiAoLoLoGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexLoLo + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexLoLo + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiAoLoLoGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexLoLo + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexLoLo + 10) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiAoLoLoSpare() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexLoLo + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexLoLo + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiAoLoLoStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexLoLo + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiAoStartIndexLoLo + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property ValveAiAoLoLoManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexLoLo + 20), udtChTypeData(mCstValveAiAoStartIndexLoLo + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexLoLo + 20), udtChTypeData(mCstValveAiAoStartIndexLoLo + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property ValveAiAoLoLoManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexLoLo + 22), udtChTypeData(mCstValveAiAoStartIndexLoLo + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexLoLo + 22), udtChTypeData(mCstValveAiAoStartIndexLoLo + 23))
            End Set
        End Property

#End Region

#Region " SensorFail "

        Private Const mCstValveAiAoStartIndexSensorFail As Integer = 96

        ''Use
        Public Property ValveAiAoSensorFailUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexSensorFail), udtChTypeData(mCstValveAiAoStartIndexSensorFail + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexSensorFail), udtChTypeData(mCstValveAiAoStartIndexSensorFail + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiAoSensorFailDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexSensorFail + 2), udtChTypeData(mCstValveAiAoStartIndexSensorFail + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexSensorFail + 2), udtChTypeData(mCstValveAiAoStartIndexSensorFail + 3))
            End Set
        End Property

        ''Value
        Public Property ValveAiAoSensorFailValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoStartIndexSensorFail + 4), _
                                     udtChTypeData(mCstValveAiAoStartIndexSensorFail + 5), _
                                     udtChTypeData(mCstValveAiAoStartIndexSensorFail + 6), _
                                     udtChTypeData(mCstValveAiAoStartIndexSensorFail + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexSensorFail + 4), _
                                   udtChTypeData(mCstValveAiAoStartIndexSensorFail + 5), _
                                   udtChTypeData(mCstValveAiAoStartIndexSensorFail + 6), _
                                   udtChTypeData(mCstValveAiAoStartIndexSensorFail + 7))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiAoSensorFailExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexSensorFail + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexSensorFail + 8) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiAoSensorFailGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexSensorFail + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexSensorFail + 9) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiAoSensorFailGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexSensorFail + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexSensorFail + 10) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiAoSensorFailSpare() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoStartIndexSensorFail + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoStartIndexSensorFail + 11) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiAoSensorFailStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexSensorFail + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiAoStartIndexSensorFail + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualRepose
        Public Property ValveAiAoSensorFailManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexSensorFail + 20), udtChTypeData(mCstValveAiAoStartIndexSensorFail + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexSensorFail + 20), udtChTypeData(mCstValveAiAoStartIndexSensorFail + 21))
            End Set
        End Property

        ''ManualReposeSet
        Public Property ValveAiAoSensorFailManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexSensorFail + 22), udtChTypeData(mCstValveAiAoStartIndexSensorFail + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoStartIndexSensorFail + 22), udtChTypeData(mCstValveAiAoStartIndexSensorFail + 23))
            End Set
        End Property

#End Region

#End Region

#Region "バルブAiAo項目"

        Private Const mCstValveAiAoStartIndexItem As Integer = 120

        ''RangeHigh
        Public Property ValveAiAoRangeHigh() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 0), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 1), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 2), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 3))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 0), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 1), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 2), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 3))
            End Set
        End Property

        ''RangeLow
        Public Property ValveAiAoRangeLow() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 4), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 5), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 6), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 4), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 5), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 6), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 7))
            End Set
        End Property

        ''NormalHigh
        Public Property ValveAiAoNormalHigh() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 8), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 9), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 10), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 11))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 8), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 9), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 10), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 11))
            End Set
        End Property

        ''NormalLow
        Public Property ValveAiAoNormalLow() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 12), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 13), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 14), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 15))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 12), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 13), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 14), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 15))
            End Set
        End Property

        ''OffsetValue
        Public Property ValveAiAoOffsetValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 16), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 17), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 18), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 19))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 16), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 17), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 18), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 19))
            End Set
        End Property

        ''String
        Public Property ValveAiAoString() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 20), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 21))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 20), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 21))
            End Set
        End Property

        ''DecimalPosition
        Public Property ValveAiAoDecimalPosition() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 22), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 23))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 22), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 23))
            End Set
        End Property

        ''Display3
        Public Property ValveAiAoDisplay3() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 24), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 25))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 24), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 25))
            End Set
        End Property

        ''Spare
        Public Property ValveAiAoSpare() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 26), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 27))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 26), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 27))
            End Set
        End Property

        ''Feedback
        Public Property ValveAiAoFeedback() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 28), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 29))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 28), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 29))
            End Set
        End Property

        ''FuNo
        Public Property ValveAiAoFuNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 30), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 31))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 30), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 31))
            End Set
        End Property

        ''PortNo
        Public Property ValveAiAoPortNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 32), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 33))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 32), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 33))
            End Set
        End Property

        ''Pin
        Public Property ValveAiAoPin() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 34), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 35))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 34), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 35))
            End Set
        End Property

        ''PinNo
        Public Property ValveAiAoPinNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 36), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 37))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 36), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 37))
            End Set
        End Property

        ''OutStatus
        Public Property ValveAiAoOutStatus() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoStartIndexItem + 38), _
                                     udtChTypeData(mCstValveAiAoStartIndexItem + 39))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 38), _
                                   udtChTypeData(mCstValveAiAoStartIndexItem + 39))
            End Set
        End Property

#End Region

#Region "出力ステータス"

        Private Const mCstValveAiAoStartIndexOutStatus As Integer = mCstValveAiAoStartIndexItem + 40

        ''OutStatus1
        Public Property ValveAiAoOutStatus1() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexOutStatus + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiAoStartIndexOutStatus + j) = bytArray(j)
                Next j

            End Set
        End Property

        ' ''OutStatus2
        'Public Property ValveAiAoOutStatus2() As String
        '    Get

        '        Dim strRtn As String
        '        Dim bytArray(8) As Byte

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexOutStatus + 8 + j)
        '        Next j

        '        strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
        '        Return strRtn

        '    End Get
        '    Set(ByVal value As String)

        '        Dim bytArray() As Byte

        '        bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(Left(value & "        ", 8))

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            udtChTypeData(mCstValveAiAoStartIndexOutStatus + 8 + j) = bytArray(j)
        '        Next j

        '    End Set
        'End Property

        ' ''OutStatus3
        'Public Property ValveAiAoOutStatus3() As String
        '    Get

        '        Dim strRtn As String
        '        Dim bytArray(8) As Byte

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexOutStatus + 16 + j)
        '        Next j

        '        strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
        '        Return strRtn

        '    End Get
        '    Set(ByVal value As String)

        '        Dim bytArray() As Byte

        '        bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(Left(value & "        ", 8))

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            udtChTypeData(mCstValveAiAoStartIndexOutStatus + 16 + j) = bytArray(j)
        '        Next j

        '    End Set
        'End Property

        ' ''OutStatus4
        'Public Property ValveAiAoOutStatus4() As String
        '    Get

        '        Dim strRtn As String
        '        Dim bytArray(8) As Byte

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexOutStatus + 24 + j)
        '        Next j

        '        strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
        '        Return strRtn

        '    End Get
        '    Set(ByVal value As String)

        '        Dim bytArray() As Byte

        '        bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(Left(value & "        ", 8))

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            udtChTypeData(mCstValveAiAoStartIndexOutStatus + 24 + j) = bytArray(j)
        '        Next j

        '    End Set
        'End Property

        ' ''OutStatus5
        'Public Property ValveAiAoOutStatus5() As String
        '    Get

        '        Dim strRtn As String
        '        Dim bytArray(8) As Byte

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexOutStatus + 32 + j)
        '        Next j

        '        strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
        '        Return strRtn

        '    End Get
        '    Set(ByVal value As String)

        '        Dim bytArray() As Byte

        '        bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(Left(value & "        ", 8))

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            udtChTypeData(mCstValveAiAoStartIndexOutStatus + 32 + j) = bytArray(j)
        '        Next j

        '    End Set
        'End Property

        ' ''OutStatus6
        'Public Property ValveAiAoOutStatus6() As String
        '    Get

        '        Dim strRtn As String
        '        Dim bytArray(8) As Byte

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexOutStatus + 40 + j)
        '        Next j

        '        strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
        '        Return strRtn

        '    End Get
        '    Set(ByVal value As String)

        '        Dim bytArray() As Byte

        '        bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(Left(value & "        ", 8))

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            udtChTypeData(mCstValveAiAoStartIndexOutStatus + 40 + j) = bytArray(j)
        '        Next j

        '    End Set
        'End Property

        ' ''OutStatus7
        'Public Property ValveAiAoOutStatus7() As String
        '    Get

        '        Dim strRtn As String
        '        Dim bytArray(8) As Byte

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexOutStatus + 48 + j)
        '        Next j

        '        strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
        '        Return strRtn

        '    End Get
        '    Set(ByVal value As String)

        '        Dim bytArray() As Byte

        '        bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(Left(value & "        ", 8))

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            udtChTypeData(mCstValveAiAoStartIndexOutStatus + 48 + j) = bytArray(j)
        '        Next j

        '    End Set
        'End Property

        ' ''OutStatus8
        'Public Property ValveAiAoOutStatus8() As String
        '    Get

        '        Dim strRtn As String
        '        Dim bytArray(8) As Byte

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            bytArray(j) = udtChTypeData(mCstValveAiAoStartIndexOutStatus + 56 + j)
        '        Next j

        '        strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
        '        Return strRtn

        '    End Get
        '    Set(ByVal value As String)

        '        Dim bytArray() As Byte

        '        bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(Left(value & "        ", 8))

        '        For j As Integer = LBound(bytArray) To UBound(bytArray)
        '            udtChTypeData(mCstValveAiAoStartIndexOutStatus + 56 + j) = bytArray(j)
        '        Next j

        '    End Set
        'End Property

#End Region

#Region "フィードバックアラーム情報"

        Private Const mCstValveAiAoAlarmStartIndex As Integer = mCstValveAiAoStartIndexOutStatus + 8

        ''Use
        Public Property ValveAiAoAlarmUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex), udtChTypeData(mCstValveAiAoAlarmStartIndex + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex), udtChTypeData(mCstValveAiAoAlarmStartIndex + 1))
            End Set
        End Property

        ''Delay
        Public Property ValveAiAoAlarmDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 2), udtChTypeData(mCstValveAiAoAlarmStartIndex + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex + 2), udtChTypeData(mCstValveAiAoAlarmStartIndex + 3))
            End Set
        End Property

        ''Sp1
        Public Property ValveAiAoAlarmSp1() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 4), udtChTypeData(mCstValveAiAoAlarmStartIndex + 5))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex + 4), udtChTypeData(mCstValveAiAoAlarmStartIndex + 5))
            End Set
        End Property

        ''Sp2
        Public Property ValveAiAoAlarmSp2() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 6), udtChTypeData(mCstValveAiAoAlarmStartIndex + 7))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex + 6), udtChTypeData(mCstValveAiAoAlarmStartIndex + 7))
            End Set
        End Property

        ''Hys1
        Public Property ValveAiAoAlarmHys1() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 8), udtChTypeData(mCstValveAiAoAlarmStartIndex + 9))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex + 8), udtChTypeData(mCstValveAiAoAlarmStartIndex + 9))
            End Set
        End Property

        ''Hys2
        Public Property ValveAiAoAlarmHys2() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 10), udtChTypeData(mCstValveAiAoAlarmStartIndex + 11))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex + 10), udtChTypeData(mCstValveAiAoAlarmStartIndex + 11))
            End Set
        End Property

        ''St
        Public Property ValveAiAoAlarmSt() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 12), udtChTypeData(mCstValveAiAoAlarmStartIndex + 13))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex + 12), udtChTypeData(mCstValveAiAoAlarmStartIndex + 13))
            End Set
        End Property

        ''Spare1
        Public Property ValveAiAoAlarmSpare1() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 14), udtChTypeData(mCstValveAiAoAlarmStartIndex + 15))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex + 14), udtChTypeData(mCstValveAiAoAlarmStartIndex + 15))
            End Set
        End Property

        ''Var
        Public Property ValveAiAoAlarmVar() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 16), _
                                           udtChTypeData(mCstValveAiAoAlarmStartIndex + 17), _
                                           udtChTypeData(mCstValveAiAoAlarmStartIndex + 18), _
                                           udtChTypeData(mCstValveAiAoAlarmStartIndex + 19))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex + 16), _
                                          udtChTypeData(mCstValveAiAoAlarmStartIndex + 17), _
                                          udtChTypeData(mCstValveAiAoAlarmStartIndex + 18), _
                                          udtChTypeData(mCstValveAiAoAlarmStartIndex + 19))
            End Set
        End Property

        ''ExtGroup
        Public Property ValveAiAoAlarmExtGroup() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoAlarmStartIndex + 20)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoAlarmStartIndex + 20) = value
            End Set
        End Property

        ''GroupRepose1
        Public Property ValveAiAoAlarmGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoAlarmStartIndex + 21)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoAlarmStartIndex + 21) = value
            End Set
        End Property

        ''GroupRepose2
        Public Property ValveAiAoAlarmGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoAlarmStartIndex + 22)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoAlarmStartIndex + 22) = value
            End Set
        End Property

        ''Spare
        Public Property ValveAiAoAlarmSpare2() As Byte
            Get
                Return udtChTypeData(mCstValveAiAoAlarmStartIndex + 23)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstValveAiAoAlarmStartIndex + 23) = value
            End Set
        End Property

        ''StatusInput
        Public Property ValveAiAoAlarmStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstValveAiAoAlarmStartIndex + 24) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstValveAiAoAlarmStartIndex + 24) + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''ManualReposeState
        Public Property ValveAiAoAlarmManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 32), udtChTypeData(mCstValveAiAoAlarmStartIndex + 33))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex + 32), udtChTypeData(mCstValveAiAoAlarmStartIndex + 33))
            End Set
        End Property

        ''ManualReposeState
        Public Property ValveAiAoAlarmManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 34), udtChTypeData(mCstValveAiAoAlarmStartIndex + 35))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstValveAiAoAlarmStartIndex + 34), udtChTypeData(mCstValveAiAoAlarmStartIndex + 35))
            End Set
        End Property

#End Region

#Region "その他"
        ''TagNo
        '' 2015.10.22 Verf1.7.5 追加
        Public Property ValveAiAoTagNo() As String

            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstValveAiAoAlarmStartIndex + 36 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstValveAiAoAlarmStartIndex + 36 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''LRNo
        '' 2015.11.12 Verf1.7.8 追加
        Public Property ValveAiAoLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 52), _
                                     udtChTypeData(mCstValveAiAoAlarmStartIndex + 53))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoAlarmStartIndex + 52), _
                                   udtChTypeData(mCstValveAiAoAlarmStartIndex + 53))
            End Set
        End Property

        ''fire_Alm_mimic
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Public Property ValveAiAoAlmMimic() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstValveAiAoAlarmStartIndex + 54), _
                                     udtChTypeData(mCstValveAiAoAlarmStartIndex + 55))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstValveAiAoAlarmStartIndex + 54), _
                                   udtChTypeData(mCstValveAiAoAlarmStartIndex + 55))
            End Set
        End Property
#End Region

#End Region

#End Region

#Region "コンポジット"

        ''TableIndex
        Public Property CompositeTableIndex() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(0), udtChTypeData(1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(0), udtChTypeData(1))
            End Set
        End Property


        ''TagNo
        '' 2015.10.22 Verf1.7.5 追加
        Public Property CompositeTagNo() As String

            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(2 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(2 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''LRNo
        '' 2015.11.12 Verf1.7.8 追加
        Public Property CompositeLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(18), _
                                     udtChTypeData(19))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(18), _
                                   udtChTypeData(19))
            End Set
        End Property

        ''fire_Alm_mimic
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Public Property CompositeAlmMimic() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(20), _
                                     udtChTypeData(21))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(20), _
                                   udtChTypeData(21))
            End Set
        End Property
#End Region

#Region "パルス"

        ''Use
        Public Property PulseUse() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(0), udtChTypeData(1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(0), udtChTypeData(1))
            End Set
        End Property

        ''DiFilter
        Public Property PulseDiFilter() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(2), udtChTypeData(3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(2), udtChTypeData(3))
            End Set
        End Property

        ''Value
        Public Property PulseValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(4), udtChTypeData(5), udtChTypeData(6), udtChTypeData(7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, udtChTypeData(4), udtChTypeData(5), udtChTypeData(6), udtChTypeData(7))
            End Set
        End Property

        ''String
        Public Property PulseString() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(8), udtChTypeData(9))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(8), udtChTypeData(9))
            End Set
        End Property

        ''DecPoint
        Public Property PulseDecPoint() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(10), udtChTypeData(11))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(10), udtChTypeData(11))
            End Set
        End Property

        ''TagNo
        '' 2015.10.22 Verf1.7.5 追加
        Public Property PulseTagNo() As String

            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(12 + j) = bytArray(j)
                Next j

            End Set
        End Property


        ''LRNo
        '' 2015.11.12 Verf1.7.8 追加
        Public Property PulseLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(28), _
                                     udtChTypeData(29))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(28), _
                                   udtChTypeData(29))
            End Set
        End Property

        ''fire_Alm_mimic
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Public Property PulseAlmMimic() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(30), _
                                     udtChTypeData(31))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(30), _
                                   udtChTypeData(31))
            End Set
        End Property
#End Region

#Region "積算"

        ''Use
        Public Property RevoUse() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(0), udtChTypeData(1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(0), udtChTypeData(1))
            End Set
        End Property

        ''DiFilter
        Public Property RevoDiFilter() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(2), udtChTypeData(3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(2), udtChTypeData(3))
            End Set
        End Property

        ''Value
        Public Property RevoValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(4), udtChTypeData(5), udtChTypeData(6), udtChTypeData(7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, udtChTypeData(4), udtChTypeData(5), udtChTypeData(6), udtChTypeData(7))
            End Set
        End Property

        ''TrigerSysno
        Public Property RevoTrigerSysno() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(8), udtChTypeData(9))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(8), udtChTypeData(9))
            End Set
        End Property

        ''TrigerChid
        Public Property RevoTrigerChid() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(10), udtChTypeData(11))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(10), udtChTypeData(11))
            End Set
        End Property

        ''String
        Public Property RevoString() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(12), udtChTypeData(13))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(12), udtChTypeData(13))
            End Set
        End Property

        ''DecPoint
        Public Property RevoDecPoint() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(14), udtChTypeData(15))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(14), udtChTypeData(15))
            End Set
        End Property

        ''TagNo
        '' 2015.10.22 Verf1.7.5 追加
        Public Property RevoTagNo() As String

            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(16 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(16 + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''LRNo
        '' 2015.11.12 Verf1.7.8 追加
        Public Property RevoLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(32), _
                                     udtChTypeData(33))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(32), _
                                   udtChTypeData(33))
            End Set
        End Property

        ''fire_Alm_mimic
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Public Property RevoAlmMimic() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(34), _
                                     udtChTypeData(35))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(34), _
                                   udtChTypeData(35))
            End Set
        End Property
#End Region

#Region "システム"

        ''Use
        Public Property SystemUse() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(0), udtChTypeData(1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(0), udtChTypeData(1))
            End Set
        End Property

        ''Spare
        Public Property SystemSpare() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(2), udtChTypeData(3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(2), udtChTypeData(3))
            End Set
        End Property

#Region "Info01"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex01 As Integer = 4

        ''InfoStatusUse01
        Public Property SystemInfoStatusUse01() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex01), udtChTypeData(mCstSystemInfoStartIndex01 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex01), udtChTypeData(mCstSystemInfoStartIndex01 + 1))
            End Set
        End Property

        ''InfoKikiCode01
        Public Property SystemInfoKikiCode01() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex01 + 2), udtChTypeData(mCstSystemInfoStartIndex01 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex01 + 2), udtChTypeData(mCstSystemInfoStartIndex01 + 3))
            End Set
        End Property

        ''InfoStatusName01
        Public Property SystemInfoStatusName01() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex01 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex01 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info02"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex02 As Integer = 16

        ''InfoStatusUse02
        Public Property SystemInfoStatusUse02() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex02), udtChTypeData(mCstSystemInfoStartIndex02 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex02), udtChTypeData(mCstSystemInfoStartIndex02 + 1))
            End Set
        End Property

        ''InfoKikiCode02
        Public Property SystemInfoKikiCode02() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex02 + 2), udtChTypeData(mCstSystemInfoStartIndex02 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex02 + 2), udtChTypeData(mCstSystemInfoStartIndex02 + 3))
            End Set
        End Property

        ''InfoStatusName02
        Public Property SystemInfoStatusName02() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex02 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex02 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info03"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex03 As Integer = 28

        ''InfoStatusUse03
        Public Property SystemInfoStatusUse03() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex03), udtChTypeData(mCstSystemInfoStartIndex03 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex03), udtChTypeData(mCstSystemInfoStartIndex03 + 1))
            End Set
        End Property

        ''InfoKikiCode03
        Public Property SystemInfoKikiCode03() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex03 + 2), udtChTypeData(mCstSystemInfoStartIndex03 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex03 + 2), udtChTypeData(mCstSystemInfoStartIndex03 + 3))
            End Set
        End Property

        ''InfoStatusName03
        Public Property SystemInfoStatusName03() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex03 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex03 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info04"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex04 As Integer = 40

        ''InfoStatusUse04
        Public Property SystemInfoStatusUse04() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex04), udtChTypeData(mCstSystemInfoStartIndex04 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex04), udtChTypeData(mCstSystemInfoStartIndex04 + 1))
            End Set
        End Property

        ''InfoKikiCode04
        Public Property SystemInfoKikiCode04() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex04 + 2), udtChTypeData(mCstSystemInfoStartIndex04 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex04 + 2), udtChTypeData(mCstSystemInfoStartIndex04 + 3))
            End Set
        End Property

        ''InfoStatusName04
        Public Property SystemInfoStatusName04() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex04 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex04 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info05"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex05 As Integer = 52

        ''InfoStatusUse05
        Public Property SystemInfoStatusUse05() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex05), udtChTypeData(mCstSystemInfoStartIndex05 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex05), udtChTypeData(mCstSystemInfoStartIndex05 + 1))
            End Set
        End Property

        ''InfoKikiCode05
        Public Property SystemInfoKikiCode05() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex05 + 2), udtChTypeData(mCstSystemInfoStartIndex05 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex05 + 2), udtChTypeData(mCstSystemInfoStartIndex05 + 3))
            End Set
        End Property

        ''InfoStatusName05
        Public Property SystemInfoStatusName05() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex05 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex05 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info06"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex06 As Integer = 64

        ''InfoStatusUse06
        Public Property SystemInfoStatusUse06() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex06), udtChTypeData(mCstSystemInfoStartIndex06 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex06), udtChTypeData(mCstSystemInfoStartIndex06 + 1))
            End Set
        End Property

        ''InfoKikiCode06
        Public Property SystemInfoKikiCode06() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex06 + 2), udtChTypeData(mCstSystemInfoStartIndex06 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex06 + 2), udtChTypeData(mCstSystemInfoStartIndex06 + 3))
            End Set
        End Property

        ''InfoStatusName06
        Public Property SystemInfoStatusName06() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex06 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex06 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info07"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex07 As Integer = 76

        ''InfoStatusUse07
        Public Property SystemInfoStatusUse07() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex07), udtChTypeData(mCstSystemInfoStartIndex07 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex07), udtChTypeData(mCstSystemInfoStartIndex07 + 1))
            End Set
        End Property

        ''InfoKikiCod0e7
        Public Property SystemInfoKikiCode07() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex07 + 2), udtChTypeData(mCstSystemInfoStartIndex07 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex07 + 2), udtChTypeData(mCstSystemInfoStartIndex07 + 3))
            End Set
        End Property

        ''InfoStatusName07
        Public Property SystemInfoStatusName07() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex07 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex07 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info08"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex08 As Integer = 88

        ''InfoStatusUse08
        Public Property SystemInfoStatusUse08() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex08), udtChTypeData(mCstSystemInfoStartIndex08 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex08), udtChTypeData(mCstSystemInfoStartIndex08 + 1))
            End Set
        End Property

        ''InfoKikiCode08
        Public Property SystemInfoKikiCode08() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex08 + 2), udtChTypeData(mCstSystemInfoStartIndex08 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex08 + 2), udtChTypeData(mCstSystemInfoStartIndex08 + 3))
            End Set
        End Property

        ''InfoStatusName08
        Public Property SystemInfoStatusName08() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex08 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex08 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info09"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex09 As Integer = 100

        ''InfoStatusUse09
        Public Property SystemInfoStatusUse09() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex09), udtChTypeData(mCstSystemInfoStartIndex09 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex09), udtChTypeData(mCstSystemInfoStartIndex09 + 1))
            End Set
        End Property

        ''InfoKikiCode09
        Public Property SystemInfoKikiCode09() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex09 + 2), udtChTypeData(mCstSystemInfoStartIndex09 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex09 + 2), udtChTypeData(mCstSystemInfoStartIndex09 + 3))
            End Set
        End Property

        ''InfoStatusName09
        Public Property SystemInfoStatusName09() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex09 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex09 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info10"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex10 As Integer = 112

        ''InfoStatusUse10
        Public Property SystemInfoStatusUse10() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex10), udtChTypeData(mCstSystemInfoStartIndex10 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex10), udtChTypeData(mCstSystemInfoStartIndex10 + 1))
            End Set
        End Property

        ''InfoKikiCode10
        Public Property SystemInfoKikiCode10() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex10 + 2), udtChTypeData(mCstSystemInfoStartIndex10 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex10 + 2), udtChTypeData(mCstSystemInfoStartIndex10 + 3))
            End Set
        End Property

        ''InfoStatusName10
        Public Property SystemInfoStatusName10() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex10 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex10 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info11"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex11 As Integer = 124

        ''InfoStatusUse11
        Public Property SystemInfoStatusUse11() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex11), udtChTypeData(mCstSystemInfoStartIndex11 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex11), udtChTypeData(mCstSystemInfoStartIndex11 + 1))
            End Set
        End Property

        ''InfoKikiCode11
        Public Property SystemInfoKikiCode11() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex11 + 2), udtChTypeData(mCstSystemInfoStartIndex11 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex11 + 2), udtChTypeData(mCstSystemInfoStartIndex11 + 3))
            End Set
        End Property

        ''InfoStatusName11
        Public Property SystemInfoStatusName11() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex11 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex11 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info12"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex12 As Integer = 136

        ''InfoStatusUse12
        Public Property SystemInfoStatusUse12() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex12), udtChTypeData(mCstSystemInfoStartIndex12 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex12), udtChTypeData(mCstSystemInfoStartIndex12 + 1))
            End Set
        End Property

        ''InfoKikiCode12
        Public Property SystemInfoKikiCode12() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex12 + 2), udtChTypeData(mCstSystemInfoStartIndex12 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex12 + 2), udtChTypeData(mCstSystemInfoStartIndex12 + 3))
            End Set
        End Property

        ''InfoStatusName12
        Public Property SystemInfoStatusName12() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex12 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex12 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info13"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex13 As Integer = 148

        ''InfoStatusUse13
        Public Property SystemInfoStatusUse13() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex13), udtChTypeData(mCstSystemInfoStartIndex13 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex13), udtChTypeData(mCstSystemInfoStartIndex13 + 1))
            End Set
        End Property

        ''InfoKikiCode13
        Public Property SystemInfoKikiCode13() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex13 + 2), udtChTypeData(mCstSystemInfoStartIndex13 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex13 + 2), udtChTypeData(mCstSystemInfoStartIndex13 + 3))
            End Set
        End Property

        ''InfoStatusName13
        Public Property SystemInfoStatusName13() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex13 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex13 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info14"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex14 As Integer = 160

        ''InfoStatusUse14
        Public Property SystemInfoStatusUse14() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex14), udtChTypeData(mCstSystemInfoStartIndex14 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex14), udtChTypeData(mCstSystemInfoStartIndex14 + 1))
            End Set
        End Property

        ''InfoKikiCode14
        Public Property SystemInfoKikiCode14() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex14 + 2), udtChTypeData(mCstSystemInfoStartIndex14 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex14 + 2), udtChTypeData(mCstSystemInfoStartIndex14 + 3))
            End Set
        End Property

        ''InfoStatusName14
        Public Property SystemInfoStatusName14() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex14 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex14 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info15"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex15 As Integer = 172

        ''InfoStatusUse15
        Public Property SystemInfoStatusUse15() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex15), udtChTypeData(mCstSystemInfoStartIndex15 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex15), udtChTypeData(mCstSystemInfoStartIndex15 + 1))
            End Set
        End Property

        ''InfoKikiCode15
        Public Property SystemInfoKikiCode15() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex15 + 2), udtChTypeData(mCstSystemInfoStartIndex15 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex15 + 2), udtChTypeData(mCstSystemInfoStartIndex15 + 3))
            End Set
        End Property

        ''InfoStatusName15
        Public Property SystemInfoStatusName15() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex15 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex15 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region
#Region "Info16"

        ''開始配列番号
        Private Const mCstSystemInfoStartIndex16 As Integer = 184

        ''InfoStatusUse16
        Public Property SystemInfoStatusUse16() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex16), udtChTypeData(mCstSystemInfoStartIndex16 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex16), udtChTypeData(mCstSystemInfoStartIndex16 + 1))
            End Set
        End Property

        ''InfoKikiCode16
        Public Property SystemInfoKikiCode16() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemInfoStartIndex16 + 2), udtChTypeData(mCstSystemInfoStartIndex16 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, udtChTypeData(mCstSystemInfoStartIndex16 + 2), udtChTypeData(mCstSystemInfoStartIndex16 + 3))
            End Set
        End Property

        ''InfoStatusName16
        Public Property SystemInfoStatusName16() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData((mCstSystemInfoStartIndex16 + 4) + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData((mCstSystemInfoStartIndex16 + 4) + j) = bytArray(j)
                Next j

            End Set
        End Property

#End Region


#Region "その他"

        ''開始配列番号    2015.10.22 Verf1.7.5 追加
        Private Const mCstSystemTagStartIndex As Integer = 196

        ''TagNo
        '' 2015.10.22 Verf1.7.5 追加
        Public Property SystemTagNo() As String

            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstSystemTagStartIndex + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstSystemTagStartIndex + j) = bytArray(j)
                Next j

            End Set
        End Property

        ''LRNo
        '' 2015.11.12 Verf1.7.8 追加
        Public Property SystemLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemTagStartIndex + 16), _
                                     udtChTypeData(mCstSystemTagStartIndex + 17))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstSystemTagStartIndex + 16), _
                                   udtChTypeData(mCstSystemTagStartIndex + 17))
            End Set
        End Property

        ''fire_Alm_mimic
        'Ver2.0.0.2 南日本M761対応 2017.02.27追加
        Public Property SystemAlmMimic() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstSystemTagStartIndex + 18), _
                                     udtChTypeData(mCstSystemTagStartIndex + 19))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstSystemTagStartIndex + 18), _
                                   udtChTypeData(mCstSystemTagStartIndex + 19))
            End Set
        End Property
#End Region

#End Region

#Region "PID"

#Region "アラーム情報"

#Region " HiHi "

        Private Const mCstPIDStartIndexHiHi As Integer = 0

        'Use
        Public Property PidHiHiUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexHiHi), udtChTypeData(mCstPIDStartIndexHiHi + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexHiHi), udtChTypeData(mCstPIDStartIndexHiHi + 1))
            End Set
        End Property

        'Delay
        Public Property PidHiHiDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexHiHi + 2), udtChTypeData(mCstPIDStartIndexHiHi + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexHiHi + 2), udtChTypeData(mCstPIDStartIndexHiHi + 3))
            End Set
        End Property

        'Value
        Public Property PidHiHiValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPIDStartIndexHiHi + 4), _
                                     udtChTypeData(mCstPIDStartIndexHiHi + 5), _
                                     udtChTypeData(mCstPIDStartIndexHiHi + 6), _
                                     udtChTypeData(mCstPIDStartIndexHiHi + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPIDStartIndexHiHi + 4), _
                                   udtChTypeData(mCstPIDStartIndexHiHi + 5), _
                                   udtChTypeData(mCstPIDStartIndexHiHi + 6), _
                                   udtChTypeData(mCstPIDStartIndexHiHi + 7))
            End Set
        End Property

        'ExtGroup
        Public Property PidHiHiExtGroup() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexHiHi + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexHiHi + 8) = value
            End Set
        End Property

        'GroupRepose1
        Public Property PidHiHiGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexHiHi + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexHiHi + 9) = value
            End Set
        End Property

        'GroupRepose2
        Public Property PidHiHiGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexHiHi + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexHiHi + 10) = value
            End Set
        End Property

        'Spare
        Public Property PidHiHiSpare() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexHiHi + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexHiHi + 11) = value
            End Set
        End Property

        'StatusInput
        Public Property PidHiHiStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPIDStartIndexHiHi + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPIDStartIndexHiHi + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        'ManualRepose
        Public Property PidHiHiManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexHiHi + 20), udtChTypeData(mCstPIDStartIndexHiHi + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexHiHi + 20), udtChTypeData(mCstPIDStartIndexHiHi + 21))
            End Set
        End Property

        'ManualReposeSet
        Public Property PidHiHiManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexHiHi + 22), udtChTypeData(mCstPIDStartIndexHiHi + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexHiHi + 22), udtChTypeData(mCstPIDStartIndexHiHi + 23))
            End Set
        End Property

#End Region

#Region " Hi "
        Private Const mCstPIDStartIndexHi As Integer = 24

        'Use
        Public Property PidHiUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexHi), udtChTypeData(mCstPIDStartIndexHi + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexHi), udtChTypeData(mCstPIDStartIndexHi + 1))
            End Set
        End Property

        'Delay
        Public Property PidHiDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexHi + 2), udtChTypeData(mCstPIDStartIndexHi + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexHi + 2), udtChTypeData(mCstPIDStartIndexHi + 3))
            End Set
        End Property

        'Value
        Public Property PidHiValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPIDStartIndexHi + 4), _
                                     udtChTypeData(mCstPIDStartIndexHi + 5), _
                                     udtChTypeData(mCstPIDStartIndexHi + 6), _
                                     udtChTypeData(mCstPIDStartIndexHi + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPIDStartIndexHi + 4), _
                                   udtChTypeData(mCstPIDStartIndexHi + 5), _
                                   udtChTypeData(mCstPIDStartIndexHi + 6), _
                                   udtChTypeData(mCstPIDStartIndexHi + 7))
            End Set
        End Property

        'ExtGroup
        Public Property PidHiExtGroup() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexHi + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexHi + 8) = value
            End Set
        End Property

        'GroupRepose1
        Public Property PidHiGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexHi + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexHi + 9) = value
            End Set
        End Property

        'GroupRepose2
        Public Property PidHiGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexHi + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexHi + 10) = value
            End Set
        End Property

        'Spare
        Public Property PidHiSpare() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexHi + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexHi + 11) = value
            End Set
        End Property

        'StatusInput
        Public Property PidHiStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPIDStartIndexHi + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPIDStartIndexHi + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        'ManualRepose
        Public Property PidHiManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexHi + 20), udtChTypeData(mCstPIDStartIndexHi + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexHi + 20), udtChTypeData(mCstPIDStartIndexHi + 21))
            End Set
        End Property

        'ManualReposeSet
        Public Property PidHiManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexHi + 22), udtChTypeData(mCstPIDStartIndexHi + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexHi + 22), udtChTypeData(mCstPIDStartIndexHi + 23))
            End Set
        End Property

#End Region

#Region " Lo "
        Private Const mCstPIDStartIndexLo As Integer = 48

        'Use
        Public Property PidLoUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexLo), udtChTypeData(mCstPIDStartIndexLo + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexLo), udtChTypeData(mCstPIDStartIndexLo + 1))
            End Set
        End Property

        'Delay
        Public Property PidLoDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexLo + 2), udtChTypeData(mCstPIDStartIndexLo + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexLo + 2), udtChTypeData(mCstPIDStartIndexLo + 3))
            End Set
        End Property

        'Value
        Public Property PidLoValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPIDStartIndexLo + 4), _
                                     udtChTypeData(mCstPIDStartIndexLo + 5), _
                                     udtChTypeData(mCstPIDStartIndexLo + 6), _
                                     udtChTypeData(mCstPIDStartIndexLo + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPIDStartIndexLo + 4), _
                                   udtChTypeData(mCstPIDStartIndexLo + 5), _
                                   udtChTypeData(mCstPIDStartIndexLo + 6), _
                                   udtChTypeData(mCstPIDStartIndexLo + 7))
            End Set
        End Property

        'ExtGroup
        Public Property PidLoExtGroup() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexLo + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexLo + 8) = value
            End Set
        End Property

        'GroupRepose1
        Public Property PidLoGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexLo + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexLo + 9) = value
            End Set
        End Property

        'GroupRepose2
        Public Property PidLoGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexLo + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexLo + 10) = value
            End Set
        End Property

        'Spare
        Public Property PidLoSpare() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexLo + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexLo + 11) = value
            End Set
        End Property

        'StatusInput
        Public Property PidLoStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPIDStartIndexLo + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPIDStartIndexLo + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        'ManualRepose
        Public Property PidLoManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexLo + 20), udtChTypeData(mCstPIDStartIndexLo + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexLo + 20), udtChTypeData(mCstPIDStartIndexLo + 21))
            End Set
        End Property

        'ManualReposeSet
        Public Property PidLoManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexLo + 22), udtChTypeData(mCstPIDStartIndexLo + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexLo + 22), udtChTypeData(mCstPIDStartIndexLo + 23))
            End Set
        End Property

#End Region

#Region " LoLo "

        Private Const mCstPIDStartIndexLoLo As Integer = 72

        'Use
        Public Property PidLoLoUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexLoLo), udtChTypeData(mCstPIDStartIndexLoLo + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexLoLo), udtChTypeData(mCstPIDStartIndexLoLo + 1))
            End Set
        End Property

        'Delay
        Public Property PidLoLoDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexLoLo + 2), udtChTypeData(mCstPIDStartIndexLoLo + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexLoLo + 2), udtChTypeData(mCstPIDStartIndexLoLo + 3))
            End Set
        End Property

        'Value
        Public Property PidLoLoValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPIDStartIndexLoLo + 4), _
                                     udtChTypeData(mCstPIDStartIndexLoLo + 5), _
                                     udtChTypeData(mCstPIDStartIndexLoLo + 6), _
                                     udtChTypeData(mCstPIDStartIndexLoLo + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPIDStartIndexLoLo + 4), _
                                   udtChTypeData(mCstPIDStartIndexLoLo + 5), _
                                   udtChTypeData(mCstPIDStartIndexLoLo + 6), _
                                   udtChTypeData(mCstPIDStartIndexLoLo + 7))
            End Set
        End Property

        'ExtGroup
        Public Property PidLoLoExtGroup() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexLoLo + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexLoLo + 8) = value
            End Set
        End Property

        'GroupRepose1
        Public Property PidLoLoGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexLoLo + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexLoLo + 9) = value
            End Set
        End Property

        'GroupRepose2
        Public Property PidLoLoGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexLoLo + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexLoLo + 10) = value
            End Set
        End Property

        'Spare
        Public Property PidLoLoSpare() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexLoLo + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexLoLo + 11) = value
            End Set
        End Property

        'StatusInput
        Public Property PidLoLoStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPIDStartIndexLoLo + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPIDStartIndexLoLo + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        'ManualRepose
        Public Property PidLoLoManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexLoLo + 20), udtChTypeData(mCstPIDStartIndexLoLo + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexLoLo + 20), udtChTypeData(mCstPIDStartIndexLoLo + 21))
            End Set
        End Property

        'ManualReposeSet
        Public Property PidLoLoManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexLoLo + 22), udtChTypeData(mCstPIDStartIndexLoLo + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexLoLo + 22), udtChTypeData(mCstPIDStartIndexLoLo + 23))
            End Set
        End Property

#End Region

#Region " SensorFail "

        Private Const mCstPIDStartIndexSensorFail As Integer = 96

        'Use
        Public Property PidSensorFailUse() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexSensorFail), udtChTypeData(mCstPIDStartIndexSensorFail + 1))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexSensorFail), udtChTypeData(mCstPIDStartIndexSensorFail + 1))
            End Set
        End Property

        'Delay
        Public Property PidSensorFailDelay() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexSensorFail + 2), udtChTypeData(mCstPIDStartIndexSensorFail + 3))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexSensorFail + 2), udtChTypeData(mCstPIDStartIndexSensorFail + 3))
            End Set
        End Property

        'Value
        Public Property PidSensorFailValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPIDStartIndexSensorFail + 4), _
                                     udtChTypeData(mCstPIDStartIndexSensorFail + 5), _
                                     udtChTypeData(mCstPIDStartIndexSensorFail + 6), _
                                     udtChTypeData(mCstPIDStartIndexSensorFail + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPIDStartIndexSensorFail + 4), _
                                   udtChTypeData(mCstPIDStartIndexSensorFail + 5), _
                                   udtChTypeData(mCstPIDStartIndexSensorFail + 6), _
                                   udtChTypeData(mCstPIDStartIndexSensorFail + 7))
            End Set
        End Property

        'ExtGroup
        Public Property PidSensorFailExtGroup() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexSensorFail + 8)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexSensorFail + 8) = value
            End Set
        End Property

        'GroupRepose1
        Public Property PidSensorFailGroupRepose1() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexSensorFail + 9)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexSensorFail + 9) = value
            End Set
        End Property

        'GroupRepose2
        Public Property PidSensorFailGroupRepose2() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexSensorFail + 10)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexSensorFail + 10) = value
            End Set
        End Property

        'Spare
        Public Property PidSensorFailSpare() As Byte
            Get
                Return udtChTypeData(mCstPIDStartIndexSensorFail + 11)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPIDStartIndexSensorFail + 11) = value
            End Set
        End Property

        'StatusInput
        Public Property PidSensorFailStatusInput() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPIDStartIndexSensorFail + 12 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPIDStartIndexSensorFail + 12 + j) = bytArray(j)
                Next j

            End Set
        End Property

        'ManualRepose
        Public Property PidSensorFailManualReposeState() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexSensorFail + 20), udtChTypeData(mCstPIDStartIndexSensorFail + 21))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexSensorFail + 20), udtChTypeData(mCstPIDStartIndexSensorFail + 21))
            End Set
        End Property

        'ManualReposeSet
        Public Property PidSensorFailManualReposeSet() As Short
            Get
                Return gConnect2Byte(udtChTypeData(mCstPIDStartIndexSensorFail + 22), udtChTypeData(mCstPIDStartIndexSensorFail + 23))
            End Get
            Set(ByVal value As Short)
                Call gSeparat2Byte(value, udtChTypeData(mCstPIDStartIndexSensorFail + 22), udtChTypeData(mCstPIDStartIndexSensorFail + 23))
            End Set
        End Property

#End Region

#End Region

#Region "項目"

        Private Const mCstPidStartIndexItem As Integer = 120

        'RangeHigh(スケール値)
        Public Property PidRangeHigh() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPidStartIndexItem + 0), _
                                     udtChTypeData(mCstPidStartIndexItem + 1), _
                                     udtChTypeData(mCstPidStartIndexItem + 2), _
                                     udtChTypeData(mCstPidStartIndexItem + 3))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 0), _
                                   udtChTypeData(mCstPidStartIndexItem + 1), _
                                   udtChTypeData(mCstPidStartIndexItem + 2), _
                                   udtChTypeData(mCstPidStartIndexItem + 3))
            End Set
        End Property

        'RangeLow(スケール値)
        Public Property PidRangeLow() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPidStartIndexItem + 4), _
                                     udtChTypeData(mCstPidStartIndexItem + 5), _
                                     udtChTypeData(mCstPidStartIndexItem + 6), _
                                     udtChTypeData(mCstPidStartIndexItem + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 4), _
                                   udtChTypeData(mCstPidStartIndexItem + 5), _
                                   udtChTypeData(mCstPidStartIndexItem + 6), _
                                   udtChTypeData(mCstPidStartIndexItem + 7))
            End Set
        End Property

        'NormalHigh(ノーマルレンジ)
        Public Property PidNormalHigh() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPidStartIndexItem + 8), _
                                     udtChTypeData(mCstPidStartIndexItem + 9), _
                                     udtChTypeData(mCstPidStartIndexItem + 10), _
                                     udtChTypeData(mCstPidStartIndexItem + 11))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 8), _
                                   udtChTypeData(mCstPidStartIndexItem + 9), _
                                   udtChTypeData(mCstPidStartIndexItem + 10), _
                                   udtChTypeData(mCstPidStartIndexItem + 11))
            End Set
        End Property

        'NormalLow(ノーマルレンジ)
        Public Property PidNormalLow() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPidStartIndexItem + 12), _
                                     udtChTypeData(mCstPidStartIndexItem + 13), _
                                     udtChTypeData(mCstPidStartIndexItem + 14), _
                                     udtChTypeData(mCstPidStartIndexItem + 15))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 12), _
                                   udtChTypeData(mCstPidStartIndexItem + 13), _
                                   udtChTypeData(mCstPidStartIndexItem + 14), _
                                   udtChTypeData(mCstPidStartIndexItem + 15))
            End Set
        End Property

        'OffsetValue
        Public Property PidOffsetValue() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPidStartIndexItem + 16), _
                                     udtChTypeData(mCstPidStartIndexItem + 17), _
                                     udtChTypeData(mCstPidStartIndexItem + 18), _
                                     udtChTypeData(mCstPidStartIndexItem + 19))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 16), _
                                   udtChTypeData(mCstPidStartIndexItem + 17), _
                                   udtChTypeData(mCstPidStartIndexItem + 18), _
                                   udtChTypeData(mCstPidStartIndexItem + 19))
            End Set
        End Property

        'String(display1)
        Public Property PidString() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 20), _
                                     udtChTypeData(mCstPidStartIndexItem + 21))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 20), _
                                   udtChTypeData(mCstPidStartIndexItem + 21))
            End Set
        End Property

        'DecimalPosition(display2)
        Public Property PidDecimalPosition() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 22), _
                                     udtChTypeData(mCstPidStartIndexItem + 23))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 22), _
                                   udtChTypeData(mCstPidStartIndexItem + 23))
            End Set
        End Property

        'Display3
        Public Property PidDisplay3() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 24), _
                                     udtChTypeData(mCstPidStartIndexItem + 25))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 24), _
                                   udtChTypeData(mCstPidStartIndexItem + 25))
            End Set
        End Property

        '外部FuNo
        Public Property PidOutFuNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 26), _
                                     udtChTypeData(mCstPidStartIndexItem + 27))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 26), _
                                   udtChTypeData(mCstPidStartIndexItem + 27))
            End Set
        End Property

        '外部PortNo
        Public Property PidOutPortNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 28), _
                                     udtChTypeData(mCstPidStartIndexItem + 29))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 28), _
                                   udtChTypeData(mCstPidStartIndexItem + 29))
            End Set
        End Property

        '外部Pin
        Public Property PidOutPin() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 30), _
                                     udtChTypeData(mCstPidStartIndexItem + 31))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 30), _
                                   udtChTypeData(mCstPidStartIndexItem + 31))
            End Set
        End Property

        'PinNo(出力点数)
        Public Property PidOutPinNo() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 32), _
                                     udtChTypeData(mCstPidStartIndexItem + 33))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 32), _
                                   udtChTypeData(mCstPidStartIndexItem + 33))
            End Set
        End Property

        'RangeType
        Public Property PidRangeType() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 34), _
                                     udtChTypeData(mCstPidStartIndexItem + 35))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 34), _
                                   udtChTypeData(mCstPidStartIndexItem + 35))
            End Set
        End Property

        'TagNo
        Public Property PidTagNo() As String
            Get
                Dim strRtn As String
                Dim bytArray() As Byte
                Dim tag_size As Integer

                tag_size = GetTagSize()
                ReDim bytArray(tag_size - 1)


                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPidStartIndexItem + 36 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte
                Dim tag_size As Integer
                Dim strSpace As String

                tag_size = GetTagSize()
                strSpace = ""
                For j = 0 To tag_size - 1
                    strSpace = strSpace & " "
                Next
                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & strSpace, tag_size))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPidStartIndexItem + 36 + j) = bytArray(j)
                Next j

            End Set
        End Property

        'LRNo
        Public Property PidLRMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 52), _
                                     udtChTypeData(mCstPidStartIndexItem + 53))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 52), _
                                   udtChTypeData(mCstPidStartIndexItem + 53))
            End Set
        End Property

        'out_mode
        Public Property PidOutMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 54), _
                                     udtChTypeData(mCstPidStartIndexItem + 55))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 54), _
                                   udtChTypeData(mCstPidStartIndexItem + 55))
            End Set
        End Property

        'cas_mode
        Public Property PidCasMode() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 56), _
                                     udtChTypeData(mCstPidStartIndexItem + 57))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 56), _
                                   udtChTypeData(mCstPidStartIndexItem + 57))
            End Set
        End Property

        'sp_tracking
        Public Property PidSpTracking() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexItem + 58), _
                                     udtChTypeData(mCstPidStartIndexItem + 59))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexItem + 58), _
                                   udtChTypeData(mCstPidStartIndexItem + 59))
            End Set
        End Property

        'Spare1
        Public Property PidItemSp1() As Byte
            Get
                Return udtChTypeData(mCstPidStartIndexItem + 60)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPidStartIndexItem + 60) = value
            End Set
        End Property

        'Spare2
        Public Property PidItemSp2() As Byte
            Get
                Return udtChTypeData(mCstPidStartIndexItem + 61)
            End Get
            Set(ByVal value As Byte)
                udtChTypeData(mCstPidStartIndexItem + 61) = value
            End Set
        End Property
#End Region

#Region "PID項目"

#Region "PID制御情報：標準パラメータ"
        Private Const mCstPidStartIndexPID As Integer = 182
        'セットポイント設定上限(sp_high)
        Public Property PidDefSpHigh() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPidStartIndexPID + 0), _
                                     udtChTypeData(mCstPidStartIndexPID + 1), _
                                     udtChTypeData(mCstPidStartIndexPID + 2), _
                                     udtChTypeData(mCstPidStartIndexPID + 3))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPID + 0), _
                                   udtChTypeData(mCstPidStartIndexPID + 1), _
                                   udtChTypeData(mCstPidStartIndexPID + 2), _
                                   udtChTypeData(mCstPidStartIndexPID + 3))
            End Set
        End Property
        'セットポイント設定下限(sp_low)
        Public Property PidDefSpLow() As Integer
            Get
                Return gConnect4Byte(udtChTypeData(mCstPidStartIndexPID + 4), _
                                     udtChTypeData(mCstPidStartIndexPID + 5), _
                                     udtChTypeData(mCstPidStartIndexPID + 6), _
                                     udtChTypeData(mCstPidStartIndexPID + 7))
            End Get
            Set(ByVal value As Integer)
                Call gSeparat4Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPID + 4), _
                                   udtChTypeData(mCstPidStartIndexPID + 5), _
                                   udtChTypeData(mCstPidStartIndexPID + 6), _
                                   udtChTypeData(mCstPidStartIndexPID + 7))
            End Set
        End Property
        '制御出力MV設定上限(mv_high)
        Public Property PidDefMvHigh() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPID + 8), _
                                     udtChTypeData(mCstPidStartIndexPID + 9))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPID + 8), _
                                   udtChTypeData(mCstPidStartIndexPID + 9))
            End Set
        End Property
        '制御出力MV設定下限(mv_low)
        Public Property PidDefMvLow() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPID + 10), _
                                     udtChTypeData(mCstPidStartIndexPID + 11))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPID + 10), _
                                   udtChTypeData(mCstPidStartIndexPID + 11))
            End Set
        End Property
        '比例帯(pb)
        Public Property PidDefPB() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPID + 12), _
                                     udtChTypeData(mCstPidStartIndexPID + 13))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPID + 12), _
                                   udtChTypeData(mCstPidStartIndexPID + 13))
            End Set
        End Property
        '積分時間(ti)
        Public Property PidDefTI() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPID + 14), _
                                     udtChTypeData(mCstPidStartIndexPID + 15))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPID + 14), _
                                   udtChTypeData(mCstPidStartIndexPID + 15))
            End Set
        End Property
        '微分時間(td)
        Public Property PidDefTD() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPID + 16), _
                                     udtChTypeData(mCstPidStartIndexPID + 17))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPID + 16), _
                                   udtChTypeData(mCstPidStartIndexPID + 17))
            End Set
        End Property
        'ギャップ(gap)
        Public Property PidDefGAP() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPID + 18), _
                                     udtChTypeData(mCstPidStartIndexPID + 19))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPID + 18), _
                                   udtChTypeData(mCstPidStartIndexPID + 19))
            End Set
        End Property
        'Spare
        Public Property PidDefSpare() As String
            Get
                Dim strRtn As String
                Dim bytArray(3) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPidStartIndexPID + 20 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "    ", 4))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPidStartIndexPID + 20 + j) = bytArray(j)
                Next j

            End Set
        End Property
#End Region

#Region "PID制御情報：拡張パラメータ1"
        Private Const mCstPidStartIndexPIDext1 As Integer = 206
        'パラメータ(para)
        Public Property PidExtPara1() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext1 + 0), _
                                     udtChTypeData(mCstPidStartIndexPIDext1 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext1 + 0), _
                                   udtChTypeData(mCstPidStartIndexPIDext1 + 1))
            End Set
        End Property
        'パラメータ設定上限(para_high)
        Public Property PidExtParaHigh1() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext1 + 2), _
                                     udtChTypeData(mCstPidStartIndexPIDext1 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext1 + 2), _
                                   udtChTypeData(mCstPidStartIndexPIDext1 + 3))
            End Set
        End Property
        'パラメータ設定下限(para_low)
        Public Property PidExtParaLow1() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext1 + 4), _
                                     udtChTypeData(mCstPidStartIndexPIDext1 + 5))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext1 + 4), _
                                   udtChTypeData(mCstPidStartIndexPIDext1 + 5))
            End Set
        End Property
        'パラメータ名称(para_name)
        Public Property PidExtParaName1() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPidStartIndexPIDext1 + 6 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPidStartIndexPIDext1 + 6 + j) = bytArray(j)
                Next j

            End Set
        End Property
        'パラメータ単位(unit)
        Public Property PidExtParaUnit1() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPidStartIndexPIDext1 + 14 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPidStartIndexPIDext1 + 14 + j) = bytArray(j)
                Next j

            End Set
        End Property
        '予備(spare)
        Public Property PidExtSpare1() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext1 + 22), _
                                     udtChTypeData(mCstPidStartIndexPIDext1 + 23))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext1 + 22), _
                                   udtChTypeData(mCstPidStartIndexPIDext1 + 23))
            End Set
        End Property
#End Region

#Region "PID制御情報：拡張パラメータ2"
        Private Const mCstPidStartIndexPIDext2 As Integer = 230
        'パラメータ(para)
        Public Property PidExtPara2() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext2 + 0), _
                                     udtChTypeData(mCstPidStartIndexPIDext2 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext2 + 0), _
                                   udtChTypeData(mCstPidStartIndexPIDext2 + 1))
            End Set
        End Property
        'パラメータ設定上限(para_high)
        Public Property PidExtParaHigh2() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext2 + 2), _
                                     udtChTypeData(mCstPidStartIndexPIDext2 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext2 + 2), _
                                   udtChTypeData(mCstPidStartIndexPIDext2 + 3))
            End Set
        End Property
        'パラメータ設定下限(para_low)
        Public Property PidExtParaLow2() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext2 + 4), _
                                     udtChTypeData(mCstPidStartIndexPIDext2 + 5))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext2 + 4), _
                                   udtChTypeData(mCstPidStartIndexPIDext2 + 5))
            End Set
        End Property
        'パラメータ名称(para_name)
        Public Property PidExtParaName2() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPidStartIndexPIDext2 + 6 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPidStartIndexPIDext2 + 6 + j) = bytArray(j)
                Next j

            End Set
        End Property
        'パラメータ単位(unit)
        Public Property PidExtParaUnit2() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPidStartIndexPIDext2 + 14 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPidStartIndexPIDext2 + 14 + j) = bytArray(j)
                Next j

            End Set
        End Property
        '予備(spare)
        Public Property PidExtSpare2() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext2 + 22), _
                                     udtChTypeData(mCstPidStartIndexPIDext2 + 23))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext2 + 22), _
                                   udtChTypeData(mCstPidStartIndexPIDext2 + 23))
            End Set
        End Property
#End Region

#Region "PID制御情報：拡張パラメータ3"
        Private Const mCstPidStartIndexPIDext3 As Integer = 254
        'パラメータ(para)
        Public Property PidExtPara3() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext3 + 0), _
                                     udtChTypeData(mCstPidStartIndexPIDext3 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext3 + 0), _
                                   udtChTypeData(mCstPidStartIndexPIDext3 + 1))
            End Set
        End Property
        'パラメータ設定上限(para_high)
        Public Property PidExtParaHigh3() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext3 + 2), _
                                     udtChTypeData(mCstPidStartIndexPIDext3 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext3 + 2), _
                                   udtChTypeData(mCstPidStartIndexPIDext3 + 3))
            End Set
        End Property
        'パラメータ設定下限(para_low)
        Public Property PidExtParaLow3() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext3 + 4), _
                                     udtChTypeData(mCstPidStartIndexPIDext3 + 5))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext3 + 4), _
                                   udtChTypeData(mCstPidStartIndexPIDext3 + 5))
            End Set
        End Property
        'パラメータ名称(para_name)
        Public Property PidExtParaName3() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPidStartIndexPIDext3 + 6 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPidStartIndexPIDext3 + 6 + j) = bytArray(j)
                Next j

            End Set
        End Property
        'パラメータ単位(unit)
        Public Property PidExtParaUnit3() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPidStartIndexPIDext3 + 14 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPidStartIndexPIDext3 + 14 + j) = bytArray(j)
                Next j

            End Set
        End Property
        '予備(spare)
        Public Property PidExtSpare3() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext3 + 22), _
                                     udtChTypeData(mCstPidStartIndexPIDext3 + 23))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext3 + 22), _
                                   udtChTypeData(mCstPidStartIndexPIDext3 + 23))
            End Set
        End Property
#End Region

#Region "PID制御情報：拡張パラメータ4"
        Private Const mCstPidStartIndexPIDext4 As Integer = 278
        'パラメータ(para)
        Public Property PidExtPara4() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext4 + 0), _
                                     udtChTypeData(mCstPidStartIndexPIDext4 + 1))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext4 + 0), _
                                   udtChTypeData(mCstPidStartIndexPIDext4 + 1))
            End Set
        End Property
        'パラメータ設定上限(para_high)
        Public Property PidExtParaHigh4() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext4 + 2), _
                                     udtChTypeData(mCstPidStartIndexPIDext4 + 3))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext4 + 2), _
                                   udtChTypeData(mCstPidStartIndexPIDext4 + 3))
            End Set
        End Property
        'パラメータ設定下限(para_low)
        Public Property PidExtParaLow4() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext4 + 4), _
                                     udtChTypeData(mCstPidStartIndexPIDext4 + 5))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext4 + 4), _
                                   udtChTypeData(mCstPidStartIndexPIDext4 + 5))
            End Set
        End Property
        'パラメータ名称(para_name)
        Public Property PidExtParaName4() As String
            Get
                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPidStartIndexPIDext4 + 6 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn
            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPidStartIndexPIDext4 + 6 + j) = bytArray(j)
                Next j

            End Set
        End Property
        'パラメータ単位(unit)
        Public Property PidExtParaUnit4() As String
            Get

                Dim strRtn As String
                Dim bytArray(7) As Byte

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    bytArray(j) = udtChTypeData(mCstPidStartIndexPIDext4 + 14 + j)
                Next j

                strRtn = System.Text.Encoding.GetEncoding("shift_jis").GetString(bytArray)
                Return strRtn

            End Get
            Set(ByVal value As String)

                Dim bytArray() As Byte

                bytArray = System.Text.Encoding.GetEncoding("shift_jis").GetBytes(LeftB(value & "        ", 8))

                For j As Integer = LBound(bytArray) To UBound(bytArray)
                    udtChTypeData(mCstPidStartIndexPIDext4 + 14 + j) = bytArray(j)
                Next j
            End Set
        End Property
        '予備(spare)
        Public Property PidExtSpare4() As UInt16
            Get
                Return gConnect2Byte(udtChTypeData(mCstPidStartIndexPIDext4 + 22), _
                                     udtChTypeData(mCstPidStartIndexPIDext4 + 23))
            End Get
            Set(ByVal value As UInt16)
                Call gSeparat2Byte(value, _
                                   udtChTypeData(mCstPidStartIndexPIDext4 + 22), _
                                   udtChTypeData(mCstPidStartIndexPIDext4 + 23))
            End Set
        End Property
#End Region

#End Region

#End Region

#End Region

    End Structure

#Region "CH共通項目"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChRecCommon

#Region "        Dim shtChid As Short                                        ''CH ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChid As Short

#End Region
        Dim shtSpare1 As Short                                      ''予備
        Dim shtGroupNo As Short                                     ''グループＮｏ．
        Dim shtDispPos As Short                                     ''グループ内表示位置
        Dim shtSysno As Short                                       ''SYSTEM No.
#Region "        Dim shtChno As Short                                        ''CH No."

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChno() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChno), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChno = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChno As Short

#End Region
        <VBFixedStringAttribute(32)> Dim strChitem As String        ''CH アイテム名称
        <VBFixedStringAttribute(16)> Dim strRemark As String        ''備考（remark）

#Region "        Dim shtExtGroup As Short                                    ''EXT. グループ（延長警報ｸﾞﾙｰﾌﾟ)"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtExtGroup() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtExtGroup), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtExtGroup = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtExtGroup As Short

#End Region
#Region "        Dim shtDelay As Short                                       ''ディレイタイマ値（アラーム継続時間）"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtDelay() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtDelay), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtDelay = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtDelay As Short

#End Region
#Region "        Dim shtGRepose1 As Short                                    ''グループ リポーズ １"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtGRepose1() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtGRepose1), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtGRepose1 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtGRepose1 As Short

#End Region
#Region "        Dim shtGRepose2 As Short                                    ''グループ リポーズ ２"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtGRepose2() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtGRepose2), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtGRepose2 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtGRepose2 As Short

#End Region

        Dim shtM_Repose As Short                                    ''マニュアルリポーズ
        Dim shtChType As Short                                      ''CH種別
        Dim shtData As Short                                        ''データ種別コード
        Dim shtUnit As Short                                        ''単位種別コード
        Dim shtFlag1 As Short                                       ''動作設定１
        Dim shtFlag2 As Short                                       ''動作設定２
        Dim shtStatus As Short                                      ''ステータス種別コード

#Region "        Dim shtFuno As Short                                        ''FU 番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtFuno() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtFuno), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtFuno = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtFuno As Short

#End Region
#Region "        Dim shtPortno As Short                                      ''FU ポート番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtPortno() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtPortno), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtPortno = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtPortno As Short

#End Region
#Region "        Dim shtPin As Short                                         ''FU 計測点番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtPin() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtPin), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtPin = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtPin As Short

#End Region
        Dim shtPinNo As Short                                       ''計測点個数

        Dim shtEccFunc As Short                                     ''延長警報盤ECC入出力機能種別コード
        Dim shtOutPort As Short                                     ''SIOポート使用有無
        Dim shtGwsPort As Short                                     ''GWSポート使用有無 
        <VBFixedStringAttribute(8)> Dim strUnit As String           ''単位種別名称(単位種別コードが0xFFの場合)
        <VBFixedStringAttribute(16)> Dim strStatus As String        ''ステータス名称(ステータス種別コードが0xFFの場合)

        Dim shtShareType As Short                                   ''共有CHタイプ
#Region "        Dim shtShareChid As Short                                   ''共有CH番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtShareChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtShareChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtShareChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtShareChid As Short

#End Region

        Dim shtM_ReposeSet As Short                                 ''マニュアルリポーズ設定
        Dim shtSignal As Short                                      ''入力信号　ver.1.4.0 2011.07.29

    End Structure

#End Region

#End Region

#Region "出力チャンネル設定"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChOutput

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''出力チャンネル情報
        <VBFixedArray(575)> Dim udtCHOutPut() As gTypSetChOutputRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtCHOutPut(575)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChOutputRec

        Dim shtSysno As Short                                       ''SYSTEM No.
        Dim shtChid As Short                                        ''CH ID 又は 論理出力 ID
        Dim bytType As Byte                                         ''CHデータ、論理出力チャネルデータ
        Dim bytStatus As Byte                                       ''Output Movement
        Dim shtMask As Short                                        ''Output Movement マスクデータ（ビットパターン）
        Dim bytOutput As Byte                                       ''CH OUT Type Setup
        Dim bytFuno As Byte                                         ''FU 番号
        Dim bytPortno As Byte                                       ''FU ポート番号
        Dim bytPin As Byte                                          ''FU 計測点番号
        <VBFixedStringAttribute(4)> Dim strSpare As String          ''予備

    End Structure

#End Region

#Region "論理出力設定"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChAndOr

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''出力チャンネル情報
        <VBFixedArray(63)> Dim udtCHOut() As gTypSetChAndOrRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtCHOut(63)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChAndOrRec

        ''論理出力チャンネル情報
        <VBFixedArray(23)> Dim udtCHAndOr() As gTypSetChAndOrRecDetail

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtCHAndOr(23)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChAndOrRecDetail

        Dim shtSysno As Short                                       ''SYSTEM No.
        Dim shtChid As Short                                        ''CH ID
        Dim bytSpare As Byte                                        ''予備
        Dim bytStatus As Byte                                       ''ステータス種類
        Dim shtMask As Short                                        ''マスクデータ

    End Structure

#End Region

#Region "グループＣＨ設定データ"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChGroupSet

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''グループ情報
        Dim udtGroup As gTypSetChGroupSetRec

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChGroupSetRec

        <VBFixedStringAttribute(8)> Dim strDrawNo As String         ''Draw No.
        <VBFixedStringAttribute(8)> Dim strSpare1 As String         ''予備１
        <VBFixedStringAttribute(32)> Dim strComment As String       ''コメント
        <VBFixedStringAttribute(16)> Dim strSpare2 As String        ''予備２
        <VBFixedStringAttribute(40)> Dim strShipNo As String        ''船番
        <VBFixedStringAttribute(24)> Dim strSpare3 As String        ''予備３

        ''グループ設定データ
        <VBFixedArray(35)> Dim udtGroupInfo() As gTypSetChGroupSetRecDetail

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtGroupInfo(35)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChGroupSetRecDetail

        Dim shtGroupNo As Short                                     ''グループ番号
        Dim shtSpare As Short                                       ''予備
        <VBFixedStringAttribute(16)> Dim strName1 As String         ''グループ名称　1行目
        <VBFixedStringAttribute(16)> Dim strName2 As String         ''グループ名称　2行目
        <VBFixedStringAttribute(16)> Dim strName3 As String         ''グループ名称　3行目
        Dim shtColor As Short                                       ''カラー設定
#Region "        Dim shtDisplayPosition As Short                    ''表示位置"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtDisplayPosition() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtDisplayPosition), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtDisplayPosition = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtDisplayPosition As Short

#End Region

    End Structure

#End Region

#Region "リポーズ用CH設定データ"

    ''リポーズ入力設定構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChGroupRepose

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        ''' <summary>
        ''' リポーズ入力設定
        ''' </summary>
        ''' <remarks>各リポーズ入力設定</remarks>
        ''' 47→71に拡張2018.12.13
        <VBFixedArray(71)> Dim udtRepose() As gTypSetChGroupReposeRec

        '''配列数初期化
        Public Sub InitArray()
            ReDim udtRepose(71) '47→71に拡張2018.12.13
        End Sub

    End Structure

    ''2018.12.13 グループリポーズが48の場合の構造体を追加
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChGroupRepose48

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        ''' <summary>
        ''' リポーズ入力設定
        ''' </summary>
        ''' <remarks>各リポーズ入力設定</remarks>
        <VBFixedArray(47)> Dim udtRepose() As gTypSetChGroupReposeRec


        '''配列数初期化
        Public Sub InitArray()
            ReDim udtRepose(47)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChGroupReposeRec

        Dim shtData As Short                                                    ''データ種別コード
#Region "        Dim shtChId As Short                                                    ''CH_ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChId() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChId), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChId = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChId As Short

#End Region
        <VBFixedArray(5)> Dim udtReposeInf() As gTypSetChGroupReposeRecInfo     ''リポーズ情報

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtReposeInf(5)
        End Sub

        <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Public Structure gTypSetChGroupReposeRecInfo

#Region "            Dim shtChId As Short                                    ''CH_ID"

            ''UInt16が構造体メンバとして使えないためプロパティとして定義
            Public Property shtChId() As UInt16
                Get
                    Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChId), 0)
                End Get
                Set(ByVal value As UInt16)
                    _shtChId = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
                End Set
            End Property

            ''内部的に使用されるメンバ
            ''外部からの値設定は行わない事
            Dim _shtChId As Short

#End Region
            <VBFixedStringAttribute(1)> Dim bytMask As Byte         ''マスク値
            <VBFixedStringAttribute(1)> Dim bytSpare As Byte        ''予備

        End Structure

    End Structure

#End Region

#Region "積算データ設定ファイル"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChRunHour

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''ポイント数
        <VBFixedArray(255)> Dim udtDetail() As gTypSetChRunHourRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtDetail(255)
        End Sub

    End Structure

#Region "積算データ設定ファイルDATA"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChRunHourRec

        Dim shtSysno As Short                                   ''計測チャンネルSYSTEM NO.
#Region "        Dim shtChid As Short                                    ''計測チャンネルCH ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChid As Short

#End Region
        Dim shtTrgSysno As Short                                ''トリガチャンネルSYSTEM NO.
#Region "        Dim shtTrgChid As Short                                 ''計測チャンネルCH ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtTrgChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtTrgChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtTrgChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtTrgChid As Short

#End Region
        Dim shtStatus As Short                                  ''ステータス
        Dim shtMask As Short                                    ''マスクデータ(ビットパターン）
        Dim shtSpare1 As Short                                  ''予備
        Dim shtSpare2 As Short                                  ''予備

    End Structure

#End Region

#End Region

#Region "排ガス演算処理設定"

    ''排ガス演算処理設定構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChExhGus

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        ''' <summary>
        ''' 排ガス演算用設定
        ''' </summary>
        ''' <remarks>排ガス演算用設定</remarks>
        <VBFixedArray(15)> Dim udtExhGusRec() As gTypSetChExhGusRec

        '''配列数初期化
        Public Sub InitArray()
            ReDim udtExhGusRec(15)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChExhGusRec

        Dim shtNum As Short                                         ''ｼﾘﾝﾀﾞ本数
        Dim shtSpare As Short                                       ''予備
        Dim shtAveSysno As Short                                    ''平均値出力CH  SYSTEM_NO.
#Region "        Dim shtAveChid As Short                                     ''平均値出力CH　CH_ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtAveChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtAveChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtAveChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtAveChid As Short

#End Region
        Dim shtRepSysno As Short                                    ''リポーズCH    SYSTEM_NO.
#Region "        Dim shtRepChid As Short                                     ''リポーズCH    CH_ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtRepChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtRepChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtRepChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtRepChid As Short

#End Region
        <VBFixedArray(23)> Dim udtExhGusCyl() As gTypSetChExhGusRecCyl      ''シリンダCH設定
        <VBFixedArray(23)> Dim udtExhGusDev() As gTypSetChExhGusRecDev      ''偏差CH設定

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtExhGusCyl(23)
            ReDim udtExhGusDev(23)
        End Sub

        <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Public Structure gTypSetChExhGusRecCyl

            Dim shtSysno As Short               ''NO.n  SYSTEM_NO.
#Region "            Dim shtChid As Short                ''NO.n  CH_ID"

            ''UInt16が構造体メンバとして使えないためプロパティとして定義
            Public Property shtChid() As UInt16
                Get
                    Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChid), 0)
                End Get
                Set(ByVal value As UInt16)
                    _shtChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
                End Set
            End Property

            ''内部的に使用されるメンバ
            ''外部からの値設定は行わない事
            Dim _shtChid As Short

#End Region

        End Structure

        <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Public Structure gTypSetChExhGusRecDev

            Dim shtSysno As Short               ''NO.n  SYSTEM_NO.
#Region "            Dim shtChid As Short                ''NO.n  CH_ID"

            ''UInt16が構造体メンバとして使えないためプロパティとして定義
            Public Property shtChid() As UInt16
                Get
                    Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChid), 0)
                End Get
                Set(ByVal value As UInt16)
                    _shtChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
                End Set
            End Property

            ''内部的に使用されるメンバ
            ''外部からの値設定は行わない事
            Dim _shtChid As Short

#End Region

        End Structure

    End Structure

#End Region

#Region "コントロール使用可／不可設定"

    ''コントロール使用可／不可設定構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChCtrlUse

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        ''' <summary>
        ''' コントロール使用可／不可設定
        ''' Ver1.9.6 2016.02.16  32 → 128 拡張
        ''' Ver2.0.0.7 128 → 256 拡張
        ''' </summary>
        ''' <remarks>コントロール使用可／不可設定</remarks>
        <VBFixedArray(255)> Dim udtCtrlUseNotuseRec() As gTypSetChCtrlUseRec

        '''配列数初期化
        ''' Ver1.9.6 2016.02.16  32 → 128 拡張
        ''' Ver2.0.0.7 128 → 256 拡張
        Public Sub InitArray()
            ReDim udtCtrlUseNotuseRec(255)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChCtrlUseRec

        Dim shtNo As Short                                                              ''項目番号
        Dim shtCount As Short                                                           ''条件数
        Dim bytFlg As Byte                                                              ''条件種類
        Dim bytSpare As Byte                                                            ''予備
        <VBFixedArray(31)> Dim udtUseNotuseDetails() As gTypSetChCtrlUseRecDetail     ''詳細設定

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtUseNotuseDetails(31)
        End Sub

        <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
        Public Structure gTypSetChCtrlUseRecDetail

#Region "            Dim shtChno As Short                ''CH_NO."

            ''UInt16が構造体メンバとして使えないためプロパティとして定義
            Public Property shtChno() As UInt16
                Get
                    Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChno), 0)
                End Get
                Set(ByVal value As UInt16)
                    _shtChno = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
                End Set
            End Property

            ''内部的に使用されるメンバ
            ''外部からの値設定は行わない事
            Dim _shtChno As Short

#End Region
            Dim bytType As Byte                 ''条件タイプ
            Dim bytSpare As Byte                ''予備
            Dim shtBit As Short                 ''ビット条件
            Dim shtProcess1 As Short            ''Process1
            Dim shtProcess2 As Short            ''Process2

        End Structure

    End Structure

#End Region

#Region "データ転送テーブル設定"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChDataForward

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''ポイント数
        <VBFixedArray(63)> Dim udtDetail() As gTypSetChDataForwardRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtDetail(63)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChDataForwardRec

#Region "        Dim shtDataCode As Short   　　             ''データコード"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtDataCode() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtDataCode), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtDataCode = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtDataCode As Short

#End Region
#Region "        Dim shtDataSubCode As Short                 ''データサブコード"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtDataSubCode() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtDataSubCode), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtDataSubCode = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtDataSubCode As Short

#End Region
#Region "        Dim intOffsetToFCU As integer               ''（OPS→FCU）オフセットアドレス"

        ''UInt32が構造体メンバとして使えないためプロパティとして定義
        Public Property intOffsetToFCU() As UInt32
            Get
                Return BitConverter.ToUInt32(BitConverter.GetBytes(_intOffsetToFCU), 0)
            End Get
            Set(ByVal value As UInt32)
                _intOffsetToFCU = BitConverter.ToInt32(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _intOffsetToFCU As Integer

#End Region
#Region "        Dim shtSizeToFCU As short                   ''（OPS→FCU）データサイズ"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSizeToFCU() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSizeToFCU), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSizeToFCU = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSizeToFCU As Short

#End Region
        Dim shtSpare1 As Short                      ''予備
#Region "        Dim intOffsetToOPS As integer               ''（FCU→OPS）オフセットアドレス"

        ''UInt32が構造体メンバとして使えないためプロパティとして定義
        Public Property intOffsetToOPS() As UInt32
            Get
                Return BitConverter.ToUInt32(BitConverter.GetBytes(_intOffsetToOPS), 0)
            End Get
            Set(ByVal value As UInt32)
                _intOffsetToOPS = BitConverter.ToInt32(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _intOffsetToOPS As Integer

#End Region
#Region "        Dim shtSizeToOps As short                   ''（FCU→OPS）データサイズ"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSizeToOps() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSizeToOps), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSizeToOps = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSizeToOps As Short

#End Region
        Dim shtSpare2 As Short                      ''予備
        <VBFixedArray(1)> Dim shtSpare3() As Short  ''予備

        ''配列数初期化
        Public Sub InitArray()
            ReDim shtSpare3(1)
        End Sub

    End Structure

#End Region

#Region "データ保存テーブル"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChDataSave

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''ポイント数
        <VBFixedArray(63)> Dim udtDetail() As gTypSetChDataSaveRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtDetail(63)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChDataSaveRec

        Dim shtSysno As Short               ''SYSTEM_NO
#Region "        Dim shtChid As Short          　　　''種類"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChid As Short

#End Region
        Dim intDefault As Integer           ''デフォルト値 ver1.4.0 2011.07.22 Single → Integer
        Dim shtSet As Short                 ''立上げ時のデータ保存方法
        Dim shtSpare As Short               ''予備

    End Structure

#End Region

#Region "SIO設定（外部機器VDR情報設定）"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChSio

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''チャンネル設定数レコード
        <VBFixedArray(15)> Dim shtNum() As Short

        ''VDR情報レコード
        <VBFixedArray(15)> Dim udtVdr() As gTypSetChSioVdr

        ''配列数初期化
        Public Sub InitArray()
            ReDim shtNum(15)
            ReDim udtVdr(15)
        End Sub

    End Structure

#Region "SIO(VDR)情報"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChSioVdr

#Region "        Dim shtPort As Short                                       ''ポート番号"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtPort() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtPort), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtPort = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtPort As Short

#End Region
#Region "        Dim shtExtComID As Short                                   ''外部機器識別子"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtExtComID() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtExtComID), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtExtComID = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtExtComID As Short

#End Region
#Region "        Dim shtPriority As Short                                   ''優先度"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtPriority() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtPriority), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtPriority = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtPriority As Short

#End Region
#Region "        Dim shtSysno As Short                                      ''SYSTEM NO"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSysno() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSysno), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSysno = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSysno As Short

#End Region
#Region "        Dim shtCommType1 As Short                                  ''i/o種類"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtCommType1() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtCommType1), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtCommType1 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtCommType1 As Short

#End Region
#Region "        Dim shtCommType2 As Short                                  ''通信種類"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtCommType2() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtCommType2), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtCommType2 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtCommType2 As Short

#End Region
        Dim udtCommInf As gTypSetChSioVdrSio                       ''回線情報
#Region "        Dim shtReceiveInit As Short                                ''受信タイムアウト（秒）起動時"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtReceiveInit() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtReceiveInit), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtReceiveInit = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtReceiveInit As Short

#End Region
#Region "        Dim shtReceiveUseally As Short                             ''受信タイムアウト（秒）起動後"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtReceiveUseally() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtReceiveUseally), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtReceiveUseally = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtReceiveUseally As Short

#End Region
#Region "        Dim shtSendInit As Short                                   ''送信間隔（秒）起動時"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSendInit() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSendInit), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSendInit = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSendInit As Short

#End Region
#Region "        Dim shtSendUseally As Short                                ''送信間隔（秒）起動後"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSendUseally() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSendUseally), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSendUseally = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSendUseally As Short

#End Region
#Region "        Dim shtRetry As Short                                      ''リトライ回数"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtRetry() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtRetry), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtRetry = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtRetry As Short

#End Region
#Region "        Dim shtDuplexSet As Short                                  ''Duplex 設定"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtDuplexSet() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtDuplexSet), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtDuplexSet = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtDuplexSet As Short

#End Region
#Region "        Dim shtSendCH As Short                                     ''送信CH"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSendCH() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSendCH), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSendCH = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSendCH As Short

#End Region
#Region "        Dim shtKakuTbl As Short                                      ''通信設定拡張ﾃｰﾌﾞﾙ有無 Ver2.0.5.8"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtKakuTbl() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtKakuTbl), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtKakuTbl = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtKakuTbl As Short

#End Region
        <VBFixedArray(7)> Dim udtNode() As gTypSetChSioVdrNode     ''ノード情報
        <VBFixedArray(511)> Dim bytSetData() As Byte               ''詳細設定データ

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtNode(7)
            ReDim bytSetData(511)
        End Sub

    End Structure

#End Region

#Region "SIOの場合_P42"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChSioVdrSio

#Region "        Dim shtComm As Short                                       ''回線種類"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtComm() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtComm), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtComm = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtComm As Short

#End Region
#Region "        Dim shtDataBit As Short                                    ''データビット"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtDataBit() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtDataBit), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtDataBit = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtDataBit As Short

#End Region
#Region "        Dim shtParity As Short                                     ''パリティ"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtParity() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtParity), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtParity = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtParity As Short

#End Region
#Region "        Dim shtStop As Short                                       ''ストップビット"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtStop() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtStop), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtStop = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtStop As Short

#End Region
#Region "        Dim shtComBps As Short                                     ''通信速度"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtComBps() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtComBps), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtComBps = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtComBps As Short

#End Region
#Region "        Dim shtSpare1 As Short                                     ''予備1"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSpare1() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSpare1), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSpare1 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSpare1 As Short

#End Region
#Region "        Dim shtSpare2 As Short                                     ''予備2"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSpare2() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSpare2), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSpare2 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSpare2 As Short

#End Region
#Region "        Dim shtSpare3 As Short                                     ''予備3"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSpare3() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSpare3), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSpare3 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSpare3 As Short

#End Region

    End Structure

#End Region

#Region "IPの場合_P42"

    '<Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    'Public Structure gTypSetChSioVdrIp

    '    Dim bytComm As Byte                                         ''回線種類
    '    Dim bytSpare As Byte                                        ''予備
    '    Dim lngIp As Long                                           ''ＩＰアドレス
    '    Dim lngMask As Long                                         ''マスク
    '    Dim bytSend As Byte                                         ''ポート番号（送信）
    '    Dim bytReceive As Byte                                      ''ポート番号（受信）
    '    <VBFixedStringAttribute(2)> Dim strSpare As String          ''予備

    'End Structure

#End Region

#Region "VDRのノード情報"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChSioVdrNode

#Region "        Dim shtCheck As Short                                       ''回線種類"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtCheck() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtCheck), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtCheck = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtCheck As Short

#End Region
#Region "        Dim shtAddress As Short                                     ''データ長"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtAddress() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtAddress), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtAddress = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtAddress As Short

#End Region

    End Structure

#End Region

#End Region

#Region "SIO設定（外部機器VDR情報設定）CH設定データ"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChSioCh

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''CH設定レコード
        <VBFixedArray(2999)> Dim udtSioChRec() As gTypSetChSioChRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtSioChRec(2999)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChSioChRec

#Region "        Dim shtChNo As Short                                   ''チャンネルNo"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChNo() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChNo), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChNo = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChNo As Short

#End Region
#Region "        Dim shtChId As Short                                   ''チャンネルID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChId() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChId), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChId = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChId As Short

#End Region
#Region "        Dim shtSpare2 As Short                        ''予備 size未使用のため、予備に修正 2011.12.15 K.Tanigawa"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtSpare2() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtSpare2), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtSpare2 = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtSpare2 As Short

#End Region

        Dim shtSpare As Short                                  ''予備

    End Structure

#End Region

#Region "SIO設定（外部機器VDR情報設定）拡張設定データ バイト4032 Ver2.0.5.8"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChSioExt
        ''拡張設定レコード
        <VBFixedArray(4031)> Dim bytSioExtRec() As Byte

        ''配列数初期化
        Public Sub InitArray()
            ReDim bytSioExtRec(4031)
        End Sub

    End Structure

#End Region

#Region "シーケンス設定"

#Region "シーケンスID"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqID

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''シーケンスＩＤ
        <VBFixedArray(1023)> Dim shtID() As Short

        ''配列数初期化
        Public Sub InitArray()
            ReDim shtID(1023)
        End Sub

    End Structure

#End Region

#Region "シーケンス設定"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqSet

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''シーケンス設定詳細
        <VBFixedArray(1023)> Dim udtDetail() As gTypSetSeqSetRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtDetail(1023)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqSetRec

        Dim shtId As Short                                              ''シーケンスＩＤ
        Dim shtLogicType As Short                                       ''出力ロジックタイプ
        <VBFixedArray(7)> Dim udtInput() As gTypSetSeqSetRecInput       ''８ＣＨ分設定可能
        <VBFixedStringAttribute(16)> Dim strRemarks As String           ''備考
#Region "        Dim shtLogicItem(5) As Short                                    ''ロジック項目"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtLogicItem(ByVal Index As Integer) As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtLogicItem(Index)), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtLogicItem(Index) = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        <VBFixedArray(5)> Dim _shtLogicItem() As Short

#End Region
        <VBFixedArray(5)> Dim shtUseCh() As Short                       ''チャンネル使用有無
        Dim shtOutSysno As Short                                        ''SYSTEM No.
#Region "        Dim shtOutChid As Short                                         ''CH ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtOutChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtOutChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtOutChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtOutChid As Short

#End Region
        Dim shtOutData As Short                                         ''出力データ
        Dim shtOutDelay As Short                                        ''出力オフディレイ
        Dim bytOutStatus As Byte                                        ''出力ステータス
        Dim bytOutIoSelect As Byte                                      ''入出力区分
        Dim bytOutDataType As Byte                                      ''出力データタイプ
        Dim bytOutInv As Byte                                           ''出力反転
        Dim bytFuno As Byte                                             ''FU　番号
        Dim bytPort As Byte                                             ''FU ポート番号
        Dim bytPin As Byte                                              ''FU　計測点位置
        Dim bytPinNo As Byte                                            ''FU　計測点個数
        Dim bytOutType As Byte                                          ''出力タイプ
        Dim bytOneShot As Byte                                          ''出力ワンショット時間
        Dim bytContine As Byte                                          ''処理継続中止
        Dim bytSpare1 As Byte                                           ''備考

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtInput(7)
            ReDim _shtLogicItem(5)
            ReDim shtUseCh(5)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqSetRecInput

        Dim shtSysno As Short                                       ''SYSTEM No.
#Region "        Dim shtChid As Short                                        ''CH ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChid As Short

#End Region
        Dim shtChSelect As Short                                    ''CH選択
        Dim shtIoSelect As Short                                    ''入出力区分
        Dim bytStatus As Byte                                       ''参照ステータス
        Dim bytType As Byte                                         ''タイプ
#Region "        Dim shtMask As Short                                        ''マスク値"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtMask() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtMask), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtMask = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtMask As Short

#End Region
        Dim shtAnalogType As Short                                  ''アナログ入力種別
        Dim strSpare As Short                                       ''予備

    End Structure

#End Region

#End Region

#Region "リニアライズテーブル"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqLinear

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''ポイント数
        <VBFixedArray(255)> Dim udtPoints() As gTypSetSeqLinearPoints

        ''リニアライズテーブル詳細
        <VBFixedArray(255)> Dim udtTables() As gTypSetSeqLinearTables

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtPoints(255)
            ReDim udtTables(255)
        End Sub

    End Structure

#Region "コントロールシーケンスのリニアライズテーブル値の数を設定_P65"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqLinearPoints

        Dim shtPoint As Short                                       ''リニアライズテーブル n　ポイント数

    End Structure

#End Region

#Region "コントロールシーケンスのリニアライズテーブル値設定_P65"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqLinearTables

        ''リニアライズテーブル詳細
        <VBFixedArray(1023)> Dim udtRow() As gTypSetSeqLinearTablesRow

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtRow(1023)
        End Sub

    End Structure


    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqLinearTablesRow

        Dim sngPtX As Single            ''リニアライズテーブル値（Ｘ）
        Dim sngPtY As Single            ''リニアライズテーブル値（Ｙ）

    End Structure

#End Region

#End Region

#Region "演算式テーブル"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqOperationExpression

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''演算式設定
        <VBFixedArray(63)> Dim udtTables() As gTypSetSeqOperationExpressionTable

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtTables(63)
        End Sub

    End Structure

#Region "コントロールシーケンスの演算テーブル値設定_P63"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqOperationExpressionTable

        <VBFixedStringAttribute(32)> Dim strExp As String                           ''式
        <VBFixedArray(7)> <VBFixedStringAttribute(8)> Dim strVariavleName() As String ''VariableName
        <VBFixedArray(15)> Dim udtAryInf() As gTypSetSeqOperationExpressionDeta     ''８Byte単位の配列

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtAryInf(15)
            ReDim strVariavleName(7)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetSeqOperationExpressionDeta

        Dim shtType As Short                                        ''定数種類
        Dim shtSpare As Short                                       ''予備
        <VBFixedArray(3)> Dim bytInfo() As Byte                     ''情報        
        <VBFixedStringAttribute(8)> Dim strFixNum As String         ''FixedNumberName

        ''配列数初期化
        Public Sub InitArray()
            ReDim bytInfo(3)
        End Sub

    End Structure

#End Region

#End Region

#Region "タイマ設定"

    ''タイマ設定構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetExtTimerSet

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        ''' <summary>
        ''' タイマ設定
        ''' </summary>
        ''' <remarks>タイマ設定</remarks>
        <VBFixedArray(15)> Dim udtTimerInfo() As gTypSetExtTimerRec

        '''配列数初期化
        Public Sub InitArray()
            ReDim udtTimerInfo(15)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetExtTimerRec

        Dim shtType As Short        ''種類
        Dim bytIndex As Byte        ''タイマ表示名称テーブル レコード番号
        Dim bytPart As Byte         ''パート区別
        Dim shtTimeDisp As Short    ''分/秒切替設定
        Dim shtInit As Short        ''初期値
        Dim shtLow As Short         ''下限値
        Dim shtHigh As Short        ''上限値

    End Structure

#End Region

#Region "タイマ表示名称設定"

    ''タイマ表示名称設定構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetExtTimerName

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        ''' <summary>
        ''' タイマ表示名称設定
        ''' </summary>
        ''' <remarks>タイマ表示名称設定</remarks>
        <VBFixedArray(15)> Dim udtTimerRec() As gTypSetExtTimerNameRec

        '''配列数初期化
        Public Sub InitArray()
            ReDim udtTimerRec(15)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
     Public Structure gTypSetExtTimerNameRec

        <VBFixedStringAttribute(32)> Dim strName As String          ''タイマ表示名称

    End Structure

#End Region

#Region "コンポジット設定データ"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChComposite

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''コンポジット情報
        <VBFixedArray(63)> Dim udtComposite() As gTypSetChCompositeRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtComposite(63)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChCompositeRec

#Region "        Dim shtChid As Short                                        ''CH ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChid As Short

#End Region
        Dim shtDiFilter As Short                                    ''ソフトウェアフィルタ定数
        <VBFixedArray(8)> Dim udtCompInf() As gTypSetChCompositeRecInfo          ''コンポジット設定値情報（9ﾊﾟﾀｰﾝ分）

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtCompInf(8)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChCompositeRecInfo

        Dim bytBitPattern As Byte                                   ''ステータスビットパターン
        Dim bytAlarmUse As Byte                                     ''ステータスビット仕様有無
        Dim bytDelay As Byte                                        ''ディレィタイマ値(ｱﾗｰﾑ継続時間)
        Dim bytExtGroup As Byte                                     ''EXT. グループ(延長警報ｸﾞﾙｰﾌﾟ)
        Dim bytGRepose1 As Byte                                     ''グループ・リポーズ１
        Dim bytGRepose2 As Byte                                     ''グループ・リポーズ１
        <VBFixedStringAttribute(8)> Dim strStatusName As String     ''ステータス名称
        Dim bytManualReposeState As Byte                            ''マニュアルリポーズ状態
        Dim bytManualReposeSet As Byte                              ''マニュアルリポーズ設定

    End Structure

#End Region

#Region "CH変換テーブル"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChConv

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''CH変換テーブル情報
        <VBFixedArray(3079)> Dim udtChConv() As gTypSetChConvRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtChConv(3079)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetChConvRec

#Region "        Dim shtChid As Short                                        ''CH ID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChid() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChid), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChid = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChid As Short

#End Region

    End Structure

#End Region

#Region "ログ印字設定"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOtherLogTime

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''ログ印字設定情報
        Dim udtLogTimeRec As gTypSetOtherLogTimeRec

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOtherLogTimeRec

        <VBFixedArray(4)> Dim udtLogTimeRegular() As gTypSetOtherLogTimeRecRegular     ''レギュラーログ印字時刻設定
        <VBFixedArray(0)> Dim udtLogTimeReport() As gTypSetOtherLogTimeRecReport       ''レポート印字時刻設定
        <VBFixedArray(0)> Dim udtLogTimeInterval() As gTypSetOtherLogTimeRecInterval   ''インターバルログ印字時刻設定

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtLogTimeRegular(4)
            ReDim udtLogTimeReport(0)
            ReDim udtLogTimeInterval(0)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOtherLogTimeRecRegular

        Dim bytUse As Byte          ''使用有無
        Dim bytTimeHH As Byte       ''時
        Dim bytTimeMM As Byte       ''分
        Dim bytSpare As Byte        ''予備

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOtherLogTimeRecReport

        Dim bytUse As Byte          ''使用有無
        Dim bytTimeHH As Byte       ''時
        Dim bytTimeMM As Byte       ''分
        Dim bytSpare As Byte        ''予備

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOtherLogTimeRecInterval

        Dim bytUse As Byte          ''使用有無
        Dim bytTimeHH As Byte       ''時
        Dim bytSpare As Byte        ''予備

    End Structure


#Region "ログ印字 ｵﾌﾟｼｮﾝ設定"     '' Ver1.9.3 2016.01.25 追加

    Public Structure gLogPosition   '' 座標　構造体

#Region "        Dim shtPosX As Short                         '' X座標."

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtPosX() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtPosX), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtPosX = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtPosX As Short

#End Region

#Region "        Dim shtPosY As Short                         '' Y座標."

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtPosY() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtPosY), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtPosY = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtPosY As Short

#End Region

    End Structure


    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gLogOptionCH   '' ｵﾌﾟｼｮﾝ設定　構造体

#Region "        Dim shtCHNo As Short                         '' CHNo."

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtCHNo() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtCHNo), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtCHNo = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtCHNo As Short

#End Region

        Dim bytType As Byte     '' ﾀｲﾌﾟ　　0:ﾇｰﾝﾃﾞｰﾀのみ印字  1:6回分ﾃﾞｰﾀ印字
        Dim bytSpare As Byte    '' 予備
        <VBFixedArray(5)> Dim gLogPosition() As gLogPosition  '' 印字座標

        ''配列数初期化
        Public Sub InitArray()
            ReDim gLogPosition(5)
        End Sub
    End Structure


    ''ﾛｸﾞｵﾌﾟｼｮﾝ設定構造体
    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypLogOption

        '''<summary>
        '''ヘッダーレコード
        '''</summary>
        Dim udtHeader As gTypSetHeader

        Dim bytSetting As Byte      '' 設定種別　1:ﾌｰﾄﾝH3020向け
        <VBFixedArray(6)> Dim udtSpare() As Byte    '' 予備

        '''<summary>
        '''ﾛｸﾞ設定個別ﾃﾞｰﾀ
        '''全1500個分設定
        '''</summary>
        <VBFixedArray(gCstOpsLogOptionMax - 1)> Dim udtLogOption() As gLogOptionCH

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtSpare(6)
            ReDim udtLogOption(gCstOpsLogOptionMax - 1)
        End Sub

    End Structure


#End Region
    

#End Region

#Region "フリーディスプレイ"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsFreeDisplay

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''フリーディスプレイ情報
        <VBFixedArray(79)> Dim udtFreeDisplayRec() As gTypSetOpsFreeDisplayRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtFreeDisplayRec(79)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsFreeDisplayRec

        Dim bytOps As Byte          ''OPS番号
        Dim bytPage As Byte         ''ページ番号
        <VBFixedStringAttribute(31)> Dim strPageTitle As String     ''ページタイトル
        <VBFixedArray(19)> Dim udtFreeDisplayRecChno() As gTypSetOpsFreeDisplayRecChno

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtFreeDisplayRecChno(19)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsFreeDisplayRecChno

#Region "        Dim shtChno As Short                                        ''CH NO"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChno() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChno), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChno = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChno As Short

#End Region

    End Structure

#End Region

#Region "トレンドグラフ"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsTrendGraph

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''トレンドグラフ情報
        <VBFixedArray(159)> Dim udtTrendGraphRec() As gTypSetOpsTrendGraphRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtTrendGraphRec(159)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsTrendGraphRec

        Dim bytOps As Byte          ''OPS番号
        Dim bytNo As Byte                                           ''グラフ番号
        Dim bytSpare As Byte                                        ''予備
        <VBFixedStringAttribute(32)> Dim strPageTitle As String     ''グラフタイトル
        Dim bytSnpType As Byte                                      ''サンプリング時間 種別
        Dim bytSnpTime As Byte                                      ''サンプリング時間 時間値
        Dim shtTrgUse As Short                                      ''トリガ CH有無
#Region "        Dim shtTrgChno As Short                                     ''CH NO"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtTrgChno() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtTrgChno), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtTrgChno = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtTrgChno As Short

#End Region
        Dim shtTrgSelect As Short                                   ''トリガ 種別
        Dim shtTrgSet As Short                                      ''トリガ 比較条件
        Dim shtTrgValue As Short                                    ''トリガ 値
        Dim shtDelay As Short                                       ''ディレイポイント値
        <VBFixedArray(15)> Dim udtTrendGraphRecChno() As gTypSetOpsTrendGraphRecChno    '' 19->15 へ変更 2011.12.13 K.Tanigawa

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtTrendGraphRecChno(15)  '' 19->15 へ変更 2011.12.13 K.Tanigawa
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsTrendGraphRecChno

#Region "        Dim shtChno As Short                                        ''CH NO"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChno() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChno), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChno = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChno As Short

#End Region
#Region "        Dim shtMask As Short                                        ''Mask"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtMask() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtMask), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtMask = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtMask As Short

#End Region

    End Structure

#End Region


#Region "GWS設定 CH設定データ"     '' 2014.02.04

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGwsCh

        ''ヘッダーレコード
        Dim udtHeader As gTypSetHeader

        ''CH設定レコード
        <VBFixedArray(7)> Dim udtGwsFileRec() As gTypSetOpsGwsFileRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtGwsFileRec(7)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGwsFileRec

        ''CH設定レコード
        <VBFixedArray(2999)> Dim udtGwsChRec() As gTypSetOpsGwsChRec

        ''配列数初期化
        Public Sub InitArray()
            ReDim udtGwsChRec(2999)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetOpsGwsChRec

#Region "        Dim shtChNo As Short                                   ''チャンネルNo"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChNo() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChNo), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChNo = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChNo As Short

#End Region
#Region "        Dim shtChId As Short                                   ''チャンネルID"

        ''UInt16が構造体メンバとして使えないためプロパティとして定義
        Public Property shtChId() As UInt16
            Get
                Return BitConverter.ToUInt16(BitConverter.GetBytes(_shtChId), 0)
            End Get
            Set(ByVal value As UInt16)
                _shtChId = BitConverter.ToInt16(BitConverter.GetBytes(value), 0)
            End Set
        End Property

        ''内部的に使用されるメンバ
        ''外部からの値設定は行わない事
        Dim _shtChId As Short

#End Region

    End Structure

#End Region

#Region "負荷曲線"

    '構造体
    Public udtLoadCurve As gTypLoadCurve
    Public mudtSetLoadCurveNew As gTypLoadCurve    '設定保存用構造体(比較)

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypLoadCrvData   '設定毎の構造体
        'ポインタ1
        Dim AlmNoStbd01 As Short         'アラーム時 表示マーク部品番号
        Dim CrvFlg01 As Short            '特殊設定フラグ
        Dim CrvColr01 As Short           '負荷曲線描画色
        Dim CrvWdh01 As Short            '　　　　太さ
        Dim Spare11 As Short             '予備
        Dim ChNoDialPos01 As Short     '負荷曲線設定CH (CH名称：主機負荷ダイヤル位置）
        Dim CrvRangeMin As Short        '最小値
        Dim CrvRangeMax As Short        '最大値
        Dim CrvDrawMin As Short         '描画最小値
        Dim CrvDrawMax As Short         '描画最大値
        Dim CrvVertRangeHigh As Short   '縦軸 上限（Gラベルで設定した範囲におけるRANGE値）
        Dim CrvVertRangeLow As Short    '　　 下限
        Dim CrvWidhRangeHigh As Short   '横軸 上限
        Dim CrvWidhRangeLow As Short    '　　 下限
        Dim RangeSplit As Short          '負荷曲線分割数
        Dim Spare12 As Short             '予備

        'ポインタ2
        Dim CrvDispFlg02 As Short        '表示データ有無  1:表示あり
        Dim Spare21 As Short             '予備
        Dim OutputChNoPower02 As Short '出力 縦軸CHNo. (CH名称：主機出力）
        Dim OutputChNoSpeed02 As Short '     横軸CHNo.　(CH名称：主機回転速度）
        Dim OutputNrmlNo02 As Short    '     ノーマル時 表示マーク部品番号
        Dim OutputAlmNo02 As Short     '     アラーム時 表示マーク部品番号
        Dim OutputNrmlColr02 As Short    '     ノーマル時 描画色
        Dim OutputAlmColr02 As Short     '     アラーム時 描画色
        Dim CrvFlg02 As Short            '特殊設定フラグ
        Dim CrvColr02 As Short           '負荷曲線描画色
        Dim CrvWdh02 As Short            '        太さ
        Dim Spare22 As Short             '予備
        Dim ChNoDialPos02 As Short     '負荷曲線設定CH (CH名称：主機負荷ダイヤル位置）
        Dim Power As Short             '負荷曲線計算用  出力(定格値(Kw))
        Dim Coeff As Short             '係数(rpm)
        <VBFixedArray(1)> Dim Spare23() As Short           '予備[2]

        'ポインタ3
        Dim CrvDispFlg03 As Short        '表示データ有無  1:表示あり
        Dim Spare31 As Short             '予備
        Dim OutputChNoPower03 As Short '出力 縦軸CHNo.
        Dim OutputChNoSpeed03 As Short '     横軸CHNo.
        Dim OutputNrmlNo03 As Short    '     ノーマル時 表示マーク部品番号
        Dim OutputAlmNo03 As Short     '     アラーム時 表示マーク部品番号
        Dim OutputNrmlColr03 As Short    '     ノーマル時 描画色
        Dim OutputAlmColr03 As Short     '     アラーム時 描画色
        <VBFixedArray(1)> Dim Spare32() As Short           '予備[2]

        'ポインタ4
        Dim CrvDispFlg04 As Short        '表示データ有無  1:表示あり
        Dim Spare41 As Short             '予備
        Dim OutputChNoPower04 As Short '出力 縦軸CHNo.
        Dim OutputChNoSpeed04 As Short '     横軸CHNo.
        Dim OutputNrmlNo04 As Short    '     ノーマル時 表示マーク部品番号
        Dim OutputAlmNo04 As Short     '     アラーム時 表示マーク部品番号
        Dim OutputNrmlColr04 As Short    '     ノーマル時 描画色
        Dim OutputAlmColr04 As Short     '     アラーム時 描画色
        Dim RepsChNoStbd As Short      '負荷曲線１（右舷（Ｓ）） 休止
        Dim RepsChNoPort As Short      '負荷曲線２（左舷（Ｐ）） 休止
        <VBFixedArray(15)> Dim Spare() As Short             '予備[16]

        ''配列数初期化
        Public Sub InitArray()
            ReDim Spare23(1)
            ReDim Spare32(1)
            ReDim Spare(15)
        End Sub

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypLoadCurve     '設定1～4まで含んだ全体の構造体
        <VBFixedArray(3)> Dim CrvSet As gTypLoadCrvData()

        Public Sub InitArray()
            ReDim CrvSet(3)
        End Sub

    End Structure

#End Region


#Region "ファイル更新情報"

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetEditorUpdateInfo

        '''ヘッダーレコード
        Dim udtHeader As gTypSetHeader                  '32byte

        ''ファイル更新情報
        Dim udtSave As gTypSetEditorUpdateInfoRec
        Dim udtCompile As gTypSetEditorUpdateInfoRec

    End Structure

    <Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    Public Structure gTypSetEditorUpdateInfoRec

        Dim bytSystem As Byte                    ''システム設定データ
        Dim bytFuChannel As Byte                 ''FU チャンネル情報
        Dim bytChDisp As Byte                    ''チャンネル情報データ（表示名設定
        Dim bytChannel As Byte                   ''チャンネル情報
        Dim bytComposite As Byte                 ''コンポジット情報
        Dim bytGroupM As Byte                    ''グループ設定
        Dim bytGroupC As Byte                    ''グループ設定
        Dim bytRepose As Byte                    ''リポーズ入力設定
        Dim bytOutPut As Byte                    ''出力チャンネル設定
        Dim bytOrAnd As Byte                     ''論理出力設定
        Dim bytChRunHour As Byte                 ''積算データ設定
        Dim bytCtrlUseNotuseM As Byte            ''コントロール使用可／不可設定
        Dim bytCtrlUseNotuseC As Byte            ''コントロール使用可／不可設定
        Dim bytChSio As Byte                     ''SIO設定

        <VBFixedArray(15)> _
        Dim bytChSioCh() As Byte                 ''SIO通信チャンネル設定1～16

        '2019.03.18 8->15
        <VBFixedArray(15)> _
        Dim bytChSioExt() As Byte                 ''SIO通信拡張設定1～9

        Dim bytExhGus As Byte                    ''排ガス処理演算設定
        Dim bytExtAlarm As Byte                  ''延長警報
        Dim bytTimer As Byte                     ''タイマ設定
        Dim bytTimerName As Byte                 ''タイマ表示名称設定
        Dim bytSeqSequenceID As Byte             ''シーケンスID
        Dim bytSeqSequenceSet As Byte            ''シーケンス設定
        Dim bytSeqLinear As Byte                 'リニアライズテーブル
        Dim bytSeqOperationExpression As Byte    '演算式テーブル
        Dim bytChDataSaveTable As Byte           ''データ保存テーブル設定
        Dim bytChDataForwardTableSet As Byte     ''データ転送テーブル設定
        Dim bytOpsScreenTitleM As Byte           ''OPSスクリーンタイトル
        Dim bytOpsScreenTitleC As Byte           ''OPSスクリーンタイトル
        Dim bytOpsManuMainM As Byte                 ''プルダウンメニュー
        Dim bytOpsManuMainC As Byte                 ''プルダウンメニュー
        Dim bytOpsSelectionMenuM As Byte            ''セレクションメニュー
        Dim bytOpsSelectionMenuC As Byte            ''セレクションメニュー
        Dim bytOpsGraphM As Byte                 ''OPSグラフ設定
        Dim bytOpsGraphC As Byte                 ''OPSグラフ設定
        Dim bytOpsFreeGraphM As Byte             ''フリーグラフ       ' 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
        Dim bytOpsFreeGraphC As Byte             ''フリーグラフ       ' 2013.07.22 グラフとフリーグラフと分離  K.Fujimoto
        Dim bytOpsFreeDisplayM As Byte           ''フリーディスプレイ
        Dim bytOpsFreeDisplayC As Byte           ''フリーディスプレイ
        Dim bytOpsTrendGraphM As Byte            ''トレンドグラフ
        Dim bytOpsTrendGraphC As Byte            ''トレンドグラフ
        Dim bytOpsTrendGraphPID As Byte          ''PIDトレンドグラフ
        Dim bytOpsTrendGraphPID2 As Byte         ''PIDトレンドグラフ2
        Dim bytOpsLogFormatM As Byte             ''ログフォーマット
        Dim bytOpsLogFormatC As Byte             ''ログフォーマット
        Dim bytChConvNow As Byte                 ''CH変換テーブル
        Dim bytChConvPrev As Byte                ''CH変換テーブル
        Dim bytOpsLogIdDataM As Byte             ''ログフォーマットCHID ☆2012/10/26 K.Tanigawa
        Dim bytOpsLogIdDataC As Byte             ''ログフォーマットCHID ☆2012/10/26 K.Tanigawa
        Dim bytOpsGwsCh As Byte                  ''GWS通信チャンネル設定  2014.02.04
        Dim bytOpsLogOption As Byte              ''ﾛｸﾞｵﾌﾟｼｮﾝ設定　Ver1.9.3 2016.01.25

        ''        <VBFixedStringAttribute(44)> _  2012/10/26 K.Tanigawa 44->42
        ''                                        2013.07.22 K.Fujimoto 42->40
        ''                                        2014.02.04 K.Fujimoto 40->39
        ''                                        2016.01.25 Ver1.9.3   39->38
        <VBFixedStringAttribute(38)> _
        Dim strSpare As String                   ''予備（全フラグ + 予備 = 100 byte）

        ''配列数初期化
        Public Sub InitArray()
            ReDim bytChSioCh(15)
            ReDim bytChSioExt(15)                   ''20200514　Iwasaki 8->15
        End Sub

    End Structure

#End Region

#Region "未使用"

#Region "CHデータ出力情報設定_P56"

    '<Serializable()>     <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    '    Public Structure gTypSetChOutput

    '        Dim bytSysno As Byte                                        ''SYSTEM No.
    '        Dim bytChid As Byte                                         ''CH ID 又は 論理出力 ID
    '        Dim bytSpare As Byte                                        ''予備
    '        Dim bytType As Byte                                         ''ＣＨデータ、論理出力チャネルデータ
    '        Dim bytStatus As Byte                                       ''Output Movement
    '        Dim bytMask As Byte                                         ''Output Movement
    '        Dim bytOutput As Byte                                       ''CH OUT Type Setup
    '        Dim bytFuno As Byte                                         ''FU 番号
    '        Dim bytPortno As Byte                                       ''FU ポート番号
    '        Dim bytPin As Byte                                          ''FU 計測点番号
    '        Dim bytOutValue As Byte                                     ''出力値

    '    End Structure

#End Region

#Region "CH出力のOR設定_P59"

    '<Serializable()>     <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    '    Public Structure gTypSetCHor

    '        Dim bytSysno As Byte                                        ''SYSTEM No.
    '        Dim bytChid As Byte                                         ''CH ID
    '        Dim bytSpare As Byte                                        ''予備
    '        Dim bytStatus As Byte                                       ''ステータス種類
    '        Dim bytMask As Byte                                         ''マスクデータ

    '    End Structure

#End Region

#Region "CH出力のAND設定_P59"

    '<Serializable()>     <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    '    Public Structure gTypSetCHand

    '        Dim bytSysno As Byte                                        ''SYSTEM No.
    '        Dim bytChid As Byte                                         ''CH ID
    '        Dim bytSpare As Byte                                        ''予備
    '        Dim bytStatus As Byte                                       ''ステータス種類
    '        Dim bytMask As Byte                                         ''マスクデータ

    '    End Structure

#End Region

#Region "ECC入力設定DI_P35"

    '<StructLayout(LayoutKind.Sequential, Pack:=1)> _
    'Public Structure gTypSetEccDi

    '    Dim bytType As Byte                                         ''機能種別
    '    Dim bytSlot As Byte                                         ''スロットNo. （端子ID）
    '    Dim bytPin As Byte                                          ''計測点位置（端子ID）
    '    Dim bytSysno As Byte                                        ''SYSTEM No.
    '    Dim bytChid As Byte                                         ''CH ID　
    '    <VBFixedStringAttribute(12)> Dim strName As String          ''名称
    '    <VBFixedStringAttribute(12)> Dim strRemark As String        ''Remark

    'End Structure

#End Region

#Region "ECC入力設定DO_P35"

    '<StructLayout(LayoutKind.Sequential, Pack:=1)> _
    'Public Structure gTypSetEccDo

    '    Dim bytType As Byte                                         ''機能種別
    '    Dim bytSlot As Byte                                         ''スロットNo. （端子ID）
    '    Dim bytPin As Byte                                          ''計測点位置（端子ID）
    '    Dim bytSysno As Byte                                        ''SYSTEM No.
    '    Dim bytChid As Byte                                         ''CH ID　
    '    <VBFixedStringAttribute(12)> Dim strName As String          ''名称
    '    <VBFixedStringAttribute(12)> Dim strRemark As String        ''Remark

    'End Structure

#End Region

#Region "CH設定(情報)_P41"

    '<StructLayout(LayoutKind.Sequential, Pack:=1)> _
    'Public Structure gTypSetCHno

    '    <VBFixedArray(255)> Dim intChno01() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno02() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno03() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno04() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno05() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno06() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno07() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno08() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno09() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno10() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno11() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno12() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno13() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno14() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno15() As Integer                                                    ''ＣＨ情報
    '    <VBFixedArray(255)> Dim intChno16() As Integer                                                    ''ＣＨ情報

    'End Structure

#End Region

#Region "パルスカウントを積算するチャンネル設定_P61"

    '<Serializable()> <StructLayout(LayoutKind.Sequential, Pack:=1)> _
    'Public Structure gTypSetPcou

    '    Dim bytSysno As Byte                                        ''SYSTEM No.
    '    Dim bytChid As Byte                                         ''CH ID
    '    Dim bytType As Byte                                         ''データ種類
    '    Dim bytSpare As Byte                                        ''予備
    '    Dim bytASysno As Byte                                       ''積算トリガCH　SYSTEM No.
    '    Dim bytAChid As Byte                                        ''積算トリガCH　CH ID
    '    Dim shtSpare As Short                                       ''予備

    'End Structure

#End Region

#End Region


End Module