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

// # Sample for Refund API 
// Use the Refund API operation to refund all or part of a payment.
// This sample code uses AdaptivePayments .NET SDK to make API call. You can
// download the SDK [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class RefundSample
{
    // # Static constructor for configuration setting
    static RefundSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(RefundSample));

    // # Refund API Operation
    // Use the Refund API operation to refund all or part of a payment. You can specify the amount of the refund and identify the accounts to receive the refund by the payment key or tracking ID, and optionally, by transaction ID or the receivers of the original payment. 
    public RefundResponse RefundAPIOperation()
    {
        // Create the RefundResponse object
        RefundResponse responseRefund = new RefundResponse();

        try
        {
            // # RefundRequest
            // The code for the language in which errors are returned
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            // RefundRequest which takes,
            // `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            RefundRequest requestRefund = new RefundRequest(envelopeRequest);

            // You must specify either,
            //
            // * `Pay Key` - The pay key that identifies the payment for which you
            // want to retrieve details. This is the pay key returned in the
            // PayResponse message.
            // * `Transaction ID` - The PayPal transaction ID associated with the
            // payment. The IPN message associated with the payment contains the
            // transaction ID.
            // `requestRefund.transactionId = transactionId`
            // * `Tracking ID` - The tracking ID that was specified for this payment
            // in the PayRequest message.
            // `requestRefund.trackingId = trackingId`
            requestRefund.payKey = "AP-86H50830VE600922B";

            // Create the service wrapper object to make the API call
            AdaptivePaymentsService service = new AdaptivePaymentsService();

            // # API call
            // Invoke the Refund method in service wrapper object
            responseRefund = service.Refund(requestRefund);

            if (responseRefund != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "Refund API Operation - ";
                acknowledgement += responseRefund.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseRefund.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // List of refunds associated with the payment.
                    IEnumerator<RefundInfo> iterator = responseRefund.refundInfoList.refundInfo.GetEnumerator();

                    while (iterator.MoveNext())
                    {
                        // Represents the refund attempt made to a receiver of a
                        // PayRequest.
                        RefundInfo refundInfo = iterator.Current;

                        // Status of the refund. It is one of the following values:
                        //
                        // * REFUNDED - Refund successfully completed
                        // * REFUNDED_PENDING - Refund awaiting transfer of funds; for
                        // example, a refund paid by eCheck.
                        // * NOT_PAID - Payment was never made; therefore, it cannot
                        // be refunded.
                        // * ALREADY_REVERSED_OR_REFUNDED - Request rejected because
                        // the refund was already made, or the payment was reversed
                        // prior to this request.
                        // * NO_API_ACCESS_TO_RECEIVER - Request cannot be completed
                        // because you do not have third-party access from the
                        // receiver to make the refund.
                        // * REFUND_NOT_ALLOWED - Refund is not allowed.
                        // * INSUFFICIENT_BALANCE - Request rejected because the
                        // receiver from which the refund is to be paid does not
                        // have sufficient funds or the funding source cannot be
                        // used to make a refund.
                        // * AMOUNT_EXCEEDS_REFUNDABLE - Request rejected because you
                        // attempted to refund more than the remaining amount of the
                        // payment; call the PaymentDetails API operation to
                        // determine the amount already refunded.
                        // * PREVIOUS_REFUND_PENDING - Request rejected because a
                        // refund is currently pending for this part of the payment
                        // * NOT_PROCESSED - Request rejected because it cannot be
                        // processed at this time
                        // * REFUND_ERROR - Request rejected because of an internal
                        // error
                        // * PREVIOUS_REFUND_ERROR - Request rejected because another
                        // part of this refund caused an internal error.
                        logger.Info("Refund Status : " + refundInfo.refundStatus + "\n");
                        Console.WriteLine("Refund Status : " + refundInfo.refundStatus + "\n");
                    }
                }
                // # Error Values
                else
                {
                    List<ErrorData> errorMessages = responseRefund.error;
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
        return responseRefund;
    }

}