# Part 02 — answers

---

## Reports

- What was the problem?
  `CsvReportExporter`, `JsonReportExporter` and `TextReportExporter` were three copies of the same class. The steps `Export` runs (Load → Validate → Format → Save), their order, and the code of `Load`, `Validate` and `Save` were copy-pasted. Only `Format` was really different. A change to the shared flow (for example, a new step or a different validation rule) had to be made in three places, and it was easy to miss one.
- What did you change?
  I added an abstract class `ReportExporter` (Template Method pattern). Its non-virtual `Export(path)` is the template method: it is the only place where the steps and their order are written. `Load`, `Validate` and `Save` are shared `protected virtual` methods with the default implementation, and `Format` is `protected abstract`. `CsvReportExporter`, `JsonReportExporter` and `TextReportExporter` now inherit from it and contain only their own `Format`. `Program.cs` is unchanged, and the three report files are byte-identical to before (checked by comparing MD5 hashes of the files before and after).
- Why did you choose that approach? Why an abstract class and not an interface?
  An interface can only describe *what* a type can do. It cannot hold the shared code, and (before default interface methods) not even a method body. With an interface, each exporter would still have to implement `Load`, `Validate`, `Save` and the order of the steps itself, so the duplication would remain. An abstract class lets me put the algorithm skeleton and the shared steps in one base class, and force subclasses to implement only the step that differs (`abstract Format`). Because `Export` is not virtual, a subclass cannot change the order of the steps or skip validation, which an interface cannot guarantee either. An abstract class also fits the relationship here: the three exporters really are "kinds of" report exporter that share state-free common behavior, and they have only one base class to inherit, so the single-inheritance limit is not a problem. An interface would be the right choice if I only needed a contract for callers (for example, `IReportExporter` for dependency injection), and it can still be added on top of the abstract class.

---

## Enrollment

- What was the problem?
  To enroll one student, `Program.cs` had to create four services (`PaymentGateway`, `SeatInventory`, `InvoiceGenerator`, `EmailService`), know the correct order of the calls (charge → reserve → create invoice → send email), and pass the invoice id from one call to the next. This knowledge belonged to the enrollment process, not to the caller. Any other place that enrolled a student would have to copy these steps, and could easily get the order wrong.
- What did you change?
  I added `EnrollmentFacade` with one method, `Enroll(studentId, courseId, amount)`. It owns the four services and runs them in the right order. `Program.cs` now only calls `new EnrollmentFacade().Enroll(studentId, courseId, amount)` and no longer creates or calls any of the four services directly. `Services.cs` is unchanged and the console output is identical to before (verified by comparing the output before and after).
- Why did you choose that approach? What does the caller no longer need to know?
  The Facade pattern gives a simple, single entry point to a group of classes that work together. The caller no longer needs to know which services exist, how to create them, in which order to call them, or that the invoice id returned by `InvoiceGenerator` is needed for the confirmation email. If the process changes (for example, a fifth step is added), only the facade changes and the callers stay the same. The facade also has a constructor that accepts the four services, so they can be replaced with fakes in tests.
