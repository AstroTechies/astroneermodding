Developing your first mod
==========================

.. contents:: Contents
    :depth: 3

Creating the mod folder
-----------------------

To begin making mods you need to create a folder for your mod. 

For this tutorial we will create the folder in ``Examples`` and call it ``TutorialMod``.

Creating an item
----------------

Items in Astroneer consist of two components, ItemType and PhysicalItem.

ItemType is used by Astroneer to get your item's recipe, menu icon, item properties, description, etc.

PhysicalItem is the object that is going to be created in the world.


Creating the PhysicalItem
^^^^^^^^^^^^^^^^^^^^^^^^^

Create a blueprint class with name ``ExampleItem_BP`` and in the creation dialogue open All classes.

Select PhysicalItem to inherit from, create and open the class.

First of all we want to set the model for that item so that we have something to look at.

For that we will need to :ref:`import the mesh <importingMesh>`

.. _importingMesh:

Importing the mesh
""""""""""""""""""

    1. Open your mod folder in the **Content Browser**
    2. Download this example mesh :download:`exampleMesh.fbx`
    3. Drag&drop your mesh into the **Content Browser** and click ``Import all``
    4. Open the mesh and in the **Details** tab search for ``Has navigation Data`` and uncheck that checkmark.

.. warning::
    If you don't uncheck ``Has navigation Data`` the game will crash when loading your mod.

After importing the mesh, open the item you created and click on **StaticMeshComponent**.

.. note:: 
    If you don't see the **StaticMeshComponent** click on **Open Full Blueprint Editor**

There in the **Details** tab set your mesh in ``Static Mesh`` field.

Click on **Compile** and then **Save**.

Creating the ItemType
^^^^^^^^^^^^^^^^^^^^^

Now that we have created the PhysicalItem it's time to make the ItemType for it.

Create a blueprint class with name ``ExampleItem_IT``, and in the creation dialogue open All classes.

Select ItemType to inherit from, create and open the class.

This is a simple item so we will leave most of the options untouched, you can experiment with them yourself and see what they do.

* Set **Pickup Actor** to be ``TestItem_BP``.
* Open **Construction Recipe**
    * Press "+" on the **Ingredients** field.
    * Set Item type of the ingredient to **Astronium**
    * Set Count of the ingredient to 1.
* Set **Catalog Data** to ``Item Catalog Data``

- ``Base Item Type`` in **Catalog Data** is used to determine where in the item catalog the item will be listed.
- ``Varitation Sequence Number`` in **Catalog Data** is used to determine the order in which the item will be listed in the item catalog.
- ``Catalog Mesh`` in **Catalog Data** is used to determine the mesh that will be displayed in the item catalog.

Set ``Base Item Type`` to ``Consumable_JumpJet_IT`` so it gets listed near jetpacks and hoverboards.

Set ``Catalog Mesh`` to the mesh we imported earlier.


Open **Control Symbol** section and fill the fields out like this:

* **Name**: ``TestItem``
* **All caps Name**: ``TESTITEM``
* **Tooltip Subtitle**: ``Test Item``
* **Description**: ``This is a test item.``

Cooking the mod
---------------

Click on **File** > **Cook Content for Windows**

While the content is being cooked create a folder in file explorer with the name ``000-TutorialMod-0.1.0_P`` and open it.

Inside this folder create a file called ``metadata.json``

Fill this file out like this

.. code-block:: JSON

    {
        "schema_version": 1,
        "name": "Tutorial Mod",
        "mod_id": "TutorialMod",
        "author": "YOUR_NAME",
        "description": "A tutorial mod.",
        "version": "0.1.0",
        "sync": "serverclient",
        "item_list_entries": {
            "/Game/Items/ItemTypes/MasterItemList": {
                "ItemTypes": [
                    "/Game/Examples/TutorialMod/TestItem_IT"
                ]
            },
            "/Game/Items/BackpackRail": {
                "PrinterComponent.Blueprints": [
                    "/Game/Examples/TutorialMod/TestItem_IT"
                ]
            }
        }
    }

Replace ``YOUR_NAME`` with your name.

What this file will do is tell modloader the info about this mod and which files to register with Astroneer.

``/Game/Items/ItemTypes/MasterItemList$ItemTypes`` contains ItemTypes for all items so we register our ItemType with this.

``/Game/Items/BackpackRail$PrinterComponent.Blueprints`` contains ItemTypes that can be crafted so we need to register here too.

More info about the format can be found in :doc:`../standards/index`


Now that the content has cooked go to the project folder.

From there navigate to ``Saved/Cooked/WindowsNoEditor/Astro/Content/Examples`` and copy ``TutorialMod`` folder to the folder we created earlier.

Now that the mod structure is complete, time to pack the mod.

For packing the mod we will be using ``UnrealPak`` which comes preinstalled with your unreal engine installation.

To make life easier for us we have created :download:`Packing Scripts <pakTools.zip>` that help with using UnrealPak, download and extract them.

After extracting them edit path to unreal engine in both of them to the respective path on your system.

Now that the scripts are ready we can pack our mod, drag and drop the mod folder onto the ``_Repack.bat`` file.

After UnrealPak finishes you should be able to see ``000-TutorialMod-0.1.0_P.pak`` file.

To load this mod drag&drop it onto the modloader window and check the checkbox.

After all this work you should be able to print your first item.