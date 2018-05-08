If you're a developer who wants to work on the Kudu source code and submit your changes for consideration to be merged into the master branch, here's how. This page was inspired by [this guide](https://github.com/dradis/dradisframework/wiki/Working-with-git).

## If you're not familiar with Git

Maybe this will help:

http://eagain.net/articles/git-for-computer-scientists/

## Do's and Don't's

If you're familiar with git and GitHub, here's the short version of what you need to know. Once you fork and clone the Kudu code:

* Don't develop on the master branch. Always create a development branch specific to the issue you're working on. Name it by issue # and description. For example, if you're working on Issue #26, a Skipfish import plugin, your development branch should be called 26-skipfish-plugin. If you decide to work on another issue mid-stream, create a new branch for that issue—don't work on both in one branch.

* Do not merge the upstream master with your development branch; rebase your branch on top of the upstream master.

* A single development branch should represent changes related to a single issue. If you decide to work on another issue, create another branch.


## Step-by-step

1. Fork on GitHub (click Fork button)
2. Clone to computer ($ git clone git@github.com:you/kudu.git )
3. Don't forget to cd into your repo: ($ cd kudu/ )
4. Set up remote upstream ($ git remote add upstream git://github.com/projectkudu/kudu.git)
5. Create a branch for new issue ($ git checkout -b 100-new-feature, if you don't have a bug report no worries just skip the number)
6. Develop on issue branch. [Time passes, the main Kudu repository accumulates new commits]
7. Commit changes to issue branch. ($ git add . ; git commit -m 'commit message')
8. Fetch upstream ($ git fetch upstream)
9. Update local master ($ git checkout master; git pull upstream master)
10. Repeat steps 6-8 till dev is complete
11. Rebase issue branch ($ git checkout 100-new-feature; git rebase master)
12. Push branch to GitHub ($ git push origin 100-new-feature)
13. Issue pull request (Click Pull Request button)

## Squash commits before pushing to master

To keep our history clean, it's best to squashing commits before pushing to master, unless it makes sense to have separate commits.

e.g.
- "First part of work" and "second part of work" is fine, BUT
- "Fixed this issue", followed by "oh crap fixed typo" is dirty, and adds no value to the history line

You can find simple instructions [here](http://stackoverflow.com/questions/6934752/git-combining-multiple-commits-before-pushing), using interactive rebasing.


