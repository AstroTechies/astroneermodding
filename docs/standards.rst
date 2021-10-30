.. _standards:

Modding standards
=================

This section of the documentation was primarily written by `atenfyr <https://github.com/atenfyr>`_.

.. toctree::
   :caption: Contents
   :maxdepth: 3

   standards

.. include:: notice.rst


The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT", "SHOULD", "SHOULD NOT", 
"RECOMMENDED",  "MAY", and "OPTIONAL" in this document are to be interpreted as described in 
`RFC 2119 <https://tools.ietf.org/html/rfc2119>`_.


.. _general:

General Information
-------------------

All mods are Unreal Engine 4 .pak files. The .pak file format is used by the Unreal Engine for storing large amounts of data in a compact space. 
When placed into the ``%localappdata%\Astro\Saved\Paks`` directory, these .pak files are loaded by Astroneer as patches, or partial replacements, 
of the main game assets found in the ``Astro-WindowsNoEditor.pak`` file.

.. _filename:

.pak Mod File Names
-------------------

**On distribution, .pak mod file names MUST follow the following format, as to facilitate the usage of mods even without a mod loader:**

``{PRIORITY}-{MOD ID}-{VERSION}_P.pak``

-  ``{PRIORITY}`` represents a 3-digit number, such as ``001`` or ``005``. Larger numbers are generally loaded later by the engine, so they always
   have priority when multiple mods override the same file. Most mods will likely want to use a priority of ``000`` or ``001``, but in some cases,
   higher priorities are called for. The priority level ``999`` is reserved for external applications, and MUST NOT be used by regular mods.

-  ``{MOD ID}`` is any alphanumeric string representing the ID (and, roughly, the name) of the mod, as a means of distinguishing it from other mods.
   Mod IDs MUST NOT contain any characters other than uppercase ASCII letters, lowercase ASCII letters, and the digits zero through nine;
   as such, they also MUST NOT include any special characters, including, but not limited to, hyphens, underscores, and spaces.
   Mod IDs SHOULD be formatted in upper camel case, also known as Pascal case.

-  ``{VERSION}`` represents the current revision of the mod file. The version MUST be represented by at least two numbers separated by periods,
   but ultimately SHOULD be ``MAJOR.MINOR.PATCH`` .

-  The ``_P`` at the end of the file's name is a requirement for the mod to load.


.. _metadata:

Metadata standard
-----------------

metadata


.. _indexfile:

Index file standard
-------------------

index file


