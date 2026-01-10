Setting Up the Modding Kit
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

To develop your mods, you will need to use the AstroTechies ModdingKit, which can be downloaded from 
`this link <https://github.com/AstroTechies/ModdingKit>`_.

The AstroTechies ModdingKit

If you are familiar with version control software, you may wish to clone the ModdingKit for easier updates.

Wwise (optional)
----------------
This step is not required.

Astroneer uses Wwise as its sound engine. If you want to make mods which play sounds, you may wish to install Wwise.

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

Select the ``Astro.uproject`` file from the AstroTechies ModdingKit in the file picker. 

Now press ``Integrate WWise into project``. Here select ``All`` > ``2019.1`` > ``2019.1.8.7173``.

In the wwise project path field press on the triangle on the right side and click ``New``.

Now press the ``Integrate`` button.

Generating Visual Studio project files (optional)
--------------------------------------------------
This step is not required.

If you'd like to use Visual Studio to edit or build the AstroTechies ModdingKit, simply right-click on the ``Astro.uproject`` file and select "Generate Visual Studio project files". You can then open the new "Astro.sln" file in Visual Studio and build the project as desired.

If the option does not appear, you can try to instead execute the following command on the command line:

.. code-block:: 

    "UE_INSTALL_PATH\Engine\Binaries\DotNET\UnrealBuildTool.exe" -projectfiles -project="PATH_TO_PROJECT\Astro.uproject" -game -rocket -progress


Replace **UE_INSTALL_PATH** with your Unreal Engine installation folder, which is usually found at ``C:\Program Files\Epic Games\UE_4.27\``. Replace **PATH_TO_PROJECT** with the path to your copy of the AstroTechies ModdingKit.

Example:

.. code-block:: 

    "C:\Program Files\Epic Games\UE_4.27\Engine\Binaries\DotNET\UnrealBuildTool.exe" -projectfiles -project="C:\Users\username\Documents\Astro.uproject" -game -rocket -progress

First-time Launch
--------------------------------------------------
Now, open the ``Astro.uproject`` file by double-clicking it. If you receive a pop-up asking if you would like to rebuild missing modules, press "Yes" and wait.

If double-clicking the ``Astro.uproject`` file fails to open the project, right-click on ``Astro.uproject``, select "Open with" -> "Choose another app", scroll down and click "Choose an app on your PC," and browse to and select the executable file at ``UE_INSTALL_PATH\Engine\Binaries\Win64\UE4Editor.exe``. Replace **UE_INSTALL_PATH** with your Unreal Engine installation folder, which is usually found at ``C:\Program Files\Epic Games\UE_4.27\``.

Developing Mods
---------------

You can now start with the :doc:`kitModding` guide.
