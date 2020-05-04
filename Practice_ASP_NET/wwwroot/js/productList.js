var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/products/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "weight", "width": "20%" },
            { "data": "amount", "width": "20%" },
            { "data": "manufacturer", "width": "20%" },
            { "data": "description", "width": "20%" }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}