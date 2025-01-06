document.addEventListener('DOMContentLoaded', function () {
    // Selecciona todos los botones con la clase 'editar-ejercicio'
    const botonesEditar = document.querySelectorAll('.editar-ejercicio');

    // Agrega un evento 'click' a cada botón
    botonesEditar.forEach(function (boton) {
        boton.addEventListener('click', function (e) {
            e.preventDefault();

            // Obtén los datos del botón
            const id = this.getAttribute('data-id');
            const nombre = this.getAttribute('data-nombre');
            const categorias = JSON.parse(this.dataset.categorias); // Convertir JSON a arreglo

            let datos = { id, nombre, categorias }
            abrirModalEditar(datos)

        });
    });
});

function abrirModalEditar(datos) {

    console.log(datos);

    $('#txtEditarNombre').val(datos.nombre);
    

}