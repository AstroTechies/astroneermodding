Setting up the Modding Kit
==========================

.. contents:: Contents
    :depth: 3

These are the requirements for making custom items with Unreal Engine and the Modding Kit.

Visual Studio
-------------

Download `Visual Studio <https://visualstudio.microsoft.com/downloads/>`_ 2017 or higher.

When running the installer, on the "Workloads" page make sure you select "Desktop & Mobile > 
Desktop development with C++" and "Gaming > Game development with C++".

If Visual Studio is already installed you can run the installer and press modify to add the
necessary workloads.


Unreal Engine 4
---------------

Open Epic Games launcher and go to "Unreal Engine" tab. 

Next go to Library and add a new engine version. 

Select 4.23.x where x can be any number, and press install.

Modding Kit
-----------

To develop your mods you will need a modkit which can be downloaded from 
`this link <https://github.com/AstroTechies/ModdingKit>`_.

If you are familiar with version control software you should clone it for easier updates.

After cloning right click the .uproject file and press "Generate Visual Studio project files"

Then open the ``Astro.uproject`` file and if it asks to build unbuilt modules press yes and wait.

Developing Mods
---------------

You can now start with :doc:`kitModding`.