const uri = "/api/account/";
document.addEventListener("DOMContentLoaded", function (event) {
   // getCurrentUser();
    getQuests();
});
//function getCurrentUser() {
//    let request = new XMLHttpRequest();
//    // let id = 0;
//    var user_id;
//    request.open("POST", "/api/Account/isAuthenticated", true);
//    request.onload = function () {
//        /*
//        let myObj = "";
//        myObj = request.responseText !== "" ?
//            JSON.parse(request.responseText) : {};
//        document.getElementById("msg").innerHTML = myObj.message;*/
//        try {
//            user_id = JSON.parse(request.responseText);

//        }

//        catch (err) {
//            user_id = -1;//
//        }
//        var autor = document.getElementById('questsDiv');
//        autor.dataset.user = user_id;
//        getQuests();
//    };
//    request.send();
//}
function getQuests() {
    
    let request = new XMLHttpRequest();
    request.open("POST", uri + 'GetUser');
    request.onload = function () {
        let qs = "";
        let qsHTML = "";
        qs = JSON.parse(request.responseText);
        if (typeof qs.userQuest!== "undefined"&&qs!=-1) {
            // getCount(qs.length);
            if (qs.userQuest.length > 0) {
                var i;
                for (i in qs.userQuest) {
                    if (typeof qs.userQuest[i].quest != "undefined") {
                        
                            qsHTML += '<div class="quest"><span>' + 'Квест №' + qs.userQuest[i].quest.id_quest + ' : ' + 'Статус: ' + qs.userQuest[i].quest.status + ' Тематика: ' + qs.userQuest[i].quest.thematics + ' Дата: ' + qs.userQuest[i].quest.date.split('T')[0] + ' Уровень сложности: ' + qs.userQuest[i].quest.level_of_complexity.name_level + /*' Автор: ' + qs.userQuest[i].quest.user.userName + */' </span></div>';
                            if (typeof qs.userQuest[i].quest.questRiddle !== "undefined" && qs.userQuest[i].quest.questRiddle.length > 0) {
                                let k;
                               for (k in qs.userQuest[i].quest.questRiddle) {
                                   qsHTML += '<p class="riddle">' + 'Текст загадки: ' + qs.userQuest[i].quest.questRiddle[k].riddle.text + ' Ответ: ' + qs.userQuest[i].quest.questRiddle[k].riddle.answer.object;
                                       // '<button class = "del-button" onclick="deleteRiddleInQuest(' + qs.userQuest[i].quest.id_quest + ',' + qs.userQuest[i].quest.questRiddle[k].id_Riddle_Fk + ')">&#10006;</button> </p>';
                                }
                            
                            }
                    }

                }
                
                        
                    
                
            }
          //  items = qs;
            document.querySelector("#questsDiv").innerHTML = qsHTML;
        }
    };
    request.send();
}