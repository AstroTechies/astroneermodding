Basic Modding
=============

.. contents:: Contents
    :depth: 3

In this section you will learn how to create your first mod. We will start by simply reducing the byte
cost of the Floodlight.

How Making a Mod Works
----------------------

Using UAssetGUI is a fairly simple way to create a mod. The process, which is covered in detail
below, involves making a copy of Astroneer's file structure (while only containing the necessary 
files) inside your mod folder. Then, UAssetGUI is used to modify those game files to your liking.
Once the metadata file is set up correctly, the mod folder is packaged using repak, and 
your mod is ready to use.  

 While the ways in which the game files are modified will vary, most other methods of creating 
 mods involve a similar process as this one.

Setting up Folders
------------------

First start by creating a new folder called ``TutorialMod`` in your ``AstroneerModding`` folder.
We will be using that folder in this and future tutorials.

Next create another folder inside the ``TutorialMod`` folder called ``000-TutorialMod-0.1.0_P``.
This name matches what the mod .pak file will later be called. You can read more on the
specifications for mod .pak files in the :ref:`filename` section of the documentation.

Creating metadata.json File
---------------------------

Each mod must have a metadata.json file at the root. This file contains information about the mod.
In your ``000-TutorialMod-0.1.0_P`` folder create a file called ``metadata.json``. Then paste the
following JSON into the file. Make sure to replace ``YOUR_NAME`` with your name.

.. code-block:: JSON

    {
        "schema_version": 2,
        "name": "Tutorial Mod",
        "mod_id": "TutorialMod",
        "author": "YOUR_NAME",
        "description": "A tutorial mod.",
        "version": "0.1.0",
        "sync": "serverclient"
    }

Finding the Asset to modify
---------------------------

Because we are modifying an item that is already in the game we need to find the asset of the item.
We want to change the byte cost of the floodlight so we need to find the ItemType asset of the
floodlight. Sometimes finding the proper file can hard, but Windows search can help.

The asset file we are looking for is located at 
``GameFiles\pakchunk0-WindowsNoEditor\Astro\Content\Items\ItemTypes\FloodLight_IT.uasset`` and
``GameFiles\pakchunk0-WindowsNoEditor\Astro\Content\Items\ItemTypes\FloodLight_IT.uexp``.
It is important to remember that each asset is split into two files and you always need to keep
both together. Now we are going to copy the two files over to our folder so that we can modify them.
Copy them to
``TutorialMod\000-TutorialMod-0.1.0_P\Astro\Content\Items\ItemTypes\FloodLight_IT.uasset`` and
``TutorialMod\000-TutorialMod-0.1.0_P\Astro\Content\Items\ItemTypes\FloodLight_IT.uexp``.
You will have to create the folders if they do not exist yet. It is very import that the files have
the exact same folder structure as the game.

Never directly modify the files in your ``GameFiles`` folder.  Should you wish to create a different
mod utilizing those same files, you will have to recreate the entire GameFiles folder if you 
accidentially save over the original files.

Modifying the Asset
-------------------

Now open the ``FloodLight_IT.uasset`` file in the ``TutorialMod`` folder using UAssetGUI.

Each asset is made from multiple sections. The most interesting ones are usually ``Name Map``,
``Import Data`` and most importantly the ``Export Data`` section. Assets can have a few to hundreds
of exports. Exports are roughly labeled with what they contain. Finding the correct export for
what you are looking for requires some practice. For small assets like this one, you can go to
View > Expand All to see all the subproperties of the asset.

Here you will quickly spot the ``ItemCatalogData`` export. This is the export that contains
information like unlock cost and other stuff relevant for the catalog. If you are unable to find what 
you are looking for, you also can search for specific text in UAssetGUI by pressing ``ctrl+f`` and 
typing your text into the search bar. 

Each export has multiple subproperties arranged in a tree structure. You can see the subproperties by 
expanding the export. Click on ``ItemCatalogData(7)`` to see the contents of the catalog data. 
This export has a very simple structure.

At the very top of the table view there is an ``UnlockCost`` property with a number to the right.
Simply the change the number to what amount of bytes you would like the floodlight to cost. For
example, ``500``.

Then simply press ``Ctrl+S`` to save the file, or click on ``File > Save``.  You should see two 
``.bak`` files appear.  Ignore these files, they are simply backup files and are ignored by the 
software.

Packing the Mod
---------------

Close UAssetGUI and go back to your ``TutorialMod`` folder.  Right click on the 
``000-TutorialMod-0.1.0_P`` folder, then select ``Send to > Repack folder with repak``. 
This will create a ``000-TutorialMod-0.1.0_P.pak`` file in the same folder.

Installing the Mod
------------------

Double-check that the .pak file in the ``TutorialMod`` folder is named correctly.  If not, rename it 
to ``000-TutorialMod-0.1.0_P.pak``.  Once you have verified that the name is correct, simply drag the 
newly created ``000-TutorialMod-0.1.0_P.pak`` file onto the Modloader window and you should see 
``Tutorial Mod`` appear in the mod list. Make sure it is enabled in the modloader window.

Verifying the mod works
-----------------------

If the mod loads into the modloader without issues, start your game. If the modloader has issues loading the mod, double check that you followed the tutorial exactly.  Then head to the catalog and check that the floodlight is now costs 500 bytes.  If your mod has other issues that you need additional help with, joining the Astroneer Modding Discord is your best bet.
