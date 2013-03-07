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

// # Sample for PreapprovalDetails API  
// Use the PreapprovalDetails API operation to obtain information about an agreement between you and a sender for making payments on the sender's behalf.
// This sample code uses AdaptivePayments .NET SDK to make API call. You can
// download the SDKs [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class PreapprovalDetailsSample
{
    // # Static constructor for configuration setting
    static PreapprovalDetailsSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(PreapprovalDetailsSample));

    // # PreapprovalDetails API Operation
    // Use the PreapprovalDetails API operation to obtain information about an agreement between you and a sender for making payments on the sender’s behalf. 
    public PreapprovalDetailsResponse PreapprovalDetailsAPIOperation()
    {
        // Create the PreapprovalDetailsResponse object
        PreapprovalDetailsResponse responsePreapprovalDetails = new PreapprovalDetailsResponse();

        try
        {
            // # PreapprovaDetailslRequest
            // The code for the language in which errors are returned
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            // PreapprovalDetailsRequest object which takes mandatory params:
            //
            // * `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            // * `Preapproval Key` - A preapproval key that identifies the
            // preapproval for which you want to retrieve details. The preapproval
            // key is returned in the PreapprovalResponse message.
            PreapprovalDetailsRequest preapprovDetailsRequest = new PreapprovalDetailsRequest(
            envelopeRequest, "PA-1KM93450LF5424305");

            // Create the service wrapper object to make the API call
            AdaptivePaymentsService service = new AdaptivePaymentsService();

            // # API call
            // Invoke the PreapprovalDetails method in service wrapper object
            responsePreapprovalDetails = service.PreapprovalDetails(preapprovDetailsRequest);

            if (responsePreapprovalDetails != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "PreapprovalDetails API Operation - ";
                acknowledgement += responsePreapprovalDetails.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responsePreapprovalDetails.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // First date for which the preapproval is valid.
                    logger.Info("Starting Date : " + responsePreapprovalDetails.startingDate + "\n");
                    Console.WriteLine("Starting Date : " + responsePreapprovalDetails.startingDate + "\n");
                }
                // # Error Values
                else
                {
                    List<ErrorData> errorMessages = responsePreapprovalDetails.error;
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
        return responsePreapprovalDetails;
    }

}