// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Modal placeholder div
var modalPlaceholder = $('#modal_placeholder');

// Create task button that uses modal placeholder to display the modal
$('#create_modal_button').click(function (event) {
    var url = $(this).data('url');
    $.get(url).done(function (data) {
        modalPlaceholder.html(data);

        var ModalOverlay = $('#modal_overlay');
        var closeModalButton = $('#close_modal_button');

        ModalOverlay.css('visibility', 'visible');
        ModalOverlay.animate({ opacity: 1 }, 250);

        closeModalButton.click(closeModal);
        ModalOverlay.click(function (e) {
            if (e.target === this) {
                closeModal();
            }
        });
    });
});

// Double click on the task div that uses modal placeholder to display the update task modal
$('.update_modal_button').dblclick(function (event) {
    var url = $(this).data('url');
    $.get(url).done(function (data) {
        modalPlaceholder.html(data);

        var ModalOverlay = $('#modal_overlay');
        var closeModalButton = $('#close_modal_button');

        ModalOverlay.css('visibility', 'visible');
        ModalOverlay.animate({ opacity: 1 }, 250);

        closeModalButton.click(closeModal);
        ModalOverlay.click(function (e) {
            if (e.target === this) {
                closeModal();
            }
        });
    });
});

// Delete task button that uses modal placeholder to display the modal
$('.delete_task_button').click(function (event) {
    var url = $(this).data('url');
    $.get(url).done(function (data) {
        modalPlaceholder.html(data);

        var ModalOverlay = $('#modal_overlay');
        var closeModalButton = $('#close_modal_button');

        ModalOverlay.css('visibility', 'visible');
        ModalOverlay.animate({ opacity: 1 }, 250);

        closeModalButton.click(closeModal);
        ModalOverlay.click(function (e) {
            if (e.target === this) {
                closeModal();
            }
        });
    });
});

// Modal close function
function closeModal() {
    $('.modal_overlay').animate({ opacity: 0 }, 250, function () {
        modalPlaceholder.empty();
    });
}

// Update the task's finished status after clicking on "finished" indicator
function checkedClicked(selector) {
    var id = selector.attr('id');
    var isChecked = selector.is(":checked");
    $.get(`/Finished/${id}/${isChecked}`, function () { })
}
