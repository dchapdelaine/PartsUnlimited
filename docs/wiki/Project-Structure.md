Kudu Project Structure

- **Kudu.Client** - Client APIs for Kudu web services
- **Kudu.Core** - Core Kudu API. Includes source control management, deployment management etc.
- **Kudu.Contracts** - Interfaces that Kudu.Client and Kudu.Core share.
- **Kudu.SiteManagement** - IIS Site management layer (CRUD operations for IIS sites).
- **Kudu.Web** - Front end to manage Kudu site (UI etc)
- **Kudu.Services** - Kudu's web service layer. Exposes services like deployment, editing and source control management over http.
- **Kudu.Services.Web** - The host project for the Kudu.Services.
- **Kudu.Setup** - Builds an msi for Kudu.Services.Web.