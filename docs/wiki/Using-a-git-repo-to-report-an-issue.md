When you run into an issue, it is often helpful to provide a repro scenario to the people who will be investigating it. The best way of doing this is to create a git repository that captures the issue, and share publicly on GitHub.

## The benefits of using a git repo to share your repro

### It unambiguously captures the state of your code

A common alternative is to provide some sequence of steps. e.g. I opened VS, I created a new project etc... The problem here is that there can be many variables that can sneak into this process, causing others to get different behavior. It is also much more time consuming for others to run through a long sequence of steps.

By contrast, if you share a Git repo, we are guaranteed that everyone looking at the issue is using exactly the same code, by simply cloning it.

### It allows others to easily look at your code and comment on it

With your code in a GitHub repo, anyone can easily take a look, and can also comment on specific line, where conversations can happen. This is far easier than trying to talk about a certain line of code from the 'outside' (e.g. in a detached forum thread).

### Other can make suggestions via pull requests

If someone finds what they think is a problem in your code, instead if describing it in words, they can very easily create a pull request, which will then show a very clean diff of what they're suggesting. It also makes commenting on the change easy.

If if the change looks good, you can just merge in the pull request and verify yourself whether that solves your issue.


## What makes a good repro repository

It's one thing to share a git repo, but it needs to be built carefully to be used optimally.

### It should be as simple as you can make it

If you're seeing an issue in a really complex situation that involves tons of technologies, but the issue only relates to a small aspect of it, please try to come up with a repro case that doesn't do any more than it has to. There are two benefits to this.

First, it makes things easier on people investigating, as it directly gets their focus on the right thing. They will spend less time investigating, and are more likely to be helpful.

The second one is more subtle: as part of putting together this simpler repro, you will yourself see things more clearly, and you may very well discover the root of your problem (yes, this does happen a lot!). Or at the very least, you'll get clearer understanding of the scope of the problem, allowing you to discuss it more productively (i.e. no *"my site is broken, help me!"*).

### Keep it free of secrets

That goes without saying, but make sure you don't publicly share a repo that has your database password! If your original app has secrets, try to change your minimal repro so it doesn't need them. Or if that's not possible, explain to others how they can set things up with their own secrets (hopefully that's a rare occurrence).

### Don't commit all your binaries!

This is particularly true of .NET apps that use NuGet and have tons of DLLs in `bin`. The best way to do this is to copy a `.gitignore` file at the root before you start committing files (here is an [example](https://github.com/projectkudu/kudu/blob/master/.gitignore) from Kudu).

With this, a clean clone of your repo should pretty much just contain text files (and maybe a few png's).

### Test your scenario end to end

Before you get others to look at your test repo, please make sure that you try the scenario end to end using it. Always use a clean clone here to make sure that nothing is affected by uncommitted files that you may have around.

### Outline the repro steps

Even though the repo captures all the code, you still need to explain what others should be doing with it. e.g. *"push it to a new azure web site, run it, click here, and it blows up"*.