🛡️ Technical Summary: The Base Class

The Character class is an abstract class that serves as the definitive blueprint for creating any hero, minion, or creature within the Batalha Primeira Era universe. Furthermore, it implements the IDamageable interface. Consequently, any character derived from this base class inherently acquires the mechanical capability to receive damage and seamlessly interact within the combat flow.

Here is the logical division of its structural management:

🗂️ 1. Attributes and Data shielding (Encapsulation)

The class encapsulates the character's core state data and gameplay statistics. Through the use of the Math.Clamp method, attribute values are constrained by predefined domain rules, ensuring they remain within valid boundaries (minimum of 0 and maximum of 99). This validation mechanism prevents overflow conditions and unintended stat escalation.

    I.LifePoints and Armor: Responsible for managing character survivability, health state, and physical damage mitigation calculations.

    II.Primary Attributes: Strength, Dexterity, and Knowledge, which serve as the foundational parameters for gameplay mechanics and character progression.

    III.Spectral Insight: Specialized attribute governing the character's ability to perceive and interact with the Spectral Realm. Perception is enabled when the attribute IV.reaches a threshold value of 50 or greater. A dedicated boolean validation method performs runtime verification of this capability.

    IV.Equipped Weapon: Maintains a reference to the currently equipped weapon instance, which is utilized as an input parameter in combat and damage calculation routines.

    V.TakeAction (Method): Executes the offensive action workflow, including target selection, hit location determination, damage computation, and weapon durability validation. The method also applies degradation penalties that may reduce damage output when durability thresholds are reached.

    VI. ReceiveDamage (Method): Processes incoming damage events by multiplying the base damage according to the hit body part, reducing the result by 50% of the character's armor value, and ensuring the final damage is not negative before updating the character's health.

🗂️ 2. Bosses

Spectrum

This class inherits from Character and represents one of the game's bosses. What sets it apart from the others is its ability to break the will of the player-controlled character, should their discernment points prove insufficient for the confrontation.

DefendAgainstAttacker: This boolean method takes a Character object, named target, as a parameter to evaluate its SpectralInsight value. If this value is below 40 points, the Spectrum reduces the player's weapon durability to zero, rendering it useless during combat. Conversely, if the insight is equal to or greater than 40, the protagonist becomes immune to the boss's attack.

Dragon

The Dragon class represents one of the primary and most formidable bosses within the Batalha Primeira Era universe. It inherits directly from the core Character class, expanding its mechanical complexity and threat level in combat.

    I. Targetable Body Parts (GetTargetTableParts) The class overrides the base target table system to introduce localized damage mechanics. In addition to standard character hitboxes, players can strategically target a Dragon's specific vulnerabilities: BodyPart.Wings: Allows players to target the wings (potentially affecting mobility or airborne actions). BodyPart.Belly: Exposes the traditional weak point of ancient drakes for high-risk, high-reward tactical strikes.

    II. Boss Scaling (LifeMultiplier) To reflect its status as an apex predator of the First Age, the class features a dynamic health-scaling method: Multiplication Factor: Instantly scales the Dragon's current health pool by 10x (lifePont *= 10). This ensures a massive, multi-phased challenge during encounter transitions.

🗂️ 3. Heroes

Heroes: The classes inherits from Character, a hybrid hero in the game Batalha_Primeira_Era. It is a subclass that demonstrates a solid understanding of Object-Oriented Programming (OOP) principles in C#, specifically Inheritance, Interfaces with Generics, and Constructor Overloading (Polymorphism).

🗂️ 4. Weapons

Weapons: This class encapsulates the specific attributes and behaviors of weaponry for each hero class, thereby enhancing both code readability and overall system maintainability. By leveraging this structural design, the class exposes the CalculateDamage method, which not only computes the final strike value but also dynamically manages and overrides the weapon's durability during combat sequences. Consequently, this centralized logic ensures that combat calculations remain consistent and easily scalable as new hero classes or equipment types are introduced.

🗂️ 5. Inventory

The Inventory class was introduced to decouple item management and equipment logic from the core character classes. Instead of overloading the Character class with lists of possessions, the Inventory acts as a dedicated controller that encapsulates storage boundaries and tracks item states.

The main advantage of this implementation is its clean interaction within the main game loop.

