Frequently Asked Questions
============================

.. contents:: Contents
    :depth: 3

This page contains an assortment of "frequency asked questions" related to Astroneer modding, answered in short form.

If you are a modder, your contributions would be greatly appreciated, no matter how simple or complex. Adding an entry to this page is a great way to help fill some of the gaps in our existing documentation and help future modders solve difficult problems that you may have already solved.
If you'd like to contribute to this project, please submit a pull-request on the `AstroTechies/astroneermodding`_ GitHub repository that adds your new question to  `the faq.rst file`_.

.. _`AstroTechies/astroneermodding`: https://github.com/AstroTechies/astroneermodding/
.. _`the faq.rst file`: https://github.com/AstroTechies/astroneermodding/blob/main/docs/guides/faq.rst

General Questions
--------------------------

How can I get help with modding Astroneer?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
If you have any questions or are facing any issues that are not answered by this documentation, please feel free to join the Astroneer Modding Discord server and ask your questions there. You can join the Discord server using the following invite link: https://discord.gg/bBqdVYxu4k

Why are there two major mod loaders (astro_modloader and AstroModLoader Classic)?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
The first mod loader for Astroneer was called AstroModLoader by AstroTechies (primarily developed by atenfyr in 2020). When atenfyr initially left the Astroneer Modding community in late 2021, Konsti and localcc decided to rewrite AstroModLoader, along with AstroModIntegrator and UAssetAPI, into the Rust programming language. astro_modloader (Rust) was created from this effort.

In late 2024, Konsti shut down the astroneermods.space website and left the Astroneer Modding community, which caused astro_modloader (Rust) to become unmaintained.
Shortly thereafter, atenfyr returned to the Astroneer Modding community, began to maintain astro_modloader (Rust), and also created his own updated fork of the original AstroTechies AstroModLoader, which is now AstroModLoader Classic.

Both mod loaders are compatible with most Astroneer mods, but there are some minor variations that exist between each mod manager's implementation of the mod integrator, discussed further in the :ref:`compatibility` section of the :ref:`metadatav2` page. Most notably, astro_modloader (Rust) does not currently support UE4SS mods.

How can I implement Astroneer support for my custom mod manager?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
The simplest strategy for implementing Astroneer support for your own mod manager is to implement support for managing UE4SS mods, and then require that all users install the `AutoIntegrator`_ C++ mod, which is a UE4SS mod that allows AstroModLoader mods to be loaded as UE4SS LogicMods by executing the mod integrator on game launch. This reduces the complexity of the implementation to simple file management. Many other Unreal Engine games use UE4SS for managing their mods, so any implementation that brings mod support to those games can typically be cloned to implement support for Astroneer.

.. _`AutoIntegrator`: https://new.thunderstore.io/c/astroneer/p/atenfyr/AutoIntegrator/

How can I tell if a mod is compatible with the latest game version?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Any mods that were last updated before November 21st, 2025, are incompatible with the latest version of the game, because the recent MEGATECH update (1.36.42.0) updated the Unreal Engine version of the game from 4.23 to 4.27, which required that all mods be re-cooked.

Many mods specify the ``game_build`` field in their ``metadata.json`` file, which provides information about what version of the game the mod is intended to be used with. If this version is unspecified or is not the latest version of the game, the mod is not guaranteed to work, but may work nonetheless. Any mod that is designed for game version 1.35 or earlier is guaranteed to not function on the latest version of the game.

What is a .pak file?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
The .pak format is an archive file format used by the Unreal Engine for storing multiple files of any type. It is similar in concept to a .zip or .rar file. These files are relatively simple to "zip" or "unzip" using external tools such as `trumank's repak`_.

.. _`trumank's repak`: https://github.com/trumank/repak

What is a .uasset file?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
The .uasset format is a file format used by the Unreal Engine to store game assets.

Game assets can either be **uncooked**, meaning that they are designed for use in the Unreal Editor, or **cooked**, meaning that they are designed to be loaded by a shipped game and have had editor data stripped for performance reasons.
Uncooked assets can be opened simply by placing them in an Unreal Editor project's ``Content`` directory and opening them in the Unreal Editor.
Cooked assets, such as those found in .pak files, can be examined and modified at a low level using tools like `atenfyr's UAssetGUI`_.

It is theoretically possible to reconstruct uncooked assets from the information contained within cooked assets, although tools to do so are currently severely limited (as of 2025), meaning that it is often easier to recreate assets in the Unreal Editor by hand. Such tools that have been developed in the past include `JsonAsAsset`_ and `UEAssetToolkitGenerator`_.

.. _`JsonAsAsset`: https://github.com/JsonAsAsset/JsonAsAsset
.. _`UEAssetToolkitGenerator`: https://github.com/LongerWarrior/UEAssetToolkitGenerator

What is the "mod integrator?"
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
The mod integrator is a piece of software that modifies base game assets on behalf of other mods. The mod integrator executes whenever a mod is added or removed (or more often) and produces a new mod with the filename ``999-AstroModIntegrator_P.pak`` that contains automatically-modified base game assets. The purpose of the mod integrator is to allow multiple mods to make certain modifications to the same base game asset at once and thus reduce mod conflict, because a base game asset can only be modified by one mod at a time.

Mods communicate with the mod integrator by providing a ``metadata.json`` file within the mod's ``.pak`` file. The ``metadata.json`` file follows a standardized file format (see the :ref:`metadatav2` page) that contains information about what the integrator should do on the mod's behalf, among other things. This includes routines like: adding custom Actors to the level; attaching custom Actor Components to base game Actors; adding custom mission trailheads to the level; adding items to base game item lists; and so on.

Two major implementations of the "mod integrator" exist: AstroModIntegrator Classic and astro_mod_integrator (Rust). AstroModIntegrator Classic is used by AstroModLoader Classic and AutoIntegrator-based mod loaders, and astro_mod_integrator (Rust) is used by astro_modloader (Rust). There are some minor variations that exist between each of these implementations of the mod integrator, discussed further in the :ref:`compatibility` section of the :ref:`metadatav2` page.

The modding communities for some other Unreal Engine games, such as State of Decay 2, have adopted the concept of a "mod integrator" as well, but most Unreal Engine games instead choose to create mods by exclusively using tools like UE4SS, which can perform at runtime much of the actions that the mod integrator performs.

What is the difference between a package name and a raw file path?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
When internally referring to the location of packages (game assets), the Unreal Engine uses the ``/Game/`` path as a shorthand for the ``Astro\Content`` directory, consistently uses the forward slash (``/``) as a directory separator instead of the backslash (``\``), and neglects file extensions.

For example, a package name could be something like ``/Game/Items/ItemTypes/FloodLight_IT``, which corresponds to the file loaded at the raw file path ``Astro\Content\Items\ItemTypes\FloodLight_IT.uasset``. When packaging a .pak file, all files should be located at their raw file path (i.e., never create a folder named ``Game`` at the root of your .pak file).

You may also see other prefixes used where a package name would be located, such as ``/Script``, which is used to refer to classes defined in a specific C++ module (see the `Source/Astro/Public directory within the ModdingKit`_ to examine classes provided under the ``/Script/Astro`` path).

.. _`Source/Astro/Public directory within the ModdingKit`: https://github.com/AstroTechies/ModdingKit/tree/master/Source/Astro/Public

Game Asset Questions
--------------------------

How can I examine and modify .uasset files?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
You can examine and modify .uasset files using `atenfyr's UAssetGUI`_, which is discussed further on the :ref:`basicmodding` page.

.. _`atenfyr's UAssetGUI`: https://github.com/atenfyr/UAssetGUI

Can I write my own scripts for manipulating .uasset files?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Yes, you can write your own programs for manipulating .uasset files using `atenfyr's UAssetAPI`_, which UAssetGUI is based on. Several guides and some documentation for interfacing with UAssetAPI using the C# programming language are available here: https://atenfyr.github.io/UAssetAPI/

Alternatively, UAssetGUI can be used directly to convert .uasset files to-and-from the JSON format via the command line, which can then be used to indirectly examine and manipulate .uasset files in any programming language. You can find more information about UAssetGUI's command line arguments here: https://github.com/atenfyr/UAssetGUI?tab=readme-ov-file#command-line-arguments 

.. _`atenfyr's UAssetAPI`: https://github.com/atenfyr/UAssetAPI

How can I find what asset an ObjectProperty points to?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Given an ObjectProperty within an asset, examine the asset's Import Map to find the import that the ObjectProperty points to (defined by a negative number in the property's "Variant" field). Then, follow that import's OuterIndex, which points to another import. That import will then have the path to the corresponding asset under the ``ObjectName`` column. 

See the images provided below for a visual guide on how to follow an import, using the FloodLight item type as an example. In this case, the ItemType at ``/Game/Items/ItemTypes/FloodLight_IT`` corresponds to the PhysicalItem at ``/Game/Components_Small/FloodLight_BP``.

.. image:: faq1.png
  :width: 1200

.. image:: faq2.png
  :width: 1200

I want to modify a specific item, but I can't find its ItemType asset!
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
One of the most common ways to find a specific asset is to search for a specific string that is expected to occur within the asset using grep or Windows findstr.

For example, navigating to the ``Astro\Content\Items\ItemTypes`` directory and executing ``findstr /S /I /M /C:"jetpack" *.uasset`` on the Windows command line will return the paths of every file within the current directory containing the string "jetpack", recursively, including the paths to the item type assets for both the Hydrazine Jetpack and the Solid-Fuel Jump Jet.

You may wish to alternatively refer to the lookup table that was generated below using UAssetAPI on January 1st, 2026, which contains the English name of every item in the game paired with the path on disk to its ItemType asset. The relevant source code that was used to generate the lookup table is also provided for those who are interested.

.. collapse:: LookupTableIT.json

  .. literalinclude:: LookupTableIT.json
    :language: json

.. collapse:: LookupTableGenerator.cs

  .. literalinclude:: LookupTableGenerator.cs
    :language: cs

I want to modify a specific item, but I can't find its PhysicalItem asset!
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Refer to the question above to try and find the ItemType asset corresponding to the PhysicalItem asset that you are looking for. Then, open the corresponding ItemType asset in UAssetGUI, and examine the ``PickupActor`` field within the Class Default Object (typically Export 2). Follow this import to find the PhysicalItem asset that corresponds to the ``PickupActor`` ObjectProperty (as seen in the "How can I find what asset an ObjectProperty points to?" question).

You may wish to alternatively refer to the lookup table that was generated below using UAssetAPI on January 1st, 2026, which contains the English name of every item in the game paired with the path on disk to its PhysicalItem asset. The relevant source code that was used to generate the lookup table is also provided for those who are interested.

.. collapse:: LookupTableBP.json

  .. literalinclude:: LookupTableBP.json
    :language: json

.. collapse:: LookupTableGenerator.cs

  .. literalinclude:: LookupTableGenerator.cs
    :language: cs

How can I add an item to an existing printer?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Add the following entry to your metadata.json file. If the "integrator" or "item_list_entries" tags already exist in your metadata.json file, merge these new entries with your existing entries.

.. code-block:: json

  "integrator": {
      "item_list_entries": {
          "/Game/Items/BackpackRail": {
              "PrinterComponent.Blueprints": [
                  "/Game/PATH/TO/ITEM/TO/ADD/ExampleItem_BP"
              ]
          }
      }
  }

Replace ``/Game/Items/BackpackRail`` with one of the following entries, depending on which printer you would like to add the item to:

* ``/Game/Items/BackpackRail``: Backpack Printer
* ``/Game/Components_Small/Printer_Breadboards_T1``: Small Printer
* ``/Game/Components_Medium/Printer_Breadboards_T2``: Medium Printer
* ``/Game/Components_Large/Printer_Breadboards_T3``: Large Printer

If your mod does nothing other than add a vanilla item to a printer, then you don't need to include anything else in your .pak file other than the metadata.json file.

How can I have my item start off as "hacked" in the Glitchwalkers DLC?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Add the following entry to your metadata.json file. If the "integrator" or "item_list_entries" tags already exist in your metadata.json file, merge these new entries with your existing entries.

.. code-block:: json

  "integrator": {
      "item_list_entries": {
          "/Game/U32_Expansion/Items/HackedCatalog/InitialHackedItemList": {
              "ItemTypes": [
                  "/Game/PATH/TO/ITEM/TO/ADD/ExampleItem_BP"
              ]
          },
          "/Game/U32_Expansion/Items/HackedCatalog/GW_VP000_Unhack_ItemList": {
              "ItemTypes": [
                  "/Game/PATH/TO/ITEM/TO/ADD/ExampleItem_BP"
              ]
          }
      }
  }

Replace ``/Game/U32_Expansion/Items/HackedCatalog/GW_VP000_Unhack_ItemList`` with one of the following entries, depending on which rootkit you want to unlock the item:

* ``/Game/U32_Expansion/Items/HackedCatalog/GW_VP000_Unhack_ItemList``: Alpha Rootkit
* ``/Game/U32_Expansion/Items/HackedCatalog/GW_VP001_Unhack_ItemList``: Delta Rootkit
* ``/Game/U32_Expansion/Items/HackedCatalog/GW_VP002_Unhack_ItemList``: Zeta Rootkit

If your mod does nothing other than make a vanilla item "hacked" or "unhacked" in the Glitchwalkers DLC, then you don't need to include anything else in your .pak file other than the metadata.json file.

Unreal Editor Questions
--------------------------

When I try to open the .uproject file, I get the error "Astro could not be compiled."!
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Make sure that the .uproject file you are opening was downloaded as part of the AstroTechies ModdingKit, and that you have installed UE 4.27 from the Epic Games Launcher. You may wish to attempt to right-click the .uproject file and select "Generate Visual Studio project files". Then, open the ``Astro.sln`` file, and attempt to build the project from source manually.

My custom item won't show up in the research catalog!
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
This is a general indicator that there is some issue in your mod setup or in the configuration of your item. You may wish to first verify that your metadata.json is set-up properly.

My custom item is overwriting another item in the printer menu!
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
For items that are printed in any printer other than the backpack printer, the item must be to the right of the item's base item in the catalog (i.e., your "Variant Type" must be set to "Right" instead of "Left"). You may wish to consider creating your own row in the catalog for your item.

How can I add a body slot to my PhysicalItem?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
To add a "body slot" (which is the slot that allows your item to slot onto platforms, etc.), add a new Child Slot Component called "BodySlot" and attach it to your Static Mesh Component. Move the slot to its desired location, and modify the "Rotation" parameter so that the blue arrow is facing normal to the surface the slot is on (typically, downwards). Under the Details pane, change the "Child Slot Class" to one of the following:

* ``PowerSlot_BodySlot``: T1 slot
* ``Chassis_BodySlot``: T2 slot
* ``LargeChassis_BodySlot``: T3 slot
* ``XLChassis_BodySlot``: T4 slot

Note that these types are different than the types used for non-body slots.

.. image:: faq3.png
  :width: 1200

Then, you must create a new "Get Body Slot Legacy" function on our item. Click on the "Override" dropdown next to "Functions" and select "Get Body Slot Legacy".

.. image:: faq4.png
  :width: 1200

Then, simply set the function to return a copy of the "Body Slot" variable, which should have been automatically created when you added the new Child Slot Component.

.. image:: faq5.png
  :width: 400

How can I add a slot to my new item?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
As is done for the body slot, first add a new Child Slot Component of any name attached to the Static Mesh Component. The slot should be rotated so that the blue arrow is facing normal to the surface it is on (typically, upwards). Change the Child Slot Class to one of the following:

* ``PowerSlotGeneric``: T1 slot
* ``ChassisSlot``: T2 slot
* ``LargeChassisSlot``: T3 slot
* ``XLChassisSlot``: T4 slot
* ``PowerSlot_StreamingCable``: power cable slot; change "Configuration" to "Horizontal"

Note that these types are different than the types used for body slots.

If your item has custom (non-body) slots, you should also add a Storage Chassis Component and an Actor Streaming Power Node Component (named "ActorStreamingPowerNode") to your PhysicalItem class. The fields of these components do not necessarily need to be changed from their default values, but the components should be present nonetheless.

To set up the Actor Streaming Power Node, add the following data to the PhysicalItem's Entity Link Component:

.. image:: faq6.png
  :width: 400

How can I have my item consume/produce power?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Add a PowerComponent to your PhysicalItem with the "Net Power Output" field set to either a positive value (generated power in U/s) or a negative value (consumed power in U/s). If power is being generated, check the "Is Generator" checkbox.

How can I add a custom interaction to my item?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Under the PhysicalItem's "Clickable Component", enable the "Has Use Interaction by Default" field, and, if applicable, enable the "Has Aux Slot Use by Default", "Has Actuator Use by Default", and the "Has Use While Player Driving" fields. Change the "Default Use Context" field to whatever should be displayed by default in the item tooltip as the "use context".

Then, add the "InputAction Use" event to the Event Graph (under Input, Action Events, Use), and attach whatever blueprint code you would like to execute when the item is used to the "Pressed" node.

You can use the "Set Active Use Context" node to change the current use context (for example, to change the tooltip text from "Turn On" to "Turn Off").

How can I execute some Blueprint code on game load, or every tick?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Create a new Actor, and implement your code under the "Event BeginPlay" node, or the "Event Tick" node, as desired. Then, add the package file name for your new actor to the ``persistent_actors`` integrator entry in your metadata.json file, which will instruct the integrator to add your actor to the level.

Alternatively, you can instead create a new Actor Component, and add the path to your new Actor Component to your metadata.json file under ``/Game/Globals/PlayControllerInstance`` in the ``linked_actor_components`` entry. This will attach your custom ActorComponent to each Player Controller. An older style of mod development, pioneered before the creation of the ``persistent_actors`` integrator entry, was to spawn an Actor into the level (if one did not already exist) within an Actor Component attached to the Player Controller.

"Event BeginPlay" will be executed when the game fully loads, and it will also be executed whenever a save is loaded (on a different instance of the Actor).

How can I make my custom Actor get saved when the user saves the game?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
After cooking your assets, you will have to use UAssetGUI to add a new "bSaveGameRelevant" BoolProperty set to "true" within the class default object. This step is not necessary for PhysicalItem classes, which automatically have bSaveGameRelevant set to true.

How can I have one of my custom missions be activated by a vanilla mission?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
The following blueprint code is part of the Rocket Launcher Mod example in the AstroTechies ModdingKit, and is placed in a persistent actor:

.. image:: faq_mission_unlock1.png
  :width: 1200

.. image:: faq_mission_unlock2.png
  :width: 1200

.. image:: faq_mission_unlock3.png
  :width: 1200

How can I detect which mod loader the user is using?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Refer to the code segment below. You must reference ``IntegratorStatics_BP`` in the construct node. AstroModIntegrator Classic will return a version like "Classic 1.6.2.0", while astro_modloader (Rust) will return a version like "0.1.12".

.. image:: faq7.png
  :width: 1200

How can I get a list of all mods that are enabled?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Refer to the code segment below. "Out" is a local variable; it is a map with the key type set to "Name" and the value type set to "Mod". This code is compatible with both AstroModLoader Classic and astro_modloader (Rust).

.. image:: faq8.png
  :width: 1200

Where can I find some example mods?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
A variety of example mods are provided as part of the AstroTechies ModdingKit, in the ``Astro/Content/Mods`` directory. You are encouraged to refer to these examples while creating your own mods.

UE4SS Questions
--------------------------

How can I add a UE4SS .lua script to my mod?
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Set the "enable_ue4ss" field to "true" in your metadata.json file, as in the example below:

.. code-block:: json

    {
        "schema_version": 2,
        "name": "Tutorial Mod",
        "mod_id": "TutorialMod",
        "author": "YOUR_NAME",
        "description": "A tutorial mod.",
        "version": "0.1.0",
        "sync": "serverclient",
        "enable_ue4ss": true
    }

Then, add a .lua script within your .pak file at the path ``UE4SS\Scripts\main.lua`` (where the UE4SS directory is at the root of your .pak file). If you are developing a C++ mod, you may place your .dll file at ``UE4SS\dlls\main.dll`` within your .pak file.

You may wish to examine the following ``main.lua`` script as an example for testing UE4SS capability. The ``UEHelpers.lua`` and ``AstroHelpers.lua`` files are provided as shared libraries automatically by AstroModLoader Classic and AutoIntegrator-based mod loaders.

.. collapse:: main.lua

  .. literalinclude:: main.lua
    :language: lua

.. collapse:: UEHelpers.lua (as of 2025-12-31)

  .. literalinclude:: UEHelpers.lua
    :language: lua

.. collapse:: AstroHelpers.lua (as of 2025-12-31)

  .. literalinclude:: AstroHelpers.lua
    :language: lua

.. warning::

   UE4SS mods are only supported by AstroModLoader Classic and AutoIntegrator-based mod loaders (such as the Vortex Mod Manager). If the ``enable_ue4ss`` field is specified, your mod will no longer be compatible with astro_modloader (Rust).
