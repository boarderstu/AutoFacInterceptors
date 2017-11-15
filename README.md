# AutoFacInterceptors
Show and tell code to demo how Autofac.Extra.DynamicProxy can be used to implement a AOP which can reduce commonly used code.

The example used in this process shows how we can implement an `Cacheable` attribute which can then be attached to methods to *tag* them as Cacheable.

Coupled with code that will prior to method calls, we can implement a write-once (test once __!!__) solution which all other methods can hook into. The previous approach would be to litter all the methods with caching code.
