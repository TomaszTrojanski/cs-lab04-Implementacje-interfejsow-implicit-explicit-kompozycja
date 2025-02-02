﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public class Printer : IPrinter
    {
        public int Counter { get; private set; } = 0;
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
    }

    public class Scanner : IScanner
    {
        public int Counter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;

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

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            if (formatType == IDocument.FormatType.PDF)
            {
                document = new PDFDocument("aaa.pdf");
            }
            else if (formatType == IDocument.FormatType.JPG)
            {
                document = new ImageDocument("aaa.jpg");
            }
            else
            {
                document = new TextDocument("aaa.txt");
            }

            if (state == IDevice.State.on)
            {
                ScanCounter++;
                Console.WriteLine($"{DateTime.Now} Scan: {document.GetFileName()}");
            }
        }
    }

    public class Copier
    {
        Printer printer;
        Scanner scanner;
        public Copier(Printer _printer, Scanner _scanner)
        {
            printer = _printer;
            scanner = _scanner;
        }
        public int ScanCounter
        {
            get { return scanner.ScanCounter; }
        }
        public int PrintCounter
        {
            get { return printer.PrintCounter; }
        }

        public int Counter { get; private set; } = 0;
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            printer.PowerOff();
            scanner.PowerOff();
            state = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                printer.PowerOn();
                scanner.PowerOn();
                state = IDevice.State.on;
                Counter++;
            }
            Console.WriteLine("Device is on ...");
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                printer.Print(document);
            }

        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.PDF)
        {
            if (formatType == IDocument.FormatType.PDF)
            {
                document = new PDFDocument("aaa.pdf");
            }
            else if (formatType == IDocument.FormatType.JPG)
            {
                document = new ImageDocument("aaa.jpg");
            }
            else
            {
                document = new TextDocument("aaa.txt");
            }
            if (state == IDevice.State.on)
            {
                scanner.Scan(out document, formatType);
            }

        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on && scanner.GetState() == IDevice.State.on && printer.GetState() == IDevice.State.on)
            {
                IDocument doc;
                Scan(out doc, formatType: IDocument.FormatType.JPG);
                Print(doc);
            }
        }

    }
}
