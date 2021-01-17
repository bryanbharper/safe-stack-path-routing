module Client.Urls

open Microsoft.FSharp.Reflection

type Url =
    | About
    | Blog
    | BlogEntry of string
    | Todo
    | NotFound
    member this.asString =
        let (case, _) =
            FSharpValue.GetUnionFields(this, typeof<Url>)

        case.Name.ToLower()

let fromString (s: string) =
    let caseInfo =
        FSharpType.GetUnionCases typeof<Url>
        |> Array.tryFind (fun case -> case.Name.ToLower() = s.ToLower())

    match caseInfo with
    | Some case ->
        match case.GetFields() with
        | [||] ->
            FSharpValue.MakeUnion(case, [||]) :?> Url |> Some
        | _ ->
            FSharpValue.MakeUnion(case,  [| "" |> box |] ) :?> Url |> Some
    | _ -> None
