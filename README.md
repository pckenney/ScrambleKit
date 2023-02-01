Base code that I use for game jams.

Most recently updated for Unity 2020.3.4f1

## To install when you have an existing Unity project:

Open the Unity package manager

Click the + button and choose "Add package from git URL"

Use this url: https://github.com/pckenney/ScrambleKit.git?path=/Packages/com.scramblekit


## My steps for starting an empty project

*Your workflow may differ...*

Create a new projet in Unity

Get that project into a new repo. I'm hosting private repos on bitbucket, so I follow this doc to intialize:
https://confluence.atlassian.com/bitbucketserver/importing-code-from-an-existing-project-776640909.html#Importingcodefromanexistingproject-Importanexisting,unversionedcodeproject

Except I turn off the "Create gitignore" option, and copy the .gitignore from this repo into the new project before doing a git add.

Then I follow the steps above to install scramblekit.

## Things I end up doing in every project

If I'm using 2D art, I set a default for sprite properties: https://answers.unity.com/questions/1128274/how-to-change-default-import-settings.html

If I'm using Raycast2D as part of my character controller: Edit -> Project Settings -> Physics2D -> UNCHECK Queries Start In Colliders

## Other Useful Links

- http://dotween.demigiant.com/documentation.php
- https://easings.net/
- https://pbs.twimg.com/media/C3O3vckWcAAVzmU.jpg

