const xapiData = JSON.parse(sessionStorage.getItem("myXapiQuery"));
console.log("Tidigare hämtad xAPI-data:", xapiData.statements);

ShowStoredEpisodes()
ShowStoredChapter()
showStoredExamination()

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
        if (allSuccess.length === numberOfEpisodes) {
            for (let i = 0; i < calcResult; i++) {
                const star = document.createElement("p")
                star.innerHTML = "&#9733";
                star.classList.add("checked")
                chapterStars.appendChild(star)
            }
            for (let i = 0; i < 5 - calcResult; i++) {
                const star = document.createElement("p")
                star.innerHTML = "&#9734";
                chapterStars.appendChild(star)
            }
            const medal = div.querySelectorAll(".chapter-medal-div")
         
            medal.forEach(div => {
                const medalImg = div.querySelector(".chapter-medal")
                const medalText = div.querySelector(".medal-text")
                console.log(medalImg)
                if (calcResult === 5) {
                    ///gold
                    medalText.textContent ="Guld"
                    div.style.background = "gold";
                    medalImg.src = "/images/Kottemedbådetummarupp.png";
                } else if (calcResult >= 3) {
                    ///silver
                    medalText.textContent = "Silver"
                    div.style.background = "silver";
                    medalImg.src = "/images/Kottemedbådetummarupp.png";
                } else {
                    ///bronze
                    medalText.textContent = "Brons"
                    div.style.background = "#cd7f32"
                    medalImg.src = "/images/Kottemedbådetummarupp.png";
                }
            })
           
        }
        else {
            for (let i = 0; i < 5; i++) {
                const star = document.createElement("p")
                star.innerHTML = "&#9734";
                chapterStars.appendChild(star)
            }
        }
    })
    
}
function ShowStoredEpisodes() {
    const episodes = document.querySelectorAll(".episode-div")
    
    episodes.forEach(div => {
        const episodeId = parseInt(div.getAttribute("data-episode-div"))
        const episodeStars = div.querySelector(".episode-star-div")
        
        let bestStatement = null;
        let highestScore = -Infinity;
        xapiData?.statements.forEach(statement => {
            const extensionId = parseInt(statement.object?.definition?.extensions?.["https://localhost:7142/extensions/episodeId"]);

            if (extensionId === episodeId) {
                const score = statement.result?.score?.raw ?? 0;

                if (score > highestScore) {
                    highestScore = score;
                    bestStatement = statement;
                }
            }
        });
            
        if (bestStatement) {
            const score = bestStatement.result;
            console.log(score)
            if (score.success === true) {
                for (let i = 0; i < score.score.raw; i++) {
                    const star = document.createElement("p")
                    star.innerHTML = "&#9733";
                    star.classList.add("checked")
                    episodeStars.appendChild(star)
                }
                if (score.score.raw < 5) {
                    for (let i = 0; i < 5 - score.score.raw; i++) {
                        const star = document.createElement("p")
                        star.innerHTML = "&#9734";
                        episodeStars.appendChild(star)
                    }
                }
                const medal = div.querySelectorAll(".episode-medal-div")
                
                medal.forEach(div => {
                    const medalImg = div.querySelector(".episode-medal")
                    const medalText = div.querySelector(".episode-medal-text")
                    if (score.score.raw === 5) {
                        ///gold
                        medalText.textContent = "Guld"
                        div.style.background = "gold";
                        medalImg.src = "/images/Kottemedbådetummarupp.png";
                    } else if (score.score.raw > 3) {
                        ///silver
                        medalText.textContent = "Silver"
                        div.style.background = "silver";
                        medalImg.src = "/images/Kottemedbådetummarupp.png";
                    } else if(score.score.raw = 3){
                        ///bronze
                        medalText.textContent = "Brons"
                        div.style.background = "#cd7f32"
                        medalImg.src = "/images/Kottemedbådetummarupp.png";
                    }
                });
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

function showStoredExamination() {
    const examinationStarDiv = document.querySelector(".examination-star-div")

    const matchingStatement = xapiData?.statements.find(statement => 
        statement.object?.id === "https://localhost:7142/Test/ExaminationResult"
    );
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
        const score = bestStatement.result;
        if (score.success === true) {
            for (let i = 0; i < score.score.raw/2; i++) {
                const star = document.createElement("p")
                star.innerHTML = "&#9733";
                star.classList.add("checked")
                examinationStarDiv.appendChild(star)
            }
            if (score.score.raw/2 < 5) {
                for (let i = 0; i < 5 - score.score.raw/2; i++) {
                    const star = document.createElement("p")
                    star.innerHTML = "&#9734";
                    examinationStarDiv.appendChild(star)
                }
            }
            const medal = document.querySelector(".examination-medal-div")
            const medalImg = document.querySelector(".examination-medal")
            const medalText = medal.querySelector(".medal-text")
            console.log(medal)
            if (score.score.raw/2 === 5) {
                ///gold
                medalText.textContent = "Guld"
                medal.style.background = "gold";
                medalImg.src = "/images/Kottemedbådetummarupp.png";
            } else if (score.score.raw  > 3) {
                ///silver
                medalText.textContent = "Silver"
                medal.style.background = "silver";
                medalImg.src = "/images/Kottemedbådetummarupp.png";
            } else if (score.score.raw =3){
                ///bronze
                medalText.textContent = "Brons"
                medal.style.background = "#cd7f32"
                medalImg.src = "/images/Kottemedbådetummarupp.png";
            }
            medalImg.src = "/images/Kottemedbådetummarupp.png";
        }
        else {
            for (let i = 0; i < 5; i++) {
                const star = document.createElement("p")
                star.innerHTML = "&#9734";
                examinationStarDiv.appendChild(star)
            }
        }
    }
    else {
        for (let i = 0; i < 5; i++) {
            const star = document.createElement("p")
            star.innerHTML = "&#9734";
            examinationStarDiv.appendChild(star)
        }
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