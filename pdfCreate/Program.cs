using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text; // using iTextSharp package
using iTextSharp.text.pdf;
using System.IO;


namespace pdfCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            //**********************************
            // Init
            //**********************************
            string templateDir = "C:/TeamCity/pdfSample/" +
                "QF0382_A01_no.pdf"; // SVD template file directory
            string saveDir = "C:/TeamCity/pdfOutput/";
            string newFileName = "newFile";
            var reader = new PdfReader(templateDir); // Load SVD template
            var fields = reader.AcroFields; // To save time writing out two terms
            var output = new FileStream(saveDir + newFileName + 
                ".pdf", FileMode.Create, FileAccess.Write); // New home to the new file
            var stamper = new PdfStamper(reader, output); // Applies extra content to the PDF document


            //**********************************
            // Field input variables
            //**********************************
            /*var p_productName = new Phrase("BDD Inputs - BddSQL_TEST"); // 'Phrase' is inside iTextSharp
            var p_versionMajor = new Phrase("6_TEST");
            var p_versionMinor = new Phrase("0_TEST");
            var p_versionRevision = new Phrase("-_TEST");
            var p_versionBuild = new Phrase("-_TEST");
            var p_CRCO = new Phrase("9285_TEST");
            var p_baselineTag = new Phrase("CR9285_BOND_6.0_Release_TEST");
            var p_checksum = new Phrase("e74e78a36fd4c25717b57efc8b72c5bcc3a2f5ff_TEST");
            var p_tick = new Phrase("x");*/

            string v_productName = (string) args[0];
            string v_versionMajor = "version major";
            string v_versionMinor = "version minor";
            string v_versionRevision = "versionRevision";
            string v_versionBuild = "versionBuild";
            string v_CRCO = "CR/CO";
            string v_baselineTag = "baseline/tag";
            string v_checksum = "checksum";

            // Would have information about the 'Witness' for Software Release + Baseline Details?

            string v_formDHFR = "form DHFR";
            string v_formVer = "form version";

            string v_CMRepoLoc = "repoLoc";
            string v_SCMPDHFR = "planDHFRNo";
            string v_SCMPVer = "versionNo";

            string v_DMRProductNo402 = 'dddd';
            string v_DMRIdentifier402 = "identifier";
            string v_DMRRevision402 = "revision";
            string v_DMRProductNo401 = "productNo";
            string v_DMRIdentifier401 = "identifier";
            string v_DMRRevision401 = "revision";

            string v_releaseNotesDHFR = "release notes DHFR";
            string v_releaseNotesVer = "release notes version";
            string v_releaseNotesRationale = "release notes rationale";



            /*List<bool> checkboxState = new List<bool>();
            checkboxState[0] = false;
            checkboxState[1] = true;
            checkboxState[2] = false;*/

            bool checkbox_1 = false;
            bool checkbox_2 = true;
            bool checkbox_3 = false;


            /*
            //**********************************
            // Watermark
            //**********************************
            // Watermark settings
            PdfContentByte canvas; //PdfContentByte is an object containing the user positioned text and graphic contents of a page
            canvas = stamper.GetOverContent(1);
            // Master Media Set - Physical Media Set Required
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, p_tick, 270, 470, 0);
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, p_versionMajor, 32, 425, 0); //Product Number
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, p_versionMajor, 108, 425, 0); //Identifier
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, p_versionMajor, 185, 425, 0); //Artefact Type
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, p_versionMajor, 232, 425, 0); //Revision
            ColumnText.ShowTextAligned(canvas, Element.ALIGN_LEFT, p_versionMajor, 232, 425, 0); //Checksum
            */


            //**********************************
            // Get field names
            //**********************************
            var fieldList = GetFormFieldNames(reader); // Disorganised field names
            List<string> result = fieldList.Split('\n').ToList(); // Organised field names list

            
            //**********************************
            // Print field names + result indices
            //**********************************
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine(i); // Form field index
                Console.WriteLine(result[i]); // Form field name
            }
            Console.WriteLine("=========================================");


            //**********************************
            // Form field types
            //**********************************
            List<string> checkboxFields = new List<string>();
            List<string> textFields = new List<string>();
            for (int i = 0; i < result.Count; i++) // For each form field on PDF
            {
                int j = fields.GetFieldType(result[i]); // Read form field type
                Console.WriteLine(result[i]); // Form field name
                Console.WriteLine(j); // Form field type
                if (j == 3) // Store checkbox type form field names
                {
                    checkboxFields.Add(result[i]); 
                }
                else
                {
                    textFields.Add(result[i]);
                }

            }
            Console.WriteLine("=========================================");


            //**********************************
            // Checkbox-type form fields
            //**********************************
            for (int i = 0; i < checkboxFields.Count; i++)
            {
                string j = fields.GetField(checkboxFields[i]); // Read form field value
                Console.WriteLine(checkboxFields[i]); // Form field name
                Console.WriteLine(j); // Value in the form field
            }
            Console.WriteLine("=========================================");


            /*
            //**********************************
            // Field positions
            //**********************************
            //Field positions
            IList<AcroFields.FieldPosition> fieldPositions = fields.GetFieldPositions(result[0]);
            AcroFields.FieldPosition fieldPosition = fieldPositions[0];
            Console.WriteLine(fieldPosition.position.Left);
            Console.WriteLine(fieldPosition.position.Right);
            Console.WriteLine(fieldPosition.position.Top);
            Console.WriteLine(fieldPosition.position.Bottom);
            */


            //**********************************
            // Consistent form field fills
            //**********************************
            /*for (int i = 0; i < textFields.Count; i++)
            {
                stamper.AcroFields.SetField(textFields[i], textFields[i]);
            }*/
            stamper.AcroFields.SetField(result[2], v_productName);
            stamper.AcroFields.SetField(result[3], v_versionMajor);
            stamper.AcroFields.SetField(result[4], v_versionMinor);
            stamper.AcroFields.SetField(result[5], v_versionRevision);
            stamper.AcroFields.SetField(result[6], v_versionBuild);
            stamper.AcroFields.SetField(result[7], v_CRCO);
            stamper.AcroFields.SetField(result[11], v_baselineTag);
            stamper.AcroFields.SetField(result[12], v_checksum);
            stamper.AcroFields.SetField(result[0], v_formDHFR);
            stamper.AcroFields.SetField(result[1], v_formVer);


            //**********************************
            // Checkbox conditions and consequent sfills
            //**********************************
            if (checkbox_1)
            {
                stamper.AcroFields.SetField(checkboxFields[0], "Yes");
                stamper.AcroFields.SetField(result[10], v_CMRepoLoc);
            }
            else
            {
                stamper.AcroFields.SetField(result[8], v_SCMPDHFR);
                stamper.AcroFields.SetField(result[9], v_SCMPVer);
            }
            if (checkbox_2)
            {
                stamper.AcroFields.SetField(checkboxFields[1], "Yes");
            }
            if (checkbox_3)
            {
                stamper.AcroFields.SetField(checkboxFields[2], "Yes");
                stamper.AcroFields.SetField(result[23], v_releaseNotesDHFR);
                stamper.AcroFields.SetField(result[24], v_releaseNotesVer);
            }
            else
            {
                stamper.AcroFields.SetField(result[25], v_releaseNotesRationale);
            }


            //**********************************
            // Wrap up
            //**********************************
            //Console.ReadLine(); //To stop console from closing
            stamper.Close(); // Close iText PDF stamper
            
        }
        private static string GetFormFieldNames(PdfReader pdfReader)
        {
            return string.Join("\n", pdfReader.AcroFields.Fields
                                           .Select(x => x.Key).ToArray());
        }
    }
}
