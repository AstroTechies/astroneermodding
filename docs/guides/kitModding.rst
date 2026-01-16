Adding Custom Items with the Unreal Editor
==========================================

.. contents:: Contents
    :depth: 3

Creating the Mod Folder
-----------------------

After opening the project in the Unreal Editor, you will need to make a folder for your mod.

In the content browser head to ``Mods`` folder and create a folder called ``YOUR_USERNAME``, and open that folder.
You would probably want to change ``YOUR_USERNAME`` to your actual username.

Inside that folder, create a folder called ``TutorialMod`` and open it.

Creating an Item
----------------

Items in Astroneer consist of two components: the ItemType and the PhysicalItem.

ItemType classes (you'll use an ``_IT`` suffix for these files) are used by Astroneer to get your item's recipe, menu icon, item properties, description, etc.

PhysicalItem classes (you'll use an ``_BP`` suffix for these files) are the actual objects ("Actors") that are created in the world.

These are different from the 3D mesh that you'll import, which has no suffix in its name and stores the 3D model of the object you'll see inside the game.


Creating the PhysicalItem class
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Your first step is to create the PhysicalItem blueprint class. Start by making sure that you have opened the ``TutorialMod`` folder within the Unreal Editor before proceeding.

Click on "Create a blueprint class" and a pop-up window will appear. Click on "Open All Classes" if it isn't opened already.

In the search bar above the class type list, type in "PhysicalItem" and select it once it appears. This menu allows you to select what class your new blueprint will inherit from.  

Then, create the class. Once the dialog window closes, you should be able to type in the name of the blueprint. Set it to ``ExampleItem_BP`` - it must 
match this exactly. Then, press enter to save your name, and double-click on it to open up the PhysicalItem's settings menu.

Now, we want to set the 3D model for that item, so that we have something to look at inside the game.
To do that, we will need to :ref:`import the mesh <importingMesh>`.

.. _importingMesh:


Importing the Mesh
""""""""""""""""""""""

    1. Open your mod folder in the **Content Browser**
    2. Download this example mesh :download:`exampleMesh.fbx`
    3. Drag & drop your mesh into the **Content Browser** and click ``Import all`` at the bottom right.  Don't change any settings in this window.
    4. Open the mesh and in the **Details** tab search for ``Has navigation Data`` and uncheck that checkmark.

.. warning::
    If you don't uncheck ``Has navigation Data`` the game will crash when loading your mod.

After importing the mesh, open the PhysicalItem again that you created earlier, and click on **StaticMeshComponent** in the left-hand side menu.

.. note:: 
    If you don't see the **StaticMeshComponent** click on **Open Full Blueprint Editor**

In the **Details** tab, set your mesh in the ``Static Mesh`` field to point to the example mesh you imported.

Click on **Compile** and then **Save**.


Creating the ItemType class
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Now that we have created the PhysicalItem, it's time to make the ItemType for it. 

Create a second blueprint class, and in the creation dialogue choose "Open All Classes" as before.

Select "ItemType" as the class to inherit from, create the new blueprint, set its name to ``ExampleItem_IT``, and open the class.

In this basic guide, we will leave most of the options untouched, but you can experiment with them yourself and see what they do if you so choose.

* Set **Pickup Actor** to be ``ExampleItem_BP``.
* Open **Construction Recipe**
    * Press "+" on the **Ingredients** field.
    * Set Item type of the ingredient to **Astronium**
    * Set Count of the ingredient to 1.
* Set **Catalog Data** to ``Item Catalog Data``

- ``Is Base Item`` in **Catalog Data** is used to determine whether this item will create it's own line inside the catalog or use an existing one from a base item.
- ``Base Item Type`` in **Catalog Data** is used to determine what row of the item catalog that the item will be listed in.
- ``Variation Sequence Number`` in **Catalog Data** is used to determine the order in which the item will be listed in the item catalog.
- ``Catalog Mesh`` in **Catalog Data** is used to determine the mesh that will be displayed in the item catalog.

.. warning:: 
    If you enable ``Is Base Item`` and then set ``Base Item Type`` to equal another object, your item **WILL NOT** show up in the catalog.  

* Set **Base Item Type** to ``Consumable_JumpJet_IT`` so it gets listed near jetpacks and hoverboards.
* Untick **Is Base Item** because you are using the same row as the jetpacks, which already has a base item.
* Set **Catalog Mesh** to the mesh we imported earlier.
* Set **Crate Overlay Texture** to ``ui_icon_package_drill``. This is used to determine the icon that will be displayed on the packaged item.
* Set **Widget Icon** to ``ui_icon_comp_drill``. This is used to determine the icon that will be displayed in the item catalog and on hovering on the item.

Open **Control Symbol** section and fill the fields out like this:

* **Name**: ``Example Item``
* **All caps Name**: ``EXAMPLE ITEM``
* **Tooltip Subtitle**: ``Example Item``
* **Description**: ``This is an example item.``

These four entries do not have to match the names of your objects, they are only used to determine the text used in the research catalog and tooltips in-game.

Linking ItemType and BP together
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Now, open the ``ExampleItem_BP`` yet again, and click on **ItemComponent**. 

Within the Details pane, open the **Item Component** dropdown, and set the **Item Type** to ``ExampleItem_IT``.

Cooking the Mod
-----------------

Remember to save every asset you have changed before cooking.

Click on **File** -> **Cook Content for Windows**.

After the content has finished cooking, create a folder in File Explorer with the name ``000-TutorialMod-0.1.0_P`` and open this folder.

.. note:: 
    This folder must be outside of your Unreal project.

.. tip::
    You can significantly reduce the time it takes to re-cook the project by unchecking "Full Rebuild" under Edit -> Project Settings... -> Project -> Packaging.

Inside this new folder, create a file called ``metadata.json``.
This file provides information about your mod and provides instructions for how the mod loader should integrate your mod with the game.

.. code-block:: JSON

    {
        "schema_version": 2,
        "name": "Tutorial Mod",
        "mod_id": "TutorialMod",
        "author": "YOUR_USERNAME",
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

``/Game/Items/BackpackRail$PrinterComponent.Blueprints`` contains ItemTypes that can be crafted in the backpack so we need to register here too.

.. note::
    More info about the metadata JSON format can be found on the :doc:`../standards/metadatav2` page.

In this folder, also create a folder structure like this ``Astro/Content/Mods/YOUR_USERNAME``.

Now, navigate to the folder where your Unreal project is located in File Explorer and then navigate to ``Saved/Cooked/WindowsNoEditor/Astro/Content/Mods/YOUR_USERNAME`` and copy ``TutorialMod`` folder to the folder we created previously, so that the folder structure looks like this:

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
    The files in ``Saved/Cooked/WindowsNoEditor/Astro/Content/Mods/YOUR_USERNAME`` and ``Content/Mods/YOUR_USERNAME`` are different.
    The first location contains the cooked files, while the second one contains the uncooked ones.

    You **MUST** copy from the first location rather than the second, because the game only accepts cooked assets.

Packaging the Mod
-------------------

Now that the mod structure is complete, you can now package your mod for execution. If you have already followed the guide on the :ref:`basicModding` page, you may skip these steps and package your mod using the steps on that page ("Packing the Mod" and onward).

To pack the mod, we will be using ``repak``, the popular open-source program developed by trumank, which is discussed more on the :ref:`basicsetup` page.

To make life easier, we have created a .zip file containing a copy of ``repak`` and other helper scripts to help you pack your mod folder. Download and extract that .zip file here: :download:`repak_for_astroneer <repak_for_astroneer.zip>`.

Now that the scripts and the program are extracted, we can pack our mod. Open two File Explorer windows, one with the _Repack.bat file, and the other showing your mod folder.

Next, drag and drop your project's main folder (000-TutorialMod-0.1.0_P) onto the ``_Repack.bat`` file.

After repak finishes, you should be able to see a new file named ``000-TutorialMod-0.1.0_P.pak``.
To load this mod, drag & drop this .pak file onto the mod loader window and check the checkbox to enable it.

You should now be able to print your first item.
