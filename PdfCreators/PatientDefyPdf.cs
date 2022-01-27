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

            #region FontsSettings
            var titleNameFont = FontFactory.GetFont("Times New Roman", 14, Font.BOLD);
            var titleFont = FontFactory.GetFont("Times New Roman", 12, Font.BOLD);
            #endregion

            document.AddTitle("DocumentName");
            document.Open();

            var titleTable = new PdfPTable(2);
            titleTable.WidthPercentage = 40f;
            #region Title table (Doc info)

            var docNameCell = new PdfPCell(new Phrase($"{doctor.Name}", titleNameFont))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                PaddingBottom = 10f,
            };
            titleTable.AddCell(docNameCell);

            var docAddressCell = new PdfPCell(new Phrase($"{doctor.Address}"))
            {
                Colspan = 2,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docAddressCell);

            var docPhoneCell = new PdfPCell(new Phrase($"Phone:", titleFont))
            {
                PaddingLeft = 45,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docPhoneCell);

            var docPhoneNumberCell = new PdfPCell(new Phrase($"{doctor.Phone}"))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docPhoneNumberCell);

            var docFaxCell = new PdfPCell(new Phrase($"Fax:", titleFont))
            {
                PaddingLeft = 45,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docFaxCell);

            var docFaxNumberCell = new PdfPCell(new Phrase($"{doctor.Fax}"))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docFaxNumberCell);

            var docDeaCell = new PdfPCell(new Phrase($"DEA{doctor.DEA}", titleFont))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docDeaCell);

            var docNpiCell = new PdfPCell(new Phrase($"NPI{doctor.NPI}", titleFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            titleTable.AddCell(docNpiCell);

            var docLicenseCell = new PdfPCell(new Phrase($"LICENSE{doctor.LICENSE}", titleFont))
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

            var patientTable = new PdfPTable(5);
            patientTable.WidthPercentage = 100f;
            patientTable.SetWidths(new float[] { 2f, 3f, 2f, 1.2f, 2.8f });

            #region Patient table

            var patientNameCell = new PdfPCell(new Phrase($"Patient Name: ", titleFont))
            {
                Rowspan = 5,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
                BorderWidthTop = 1,
            };
            patientTable.AddCell(patientNameCell);

            var patientNameNameCell = new PdfPCell(new Phrase($"{patient.Name}"))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
                BorderWidthTop = 1,
            };
            patientTable.AddCell(patientNameNameCell);

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

            var patientAddressCell = new PdfPCell(new Phrase($"{patient.Address}"))
            {
                Rowspan = 2,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientAddressCell);

            var patientDobCell = new PdfPCell(new Phrase($"DOB:", titleFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientDobCell);

            var patientDobDobCell = new PdfPCell(new Phrase($"{patient.DOB}"))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientDobDobCell);

            var patientGenderCell = new PdfPCell(new Phrase($"Gender:", titleFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientGenderCell);

            var patientGenderGenderCell = new PdfPCell(new Phrase($"{patient.Gender}"))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientGenderGenderCell);

            var patientPhoneCell = new PdfPCell(new Phrase($"{patient.PhoneNumber}"))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientPhoneCell);

            var patientPreferredCell = new PdfPCell(new Phrase($"Preferred"))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientPreferredCell);

            var patientMrnCell = new PdfPCell(new Phrase($"MRN:", titleFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientMrnCell);

            var patientMrnMrnCell = new PdfPCell(new Phrase($"{patient.MRN}"))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientMrnMrnCell);

            var bottomSpaceCell = new PdfPCell()
            {
                Colspan = 2,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(bottomSpaceCell);

            var patientAcctCell = new PdfPCell(new Phrase($"Acct#:", titleFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientAcctCell);

            var patientAcctAcctCell = new PdfPCell(new Phrase($"{patient.Acct}"))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientAcctAcctCell);

            var patientDateCell = new PdfPCell(new Phrase($"Date:", titleFont))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientDateCell);

            var patientDateDateCell = new PdfPCell(new Phrase($"{patient.Date}"))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(patientDateDateCell);

            var dateSpaceCell = new PdfPCell()
            {
                Colspan = 3,
                Border = 0,
                PaddingTop = 5,
            };
            patientTable.AddCell(dateSpaceCell);

            #endregion
            document.Add(patientTable);

            document.Close();
            writer.Close();

            return memoryStream.ToArray();
        }
    }
}
