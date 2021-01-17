module Client.Components.Navbar

open Client.Urls
open Feliz
open Feliz.Bulma
open Feliz.Router

let render =
    Bulma.navbar [
        navbar.isFixedTop
        prop.children [
            Bulma.navbarBrand.div [
                Bulma.navbarItem.a [
                    "" |> Router.format |> prop.href

                    prop.children [
                        Html.img [ prop.src "favicon.png" ]
                    ]
                ]
            ]
            Bulma.navbarMenu [
                Bulma.navbarEnd.div [
                    Bulma.navbarItem.a [
                        prop.text Url.About.asString

                        Url.About.asString |> Router.format |> prop.href
                    ]
                    Bulma.navbarItem.a [
                        prop.text Url.Blog.asString

                        Url.Blog.asString |> Router.format |> prop.href
                    ]
                    Bulma.navbarItem.a [
                        prop.text Url.Todo.asString

                        Url.Todo.asString |> Router.format |> prop.href
                    ]
                ]
            ]
        ]
    ]