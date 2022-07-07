Adding Missions
===============

.. contents:: Contents
    :depth: 3

Adding the Mission Trailhead
----------------------------

Right click in the **Content Browser** and add a folder called ``Missions``.

Inside this folder create a **Data Asset**

.. note::
    **Data Asset** is located inside **Miscellaneous** in the right click menu.

Here select the ``AstroMissionDataAsset`` class and name your asset ``MissionTrailhead-TutorialMod``.

Inside this asset you can define as many missions as you want, click on "+" to add a new mission.

Here we will fill out some data to tell Astroneer where to put our mission.

* **MissionId**: ``TutorialMod-TestItemMission``
* **MissionCategory**: ``TutorialMod``
* **Description**: ``A mission that unlocks TestItem``
* **Notification Color**: ``Astro Blue``
* **Byte Reward Value**: ``1000``
* **Notification Icon**: ``ui_icon_nug_astronium``

- **Notification Color** is the color of the notification that will be shown when you complete the mission.
- **Notification Icon** icon of the notification that will be shown when you complete the mission.

- **Prerequisite Missions** is a list of missions that must be completed before this mission can be completed.
- **Next missions** missions that will be unlocked after this mission is completed.

Now let's actually add objectives, for this tutorial we will be requiring the player to collect 2 pieces of clay.

Press "+" on **Objectives**

* **Description**: ``Collect 2 pieces of clay``
* Add a new **Target type** and set it to **Clay**
* **Value**: ``2.0``
* **Progress Notify Threshold**: ``2.0``
* **Objective Type**: ``Harvest Resource``

- **Value** determines the amount of resource we want to collect for this objective
- **Progress Notify Threshold** determines the amount of resource we need to collect to get the progress notification.

And now we can go ahead and add the reward, in this case we will give the player the TestItem.

Press "+" on **Rewards** and set the reward type to be ``TestItem_IT`` and the value to 1.

This should provide us with a basic mission for the player to complete.

Now we must add it to our mod.

Adding Mission Trailhead to the Mod
-----------------------------------

As usual cook the content and move it to the mod folder, ``metadata.json`` will be used from :doc:`developingFirstMod` with some changes.

We need to add this to our metadata for the modloader to add it into Astroneer mission system.

.. code-block:: JSON

    "mission_trailheads": [
        "/Game/Examples/TutorialMod/Missions/MissionTrailhead-TutorialMod"
    ]

So the file looks like this:

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
        },
        "mission_trailheads": [
            "/Game/Examples/TutorialMod/Missions/MissionTrailhead-TutorialMod"
        ]
    }

Now cook the mod as in :doc:`developingFirstMod` and check it out!