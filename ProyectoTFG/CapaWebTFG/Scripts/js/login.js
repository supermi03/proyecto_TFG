$(document).ready(function () {
    $('#boton_login').on('click', function () {

        var email = $("#email").val();
        var password = $("#password").val();
        var recordarme = $("#recordarme").is(":checked"); // Verifica si se seleccionó "Recordarme"

        $.ajax({
            type: "POST",
            url: "../Services/ServicioLogin.asmx/IniciarSesion",
            data: JSON.stringify({
                email: $("#email").val(),
                password: $("#password").val(),
                recordarme: $("#recordarme").is(":checked") // Asumiendo que tienes un checkbox para "Recordarme"
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                // Intenta parsear la cadena JSON
                var data;
                try {
                    data = JSON.parse(response.d); // Analiza la cadena JSON
                } catch (e) {
                    console.error("Error al analizar JSON:", e);
                    return;
                }

                if (data.success) {
                    console.log("Redirigiendo a la página principal...");
                    window.location.href = "../views/mainUser.aspx"; // Redirigir tras inicio de sesión
                } else {
                    alert("Datos erroneos");
                }
            },
            error: function (xhr, status, error) {
                // Aquí el error puede ser undefined si no se captura correctamente
                console.error("Error en la llamada AJAX: ", xhr, status, error);
            }
        });

    });
});
