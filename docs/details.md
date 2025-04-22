[< Return to README](../README.md)

## Details
The RESTful Web API Source Code Solution implements two primary services:<br/>
### Ticket List Service
* **CRUD Operations**:
    * **Create**: Add a new "Ticket Item" using <span style="color:ghostwhite;background-color:green;border-radius:3px;padding:1px 3px">POST</span> method with InputDTO model and AutoMapper extension.
    * **Update**: Update an existing "Ticket Item" using <span style="color:ghostwhite;background-color:mediumseagreen;border-radius:3px;padding:1px 3px">PATCH</span> method with JsonPatchDocument extension.
    * **Read**: Return a single "Ticket Item" using <span style="color:ghostwhite;background-color:dodgerblue;border-radius:3px;padding:1px 3px">GET</span> method.
    * **Read (List)**: Return a list of "Ticket Items" using <span style="color:ghostwhite;background-color:dodgerblue;border-radius:3px;padding:1px 3px">GET</span> method.
    * **Delete**: Remove a "Ticket Item" using <span style="color:ghostwhite;background-color:firebrick;border-radius:3px;padding:1px 3px">DELETE</span> method.

* **Ticket object definition**:
    * `Id`
    * `EventName`
    * `Description`
    * `EventDate` (Date and Time)
    * `TicketNumber` (Computed class property based on `EventDate` and ticket `Id`)

### Miscellaneous Service
* **Endpoints:**
    * [Fibonacci Sequence](https://en.wikipedia.org/wiki/Fibonacci_sequence): Calculate Fibonacci sequence numbers up to a specified maximum value.
    * [Palindrome](https://en.wikipedia.org/wiki/Palindrome) Words: Determine which words are palindromes from a given list of words.

---
[< Return to README](../README.md)