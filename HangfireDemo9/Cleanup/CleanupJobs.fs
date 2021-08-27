namespace HangfireDemo9.Cleanup

open System
open System.ComponentModel
open Hangfire
open Hangfire.Server
open Hangfire.Dashboard.Management.v2.Support
open Hangfire.Dashboard.Management.v2.Metadata

[<ManagementPage("cleanup", "Cleanup Jobs")>] // Important: Title must not be ""!
type CleanupJobs() =
    interface IJob

    [<DisplayName("DeleteOldData")>]
    [<AutomaticRetry(Attempts = 0)>]
    [<DisableConcurrentExecution(90)>]
    member this.DeleteOldData() =
        printfn "Cleanup started! (Demo Only)"

    [<DisplayName("Parameter Demo")>]
    [<Description("only for demonstration")>]
    member this.ParameterDemo(context:PerformContext, 
          [<DisplayData("Input Text", "Enter something...")>]
              t:string,
          [<DisplayData("Input Bool")>]
              b:bool,
          [<DisplayData("Input Date", "Choose a date")>]
              d:DateTime) =
        printfn "ParameterDemo started!"
        printfn "Input Text: %s" t
        printfn "Input Bool: %A" b
        printfn "Input Date: %A" d



