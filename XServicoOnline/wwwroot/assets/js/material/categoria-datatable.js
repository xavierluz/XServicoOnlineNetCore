Categoria = (function () {
    
    let tableName = '#tableCategoria';
    let actionDetalhes = '.action-detalhe';
    let actionEditar = '.action-editar';
    let actionExcluir = '.action-excluir';
    let actionReivindicacao = '.action-usuario';
    let actionId = '.categoria-id';

    Categoria.prototype.Load = function () {
        criarTablecategoria();

    };
    var criarAction = function () {
        $(actionDetalhes).click(function () {
            let id = $(this).parent().parent().parent().parent().find(actionId).text()
            window.location = './Detalhes?id=' + id;
        });
        $(actionEditar).click(function () {
            var id = $(this).parent().parent().parent().parent().find(actionId).text()
            window.location = './Editar?id=' + id;
        });
    };
    var criarTablecategoria = function () {

        $(tableName).DataTable({
            "bPaginate": true,
            "bLengthChange": true,
            "bFilter": true,
            "bSort": true,
            "bInfo": true,
            "bAutoWidth": false,
            "columnDefs": [
                { "className": "actionButton border-table-td", "targets": [4] },
                { "className": "border-table-td", "targets": [0] },
                { "className": "border-table-td text-center", "targets": [1] },
                { "className": "border-table-td ativo", "targets": [2] },
                { "className": "td-invisivel categoria-id", "targets": [3], "searchable": false },
                { "className": "text-center custom-middle-align", "targets": [0, 1, 2, 3] }
            ],
            "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
            "processing": true,
            "serverSide": true,
            "ajax":
            {
                "url": "GetCategorias",
                "type": "POST",
                "dataType": "JSON"
            },
            "columns": [
                { "data": "Nome" },
                { "data": "Descricao" },
                { "data": "Ativo" },
                { "data": "Id" },
                { "defaultContent": '<div class="form-group" style="margin-left: -1%;"><p><a class="action-editar actionButton" title="Editar"><i class="glyphicon glyphicon-pencil editarclass"></i><span class="sr-only aria-hidden="true"">Editar</span></a><a class="action-detalhe actionButton" title="Detalhes"><i class="glyphicon glyphicon-search detalheclass"></i><span class="sr-only" aria-hidden="true">Detalhes</span></a></p></div>' },
            ],
            "initComplete": function () {

            },
            "fnUpdate": function () {

            },
            "fnInfoCallback": function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                criarAction();
                console.log(oSettings);
            },
            "oLanguage": {
                "sProcessing": "Processando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "Não foram encontrados resultados",
                "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando de 0 até 0 de 0 registros",
                "sInfoFiltered": "",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "oPaginate": {
                    "sFirst": "Primeiro",
                    "sPrevious": "Anterior",
                    "sNext": "Próximo",
                    "sLast": "Último"
                }
            }
        });
    };
});