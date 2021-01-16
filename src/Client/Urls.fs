module Client.Urls

open Microsoft.FSharp.Reflection

type Url =
    | About
    | Blog
    | BlogEntry of string
    | Todo
    member this.asString =
        let (case, _) =
            FSharpValue.GetUnionFields(this, typeof<Url>)

        case.Name.ToLower()