First, make sure you look at the [[Getting started]] page for the list of prerequisites, including those under `Additional prerequisites to run the functional tests`.

All tests use xunit; this includes the unit tests and functional tests. 

## Unit tests
To run the unit tests just use build.cmd or use the VS 2012 XUnit.net runner (or [Test Driven .NET](http://www.testdriven.net/)) to run them. They are in `Kudu.Core.Test`.

## Functional tests
The functional tests are located in the `Kudu.FunctionalTests` project. Running them requires the following software to be installed.

The functional tests take about 10 minutes to run.

Note that all tests end up reusing the same IIS application to avoid creating a new one for each. By default, it's called `TestRunnerSite`. You can configure that under `Kudu.FunctionalTests\App.config`.
