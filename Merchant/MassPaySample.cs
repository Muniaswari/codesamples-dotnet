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

// # Sample for MassPay API     
// The MassPay API operation makes a payment to one or more PayPal account
// holders.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class MassPaySample
{
    // # Static constructor for configuration setting
    static MassPaySample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(MassPaySample));

    // # MassPay API Operation
    // The MassPay API operation makes a payment to one or more PayPal account holders. 
    public MassPayResponseType MassPayAPIOperation()
    {
        // Create the MassPayResponseType object
        MassPayResponseType responseMassPayResponseType = new MassPayResponseType();

        try
        {
            // # MassPayReq
            // Details of each payment.
            // `Note:
            // A single MassPayRequest can include up to 250 MassPayItems.`
            MassPayReq massPay = new MassPayReq();
            List<MassPayRequestItemType> massPayItemList = new List<MassPayRequestItemType>();

            // `Amount` for the payment which contains
            //
            // * `Currency Code`
            // * `Amount`
            BasicAmountType amount1 = new BasicAmountType(CurrencyCodeType.USD, "4.00");

            MassPayRequestItemType massPayRequestItem1 = new MassPayRequestItemType(amount1);

            // Email Address of receiver
            massPayRequestItem1.ReceiverEmail = "abc@paypal.com";

            // `Amount` for the payment which contains
            //
            // * `Currency Code`
            // * `Amount`
            BasicAmountType amount2 = new BasicAmountType(CurrencyCodeType.USD, "3.00");
            MassPayRequestItemType massPayRequestItem2 = new MassPayRequestItemType(amount2);

            // Email Address of receiver
            massPayRequestItem2.ReceiverEmail = "xyz@paypal.com";

            // `Amount` for the payment which contains
            //
            // * `Currency Code`
            // * `Amount`
            BasicAmountType amount3 = new BasicAmountType(CurrencyCodeType.USD, "7.00");
            MassPayRequestItemType massPayRequestItem3 = new MassPayRequestItemType(amount3);

            // Email Address of receiver
            massPayRequestItem3.ReceiverEmail = "def@paypal.com";
            massPayItemList.Add(massPayRequestItem2);
            massPayItemList.Add(massPayRequestItem1);
            massPayItemList.Add(massPayRequestItem3);
            MassPayRequestType massPayRequest = new MassPayRequestType(massPayItemList);
            massPay.MassPayRequest = massPayRequest;

            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the massPay method in service wrapper object
            responseMassPayResponseType = service.MassPay(massPay);

            if (responseMassPayResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "MassPay API Operation - ";
                acknowledgement += responseMassPayResponseType.Ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseMassPayResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    logger.Info("Acknowledgement : " + responseMassPayResponseType.Ack.ToString() + "\n");
                    Console.WriteLine("Acknowledgement : " + responseMassPayResponseType.Ack.ToString() + "\n");
                }
                // # Error Values
                else
                {
                    List<ErrorType> errorMessages = responseMassPayResponseType.Errors;
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
        return responseMassPayResponseType;
    }
       
}
