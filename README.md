# ⚔️ Batalha Primeira Era — Core Architecture

## 🛡️ Technical Overview: Composition over Inheritance & Creational Patterns

The core architecture of *Batalha Primeira Era* is built around a unified **Character** model supported by **Composition, Component-based Design, and Creational Design Patterns (Builder)**. Instead of depending on rigid inheritance structures or monster constructors with excessive parameter overloads, characters are dynamically and fluently constructed through data definitions (`HeroClass`), interface contracts (`IDamageable`, `IAbility`), modular behaviors (`Immortality`, `Horde`), and a **Fluent Builder API**.

---

## 🗂️ Core Architecture & Structural Components

### 1. Centralized Character Model & Fluent Builder Pattern

The `Character` class implements `IDamageable` and serves as the main entity for player characters, bosses, and standard enemies. 

To eliminate constructor complexity and handle optional attributes gracefully, `Character` uses the **Builder Pattern** via a **Fluent Interface** (`WithLife`, `WithArmor`, `WithAbilities`, etc.). Through internal encapsulation and `Math.Clamp`, attributes remain restricted to a controlled range (0–99), preserving data consistency and preventing invalid state mutations.

#### 🔨 Character Construction via Builder (Fluent Method Chaining)

```csharp
// Example of fluent character creation using the Builder pattern
var hero = new Character("Aragorn", warriorClass)
    .WithLife(100f)
    .WithArmor(50f)
    .WithStrenght(80)
    .WithDextery(65)
    .WithKnowledge(40)
    .WithSpectral(55)
    .WithEquippedWeapon(andurilSword)
    .WithAbilities(powerStrike, shieldBash);
```    

---

### 2. Dynamic Class System (`HeroClass`)

Encapsulates class rules and restrictions into data-driven definitions, avoiding hardcoded user roles.

* **Weapon Restrictions:** Validates if a target `WeaponType` is present in the character's `HeroClass.AllowedWeapons` list before equipping it via `EquipWeapon(Weapon weapon)`.

---

### 3. Combat Operations & Target Selection

**`TakeAction(IDamageable target)`**

  Manages the character's offensive workflow. The method checks the character's current health state, validates the durability of the equipped weapon, selects a specific body part through `GetTargetTableParts()`, and forwards the attack to the weapon's damage calculation system. When no weapon is equipped, it calculates base unarmed damage using the character's `Strength` attribute.

**`ReceiveDamage(float damage, BodyPart hitPart)`**

  Handles incoming attacks by applying a non-linear armor mitigation formula:

  $$\text{Damage Multiplier} = \frac{100}{100 + \frac{\text{Armor}}{2}}$$

  The method additionally applies special damage modifiers based on the affected body part, such as `Belly` x3.0 and `Head` x2.0. Before applying lethal damage, it also verifies whether any active survival mechanics can prevent the character from being defeated.

---
## 🧩 Modular Behaviors & Gameplay Mechanics

Instead of changing or overriding the main methods inherited from the base class, specific behaviors are assigned to `Character` objects using independent, reusable components.

### 1. Immortality Behavior (`ImmortalityBehavior`)

* **Survival Trigger:** If incoming damage is expected to bring a character's health down to a critical level (`expectedLife <= 1%`), the `ImmortalityBehavior` is triggered. It keeps the character alive with `1 HP` and provides temporary protection against additional damage (for example, 5 seconds).

### 2. Horde Integration (`MyHorde`)

* **Automatic Group Coordination:** Characters belonging to a `Horde` instance automatically notify the group when they are defeated (`lifePoint <= 0`). This makes it possible to trigger shared mechanics, such as soul absorption or morale penalties.

### 3. Body-Part Targeting (`GetTargetTableParts`)

* Enables a polymorphic targeting system that can be expanded to handle special anatomical components, including structures like `Wings` or `Belly` for large creatures and dragons.


---

## 🗂️ Weapons & Inventory

### Weapon Scaling System
Weapons calculate final output dynamically using the wielder's attributes (`Strength`, `Dexterity`, `Knowledge`) alongside diminishing returns soft caps:

| Stat Range | Scaling Efficiency | Description |
| :--- | :--- | :--- |
| **1–30 points** | **100%** | Full efficiency scaling |
| **31–60 points** | **50%** | Moderate soft cap |
| **61+ points** | **15%** | Heavy soft cap |

### Decoupled Inventory
The `Inventory` class encapsulates item management, durability tracking, and capacity validation, keeping the `Character` class clean and focused purely on combat logic.