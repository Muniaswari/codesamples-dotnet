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

// # Sample for SetPaymentOptions API   
// You use the SetPaymentOptions API operation to specify settings for a payment of the actionType `CREATE`. This actionType is specified in the PayRequest message.
// This sample code uses AdaptivePayments .NET SDK to make API call. You can
// download the SDK [here](https://github.com/paypal/sdk-packages/tree/gh-pages/adaptivepayments-sdk/dotnet)
public class SetPaymentOptionsSample
{
    // # Static constructor for configuration setting 
    static SetPaymentOptionsSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(SetPaymentOptionsSample));
    
    // # SetPaymentOptions API Operation
    // You use the SetPaymentOptions API operation to specify settings for a payment of the actionType CREATE. This actionType is specified in the PayRequest message. 
    public SetPaymentOptionsResponse SetPaymentOptionsAPIOperation()
    {
        // Create the SetPaymentOptionsResponse object
        SetPaymentOptionsResponse responseSetPaymentOptions = new SetPaymentOptionsResponse();

        try
        {
            // # SetPaymentOptionsRequest
            // The code for the language in which errors are returned
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            // SetPaymentOptionsRequest which takes,
            //
            // * `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            // * `Pay Key` - The pay key that identifies the payment for which you
            // want to set payment options. This is the pay key returned in the
            // PayResponse message. Action Type in PayRequest must be `CREATE`
            SetPaymentOptionsRequest requestSetPaymentOptions = new SetPaymentOptionsRequest(envelopeRequest, "AP-1VB65877N5917862M");

            // Specifies display items in payment flows and emails.
            DisplayOptions displayOptions = new DisplayOptions();

            // The business name to display      
            // The name cannot exceed 128 characters    
            displayOptions.businessName = "Toy Shop";
            requestSetPaymentOptions.displayOptions = displayOptions;

            // Create the service wrapper object to make the API call
            AdaptivePaymentsService service = new AdaptivePaymentsService();

            // # API call
            // Invoke the SetPaymentOptions method in service wrapper object
            responseSetPaymentOptions = service.SetPaymentOptions(requestSetPaymentOptions);

            if (responseSetPaymentOptions != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "SetPaymentOptions API Operation - ";
                acknowledgement += responseSetPaymentOptions.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseSetPaymentOptions.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    responseSetPaymentOptions.responseEnvelope.ack.ToString();
                }
                // # Error Values                
                else
                {
                    List<ErrorData> errorMessages = responseSetPaymentOptions.error;
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
        return responseSetPaymentOptions;
    }

    
}