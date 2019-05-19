const uri = "/api/account/";
document.addEventListener("DOMContentLoaded", function (event) {
 
    loved.getQuests();
});
// модуль "Избранное"
var loved = {
    getQuests: function () { //функция формирования избранных квестов для текущего пользователя
        let request = new XMLHttpRequest();
        request.open("POST", uri + 'GetUser'); // запрос текущего пользователя
        request.onload = function () {
            let qs = "";
            let qsHTML = "";
            qs = JSON.parse(request.responseText); 
            if (typeof qs.userQuest !== "undefined" && qs != -1) { 
                if (qs.userQuest.length > 0) {
                    var i;
                    for (i in qs.userQuest) { //проходимся по списку избранных квестов у полученного пользователя
                        if (typeof qs.userQuest[i].quest != "undefined") {
                            qsHTML += '<div class="quest"><span>' + 'Квест №' + qs.userQuest[i].quest.id_quest + ' : ' + 'Статус: ' + qs.userQuest[i].quest.status + ' Тематика: ' + qs.userQuest[i].quest.thematics + ' Дата: ' + qs.userQuest[i].quest.date.split('T')[0] + ' Уровень сложности: ' + qs.userQuest[i].quest.level_of_complexity.name_level + /*' Автор: ' + qs.userQuest[i].quest.user.userName + */' </span></div>';
                            if (typeof qs.userQuest[i].quest.questRiddle !== "undefined" && qs.userQuest[i].quest.questRiddle.length > 0) {//выводим для этих квестов загадки
                                let k;
                                for (k in qs.userQuest[i].quest.questRiddle) {
                                    qsHTML += '<p class="riddle">' + 'Текст загадки: ' + qs.userQuest[i].quest.questRiddle[k].riddle.text + ' Ответ: ' + qs.userQuest[i].quest.questRiddle[k].riddle.answer.object;
                                }
                            }
                        }
                    }
                }
                document.querySelector("#questsDiv").innerHTML = qsHTML; //присваиваем разметку
            }
        };
        request.send();
    }
}
