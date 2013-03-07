using System;
using System.Collections.Generic;
using System.Text;

namespace Merchant
{
    class Program
    {

        // # Main method
        private static void Main()
        {
            CreateRecurringPaymentsProfileSample sampleCreateRecurringPaymentsProfile = new CreateRecurringPaymentsProfileSample();
            sampleCreateRecurringPaymentsProfile.CreateRecurringPaymentsProfileAPIOperation();

            DoAuthorizationSample sampleDoAuthorization = new DoAuthorizationSample();
            sampleDoAuthorization.DoAuthorizationAPIOperation();
            
            DoCaptureSample sampleDoCapture = new DoCaptureSample();
            sampleDoCapture.DoCaptureAPIOperation();

            DoDirectPaymentSample sampleDoDirectPayment = new DoDirectPaymentSample();
            sampleDoDirectPayment.DoDirectPaymentAPIOperation();

            DoExpressCheckoutPaymentSample sampleDoExpressCheckoutPayment = new DoExpressCheckoutPaymentSample();
            sampleDoExpressCheckoutPayment.DoExpressCheckoutPaymentAPIOperation();

            GetTransactionDetailsSample sampleGetTransactionDetails = new GetTransactionDetailsSample();
            sampleGetTransactionDetails.GetTransactionDetailsAPIOperation();

            GetExpressCheckoutDetailsSample sampleGetExpressCheckoutDetails = new GetExpressCheckoutDetailsSample();
            sampleGetExpressCheckoutDetails.GetExpressCheckoutDetailsAPIOperation();

            GetBalanceSample sampleGetBalance = new GetBalanceSample();
            sampleGetBalance.GetBalanceAPIOperation();

            DoVoidSample sampleDoVoid = new DoVoidSample();
            sampleDoVoid.DoVoidAPIOperation();

            DoReferenceTransactionSample sampleDoReferenceTransaction = new DoReferenceTransactionSample();
            sampleDoReferenceTransaction.DoReferenceTransactionAPIOperation();

            DoReauthorizationSample sampleDoReauthorization = new DoReauthorizationSample();
            sampleDoReauthorization.DoReauthorizationAPIOperation();

            TransactionSearchSample sampleTransactionSearch = new TransactionSearchSample();
            sampleTransactionSearch.TransactionSearchAPIOperation();

            SetExpressCheckoutSample sampleSetExpressCheckout = new SetExpressCheckoutSample();
            sampleSetExpressCheckout.SetExpressCheckoutAPIOperation();

            RefundTransactionSample sampleRefundTransaction = new RefundTransactionSample();
            sampleRefundTransaction.RefundTransactionAPIOperation();

            MassPaySample sampleMassPay = new MassPaySample();
            sampleMassPay.MassPayAPIOperation();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
