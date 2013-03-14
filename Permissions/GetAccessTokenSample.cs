// # Namespaces 
using System;
using System.Collections.Generic;
using System.IO;
// # NuGet Install        
// Visual Studio 2012 and 2010 Command:  
// Install-Package PayPalPermissionsSDK    
// Visual Studio 2005 and 2008 (NuGet.exe) Command:  
// install PayPalPermissionsSDK    
using PayPal.Permissions;
using PayPal.Permissions.Model;
using log4net;

// # Sample for GetAccessToken API  
// Use the GetAccessToken API operation to obtain an access token for a set of permissions.
// This sample code uses Permissions .NET SDK to make API call. You can
// download the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/permissions-sdk/dotnet)
public class GetAccessTokenSample
{
    // # Static constructor for configuration setting
    static GetAccessTokenSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(GetAccessTokenSample));

    // # GetAccessToken API Operation
    // Use the GetAccessToken API operation to obtain an access token for a set of permissions
    public GetAccessTokenResponse GetAccessTokenAPIOperation()
    {
        // Create the GetAccessTokenResponse object
        GetAccessTokenResponse responseGetAccessToken = new GetAccessTokenResponse();

        try
        {
            // Create the GetAccessTokenRequest object
            GetAccessTokenRequest requestGetAccessToken = new GetAccessTokenRequest();

            // The request token from the response to RequestPermissions.
            requestGetAccessToken.token = "AAAAAAAXO-JZhFLpTLLe";

            // The verification code returned in the redirect from PayPal to the
            // return URL after `RequestPermissions` call
            requestGetAccessToken.verifier = "R.X1BWK7QEv-dcjQEzk2xg";

            // Create the service wrapper object
            PermissionsService service = new PermissionsService();

            // # API call 
            // Invoke the GetAccessToken method in service wrapper object
            responseGetAccessToken = service.GetAccessToken(requestGetAccessToken);

            if (responseGetAccessToken != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "GetAccessToken API Operation - ";
                acknowledgement += responseGetAccessToken.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseGetAccessToken.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    logger.Info("Access Token : " + responseGetAccessToken.token + "\n");
                    logger.Info("Token Secret : " + responseGetAccessToken.tokenSecret + "\n");
                    Console.WriteLine("Access Token : " + responseGetAccessToken.token + "\n");
                    Console.WriteLine("Token Secret : " + responseGetAccessToken.tokenSecret + "\n");
                }
                // # Error Values            
                else
                {
                    List<ErrorData> errorMessages = responseGetAccessToken.error;
                    foreach (ErrorData error in errorMessages)
                    {
                        logger.Debug("API Error Message : " + error.message);
                        Console.WriteLine("API Error Message : " + error.message + "\n");
                    }
                }
            }
        }
        // # Exception log    
        catch (System.Exception ex)
        {
            // Log the exception message       
            logger.Debug("Error Message : " + ex.Message);
            Console.WriteLine("Error Message : " + ex.Message);
        }
        return responseGetAccessToken;
    }

}