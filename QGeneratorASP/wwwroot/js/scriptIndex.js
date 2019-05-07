document.addEventListener("DOMContentLoaded", function (event) {
    getCurrentUser();
});
function getCurrentUser() {
    let request = new XMLHttpRequest();
    request.open("POST", "/api/Account/isAuthenticated", true);
    request.onload = function () {
        let myObj = document.getElementById("user");
       myObj = request.responseText !== "" ?
            JSON.parse(request.responseText) : {};
        
       // document.getElementById("user").innerHTML = myObj;
    };
    request.send();
}