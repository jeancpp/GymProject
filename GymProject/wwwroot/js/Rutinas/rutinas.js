let nextSetNumber = 1;
const form = document.getElementById('formRutina');
const sets = document.querySelectorAll('[name^="Sets["]');
const btnLimpiar = document.getElementById("btnLimpiar");
const buscarButton = document.getElementById("btnBuscar");


document.addEventListener("DOMContentLoaded", function () {
    const accordionSets = document.getElementById("accordionSets");
    const btnAgregarSet = document.getElementById("btnAgregarSet");
    const btnAbrirModal = document.getElementById("btnCrear");


    //CREACIÓN DE RUTINAS
    btnAbrirModal.addEventListener("click", function () {
        $('#modalAgregar').modal({
            backdrop: 'static',
            keyboard: false
        }).modal('show');

        limpiarModal();
    });
    // Evento para agregar un nuevo set
    btnAgregarSet.addEventListener("click", function () {
        const setId = `set${nextSetNumber}`;
        const ejercicioId = `ejerciciosSet${nextSetNumber}`;

        // Crear un nuevo panel del acordeón
        const setAccordion = document.createElement("div");
        setAccordion.classList.add("accordion-item");
        setAccordion.id = setId; // Asignar un ID único al contenedor del set
        setAccordion.innerHTML = `
           <h2 class="accordion-header" id="heading${nextSetNumber}">
            <button class="accordion-button ${nextSetNumber > 1 ? "collapsed" : ""}" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${nextSetNumber}" aria-expanded="${nextSetNumber === 1}" aria-controls="collapse${nextSetNumber}">
                Set ${nextSetNumber}
            </button>
           </h2>
           <div id="collapse${nextSetNumber}" class="accordion-collapse collapse ${nextSetNumber === 1 ? "show" : ""}" aria-labelledby="heading${nextSetNumber}" data-bs-parent="#accordionSets">
                <div class="accordion-body">
                    <div class="row mb-3">
                        <div class="col-md-8">
                            <input type="text" class="form-control" name="Sets[${nextSetNumber - 1}].Nombre" value="Set ${nextSetNumber}" placeholder="Nombre del set" required/>
                        </div>
                        <div class="col-md-4">
                            <input type="number" class="form-control" name="Sets[${nextSetNumber - 1}].Series" placeholder="Series" min="1" value="1" required />
                        </div>
                    </div>
                    <div id="${ejercicioId}" class="mb-3"></div>
                    <button type="button" class="btn btn-sm btn-outline-primary btnAgregarEjercicio" data-set="${nextSetNumber}">
                        Agregar Ejercicio
                    </button>
                    <button type="button" class="btn btn-sm btn-outline-danger btnEliminarSet" data-set="${nextSetNumber}">
                        Eliminar Set
                    </button>
                </div>
            </div>
        `;
        accordionSets.appendChild(setAccordion);
        nextSetNumber++;
    });

    // Delegación de eventos para sets y ejercicios
    accordionSets.addEventListener("click", function (e) {
        // Agregar un ejercicio al set
        if (e.target.classList.contains("btnAgregarEjercicio")) {
            const setId = e.target.dataset.set;
            const ejerciciosContainer = document.getElementById(`ejerciciosSet${setId}`);

            const ejercicioIndex = ejerciciosContainer.querySelectorAll(".ejercicio-item").length;

            const ejercicioDiv = document.createElement("div");
            ejercicioDiv.classList.add("mb-2", "ejercicio-item");
            ejercicioDiv.innerHTML = `
            <div class="row align-items-center">
                <div class="col-md-7">
                    <select class="form-control select-ejercicios" name="Sets[${setId - 1}].Ejercicios[${ejercicioIndex}].IdEjercicio" required>
                        <option value="" disabled selected>Selecciona un ejercicio</option>
                        ${listaEjercicios.map(ejercicio => `<option value="${ejercicio.idEjercicio}">${ejercicio.nombre}</option>`).join('')}
                    </select>
                </div>
                <div class="col-md-3">
                    <input type="number" class="form-control" placeholder="Repeticiones" min="1" name="Sets[${setId - 1}].Ejercicios[${ejercicioIndex}].Repeticiones" required />
                </div>
                <div class="col-md-2 text-end">
                    <button type="button" class="btn btn-sm btn-danger btnEliminarEjercicio">Eliminar</button>
                </div>
            </div>
        `;
            ejerciciosContainer.appendChild(ejercicioDiv);

            const nuevoSelect = ejercicioDiv.querySelector('.select-ejercicios');
            $(nuevoSelect).select2({
                dropdownParent: $('#modalAgregar'),
                placeholder: "Selecciona",
                allowClear: true
            });
        }

        // Eliminar un ejercicio
        if (e.target.classList.contains("btnEliminarEjercicio")) {
            const ejercicioItem = e.target.closest(".ejercicio-item");
            const ejerciciosContainer = ejercicioItem.parentNode; // Contenedor de todos los ejercicios de un set
            ejercicioItem.remove();

            // Reenumerar los ejercicios restantes
            reenumerarEjercicios(ejerciciosContainer);
        }

        // Eliminar un set completo
        if (e.target.classList.contains("btnEliminarSet")) {
            const setId = e.target.closest(".accordion-item"); // Obtener el contenedor del set
            setId.remove(); // Eliminar el set completo
            reenumerarSets();
        }
    });
});

function limpiarModal() {
    document.getElementById("txtNombre").value = "";
    document.getElementById("txtDescripcion").value = "";

    // Limpiar los sets y ejercicios dinámicos
    accordionSets.innerHTML = "";

    // Reiniciar el contador de sets
    nextSetNumber = 1;
}

function reenumerarSets() {
    const allSets = accordionSets.querySelectorAll(".accordion-item");
    let currentNumber = 0; // Índice basado en cero para los nombres de los inputs

    allSets.forEach(set => {
        const heading = set.querySelector(".accordion-header button");
        const collapse = set.querySelector(".accordion-collapse");

        // Actualizar el texto del botón
        heading.textContent = `Set ${currentNumber + 1}`;

        // Actualizar los atributos relacionados
        const newId = `collapse${currentNumber + 1}`;

        heading.setAttribute("data-bs-target", `#${newId}`);
        heading.setAttribute("aria-controls", newId);
        heading.id = `heading${currentNumber + 1}`;

        collapse.id = newId;
        collapse.setAttribute("aria-labelledby", `heading${currentNumber + 1}`);

        // Actualizar el atributo del contenedor del set
        set.id = `set${currentNumber + 1}`;

        // Actualizar los nombres de los inputs dentro del set
        const nombreInput = set.querySelector('input[name^="Sets["][name$=".Nombre"]');
        const seriesInput = set.querySelector('input[name^="Sets["][name$=".Series"]');

        if (nombreInput) {
            nombreInput.name = `Sets[${currentNumber}].Nombre`;
        }

        if (seriesInput) {
            seriesInput.name = `Sets[${currentNumber}].Series`;
        }

        // Actualizar los ejercicios dentro del set, si hay
        const ejercicios = set.querySelectorAll('select[name^="sets["], input[name^="reps["]');
        ejercicios.forEach((ejercicio, index) => {
            if (ejercicio.name.startsWith("sets[")) {
                ejercicio.name = `Sets[${currentNumber}].Ejercicios[${index}].IdEjercicio`;
            } else if (ejercicio.name.startsWith("reps[")) {
                ejercicio.name = `Sets[${currentNumber}].Ejercicios[${index}].Repeticiones`;
            }
        });

        currentNumber++;
    });

    // Ajustar el contador global
    nextSetNumber = currentNumber + 1;
}

function reenumerarEjercicios(container) {
    const allEjercicios = container.querySelectorAll(".ejercicio-item"); // Todos los ejercicios del set
    allEjercicios.forEach((ejercicio, index) => {
        // Actualizar los nombres de los inputs/selects dentro del ejercicio
        const ejercicioIdInput = ejercicio.querySelector('select[name^="Sets["][name$=".IdEjercicio"]');
        const repeticionesInput = ejercicio.querySelector('input[name^="Sets["][name$=".Repeticiones"]');

        // Cambiar los índices en los nombres para cada input/select
        if (ejercicioIdInput) {
            ejercicioIdInput.name = ejercicioIdInput.name.replace(/\Ejercicios\[\d+\]/, `Ejercicios[${index}]`);
        }

        if (repeticionesInput) {
            repeticionesInput.name = repeticionesInput.name.replace(/\Ejercicios\[\d+\]/, `Ejercicios[${index}]`);
        }
    });
}
form.addEventListener('submit', function (event) {
    sets.forEach(function (set, index) {
        const setNumber = index; // El índice de cada set
        const nombreInput = document.querySelector(`[name="Sets[${setNumber}].Nombre"]`);

        // Si el nombre está vacío, eliminamos el set
        if (!nombreInput || nombreInput.value.trim() === "") {
            document.querySelector(`#set${setNumber}`).remove(); // Eliminar el set del DOM
        }
    });
});


btnLimpiar.addEventListener("click", function () {

    $("#searchQuery").val("");
    $("#searchDate").val("");

    buscarButton.click();

});