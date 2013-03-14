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

// # Sample for ExecutePayment API 
// The ExecutePayment API operation lets you execute a payment set up with the Pay API operation with the actionType CREATE. To pay receivers identified in the Pay call, set the pay key from the PayResponse message in the ExecutePaymentRequest message.
// This sample code uses AdaptivePayments .NET SDK to make API call. You can
// download the SDK [here](https://github.com/paypal/sdk-packages/tree/gh-pages/adaptivepayments-sdk/dotnet)
public class ExecutePaymentSample
{
    // # Static constructor for configuration setting
    static ExecutePaymentSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(ExecutePaymentSample));

    // # ExecutePayment API Operation
    // The ExecutePayment API operation lets you execute a payment set up with the Pay API operation with the actionType CREATE. To pay receivers identified in the Pay call, set the pay key from the PayResponse message in the ExecutePaymentRequest message. 
    public ExecutePaymentResponse ExecutePaymentAPIOperation()
    {
        // Create the ExecutePaymentResponse object
        ExecutePaymentResponse responseExecutePayment = new ExecutePaymentResponse();

        try
        {
            // # ExecutePaymentRequest
            // The code for the language in which errors are returned
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            // ExecutePaymentRequest which takes,
            //
            // * `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            // * `Pay Key` - The pay key that identifies the payment for which you
            // want to set payment options. This is the pay key returned in the
            // PayResponse message. Action Type in PayRequest must be `CREATE`
            ExecutePaymentRequest requestExecutePayment = new ExecutePaymentRequest(envelopeRequest, "AP-1VB65877N5917862M");

            // The ID of the funding plan from which to make this payment.
            requestExecutePayment.fundingPlanId = "0";

            // Create the service wrapper object to make the API call
            AdaptivePaymentsService service = new AdaptivePaymentsService();

            // # API call           
            // Invoke the ExecutePayment method in service wrapper object
            responseExecutePayment = service.ExecutePayment(requestExecutePayment);

            if (responseExecutePayment != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "ExecutePayment API operation - ";
                acknowledgement += responseExecutePayment.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseExecutePayment.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // The status of the payment. Possible values are:
                    //
                    // * CREATED - The payment request was received; funds will be
                    // transferred once the payment is approved
                    // * COMPLETED - The payment was successful
                    // * INCOMPLETE - Some transfers succeeded and some failed for a
                    // parallel payment
                    // * ERROR - The payment failed and all attempted transfers failed
                    // or all completed transfers were successfully reversed
                    // * REVERSALERROR - One or more transfers failed when attempting
                    // to reverse a payment
                    logger.Info("Payment Execution Status: " + responseExecutePayment.paymentExecStatus + "\n");
                    Console.WriteLine("Payment Execution Status : " + responseExecutePayment.paymentExecStatus + "\n");

                }
                // # Error Values 
                else
                {
                    List<ErrorData> errorMessages = responseExecutePayment.error;
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
        return responseExecutePayment;
    }
}

