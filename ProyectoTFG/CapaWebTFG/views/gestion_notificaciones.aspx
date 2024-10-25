<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  Inherits="CapaWebTFG._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <header>
        <link rel="stylesheet" href="../css/iniciar_sesion_styles.css" />   
        <link rel="stylesheet" href="../css/admin.css" />     

        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/underscore.js/1.13.1/underscore-min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/backbone.js/1.4.0/backbone-min.js"></script>
        <script src="../Scripts/js/gestion_notificaciones.js"></script>
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
             
                <h2>Lista de Notificaciones</h2>
                <button id="boton_agregar">Añadir Notificación</button>

                <div id="resultado"></div>
                </div>
        </div>
    </form>


<div id="modalEditar" class="modal" style="display:none;">
    <div class="modal-content">

        <h2>Editar Notificación</h2>
        <input type="hidden" id="Notificaciones_Id" />
    
        <label for="editNotificaciones_Id">ID de Notificación:</label>
        <input type="text" id="editNotificaciones_Id" disabled /> <!-- ID debe ser solo lectura -->
        <br />
    
        <label for="editMensaje">Mensaje:</label>
        <input type="text" id="editMensaje" required />
        <br />
    
        <label for="editFecha">Fecha:</label>
        <input type="date" id="editFecha" required />
        <br />
    
        <label for="editPersona_Id">ID de Persona:</label>
        <input type="text" id="editPersona_Id" required />
        <br />
    
        <button id="guardarCambios">Guardar Cambios</button>
        <button id="cerrarModal">Cerrar</button>
    </div>
</div>



<div id="modalAgregar" class="modal" style="display:none;">
    <div class="modal-content">
        <span id="cerrarAgregarModal" class="close">&times;</span>
        <h2>Agregar Nueva Notificación</h2>
        <label for="nuevoMensaje">mensaje:</label>
        <input type="text" id="nuevoMensaje" required>
        <label for="nuevoFechaNotificacion">Fecha:</label>
        <input type="date" id="nuevoFechaNotificacion" required>
        <label for="nuevoPersona_Id">Persona_Id:</label>
        <input type="number" id="nuevoPersona_Id" required>
        <button id="guardarNuevanotificacion">Guardar</button>
    </div>
</div>

</asp:Content>
