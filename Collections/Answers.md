# Collections — Answers

> These answers are written in my own words after reading the documentation listed below.
> Replace each `TODO`.

## Task 2.1 — Research: IReadOnlyDictionary & SortedDictionary

**Sources I read (at least two):**
1. Microsoft Learn — [IReadOnlyDictionary<TKey, TValue> Interface](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2?view=net-10.0)
2. Microsoft Learn — [SortedDictionary<TKey, TValue> Class](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sorteddictionary-2?view=net-10.0)
3. Microsoft Learn — [Dictionary<TKey, TValue> Class](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-10.0)

**What is `IReadOnlyDictionary<TKey, TValue>`?**
`IReadOnlyDictionary<TKey, TValue>` is an interface for a collection of unique keys and their values. It lets code read values by key and enumerate the key/value pairs, but it does not expose methods for adding, removing, or replacing entries.

**How is it different from `Dictionary`, and why would a public method return one?**
`Dictionary<TKey, TValue>` is a concrete collection that allows entries to be added, changed, and removed. `IReadOnlyDictionary<TKey, TValue>` exposes only the read operations through its interface. A public method can return `IReadOnlyDictionary` when callers should inspect the data but should not modify it through the returned reference; it also avoids tying the public API to one specific dictionary implementation. This interface does not guarantee that the underlying data can never change through another reference.

**What is `SortedDictionary<TKey, TValue>`?**
`SortedDictionary<TKey, TValue>` is a key/value collection that keeps its entries sorted by key. It uses a sorted tree structure, and the order is determined by the key comparer.

**How does it differ from `Dictionary` in ordering and lookup speed?**
`Dictionary<TKey, TValue>` uses a hash table and provides lookup that is typically close to O(1), but it does not promise to enumerate entries in key-sorted order. `SortedDictionary<TKey, TValue>` always enumerates entries in sorted key order, while lookup takes O(log n) because it uses a tree structure. Therefore, a regular dictionary is generally faster when quick lookup is the main goal, while a sorted dictionary maintains order as entries are added or removed.

**When would I choose it?**
I would choose `SortedDictionary<TKey, TValue>` when I need to add and remove entries over time and always process or display them in key order, such as a timetable whose keys are session start times. If sorted order is not required, I would usually choose `Dictionary<TKey, TValue>` for faster lookups.

## Task 2.2 — Pick the Collection

Each type is the best answer for exactly one scenario: List, Dictionary, HashSet, IEnumerable, IReadOnlyDictionary, SortedDictionary.

| # | Scenario | Collection | Why (one sentence) |
|---|----------|------------|--------------------|
| S1 | Find a student by national ID — thousands of times a day. | `Dictionary` | A dictionary provides fast average-time lookup by national ID using the ID as the key. |
| S2 | Keep the tags of a course. The same tag must never be stored twice. | `HashSet` | A hash set stores unique values, so the same tag cannot be added more than once. |
| S3 | Keep a student's grades in the order they were entered. The same grade can appear more than once. | `List` | A list preserves insertion order and allows duplicate grades. |
| S4 | A public method returns the course price list. Callers can read prices but must not add or change any. | `IReadOnlyDictionary` | It lets callers look up and enumerate prices without exposing dictionary methods for adding, removing, or replacing entries. |
| S5 | A timetable keyed by session start time. Sessions are added at any moment, and it must always print in time order. | `SortedDictionary` | It keeps sessions ordered by their start-time keys as sessions are added or removed. |
| S6 | A method returns results that the caller only loops over once — and may stop early. | `IEnumerable` | It exposes a sequence that the caller can iterate through and stop consuming early without requiring a concrete collection type. |
