using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OfficeOpenXml;

namespace Appium.Net.Integration.Tests.src.Main.Utils.Actions
{
    public class DataHelper
    {
        public static IEnumerable<TestCaseData> ExcelUserData()

        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            string filePath = Path.Combine(projectDirectory, Constantes.ExcelFilePath);



            // Verificamos si el archivo existe
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("El archivo no existe en la ruta especificada.");
            }

            // Abrimos el archivo Excel
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                // Iteramos sobre las filas para leer los datos
                for (int row = 2; row <= worksheet.Dimension.Rows; row++) // Empezamos desde la segunda fila, suponiendo que la primera fila es para los encabezados
                {
                    // Obtenemos los datos de usuario de la fila actual
                    string tcname = worksheet.Cells[row, 1].Value?.ToString();
                    string pais = worksheet.Cells[row, 2].Value?.ToString();

                    // Agregamos los datos como casos de prueba
                    yield return new TestCaseData(tcname,pais);
                }
            }
        }
    }
}

