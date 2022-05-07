using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1
{
    public class Copier : IPrinter, IScanner
    {
        public int Counter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public int PrintCounter { get; private set; } = 0;

        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            state = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Counter++;
            }
            Console.WriteLine("Device is on ...");
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            }

        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            if (formatType == IDocument.FormatType.PDF)
            {
                document = new PDFDocument("cos.pdf");
            }
            else if (formatType == IDocument.FormatType.JPG)
            {
                document = new ImageDocument("cos.jpg");
            }
            else
            {
                document = new TextDocument("cos.txt");
            }

            if (state == IDevice.State.on)
            {
                ScanCounter++;
                Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
            }
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                IDocument doc;
                Scan(out doc, formatType: IDocument.FormatType.JPG);
                Print(doc);
            }

        }
    }
}
