![logo](https://github.com/maurictg/Overstag/blob/master/Overstag/wwwroot/img/logo.png)
# Overstag


## Overstag website API

Call the login function and check response using the WebClient class
```c#
var url = "https://stoverstag.nl/Register/postLogin";
var request = (HttpWebRequest)WebRequest.Create(url);

var postData = "Username=" + "test" + "&Password=" + "password";
var data = System.Text.Encoding.ASCII.GetBytes(postData);

request.Method = "POST";
request.ContentType = "application/x-www-form-urlencoded";
request.ContentLength = data.Length;

using (var stream = request.GetRequestStream()) { stream.Write(data, 0, data.Length); }

var response = (HttpWebResponse)request.GetResponse();
var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
//Response string looks like: {"status":"success"} or {"status":"error","error":"Exception blah blah"}
//You can parse it to JSON using the Newtonsoft.JSON (JSON.net) package from nuget
```
In this way, you can call the Register controller from an external application to validate a login
In the future, the api will be improved.
