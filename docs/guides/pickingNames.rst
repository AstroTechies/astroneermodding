Picking Names
=============

.. contents:: Contents
    :depth: 3

Naming is hard
--------------

Naming things is hard. That is something everyone has experienced at some point. But choosing good
names for your mod(s) is even harder becasue they also need to be descriptive, short and readable. So
here are some tips on how to do it right.

Allowed Characters
------------------

All IDs can generally only contain upper- and lowercase latin characters and the numbers 0-9. This
restriction exists becasue they need to work in URLs and filepaths but also to avoid confusion.

What you need to choose
-----------------------

- Your Author ID (only once of course)
- A mod ID
- A name for the mod

Author ID
---------

An Author ID is used to uniquely identify a person creating mods. It should be whatever nickname
you have choosen condensed into the allowed characters. Note that this one is rarely shown to users
and the one shown to users can contain basically any characters.

Mod Name and ID
---------------

Whatever you choose to call your mod, it has to clearly describe what the mod it/does while also
being relatively short. Especially the mod ID has to be readable to allow users to identify mod
files just by their file name.

It is recommend to use 2-3 full words. Do not use acronyms, unless they are already used by the
base game (like ``RTG``). Also avoid shortening words except for stuff like ``Astroneer`` =>
``Astro``. The mod name should include spaces (unlike the ID) and can also be a bit longer.

Here are some good examples:

- ``RocketLauncher`` for Rocket Launcher Mod
- ``LavaLamp`` for Lava Lamp Mod
- ``MoreTradables`` for Mo' Tradables

Here are some bad ones:

- ``QTRTG`` for Print QT-RTG. Here it is unclear what the mod does in the mod ID.
- ``Astronium`` for Super Astronium. Not even from the full name it is clear what the mod does.
- ``Pumpkin`` for PUMPKINS!!!. Again what does it do.
- ``6A1S``. What does this even mean?

Mod ID extension
----------------

You can extend your mod ID with your author ID like this ``modid.authorid``. In practice it looks
like this ``PickupRovers.Konsti``. This is to differentiate two mods with the same base mod ID that
are by different authors. This new string will be the new mod ID used in both filename and
metadata. Also this is the only time a single dot is allowed in IDs. Note that this is a relatively
new addition and not all programs/website will support it.