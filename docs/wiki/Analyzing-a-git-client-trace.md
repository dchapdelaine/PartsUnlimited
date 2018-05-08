After enabling the git trace ([[Investigating issues]]) and performing a git clone or push, you should see something like this:

    trace: built-in: git 'push' 'http://dfowler-nb.redmond.corp.microsoft.com:35954/NuGet199c8e.git' 'master'
    trace: run_command: 'git-remote-http' 'http://dfowler-nb.redmond.corp.microsoft.com:35954/NuGet199c8e.git' 'http://dfowler-nb.redmond.corp.microsoft.com:35954/NuGet199c8e.git'
    trace: run_command: 'send-pack' '--stateless-rpc' '--helper-status' '--thin' 'http://dfowler-nb.redmond.corp.microsoft.com:35954/NuGet199c8e.git/' 'refs/heads/master:refs/heads/master'
    * Couldn't find host dfowler-nb.redmond.corp.microsoft.com in the _netrc file; using defaults
    * About to connect() to dfowler-nb.redmond.corp.microsoft.com port 35954 (#0)
    *   Trying 10.80.181.12... * connected
    * Connected to dfowler-nb.redmond.corp.microsoft.com (10.80.181.12) port 35954 (#0)
    * 0x1f8ce40 is at send pipe head!
    > GET /NuGet199c8e.git/info/refs?service=git-receive-pack HTTP/1.1
    User-Agent: git/1.7.7.msysgit.0.5.ge59f1
    Host: dfowler-nb.redmond.corp.microsoft.com:35954
    Accept: */*
    Pragma: no-cache

    < HTTP/1.1 200 OK
    < Cache-Control: must-revalidate, no-cache, max-age=0
    < Pragma: no-cache
    < Transfer-Encoding: chunked
    < Content-Type: application/x-git-receive-pack-advertisement
    < Expires: Tue, 01 Jan 1980 00:00:00 GMT
    < Server: Microsoft-IIS/7.5
    < X-AspNet-Version: 4.0.30319
    < X-Powered-By: ASP.NET
    < Date: Tue, 24 Apr 2012 01:11:05 GMT
    < 
    * Connection #0 to host dfowler-nb.redmond.corp.microsoft.com left intact
    trace: built-in: git 'send-pack' '--stateless-rpc' '--helper-status' '--thin' 'http://dfowler-nb.redmond.corp.microsoft.com:35954/NuGet199c8e.git/' 'refs/heads/master:refs/heads/master'
    trace: run_command: 'pack-objects' '--all-progress-implied' '--revs' '--stdout' '--thin' '--delta-base-offset'
    trace: built-in: git 'pack-objects' '--all-progress-implied' '--revs' '--stdout' '--thin' '--delta-base-offset'
    remote: New deployment received.[K
    remote: Updating branch 'master'.[K
    remote: Preparing deployment for commit id '1d120b515b'.[K
    remote: Building web project 'MvcAppWithNuGet19.csproj'.[K
    remote: C:\dev\github\kudu\Kudu.FunctionalTests\bin\Debug\apps\NuGet199c8e\live\repository\.nuget\nuget.targets(58,9): error : Package restore is disabled by default. To give consent, open the Visual Studio Options dialog, click on Package Manager node and check 'Allow NuGet to download missing packages during build.' You can also give consent by setting the environment variable 'EnableNuGetPackageRestore' to 'true'. [C:\dev\github\kudu\Kudu.FunctionalTests\bin\Debug\apps\NuGet199c8e\live\repository\MvcAppWithNuGet19\MvcAppWithNuGet19.csproj][K
    remote: C:\dev\github\kudu\Kudu.FunctionalTests\bin\Debug\apps\NuGet199c8e\live\repository\.nuget\nuget.targets(58,9): error MSB3073: The command ""C:\dev\github\kudu\Kudu.FunctionalTests\bin\Debug\apps\NuGet199c8e\live\repository\.nuget\nuget.exe" install "C:\dev\github\kudu\Kudu.FunctionalTests\bin\Debug\apps\NuGet199c8e\live\repository\MvcAppWithNuGet19\packages.config" -source "" -o "C:\dev\github\kudu\Kudu.FunctionalTests\bin\Debug\apps\NuGet199c8e\live\repository\packages"" exited with code 1. [C:\dev\github\kudu\Kudu.FunctionalTests\bin\Debug\apps\NuGet199c8e\live\repository\MvcAppWithNuGet19\MvcAppWithNuGet19.csproj][K
    remote: 
    remote: Error - Changes committed to remote repository but your website not updated.[K
    * Couldn't find host dfowler-nb.redmond.corp.microsoft.com in the _netrc file; using defaults
    * About to connect() to dfowler-nb.redmond.corp.microsoft.com port 35954 (#0)
    *   Trying 10.80.181.12... * connected
    * Connected to dfowler-nb.redmond.corp.microsoft.com (10.80.181.12) port 35954 (#0)
    > POST /NuGet199c8e.git/git-receive-pack HTTP/1.1
    User-Agent: git/1.7.7.msysgit.0.5.ge59f1
    Host: dfowler-nb.redmond.corp.microsoft.com:35954
    Accept-Encoding: deflate, gzip
    Content-Type: application/x-git-receive-pack-request
    Accept: application/x-git-receive-pack-result
    Content-Length: 823407

    < HTTP/1.1 200 OK
    < Cache-Control: no-cache, max-age=0, must-revalidate
    < Pragma: no-cache
    < Transfer-Encoding: chunked
    < Content-Type: application/x-git-receive-pack-result
    < Expires: Fri, 01 Jan 1980 00:00:00 GMT
    < Server: Microsoft-IIS/7.5
    < X-AspNet-Version: 4.0.30319
    < X-Powered-By: ASP.NET
    < Date: Tue, 24 Apr 2012 01:11:05 GMT
    < 
    * Connection #0 to host dfowler-nb.redmond.corp.microsoft.com left intact
    * Closing connection #0
    To http://dfowler-nb.redmond.corp.microsoft.com:35954/NuGet199c8e.git
     * [new branch]      master -> master


It's alot of noise but the only thing you need to know is how to read requests and responses:

 ">" denotes a request and "<" denotes a response:

    > GET /NuGet199c8e.git/info/refs?service=git-receive-pack HTTP/1.1
        User-Agent: git/1.7.7.msysgit.0.5.ge59f1
        Host: dfowler-nb.redmond.corp.microsoft.com:35954
        Accept: */*
        Pragma: no-cache

        < HTTP/1.1 200 OK
        < Cache-Control: must-revalidate, no-cache, max-age=0
        < Pragma: no-cache
        < Transfer-Encoding: chunked
        < Content-Type: application/x-git-receive-pack-advertisement
        < Expires: Tue, 01 Jan 1980 00:00:00 GMT
        < Server: Microsoft-IIS/7.5
        < X-AspNet-Version: 4.0.30319
        < X-Powered-By: ASP.NET
        < Date: Tue, 24 Apr 2012 01:11:05 GMT
        <

See [[Anatomy of a git request]] for more details.

## Git errors

### Error in sideband demultiplexer

    fatal: The remote end hung up unexpectedly
    error: error in sideband demultiplexer

The most likely cause of this error is a timeout. This can happen when the request becomes idle for deveral minutes (i.e. no response data is sent back).

### bad line length character

If you see the following:

    fatal: protocol error: bad line length character

The timeout above is still relevant but when we try to write to the response, ASP.NET hits a null reference and fails. In the git command line output there will be an error but the operation will still continue on the server.

### Did you run git update-server-info on the server

    fatal: myapp.git/info/refs not found: did you run git update-server-info on the server?

1. Make sure you're using the right url.
2. Do you see a 404?
