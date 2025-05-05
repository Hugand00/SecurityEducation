import {sendStatement} from "./xApi/xApi-statements.js"
var amountOfCorrectAnswers = sessionStorage.getItem('correctAnswers')
document.addEventListener("DOMContentLoaded", function () {
    const vm = window.viewModel;
    console.log(vm.Episode.Name)
    console.log(vm.Chapter.Name)
    showCorrectAnswers(vm)
});
function showCorrectAnswers(vm) {
    var resultText = document.querySelector("#result")

    if (amountOfCorrectAnswers >= 3) {
        resultText.textContent = `Grattis du är godkänd! Du fick ${amountOfCorrectAnswers}/5`
        sendStatement("completed", "klarade", amountOfCorrectAnswers, vm.Chapter.Id, vm.Chapter.Name, vm.Episode.Id, vm.Episode.Name, true);
    }
    else {
        resultText.textContent = `Tyvärr är du inte godkänd! Du fick ${amountOfCorrectAnswers}/5`
        sendStatement("failed", "misslyckades med", amountOfCorrectAnswers, vm.Chapter.Id, vm.Chapter.Name, vm.Episode.Id, vm.Episode.Name, false);
    }
    
}
