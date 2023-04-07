About Astroneer Modding
========================

.. contents:: Contents
    :depth: 3

This page will give you an overview of how Astroneer mods are structured and distributed.

.. include:: ../notice.rst

About Astroneer
---------------

Astroneer uses a slightly modified version of Unreal Engine 4.23.1. Modding Unreal Engine games
can be hard, but is not impossible. The modding tools that will be shown later in this guide can 
simplify the process extensively, depending on the mod you are trying to make.

Doing small tweaks for stuff like power values is quite simple, but adding your own logic
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

Unreal Asset Files
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

Tools For Creating Mods
-----------------------

Typically, mods are created using either UassetGUI or Unreal Engine directly.  Each of these tools 
provides benefits over the other.
UassetGUI, which was originally written for Astroneer modding, is a great way to quickly generate
a mod that involves small changes to the game, such as changing numbers.  An example of this is
modifying the max speed of a rover, which is very easy to do with UassetGUI.  UassetGUI's main downside
is that it's limited in capabilities for more advanced projects, such as adding new items to the game.
Your first tutorial for making mods will utilize UassetGUI.
Unreal Engine Editor 4.23.1 is also used to create mods, utilizing an environment similar to game 
development on the platform.  While it is a bit harder to learn than UassetGUI, and does not have all
of the game files available to it (yet), it allows for much more advanced mods to be created.  Unreal
Editor also has less chance of causing mod conflicts due to how .uasset files are registered within
the modloader.
**Using Unreal Engine Editor is recommended for advanced projects.**

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

Getting Help
------------

If you ever need help or stuck with a mod you are trying to make ask in the
`Astroneer Modding Discord <https://discord.gg/bBqdVYxu4k>`_ in the #making-mods-help channel.

To get started go to the :doc:`basicSetup` section.
