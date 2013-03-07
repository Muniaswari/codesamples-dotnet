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

// # Sample for TransactionSearch API   
// The TransactionSearch API searches transaction history for transactions
// that meet the specified criteria.
// `Note:
// The maximum number of transactions that can be returned from a
// TransactionSearch API call is 100.`
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs
// [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class TransactionSearchSample
{
    // # Static constructor for configuration setting
    static TransactionSearchSample()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file
    private static ILog logger = LogManager.GetLogger(typeof(TransactionSearchSample));

    // # TransactionSearch API Operation
    // The TransactionSearch API searches transaction history for transactions that meet the specified criteria
    public TransactionSearchResponseType TransactionSearchAPIOperation()
    {
        // Create the TransactionSearchResponseType object
        TransactionSearchResponseType responseTransactionSearchResponseType = new TransactionSearchResponseType();

        try
        {
            // # Create the TransactionSearchReq object
            TransactionSearchReq requestTransactionSearch = new TransactionSearchReq();

            // `TransactionSearchRequestType` which takes mandatory argument:
            // 
            // * `Start Date` - The earliest transaction date at which to start the
            // search.
            TransactionSearchRequestType transactionSearchRequest = new TransactionSearchRequestType("2012-12-25T00:00:00+0530");
            requestTransactionSearch.TransactionSearchRequest = transactionSearchRequest;

            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the TransactionSearch method in service wrapper object
            responseTransactionSearchResponseType = service.TransactionSearch(requestTransactionSearch);

            if (responseTransactionSearchResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "TransactionSearch API Operation - ";
                acknowledgement += responseTransactionSearchResponseType.Ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseTransactionSearchResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Search Results
                    IEnumerator<PaymentTransactionSearchResultType> iterator = responseTransactionSearchResponseType.PaymentTransactions.GetEnumerator();

                    while (iterator.MoveNext())
                    {
                        PaymentTransactionSearchResultType searchResult = iterator.Current;

                        // Merchant's transaction ID.
                        logger.Info("Transaction ID : " + searchResult.TransactionID + "\n");
                        Console.WriteLine("Transaction ID : " + searchResult.TransactionID + "\n");
                    }
                }
                // # Error Values
                else
                {
                    List<ErrorType> errorMessages = responseTransactionSearchResponseType.Errors;
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
        return responseTransactionSearchResponseType;
    }
     
}

