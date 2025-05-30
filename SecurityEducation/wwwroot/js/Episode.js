const xapiData = JSON.parse(sessionStorage.getItem("myXapiQuery"));
console.log("Tidigare hämtad xAPI-data:", xapiData.statements);
showStoredResults()
function showStoredResults() {
    const episodeDivs = document.querySelectorAll(".episode-div");

    episodeDivs.forEach(div => {
        const episodeId = parseInt(div.getAttribute("data-episode-div"));
        const resultP = div.querySelector(".result-text");
        const episodeComplete = div.querySelector(".completed")
        console.log(episodeId)
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
          
            if (score.success === true) {
                episodeComplete.textContent = "Avklarad";
            }
            else {
                episodeComplete.textContent = "Inte avklarad"
                episodeComplete.style.background = "red"
            }
            resultP.textContent = `Bästa försöket: ${score.score.raw}/${score.score.max}`;
            console.log("inne")
        }
        else {
            episodeComplete.textContent = "Inte avklarad"
            episodeComplete.style.background = "red"
            resultP.textContent = "Bästa försöket: Saknas"
        }
    });
}
