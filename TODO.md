#TODO

Main areas remaining:
* Tail call optimization - do LOOP instead
* Macros
* tick reader macro
* CLI interop
* Mistpacker
* Web site, documentation project
.. then more datatypes, misc missing functions

Wishlist, in prioritized order:

* Stream processiong functions with f argument: all, any, distinct (with comparer), selectmany, takewhile, first (with function)
* Other list functions: contains, count, alamantat, last, max, min, reverse, skip (drop), take, append
* TEST THAT PRECONDITIONS ARE USED!!
* Add extension methods to all kind of objects to make it easier to create expressions from anything...
* Reflection used when loading special forms is cleaner than for built in functions
* Dictionary literal {key val key val}. Also dictionary form. Add DictionaryExpression. Should extend Function as well!
* Meta dictionary on Expressions. Add forms to manipulate
* Doc form (gets doc string from meta). Add doc possibilities to def.
* inspect form. Add virtual Inspect method to Expression.
* .net interop: static functions (when resolve miss, try reflection (but not when creating closures!))
* .net interop: new form, ObjectExpression, extends Function.
* Script as argument to repl (evaluate load)
* Mistpacker
* Create missing built in forms: eval list first rest print printf > < * / % cons time quote
* Better exceptions on parse errors
* Keywords (?)
* defmacro
* Create missing macros: and or cond defun
* Tail-call optimization
* Map | filter | Reduce (built in or base lib?)
* Base lib: zero? empty? inc dec >= <= ....
* Generate doc from docstrings (add separate doc generator project)
* Inner defs are causing some problems!!
