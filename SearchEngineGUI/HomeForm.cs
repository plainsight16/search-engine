using DocHandler;
using DocRanker;
using DocRepresentation;
using Query;
using System.Diagnostics;

namespace SearchEngineGUI
{
    public partial class HomeForm : Form
    {
        Dictionary<string, List<Token>> mergedIndex;
        AutoCompleteStringCollection searchQueries = new AutoCompleteStringCollection();

        public HomeForm()
        {
            InitializeComponent();
            InitializeIndexer();
            InitializeTextFieldSearch();
        }

        private void InitializeIndexer()
        {
            DocumentRepresentation docRep = new DocRepLocalStorage(@"..\..\..\..\Files\Databases\db.json").LoadObjectFromFile();
            mergedIndex = docRep.mergedIndex;
        }

        private void InitializeTextFieldSearch()
        {
            TextBoxQuery.AutoCompleteMode = AutoCompleteMode.Suggest;
            TextBoxQuery.AutoCompleteSource = AutoCompleteSource.CustomSource;

            SearchQuery searchQuery = new SearchQueryLocalStorage(@"..\..\..\..\Files\Databases\queries.json").LoadObjectFromFile();
            if (searchQuery != null)
            {
                List<string> previousQueries = searchQuery.previousSearchQueries;
                if (previousQueries != null)
                {
                    searchQueries.AddRange(previousQueries.ToArray());
                }
            }

            TextBoxQuery.AutoCompleteCustomSource = searchQueries;

        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            // Create a Stopwatch instance to measure time taken
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            string query = TextBoxQuery.Text;

            Ranker ranker = new Ranker(mergedIndex);
            List<Token> rankedDocuments = ranker.RankQuery(query);

            if (!searchQueries.Contains(query))
            {
                searchQueries.Add(query);
                new SearchQueryLocalStorage(@"..\..\..\..\Files\Databases\queries.json").AddQuery(query);
            }

            stopwatch.Start();

            new ResultsForm(rankedDocuments, query, stopwatch.Elapsed.TotalSeconds).Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBoxQuery_TextChanged(object sender, EventArgs e)
        {

        }

        private void ButtonUpload_Click(object sender, EventArgs e)
        {
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the initial directory and filter for file selection
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "Supported Files (*.pdf;*.doc;*.docx;*.ppt;*.pptx;*.xls;*.xlsx;*.txt;*.html;*.xml)|*.pdf;*.doc;*.docx;*.ppt;*.pptx;*.xls;*.xlsx;*.txt;*.html;*.xml";

            // Show the file dialog and check if the user clicked the OK button
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string selectedFilePath = openFileDialog.FileName;

                bool uploaded = UploadDocument.Upload(selectedFilePath);
                if (uploaded)
                {
                    MessageBox.Show("Started Indexing All Available Documents. Results will not contain newly uploaded document", "File uploaded successfully!");

                    // start indexing
                    bool indexed = DocumentRepresentation.IndexFiles();
                    if (indexed)
                    {
                        // update mergedIndex
                        InitializeIndexer();

                        MessageBox.Show("Finished Indexing All Documents!");
                    }
                    else
                    {
                        MessageBox.Show("Error Indexing Documents!", "Error Occured!");
                    }
                }
                else
                {
                    MessageBox.Show("Error uploading file!", "Error Occured!");
                }
            }
        }

        private void TextBoxQuery_Paint(object sender, PaintEventArgs e)
        {
            // Calculate the vertical offset to center the text
            int verticalOffset = (TextBoxQuery.Height - (int)TextBoxQuery.Font.GetHeight()) / 2;

            // Draw the text at the vertically centered position
            e.Graphics.DrawString(TextBoxQuery.Text, TextBoxQuery.Font, SystemBrushes.ControlText, new PointF(0, verticalOffset));
        }
    }
}