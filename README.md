![logo](https://github.com/maurictg/Overstag/blob/master/Overstag/wwwroot/img/logo.png)
# Overstag


## Overstag website API

Call the login function and check response using the WebClient class
```c#
var url = "http://localhost:5000/Register/postLogin";
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
You can also do this using JQuery:
```js
$.post("/Register/postLogin", { Username: 'test', Password: 'test123' }, function (response) {
    if (response.status == 'success') {
      //login is valid    
    }
    else {
      //status = error  
    }
 }, 'json'
 );
 //Response looks like: {'status':'success'} or {'status':'error','error':'Exception blah blah'}  
```
In the future, the api will be made better
