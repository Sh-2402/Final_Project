## ----- Unity Game Project -----

This project is a Unity-based game. It includes core gameplay systems such as player movement, combat mechanics, equipment handling, and level design elements using low-poly assets.

The goal of the project is to build a functional and expandable game while learning and applying game development concepts using Unity.
## ----- Technologies Used -----

Unity Engine
C#
Unity Asset Store
Git & GitHub for version control

## ----- Prerequisites -----

Make sure you have installed:

Unity Hub
The correct Unity version (check ProjectSettings/ProjectVersion.txt)
Git

## ----- Installation Steps -----

Clone the repository:

git clone https://github.com/yourusername/your-repository-name.git
Open Unity Hub
Click "Add Project" and select the cloned folder
Open the project using the correct Unity version

## ----! Purple Materials Issue !----

If objects appear purple, it usually means:

Missing or broken shaders
Render Pipeline mismatch (URP vs Built-in)

- Fix:

Ensure both users are using the same Render Pipeline
Reimport materials or upgrade them via:
Edit → Render Pipeline → Universal Render Pipeline → Upgrade Materials


Note: This project is solely for education purposes
