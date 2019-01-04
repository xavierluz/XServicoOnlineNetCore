Material = (function () {
    
    let tableName = '#tableMaterial';
    let actionDetalhes = '.action-detalhe';
    let actionEditar = '.action-editar';
    let actionId = '.material-id';

    Material.prototype.Load = function () {
        criarTableMaterial();

    };
    var criarAction = function () {
        $(actionDetalhes).click(function () {
            let id = $(this).parent().parent().parent().parent().find(actionId).text()
            window.location = './Detalhes?id=' + id;
        });
        //$(actionEditar).click(function () {
        //    var id = $(this).parent().parent().parent().parent().find(actionId).text()
        //    window.location = './Editar?id=' + id;
        //});
    };
    var editarClickHandler = function (material) {
        window.location = './EditarMaterial?materialId=' + material.id; 
    };
    var criarTableMaterial = function () {

        $(tableName).DataTable({
            "bPaginate": true,
            "bLengthChange": true,
            "bFilter": true,
            "bSort": true,
            "bInfo": true,
            "bAutoWidth": false,
            "columnDefs": [
                { "className": "actionButton border-table-td coluna-grid-padding", "targets": [4] },
                { "className": "border-table-td coluna-grid-padding", "targets": [0] },
                { "className": "border-table-td text-center coluna-grid-padding", "targets": [1] },
                { "className": "border-table-td text-center ativo coluna-grid-padding", "targets": [2] },
                { "className": "td-invisivel categoria-id coluna-grid-padding", "targets": [3], "searchable": false },
                { "className": "text-center custom-middle-align coluna-grid-padding", "targets": [0, 1, 2, 3] }
            ],
            "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
            "processing": true,
            "serverSide": true,
            "ajax":
            {
                "url": "GetMateriais",
                "type": "POST",
                "dataType": "JSON"
            },
            "columns": [
                { "data": "nome" },
                { "data": "descricao" },
                { "data": "ativo" },
                { "data": "id" },
                { "defaultContent": '<div class="form-group default-content-data-table" style="margin-left: -1%;"><a class="action-editar" title="Editar"><i class="material-icons editarclass" data-toggle="tooltip" data-placement="top" title="Tooltip on top">edit</i><span class="sr-only aria-hidden="true"">Editar</span></a><a class="action-detalhe" title="Detalhes"><i class="material-icons detalheclass">details</i><span class="sr-only" aria-hidden="true">Detalhes</span></a></div>' }
            ],
            "initComplete": function () {
            },
            "fnUpdate": function () {

            },
            "fnInfoCallback": function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {

            },
            "rowCallback": function (row, data, Object, index) {
                $(row).find('td').each(function () {
                    if ($(this).text() === 'false')
                        $(this).text('Não');
                    if ($(this).text() === 'true')
                        $(this).text('Sim');
                });
                $(actionEditar, row).bind('click', () => {
                    editarClickHandler(data);
                });
                return row;
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