using InvoiceGenerate.BAL;

namespace InvoiceGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            InvoiceBAL InvoiceBALObj = new InvoiceBAL();
           // InvoiceBALObj.InvoiceReader();
            InvoiceBALObj.InvoiceReaderMailSendTrowInvoiceTemplete();
        }
    }
}
