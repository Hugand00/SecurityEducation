const queryResult = sendQuery("completed");
console.log(queryResult)
sessionStorage.setItem("myXapiQuery", JSON.stringify(queryResult)); 
export function sendQuery(verb)//hämtar statements
{ 
    //const uName = "admin";//Behövs ej egentligen
    //const uEmail = "huan2300@student.miun.se";//ID för inloggade användare
    const uName = xApiConfig.user;//Behövs ej egentligen
    const uEmail = xApiConfig.userEmail;//ID för inloggade användare

    //const config = {
    //    "endpoint": "https://seceducation-hugand.lrs.io/xapi/",
    //    "auth": "Basic " + btoa("herabr:dudfoh")//Access key
    //}
    console.log(xApiConfig)
    ADL.XAPIWrapper.changeConfig(xApiConfig)
    const parameters = ADL.XAPIWrapper.searchParams();

    parameters["agent"] = JSON.stringify({ mbox: "mailto:" + uEmail });//Vem har gjort
    parameters["verb"] = "http://adlnet.gov/expapi/verbs/" + verb;//vad

    const queryData = ADL.XAPIWrapper.getStatements(parameters)

    return queryData;
    
}
