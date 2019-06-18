$(document).ready(function () {

    var http = "http://";
    var host = window.location.host;
    var token = null;
    var headers = {};    
    var editingId;
    var formAction = "Create";

    ////////////RUTE//////////////////////////////////
    var pozoristaEndpoint = "/api/pozorista/";
    var predstaveEndpoint = "/api/predstave/";
    var vrstePredstaveEndpoint = "/api/vrstepredstave/";
    var gradEndpoint = "/api/pozoriste/grad/"; 
    var glumciEndpoint = "/api/glumci/";

    var pozAdd = document.getElementById("pozAdd");
    var table = document.getElementById("table");
    var newAdd = document.getElementById("newAdd");
    var editovanje = document.getElementById("editovanje");

    //// pripremanje dogadjaja za brisanje
    $("#tablebody").on("click", "#btnDelete", deletePozoriste);
    $("#bodyPredstave").on("click", "#btnPre", deletePredstava);
    $("#bodyGlumci").on("click", "#btnGlumac", deleteGlumac);
    $("#bodyVrste").on("click", "#btnVrste", deleteVrste);
    
    //// priprema dogadjaja za izmenu
    $("#tablebody").on("click", "#btnEdit", editPozoriste);
    $("#bodyPredstave").on("click", "#btnEditPre", editPredstava);
    $("#bodyGlumci").on("click", "#btnEditGlu", editGlumac);
    $("#bodyVrste").on("click", "#btnEditVrste", editVrste);


    $("#btnLogin").click(function () {

        $("#prijava").css("display", "block");
        $("#btnLogin").css("display", "none");
        $("#btnRegister").css("display", "none");
        $("#table").css("display", "none");
    });
    $("#btnRegister").click(function () {
           
        $("#registracija").css("display", "block");
        $("#btnLogin").css("display", "none");
        $("#btnRegister").css("display", "none");
        $("#table").css("display", "none");
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
            $("#start").css("display", "none");
            $("#zaglavlje").css("display", "block");
            PadajuciPozorista();
            popuniVrstePredstave();                 
            $("#odjava").css("display", "block");

        }).fail(function (data) {
            alert("Greska prilikom prijavljivanja!");
        });
    });
    //////////////////////////////////// ODJAVA ///////////////////////////////////////////////////////////////////////////////
    $("#odjavise").click(function () {
        token = null;
        headers = {};
        formAction = "Create";
        editingId = "";

        window.location.reload(true);
    });
    ////////////////////////////////////// SETOVANJE TABELA  /////////////////////////////////////////////////////////////////
   
    $("#noviSad").click(function () {
       
        $("#table").css("display", "block");
        $("#quote").replaceWith($("#table"));
        setHomeTableNS();
    });
    $("#beograd").click(function () {
        
        $("#table").css("display", "block");
        $("#quote").replaceWith($("#table"));
        setHomeTableBG();
    });
    $("#sombor").click(function () {
        
        $("#table").css("display", "block");
        $("#quote").replaceWith($("#table"));
        setHomeTableSO();
    });
    //////////////pocetne slike popuna tabela////////////////////////
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
    ///////////////////////////////////Tabela za kategorije//////////////////////////////////////////
    function setHomePredstave() {

        $.ajax({
            "type": "GET",
            "url": http + host + predstaveEndpoint,
            "headers": headers

        })
        .done(function (data) {

            punjenjePredstave(data);

        }).fail(function(data) {
            alert(data.status + ": " + data.statusText);
        });
    }
    function setHomePozorista() {

        $.ajax({
            "type": "GET",
            "url": http + host + pozoristaEndpoint,
            "headers": headers
        })
        .done(function (data) {

            punjenjeTabela(data);

        }).fail(function (data) {
            alert(data.status + ": " + data.statusText);
        });
    }
    function setHomeGlumci() {

        $.ajax({
            "type": "GET",
            "url": http + host + glumciEndpoint,
            "headers": headers

        })
        .done(function(data) {
            punjenjeGlumci(data);

        }).fail(function (data) {
            alert(data.status + ": " + data.statusText);
        });
    }
    function setHomeVrste() {

        $.ajax({
            "type": "GET",
            "url": http + host + vrstePredstaveEndpoint,
            "headers": headers

        })
        .done(function (data) {

            punjenjeVrste(data);

        }).fail(function (data) {
            alert(data.status + ": " + data.statusText);
        });
    }
    ///////////////////////////////////////// PODACI ZA PUNJENJE pozoriste//////////////////////////////////////////////////////////////////
    function punjenjeTabela(data) {
        var $container = $("#tablebody");
        $container.empty();

        if (token) {
            $("#kolDelete").show();
            $("#kolEdit").show();
            $("#table").css("width", "100%");
            for (i = 0; i < data.length; i++) {
                var row = '<tr>';
                var displayData = "<td style='color: red'> &nbsp" + data[i].Naziv + '&nbsp</td><td>' + data[i].UpravnikImePrezime + '</td><td>' + data[i].GodinaOsnivanja
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
                displayData = "<td style='color: red'>&nbsp" + data[i].Naziv + '&nbsp</td><td>' + data[i].UpravnikImePrezime + '</td><td>' + data[i].GodinaOsnivanja
                    + '</td><td>' + data[i].Grad + '</td><td>' + data[i].Adresa + '</td><td>' + data[i].Telefon + '</td><td>' + data[i].Email + '</td>';
                row += displayData + "</tr>";
                $container.append(row);
            }
        }
    }
    ///////////////////////////////////////// PODACI ZA PUNJENJE Glumci//////////////////////////////////////////////////////////////////
    function punjenjeGlumci(data) {
        var $container = $("#bodyGlumci");

        $container.empty();

            for (i = 0; i < data.length; i++) {
                var row = '<tr>';
                var displayData = "<td>&nbsp" + data[i].Ime + '&nbsp</td><td>' + data[i].Prezime + '</td><td>' + data[i].GodinaRodjenja + '</td>';
                var stringId = data[i].Id.toString();
                var displayDelete = "<td><button id='btnGlumac' style='background-color:red' class='btn btn-md glyphicon glyphicon-trash' name=" + stringId + "></button></td>";

                var displayEdit = "<td><button id='btnEditGlu' style='background-color:skyblue' class='btn btn-md glyphicon glyphicon-edit' name=" + stringId + "></button></td>";

                row += displayData + displayDelete + displayEdit + "</tr>";

                $container.append(row);
            }
       
    }
    ///////////////////////////////////////// PODACI ZA PUNJENJE Glumci//////////////////////////////////////////////////////////////////
    function punjenjePredstave(data) {
        var $container = $("#bodyPredstave");
        $container.empty();

        for (i = 0; i < data.length; i++) {
            var row = '<tr>';
            var displayData = "<td style='color: red'>&nbsp" + data[i].Naziv + '&nbsp</td><td>' + data[i].ImeRezisera + '</td><td>' + data[i].Opis + '</td><td>' + data[i].DatumOdrazavanja + '</td><td>' + data[i].VremeOdrzavanja + '</td><td>'
                + data[i].CenaKarte + '</td><td>' + data[i].Premijera + '</td><td>' + data[i].VrstaPredstave.Naziv + '</td><td>' + data[i].Pozoriste.Naziv + '</td>';
            var stringId = data[i].Id.toString();
            var displayDelete = "<td><button id='btnPre' style='background-color:red' class='btn btn-md glyphicon glyphicon-trash' name=" + stringId + "></button></td>";

            var displayEdit = "<td><button id='btnEditPre' style='background-color:skyblue' class='btn btn-md glyphicon glyphicon-edit' name=" + stringId + "></button></td>";

            row += displayData + displayDelete + displayEdit + "</tr>";

            $container.append(row);
        }

    }
    /////////////////////////////////////////PODACI ZA PUNJENJE VRSTE////////////////////////////////////////////////////////////
    function punjenjeVrste(data) {
        var $container = $("#bodyVrste");
        $container.empty();

        for (i = 0; i < data.length; i++) {
            var row = '<tr>';
            var displayData = "<td style='color: red'>&nbsp" + data[i].Naziv + '&nbsp</td><td>' + data[i].Opis + '</td>';
            var stringId = data[i].Id.toString();
            var displayDelete = "<td><button id='btnVrste' style='background-color:red' class='btn btn-md glyphicon glyphicon-trash' name=" + stringId + "></button></td>";

            var displayEdit = "<td><button id='btnEditVrste' style='background-color:skyblue' class='btn btn-md glyphicon glyphicon-edit' name=" + stringId + "></button></td>";

            row += displayData + displayDelete + displayEdit + "</tr>";

            $container.append(row);
        }
    }
    /////////////////////////////////// DODDAVANJE ZA SVAKU Stavku U NAV BAR - u ///////////////////////////////////////
    $("#nav2").click(function () {
       
        document.getElementById("popuna").appendChild(editovanje); 
       
        $("#editovanje").css("display", "block");
        $("#newAdd").css("display", "none");
        
    });
    $("#nav3").click(function () {

        document.getElementById("popuna").appendChild(newAdd); 
        $("#newAdd").css("display", "block");
        $("#editovanje").css("display", "none");
    });
    $("#nav4").click(function () {
        $("#newAdd").css("display", "block");

    });
    ///////////NAV 2 ////////////////////////////////////////
    
    $("#pozEdit").click(function () {
        document.getElementById("mainEdit").appendChild(table);   
        setHomePozorista();
        $("#table").css("display", "block");
        $("#tabelaPredstava").css("display", "none");
        $("#tabelaGlumci").css("display", "none");
        $("#tabelaVrste").css("display", "none"); 
    });
    $("#predEdit").click(function () {
        document.getElementById("mainEdit").appendChild(tabelaPredstava);
        setHomePredstave();
        $("#tabelaPredstava").css("display", "block");   
        //$("#predAdd").css("display", "none");
        //$("#addGlu").css("display", "none");
        //$("#pozAdd").css("display", "none");
        $("#table").css("display", "none");
        $("#tabelaGlumci").css("display", "none");  
        $("#tabelaVrste").css("display", "none");  
    });
    $("#glumacEdit").click(function () {
        document.getElementById("mainEdit").appendChild(tabelaGlumci);
        setHomeGlumci();
        $("#tabelaGlumci").css("display", "block");   
        //$("#addGlu").css("display", "none");
        //$("#predAdd").css("display", "none");
        //$("#pozAdd").css("display", "none");
        $("#table").css("display", "none");
        $("#tabelaPredstava").css("display", "none");
        $("#tabelaVrste").css("display", "none");  
    });
    $("#vrsteEdit").click(function () {
        document.getElementById("mainEdit").appendChild(tabelaVrste);
        setHomeVrste();
        $("#tabelaVrste").css("display", "block");   
        //$("#addGlu").css("display", "block");
        //$("#predAdd").css("display", "none");
        //$("#pozAdd").css("display", "none");
        $("#table").css("display", "none");
        $("#tabelaPredstava").css("display", "none");
        $("#tabelaGlumci").css("display", "none");  
    });
    function openPoz() {
        document.getElementById("pozAdd").style.width = "100%";
        $("#pozAdd").css("display", "block"); 
    }
    function openPred() {
        document.getElementById("predAdd").style.width = "100%";
        $("#predAdd").css("display", "block");
    }
    function openGlu() {
        document.getElementById("addGlu").style.width = "100%";
        $("#addGlu").css("display", "block"); 
    }
    function openVrste() {
        document.getElementById("addVrste").style.width = "100%";
        $("#addVrste").css("display", "block"); 
    }
    ///////////NAV 3/////////////////////////////////////////
    $("#poz").click(function () {
        openPoz();
        $("#predAdd").css("display", "none");
        $("#addGlu").css("display", "none");
        $("#addVrste").css("display", "none");
    });
    $("#pred").click(function () {
        openPred();      
        $("#addGlu").css("display", "none");
        $("#pozAdd").css("display", "none");
        $("#addVrste").css("display", "none");
    });
    $("#glumac").click(function () {
        openGlu();         
        $("#predAdd").css("display", "none");
        $("#pozAdd").css("display", "none");
        $("#addVrste").css("display", "none");
    });    
    $("#vrste").click(function () {
        openVrste();
        $("#predAdd").css("display", "none");
        $("#pozAdd").css("display", "none");
        $("#addGlu").css("display", "none");
    });
    ///////////////////PADAJUCI MENI ZA DODAVANJE////////////////////////////////////////////////////////////////////////
   
    function PadajuciPozorista() {
        var select = document.getElementById("pozoristadId");

        $.ajax({
            url: http + host + pozoristaEndpoint,
            type: "GET"
        })
        .done(function (data) {
            if ($("#pozoristadId option").length === 0) {
                for (var i = 0; i < data.length; i++) {
                    if (select) {
                        select.options[i] = new Option(data[i].Naziv, data[i].Id );
                    }
                }
            }
        })
        .fail(function (data) {
            alert("Desila se greška pri dobavljanju pozorista.");
        });
    }
    function popuniVrstePredstave() {
        var select = document.getElementById("vrsteId");

        $.ajax({
            url: http + host + vrstePredstaveEndpoint,
            type: "GET"
        })
        .done(function (data) {
            if ($("#vrsteId option").length === 0) {
                for (var i = 0; i < data.length; i++) {
                    if (select) {
                        select.options[i] = new Option(data[i].Naziv, data[i].Id);
                    }
                }
            }
        })
        .fail(function (data) {
            alert("Desila se greška pozorista.");
        });
    }
    //////////////////////////////////KREIRAJ POZORISTE***************************************************************************

    $("#kreirajPoz").click(function (e) {

        e.preventDefault();
        $("#skriveniSubmitPoz").trigger("click");
    });
    $("#createPoz").submit(function (e) {
        e.preventDefault();

        var input1 = $("#pozPolje1").val();
        var input2 = $("#pozPolje2").val();
        var input3 = $("#pozPolje3").val();
        var input4 = $("#pozPolje4").val();
        var input5 = $("#pozPolje5").val();
        var input6 = $("#pozPolje6").val();
        var input7 = $("#pozPolje7").val();

        if (token) {
            headers.Authorization = "Bearer " + token;
        }
        //  u zavisnosti od akcije pripremam objekat
        if (formAction === "Create") {
            httpAction = "POST";
            url = http + host + pozoristaEndpoint;
            sendData = {
                "Naziv": input1,
                "UpravnikImePrezime": input2,
                "GodinaOsnivanja": input3,
                "Grad": input4,
                "Adresa": input5,
                "Telefon": input6,
                "Email": input7

            };
        }
        else {
            httpAction = "PUT";
            url = http + host + pozoristaEndpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "Naziv": input1,
                "UpravnikImePrezime": input2,
                "GodinaOsnivanja": input3,
                "Grad": input4,
                "Adresa": input5,
                "Telefon": input6,
                "Email": input7
            };
        }
        $.ajax({
            "url": url,
            "type": httpAction,
            "headers": headers,
            "data": sendData

        }).done(function (data, status) {
            formAction = "Create";
            document.getElementById("pozAdd").style.width = "0%";
            setHomePozorista();
            refreshPoz();

        }).fail(function (data, status) {
            alert("Desila se greska!");
        });
    });
    //////////////////////////////////KREIRAJ PREDSTAVU//////////////////////////////////////////////////////////////////////////////////////////////////
    $("#kreirajPre").click(function (e) {

        e.preventDefault();
        $("#skriveniSubmitPre").trigger("click");
    });
    $("#createEditPre").submit(function (e) {
        e.preventDefault();

        var input1 = $("#prePolje1").val();
        var input2 = $("#prePolje2").val();
        var input3 = $("#prePolje3").val();
        var input4 = $("#prePolje4").val();
        var input5 = $("#prePolje5").val();
        var input6 = $("#prePolje6").val();
        var input7 = $("#prePolje7").val();
        var vrste = $("#vrsteId").val();
        var inputId = $("#pozoristadId").val();

        if (token) {
            headers.Authorization = "Bearer " + token;
        }
        //  u zavisnosti od akcije pripremam objekat
        if (formAction === "Create") {
            httpAction = "POST";
            url = http + host + predstaveEndpoint;
            sendData = {
                "Naziv": input1,
                "ImeRezisera": input2,
                "Opis": input3,
                "DatumOdrazavanja": input4,
                "VremeOdrzavanja": input5 ,
                "CenaKarte": input6,
                "Premijera": input7,
                "VrstaPredstaveId": vrste,
                "PozoristeId": inputId
            };
        }
        else {
            httpAction = "PUT";
            url = http + host + predstaveEndpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "Naziv": input1,
                "ImeRezisera": input2,
                "Opis": input3,
                "DatumOdrazavanja": input4,
                "VremeOdrzavanja": input5,
                "CenaKarte": input6,
                "Premijera": input7,
                "VrstaPredstaveId": vrste,
                "PozoristeId": inputId
            };
        }
        $.ajax({
            "url": url,
            "type": httpAction,
            "headers": headers,
            "data": sendData

        }).done(function (data, status) {
            formAction = "Create";
            document.getElementById("predAdd").style.width = "0%";
            refreshPre();
            setHomePredstave();

        }).fail(function (data, status) {
            alert("Desila se greska!");
        });
    });
    ////////////////////////////////////KREIRAJ GLUMCA////////////////////////////////////////////////////////////////////////////////////

    $("#kreirajGlu").click(function (e) {

        e.preventDefault();
        $("#skriveniSubmitGlumac").trigger("click");
    });
    $("#createGlumac").submit(function (e) {
        e.preventDefault();

        var input1 = $("#imeGlu").val();
        var input2 = $("#preGlu").val();
        var input3 = $("#godiGlu").val();
      
        if (token) {
            headers.Authorization = "Bearer " + token;
        }
        //  u zavisnosti od akcije pripremam objekat
        if (formAction === "Create") {
            httpAction = "POST";
            url = http + host + glumciEndpoint;
            sendData = {
                "Ime": input1,
                "Prezime": input2,
                "GodinaRodjenja": input3
            };
        }
        else {
            httpAction = "PUT";
            url = http + host + glumciEndpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "Ime": input1,
                "Prezime": input2,
                "GodinaRodjenja": input3                
            };
        }
        $.ajax({
            "url": url,
            "type": httpAction,
            "headers": headers,
            "data": sendData

        }).done(function (data, status) {
            formAction = "Create";          
            document.getElementById("addGlu").style.width = "0%";
            setHomeGlumci();
            refreshGlu();
        }).fail(function (data, status) {
            alert("Desila se greska!");
        });
    });
    ///----------------------------
    $("#kreirajVrste").click(function (e) {

        e.preventDefault();
        $("#skriveniSubmitVrste").trigger("click");
    });
    $("#createVrste").submit(function (e) {
        e.preventDefault();

        var input1 = $("#nazivVrste").val();
        var input2 = $("#opisVrste").val();
        
        if (token) {
            headers.Authorization = "Bearer " + token;
        }
        //  u zavisnosti od akcije pripremam objekat
        if (formAction === "Create") {
           
            httpAction = "POST";
            url = http + host + vrstePredstaveEndpoint;
            sendData = {
                "Naziv": input1,
                "Opis": input2                
            };
        }
        else {
            httpAction = "PUT";
            url = http + host + vrstePredstaveEndpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "Naziv": input1,
                "Opis": input2   
            };
        }
        $.ajax({
            "url": url,
            "type": httpAction,
            "headers": headers,
            "data": sendData

        }).done(function (data, status) {
            formAction = "Create";
            refreshVrste();
            document.getElementById("addVrste").style.width = "0%";
            setHomeVrste();

        }).fail(function (data, status) {
            alert("Desila se greska!");
        });
    });
    ////////////////////////////////////////REFRESHOVANJE////////////////////////////////////////////////////////
    
    $("#refreshPoz").click(function (e) {
        $("#pozAdd").hide();
        refreshPoz();

    });
    function refreshPoz() {
        $("#pozPolje1").val("");
        $("#pozPolje2").val("");
        $("#pozPolje3").val("");
        $("#pozPolje4").val("");
        $("#pozPolje5").val("");
        $("#pozPolje6").val("");
        $("#pozPolje7").val("");
    }
    ///--------------
    $("#refreshPre").click(function (e) {
        $("#predAdd").hide();
        refreshPre();

    });
    function refreshPre() {
        $("#polje1").val("");
        $("#polje2").val("");
        $("#polje3").val("");
        $("#polje4").val("");
        $("#polje5").val("");
        $("#polje6").val("");
        $("#polje7").val("");
        $("#vrsteId").val = 1;
        $("#pozoristadId").val = 1;
    }
    ///------------------
    $("#refreshGlumac").click(function (e) {
        $("#addGlu").hide();
        refreshGlu();

    });
    function refreshGlu() {

        $("#imeGlu").val("");
        $("#preGlu").val("");
        $("#godGlu").val("");
    }
    ///----------------------
    $("#refreshVrste").click(function (e) {
        $("#addVrste").hide();
        refreshVrste();
    });
    function refreshVrste() {
       $("#nazivVrste").val("");
       $("#opisVrste").val("");
       
    }
    ///////////////////////////DELETING OBJECTS////////////////////////////////////////////////

    function deletePozoriste() {

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        var deleteId = this.name;

        $.ajax({
            "type": "DELETE",
            "url": http + host + pozoristaEndpoint + deleteId.toString(),
            "headers": headers,
            "data": deleteId.toString()

        }).done(function (data, status) {
            setHomePozorista();

        }).fail(function (data, status) {
            alert("Desila se greska pri brisanju!");
        });
    }
    
    function deletePredstava() {

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        var deleteId = this.name;

        $.ajax({
            "type": "DELETE",
            "url": http + host + pozoristaEndpoint + deleteId.toString(),
            "headers": headers,
            "data": deleteId.toString()

        }).done(function (data, status) {
            setHomePredstave();

        }).fail(function (data, status) {
            alert("Desila se greska pri brisanju!");
        });
    }
    function deleteGlumac() {

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        var deleteId = this.name;

        $.ajax({
            "type": "DELETE",
            "url": http + host + glumciEndpoint + deleteId.toString(),
            "headers": headers,
            "data": deleteId.toString()

        }).done(function (data, status) {
            setHomeGlumci();

        }).fail(function (data, status) {
            alert("Desila se greska pri brisanju!");
        });
    }
    function deleteVrste() {

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        var deleteId = this.name;

        $.ajax({
            "type": "DELETE",
            "url": http + host + vrstePredstaveEndpoint + deleteId.toString(),
            "headers": headers,
            "data": deleteId.toString()

        }).done(function (data, status) {
            setHomeVrste();

        }).fail(function (data, status) {
            alert("Desila se greska pri brisanju!");
        });
    }   
    ////////////////////////////////EDITING OBJECTS///////////////////////////////////////////////////////
    function editPozoriste() {
        openPoz();       
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var pozId = this.name;

        $.ajax({
            "type": "GET",
            "url": http + host + pozoristaEndpoint + pozId.toString(),
            "headers": headers
            //"data": pozId.toString()

        }).done(function (data, status) {
            editingId = data.Id;
            formAction = "Update";
             $("#pozPolje1").val(data.Naziv);
             $("#pozPolje2").val(data.UpravnikImePrezime);
             $("#pozPolje3").val(data.GodinaOsnivanja);
             $("#pozPolje4").val(data.Grad);
             $("#pozPolje5").val(data.Adresa);
             $("#pozPolje6").val(data.Telefon);
             $("#pozPolje7").val(data.Email);           
            
        }).fail(function (data, status) {
            alert("Greška prilikom izmene.");
        });
    }
    function editPredstava() {
        // izvlacimo {id}
        openPred();
        var editId = this.name;

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
       
        $.ajax({
            "type": "GET",
            "url": http + host + predstaveEndpoint + editId.toString(),
            "headers": headers
           

        }).done(function (data, status) {
            
            $("#prePolje1").val(data.Naziv);
            $("#prePolje2").val(data.ImeRezisera);
            $("#prePolje3").val(data.Opis);
            $("#prePolje4").val(data.DatumOdrazavanja);
            $("#prePolje5").val(data.VremeOdrzavanja);
            $("#prePolje6").val(data.CenaKarte);
            $("#prePolje7").val(data.Premijera);

            $('#selectedId option[selected="selected"]').attr("selected", null);
            $('#selectedId option[value="' + data.VrstaPredstaveId + '"]').attr("selected", "selected");

            $('#selectedId option[selected="selected"]').attr("selected", null);
            $('#selectedId option[value="' + data.PozoristeId + '"]').attr("selected", "selected");

            editingId = data.Id;
            formAction = "Update";

        }).fail(function (data, status) {
            alert("Greška prilikom izmene.");
        });
    }
    function editGlumac() {
       
        openGlu();
        var editId = this.name;
        
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        $.ajax({
            "type": "GET",
            "url": http + host + glumciEndpoint + editId.toString(),
            "headers": headers     

        }).done(function (data, status) {
           
            $("#imeGlu").val(data.Ime);
            $("#preGlu").val(data.Prezime);
            $("#godiGlu").val(data.GodinaRodjenja);
           
            editingId = data.Id;
            formAction = "Update";

        }).fail(function (data, status) {
            alert("Desila se greska!");
        });
    }
    function editVrste() {

        var editId = this.name; // izvlacimo {id}
        openVrste();
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        $.ajax({
            "type": "GET",
            "url": http + host + vrstePredstaveEndpoint + editId.toString(),
            "headers": headers
            
        }).done(function (data, status) {
            $("#nazivVrste").val(data.Naziv);
            $("#opisVrste").val(data.Opis);
            
            editingId = data.Id;
            formAction = "Update";

        }).fail(function (data, status) {
            alert("Desila se greska!");
        });
    }
});