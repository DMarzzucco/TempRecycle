# Console.App.NET

> [!IMPORTANT]
> This project is still in development


This standalone project is a console application that analyzes the TEMP folder, generating a detailed log of the files and folders present, along with their total size. Additionally, it offers the option to delete all detected files, excluding those in use by running applications. However, these files in use are also recorded for your reference.

## Requirements

- .NET 8

## License

Made by Dario Marzzucco.

dotnet publish -c Release -r win-x64 --self-contained true -o ./publish
