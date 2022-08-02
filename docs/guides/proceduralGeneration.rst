Procedural Generation
========================

.. contents:: Contents
    :depth: 3

Procedural Generation 
-----------------------

.. note:: 
    This is an advanced topic, so it is preferred that you already have some experience modding Astroneer.

Procedural generation in astroneer is done using their custom generation graph that for shipping games gets compiled to low-level code.

Thus we cannot easily change a lot of its behaviors, but what we can do, is use already existing behaviors and modify them a little bit.

To generate our object we must have a **Procedural Placement** and a **Procedural Modifier** class in our mod files.

**Procedural Modifier** is like a model of spawning, the only settings we can change are **Procedural Placement** which is perfect because that is what we will be doing.
**Procedural Placement** is placements that are actually responsible for spawning your items.

Before creating them though, we must know that there is a restriction on filenames that dictate procedural generation in your mod.

This restriction comes from the Astroneer has a lot of its generation code compiled to low-level asm.

In your modkit you can find the file that describes procedural generation names restrictions in ``Content/Mods/localcc/TPuzzle/proceduralGenerationModels.json``.

This is a json file that contains an object. Keys in this object are valid names for **Procedural Modifier** classes.

Keys can also be treated as "Models" that we can choose from to get different spawning behaviors.

Values for each key are valid names for **Procedural Placement** classes that the **Procedural Modifier** can contain.

.. note:: 
    Models work cross-biome but not cross-planet (e.g. you can use ``Forests_Terran_Props`` for the Plains biome on Terran planet, but you cannot use it for Arid planet)

Planets in this tutorial are called not by their name, but by their type, because that is how the game stores them.


To create those files, right click in the content browser go to ``Miscellaneous`` and select **Procedural Modifier**.

We will name this one ``Plains_Terran_Puzzles``.

Then right click in the content browser again, and this time select **Procedural Placement**.

We will name this one ``ObjPl_Puzzle_Surface_Terran``. 

Open this asset and set the following parameters:

* **Seed**: Set this to whatever.
* **Radius**: ``600``. Radius determines how far objects will be spawned from eachother, treat is if it was objects rarity.
* **Max Projection Distance**: ``1500`` This is probably max distance the objects can be from eachother. 

.. warning:: 
    **Radius** should always be <= **Max Projection Distance**, otherwise no objects will be spawned.

Now the fun part, spawning the objects. Open variants and click **+**.

In this variant set the following parameters:

* **Selection Weight**: ``1.0``. This probably determines the chance for this variant to spawn in case there is more than one variant.
* **Placement Actor**: The actor you want to spawn.
* **Item Type**: This is for resources, if you want custom resource nodes you should set this to the resource type you want to spawn, and **Placement Actor** to **Decorator_MineralFlecks**.
* **Orientation**: ``Align to planet up``. This determines how your object will be aligned when spawned.
* **Random Yaw**: Max object random yaw when spawning.
* **Random Pitch**: Max object random pitch when spawning.
* **Uniform scale**: Check this.
* **Scale XYZ**: Set min to ``1.0`` and max to ``1.0``. This will choose a random scale between **min** and **max** when spawning your object.
* **Density scaling slop**: Set min to ``1.0`` and max to ``1.0``.
* **Cull distance**: Set min to ``23000` and max to ``25000``. This determines how far away the spawned object can be from the camera before it is culled.
* **Enable Density Scaling**: Uncheck this.
* **Cast Shadow**: Check this. This determines whether the spawned object will cast shadow.
* **Cast Shadow as Two Sided**: Uncheck this. This determines whether the spawned object will cast shadow as if it had a two-sided shader.
* **Receives decals**: Uncheck this.
* **Use as occluder**: Uncheck this. This probably should be checked for very large objects.
* **Should override destruction effects**: Uncheck this.


After setting up the **Procedural Placement** we need to add it to the **Procedural Modifier**.

Open your **Procedural Modifier** asset and add a compiled placement. Set the compiled placement to the one we just created.

Cook the mod files and copy them over to the mod directory.


Writing the metadata
--------------------

Procedural generation requires a metadata entry to work.

This is an example of what your file should look like

.. code-block:: JSON

    {
        "schema_version": 2,
        "name": "ExampleMod",
        "mod_id": "ExampleMod",
        "author": "YOUR_NAME",
        "description": "An Example Mod",
        "version": "0.1.0",
        "sync": "serverclient",
        "integrator": {
            "item_list_entries": {
                "/Game/Items/ItemTypes/MasterItemList": {
                    "ItemTypes": [
                        "/Game/Mods/YOUR_NAME/TutorialMod/ExampleItem_IT"
                    ]
                }
            },
            "biome_placement_modifiers": [
                {
                    "planet_type": "Terran",
                    "biome_type": "Surface",
                    "biome_name": "Plains",
                    "layer_name": "None",
                    "placements": [
                        "/Game/Mods/YOUR_NAME/TutorialMod/Plains_Terran_Puzzles"
                    ]
                }
            ]
        }
    }


This will add the procedural modifier to the plains biome on a terran planet.

To get which biomes/layers you can use on which planets there is a file at ``Content/Mods/localcc/TPuzzle/biomeData.json``.

For example we want to add something to the valleys biome on exotic planet. In the file we will see something like this:

.. code-block:: JSON

    {
        "Exotic": {
            "SurfaceBiomes": {
                "Hills_Exotic": {
                    "Layers": [
                    "None"
                    ]
                },
                "Valleys_Exotic": {
                    "Layers": [
                    "None"
                    ]
                },
                "Rolling_Exotic": {
                    "Layers": [
                    "None"
                    ]
                },
                "Mountains_Exotic": {
                    "Layers": [
                    "None"
                    ]
                }
                },
                "CrustBiome": {
                "Layers": [
                    "CrustExotic1",
                    "CrustExotic2",
                    "CrustExotic3",
                    "CrustExotic4"
                ]
            }
        }
    }


To add a procedural modifier to a biome we must know ``planet_type``, ``biome_type``, ``biome_name`` and ``layer_name``.

In this file we find an object with key ``Exotic``, the key corresponds to ``planet_type``.

In this object we see two biome types, ``SurfaceBiomes`` and ``CrustBiome``. We know that valleys are a surface biome, so we look inside the ``SurfaceBiomes`` object.

Also note that this means that ``biome_type`` is ``Surface``.

Here we can see all of the avaiilable surface biomes, we want valleys so look at ``Valleys_Exotic``, this becomes our ``biome_name``.

This biome contains only one layer, but we still must specify it, in this case it's ``None``.

But now that we chose the biome, we must know that file names that we chose previously do not match the one permitted for exotic planet, this must be fixed.

Rename ``Plains_Terran_Puzzles`` to ``Valleys_Exotic_Puzzles``.

Also rename ``ObjPl_Puzzle_Surface_Terran`` to ``ObjPl_Puzzle_Surface_Exotic``.

Our metadata.json file should look something like this:


.. code-block:: JSON

    {
        "schema_version": 2,
        "name": "ExampleMod",
        "mod_id": "ExampleMod",
        "author": "YOUR_NAME",
        "description": "An Example Mod",
        "version": "0.1.0",
        "sync": "serverclient",
        "integrator": {
            "item_list_entries": {
                "/Game/Items/ItemTypes/MasterItemList": {
                    "ItemTypes": [
                        "/Game/Mods/YOUR_NAME/TutorialMod/ExampleItem_IT"
                    ]
                }
            },
            "biome_placement_modifiers": [
                {
                    "planet_type": "Exotic",
                    "biome_type": "Surface",
                    "biome_name": "Valleys_Exotic",
                    "layer_name": "None",
                    "placements": [
                        "/Game/Mods/YOUR_NAME/TutorialMod/Valleys_Exotic_Puzzles"
                    ]
                }
            ]
        }
    }

Now cook the mod and verify you see objects spawning.