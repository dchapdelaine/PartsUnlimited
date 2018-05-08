```INI
    # Kudu Deployment Configuration
    
    # Select whether to use the node script or batch file script as the deployment script for kudu
    USE_NODE_SCRIPT = 0
    
    # Add extra arguments/flags to msbuild command
    MSBUILD_COMMAND_EXTRA_ARGUMENTS =
    
    # Enable next line to override default arguments
    # MSBUILD_WEB_APPLICATION_ARGUMENTS = "%PROJECT_PATH%" /nologo /verbosity:m /t:pipelinePreDeployCopyAllFilesToOneFolder /p:_PackageTempDir="%TEMPORARY_PATH%";AutoParameterizationWebConfigConnectionStrings=false;Configuration=Release /p:SolutionDir="%SOLUTION_PATH%" %MSBUILD_COMMAND_EXTRA_ARGUMENTS%
    
    # Build command for .net web application
    MSBUILD_WEB_APPLICATION_COMMAND = %MSBUILD_PATH% %MSBUILD_WEB_APPLICATION_ARGUMENTS%
    
    # Enable next line to override default arguments
    # MSBUILD_WEB_SITE_ARGUMENTS = "%SOLUTION_PATH%" /verbosity:m /nologo %MSBUILD_COMMAND_EXTRA_ARGUMENTS%
    
    # Build command for .net web site
    MSBUILD_WEB_SITE_COMMAND = %MSBUILD_PATH% %MSBUILD_WEB_SITE_ARGUMENTS%
    
    # Smart copy command
    SMART_COPY_COMMAND = %NODE_PATH% %SMART_COPY_SCRIPT_PATH%
    
    # Download npm packages command
    DOWNLOAD_NPM_PACKAGES_COMMAND = %NPM_PATH% install --production
