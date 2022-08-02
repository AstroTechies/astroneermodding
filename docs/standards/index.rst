Modding standards
=================

.. contents:: Contents
    :depth: 3

.. include:: ../notice.rst

.. _rfcnote:

The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT", "SHOULD", "SHOULD NOT", 
"RECOMMENDED",  "MAY", and "OPTIONAL" in this document are to be interpreted as described in 
`RFC 2119 <https://tools.ietf.org/html/rfc2119>`_.

.. _general:

General Information
-------------------

All mods are Unreal Engine 4 .pak files. The .pak file format is used by the Unreal Engine for storing large amounts of data in a compact space. 
When placed into the ``%localappdata%\Astro\Saved\Paks`` directory, these .pak files are loaded by Astroneer as patches, or partial replacements, 
of the main game assets found in the ``Astro-WindowsNoEditor.pak`` file.

.. _standards:

.. toctree:: 
    :caption: Standards
    :maxdepth: 1

    pakFile
    indexFile
    metadatav2