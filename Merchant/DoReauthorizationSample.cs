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

// # Sample for  DoReauthorization API  
// Authorize a payment.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/merchant-sdk/dotnet)
public class DoReauthorizationSample
{
    // # Static constructor for configuration setting
    static DoReauthorizationSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(DoReauthorizationSample));

    // # DoReauthorization API Operation
    // Authorize a payment
    public DoReauthorizationResponseType DoReauthorizationAPIOperation()
    {
        // Create the DoReauthorizationResponseType object
        DoReauthorizationResponseType responseDoReauthorizationResponseType = new DoReauthorizationResponseType();

        try
        {
            // Create the DoAuthorizationReq object
            DoReauthorizationReq requestDoReauthorization = new DoReauthorizationReq();

            // `Amount` to reauthorize which takes mandatory params:
            //
            // * `currencyCode`
            // * `amount`
            BasicAmountType amount = new BasicAmountType(CurrencyCodeType.USD, "3.00");

            // `DoReauthorizationRequest` which takes mandatory params:
            //
            // * `Authorization Id` - Value of a previously authorized transaction
            // identification number returned by PayPal.
            // * `amount`
            DoReauthorizationRequestType doReauthorizationRequest = new DoReauthorizationRequestType("9B2288061E685550E", amount);
            requestDoReauthorization.DoReauthorizationRequest = doReauthorizationRequest;

            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the DoReauthorization method in service wrapper object
            responseDoReauthorizationResponseType = service.DoReauthorization(requestDoReauthorization);

            if (responseDoReauthorizationResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "DoReauthorization API Operation - ";
                acknowledgement += responseDoReauthorizationResponseType.Ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseDoReauthorizationResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Authorization identification number
                    logger.Info("Authorization ID : " + responseDoReauthorizationResponseType.AuthorizationID + "\n");
                    Console.WriteLine("Authorization ID : " + responseDoReauthorizationResponseType.AuthorizationID + "\n");
                }
                // # Error Values
                else
                {
                    List<ErrorType> errorMessages = responseDoReauthorizationResponseType.Errors;
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
        return responseDoReauthorizationResponseType;
    }
       
}


