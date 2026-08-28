Imports System
Imports System.Collections.Generic
Imports System.Text

Imports System.Security.Cryptography
Imports System.Security.Cryptography.Xml
Imports System.Security.Cryptography.X509Certificates


Public Class KeyManager

    Public Shared KeySize As Int32 = 1024

    Private m_rsa As RSACryptoServiceProvider = Nothing  '// Provider RSA 

    Public Function KeyProvider() As RSACryptoServiceProvider     '// Propriedade que exporta o provider
        Return m_rsa
    End Function


    '// Inicializa o Provider com uma chave de tamanho igual à KeySize
    Public Sub New()
        m_rsa = New RSACryptoServiceProvider(KeySize)
    End Sub

    '// Inicializa o Provider com um ficheiro
    Public Sub New(ByVal filename As String)

        m_rsa = New RSACryptoServiceProvider(KeySize)
        _loadKey(m_rsa, filename)
    End Sub

    '// Inicializa o Provider com um ficheiro XML de chave public e um ficheiro XML de chave privada
    Public Sub New(ByVal publicKeyFilename As String, ByVal privateKeyFilename As String)

        m_rsa = New RSACryptoServiceProvider(KeySize)
        LoadPublicKeyFromXmlFile(publicKeyFilename)
        LoadPrivateKeyFromXmlFile(privateKeyFilename)
    End Sub

    '// Inicializa o Provider com um certificado 
    Public Sub New(ByVal certificate As X509Certificate2)

        If (certificate Is Nothing) Then
            Throw New Exception("Certificado não inicializado") 'Exceptions.KeyManagerException("certificate not initialized")
        End If

        If (certificate.HasPrivateKey) Then
            m_rsa = certificate.PrivateKey
        Else
            Throw New Exception("Certificado não possui Chave privada") 'Exceptions.KeyManagerException("certificate does not contains the PrivateKey ")
        End If
    End Sub

    '// Exporta a chave privada para um ficheiro XML
    Public Sub SavePrivateKeyToXmlFile(ByVal filename As String, ByVal overwrite As Boolean)
        _saveKey(m_rsa, filename, overwrite, True)
    End Sub

    '// Exporta a chave publica para um ficheiro XML
    Public Sub SavePublicKeyToXmlFile(ByVal filename As String, ByVal overwrite As Boolean)
        _saveKey(m_rsa, filename, overwrite, False)
    End Sub

    Private Sub _saveKey(ByVal rsa As RSACryptoServiceProvider, ByVal filename As String, ByVal overwrite As Boolean, ByVal privateKey As Boolean)
        If (rsa Is Nothing) Then
            Throw New Exception("Service Provider NULL Exception") 'Exceptions.ServiceProviderException("Service Provider NULL Exception")
        End If

        If (System.IO.File.Exists(filename) And Not overwrite) Then
            Return
        End If

        Dim Key As String = rsa.ToXmlString(privateKey)

        Dim sw As System.IO.StreamWriter = New System.IO.StreamWriter(filename, False, Encoding.UTF8)
        Try
            sw.WriteLine(Key)
            sw.Close()
        Finally
            sw.Dispose()
        End Try

    End Sub


    '// Carrega uma chave privada de um ficheiro XML
    Public Sub LoadPrivateKeyFromXmlFile(ByVal filename As String)
        _loadKey(m_rsa, filename)
    End Sub

    '// Carrega uma chave publica de um ficheiro XML
    Public Sub LoadPublicKeyFromXmlFile(ByVal filename As String)
        _loadKey(m_rsa, filename)
    End Sub

    Private Sub _loadKey(ByVal rsa As RSACryptoServiceProvider, ByVal filename As String)

        If (rsa Is Nothing) Then
            Throw New Exception("Service Provider NULL Exception") 'Exceptions.ServiceProviderException("Service Provider NULL Exception")
        End If

        If (Not System.IO.File.Exists(filename)) Then
            Throw New System.IO.FileNotFoundException(filename)
        End If

        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(filename, Encoding.UTF8)
        Try
            Dim Key As String = sr.ReadToEnd()
            sr.Close()
            rsa.FromXmlString(Key)
        Finally
            sr.Dispose()
        End Try

    End Sub

End Class