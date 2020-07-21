// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#btnAddemployeedetail').click(function () {
        window.location.href = '/Home/SaveEmployee/';
    });

    $('#frmsaveemployee').submit(function (event) {
        event.preventDefault();
        if ($('#frmsaveemployee .input-validation-error').length == 0) {
            $f = $(event.currentTarget);
            var url = $f.attr("action");
            var form_data = $(this).serialize();
            $.ajax({
                url: url,
                type: "POST",
                data: form_data,
                success: function (result) {
                    if (result == true) {
                        displayMessage('alert alert-success alert-dismissible fade show', 'Employee detail saved successfully.');
                    }
                    else {
                        displayMessage('alert alert-danger alert-dismissible fade show', 'A problem has been occurred while submitting.');
                    }
                },
                error: function (err) {
                    displayMessage('alert alert-danger alert-dismissible fade show', 'A problem has been occurred while submitting.');
                },
                complete: function (data) {
                    //$('#msg').delay(5000).fadeOut('slow');
                    $('#msg').fadeOut(5000, function () {
                        window.location.href = '/Home/Index/';
                    });
                }
            });
        }        
    });
    //$('[name = "empdelete"]').click(function () {
    //    var check = confirm("Are you sure you want to delete?");
    //    if (check == true) {
    //        $.ajax({
    //            url: '/Home/DeleteEmployee',
    //            type: 'POST',
    //            data: { 'id': this.id },
    //            success: function (result) {
    //                if (result == true) {
    //                    displayMessage('alert alert-success alert-dismissible fade show', 'Employee detail deleted successfully.');                       
    //                }
    //                else {
    //                    displayMessage('alert alert-danger alert-dismissible fade show', 'A problem has been occurred while deleting.');
    //                } 
    //            },
    //            error: function (err) {
    //                displayMessage('alert alert-danger alert-dismissible fade show', 'A problem has been occurred while deleting.');
    //            },
    //            complete: function (data) {
    //                $('#msg').delay(2000).fadeOut(function () {
    //                    location.reload();
    //                });                  
    //            }
    //        });
    //    }
    //    else {
    //        return false;
    //    }
    //});

    $("#btnempsearch").click(function () {
        let searchValue = $('#txtempsearch').val();
            $.ajax({
                url: '/Home/SearchEmployee',
                type: 'GET',
                data: { 'search': searchValue.trim() },
                contentType: "application/json; charset=utf-8",
                dataType: "html",
                success: function (result) {
                    $('#empsearchresult').html(result);
                },
                error: function (err) {
                    displayMessage('alert alert-danger alert-dismissible fade show', 'A problem has been occurred while deleting.');
                },
                complete: function (data) {
                }
            });
    });
});

function displayMessage(classname, msgtext) {
    $('#msg').show();
    $('#msg').removeClass();
    $('#msg').addClass(classname);
    $('#msgbody').text(msgtext);
}

function removeItem(id)
{
        var check = confirm("Are you sure you want to delete?");
        if (check == true) {
            $.ajax({
                url: '/Home/DeleteEmployee',
                type: 'POST',
                data: { 'id': id },
                success: function (result) {
                    if (result == true) {
                        displayMessage('alert alert-success alert-dismissible fade show', 'Employee detail deleted successfully.');                       
                    }
                    else {
                        displayMessage('alert alert-danger alert-dismissible fade show', 'A problem has been occurred while deleting.');
                    } 
                },
                error: function (err) {
                    displayMessage('alert alert-danger alert-dismissible fade show', 'A problem has been occurred while deleting.');
                },
                complete: function (data) {
                    $('#msg').delay(2000).fadeOut(function () {
                        location.reload();
                    });                  
                }
            });
        }
        else {
            return false;
        }
}


