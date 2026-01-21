.. _filename:

.pak Mod File Names
====================

On distribution, .pak mod file names MUST follow the following format, in order to help facilitate the usage of mods even without a mod loader:

``{PRIORITY}-{MOD ID}-{VERSION}_P.pak``

-  ``{PRIORITY}`` represents a 3-digit number, such as ``001`` or ``005``. Larger numbers are generally loaded later by the engine, so they always
   have priority when multiple mods override the same file. Most mods will likely want to use a priority of ``000`` or ``001``, but in some cases,
   higher priorities are called for. All three-digit priority levels that begin with ``8`` or ``9`` are reserved for external applications, and MUST NOT be used by regular mods.

-  ``{MOD ID}`` is any alphanumeric string representing the ID (and, roughly, the name) of the mod, as a means of distinguishing it from other mods.
   Mod IDs MUST NOT contain any characters other than uppercase ASCII letters, lowercase ASCII letters, the period character ("."), and the digits zero through nine;
   as such, they also MUST NOT include any other special characters, including, but not limited to, hyphens, underscores, and spaces.
   Mod IDs SHOULD be formatted in upper camel case, also known as Pascal case.
   A single period character MAY be used to extend the Mod ID with an Author ID to distinguish between mods made by different people.

-  ``{VERSION}`` represents the current revision of the mod file. The version MUST be represented by at least two numbers separated by periods,
   but ultimately SHOULD be ``MAJOR.MINOR.PATCH`` .

-  The ``_P`` at the end of the file's name is a requirement for the mod to load. This designates that the .pak file is a "patch", meaning that it will always load after the base game's paks load, no matter what.

Priority Allocations
----------------------

800-level and 900-level priority levels are reserved for external applications. These priority levels MUST NOT be used by regular mods. The following is a table of priority values that have been reserved for use by specific applications. Applications SHOULD NOT create .pak files with priority levels that have been reserved by other applications.

If you are developing an external application that works with Astroneer mods and would like to reserve a priority value for your application, please submit an issue or pull request to the `AstroTechies/astroneermodding`_ repository. One entry should be added for each unique priority level that is reserved.

.. _`AstroTechies/astroneermodding`: https://github.com/AstroTechies/astroneermodding/

.. csv-table:: Priority Allocations
   :header: "Priority level (exact)", "Application", "Description"

   "800", "astro_modloader (Rust)", "Used for integrator .pak files (``800-CoreMod-0.1.0_P.pak``)"
   "900", "astro_modloader (Rust)", "Used for integrator .pak files (``900-ModIntegrator_P.pak``)"
   "989", "ModDeployer", "Used for temporary .pak files produced by the AstroTechies ModdingKit ModDeployer plugin"
   "999", "AstroModLoader Classic", "Used for integrator .pak files (``999-AstroModIntegrator_P.pak``)"