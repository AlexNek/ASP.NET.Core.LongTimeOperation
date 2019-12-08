# ASP.NET.Core.LongTimeOperation
ASP.NET Core 3.0 - long time operation with progress bar Demo

Some time ago I want to have progress bar for long time operation into Web application.
As I primary did it for Desktop applications before it was not a trivial task. After some researching I have found that the best way is to using SignalR with AJAX.
In addition, there is a differences between SignalR Core 3.0 implementation and previous SignalR versions. So some sample can't be used at all as they was wriiten for previous version.

I decided to share my investigation in this Demo application.

# Steps to create the Demo application
Complete description you can found [here](https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr?view=aspnetcore-3.1&tabs=visual-studio). I have copied here important steps only.

## Add the SignalR client library to existing application

* In **Solution Explorer**, right-click the project, and select **Add** > **Client-Side Library**.

* In the **Add Client-Side Library** dialog, for **Provider** select **unpkg**.

* For **Library**, enter _@microsoft/signalr@latest_.

* Select **Choose specific files**, expand the _dist/browser_ folder, and select _signalr.js_ and _signalr.min.js_.

* Set **Target Location** to _wwwroot/js/signalr/_, and select Install.

## Create a SignalR hub

A hub is a class that serves as a high-level pipeline that handles client-server communication. We will call hub member functions from Java script and give back notification to Java script

* In the Demo project folder, create a Hubs folder.

 * In the Hubs folder, create a LongOperationHub class:
   * It must be inherited from **Hub** class 
   * It must have the constructor with parameter _IHubContext<LongOperationHub> hubContext_. Normally Hub is short live time object, with hub context we could use it longer.
   * It could have some member functions. We have main function _Start_ and test function _SendMessage_. In Addition we could use _OnConnectedAsync_ and _OnDisconnectedAsync_.

## Configure SignalR

The SignalR server must be configured to pass SignalR requests to SignalR.

* Add the some code to the Startup.cs file:
   * to the function _public void ConfigureServices(IServiceCollection services)_ add line _services.AddSignalR();__
   * to the function _public void Configure(IApplicationBuilder app, IWebHostEnvironment env)_ to the block `_app.UseEndpoints(endpoints =>_` add _endpoints.MapHub\<LongOperationHub>("/longOperationHub");_
   
```C#
 app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapHub<LongOperationHub>("/longOperationHub");
                    });
   ```
   

## Add SignalR client code

 * Add to the your page for starting long operation (in our demo it is `\Views\Home\Start.cshtml`) the next code:
 
```HTML
<button onclick="StartProcess(1)" type="button" id="startButton" class="btn btn-primary btn-danger">Start the process</button>
<a asp-action="Index">Back to List</a>
```

```JavaScript
<script src="~/js/signalr/dist/browser/signalr.js"></script>
<script src="~/js/longoperation_support.js"></script>
```
 
Where is _longoperation_support.js_ could be the next code:

```JavaScript
"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/longOperationHub")
    .build();
//alert(connection);
connection.on("ReceiveMessage",
    (user, message) => {
        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = `${user} says ${msg}`;
        alert(encodedMsg);
    });

connection.on("ReportProgress",
    (message, percentage) => {
        ProgressBarModal("show", message + ":" + percentage + "%");
        $('#ProgressMessage').width(percentage + "%");
    });

connection.on("ReportFinish",
    (message, percentage) => {
        ProgressBarModal();
        location.href = "Index";
    });

function StartProcess(parameter) {
    //alert("Start");

    connection.start()
        .then(function() {
            console.log('Now connected, connection ID=' + connection.id);
            connection.invoke('start', parameter);
        })
        .catch(error =>
            console.error('Could not connect', error)
        );
}
```
Where is `connection.on` subscription to callback functions and `connection.invoke` called function defined in hub class.
Error console will be visible into web browser. 

*Note*
>Press F12 for calling debug console in Firefox
***
**Not finished, first commit**
