$(document).ready(() => {
    $('#selectCategorias').select2({
        dropdownParent: $('#modalAgregar'),
        placeholder: "Selecciona una categoria", // Configura el placeholder
        allowClear: true, // Permite borrar la selección
        width: '100%' // Asegura que use el ancho completo
    });

    $('#editCategorias').select2({
        dropdownParent: $('#modalEditar'),
        placeholder: "Selecciona una categoria", // Configura el placeholder

    });
    new DataTable('#tablaEjercicios', {
        ordering: false,
        layout: {
            topStart: 'info',
            bottom: 'paging',
            bottomStart: null,
            bottomEnd: null
        },
    });
});


$('#tablaEjercicios tbody')
    .off('click', '.btnEditarEjercicio')
    .on('click', '.btnEditarEjercicio', function () {
        var id = $(this).data('id');
        var nombre = $(this).data('nombre');
        var categorias = $(this).data('categorias').split(','); 

        $('#editId').val(id);

        $('#editNombre').val(nombre);

        $('#editCategorias option').prop('selected', false);

        var valoresSeleccionados = [];

        categorias.forEach(function (categoria) {
            $('#editCategorias option').filter(function () {
                return $(this).text() === categoria.trim(); // Comparar por texto.
            }).each(function () {
                valoresSeleccionados.push($(this).val()); // Agregar el valor al array
            });
        });

        $('#editCategorias').val(valoresSeleccionados).trigger('change');


        $('#modalEditar').modal({
            backdrop: 'static',
            keyboard: false
        }).modal('show');

    });
