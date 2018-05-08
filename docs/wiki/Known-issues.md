### New Roslyn based compiler maxes out CPU, slowing down everything

Recently, we updated the .NET build process to use the new VS 2015 toolset. The primary reason is to give access to new C# 6 compiler features like string interpolation.

Unfortunately, we found an issue with compiler that can in some cases cause it to max out the CPU, and that can continue even after the build is complete. See[this issue](https://github.com/projectkudu/kudu/issues/1693).

We are actively discussing with the compiler team, and they will hopefully address the issue. But in the meantime, there are workarounds that can be used to avoid it.

#### Disable the 'shared compilation' mode of the new compiler

By default, the compiler starts a server process (`vbcscompiler.exe`), and sends work to it. This server keeps running across compilations. The bug is related to how this server communicate via a named pipe.

You can easily disable this behavior by adding the following App Settings in the Azure portal: 

    SCM_BUILD_ARGS=/p:UseSharedCompilation=false

#### Go back to the older compiler

As an alternative, if you are not using any VS 2015 features, you can back to the previous compiler. Add the following App Settings in the Azure portal:

    MSBUILD_PATH=D:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe


### You may get occasional "Couldn't reserve space for cygwin's heap" errors when git pushing

This is tracked by https://github.com/projectkudu/kudu/issues/878.

The full error looks like this:

```
remote:       0 [main] us 0 init_cheap: VirtualAlloc pointer is null, Win32 error 487
remote: AllocationBase 0x0, BaseAddress 0x68570000, RegionSize 0x4A0000, State 0x10000
remote: D:\Program Files (x86)\Git\bin\sh.exe: *** Couldn't reserve space for cygwin's heap, Win32 error 0
To https://xxx.scm.azurewebsites.net:443/xxx.git
 * [new branch]      master -> master
```

It seems to be a general msysgit issue that people run into occasionally (not related to Azure Web Apps). e.g. see various [references](https://www.google.com/search?q=VirtualAlloc+pointer+is+null&rlz=1C1CHFX_enUS498US498&oq=VirtualAlloc+pointer+is+null&aqs=chrome..69i57&sourceid=chrome&espv=210&es_sm=93&ie=UTF-8) to it. From our experience, this happens at most once on a given server, so I suspect you won't hit this continually (though you may get moved to a different server later).

The workaround is to make a dummy commit (e.g. using `--allow-empty` so you don't have to change any files) and push again. If you want to keep your repo clean, you can then reset that commit and force push.

Our hope is that this issue is fixed in the newest msysgit (version 2.5.x). Right now, Kudu is still using an older build (1.8.4). Unless we find some issues with using the newer build, we're planning to move to it before the end of 2015, and hopefully this will take care of this.
