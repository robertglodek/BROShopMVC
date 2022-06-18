
///function used to change page size when clicking select menu.
function ChangePageSize(area, controller, action, obj, searchText, searchName, sorting) {
    window.location.href = `/${area}/${controller}/${action}?PageSize=${obj.value}&SearchName=${searchName}&SearchValue=${searchText}&SortBy=${sorting}`;
}

///function used to change sorting when clicking select menu.
function ChangeSorting(area, controller, action, obj, searchText, searchName, currentPage, pageSize) {
    window.location.href = `/${area}/${controller}/${action}?SortBy=${obj.value}&SearchName=${searchName}&SearchValue=${searchText}&PageNumber=${currentPage}&PageSize=${pageSize}`;
}

///Js funcion used to open and close center delete confirmation window
function ToggleDeleteConfirmWindow(displayWindow, id) {
    if (displayWindow == true) {
        document.querySelector(".deleteConfirmationWrapper").style.display = "block";
        document.querySelector("#itemToDeleteId").value = id;
    }
    else {
        document.querySelector(".deleteConfirmationWrapper").style.display = "none";
    }
}
























// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.












///event handler for modyfying the shopping cart










