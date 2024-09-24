using iText.Kernel.Events;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Kernel.Geom;
using iText.Layout;
using IEventHandler = iText.Kernel.Events.IEventHandler;

namespace Appium.Net.Integration.Tests.src.Test.TestReport
{
        public class ImageEventHandler : IEventHandler
        {
            protected Image header;
            protected Image footer;

            public ImageEventHandler(Image header, Image footer)
            {
                this.header = header;
                this.footer = footer;
            }

            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
                PdfDocument pdfDoc = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas aboveCanvas = new(page.NewContentStreamAfter(), page.GetResources(), pdfDoc);
                Rectangle area = page.GetPageSize();
                new Canvas(aboveCanvas, area).Add(this.header);
                new Canvas(aboveCanvas, area).Add(this.footer);


            }
        }
    }
