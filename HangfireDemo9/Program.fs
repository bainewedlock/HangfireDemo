open System
open HangfireDemo9
open Microsoft.Owin.Hosting
open Topshelf

let mutable app:IDisposable = null

let start (hc: HostControl) =
    let url = "http://localhost:2259"
    app <- WebApp.Start<MyOwinStartup> url
    printfn "Running on %s" url
    true

let stop (hc: HostControl) =
    printfn "Stopping"
    app.Dispose()
    true

[<EntryPoint>]
let main argv =
    defaultService
    |> serviceName "MyHangfireService"
    |> withStart start
    |> withStop stop
    |> withRecovery (defaultServiceRecovery |> restart (Time.min 10))
    |> run
