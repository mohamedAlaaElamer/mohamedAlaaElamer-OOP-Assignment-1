## Q1 — Why is a 20-parameter constructor a problem?

1. **Easy to mix up the order.** With 20 parameters, it is very easy to pass
   values in the wrong position, especially when calling the constructor
   from many places.

2. **The compiler can't catch it.** If two parameters have the same type
   (e.g. SubTotal and DiscountAmount are both `decimal`, or two address
   fields are both `string`), swapping them still compiles and runs.
   This becomes a silent bug that is only discovered later, when someone
   notices the totals or the address are wrong.

3. **Poor readability at the call site.** A call like
   `new Invoice("INV-1", "Mohamed", ..., null, null, 0m, ...)` does not
   explain itself. You can't tell which property a `null` or a `0m`
   belongs to without opening the constructor and counting parameters.
   Also, `0m` could mean "no discount", "tax is really zero", or
   "someone forgot to set it".

4. **Hard to extend.** Adding one more property (even an optional one)
   changes the constructor signature, which breaks every place in the
   code that creates an Invoice.