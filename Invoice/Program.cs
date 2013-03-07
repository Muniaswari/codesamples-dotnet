using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice
{
    class Program
    {
        // # Main method
        private static void Main()
        {
            CreateInvoiceSample sampleCreateInvoice = new CreateInvoiceSample();
            sampleCreateInvoice.CreateInvoiceAPIOperation();

            CreateAndSendInvoiceSample sampleCreateAndSendInvoice = new CreateAndSendInvoiceSample();
            sampleCreateAndSendInvoice.CreateAndSendInvoiceAPIOperation();

            GetInvoiceDetailsSample sampleGetInvoiceDetails = new GetInvoiceDetailsSample();
            sampleGetInvoiceDetails.GetInvoiceDetailsAPIOperation();

            SearchInvoicesSample sampleSearchInvoices = new SearchInvoicesSample();
            sampleSearchInvoices.SearchInvoicesAPIOperation();

            SendInvoiceSample sampleSendInvoice = new SendInvoiceSample();
            sampleSendInvoice.SendInvoiceAPIOperation();

            UpdateInvoiceSample sampleUpdateInvoice = new UpdateInvoiceSample();
            sampleUpdateInvoice.UpdateInvoiceAPIOperation();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
