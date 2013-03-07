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

// # Sample for DoCapture API   
// Captures an authorized payment.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class DoCaptureSample
{
    // # Static constructor for configuration setting
    static DoCaptureSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(DoCaptureSample));

    // # DoCapture API Operation 
    // Captures an authorized payment. 
    public DoCaptureResponseType DoCaptureAPIOperation()
    {
        // Create the DoCaptureResponseType object
        DoCaptureResponseType responseDoCaptureResponseType = new DoCaptureResponseType();

        try
        {
            // Create the DoCapture object
            DoCaptureReq doCapture = new DoCaptureReq();

            // `Amount` to capture which takes mandatory params:
            //
            // * `currencyCode`
            // * `amount`
            BasicAmountType amount = new BasicAmountType(CurrencyCodeType.USD, "4.00");

            // `DoCaptureRequest` which takes mandatory params:
            //
            // * `Authorization ID` - Authorization identification number of the
            // payment you want to capture. This is the transaction ID returned from
            // DoExpressCheckoutPayment, DoDirectPayment, or CheckOut. For
            // point-of-sale transactions, this is the transaction ID returned by
            // the CheckOut call when the payment action is Authorization.
            // * `amount` - Amount to capture
            // * `CompleteCode` - Indicates whether or not this is your last capture.
            // It is one of the following values:
            // * Complete – This is the last capture you intend to make.
            // * NotComplete – You intend to make additional captures.
            // `Note:
            // If Complete, any remaining amount of the original authorized
            // transaction is automatically voided and all remaining open
            // authorizations are voided.`
            DoCaptureRequestType doCaptureRequest = new DoCaptureRequestType("O-4VR15106P7416533H", amount, CompleteCodeType.NOTCOMPLETE);
            doCapture.DoCaptureRequest = doCaptureRequest;

            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the DoCapture method in service wrapper object
            responseDoCaptureResponseType = service.DoCapture(doCapture);

            if (responseDoCaptureResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "DoCapture API Operation - ";
                acknowledgement += responseDoCaptureResponseType.Ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseDoCaptureResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Authorization identification number
                    logger.Info("Authorization ID : " + responseDoCaptureResponseType.DoCaptureResponseDetails.AuthorizationID + "\n");
                    Console.WriteLine("Authorization ID : " + responseDoCaptureResponseType.DoCaptureResponseDetails.AuthorizationID + "\n");

                }
                // # Error Values
                else
                {
                    List<ErrorType> errorMessages = responseDoCaptureResponseType.Errors;
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
        return responseDoCaptureResponseType;
    }

}  
    
