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

// # Sample for  DoVoid API     
// Void an order or an authorization.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class DoVoidSample
{
    // # Static constructor for configuration setting
    static DoVoidSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(DoVoidSample));


    //#DoVoid API Operation
    //#Void an order or an authorization. 
    public DoVoidResponseType DoVoidAPIOperation() 
    {
        // Create the DoVoidResponseType object
        DoVoidResponseType responseDoVoidResponseType = new DoVoidResponseType();

        try
        {
            // Create the DoVoidReq object
            DoVoidReq doVoid = new DoVoidReq();

            // DoVoidRequest which takes mandatory params:
            //
            // * `Authorization ID` - Original authorization ID specifying the
            // authorization to void or, to void an order, the order ID.
            // `Important:
            // If you are voiding a transaction that has been reauthorized, use the
            // ID from the original authorization, and not the reauthorization.`
            DoVoidRequestType doVoidRequest = new DoVoidRequestType("9B2288061E685550E");
            doVoid.DoVoidRequest = doVoidRequest;

            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the DoVoid method in service wrapper object
            responseDoVoidResponseType = service.DoVoid(doVoid);

            if (responseDoVoidResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "DoVoid API Operation - ";
                acknowledgement += responseDoVoidResponseType.Ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseDoVoidResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Authorization identification number you specified in the request
                    logger.Info("Authorization ID : " + responseDoVoidResponseType.AuthorizationID + "\n");
                    Console.WriteLine("Authorization ID : " + responseDoVoidResponseType.AuthorizationID + "\n");

                }
                // # Error Values
                else
                {
                    List<ErrorType> errorMessages = responseDoVoidResponseType.Errors;
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
        return responseDoVoidResponseType;
    }
       
}

