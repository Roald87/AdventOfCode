open System.IO

module utils =

    let split (sep: string) (str: string) = str.Split(sep)

    let trim (str: string) = str.Trim()

    let readAllLines fname = File.ReadAllLines(fname)
