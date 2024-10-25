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
        editarPersona(id);
    });

    $(document).on('click', '.btn-borrar', function () {
        $(document).on("submit", function () {
            return false;
        })
        const id = $(this).data('id');
        borrarPersona(id);
    });
    $(document).ready(function () {
        $('#guardarCambios').on('click', function () {
            guardarCambios();
        });
    });
}

// Función para borrar persona
function borrarPersona(id) {

    if (confirm(`¿Estás seguro de que quieres borrar a la persona con ID: ${id}?`)) {
        $.ajax({
            type: 'POST',
            url: '../Services/ServicioPersonas.asmx/BorrarPersona',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ id: id }),
            success: function (response) {
                let parsedResponse = JSON.parse(response.d);
                if (parsedResponse.success) {
                    alert(`Persona con ID: ${id} borrada exitosamente.`);
                    location.reload()
                } else {
                    alert(`Error: ${parsedResponse.message}`);
                }
            },
            error: function (err) {
                alert('Error al intentar borrar la persona.');
                console.error(err);
            }
        });
    }
}

function cargarPersonas() {
    $.ajax({
        type: 'GET',
        url: '../Services/ServicioPersonas.asmx/ObtenerPersonas',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);

            if (parsedResponse.success) {
                let personas = parsedResponse.data;
                let table = '<table class="table"><thead><tr>';
                table += '<th>ID</th><th>Nombre</th><th>Apellido</th><th>Email</th><th>Teléfono</th><th>Fecha de Nacimiento</th><th>Dirección</th><th>Tipo Persona</th><th>Acciones</th>';
                table += '</tr></thead><tbody>';

                personas.forEach(function (persona) {
                    table += '<tr>';
                    table += `<td>${persona.Persona_Id}</td>`;
                    table += `<td>${persona.Nombre}</td>`;
                    table += `<td>${persona.Apellido}</td>`;
                    table += `<td>${persona.Email}</td>`;
                    table += `<td>${persona.Telefono}</td>`;
                    table += `<td>${formatearFecha(persona.Fecha_nacimiento)}</td>`;
                    table += `<td>${persona.Direccion}</td>`;
                    table += `<td>${persona.TipoPersona.Nombre}</td>`;
                    table += `<td>
                            <button class="btn-editar" data-id="${persona.Persona_Id}">Editar</button>
                            <button class="btn-borrar" data-id="${persona.Persona_Id}">Borrar</button>
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
    })
    // Cargar personas al iniciar la página
    cargarPersonas();

    // Asignar eventos para abrir y cerrar modales
    $('#boton_agregar').on('click', function () {
        $('#modalAgregar').show();
    });

    $('#cerrarAgregarModal, #cerrarModal').on('click', function () {
        $('#modalAgregar, #modalEditar').hide();
    });

    // Evento para guardar nueva persona
    $('#guardarNuevaPersona').on('click', function () {
        const nombre = $('#nuevoNombre').val().trim();
        const apellido = $('#nuevoApellido').val().trim();
        const email = $('#nuevoEmail').val().trim();
        const password = $('#nuevaPassword').val().trim();
        const telefono = $('#nuevoTelefono').val().trim();
        const fechaNacimiento = $('#nuevoFechaNacimiento').val();
        const direccion = $('#nuevoDireccion').val().trim();
        const tipoPersona = $('#nuevoTipoPersona').val();

        // Validar que todos los campos requeridos estén llenos
        if (!nombre || !apellido || !email || !password || !telefono || !fechaNacimiento || !direccion || !tipoPersona) {
            alert("Por favor, completa todos los campos requeridos.");
            return; // Detener la ejecución si hay campos vacíos
        }
        // Llamada al servicio de agregar persona
        $.ajax({
            type: 'POST',
            url: '../Services/ServicioPersonas.asmx/AgregarPersona',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                nombre,
                apellido,
                email,
                password,
                telefono,
                fechaNacimiento,
                direccion,
                tipoPersona
            }),

            success: function (response) {
                let parsedResponse = JSON.parse(response.d);
                if (parsedResponse.success) {
                    alert('Persona añadida exitosamente.');
                    $('#modalAgregar').hide();
                    cargarPersonas();
                } else {
                    alert(`Error: ${parsedResponse.message}`);
                }
            },
            error: function (err) {
                alert('Error al intentar añadir la persona.');
                console.error(err);
            }
        });
    });
});

// Función para editar persona
function editarPersona(id) {
    $.ajax({
        type: 'GET',
        url: `../Services/ServicioPersonas.asmx/ObtenerPersonaPorId?id=${id}`, // Corregido aquí
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);
            if (parsedResponse.success) {
                const persona = parsedResponse.data;
                const fechaNacimiento = persona.Fecha_nacimiento.split('T')[0]; // Extraer fecha

                // Asignar los valores a los campos de la modal
                $('#personaId').val(persona.Persona_Id); // ID oculto para edición
                $('#editNombre').val(persona.Nombre);
                $('#editApellido').val(persona.Apellido);
                $('#editEmail').val(persona.Email);
                $('#editTelefono').val(persona.Telefono);
                $('#editFechaNacimiento').val(fechaNacimiento);
                $('#editDireccion').val(persona.Direccion);

                $('#modalEditar').show(); // Mostrar el modal de edición
            } else {
                alert(`Error: ${parsedResponse.message}`);
            }
        },
        error: function (err) {
            alert('Error al intentar obtener los datos de la persona.');
            console.error(err);
        }
    });
}



function guardarCambios() {
    const persona_Id = $('#personaId').val(); // Cambia "id" por "persona_Id"
    const nombre = $('#editNombre').val().trim();
    const apellido = $('#editApellido').val().trim();
    const email = $('#editEmail').val().trim();
    const telefono = $('#editTelefono').val().trim();
    const fechaNacimiento = $('#editFechaNacimiento').val();
    const direccion = $('#editDireccion').val().trim();

    // Validar que todos los campos requeridos estén llenos
    if (!nombre || !apellido || !email || !telefono || !fechaNacimiento || !direccion) {
        alert("Por favor, completa todos los campos requeridos.");
        return; // Detener la ejecución si hay campos vacíos
    }

    const fechaNacimientoISO = new Date(fechaNacimiento).toISOString();

    $.ajax({
        type: 'POST',
        url: '../Services/ServicioPersonas.asmx/EditarPersona',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // Cambia "id" por "persona_Id" en el objeto JSON
        data: JSON.stringify({
            persona_Id, // Aquí se utiliza el nombre correcto
            Nombre: nombre,
            Apellido: apellido,
            Email: email,
            Telefono: telefono,
            FechaNacimiento: fechaNacimientoISO,
            Direccion: direccion
        }),
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);
            if (parsedResponse.success) {
                alert(`Persona con ID: ${persona_Id} editada exitosamente.`);
                $('#modalEditar').hide(); // Cierra el modal
                cargarPersonas(); // Recarga los datos
            } else {
                alert(`Error: ${parsedResponse.message}`);
            }
        },
        error: function (err) {
            alert('Error al intentar editar la persona.');
            console.error(err);
        }
    });
}
