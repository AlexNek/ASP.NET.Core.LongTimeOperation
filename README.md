# ASP.NET.Core.LongTimeOperation
ASP.NET Core 3.0 - long time operation with progress bar Demo

Some time ago I want to have progress bar for long time operation into Web application.
As I primary did it for Desktop applications before it was not a trivial task. After some recearching I have found that the best way is to using SignalR with AJAX.
In addition, there is a diffirences between SignalR Core 3.0 implementation and previous SignalR versions. So some sample can't be used at all as they was wriiten for previous version.

I decided to share my invenstigation in this Demo application.

# Steps to create the Demo application
Complete description you can found [here](https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr?view=aspnetcore-3.1&tabs=visual-studio). I have copied here important steps only.

## Add the SignalR client library to existing application

* In **Solution Explorer**, right-click the project, and select **Add** > **Client-Side Library**.

* In the **Add Client-Side Library** dialog, for **Provider** select **unpkg**.

* For **Library**, enter _@microsoft/signalr@latest_.

* Select **Choose specific files**, expand the _dist/browser_ folder, and select _signalr.js_ and _signalr.min.js_.

* Set **Target Location** to _wwwroot/js/signalr/_, and select Install.


