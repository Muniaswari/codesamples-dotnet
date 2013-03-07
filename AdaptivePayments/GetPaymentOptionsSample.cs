// # Namespaces 
using System;
using System.Collections.Generic;
using System.IO;
// # NuGet Install        
// Visual Studio 2012 and 2010 Command:  
// Install-Package PayPalAdaptivePaymentsSDK    
// Visual Studio 2005 and 2008 (NuGet.exe) Command:  
// install PayPalAdaptivePaymentsSDK    
using PayPal.AdaptivePayments;
using PayPal.AdaptivePayments.Model;
using log4net;

// # Sample for GetPaymentOptions API 
// You use the GetPaymentOptions API operation to retrieve the payment options passed with the SetPaymentOptionsRequest.
// This sample code uses AdaptivePayments .NET SDK to make API call. You can
// download the SDK [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class GetPaymentOptionsSample
{
    // # Static constructor for configuration setting
    static GetPaymentOptionsSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(GetPaymentOptionsSample));

    // # GetPaymentOptions API Operation    
    // You use the GetPaymentOptions API operation to retrieve the payment options passed with the SetPaymentOptionsRequest. 
    public GetPaymentOptionsResponse GetPaymentOptionsAPIOperation()
    {
        // Create the  GetPaymentOptionsResponse object
        GetPaymentOptionsResponse responseGetPaymentOptions = new GetPaymentOptionsResponse();

        try
        {
            // Request envelope object
            RequestEnvelope envelopeRequest = new RequestEnvelope();

            // The code for the language in which errors are returned
            envelopeRequest.errorLanguage = "en_US";

            // GetPaymentOptionsRequest which takes,    
            //  
            // * `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            // * `Pay Key` - The pay key that identifies the payment for which you
            // want to set payment options. This is the pay key returned in the
            // PayResponse message. Action Type in PayRequest must be `CREATE`
            GetPaymentOptionsRequest getPaymentOptionsRequest = new GetPaymentOptionsRequest(
            envelopeRequest, "AP-1VB65877N5917862M");

            // Create the service wrapper object to make the API call
            AdaptivePaymentsService service = new AdaptivePaymentsService();

            // # API call   
            // Invoke the GetPaymentOptions method in service wrapper object
            responseGetPaymentOptions = service.GetPaymentOptions(getPaymentOptionsRequest);

            // Response envelope acknowledgement
            string acknowledgement = "GetPaymentOptions API Operation - ";
            acknowledgement += responseGetPaymentOptions.responseEnvelope.ack.ToString();
            logger.Info(acknowledgement + "\n");
            Console.WriteLine(acknowledgement + "\n");

            // # Success values
            if (responseGetPaymentOptions.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
            {
                // Response envelope acknowledgement
                acknowledgement = responseGetPaymentOptions.responseEnvelope.ack.ToString().Trim().ToUpper();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (acknowledgement.Equals("SUCCESS"))
                {
                    // Business Name you set in SetPaymentOptions
                    logger.Info("Business Name : " + responseGetPaymentOptions.displayOptions.businessName + "\n");
                    Console.WriteLine("Business Name : " + responseGetPaymentOptions.displayOptions.businessName + "\n");
                }
                // # Error Values
                else
                {
                    List<ErrorData> errorMessages = responseGetPaymentOptions.error;
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
        return responseGetPaymentOptions;
    }
}

