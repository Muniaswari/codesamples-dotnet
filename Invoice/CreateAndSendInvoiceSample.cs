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

// # Sample for CreateAndSendInvoice API    
// Use the CreateAndSendInvoice API operation to create and send an invoice.
// This sample code uses Invoice .NET SDK to make API call. You can
// download the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/invoice-sdk/dotnet)
public class CreateAndSendInvoiceSample
{
    // # Static constructor for configuration setting
    static CreateAndSendInvoiceSample()
    {
        // Load the log4net configuration settings from Web.config or App.config        
        log4net.Config.XmlConfigurator.Configure();
    }

    // Logs output statements, errors, debug info to a text file       
    private static ILog logger = LogManager.GetLogger(typeof(CreateAndSendInvoiceSample));

    // # CreateAndSendInvoice API Operation 
    // Use the CreateAndSendInvoice API operation to create and send an invoice.   
    public CreateAndSendInvoiceResponse CreateAndSendInvoiceAPIOperation()
    {
        // Create the CreateAndSendInvoiceResponse object
        CreateAndSendInvoiceResponse responseCreateAndSendInvoice = new CreateAndSendInvoiceResponse();

        try
        {
            // # CreateAndSendInvoiceRequest
            // Use the CreateAndSendInvoiceRequest message to create and send a new
            // invoice. The requester should authenticate the caller and verify that
            // the merchant requesting the invoice has an existing PayPal account in
            // good standing. Once the invoice is created, PayPal sends it to the
            // specified payer, who is notified of the pending invoice.

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

            // CreateAndSendInvoiceRequest which takes mandatory params:
            // 
            // * `Request Envelope` - Information common to each API operation, such
            // as the language in which an error message is returned.
            // * `Invoice` - Merchant, payer, and invoice information.
            CreateAndSendInvoiceRequest requestCreateAndSendInvoice = new CreateAndSendInvoiceRequest(envelopeRequest, invoice);

            // Create the service wrapper object to make the API call
            InvoiceService service = new InvoiceService();

            // # API call 
            // Invoke the CreateAndSendInvoice method in service
            responseCreateAndSendInvoice = service.CreateAndSendInvoice(requestCreateAndSendInvoice);

            if (responseCreateAndSendInvoice != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "CreateAndSendInvoice API Operation - ";
                acknowledgement += responseCreateAndSendInvoice.responseEnvelope.ack.ToString();
                logger.Info(acknowledgement + "\n");
                Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseCreateAndSendInvoice.responseEnvelope.ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // ID of the created invoice.
                    logger.Info("Invoice ID : " + responseCreateAndSendInvoice.invoiceID + "\n");
                    Console.WriteLine("Invoice ID : " + responseCreateAndSendInvoice.invoiceID + "\n");
                }
                // # Error Values			
                else
                {
                    List<ErrorData> errorMessages = responseCreateAndSendInvoice.error;
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
        return responseCreateAndSendInvoice;
    }

}
