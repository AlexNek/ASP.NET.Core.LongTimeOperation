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

 * In the Hubs folder, create a LongOperationHub class into LongOperationHub.cs:
   * It must be inherited from **Hub** class 
   * It must have the constructor with parameter _IHubContext<LongOperationHub> hubContext_. Normally Hub is short live time object, with hub context we could use it longer.
   * It could have some member functions. We have main function _Start_ and test function _SendMessage_. In Addition we could use _OnConnectedAsync_ and _OnDisconnectedAsync_.

***
**Not finished, first commit**
