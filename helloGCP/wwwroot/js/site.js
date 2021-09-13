// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function searchRows() {

    var table = document.getElementById("fruit-table");
    var searchString = document.getElementById("txtSearch");
    var message = document.getElementById("message")

    for (var i = 1, row; row = table.rows[i]; i++) {
        let currentFruit = row.cells[1].textContent.toLowerCase();
        if (!currentFruit.includes(searchString.value.toLowerCase())) {
            table.rows[i].style.display = "none";
        }
        else {
            table.rows[i].style.display = "";
        }
    }

    function showAllRows() {
        for (var i = 1, row; row = table.rows[i]; i++) {
            document.getElementById("fruit-table").rows[i].style.display = "";
        }
    }
}