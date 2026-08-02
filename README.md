## 🛡️ Technical Overview: The Foundation Class

The `Character` class is an abstract base type that establishes the core structure for every hero, enemy, and creature in the *Batalha Primeira Era* universe. By implementing the `IDamageable` interface, all classes derived from it automatically gain the ability to receive damage and participate consistently in the game's combat system.

---

## 🗂️ Core Architecture & Structural Management

### 1. Attribute Control and Encapsulation

This component manages the character's core data and gameplay-related properties. Using the `Math.Clamp` method, every attribute is constrained to a fixed range (0–99), ensuring data consistency and preventing invalid or out-of-bounds values.

| Attribute | Description |
| :--- | :--- |
| **Health & Defense** | Represents the character's durability, current health status, and resistance against physical damage. |
| **Core Statistics** | Strength, Agility, and Knowledge, which define the character's primary gameplay capabilities and overall performance. |
| **Spectral Insight** | Specialized attribute used to determine if the character can perceive and engage with the Spectral Realm. This condition is validated through a dedicated boolean method. |


> [!NOTE]
> **Spectral Requirement:** Access to the Spectral Realm becomes available automatically whenever the `SpectralInsight` value is **50 or higher**.

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

The `Dragon` class represents one of the game's most formidable boss entities in the **Batalha Primeira Era** project. Built upon the `Character` base class, it extends the combat system with unique mechanics and significantly increases the encounter's difficulty.

| Mechanic / System | Description |
| :--- | :--- |
| **I. Selectable Weak Points**<br>`GetTargetTableParts` | This implementation replaces the default targeting behavior by supporting localized damage zones. Beyond the standard hit locations, players may attack specific areas of the Dragon's body:<br>• **`BodyPart.Wings`**: Targets the wings, potentially limiting flight capabilities and aerial maneuvers.<br>• **`BodyPart.Belly`**: Represents the Dragon's classic vulnerable area, rewarding players willing to take greater risks in close combat. |
| **II. Health Scaling**<br>`LifeMultiplier` | To emphasize the Dragon's role as one of the dominant creatures of the First Age, the class includes a method that greatly expands its maximum survivability during battle. |

> [!NOTE]
> **Health Multiplier:** Multiplies the Dragon's current health by **10** (`lifePoints *= 10`), creating a longer and more demanding encounter, particularly during boss phase transitions.

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

### Standalone Inventory System

The `Inventory` class is responsible for isolating inventory operations and equipment management from the primary character logic. Instead of embedding these responsibilities into the `Character` class, it functions as a separate module dedicated to organizing and maintaining the player's items.

| System Feature | Purpose |
| :--- | :--- |
| **Encapsulated Storage** | Preserves inventory integrity by enforcing capacity restrictions, validating stored items, and maintaining controlled access to inventory data. |
| **Item State Management** | Keeps track of equipment assignments, item durability, and the current condition of every usable object throughout gameplay. |

> [!IMPORTANT]
> **Architectural Benefit:** Separating inventory responsibilities from the character model improves maintainability, reduces class coupling, and keeps the core gameplay loop free from unnecessary inventory-related complexity.

> [!IMPORTANT]
> **Architectural Benefit:** This design promotes efficient interaction with the primary game loop while keeping character instances lightweight and preventing unnecessary data accumulation within the core classes.

