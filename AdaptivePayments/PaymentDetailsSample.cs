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

// # Sample for PaymentDetails API  
// Use the PaymentDetails API operation to obtain information about a payment. You can identify the payment by your tracking ID, the PayPal transaction ID in an IPN message, or the pay key associated with the payment.
// This sample code uses AdaptivePayments .NET SDK to make API call. You can
// download the SDK [here](https://github.com/paypal/sdk-packages/tree/gh-pages/adaptivepayments-sdk/dotnet)
public class PaymentDetailsSample
{
    // # Static constructor for configuration setting
    static PaymentDetailsSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(PaymentDetailsSample));
        
    // # PaymentDetails API Operation
    // Use the PaymentDetails API operation to obtain information about a payment. You can identify the payment by your tracking ID, the PayPal transaction ID in an IPN message, or the pay key associated with the payment. 
    public PaymentDetailsResponse PaymentDetailsAPIOperation()
    {
        // Create the PaymentDetailsResponse object
        PaymentDetailsResponse responsePaymentDetails = new PaymentDetailsResponse();

        try
        {
            // # PaymentDetailsRequest
            // The code for the language in which errors are returned
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            // PaymentDetailsRequest which takes,
            // `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            PaymentDetailsRequest requestPaymentDetails = new PaymentDetailsRequest(envelopeRequest);

            // You must specify either,
            //
            // * `Pay Key` - The pay key that identifies the payment for which you want to retrieve details. This is the pay key returned in the PayResponse message.
            // * `Transaction ID` - The PayPal transaction ID associated with the payment. The IPN message associated with the payment contains the transaction ID.
            // `payDetailsRequest.transactionId = transactionId`
            // * `Tracking ID` - The tracking ID that was specified for this payment in the PayRequest message.
            // `requestPaymentDetails.trackingId = trackingId`
            requestPaymentDetails.payKey = "AP-86H50830VE600922B";

            // Create the service wrapper object to make the API call
            AdaptivePaymentsService service = new AdaptivePaymentsService();

            // # API call
            // Invoke the PaymentDetails method in service wrapper object
            responsePaymentDetails = service.PaymentDetails(requestPaymentDetails);

            if (responsePaymentDetails != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "PaymentDetails API Operation - ";
                acknowledgement += responsePaymentDetails.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responsePaymentDetails.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // The status of the payment. Possible values are:
                    //
                    // * CREATED - The payment request was received; funds will be
                    // transferred once the payment is approved
                    // * COMPLETED - The payment was successful
                    // * INCOMPLETE - Some transfers succeeded and some failed for a
                    // parallel payment or, for a delayed chained payment, secondary
                    // receivers have not been paid
                    // * ERROR - The payment failed and all attempted transfers failed
                    // or all completed transfers were successfully reversed
                    // * REVERSALERROR - One or more transfers failed when attempting
                    // to reverse a payment
                    // * PROCESSING - The payment is in progress
                    // * PENDING - The payment is awaiting processing
                    logger.Info("Payment Execution Status : " + responsePaymentDetails.status + "\n");
                    Console.WriteLine("Payment Execution Status : " + responsePaymentDetails.status + "\n");
                }
                // # Error Values
                else
                {
                    List<ErrorData> errorMessages = responsePaymentDetails.error;
                    foreach (ErrorData error in errorMessages)
                    {
                        logger.Debug(error.message);
                        Console.WriteLine(error.message + "\n");
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
        return responsePaymentDetails;
    }

}
