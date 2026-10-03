1- Global variables : any function can access and modify them , which can lead to unexpected behavior and make debugging difficult.
2- Edit in array + counter: using global variables to keep track of state can lead to issues with concurrency and make it hard to reason about the code.
3- No validation on price and stock: if I enter a negative price or stock value, the program may behave incorrectly or crash. Input validation is essential to ensure data integrity.
4- 3 functions with same logic (findXIndexById) : any new object will require a new function to be written, which violates the DRY (Don't Repeat Yourself) principle. This can lead to code duplication and maintenance challenges.
5- input reading mixed with logic (runInteractiveMenu) : use it only on console app.
6- calculateOrderTotal function can directly access the product array instead of passing it as a parameter, which can lead to tight coupling and make the function less reusable. It would be better to pass the necessary data as parameters to improve modularity and testability.

