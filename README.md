🛡️ Technical Summary: The Base Class

The `Character` class is an abstract class that serves as the definitive blueprint for creating any hero, minion, or creature within the *Batalha Primeira Era* universe. It implements the `IDamageable` interface, ensuring that any derived character inherently acquires the mechanical capability to receive damage and seamlessly interact within the combat flow.

---

 ## 🗂️ Core Architecture & Structural Management

### 1. Attributes and Data Shielding (Encapsulation)
This class is responsible for safeguarding the character's essential state information and gameplay-related statistics. By leveraging the Math.Clamp method, all attribute values are restricted to a predefined range, ensuring they remain within acceptable limits (from 0 to 99) and preventing invalid or excessive values.

| Attribute | Description |
| :--- | :--- |
| **LifePoints & Armor** | Handles the character's durability, current health condition, and reduction of incoming physical damage. |
| **Primary Stats** | Strength, Agility, and Knowledge, which act as the primary drivers for game mechanics and character performance. |
| **Spectral Insight** | Unique attribute that determines whether the character can detect and interact with the Spectral Realm. Its status is evaluated through a dedicated boolean validation method. |

> [!NOTE]
> **Spectral Threshold:** Perception of the Spectral Realm is automatically enabled when the `SpectralInsight` attribute reaches a threshold value of **50 or greater**.

---

### 2. Combat Mechanics & Methods

* **`TakeAction(IDamageable target)`**
    Executes the offensive action workflow. This includes target selection, hit location determination, damage computation, and weapon durability validation. It also applies degradation penalties that reduce damage output when durability thresholds are reached (e.g., when a weapon is broken).

* **`ReceiveDamage(float damage, BodyPart hitPart)`**
    Processes incoming damage events. It multiplies the base damage according to the specific body part hit, reduces the result based on a formula using 50% of the character's armor value, and ensures the final damage is never negative before updating the health pool.

 ## 🗂️ Bosses

### 1. Spectrum

Derived from the `Character` base class, this boss embodies a unique enemy archetype. Its signature ability is to psychologically overwhelm the player whenever the character lacks sufficient mental resilience before the confrontation.

### `DefendAgainstAttacker`

This function receives a `Character` instance (used as the target) and inspects its `SpectralInsight` value.

| Condition | Result |
| :--- | :--- |
| **Spectral Insight below 40** | The boss destroys the protagonist's mental stability, immediately reducing the durability of the equipped weapon to zero, making it unusable for the rest of the encounter. |
| **Spectral Insight of 40 or greater** | The protagonist withstands the psychic attack and becomes fully protected against this particular durability-degrading effect. |

> [!IMPORTANT]
> **Returns:** `bool` (specifies whether the defensive validation succeeded).

---

### 2. Dragon
The `Dragon` class defines one of the most powerful and iconic boss encounters within the Batalha Primeira Era framework. Inheriting directly from the base Character class, it introduces advanced combat mechanics and elevated threat dynamics.

| Mechanic / System | Description |
| :--- | :--- |
| **I. Targetable Body Parts**<br>`GetTargetTableParts` | The class overrides the base target table system to introduce localized damage mechanics. In addition to standard character hitboxes, players can strategically target a Dragon's specific vulnerabilities:<br>• **`BodyPart.Wings`**: Allows players to target the wings (potentially affecting mobility or airborne actions).<br>• **`BodyPart.Belly`**: Exposes the traditional weak point of ancient drakes for high-risk, high-reward tactical strikes. |
| **II. Boss Scaling**<br>`LifeMultiplier` | To reflect its status as an apex predator of the First Age, the class features a dynamic health-scaling method. |

> [!NOTE]
> **Multiplication Factor:** Instantly scales the Dragon's current health pool by **10x** (`lifePoints *= 10`). This ensures a massive, multi-phased challenge during encounter transitions.

 ## 🗂️ Heroes

### OOP Hybrid Framework
Hero classes derive directly from `Character`, establishing the foundational player-controlled archetypes within Batalha Primeira Era. This subclass design showcases a solid application of Object-Oriented Programming (OOP) tenets in C# to drive dynamic gameplay mechanics.

| Principle | Core Implementation |
| :--- | :--- |
| **Inheritance** | Extends the abstract base `Character` to automatically absorb global attributes and core combat contracts. |
| **Interfaces with Generics** | Enforces specialized, type-safe behaviors tailored to specific hero attributes and class roles. |
| **Polymorphism** | Achieved through **Constructor Overloading** and method overrides, allowing distinct customization for each hero type. |

 ## 🗂️ Enemies

### 1. Goblin 
This class inherits from `Character` and represents a pricipal enemy archetype within the game. It features a  dynamic **Horde mechanic**, where Goblins again bonus damage depending on how many other Goblins are 
present in the battle (the larger the horde, the stronger they get).

### `ReceiveDamage(float damage, BodyPart hitPart)`
This method overrides the base damage logic to calculate the final damage taken based on the specific `BodyPart` hit. Additionally, it tracks the `_myHorde` attribute: if the Goblin's life points drop to zero or below, it is automatically removed from the horde, dynamically lowering the group's overall morale and strength.

### 2. Lamenters
This class inherits from ``Character``. Its core mechanics revolve around temporary invulnerability and a frenzied state triggered when its health points drop below a critical threshold.

### `Imortality`
This method handles the Lamenter's unique death-defiance logic. When incoming damage reduces its health to or near zero, instead od dying, this method triggers a 5-seconds window. During this time, ir activates a boolean flag to ignore  all further damage, increases its attack parameters (entering a frenzy state), and starts a countdown. Once the timer expires, the method forces the character into the death sequence.

 ## 🗂️ Weapons

### Weaponry Logic & Attribute Scaling
This method handles the Lamenter's unique death-defiance logic. When incoming damage reduces its health to or near zero, instead of dying, this method triggers a 5-second window. During this period, it activates a boolean flag to ignore all further damage, boosts its attack parameters (entering a frenzy state), and starts a countdown. Once the timer expires, the method forces the character into the death sequence.

| Method / System | Description |
| :--- | :--- |
| **`CalculateDamage`** | Computes the final strike value and dynamically manages weapon degradation during combat sequences. |
| **Attribute Scaling** | Incorporates a multi-attribute scaling matrix (**Strength**, **Dexterity**, and **Knowledge**) to multiply the wielder's core stats by unique weapon scaling factors. |

> [!NOTE]
> **Dynamic Maintainability:** This centralized mathematical logic ensures that combat calculations remain consistent, balanced, and easily scalable as new hero classes, requirement thresholds, or specialized equipment types are introduced.

## 🗂️ Inventory

### Independent Inventory Manager

The `Inventory` class was designed to fully separate item handling and equipment responsibilities from the core character implementation. Rather than storing inventory-related data directly inside the `Character` class, this component serves as an autonomous manager dedicated to asset organization.

| System Feature | Responsibility |
| :--- | :--- |
| **Data Encapsulation** | Maintains strict inventory boundaries, enforces storage capacity limits, and guarantees consistent item lifecycle management. |
| **Runtime State Monitoring** | Continuously manages item durability, equipment assignment, and the current state of usable objects. |

> [!IMPORTANT]
> **Architectural Benefit:** This design promotes efficient interaction with the primary game loop while keeping character instances lightweight and preventing unnecessary data accumulation within the core classes.

