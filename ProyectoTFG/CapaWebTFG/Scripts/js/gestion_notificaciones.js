function formatearFecha(fecha) {
    const date = new Date(fecha);
    if (isNaN(date.getTime())) { // Verifica si la fecha es válida
        return 'Fecha no válida'; // Retorna un mensaje o un valor por defecto
    }
    const opciones = { day: '2-digit', month: '2-digit', year: 'numeric' };
    return date.toLocaleDateString('es-ES', opciones);
}


function asignarEventosBotones() {
    $(document).on('click', '.btn-editar', function () {
        $(document).on("submit", function () {
            return false;
        })
        const id = $(this).data('id');
        editarNotificacion(id);
    });

    $(document).on('click', '.btn-borrar', function () {
        $(document).on("submit", function () {
            return false;
        })
        const id = $(this).data('id');
        borrarNotificacion(id);
    });
    $(document).ready(function () {
        $('#guardarCambios').on('click', function () {
            guardarEdicionNotificacion();
        });
    });
}

function borrarNotificacion(id) {
    if (confirm(`¿Estás seguro de que quieres borrar la Notificación con ID: ${id}?`)) {
        $.ajax({
            type: 'POST',
            url: '../Services/ServicioNotificaciones.asmx/BorrarNotificacion',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ id: id }),
            success: function (response) {
                let parsedResponse = JSON.parse(response.d);
                if (parsedResponse.success) {
                    alert(`Notificación con ID: ${id} borrada exitosamente.`);
                    location.reload();
                } else {
                    alert(`Error: ${parsedResponse.message}`);
                }
            },
            error: function (err) {
                alert(`Error al intentar borrar la Notificación: ${err.statusText}`);
                console.error(`Error status: ${err.status} - ${err.statusText}`);
            }
        });
    }
    else return
}

function cargarNotificaciones() {
    $.ajax({
        type: 'GET',
        url: '../Services/ServicioNotificaciones.asmx/ObtenerNotificaciones',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);

            if (parsedResponse.success) {
                let notificacion = parsedResponse.data;
                let table = '<table class="table"><thead><tr>';
                table += '<th>Notificacion_Id</th><th>mensaje</th><th>Fecha</th><th>Persona_Id</th><th>Acciones</th>';
                table += '</tr></thead><tbody>';

                notificacion.forEach(function (notificacion) {
                    table += '<tr>';
                    table += `<td>${notificacion.Notificaciones_Id}</td>`;  // Corregido Notificacion_Id a Notificaciones_Id
                    table += `<td>${notificacion.Mensaje}</td>`;
                    table += `<td>${formatearFecha(notificacion.Fecha)}</td>`;
                    table += `<td>${notificacion.Persona_Id}</td>`;
                    table += `<td>
                            <button class="btn-editar" data-id="${notificacion.Notificaciones_Id}">Editar</button> 
                            <button class="btn-borrar" data-id="${notificacion.Notificaciones_Id}">Borrar</button> 
                        </td>`;
                    table += '</tr>';
                });

                table += '</tbody></table>';
                $('#resultado').html(table);

                // Asignar eventos a los botones de editar y borrar
                asignarEventosBotones();
            } else {
                $('#resultado').html('<p>No se pudieron cargar los datos.</p>');
                console.error(parsedResponse.message);
            }
        },
        error: function (err) {
            $('#resultado').html('<p>Error al cargar los datos.</p>');
            console.error(err);
        }
    });
}

$(document).ready(function () {
    $(document).on("submit", function () {
        return false;
    });

    // Cargar notificaciones al iniciar la página
    cargarNotificaciones();

    // Asignar eventos para abrir y cerrar modales
    $('#boton_agregar').on('click', function () {
        $('#modalAgregar').show();
    });

    $('#cerrarAgregarModal, #cerrarModal').on('click', function () {
        $('#modalAgregar, #modalEditar').hide();
    });

    // Evento para guardar nueva notificación
    $('#guardarNuevanotificacion').on('click', function () {
        const mensaje = $('#nuevoMensaje').val().trim();
        const fecha = $('#nuevoFechaNotificacion').val(); // Verifica el formato de fecha
        const persona_Id = $('#nuevoPersona_Id').val();

        // Validar que todos los campos requeridos estén llenos
        if (!mensaje || !fecha || !persona_Id) {
            alert("Por favor, completa todos los campos requeridos.");
            return;
        }

        $.ajax({
            type: 'POST',
            url: '../Services/ServicioNotificaciones.asmx/AgregarNotificaciones',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                Mensaje: mensaje, // Usa el mismo nombre que en el backend
                Fecha: fecha,
                Persona_Id: persona_Id,
            }),

            success: function (response) {
                let parsedResponse = JSON.parse(response.d);
                if (parsedResponse.success) {
                    alert('Notificación añadida exitosamente.');
                    $('#modalAgregar').hide();
                    cargarNotificaciones();
                } else {
                    alert(`Error: ${parsedResponse.message}`);
                }
            },
            error: function (err) {
                console.log("Datos enviados:", { mensaje, fecha, persona_Id });
                alert('Error al intentar añadir la notificación.');
                console.error(err);
            }
        });
    });
});
function editarNotificacion(id) {
    $.ajax({
        type: 'GET',
        url: '../Services/ServicioNotificaciones.asmx/ObtenerNotificacionPorId',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { id: id }, // Enviando el ID directamente como parámetro
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);
            if (parsedResponse.success) {
                const notificacion = parsedResponse.data;
                const fechaFormateada = notificacion.Fecha.split('T')[0]; // Extraer la parte de la fecha

                // Asignar los valores a los campos del formulario
                $('#editNotificaciones_Id').val(notificacion.Notificaciones_Id);
                $('#editMensaje').val(notificacion.Mensaje);
                $('#editFecha').val(fechaFormateada);
                $('#editPersona_Id').val(notificacion.Persona_Id);

                // Mostrar el modal de edición
                $('#modalEditar').show();
            } else {
                alert(`Error: ${parsedResponse.message}`);
            }
        },
        error: function (err) {
            alert('Error al intentar obtener los datos de la notificación.');
            console.error(err);
        }
    });
}

// Llama a esta función cuando se envíe el formulario de edición
function guardarEdicionNotificacion() {
    const notificaciones_Id = $('#editNotificaciones_Id').val();
    const mensaje = $('#editMensaje').val();
    const fecha = $('#editFecha').val();
    const persona_Id = $('#editPersona_Id').val();

    // Validar que todos los campos requeridos estén llenos
    if (!mensaje || !fecha || !persona_Id) {
        alert("Por favor, completa todos los campos requeridos.");
        return; // Detener la ejecución si hay campos vacíos
    }

    // Llamada al servicio de editar notificación
    $.ajax({
        type: 'POST',
        url: '../Services/ServicioNotificaciones.asmx/EditarNotificaciones',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({
            notificaciones_Id: notificaciones_Id,
            mensaje: mensaje,
            fecha: fecha,
            persona_Id: persona_Id,
        }),

        success: function (response) {
            let parsedResponse = JSON.parse(response.d);
            if (parsedResponse.success) {
                alert('Notificación editada exitosamente.');
                $('#modalEditar').hide();
                cargarNotificaciones(); 
            } else {
                alert(`Error: ${parsedResponse.message}`);
            }
        },
        error: function (err) {
            alert('Error al intentar editar la notificación.');
            console.error(err);
        }
    });
}

