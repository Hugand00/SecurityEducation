const xapiData = JSON.parse(sessionStorage.getItem("myXapiQuery"));
console.log("Tidigare hämtad xAPI-data:", xapiData.statements);
showOverview()
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

            // Progressbar
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

            // Lägg ihop och lägg till i sidan
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
