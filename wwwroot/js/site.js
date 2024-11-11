$(document).ready( function () {
   getDataTable("#table-contatos");
   getDataTable('#table-usuarios');

   $('.btn-total-contatos').click(function () {
    var usuarioid = $(this).attr('usuario-id');
    $.ajax({
        type:'GET',

        url: '/Usuario/ListarContatosPorUsuarioId/'+usuarioid, 
            success: function(result){
            $('#listaDeContatosUsuario').html(result);
            $('#modalContatosUsuario').modal();
      }});
      
   })

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
