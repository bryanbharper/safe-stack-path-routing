module Client.Components.Navbar

open Client.Urls
open Feliz
open Feliz.Bulma

let render =
    Bulma.navbar [
        navbar.isFixedTop
        prop.children [
            Bulma.navbarBrand.div [
                Bulma.navbarItem.a [
                    prop.href ""
                    prop.children [
                        Html.img [ prop.src "favicon.png" ]
                    ]
                ]
            ]
            Bulma.navbarMenu [
                Bulma.navbarEnd.div [
                    Bulma.navbarItem.a [
                        prop.text Url.About.asString
                        prop.href Url.About.asString
                    ]
                    Bulma.navbarItem.a [
                        prop.text Url.Blog.asString
                        prop.href Url.Blog.asString
                    ]
                    Bulma.navbarItem.a [
                        prop.text Url.Todo.asString
                        prop.href Url.Todo.asString
                    ]
                ]
            ]
        ]
    ]