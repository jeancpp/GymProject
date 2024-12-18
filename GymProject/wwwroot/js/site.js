﻿document.addEventListener('DOMContentLoaded', function () {
    // Selecciona todos los botones con la clase 'editar-ejercicio'
    const botonesEditar = document.querySelectorAll('.editar-ejercicio');

    // Agrega un evento 'click' a cada botón
    botonesEditar.forEach(function (boton) {
        boton.addEventListener('click', function (e) {
            e.preventDefault();

            // Obtén los datos del botón
            const id = this.getAttribute('data-id');
            const nombre = this.getAttribute('data-nombre');

            // Aquí puedes usar estos valores para mostrar el modal
            console.log('ID:', id, 'Nombre:', nombre);
        });
    });
});
