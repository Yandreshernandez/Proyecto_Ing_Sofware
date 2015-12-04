Public Class frmMuestra
    Private Fun_Muestra As New Gestor_Muestra

    Private Tabla_SesionesCatacion As New BindingSource
    Private RegistroSeleccionado As New BindingSource
    Private TablaProductores As New BindingSource

    Private Sub frmMuestra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Iniciar_Formulario()
        Cargar_Ciudades()
        CargarTabla_Productores()
    End Sub

    Sub Iniciar_Formulario() 'Carga todos los registros de sesiones de catacion en la tabla
        Dim Cod_Sesion As String
        Dim Var_Sesion As String

        Cod_Sesion = frmRealizarCataciones.varCod_Sesion 'Obtiene el Codigo de la sesion seleccionado
        Var_Sesion = frmRealizarCataciones.varSesion

        'Cargos los valor de identificador
        Tabla_SesionesCatacion.DataSource = Fun_Muestra.Lista_Valor_Identificado(Cod_Sesion)
        dgMuestras.DataSource = Tabla_SesionesCatacion
        dgMuestras.Columns("codigo").Visible = False

        'Mostramos el numero de la sesicion
        lbSesion.Text = Var_Sesion
    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        If txtBuscar.TextLength = 0 Or txtBuscar.Text = "Buscar" Then
            TablaProductores.RemoveFilter()
            Exit Sub
        End If
        'Dim da As New SqlDataAdapter("SELECT cedula 'Cedula', nombre 'Nombre', apellido 'Apellido' FROM PERSONA WHERE tipo_persona = 'Productor' ORDER BY cedula", db.Conexion)
        If txtBuscarPor.Text = "Cedula" Then
            TablaProductores.Filter = "cedula like '" & txtBuscar.Text & "%'"
        ElseIf txtBuscarPor.Text = "Nombre" Then
            TablaProductores.Filter = "nombre like '" & txtBuscar.Text & "%'"
        ElseIf txtBuscarPor.Text = "Apellido" Then
            TablaProductores.Filter = "apellido like '" & txtBuscar.Text & "%'"
        End If
    End Sub

    Private Sub dgMuestras_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgMuestras.CellEnter

        Dim varCod_Muestra As String
        Dim row As DataGridViewRow = dgMuestras.CurrentRow
        varCod_Muestra = CStr(row.Cells("codigo").Value)
        Try
            RegistroSeleccionado.DataSource = Fun_Muestra.Buscar_Muestra() 'Obtiene la muestra de la sesion seleccionada
            RegistroSeleccionado.Filter = "cod_muestra = '" & varCod_Muestra & "'"

            lbMuestra.Text = RegistroSeleccionado.Item(0)("valor_identificado")
            txtNombre.Text = RegistroSeleccionado.Item(0)("nombre_muestra")
            txtDescripcion.Text = RegistroSeleccionado.Item(0)("descripcion")
            txtEspecie.Text = RegistroSeleccionado.Item(0)("especie")
            cbAnio_Cosecha.Text = RegistroSeleccionado.Item(0)("anio_cosecha")
            nuHumedad.Text = RegistroSeleccionado.Item(0)("humedad")
            cbCiudad.Text = RegistroSeleccionado.Item(0)("ciudad")
            'txtProductor.Text = RegistroSeleccionado.Item(0)("finca_productor")
            'txtProveedor.Text = RegistroSeleccionado.Item(0)("finca_proveedor")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub

    Sub Cargar_Ciudades()
        Dim Lista_Ciudades As New BindingSource
        Lista_Ciudades.DataSource = Fun_Muestra.Lista_Ciudad()
        cbCiudad.Items.Clear()
        For i = 0 To Lista_Ciudades.Count - 1
            cbCiudad.Items.Add(Lista_Ciudades.Item(i)("nombre"))
        Next
    End Sub

    Sub CargarTabla_Productores() 'Carga los registros de los productores en la tabla
        TablaProductores.DataSource = Fun_Muestra.Tabla_Productores()
        dgLista_Productores.DataSource = TablaProductores
    End Sub

    Private Sub btnCiudad_Click(sender As Object, e As EventArgs) Handles btnCiudad.Click
        frmCiudad.ShowDialog()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
End Class