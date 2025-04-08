let calendar;

$(document).ready(() => {
    var calendarEl = document.getElementById('calendario');
    $('#guardarAsignacion').on('click', AsignarRutina);

    calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'timeGridWeek', // Vista semanal con horas
        allDaySlot: false,
        locale: 'es',
        slotDuration: "00:30:00",
        slotMinTime: "05:00:00",
        slotMaxTime: "22:00:00",
        selectable: true, // Permite seleccionar horarios
        height: 'auto',
        timeZone: 'America/Santo_Domingo',
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: "dayGridMonth,timeGridWeek,listWeek",

        },
        slotLabelFormat: {
            hour: 'numeric',
            minute: '2-digit',
            hour12: true 
        },
        eventClick: function (info) {
            // Evita que navegue si el evento tiene un href
            info.jsEvent.preventDefault();

            const nombreRutina = info.event.extendedProps.nombreRutina || 'Rutina';
            const usuarios = info.event.title;

            // Llena contenido del modal
            document.getElementById('modal-titulo').innerText = nombreRutina;
            document.getElementById('modal-detalles').innerText = usuarios;

            // Muestra el modal
            const modal = new bootstrap.Modal(document.getElementById('modalAsignacion'));
            modal.show();
        },

        datesSet: function (info) {
            mostrarCargador();

            const startDate = info.startStr;
            const endDate = info.endStr;

            obtenerAsignaciones(startDate, endDate);
        },
        
        dateClick: function (info) {
            abrirModalAsignar(info.dateStr);
        }


    });

    calendar.render();

    $('#ddlAgregarUsuario').select2({
        dropdownParent: $('#modalAsignar'),
        placeholder: "Selecciona", 
        allowClear: true, 
        width: '100%',
    });

    $('#ddlAgregarRutina').select2({
        dropdownParent: $('#modalAsignar'),
        placeholder: "Selecciona", 
        width: '100%',
    });

});

function abrirModalAsignar(fechaSeleccionada) {
    fechaSeleccionada = new Date(fechaSeleccionada);
    const fechaLocal = fechaSeleccionada.getFullYear() + '-' +
        String(fechaSeleccionada.getMonth() + 1).padStart(2, '0') + '-' +
        String(fechaSeleccionada.getDate()).padStart(2, '0') + 'T' +
        String(fechaSeleccionada.getHours()).padStart(2, '0') + ':' +
        String(fechaSeleccionada.getMinutes()).padStart(2, '0') + ':' +
        String(fechaSeleccionada.getSeconds()).padStart(2, '0');

    $('#txtFechaFormat').val(fechaLocal);
    let fecha = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit', hour12: true };


    $('#txtFecha').val(fechaSeleccionada.toLocaleDateString('es-ES', fecha).replace(/\./g, '')); 
    $('#detalleRutina').html('').hide();
    $('#ddlAgregarUsuario').val(null).trigger('change');
    $('#ddlAgregarRutina').val(null).trigger('change');


    $('#modalAsignar').modal({
        backdrop: 'static',
        keyboard: false
    }).modal('show');

    $('#ddlAgregarRutina').on('change', function () {
        let id = parseInt($(this).val());
        if (isNaN(id)) {
            return; 
        }
        mostrarCargador();
        fetch(`${controller}/ObtenerRutina`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(id) // Envía el string correctamente
        })
            .then(response => response.json())
            .then(data => {
                if (data.success == true) {
                    mostrarDetalleRutina(data.rutina); 
                } else {
                    alertaMensaje('warning', 'La rutina no pudo ser encontrada.');
                    ocultarCargador();
                }
            })
            .catch(error => {
                console.error(`Error al registrar la asignación: ${error}`);
                ocultarCargador();
            });
    });
}

function resizeCalendar() {
    setTimeout(function () {
        calendar.updateSize(); // Ajusta el tamaño del calendario
    }, 300); // Pequeño retraso para asegurar el redimensionamiento
}

// Detecta cuando se oculta o muestra el aside
document.getElementById("btnAside").addEventListener("click", function () {
    resizeCalendar();
});

// También se ajusta cuando cambie el tamaño de la ventana
window.addEventListener("resize", function () {
    resizeCalendar();
});

function mostrarDetalleRutina(rutina) {
    let html = `
        <div class="border rounded-xl p-4 bg-white shadow-sm">
            <h5 class="text-dark fw-bold mb-3"><i class="bi bi-clipboard-check me-2"></i> ${rutina.nombre}</h5>
            <p class="text-muted mb-4">${rutina.descripcion}</p>`;

    rutina.sets.forEach(set => {
        html += `
            <div class="mb-4 p-3 bg-light rounded-lg border-start border-4 border-custom">
                <div class="d-flex justify-content-between align-items-center mb-2">
                    <span class="fw-semibold text-secondary"><i class="bi bi-list-check me-2"></i>${set.nombre}</span>
                    <span class="badge rounded-pill bg-secondary">${set.series} series</span>
                </div>
                <ul class="list-unstyled ms-2">`;
        set.setEjercicios.forEach(ej => {
            html += `
                    <li class="d-flex justify-content-between align-items-center py-1 border-bottom border-light">
                        <span class="d-flex align-items-center">
                            <i class="bi bi-chevron-right me-2 icon-custom"></i> ${ej.nombre}
                        </span>
                        <span class="badge rounded-pill badge-custom">${ej.repeticiones} reps</span>
                    </li>`;
        });
        html += `
                </ul>
            </div>`;
    });

    html += `</div>`;

    $('#detalleRutina').html(html).show();
    ocultarCargador();
}

function AsignarRutina() {
    let fecha = $('#txtFechaFormat').val();
    let usuarios = $('#ddlAgregarUsuario').val();
    let rutina = $('#ddlAgregarRutina').val();


    if (usuarios.length === 0) {
        alertaMensaje('warning', 'Debe seleccionar al menos un cliente');
        return;
    }

    if (rutina === null) {
        alertaMensaje('warning', 'Debe seleccionar una rutina');
        return;
    }

    let asignacion = {
        IdRutina: parseInt(rutina),
        IdUsuarios: usuarios,
        Fecha: fecha
    };

    console.log(JSON.stringify(asignacion));

    fetch(`${controller}/GuardarAsignacion`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(asignacion) // Envía el string correctamente
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alertaRecargar('success', 'La asignación fue registrada correctamente.');

            } else {
                alertaMensaje('warning', 'La asignación no pudo ser registrada, por favor intentalo más tarde.');
            }
        })
        .catch(error => {
            console.error(`Error al registrar la asignación: ${error}`);
            ocultarCargador();
        });
}

function obtenerAsignaciones(start, end) {
    fetch(`${controller}/ObtenerAsignaciones?start=${start}&end=${end}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al obtener asignaciones');
            }
            return response.json();
        })
        .then(eventos => {
            calendar.removeAllEvents(); // Limpia eventos anteriores
            calendar.addEventSource(eventos); // Agrega los nuevos
            ocultarCargador();
        })
        .catch(error => {
            console.error('Error:', error);
        });
}
