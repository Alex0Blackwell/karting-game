
To get this repository, you need to make it local on your computer. You can clone it with https (you have to enter your password and username each time you do anything) or clone it with ssh (you have to set up ssh on your computer before anything will work).
Press the green button that says clone or download and get that link.

Also you need git installed. Native on linux / mac, have to download on windows.

Go to the terminal with git installed and type:

    git clone thingyoucopied

To work on anything, make a branch. Call your branch "yourname-branch" just so we can keep track of it.

to make this branch type:

    git checkout -b yourname-branch

Now you make your changes to the code.

Once something is working, you should submit it to the master branch.

To do this, you need to add it to your repo, commit it, and push it

    git add .  // adds everything
    git commit -m "message here"
    git push --set-upstream origin yourname-branch

now it should appear as a merge request

so I have to approve it because I am all powerful
If someone seems worthy of this power it will be granted

Now each time someone makes an update, the master branch will change to have this new stuff
