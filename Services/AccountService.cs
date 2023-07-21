using VacationSplit.IServices;

namespace VacationSplit.Services
{
    public class AccountService:IAccountService
    {

        protected void UploadPicture(object sender, EventArgs e)
        {
            //string strFileName;
            //string strFilePath;
            //string strFolder;
            //strFolder = @"./UserPictures";
            //// Get the name of the file that is posted.
            //strFileName = oFile.PostedFile.FileName;
            //strFileName = Path.GetFileName(strFileName);
            //if (oFile.Value != "")
            //{
            //    // Create the directory if it does not exist.
            //    if (!Directory.Exists(strFolder))
            //    {
            //        Directory.CreateDirectory(strFolder);
            //    }
            //    // Save the uploaded file to the server.
            //    strFilePath = strFolder + strFileName;
            //    if (File.Exists(strFilePath))
            //    {
            //        lblUploadResult.Text = strFileName + " already exists on the server!";
            //    }
            //    else
            //    {
            //        oFile.PostedFile.SaveAs(strFilePath);
            //        lblUploadResult.Text = strFileName + " has been successfully uploaded.";
            //    }
            //}
            //else
            //{
            //    lblUploadResult.Text = "Click 'Browse' to select the file to upload.";
            //}
            //// Display the result of the upload.
            //frmConfirmation.Visible = true;
        }

    }
}
