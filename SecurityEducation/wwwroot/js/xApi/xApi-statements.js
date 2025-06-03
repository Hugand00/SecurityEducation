
export function sendStatement(verb, verbSwe, userResult, chapterId, chapterName, episodeId, episodeName, passed) {
    const uName = "admin"; //Displaynamn
    const uEmail = "huan2300@student.miun.se";//Mail till inloggade användare = unikt id


    const config = {
        "endpoint": "https://seceducation-hugand.lrs.io/xapi/", //Skapa egen Lrs hos Veracity
        "auth": "Basic " + btoa("herabr:dudfoh") //Access key
    }
    ADL.XAPIWrapper.changeConfig(config)


    const statement = {
        "actor": {
            "name": uName, //Displaynamn
            "mbox": "mailto:" + uEmail //Vem har gjort det?
        },
        "verb": {
            "id": "http://adlnet.gov/expapi/verbs/" + verb, //Vad har gjorts
            "display": { "sv-SE": verbSwe }
        },
        "object": {
            "id": "https://localhost:7142/Test/Result/" + chapterId + "/" + episodeId, //Vilket object har det gjorts på
            "objectType": "Activity",
            "definition": {
                "name": {"sv-SE": chapterName + " - " + episodeName}, 
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

export function sendFinalExamStatement(verb, verbSwe, userResult, passed) {
    //const uName = "admin";
    //const uEmail = "huan2300@student.miun.se";
    const uName = xApiConfig.user;//Behövs ej egentligen
    const uEmail = xApiConfig.userEmail;//ID för inloggade användare
    //const config = {
    //    "endpoint": "https://seceducation-hugand.lrs.io/xapi/",
    //    "auth": "Basic " + btoa("herabr:dudfoh")
    //};
    ADL.XAPIWrapper.changeConfig(xApiConfig);

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
            "id": "https://localhost:7142/Test/ExaminationResult", 
            "objectType": "Activity",
            "definition": {
                "name": { "sv-SE": "Slutprov" },
                "description": { "sv-SE": "Avslutande test för hela kursen" }
            }
        },
        "result": {
            "score": {
                "min": 0,
                "max": 10, 
                "raw": userResult,
                "scaled": userResult / 10
            },
            "success": passed
        }
    };

    const res = ADL.XAPIWrapper.sendStatement(statement);
}
