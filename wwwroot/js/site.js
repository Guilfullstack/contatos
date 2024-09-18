$(document).ready( function () {
    $('#table-contatos').DataTable({
        // columns: [
        //     { data: '#' },
        //     { data: 'Nome' },
        //     { data: 'Email' },
        //     { data: 'Celular' },
        //     { data: 'Button' },
        // ],
        language: {
            url: "//cdn.datatables.net/plug-ins/1.13.5/i18n/pt-BR.json"
        }
    });
} );

$('.close-alert').click(function () {
    $('.alert').hide('hide');
});
