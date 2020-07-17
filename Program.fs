open System
open System.Text
open System.IO
open System.Net.Http
open System.Net.Mime

let runGame playerKey uriResult : Async<int> =
    async {
        let httpClient = new HttpClient()
        let _ = httpClient.BaseAddress <- uriResult
        try
            let requestContent = new StringContent(playerKey, Encoding.UTF8, MediaTypeNames.Text.Plain)
            let! (response : HttpResponseMessage) = httpClient.PostAsync("", requestContent) |> Async.AwaitTask
            try
                if not response.IsSuccessStatusCode then
                    printfn "Unexpected server response: %s" (String.Format("{0}", response))
                    return 2
                else
                    let! responseString = response.Content.ReadAsStringAsync() |> Async.AwaitTask in
                    printfn "Server response: %s" responseString
                    return 0
            finally
                response.Dispose()
        finally
            httpClient.Dispose()
    }


[<EntryPoint>]
let main argv =
    match Array.toList argv with
    | "contest" :: serverUrl :: playerKey :: _ ->
        begin
            printfn "ServerUrl: %s; PlayerKey: %s" serverUrl playerKey
            match Uri.TryCreate(serverUrl, UriKind.Absolute) with
            | true, uriResult -> Async.RunSynchronously(runGame playerKey uriResult)
            | _ ->
                printfn "Failed to parse ServerUrl"
                1
        end
    | _ ->
        printfn "tests/diagnostics/exercise"
        0 // return an integer exit code
