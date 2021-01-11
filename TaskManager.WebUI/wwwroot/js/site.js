// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var modalPlaceholder = $('#modal_placeholder');

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

function closeModal() {
    $('.modal_overlay').animate({ opacity: 0 }, 250, function () {
        modalPlaceholder.empty();
    });
}

// Update the task's finished status after clicking on "finished" indicator
function checkedClicked(selector) {
    var id = selector.attr('id');
    $.get('/Tasks/CheckedChanged', { id: id, isChecked: selector.is(":checked") }, function () { })
}
