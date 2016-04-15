
<Serializable()>
<Xml.Serialization.XmlRoot("RootTag")>
Public Class XMLTemplate
    Private providerField As String
    <Xml.Serialization.XmlElement("Product")>
    Public Products As List(Of Product)

    Public Property Provider() As String
        Get
            Return Me.providerField
        End Get
        Set(value As String)
            Me.providerField = value
        End Set
    End Property

    Public Class Product
        Private nameField As String
        Private priceField As ProductPrice

        Public Property Name() As String
            Get
                Return Me.nameField
            End Get
            Set(value As String)
                Me.nameField = value
            End Set
        End Property

        Public Property Price() As ProductPrice
            Get
                Return Me.priceField
            End Get
            Set
                Me.priceField = Value
            End Set
        End Property

    End Class
End Class


Partial Public Class ProductPrice

    Private currencyField As String
    Private valueField As String

    <System.Xml.Serialization.XmlAttributeAttribute()>
    Public Property Currency() As String
        Get
            Return Me.currencyField
        End Get
        Set
            Me.currencyField = Value
        End Set
    End Property

    <System.Xml.Serialization.XmlTextAttribute()>
    Public Property Value() As String
        Get
            Return Me.valueField
        End Get
        Set
            Me.valueField = Value
        End Set
    End Property
End Class


