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

// # Sample for SendInvoice API     
// Use the SendInvoice API operation to send an invoice to a payer, and notify the payer of the pending invoice.
// This sample code uses Invoice .NET SDK to make API call. You can
// download the SDKs [here](https://www.x.com/developers/paypal/documentation-tools/paypal-sdk-index)
public class SendInvoiceSample
{
    // # Static constructor for configuration setting
    static SendInvoiceSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(SendInvoiceSample));
    
    // # SendInvoice API Operation
    // Use the SendInvoice API operation to send an invoice to a payer, and notify the payer of the pending invoice. 
    public SendInvoiceResponse SendInvoiceAPIOperation()
    {
        // Create the SendInvoiceResponse object
        SendInvoiceResponse responseSendInvoice = new SendInvoiceResponse();
        
        try
        {
            // # SendInvoiceRequest
            // Use the SendInvoiceRequest message to send an invoice to a payer, and
            // notify the payer of the pending invoice.

            // The code for the language in which errors are returned, which must be
            // en_US.
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            // SendInvoiceRequest which takes mandatory params:
            //
            // * `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            // * `Invoice ID` - ID of the invoice to send.
            SendInvoiceRequest requestSendInvoice = new SendInvoiceRequest(envelopeRequest, "INV2-ZC9R-X6MS-RK8H-4VKJ");

            // Create the service wrapper object to make the API call
            InvoiceService service = new InvoiceService();
           
            // # API call 
            // Invoke the SendInvoice method in service
            responseSendInvoice = service.SendInvoice(requestSendInvoice);

            if (responseSendInvoice != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "SendInvoice API Operation - ";
                acknowledgement += responseSendInvoice.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseSendInvoice.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // ID of the created invoice.
                    logger.Info("Invoice ID : " + responseSendInvoice.invoiceID + "\n");
                    Console.WriteLine("Invoice ID : " + responseSendInvoice.invoiceID + "\n");
                }
                // # Error Values                
                else
                {
                    List<ErrorData> errorMessages = responseSendInvoice.error;
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
        return responseSendInvoice;
    }
}