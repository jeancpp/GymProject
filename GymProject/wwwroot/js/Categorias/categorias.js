$(document).ready(() => {

    new DataTable('#tablaCategorias', {
        ordering: false,
        layout: {
            topStart: 'info',
            bottom: 'paging',
            bottomStart: null,
            bottomEnd: null
        },
    });
});


