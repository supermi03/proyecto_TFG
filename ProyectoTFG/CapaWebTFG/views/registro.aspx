<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="CapaWebTFG._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <header>
    <link rel="stylesheet" href="../css/iniciar_sesion_styles.css" />     
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/underscore.js/1.13.1/underscore-min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/backbone.js/1.4.0/backbone-min.js"></script>
    <script src="../Scripts/js/registro.js"></script>
    <nav>
        <ul class="nav logo">
            <li class="nav-item">
                <a class="nav" aria-current="page" href="#"></a>
            </li>
        </ul>
        <ul class="nav menu">
            <li class="nav-item">
                <a class="nav" aria-current="page" href="../Default.aspx">Inicio</a>
            </li>
            <li class="nav-item">
                <a class="nav" aria-current="page" href="../Default.aspx/#caracteristicas">Conócenos</a>
            </li>
            <li class="nav-item">
                <a class="nav" href="../Default.aspx/#tarifas">Tarifas</a>
            </li>
            <li class="nav-item">
                <a class="boton_iniciar_sesion" href="../views/IniciarSesion.aspx">Acceder</a>
            </li>
        </ul>
    </nav>
    </header>

    <br/><br/><br/>
<div class="main-content">
    <div class="iniciar_sesion">
        <div class="cuadrado_iniciar_sesion">
            <h3 class="titulo">Registrarse</h3>
            <!-- Aquí está el formulario -->
            <form id="FormRegistro" method="POST">
                <div class="form-group">
                    <label for="nombre">Nombre</label>
                    <input type="text" id="nombre" name="nombre" class="form-control" placeholder="Ingresa tu nombre" required>
                </div>
                <div class="form-group">
                    <label for="apellido">Apellido</label>
                    <input type="text" id="apellido" name="apellido" class="form-control" placeholder="Ingresa tu apellido" required>
                </div>
                <div class="form-group">
                    <label for="email">Email</label>
                    <input type="text" id="email" name="email" class="form-control" placeholder="Ingresa tu email" required>
                </div>
                <div class="form-group">
                    <label for="password">Contraseña</label>
                    <input type="password" id="password" name="password" class="form-control" placeholder="Ingresa tu contraseña" required>
                </div>
                <div class="form-group">
                    <label for="direccion">Dirección</label>
                    <input type="text" id="direccion" name="direccion" class="form-control" placeholder="Ingresa tu dirección" required>
                </div>
                <div class="form-group">
                    <label for="nacimiento">Fecha de Nacimiento</label>
                    <input type="date" id="nacimiento" name="fecha_nacimiento" class="form-control" required>
                </div>
                <div class="form-group">
                    <label for="telefono">Teléfono</label>
                    <input type="text" id="telefono" name="telefono" class="form-control" placeholder="Ingresa tu teléfono" required>
                </div>

                <input id="boton_registro" type="button" class="boton_iniciar_sesion" value ="Registrarse"/>
                <br><br>
                <a>¿Ya tienes cuenta? Inicia Sesión <a class="link_registro" href="../views/IniciarSesion.aspx">aquí</a></a>
            </form>
        </div>
    </div>
</div>



</asp:Content>
