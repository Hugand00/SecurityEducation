const queryResult = sendQuery("completed");
console.log(queryResult)
sessionStorage.setItem("myXapiQuery", JSON.stringify(queryResult)); 
function sendQuery(verb)
{ 
    const uName = "admin";
    const uEmail = "huan2300@student.miun.se";


    const config = {
        "endpoint": "https://seceducation-hugand.lrs.io/xapi/",
        "auth": "Basic " + btoa("herabr:dudfoh")
    }
    ADL.XAPIWrapper.changeConfig(config)
    const parameters = ADL.XAPIWrapper.searchParams();

    parameters["agent"] = JSON.stringify({ mbox: "mailto:" + uEmail });
    parameters["verb"] = "http://adlnet.gov/expapi/verbs/" + verb;

    const queryData = ADL.XAPIWrapper.getStatements(parameters)

    return queryData;
    
}
