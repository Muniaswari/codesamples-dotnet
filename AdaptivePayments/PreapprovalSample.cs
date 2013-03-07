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

// # Sample for Preapproval API 
// Use the Preapproval API operation to set up an agreement between yourself
// and a sender for making payments on the sender's behalf. You set up a
// preapprovals for a specific maximum amount over a specific period of time
// and, optionally, by any of the following constraints:
//
// * the number of payments
// * a maximum per-payment amount
// * a specific day of the week or the month
// * whether or not a PIN is required for each payment request.
//
// This sample code uses AdaptivePayments .NET SDK to make API call. You can
// download the SDK [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class PreapprovalSample
{
    // # Static constructor for configuration setting
    static PreapprovalSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(PreapprovalSample));
    
    // # Preapproval API Operation 
    // Use the Preapproval API operation to set up an agreement between yourself and a sender for making payments on the sender’s behalf. You set up a preapprovals for a specific maximum amount over a specific period of time and, optionally, by any of the following constraints: the number of payments, a maximum per-payment amount, a specific day of the week or the month, and whether or not a PIN is required for each payment request. 
    public PreapprovalResponse PreapprovalAPIOperation()
    {
        // Create the PreapprovalResponse object
        PreapprovalResponse responsePreapproval = new PreapprovalResponse();

        try
        {
            // # PreapprovalRequest
            // The code for the language in which errors are returned
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            // PreapprovalRequest takes mandatory params:
            //      
            // * `RequestEnvelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            // * `Cancel URL` - URL to redirect the sender's browser to after
            // canceling the preapproval
            // * `Currency Code` - The code for the currency in which the payment is
            // made; you can specify only one currency, regardless of the number of
            // receivers
            // * `Return URL` - URL to redirect the sender's browser to after the
            // sender has logged into PayPal and confirmed the preapproval
            // * `Starting Date` - First date for which the preapproval is valid. It
            // cannot be before today's date or after the ending date.
            PreapprovalRequest requestPreapproval = new PreapprovalRequest(envelopeRequest, "http://localhost/cancel", "USD", "http://localhost/return", "2013-12-18");

            // IPN URL
            //  
            // * PayPal Instant Payment Notification is a call back system that is initiated when a transaction is completed        
            // * The transaction related IPN variables will be received on the call back URL specified in the request       
            // * The IPN variables have to be sent back to the PayPal system for validation, upon validation PayPal will send a response string "VERIFIED" or "INVALID"     
            // * PayPal would continuously resend IPN if a wrong IPN is sent        
            requestPreapproval.ipnNotificationUrl = "http://IPNhost";

            // Create the service wrapper object to make the API call
            AdaptivePaymentsService service = new AdaptivePaymentsService();

            // # API call
            // Invoke the Preapproval method in service wrapper object
            responsePreapproval = service.Preapproval(requestPreapproval);

            if (responsePreapproval != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "Preapproval API Operation - ";
                acknowledgement += responsePreapproval.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responsePreapproval.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    logger.Info("Preapproval Key : " + responsePreapproval.preapprovalKey + "\n");
                    Console.WriteLine("Preapproval Key : " + responsePreapproval.preapprovalKey + "\n");

                    // Once you get success response, user has to redirect to PayPal
                    // to preapprove the payment. Construct redirectURL as follows,
                    // `redirectURL=https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_ap-preapproval&preapprovalkey="
                    // + responsePreapproval.preapprovalKey;`                    
                }
                // # Error Values
                else
                {
                    List<ErrorData> errorMessages = responsePreapproval.error;
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
        return responsePreapproval;
    }
    
    
}