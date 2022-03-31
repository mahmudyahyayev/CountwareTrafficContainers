# Cross-platform .NET sample microservices and container based application that runs on Linux Windows and macOS. Powered by .NET 5, Docker Containers. Supports Visual Studio

## Give a Star! :star:

If you like this project, learn something or you are using it in your applications, please give it a star. Thanks!

## Description
todo

## IMPORTANT NOTES!
**You can use either the latest version of Visual Studio or simply Docker CLI and .NET CLI for Windows, Mac and Linux**.

### Internal architecture and design of the microservices
> The microservices are different in type, meaning different internal architecture pattern approaches depending on its purpose, as shown in the image below.
<p>
<img src="img/CountwareContainer_Types_Of_Microservices.png">
<p>
<p>

## CQRS
Read Model - Varies according to microservices
 1. Executing IQuarable queries by  Entity Framework Core (using Entity Framework Core) 
 2.  By running elastic queries (using Nest Elastic)

Write Model - Domain Driven Design approach (using Entity Framework Core).

Commands/Queries/Domain Events handling using Convey library.
