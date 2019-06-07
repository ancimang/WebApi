$(document).ready(function () {

    var http = "http://";
    var host = window.location.host;
    var token = null;
    var headers = {};
    var editingId;

    var pozoristaEndpoint = "/api/pozorista/";
    var predstaveEndpoint = "/api/predstava/";
    var gradEndpoint = "/api/pozoriste/grad/"; 

    $("#btnLogin").click(function () {

        $("#prijava").css("display", "block");
        $("#btnLogin").css("display", "none");
        $("#btnRegister").css("display", "none");

    });
    $("#btnRegister").click(function () {
           
        $("#registracija").css("display", "block");
        $("#btnLogin").css("display", "none");
        $("#btnRegister").css("display", "none");
    });


    $("#odustajanjePrijava").click(function () {

        $("#prijava").css("display", "none"); 
        $("#btnLogin").css("display", "block");
        $("#btnRegister").css("display", "block");

    });
    $("#odustajanjeRegistracija").click(function () {
        
        $("#registracija").css("display", "none");  
        $("#btnLogin").css("display", "block");
        $("#btnRegister").css("display", "block");

    });
    
    // //////////------------REGISTRACIJA------------------------------------------------
    $("#registrovanjeDugme").click(function (e) {
        e.preventDefault();
        $("#skrivenaRegistracija").trigger("click");
    });
    $("#registracija").submit(function (e) {
        e.preventDefault();

        var email = $("#regEmail").val();
        var loz1 = $("#regLoz").val();
        var loz2 = $("#regLoz2").val();

        // objekat koji se salje
        var sendData = {
            "Email": email,
            "Password": loz1,
            "ConfirmPassword": loz2
        };

        $.ajax({
            type:"POST",
            url: "http://" + host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            $("#registracija").css("display", "none");
            $("#info").empty();
            $("#info").append("Uspešna registracija na sistem!.");          
            $("#prijava").css("display", "block");
            clearForm();


        }).fail(function (data) {
            alert("Greska prilikom registracije!");
        });
    });
    
    // //////////------------PRIJAVA------------------------------------------------
    $("#prijavaDugme").click(function (e) {
        e.preventDefault();
        $("#skrivenaPrijava").trigger("click");
    });
    $("#prijava").submit(function (e) {
        e.preventDefault();

        var email = $("#priEmail").val();
        var loz = $("#priLoz").val();

        // objekat koji se salje
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        $.ajax({
            "type": "POST",
            "url": "http://" + host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            $("#info").hide();
            $("#infoDiv").append("Prijavljen korisnik: " + data.userName);
            token = data.access_token;
            $("#qwerty").css("display", "block");
            $("#zaglavlje").css("display", "block");
            $("#prijava").css("display", "none");
            $("#btnLogin").css("display", "none");
            $("#btnRegister").css("display", "none");
            $("#pozdrav").empty();
            $("#donja").empty();
            $("#odjava").css("display", "block");
            clearForm();

        }).fail(function (data) {
            alert("Greska prilikom prijavljivanja!");
        });
    });
    ////////////////////////////////////ODJAVA/////////////////////////////////////////////////////////
    $("#odjavise").click(function () {
        token = null;
        headers = {};
        formAction = "Create";
        editingId = "";

        $("#infoDiv").empty();
        $("#zaglavlje").css("display", "none");
        $("#btnLogin").css("display", "block");
        $("#btnRegister").css("display", "block");
        $("#pozdrav").s;
        $("#donja").append("#donja");

    });


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    $("#noviSad").click(function () {
        $("#pozdrav").empty();
        $("#pozdrav").add("#table");
        $("#table").css("display", "block");
        
        setHomeTableNS();
    });
    $("#beograd").click(function () {
        $("#pozdrav").empty();
        $("#pozdrav").add("#table");
        $("#table").css("display", "block");

        setHomeTableBG();
    });
    $("#sombor").click(function () {
        $("#pozdrav").empty();
        $("#pozdrav").add("#table");
        $("#table").css("display", "block");

        setHomeTableSO();
    });

    function setHomeTableNS() {
       
        $.ajax({
            "type": "GET",
            "url": http + host + gradEndpoint + "?grad=novi+sad",
            "headers": headers

        })
       .done(function (data) {
            
            punjenjeTabela(data);

        }).fail(function (data) {
            alert(data.status + ": " + data.statusText);
        });
    }
    function setHomeTableBG() {

        $.ajax({
            "type": "GET",
            "url": http + host + gradEndpoint + "?grad=beograd",
            "headers": headers

        })
            .done(function (data) {
               punjenjeTabela(data);

            }).fail(function (data) {
                alert(data.status + ": " + data.statusText);
            });
    }
    function setHomeTableSO() {

        $.ajax({
            "type": "GET",
            "url": http + host + gradEndpoint + "?grad=sombor",
            "headers": headers

        })
            .done(function (data) {

                punjenjeTabela(data);

            }).fail(function (data) {
                alert(data.status + ": " + data.statusText);
            });
    }
    function punjenjeTabela(data) {
        var $container = $("#tablebody");
        $container.empty();

        if (token) {
            $("#kolDelete").show();
            $("#kolEdit").show();
            $("#table").css("width", "100%");
            for (i = 0; i < data.length; i++) {
                var row = '<tr>';
                var displayData = '<td>' + data[i].Naziv + '</td><td>' + data[i].UpravnikImePrezime + '</td><td>' + data[i].GodinaOsnivanja
                    + '</td><td>' + data[i].Grad + '</td><td>' + data[i].Adresa + '</td><td>' + data[i].Telefon + '</td><td>' + data[i].Email +'</td>';
                var stringId = data[i].Id.toString();
                var displayDelete = "<td><button id='btnDelete' style='background-color:red' class='btn btn-md glyphicon glyphicon-trash' name=" + stringId + "></button></td>";

                var displayEdit = "<td><button id='btnEdit' style='background-color:skyblue' class='btn btn-md glyphicon glyphicon-edit' name=" + stringId + "></button></td>";

                row += displayData + displayDelete + displayEdit + "</tr>";

                $container.append(row);
            }
        }
        else {
            $("#kolDelete").hide();
            $("#kolEdit").hide();
            for (i = 0; i < data.length; i++) {

                row = "<tr>";
                displayData = '<td>' + data[i].Naziv + '</td><td>' + data[i].UpravnikImePrezime + '</td><td>' + data[i].GodinaOsnivanja
                    + '</td><td>' + data[i].Grad + '</td><td>' + data[i].Adresa + '</td><td>' + data[i].Telefon + '</td><td>' + data[i].Email + '</td>';
                row += displayData + "</tr>";
                $container.append(row);
            }
        }
    }
    function clearForm() {
       
        document.getElementById("#regEmail").value = "";
        document.getElementById("#regLoz").value = "";
        document.getElementById("#regLoz2").value = "";

    }
});