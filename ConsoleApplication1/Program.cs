using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Microsoft.VisualStudio.Package;

namespace ConsoleApplication1
{
    class MainProgram
    {
        [STAThread]
        static void Main(string[] args)
        {
            string stPrompt;
            //stPrompt = getFolderPath();

            //foreach (string stFilePath in System.IO.Directory.GetFiles(stPrompt, "*.prc"))
            //{

            //}
            var query = @"
SELECT * FROM R_1_1_0_SC..AAA WHERE A = 'B' ORDER BY A

INSERT INTO R_1_1_0_SC..FFF
SELECT * FROM R_1_1_0_SC..EEE WHERE C = 'D'

            ";
            Console.Write(query);

            SqlParserDebug test = new SqlParserDebug();
            AnalyzeSQL(query);
            //test.RunDebug();

            Console.ReadLine();
            //Console.ReadKey();


        }

        static string getFolderPath()
        {
            string strFolderPath;

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "読み込むSQLが格納されているフォルダを指定してください。";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.ShowNewFolderButton = true;

            DialogResult dr = fbd.ShowDialog();

            if (dr != DialogResult.OK)
            {
                strFolderPath = "";
            } else
            {
                strFolderPath = fbd.SelectedPath;
            }

            return strFolderPath;
        }

        static void AnalyzeSQL(string query)
        {
            IList<ParseError> errors;

            var parser = new TSql110Parser(false);
            TSqlFragment parsed = parser.Parse(new StringReader(query), out errors);

            var parserStatements = parser.ParseStatementList(new StringReader(query), out errors);

            QuerySpecification QSF = new QuerySpecification();
            



            foreach (var statement in parserStatements.Statements)
            {
                IQueryable<TSqlParserToken> tempA;
                tempA = statement.ScriptTokenStream.AsQueryable();
                //QueryExpression Expression = new QueryExpression();
                




            }


            var a = parsed.StartOffset;

            var t =  parsed.ScriptTokenStream[0];
            //TableNameDeclareCheckVisitor visitor = new TableNameDeclareCheckVisitor();
            //TSqlFragmentVisitor visitor = new TSqlFragmentVisitor();
            //parsed.Accept()

            var v1 = new TSqlElementVisitor();

            Console.WriteLine("Start Console");
            parsed.Accept(v1);

            Console.WriteLine("End Console");
            //Console.WriteLine(v1.Cnt);
            //Console.WriteLine(v1.SelectList[0]);

            //parsed (var nPhrase in parsed.ScriptTokenStream)
            //{
            //    switch (nPhrase.TokenType)
            //    {
            //        case TSqlTokenType.WhiteSpace:
            //            break;
            //        case TSqlTokenType.Identifier:
            //            break;
            //        default:

            //            break;
            //    }
            //}
        }

    }

    public class TSqlElementVisitor : TSqlFragmentVisitor
    {
        public List<SelectElement> SelectList;
        public int Cnt;
        public override void ExplicitVisit(Identifier node)
        {
            Console.WriteLine("Start ExplicitVisit(Identifier)");
            Console.WriteLine("Identifier : " + node.Value);
            base.ExplicitVisit(node);
            Console.WriteLine("End ExplicitVisit(Identifier)");
        }
        public override void ExplicitVisit(SelectStarExpression node)
        {
            base.ExplicitVisit(node);
            Console.WriteLine("* ");
        }
        public override void ExplicitVisit(SelectStatement node)
        {
            base.ExplicitVisit(node);
            Console.WriteLine("SELECT ");
        }
        public override void Visit(QuerySpecification node)
        {
            Console.WriteLine("Start Visit(QuerySpecification)");
            base.Visit(node);
            foreach (var i in node.SelectElements)
            {
                Console.WriteLine("SelectElements : " + i.ToString());
            }
            Console.WriteLine("End Visit(QuerySpecification)");
        }

    }

    public class TSqlStatement
    {
        Dictionary<string, string> TSqlPhrase = new Dictionary<string, string>()
        {
            {"TokenType", "Nothing"},
            {"Key", "Nothing"},
            {"State", "Nothing"}
        };
    }


    //public class SelectStarVisitor : TSqlConcreteFragmentVisitor
    //{
    //    public int Count { get; set; }
    //    public override void Visit(SelectStarExpression node)
    //    {
    //        Count++;
    //        base.Visit(node);
    //    }
    //}
}
