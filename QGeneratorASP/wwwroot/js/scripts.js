/*jshint esversion: 6 */
const uri = "/api/riddle/";
let items = null;
document.addEventListener("DOMContentLoaded", function (event) {
    user.getCurrentUser();
    collections.init();
    riddles.getRiddles();

});
var user = {
    getCurrentUser: function () {
        let request = new XMLHttpRequest();
        var user;
        request.open("POST", "/api/Account/isAuthenticated", true);
        request.onload = function () {
            var autor = document.getElementById('riddlesDiv');
            try {
                user = JSON.parse(request.responseText);
                if (user != -1) {
                    autor.dataset.user = user.id;
                    autor.dataset.access = user.accessLevel;
                    let elm = document.querySelector("#rDiv");
                    elm.style.display = "block";
                }
            }
            catch (err) {
                autor.dataset.user = -1;
                autor.dataset.access = false;
            }
        };
        request.send();
    }
}
var collections = {
    types: "",
    levels: "",
    answers: "",
    init: function () {
        
        let request = new XMLHttpRequest();
        request.open("GET", uri + "GetTypes");
        request.onload = function () {
            try {
                collections.types = JSON.parse(request.responseText);
                collections.getTypes("createTypeId");
            }
            catch (err) {

            }
        }
        request.send();
        let request2 = new XMLHttpRequest();
        request2.open("GET", uri + "GetLevels");
        request2.onload = function () {
            collections.levels = JSON.parse(request2.responseText);
            collections.getLevels("createLevelId"); 
        }
        request2.send();
        let request3 = new XMLHttpRequest();
        request3.open("GET", uri + "GetAnswers");
        request3.onload = function () {
            collections.answers = JSON.parse(request3.responseText);
            collections.getAnswers("createAnswerId");
        }
        request3.send();

    },
    getTypes: function (id) {
        if (typeof collections.types !== "undefined") {
            let objSel = document.getElementById(id);
            if (collections.types.length > 0 && objSel.options.length < collections.types.length) {
                if (collections.types) {
                        var i;
                    for (i in collections.types) {
                        objSel.options[objSel.options.length] = new Option(collections.types[i].name, collections.types[i].id_type);

                        }
                    }
                }
            }
    },
    getLevels: function (id) { 
            
        if (typeof collections.levels !== "undefined") {
            let objSel = document.getElementById(id);
            if (collections.levels.length > 0 && objSel.options.length < collections.levels.length) {
                if (collections.levels) {
                            var i;
                    for (i in collections.levels) {
                        objSel.options[objSel.options.length] = new Option(collections.levels[i].name_level, collections.levels[i].id_level);
                                
                            }
                        }
                    }
                }
           
    },
    getAnswers: function (id) {
        
        if (typeof collections.answers !== "undefined") {
            let objSel = document.getElementById(id);
            if (collections.answers.length > 0 && objSel.options.length < collections.answers.length) {
                if (collections.answers) {
                        var i;
                    for (i in collections.answers) {
                        objSel.options[objSel.options.length] = new Option(collections.answers[i].object, collections.answers[i].id_answer);
                        }
                    }
                }
            }
    }


}
var riddles = {
    getRiddles: function () {
        let request = new XMLHttpRequest();
        request.open("GET", "/api/riddle/" + 'GetAll');
        request.onload = function () {
            let rs = "";
            let rsHTML = "";
            rs = JSON.parse(request.responseText);
            var autor = document.getElementById('riddlesDiv');
            var user_id = parseInt(autor.dataset.user, 10);
            if (typeof rs !== "undefined") {
                if (rs.length > 0) {
                    if (rs) {
                        var i;
                        for (i in rs) {
                            rsHTML += '<div class="blogText"><span>' + 'Загадка №' + rs[i].id_riddle + ' : ' + 'Статус: ' + rs[i].status + ' Теxt: ' + rs[i].text + ' Desc: ' + rs[i].description + ' Уровень сложности: ' + rs[i].level_of_complexity.name_level + ' Type: ' + rs[i].type_of_question.name + ' Answer: ' + rs[i].answer.object + ' Автор: ' + rs[i].user.userName + ' </span></br>';
                            if (user_id != -1) rsHTML += '<button type="button" class= "btn btn-primary btn-icon" onclick="riddles.editRiddle(' + rs[i].id_riddle + ')"><span class="glyphicon glyphicon-pencil" aria-hidden="true" style="padding: 7px 6px;"></span>Изменить</button>';
                            if (user_id != -1) rsHTML += '<button type="button" class= "btn btn-primary btn-icon" onclick="riddles.deleteRiddle(' + rs[i].id_riddle + ')"><span class="glyphicon glyphicon-remove" aria-hidden="true" style="padding: 7px 6px;"></span>Удалить</button></div>';
                        }
                    }
                }
                items = rs;
                document.querySelector("#riddlesDiv").innerHTML = rsHTML;
            }
        };
        request.send();
    },
    createRiddle: function () {
        var _text = document.querySelector("#createText").value;
        var _desc = document.querySelector("#createDescription").value;
        var _status = false;
        var objSel = document.getElementById("createLevelId");
        var _level = parseInt(objSel.options[objSel.selectedIndex].value, 10);
        objSel = document.getElementById("createTypeId");
        var _type = parseInt(objSel.options[objSel.selectedIndex].value, 10);
        objSel = document.getElementById("createAnswerId");
        var _answer = parseInt(objSel.options[objSel.selectedIndex].value, 10);
        var autor = document.getElementById('riddlesDiv');
        var _user = autor.dataset.user;
        var request = new XMLHttpRequest();
        request.open("POST", "/api/riddle/" + 'Create');
        request.onload = function () {
            riddles.getRiddles();
            var msg = "";
            if (request.status === 401) {
                msg = "У вас не хватает прав";
            } else if (request.status === 201) {
                msg = "Запись добавлена";

            } else {
                msg = "Неизвестная ошибка";
            }
            document.querySelector("#actionMsg").innerHTML = msg;

        };
        request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
        request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        request.send(JSON.stringify({
            status: _status,
            text: _text,
            description: _desc,
            id_type_fk: _type,
            id_answer_fk: _answer,
            id_level_Fk: _level,
            id_autor_Fk: _user
        }));
    },
    editRiddle: function (id) {
        let elm = document.querySelector("#editDiv");
        elm.style.display = "block";
        collections.getLevels("editLevelId");
        collections.getTypes("editTypeId");
        collections.getAnswers("editAnswerId");
        if (items) {
            let i;
            for (i in items) {
                if (id === items[i].id_riddle) {
                    document.querySelector("#edit-id").value = items[i].id_riddle;
                    document.querySelector("#edit-text").value = items[i].text;
                    document.querySelector("#edit-description").value = items[i].description;
                    let objSel = document.getElementById("editLevelId");
                    let j;
                    for (j in objSel.options) {
                        if (objSel.options[j].value == items[i].id_Level_FK) objSel.options[j].selected = true;
                    }
                    objSel = document.getElementById("editTypeId");

                    for (j in objSel.options) {
                        if (objSel.options[j].value == items[i].id_Type_FK) objSel.options[j].selected = true;
                    }
                    objSel = document.getElementById("editAnswerId");

                    for (j in objSel.options) {
                        if (objSel.options[j].value == items[i].id_Answer_FK) objSel.options[j].selected = true;
                    }
                }
            }
        }
    },
    updateRiddle: function () {
        let objSel = document.getElementById("editLevelId");
        let objSel2 = document.getElementById("editTypeId");
        let objSel3 = document.getElementById("editAnswerId");
        var autor = document.getElementById('riddlesDiv');
        const riddle = {
            id_riddle: document.querySelector("#edit-id").value,
            status: false,//document.querySelector("#edit-status").value,
            text: document.querySelector("#edit-text").value,
            description: document.querySelector("#edit-description").value,
            id_Level_FK: objSel.options[objSel.selectedIndex].value,
            id_Type_FK: objSel2.options[objSel2.selectedIndex].value,
            id_Answer_FK: objSel3.options[objSel3.selectedIndex].value,
            id_autor_Fk: autor.dataset.user,
        };
        var request = new XMLHttpRequest();
        request.open("PUT", "/api/riddle/"+ 'Update/' + riddle.id_riddle);
        request.onload = function () {
           riddles.getRiddles();
            riddles.closeInput();
            var msg = "";
            if (request.status === 401) {
                msg = "У вас не хватает прав";
            } else if (request.status === 204) {
                msg = "Запись отредактирована";

            } else {
                msg = "Неизвестная ошибка";
            }
            document.querySelector("#actionMsg").innerHTML = msg;
        };
        request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        request.send(JSON.stringify(riddle));
    },
    deleteRiddle: function (id) {
        let request = new XMLHttpRequest();
        request.open("DELETE", "/api/riddle/" + 'Delete/' + id, false);
        request.onload = function () {
            var msg = "";
            if (request.status === 401) {
                msg = "У вас не хватает прав";
            } else if (request.status === 204) {
                msg = "Запись удалена";

            } else {
                msg = "Неизвестная ошибка";
            }
            document.querySelector("#actionMsg").innerHTML = msg;
            riddles.getRiddles();
        };
        request.send();
    },
    closeInput: function () {
        let elm = document.querySelector("#editDiv");
        elm.style.display = "none";
    }
}

