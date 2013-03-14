// # Namespaces  
using System;
using System.Collections.Generic;
// # NuGet Install          
// Visual Studio 2012 and 2010 Command:  
// Install-Package PayPalMerchantSDK        
// Visual Studio 2005 and 2008 (NuGet.exe) Command:   
// install PayPalMerchantSDK      
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using log4net;

// # Sample for DoAuthorization API     
// Authorize a payment.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs
// [here](https://github.com/paypal/sdk-packages/tree/gh-pages/merchant-sdk/dotnet)
public class DoAuthorizationSample
{
    // # Static constructor for configuration setting
    static DoAuthorizationSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(DoAuthorizationSample));

    //# DoAuthorization API Operation
    //Authorize a payment. 
    public DoAuthorizationResponseType DoAuthorizationAPIOperation()
    {
        // Create the DoAuthorizationResponseType object
        DoAuthorizationResponseType responseDoAuthorizationResponseType = new DoAuthorizationResponseType();

        try
        {
            // Create the DoAuthorizationReq object
            DoAuthorizationReq doAuthorization = new DoAuthorizationReq();

            // `Amount` which takes mandatory params:
            // 
            // * `currencyCode`
            // * `amount`
            BasicAmountType amount = new BasicAmountType(CurrencyCodeType.USD, "4.00");

            // `DoAuthorizationRequest` which takes mandatory params:
            // 
            // * `Transaction ID` - Value of the order's transaction identification
            // number returned by PayPal.
            // * `Amount` - Amount to authorize.
            DoAuthorizationRequestType doAuthorizationRequest = new DoAuthorizationRequestType("O-4VR15106P7416533H", amount);
            doAuthorization.DoAuthorizationRequest = doAuthorizationRequest;

            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the DoAuthorization method in service wrapper object
            responseDoAuthorizationResponseType = service.DoAuthorization(doAuthorization);

            if (responseDoAuthorizationResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "DoAuthorization API Operation - ";
                acknowledgement += responseDoAuthorizationResponseType.Ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseDoAuthorizationResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Authorization identification number
                    logger.Info("Transaction ID : " + responseDoAuthorizationResponseType.TransactionID + "\n");
                    Console.WriteLine("Transaction ID : " + responseDoAuthorizationResponseType.TransactionID + "\n");

                }
                // # Error Values                
                else
                {
                    // Access error values from error list using getter methods
                    List<ErrorType> errorMessages = responseDoAuthorizationResponseType.Errors;
                    foreach (ErrorType error in errorMessages)
                    {
                        logger.Debug("API Error Message : " + error.LongMessage);
                        Console.WriteLine("API Error Message : " + error.LongMessage + "\n");
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
        return responseDoAuthorizationResponseType;
    }

}  
    
    
    