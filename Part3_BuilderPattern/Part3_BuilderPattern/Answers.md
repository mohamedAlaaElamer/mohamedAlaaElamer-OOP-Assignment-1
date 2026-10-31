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

   ## Q2 — Is it only a "long constructor" problem?

No. The long constructor is a symptom of a deeper design problem:
the Invoice class has too many loosely related properties.

1. **Related data is scattered.** Street, City, State, ZipCode and Country
   belong together as one concept (an Address), but they are spread out as
   separate properties on the Invoice.

2. **Duplication.** The same five address fields are repeated twice
   (Billing and Shipping), only with a different prefix. Any change, like
   adding ApartmentNumber, must be made in two places, and it is easy to
   forget one of them. With an `Address` class, the change happens in one
   place, and billing and shipping always stay consistent.

3. **Wrong responsibility.** Rules like "Street must not be empty" belong
   to the Address, because it owns the address data. The Invoice should
   only know which address is billing and which is shipping, not how an
   address is validated.

The same idea applies to the order/payment fields: they form their own
group and could be modeled separately instead of sitting directly on Invoice.