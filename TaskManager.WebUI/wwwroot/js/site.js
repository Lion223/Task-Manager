// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Modal placeholder div
var modalPlaceholder = $('#modal_placeholder');

// Create task button that uses modal placeholder to display the modal
$('#create_modal_button').click(loadModal);

// Double click on the task div that uses modal placeholder to display the update task modal
$('.update_modal_button').dblclick(loadModal);

// Delete task button that uses modal placeholder to display the modal
$('.delete_task_button').click(loadModal);

// Call for the sign-out modal window
$('#sign_out_button').click(loadModal);

// Load the appropriate modal window
function loadModal(e) {
    e.stopPropagation();
    var url = $(this).data('url');
    $.get(url).done(function (data) {
        modalPlaceholder.html(data);

        var ModalOverlay = $('#modal_overlay');
        var closeModalButton = $('#close_modal_button');

        ModalOverlay.css('visibility', 'visible');
        ModalOverlay.animate({ opacity: 1 }, 250);

        closeModalButton.click(closeModal);
        window.addEventListener("click", clickedOutside);
    });
}

// If user has clicked outside of modal window - then close the modal window
function clickedOutside(e) {
    if (!document.getElementsByClassName('modal_window')[0].contains(e.target)) {
        window.removeEventListener("click", clickedOutside);
        closeModal();
    } 
}

// Modal close function
function closeModal() {
    $('.modal_overlay').animate({ opacity: 0 }, 250, function () {
        window.removeEventListener("click", clickedOutside);
        modalPlaceholder.empty();
    });
}

// AJAX validation inside create and update modal overlays
function onValidateTask(data) {

    if (data.status === "success") {
        window.location.replace("/");
    }

    var summary = $("[data-valmsg-summary]");
    var ul = summary.find("ul");
    ul.empty();
    $.each(data.formErrors, function () {
        if (this.errors.length > 0) {
            summary.children('ul').append($('<li></li>').text(this.errors.join()));
            // If each field needs to have it's own validation field filled:
                //$("[data-valmsg-for=" + this.key + "]").html(this.errors.join());
        }
        
    });
}

// Update the task's finished status after clicking on "finished" indicator
function checkedClicked(selector) {
    var id = selector.attr('id');
    var isChecked = selector.is(":checked");
    $.get(`/Finished/${id}/${isChecked}`, function () { })
}

// Loader overlay
function showLoader() {
    var loader = document.getElementById('loader');
    var checkbox = $('.task_finished_checkbox');
    loader.style.visibility = "visible";
    checkbox.prop('checked', true);
    loader.animate({ opacity: 1 }, 250);
}
