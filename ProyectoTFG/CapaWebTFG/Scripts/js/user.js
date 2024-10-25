$(document).ready(function () {
    cargarEjercicios(); // Cargar ejercicios al inicio

    // Manejar el cambio en el select para filtrar ejercicios
    $('#categoria-select').change(function () {
        var categoriaSeleccionada = $(this).val();
        cargarEjercicios(categoriaSeleccionada);
    });
});

function cargarEjercicios(categoria = 'all') {
    $.ajax({
        type: "GET",
        url: "../Services/ServicioEjercicios.asmx/ObtenerEjercicios",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            // La respuesta se encuentra en response.d, así que lo convertimos a objeto JSON
            var responseData = JSON.parse(response.d);
            console.log("Respuesta completa del servidor:", responseData); // Inspeccionamos la respuesta completa

            // Verificamos la estructura de la respuesta
            if (responseData && typeof responseData.success !== 'undefined') {
                if (responseData.success) {
                    var ejercicios = responseData.data; // Accedemos a responseData.data
                    console.log("Ejercicios:", ejercicios); // Verificamos los datos de ejercicios

                    // Filtrar ejercicios según la categoría seleccionada
                    if (categoria !== 'all') {
                        ejercicios = ejercicios.filter(function (ejercicio) {
                            return ejercicio.Descripcion.toLowerCase().includes(categoria.toLowerCase());
                        });
                    }

                    // Verificamos si 'ejercicios' es un arreglo
                    if (Array.isArray(ejercicios)) {
                        var html = '';
                        ejercicios.forEach(function (ejercicio) {
                            html += `
                                <div class="ejercicio">
                                    <h3>${ejercicio.Nombre}</h3>
                                    <p>${ejercicio.Descripcion}</p>
                                    <img src="${ejercicio.Imagen_Ejercicio}" alt="${ejercicio.Nombre}" />
                                    <br><br><button id="agregar_ejercicio">Añadir a favoritos</button>

                                </div>
                            `;
                        });
                        $('#ejercicios-container').html(html);
                    } else {
                        console.log("Error: 'ejercicios' no es un arreglo.", ejercicios);
                    }
                } else {
                    console.log("Error: la operación no fue exitosa.");
                }
            } else {
                console.log("Error: La respuesta no contiene 'success' o 'data'.", responseData);
            }
        },
        error: function (error) {
            console.log("Error al cargar los ejercicios:", error);
        }
    });
}
