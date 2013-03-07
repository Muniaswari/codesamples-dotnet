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

// # Sample for RefundTransaction API   
// The RefundTransaction API operation issues a refund to the PayPal account
// holder associated with a transaction.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class RefundTransactionSample
{
    // # Static constructor for configuration setting
    static RefundTransactionSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(MassPaySample));

    // # RefundTransaction API Operation
    // The RefundTransaction API operation issues a refund to the PayPal account holder associated with a transaction
    public RefundTransactionResponseType RefundTransactionAPIOperation()
    {
        // Create the RefundTransactionResponseType object
        RefundTransactionResponseType responseRefundTransactionResponseType = new RefundTransactionResponseType();

        try
        {
        // Create the RefundTransactionReq object
        RefundTransactionReq refundTransaction = new RefundTransactionReq();
        RefundTransactionRequestType refundTransactionRequest = new RefundTransactionRequestType();

        // Either the `transaction ID` or the `payer ID` must be specified.
        // PayerID is unique encrypted merchant identification number
        // For setting `payerId`,
        // `refundTransactionRequest.PayerID = "A9BVYX8XCR9ZQ";`        

        // Unique identifier of the transaction to be refunded.
        refundTransactionRequest.TransactionID = "1GF88795WC5643301";

        // Type of refund you are making. It is one of the following values:
        // 
        // * `Full` - Full refund (default).
        // * `Partial` - Partial refund.
        // * `ExternalDispute` - External dispute. (Value available since version
        // 82.0)
        // * `Other` - Other type of refund. (Value available since version 82.0)
        refundTransactionRequest.RefundType = RefundType.PARTIAL;

        // `Refund amount`, which contains
        // 
        // * `Currency Code`
        // * `Amount`
        // The amount is required if RefundType is Partial.
        // `Note:
        // If RefundType is Full, do not set the amount.`
        BasicAmountType amount = new BasicAmountType(CurrencyCodeType.USD, "1.00");
        refundTransactionRequest.Amount = amount;

        refundTransaction.RefundTransactionRequest = refundTransactionRequest;

        // Create the service wrapper object to make the API call
        PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the RefundTransaction method in service wrapper object
            responseRefundTransactionResponseType = service.RefundTransaction(refundTransaction);

            if (responseRefundTransactionResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "RefundTransaction API Operation - ";
                acknowledgement += responseRefundTransactionResponseType.Ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseRefundTransactionResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Unique transaction ID of the refund
                    logger.Info("Refund Transaction ID : " + responseRefundTransactionResponseType.RefundTransactionID + "\n");
                    Console.WriteLine("Refund Transaction ID : " + responseRefundTransactionResponseType.RefundTransactionID + "\n");
                }
                // # Error Values
                else
                {
                    List<ErrorType> errorMessages = responseRefundTransactionResponseType.Errors;
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
        return responseRefundTransactionResponseType;
    }
       
}

