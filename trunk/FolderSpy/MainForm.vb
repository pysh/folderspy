'
' Created by SharpDevelop.
' User: pnosov
' Date: 15.03.2012
' Time: 17:06
' 
' To change this template use Tools | Options | Coding | Edit Standard Headers.
'
Imports System.ComponentModel
Imports System.IO
Imports System.Text
'Imports ICSharpCode.SharpZipLib.Core
'Imports ICSharpCode.SharpZipLib.Zip

Public Partial Class MainForm
	
	'Private dataObj As ShellDataObject = Nothing
	Private t2 As Integer = 0
	Private ArchExe As String = System.IO.Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "7zG.exe")
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		listView1.Groups.Clear
		listView1.Items.Clear
		AddLogEntry (String.Format("{0} * Старт программы",Now.ToString))
		notifyIcon1.ShowBalloonTip(20000, Now.ToString & ". FolderSpy запущен", " ", ToolTipIcon.Info)
		RefreshList()
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Sub AddLogEntry(strLogstring As String)
		LogBox.SelectedIndex = LogBox.Items.Add(strLogstring)
	End Sub
	
	Sub MainTimerStart()
		t2 = 0
		timer1.Start()
		timer2.Start()
	End Sub
	
	Sub FileCreated(sender As Object, e As System.IO.FileSystemEventArgs)
		notifyIcon1.ShowBalloonTip(20000, Now.ToString & ". Обнаружен новый файл", e.Name, ToolTipIcon.Info)
	    'notifyIcon1.BalloonTipTitle = Now.ToString & ". Обнаружен новый файл"
		'NotifyIcon1.BalloonTipText = e.Name
		AddLogEntry (String.Format("{0} + {1}",Now.ToString, e.Name))
		MainTimerStart
	End Sub	
	
	Sub FileDeleted(sender As Object, e As System.IO.FileSystemEventArgs)
		notifyIcon1.ShowBalloonTip(20000, Now.ToString & ". Удален файл", e.Name, ToolTipIcon.Info)
		'notifyIcon1.BalloonTipTitle = Now.ToString & ". Удален файл"
		'NotifyIcon1.BalloonTipText = e.Name
		AddLogEntry (String.Format("{0} - {1}",Now.ToString, e.Name))
		MainTimerStart()
	End Sub
	
	Sub RefreshList()
		listView1.Groups.Clear
		listView1.Items.Clear
		listView1.BeginUpdate
		GetFileList (fswEmergency.Path, "Emergency")
		GetFileList (fswDiasoft.Path, "GP Auto IL Diasoft")
		GetFileList (fswProfile.Path, "IL Profile")
		GetFileList (fswOffline.Path, "Offline")
		'listView1.Columns(0).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
		'listView1.Columns(1).AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent)
		listView1.EndUpdate
		' notifyIcon1.ShowBalloonTip(20000)
		Me.Visible=True		
	End Sub
	
	Sub GetFileList(Path As String, GroupName As String)
		Dim dir As New IO.DirectoryInfo(path)
		Dim lvg As ListViewGroup
		Dim lvi As ListViewItem
		lvg = listView1.Groups.Add(GroupName, GroupName)
		'lvg.
		Debug.WriteLine(Path, GroupName)
		for each files As IO.FileInfo in dir.GetFiles()
			Debug.WriteLine(files.Name)
			'MsgBox (IO.Path.GetExtension(files.Name))
			If IO.Path.GetExtension(files.Name) = ".xml" Then
				Dim rc As Long = GetXMLRecordCount(files.FullName)
				Dim fff As String = IO.Path.Combine(IO.path.GetDirectoryName(files.FullName),"Архив\" & IO.Path.GetFileName(files.FullName))
				If IO.File.Exists(fff) Then
					lvi=listView1.Items.Add(files.FullName, files.Name, "page_white_code_red.png")
					lvi.Checked = False
					'lvi.ForeColor = Color.Silver
					lvi.ToolTipText = "Файл уже отправлялся ранее"
				ElseIf rc = 0
					lvi=listView1.Items.Add(files.FullName, files.Name, "page_white_code_red.png")
					lvi.Checked = False
					'lvi.ForeColor = Color.Silver
					lvi.ToolTipText = "Файл не содержит данных"					
				Else
					lvi=listView1.Items.Add(files.FullName, files.Name, "page_code.png")
					lvi.Checked = true
				End If
				lvi.SubItems.Add(rc.ToString)
				lvi.Group = lvg
			End If
		Next files
	End Sub
	
	Function GetXMLRecordCount(strXMLfile As String) As Long
		Debug.WriteLine ("len", GetFileSize(strXMLfile).ToString)
		If GetFileSize(strXMLfile) = 0 then
			Return 0
		Else
			Dim ds As New System.Data.DataSet
			ds.ReadXml(strXMLFile)
			Return ds.Tables("letter").Rows.Count
		End If
	End Function

	
	Sub ToolStripMenuItem2Click(sender As Object, e As EventArgs)
		Application.Exit
	End Sub
	
	
	Sub MenuItemShowHideClick(sender As Object, e As EventArgs)
		Me.Visible = Not Me.Visible
	End Sub
	
	Private Function GetFileSize(ByVal MyFilePath As String) As Long
		Dim MyFile As New System.IO.FileInfo(MyFilePath)
		Dim FileSize As Long = MyFile.Length
		Return FileSize
	End Function
	
	
	Sub MainFormResize(sender As Object, e As EventArgs)
		If Me.WindowState = FormWindowState.Minimized Then
			Me.WindowState=FormWindowState.Normal
			Me.Visible=False
		End If
	End Sub
	
	Sub NotifyIcon1DoubleClick(sender As Object, e As EventArgs)
		Me.WindowState=FormWindowState.Normal		
		Me.Visible = Not Me.Visible
	End Sub
	
	Sub MainFormShown(sender As Object, e As EventArgs)
		Me.Hide
	End Sub
	
	
	
	Public Sub SendToClipboard (ByVal ListViewObj As ListView)  
		Dim ListItemObj As ListViewItem ' MSComctlLib.ListItem  
	  	Dim ListSubItemObj As ListViewItem.ListViewSubItem 'MSComctlLib.ListSubItem  
	  	'Dim ColumnHeaderObj As  ListViewItem 'MSComctlLib.ColumnHeader  
	  	Dim ClipboardText As String  
	  	Dim ClipboardLine As String  
	  
	   	ClipboardText=""
		'  '
		'  ' копирование заголовков:
		'  For Each ColumnHeaderObj In _  
		'      ListViewObj.ColumnHeaders  
		'    If ColumnHeaderObj.Index = 1 Then  
		'      ClipboardText = ColumnHeaderObj.Text   
		'    Else  
		'      ClipboardText = ClipboardText & _  
		'         vbTab & ColumnHeaderObj.Text   
		'    End If
		'  Next
	  	' содержимое колонок: 
	  	For Each ListItemObj In ListViewObj.Items
	  		'ClipboardText = ClipboardText & IIf(ClipboardText="","", vbCrLf)
	  		ClipboardLine = "" ' ListItemObj.Text
	    	' содержимое подчиненных элементов
	    	If ListItemObj.Checked Then
	    		For Each ListSubItemObj In ListItemObj.SubItems
	    			ClipboardLine = ClipboardLine + IIf(ClipboardLine="","", vbTab) + ListSubItemObj.Text
	    		Next  
	    		ClipboardText = ClipBoardText + IIf(ClipBoardText="","", vbCrLf) + ClipboardLine
	    	End If
		Next  
		If ClipboardText <> "" Then
			ClipBoard.Clear
			Clipboard.SetText (ClipboardText,TextDataFormat.Text)
			notifyIcon1.ShowBalloonTip(5000,"Скопировано в буфер", ClipboardText, ToolTipIcon.Info)
		Else	
			notifyIcon1.ShowBalloonTip(1000,":-(","Нечего копировать", ToolTipIcon.Warning)
		End If
	End Sub 
	
	Sub BtnCopyClick(sender As Object, e As EventArgs)
		SendToClipboard(Me.listView1)	
	End Sub
	
	Sub ListView1DragDrop(sender As Object, e As DragEventArgs)
		statusStrip1.Text = "DragDrop"
        Dim NewNode As TreeNode
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", False) Then
            Dim pt As Point
            Dim DestinationNode As TreeNode
            pt = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
            DestinationNode = CType(sender, TreeView).GetNodeAt(pt)
            NewNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), _
                                            TreeNode)
            If Not DestinationNode.TreeView Is NewNode.TreeView Then
                DestinationNode.Nodes.Add(NewNode.Clone)
                DestinationNode.Expand()
                'Remove original node
                NewNode.Remove()
            End If
        End If
	End Sub
	
	Sub ListView1DragEnter(sender As Object, e As DragEventArgs)
		statusStrip1.Text = "DragEnter"
	End Sub
	
	Sub ListView1DragLeave(sender As Object, e As EventArgs)
		statusStrip1.Text = "DragLeave"
	End Sub
	
	Sub ListView1DragOver(sender As Object, e As DragEventArgs)
		statusStrip1.Text = "DragOver"
	End Sub
	
	Sub ListView1GiveFeedback(sender As Object, e As GiveFeedbackEventArgs)
		statusStrip1.Text = "GiveFeedBack"
	End Sub
	
	Sub ListView1QueryContinueDrag(sender As Object, e As QueryContinueDragEventArgs)
'		statusStrip1.Text = "QueryContinueDrag"
'		'ESC pressed
'        If e.EscapePressed Then
'            e.Action = DragAction.Cancel
'            Return
'        End If
'        'Drop!
'        If e.KeyState = 0 Then
'            dataObj.SetData(ShellClipboardFormats.CFSTR_INDRAGLOOP, 0)
'            e.Action = DragAction.Drop
'            Return
'        End If
'        e.Action = DragAction.Continue
	End Sub
	
	Sub ListView1ItemDrag(sender As Object, e As ItemDragEventArgs)
		statusStrip1.Text = "ItemDrag"
		DoDragDrop(e.Item, DragDropEffects.Move)
	End Sub

	Sub Timer1Tick(sender As Object, e As EventArgs)
		Timer1T
	End Sub
	
	Sub Timer1T
		timer1.Stop()
		timer2.Stop()
		StatusLabel1.Text = "Подсчет..."
		statusStrip1.Refresh
		t2 = 0
		RefreshList()
		StatusLabel1.Text = ""
		statusStrip1.Refresh
	End Sub
	
	Sub Timer2Tick(sender As Object, e As EventArgs)
		t2 = t2 + 1
		StatusLabel1.Text = "Ожидание... " & FormatDateTime(TimeSerial(0,0,((timer1.Interval/1000)-t2)), DateFormat.LongTime).ToString
		statusStrip1.Refresh
	End Sub
	
	Sub StatusStrip1DoubleClick(sender As Object, e As EventArgs)
		Timer1T
	End Sub
	
	Sub ListView1MouseDown(sender As Object, e As MouseEventArgs)
		
	End Sub
	
	Sub BtnCompressClick(sender As Object, e As EventArgs)
		Dim lstFileNames As New List(Of [String])()
		Dim s As Long
		Dim strDate As String = Now.Year.ToString & Now.Month.ToString & Now.Day.ToString
		
		For Each lvi As ListViewItem In listView1.Items
			If lvi.Checked Then
				lstFileNames.Add(lvi.Name)
				s = s + CLng(lvi.SubItems(1).Text)
			End If
		Next
		
		Dim strTmpfileName As String = System.IO.Path.GetTempFileName
		Using writer As StreamWriter = System.IO.File.CreateText(strTmpfileName)
			writer.Write(Strings.Join(lstFileNames.ToArray, vbCrLf))
		End Using
		
		Dim strArchName As String = Path.Combine(Path.GetDirectoryName(fswOffline.Path), strDate & "_графики_платежей=" & s.ToString & ".zip")
		Dim strArguments As String = String.Format(" a -tzip -mx9 -- ""{0}"" @""{1}""", strArchName, strTmpfileName)
		' CompressFiles(lstFileNames.ToArray, strArchName & "+",0)
		
		' < Запуск архиватора>
		Me.TopMost = False
		Me.WindowState = FormWindowState.Minimized
		Dim procEC As Integer
		procEC = RunExe(ArchExe, strArguments)
		Debug.WriteLine(ArchExe & " " & strArguments, procEC.ToString)
		Me.WindowState = FormWindowState.Normal
		Me.TopMost = True
		' </Запуск архиватора>
		' < Шифрование архива>
		Dim strEncryptedArchName As String = CryptoArm_EncryptFile(strArchName, "ТМ-Стрим", True, True)
		If strEncryptedArchName <> "" Then
			SendEmail(strDate, s, strEncryptedArchName)
		End If
		' </Шифрование архива>
		' < Если выполнение удачно, то перенос файлов в папку Архив>
		If procEC = 0 Then
			For Each strFileName As String In lstFileNames
				IO.File.Move(strFileName, _
						 	 IO.Path.Combine(IO.Path.GetDirectoryName(strFileName), _
							 IO.Path.Combine("Архив\", IO.Path.GetFileName(strFileName))))
			Next
		End If
		' </Если выполнение удачно, то перенос файлов в папку Архив>
	End Sub
	
	Function RunExe(strExename As String, strArguments As String) As Integer
		Dim procID As Integer
		Dim newProc As Diagnostics.Process
		newProc = Diagnostics.Process.Start(strExename, strArguments)
		procID = newProc.Id
		newProc.WaitForExit()
		Dim procEC As Integer = -1
		If newProc.HasExited Then
    		procEC = newProc.ExitCode
		End If
		Return procEC
	End Function

	
	
	Sub SendEmail(strDate As String, lngQty As Long, strAttachFileName As String)
		Dim olApp As New Microsoft.Office.Interop.Outlook.Application
		Dim m As Microsoft.Office.Interop.Outlook.MailItem
		m = olApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem)
		m.To = "" 'korneychuk@directoria.su"
		m.CC = IIf(GetUserName()="pnosov", "opavlova5@rencredit.ru", "pnosov@rencredit.ru")
		m.Subject = String.Format("Выгружены файлы для печати и отправки графиков платежей {0} ({1} шт.)", strDate, lngQty)
		m.Attachments.Add(strAttachFileName)
		m.Display(True)
	End Sub
	
	
	Function GetUserName() As String
	    If TypeOf My.User.CurrentPrincipal Is 
	      Security.Principal.WindowsPrincipal Then
	        ' The application is using Windows authentication.
	        ' The name format is DOMAIN\USERNAME.
	        Dim parts() As String = Split(My.User.Name, "\")
	        Dim username As String = parts(1)
	        Return username
	    Else
	        ' The application is using custom authentication.
	        Return My.User.Name
	    End If
	End Function	
	
	

'Private Sub CompressFiles(files As String(), outPathname As String, Optional folderOffset As Integer = 0, Optional strPassword As String = "")
'    Dim fsOut As FileStream = File.Create(outPathname)
'    Dim zipStream As New ZipOutputStream(fsOut)
'
'    
'    zipStream.SetLevel(9)       '0-9, 9 being the highest level of compression
'    zipStream.Password = strPassword   ' optional. Null is the same as not setting.
'    
'' ----------------------    
'    For Each filename As String In files
'
'        Dim fi As New FileInfo(filename)
'        Dim entryName As String = filename.Substring(folderOffset)  ' Makes the name in zip based on the folder
'        entryName = ZipEntry.CleanName(entryName)       ' Removes drive from name and fixes slash direction
'        Dim newEntry As New ZipEntry(entryName)
'        newEntry.DateTime = fi.LastWriteTime            ' Note the zip format stores 2 second granularity
'
'        ' Specifying the AESKeySize triggers AES encryption. Allowable values are 0 (off), 128 or 256.
'        '   newEntry.AESKeySize = 256;
'
'        ' To permit the zip to be unpacked by built-in extractor in WinXP and Server2003, WinZip 8, Java, and other older code,
'        ' you need to do one of the following: Specify UseZip64.Off, or set the Size.
'        ' If the file may be bigger than 4GB, or you do not need WinXP built-in compatibility, you do not need either,
'        ' but the zip will be in Zip64 format which not all utilities can understand.
'        zipStream.UseZip64 = UseZip64.Off
'        newEntry.Size = fi.Length
'
'        zipStream.PutNextEntry(newEntry)
'
'        ' Zip the file in buffered chunks
'        ' the "using" will close the stream even if an exception occurs
'        Dim buffer As Byte() = New Byte(4095) {}
'        Using streamReader As FileStream = File.OpenRead(filename)
'            StreamUtils.Copy(streamReader, zipStream, buffer)
'        End Using
'        zipStream.CloseEntry()
'    Next
''    Dim folders As String() = Directory.GetDirectories(path)
''    For Each folder As String In folders
''        CompressFolder(folder, zipStream, folderOffset)
''    Next
'' -----------------
'    zipStream.IsStreamOwner = True
'    ' Makes the Close also Close the underlying stream
'    zipStream.Close()
'End Sub

	
	
	
	
	
End Class
