# How to Contribute

# Contributing
There are many ways to contribute to the 'Outsystems Integration' project: logging bugs, submitting pull requests, reporting issues, and creating suggestions.

After cloning and building the repo, check out the [issues list](https://github.com/OutSystems/vsts/issues?utf8=%E2%9C%93&q=is%3Aopen+is%3Aissue).  

## Build and Run From Source

If you want to understand how VSTS extensions work or want to debug an issue, you'll want to get the source, build it, and debug it. We recommend using a VSTS agent, so execution can be local.

### Installing Prerequisites

You'll need [git](https://git-scm.com) and a recent version of [Node.JS](https://nodejs.org/en/) (any `v7.2.1+` is recommended with npm `v3.10.10`+). 

**Visual Studio Code**
If you are running in OS X, Windows or Linux we recommend using VSCode as main IDE, based on experience from development team. However, feel free to use any other IDE.

After you have your development environment ready, run the following commands to check out vsts-outsystems  and install dependencies:

```bash
git clone https://github.com/OutSystems/vsts
cd VSTS\SRC\outsystems-integration
npm install 
npm run install-task-lib
```

> **Note:** For more information on how to install NPM modules globally on UNIX systems without resorting to `sudo`, refer to [this guide](http://www.johnpapa.net/how-to-use-npm-global-without-sudo-on-osx/).

## Pull Requests
To enable us to quickly review and accept your pull requests, always create one pull request per issue and [link the issue in the pull request](https://github.com/blog/957-introducing-issue-mentions). Never merge multiple requests in one unless they have the same root cause. Be sure to follow our [[Coding Guidelines|Coding-Guidelines]] and keep code changes as small as possible. Avoid pure formatting changes to code that has not been modified otherwise. Pull requests should contain tests whenever possible. 

### Where to Contribute
Check out the [full issues list](https://github.com/OutSystems/vsts/issues?utf8=%E2%9C%93&q=is%3Aopen+is%3Aissue) for a list of all potential areas for contributions. Note that just because an issue exists in the repository does not mean we will accept a contribution to the core editor for it.

To improve the chances to get a pull request merged you should select an issue that is labelled with the [`help-wanted`](https://github.com/OutSystems/vsts/issues?q=is%3Aopen+is%3Aissue+label%3A%22help+wanted%22) or [`bug`](https://github.com/OutSystems/vsts/issues?q=is%3Aopen+is%3Aissue+label%3A%22bug%22) labels. If the issue you want to work on is not labelled with `help-wanted` or `bug`, you can start a conversation with the issue owner.

## Suggestions
We're also interested in your feedback for the future of the 'Outsystems-Integration' extension. You can submit a suggestion or feature request through the issue tracker.  

## Discussion Etiquette

In order to keep the conversation clear and transparent, please limit discussion to English and keep things on topic with the issue. Be considerate to others and try to be courteous and professional at all times.