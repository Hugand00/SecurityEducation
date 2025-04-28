var amountOfCorrectAnswers = sessionStorage.getItem('correctAnswers')
showCorrectAnswers()
function showCorrectAnswers() {
    var resultText = document.querySelector("#result")

    if (amountOfCorrectAnswers >= 3) {
        resultText.textContent = `Grattis du är godkänd! Du fick ${amountOfCorrectAnswers}/5`
    }
    else {
        resultText.textContent = `Tyvärr är du inte godkänd! Du fick ${amountOfCorrectAnswers}/5`
    }
    
}
