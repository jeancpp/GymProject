let contadorSets = 1;

document.addEventListener("DOMContentLoaded", function () {
    const accordionSets = document.getElementById("accordionSets");
    const btnAgregarSet = document.getElementById("btnAgregarSet");
    const btnAbrirModal = document.getElementById("btnCrear"); // El botón que abre el modal

    // Contador de sets para nombres únicos

    btnAbrirModal.addEventListener("click", function () {
        limpiarModal();
    });

    // Evento para agregar un nuevo set
    btnAgregarSet.addEventListener("click", function () {
        const setDiv = document.createElement("div");
        setDiv.id = `set${contadorSets}`;
        setDiv.classList.add("mb-4", "set-container");

        setDiv.innerHTML = `
            <div class="d-flex justify-content-between align-items-center mb-3">
                <div class="w-75">
                    <input type="text" class="form-control" name="setNombres[]" value="Set ${contadorSets}" placeholder="Nombre del set" />
                </div>
                <button type="button" class="btn btn-sm btn-outline-danger btnEliminarSet">
                    Eliminar Set
                </button>
            </div>
            <div id="ejerciciosSet${contadorSets}" class="mb-3"></div>
            <button type="button" class="btn btn-sm btn-outline-primary btnAgregarEjercicio" data-set="${contadorSets}">
                Agregar Ejercicio
            </button>
        `;

        contenedorSets.appendChild(setDiv);
        contadorSets++;
    });

    // Delegación de eventos para los sets y ejercicios
    contenedorSets.addEventListener("click", function (e) {
        // Agregar un nuevo ejercicio
        if (e.target.classList.contains("btnAgregarEjercicio")) {
            const setId = e.target.dataset.set;
            const ejerciciosContainer = document.getElementById(`ejerciciosSet${setId}`);

            const ejercicioDiv = document.createElement("div");
            ejercicioDiv.classList.add("mb-2", "ejercicio-item");
            ejercicioDiv.innerHTML = `
                <div class="row align-items-center">
                    <div class="col-md-7">
                        <input type="text" class="form-control" placeholder="Nombre del ejercicio" name="sets[${setId}][]" />
                    </div>
                    <div class="col-md-3">
                        <input type="number" class="form-control" placeholder="Repeticiones" min="1" name="reps[${setId}][]" />
                    </div>
                    <div class="col-md-2 text-end">
                        <button type="button" class="btn btn-sm btn-danger btnEliminarEjercicio">Eliminar</button>
                    </div>
                </div>
            `;
            ejerciciosContainer.appendChild(ejercicioDiv);
        }

        // Eliminar un ejercicio
        if (e.target.classList.contains("btnEliminarEjercicio")) {
            e.target.closest(".ejercicio-item").remove();
        }

        // Eliminar un set
        if (e.target.classList.contains("btnEliminarSet")) {
            const setId = e.target.closest(".set-container").id;
            document.getElementById(setId).remove();
        }
    });
});

function limpiarModal() {
    console.log("Limpiando modal...");
    // Limpiar el nombre y la descripción
    document.getElementById("txtNombre").value = "";
    document.getElementById("txtDescripcion").value = "";

    // Limpiar los sets y ejercicios dinámicos
    contenedorSets.innerHTML = "";

    // Reiniciar el contador de sets
    contadorSets = 1;
}