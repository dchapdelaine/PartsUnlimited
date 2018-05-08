## Filing issues

Filing issues is a great way to contribute to Kudu.

Here are some guidelines:

- Include as much detail as you can be about the problem
- Point to a test repository (e.g. hosted on GitHub) that can help reproduce the issue. This works better then trying to describe step by step how to create a repro scenario.
- Make it clear whether the issue relates to running on Azure Websites, or to Kudu running in your own environment.

Github supports [markdown](http://github.github.com/github-flavored-markdown/), so when filing bugs make sure you check the formatting before clicking submit. 

## Contributing code

### Coding guidelines

We follow the [ASP.NET webstack coding guidelines](http://aspnetwebstack.codeplex.com/wikipage?title=CodingConventions)

### Development workflow

We actively develop in the **master** branch. This means that all pull requests by contributors need to be developed and submitted to the master branch.

When we release bits to Azure, we create a tag for it.

### Submitting Pull requests

You will need to sign a [Contributor License Agreement](http://cla.dotnetfoundation.org/) before submitting your pull request. To complete the Contributor License Agreement (CLA), you will need to submit a request via the form and then electronically sign the Contributor License Agreement when you receive the email containing the link to the document. This needs to only be done once for any Microsoft Open Technologies OSS project.

Make sure you can build the code. Familiarize yourself with the project workflow and our coding conventions. If you don't know what a pull request is
read this https://help.github.com/articles/using-pull-requests.

Before submitting a feature or substantial code contribution please discuss it with the team and ensure it follows the product roadmap. Note that all code submissions will be rigorously reviewed and tested by the Kudu Team, and only those that meet the
bar for both quality and design/roadmap appropriateness will be merged into the source.
