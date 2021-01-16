module Client.Pages.Blog

type Entry =
    {
        Slug: string
        Title: string
    }

type State =
    {
        Entries: Entry list
    }

type Msg =
    | EntryClicked of string
    | PlaceHolder


open Elmish

let init () =
    let entries =
        [
            { Title = "I Like Turtles"; Slug = "turtles-so-cool" }
            { Title = "I'd Rather Be Eating"; Slug = "food-yum" }
            { Title = "The Perils of Having a Mind"; Slug = "mind-bad" }
        ]
    { Entries = entries}, Cmd.none

let update msg state =
    match msg with
    | EntryClicked _ -> state, Cmd.none // Index renders entry
    | PlaceHolder -> state, Cmd.none


open Feliz
open Feliz.Bulma

let renderEntry dispatch entry: ReactElement =
    Html.a [
        prop.onClick (fun _ -> entry.Slug |> Msg.EntryClicked |> dispatch)
        entry.Title |> prop.text
    ]

let render state (dispatch: Msg -> unit) =
    state.Entries
    |> List.map (renderEntry dispatch)
    |> List.map Html.li
    |> Html.ul
    |> List.singleton
    |> Bulma.section