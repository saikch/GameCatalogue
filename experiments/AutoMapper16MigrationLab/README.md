# AutoMapper 16.2.0 Migration Lab

This is a **representative** .NET 8 experiment based on the AutoMapper patterns observed in OperationsPortal. It does not contain LifeLabs source code.

It validates two approaches:

1. **Minimal migration** — retain manual `MapperConfiguration` and supply an `ILoggerFactory`.
2. **Centralized/future-safe design** — put mappings in a `Profile`, register AutoMapper once through DI, and inject/reuse `IMapper`.

Representative mappings tested:

- `Comment.Id -> CommentDto.CommentId`
- `Comment.UserId -> CommentDto.EditedUserGuid`
- `QueryRule -> QueryRuleModel`

The program calls `AssertConfigurationIsValid()`, verifies mapping behavior, verifies DI reuse of `IMapper`, and prints a small illustrative timing comparison between reusing one mapper and repeatedly constructing `MapperConfiguration`.

The timing output is **not a production benchmark**. The actual OperationsPortal solution still needs its own restore/build/test validation before any refactor is accepted.
