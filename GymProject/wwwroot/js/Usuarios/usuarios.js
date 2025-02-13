const btnAbrirModal = document.getElementById("btnAgregar");

$(document).ready(() => {
    $('#ddlTipoUsuario').select2({
        dropdownParent: $('#modalAgregar'),
        placeholder: "Selecciona", // Texto que se mostrará cuando no haya selección
        width: '100%'
    });
    new DataTable('#tablaUsuarios', {
        ordering: false,
        layout: {
            topStart: 'info',
            bottom: 'paging',
            bottomStart: null,
            bottomEnd: null
        },
    });


});

btnAbrirModal.addEventListener("click", function () {
    $('#modalAgregar').modal({
        backdrop: 'static',
        keyboard: false
    }).modal('show');

    document.getElementById("txtNombre").value = "";
    document.getElementById("txtCon").value = "";
    $('#ddlTipoUsuario').val(null).trigger('change');
});
// 