/*jshint esversion: 6 */
const uri = "/api/riddle/";
let items = null;
//var levels = null;
//var types = null;
//var answers = null;
document.addEventListener("DOMContentLoaded", function (event) {
    getCurrentUser();
    getRiddles();

});
document.addEventListener("DOMContentLoaded", function (event) {
    getLevels("createLevelId");
    getTypes("createTypeId");
    getAnswers("createAnswerId");
});
function getCurrentUser() {
    let request = new XMLHttpRequest();
    // let id = 0;
    var user_id;
    request.open("POST", "/api/Account/isAuthenticated", true);
    request.onload = function () {
        /*
        let myObj = "";
        myObj = request.responseText !== "" ?
            JSON.parse(request.responseText) : {};
        document.getElementById("msg").innerHTML = myObj.message;*/
        try {
            user_id = JSON.parse(request.responseText);

        }

        catch (err) {
            user_id = -1;//
        }
        var autor = document.getElementById('riddlesDiv');
        autor.dataset.user = user_id;
    };
    request.send();
}
function getCount(data) {
    const el = document.querySelector("#counter");
    let name = "Количество riddle:";
    if (data > 0) {
        el.innerText = name + data;
    } else {
        el.innerText = "riddle еще нет";
    }
}

function getRiddles() {
    let request = new XMLHttpRequest();
    request.open("GET", uri + 'GetAll');
    request.onload = function () {
        let rs = "";
        let rsHTML = "";
        rs = JSON.parse(request.responseText);

        if (typeof rs !== "undefined") {
            //getCount(rs.length);
            if (rs.length > 0) {
                if (rs) {
                    var i;
                    for (i in rs) {
                        rsHTML += '<div class="blogText"><span>' + 'Загадка №' + rs[i].id_riddle + ' : ' + 'Статус: ' + rs[i].status + ' Теxt: ' + rs[i].text + ' Desc: ' + rs[i].description + ' Уровень сложности: ' + rs[i].level_of_complexity.name_level + ' Type: ' + rs[i].type_of_question.name + ' Answer: ' + rs[i].answer.object + ' Автор: ' + rs[i].user.userName + ' </span></br>';
                       // rsHTML +='<span>'+
                        rsHTML += '<button type="button" class= "btn btn-primary btn-icon" onclick="editRiddle(' + rs[i].id_riddle + ')"><span class="glyphicon glyphicon-pencil" aria-hidden="true" style="padding: 7px 6px;"></span>Изменить</button>';
                        rsHTML += '<button type="button" class= "btn btn-primary btn-icon" onclick="deleteRiddle(' + rs[i].id_riddle + ')"><span class="glyphicon glyphicon-remove" aria-hidden="true" style="padding: 7px 6px;"></span>Удалить</button></div>';
                        
                    }
                }
            }
            items = rs;
            document.querySelector("#riddlesDiv").innerHTML = rsHTML;
        }
    };
    request.send();
}
function getLevels(id) {
    let request = new XMLHttpRequest();
    request.open("GET", uri + "GetLevels");
    request.onload = function () {
       
        //   let lsHTML = "";
        let objSel = document.getElementById(id);
        let levels = "";
        levels = JSON.parse(request.responseText);
        if (typeof levels !== "undefined") {

            if (levels.length > 0 && objSel.options.length < levels.length) {
                if (levels) {
                    var i;
                    for (i in levels) {
                        objSel.options[objSel.options.length] = new Option(levels[i].name_level, levels[i].id_level);
                        //lsHTML += 

                    }
                }
            }

            //document.querySelector("#createLevel").innerHTML = lsHTML;
        }
    };
    request.send();
}
function getTypes(id) {
    let request = new XMLHttpRequest();
    request.open("GET", uri + "GetTypes");
    request.onload = function () {

        //   let lsHTML = "";
        let objSel = document.getElementById(id);
        let types = "";
        types = JSON.parse(request.responseText);
        if (typeof types !== "undefined") {

            if (types.length > 0 && objSel.options.length < types.length) {
                if (types) {
                    var i;
                    for (i in types) {
                        objSel.options[objSel.options.length] = new Option(types[i].name, types[i].id_type);
                        //lsHTML += 

                    }
                }
            }

            //document.querySelector("#createLevel").innerHTML = lsHTML;
        }
    };
    request.send();
}
function getAnswers(id) {
    let request = new XMLHttpRequest();
    request.open("GET", uri + "GetAnswers");
    request.onload = function () {

        //   let lsHTML = "";
        let objSel = document.getElementById(id);
        let answers = "";
        answers = JSON.parse(request.responseText);
        if (typeof answers !== "undefined") {

            if (answers.length > 0 && objSel.options.length < answers.length) {
                if (answers) {
                    var i;
                    for (i in answers) {
                        objSel.options[objSel.options.length] = new Option(answers[i].object, answers[i].id_answer);
                        //lsHTML += 

                    }
                }
            }
            
        }
    };
    request.send();
}

function createRiddle() {

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
    //var _user = parseInt(document.querySelector("#createUser").value, 10);
    //var riddle = parseInt(document.querySelector("#createLevel").value, 10);

    var request = new XMLHttpRequest();
    request.open("POST", uri + 'Create');
    request.onload = function () {
        getRiddles();
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
}

function editRiddle(id) {
    let elm = document.querySelector("#editDiv");
    elm.style.display = "block";
    getLevels("editLevelId");
    getTypes("editTypeId");
    getAnswers("editAnswerId");
    if (items) {
        let i;
        for (i in items) {
            if (id === items[i].id_riddle) {
                document.querySelector("#edit-id").value = items[i].id_riddle;
                //document.querySelector("#edit-status").value = items[i].status;
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
                //document.querySelector("#edit-user").value = items[i].id_Autor_FK;
                // document.querySelector("#edit-questRiddle").value = items[i].
            }
        }
    }
}

function updateRiddle() {
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
        id_autor_Fk: autor.dataset.user//document.querySelector("#edit-user").value,
        //questRiddle: document.querySelector("#edit-questRiddle").value


    };
    var request = new XMLHttpRequest();
    request.open("PUT", uri + 'Update/' + riddle.id_riddle);
    request.onload = function () {
        getRiddles();
        closeInput();
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
}

function deleteRiddle(id) {
    let request = new XMLHttpRequest();
    request.open("DELETE", uri + 'Delete/' + id, false);
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
        getRiddles();
    };
    request.send();
}


function closeInput() {
    let elm = document.querySelector("#editDiv");
    elm.style.display = "none";
}