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

// # Sample for GetTransactionDetails API   
// The GetTransactionDetails API operation obtains information about a
// specific transaction.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class GetTransactionDetailsSample
{
    // # Static constructor for configuration setting
    static GetTransactionDetailsSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(GetTransactionDetailsSample));

    // # GetTransactionDetails API Operation
    // The GetTransactionDetails API operation obtains information about a specific transaction. 
    public GetTransactionDetailsResponseType GetTransactionDetailsAPIOperation()
    {
        // Create the GetTransactionDetailsResponseType object
        GetTransactionDetailsResponseType responseGetTransactionDetailsResponseType = new GetTransactionDetailsResponseType();

        try
        {
            // Create the GetTransactionDetailsReq object
            GetTransactionDetailsReq getTransactionDetails = new GetTransactionDetailsReq();
            GetTransactionDetailsRequestType getTransactionDetailsRequest = new GetTransactionDetailsRequestType();

            // Unique identifier of a transaction.
            // `Note:
            // The details for some kinds of transactions cannot be retrieved with
            // GetTransactionDetails. You cannot obtain details of bank transfer
            // withdrawals, for example.`
            getTransactionDetailsRequest.TransactionID = "5AT5731435011481X";
            getTransactionDetails.GetTransactionDetailsRequest = getTransactionDetailsRequest;

            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the GetTransactionDetails method in service wrapper object
            responseGetTransactionDetailsResponseType = service.GetTransactionDetails(getTransactionDetails);

            if (responseGetTransactionDetailsResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "GetTransactionDetails API Operation - ";
                acknowledgement += responseGetTransactionDetailsResponseType.Ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseGetTransactionDetailsResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Unique PayPal Customer Account identification number
                    logger.Info("Payer ID : " + responseGetTransactionDetailsResponseType.PaymentTransactionDetails.PayerInfo.PayerID + "\n");
                    Console.WriteLine("Payer ID : " + responseGetTransactionDetailsResponseType.PaymentTransactionDetails.PayerInfo.PayerID + "\n");

                }
                // # Error Values             
                else
                {
                    List<ErrorType> errorMessages = responseGetTransactionDetailsResponseType.Errors;
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
        return responseGetTransactionDetailsResponseType;
    }

}

