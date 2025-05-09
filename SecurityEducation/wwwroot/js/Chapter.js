const xapiData = JSON.parse(sessionStorage.getItem("myXapiQuery"));
console.log("Tidigare hämtad xAPI-data:", xapiData.statements);

showStoredChapters()
showExamination()
allChaptersDone()
function showStoredChapters() {
    const chapterDivs = document.querySelectorAll(".chapter-div");

    chapterDivs.forEach(div => {
        const chapterId = parseInt(div.getAttribute("data-chapter-div"));
        const chapterComplete = div.querySelector(".completed");
        const completedEpisodes = div.querySelector(".completed-episodes");
        const result = getnumberOfCompletedEpisodes(chapterId)
     
        let rawRelevantItems = result.filter(item =>
            item.object?.definition?.extensions?.["https://localhost:7142/extensions/chapterId"] === chapterId
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
        let relevantItems = Object.values(bestResultsByEpisode);
        let chapter = chapters.find(item => item.Id === chapterId);

        let numberOfEpisodes = []
        if (chapter) {
            numberOfEpisodes = chapter.NumberOfEpisodes;
        }       
        let allSuccess = relevantItems.filter(item => item.result?.success === true);
        if (allSuccess.length === numberOfEpisodes) {
            chapterComplete.textContent = "Avklarad";
        }
        else{
            chapterComplete.textContent = "Inte avklarad"
            chapterComplete.style.background = "red"
        }
        if (completedEpisodes) {
            completedEpisodes.textContent = `Avklarade avsnitt: ${allSuccess.length}/${numberOfEpisodes}`;
        }
    });
}

function showExamination() {
    const examDiv = document.querySelector("#examination");
    const result = allChaptersDone()
    if (!result) {
        const overlay = document.createElement("div")
        overlay.classList.add("overlay")
        examDiv.appendChild(overlay)
    }  
}

function allChaptersDone() {
    let chaptersDone = 0
    chapters.forEach(chapter => {
        const result = getnumberOfCompletedEpisodes(chapter.Id)
        
        let rawRelevantItems = result.filter(item =>
            item.object?.definition?.extensions?.["https://localhost:7142/extensions/chapterId"] === chapter.Id
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
        if (bestResultsByEpisode.length === chapter.NumberOfEpisodes) {
            chaptersDone++;
        } 
    })
    if (chaptersDone === chapters.length)
    { return true }
    else { return false }
}

export function getnumberOfCompletedEpisodes(chapterId) {
    let chapterArray = []
    xapiData?.statements.forEach(statement => {
        const extensionId = parseInt(statement.object?.definition?.extensions?.["https://localhost:7142/extensions/chapterId"]);
        if (extensionId === chapterId) {
            chapterArray.push(statement)
        }
    });
    return chapterArray
}