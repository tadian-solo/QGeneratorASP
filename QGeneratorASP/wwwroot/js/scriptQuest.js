/*jshint esversion: 6 */
const uri = "/api/quest/";
let items = null;

document.addEventListener("DOMContentLoaded", function (event) {
    getQuests();
    getCurrentUser();
});
document.addEventListener("DOMContentLoaded", function (event) {
    getLevels("createLevelId");
});
function getCurrentUser() {
    let request = new XMLHttpRequest();
    request.open("POST", "/api/Account/isAuthenticated", true);
    request.onload = function () {
        let myObj = "";
        myObj = request.responseText !== "" ?
            JSON.parse(request.responseText) : {};
        document.getElementById("msg").innerHTML = myObj.message;
    };
    request.send();
}
function getCount(data) {
    const el = document.querySelector("#counter");
    let name = "Количество квестов:";
    if (data > 0) {
        el.innerText = name + data;
    } else {
        el.innerText = "Квестов еще нет";
    }
}

function getQuests() {
  /*  let request = new XMLHttpRequest();
    request.open("GET", '/api/QR/');*/
    let id = 0;
    let request1 = new XMLHttpRequest();
    request1.open("POST", "/api/Account/getUser", true);
    request1.onload = function () {
        id= JSON.parse(request1.responseText);
    };
    request1.send();

    let request = new XMLHttpRequest();
    request.open("GET", uri+'GetAll');
    request.onload = function () {
        let qs = "";
        let qsHTML = "";
        qs = JSON.parse(request.responseText);

        if (typeof qs !== "undefined") {
           // getCount(qs.length);
            if (qs.length > 0) {
                if (qs) {
                    var i;
                    for (i in qs) {
                        qsHTML += '<div class="quest"><span>' + 'Квест №' + qs[i].id_quest + ' : ' + 'Статус: ' + qs[i].status + ' Тематика: ' + qs[i].thematics + ' Дата: ' + qs[i].date + ' Уровень сложности: ' + qs[i].level_of_complexity.name_level + ' Автор: ' +qs[i].user.userName+' </span>';
                        qsHTML += '<button onclick="editQuest(' + qs[i].id_quest  + ')">Изменить</button>';
                        qsHTML += '<button onclick="deleteQuest(' + qs[i].id_quest + ')">Удалить</button></div>';
                        //qsHTML += '<button onclick="addQuestLoved(' + qs[i].id_quest + ')">В Избранное</button></div>';
                        let isLoved = false;
                        if (id != undefined)
                        {
                            for (let j in qs[i].userQuest) {
                                if (qs[i].userQuest[j].user.id == id)
                                    isLoved = true;
                                    //qsHTML += '<button onclick="deleteQuestLoved(' + qs[i].id_quest + ',' +id+')">Из Избранное</button></div>';
                            }
                        }

                        qsHTML += isLoved == false ? '<button onclick="addQuestLoved(' + qs[i].id_quest + ')">В Избранное</button></div>' : '<button onclick="deleteQuestLoved(' + qs[i].id_quest + ',' + id + ')">Из Избранное</button></div>';
                        if (typeof qs[i].questRiddle !== "undefined" && qs[i].questRiddle.length > 0) 
                          {
                            let j;
                            for (j in qs[i].questRiddle) {
                                qsHTML += '<p class="riddle">' + 'Текст загадки: ' + qs[i].questRiddle[j].riddle.text + ' Ответ: ' + qs[i].questRiddle[j].riddle.answer.object +
                                    '<button class = "del-button" onclick="deleteRiddleInQuest(' + qs[i].id_quest + ',' + qs[i].questRiddle[j].id_Riddle_Fk + ')">&#10006;</button> </p>';
                            }
                         }
                    }
                }
            }
            items = qs;
            document.querySelector("#questsDiv").innerHTML = qsHTML;
        }
    };
    request.send();
}
function getLevels(id) {
    let request = new XMLHttpRequest();
    request.open("GET", uri+"GetLevels");
    request.onload = function () {
        let ls = "";
     //   let lsHTML = "";
        let objSel = document.getElementById(id);
        ls = JSON.parse(request.responseText);
        if (typeof ls !== "undefined") {

            if (ls.length > 0 && objSel.options.length < ls.length) {
                if (ls) {
                    var i;
                    for (i in ls) {
                        objSel.options[objSel.options.length] = new Option(ls[i].name_level, ls[i].id_level);
                        //lsHTML += 
                        
                    }
                }
            }
            
            //document.querySelector("#createLevel").innerHTML = lsHTML;
        }
    };
    request.send();
}


function createQuest()
{
    
    var _thematics = document.querySelector("#createThematics").value;
    var _status = false;
    var _date = new Date();
    var objSel = document.getElementById("createLevelId");
    var _level = parseInt(objSel.options[objSel.selectedIndex].value,10);
    var _user = parseInt(document.querySelector("#createUser").value, 10);
    //var riddle = parseInt(document.querySelector("#createLevel").value, 10);
    
    var request = new XMLHttpRequest();
    request.open("POST", uri+'Create');
    request.onload = function () {
        getQuests();
        
    };
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify({
        status: _status,
        number_of_question: 0,
        thematics: _thematics,
        id_level_Fk: _level,
        id_autor_Fk: _user,
        date: _date
    }));
    }
function createRiddleInQuest()
{
    var _id_quest = document.querySelector("#id_quest").value;
    var _id_riddle = document.querySelector("#id_riddle").value;
    var request = new XMLHttpRequest();
    request.open("POST", "/api/QR/");
    request.onload = function () {
        getQuests();

    };
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify({ id_quest: _id_quest, id_riddle: _id_riddle}));

}

function editQuest(id)
{
    let elm = document.querySelector("#editDiv");
    elm.style.display = "block";
    if (items) {
        let i;
        for (i in items)
        {
            if (id === items[i].id_quest)
            {
                document.querySelector("#edit-id").value = items[i].id_quest;
                document.querySelector("#edit-status").value = items[i].status;
               // if (items[i].questRiddle.length) document.querySelector("#edit-number").value = 0;
                document.querySelector("#edit-thematics").value = items[i].thematics;
                document.querySelector("#edit-date").value = items[i].date;
                getLevels("editLevelId");
                let objSel = document.getElementById("editLevelId");
                let j;
                for (j in objSel.options) {
                    if (objSel.options[j].value == items[i].id_level_Fk) objSel.options[j].selected = true;
                }
               
                document.querySelector("#edit-user").value = items[i].id_autor_Fk;
                   // document.querySelector("#edit-questRiddle").value = items[i].
            }
        }
    }
}

function updateQuest()
{
    let objSel = document.getElementById("editLevelId");
    const quest = {
        questid: document.querySelector("#edit-id").value,
        status: document.querySelector("#edit-status").value,
        number_of_question: 0,
        thematics: document.querySelector("#edit-thematics").value,
        date: document.querySelector("#edit-date").value,
        id_level_Fk: objSel.options[objSel.selectedIndex].value,
        id_autor_Fk: document.querySelector("#edit-user").value,
        //questRiddle: document.querySelector("#edit-questRiddle").value


    };
    var request = new XMLHttpRequest();
    request.open("PUT", uri +'Update/'+ quest.questid);
    request.onload = function () {
        getQuests();
        closeInput();
    };
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify(quest));
}

function deleteQuest(id) {
    let request = new XMLHttpRequest();
    request.open("DELETE", uri +'Delete/'+ id, false);
    request.onload = function () {
        // Обработка кода ответа
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав";
        } else if (request.status === 204) {
            msg = "Запись удалена";
            getQuests();
        } else {
            msg = "Неизвестная ошибка";
        }
        document.querySelector("#actionMsg").innerHTML = msg;
       // getQuests();
    };
    request.send();
    getQuests();

}
function addQuestLoved(id){
   
  
    var request = new XMLHttpRequest();
    request.open("POST", "/api/UQ/");
    request.onload = function () {
        getQuests();

    };
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify({ id_quest: id, id_user: -1 }));
}
function deleteQuestLoved(id_q, id_u) {
    let request = new XMLHttpRequest();
    request.open("DELETE", "/api/UQ/");
    request.onload = function () {
        getQuests();

    };
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify({ id_quest: id_q, id_user: id_u }));

}
function deleteRiddleInQuest(id_q, id_r)
{
    let request = new XMLHttpRequest();
    request.open("DELETE", "/api/QR/");
    request.onload = function () {
        getQuests();

    };
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify({ id_quest: id_q, id_riddle: id_r }));
    
}

function closeInput() {
    let elm = document.querySelector("#editDiv");
    elm.style.display = "none";
}
function logIn() {
    var login, password = "";
    // Считывание данных с формы
    login= document.getElementById("Login").value;
    password = document.getElementById("Password").value;
    var request = new XMLHttpRequest();
    request.open("POST", "/api/Account/Login");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.onreadystatechange = function () {
        // Очистка контейнера вывода сообщений
        document.getElementById("msg").innerHTML = "";
        var mydiv = document.getElementById('formError');
        while (mydiv.firstChild) {
            mydiv.removeChild(mydiv.firstChild);
        }
        // Обработка ответа от сервера
        if (request.responseText !== "") {
            var msg = null;
            msg = JSON.parse(request.responseText);
            document.getElementById("msg").innerHTML = msg.message;
            // Вывод сообщений об ошибках
            if (typeof msg.error !== "undefined" && msg.error.length > 0) {
                for (var i = 0; i < msg.error.length; i++) {
                   // var ul = document.getElementById('formError');
                    var li = document.createElement("li");
                    li.appendChild(document.createTextNode(msg.error[i]));
                    // ul[0].appendChild(li);
                    mydiv.appendChild(li);
                }
            }
            document.getElementById("Password").value = "";
        }
    };
    // Запрос на сервер
    request.send(JSON.stringify({
        login: login,
        password: password
    }));
}

function logOff() {
    var request = new XMLHttpRequest();
    request.open("POST", "api/account/logoff");
    request.onload = function () {
        var msg = JSON.parse(this.responseText);
        document.getElementById("msg").innerHTML = "";
        var mydiv = document.getElementById('formError');
        while (mydiv.firstChild) {
            mydiv.removeChild(mydiv.firstChild);
        }
        document.getElementById("msg").innerHTML = msg.message;
    };
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send();
}

// Обработка кликов по кнопкам
document.getElementById("loginBtn").addEventListener("click", logIn);
document.getElementById("logoffBtn").addEventListener("click", logOff);

/*function getCurrentUser() {
    let request = new XMLHttpRequest();
    request.open("POST", "/api/Account/getUser", true);
    request.onload = function () {
        return JSON.parse(request.responseText);
    };
    request.send();
}*/