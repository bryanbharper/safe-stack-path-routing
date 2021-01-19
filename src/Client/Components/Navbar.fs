module Client.Components.Navbar

open Client.Urls
open Feliz
open Feliz.Bulma

let render (msg: Url -> 'msg) (dispatch: 'msg -> unit) =
    Bulma.navbar [
        navbar.isFixedTop
        prop.children [
            Bulma.navbarBrand.div [
                Bulma.navbarItem.a [
                    prop.onClick (fun _ -> Url.Blog |> msg |> dispatch)
                    prop.children [
                        Html.img [ prop.src "favicon.png" ]
                    ]
                ]
            ]
            Bulma.navbarMenu [
                Bulma.navbarEnd.div [
                    Bulma.navbarItem.a [
                        prop.text Url.About.asString
                        prop.onClick (fun _ -> Url.About |> msg |> dispatch)
                    ]
                    Bulma.navbarItem.a [
                        prop.text Url.Blog.asString
                        prop.onClick (fun _ -> Url.Blog |> msg |> dispatch)
                    ]
                    Bulma.navbarItem.a [
                        prop.text Url.Todo.asString
                        prop.onClick (fun _ -> Url.Todo |> msg |> dispatch)
                    ]
                ]
            ]
        ]
    ]