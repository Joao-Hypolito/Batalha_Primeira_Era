🛡️ Technical Summary: The Base Class

The `Character` class is an abstract class that serves as the definitive blueprint for creating any hero, minion, or creature within the *Batalha Primeira Era* universe. It implements the `IDamageable` interface, ensuring that any derived character inherently acquires the mechanical capability to receive damage and seamlessly interact within the combat flow.

---

## 🗂️ Core Architecture & Structural Management

### 1. Attributes and Data Shielding (Encapsulation)
The class encapsulates the character's core state data and gameplay statistics. Through the use of the `Math.Clamp` method, attribute values are constrained by predefined domain rules, ensuring they remain within valid boundaries (minimum of 0 and maximum of 99) to prevent overflow conditions.

| Attribute | Description |
| :--- | :--- |
| **LifePoints & Armor** | Manages character survivability, health state, and physical damage mitigation calculations. |
| **Primary Stats** | Strength, Dexterity, and Knowledge, which serve as the foundational parameters for gameplay mechanics. |
| **Spectral Insight** | Special stat governing the ability to perceive the Spectral Realm. Verification is performed at runtime via a dedicated boolean method. |

> [!NOTE]
> **Spectral Threshold:** Perception of the Spectral Realm is automatically enabled when the `SpectralInsight` attribute reaches a threshold value of **50 or greater**.

---

### 2. Combat Mechanics & Methods

* **`TakeAction(IDamageable target)`**
    Executes the offensive action workflow. This includes target selection, hit location determination, damage computation, and weapon durability validation. It also applies degradation penalties that reduce damage output when durability thresholds are reached (e.g., when a weapon is broken).

* **`ReceiveDamage(float damage, BodyPart hitPart)`**
    Processes incoming damage events. It multiplies the base damage according to the specific body part hit, reduces the result based on a formula using 50% of the character's armor value, and ensures the final damage is never negative before updating the health pool.

## 🗂️ 2. Bosses

### Spectrum
This class inherits from `Character` and represents a specific boss archetype within the game. Its defining mechanic is the ability to mentally fracture the player-controlled character if their cognitive defenses are insufficient prior to the encounter.

### `DefendAgainstAttacker`
This method processes a `Character` object (designated as target) to evaluate its `SpectralInsight` attribute.

| Condition | Outcome |
| :--- | :--- |
| **Insight below 40** | The entity shatters the protagonist's resolve, instantly reducing their current weapon's durability to zero and rendering it ineffective for the remainder of the encounter. |
| **Insight of 40 or higher** | The protagonist successfully mitigates the mental assault, granting complete immunity to this specific status degradation. |

> [!IMPORTANT]
> **Returns:** `bool` (indicates whether the defensive check was successfully passed).

---

### Dragon
The `Dragon` class represents one of the primary and most formidable bosses within the *Batalha Primeira Era* universe. It inherits directly from the core `Character` class, expanding its mechanical complexity and threat level in combat.

| Mechanic / System | Description |
| :--- | :--- |
| **I. Targetable Body Parts**<br>`GetTargetTableParts` | The class overrides the base target table system to introduce localized damage mechanics. In addition to standard character hitboxes, players can strategically target a Dragon's specific vulnerabilities:<br>• **`BodyPart.Wings`**: Allows players to target the wings (potentially affecting mobility or airborne actions).<br>• **`BodyPart.Belly`**: Exposes the traditional weak point of ancient drakes for high-risk, high-reward tactical strikes. |
| **II. Boss Scaling**<br>`LifeMultiplier` | To reflect its status as an apex predator of the First Age, the class features a dynamic health-scaling method. |

> [!NOTE]
> **Multiplication Factor:** Instantly scales the Dragon's current health pool by **10x** (`lifePoints *= 10`). This ensures a massive, multi-phased challenge during encounter transitions.

 ## 🗂️ Heroes

### OOP Hybrid Framework
The hero classes inherit directly from `Character`, serving as the foundational player-controlled archetypes in *Batalha Primeira Era*. This subclass architecture demonstrates a robust implementation of Object-Oriented Programming (OOP) principles in C# to achieve dynamic gameplay behavior.

| Principle | Core Implementation |
| :--- | :--- |
| **Inheritance** | Extends the abstract base `Character` to automatically absorb global attributes and core combat contracts. |
| **Interfaces with Generics** | Enforces specialized, type-safe behaviors tailored to specific hero attributes and class roles. |
| **Polymorphism** | Achieved through **Constructor Overloading** and method overrides, allowing distinct customization for each hero type. |

---

## 🗂️ Weapons

### Weaponry Logic & Attribute Scaling
This class encapsulates the specific attributes and behaviors of weaponry for each hero class, enhancing both code readability and overall system maintainability. It centralizes damage output calculations and equipment wear parameters.

| Method / System | Description |
| :--- | :--- |
| **`CalculateDamage`** | Computes the final strike value and dynamically manages weapon degradation during combat sequences. |
| **Attribute Scaling** | Incorporates a multi-attribute scaling matrix (**Strength**, **Dexterity**, and **Knowledge**) to multiply the wielder's core stats by unique weapon scaling factors. |

> [!NOTE]
> **Dynamic Maintainability:** This centralized mathematical logic ensures that combat calculations remain consistent, balanced, and easily scalable as new hero classes, requirement thresholds, or specialized equipment types are introduced.

---

## 🗂️ Inventory

### Decoupled Storage Controller
The `Inventory` class was introduced to completely decouple item management and equipment logic from the core character classes. Instead of overloading the `Character` class with asset lists, the inventory acts as a dedicated standalone manager.

| System Feature | Management Domain |
| :--- | :--- |
| **Encapsulation** | Enforces hard storage boundaries, maximum capacity caps, and strict item state tracking. |
| **State Tracking** | Dynamically monitors item durability, equipment status, and active item properties. |

> [!IMPORTANT]
> **Architecture Advantage:** The main architectural benefit of this implementation is its clean, zero-overhead interaction within the main game loop, avoiding data clutter inside the character instances.

