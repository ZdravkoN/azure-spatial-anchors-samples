namespace SharingService.Core.Services.Token
{
    public class TokenServiceSettings
    {
        // Azure Spatial Anchors configuration
        public string SpatialAnchorsAccountId { get; set; } = "<Spatial Anchors Account Id>"; // Account ID from Azure Spatial Anchors account
        public string SpatialAnchorsResource { get; set; } = "https://sts.mixedreality.azure.com";

        // AAD configuration
        public string AadClientId { get; set; } = "<AAD client id>"; // Application ID from AAD registration
        public string AadClientKey { get; set; } = "<AAD client key>"; // Application key from AAD registration
        public string AadTenantId { get; set; } = "<AAD Tenant ID>"; //  Specify the Azure tenant ID in which the application was registered
    }
}
