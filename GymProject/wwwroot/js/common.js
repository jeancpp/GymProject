function mostrarCargador() {
    Swal.fire({
        title: 'Cargando',
        text: 'Por favor espere',
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
            Swal.showLoading();
        }
    });
}

function mostrarCargadorDinamico(titulo, mensaje) {
    Swal.fire({
        title: titulo,
        text: mensaje,
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
            Swal.showLoading();
        }
    });
}

function ocultarCargador() {
    Swal.close();
}

function alertaMensaje(tipo, mensaje) {
    Swal.fire({
        icon: tipo,
        title: mensaje,
        confirmButtonColor: "#027b8c",
        confirmButtonText: "Aceptar",
        allowOutsideClick: false
    });
}

function alertaRecargar(tipo, mensaje) {
    Swal.fire({
        icon: tipo,
        title: mensaje,
        confirmButtonText: 'Aceptar',
        confirmButtonColor: "#027b8c",
        allowOutsideClick: false
    }).then((result) => {
        if (result.isConfirmed) {
            location.reload(); // Recarga la página cuando el usuario hace clic en "Aceptar"
        }
    });
}
