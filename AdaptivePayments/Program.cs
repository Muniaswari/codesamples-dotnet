using System;
using PayPal.AdaptivePayments.Model;

/// <summary>
/// Summary description for Program
/// </summary>
public class Program
{
    // # Main method
    private static void Main()
    {
        SetPaymentOptionsSample sampleSetPaymentOptions = new SetPaymentOptionsSample();
        sampleSetPaymentOptions.SetPaymentOptionsAPIOperation();
        
        RefundSample sampleRefund = new RefundSample();
        sampleRefund.RefundAPIOperation();
        

        PreapprovalSample samplePreapproval = new PreapprovalSample();
        samplePreapproval.PreapprovalAPIOperation();
        
        PreapprovalDetailsSample samplePreapprovalDetails = new PreapprovalDetailsSample();
        samplePreapprovalDetails.PreapprovalDetailsAPIOperation();
       
        PaySample samplePay = new PaySample();

        PayRequest requestPay = samplePay.SimplePayment();
        samplePay.PayAPIOperations(requestPay);
        requestPay = samplePay.ParallelPayment();
        samplePay.PayAPIOperations(requestPay);
        requestPay = samplePay.ChainPayment();
        samplePay.PayAPIOperations(requestPay);
        


        PaymentDetailsSample samplePaymentDetails = new PaymentDetailsSample();
        samplePaymentDetails.PaymentDetailsAPIOperation();
        


        GetPaymentOptionsSample sampleGetPaymentOptions = new GetPaymentOptionsSample();
        sampleGetPaymentOptions.GetPaymentOptionsAPIOperation();
       

        ExecutePaymentSample sampleExecutePayment = new ExecutePaymentSample();
        sampleExecutePayment.ExecutePaymentAPIOperation();
       
        ConvertCurrencySample sampleConvertCurrency = new ConvertCurrencySample();
        sampleConvertCurrency.ConvertCurrencyAPIOperation();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}
