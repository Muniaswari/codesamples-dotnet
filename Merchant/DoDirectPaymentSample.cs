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

// # Sample for DoDirectPayment API     
// The DoDirectPayment API Operation enables you to process a credit card
// payment.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs
// [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class DoDirectPaymentSample
{
    // # Static constructor for configuration setting
    static DoDirectPaymentSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(DoDirectPaymentSample));

    // # DoDirectPaymentAPIOperation
    // The MassPay API operation makes a payment to one or more PayPal account holders.
    public DoDirectPaymentResponseType DoDirectPaymentAPIOperation()
    {
        // Create the DoDirectPaymentResponseType object
        DoDirectPaymentResponseType responseDoDirectPaymentResponseType = new DoDirectPaymentResponseType();

        try
        {
            // Create the DoDirectPaymentReq object
            DoDirectPaymentReq doDirectPayment = new DoDirectPaymentReq();
            DoDirectPaymentRequestDetailsType doDirectPaymentRequestDetails = new DoDirectPaymentRequestDetailsType();

            // Information about the credit card to be charged.
            CreditCardDetailsType creditCard = new CreditCardDetailsType();

            // Type of credit card. For UK, only Maestro, MasterCard, Discover, and
            // Visa are allowable. For Canada, only MasterCard and Visa are
            // allowable and Interac debit cards are not supported. It is one of the
            // following values:
            //
            // * Visa
            // * MasterCard
            // * Discover
            // * Amex
            // * Solo
            // * Switch
            // * Maestro: See note.
            // `Note:
            // If the credit card type is Maestro, you must set currencyId to GBP.
            // In addition, you must specify either StartMonth and StartYear or
            // IssueNumber.`
            creditCard.CreditCardType = CreditCardTypeType.VISA;

            // Credit Card number
            creditCard.CreditCardNumber = "4770461107194023";

            // ExpiryMonth of credit card
            creditCard.ExpMonth = Convert.ToInt32("12");

            // Expiry Year of credit card
            creditCard.ExpYear = Convert.ToInt32("2021");

            //Details about the owner of the credit card.
            PayerInfoType cardOwner = new PayerInfoType();

            // Email address of buyer.
            cardOwner.Payer = "enduser_biz@gmail.com";
            creditCard.CardOwner = cardOwner;

            doDirectPaymentRequestDetails.CreditCard = creditCard;

            // Information about the payment
            PaymentDetailsType paymentDetails = new PaymentDetailsType();

            // IPN URL
            // * PayPal Instant Payment Notification is a call back system that is initiated when a transaction is completed        
            // * The transaction related IPN variables will be received on the call back URL specified in the request       
            // * The IPN variables have to be sent back to the PayPal system for validation, upon validation PayPal will send a response string "VERIFIED" or "INVALID"     
            // * PayPal would continuously resend IPN if a wrong IPN is sent        
            paymentDetails.NotifyURL = "http://IPNhost";

            // Total cost of the transaction to the buyer. If shipping cost and tax
            // charges are known, include them in this value. If not, this value
            // should be the current sub-total of the order.
            //
            // If the transaction includes one or more one-time purchases, this field must be equal to
            // the sum of the purchases. Set this field to 0 if the transaction does
            // not include a one-time purchase such as when you set up a billing
            // agreement for a recurring payment that is not immediately charged.
            // When the field is set to 0, purchase-specific fields are ignored.
            //
            // * `Currency Code` - You must set the currencyID attribute to one of the
            // 3-character currency codes for any of the supported PayPal
            // currencies.
            // * `Amount`
            BasicAmountType orderTotal = new BasicAmountType(CurrencyCodeType.USD, "4.00");
            paymentDetails.OrderTotal = orderTotal;
            doDirectPaymentRequestDetails.PaymentDetails = paymentDetails;

            // IP address of the buyer's browser.
            // `Note:
            // PayPal records this IP addresses as a means to detect possible fraud.`
            doDirectPaymentRequestDetails.IPAddress = "127.0.0.1";

            DoDirectPaymentRequestType doDirectPaymentRequest = new DoDirectPaymentRequestType(doDirectPaymentRequestDetails);
            doDirectPayment.DoDirectPaymentRequest = doDirectPaymentRequest;

            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the DoDirectPayment method in service wrapper object
            responseDoDirectPaymentResponseType = service.DoDirectPayment(doDirectPayment);

            if (responseDoDirectPaymentResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "DoDirectPayment API Operation - ";
                acknowledgement += responseDoDirectPaymentResponseType.Ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseDoDirectPaymentResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Unique identifier of the transaction
                    logger.Info("Transaction ID : " + responseDoDirectPaymentResponseType.TransactionID + "\n");
                    Console.WriteLine("Transaction ID : " + responseDoDirectPaymentResponseType.TransactionID + "\n");

                }
                // # Error Values
                else
                {
                    List<ErrorType> errorMessages = responseDoDirectPaymentResponseType.Errors;
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
        return responseDoDirectPaymentResponseType;
    }

}

