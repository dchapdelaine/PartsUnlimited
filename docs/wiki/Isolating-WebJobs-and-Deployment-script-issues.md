When you write a WebJob, you can use it to run arbitrary logic. e.g. it can be a batch file that launches arbitrary exes. Likewise, when writing a custom deployment script, you can run arbitrary logic. And the same is also true when writing running code inside an Azure Function.

Sometimes, things that you do from your WebJob or script may not work as you expect. In most cases, when that happens, the issue really has nothing to do with WebJobs, Functions or Deployment (or even Kudu), but it has to do with the ability to run what you need within the context of the [[Azure Web App sandbox]].

When you're in that situation, instead of directly reporting the problem as a Deployment or WebJob issue, the first step is to try running the same logic outside of those context, which will help isolate the steps.

The simplest way to do this is to use [[Kudu Console]]. In the Console, you can easily drag and drop any tool you want to test from your local machine, and run them directly.

Then, when it's time to report the issue, you will be able to provide much simpler and targeted repro steps that will help speed up the investigation.