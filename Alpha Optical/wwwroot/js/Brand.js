﻿var dataTable

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/Brand/getall' }, // API URL should retrieve all data

        "columns": [
            { data: 'name' },
            { data: 'displayOrder' },
         
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                     <a href="/admin/Brand/Edit?id=${data}" class="btn btn-success mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                    <a href="/admin/Brand/Delete?id=${data}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
             
            }
        ]

    });

}
