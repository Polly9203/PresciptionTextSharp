using System.Collections.Generic;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using PresciptionTextSharp.Models;

namespace PresciptionTextSharp.PdfCreators
{
    public static class PatientDefyPdf
    {
        public static byte[] GeneratePdf(Doctor doctor, Patient patient, List<Presciption> Presciptions)
        {
            var memoryStream = new MemoryStream();
            var document = new Document(PageSize.A4, 30, 30, 30, 30);
            var writer = PdfWriter.GetInstance(document, memoryStream);
            writer.PageEvent = new FooterEvent();

            #region FontsSettings
            var titleNameFont = FontFactory.GetFont("Times New Roman", 14, Font.BOLD);
            var boldFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
            var normalFont = FontFactory.GetFont("Times New Roman", 10);
            #endregion

            document.AddTitle("DocumentName");  // Add name
            document.Open();

            var titleTable = new PdfPTable(2);
            titleTable.WidthPercentage = 35f;
            titleTable.SetWidths(new float[] { 1f, 2f });
            #region Title table cells (Doc info)
            var docNameCell = new PdfPCell(new Phrase($"{doctor.Name}", titleNameFont))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                PaddingBottom = 10f,
            };
            titleTable.AddCell(docNameCell);

            var docAddressCell = new PdfPCell(new Phrase($"{doctor.Address}", normalFont))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docAddressCell);

            var docPhoneCell = new PdfPCell(new Phrase($"Phone:", boldFont))
            {
                PaddingLeft = 15f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docPhoneCell);

            var docPhoneValueCell = new PdfPCell(new Phrase($"{doctor.Phone}", normalFont))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docPhoneValueCell);

            var docFaxCell = new PdfPCell(new Phrase($"Fax:", boldFont))
            {
                //PaddingLeft = 45,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docFaxCell);

            var docFaxValueCell = new PdfPCell(new Phrase($"{doctor.Fax}", normalFont))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docFaxValueCell);

            var docDeaNpiCell = new PdfPCell(new Phrase($"DEA{doctor.DEA} NPI{doctor.NPI}", boldFont))
            {
                Colspan = 2,
                Border = 0,
                PaddingTop = 5,
                HorizontalAlignment = Element.ALIGN_CENTER,
            };
            titleTable.AddCell(docDeaNpiCell);

            var docLicenseCell = new PdfPCell(new Phrase($"LICENSE{doctor.LICENSE}", boldFont))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                PaddingTop = 5,
                PaddingBottom = 10f,
            };
            titleTable.AddCell(docLicenseCell);

            #endregion
            document.Add(titleTable);

            var signTable = new PdfPTable(4);
            signTable.WidthPercentage = 90f;
            signTable.SetWidths(new float[] { 2f, 2f, 1f, 4f });
            signTable.HorizontalAlignment = Element.ALIGN_RIGHT;
            #region Sign table cells
            var authenticatedCell = new PdfPCell(new Phrase($"Authenticated by: ", normalFont))
            {
                Border = 0,
                Rowspan = 2,
                PaddingTop = 5,
                PaddingBottom = 25,
                BorderWidthBottom = 1,
                HorizontalAlignment = Element.ALIGN_LEFT
            };
            signTable.AddCell(authenticatedCell);

            var authenticatedDocCell = new PdfPCell(new Phrase($"{doctor.Name}", normalFont))
            {
                Border = 0,
                Rowspan = 2,
                PaddingTop = 5,
                PaddingBottom = 25,
                BorderWidthBottom = 1,
                HorizontalAlignment = Element.ALIGN_CENTER
            };
            signTable.AddCell(authenticatedDocCell);

            var spacerowCell = new PdfPCell()
            {
                Border = 0,
                Rowspan = 4,
                PaddingTop = 5,
            };
            signTable.AddCell(spacerowCell);

            var spacecolCell = new PdfPCell(new Phrase(" "))
            {
                Border = 0,
            };
            signTable.AddCell(spacecolCell);

            var signCell = new PdfPCell(new Phrase($"{doctor.Name}", normalFont))
            {
                Border = 0,
                PaddingTop = 5,
                PaddingBottom = 5,
                BorderWidthBottom = 1,
                HorizontalAlignment = Element.ALIGN_LEFT
            };
            signTable.AddCell(signCell);

            signTable.AddCell(spacecolCell);
            signTable.AddCell(spacecolCell);


            var electronicallyCell = new PdfPCell(new Phrase($"Electronically Signed", normalFont))
            {
                Border = 0,
                PaddingTop = 1,
                HorizontalAlignment = Element.ALIGN_LEFT
            };
            signTable.AddCell(electronicallyCell);

            signTable.AddCell(spacecolCell);
            signTable.AddCell(spacecolCell);

            var dispenseSignCell = new PdfPCell(new Phrase($"Dispense As Written", normalFont))
            {
                Border = 0,
                PaddingTop = 0,
                HorizontalAlignment = Element.ALIGN_LEFT
            };
            signTable.AddCell(dispenseSignCell);
            #endregion
            signTable.KeepTogether = true;

            var patientTable = new PdfPTable(5);
            patientTable.WidthPercentage = 100f;
            patientTable.SetWidths(new float[] { 2f, 3f, 2f, 1.2f, 2.8f });
            #region Patient table cells
            var patientNameCell = new PdfPCell(new Phrase($"Patient Name: ", boldFont))
            {
                Rowspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
                BorderWidthTop = 1,
            };
            patientTable.AddCell(patientNameCell);

            var patientNameValueCell = new PdfPCell(new Phrase($"{patient.Name}", normalFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
                BorderWidthTop = 1,
            };
            patientTable.AddCell(patientNameValueCell);

            var centerSpaceCell = new PdfPCell()
            {
                Rowspan = 3,
                Border = 0,
                PaddingTop = 5,
                BorderWidthTop = 1,
            };
            patientTable.AddCell(centerSpaceCell);

            var rightSpaceCell = new PdfPCell()
            {
                Colspan = 2,
                Border = 0,
                PaddingTop = 5,
                BorderWidthTop = 1,
            };
            patientTable.AddCell(rightSpaceCell);

            var patientAddressCell = new PdfPCell(new Phrase($"{patient.Address}", normalFont))
            {
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientAddressCell);

            var patientDobCell = new PdfPCell(new Phrase($"DOB:", boldFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientDobCell);

            var patientDobValueCell = new PdfPCell(new Phrase($"{patient.DOB}", normalFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientDobValueCell);

            var patientGenderCell = new PdfPCell(new Phrase($"Gender:", boldFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientGenderCell);

            var patientGenderValueCell = new PdfPCell(new Phrase($"{patient.Gender}", normalFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientGenderValueCell);

            var patientPhoneCell = new PdfPCell(new Phrase($"{patient.PhoneNumber}", normalFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientPhoneCell);

            var patientPreferredCell = new PdfPCell(new Phrase($"Preferred", normalFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientPreferredCell);

            var patientMrnCell = new PdfPCell(new Phrase($"MRN:", boldFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientMrnCell);

            var patientMrnValueCell = new PdfPCell(new Phrase($"{patient.MRN}", normalFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientMrnValueCell);

            var bottomSpaceCell = new PdfPCell()
            {
                Colspan = 2,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(bottomSpaceCell);

            var patientAcctCell = new PdfPCell(new Phrase($"Acct#:", boldFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientAcctCell);

            var patientAcctValueCell = new PdfPCell(new Phrase($"{patient.Acct}", normalFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientAcctValueCell);

            var patientDateCell = new PdfPCell(new Phrase($"Date:", boldFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
                PaddingBottom = 10

            };
            patientTable.AddCell(patientDateCell);

            var patientDateValueCell = new PdfPCell(new Phrase($"{patient.Date}", normalFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
                PaddingBottom = 10

            };
            patientTable.AddCell(patientDateValueCell);

            var dateSpaceCell = new PdfPCell()
            {
                Colspan = 3,
                Border = 0,
                PaddingTop = 5,
                PaddingBottom = 10
            };
            patientTable.AddCell(dateSpaceCell);

            #endregion
            document.Add(patientTable);

            #region Presciption table

            foreach (var presciption in Presciptions)
            {
                var presciptionTable = new PdfPTable(2);
                presciptionTable.PaddingTop = 40;
                presciptionTable.WidthPercentage = 100f;
                presciptionTable.SetWidths(new float[] { 1f, 9f });

                //var logo = Image.GetInstance(@"");
                //logo.ScaleToFit(100f, 100f);
                //var logoCell = new PdfPCell(logo)
                var logoCell = new PdfPCell(new Phrase($"Some logo"))     // Add img
                {
                    Rowspan = 7,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = 0,
                };
                presciptionTable.AddCell(logoCell);

                var sigCell = new PdfPCell(new Phrase($"SIG: {presciption.SIG}", normalFont))
                {
                    PaddingLeft = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    BorderWidthBottom = 0,
                };
                presciptionTable.AddCell(sigCell);

                var directionsCell = new PdfPCell(new Phrase($"Directions: {presciption.Directions}", normalFont))
                {
                    PaddingLeft = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0
                };
                presciptionTable.AddCell(directionsCell);

                var dispenseCell = new PdfPCell(new Phrase($"Dispense as Written", normalFont))
                {
                    PaddingLeft = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0
                };
                presciptionTable.AddCell(dispenseCell);

                var typeCell = new PdfPCell(new Phrase($"Type: {presciption.Type}", normalFont))
                {
                    PaddingLeft = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0
                };
                presciptionTable.AddCell(typeCell);

                var dxCell = new PdfPCell(new Phrase($"DX: {presciption.DX}", normalFont))
                {
                    PaddingLeft = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0
                };
                presciptionTable.AddCell(dxCell);

                var pharmasictCell = new PdfPCell(new Phrase($"Pharmacist: {presciption.Pharmacist}", normalFont))
                {
                    PaddingLeft = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    BorderWidthBottom = 0,
                    BorderWidthTop = 0
                };
                presciptionTable.AddCell(pharmasictCell);

                var idCell = new PdfPCell(new Phrase($"ID: {presciption.ID}", normalFont))
                {
                    PaddingLeft = 5,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    BorderWidthTop = 0,
                    PaddingBottom = 5
                };
                presciptionTable.AddCell(idCell);

                presciptionTable.SpacingAfter = 5f;
                presciptionTable.KeepTogether = true;

                var presciptionHeight = patientTable.CalculateHeights();
                var footerHight = 72f;
                var spaceHight = writer.GetVerticalPosition(true);

                if(spaceHight < footerHight + presciptionHeight)
                {
                    document.Add(signTable);
                    document.NewPage();
                }

                document.Add(presciptionTable);
            }
            #endregion

            document.Add(signTable);

            document.Close();
            writer.Close();

            return memoryStream.ToArray();
        }
    }


}

#region Page footer
class FooterEvent : PdfPageEventHelper
{
    Font FONT = new Font(Font.FontFamily.HELVETICA, 8);
    Font centerFONT = new Font(Font.FontFamily.HELVETICA, 6);

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        PdfContentByte canvas = writer.DirectContent;

        string pageNumber = "Page: " + writer.PageNumber;

        ColumnText.ShowTextAligned(
             canvas, Element.ALIGN_LEFT,
             new Phrase("AdvancedMD - EHR 21.3",
             FONT), 40, 25, 0
            );

        ColumnText.ShowTextAligned(
            canvas, Element.ALIGN_LEFT,
            new Phrase("The compounded medications listed are made at the request of the prescribing practitioner whose signature appears",
            centerFONT), 150, 30, 0);
        ColumnText.ShowTextAligned(
            canvas, Element.ALIGN_LEFT,
            new Phrase("above due to the medical need of a specific patient and the preparation is prescribed because the prescriber has",
            centerFONT), 150, 24, 0);
        ColumnText.ShowTextAligned(
            canvas, Element.ALIGN_LEFT,
            new Phrase("determined that the preparation will produce a clinically significant therapeutic response compared to a commercially",
            centerFONT), 150, 18, 0);
        ColumnText.ShowTextAligned(
            canvas, Element.ALIGN_LEFT,
            new Phrase("available product.",
            centerFONT), 150, 12, 0);


        ColumnText.ShowTextAligned(
            canvas, Element.ALIGN_LEFT,
            new Phrase($"{pageNumber}",
            FONT), 480, 25, 0
           );
    }
}

#endregion

