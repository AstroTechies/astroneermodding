About Astroneer Moddding
========================

.. contents:: Contents
    :depth: 3

This page will give you an overview of how Astroneer mods are structured and distributed.

.. include:: ../notice.rst

About Astroneer
---------------

Astroneer uses a slightly modified version of Unreal Engine 4.23.1. Modding Unreal Engine games
is hard but not impossible.

Doing number tweaks for stuff like power values is quite simple to do but adding your own logic
or items takes a lot of time.

Unreal Pak Files
----------------

All mods (or at least what we are focusing on) are Unreal Engine 4 .pak files.
These files are comparable to .zip files. They store a bunch of compressed files inside them.
Distributing a mod is as easy as giving somebody else a .pak file. 

When put in specific directories (manged by the modloader) they will loaded by the game after the
main ``Astro-WindowsNoEditor.pak``. Then they can either replace or modify existing parts of the
game or even add new files and items to the game.

Mod .pak files also include some extra data in the ``metadata.json`` file placed at the root of the
directory structure inside. This includes information like the mod name, author, where to get
updates and some special instruction for the mod integrator, which is responsible for avoiding
conflicts between mods.

Unreal Asset files
------------------

All the actual game data like values, textures, models, some code and much more is stored in Unreal
asset files. They usually have the .uasset extension. There are two different types of asset files:

- Uncooked files. These files are used during developement and are what are stored in a Unreal
  project. They can be easily opened and modified in the Unreal Editor.

- Cooked files. These files are included in distribution game builds and have a lot of data 
  stripped. Opening and modifying these files requires special tools because they are usually
  generated from uncooked ones and not meant to be modified directly. But there are all that we
  have for modding.

In most games cooked .uasset files also come with a .uexp for the same asset. This extra file
stores the bulk of the data for optimization.

Up until the Xenobiology Update (aka. Snail Update) Astroneer did not use .uexp files and bundled
all the data in the main .uasset file. This change broke a lot of mods.

File Structure and Paths
------------------------

The files in .pak files in Unreal Engine games are always structured similarly. Here is how it is
in Astroneer.

.. code-block::

    root
    ├─Astro
    │   ├─Config
    │   │   └─...
    │   ├─Content
    │   │   ├─Animations
    │   │   │   └─...
    │   │   ├─Items
    │   │   │   └─...
    │   │   └─...
    │   └─...
    └─Engine
        └─...

All files important for the actual game are in the ``/Astro/Content`` directory. However paths in
assets don't actually directly referece that. Instead the they use the ``/Game/...`` path which
gets remapped to ``/Astro/Content/...`` at runtime. Also Unreal uses forward slashes as path
separators.

When making a mod you would place an asset file
``/Astro/Content/Mods/ModderName/MyModId/MyAsset.uasset`` in you .pak file but referece it as
``/Game/Mods/ModderName/MyModId/MyAsset.uasset`` inside assets.

Items in Astroneer
------------------

One of the most common things you might want to do is tweak or make are items. Items in Astroneer consist
of two assets, ItemType and PhysicalItem.

- ItemType is used by Astroneer to get in item's recipe, menu icon, item properties,
  description, etc. Most of them are found in ``/Game/Items/ItemTypes/``. They sometimes have the
  suffix ``_IT``.

- PhysicalItem is the object that is going to be created in the world. Most of them are found in
  ``/Game/Components_*/`` and ``/Game/Items/``. They sometimes have the suffix ``_BP``.

Also keep in mind that the Astroneer game files are very messy and it can be hard to find stuff.
Windows search is your biggest help.