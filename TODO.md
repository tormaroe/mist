#TODO

Wishlist, in prioritized order:

* Extract Function interface. Rename Function class to BuiltInFunction. Clean it up (use of scope vs. environment).
* Dictionary literal {key val key val}. Also dictionary form. Add DictionaryExpression. Should extend Function as well!
* Meta dictionary on Expressions. Add forms to manipulate
* Doc form (gets doc string from meta). Add doc possibilities to def.
* inspect form. Add virtual Inspect method to Expression.
* .net interop: static functions (when resolve miss, try reflection (but not when creating closures!))
* .net interop: new form, ObjectExpression, extends Function.
* Create missing built in forms: list first rest print printf * / % cons time quote
* Better exceptions on parse errors
* Keywords (?)
* defmacro
* Create missing macros: and or cond defun
* Tail-call optimization
* Map | filter | Reduce (built in or base lib?)
* Base lib: zero? empty? inc dec ....
