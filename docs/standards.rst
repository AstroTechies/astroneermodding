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

The following describes Schema Version 1.

The metadata of mods is stored in the JSON format, as described in `RFC 8259 <https://tools.ietf.org/html/rfc8259>`_,
within a file with the name ``metadata.json``. This file MUST be placed at the root directory within all .pak mods,
MUST be encoded in UTF-8, and MUST NOT include a byte order mark.

The following is a list of fields that can be specified within the root object of the metadata:

-  ``schema_version``: An integer that represents the current version of the ``metadata.json`` standard that is being used.
   The schema version is incremented every time there is a backwards-incompatible change to the format.
   This field SHOULD be specified, but if it is left unspecified, it defaults to ``1``, the initial and current version of the standard.

-  ``name``: A plain text display name for the mod. This field is represented as a string, and is REQUIRED.

-  ``mod_id``: The ID for the mod, which MUST be the exact same as the mod ID specified in the mod's original file name, and follows the
   same restrictions and recommendations described within the :ref:`filename` section. This field is represented as a string and is REQUIRED.

-  ``author``: The author of the mod. This field is represented as a string, and is OPTIONAL, defaulting to an empty string.

-  ``description``: A plain text display description of the mod. This field is represented as a string, and is OPTIONAL, defaulting to an empty string.

-  ``version``: A version for the mod, which MUST be the exact same as the version found in the mod's original file name,
   and follows the same restrictions and recommendations described within the :ref:`filename` section.
   This field is represented as a string, and is REQUIRED.

-  ``game_build``: The Astroneer build for which the mod was built. This field is represented as a string, and is OPTIONAL. It defaults to ``null``,
   which is generally understood to mean that the mod works regardless of the current Astroneer build.

-  ``sync``: The sync mode between servers and clients. This field is represented as a string, and is OPTIONAL, defaulting to ``"serverclient"``.
   Valid options are:

   -  ``none``: Represents a mod which will be ignored while syncing.
   -  ``server``: Represents a mod which will only be installed server-side.
   -  ``client``: Represents a mod which will only be installed client-side.
   -  ``serverclient``: Represents a mod which will be installed both server-side and client-side.

-  ``homepage``: A link to the homepage of the mod, a web page where users can go to to find more information about the mod or the author of the mod.
   This field is represented as a string, and is OPTIONAL, defaulting to an empty string.

-  ``download``: An object with fields defining how to auto-update the mod. This field is represented as an object, and is OPTIONAL,
   defaulting to ``{}``, in which case auto-updating is disabled. These are the valid fields:

   -  ``type``: The type of download. This field is represented as a string, and is OPTIONAL. Valid options:

      -  ``"index_file"``: This mod can be downloaded through an "index file" hosted on the web,
         which contains the version info of any number of mods and their direct download web URLs.
         See :ref:`indexfile`.

   -  ``url``: If the type field is set to ``"index_file"``, this is set to the web URL of the mod's index file.
      This field is represented as a string, and is OPTIONAL.

-  ``persistent_actors``: A standard JSON array of asset paths to actors to bake into the level. This field is represented as an array,
   and is OPTIONAL, defaulting to ``[]``.

-  ``mission_trailheads``: A standard JSON array of asset paths to mission trailheads (such as those found in the ``/Game/Missions`` folder)
   to bake into the level. This field is represented as an array, and is OPTIONAL, defaulting to ``[]``.

-  ``linked_actor_components``: A standard JSON object, where the keys are game paths to Actors and the values are standard JSON arrays
   that provide a list of game paths that the mod integrator will automatically attach to the specified Actors.
   This field is represented as an object, and is OPTIONAL, defaulting to ``{}``.

-  ``item_list_entries``: A standard JSON object where the keys are game paths to any asset and the values are standard JSON objects
   where the keys are array names to modify in the asset and the values are standard JSON arrays which list entries to add to the specified
   array as object pointers.
   Alternatively, the array names to modify in the asset can be specified with the format ``category`` ``name.array`` ``name`` in order
   to hone in on one particular array.
   This field has a niche use, but is especially important in adding entries to commonly used item lists,
   such as the list of items that a certain printer can print or the global master list that contains items that need to be referenced on
   bootup for the research catalog or otherwise. This field is represented as an object, and is OPTIONAL, defaulting to ``{}``.

As an example, here is a valid ``metadata.json`` file incorporating all of the defined fields:

.. code-block:: JSON

   {
       "schema_version": 1,
       "name": "Coordinate GUI",
       "mod_id": "CoordinateGUI",
       "author": "ExampleModder123",
       "description": "Adds a coordinate display that toggles with the F3 key.",
       "version": "0.1.0",
       "game_build": "1.19.143.0",
       "sync": "client",
       "homepage": "https://example.com",
       "download": {
           "type": "index_file",
           "url": "https://cdn.example.com/index.json"
       },
       "persistent_actors": [
           "/Game/ExampleModder123/ExampleGUI/ExampleGUIActor"
       ],
       "mission_trailheads": [
           "/Game/ExampleModder123/ExampleMod/MissionTrailhead04-Example"
       ],
       "linked_actor_components": {
           "/Game/Character/DesignAstro": [
               "/Game/ExampleModder123/ExampleGUI/MyActorComponent"
           ]
       },
       "item_list_entries": {
           "/Game/InitialUnlocks_Generous": {
               "ItemTypes": [
                    "/Game/Items/ItemTypes/Components/LevelingBlock"
               ]
               },
               "/Game/Items/BackpackRail": {
                   "PrinterComponent.Blueprints": [
                       "/Game/Components_Terrain/LevelingBlock",
                       "/Game/ExampleModder123/ExampleGUI/ExampleItem_BP"
               ]
           }
       }
   }

As another example, here is a valid ``metadata.json`` file containing only the ``"schema_version"`` field and the REQUIRED fields:

.. code-block:: JSON

   {
       "schema_version": 1,
       "name": "My Tiny Mod",
       "mod_id": "TinyMod",
       "version": "0.1.0"
   }


.. _indexfile:

Index file standard
-------------------

This file contains data and direct download URLs for one or more mods. It is used to automatically find a mod's download link and
to facilitate auto-updating. All mod listings are contained in the ``mods`` field. Each field contains data for one mod and the
field key should be the ``mod_id`` of the corresponding mod.

As an example, here is a valid index file:

.. code-block:: JSON

   {
       "mods": {
           "ExampleMod": {
               "latest_version": "1.1.0",
               "versions": {
                  "1.0.0": {
                      "download_url": "https://example.com/upload/123/file.pak",
                      "filename": "000-ExampleMod-1.0.0_P.pak"
                  },
                  "1.1.0": {
                      "download_url": "https://example.com/upload/124/file.pak",
                      "filename": "000-ExampleMod-1.1.0_P.pak"
                  }
               }
           }
       }
   }


