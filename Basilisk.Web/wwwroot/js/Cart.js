var btnChecks = document.querySelectorAll("[name='check']");
btnChecks.forEach(function (checkbox) {
    checkbox.addEventListener("change", function () {
        var aChecked = checkbox.nextElementSibling;
        aChecked.click();
    });
});


var btnChecks = document.querySelectorAll("[name='checkAll']");
btnChecks.forEach(function (checkbox) {
    checkbox.addEventListener("change", function () {
        var aChecked = checkbox.nextElementSibling;
        aChecked.click();
    });
});

//var btnModal = document.getElementById("modBtn")

//btnModal.addEventListener("click", function () {
//    document.getElementById("modal").show();
//})