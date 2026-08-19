# ⚔️ Batalha Primeira Era — Core Architecture

## 🛡️ Technical Overview: Composition over Inheritance

The core architecture of *Batalha Primeira Era* centers around a unified **`Character`** model powered by **Composition and Component-based Design**. Rather than relying on rigid class inheritance for every hero or monster type, characters are configured dynamically through data definitions (`HeroClass`), interface contracts (`IDamageable`, `IAbility`), and modular behaviors (`Immortality`, `Horde`).

---

## 🗂️ Core Architecture & Structural Components

### 1. Unified Character Model & Encapsulation

The `Character` class implements `IDamageable` and acts as the primary entity for players, bosses, and common enemies. Using `Math.Clamp`, character statistics are constrained to safe operational limits (0–99), ensuring data integrity across combat calculations.

| Attribute | Range / Type | Description |
| :--- | :--- | :--- |
| **`lifePoint`** | `float` | Current vitality status. Triggers death or survival behaviors when it reaches zero. |
| **`Armor`** | `float` | Base physical defense used to mitigate incoming damage. |
| **`Strength` / `Dexterity` / `Knowledge`** | `int` (0–99) | Core attributes that scale weapon damage and determine physical capabilities. |
| **`SpectralInsight`** | `int` (0–99) | Specialized attribute determining interaction with the Spectral Realm (active at **50+**). |
| **`ClassDefinition`** | `HeroClass` | Defines class identity and weapon restrictions dynamically. |

---

### 2. Dynamic Class System (`HeroClass`)

Instead of creating sub-classes for every archetype, class roles and rules are encapsulated within the `HeroClass` object. 

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

## 🧩 Modular Behaviors & Mechanics

Instead of overriding base methods, special entity behaviors are attached to `Character` instances as modular components:

### 1. Immortality Behavior (`ImmortalityBehavior`)
* **Survival Threshold:** When a character receives fatal damage (`expectedLife <= 1%`), the `ImmortalityBehavior` intervenes, setting health to `1 HP` and triggering a temporary invulnerability window (e.g., 5 seconds).

### 2. Horde Integration (`MyHorde`)
* **Dynamic Group Management:** Characters assigned to a `Horde` instance automatically notify the group upon defeat (`lifePoint <= 0`), triggering group mechanics such as soul absorption or morale reduction.

### 3. Targeted Body Parts (`GetTargetTableParts`)
* Polymorphic method that can be customized for specific body structures (e.g., adding `Wings` or `Belly` for large creatures or dragons).

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