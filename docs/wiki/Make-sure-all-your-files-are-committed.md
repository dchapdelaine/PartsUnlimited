Sometimes, users report that everything works fine on their machine, but doesn't run correctly when they 'git push' to Azure. While there are many possible reasons for this, one common theme is that they have uncommited files on their client that are necessary for things to run. Hence it is important to remember that **only committed files end up on Azure**.

## How to test that all your files are committed

The best way to eliminate that possibility is to test your site using a clean copy of your repository on the client. One simple way to do this is to clone your repo locally. e.g. if your repo is in the `MySite` folder, try running:

    git clone MySite MySiteClean

One you've done that, try running your site locally using this new `MySiteClean` repo instead of your original repo. If you find that it's broken, then you most likely didn't commit some required files, and your issue is unrelated to Azure. The next step is to figure out what files you're missing by comparing your `MySite` and `MySiteClean` folders.