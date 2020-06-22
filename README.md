# Karting Game

## Table of contents
* [Overview](#overview)
* [Using Git](#using-git)
* [Miscellaneous](#miscellaneous)

## Overview
This is a 3D, low-poly racing game made in Unity.

## Using Git
To get this repository, you need to make it local on your computer. You can clone it with https (you have to enter your password and username each time you do anything) or clone it with ssh (you have to set up ssh on your computer before anything will work).
Press the green button that says clone or download and get that link.

Also you need git installed. Native on linux / mac, have to download on windows.

### Clone
Go to the terminal with git installed and type:

    git clone thingyoucopied

To work on anything, make a branch.

### Make a branch
to make this branch type:

    git checkout -b your-branch-name

Now you make your changes to the code.
### Add to remote
Once something is working, you should submit it to the master branch.

To do this, you need to add it to your repo, commit it, and push it

    git add .
    git commit -m "message here"
    git push origin your-branch-name

now it should appear as a merge request
### Update with remote
Now each time someone makes an update, the master branch will change to have this new stuff  
You want to make sure you also have this new stuff  
So, you should keep your branch updated with the master branch  
To do this:

    git checkout master
    git pull
    git checkout your-branch-name
    git merge origin/master

Other than that, just push from your own branch.
You probably wont break it.
If you have questions I might know but I will definitely just search it up online.

## Miscellaneous

### Links
