var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Movies/GetAllMovies",
            "type": "GET",
            "datatype": "json",
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "genre.name", "width": "20%" }
        ]
    });
}
