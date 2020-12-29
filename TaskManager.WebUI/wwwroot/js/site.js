// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const add_task_modal = document.querySelector('#add_task_modal_overlay');
const add_modalBtn = document.querySelector('#add_task_button');
const add_closeBtn = document.querySelector('#add_close_modal');

const update_task_modal = document.querySelector('#add_task_modal_overlay');
const add_modalBtn = document.querySelector('#add_task_button');
const add_closeBtn = document.querySelector('#add_close_modal');


// Events
add_modalBtn.addEventListener('click', openAddModal);
add_closeBtn.addEventListener('click', closeAddModal);
window.addEventListener('click', outsideClick);

// Open
function openAddModal() {
    add_task_modal.style.display = 'flex';
}

// Close
function closeAddModal() {
    add_task_modal.style.display = 'none';
}

// Close If Outside Click
function outsideClick(e) {
    if (e.target == add_task_modal) {
        add_task_modal.style.display = 'none';
    }
}

function checkedClicked(selector) {
    var id = selector.attr('id');
    $.get('/Tasks/CheckedChanged', { id: id, isChecked: selector.is(":checked") }, function () { })
}

function updateTask() {

}