module Client.Index

(*******************************************
*               TYPES
*******************************************)
open Client.Pages
open Client.Urls

type Page =
    | About
    | Blog of Blog.State
    | BlogEntry of BlogEntry.State
    | Todo of Todo.State
    | NotFound

type State = { CurrentPage: Page }

type Msg =
    | UrlChanged of Url
    | Blog of Blog.Msg
    | BlogEntry of BlogEntry.Msg
    | Todo of Todo.Msg


(*******************************************
*               INIT
*******************************************)
open Elmish

let initFromUrl url =
    match url with
    | Url.About -> { CurrentPage = About }, Cmd.none
    | Url.Blog ->
        let s, c = Blog.init ()
        { CurrentPage = Page.Blog s }, Cmd.map Msg.Blog c
    | Url.BlogEntry slug ->
        let s, c = BlogEntry.init slug
        { CurrentPage = Page.BlogEntry s }, Cmd.map Msg.BlogEntry c
    | Url.Todo ->
        let s, c = Todo.init ()
        { CurrentPage = Page.Todo s }, Cmd.map Msg.Todo c
    | Url.NotFound -> { CurrentPage = NotFound }, Cmd.none

let init (): State * Cmd<Msg> =
    let state, cmd = Blog.init ()
    { CurrentPage = Page.Blog state }, Cmd.map Msg.Blog cmd


(*******************************************
*               UPDATE
*******************************************)
let update (msg: Msg) (state: State): State * Cmd<Msg> =
    match msg, state.CurrentPage with
    | UrlChanged url, _ -> initFromUrl url
    | Msg.Blog (Blog.Msg.EntryClicked slug), _ ->
        let s, c = BlogEntry.init slug
        { CurrentPage = Page.BlogEntry s }, Cmd.map Msg.BlogEntry c
    | Msg.Blog msg', Page.Blog state' ->
        let s, c = Blog.update msg' state'
        { state with CurrentPage = Page.Blog s }, Cmd.map Msg.Blog c
    | Msg.BlogEntry msg', Page.BlogEntry state' ->
        let s, c = BlogEntry.update msg' state'

        { state with
              CurrentPage = Page.BlogEntry s },
        Cmd.map Msg.Todo c
    | Msg.Todo msg', Page.Todo state' ->
        let s, c = Todo.update msg' state'
        { state with CurrentPage = Page.Todo s }, Cmd.map Msg.Todo c
    | _ -> state, Cmd.none


(*******************************************
*               RENDER
*******************************************)
open Feliz
open Feliz.Bulma

let navbar dispatch =
    Bulma.navbar [
        navbar.isFixedTop
        prop.children [
            Bulma.navbarBrand.div [
                Bulma.navbarItem.a [
                    prop.onClick (fun _ -> Url.Blog |> Msg.UrlChanged |> dispatch)
                    prop.children [
                        Html.img [ prop.src "favicon.png" ]
                    ]
                ]
            ]
            Bulma.navbarMenu [
                Bulma.navbarEnd.div [
                    Bulma.navbarItem.a [
                        prop.text Url.About.asString
                        prop.onClick (fun _ -> Url.About |> Msg.UrlChanged |> dispatch)
                    ]
                    Bulma.navbarItem.a [
                        prop.text Url.Blog.asString
                        prop.onClick (fun _ -> Url.Blog |> Msg.UrlChanged |> dispatch)
                    ]
                    Bulma.navbarItem.a [
                        prop.text Url.Todo.asString
                        prop.onClick (fun _ -> Url.Todo |> Msg.UrlChanged |> dispatch)
                    ]
                ]
            ]
        ]
    ]

let render (state: State) (dispatch: Msg -> unit) =
    let activePage =
        match state.CurrentPage with
        | About -> About.render
        | Page.Blog state' -> Blog.render state' (Msg.Blog >> dispatch)
        | Page.BlogEntry state' -> BlogEntry.render state' (Msg.BlogEntry >> dispatch)
        | Page.Todo state' -> Todo.render state' (Msg.Todo >> dispatch)
        | Page.NotFound -> NotFound.render

    [ navbar dispatch; activePage ] |> Html.div
