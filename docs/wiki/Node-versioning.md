This page discusses a number of aspects of Node versioning on App Service

## Installation structure

### Root node

There is a 'root' Node installed directly in `D:\Program Files (x86)\nodejs`, which is an ancient version: `v0.6.20`. There is no legitimate use for this version.

**Improvement**: we should clean this up.

### Selection of Node versions under that

Then under that folder, there is one subfolder for each supported version. e.g. `v8.8.1` is in `D:\Program Files (x86)\nodejs\8.8.1`.

### Removed versions

When we need to remove a version from the VM, instead of just deleting the folder, we create a symlink to a different version, so that
apps pointing to the deleted version have a chance to continue working.

You can see this by running 'dir' under `D:\Program Files (x86)\nodejs` in Kudu Console. e.g.

    10/26/2017  04:04 AM    <SYMLINKD>     5.8.0 [D:\Program Files (x86)\nodejs\6.11.2]
    10/26/2017  04:04 AM    <SYMLINKD>     5.9.1 [D:\Program Files (x86)\nodejs\6.11.2]

**Improvement**: instead of relying on symlinks, it may be better to have smart behavior on App Starts that picks a best match version.


## The `WEBSITE_NODE_DEFAULT_VERSION` App Setting

### Case where it's missing

In the absence of this app setting, we use the root Node, which is `v0.6.20`. This is bad.

**Improvement**: instead of using the default, it should use the latest. Current behavior was to avoid a breaking change earlier. But now that `v0.6.20` is for all practical purpose unusable, we can change the behavior.

### Case where it points to a version that doesn't exist

If the version doesn't exist, the site is 100% busted with 500.

**Improvement**: instead of just failing, it should use the closest match

### Semver range support

Today, `WEBSITE_NODE_DEFAULT_VERSION` must be set to an exact value, e.g. `8.8.1`

**Improvement**: it could support some subset of semver ranges, e.g. ~8 for the latest 8.x.