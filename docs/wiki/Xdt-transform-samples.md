Note: this section applies to both Pre-Installed and Private [[site extensions|Azure Site Extensions]].

There is a very cool Site Extension called [IIS Manager](https://github.com/shibayan/IISManager) which lets you very easily create XDT files simply by editing your applicationhost.config. This page contains a bunch of sample XDT's, but if you don't find what you want, it's pretty easy to just create your own, as long as you know the IIS applicationhost.config syntax.

The simplest way to apply one of the transforms below for your site is:

- Use [[Kudu Console]] to create an `applicationHost.xdt` file under your 'site' folder (i.e. `d:\home\site\applicationHost.xdt`), and copy the content from the samples below into it

See [Xml Document Transform](http://msdn.microsoft.com/en-us/library/dd465326.aspx) for detailed documentation on the XDT syntax.

### Adding environment variables

The following will inject an environment variable named `FOO`, with value `BAR`, and add a folder to the `PATH`:
```xml
<?xml version="1.0"?> 
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform"> 
  <system.webServer> 
    <runtime xdt:Transform="InsertIfMissing">
      <environmentVariables xdt:Transform="InsertIfMissing">
        <add name="FOO" value="BAR" xdt:Locator="Match(name)" xdt:Transform="InsertIfMissing" />    
        <add name="PATH" value="%PATH%;%HOME%\FolderOnPath" xdt:Locator="Match(name)" xdt:Transform="InsertIfMissing" />    
      </environmentVariables>
    </runtime> 
  </system.webServer> 
</configuration> 
```


### Adding new applications to the 'scm' site

You may wonder how Kudu or other extensions gets set up in the SCM site. The key is the applicationHost.xdt (notice one exists for each versioned folders). The [Xml Document Transform](http://msdn.microsoft.com/en-us/library/dd465326.aspx) file is used to transform the actual applicationHost.config for the site. 

This transform adds a /somepath IIS application under the SCM site.

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.applicationHost>
    <sites>
      <site name="%XDT_SCMSITENAME%" xdt:Locator="Match(name)">
        <application path="/somepath" xdt:Transform="Insert">
          <virtualDirectory path="/" physicalPath="%XDT_EXTENSIONPATH%" />
        </application>
      </site>
    </sites>
  </system.applicationHost>
</configuration>
```

### Adding new applications to the 'main' site

It is a variation of the above but simply adds a /somepath IIS application under the main site (`%XDT_SITENAME%`) instead.

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.applicationHost>
    <sites>
      <site name="%XDT_SITENAME%" xdt:Locator="Match(name)">
        <application path="/somepath" xdt:Transform="Insert">
          <virtualDirectory path="/" physicalPath="%XDT_EXTENSIONPATH%" />
        </application>
      </site>
    </sites>
  </system.applicationHost>
</configuration>
```

### Adding new `sub` applications to the 'main' site

It is a variation of the above but simply adds a /somepath/subpath IIS application under the main site (`%XDT_SITENAME%`).

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.applicationHost>
    <sites>
      <site name="%XDT_SITENAME%" xdt:Locator="Match(name)">
        <application path="/somepath/subpath" xdt:Transform="Insert">
          <virtualDirectory path="/" physicalPath="%XDT_EXTENSIONPATH%" />
        </application>
      </site>
    </sites>
  </system.applicationHost>
</configuration>
```

### Changing the number of segments allowed in the URL

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.applicationHost>
    <sites>
      <site name="%XDT_SITENAME%" xdt:Locator="Match(name)">
        <limits xdt:Transform="Remove" />
        <limits xdt:Transform="Insert" maxUrlSegments="64" />
      </site>
    </sites>
  </system.applicationHost>
</configuration>
```

### Adding a mime type to the httpCompression section

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <httpCompression>
      <dynamicTypes>
        <add mimeType="application/foo" enabled="true" xdt:Transform="Insert" />
      </dynamicTypes>
    </httpCompression>
  </system.webServer>
</configuration>
```

### Turning off `noCompressionForProxies` attribute

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <httpCompression xdt:Transform="SetAttributes(noCompressionForProxies)" noCompressionForProxies="false" >
    </httpCompression>
  </system.webServer>
</configuration>
```

### Increase the queueLength for your application pool

Note: the default is 1000

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.applicationHost>
    <applicationPools>
      <add name="%XDT_SITENAME%" xdt:Locator="Match(name)" xdt:Transform="SetAttributes(queueLength)" queueLength="5000">
      </add>
    </applicationPools>
  </system.applicationHost>
</configuration>
```

### Adding or changing an attribute for a specific version of PHP

This transform finds the `<application>` tag that has the v5.4 full path, and adds a new `queueLength` attribute to it.

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <fastCgi>
      <application xdt:Locator="Match(fullPath)" xdt:Transform="SetAttributes(queueLength)"
            fullPath="D:\Program Files (x86)\PHP\v5.4\php-cgi.exe" queueLength="5000"/>
    </fastCgi>
  </system.webServer>
</configuration>
```

This transform changes the value of `maxInstances` for PHP 5.4.

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <fastCgi>
      <application xdt:Locator="Match(fullPath)" xdt:Transform="SetAttributes(maxInstances)"
            fullPath="D:\Program Files (x86)\PHP\v5.4\php-cgi.exe" maxInstances="8"/>
    </fastCgi>
  </system.webServer>
</configuration>
```

### Using a custom php.ini

Use the following technique if you need to make changes to php.ini that can't be overridden in user.ini. The steps assume PHP 5.5, so tweak in the obvious way for other versions.

- In the Kudu console, click on the planet-looking icon.
- From there go in the `config` folder, and then in the `PHP-5.5.18` folder (or whatever version you're using).
- Run `copy php.ini d:\home\site` to copy it to your site folder.
- Click the Home icon, and then go in the site folder to find your copy of php.ini.
- Edit it and make any changes you need. e.g. to disable impersonation, comment out the line that has `fastcgi.impersonate=1`.
- Now, deploy the `applicationhost.xdt` below to that same `site` folder (changing 5.5 to other version if needed). It will cause PHP to use your php.ini instead of the default.
- Restart your site.

```xml
<?xml version="1.0"?> 
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform"> 
  <system.webServer> 
    <fastCgi>
      <application fullPath="D:\Program Files (x86)\PHP\v5.5\php-cgi.exe" xdt:Locator="Match(fullPath)">
        <environmentVariables>
          <environmentVariable name="PHPRC" xdt:Locator="Match(name)" value="d:\home\site\php.ini" xdt:Transform="SetAttributes(value)" />
        </environmentVariables>
      </application>
    </fastCgi>
  </system.webServer> 
</configuration> 
```

### Adding an ASP Classic attribute

e.g. this enables parent paths

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <asp xdt:Transform="SetAttributes(enableParentPaths)" enableParentPaths="true" />
  </system.webServer>
</configuration>
```
### Registering an IIS Native HttpModule

e.g. this enables native module (native module must be registered at globalModules)

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <globalModules>
      <add name="Your Module Name" image="<path to dll such as d:\home\SiteExtensions\MyNative\module.dll>" preCondition="bitness32" xdt:Locator="Match(name)" xdt:Transform="InsertIfMissing" />
    </globalModules>
  </system.webServer>
</configuration>
```

### Registering an IIS Managed HttpModule

e.g. this enables SomeModule on the main site

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <location path="%XDT_SITENAME%" xdt:Locator="Match(path)">
    <system.webServer xdt:Transform="InsertIfMissing">
      <modules xdt:Transform="InsertIfMissing">
        <add name="SomeModule" type="SomeModule.SomeModuleType" xdt:Locator="Match(name)" xdt:Transform="InsertIfMissing"/>
      </modules>
    </system.webServer>
  </location>
</configuration>
```

### Allowing arbitrary ISAPI extensions to be loaded

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <security>
      <isapiCgiRestriction xdt:Transform="SetAttributes(notListedIsapisAllowed)" notListedIsapisAllowed="true"/>
    </security>
  </system.webServer>
</configuration>
```

### Adding IP restrictions

This will apply IP restrictions on both the main site and Scm site. To only make it apply to the Main site, you can add a location attribute as in the "Registering an IIS Managed HttpModule" sample.

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <security>
      <ipSecurity xdt:Transform="RemoveAttributes(allowUnlisted)">
        <add ipAddress="204.79.197.200" allowed="true" xdt:Transform="Insert"/>
      </ipSecurity>
    </security>
  </system.webServer>
</configuration>
```

### Only log successful http requests to the IIS log (aka Web Server log)

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <httpLogging xdt:Transform="SetAttributes(selectiveLogging)" selectiveLogging="LogSuccessful" />
  </system.webServer>
</configuration>
```

### Change the number of failed request files that get kepts

The default is 50, and the following changes it to 100.

Note that the cleanup happens is a kind of unusual way. When `maxLogFiles` is reached, half the files get deleted. So for example when set to 100, you'll have between 50 and 100 at any given time.

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.applicationHost>
    <sites>
      <site name="%XDT_SITENAME%" xdt:Locator="Match(name)">
        <traceFailedRequestsLogging xdt:Transform="Remove" />
        <traceFailedRequestsLogging xdt:Transform="Insert" enabled="true" customActionsEnabled="true" directory="D:\home\LogFiles" maxLogFileSizeKB="4096" maxLogFiles="100" />
      </site>
    </sites>
  </system.applicationHost>
</configuration>
```

### Log failed request trace only for a certain status code

The following makes FREB logs only for `409` requests

```
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <tracing>
      <traceFailedRequests>
        <add path="*" xdt:Locator="Match(path)">
          <failureDefinitions xdt:Transform="SetAttributes(statusCodes)" statusCodes="409" />
        </add>
      </traceFailedRequests>
    </tracing>
  </system.webServer>
</configuration>
```

### Enable Web Sockets

The following does the equivalent of enabling Web Sockets in the portal

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer>
    <globalModules>
      <add name="WebSocketModule" xdt:Locator="Match(WebSocketModule)" xdt:Transform="Remove" />
      <add name="WebSocketModule" image="%windir%\System32\inetsrv\iiswsock.dll" xdt:Transform="Insert" />
    </globalModules>
    <webSocket xdt:Transform="SetAttributes(pingInterval)" pingInterval="00:02:30" />
  </system.webServer>
  <location path="" xdt:Locator="Match(path)" xdt:Transform="InsertIfMissing">
    <system.webServer xdt:Transform="InsertIfMissing">
      <modules xdt:Transform="InsertIfMissing">
        <add name="WebSocketModule" xdt:Locator="Match(WebSocketModule)" xdt:Transform="Remove" />
        <add name="WebSocketModule" lockItem="true" xdt:Transform="Insert" />
      </modules>
    </system.webServer>
  </location>
</configuration>
```

### Add a rewrite rule

Add a rule that returns a 403 if a certain http header is present

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.webServer xdt:Transform="InsertIfMissing">
    <rewrite xdt:Transform="InsertIfMissing">
      <rules xdt:Transform="InsertIfMissing">
        <rule name="MyRule" xdt:Locator="Match(name)" xdt:Transform="RemoveAll"/>
        <rule name="MyRule" xdt:Locator="Match(name)" patternSyntax="Wildcard" stopProcessing="true" xdt:Transform="InsertIfMissing">
          <match url="*" />
          <conditions>
              <add input="{HTTP_X_FORWARDED_SSL30}" pattern="1" />
          </conditions>
          <action type="CustomResponse" statusCode="403" subStatusCode="900" statusReason="Forbidden" statusDescription="Forbidden" />
        </rule>
      </rules>
    </rewrite>    
  </system.webServer>
</configuration>
```

### Redirect http traffic to https

**Update (11/1/2017)**: there is now a new `httpsOnly` flag on the site object that you can set to true to achieve this in a much easier way. You can use ARM, PowerShell, or Resource Explorer to set it. There is also UI for it in the Azure Portal.

*Original sample for historical purpose*

This redirects all http traffic to https. It also makes sure that the site warmup request gets through, which makes things work correctly in site swap and Always On scenarios.

```xml
<?xml version="1.0"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <location path="%XDT_SITENAME%" xdt:Transform="InsertIfMissing" xdt:Locator="Match(path)">
    <system.webServer xdt:Transform="InsertIfMissing">
      <applicationInitialization xdt:Transform="InsertIfMissing">
        <add initializationPage="/"  xdt:Transform="InsertIfMissing"/>
      </applicationInitialization>

      <rewrite xdt:Transform="InsertIfMissing">
        <rules xdt:Transform="InsertIfMissing">
          <rule name="Force HTTPS" enabled="true" stopProcessing="true">
            <match url="(.*)" ignoreCase="false" />
            <conditions>
              <add input="{HTTPS}" pattern="off" />
              <add input="{WARMUP_REQUEST}" pattern="1" negate="true" />
            </conditions>
            <action type="Redirect" url="https://{HTTP_HOST}/{R:1}" appendQueryString="true" redirectType="Permanent" />
          </rule>
        </rules>
      </rewrite>    
    </system.webServer>
  </location>
</configuration>
```

### Add an allowedServerVariables

Add `CONTENT_TYPE` to the list of allowed variables:

```xml
<?xml version="1.0"?> 
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform"> 
  <system.webServer> 
    <rewrite>
      <allowedServerVariables>
        <add name="CONTENT_TYPE" xdt:Transform="InsertIfMissing" />
      </allowedServerVariables>
    </rewrite>
  </system.webServer>
</configuration>
```