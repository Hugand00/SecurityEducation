import { sendStatement, sendFinalExamStatement } from "./xApi/xApi-statements.js"
import { getnumberOfCompletedEpisodes } from "./chapter.js"
var amountOfCorrectAnswers = parseInt(sessionStorage.getItem('correctAnswers'))
var amountOfCorrectFinalAnswers = parseInt(sessionStorage.getItem('correctFinalAnswers'))
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
        resultText.innerHTML = `<img src="/images/Kottemedbådetummarupp.png" alt="Igelkott med båda tummarna upp" />
        <br>Grattis du är godkänd! Du fick ${amountOfCorrectAnswers} av 5 rätt.<br>${starHtml}`;
        sendStatement("completed", "klarade", amountOfCorrectAnswers, vm.Chapter.Id, vm.Chapter.Name, vm.Episode.Id, vm.Episode.Name, true);
    } else {
        resultText.innerHTML = `<img src="/images/Kotte_med_tummarna_ner.png" alt="Igelkott med båda tummarna ned" />
        <br>Bra jobbat men tyvärr är du inte godkänd. Du fick ${amountOfCorrectAnswers} av 5 rätt.<br>Försök en gång till!<br>${starHtml}`;

        sendStatement("failed", "misslyckades med", amountOfCorrectAnswers, vm.Chapter.Id, vm.Chapter.Name, vm.Episode.Id, vm.Episode.Name, false);
    }

    const medalDiv = document.querySelector(".episode-medal-div");    
    const medalImg = medalDiv.querySelector(".episode-medal");
    const medalText = medalDiv.querySelector(".episode-medal-text");

    const medalContainerDiv = document.querySelector(".episode-medal-container-div")
    const medalCongrat = medalContainerDiv.querySelector(".episode-congrat-text");
    
    if (amountOfCorrectAnswers === 5) {
        medalCongrat.textContent = "Grattis du har fått guldmedalj på avsnittet!";
        medalText.textContent = "Guld";
        medalDiv.style.background = "gold";
        medalImg.src = "/images/Kottemedbådetummarupp.png";        
    } else if (amountOfCorrectAnswers === 4) {
        medalCongrat.innerHTML = "Grattis du har fått silvermedalj på avsnittet!<br>Om du försöker igen kan du säkert få guld!";
        medalText.textContent = "Silver";
        medalDiv.style.background = "silver";
        medalImg.src = "/images/Kottemedbådetummarupp.png";
    } else if (amountOfCorrectAnswers === 3) {
        medalCongrat.innerHTML = "Grattis du har fått bronsmedalj på avsnittet!<br>Om du försöker igen kan du säkert få silver eller guld!";
        medalText.textContent = "Brons";
        medalDiv.style.background = "#cd7f32"; 
        medalImg.src = "/images/Kottemedbådetummarupp.png";
    } else {        
        medalCongrat.innerHTML = "Tyvärr fick du ingen medalj denna gång.<br>Men försök gärna igen så går det säkert bättre!";
        //medalText.textContent = "";
        medalImg.src = "/images/förvirrad_kotte.png";
        medalDiv.style.background = "gray";
    }

    const medalChapterContainerDiv = document.querySelector(".chapter-medal-container-div")
    const medalChapterCongrat = medalChapterContainerDiv.querySelector(".chapter-congrat-text");

    const medalChapterDiv = medalChapterContainerDiv.querySelector(".chapter-medal-div");
    const medalChapterImg = medalChapterDiv.querySelector(".chapter-medal");
    const medalChapterText = medalChapterDiv.querySelector(".chapter-medal-text");

    


    const result = getnumberOfCompletedEpisodes(vm.Chapter.Id)
    let rawRelevantItems = result.filter(item =>
        item.object?.definition?.extensions?.["https://localhost:7142/extensions/chapterId"] === vm.Chapter.Id
    );
    let bestResultsByEpisode = {};

    for (let item of rawRelevantItems) {
        let episodeId = item.object?.definition?.extensions?.["https://localhost:7142/extensions/episodeId"];
        let score = item.result?.score?.raw ?? 0;
        if (!episodeId) continue;
        if (!bestResultsByEpisode[episodeId] || score > (bestResultsByEpisode[episodeId].result?.score?.raw ?? 0)) {
            bestResultsByEpisode[episodeId] = item;
        }
    }
    console.log(rawRelevantItems);    
    console.log(vm.Chapter);

    let relevantItems = Object.values(bestResultsByEpisode);
        
    let allSuccess = relevantItems.filter(item => item.result?.success === true);
    let totalStars = 0;
    for (let item of allSuccess) {
        totalStars += item.result.score.raw;
    }
    const calcResult = Math.round(totalStars / vm.Chapter.NumberOfEpisodes)
    console.log(calcResult);
    console.log(allSuccess.length);
    if (allSuccess.length === parseInt(vm.Chapter.NumberOfEpisodes)) {
        console.log(calcResult);
        if (calcResult === 5) {
            medalChapterCongrat.textContent = "Grattis du har fått guldmedalj på kapitlet!";
            medalChapterText.textContent = "Guld";
            medalChapterDiv.style.background = "gold";
            medalChapterImg.src = "/images/Kottemedbådetummarupp.png";
        } else if (calcResult === 4) {
            medalChapterCongrat.innerHTML = "Grattis du har fått silvermedalj på kapitlet!<br>Om du försöker igen kan du säkert få guld!";
            medalChapterText.textContent = "Silver";
            medalChapterDiv.style.background = "silver";
            medalChapterImg.src = "/images/Kottemedbådetummarupp.png";
        } else if (calcResult === 3) {
            medalChapterCongrat.innerHTML = "Grattis du har fått bronsmedalj på kapitlet!<br>Om du försöker igen kan du säkert få silver eller guld!";
            medalChapterText.textContent = "Brons";
            medalChapterDiv.style.background = "#cd7f32";
            medalChapterImg.src = "/images/Kottemedbådetummarupp.png";
        } else {
            medalChapterCongrat.innerHTML = "Tyvärr fick du ingen kapitelmedalj denna gång.<br>Men försök gärna igen så går det säkert bättre!";
            //medalText.textContent = "";
            medalChapterImg.src = "/images/förvirrad_kotte.png";
            medalChapterDiv.style.background = "gray";
        };
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






