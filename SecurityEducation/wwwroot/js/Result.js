import { sendStatement, sendFinalExamStatement } from "./xApi/xApi-statements.js"
import { sendQuery } from "./xApi/xApiQueries.js"
let xapiData;

var amountOfCorrectAnswers = parseInt(sessionStorage.getItem('correctAnswers'))
var amountOfCorrectFinalAnswers = parseInt(sessionStorage.getItem('correctFinalAnswers'))
document.addEventListener("DOMContentLoaded", function () {
    xapiData = JSON.parse(sessionStorage.getItem("myXapiQuery"));
    const vm = window.viewModel;
    if (isExamination === true) {
        showCorrectFinalAnswers(vm)
        const form = document.querySelector("form");
     
        if (form) {
            form.addEventListener("submit", function () {
                document.getElementById("nameInput").value = `${xapiData?.statements[0].actor?.name}` || "Okänd";
                document.getElementById("starsInput").value = GetTotalAmountOfStars();
                document.getElementById("chaptersInput").value = JSON.stringify(GetAllChapterNames());
                document.getElementById("episodesInput").value = JSON.stringify(GetAllEpisodeNames());
            });
        }
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
            starHtml += `<span class="star">&#9733;</span>`;
        } else {
            starHtml += `<span class="star-empty">&#9734;</span>`;
        }
    }

    if (amountOfCorrectAnswers >= 3) {
        resultText.innerHTML = `<img src="/images/Kottemedbådetummarupp.png" alt="Igelkott med båda tummarna upp" />
        <br>Grattis du är godkänd! Du fick ${amountOfCorrectAnswers} av 5 rätt.<br>${starHtml}`;
        sendStatement("completed", "klarade", amountOfCorrectAnswers, vm.Chapter.Id, vm.Chapter.Name, vm.Episode.Id, vm.Episode.Name, true);
        const queryResult = sendQuery("completed");
        sessionStorage.setItem("myXapiQuery", JSON.stringify(queryResult)); 
        xapiData = JSON.parse(sessionStorage.getItem("myXapiQuery"));
        console.log("Tidigare hämtad xAPI-data:", xapiData.statements);
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
        medalImg.alt = "Igelkott med båda tummarna upp."
    } else if (amountOfCorrectAnswers === 4) {
        medalCongrat.innerHTML = "Grattis du har fått silvermedalj på avsnittet!<br>Om du försöker igen kan du säkert få guld!";
        medalText.textContent = "Silver";
        medalDiv.style.background = "silver";
        medalImg.src = "/images/Kottemedbådetummarupp.png";
        medalImg.alt = "Igelkott med båda tummarna upp."
    } else if (amountOfCorrectAnswers === 3) {
        medalCongrat.innerHTML = "Grattis du har fått bronsmedalj på avsnittet!<br>Om du försöker igen kan du säkert få silver eller guld!";
        medalText.textContent = "Brons";
        medalDiv.style.background = "#cd7f32"; 
        medalImg.src = "/images/Kottemedbådetummarupp.png";
        medalImg.alt = "Igelkott med båda tummarna upp."
    } else {        
        medalCongrat.innerHTML = "Tyvärr fick du ingen medalj denna gång.<br>Men försök gärna igen så går det säkert bättre!";
        medalImg.src = "/images/förvirrad_kotte.png";
        medalImg.alt = "Igelkott som ser förvirrad ut."
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
    console.log(result)

    let relevantItems = Object.values(bestResultsByEpisode);
        
    let allSuccess = relevantItems.filter(item => item.result?.success === true);
    let totalStars = 0;
    for (let item of allSuccess) {
        totalStars += item.result.score.raw;
    }
    const calcResult = Math.floor(totalStars / vm.Chapter.NumberOfEpisodes)
    
    if (allSuccess.length === parseInt(vm.Chapter.NumberOfEpisodes)) {
        
        if (calcResult === 5) {
            medalChapterCongrat.textContent = "Grattis du har fått guldmedalj på kapitlet!";
            medalChapterText.textContent = "Guld";
            medalChapterDiv.style.background = "gold";
            medalChapterImg.src = "/images/Kottemedbådetummarupp.png";
            medalChapterImg.alt = "Igelkott med båda tummarna upp.";
        } else if (calcResult === 4) {
            medalChapterCongrat.innerHTML = "Grattis du har fått silvermedalj på kapitlet!<br>Om du försöker igen kan du säkert få guld!";
            medalChapterText.textContent = "Silver";
            medalChapterDiv.style.background = "silver";
            medalChapterImg.src = "/images/Kottemedbådetummarupp.png";
            medalChapterImg.alt = "Igelkott med båda tummarna upp.";
        } else if (calcResult === 3) {
            medalChapterCongrat.innerHTML = "Grattis du har fått bronsmedalj på kapitlet!<br>Om du försöker igen kan du säkert få silver eller guld!";
            medalChapterText.textContent = "Brons";
            medalChapterDiv.style.background = "#cd7f32";
            medalChapterImg.src = "/images/Kottemedbådetummarupp.png";
            medalChapterImg.alt = "Igelkott med båda tummarna upp.";
        } else {
            medalChapterCongrat.innerHTML = "Tyvärr fick du ingen kapitelmedalj denna gång.<br>Men försök gärna igen så går det säkert bättre!";
            medalChapterImg.src = "/images/förvirrad_kotte.png";
            medalChapterImg.alt = "Igelkott som ser förvirrad ut.";
            medalChapterDiv.style.background = "gray";
        };
    } else {        
        medalChapterCongrat.innerHTML = "Du har inte klarat alla avsnitt ännu.<br>Fortsätt kämpa för att få en kapitelmedalj!";
        medalChapterText.textContent = "";
        medalChapterDiv.style.background = "gray";
        medalChapterImg.src = "/images/förvirrad_kotte.png";
        medalChapterImg.alt = "Igelkott som ser förvirrad ut.";
    }

}
function getnumberOfCompletedEpisodes(chapterId) {
    let chapterArray = []
    xapiData?.statements.forEach(statement => {
        const extensionId = parseInt(statement.object?.definition?.extensions?.["https://localhost:7142/extensions/chapterId"]);
        if (extensionId === chapterId) {
            chapterArray.push(statement)
        }
    });
    return chapterArray
}

function showCorrectFinalAnswers(vm) {
    var resultText = document.querySelector("#result");
    
    
    const starCount = Math.round((amountOfCorrectFinalAnswers / 10) * 5);
    
    let starHtmlFinal = "";
    for (let i = 0; i < 5; i++) {
        if (i < starCount) {
            starHtmlFinal += `<span class="star">&#9733;</span>`;
        } else {
            starHtmlFinal += `<span class="star-empty">&#9734;</span>`; 
        }
    }

    if (amountOfCorrectFinalAnswers >= 7) {
        resultText.innerHTML = `<img src="/images/Kottemedbådetummarupp.png" alt="Igelkott med båda tummarna upp" />
        <br>Grattis du är godkänd! Du fick ${amountOfCorrectFinalAnswers} av 10 rätt.<br>${starHtmlFinal}`;
        sendFinalExamStatement("completed", "klarade", amountOfCorrectFinalAnswers, true);
        showDownloadDiplomaButton();
    } else {
        resultText.innerHTML = `<img src="/images/Kotte_med_tummarna_ner.png" alt="Igelkott med båda tummarna ned" />
        <br>Bra jobbat men tyvärr är du inte godkänd. Du fick ${amountOfCorrectFinalAnswers} av 10 rätt och det krävs 7 rätt för att bli godkänd.<br> Försök en gång till!<br>${starHtmlFinal}`;
        sendFinalExamStatement("failed", "misslyckades med", amountOfCorrectFinalAnswers, false);
    }

    const finalMedalDiv = document.querySelector(".final-medal-div");
    const finalMedalImg = finalMedalDiv.querySelector(".final-medal");
    const finalMedalText = finalMedalDiv.querySelector(".final-medal-text");

    const finalMedalContainerDiv = document.querySelector(".final-medal-container-div")
    const finalMedalCongrat = finalMedalContainerDiv.querySelector(".final-congrat-text");

    if (amountOfCorrectFinalAnswers === 10) {
        finalMedalCongrat.textContent = "Grattis du har fått guldmedalj på slutprovet!";
        finalMedalText.textContent = "Guld";
        finalMedalDiv.style.background = "gold";
        finalMedalImg.src = "/images/Kottemedbådetummarupp.png";
        finalMedalImg.alt = "Igelkott med båda tummarna upp.";
    } else if (amountOfCorrectFinalAnswers === 9) {
        finalMedalCongrat.innerHTML = "Grattis du har fått silvermedalj på slutprovet!<br>Om du försöker igen kan du säkert få guld!";
        finalMedalText.textContent = "Silver";
        finalMedalDiv.style.background = "silver";
        finalMedalImg.src = "/images/Kottemedbådetummarupp.png";
        finalMedalImg.alt = "Igelkott med båda tummarna upp.";
    } else if (amountOfCorrectFinalAnswers === 8) {
        finalMedalCongrat.innerHTML = "Grattis du har fått bronsmedalj på slutprovet!<br>Om du försöker igen kan du säkert få silver eller guld!";
        finalMedalText.textContent = "Brons";
        finalMedalDiv.style.background = "#cd7f32";
        finalMedalImg.src = "/images/Kottemedbådetummarupp.png";
        finalMedalImg.alt = "Igelkott med båda tummarna upp.";
    } else {
        finalMedalCongrat.innerHTML = "Tyvärr fick du ingen medalj denna gång.<br>Men försök gärna igen så går det säkert bättre!";
        finalMedalImg.src = "/images/förvirrad_kotte.png";
        finalMedalImg.alt = "Igelkott som ser förvirrad ut.";
        finalMedalDiv.style.background = "gray";
    }

   
    function showDownloadDiplomaButton() {
        var button = document.getElementById("diploma-btn");

        if (button) {
           button.removeAttribute("hidden");
        }
    }
}
function GetAllEpisodeNames() {
    let episodesInfo = [];
   
    window.chapters.forEach(chapter => {
        window.episodes.forEach(episode => {
            if (chapter.Id === episode.ChapterId) {
                if (!episodesInfo.some(e => e.id === episode.Id)) {
                    episodesInfo.push({
                        Id: episode.Id,
                        Name: episode.Name,
                        ChapterId: chapter.Id
                    });
                }
            }
        });
    });
    return episodesInfo;
}
function GetAllChapterNames() {
    let chapterInfo = [];
    
    window.chapters.forEach(chapter => {
        chapterInfo.push({
            Id: chapter.Id,
            Name: chapter.Name
        });
    });
    return chapterInfo;
}
function GetTotalAmountOfStars() {
    let totalAmountOfStars = 0;
    window.episodes.forEach(episode => {
        let bestStatement = null;
        let highestScore = -Infinity;
        xapiData?.statements.forEach(statement => {
            const extensionId = parseInt(statement.object?.definition?.extensions?.["https://localhost:7142/extensions/episodeId"]);

            if (extensionId === episode.Id) {
                const score = statement.result?.score?.raw ?? 0;

                if (score > highestScore) {
                    highestScore = score;
                    bestStatement = statement;
                }
            }
        });
        if (bestStatement) {
            totalAmountOfStars += highestScore;
        }
    })

    let bestStatement = null;
    let highestScore = -Infinity;
    xapiData?.statements.forEach(statement => {
        statement.object?.id === "https://localhost:7142/Test/ExaminationResult";
        
        const score = statement.result?.score?.raw ?? 0;
        if (score > highestScore) {
            highestScore = score;
            bestStatement = statement;
        }
    });
  
    if (bestStatement) {
        totalAmountOfStars += highestScore/2;
    }
    return totalAmountOfStars;
}





