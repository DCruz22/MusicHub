$(document).ready(function () {

    $('.datepicker').pickadate({
        selectMonths: true, // Creates a dropdown to control month
        selectYears: 30 // Creates a dropdown of 15 years to control year
    });

    $("select").material_select();

    Dropzone.options.myDropZone = {
        maxFilesize: 1,
        maxFiles: 1,
        acceptedFiles: "image/*",
        uploadMultiple: false,
        url: "/User/SaveImage",
        init: function () {
            myDropzone = this;
            this.on("success", function (file, result) {
                $('input[id=photoTempUrl]').val(result);
            });
            this.on("maxfilesexceeded", function () {
                alert("Only 1 image permitted");
                clearDropzone();
            });
        }
    };

});

function FriendShipSuccess(data) {
    $('#friendshipContainer-' + data.userFollowedId).html(data.result);
}


function ChangeProfilePictureComplete() {
    window.location.href = "../Home/Index";
}

function clearDropzone() {

    myDropzone.removeAllFiles();

    $('input[id=photoTempUrl]').val('');
}