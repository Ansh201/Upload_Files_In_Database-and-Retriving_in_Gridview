using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Security.Cryptography;


namespace Insert_And_Retriving_Images
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                fillGridView();
            }

            if (IsPostBack)
            {
                fillGridView();
            }
           

        } 

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string flocation = "Images/";
            string fiileName = Path.GetFileName(FileUpload1.FileName);
            string imageUrl = GetImageUrl(fiileName);
            Image Image1 = (Image)GridView1.Rows[0].FindControl("Image1");
            Image1.ImageUrl = imageUrl;

            string path = Server.MapPath("flocation");
            if (FileUpload1.HasFile)
            {

                string fileName = Path.GetFileName(FileUpload1.FileName);
                string Extension = Path.GetExtension(fileName);
                HttpPostedFile postedFile = FileUpload1.PostedFile;
                int length = postedFile.ContentLength;
                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".png" || Extension.ToLower() == ".jpeg" || Extension.ToLower() == ".pdf" || Extension.ToLower() == ".txt")
                {
                    if (length <= 1000000)
                    {
                       
                            FileUpload1.SaveAs(path + fileName);
                            //create another string variable
                            String name = "Images/" + fileName;
                            //write query
                            String query = "insert into img values(@img)";
                            SqlCommand cmd = new SqlCommand(query, con);
                            // from where value comes write line for this
                            cmd.Parameters.AddWithValue("@img", name);
                           
                            con.Open();
                            int a = cmd.ExecuteNonQuery();
                            if (a > 0)







                            {
                                Label1.Text = "Insertion Successfull";
                                Label1.ForeColor = System.Drawing.Color.Green;
                                Label1.Visible = true;
                                fillGridView();

                            }
                            else
                            {

                                Label1.Text = "Insertion Failed";
                                Label1.ForeColor = System.Drawing.Color.DarkRed;
                                Label1.Visible = true;



                            }


                        

                        }
                    else
                        {
                            Label1.Text = "Image Should not be greater than 1MB";
                            Label1.ForeColor = System.Drawing.Color.DarkRed;
                            Label1.Visible = true;

                        }





                    }
                    else
                    {

                        Label1.Text = "Image Formate is not supported";
                        Label1.ForeColor = System.Drawing.Color.DarkRed;
                        Label1.Visible = true;

                    }
                }
                else
                {
                    Label1.Text = "Please Upload Image!!";
                    Label1.ForeColor = System.Drawing.Color.DarkRed;
                    Label1.Visible = true;

                
            }


        }

        void fillGridView()
        {

            SqlConnection con = new SqlConnection(cs);
            string query = "select * from img";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            
            GridView1.DataSource = data;
            foreach (DataControlField column in GridView1.Columns)
            {
                if (column.GetType() == typeof(ImageField))
                {
                    ((ImageField)column).DataImageUrlField = "Image_Name";
                    ((ImageField)column).DataImageUrlFormatString = GetImageUrl("{0}");
                }
                
            }
            GridView1.DataBind();
            // now call this method on page load event
          


        }

      

        //protected string GetImageUrl(string fileName)
        //{
        //    string extension = Path.GetExtension(fileName);
        //    switch (extension.ToLower())
        //    {
        //        case ".pdf":
        //            return "~/images/pfdlogo.jpeg";
        //        case ".txt":
        //            return "~/images/txt-icon.png";
        //        default:
        //            return "~/images/default-icon.png";
        //    }
        //}

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

            {
                int rowIndex = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
                string flocation = GridView1.Rows[rowIndex].Cells[3].Text;
                string FilePath = Server.MapPath(flocation);

                WebClient User = new WebClient();

                Byte[] fileBuffer = User.DownloadData(FilePath);

                string extension = Path.GetExtension(FilePath);


                if (fileBuffer != null)
                {        

                    if (extension.ToLower() == ".pdf")

                    {


                        Response.ContentType = "application/pdf";



                        Response.AddHeader("content-length", fileBuffer.Length.ToString());
                        Response.BinaryWrite(fileBuffer);


                    }
                    else if (extension.ToLower() == ".txt")
                    {

                        string fileContent = File.ReadAllText(FilePath);
                       
                      
                        
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileBuffer + "\"");
                        Response.BinaryWrite(fileBuffer);
                        Response.End();

                    }
               
                
                }
               


            }
       
        
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    // Get the data for the current row
            //    var dataItem = e.Row.DataItem as data;

            //    // Set the file type image based on the file type
            //    var fileType = dataItem.FileType.ToLower();
            //    var fileTypeImage = e.Row.FindControl("FileTypeImage") as Image;
            //    if (fileType == "pdf")
            //    {
            //        fileTypeImage.ImageUrl = "Images/pfdlogo.jpeg";
            //    }
            //    else if (fileType == "txt")
            //    {
            //        fileTypeImage.ImageUrl = "Images/txt.png";
            //    }
            //    else
            //    {
            //        fileTypeImage.Visible = false;
            //    }
            //}

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Image image = (Image)e.Row.FindControl("Image1");
            //    string imageUrl = image.ImageUrl;
            //    // do something with the image URL
            //}
        }

        public string GetImageUrl(object fileName)
        {
            string extension = Path.GetExtension(fileName.ToString()).ToLower();

            switch (extension)
            {
                case ".pdf":
                    return "Images/pfdlogo.jpeg";
                case ".txt":
                    return "Images/txtlogo.png";
                default:
                    string filePath = "/Images/" + fileName;
                    return "Images/" + filePath;

            }
        }

            }
        }
