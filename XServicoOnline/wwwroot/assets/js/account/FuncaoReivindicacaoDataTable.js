FuncaoReivindicacaoDataTable = (function () {

    let tableName = '#tableFuncaoReivindicacao';
    let actionDetalhes = '.action-detalhe';
    let actionEditar = '.action-editar';
    let actionRemover = '.action-remover';
    let actionId = '.funcao-reivindicacao-id';
    let actionFuncaoId = '.funcao-id';
    FuncaoReivindicacaoDataTable.prototype.Load = function () {
        criarTableFuncaoReivindicacao();

    };
    var criarAction = function () {
        $(actionDetalhes).click(function () {
            let id = $(this).parent().parent().parent().parent().find(actionId).text();
            window.location = './Detalhes?id=' + id;
        });
        //$(actionEditar).click(function () {
        //    var id = $(this).parent().parent().parent().parent().find(actionId).text()
        //    window.location = './Editar?id=' + id;
        //});
    };
    var removerClickHandler = function (row,funcaoReivindicacao) {
        var builderHtml = [];
        builderHtml.push('<div class="modal" tabindex="-1" role="dialog">');
        builderHtml.push('<div class="modal-dialog" role="document">');
        builderHtml.push('        <div class="modal-content">');
        builderHtml.push('            <div class="modal-header">');
        builderHtml.push('                <h5 class="modal-title">Modal title</h5>');
        builderHtml.push('                <button type="button" class="close" data-dismiss="modal" aria-label="Close">');
        builderHtml.push('                    <span aria-hidden="true">&times;</span>');
        builderHtml.push('                </button>');
        builderHtml.push('            </div>');
        builderHtml.push('            <div class="modal-body">');
        builderHtml.push('                <p>Modal body text goes here.</p>');
        builderHtml.push('            </div>');
        builderHtml.push('            <div class="modal-footer">');
        builderHtml.push('                <button type="button" id="deletarReivindicacao" class="btn btn-primary pull-right">Excluir</button>');
        builderHtml.push('                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>');
        builderHtml.push('            </div>');
        builderHtml.push('        </div>');
        builderHtml.push('    </div>');
        builderHtml.push('</div>');
        $('.body-reivindicacao').append(builderHtml.join(""));
        $('.modal').modal();
        builderHtml = [];
        $('#deletarReivindicacao').click(function () {
            $('.modal').modal('toggle');
            //window.location = './RemoverFuncaoReivindicacao?funcaoId=' + funcaoReivindicacao.funcaoId + '&funcaoReivindicacaoId=' + funcaoReivindicacao.id;
            $.post('RemoverFuncaoReivindicacao', { funcaoId: funcaoReivindicacao.funcaoId, funcaoReivindicacaoId: funcaoReivindicacao.id }, function (data) {
                if (data.retorno.erro === undefined || data.retorno.erro === null) {
                    md.notificacaoSucesso('top', 'right', data.retorno.sucesso);
                    removeTr(row);
                } else {
                    md.notificacaoErro('top', 'right', data.retorno.erro);
                }
               
            });
        });
       
    };
    var removeTr = function (item) {
        var tr = $(item).closest('tr');
        tr.remove();
    };
    var detalheClickHandler = function (funcaoReivindicacao) {
        window.location = './CreateFuncaoReivindicacao?funcaoId=' + funcaoReivindicacao.funcaoId;
    };
    var criarTableFuncaoReivindicacao = function () {

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
                { "className": "td-invisivel funcao-reivindicacao-id coluna-grid-padding", "targets": [2], "searchable": false },
                { "className": "td-invisivel funcao-id coluna-grid-padding", "targets": [3], "searchable": false },
                { "className": "text-center custom-middle-align coluna-grid-padding", "targets": [0, 1, 2] }
            ],
            "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
            "processing": true,
            "serverSide": true,
            "ajax":
            {
                "url": "GetFuncoesReivindicacoes",
                "type": "POST",
                "dataType": "JSON"
            },
            "columns": [
                { "data": "reivindicacao" },
                { "data": "valor" },
                { "data": "id" },
                { "data": "funcaoId" },
                { "defaultContent": '<div class="form-group default-content-data-table" style="margin-left: -1%;"><a class="action-remover" title="Excluir"><i class="material-icons editarclass" data-toggle="tooltip" data-placement="top" title="Excluir a reivindicação">delete_forever</i><span class="sr-only aria-hidden="true"">Excluir</span></a></div>' }
            ],
            "initComplete": function () {

            },
            "fnUpdate": function () {

            },
            "fnInfoCallback": function (oSettings, iStart, iEnd, iMax, iTotal, sPre) {
                console.log(oSettings);
            },
            "rowCallback": function (row, data, Object, index) {
                $(actionRemover, row).bind('click', () => {
                    removerClickHandler(row,data);
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