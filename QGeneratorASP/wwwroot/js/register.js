const uri = "/api/account/Register";

function closeInput() {

    let elm = document.querySelector("#formE");
    elm.style.display = "none";
    let elm1 = document.querySelector("#formR");
    elm1.style.display = "block";
}
function logIn() {
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
            document.getElementById("myPassword").value = "";
        }
    };
    // Запрос на сервер
    request.send(JSON.stringify({
        login: login,
        password: password
    }));
}

// Обработка кликов по кнопкам
document.getElementById("loginBtn").addEventListener("click", logIn);
document.getElementById("logoffBtn").addEventListener("click", closeInput);

function Register() {
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
        ParseResponse(this);
    };
    // Запрос на сервер
    request.send(JSON.stringify({
        login: login,
        password: password,
        passwordConfirm: passwordConfirm,
        accessLevel:false
    }));
}

// Разбор ответа
function ParseResponse(e) {
    // Очистка контейнера вывода сообщений
    document.querySelector("#msg").innerHTML = "";
    var formError = document.querySelector("#formError");
    while (formError.firstChild) {
        formError.removeChild(formError.firstChild);
    }
    // Обработка ответа от сервера
    let response = JSON.parse(e.responseText);
    document.querySelector("#msg").innerHTML = response.message;
    // Вывод сообщений об ошибках
    if (response.error.length > 0) {
        for (var i = 0; i < response.error.length; i++) {
            let ul = document.querySelector("ul");
            let li = document.createElement("li");
            li.appendChild(document.createTextNode(response.error[i]));
            ul.appendChild(li);
        }
    }
    // Очистка полей паролей
    document.querySelector("#password").value = "";
    document.querySelector("#passwordConfirm").value = "";
}

// Обработка клика по кнопке регистрации
document.querySelector("#registerBtn").addEventListener("click", Register);
