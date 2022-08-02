.. _indexfile:

Index File Standard
====================

This file contains data and direct download URLs for one or more mods. It is used to automatically find a mod's download link and
to facilitate auto-updating. All mod listings are contained in the ``mods`` field. Each field contains data for one mod and the
field key should be the ``mod_id`` of the corresponding mod.

As an example, here is a valid index file:

.. code-block:: JSON

   {
       "mods": {
           "ExampleMod": {
               "latest_version": "1.1.0",
               "versions": {
                  "1.0.0": {
                      "download_url": "https://example.com/upload/123/file.pak",
                      "filename": "000-ExampleMod-1.0.0_P.pak"
                  },
                  "1.1.0": {
                      "download_url": "https://example.com/upload/124/file.pak",
                      "filename": "000-ExampleMod-1.1.0_P.pak"
                  }
               }
           }
       }
   }
