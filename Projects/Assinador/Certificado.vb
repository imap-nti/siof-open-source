Imports System.Security.Cryptography
Imports System.Security.Cryptography.Xml
Imports System.Security.Cryptography.X509Certificates
Imports System.Xml

Imports System.Security.Cryptography.Pkcs

Public Class Certificado

    Public uriTxt As String = ""

    Public mostrarData As Boolean = False

    Private licenciou As Boolean = False

    Public licensa As String = ""
    Private q4234fsdf23fd As String = "695dfw683451s6d8f4$$@!3468t1vb844536454g1f#$%&¨¨#$8f+9sdf"

    Public dataAtual As DateTime = DateTime.Now 'permite que seja passado a dataAtual como parametro
    Public pedirSenhaApenasUmaVez As Boolean = False

    Private certificado As X509Certificate2 = Nothing
    Private cadeia As X509Chain = Nothing

    Private key As KeyManager

    Private keyUsage As Int32


    Private bDecipherOnly As Boolean = False
    Private bDigitalSignature As Boolean = False
    Private bNonRepudiation As Boolean = False
    Private bKeyEncipherment As Boolean = False
    Private bDataEncipherment As Boolean = False
    Private bKeyAgreement As Boolean = False
    Private bKeyCertSign As Boolean = False
    Private bCrlSign As Boolean = False
    Private bEncipherOnly As Boolean = False

    Private RepositorioAssinatura As RepositorioSerializavel

    Public Function RetornaCertificado() As X509Certificate2
        Return certificado
    End Function

    Public Function CarregaCertificadoAC() As X509Certificate2

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        Dim store As New X509Store(StoreName.Root, StoreLocation.CurrentUser)
        store.Open(OpenFlags.ReadOnly)
        Dim collection As X509Certificate2Collection = CType(store.Certificates, X509Certificate2Collection)
        Dim fcollection As X509Certificate2Collection = CType(collection.Find(X509FindType.FindByTimeValid, dataAtual, False), X509Certificate2Collection)
        Dim scollection As X509Certificate2Collection = X509Certificate2UI.SelectFromCollection(fcollection, "Selecione o certificado", "Selecione o certificado da lista para ver informações", X509SelectionFlag.SingleSelection)

        If scollection.Count > 0 Then
            Return scollection.Item(0)
        End If

        Return Nothing
    End Function

    Public Sub CarregaCertificado()

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        Try
            Dim store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
            store.Open(OpenFlags.ReadOnly Or OpenFlags.OpenExistingOnly)
            Dim collection As X509Certificate2Collection = CType(store.Certificates, X509Certificate2Collection)
            Dim fcollection As X509Certificate2Collection = CType(collection.Find(X509FindType.FindByTimeValid, dataAtual, False), X509Certificate2Collection)
            Dim scollection As X509Certificate2Collection = X509Certificate2UI.SelectFromCollection(fcollection, "Selecione o certificado", "Selecione o certificado da lista para ver informações", X509SelectionFlag.SingleSelection)

            If scollection.Count > 0 Then
                certificado = scollection.Item(0)
                keyUsage = pegaKeyUsage(certificado)
                pegaTipo()
            End If

            'Console.WriteLine("Quantidade de certificados: {0}{1}", scollection.Count, Environment.NewLine)

            'For Each certificado In scollection
            'Dim rawdata As Byte() = certificado.RawData
            'Console.WriteLine("Content Type: {0}{1}", X509Certificate2.GetCertContentType(rawdata), Environment.NewLine)
            'Console.WriteLine("Friendly Name: {0}{1}", certificado.FriendlyName, Environment.NewLine)
            'Console.WriteLine("Certificate Verified?: {0}{1}", certificado.Verify(), Environment.NewLine)
            'Console.WriteLine("Simple Name: {0}{1}", certificado.GetNameInfo(X509NameType.SimpleName, True), Environment.NewLine)
            'Console.WriteLine("Signature Algorithm: {0}{1}", certificado.SignatureAlgorithm.FriendlyName, Environment.NewLine)
            'Console.WriteLine("Private Key: {0}{1}", certificado.PrivateKey.ToXmlString(False), Environment.NewLine)
            'Console.WriteLine("Public Key: {0}{1}", certificado.PublicKey.Key.ToXmlString(False), Environment.NewLine)
            'Console.WriteLine("Certificate Archived?: {0}{1}", certificado.Archived, Environment.NewLine)
            'Console.WriteLine("Length of Raw Data: {0}{1}", certificado.RawData.Length, Environment.NewLine)
            'X509Certificate2UI.DisplayCertificate(certificado)
            'certificado.Reset()
            'Next certificado
            store.Close()

            If Not certificado Is Nothing Then
                key = New KeyManager(certificado)
            End If

        Catch cExcept As Exception
            MsgBox(cExcept.Message, MsgBoxStyle.Critical)

            Console.WriteLine("Information could not be written out for this certificate.")
        End Try

    End Sub

    'valida toda cadeia
    Public Function ValidarCertificado() As Boolean

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        If (key Is Nothing) Then
            Throw New ArgumentException("O KeyManager Provider está vazio.")
        End If

        Dim oX509Certificate As X509Certificate2 = certificado

            cadeia = New X509Chain

            cadeia.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain 'Valida toda cadeia
            cadeia.ChainPolicy.RevocationMode = X509RevocationMode.Online 'valida online
            cadeia.ChainPolicy.UrlRetrievalTimeout = New TimeSpan(1, 0, 0)
            cadeia.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag
            cadeia.Build(oX509Certificate)

            Dim erro As String = ""
            For i As Int32 = 0 To cadeia.ChainStatus.Length - 1
                erro &= "Erro: " + cadeia.ChainStatus(i).StatusInformation.ToString & "/ " & cadeia.ChainStatus(i).Status.ToString & ""
            Next

            If (erro <> "") Then
                Throw New ArgumentException(erro)
                Return False
            End If

            Return True

    End Function

    'Verifica se o certificado informado faz parte da cadeia raiz passado como parametro
    Public Function ValidarCadeia(ByVal ac As List(Of X509Certificate2)) As Boolean

        Dim certificadoTemp As X509Certificate2

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        If cadeia Is Nothing Then
            Throw New ArgumentException("A cadeia do certificado ainda não foi carregada." & vbNewLine & "Para carregar a cadeia é necessário validar o certificado.")
            Return False
        Else


            Dim cert1 As X509Certificate2
            Dim cert2 As X509Certificate2
            Dim cert3 As X509Certificate2
            Dim cert4 As X509Certificate2

            Try
                cert1 = cadeia.ChainElements(0).Certificate
                cert2 = cadeia.ChainElements(1).Certificate
                cert3 = cadeia.ChainElements(2).Certificate
                cert4 = cadeia.ChainElements(3).Certificate 'AC Raiz
            Catch ex As Exception
                Throw New ArgumentException("O certificado informado está com a cadeia incompleta.")
                Return False
            End Try

            For i As Int32 = 0 To ac.Count - 1
                certificadoTemp = ac(i)

                If cert4.Thumbprint = certificadoTemp.Thumbprint Then
                    RepositorioAssinatura = New RepositorioSerializavel

                    'RepositorioAssinatura.certificado1 = cert1.Export(X509ContentType.SerializedCert)
                    'RepositorioAssinatura.certificado2 = cert2.Export(X509ContentType.SerializedCert)
                    'RepositorioAssinatura.certificado3 = cert3.Export(X509ContentType.SerializedCert)
                    'RepositorioAssinatura.certificado4 = cert4.Export(X509ContentType.SerializedCert)

                    Try
                        RepositorioAssinatura.LCRcertificado1 = BaixarLCR(pegaUrlLCR(cert1))
                    Catch ex As Exception

                    End Try

                    Try
                        RepositorioAssinatura.LCRcertificado2 = BaixarLCR(pegaUrlLCR(cert2))
                    Catch ex As Exception

                    End Try
                    Try
                        RepositorioAssinatura.LCRcertificado3 = BaixarLCR(pegaUrlLCR(cert3))
                    Catch ex As Exception

                    End Try
                    Try
                        RepositorioAssinatura.LCRcertificado4 = BaixarLCR(pegaUrlLCR(cert4))
                    Catch ex As Exception

                    End Try


                    Return True
                End If
            Next
        End If
        Return False

    End Function

    Public Sub XmlDsign_Sign2()

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        Dim sha As SHA1 = New SHA1CryptoServiceProvider()
        Dim hash As Byte()
        hash = sha.ComputeHash(certificado.RawData)
    End Sub

    Public Sub XmlDsign_SignXml(ByRef Doc As XmlDocument)

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        If (Doc Is Nothing) Then
            Throw New ArgumentException("O XML está vazio.")
        End If
        If (key Is Nothing) Then
            Throw New ArgumentException("O KeyManager Provider está vazio.")
        End If

        If Not pedirSenhaApenasUmaVez Then
            key = New KeyManager(certificado)
        End If


        Dim signedXml As SignedXml = New SignedXml(Doc)

        'Dim dataObject As DataObject = New DataObject()
        'dataObject.Data = Doc.ChildNodes
        'dataObject.Id = "CONTENT"
        'signedXml.AddObject(dataObject)

        ' Passamos a chave
        signedXml.KeyInfo = New KeyInfo()

        Dim x509data = New KeyInfoX509Data(certificado)
        x509data.AddSubjectName(certificado.SubjectName.Name.ToString())
        signedXml.KeyInfo.AddClause(x509data)

        signedXml.KeyInfo.AddClause(New RSAKeyValue(key.KeyProvider))

        signedXml.SigningKey = key.KeyProvider

        Dim xmlDsigUrl As String = signedXml.XmlDsigNamespaceUrl

        If mostrarData Then
            Dim elem As XmlElement = Doc.CreateElement("SigningTime", xmlDsigUrl)
            elem.InnerText = dataAtual.ToString()
            Dim DataObject As DataObject = New DataObject(Nothing, Nothing, Nothing, elem)
            signedXml.AddObject(DataObject)
        End If

        ' Criamos uma referencia para a assinatura
        Dim reference As Reference = New Reference()
        reference.Uri = uriTxt
        reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1"

        ' Adicionamos uma transformação enveloped à referencia.
        Dim env As XmlDsigEnvelopedSignatureTransform = New XmlDsigEnvelopedSignatureTransform()
        reference.AddTransform(env)

        'Adicionamos a referência ao objecto SignedXml.
        signedXml.AddReference(reference)

        'Assinamos.
        signedXml.ComputeSignature()

        'Extraimos a representação da assinatura em XML
        Dim xmlDigitalSignature As XmlElement = signedXml.GetXml()

        'Juntamos a assinatura XML ao documento.
        Doc.DocumentElement.AppendChild(Doc.ImportNode(xmlDigitalSignature, True))

    End Sub


    Public Function XmlDsign_VerificarAssinatura(ByVal Doc As XmlDocument) As Boolean

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        If (Doc Is Nothing) Then
            Throw New ArgumentException("O XML está vazio.")
        End If
        If (key Is Nothing) Then
            Throw New ArgumentException("O KeyManager Provider está vazio.")
        End If

        If Not pedirSenhaApenasUmaVez Then
            key = New KeyManager(certificado)
        End If

        'Instanciação/Inicialização
        Dim signedXml As SignedXml = New SignedXml(Doc)

        'Neste ponto vamos verificar quantos elementos (ASSINATURAS) possui o ficheiro XML.
        Dim nodeList As XmlNodeList = Doc.GetElementsByTagName("Signature")

        'O documento XML não possui nenhuma assinatura!!
        'Decidimos lançar uma excepção .
        If (nodeList.Count <= 0) Then
            Throw New CryptographicException("Erro: Nenhuma assinatura foi encontrada.")
        End If

        'O documento XML possui mais que uma assinatura!!
        'No contexto actual, não possuimos KnowHow suficiente para tratar esta variante, 
        'pois queremos verificar uma relação de 1 para 1, ou seja, Um documento Assinado por uma chave.
        'Decidimos lançar uma excepção .

        If (nodeList.Count >= 2) Then
            Throw New CryptographicException("Erro: Mais de uma assinatura foi encontrada no documento.")
        End If

        'Ok, se chegamos ate aqui, então estamos prontos para verificar a assinatura.
        'Carregamos o Elemento XML que contém a assinatura para o respectivo objecto 
        'que faz a verfificação.
        signedXml.LoadXml(nodeList(0))

        'Retornamos o resultado da verificação.
        Return signedXml.CheckSignature(key.KeyProvider)

    End Function

    Public Sub CMS_Assinar(ByVal arquivo As String)

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        '//Sign the Message
        Dim msgBytes As Byte() = IO.File.ReadAllBytes(arquivo)
        Dim encodedSignedCms As Byte() = CMS_SignMesage(msgBytes)
        '//Encrypt the Message
        '//Encrypt the encoded SignedCms message.
        Dim encodedEnvelopedCms As Byte() = CMS_EncryptMsg(encodedSignedCms)
        '//Send
        Dim encryptedMessage As String = Convert.ToBase64String(encodedEnvelopedCms)

        Dim chave As String = Convert.ToBase64String(encodedSignedCms)

        Dim arquivoC As IO.StreamWriter = IO.File.CreateText("c:\chave_cms.p7s")
        arquivoC.Write(chave)
        arquivoC.Close()
        arquivoC.Dispose()

        Dim arquivoG As IO.StreamWriter = IO.File.CreateText("c:\teste_cms.txt")
        arquivoG.Write(encryptedMessage)
        arquivoG.Close()
        arquivoG.Dispose()
    End Sub

    Private Function CMS_SignMesage(ByVal data As Byte()) As Byte()

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        '//  Place message in a ContentInfo object.
        '//  This is required to build a SignedCms object.
        Dim contentInfo As ContentInfo = New ContentInfo(data)

        '//  Instantiate SignedCms object with the ContentInfo above.
        '//  Has default SubjectIdentifierType IssuerAndSerialNumber.
        '//  Has default Detached property value false, so message is
        '//  included in the encoded SignedCms.


        Dim signedCms As SignedCms = New SignedCms(contentInfo)

        '//  Formulate a CmsSigner object, which has all the needed
        '//  characteristics of the signer.
        Dim cmsSigner As CmsSigner = New CmsSigner(certificado)

        '//  Sign the PKCS #7 message.
        signedCms.ComputeSignature(cmsSigner)
        '//  Encode the PKCS #7 message.

        Return signedCms.Encode()

    End Function

    Private Function CMS_EncryptMsg(ByVal message As Byte()) As Byte()

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        '//  Place message in a ContentInfo object.
        '//  This is required to build an EnvelopedCms object.
        Dim contentInfo As ContentInfo = New ContentInfo(message)

        '//  Instantiate EnvelopedCms object with the ContentInfo
        '//  above.
        '//  Has default SubjectIdentifierType IssuerAndSerialNumber.
        '//  Has default ContentEncryptionAlgorithm property value
        '//  RSA_DES_EDE3_CBC.
        Dim envelopedCms As EnvelopedCms = New EnvelopedCms(contentInfo)

        '//  Formulate a CmsRecipient object that
        '//  represents information about the recipient
        '//  to encrypt the message for.
        Dim recip1 As CmsRecipient = New CmsRecipient(SubjectIdentifierType.IssuerAndSerialNumber, certificado)

        '//  Encrypt the message for the recipient.
        envelopedCms.Encrypt(recip1)

        '//  The encoded EnvelopedCms message contains the encrypted
        '//  message and the information about each recipient that
        '//  the message was enveloped for.
        Return envelopedCms.Encode()
    End Function

    Public Function CMS_VerificarAssinatura(ByVal arquivo As String) As Boolean

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        Dim data As Byte() = IO.File.ReadAllBytes(arquivo)

        Dim signedCms As SignedCms = New SignedCms()
        signedCms.Decode(data)

        Try
            signedCms.CheckSignature(True)
        Catch e As System.Security.Cryptography.CryptographicException
            Return False
        End Try

        Return True

    End Function

    Public Sub CMS_DecryptMsg(ByVal infile As String, ByVal outfile As String)

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        Dim data As Byte() = IO.File.ReadAllBytes(infile)

        Dim envCms As EnvelopedCms = New EnvelopedCms()
        envCms.Decode(data)
        envCms.Decrypt()

        Dim arquivoG As IO.StreamWriter = IO.File.CreateText(outfile)
        arquivoG.Write(envCms.ContentInfo.Content)
        arquivoG.Close()
        arquivoG.Dispose()

    End Sub

    Public Function RetornaRepositorioSerializavel() As RepositorioSerializavel
        Return RepositorioAssinatura
    End Function

    Private Function pegaKeyUsage(ByVal c As X509Certificate2) As Int16

        Dim extension As X509Extension
        For Each extension In c.Extensions
            Console.WriteLine(extension.Oid.FriendlyName + "(" + extension.Oid.Value + ")")

            If extension.Oid.Value = "2.5.29.15" Then 'Key Usage
                Dim ext As X509KeyUsageExtension = CType(extension, X509KeyUsageExtension)
                Return ext.KeyUsages
            End If

        Next extension

        Return 0

    End Function

    Private Sub pegaTipo()

        Dim DecipherOnly As Int32 = 32768
        Dim DigitalSignature As Int32 = 128
        Dim NonRepudiation As Int32 = 64
        Dim KeyEncipherment As Int32 = 32
        Dim DataEncipherment As Int32 = 16
        Dim KeyAgreement As Int32 = 8
        Dim KeyCertSign As Int32 = 4
        Dim CrlSign As Int32 = 2
        Dim EncipherOnly As Int32 = 1
        Dim None As Int32 = 0

        Dim valor As Int32 = keyUsage

        If valor >= DecipherOnly Then
            bDecipherOnly = True
            valor = valor - DecipherOnly
        Else
            bDecipherOnly = False
        End If

        If valor >= DigitalSignature Then
            bDigitalSignature = True
            valor = valor - DigitalSignature
        Else
            bDigitalSignature = False
        End If

        If valor >= NonRepudiation Then
            bNonRepudiation = True
            valor = valor - NonRepudiation
        Else
            bNonRepudiation = False
        End If

        If valor >= KeyEncipherment Then
            bKeyEncipherment = True
            valor = valor - KeyEncipherment
        Else
            bKeyEncipherment = False
        End If

        If valor >= DataEncipherment Then
            bDataEncipherment = True
            valor = valor - DataEncipherment
        Else
            bDataEncipherment = False
        End If

        If valor >= KeyAgreement Then
            bKeyAgreement = True
            valor = valor - KeyAgreement
        Else
            bKeyAgreement = False
        End If

        If valor >= KeyCertSign Then
            bKeyCertSign = True
            valor = valor - KeyCertSign
        Else
            bKeyCertSign = False
        End If

        If valor >= CrlSign Then
            bCrlSign = True
            valor = valor - CrlSign
        Else
            bCrlSign = False
        End If

        If valor >= EncipherOnly Then
            bEncipherOnly = True
            valor = valor - EncipherOnly
        Else
            bEncipherOnly = False
        End If

    End Sub

    Public Function isDecipherOnly() As Boolean
        Return bDecipherOnly
    End Function
    Public Function isDigitalSignature() As Boolean
        Return bDigitalSignature
    End Function
    Public Function isNonRepudiation() As Boolean
        Return bNonRepudiation
    End Function
    Public Function isKeyEncipherment() As Boolean
        Return bKeyEncipherment
    End Function
    Public Function isDataEncipherment() As Boolean
        Return bDataEncipherment
    End Function
    Public Function isKeyAgreement() As Boolean
        Return bKeyAgreement
    End Function
    Public Function isKeyCertSign() As Boolean
        Return bKeyCertSign
    End Function
    Public Function isCrlSign() As Boolean
        Return bCrlSign
    End Function
    Public Function isEncipherOnly() As Boolean
        Return bEncipherOnly
    End Function

    Private Function VerificaLicenca()

        If Not licenciou And licensa.Length > 0 Then
            Dim vetor() As String = Split(licensa, "?")
            Dim resultado1 As String = ""
            Dim resultado2 As String = ""

            If q4234fsdf23fd.Length = vetor(0).Length Then
                For i As Int32 = 0 To q4234fsdf23fd.Length - 1
                    resultado1 &= Asc(q4234fsdf23fd.Chars(i)) + Asc(vetor(0).Chars(i))
                Next

                For i As Int32 = 0 To resultado1.Length - 1 Step 3
                    Dim num1 As Int32 = (resultado1 & "00").Substring(i, 3)
                    While num1 > 255
                        num1 = num1 - 255
                    End While

                    resultado2 &= Convert.ToChar(num1).ToString
                Next
            End If

            If resultado2 = vetor(1) Then
                licenciou = True
            Else
                licenciou = False
            End If
        End If

        Return licenciou
    End Function

    Public Function ValidaCPF(ByVal cpf As String) As Boolean

        cpf = cpf.Replace(",", "")
        cpf = cpf.Replace(".", "")
        cpf = cpf.Replace(" ", "")
        cpf = cpf.Replace("-", "")

        If Not VerificaLicenca() Then Throw New ArgumentException("Licença inválida.")

        If (key Is Nothing) Then
            Throw New ArgumentException("O KeyManager Provider está vazio.")
        End If

        Dim a As X500DistinguishedName

        a = certificado.SubjectName

        Dim vetorDados() As String
        vetorDados = a.Name.Split(",")


        Dim num_cpf As String = ""
        For i As Int32 = 0 To vetorDados.Length - 1
            If (vetorDados(i).IndexOf(":") > -1) Then
                num_cpf = vetorDados(i).Substring(vetorDados(i).IndexOf(":") + 1)
                Exit For
            End If

        Next

        If num_cpf = cpf Then
            Return True
        Else
            Return False
        End If

        Return False
    End Function

    Private Function pegaUrlLCR(ByVal c As X509Certificate2) As String
        Dim link As String = ""
        Dim extension As X509Extension
        For Each extension In c.Extensions
            Console.WriteLine(extension.Oid.FriendlyName + "(" + extension.Oid.Value + ")")

            If extension.Oid.Value = "2.5.29.31" Then '"CRL Distribution Points" Then
                Dim ext As X509Extension = extension

                For i As Int32 = 0 To ext.RawData.Length - 1
                    link &= Chr(ext.RawData(i))
                Next
                link = link.Substring(link.IndexOf(":") - 4)
                Exit For
            End If

        Next extension

        Return link

    End Function

    Private Function BaixarLCR(ByVal url As String) As Byte()
        Dim webClient As New Net.WebClient()
        Dim b As Byte() = webClient.DownloadData(url)

        Return b
    End Function

End Class
