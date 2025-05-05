
export function sendStatement(verb, verbSwe, userResult, chapterId, chapterName, episodeId, episodeName, passed) {
    const uName = "admin";
    const uEmail = "huan2300@student.miun.se";


    const config = {
        "endpoint": "https://seceducation-hugand.lrs.io/xapi/",
        "auth": "Basic " + btoa("herabr:dudfoh")
    }
    ADL.XAPIWrapper.changeConfig(config)

    const statement = {
        "actor": {
            "name": uName,
            "mbox": "mailto:" + uEmail
        },
        "verb": {
            "id": "http://adlnet.gov/expapi/verbs/" + verb,
            "display": { "sv-SE": verbSwe }
        },
        "object": {
            "id": "https://localhost:7142/Test/Result/" + chapterId + "/" + episodeId, //Inkluderar båda id:n i objectets id
            "objectType": "Activity",// Se till att objektet definieras som en aktivitet
            "definition": {
                "name": {"sv-SE": chapterName + " - " + episodeName}, // Kombinera kapitel och episodenamn
                "extensions": {
                    "https://localhost:7142/extensions/chapterId": chapterId,
                    "https://localhost:7142/extensions/episodeId": episodeId
                }
            }   
        },
        "result": {
                "score": {
                    "min": 0,
                    "max": 5,
                    "raw": userResult,
                    "scaled": userResult / 5
                },
                "success": passed
            }
        }
    
    const res = ADL.XAPIWrapper.sendStatement(statement);
    console.log(res)
}
