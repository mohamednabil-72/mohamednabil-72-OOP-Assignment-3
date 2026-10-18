# Generics — Answers

> The compiler errors below were copied from real builds of this project.
> The explanations are left for you to write in your own words (the assignment asks for that).
> Replace each `TODO`.

## Step 2 — What is the same between StudentStore and CourseStore, and what is different?

**Same:**
TODO

**Different:**
TODO

## Step 3 — Why does `GetById` not compile in `Store<T>`?

Compiler error (from `Store.cs`, `if (item.Id == id)`):

```
error CS1061: 'T' does not contain a definition for 'Id' and no accessible extension method 'Id' accepting a first argument of type 'T' could be found (are you missing a using directive or an assembly reference?)
```

Why the compiler rejects it, in my own words:

TODO

## Step 7 — Why must `new Store<string>()` NOT compile?

Compiler error when the line is un-commented:

```
error CS0311: The type 'string' cannot be used as type parameter 'T' in the generic type or method 'Store<T>'. There is no implicit reference conversion from 'string' to 'GenericsApp.IHasId'.
```

Why, in my own words:

TODO

## Last question — search

What is the common name for the kind of class built in Steps 4 and 5?

TODO (write what you found and which sources you read)
