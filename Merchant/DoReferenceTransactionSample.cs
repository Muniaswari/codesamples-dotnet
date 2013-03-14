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

// # Sample for  DoReferenceTransaction API     
// The DoReferenceTransaction API operation processes a payment from a
// buyer's account, which is identified by a previous transaction.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/merchant-sdk/dotnet)
public class DoReferenceTransactionSample
{
    // # Static constructor for configuration setting
    static DoReferenceTransactionSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(DoReferenceTransactionSample));

    //#DoReferenceTransaction API Operation
    //The DoReferenceTransaction API operation processes a payment from a buyer’s account, which is identified by a previous transaction. 
    public DoReferenceTransactionResponseType DoReferenceTransactionAPIOperation()
    {
        DoReferenceTransactionResponseType responseDoReferenceTransactionResponseType = new DoReferenceTransactionResponseType();

        try
        {
            // Create the DoReferenceTransactionReq object
            DoReferenceTransactionReq doReferenceTransaction = new DoReferenceTransactionReq();

            // Information about the payment.
            PaymentDetailsType paymentDetails = new PaymentDetailsType();

            // The total cost of the transaction to the buyer. If shipping cost and
            // tax charges are known, include them in this value. If not, this value
            // should be the current subtotal of the order.

            // If the transaction includes one or more one-time purchases, this field must be equal to
            // the sum of the purchases. Set this field to 0 if the transaction does
            // not include a one-time purchase such as when you set up a billing
            // agreement for a recurring payment that is not immediately charged.
            // When the field is set to 0, purchase-specific fields are ignored
            //
            // * `Currency ID` - You must set the currencyID attribute to one of the
            // 3-character currency codes for any of the supported PayPal
            // currencies.
            // * `Amount`
            BasicAmountType orderTotal = new BasicAmountType(CurrencyCodeType.USD, "3.00");
            paymentDetails.OrderTotal = orderTotal;

            // IPN URL
            // * PayPal Instant Payment Notification is a call back system that is initiated when a transaction is completed        
            // * The transaction related IPN variables will be received on the call back URL specified in the request       
            // * The IPN variables have to be sent back to the PayPal system for validation, upon validation PayPal will send a response string "VERIFIED" or "INVALID"     
            // * PayPal would continuously resend IPN if a wrong IPN is sent        
            paymentDetails.NotifyURL = "http://IPNhost";

            // `DoReferenceTransactionRequestDetails` takes mandatory params:
            //
            // * `Reference Id` - A transaction ID from a previous purchase, such as a
            // credit card charge using the DoDirectPayment API, or a billing
            // agreement ID.
            // * `Payment Action Code` - How you want to obtain payment. It is one of
            // the following values:
            // * Authorization
            // * Sale
            // * Order
            // * None
            // * `Payment Details`
            DoReferenceTransactionRequestDetailsType doReferenceTransactionRequestDetails
                = new DoReferenceTransactionRequestDetailsType("97U72738FY126561H", PaymentActionCodeType.SALE, paymentDetails);
            DoReferenceTransactionRequestType doReferenceTransactionRequest = new DoReferenceTransactionRequestType(doReferenceTransactionRequestDetails);
            doReferenceTransaction.DoReferenceTransactionRequest = doReferenceTransactionRequest;

            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the DoReferenceTransaction method in service wrapper object
            responseDoReferenceTransactionResponseType = service.DoReferenceTransaction(doReferenceTransaction);

            if (responseDoReferenceTransactionResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "DoReferenceTransaction API Operation - ";
                acknowledgement += responseDoReferenceTransactionResponseType.Ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseDoReferenceTransactionResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // The final amount charged, including any shipping and taxes from your Merchant Profile
                    logger.Info("Amount : " + responseDoReferenceTransactionResponseType.DoReferenceTransactionResponseDetails.Amount.currencyID
                        + " " + responseDoReferenceTransactionResponseType.DoReferenceTransactionResponseDetails.Amount.value + "\n");
                    Console.WriteLine("Amount : " + responseDoReferenceTransactionResponseType.DoReferenceTransactionResponseDetails.Amount.currencyID
                        + " " + responseDoReferenceTransactionResponseType.DoReferenceTransactionResponseDetails.Amount.value + "\n");
                }
                // # Error Values
                else
                {
                    List<ErrorType> errorMessages = responseDoReferenceTransactionResponseType.Errors;
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
        return responseDoReferenceTransactionResponseType;
    }
        
}
  

