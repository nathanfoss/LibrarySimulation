# LibrarySimulation
We provide a full-featured digital library management experiencefor our patrons and library staff. viewing our full catalog of books, 

## Business Requirements
### Patrons (As a patron, I would like to...)
- Register for a digital library card
- View the full catalog of books and their status (available, checked out, etc.)
- Search for a book by title or author
- View my current checked-out books and their due dates 
- Renew a book that I have reserved and is almost due
- Reserve an available book
- View my own borrowing records

### Library Staff (As a libarian, I would like to...)
- View the full catalog of books
- Search for a book by title, author, or ISBN
- Add/Remove books from the catalog
- View all patrons
- Search for a patron by name
- Approve/Deny new patron requests
- Remove an existing Patron
- View borrowing record for a specific book
- View overdue books

## Architecture
- Console Application Leveraging the Domain-Driven Design (DDD) Pattern.
- CQRS Powered by MediatR
- Automated testing provided by XUnit
- Quality gateways provided by husky commit hooks
- Event-Driven Architecture using background services
- In memory database by EF Core

### Modules
- Patron
- Book
- Borrowing Records
