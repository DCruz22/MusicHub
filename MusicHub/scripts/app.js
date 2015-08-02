$(document).ready(function () {

    $('.datepicker').pickadate({
        selectMonths: true, // Creates a dropdown to control month
        selectYears: 30 // Creates a dropdown of 15 years to control year
    });

    $("select").material_select();

});

function ChangeProfilePictureComplete() {
    window.location.href = "../Home/Index";
}