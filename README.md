# AW_Utils

Various functions I use in avatar projects. Distrubuted with most Angelware packages. Includes some deps for AW_Platform (Private at this time).

More to come soon!

---

### AW_Seperator
Used for component organisation and notes, good for distributing avatars that could use notes and directions for users. I mainly use this especially to label physbone sections in a root gameobject. Uses VRCSDK whitelist hook for editor only functions.

### AW_SDKValidator
Does what it says on the tin. Validates SDK versions. For now I have a second script to validate VRCFury installs, although these need to be merged into a more modular script.

### AW_StartupChecks
Runs required checks on startup. Useful for triggering package validation in distributed packages.

### AW_ParameterManager
Various functions for managing VRCExpressionsParameters. Also includes a quick removal of GoGoLoco for my GodRevenger packages, just to make parameters space, as useful as Gogo is.

### AW_SceneReload
Seperated function for reloading scenes. Mostly used for adding scene tags for avatars, as tags will not update unless a scene is reloaded.