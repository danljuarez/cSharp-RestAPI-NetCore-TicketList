[< Return to README](../README.md)

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
            Value cannot be null.

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
            Ticket was not found.

        Status Code: 404 Not Found
        ```
### Miscellaneous Service:
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
            With description: "words list contain at least one element that is not allowed. Try with ['civic', 'type', 'radar'], will respond with ['civic', 'radar']"

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
            With description: "words list contain at least one element that is not allowed. Try with ['civic', 'type', 'radar'], will respond with ['civic', 'radar']"

        Status Code: 400 Bad Request
        ```

---
[< Return to README](../README.md)