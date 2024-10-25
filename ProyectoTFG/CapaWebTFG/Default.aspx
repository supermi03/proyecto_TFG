<%@ Page Title="Pagina Principal" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CapaWebTFG._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <header>
    <link rel="stylesheet" href="css/main.css" />       
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/underscore.js/1.13.1/underscore-min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/backbone.js/1.4.0/backbone-min.js"></script>
    <script src="registro.js"></script>
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
                <a class="nav" aria-current="page" href="#caracteristicas">Conócenos</a>
            </li>
            <li class="nav-item">
                <a class="nav" href="#tarifas">Tarifas</a>
            </li>
            <li class="nav-item">
                <a class="boton_iniciar_sesion" href="../views/IniciarSesion.aspx">Iniciar sesión</a>
            </li>
        </ul>
    </nav>

    </header>

    <br/><br/><br/><br/><br/><br/><br/><br id ="tarifas" /><br/><br/><br/><br/><br/><br/><br/>
    
    <div class="main-content">

        <div class="cuadrados">
            <div class="cuadrado">
                <h3>Prúebalo gratis</h3>
        </div>

            <div class="cuadrado">
                <h3>Tarifa Básica</h3>
                <p>9,99€/mes</p>
            </div>
            <div class="cuadrado">
                <h3>Tarifa Premium</h3>
                <p>24,99€/mes</p>
            </div>
        </div>
           <br/><br/><br/><br/><br/><br/><br id="caracteristicas"/><br/><br/><br/><br/><br/><br/><br/>

         <div class="cuadrados">
             <div class="cuadrado">
                 <h3>Caracteristica 1</h3>
             </div>

             <div class="cuadrado">
                 <h3>Caracteristica 2</h3>
             </div>
             <div class="cuadrado">
                 <h3>Caracteristica 3</h3>
             </div>
         </div>
         <div class="cuadrados">
             <div class="cuadrado">
                 <h3>Caracteristica 4</h3>
             </div>

             <div class="cuadrado">
                 <h3>Caracteristica 5</h3>
             </div>
             <div class="cuadrado">
                 <h3>Caracteristica 6</h3>
             </div>
        </div>
        <div class="cuadrados">
             <div class="cuadrado">
                 <h3>Caracteristica 7</h3>
             </div>

             <div class="cuadrado">
                 <h3>Caracteristica 8</h3>
             </div>
             <div class="cuadrado">
                 <h3>Caracteristica 9</h3>
             </div>
         </div>
    </div>
               <br/><br/><br/><br/><br/>

</asp:Content>
