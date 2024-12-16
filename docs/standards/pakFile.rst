.. _filename:

.pak Mod File Names
====================

**On distribution, .pak mod file names MUST follow the following format, as to facilitate the usage of mods even without a mod loader:**

``{PRIORITY}-{MOD ID}-{VERSION}_P.pak``

-  ``{PRIORITY}`` represents a 3-digit number, such as ``001`` or ``005``. Larger numbers are generally loaded later by the engine, so they always
   have priority when multiple mods override the same file. Most mods will likely want to use a priority of ``000`` or ``001``, but in some cases,
   higher priorities are called for. The priority levels ``9xx`` is reserved for external applications, and MUST NOT be used by regular mods.

-  ``{MOD ID}`` is any alphanumeric string representing the ID (and, roughly, the name) of the mod, as a means of distinguishing it from other mods.
   Mod IDs MUST NOT contain any characters other than uppercase ASCII letters, lowercase ASCII letters, the period character ("."), and the digits zero through nine;
   as such, they also MUST NOT include any other special characters, including, but not limited to, hyphens, underscores, and spaces.
   Mod IDs SHOULD be formatted in upper camel case, also known as Pascal case.
   A single period character MAY be used to extend the Mod ID with an Author ID to distinguish between mods made by different people.

-  ``{VERSION}`` represents the current revision of the mod file. The version MUST be represented by at least two numbers separated by periods,
   but ultimately SHOULD be ``MAJOR.MINOR.PATCH`` .

-  The ``_P`` at the end of the file's name is a requirement for the mod to load.
