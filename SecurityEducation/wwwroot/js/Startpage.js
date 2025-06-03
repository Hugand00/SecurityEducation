import { sendQuery } from "./xApi/xApiQueries.js" 
const queryResult = sendQuery("completed");
sessionStorage.setItem("myXapiQuery", JSON.stringify(queryResult));
const xapiData = JSON.parse(sessionStorage.getItem("myXapiQuery"));
console.log("Tidigare hämtad xAPI-data:", xapiData.statements);
showOverview()
showTotalAmountOfStars()

function showOverview() {
    var div = document.querySelector(".overview-div");
    chapters.forEach(chapter => {
        var chapterName = document.createElement("p");
        chapterName.classList.add("chapter-name");

        var completedEpisode = document.createElement("p");
        completedEpisode.classList.add("completed-episode");

        var result = getnumberOfCompletedEpisodes(chapter.Id);

        let bestResultsByEpisode = {};
        let rawRelevantItems = result.filter(item =>
            item.object?.definition?.extensions?.["https://localhost:7142/extensions/chapterId"] === chapter.Id
        );
        for (let item of rawRelevantItems) {
            let episodeId = item.object?.definition?.extensions?.["https://localhost:7142/extensions/episodeId"];
            let score = item.result?.score?.raw ?? 0;
            if (!episodeId) continue;
            if (!bestResultsByEpisode[episodeId] || score > (bestResultsByEpisode[episodeId].result?.score?.raw ?? 0)) {
                bestResultsByEpisode[episodeId] = item;
            }
        }
        let relevantItems = Object.values(bestResultsByEpisode);

        let numberOfEpisodes = chapter?.NumberOfEpisodes ?? 0;
        let allSuccess = relevantItems.filter(item => item.result?.success === true);

        if (completedEpisode) {
            completedEpisode.textContent = `Avklarade avsnitt: ${allSuccess.length}/${numberOfEpisodes}`;
            chapterName.textContent = chapter.Name;

           
            var progressWrapper = document.createElement("div");
            progressWrapper.classList.add("progress-wrapper");

            
            var progressBar = document.createElement("div");
            progressBar.classList.add("progress-bar");

            
            let percent = numberOfEpisodes > 0 ? (allSuccess.length / numberOfEpisodes) * 100 : 0;
            progressBar.style.width = `${percent}%`;

            
            let percentText = document.createElement("span");
            percentText.textContent = `${Math.round(percent)}%`;
            percentText.classList.add("progress-text");
            progressBar.appendChild(percentText);

            
            progressWrapper.appendChild(progressBar);
            div.appendChild(chapterName);
            div.appendChild(completedEpisode);
            div.appendChild(progressWrapper);
        }
    });
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
function GetTotalAmountOfStars() {
    let totalAmountOfStars = 0;
    episodes.forEach(episode => {
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
        if (statement.object?.id === "https://localhost:7142/Test/ExaminationResult") {
            const score = statement.result?.score?.raw ?? 0;
            if (score > highestScore) {
                highestScore = score;
                bestStatement = statement;
            }
        }
    });
    console.log(bestStatement)
    if (bestStatement) {
        totalAmountOfStars += Math.floor(highestScore/2);
    }
    return totalAmountOfStars;
}
function showTotalAmountOfStars() {
    let amountOfStars = GetTotalAmountOfStars()
    var div = document.querySelector(".total-star-div")

    var amount = document.createElement("p")
    var star = document.createElement("p")
    star.innerHTML = `<span class="star">&#9733;</span>`
    star.classList.add("checked");
    amount.textContent = `${amountOfStars}/${GetAllStarsAmount()}`

    div.appendChild(amount)
    div.appendChild(star)
    
}
function GetAllStarsAmount() {
    let episodeStars = 0
    episodes.forEach(episode => {
        episodeStars += 5
    })
    episodeStars += 5
    return episodeStars
}

