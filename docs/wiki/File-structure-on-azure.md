See [[Accessing files via FTP]] for info on getting at all the files that relate to your site.

In there, you will see the following structure (or at least a subset of it):


    /
        LogFiles
            Application
                <instance>-<pid>-<ticks>.txt // application (nodejs/dotnet/php) traces
            DetailedError
                ErrorPage####.htm // error details
            Git
                trace
                    trace.xml // Trace generated during git deployments
                    <instance>-<guid>.txt // kudu related traces
                deployment
                    <instance>-<guid>.txt // deployment related traces
            http
                RawLogs
                    <logfile>.log     // iis http log. iis buffers and flushes every 60sec.
            W3SVC#####
                fr####.xml            // IIS fail request traces
                freb.xsl
        site
            wwwroot
                hello.htm             // The files that are live in your app
            repository                // Your repo, including working files (i.e. not bare)
                .git
                    HEAD, index and other git files
            deployments
                [commit id 1]
                    log.xml           // The deployment log, similar to what the protal shows
                    status.xml        // The status of the deployment (success/failure)
                    manifest          // The list of files that were deployed
                [commit id 2]
                    ...
        .ssh
            config                // This contains config to disable strict host checking
            id_rsa                // private key in PEM format
            known_hosts           // known hosts have been accepted
        Data
            Jobs
                Triggered
                    MyTriggeredJob1
                        20131112101559
                            output.log
                            error.log
                            status

                Continuous
                    MyContinuousJob1
                        job.log
                        status
        SiteExtensions
            // TODO: add details here