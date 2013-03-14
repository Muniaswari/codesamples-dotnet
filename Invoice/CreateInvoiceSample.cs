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

// # Sample for CreateInvoice API   
// Use the CreateInvoice API operation to create a new invoice. The call includes merchant, payer, and API caller information, in addition to invoice detail. The response to the call contains an invoice ID and URL.
// This sample code uses Invoice .NET SDK to make API call. You can
// download the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/invoice-sdk/dotnet)
public class CreateInvoiceSample
{
    // # Static constructor for configuration setting
    static CreateInvoiceSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(CreateInvoiceSample));

    // # CreateInvoice API Operation
    // Use the CreateInvoice API operation to create a new invoice. The call includes merchant, payer, and API caller information, in addition to invoice detail. The response to the call contains an invoice ID and URL. 
    public CreateInvoiceResponse CreateInvoiceAPIOperation()
    {
        // Create the CreateInvoiceResponse object
        CreateInvoiceResponse responseCreateInvoice = new CreateInvoiceResponse();

        try
        {
            // # CreateInvoiceRequest
            // Use the CreateInvoiceRequest message to create a new invoice. The
            // merchant issuing the invoice, and the partner, if any, making the
            // call, must have a PayPal account in good standing.

            // The code for the language in which errors are returned, which must be
            // en_US.
            RequestEnvelope envelopeRequest = new RequestEnvelope();
            envelopeRequest.errorLanguage = "en_US";

            List<InvoiceItemType> invoiceItemList = new List<InvoiceItemType>();

            // InvoiceItemType which takes mandatory params:
            // 
            // * `Item Name` - SKU or name of the item.
            // * `Quantity` - Item count.
            // * `Amount` - Price of the item, in the currency specified by the
            // invoice.
            InvoiceItemType invoiceItem = new InvoiceItemType("Item", Convert.ToDecimal("2"), Convert.ToDecimal("4.00"));
            invoiceItemList.Add(invoiceItem);

            // Invoice item.
            InvoiceItemListType itemList = new InvoiceItemListType(invoiceItemList);

            // InvoiceType which takes mandatory params:
            // 
            // * `Merchant Email` - Merchant email address.
            // * `Personal Email` - Payer email address.
            // * `InvoiceItemList` - List of items included in this invoice.
            // * `CurrencyCode` - Currency used for all invoice item amounts and
            // totals.
            // * `PaymentTerms` - Terms by which the invoice payment is due. It is
            // one of the following values:
            //  * DueOnReceipt - Payment is due when the payer receives the invoice.
            //  * DueOnDateSpecified - Payment is due on the date specified in the
            //  invoice.
            //  * Net10 - Payment is due 10 days from the invoice date.
            //  * Net15 - Payment is due 15 days from the invoice date.
            //  * Net30 - Payment is due 30 days from the invoice date.
            //  * Net45 - Payment is due 45 days from the invoice date.
            InvoiceType invoice = new InvoiceType("jb-us-seller@paypal.com", "jbui-us-personal1@paypal.com", itemList, "USD", PaymentTermsType.DUEONRECEIPT);

            // CreateInvoiceRequest which takes mandatory params:
            // 
            // * `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            // * `Invoice` - Merchant, payer, and invoice information.
            CreateInvoiceRequest createInvoiceRequest = new CreateInvoiceRequest(envelopeRequest, invoice);

            // Create the service wrapper object to make the API call
            InvoiceService service = new InvoiceService();

            // # API call   
            // Invoke the CreateInvoice method in service wrapper object  
            responseCreateInvoice = service.CreateInvoice(createInvoiceRequest);

            if (responseCreateInvoice != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "CreateInvoice API Operation - ";
                acknowledgement += responseCreateInvoice.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseCreateInvoice.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // ID of the created invoice.
                    logger.Info("Invoice ID : " + responseCreateInvoice.invoiceID + "\n");
                    Console.WriteLine("Invoice ID : " + responseCreateInvoice.invoiceID + "\n");

                }
                // # Error Values
                else
                {
                    List<ErrorData> errorMessages = responseCreateInvoice.error;
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
        return responseCreateInvoice;
    }

}