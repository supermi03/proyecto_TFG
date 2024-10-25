$(document).ready(function () {
    let fechaActual = new Date(); // Fecha actual
    let mesActual = fechaActual.getMonth(); // Mes actual (0-11)
    let anioActual = fechaActual.getFullYear(); // Año actual

    function mostrarCalendario(mes, anio) {
        $('#mesActual').text(`${mes + 1} / ${anio}`); // Mostrar mes y año
        $('#diasDelMes').empty(); // Limpiar días del mes

        // Obtener el primer día del mes
        let primerDia = new Date(anio, mes, 1);
        let diasEnMes = new Date(anio, mes + 1, 0).getDate(); // Total de días en el mes
        let diaInicio = primerDia.getDay()-1; // Día de la semana del primer día


        // Crear celdas vacías hasta el primer día del mes
        for (let i = 0; i < diaInicio; i++) {
            $('#diasDelMes').append('<div class="celda vacia"></div>');
        }

        // Agregar los días del mes
        for (let dia = 1; dia <= diasEnMes; dia++) {
            $('#diasDelMes').append(`<div class="celda">${dia}</div>`);
        }
    }

    // Navegación entre meses
    $('#prev').click(function () {
        mesActual--;
        if (mesActual < 0) {
            mesActual = 11; // Volver a diciembre
            anioActual--;
        }
        mostrarCalendario(mesActual, anioActual);
    });

    $('#next').click(function () {
        mesActual++;
        if (mesActual > 11) {
            mesActual = 0; // Volver a enero
            anioActual++;
        }
        mostrarCalendario(mesActual, anioActual);
    });

    // Mostrar el calendario inicial
    mostrarCalendario(mesActual, anioActual);
});