<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  Inherits="CapaWebTFG._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <header>
        <link rel="stylesheet" href="../css/iniciar_sesion_styles.css" />   
        <link rel="stylesheet" href="../css/admin.css" />     

        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/underscore.js/1.13.1/underscore-min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/backbone.js/1.4.0/backbone-min.js"></script>
        <script src="../Scripts/js/gestion_ejercicios.js"></script>
        <nav>
            <a href="../Default.aspx"`>
                <ul class="nav logo"  >

            </ul>
            </a>
            <ul class="nav menu">
                <li class="nav-item">
                    <a class="nav" aria-current="page" href="../views/gestion_usuarios.aspx">Gestion de Usuarios</a>
                </li>            
                <li class="nav-item">
                    <a class="nav" aria-current="page" href="gestion_citas.aspx">Gestion de Citas</a>
                </li>
                <li class="nav-item">
                    <a class="nav" href="gestion_notificaciones.aspx">Gestion de Notificaciones</a>
                </li>
                <li class="nav-item">
                    <a class="nav" href="gestion_ejercicios.aspx">Gestion de Ejercicios</a>
                </li>
            </ul>
        </nav>
    </header>
   <form id="form1">
        <div class="container">
            <div class="content mt-4">

                <h2>Lista de Ejercicios</h2>
                <button id="boton_agregar">Añadir ejercicio</button>

                <div id="resultado"></div>

             </div>
        </div>
    </form>

<div id="modalEditar" class="modal" style="display:none;">
    <div class="modal-content">
        <h2>Editar Ejercicio</h2>
        <input type="hidden" id="editEjercicios_Id" />
        <label>Nombre:</label>
        <input type="text" id="editNombre" />
        <br />
        <label>Descripción:</label>
        <input type="text" id="editDescripcion" />
        <br />
        <label>imagen:</label>
        <input type="file" id="editimagen"/>
        <br/>

        <button id="guardarCambios">Guardar Cambios</button>
        <button id="cerrarModal">Cerrar</button>
    </div>
</div>


<div id="modalAgregar" class="modal" style="display:none;">
    <div class="modal-content">

        <h2>Agregar Nuevo Ejercicio</h2>    
        <label for="Nombre">Nombre:</label>
        <input type="text" id="nuevoNombre" required><br>
    
        <label for="Descripcion">Descripción:</label>
        <input type="text" id="nuevoDescripcion" required><br>
    
        <label for="imagen">Imagen:</label>
        <input type="file" id="nuevoImagen" ><br> 
    
        <button id="guardarNuevaEjercicio">Guardar</button>
        <button id="cerrarAgregarModal">Cerrar</button>
    </div>
</div>


</asp:Content>
