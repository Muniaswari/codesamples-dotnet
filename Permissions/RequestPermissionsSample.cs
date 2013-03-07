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

// # Sample for RequestPermissions API  
// Use the RequestPermissions API operation to request permissions to execute API operations on a PayPal account holder's behalf.
// This sample code uses Permissions .NET SDK to make API call. You can
// download the SDKs [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class RequestPermissionsSample
{
    // # Static constructor for configuration setting
    static RequestPermissionsSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(RequestPermissionsSample));

    // # RequestPermissions API Operation
    // Use the RequestPermissions API operation to request permissions to execute API operations on a PayPal account holder’s behalf. 
    public RequestPermissionsResponse RequestPermissionsAPIOperation()
    {
        // Create the RequestPermissionsResponse object
        RequestPermissionsResponse responseRequestPermissions = new RequestPermissionsResponse();
        
        try
        {
            // # RequestPermissionsRequest
            // `Scope`, which can take at least 1 of the following permission
            // categories:
            // 
            // * EXPRESS_CHECKOUT
            // * DIRECT_PAYMENT
            // * AUTH_CAPTURE
            // * AIR_TRAVEL
            // * TRANSACTION_SEARCH
            // * RECURRING_PAYMENTS
            // * ACCOUNT_BALANCE
            // * ENCRYPTED_WEBSITE_PAYMENTS
            // * REFUND
            // * BILLING_AGREEMENT
            // * REFERENCE_TRANSACTION
            // * MASS_PAY
            // * TRANSACTION_DETAILS
            // * NON_REFERENCED_CREDIT
            // * SETTLEMENT_CONSOLIDATION
            // * SETTLEMENT_REPORTING
            // * BUTTON_MANAGER
            // * MANAGE_PENDING_TRANSACTION_STATUS
            // * RECURRING_PAYMENT_REPORT
            // * EXTENDED_PRO_PROCESSING_REPORT
            // * EXCEPTION_PROCESSING_REPORT
            // * ACCOUNT_MANAGEMENT_PERMISSION
            // * INVOICING
            // * ACCESS_BASIC_PERSONAL_DATA
            // * ACCESS_ADVANCED_PERSONAL_DATA
            List<String> scopeList = new List<String>();
            scopeList.Add("INVOICING");
            scopeList.Add("EXPRESS_CHECKOUT");

            // Create RequestPermissionsRequest object which takes mandatory params:
            // 
            // * `Scope`
            // * `Callback` - Your callback function that specifies actions to take
            // after the account holder grants or denies the request.
            RequestPermissionsRequest requestRequestPermissions = new RequestPermissionsRequest(scopeList, "http://localhost/callback");

            // Create the service wrapper object
            PermissionsService service = new PermissionsService();

            // # API call      
            // Invoke the RequestPermissions method in service wrapper object
            responseRequestPermissions = service.RequestPermissions(requestRequestPermissions);

            if (responseRequestPermissions != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "Request Permissions API Operation - ";
                acknowledgement += responseRequestPermissions.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseRequestPermissions.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // # Redirecting to PayPal
                    // Once you get the success response, user needs to redirect to
                    // paypal to authorize. Construct the `redirectUrl` as follows,
                    // 
                    //     redirectURL=https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_grant-permission&request_token="+responseRequestPermissions.token;
                    // 
                    // Once you are done with authorization, you will be returning
                    // back to `callback` url mentioned in your request. While
                    // returning, PayPal will send two parameters in request:
                    // 
                    // * `request_token`
                    // * `token_verifier`
                    // You have to use these values in `GetAccessToken` API call to
                    // generate `AccessToken` and `TokenSecret`
                }
                // # Error Values                
                else
                {
                    List<ErrorData> errorMessages = responseRequestPermissions.error;
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
        return responseRequestPermissions;
    }
}