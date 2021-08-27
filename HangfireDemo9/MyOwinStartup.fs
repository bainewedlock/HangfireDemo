namespace HangfireDemo9

open Owin
open System
open Hangfire
open Hangfire.SqlServer
open HangfireDemo9.Cleanup
open Hangfire.Dashboard.Management.v2

type MyOwinStartup() =
    member this.Configuration(app: IAppBuilder) =
        let cs = [
            "Server=(localdb)\\mssqllocaldb"
            "Database=HangfireDemo9"
            "Integrated Security=True" ] |> String.concat "; "
        let opt =
            SqlServerStorageOptions (
                CommandBatchMaxTimeout = TimeSpan.FromMinutes 5.,
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes 5.,
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true)
        GlobalConfiguration.Configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(cs, opt)
            .UseManagementPages(typeof<CleanupJobs>.Assembly)
            |> ignore
        app.UseHangfireDashboard("",
            new DashboardOptions(
                DisplayStorageConnectionString = false,
                DashboardTitle = "My Dashboard",
                StatsPollingInterval = 5000)) |> ignore
        app.UseHangfireServer() |> ignore
