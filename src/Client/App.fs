module Client.App

open Client.Urls
open Elmish
open Elmish.React

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif


open Elmish.UrlParser
open Elmish.Navigation

let pageParser : Parser<Url->Url, Url> =
  oneOf
    [
      map Url.About (s Url.About.asString)
      map Url.Blog (s Url.Blog.asString)
      map Url.Blog top
      map Url.BlogEntry (s Url.Blog.asString </> str)
      map Url.Todo (s Url.Todo.asString)
      map Url.NotFound (s Url.NotFound.asString)
    ]

let urlUpdate (result:Option<Url>) model =
  match result with
  | Some url ->
      Index.initFromUrl url
  | None ->
      model, Navigation.newUrl Url.NotFound.asString


Program.mkProgram Index.init Index.update Index.render
|> Program.toNavigable (parsePath pageParser) urlUpdate // NOTE THIS LINE
#if DEBUG
|> Program.withConsoleTrace
#endif
|> Program.withReactSynchronous "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
