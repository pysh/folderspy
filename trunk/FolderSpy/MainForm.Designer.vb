'
' Created by SharpDevelop.
' User: pnosov
' Date: 15.03.2012
' Time: 17:06
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Partial Class MainForm
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.notifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
		Me.contextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.MenuItemShowHide = New System.Windows.Forms.ToolStripMenuItem()
		Me.MenuItemExit = New System.Windows.Forms.ToolStripMenuItem()
		Me.fswOffline = New System.IO.FileSystemWatcher()
		Me.fswEmergency = New System.IO.FileSystemWatcher()
		Me.fswProfile = New System.IO.FileSystemWatcher()
		Me.fswDiasoft = New System.IO.FileSystemWatcher()
		Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.btnCopy = New System.Windows.Forms.ToolStripButton()
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.btnCompress = New System.Windows.Forms.ToolStripButton()
		Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
		Me.lbl1 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.StatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
		Me.timer1 = New System.Windows.Forms.Timer(Me.components)
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.listView1 = New System.Windows.Forms.ListView()
		Me.colFileName = New System.Windows.Forms.ColumnHeader()
		Me.colFileQty = New System.Windows.Forms.ColumnHeader()
		Me.LogBox = New System.Windows.Forms.ListBox()
		Me.timer2 = New System.Windows.Forms.Timer(Me.components)
		Me.contextMenuStrip1.SuspendLayout
		CType(Me.fswOffline,System.ComponentModel.ISupportInitialize).BeginInit
		CType(Me.fswEmergency,System.ComponentModel.ISupportInitialize).BeginInit
		CType(Me.fswProfile,System.ComponentModel.ISupportInitialize).BeginInit
		CType(Me.fswDiasoft,System.ComponentModel.ISupportInitialize).BeginInit
		Me.toolStrip1.SuspendLayout
		Me.statusStrip1.SuspendLayout
		Me.splitContainer1.Panel1.SuspendLayout
		Me.splitContainer1.Panel2.SuspendLayout
		Me.splitContainer1.SuspendLayout
		Me.SuspendLayout
		'
		'notifyIcon1
		'
		Me.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
		Me.notifyIcon1.ContextMenuStrip = Me.contextMenuStrip1
		Me.notifyIcon1.Icon = CType(resources.GetObject("notifyIcon1.Icon"),System.Drawing.Icon)
		Me.notifyIcon1.Text = "notifyIcon1"
		Me.notifyIcon1.Visible = true
		AddHandler Me.notifyIcon1.DoubleClick, AddressOf Me.NotifyIcon1DoubleClick
		'
		'contextMenuStrip1
		'
		Me.contextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemShowHide, Me.MenuItemExit})
		Me.contextMenuStrip1.Name = "contextMenuStrip1"
		Me.contextMenuStrip1.Size = New System.Drawing.Size(202, 48)
		'
		'MenuItemShowHide
		'
		Me.MenuItemShowHide.Name = "MenuItemShowHide"
		Me.MenuItemShowHide.Size = New System.Drawing.Size(201, 22)
		Me.MenuItemShowHide.Text = "Показать/скрыть окно"
		AddHandler Me.MenuItemShowHide.Click, AddressOf Me.MenuItemShowHideClick
		'
		'MenuItemExit
		'
		Me.MenuItemExit.Name = "MenuItemExit"
		Me.MenuItemExit.Size = New System.Drawing.Size(201, 22)
		Me.MenuItemExit.Text = "Выход"
		AddHandler Me.MenuItemExit.Click, AddressOf Me.ToolStripMenuItem2Click
		'
		'fswOffline
		'
		Me.fswOffline.EnableRaisingEvents = true
		Me.fswOffline.Filter = "*.xml"
		Me.fswOffline.Path = "G:\Projects\CardOps\Schedules\Off-Line"
		Me.fswOffline.SynchronizingObject = Me
		AddHandler Me.fswOffline.Created, AddressOf Me.FileCreated
		AddHandler Me.fswOffline.Deleted, AddressOf Me.FileDeleted
		'
		'fswEmergency
		'
		Me.fswEmergency.EnableRaisingEvents = true
		Me.fswEmergency.Filter = "*.xml"
		Me.fswEmergency.Path = "G:\Projects\CardOps\Schedules\Emergency"
		Me.fswEmergency.SynchronizingObject = Me
		AddHandler Me.fswEmergency.Created, AddressOf Me.FileCreated
		AddHandler Me.fswEmergency.Deleted, AddressOf Me.FileDeleted
		'
		'fswProfile
		'
		Me.fswProfile.EnableRaisingEvents = true
		Me.fswProfile.Filter = "*.xml"
		Me.fswProfile.Path = "G:\Projects\CardOps\Schedules\IL_Profile"
		Me.fswProfile.SynchronizingObject = Me
		AddHandler Me.fswProfile.Created, AddressOf Me.FileCreated
		AddHandler Me.fswProfile.Deleted, AddressOf Me.FileDeleted
		'
		'fswDiasoft
		'
		Me.fswDiasoft.EnableRaisingEvents = true
		Me.fswDiasoft.Filter = "*.xml"
		Me.fswDiasoft.Path = "G:\Projects\CardOps\Schedules\GP_AUTO_IL_Diasoft"
		Me.fswDiasoft.SynchronizingObject = Me
		AddHandler Me.fswDiasoft.Created, AddressOf Me.FileCreated
		AddHandler Me.fswDiasoft.Deleted, AddressOf Me.FileDeleted
		'
		'imageList1
		'
		Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"),System.Windows.Forms.ImageListStreamer)
		Me.imageList1.TransparentColor = System.Drawing.Color.Transparent
		Me.imageList1.Images.SetKeyName(0, "folder.png")
		Me.imageList1.Images.SetKeyName(1, "page_code.png")
		Me.imageList1.Images.SetKeyName(2, "page_white_code_red.png")
		'
		'toolStrip1
		'
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnCopy, Me.toolStripSeparator1, Me.btnCompress})
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(430, 25)
		Me.toolStrip1.TabIndex = 1
		Me.toolStrip1.Text = "toolStrip1"
		'
		'btnCopy
		'
		Me.btnCopy.Image = CType(resources.GetObject("btnCopy.Image"),System.Drawing.Image)
		Me.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.btnCopy.Name = "btnCopy"
		Me.btnCopy.Size = New System.Drawing.Size(160, 22)
		Me.btnCopy.Text = "Скопировать выделенное"
		Me.btnCopy.ToolTipText = "Копирование списка выделенных файлов в буфер обмена"
		AddHandler Me.btnCopy.Click, AddressOf Me.BtnCopyClick
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
		'
		'btnCompress
		'
		Me.btnCompress.Image = CType(resources.GetObject("btnCompress.Image"),System.Drawing.Image)
		Me.btnCompress.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.btnCompress.Name = "btnCompress"
		Me.btnCompress.Size = New System.Drawing.Size(205, 22)
		Me.btnCompress.Text = "Архивировать выделенные файлы"
		AddHandler Me.btnCompress.Click, AddressOf Me.BtnCompressClick
		'
		'statusStrip1
		'
		Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbl1, Me.StatusLabel1})
		Me.statusStrip1.Location = New System.Drawing.Point(0, 553)
		Me.statusStrip1.Name = "statusStrip1"
		Me.statusStrip1.Size = New System.Drawing.Size(430, 22)
		Me.statusStrip1.TabIndex = 2
		Me.statusStrip1.Text = "statusStrip1"
		AddHandler Me.statusStrip1.DoubleClick, AddressOf Me.StatusStrip1DoubleClick
		'
		'lbl1
		'
		Me.lbl1.Name = "lbl1"
		Me.lbl1.Size = New System.Drawing.Size(0, 17)
		'
		'StatusLabel1
		'
		Me.StatusLabel1.Name = "StatusLabel1"
		Me.StatusLabel1.Size = New System.Drawing.Size(19, 17)
		Me.StatusLabel1.Text = "---"
		'
		'timer1
		'
		Me.timer1.Interval = 90000
		AddHandler Me.timer1.Tick, AddressOf Me.Timer1Tick
		'
		'splitContainer1
		'
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.Location = New System.Drawing.Point(0, 25)
		Me.splitContainer1.Name = "splitContainer1"
		Me.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.listView1)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.LogBox)
		Me.splitContainer1.Size = New System.Drawing.Size(430, 528)
		Me.splitContainer1.SplitterDistance = 433
		Me.splitContainer1.TabIndex = 3
		'
		'listView1
		'
		Me.listView1.AllowDrop = true
		Me.listView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.listView1.CheckBoxes = true
		Me.listView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colFileName, Me.colFileQty})
		Me.listView1.FullRowSelect = true
		Me.listView1.LabelEdit = true
		Me.listView1.Location = New System.Drawing.Point(12, 3)
		Me.listView1.Name = "listView1"
		Me.listView1.Size = New System.Drawing.Size(406, 427)
		Me.listView1.SmallImageList = Me.imageList1
		Me.listView1.TabIndex = 1
		Me.listView1.UseCompatibleStateImageBehavior = false
		Me.listView1.View = System.Windows.Forms.View.Details
		AddHandler Me.listView1.DragDrop, AddressOf Me.ListView1DragDrop
		AddHandler Me.listView1.DragEnter, AddressOf Me.ListView1DragEnter
		AddHandler Me.listView1.DragOver, AddressOf Me.ListView1DragOver
		AddHandler Me.listView1.DragLeave, AddressOf Me.ListView1DragLeave
		AddHandler Me.listView1.GiveFeedback, AddressOf Me.ListView1GiveFeedback
		AddHandler Me.listView1.QueryContinueDrag, AddressOf Me.ListView1QueryContinueDrag
		AddHandler Me.listView1.MouseDown, AddressOf Me.ListView1MouseDown
		'
		'colFileName
		'
		Me.colFileName.Text = "Имя файла"
		Me.colFileName.Width = 298
		'
		'colFileQty
		'
		Me.colFileQty.Text = "Количество"
		Me.colFileQty.Width = 104
		'
		'LogBox
		'
		Me.LogBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
						Or System.Windows.Forms.AnchorStyles.Left)  _
						Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
		Me.LogBox.Font = New System.Drawing.Font("Consolas", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(204,Byte))
		Me.LogBox.FormattingEnabled = true
		Me.LogBox.Location = New System.Drawing.Point(12, 3)
		Me.LogBox.Name = "LogBox"
		Me.LogBox.Size = New System.Drawing.Size(406, 82)
		Me.LogBox.TabIndex = 0
		AddHandler Me.LogBox.DoubleClick, AddressOf Me.LogBoxDoubleClick
		'
		'timer2
		'
		Me.timer2.Interval = 1000
		AddHandler Me.timer2.Tick, AddressOf Me.Timer2Tick
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(430, 575)
		Me.Controls.Add(Me.splitContainer1)
		Me.Controls.Add(Me.statusStrip1)
		Me.Controls.Add(Me.toolStrip1)
		Me.MaximizeBox = false
		Me.Name = "MainForm"
		Me.ShowInTaskbar = false
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "FolderSpy /16.05.2014/"
		Me.TopMost = true
		AddHandler Shown, AddressOf Me.MainFormShown
		AddHandler Resize, AddressOf Me.MainFormResize
		Me.contextMenuStrip1.ResumeLayout(false)
		CType(Me.fswOffline,System.ComponentModel.ISupportInitialize).EndInit
		CType(Me.fswEmergency,System.ComponentModel.ISupportInitialize).EndInit
		CType(Me.fswProfile,System.ComponentModel.ISupportInitialize).EndInit
		CType(Me.fswDiasoft,System.ComponentModel.ISupportInitialize).EndInit
		Me.toolStrip1.ResumeLayout(false)
		Me.toolStrip1.PerformLayout
		Me.statusStrip1.ResumeLayout(false)
		Me.statusStrip1.PerformLayout
		Me.splitContainer1.Panel1.ResumeLayout(false)
		Me.splitContainer1.Panel2.ResumeLayout(false)
		Me.splitContainer1.ResumeLayout(false)
		Me.ResumeLayout(false)
		Me.PerformLayout
	End Sub
	Private btnCompress As System.Windows.Forms.ToolStripButton
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private timer2 As System.Windows.Forms.Timer
	Private LogBox As System.Windows.Forms.ListBox
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private timer1 As System.Windows.Forms.Timer
	Private StatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
	Private lbl1 As System.Windows.Forms.ToolStripStatusLabel
	Private statusStrip1 As System.Windows.Forms.StatusStrip
	Private btnCopy As System.Windows.Forms.ToolStripButton
	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private MenuItemExit As System.Windows.Forms.ToolStripMenuItem
	Private MenuItemShowHide As System.Windows.Forms.ToolStripMenuItem
	Private contextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
	Private imageList1 As System.Windows.Forms.ImageList
	Private colFileQty As System.Windows.Forms.ColumnHeader
	Private colFileName As System.Windows.Forms.ColumnHeader
	Private listView1 As System.Windows.Forms.ListView
	Private fswDiasoft As System.IO.FileSystemWatcher
	Private fswProfile As System.IO.FileSystemWatcher
	Private fswEmergency As System.IO.FileSystemWatcher
	Private fswOffline As System.IO.FileSystemWatcher
	Private notifyIcon1 As System.Windows.Forms.NotifyIcon
End Class
