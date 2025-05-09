import {sendStatement, sendFinalExamStatement} from "./xApi/xApi-statements.js"
var amountOfCorrectAnswers = sessionStorage.getItem('correctAnswers')
var amountOfCorrectFinalAnswers = sessionStorage.getItem('correctFinalAnswers')
document.addEventListener("DOMContentLoaded", function () {
    const vm = window.viewModel;
    //console.log(vm.Episode.Name)
    //console.log(vm.Chapter.Name)
    if (isExamination === true) {
        showCorrectFinalAnswers(vm)
    }
    else {
        showCorrectAnswers(vm)
    }    
    
});
function showCorrectAnswers(vm) {
    var resultText = document.querySelector("#result");

    let starHtml = "";
    for (let i = 0; i < 5; i++) {
        if (i < amountOfCorrectAnswers) {
            starHtml += `<span class="star">&#9733;</span>`; // fylld stjärna
        } else {
            starHtml += `<span class="star-empty">&#9734;</span>`; // tom stjärna
        }
    }

    if (amountOfCorrectAnswers >= 3) {
        resultText.innerHTML = `Grattis du är godkänd! Du fick ${amountOfCorrectAnswers} av 5 rätt.<br>${starHtml}`;
        sendStatement("completed", "klarade", amountOfCorrectAnswers, vm.Chapter.Id, vm.Chapter.Name, vm.Episode.Id, vm.Episode.Name, true);
    } else {
        resultText.innerHTML = `Bra jobbat men tyvärr är du inte godkänd. Du fick ${amountOfCorrectAnswers} av 5 rätt.<br>Försök en gång till!<br>${starHtml}`;
        sendStatement("failed", "misslyckades med", amountOfCorrectAnswers, vm.Chapter.Id, vm.Chapter.Name, vm.Episode.Id, vm.Episode.Name, false);
    }
}




function showCorrectFinalAnswers(vm) {
    var resultText = document.querySelector("#result");
    
    const starCount = Math.round((amountOfCorrectFinalAnswers / 10) * 5);
    
    let starHtmlFinal = "";
    for (let i = 0; i < 5; i++) {
        if (i < starCount) {
            starHtmlFinal += `<span class="star">&#9733;</span>`; // fylld stjärna
        } else {
            starHtmlFinal += `<span class="star-empty">&#9734;</span>`; // tom stjärna
        }
    }

    if (amountOfCorrectFinalAnswers >= 7) {
        resultText.innerHTML = `Grattis du är godkänd! Du fick ${amountOfCorrectFinalAnswers} av 10 rätt.<br>${starHtmlFinal}`;
        sendFinalExamStatement("completed", "klarade", amountOfCorrectFinalAnswers, true);
    } else {
        resultText.innerHTML = `Bra jobbat men tyvärr är du inte godkänd. Du fick ${amountOfCorrectFinalAnswers} av 10 rätt och det krävs 7 rätt för att bli godkänd.<br> Försök en gång till!<br>${starHtmlFinal}`;
        sendFinalExamStatement("failed", "misslyckades med", amountOfCorrectFinalAnswers, false);
    }
}


