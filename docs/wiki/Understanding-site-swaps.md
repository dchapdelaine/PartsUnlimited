**Note: you should also check out [this blog post](http://ruslany.net/2015/09/how-to-warm-up-azure-web-app-during-deployment-slots-swap/) which goes in great depth about this topic.**

When you swap two slots, there is always a source and a target. Typically, the target is the Production slot (i.e. the site itself). Swapping is performed in a way that avoids any down time for the target site. However, we've seen a fair bit of misunderstanding from users about exactly what that means, so this will try to clarify.

Here is what happens when you swap a source slot (let's call it 'Staging') into a target slot (Production).

1. First, the Staging site needs to go through some setting changes for App Setting and Connection Strings that are marked as 'slot'. There are also other changes related to source control that may need to be applied. This causes the Staging site to restart, which is fine.
1. Next, the Staging site gets warmed up, by having a request sent to its root path (i.e. '/'), and waiting for it to complete.
1. Now that the Staging site is warm, it gets swapped into Production. There is no down time, since it goes straight from one warm site to another one.
1. Finally, the site that used to be Production and is now Staging also needs to get some settings apply, causing it to restart. Again, this is fine since it happens in the staging site.

The confusion happens when users expect that their Staging site will not be restarted at all during the swap, and notice that it in fact is. But that is not normally as issue, since by the time the Staging site gets swapped, it has been warmed up.

So to summarize: the swap mechanism does not guarantee that the Staging site won't be restarted, but it does guarantee that no cold site is ever used for Production.

### What if hitting the site root is not good enough to warm up the site?

If you need additional paths to be hit to warm up your site, you can use the `applicationInitialization` feature in your web.config file, e.g.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.webServer>
    <applicationInitialization>
      <add initializationPage="/pagetowarmup1.php" />
      <add initializationPage="/pagetowarmup2.php" />
      <add initializationPage="/pagetowarmup3.php" />
    </applicationInitialization>
  </system.webServer>
</configuration>
```