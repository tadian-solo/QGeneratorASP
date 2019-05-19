const uri = "/api/account/Register";
//модуль формы входа/регистрации
var loginForm = {
    ParseResponse: function(e) {
        // Очистка контейнера вывода сообщений
        document.querySelector("#msg").innerHTML = "";
        var formError = document.querySelector("#formError");
        while (formError.firstChild) {
            formError.removeChild(formError.firstChild);
        }
        // Обработка ответа от сервера
        let response = JSON.parse(e.responseText);
        document.querySelector("#msg").innerHTML = response.message;
        if (typeof response.error == "undefined") window.location.href = '/index.html';
        // Вывод сообщений об ошибках
        if (response.error.length > 0) {
            for (var i = 0; i < response.error.length; i++) {
                let ul = document.querySelector("ul");
                let li = document.createElement("li");
                li.appendChild(document.createTextNode(response.error[i]));
                ul.appendChild(li);
            }
        }
        else window.location.href = '/index.html';
        // Очистка полей паролей
        document.querySelector("#password").value = "";
        document.querySelector("#passwordConfirm").value = "";
    },
    
    logIn: function () {
        var login, password = "";
        // Считывание данных с формы
        login = document.getElementById("myLogin").value;
        password = document.getElementById("myPassword").value;
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
            loginForm.ParseResponse(this);
        };
        // Запрос на сервер
        request.send(JSON.stringify({
            login: login,
            password: password
        }));
    },
    closeInput: function () {
        let elm = document.querySelector("#formE");
        elm.style.display = "none";
        let elm1 = document.querySelector("#formR");
        elm1.style.display = "block";
    },
    logOff: function() {
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
         window.location.href = '/index.html';  //getQuests();
        };
        request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        request.send();
    },
    Register: function () {
        // Считывание данных с формы
        var login = document.querySelector("#login").value;
        var password = document.querySelector("#password").value;
        var passwordConfirm = document.querySelector("#passwordConfirm").value;
        let request = new XMLHttpRequest();
        request.open("POST", uri);
        request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
        request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        // Обработка ответа
        request.onload = function () {
            loginForm.ParseResponse(this);
        };
        // Запрос на сервер
        request.send(JSON.stringify({
            login: login,
            password: password,
            passwordConfirm: passwordConfirm,
            accessLevel: false
        }));
    }
}
// Обработка кликов по кнопкам
document.getElementById("loginBtn").addEventListener("click", loginForm.logIn);
document.getElementById("logOffBtn").addEventListener("click", loginForm.logOff);
document.getElementById("logoffBtn").addEventListener("click", loginForm.closeInput);
document.querySelector("#registerBtn").addEventListener("click", loginForm.Register);
