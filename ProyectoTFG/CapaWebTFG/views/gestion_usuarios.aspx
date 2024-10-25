<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="CapaWebTFG._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <header>
        <link rel="stylesheet" href="../css/iniciar_sesion_styles.css" />   
        <link rel="stylesheet" href="../css/admin.css" />     

        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/underscore.js/1.13.1/underscore-min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/backbone.js/1.4.0/backbone-min.js"></script>
        <script src="../Scripts/js/gestion_usuarios.js"></script>
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
       
        
                <h2>Lista de Personas</h2>
                <button id="boton_agregar">Añadir Persona</button>
                <div id="resultado"></div>
            </div>
        </div>
    </form>

    <div id="modalAgregar" class="modal" style="display:none;">
        <div class="modal-content">
            <span id="cerrarAgregarModal" class="close">&times;</span>
            <h2>Agregar Nueva Persona</h2>
            <label for="nuevoNombre">Nombre:</label>
            <input type="text" id="nuevoNombre" required>
            <label for="nuevoApellido">Apellido:</label>
            <input type="text" id="nuevoApellido" required>
            <label for="nuevoEmail">Email:</label>
            <input type="email" id="nuevoEmail" required>
            <label for="nuevaPassword">Password:</label>
            <input type="password" id="nuevaPassword" required>
            <label for="nuevoTelefono">Teléfono:</label>
            <input type="text" id="nuevoTelefono" required>
            <label for="nuevoFechaNacimiento">Fecha de Nacimiento:</label>
            <input type="date" id="nuevoFechaNacimiento" required>
            <label for="nuevoDireccion">Dirección:</label>
            <input type="text" id="nuevoDireccion" required>
            <label for="nuevoTipoPersona">Tipo Persona:</label>
            <select id="nuevoTipoPersona" required>
                <option value="1">Cliente</option>
                <option value="2">Administrador</option>
                <option value="3">Monitor</option>
            </select>
            <button id="guardarNuevaPersona">Guardar</button>
        </div>
    </div>
    <!-- Modal para Editar Persona -->
<div id="modalEditar" class="modal" style="display:none;">
    <div class="modal-content">
        <span class="cerrar" id="cerrarModal">&times;</span>
        <h2>Editar Persona</h2>
        <input type="hidden" id="personaId" />

        <label for="editNombre">Nombre:</label>
        <input type="text" id="editNombre" required />

        <label for="editApellido">Apellido:</label>
        <input type="text" id="editApellido" required />

        <label for="editEmail">Email:</label>
        <input type="email" id="editEmail" required />

        <label for="editTelefono">Teléfono:</label>
        <input type="tel" id="editTelefono" required />

        <label for="editFechaNacimiento">Fecha de Nacimiento:</label>
        <input type="date" id="editFechaNacimiento" required />

        <label for="editDireccion">Dirección:</label>
        <input type="text" id="editDireccion" required />

        <button id="guardarCambios">Guardar Cambios</button>
    </div>
</div>

</asp:Content>
