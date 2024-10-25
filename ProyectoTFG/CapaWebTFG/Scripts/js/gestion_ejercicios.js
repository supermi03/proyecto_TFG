
function asignarEventosBotones() {
    $(document).on('click', '.btn-editar', function () {
        const id = $(this).data('id');
        editarEjercicio(id);
    });

    $(document).on('click', '.btn-borrar', function () {
        const id = $(this).data('id');
        borrarEjercicio(id);
    });


}
function borrarEjercicio(id) {
    if (confirm(`¿Estás seguro de que quieres borrar el ejercicio con ID: ${id}?`)) {
        $.ajax({
            type: 'POST',
            url: '../Services/ServicioEjercicios.asmx/BorrarEjercicio',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({ id: id }),
            success: function (response) {
                let parsedResponse = JSON.parse(response.d);
                if (parsedResponse.success) {
                    alert(`Ejercicio con ID: ${id} borrado exitosamente.`);
                    location.reload();
                } else {
                    alert(`Error: ${parsedResponse.message}`);
                }
            },
            error: function (err) {
                alert('Error al intentar borrar el ejercicio.');
                console.error(err);
            }
        });
    }
}
function cargarEjercicio() {
    $.ajax({
        type: 'GET',
        url: '../Services/ServicioEjercicios.asmx/ObtenerEjercicios',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);

            if (parsedResponse.success) {
                let ejercicios = parsedResponse.data;
                let table = '<table class="table"><thead><tr>';
                table += '<th>ID</th><th>Nombre</th><th>Descripcion</th><th>Imagen</th><th>Acciones</th>';
                table += '</tr></thead><tbody>';

                ejercicios.forEach(function (ejercicio) {
                    table += '<tr>';
                    table += `<td>${ejercicio.Ejercicios_Id}</td>`;
                    table += `<td>${ejercicio.Nombre}</td>`;
                    table += `<td>${ejercicio.Descripcion}</td>`;

                    // Mostrar imagen usando base64 o URL
                    table += `<td><img src="${ejercicio.Imagen_Ejercicio}" width="100" height="100"></td>`;

                    table += `<td>
                                <button class="btn-editar" data-id="${ejercicio.Ejercicios_Id}">Editar</button>
                                <button class="btn-borrar" data-id="${ejercicio.Ejercicios_Id}">Borrar</button>
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
    })
    cargarEjercicio();

    // Asignar eventos a los botones
    asignarEventosBotones();

    // Asignar eventos para abrir y cerrar modales
    $('#cerrarAgregarModal, #cerrarModal').on('click', function () {
        $('#modalAgregar, #modalEditar').hide();
    });

    // Evento para mostrar el modal de agregar
    $('#boton_agregar').on('click', function () {
        $('#modalAgregar').show();
    });
    $('#guardarCambios').on('click', function () {
        guardarEjercicio();
    });
    $(document).ready(function () {
        $('#guardarNuevaEjercicio').click(function (e) {
            e.preventDefault();

            // Crear el objeto FormData
            var formData = new FormData();
            formData.append('Nombre', $('#nuevoNombre').val());
            formData.append('Descripcion', $('#nuevoDescripcion').val());

            // Añadir el archivo de imagen
            var imagen = $('#nuevoImagen')[0].files[0];
            if (imagen) {
                formData.append('Imagen_Ejercicio', imagen);
            }

            // Hacer la petición AJAX
            $.ajax({
                url: '../Services/ServicioEjercicios.asmx/AgregarEjercicio',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    alert('Ejercicio agregado con éxito.');
                    location.reload();
                    
                },
                error: function (error) {
                    alert('Error en la solicitud: ' + error.responseText);
                }
            });
        });
    });
});

function editarEjercicio(id) {
    $.ajax({
        type: 'GET',
        url: '../Services/ServicioEjercicios.asmx/ObtenerEjercicioPorId',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { id: id },
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);
            if (parsedResponse.success) {
                const ejercicio = parsedResponse.data;

                $('#editEjercicios_Id').val(ejercicio.Ejercicios_Id);
                $('#editNombre').val(ejercicio.Nombre);
                $('#editDescripcion').val(ejercicio.Descripcion);
                $('#modalEditar').show();
            } else {
                alert(`Error: ${parsedResponse.message}`);
            }
        },
        error: function (err) {
            alert('Error al intentar obtener los datos del ejercicio.');
            console.error(err);
        }
    });
}

function guardarEjercicio() {
    const ejercicios_Id = $('#editEjercicios_Id').val();
    const nombre = $('#editNombre').val();
    const descripcion = $('#editDescripcion').val();
    const imagen_ejercicio = $('#editimagen').val();
    console.log(imagen_ejercicio);
    // Validar que todos los campos requeridos estén llenos
    if (!ejercicios_Id || !nombre || !descripcion) {
        alert("Por favor, completa todos los campos requeridos.");
        return; // Detener la ejecución si hay campos vacíos
    }

    $.ajax({
        type: 'POST',
        url: '../Services/ServicioEjercicios.asmx/EditarEjercicio', // Asegúrate de que la URL es correcta
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ Ejercicios_Id: ejercicios_Id, Nombre: nombre, Descripcion: descripcion, Imagen_Ejercicio: imagen_ejercicio }), // Asegúrate de que los nombres coincidan
        success: function (response) {
            let parsedResponse = JSON.parse(response.d);
            if (parsedResponse.success) {
                alert(`Ejercicio con ID: ${ejercicios_Id} editado exitosamente.`); // Usa citas_Id en lugar de id
                $('#modalEditar').hide(); // Cierra el modal
                cargarEjercicio(); // Recarga los datos
            } else {
                alert(`Error: ${parsedResponse.message}`);
            }
        },
        error: function (err) {
            alert('Error al intentar editar el ejercicio.');
            console.error(err);
        }
    });
}
