'use-strict';
$(document).ready(function() {

    $("#btnRegister").click(function(e) {
        e.preventDefault();

        var userDetails = {};
        userDetails.Fname = $("#fname").val();
        userDetails.Mname = $("#mname").val();
        userDetails.Lname = $("#lname").val();
        userDetails.IdNumber = $("#idnumber").val();
        userDetails.PhoneNumber = $("#phonenumber").val();
        userDetails.AccountNumber = $("#accountnumber").val();
        userDetails.Email = $("#email").val();
        userDetails.Password1 = $("#password1").val();
        userDetails.Password2 = $("#password2").val();

        $.ajax({
            url: "http://localhost:3030/api/AddNewUser/",
            type: "POST",
            crossDomain: true,
            // headers: { "Access-Control-Allow-Origin:": "*" },
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(userDetails),
            dataType: "json",
            success: function(response) {
                alert(response);
            },

            error: function(x, e) {
                alert('Failed');
                alert(x.responseText);
                alert(x.status);

            }
        });
    });

    //other buttons here


});

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function IsValidEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/ig;
    return expr.test(email);
};
function EmailValidator(emailadd) {
    var isValid = $("#register_feedback");
    if (!IsValidEmail(emailadd.value)) {
        Swal.fire
        ({
            title: "Email Validation Error!",
            text: "Invalid Email address detected!",
            type: "error",
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "Provide a valid email!",
            confirmButtonClass: "btn-danger",
            confirmButtonColor: "#ec6c62",
            position: "center"
        }).then(() => {
            isValid.css("display", "block");
            isValid.css("color", "red");
            isValid.attr("class", "alert alert-danger");
            isValid.html("Email is Invalid!");
            $("#txtEmail").focus();
            $("#txtEmail").css("border", "solid 1px red");
        });
        return;
    }
    //isValid.css("display", "block");
    //isValid.css("color", "green");
    //isValid.attr("class", "alert alert-success");
    //isValid.html("Email is valid");    
}

function IsPhoneNumberValid(phone) {
    var expr = /^(?:254|\+254|0)?(7(?:(?:[129][0-9])|(?:0[0-8])|(4[0-1]))[0-9]{6})$/ig;
    return expr.test(phone);
}
function PhoneNoValidator(phonenum) {
    var isValid = $("#register_feedback");
    if (!IsPhoneNumberValid(phonenum.value)) {
        Swal.fire
        ({
            title: "Mobile Number Validation Error!",
            text: "Invalid Mobile Number detected!",
            type: "error",
            showCancelButton: true,
            closeOnConfirm: false,
            confirmButtonText: "Provide a valid Phone Number!",
            confirmButtonClass: "btn-danger",
            confirmButtonColor: "#ec6c62",
            position: "center"
        }).then(() => {
            isValid.css("display", "block");
            isValid.css("color", "red");
            isValid.attr("class", "alert alert-danger");
            isValid.html("Provide a valid Phone Number!");
            $("#txtPhoneNumber").focus();
            $("#txtPhoneNumber").css("border", "solid 1px red");
        });
        return
    }    
}