# Part 01 — Answers

---

## ShippingCostCalculator

- **What was the problem?**

  `ShippingCostCalculator` used a `switch` statement to calculate shipping costs based on carrier names. Adding a new carrier required modifying the existing class, violating the Open/Closed Principle (OCP).

- **What did you change?**

  Introduced the `IShippingCarrier` interface and implemented it in `AramexCarrier`, `FedExCarrier`, and `DhlCarrier`. Refactored `ShippingCostCalculator` to depend on the interface and delegate the calculation to the selected carrier. New carriers can now be added without modifying the calculator.

---

## OrderProcessor

- **What was the problem?**

  `OrderProcessor` directly instantiated `SqlOrderRepository` and `SmtpEmailSender`. This created tight coupling, made dependencies difficult to replace, and made unit testing harder.

- **What did you change?**

  Introduced the `IOrderRepository` and `IEmailSender` interfaces. Updated `SqlOrderRepository` and `SmtpEmailSender` to implement these interfaces, and used constructor injection in `OrderProcessor` to receive its dependencies. This applies the Dependency Inversion Principle (DIP).

---

## Notifications

- **What was the problem?**

  The original design used inheritance to combine notification channels, urgency, and scheduling. This led to duplicated code and a growing number of classes for different combinations, making the design difficult to extend.

- **What did you change?**

  Introduced the `INotificationChannel` interface and made notification channels independent components. Used composition to combine a channel with an `INotificationDelivery` strategy and optional `INotificationModifier` components. Implemented `ImmediateDelivery`, `ScheduledDelivery`, and `UrgentModifier` to support different combinations without creating a separate class for each combination.

---

## Proof

## Proof

- New carrier file(s): BostaCarrier.cs
- New notification channel file(s): WhatsAppNotification.cs
- Existing classes left unchanged? (yes/no): Yes, except Program.cs for wiring.