using System;
using System.Collections.Generic;
using System.Linq;
using Appium.Net.Integration.Tests.src.Test.TestReport.LecturaArchivos;
using Appium.Net.Integration.Tests.src.Test.TestReport.Models;
using Appium.Net.Integration.Tests.src.Test.TestReport.Utility;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;
using iText.Layout;
using iText.Layout.Borders;
using iText.Kernel.Geom;
using Path = System.IO.Path;


namespace Appium.Net.Integration.Tests.src.Test.TestReport
{
    public class Createreport
    {

        public const float ancho1 = 130;
        public const float ancho2 = 400;

        public Createreport()
        {
        }

        public string CreateReport(string csvRoute, string reportName)
        {
            var readFile = new LeerCsv();
            var file = csvRoute;
            var reportModel = readFile.GetModelsFromFile(file);
            var header = reportModel.FirstOrDefault() ?? new ReportModel();

            // Obtener la ruta del directorio de la solución
            string solutionDirectory = AppDomain.CurrentDomain.BaseDirectory;

            string pdfFilePath = Path.Combine(solutionDirectory, ReportConstant.CarpetaReporte, reportName);
            Console.WriteLine(pdfFilePath);

            string Futura_path = Path.Combine(solutionDirectory, ReportConstant.CarpetaInsumos, ReportConstant.CarpetaFonts, ReportConstant.FuenteFutura);

            string A4Folder = Path.Combine(solutionDirectory, ReportConstant.CarpetaInsumos, ReportConstant.ReporteDevSecOpsImagenes);
            string Encabezado_path = Path.Combine(A4Folder, ReportConstant.EncabezadoReporte);
            string BaseI_path = Path.Combine(A4Folder, ReportConstant.PlecaReporte);
            string Devsecops_path = Path.Combine(A4Folder, ReportConstant.DevSecOpsImagen);

            DeviceRgb yellowColor = new(0xF9, 0xB0, 0x17);

            // Generación del PDF
            PdfWriter writer = new(pdfFilePath);
            PdfDocument pdf = new(writer);

            var fontProgram_Futura = FontProgramFactory.CreateFont(Futura_path);
            var Futura_Font = PdfFontFactory.CreateFont(fontProgram_Futura, PdfEncodings.IDENTITY_H);

            Table table = new(2, false);

            var encabezado = CreateHeader(Encabezado_path);
            var basee = CreateBase(BaseI_path);
            var logo = CreateLogo(Devsecops_path);
            var title = CreateTitle(Futura_Font);

            var handler = new ImageEventHandler(encabezado, basee);
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, handler);
            Document document = new(pdf, PageSize.A4, false);


            var tableProjectData = CreateProjectDataTable(header, Futura_Font);
            var tableDescription = CreateDescriptionTable(header, Futura_Font, yellowColor);

            Paragraph steps = new Paragraph(ReportConstant.DescripcionCasoPrueba)
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(Futura_Font)
             .SetBold()
            .SetFontSize(15);

            float cellWidth = 700f; // Ajusta este valor según sea necesario

            Table stepsTable = new(1, false);
            stepsTable.AddCell(CreateCell(ReportConstant.PasosReproduccion, true, Futura_Font, yellowColor, cellWidth, ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER));

            Table expectedResultsTable = new(1, false);
            expectedResultsTable.AddCell(CreateCell(ReportConstant.ResultadosEsperados, true, Futura_Font, yellowColor, cellWidth, ColorConstants.WHITE).SetTextAlignment(TextAlignment.CENTER));

            var listTables = new List<Table>();

            reportModel.ForEach(line =>
            {
                var color = line.EstatusPaso.Equals(ReportConstant.EstatusPasado) ? ColorConstants.GREEN : ColorConstants.RED;

                stepsTable.AddCell(CreateCell(line.DescPaso, false, Futura_Font, ColorConstants.WHITE, cellWidth, ColorConstants.BLACK));
                expectedResultsTable.AddCell(CreateCell(line.DescResult, false, Futura_Font, ColorConstants.WHITE, cellWidth, ColorConstants.BLACK));

                Table tableStepsWithInputs = new Table(3, false)
                    .SetBorder(new SolidBorder(yellowColor, 1f))// Establecer el borde de la tabla en amarillo
                    .SetKeepTogether(true); // Mantener la tabla completa en una sola página

                // Ancho relativo de las celdas
                tableStepsWithInputs.SetWidth(UnitValue.CreatePercentValue(100));

                tableStepsWithInputs.AddCell(CreateCell(line.DescPaso, false, Futura_Font, ColorConstants.WHITE, 260, ColorConstants.BLACK).SetBorder(new SolidBorder(yellowColor, 1)));
                tableStepsWithInputs.AddCell(CreateCell(line.DescResult, false, Futura_Font, ColorConstants.WHITE, 260, ColorConstants.BLACK).SetBorder(new SolidBorder(yellowColor, 1)));
                tableStepsWithInputs.AddCell(CreateCell(line.EstatusPaso, false, Futura_Font, ColorConstants.WHITE, 20, color).SetBorder(new SolidBorder(yellowColor, 1)));

                // Agregar otra fila para el título "Evidencia"
                tableStepsWithInputs.StartNewRow();
                if (string.IsNullOrEmpty(line.InputRoute))
                {
                    var cell = CreateCell(string.Empty, false, Futura_Font, ColorConstants.WHITE, 260, ColorConstants.BLACK);
                    tableStepsWithInputs.AddCell(cell);
                }
                else
                {
                    Image screenshot = new(ImageDataFactory.Create(line.InputRoute));
                    //Jugar con los parametros para adecuarla a la imagen.
                    screenshot.ScaleToFit(600, 275).SetHorizontalAlignment(HorizontalAlignment.CENTER);

                    var imagenCell = new Cell(1, 3)
                        .Add(screenshot)
                        .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                    tableStepsWithInputs.AddCell(imagenCell);
                }

                listTables.Add(tableStepsWithInputs);
            });

            Color whiteColor = ColorConstants.WHITE;

            document.Add(logo);
            document.Add(title);
            document.Add(table);
            document.Add(new Paragraph());
            document.Add(new Paragraph());
            document.Add(tableProjectData);
            document.Add(new Paragraph());
            document.Add(steps);
            document.Add(tableDescription);
            document.Add(new Paragraph());
            document.Add(new Paragraph());
            document.Add(new Paragraph());
            document.Add(stepsTable);
            document.Add(new Paragraph());
            document.Add(new Paragraph());
            document.Add(expectedResultsTable);
            document.Add(new AreaBreak());

            var count = 0;
            listTables.ForEach(table =>
            {
                document.Add(new Paragraph("\n\n"));
                document.Add(table);

                count++;

                if (count < listTables.Count)
                {
                    document.Add(new AreaBreak());
                }
            });

            float xcoordinateFooterText = 300;
            float ycoordinateFooterText = 25;

            var totalPages = pdf.GetNumberOfPages();

            for (var i = 1; i <= totalPages; i++)
            {
                Paragraph pageNumber = new Paragraph(string.Format(ReportConstant.NumberPage, i, totalPages))
                .SetFontColor(whiteColor);

                try
                {
                    document.ShowTextAligned(pageNumber,
                     xcoordinateFooterText, ycoordinateFooterText, i, TextAlignment.CENTER,
                    VerticalAlignment.BOTTOM, 0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            document.Close();
            return pdfFilePath;

        }
        private static Paragraph CreateTitle(PdfFont Futura_Font)
        {
            float interlineado = 0.8f; // Puedes ajustar este valor según tus necesidades

            Paragraph Titulo = new Paragraph()
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(Futura_Font)
                .SetBold()
                .SetFontSize(22)
                .SetMultipliedLeading(interlineado); // Ajusta el interlineado aquí

            Titulo.Add(ReportConstant.TituloReporte);
            return Titulo;
        }

        private static Image CreateLogo(string Devsecops_path)
        {
            Image devsecops = new(ImageDataFactory.Create(Devsecops_path));
            devsecops.ScaleToFit(110, 110); // Ajusta el tamaño de la imagen
            devsecops.SetHorizontalAlignment(HorizontalAlignment.CENTER); // Centra la imagen dentro de la celda
            devsecops.SetFixedPosition(445, 690);
            return devsecops;
        }

        private static Image CreateBase(string BaseI_path)
        {
            Image basee = new(ImageDataFactory.Create(BaseI_path));
            basee.ScaleToFit(600, 500); // Ajusta el tamaño de la imagen
            basee.SetHorizontalAlignment(HorizontalAlignment.CENTER); // Centra la imagen dentro de la celda
            basee.SetFixedPosition(0, 0);
            return basee;
        }

        private static Image CreateHeader(string Encabezado_path)
        {
            Image encabezado = new(ImageDataFactory.Create(Encabezado_path));
            encabezado.ScaleToFit(600, 500); // Ajusta el tamaño de la imagen
            encabezado.SetHorizontalAlignment(HorizontalAlignment.CENTER); // Centra la imagen dentro de la celda
            encabezado.SetFixedPosition(0, 790);
            return encabezado;
        }

        private static Cell CreateCell(string paragraph, bool needBold, PdfFont font, Color backgroudColor, float width, Color fontColor)
        {
            Cell cell = new Cell(1, 1)
               .SetBackgroundColor(backgroudColor)
               .SetTextAlignment(TextAlignment.LEFT)
               .SetWidth(width)
               .SetFontColor(fontColor)
               .SetFont(font)
               .Add(new Paragraph(paragraph));

            if (needBold)
            {
                cell.SetBold();
            }

            return cell;
        }

        private static Table CreateProjectDataTable(ReportModel header, PdfFont font)
        {
            DeviceRgb redColor = new(0xE4, 0x20, 0x14);

            Table dataTable = new(2, false);

            dataTable.AddCell(CreateCell(ReportConstant.HeaderNombreProyecto, true, font, redColor, ancho1, ColorConstants.WHITE));
            dataTable.AddCell(CreateCell(header.NombreProyecto, false, font, redColor, ancho2, ColorConstants.WHITE));

            dataTable.AddCell(CreateCell(ReportConstant.HeaderTiempoEjecucion, true, font, ColorConstants.WHITE, ancho1, ColorConstants.BLACK));
            dataTable.AddCell(CreateCell(header.TiempoEjecuccion, false, font, ColorConstants.WHITE, ancho2, ColorConstants.BLACK));

            dataTable.AddCell(CreateCell(ReportConstant.HeaderInicioEjecucion, true, font, ColorConstants.WHITE, ancho1, ColorConstants.BLACK));
            dataTable.AddCell(CreateCell(header.InicioEjecuccion.ToString("yyyy/MM/dd hh:mm:ss tt"), false, font, ColorConstants.WHITE, ancho2, ColorConstants.BLACK));

            dataTable.AddCell(CreateCell(ReportConstant.HeaderFinEjecucion, true, font, ColorConstants.WHITE, ancho1, ColorConstants.BLACK));
            dataTable.AddCell(CreateCell(header.FinEjecuccion.ToString("yyyy/MM/dd hh:mm:ss tt"), false, font, ColorConstants.WHITE, ancho2, ColorConstants.BLACK));

            dataTable.AddCell(CreateCell(ReportConstant.HeaderEstatusEjecucion, true, font, ColorConstants.WHITE, ancho1, ColorConstants.BLACK));
            dataTable.AddCell(CreateCell(header.StatusEjecuccion, false, font, ColorConstants.WHITE, ancho2, ColorConstants.BLACK));

            dataTable.AddCell(CreateCell(ReportConstant.HeaderPrecondicion, true, font, ColorConstants.WHITE, ancho1, ColorConstants.BLACK));
            dataTable.AddCell(CreateCell(header.Precondiciones, false, font, ColorConstants.WHITE, ancho2, ColorConstants.BLACK));

            return dataTable;
        }

        private static Table CreateDescriptionTable(ReportModel header, PdfFont font, DeviceRgb backgroundColor)
        {
            Table dataTable = new(2, false);

            dataTable.AddCell(CreateCell(ReportConstant.DescNombreCasoPrueba, true, font, backgroundColor, ancho1, ColorConstants.WHITE));
            dataTable.AddCell(CreateCell(header.NombreCasoPrueba, false, font, backgroundColor, ancho2, ColorConstants.WHITE));

            dataTable.AddCell(CreateCell(ReportConstant.DescCasoPrueba, true, font, ColorConstants.WHITE, ancho1, ColorConstants.BLACK));
            dataTable.AddCell(CreateCell(header.DescCasoPrueba, false, font, ColorConstants.WHITE, ancho2, ColorConstants.BLACK));

            dataTable.AddCell(CreateCell(ReportConstant.DescNombreScript, true, font, ColorConstants.WHITE, ancho1, ColorConstants.BLACK));
            dataTable.AddCell(CreateCell(header.NombreScript, false, font, ColorConstants.WHITE, ancho2, ColorConstants.BLACK));
            return dataTable;
        }
    }
}
