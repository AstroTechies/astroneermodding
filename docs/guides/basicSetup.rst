Setting up Modding Tools
========================

.. contents:: Contents
    :depth: 3

Setting up Folders
------------------

Start by creating a folder ``AstroneerModding`` in your PC.  A good location is your documents 
folder.  This folder must be on a drive with at least 10GB of free space. If you also want to 
make mods with Unreal Engine later you will need another 30GB.

Setting up unreal_pak_cli
-------------------------

When working with .pak files you need some kind of program to extract and create them. We will be
using the community-created ``unreal_pak_cli`` program. The source code can be found
`here <https://github.com/AstroTechies/unrealmodding/tree/main/unreal_pak_cli>`_. To download a
pre-built binary and some useful .bat files click :download:`HERE <unreal_pak_cli.zip>`. (We will
be using this)

Once you have downloaded the .zip extract it to your ``AstroneerModding`` folder. Then go into the
``unreal_pak_cli``. You should see the .exe and two .bat files. We will be adding the bat files to
the Windows Send to window to speed up the modding workflow. This is not strictly neccessary and
use the .exe like any other cli tool.

Right click both .bat files and select ``Create Shortcut``. Then press ``Win+R`` and enter
``Shell:sendto``. This will open a new window. Drag both shortcuts into that window. Then rename
them ``Repack folder with unreal_pak_cli`` and ``Unpack .pak with unreal_pak_cli``.

Extracting the Game Files
-------------------------

First create a new folder in your ``AstroneerModding`` folder called ``GameFiles``. This will be
where you will extract the game files to.

Next, you will need to find your Astroneer's .pak file by going to where you have Astroneer
installed.

- The usual location for this is 
  ``C:\Program Files (x86)\Steam\steamapps\common\ASTRONEER\Astro\Content\Paks``.

- If you have the Game installed anywhere outside the default Steam location you can find it on
  Steam by right clicking on Astroneer then going to ``Properties`` and then
  ``Local Files > Browse``. From there go to the sub folder ``Astro\Content\Paks``.
  
Copy the ``Astro-WindowsNoEditor.pak`` files to your ``GameFiles`` folder. Finally in your 
``GameFiles`` folder right click on the ``Astro-WindowsNoEditor.pak`` file and select
``Send to > Unpack .pak with unreal_pak_cli``. This may take a minute.

Setting up UAssetGUI
--------------------

UAssetGUI is tool for viewing and editing cooked .uasset files. Downlaod the latest version from
`GitHub <https://github.com/atenfyr/UAssetGUI/releases>`_. Extract the .zip file to your
``AstroneerModding`` folder.

Opening an Asset
----------------

Go to ``AstroneerModding\GameFiles\WindowsNoEditor\Astro\Content\Items\ItemTypes`` and double click
on ``FloodLight_IT.uasset``. Since you probably have not opened one of these files before Windows
will ask you for a program. Select "more apps," then "Look for another app on this PC." Browse to 
and select the ``UAssetGUI.exe`` we extracted earlier. This will set UAssetGUI as the default 
program for .uasset files, which you will need for making your first mod.

To make your first mod continue with :doc:`basicModding`.