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

// # Sample for GetInvoiceDetails API   
// Use the GetInvoiceDetails API operation to get detailed information about an invoice.
// This sample code uses Invoice .NET SDK to make API call. You can
// download the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/invoice-sdk/dotnet)
public class GetInvoiceDetailsSample
{
    // # Static constructor for configuration setting
    static GetInvoiceDetailsSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(GetInvoiceDetailsSample));

    // # GetInvoiceDetails API Operation
    // Use the GetInvoiceDetails API operation to get detailed information about an invoice.
    public GetInvoiceDetailsResponse GetInvoiceDetailsAPIOperation()
    {
        // Create the GetInvoiceDetailsResponse object;
        GetInvoiceDetailsResponse responseGetInvoiceDetails = new GetInvoiceDetailsResponse();

        try
        {
            // # GetInvoiceDetailsRequest
            // Use the GetInvoiceDetailsRequest message to get detailed information
            // about an invoice.

            // The code for the language in which errors are returned, which must be
            // en_US.
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            // GetInvoiceDetailsRequest which takes mandatory params:
            // 
            // * `Request Envelope` -  Information common to each API operation, such as the language in which an error message is returned.
            // * `Invoice ID` - ID of the invoice to retrieve.
            GetInvoiceDetailsRequest getInvoiceDetailsRequest = new GetInvoiceDetailsRequest(envelopeRequest, "INV2-ZC9R-X6MS-RK8H-4VKJ");

            // Create the service wrapper object to make the API call
            InvoiceService service = new InvoiceService();

            // # API call  
            // Invoke the GetInvoiceDetails method
            responseGetInvoiceDetails = service.GetInvoiceDetails(getInvoiceDetailsRequest);

            if (responseGetInvoiceDetails != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "GetInvoiceDetails API Operation - ";
                acknowledgement += responseGetInvoiceDetails.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseGetInvoiceDetails.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Status of the invoice searched.
                    logger.Info("Status : " + responseGetInvoiceDetails.invoiceDetails.status + "\n");
                    Console.WriteLine("Status : " + responseGetInvoiceDetails.invoiceDetails.status + "\n");

                }
                // # Error Values                
                else
                {
                    List<ErrorData> errorMessages = responseGetInvoiceDetails.error;
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
        return responseGetInvoiceDetails;
    }

}