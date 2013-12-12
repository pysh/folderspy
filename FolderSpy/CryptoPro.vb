'
' Сделано в SharpDevelop.
' Пользователь: pnosov
' Дата: 04.12.2013
' Время: 13:13
' 
' Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
'
Public Module CryptoPro
'enum FORMAT
Const BASE64_TYPE As Integer = 0
Const DER_TYPE As Integer = 1

'enim PROFILESTORETYPE (профили)
Const REGISTRY_STORE As Integer = 0
Const XML_STORE As Integer = 1

'enum DATATYPE (тип данных)
Public Const DT_PLAIN_DATA As Integer = 0
Public Const DT_SIGNED_DATA As Integer = 2
Public Const DT_ENVELOPED_DATA As Integer = 3

'enum WIZARD_TYPE and RESULTTYPE
Public Const ENCRYPT_WIZARD_TYPE As Integer = 64
Public Const ADD_SIGN_WIZARD_TYPE As Integer = 2

'enum CHECKING_WIZARD
Public Const ALL_OK As Integer = 0

' Public FSO As New Scripting.FileSystemObject



Public Function CryptoArm_EncryptFile(strFileNameInput As String, _
							 Optional strProfileName As String = "", _
							 Optional boolSignData As Boolean = True, _
							 Optional boolDeleteSignedFile As Boolean = False) As String
    ' в хранилище должен быть профиль с заполненными параметрами ЭЦП
    Dim f As Integer
    Dim strFileNameSigned As String
    Dim strFileNameEncrypted As String
    Dim objProfileStore As New DigtCryptoLib.ProfileStore
    Dim CheckResult As Integer    
    CryptoArm_EncryptFile = ""
    objProfileStore.Open (REGISTRY_STORE)  'Открываем хранилище профилей
    Dim objProfiles As DigtCryptoLib.Profiles = objProfileStore.Store 'Получаем коллекцию профилей
    
    
    If strProfileName = "" Then strProfileName = objProfiles.DefaultProfile.Name
    f = GetProfileIndexByName(objProfiles, strProfileName)
    If f > -1 Then
        Dim objProfile As DigtCryptoLib.Profile = objProfiles.Item(f)
        'Приступим к получению подписи, используя данные, полученные из профиля
        ' objProfile.CollectData SIGN_WIZARD_TYPE 'Запустим мастер подписи для сбора данных
        CheckResult = objProfile.CheckData(DigtCryptoLib.WIZARD_TYPE.ADD_SIGN_WIZARD_TYPE)  'Проверим, все ли данные собраны
        If CheckResult = ALL_OK Then
            Dim oPKCS7Message As New DigtCryptoLib.PKCS7Message
	        oPKCS7Message.Profile = objProfile 'Установим профиль с настройками
	        If boolSignData Then
		    	strFileNameSigned = strFileNameInput & "." & objProfile.SignatureExtension(objProfile.SignExitFormat)
		        oPKCS7Message.Load (DigtCryptoLib.DATA_TYPE.DT_PLAIN_DATA, CStr(strFileNameInput))  'Загрузим исходные данные
		        oPKCS7Message.Sign  'Подпишем данные, используя параметры подписи из профиля
		        oPKCS7Message.Save (DigtCryptoLib.DATA_TYPE.DT_SIGNED_DATA, objProfile.SignExitFormat, strFileNameSigned) 'Сохраним данные
	        Else
	            strFileNameSigned = strFileNameInput
			End If
            strFileNameEncrypted = strFileNameSigned & "." & objProfile.EncryptedExtension(objProfile.EncryptExitFormat)
            oPKCS7Message.Load (DigtCryptoLib.DATA_TYPE.DT_PLAIN_DATA, CStr(strFileNameInput))  'Загрузим исходные данные
            oPKCS7Message.Sign  'Зашифруем данные, используя параметры подписи из профиля
            oPKCS7Message.Save (DigtCryptoLib.DATA_TYPE.DT_SIGNED_DATA, objProfile.EncryptExitFormat, strFileNameEncrypted) 'Сохраним данные
            CryptoArm_EncryptFile = strFileNameEncrypted
            If (boolDeleteSignedFile) AndAlso (strFileNameInput <> strFileNameSigned) Then
            	IO.File.Delete (strFileNameSigned)
            End If
        Else
            MsgBox ("Профиль некорректно заполнен")
        End If
    End If
End Function

Function GetProfileIndexByName(objProfiles As DigtCryptoLib.Profiles, strProfileName As String) As Integer
    Dim f As Integer
    f = -1
    For f = 0 To objProfiles.Count - 1
        If objProfiles.Item(f).Name = strProfileName Then
            GetProfileIndexByName = f
            Exit For
        End If
    Next f
End Function
End Module
