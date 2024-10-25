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
        const id = $(this).data('id');
        editarCita(id);
    });

    $(document).on('click', '.btn-borrar', function () {
        const id = $(this).data('id');
        borrarCita(id);
    });
    $(document).ready(function () {
        $('#guardarCambios').on('click', function () {
            guardarCita();
        });
    });
}

function borrarCita(id) {

    if (confirm(`¿Estás seguro de que quieres borrar a la cita con ID: ${id}?`)) {
        $.ajax({
            type: 'POST',
            url: '../Services/ServicioCitas.asmx/BorrarCita',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ id: id }),
            success: function (response) {
                let parsedResponse = JSON.parse(response.d);
                if (parsedResponse.success) {
                    alert(`Cita con ID: ${id} borrada exitosamente.`);
                    location.reload();
                } else {
                    alert(`Error: ${parsedResponse.message}`);
                }
            },
            error: function (err) {
                alert('Error al intentar borrar la cita.');
                console.error(err);
            }
        });
    }
}

function cargarCitas() {
    $.ajax({
        type: 'GET',
        url: '../Services/ServicioCitas.asmx/ObtenerCitas',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);

            if (parsedResponse.success) {
                let citas = parsedResponse.data;
                let table = '<table class="table"><thead><tr>';
                table += '<th>ID</th><th>Motivo</th><th>Fecha</th><th>Persona_ID</th><th>Acciones</th>';
                table += '</tr></thead><tbody>';

                citas.forEach(function (cita) { 
                    table += '<tr>';
                    table += `<td>${cita.Citas_Id}</td>`;
                    table += `<td>${cita.Motivo}</td>`;
                    table += `<td>${formatearFecha(cita.Fecha)}</td>`; 
                    table += `<td>${cita.Persona_Id}</td>`;

                    table += `<td>
                            <button class="btn-editar" data-id="${cita.Citas_Id}">Editar</button>
                            <button class="btn-borrar" data-id="${cita.Citas_Id}">Borrar</button>
                        </td>`;
                    table += '</tr>';
                });
                table += '</tbody></table>';
                $('#resultado').html(table);
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

    // Cargar citas al iniciar la página
    cargarCitas();
    asignarEventosBotones();
    // Asignar eventos para abrir y cerrar modales
    $('#boton_agregar').on('click', function () {
        $('#modalAgregar').show();
    });

    $('#cerrarAgregarModal, #cerrarModal').on('click', function () {
        $('#modalAgregar, #modalEditar').hide();
    });

    // Evento para guardar nueva cita
    $('#guardarNuevaCita').on('click', function () {
        const Motivo = $('#nuevoMotivo').val();
        const Fecha = $('#nuevoFecha').val(); // Verifica el formato de fecha
        const Persona_Id = $('#nuevoPersona_Id').val();

        // Validar que todos los campos requeridos estén llenos
        if (!Motivo || !Fecha || !Persona_Id) {
            alert("Por favor, completa todos los campos requeridos.");
            return;
        }

        $.ajax({
            type: 'POST',
            url: '../Services/ServicioCitas.asmx/AgregarCita',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                Motivo: Motivo, // Usar la variable correcta
                Fecha: Fecha,   // Usar la variable correcta
                Persona_Id: Persona_Id // Usar la variable correcta
            }),

            success: function (response) {
                let parsedResponse = JSON.parse(response.d);
                if (parsedResponse.success) {
                    alert('Cita añadida exitosamente.');
                    $('#modalAgregar').hide();
                    cargarCitas();
                } else {
                    alert(`Error: ${parsedResponse.message}`);
                }
            },
            error: function (err) {
                console.log("Datos enviados:", { Motivo, Fecha, Persona_Id });
                alert('Error al intentar añadir la cita.');
                console.error(err);
            }
        });
    });
});
function editarCita(id) {
    $.ajax({
        type: 'GET',
        url: '../Services/ServicioCitas.asmx/ObtenerCitaPorId',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { id: id },
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);
            if (parsedResponse.success) {
                const cita = parsedResponse.data;
                const fecha = cita.Fecha.split('T')[0];

                $('#editCitas_Id').val(cita.Citas_Id);
                $('#editMotivo').val(cita.Motivo);
                $('#editFecha').val(fecha);
                $('#editPersona_Id').val(cita.Persona_Id); // Corrected to use cita instead of persona_Id
                $('#modalEditar').show();
            } else {
                alert(`Error: ${parsedResponse.message}`);
            }
        },
        error: function (err) {
            alert('Error al intentar obtener los datos de la cita.');
            console.error(err);
        }
    });
}

function guardarCita() {
    const citas_Id = $('#editCitas_Id').val();
    const motivo = $('#editMotivo').val();
    const fecha = $('#editFecha').val().trim();
    const persona_Id = $('#editPersona_Id').val();

    // Validar que todos los campos requeridos estén llenos
    if (!citas_Id || !motivo || !fecha || !persona_Id) {
        alert("Por favor, completa todos los campos requeridos.");
        return; // Detener la ejecución si hay campos vacíos
    }

    $.ajax({
        type: 'POST',
        url: '../Services/ServicioCitas.asmx/EditarCita', // Asegúrate de que la URL es correcta
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ citas_Id: citas_Id, motivo: motivo, fecha: fecha, persona_Id: persona_Id }), // Asegúrate de que los nombres coincidan
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);
            if (parsedResponse.success) {
                alert(`Cita con ID: ${citas_Id} editada exitosamente.`); // Usa citas_Id en lugar de id
                $('#modalEditar').hide(); // Cierra el modal
                cargarCitas(); // Recarga los datos
            } else {
                alert(`Error: ${parsedResponse.message}`);
            }
        },
        error: function (err) {
            alert('Error al intentar editar la cita.');
            console.error(err);
        }
    });
}
