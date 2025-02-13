
$(document).ready(() => {
    $('.btnEditarRutina').on('click', editarRutina);

});
function editarRutina() {
    limpiarModal();

    var nombre = $(this).data('nombre');
    var descripcion = $(this).data('descripcion');
    var sets = $(this).data('sets');

    $('#editNombre').val(nombre);
    $('#editDescripcion').val(descripcion);

    let setIndex = 0;
    sets.forEach(set => {
        agregarSetDesdeDatos(set, setIndex);
        setIndex++;
    });

    $('#modalEditar').modal({
        backdrop: 'static',
        keyboard: false
    }).modal('show');

};

function agregarSetDesdeDatos(set, index) {
    const setId = `set${index + 1}`;
    const ejercicioId = `ejerciciosSet${index + 1}`;

    // Crear el HTML del set
    const setAccordion = document.createElement("div");
    setAccordion.classList.add("accordion-item");
    setAccordion.id = setId;
    setAccordion.innerHTML = `
        <h2 class="accordion-header" id="heading${index + 1}">
            <button class="accordion-button ${index > 0 ? "collapsed" : ""}" 
                    type="button" data-bs-toggle="collapse" 
                    data-bs-target="#collapse${index + 1}" 
                    aria-expanded="${index === 0}" 
                    aria-controls="collapse${index + 1}">
                ${set.nombre}
            </button>
        </h2>
        <div id="collapse${index + 1}" class="accordion-collapse collapse ${index === 0 ? "show" : ""}" 
             aria-labelledby="heading${index + 1}" data-bs-parent="#accordionSets">
            <div class="accordion-body">
                <div class="row mb-3">
                    <div class="col-md-8">
                        <input type="text" class="form-control" 
                               name="Sets[${index}].Nombre" 
                               value="${set.nombre}" placeholder="Nombre del set" />
                    </div>
                    <div class="col-md-4">
                        <input type="number" class="form-control" 
                               name="Sets[${index}].Series" 
                               placeholder="Series" min="1" value="${set.series}" />
                    </div>
                </div>
                <div id="${ejercicioId}" class="mb-3"></div>
                <button type="button" class="btn btn-sm btn-outline-primary btnAgregarEjercicio" 
                        data-set="${index + 1}">
                    Agregar Ejercicio
                </button>
                <button type="button" class="btn btn-sm btn-outline-danger btnEliminarSet" 
                        data-set="${index + 1}">
                    Eliminar Set
                </button>
            </div>
        </div>
    `;

    // Agregar el set al contenedor de sets
    $("#accordionEditarSets").append(setAccordion);

    // Agregar ejercicios dentro del set
    set.setEjercicios.forEach((ej, ejIndex) => {
        agregarEjercicioDesdeDatos(ej, ejercicioId, index, ejIndex);
    });
}

function agregarEjercicioDesdeDatos(ejercicio, containerId, setIndex, ejercicioIndex) {
    const ejerciciosContainer = document.getElementById(containerId);

    const ejercicioDiv = document.createElement("div");
    ejercicioDiv.classList.add("mb-2", "ejercicio-item");
    ejercicioDiv.innerHTML = `
        <div class="row align-items-center">
            <div class="col-md-7">
    <select class="form-control select-ejercicios w-100"
            name="Sets[${setIndex}].Ejercicios[${ejercicioIndex}].IdEjercicio">
        <option value="" disabled>Selecciona un ejercicio</option>
        ${listaEjercicios.map(ej =>
            `<option value="${ej.idEjercicio}" 
            ${ej.nombre === ejercicio.nombre ? "selected" : ""}>
                ${ej.nombre}
            </option>`).join('')}
    </select>
</div>
            <div class="col-md-3">
                <input type="number" class="form-control" 
                       placeholder="Repeticiones" min="1" 
                       name="Sets[${setIndex}].Ejercicios[${ejercicioIndex}].Repeticiones" 
                       value="${ejercicio.repeticiones}" />
            </div>
            <div class="col-md-2 text-end">
                <button type="button" class="btn btn-sm btn-danger btnEliminarEjercicio">Eliminar</button>
            </div>
        </div>
    `;

    ejerciciosContainer.appendChild(ejercicioDiv);
    const nuevoSelect = ejercicioDiv.querySelector('.select-ejercicios');
    $(nuevoSelect).select2({
        dropdownParent: $('#modalEditar'),
        placeholder: "Selecciona",
        allowClear: true,
        width: '100%'  // Asegura que select2 ocupe todo el ancho
    });
}



function limpiarModal() {
    document.getElementById("editNombre").value = "";
    document.getElementById("editDescripcion").value = "";
    $("#accordionEditarSets").empty();
}

