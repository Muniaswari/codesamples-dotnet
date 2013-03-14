// # Namespaces 
using System;
using System.Collections.Generic;
using System.IO;
// # NuGet Install        
// Visual Studio 2012 and 2010 Command:  
// Install-Package PayPalInvoiceSDK    
// Visual Studio 2005 and 2008 (NuGet.exe) Command:  
// install PayPalInvoiceSDK    
using PayPal.Invoice;
using PayPal.Invoice.Model;
using log4net;

// # Sample for SearchInvoices API  
// Use the SearchInvoice API operation to search an invoice.
// This sample code uses Invoice .NET SDK to make API call. You can
// the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/invoice-sdk/dotnet)
public class SearchInvoicesSample
{
    // # Static constructor for configuration setting
    static SearchInvoicesSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(SearchInvoicesSample));


    // # SearchInvoices API Operation
    // Use the SearchInvoice API operation to search an invoice. 
    public SearchInvoicesResponse SearchInvoicesAPIOperation()
    {
        // Create the SearchInvoicesResponse object
        SearchInvoicesResponse responseSearchInvoices = new SearchInvoicesResponse();

        try
        {
            // # SearchInvoicesRequest
            // Use the SearchInvoiceRequest message to search an invoice.

            // The code for the language in which errors are returned, which must be
            // en_US.
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            SearchParametersType parameters = new SearchParametersType();

            // Invoice amount search. It specifies the smallest amount to be
            // returned. If you pass a value for this field, you must also pass a
            // currencyCode value.
            parameters.upperAmount = Convert.ToDecimal("4.00");

            // Currency used for lower and upper amounts. It is required when you
            // specify lowerAmount or upperAmount.
            parameters.currencyCode = "USD";

            // SearchInvoicesRequest which takes mandatory params:
            // 
            // * `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            // * `Merchant Email` - Email address of invoice creator.
            // * `SearchParameters` - Parameters constraining the search.
            // * `Page` - Page number of result set, starting with 1.
            // * `Page Size` - Number of results per page, between 1 and 100.
            SearchInvoicesRequest requestSearchInvoices = new SearchInvoicesRequest(envelopeRequest, "jb-us-seller@paypal.com", parameters, Convert.ToInt32("1"), Convert.ToInt32("10"));

            // Create the service wrapper object to make the API call
            InvoiceService service = new InvoiceService();

            // # API call   
            // Invoke the SearchInvoices method in service
            responseSearchInvoices = service.SearchInvoices(requestSearchInvoices);

            if (responseSearchInvoices != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "SearchInvoices API Operation - ";
                acknowledgement += responseSearchInvoices.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseSearchInvoices.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Number of invoices that matched the request.                    
                    logger.Info("Count : " + responseSearchInvoices.count + "\n");
                    Console.WriteLine("Count : " + responseSearchInvoices.count + "\n");

                }
                // # Error Values                
                else
                {
                    List<ErrorData> errorMessages = responseSearchInvoices.error;
                    foreach (ErrorData error in errorMessages)
                    {
                        logger.Debug("API Error Message : " + error.message + "\n");
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
        return responseSearchInvoices;
    }

}
