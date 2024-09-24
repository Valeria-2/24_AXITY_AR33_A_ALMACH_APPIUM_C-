using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Appium.Net.Integration.Tests.src.Test.TestReport.Models;


namespace Appium.Net.Integration.Tests.src.Test.TestReport.LecturaArchivos
{
    internal class LeerCsv
    {

        public List<ReportModel> GetModelsFromFile(string routeFile)
        {
            var reportMode = File.ReadAllLines(routeFile)
                             .Select(x => GetModelFromLine(x))
                             .ToList();

            return reportMode;
        }

        private ReportModel GetModelFromLine(string cvsLine)
        {
            var stringValues = cvsLine.Split(',');

            if (stringValues.Length < 12)
            {
                return new ReportModel();
            }

            return new ReportModel
            {
                NombreProyecto = stringValues[0],
                TiempoEjecuccion = stringValues[1],
                InicioEjecuccion = Convert.ToDateTime(stringValues[2]),
                FinEjecuccion = Convert.ToDateTime(stringValues[3]),
                StatusEjecuccion = stringValues[4],
                Precondiciones = stringValues[5],
                NombreCasoPrueba = stringValues[6],
                DescCasoPrueba = stringValues[7],
                NombreScript = stringValues[8],
                DescPaso = stringValues[9],
                DescResult = stringValues[10],
                EstatusPaso = stringValues[11],
                InputRoute = stringValues[12],
            };
        }
    }
}
