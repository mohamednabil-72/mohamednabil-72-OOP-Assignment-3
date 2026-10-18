# Part 03 — answers

---

## BlockedUsers

Setup used: 50,000 blocked ids, 5,000 requests, `found=5000` both before and after. Times were measured by running the project in Release mode (several runs; `MeasureMs` reports whole milliseconds, so the exact numbers vary slightly between machines and runs).

- Time complexity before: O(n · m), where n = number of blocked ids (50,000) and m = number of requests (5,000). `List<int>.Contains` scans the list (O(n)) and it is called once for every request.
- Time (ms) before: about 22 ms (22–23 ms over three runs), `found=5000`.
- What did you change?
  The blocked ids are now stored in a `HashSet<int>` instead of a `List<int>` (`BuildBlockedIds` returns a `HashSet<int>`, and `CountBlocked` and `MeasureMs` take one). `HashSet.Contains` uses a hash lookup, which is O(1) on average. The logic of `CountBlocked` is the same, so the result does not change: `found` is still 5000.
- Time complexity after: O(m), with m requests and O(1) average per lookup (building the set is a one-time O(n) cost done outside the measured method). If the set had to be built inside the method, it would be O(n + m), which is still far better than O(n · m).
- Time (ms) after: about 1 ms (1–2 ms over three runs), `found=5000`.

---

## Students

- What was the problem?
  `GetAllStudents()` built a `List<Student>` with all 1,000,000 students before returning. `Program.cs` reads only the first 3 and then breaks out of the loop, but by then the program had already created 1,000,000 `Student` objects and 1,000,000 name strings and stored them in memory. That is a lot of wasted time and memory for 3 results.
- What did you change?
  `GetAllStudents()` now returns `IEnumerable<Student>` and uses `yield return` (lazy evaluation). A student is created only when the caller asks for the next one, so when `Program.cs` stops after 3 students, only 3 are created and the other 999,997 never exist. `Program.cs` did not need to change and still prints students 1–3 and `printed=3`.
