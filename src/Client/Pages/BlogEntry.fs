module Client.Pages.BlogEntry

type State =
    {
        Slug: string
    }

type Msg =
    | Placeholder


open Elmish

let init slug =
    { Slug = slug}, Cmd.none

let update msg state =
    match msg with
    | Placeholder -> state, Cmd.none


open Feliz
open Feliz.Bulma

let render state (dispatch: Msg -> unit) =
    state.Slug
    |> sprintf "Blog Post: %s"
    |> Html.h1
    |> List.singleton
    |> Bulma.section