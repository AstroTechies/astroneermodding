Diegetic UI
==============

.. contents:: Contents
    :depth: 3

Making Diegetic UI
-------------------

To add diegetic UI to your object we will need another actor.

This actor will be the actor that displays our ui.

Create an actor with parent class of **ControlPanel**.

First things first we need to have a component that describes orientation when the actor is displayed to the player.

For that we will add a **Scene Component**, position and rotate it as you wish.

Set the name of this component to ``CrackedOrientation``.

For this component to actually do something we must specify its name inside **CrackableActorComponent**.

Open **CrackableActorComponent** and on the right side you should see a ``Cracked Orientation Component`` dropdown, open that dropdown.

In the dropdown set **Component Property** to be ``CrackedOrientation``.

Also set these parameters:

**Method**: Set this to ``Hover Face Camera``. This will make the object always follow camera when examining.
**Click to close**: Uncheck this. This will prevent the ui from closing when we click anywhere.
**Cracks only on examine**: Check this.
**Screen scale ratio**: Set this to ``1.8``. This determines the size of our object on screen when examining it.
**Camera space offset**: Set this to ``450.0 175.0 -20.0``. This determines the position of our object on screen.
**Pivot angle**: Set this to 0.

But this will not do anything useful unless we setup **ClickableComponent** to actually open our ui.

Open **ClickableComponent** and set the following parameters:

**Slow virtual cursor on hover**: Uncheck this.
**Has use interaction by default**: Check this.
**Use action requires hold**: Uncheck this.
**Default use context**: Set this to ``UC Examine``. This deterimines which action will be displayed to the player when they hover over the object.


Now we need something to actually render on our screen, for this we will be using this example panel :download:`panel.fbx`.

When importing this mesh to unreal make sure to check **Skeletal Mesh** under **Mesh** section and set the skeleton to None.

Now open the **Mesh** dropdown in **SkeletalMesh** component. Set **Skeletal Mesh** to be the mesh we just imported.

.. note:: 
    We are using a skeletal mesh, because if we use a static mesh for this, it will not be rendered in game.

.. warning:: 
    If the **Camera** or **Visibility** channel collision will be enabled on any of the child components in this blueprint, it will cause your camera to glitch.


Adding Control Panel to the item
---------------------------------

Now that we created the control panel it's time to add it to the item.

Open your item blueprint.

.. note:: 
    If you were following the previous tutorial it is probably ``ExampleItem_BP``

In the item blueprint add a **Child Actor** component. On this component set the **Child Actor Class** field to the control panel we created previously.

Remember to position child actor component in the viewport where you want it to be.

We also need to set up **ClickableComponent** in this blueprint too. 

Open **ClickableComponent** and set the following parameters:

**Slow virtual cursor on hover**: Uncheck this.
**Has use interaction by default**: Check this.
**Use action requires hold**: Uncheck this.
**Default use context**: Set this to ``UC Examine``. This deterimines which action will be displayed to the player when they hover over the object.


At this point, the diegetic UI is done, so cook the mod and test it!