<%@ Page Title="Iniciar Sesion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CapaWebTFG._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <header>
        <link rel="stylesheet" href="../css/iniciar_sesion_styles.css" />    
        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script src="../Scripts/js/login.js"></script>
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
                    <a class="nav" aria-current="page" href="../Default.aspx/#caracteristicas">Con�cenos</a>
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
                <h3 class="titulo">Iniciar Sesi�n</h3>
                <form id="FormInicioSesion" method="post" onsubmit="return false;">
                    <div class="form-group">
                        <label for="email">Correo Electr�nico</label>
                        <input type="text" id="email" name="email" class="form-control" placeholder="Ingresa tu correo" required>
                    </div>
                    <div class="form-group">
                        <label for="password">Contrase�a</label>
                        <input type="password" id="password" name="password" class="form-control" placeholder="Ingresa tu contrase�a" required>
                    </div>
                    <div class="form-group">
                        <input type="checkbox" id="recordarme" name="recordarme">
                        <label for="recordarme">Recordarme</label>
                    </div>
                    <input id="boton_login" type="button" class="boton_iniciar_sesion" value="Iniciar Sesi�n"/>
                    <br><br> 
                    <a> �No tienes cuenta? Reg�strate <a class="link_registro" href="../views/registro.aspx"> aqu�</a></a>
                </form>
            </div>
        </div>
    </div>

</asp:Content>
