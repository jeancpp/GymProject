$(document).ready(() => {

    new DataTable('#tablaCategorias', {
        ordering: false,
        layout: {
            topStart: 'info',
            bottom: 'paging',
            bottomStart: null,
            bottomEnd: null
        },
        info: false,
        searching:false
    });

    const btnAbrirModal = document.getElementById("agregarCategoria");

    btnAbrirModal.addEventListener("click", function () {
        $('#txtNombre').val('');
    });

    $('#btnGuardarCategoria').on('click', crearCategoria);
});

function crearCategoria() {
    mostrarCargador();
    let Nombre = $('#txtNombre').val();

    if (Nombre === "") {
        alertaMensaje("warning", "Debe ingresar el nombre de la categoria");
        return;
    }


    fetch(`${controller}/Index`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Nombre) // Envía el string correctamente
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alertaRecargar('success', 'La categoria ha sido registrada.');
            } else {
                alertaMensaje('warning', 'La categoria ya se encuentra registrada.');
            }
        })
        .catch(error => {
            console.error(`Error al registrar la categoría: ${error}`);
            ocultarCargador();
        });
}