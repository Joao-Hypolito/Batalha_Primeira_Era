# ⚔️ Batalha Primeira Era — Core Architecture

## 🛡️ Technical Overview: Composition over Inheritance

The core architecture of *Batalha Primeira Era* centers around a unified **`Character`** model powered by **Composition and Component-based Design**. Rather than relying on rigid class inheritance for every hero or monster type, characters are configured dynamically through data definitions (`HeroClass`), interface contracts (`IDamageable`, `IAbility`), and modular behaviors (`Immortality`, `Horde`).

---

## 🗂️ Core Architecture & Structural Components

### 1. Centralized Character Model & Data Encapsulation

The `Character` class implements `IDamageable` and serves as the main entity for player characters, bosses, and standard enemies. Through the use of `Math.Clamp`, character attributes are restricted to a controlled range (0–99), preserving data consistency and preventing invalid values from affecting combat calculations.

| Attribute | Range / Type | Description |
| :--- | :--- | :--- |
| **`lifePoint`**                            | `float`      | Represents the character's current health state and determines when death or survival mechanics are activated.             |
| **`Armor`**                                | `float`      | Provides the character's fundamental physical protection and reduces incoming damage.                                      |
| **`Strength` / `Dexterity` / `Knowledge`** | `int` (0–99) | Primary statistics that influence weapon damage scaling and define the character's physical capabilities.                  |
| **`SpectralInsight`**                      | `int` (0–99) | Specialized statistic that controls access to the Spectral Realm, becoming active when its value reaches **50 or higher**. |
| **`ClassDefinition`**                      | `HeroClass`  | Determines the character's class identity and dynamically controls applicable weapon restrictions.                         |


---

### 2. Dynamic Class System (`HeroClass`)

Instead of hardcoding the user roles, class roles and rules are encapsulated within the `HeroClass` object. 

* **Weapon Restrictions:** The `EquipWeapon(Weapon weapon)` method validates whether the target weapon type (`WeaponType`) is listed in the character's `ClassDefinition.AllowedWeapons`.

---

### 3. Combat Mechanics & Targeting

* **`TakeAction(IDamageable target)`**
  Executes an attack sequence. It verifies vitality states, evaluates equipped weapon durability, rolls a targeted body part via `GetTargetTableParts()`, and delegates damage calculation to the weapon or calculates raw unarmed damage based on `Strength`.

* **`ReceiveDamage(float damage, BodyPart hitPart)`**
  Applies precise damage reduction using a non-linear armor formula:
  $$\text{Damage Factor} = \frac{100}{100 + \frac{\text{Armor}}{2}}$$
  It also checks for critical body-part multipliers (e.g., `Belly` x3.0, `Head` x2.0) and evaluates active survival behaviors before applying fatal damage.

---

## 🧩 Modular Behaviors & Gameplay Mechanics

Rather than modifying or overriding the core methods of the base class, specialized behaviors are provided to `Character` instances through independent and reusable components.

### 1. Immortality Behavior (`ImmortalityBehavior`)

* **Survival Trigger:** When incoming damage would reduce a character to a critical health state (`expectedLife <= 1%`), the `ImmortalityBehavior` activates, preserving the character at `1 HP` and granting a temporary period of immunity to further damage (e.g., 5 seconds).

### 2. Horde Integration (`MyHorde`)

* **Automatic Group Coordination:** Characters associated with a `Horde` instance automatically communicate their defeat (`lifePoint <= 0`) to the group, allowing collective mechanics such as soul absorption or morale penalties to be activated.

### 3. Body-Part Targeting (`GetTargetTableParts`)

* Provides a polymorphic targeting mechanism that can be extended to support unique anatomical structures, such as `Wings` or `Belly` when dealing with large creatures and dragons.


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