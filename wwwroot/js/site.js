$(document).ready( function () {
   getDataTable("#table-contatos");
   getDataTable('#table-usuarios')
} );

function getDataTable(id) {
    $(id).DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/2.1.6/i18n/pt-BR.json',
        }
    });
}

$('.close-alert').click(function () {
    $('.alert').hide('hide');
});
