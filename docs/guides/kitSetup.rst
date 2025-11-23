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

Select 4.27.2 and press install.

Modding Kit
-----------

To develop your mods you will need a modkit which can be downloaded from 
`this link <https://github.com/AstroTechies/ModdingKit>`_.

If you are familiar with version control software you should clone it for easier updates.


Wwise (optional)
----------------

Astroneer uses Wwise as its sound engine. If you want to make mods which play sounds you need to install Wwise.

Go to `Wwise <https://www.audiokinetic.com/en/products/wwise>`_ website and head to ``Get Wwise`` -> ``Download Wwise``.

If you don't already have an account, create one. After wwise installed has finished downloading, open it and select WWISE in the top bar.

Click on latest and change it to ``All`` > ``2019.1`` > ``2019.1.8.7173`` and press install. Once presented with options select these:

* Packages

    * Authoring

    * SDK (C++)

* Deployment Platforms

  * Apple

      * macOS

  * Microsoft

      * Windows

      * Visual Studio 2017

      * Visual Studio 2019


After it has finished installing go to ``Unreal Engine`` tab in the top bar. There press on the burger menu and ``Browse For Project``.

Select ``Astro.uproject`` in the file picker. 

Now press ``Integrate WWise into project``. Here select ``All`` > ``2019.1`` > ``2019.1.8.7173``.

In the wwise project path field press on the triangle on the right side and click ``New``.

Now press the ``Integrate`` button.

Generating project files
-------------------------

To generate the project files we will need to run the following command:

.. code-block:: 

    "UE_INSTALL_PATH\Engine\Binaries\DotNET\UnrealBuildTool.exe" -projectfiles -project="PATH_TO_PROJECT\\Astro.uproject" -game -rocket -progress


Open cmd in your project directory and copy this inside cmd. Remember to replace **UE_INSTALL_PATH** with your unreal engine installation folder which is usually found at ``C:\\Program Files\\Epic Games\UE_4.27\\``.

And remember to change **PATH_TO_PROJECT** with path to the modkit.

Example:

.. code-block:: 

    "C:\Program Files\Epic Games\UE_4.27\Engine\Binaries\DotNET\UnrealBuildTool.exe" -projectfiles -project="C:\\Users\\username\\Documents\\Astro.uproject" -game -rocket -progress

Run the command, and then open the ``Astro.uproject`` file and if it asks to build unbuilt modules press yes and wait.

Developing Mods
---------------

You can now start with :doc:`kitModding`.