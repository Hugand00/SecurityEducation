const xapiData = JSON.parse(sessionStorage.getItem("myXapiQuery"));
console.log("Tidigare hämtad xAPI-data:", xapiData.statements);
showStoredResults()
function showStoredResults() {
    const episodeDivs = document.querySelectorAll(".episode-div");

    episodeDivs.forEach(div => {
        const episodeId = parseInt(div.getAttribute("data-episode-div"));
        const resultP = div.querySelector(".result-text");
        console.log(episodeId)
        const matchingStatement = xapiData?.statements.find(statement => {
            const extensionId = parseInt(statement.object?.definition?.extensions?.["https://localhost:7142/extensions/episodeId"]);
            console.log(extensionId)
            return extensionId === episodeId;
        });

        if (matchingStatement) {
            const score = matchingStatement.result?.score;
            resultP.textContent = `Senaste försöket: ${score.raw}/${score.max}`;
            console.log("inne")
        }
    });
}
