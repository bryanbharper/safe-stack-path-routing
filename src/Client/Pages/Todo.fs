module Client.Pages.Todo

open Elmish
open Fable.Remoting.Client
open Shared

type State = { Todos: Todo list; Input: string }

type Msg =
    | GotTodos of Todo list
    | SetInput of string
    | AddTodo
    | AddedTodo of Todo

let todosApi =
    Remoting.createApi ()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.buildProxy<ITodosApi>

let init (): State * Cmd<Msg> =
    let state = { Todos = []; Input = "" }

    let cmd =
        Cmd.OfAsync.perform todosApi.getTodos () GotTodos

    state, cmd

let update (msg: Msg) (state: State): State * Cmd<Msg> =
    match msg with
    | GotTodos todos -> { state with Todos = todos }, Cmd.none
    | SetInput value -> { state with Input = value }, Cmd.none
    | AddTodo ->
        let todo = Todo.create state.Input

        let cmd =
            Cmd.OfAsync.perform todosApi.addTodo todo AddedTodo

        { state with Input = "" }, cmd
    | AddedTodo todo ->
        { state with
              Todos = state.Todos @ [ todo ] },
        Cmd.none

open Fable.React
open Fulma
open Feliz
open Feliz.Bulma

let containerBox (state: State) (dispatch: Msg -> unit) =
    Box.box' [] [
        Content.content [] [
            Content.Ol.ol [] [
                for todo in state.Todos do
                    li [] [ str todo.Description ]
            ]
        ]
        Field.div [ Field.IsGrouped ] [
            Control.p [ Control.IsExpanded ] [
                Input.text [
                    Input.Value state.Input
                    Input.Placeholder "What needs to be done?"
                    Input.OnChange(fun x -> SetInput x.Value |> dispatch)
                ]
            ]
            Control.p [] [
                Button.a [ Button.Color IsPrimary
                           Button.Disabled(Todo.isValid state.Input |> not)
                           Button.OnClick(fun _ -> dispatch AddTodo) ] [
                    str "Add"
                ]
            ]
        ]
    ]

let todoList todos =
    Bulma.box [
        todos
        |> List.map (fun t -> Html.li t.Description)
        |> Html.ol
    ]

let render (state: State) (dispatch: Msg -> unit) =
    Container.container [] [
        Column.column [ Column.Width(Screen.All, Column.Is6)
                        Column.Offset(Screen.All, Column.Is3) ] [
            Heading.p [ Heading.Modifiers [
                            Modifier.TextAlignment(Screen.All, TextAlignment.Centered)
                        ] ] [
                str "pathNav"
            ]
            containerBox state dispatch
        ]
    ]
