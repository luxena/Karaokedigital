$(function () {

    $("#Username").on('keyup', function () {

        var dataString = "username=" + $("#Username").val();

        $.ajax({

            type: "POST",
            url: "/User/VerifyUsername",
            data: dataString,
            success: function (data) {

                if (data != "The field is empty") {

                    if (data == "Username not exists") {

                        $("#usernameValidation").removeClass("text-danger").addClass("text-success");
                    } else {
                        $("#usernameValidation").removeClass("text-success").addClass("text-danger");
                    }

                    $("#usernameValidation").text(data);

                } else {
                    $("#usernameValidation").text("");
                }

              

            }

        });

    });

    $("#Email").on('keyup', function () {

        var dataString = "email=" + $("#Email").val();

        $.ajax({

            type: "POST",
            url: "/User/VerifyEmail",
            data: dataString,
            success: function (data) {

                if (data != "The field is empty") {
                    if (data == "Email not exists") {

                        $("#emailValidation").removeClass("text-danger").addClass("text-success");
                    } else {
                        $("#emailValidation").removeClass("text-success").addClass("text-danger");
                    }

                    $("#emailValidation").text(data);
                } else {
                    $("#emailValidation").text("");
                }

                

            }

        });

    });

    $("#Phone").on('keyup', function () {

        var dataString = "phone=" + $("#Phone").val();

        $.ajax({

            type: "POST",
            url: "/User/VerifyPhone",
            data: dataString,
            success: function (data) {

                if (data != "The field is empty") {
                    if (data == "Phone not exists") {

                        $("#phoneValidation").removeClass("text-danger").addClass("text-success");
                    } else {
                        $("#phoneValidation").removeClass("text-success").addClass("text-danger");
                    }

                    $("#phoneValidation").text(data);
                } else {
                    $("#phoneValidation").text("");
                }

               

            }

        });

    });

    
});

  
