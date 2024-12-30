.. _metadatav2:

Metadata standard
=====================

.. note:: 
    If you are looking for the metadata v1 standard, it has moved :doc:`here <./metadatav1>`.
    Please note that the metadata v1 standard is outdated. New mods SHOULD use the metadata v2 standard.

The following describes Schema Version 2.

The metadata of mods is stored in the JSON format, as described in `RFC 8259 <https://tools.ietf.org/html/rfc8259>`_,
within a file with the name ``metadata.json``. This file MUST be placed at the root directory within all .pak mods,
MUST be encoded in UTF-8, and MUST NOT include a byte order mark.

The following is a list of fields that are valid within the root object of the metadata:

-  ``schema_version``

   - An integer that represents the current version of the ``metadata.json`` standard that is being used.
   - The schema version is incremented every time there is a backwards-incompatible change to the format.
   - This field SHOULD be specified, but if it is left unspecified, it defaults to ``1``, the initial version of the standard.

-  ``name``

   - A plain text display name for the mod.
   - This field is represented as a string, and is REQUIRED.

-  ``mod_id``

   - The ID for the mod, which MUST be the exact same as the mod ID specified in the mod's original file name, and follows the same restrictions and recommendations described within the :ref:`filename` section.
   - This field is represented as a string and is REQUIRED.

-  ``author``

   - The author of the mod.
   - This field is represented as a string, and is OPTIONAL, defaulting to an empty string.

-  ``description``

   - A plain text display description of the mod.
   - This field is represented as a string, and is OPTIONAL, defaulting to an empty string.

-  ``version``

   - A version for the mod, which MUST be the exact same as the version found in the mod's original file name, and follows the same restrictions and recommendations described within the :ref:`filename` section.
   - This field is represented as a string, and is REQUIRED.

-  ``game_build``

   - The Astroneer build for which the mod was built.
   - This field is represented as a string, and is OPTIONAL. It defaults to ``null``, which is generally understood to mean that the mod works regardless of the current Astroneer build.

-  ``sync``

   - The sync mode between servers and clients. This field is represented as a string, and is OPTIONAL, defaulting to ``"serverclient"``.
   - Valid options are:

     -  ``none``: Represents a mod which will be ignored while syncing.
     -  ``server``: Represents a mod which will only be installed server-side.
     -  ``client``: Represents a mod which will only be installed client-side.
     -  ``serverclient``: Represents a mod which will be installed both server-side and client-side.

-  ``homepage``

   - A link to the homepage of the mod, a web page where users can go to to find more information about the mod or the author of the mod.
   - This field is represented as a string, and is OPTIONAL, defaulting to an empty string.

-  ``download``

   - An object with fields defining how to auto-update the mod.
   - This field is represented as an object, and is OPTIONAL, defaulting to ``{}``, in which case auto-updating is disabled.
   - The following fields are always valid within this object:

     -  ``type``

        - The source that should be used to download this mod.
        - This field is represented as a string, and is OPTIONAL.
        - Valid options:

          -  ``"index_file"``

             - This mod can be downloaded through an "index file" hosted on the web, which contains the version info of any number of mods and their direct download web URLs. See :ref:`indexfile`.

          -  ``"thunderstore"``

             - This mod can be downloaded through the Thunderstore mod database, which is available online at https://thunderstore.io/c/astroneer/.
        
   - If the type field is set to ``"index_file"``, the following fields are valid:

     -  ``url``
     
        - This is set to the web URL of the mod's index file.
        - This field is represented as a string, and is OPTIONAL.

   - If the type field is set to ``"thunderstore"``, the following fields are valid:

     -  ``namespace``
     
        - This is set to the namespace of the mod on Thunderstore, typically corresponding to the "team" that the mod is associated with.
        - This field is represented as a string, and is OPTIONAL.

     -  ``name``
     
        - This is set to the name of the mod on Thunderstore
        - Any spaces in the mod name displayed on the Thunderstore mod database website MUST be replaced with underscores.
        - This field is represented as a string, and is OPTIONAL.

- ``integrator``

  - An object containing desired integration routines to perform for this mod.
  - This field is represented as an object, and is OPTIONAL, defaulting to ``{}``.
  - The following fields are always valid within this object:

   -  ``persistent_actors``

      - A standard JSON array of asset paths to actors to bake into the level.
      - This field is represented as an array, and is OPTIONAL, defaulting to ``[]``.

   -  ``persistent_actor_maps``

      - A standard JSON array of paths to additional maps (levels) to be used globally when performing ``persistent_actors`` integration.
      - Assets in this field MAY be represented with their package names (e.g. in the format of ``/Game/U32_Expansion/U32_Expansion``), but SHOULD be represented instead with their raw paths (e.g. in the format of ``Astro/Content/U32_Expansion/U32_Expansion.umap``) to ensure backwards compatibility.
      - This field applies globally, so it will affect all mods that make use of the ``persistent_actors`` field. Caution is advised.
      - This field is represented as an array, and is OPTIONAL, defaulting to ``[]``.

   -  ``mission_trailheads``

      - A standard JSON array of asset paths to mission trailheads (such as those found in the ``/Game/Missions`` folder) to bake into the level.
      - This field is represented as an array, and is OPTIONAL, defaulting to ``[]``.

   -  ``linked_actor_components``
   
      - A standard JSON object, where the keys are game paths to Actors and the values are standard JSON arrays that provide a list of game paths to components that the mod integrator will automatically attach to the specified Actors.
      - This field is represented as an object, and is OPTIONAL, defaulting to ``{}``.

   -  ``item_list_entries``
   
      - A standard JSON object, where the keys are game paths to any asset and the values are standard JSON objects containing item list modifications to make to that asset.
      - The key of each entry within each item list modification object is an array name to modify in the asset, and the corresponding value is a standard JSON arrays which lists entries to add to the specified array as object pointers.
      - Alternatively, the array names to modify in the asset MAY be instead specified with the format ``category_name.array_name`` in order to hone in on one particular array.
      - This field has a niche use, but is especially important in adding entries to commonly used item lists, such as the list of items that a certain printer can print or the global master list that contains items that need to be referenced on bootup for the research catalog or otherwise.
      - This field is represented as an object, and is OPTIONAL, defaulting to ``{}``.

   -  ``biome_placement_modifiers``
  
      - A standard JSON array of placement modifiers used for adding custom procedurally generated actors.
      - Each placement modifier is represented as a JSON object in which the following fields MUST be specified: ``planet_type``, ``biome_type``, ``biome_name``, ``layer_name``, and ``placements``.
      - Each field MUST be a string, with the exception of ``placements``, which instead MUST be a JSON array of game paths to the procedural modifiers to place into this layer.
      - This field is represented as an array, and is OPTIONAL, defaulting to ``[]``.

-  ``dependencies``

   - A JSON object containing dependencies that must be fetched for this mod to work.
   - Each dependency is represented as an entry with the key being the mod ID as a string, and the value being the version requirement as a string. 
   - Dependency version requirements follow the `semver standard <https://semver.org/>`_.
   - This field is represented as an object, and is OPTIONAL, defaulting to ``{}``.

As an example, here is a valid ``metadata.json`` file incorporating all of the defined root-level fields:

.. code-block:: JSON

   {
        "schema_version": 2,
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
        "integrator": {
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
        },
        "dependencies": {
            "ModA": ">=1.2.0",
            "ModB": "*",
            "ModC": {
                "version": "^1.2.3",
                "download": {
                    "type": "index_file",
                    "url": "https://example.com"
                }
            }
        }
   }

As another example, here is a valid ``metadata.json`` file containing only the ``"schema_version"`` field and the REQUIRED fields:

.. code-block:: JSON

   {
       "schema_version": 2,
       "name": "My Tiny Mod",
       "mod_id": "TinyMod",
       "version": "0.1.0"
   }
