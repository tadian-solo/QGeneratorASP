document.addEventListener("DOMContentLoaded", function (event) {
   // user.getCurrentUser();
});

/*var user = {
    getCurrentUser: function () {
        let request = new XMLHttpRequest();
        request.open("POST", "/api/Account/isAuthenticated", true);
        request.onload = function () {
            let myObj = document.getElementById("user");
            myObj = request.responseText !== "" ?
                JSON.parse(request.responseText) : {};
        };
        request.send();
    }
}*/
