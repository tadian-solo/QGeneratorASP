/*jshint esversion: 6 */
const uri = "/api/quest/";
let items = null;
//модуль "текущий пользователь"
var user =
{
    getCurrentUser: function() {
        let request = new XMLHttpRequest();
        // let id = 0;
        var user;//_id;

        // var access;
        request.open("POST", "/api/Account/isAuthenticated", true);
        request.onload = function () {
            /*
            let myObj = "";
            myObj = request.responseText !== "" ?
                JSON.parse(request.responseText) : {};
            document.getElementById("msg").innerHTML = myObj.message;*/
            var autor = document.getElementById('questsDiv');
            try {

                user = JSON.parse(request.responseText);
                if (user != -1) {

                    autor.dataset.user = user.id;
                    autor.dataset.access = user.accessLevel;
                    let elm = document.querySelector("#qDiv");
                    elm.style.display = "block";
                    elm = document.querySelector("#rInQDiv");
                    elm.style.display = "block";
                    //document.getElementById("qDiv").style.display = "block";
                    //document.getElementById("rInQDiv").style.display = "block";
                }
                // user.innerText = "Выйти";


            }

            catch (err) {
                autor.dataset.user = -1;//
                autor.dataset.access = false;
                //user.innerText = "Войти";
            }
            /* var autor = document.getElementById('questsDiv');
             autor.dataset.user = user_id;
             autor.dataset.access = user_access;*/
        };
        request.send();
    }
}
document.addEventListener("DOMContentLoaded", function (event) {
    
    user.getCurrentUser();
    getLevels("createLevelId"); //
    GetComboboxLevel();
   
    getQuests();
});
/*document.addEventListener("DOMContentLoaded", function (event) {
    getLevels("createLevelId");
});*/

//var user_id;

function getCount(data) {
    const el = document.querySelector("#counter");
    let name = "Количество квестов:";
    if (data > 0) {
        el.innerText = name + data;
    } else {
        el.innerText = "Квестов еще нет";
    }
}
function GetComboboxLevel() {
    var comboLevel = document.getElementById('createLevelIdR');
    comboLevel.addEventListener("change", GetComboboxType);
    //get
    let request = new XMLHttpRequest();
    request.open("GET", "/api/riddle/" + "GetLevels");
    request.onload = function () {
        let ls = "";
        ls = JSON.parse(request.responseText);
        if (typeof ls !== "undefined") {

            if (ls.length > 0 && comboLevel.options.length < ls.length) {
                if (ls) {
                    var i;
                    for (i in ls) {
                        comboLevel.options[comboLevel.options.length] = new Option(ls[i].name_level, ls[i].id_level);
                        //lsHTML += 

                    }
                }
            }
            GetComboboxType();
            //document.querySelector("#createLevel").innerHTML = lsHTML;
        }
    };
    request.send();
    
    //comboLevel.addEventListener("change", GetComboboxType);
    
}
function GetComboboxType() {
     
    var comboLevel = document.getElementById('createTypeIdR');
    comboLevel.addEventListener("change", GetComboboxAnswer);
    var cL = document.getElementById('createLevelIdR');
    //get
    let request = new XMLHttpRequest();
    request.open("GET", "/api/riddle/" + "GetTypesForLevel/" + cL.options[cL.selectedIndex].value);
    request.onload = function () {
        let ls = "";
        ls = JSON.parse(request.responseText);
        if (typeof ls !== "undefined") {
            comboLevel.options.length = 0;

            if (ls.length > 0 && comboLevel.options.length < ls.length) {
                if (ls) {
                    var i;
                    for (i in ls) {
                        comboLevel.options[comboLevel.options.length] = new Option(ls[i].name, ls[i].id_type);
                        //lsHTML += 

                    }
                }
            }
            
            //document.querySelector("#createLevel").innerHTML = lsHTML;
        } GetComboboxAnswer();
    };
    request.send();
  
    //comboLevel.addEventListener("change", GetComboboxAnswer);
    

}
function GetComboboxAnswer() {
    //get
    var comboLevel = document.getElementById('createAnswerIdR');
    var cL = document.getElementById('createLevelIdR');
    var cT = document.getElementById('createTypeIdR');
    //get
    comboLevel.options.length = 0;
    let request = new XMLHttpRequest();
    request.open("GET", "/api/riddle/" + "GetAnswersForLevelAndType?id=" + cT.options[cT.selectedIndex].value + "&level=" + cL.options[cL.selectedIndex].value);
    request.onload = function () {
        let ls = "";
        ls = JSON.parse(request.responseText);
       
        if (typeof ls !== "undefined") {
           // comboLevel.options.length = 0;

            if (ls.length > 0 && comboLevel.options.length < ls.length) {
                if (ls) {
                    var i;
                    for (i in ls) {
                        comboLevel.options[comboLevel.options.length] = new Option(ls[i].object, ls[i].id_answer)
                        //lsHTML += 

                    }
                }
            }

            //document.querySelector("#createLevel").innerHTML = lsHTML;
        }
    };
    request.send();


}

function getQuests() {
  
    let request = new XMLHttpRequest();
    request.open("GET", uri+'GetAll');
    request.onload = function () {
        let qs = "";
        let qsHTML = "";
        qs = JSON.parse(request.responseText);
        var autor = document.getElementById('questsDiv');
        var user_id = parseInt(autor.dataset.user, 10);
        var formatter = new Intl.DateTimeFormat("ru");
        if (typeof qs !== "undefined") {
           // getCount(qs.length);
            if (qs.length > 0) {
                if (qs) {
                    let elm = document.querySelector("#id_quest");
                    elm.innerText = qs[qs.length - 1].id_quest;
                    elm.value = qs[qs.length - 1].id_quest;
                    var i;
                    for (i in qs) {
                        qsHTML += '<div class="quest"><span>' + 'Квест №' + qs[i].id_quest + ' : ' + 'Статус: ' + qs[i].status + ' Тематика: ' + qs[i].thematics + ' Дата: ' + qs[i].date.split('T')[0] + ' Уровень сложности: ' + qs[i].level_of_complexity.name_level + ' Автор: ' + qs[i].user.userName + ' </span></br>';
                        if (user_id != -1) {

                            qsHTML += '<button type="button" class= "btn btn-primary btn-icon" onclick="editQuest(' + qs[i].id_quest + ')"><span class="glyphicon glyphicon-pencil" aria-hidden="true" style="padding: 7px 6px;"></span>Изменить</button>';
                            if (autor.dataset.access=="true") qsHTML += '<button type="button" class= "btn btn-primary btn-icon" onclick="deleteQuest(' + qs[i].id_quest + ')"><span class="glyphicon glyphicon-remove" aria-hidden="true" style="padding: 7px 6px;"></span>Удалить</button>';
                        }
                        //qsHTML += '<button onclick="addQuestLoved(' + qs[i].id_quest + ')">В Избранное</button></div>';
                        let isLoved = false;
                        if (user_id != -1)//undef id
                        {
                            for (let j in qs[i].userQuest) {
                                if (qs[i].userQuest[j].id_User_Fk == user_id /*id*/)
                                    isLoved = true;
                            
                            }
                            qsHTML += isLoved == false ? '<button type="button" onclick="addQuestLoved(' + qs[i].id_quest + ')"><span class="glyphicon glyphicon-star" aria-hidden="true" ></span></button></div>' : '<button type="button"  onclick="deleteQuestLoved(' + qs[i].id_quest + ',' + user_id + ')"><span class="glyphicon glyphicon-star" aria-hidden="true" style="color: yellow"></span></button></div>';

                        }
                        else qsHTML += "</div>";
                        if (typeof qs[i].questRiddle !== "undefined" && qs[i].questRiddle.length > 0) 
                          {
                            let j;
                            for (j in qs[i].questRiddle) {
                                qsHTML += '<p class="riddle">' + 'Текст загадки: ' + qs[i].questRiddle[j].riddle.text + ' Ответ: ' + qs[i].questRiddle[j].riddle.answer.object;
                                if (user_id != -1) qsHTML +=  '<button class = "del-button" onclick="deleteRiddleInQuest(' + qs[i].id_quest + ',' + qs[i].questRiddle[j].id_Riddle_Fk + ')">&#10006;</button> </p>';
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
    var _level = parseInt(objSel.options[objSel.selectedIndex].value, 10);
    var autor = document.getElementById('questsDiv');
    var _user = autor.dataset.user;
  //  var _user = parseInt(document.querySelector("#createUser").value, 10);
    //var riddle = parseInt(document.querySelector("#createLevel").value, 10);
    
    var request = new XMLHttpRequest();
    request.open("POST", uri+'Create');
    request.onload = function () {
        getQuests();
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав на создание";
        } else if (request.status === 201) {
            msg = "Запись добавлена";
            getQuests();
        } else {
            msg = "Неизвестная ошибка";
        }
        document.querySelector("#actionMsg").innerHTML = msg;
        
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
function createRiddle(_id_quest,  _id_riddle) {
    var request2 = new XMLHttpRequest();
    request2.open("POST", "/api/QR/");
    request2.onload = function () {
    getQuests();
    var msg = "";
    if (request2.status === 401) {
        msg = "У вас не хватает прав";
    } else if (request2.status === 200) {
        msg = "Успешно";
        getQuests();
    } else {
        msg = "Неизвестная ошибка";
    }
    document.querySelector("#actionMsg").innerHTML = msg;

};
request2.setRequestHeader("Accepts", "application/json;charset=UTF-8");
request2.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
request2.send(JSON.stringify({ id_quest: _id_quest, id_riddle: _id_riddle }));

}
function createRiddleInQuest() {
    var _id_quest = document.querySelector("#id_quest").value;
    var _id_riddle = document.querySelector("#id_riddle").value;
   
        if (_id_riddle == "") {
            var objSel = document.getElementById("createLevelIdR");
            var _level = parseInt(objSel.options[objSel.selectedIndex].value, 10);
            objSel = document.getElementById("createTypeIdR");
            var _type = parseInt(objSel.options[objSel.selectedIndex].value, 10);
            objSel = document.getElementById("createAnswerIdR");
            var _answer = parseInt(objSel.options[objSel.selectedIndex].value, 10);

            var request = new XMLHttpRequest();
            request.open("GET", "/api/riddle/" + "GetRiddle?id_level=" + _level + "&id_type=" + _type + "&id_answer=" + _answer);
            request.onload = function () {
                let ls = "";
                ls = JSON.parse(request.responseText);
                if (typeof ls.id_riddle !== "undefined") _id_riddle = ls.id_riddle;
                createRiddle(_id_quest, _id_riddle);
            }
            request.send();
    }
    else createRiddle(_id_quest, _id_riddle);

}
    

function editQuest(id)
{
    
    let elm = document.querySelector("#editDiv");
    elm.style.display = "block";
    elm.dataset.edit = "true";
    elm = document.querySelector("#id_quest");
    elm.innerText = id;
    elm.value = id;
    if (items) {
        let i;
        for (i in items)
        {
            if (id === items[i].id_quest)
            {
                document.querySelector("#edit-id").value = items[i].id_quest;
               // document.querySelector("#edit-status").value = items[i].status;
               // if (items[i].questRiddle.length) document.querySelector("#edit-number").value = 0;
                document.querySelector("#edit-thematics").value = items[i].thematics;
               // document.querySelector("#edit-date").value = items[i].date;
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
    var autor = document.getElementById('questsDiv');
    
    const quest = {
        questid: document.querySelector("#edit-id").value,
        status: false,//document.querySelector("#edit-status").value,
        number_of_question: 0,
        thematics: document.querySelector("#edit-thematics").value,
        date: new Date(),
        id_level_Fk: objSel.options[objSel.selectedIndex].value,
        id_autor_Fk: autor.dataset.user
        //questRiddle: document.querySelector("#edit-questRiddle").value


    };
    var request = new XMLHttpRequest();
    request.open("PUT", uri +'Update/'+ quest.questid);
    request.onload = function () {
        getQuests();
        closeInput();
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав на редактирование";
        } else if (request.status === 204) {
            msg = "Запись отредактирована";
            getQuests();
        } else {
            msg = "Неизвестная ошибка";
        }
        document.querySelector("#actionMsg").innerHTML = msg;
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
            msg = "У вас не хватает прав на удаление";
        } else if (request.status === 204) {
            msg = "Запись удалена";
            getQuests();
        } else {
            msg = "Неизвестная ошибка";
        }
        document.querySelector("#actionMsg").innerHTML = msg;
        getQuests();
    };
    request.send();
   // getQuests();

}
function addQuestLoved(id){
   
  
    var request = new XMLHttpRequest();
    request.open("POST", "/api/UQ/");
    request.onload = function () {
        getQuests();

    };
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify({ id_quest: id, id_user: -1 }));///-1 upgrade
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
        /*var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав";
        } else if (request.status === 204) {
            msg = "Запись удалена";
            getQuests();
        } else {
            msg = "Неизвестная ошибка";
        }*/
        document.querySelector("#actionMsg").innerHTML = msg;

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
            getQuests();
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
        getQuests();
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