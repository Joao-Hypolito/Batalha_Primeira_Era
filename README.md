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

### 2. Combat Operations & Main Methods

* **`TakeAction(IDamageable target)`**
  Manages the character's attack sequence from start to finish. It selects the intended target, identifies the body area being attacked, determines the final damage value, and verifies the weapon's durability condition. If the weapon reaches critical durability levels, damage penalties are introduced, lowering its combat efficiency.

* **`ReceiveDamage(float damage, BodyPart hitPart)`**
  Handles damage inflicted upon the character. The initial damage is modified based on the affected body part and subsequently mitigated through an armor calculation that considers 50% of the character's armor value. Before applying the result to the health pool, the system ensures that the calculated damage cannot fall below zero.


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
This class extends ``Character``. Its primary mechanics center on brief invincibility and an enraged state activated once its health falls under a designated threshold.

### `Imortality`
This method manages the Lamenter's signature death-prevention mechanics. When fatal or near-fatal damage is received, rather than dying, it triggers a 5-second duration. During this window, a boolean flag is set to negate all incoming damage, boost attack stats (triggering a frenzied state), and initiate a timer. After the countdown ends, the method automatically executes the character's death sequence.

## 🗂️ Weapons

### Weapon Mechanics & Attribute-Based Scaling

This system implements the Lamenter's special survival mechanic. When incoming damage would reduce its health to a critical level, the character avoids immediate death and enters a temporary 5-second survival state. During this interval, a boolean flag prevents additional damage, while its offensive attributes are increased, putting the character into a frenzy state. After the countdown reaches zero, the system proceeds with the character's death sequence.

| Method / System | Description |
| :--- | :--- |
| **`CalculateDamage`** | Determines the resulting damage dealt by an attack while also handling weapon durability loss throughout combat. |
| **`Attribute Scaling`** | Uses a multi-stat scaling system involving **Strength**, **Dexterity**, and **Knowledge**, applying each wielder attribute according to the individual scaling coefficients defined by the weapon. |

### `CalculateAttributeBonus`

Applies soft caps through a diminishing returns curve to promote balanced stat distribution:

| Attribute Points | Percentage | Diminishing returns |
| :--- | :--- | :--- | 
| **1–30 points:** | 100% attribute scaling efficiency | Full growth |
| **31–60 points:** | 50% attribute scaling efficiency | Moderate soft cap |
| **61+ points:** | 15% attribute scaling efficiency | Heavy soft cap |


> [!NOTE]
> **Dynamic Maintainability:** This centralized mathematical logic ensures that combat calculations remain consistent, balanced, and easily scalable as new hero classes, requirement thresholds, or specialized equipment types are introduced.

## 🗂️ Inventory

### Dedicated Inventory Module

The `Inventory` class separates item storage and equipment-related functionality from the main character system. Rather than placing these responsibilities directly within the `Character` class, it operates as an independent component focused on managing, organizing, and maintaining the player's inventory.

| System Feature | Purpose |
| :--- | :--- |
| **Encapsulated Storage** | Preserves inventory integrity by enforcing capacity restrictions, validating stored items, and maintaining controlled access to inventory data. |
| **Item State Management** | Keeps track of equipment assignments, item durability, and the current condition of every usable object throughout gameplay. |

> [!IMPORTANT]
> **Architectural Benefit:** Separating inventory responsibilities from the character model improves maintainability, reduces class coupling, and keeps the core gameplay loop free from unnecessary inventory-related complexity.

> [!IMPORTANT]
> > [!IMPORTANT]
> **Architectural Benefit:** This architecture isolates inventory management from the core character logic, reducing object complexity, improving maintainability, and ensuring efficient integration with the main gameplay loop.