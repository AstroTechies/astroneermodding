Adding Custom Items with the Unreal Editor
==========================================

.. contents:: Contents
    :depth: 3

Creating the Mod Folder
-----------------------

After opening the project in unreal editor you will need to make a folder for your mod.

In the content browser head to ``Mods`` folder and create a folder ``YOUR_USERNAME``, and open that folder.

You probably would want to change ``YOUR_USERNAME`` to your actual username.

Inside that folder create a folder called ``TutorialMod`` and open it.

Creating an Item
----------------

Items in Astroneer consist of two components, ItemType and PhysicalItem.

ItemType (you'll use an _IT suffix for these files) is used by Astroneer to get your item's recipe, menu icon, item properties, description, etc.

PhysicalItem (you'll use an _BP suffix for these files) is the object that is going to be created in the world.

These are different from the 3D mesh that you'll import, which has no suffix in its name and stores the 3D model of the object you'll see inside the game.


Creating the PhysicalItem
^^^^^^^^^^^^^^^^^^^^^^^^^

Your first step is to create the PhysicalItem blueprint class.  Start by making sure Unreal Editor is in the ``TutorialMod`` folder before proceeding.

Click on "Create a blueprint class" and a pop-up window will appear.  Click on "open All classes" if it isn't opened already.

In the search bar above the class type list, type in "PhysicalItem" and select it once it appears.  This will determine what the blueprint will inherit from.  

Then, create the class.  Once the dialog window closes, you should be able to type in the name of the blueprint.  Set it to ``ExampleItem_BP`` - it must 

match this exactly.  Then press enter to save your name, and double-click on it to open up the PhysicalItem's settings menu.

First of all we want to set the 3D model for that item, so that we have something to look at inside the game.

For that we will need to :ref:`import the mesh <importingMesh>`

.. _importingMesh:


Importing the Mesh
""""""""""""""""""

    1. Open your mod folder in the **Content Browser**
    2. Download this example mesh :download:`exampleMesh.fbx`
    3. Drag&drop your mesh into the **Content Browser** and click ``Import all`` at the bottom right.  Don't change any settings in this window.
    4. Open the mesh and in the **Details** tab search for ``Has navigation Data`` and uncheck that checkmark.

.. warning::
    If you don't uncheck ``Has navigation Data`` the game will crash when loading your mod.

After importing the mesh, open the PhysicalItem again that you created earlier, and click on **StaticMeshComponent** in the left-hand side menu.

.. note:: 
    If you don't see the **StaticMeshComponent** click on **Open Full Blueprint Editor**

There in the **Details** tab, set your mesh in ``Static Mesh`` field to the example mesh you imported.

Click on **Compile** and then **Save**.


Creating the ItemType
^^^^^^^^^^^^^^^^^^^^^

Now that we have created the PhysicalItem, it's time to make the ItemType for it. 

Create a second blueprint class, and in the creation dialogue open All classes.

Select ItemType to inherit from, create the class, set its name to ``ExampleItem_IT``, and open the class.

This is a simple item so we will leave most of the options untouched, you can experiment with them yourself and see what they do.

* Set **Pickup Actor** to be ``ExampleItem_BP``.
* Open **Construction Recipe**
    * Press "+" on the **Ingredients** field.
    * Set Item type of the ingredient to **Astronium**
    * Set Count of the ingredient to 1.
* Set **Catalog Data** to ``Item Catalog Data``

- ``Is Base Item`` in **Catalog Data** is used to determine whether this item will create it's own line inside the catalog or use an existing one from a base item.
- ``Base Item Type`` in **Catalog Data** is used to determine what row of the item catalog that the item will be listed in.
- ``Varitation Sequence Number`` in **Catalog Data** is used to determine the order in which the item will be listed in the item catalog.
- ``Catalog Mesh`` in **Catalog Data** is used to determine the mesh that will be displayed in the item catalog.

.. warning:: 
    If you enable ``Is Base Item`` and then set ``Base Item Type`` to equal another object, your item **WILL NOT** show up in the catalog.  

* Set **Base Item Type** to ``Consumable_JumpJet_IT`` so it gets listed near jetpacks and hoverboards.
* Untick **Is Base Item** because you are using the same row as the jetpacks, which already has a base item.
* Set **Catalog Mesh** to the mesh we imported earlier.
* Set **Crate Overlay Texture** to ``ui_icon_package_drill``. This is used to determine the icon that will be displayed on the packaged item.
* Set **Widget Icon** to ``ui_icon_comp_drill``. This is used to determine the icon that will be displayed in the item catalog and on hovering on the item.



Open **Control Symbol** section and fill the fields out like this:

* **Name**: ``ExampleItem``
* **All caps Name**: ``EXAMPLEITEM``
* **Tooltip Subtitle**: ``Example Item``
* **Description**: ``This is an example item.``

These four entries do not have to match the names of your objects, they are used to determine the text used in the research catalog and tooltips in-game.


Linking ItemType and BP together
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Now open the ``ExampleItem_BP`` yet again, and click on **ItemComponent**. 

On the right open the **Item Component** dropdown, there, set the **Item Type** to ``ExampleItem_IT``.

Cooking the Mod
---------------

Remember to save every asset you have changed before cooking.

Click on **File** > **Cook Content for Windows**

After the content has been cooked, create a folder in file explorer with the name ``000-TutorialMod-0.1.0_P`` and open this folder.

.. note:: 
    This folder must be outside of unreal project.

Inside this folder create a file called ``metadata.json``.

This file is responsible for telling the modloader where to find mod files for certain parts of the mod.

Fill this file out like this

.. code-block:: JSON

    {
        "schema_version": 2,
        "name": "Tutorial Mod",
        "mod_id": "TutorialMod",
        "author": "YOUR_NAME",
        "description": "A tutorial mod.",
        "version": "0.1.0",
        "sync": "serverclient",
        "integrator": {
            "item_list_entries": {
                "/Game/Items/ItemTypes/MasterItemList": {
                    "ItemTypes": [
                        "/Game/Mods/YOUR_USERNAME/TutorialMod/ExampleItem_IT"
                    ]
                },
                "/Game/Items/BackpackRail": {
                    "PrinterComponent.Blueprints": [
                        "/Game/Mods/YOUR_USERNAME/TutorialMod/ExampleItem_BP"
                    ]
                }
            }
        }
    }

Replace ``YOUR_USERNAME`` with your name.

``/Game/Items/ItemTypes/MasterItemList$ItemTypes`` contains ItemTypes for all items so we register our ItemType with this.

``/Game/Items/BackpackRail$PrinterComponent.Blueprints`` contains ItemTypes that can be crafted so we need to register here too.

More info about the format can be found in :doc:`../standards/index`

In this folder, also create a folder structure like this ``Astro/Content/Mods/YOUR_USERNAME``.

Now go to the unreal project folder and navigate to ``Saved/Cooked/WindowsNoEditor/Astro/Content/Mods/YOUR_USERNAME`` and copy ``TutorialMod`` folder to the folder we created previously.

So that the folder structure looks like this:

.. code-block:: 

    000-TutorialMod-0.1.0_P
        ├───metadata.json
        │
        └───Astro
            └───Content
                └───Mods
                    └───YOUR_USERNAME
                        └───TutorialMod


.. warning:: 
    Files in ``ModdingKit/Saved/Cooked/WindowsNoEditor/Astro/Content/Mods/YOUR_USERNAME`` and ``UE_PROJECT/Content/Mods/YOUR_USERNAME`` are different.
    Where UE_PROJECT is the path to unreal project.
    The first location contains the cooked files, while the second one contains the uncooked ones.
    You **MUST** copy from the first location because the game only accepts cooked ones.

Now that the mod structure is complete, time to pack the mod.

For packing the mod we will be using ``UnrealPak`` which comes preinstalled with your unreal engine installation.

To make life easier for us we have created :download:`Packing Scripts <pakTools.zip>` that help with using UnrealPak, download and extract them.

After extracting them edit path to unreal engine in both of them to the respective path on your system.

Now that the scripts are ready we can pack our mod.  Open two file explorer windows, one with the repack.bat file, and the other showing your mod folder.

Next, drag and drop your project's main folder (000-TutorialMod-0.1.0_P) onto the ``_Repack.bat`` file.

After UnrealPak finishes you should be able to see ``000-TutorialMod-0.1.0_P.pak`` file.

To load this mod, drag&drop it onto the modloader window and check the checkbox to enable it.

After all this work you should be able to print your first item.