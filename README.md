# Project C# ASP.NET Core RESTful Web API
#### Author: Daniel Juarez

## Overview
The author has developed this project to provide technical interviewers, prior to a potential interview, with a sample of a fully functional [CRUD](https://en.wikipedia.org/wiki/Create,_read,_update_and_delete) C# ASP.NET Core RESTful web API for ticket services, in addition to [other services](#miscellaneous-service) described in the [user stories](#user-stories) section.

This implementation aims to demonstrate approaches to:
- Dependency Injection.
- Implementing Swagger for API documentation.
- Following C# code best practices.
- Structuring the project following clean architecture principles.
- Error handling using exceptions types.
- Unit testing using MS Unit Test and Moq.
- Information how to publish this project to Azure App Services.

During the interview, the author will be able to explain any aspect of these approaches.

This project has been implemented using `Visual Studio 2022 Community Edition version 17.12.3` and `Framework .NET 8` (long-term support). It utilizes an
"InMemoryDatabase" to store sample data, ensuring easy to follow and portability for interview purposes.

>The elaboration of this document has been divided into 4 parts:
>
> 1. **[Details](#details)**: Project goal and model definition.
>
> 2. **[User Stories](#user-stories)**: Elaboration of the expectations.
>
> 3. **[Code Implementation](#code-implementation)**: Provides an explanation how to execute the project code.
>
> 4. **[To publish this project to Azure](#to-publish-this-project-to-azure)**: Provides information how to deploy this project to Azure App Services.

## Details
The RESTful Web API source code solution implements:<br/>
* A Ticket List Service with the following Basic CRUD functionality:
    * Add a new "Ticket Item" using <span style="color:ghostwhite;background-color:green;border-radius:3px;padding:1px 3px">POST</span> method and an InputDTO model with AutoMapper extension.
    * Update an existing "Ticket Item" using <span style="color:ghostwhite;background-color:mediumseagreen;border-radius:3px;padding:1px 3px">PATCH</span> method and JsonPatchDocument extension.
    * Return a single "Ticket Item" using <span style="color:ghostwhite;background-color:dodgerblue;border-radius:3px;padding:1px 3px">GET</span> method.
    * Return a list of "Ticket Items" using <span style="color:ghostwhite;background-color:dodgerblue;border-radius:3px;padding:1px 3px">GET</span> method.
    * Remove a "Ticket Item" using <span style="color:ghostwhite;background-color:firebrick;border-radius:3px;padding:1px 3px">DELETE</span> method.

    * Ticket object definition:
        * Id
        * EventName
        * Description
        * EventDate (Date and Time)
        * TicketNumber (Computed class property based on EventDate and ticket Id)

* A Miscellaneous Service with the following features (endpoints):
    * Calculation of [Fibonacci Sequence](https://en.wikipedia.org/wiki/Fibonacci_sequence).- Calculates Fibonacci sequence numbers up to a specified maximum value passed as an argument.
    * Determine [Palindrome](https://en.wikipedia.org/wiki/Palindrome) Words.- Determine which words are palindromes when given a list of words. This endpoint should `only` allow words.

## User Stories
### Ticket Services:
* As a user, I want to call a service endpoint that returns a list of Ticket items.

    - Acceptance Criteria:
        -  Done when the GET method returns a list of Ticket Items in JSON format.
    - Execute in Postman or Swagger:
        ```
        Method: GET
        Url: http://localhost:[WebAPIport#]/api/tickets/getall

        Returned: BODY
            [
                {
                    "id": 1,
                    "eventName": "Event Name 01",
                    "description": "Description 01",
                    "eventDate": "2024-04-12T13:22:12Z",
                    "ticketNumber": "2024041200001"
                },
                {
                    "id": 2,
                    "title": "Event Name 02",
                    "description": "Description 02",
                    "eventDate": "2024-05-08T10:05:52Z",
                    "ticketNumber": "2024050800002"
                },
                {
                    "id": 3,
                    "title": "Event Name 03",
                    "description": "Description 03",
                    "eventDate": "2024-06-18T20:32:32Z",
                    "ticketNumber": "2024061800003"
                },
                {
                    "id": 4,
                    "title": "Event Name 04",
                    "description": "Description 04",
                    "eventDate": "2024-07-22T17:10:45Z",
                    "ticketNumber": "2024072200004"
                }
            ]

        Status Code: 200 Ok
        ```

* As a user, I want to call a service endpoint that returns the details of a single Ticket item.
    - Acceptance Criteria:
        - Done when a GET method returns a valid Ticket item ID, Event name, Description, Event Date and Ticket number in JSON format.
    - Execute in Postman or Swagger:
        ```
        Method: GET
        Url: http://localhost:[WebAPIport#]/api/tickets/2

        Returned: BODY
            {
                "id": 2,
                "eventName": "Event Name 02",
                "description": "Description 02",
                "eventDate": "2024-05-08T10:05:52Z",
                "ticketNumber": "2024050800002"
            }

        Status Code: 200 Ok
        ```
    - Acceptance Criteria:
        - Done when GET method for a Ticket item ID that doesn't exist returns an HTTP 404 status code.
    - Execute in Postman or Swagger:
        ```
        Method: GET
        Url: http://localhost:[WebAPIport#]/api/tickets/18

        Returned: BODY
            <Error Object>

        Status Code: 404 Not Found
        ```

* As user, I want to call a service endpoint that updates a single Ticket item.
    - Acceptance Criteria:
        - Done when PATCH method to an existing item updates the Event name and Description.
    - Execute in Postman or Swagger:
        ```
        Method: PATCH
        Url: http://localhost:[WebAPIport#]/api/tickets/1

        Send: BODY -> raw -> JSON (application/json)
            [
                {
                    "op": "replace", "path": "/EventName", "value": "Event10"
                },
                {
                    "op": "replace", "path": "/Description", "value": "Description10"
                }
            ]

        Returned: BODY
            {
                "id": 1,
                "eventName": "Event10",
                "description": "Description10",
                "eventDate": "2024-04-12T13:22:12Z",
                "ticketNumber": "2024041200001"
            }

        Status Code: 200 Ok
        ```
    - Acceptance Criteria:
        - Done when PATCH method to a non-existent Ticket Item returns an HTTP 415 status code.
    - Execute in Postman:
        ```
        Method: PATCH
        Url: http://localhost:[WebAPIport#]/api/tickets/2

        Send: BODY -> raw -> JSON (application/json)
            <Nothing>

        Returned: BODY
            <Error Object>

        Status Code: 415 Unsupported Media Type
        ```

* As a user, I want to call a service endpoint that creates a new Ticket Item.
    - Acceptance Criteria:
        - Done when a successful POST method that includes Event name, Description, Event Date and Ticket number returns HTTP 201 status.
        - Done when a successful POST method returns the created object.
    - Execute in Postman or Swagger:
        ```
        Method: POST
        Url: http://localhost:[WebAPIport#]/api/tickets

        Send: BODY -> raw -> JSON (application/json)
            {
                "eventName": "Event5",
                "description": "Description5",
                "eventDate": "2024-06-17T16:50:34Z"
            }

        Returned: BODY
            {
                "id": 5,
                "eventName": "Event5",
                "description": "Description5",
                "eventDate": "2024-06-17T16:50:34Z"
                "ticketNumber": "2024061700005"
            }

        Status Code: 201 Created
        ```
    - Acceptance Criteria:
        - Done when an unsuccessful POST method due to any reason returns an HTTP 400 status code.
    - Execute in Postman:
        ```
        Method: POST
        Url: http://localhost:[WebAPIport#]/api/tickets

        Send: BODY -> raw -> JSON (application/json)
            <Nothing>

        Returned: BODY
            <Error Object>

        Status Code: 400 Bad Request
       ```

* As a user, I want to call a service endpoint that removes a Ticket Item.
    - Acceptance Criteria:
        - Done when a successful DELETE method item returns HTTP 200 status.

    - Execute in Postman or Swagger:
        ```
        Method: DELETE
        Url: http://localhost:[WebAPIport#]/api/tickets/1

        Send: BODY -> raw -> JSON (application/json)
            <Nothing>

        Returned: BODY
            <Nothing>

        Status Code: 200 Ok
        ```
    - Acceptance Criteria:
        - Done when an unsuccessful DELETE method returns an HTTP 404 status code.
    - Execute in Postman or Swagger:
        ```
        Method: DELETE
        Url: http://localhost:[WebAPIport#]/api/tickets/10

        Send: BODY -> raw -> JSON (application/json)
            <Nothing>

        Returned: BODY
            <Error Object>

        Status Code: 404 Not Found
        ```
### Miscellaneous Service
* As a user, I want to call a service endpoint to calculate and return a list of Fibonacci sequence numbers up to a specified maximal value.
    - Acceptance Criteria:
        - Done when a list of Fibonacci sequence numbers are provided as a response and HTTP 200 status is returned.
    - Execute in Postman or Swagger:
        ```
        Method: GET
        Url: http://localhost:[WebAPIport#]/api/miscellaneous/getfibonaccisequence/90

        Send: BODY -> raw -> JSON (application/json)
            <Nothing>

        Returned: BODY
            [ 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 ]

        Status Code: 200 Ok
        ```
    - Acceptance Criteria:
        - Done when an unsupported argument is supplied, resulting in an HTTP 400 status code.
    - Execute in Postman or Swagger:
        ```
        Method: GET
        Url: http://localhost:[WebAPIport#]/api/miscellaneous/getfibonaccisequence/-1

        Send: BODY -> raw -> JSON (application/json)
            <Nothing>

        Returned: BODY
            <Error Object>

        Status Code: 400 Bad Request
        ```
* As a user, I want to call a service endpoint to identify which words from a provided list are Palindrome.
    - Acceptance Criteria:
        - Done when the service returns only the palindrome words from the provided list, if any found; otherwise, it should return an empty list with HTTP 200 status code.
    - Execute in Postman or Swagger:
        ```
        Method: POST
        Url: http://localhost:[WebAPIport#]/api/miscellaneous/getpalindromewords

        Send: BODY -> raw -> JSON (application/json)
            ['noon', 'statement', 'level']

        Returned: BODY
            ['noon', 'level']

        Status Code: 200 Ok
        ```
    - Acceptance Criteria:
        - Done when, if no word list is provided, the service will return an HTTP 400 status code with a brief description of the error. The response will also include a sample request body showing a valid list of three words, including two palindromes.
    - Execute in Postman or Swagger:
        ```
        Method: POST
        Url: http://localhost:[WebAPIport#]/api/miscellaneous/getpalindromewords

        Send: BODY -> raw -> JSON (application/json)
            <Nothing>

        Returned: BODY
            <Error Object>
            With description: "Value cannot be null. (Parameter 'words list is null or empty. Try with ['civic', 'type', 'radar'], will respond with ['civic', 'radar']')"

        Status Code: 400 Bad Request
        ```
    - Acceptance Criteria:
        - Done when, if the list of words includes a number, the service returns an HTTP 400 status code along with a brief description of the error. Additionally, the response should include a sample request body that demonstrates how to provide a valid list of 3 words, including 2 palindromes.
    - Execute in Postman or Swagger:
        ```
        Method: POST
        Url: http://localhost:[WebAPIport#]/api/miscellaneous/getpalindromewords

        Send: BODY -> raw -> JSON (application/json)
            [20, 'radar']

        Returned: BODY
            <Error Object>
            With description: "words list contain at least an element that is not allowed. Try with ['civic', 'type', 'radar'], will respond with ['civic', 'radar']"

        Status Code: 400 Bad Request
        ```
    - Acceptance Criteria:
        - Done when, if the list of words includes any word that is one character long, the service will return an HTTP 400 status code with a brief error description. The response will also include a sample request body showing a valid list of three words, including two palindromes.
    - Execute in Postman or Swagger:
        ```
        Method: POST
        Url: http://localhost:[WebAPIport#]/api/miscellaneous/getpalindromewords

        Send: BODY -> raw -> JSON (application/json)
            ['civic', 'w']

        Returned: BODY
            <Error Object>
            With description: "words list contain at least an element that is not allowed. Try with ['civic', 'type', 'radar'], will respond with ['civic', 'radar']"

        Status Code: 400 Bad Request
        ```
## Code Implementation
This project has been implemented using Visual Studio 2022 Community Edition. If you don't have installed in your system, you can visit the [Microsoft Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) to download a free copy.

This solution is organized into two projects following clean architecture principles, ensuring effective separation of concerns. This design supports easier maintenance, scalability, and unit testing, allowing for modifications without affecting the entire system:

>- **RESTfulNetCoreWebAPI-TicketList** - The RESTful Web API project.
>- **RESTfulNetCoreWebAPI-TicketList.Tests.MSTest** - Unit Test for the RESTful Web API project.

### To open and run the project from Visual Studio 2022: <br/>
This project requires the .NET 8.0 SDK. Please verify its installation by running the following command in your preferred terminal:
```
dotnet --list-sdks
```
Output similar to the following example appears:
```
6.0.317 [C:\Program Files\dotnet\sdk]
7.0.401 [C:\Program Files\dotnet\sdk]
8.0.204 [C:\Program Files\dotnet\sdk]
8.0.300 [C:\Program Files\dotnet\sdk]
```
Ensure that a version that starts with `8` is listed. If none is listed or the command isn't found, [install the most recent .NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download).


- Open Visual Studio 2022.
- Choose 'Open a Project or Solution'.
- Choose folder where the application has been unzipped or git cloned, and choose: `RESTfulNetCoreWebAPI-TicketList.sln`
- Press `F5` or click on run button <<span style="color:green;border-radius:3px;padding:2px 2px 1px 1px">▶</span> http> at the top of VS 2022 IDE to run the application in debug mode: the Swagger document will open, displaying all available services:
    ![](./screenshots/screenshot-01.JPG)

### Or to run the project from the Command Prompt:
To execute the project from the command prompt from Visual Studio IDE:
- Tools (at the top menu from the IDE) -> Command Line -> Developer Command Prompt:
    ```
    **********************************************************************
    ** Visual Studio 2022 Developer Command Prompt v17.10.2
    ** Copyright (c) 2022 Microsoft Corporation
    **********************************************************************

    C:\<YourDrivePath>\RESTfulNetCoreWebAPI-TicketList>
    ```
- From the command prompt type: `cd RESTfulNetCoreWebAPI-TicketList` < Enter >
- From the command prompt type: `dotnet run` < Enter >
    ```
    C:\<YourDrivePath>\cSharp-RestAPI-NetCore-TicketList>cd RESTfulNetCoreWebAPI-TicketList ⤶
    C:\<YourDrivePath>\cSharp-RestAPI-NetCore-TicketList\RESTfulNetCoreWebAPI-TicketList>dotnet run ⤶
    ```
- Will display the following Command Line Console:
    ```
    Building...
    info: Microsoft.EntityFrameworkCore.Update[30100]
          Saved 4 entities to in-memory store.
    info: Microsoft.Hosting.Lifetime[14]
          Now listening on: http://localhost:[WebAPIport#]
    info: Microsoft.Hosting.Lifetime[0]
          Application started. Press Ctrl+C to shut down.
    info: Microsoft.Hosting.Lifetime[0]
          Hosting environment: Development
    info: Microsoft.Hosting.Lifetime[0]
          Content root path: C:\<YourDrivePath>\cSharp-RestAPI-NetCore-TicketList\RESTfulNetCoreWebAPI-TicketList
    ```
- The Swagger document will not be available during this execution; however, the web API service will remain operational and can be accessed using Postman or your preferred API client:

    ![](./screenshots/screenshot-02.JPG)

## To publish this project to Azure
Once you have verified that the ASP.NET Core RESTful Web API is running and working properly:
- You are now ready to [publish the web API to Azure App Service](https://learn.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-api-management-using-vs?view=aspnetcore-9.0#publish-the-web-api-to-azure-app-service).
- You may need to [create a new Azure API Management instance by using the Azure portal](https://learn.microsoft.com/en-us/azure/api-management/get-started-create-service-instance).
- You may need to [import and publish your first API](https://learn.microsoft.com/en-us/azure/api-management/import-and-publish).
- You may need to [create and publish a product](https://learn.microsoft.com/en-us/azure/api-management/api-management-howto-add-products?tabs=azure-portal&pivots=interactive).
- Finally, you may need to [authenticate your API and connector with Microsoft Entra ID](https://learn.microsoft.com/en-us/connectors/custom-connectors/azure-active-directory-authentication).

<br/>
<br/>
Thank you.
