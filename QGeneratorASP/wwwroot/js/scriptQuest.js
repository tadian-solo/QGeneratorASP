/*jshint esversion: 6 */
const uri = "/api/quest/";
let items = null;
//модуль "текущий пользователь"
var user =
{
    getCurrentUser: function() {//функция получения текущего пользователя
        let request = new XMLHttpRequest();
        var user;
        request.open("POST", "/api/Account/isAuthenticated", true);
        request.onload = function () {
            var autor = document.getElementById('questsDiv');
            try {
                user = JSON.parse(request.responseText);
                if (user != -1) { //если пользователь не авторизирован вернется -1, в противном случае сохраняем данные пользователя в атрибут data
                    autor.dataset.user = user.id;
                    autor.dataset.access = user.accessLevel;
                    let elm = document.querySelector("#qDiv");
                    elm.style.display = "block";
                    elm = document.querySelector("#rInQDiv");
                    elm.style.display = "block";
                }
            }
            catch (err) { //в случае ошибки десериализации заполняем значениями по умолчанию
                autor.dataset.user = -1;
                autor.dataset.access = false;
            }
        };
        request.send();
    }
}
document.addEventListener("DOMContentLoaded", function (event) {
    
    user.getCurrentUser();
   // collections.getLevels("createLevelId"); //
    collections.GetComboboxLevel("createLevelId");// заполняем комбобоксы уровня для создания квеста и для добавления загадки
    collections.GetComboboxLevel("createLevelIdR");
   
    quests.getQuests();// получаем квесты
});
// модуль со справочниками уровней, типов и ответов
var collections = {
    GetComboboxLevel: function (id) { // заполняет комбобокс с указанным id данными об уровнях
        var comboLevel = document.getElementById(id);
        comboLevel.addEventListener("change", collections.GetComboboxType); // при выборе уровня обновляем доступные типы загадок
        let request = new XMLHttpRequest();
        request.open("GET", "/api/riddle/" + "GetLevels");
        request.onload = function () {
            let ls = "";
            ls = JSON.parse(request.responseText);
            if (typeof ls !== "undefined") {

                if (ls.length > 0 && comboLevel.options.length < ls.length) { 
                    if (ls) {//заполняем комбобокс
                        var i;
                        for (i in ls) {
                            comboLevel.options[comboLevel.options.length] = new Option(ls[i].name_level, ls[i].id_level);
                        }
                    }
                }
                collections.GetComboboxType();// вызываем заполнение комбобокса типов
            }
        };
        request.send();
    },
    GetComboboxType: function () {// заполнение комбобокса типов
        var comboLevel = document.getElementById('createTypeIdR');
        comboLevel.addEventListener("change", collections.GetComboboxAnswer);//привязываем обновление комбобокса ответов
        var cL = document.getElementById('createLevelIdR');
        let request = new XMLHttpRequest();
        request.open("GET", "/api/riddle/" + "GetTypesForLevel/" + cL.options[cL.selectedIndex].value);// запрос на сервер, вернутся только те типы, для которых существуют загадки с выбранным уровнем
        request.onload = function () {
            let ls = "";
            ls = JSON.parse(request.responseText);
            if (typeof ls !== "undefined") {
                comboLevel.options.length = 0;

                if (ls.length > 0 && comboLevel.options.length < ls.length) {//заполняем данными
                    if (ls) {
                        var i;
                        for (i in ls) {
                            comboLevel.options[comboLevel.options.length] = new Option(ls[i].name, ls[i].id_type);
                        }
                    }
                };
            } collections.GetComboboxAnswer();//вызываем заполнение комбобокса ответов
        };
        request.send();
    },
    GetComboboxAnswer: function () {//функция заполнения комбобокса ответов 
        var comboLevel = document.getElementById('createAnswerIdR');
        var cL = document.getElementById('createLevelIdR');
        var cT = document.getElementById('createTypeIdR');
        comboLevel.options.length = 0;
        let request = new XMLHttpRequest();
        request.open("GET", "/api/riddle/" + "GetAnswersForLevelAndType?id=" + cT.options[cT.selectedIndex].value + "&level=" + cL.options[cL.selectedIndex].value);
        //венутся ответы, для которых есть загадки с заданными уровнем и типом
        request.onload = function () {
            let ls = "";
            ls = JSON.parse(request.responseText);
            if (typeof ls !== "undefined") {
                if (ls.length > 0 && comboLevel.options.length < ls.length) {
                    if (ls) {
                        var i;
                        for (i in ls) {
                            comboLevel.options[comboLevel.options.length] = new Option(ls[i].object, ls[i].id_answer) 
                        }
                    }
                }
                
            }
        };
        request.send();
    },
   /* getLevels: function (id) {
        let request = new XMLHttpRequest();
        request.open("GET", uri + "GetLevels");
        request.onload = function () {
        let ls = "";
        let objSel = document.getElementById(id);
        ls = JSON.parse(request.responseText);
        if (typeof ls !== "undefined") {
            if (ls.length > 0 && objSel.options.length < ls.length) {
                if (ls) {
                 var i;
                 for (i in ls) {
                       objSel.options[objSel.options.length] = new Option(ls[i].name_level, ls[i].id_level);
                    }
                }
            }
           }
       };
        request.send();
    }*/
}
//модуль квестов
var quests = {
  getQuests: function () {
    let request = new XMLHttpRequest();
    request.open("GET", uri + 'GetAll');
    request.onload = function () {
    let qs = "";
    let qsHTML = "";
    qs = JSON.parse(request.responseText);
    //var autor = document.getElementById('questsDiv');
    //var user_id = parseInt(autor.dataset.user, 10);
    if (typeof qs !== "undefined") { //если вернулись корректные данные, выводим квесты
        if (qs.length > 0) {
            if (qs) {
                qsHTML=quests.parseQuest(qs); //вывод квестов
            }
        }
        items = qs;
        document.querySelector("#questsDiv").innerHTML = qsHTML;
    }
  };
  request.send();
 },
    parseQuest: function (qs) {
        var autor = document.getElementById('questsDiv');
        var user_id = parseInt(autor.dataset.user, 10);
        let elm = document.querySelector("#id_quest");
        elm.innerText = qs[qs.length - 1].id_quest;
        elm.value = qs[qs.length - 1].id_quest;
        let qsHTML = "";
        var i;
        for (i in qs) {//для всех вернувшихся квестов
            qsHTML += '<div class="quest"><span>' + 'Квест №' + qs[i].id_quest + ' : ' + 'Статус: ' + qs[i].status + ' Тематика: ' + qs[i].thematics + ' Дата: ' + qs[i].date.split('T')[0] + ' Уровень сложности: ' + qs[i].level_of_complexity.name_level + ' Автор: ' + qs[i].user.userName + ' </span></br>';
            //проверка прав, для неавториизрованных пользователей недоступно редактирование
            if (user_id != -1) {
                qsHTML += '<button type="button" class= "btn btn-primary btn-icon" onclick="quests.editQuest(' + qs[i].id_quest + ')"><span class="glyphicon glyphicon-pencil" aria-hidden="true" style="padding: 7px 6px;"></span>Изменить</button>';
                if (autor.dataset.access == "true") qsHTML += '<button type="button" class= "btn btn-primary btn-icon" onclick="quests.deleteQuest(' + qs[i].id_quest + ')"><span class="glyphicon glyphicon-remove" aria-hidden="true" style="padding: 7px 6px;"></span>Удалить</button>';
            }
            qsHTML+= quests.parseLoved(qs[i], user_id); // парсим Избранное
            if (typeof qs[i].questRiddle !== "undefined" && qs[i].questRiddle.length > 0) {//выводим загадки для квестов
                let j;
                for (j in qs[i].questRiddle) {
                    qsHTML += '<p class="riddle">' + 'Текст загадки: ' + qs[i].questRiddle[j].riddle.text + ' Ответ: ' + qs[i].questRiddle[j].riddle.answer.object;
                    if (user_id != -1) qsHTML += '<button class = "del-button" onclick="quests.deleteRiddleInQuest(' + qs[i].id_quest + ',' + qs[i].questRiddle[j].id_Riddle_Fk + ')">&#10006;</button> </p>';
                }
            }
        }
        return qsHTML;
    },
    parseLoved: function (qs, user_id) {// вывод избранного
        let qsHTML = "";
        let isLoved = false;
        if (user_id != -1)//Избранное существует только для авторизированных пользователей
        {
            for (let j in qs.userQuest) {
                if (qs.userQuest[j].id_User_Fk == user_id /*id*/)
                    isLoved = true;
            }
            qsHTML += isLoved == false ? '<button type="button" onclick="quests.addQuestLoved(' + qs.id_quest + ')"><span class="glyphicon glyphicon-star" aria-hidden="true" ></span></button></div>' : '<button type="button"  onclick="quests.deleteQuestLoved(' + qs.id_quest + ',' + user_id + ')"><span class="glyphicon glyphicon-star" aria-hidden="true" style="color: yellow"></span></button></div>';
        }
        else qsHTML += "</div>";
        return qsHTML;
    },
  createQuest: function()//функция создания нового квеста
  { var _thematics = document.querySelector("#createThematics").value;//заполняем свойства квеста выбранными значениями
    var _status = false;
    var _date = new Date();
    var objSel = document.getElementById("createLevelId");
    var _level = parseInt(objSel.options[objSel.selectedIndex].value, 10);// для комбобоксов сохраняем айдишник
    var autor = document.getElementById('questsDiv');
    var _user = autor.dataset.user;
    var request = new XMLHttpRequest();
    request.open("POST", uri + 'Create');
    request.onload = function () {
        quests.getQuests();
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав на создание";
        } else if (request.status === 201) {
            msg = "Запись добавлена";
            quests.getQuests();
        } else {
            msg = "Неизвестная ошибка";
        }
        document.querySelector("#actionMsg").innerHTML = msg;
    };//вывод сообщений об ощибке, если что пошло не так
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
    }, 
  createRiddle: function(_id_quest, _id_riddle) {//функция добавления загадки в квест, их айдишник пеердаются параметрами
        var request2 = new XMLHttpRequest();
        request2.open("POST", "/api/QR/");
        request2.onload = function () {
         quests.getQuests();
         var msg = "";
        if (request2.status === 401) {
        msg = "У вас не хватает прав";
        } else if (request2.status === 200) {
        msg = "Успешно";
        quests.getQuests();
        } else {
        msg = "Неизвестная ошибка";
        }
        document.querySelector("#actionMsg").innerHTML = msg;};
        request2.setRequestHeader("Accepts", "application/json;charset=UTF-8");
        request2.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        request2.send(JSON.stringify({ id_quest: _id_quest, id_riddle: _id_riddle }));
    },
 createRiddleInQuest: function() {//промежуточная функция добавления загадки в квест
        var _id_quest = document.querySelector("#id_quest").value;
        var _id_riddle = document.querySelector("#id_riddle").value;
        if (_id_riddle == "") {// в случае, если поле ввода номера загадки пустое, пробуем найти загадку по фильтру
        var objSel = document.getElementById("createLevelIdR");
        var _level = parseInt(objSel.options[objSel.selectedIndex].value, 10);
        objSel = document.getElementById("createTypeIdR");
        var _type = parseInt(objSel.options[objSel.selectedIndex].value, 10);
        objSel = document.getElementById("createAnswerIdR");
        var _answer = parseInt(objSel.options[objSel.selectedIndex].value, 10);
        var request = new XMLHttpRequest();
        request.open("GET", "/api/riddle/" + "GetRiddle?id_level=" + _level + "&id_type=" + _type + "&id_answer=" + _answer);
        request.onload = function () {//возращается загадка по фильтрам
        let ls = "";
        ls = JSON.parse(request.responseText);
        if (typeof ls.id_riddle !== "undefined") _id_riddle = ls.id_riddle;
            quests.createRiddle(_id_quest, _id_riddle);// если все ок и такая загадка есть, то переходим к ее добавлению
        }
            request.send();}
       else quests.createRiddle(_id_quest, _id_riddle);
     },
  editQuest: function (id) {// функция подготовки к редактированию квеста
    let elm = document.querySelector("#editDiv");
    elm.style.display = "block";// выводим форму редактирования
    elm.dataset.edit = "true";
    elm = document.querySelector("#id_quest");
    elm.innerText = id;
    elm.value = id;
    if (items) {
        let i;
        for (i in items) {
            if (id === items[i].id_quest) {
                document.querySelector("#edit-id").value = items[i].id_quest;
                document.querySelector("#edit-thematics").value = items[i].thematics;
                collections.GetComboboxLevel("editLevelId");//заполняем комбобкс
                let objSel = document.getElementById("editLevelId");
                let j;
                for (j in objSel.options) {//отмечаем выбранный вариант
                    if (objSel.options[j].value == items[i].id_level_Fk) objSel.options[j].selected = true;
                }
                document.querySelector("#edit-user").value = items[i].id_autor_Fk;
            }
        }
    }
}, 
 updateQuest: function(){//функция редактирования квеста
    let objSel = document.getElementById("editLevelId");
    var autor = document.getElementById('questsDiv'); //получаем текущего пользователя
    const quest = {
        questid: document.querySelector("#edit-id").value,
        status: false,//document.querySelector("#edit-status").value,
        number_of_question: 0,
        thematics: document.querySelector("#edit-thematics").value,
        date: new Date(), //дату устанваливаем текущую
        id_level_Fk: objSel.options[objSel.selectedIndex].value,
        id_autor_Fk: autor.dataset.user
    };
    var request = new XMLHttpRequest();
    request.open("PUT", uri + 'Update/' + quest.questid);
    request.onload = function () {
        quests.getQuests();
        quests.closeInput();
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав на редактирование";
        } else if (request.status === 204) {
            msg = "Запись отредактирована";
            quests.getQuests();
        } else {
            msg = "Неизвестная ошибка";
        }
        document.querySelector("#actionMsg").innerHTML = msg;
    };//выводим ошибки
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify(quest));
}, 

 deleteQuest: function(id) {//функция удаления квеста
        let request = new XMLHttpRequest();
        request.open("DELETE", uri + 'Delete/' + id, false);
        request.onload = function () {
    // Обработка кода ответа
    var msg = "";
    if (request.status === 401) {
        msg = "У вас не хватает прав на удаление";
    } else if (request.status === 204) {
        msg = "Запись удалена";
        quests.getQuests();
    } else {
        msg = "Неизвестная ошибка";
    }
    document.querySelector("#actionMsg").innerHTML = msg;
    quests.getQuests();
};//вывод ошибок
        request.send();
   // getQuests();

    }, 
 addQuestLoved: function(id){//функция добавления в Избранное
    var request = new XMLHttpRequest();
    request.open("POST", "/api/UQ/");
   request.onload = function () {
    quests.getQuests();
   };
   request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
   request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
   request.send(JSON.stringify({ id_quest: id, id_user: -1 }));
  }, 
 deleteQuestLoved: function(id_q, id_u) {//функция удаляет квест из Избранного
    let request = new XMLHttpRequest();
    request.open("DELETE", "/api/UQ/");
    request.onload = function () {
        quests.getQuests();
    };
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify({ id_quest: id_q, id_user: id_u }));

},
deleteRiddleInQuest: function(id_q, id_r) {// функция удаляет загадку из квеста
    let request = new XMLHttpRequest();
    request.open("DELETE", "/api/QR/");
    request.onload = function () {
        quests.getQuests();
        document.querySelector("#actionMsg").innerHTML = msg;
    };
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify({ id_quest: id_q, id_riddle: id_r }));
},

 closeInput: function() {// скрытие формы редактирования
    let elm = document.querySelector("#editDiv");
    elm.style.display = "none";
}

}




