var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#DT_load').dataTable({
        "ajax": {
            "url": "/api/book",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "author", "width": "25%" },
            { "data": "isbn", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center" style="margin: 0 auto; padding: 0px;">\
                                <a href="/BookList/EditBook?id=${data}" class="btn btn-success text-white" style="cursor: pointer; width: 40%; margin: 0 auto; margin-right: 5%;">Edit</a>\
                                <a class="btn btn-danger text-white" style="cursor: pointer; width: 40%; margin: 0 auto; margin-left: 5%;">Delete</a>\
                            </div>`;
                },
                "width": "35%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    })
}