const xapiData = JSON.parse(sessionStorage.getItem("myXapiQuery"));
console.log("Tidigare hämtad xAPI-data:", xapiData.statements);

ShowStoredEpisodes()
ShowStoredChapter()

///OBS inte klar! 
function ShowStoredChapter() {
    const chapterDivs = document.querySelectorAll(".chapter-div")
   
    chapterDivs.forEach(div => {
        const chapterId = parseInt(div.getAttribute("data-chapter-div"));
        const chapterStars = div.querySelector(".star-div")

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
        let totalStars = 0;
        for (let item of allSuccess) {
            totalStars += item.result.score.raw;
        }
        const calcResult = Math.round(totalStars / numberOfEpisodes)
        console.log(calcResult)
        for (let i = 0; i < calcResult; i++) {
            const star = document.createElement("p")
            star.innerHTML = "&#9733";
            chapterStars.appendChild(star)
        }
        for (let i = 0; i < 5 - calcResult; i++) {
            const star = document.createElement("p")
            star.innerHTML = "&#9734";
            chapterStars.appendChild(star)
        }
    })
    
}
function ShowStoredEpisodes() {
    const episodes = document.querySelectorAll(".episode-div")
    
    episodes.forEach(div => {
        const episodeId = parseInt(div.getAttribute("data-episode-div"))
        const episodeStars = div.querySelector(".episode-star-div")

        const matchingStatement = xapiData?.statements.find(statement => {
            const extensionId = parseInt(statement.object?.definition?.extensions?.["https://localhost:7142/extensions/episodeId"]);
            return extensionId === episodeId;
        });
        console.log(matchingStatement)
        if (matchingStatement) {
            const score = matchingStatement.result;
            console.log(score)
            if (score.success === true) {
                for (let i = 0; i < score.score.raw; i++) {
                    const star = document.createElement("p")
                    star.innerHTML = "&#9733";
                    episodeStars.appendChild(star)
                }
                if (score.score.raw < 5) {
                    for (let i = 0; i < 5 - score.score.raw; i++) {
                        const star = document.createElement("p")
                        star.innerHTML = "&#9734";
                        episodeStars.appendChild(star)
                    }
                }
            }
            else {
                for (let i = 0; i < 5; i++) {
                    const star = document.createElement("p")
                    star.innerHTML = "&#9734";
                    episodeStars.appendChild(star)
                }
            }

        }
        else {
            for (let i = 0; i < 5; i++) {
                const star = document.createElement("p")
                star.innerHTML = "&#9734";
                episodeStars.appendChild(star)
            }
          }
    });
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