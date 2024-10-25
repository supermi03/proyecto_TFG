$(document).ready(function () {
    $('#boton_registro').on('click', function (e) {
        e.preventDefault(); // Previene el envío del formulario por defecto

        var nombre = $('#nombre').val();
        var apellido = $('#apellido').val();
        var email = $('#email').val();
        var password = $('#password').val();
        var telefono = $('#telefono').val();
        var direccion = $('#direccion').val();
        var fechaNacimiento = $('#nacimiento').val();

        // Ejemplo de validación sencilla
        if (!nombre || !apellido || !email || !password || !telefono || !direccion || !nacimiento) {
            alert("Por favor, completa todos los campos.");
            return;
        }

        $.ajax({
            type: 'POST',
            url: '../Services/ServicioRegistro.asmx/Registro', 
            data: JSON.stringify({
                nombre: nombre,
                apellido: apellido,
                email: email,
                password: password,
                direccion: direccion,
                fechaNacimiento: fechaNacimiento,
                telefono: telefono
            }),
            dataType: "json",
            async: false,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                alert('Registro exitoso!');
                window.location.href = 'IniciarSesion.aspx'; 
            },
            error: function (error) {
                alert('Ocurrió un error durante el registro. Por favor, inténtalo de nuevo.');
            }
        });
    });
});
