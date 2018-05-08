### This script will be run after:
- The repository was updated.
- Configuration values are put as environment settings.
- Temporary path is created (and passed as an environment setting).
- Project type is determined and project file/solution file are found (also passed as environment settings).

### Post script actions are:
- Remove temporary path.
- Add iis node configuration (if needed).
- Select node version (if needed).


    ```Batchfile
    @echo off
    
    :: ----------------------
    :: KUDU Deployment Script
    :: ----------------------
    ::
    :: INPUTS:
    :: -------
    :: REPOSITORY_ROOT_PATH
    :: TEMPORARY_DIRECTORY_PATH
    :: WEB_ROOT_PATH
    :: MANIFEST_PATH
    :: PROJECT_TYPE
    :: PROJECT_FILE_PATH
    :: SOLUTION_FILE_PATH
    
    :: Switch on the project type
    IF /I  "%PROJECT_TYPE%"=="DOTNET_WEB_APPLICATION" goto wap
    IF /I  "%PROJECT_TYPE%"=="DOTNET_WEB_SITE" goto website
    IF /I  "%PROJECT_TYPE%"=="OTHER" goto other
    goto error
    
    
    :wap
    echo Handling .NET Web Application deployment.
    
    :: Build to the temporary path
    %MSBUILD_TO_TEMP_COMMAND%
    IF %ERRORLEVEL% NEQ 0 goto error
    
    :: SmartCopy [From] [To] [Manifest]
    %SMART_COPY_COMMAND% %TEMPORARY_DIRECTORY_PATH% %WEB_ROOT_PATH% %MANIFEST_PATH%
    IF %ERRORLEVEL% NEQ 0 goto error
    
    goto end
    
    
    :website
    echo Handling .NET Web Site deployment.
    
    :: Build to the repository root path
    %MSBUILD_TO_REPOSITORY_COMMAND%
    IF %ERRORLEVEL% NEQ 0 goto error
    
    :: SmartCopy [From] [To] [Manifest]
    %SMART_COPY_COMMAND% %REPOSITORY_ROOT_PATH% %WEB_ROOT_PATH% %MANIFEST_PATH%
    IF %ERRORLEVEL% NEQ 0 goto error
    
    goto end
    
    
    :other
    echo Handling web site deployment.
    
    :: SmartCopy [From] [To] [Manifest]
    %SMART_COPY_COMMAND% %REPOSITORY_ROOT_PATH% %WEB_ROOT_PATH% %MANIFEST_PATH%
    IF %ERRORLEVEL% NEQ 0 goto error
    
    %DOWNLOAD_NPM_PACKAGES_COMMAND%
    IF %ERRORLEVEL% NEQ 0 goto error
    
    goto end
    
    
    :error
    echo An error has occured during web site deployment.
    exit /b 1
    
    
    :end
    echo Finished successfully.
