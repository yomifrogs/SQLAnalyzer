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
    public class TSQLAnalyzer
    {

        public List<string[]> sqlstat = new List<string[]>();

        public void AnalyzeTSQLQuery(string query)
        {
            IList<ParseError> errors;
            IQueryable<TSqlParserToken> parserStatement;
            var parser = new TSql110Parser(false);

            TSqlFragment parsed = parser.Parse(new StringReader(query), out errors);

            var parserStatements = parser.ParseStatementList(new StringReader(query), out errors);
            parserStatement = parserStatements.ScriptTokenStream.AsQueryable();

            string keywordstat = "";
            bool isNextFlg = false;
            int statementID = 0;

            ConvertTSQLStatement(parserStatements, sqlstat, ref keywordstat, ref isNextFlg, ref statementID);

        }

        private static void ConvertTSQLStatement(StatementList parserStatements, List<string[]> sqlstat, ref string keywordstat, ref bool isNextFlg, ref int statementID)
        {
            foreach (var s in parserStatements.Statements)
            {
                int fti = s.FirstTokenIndex;
                int lti = s.LastTokenIndex;

                statementID = statementID == 0 ? 0 : statementID++;
                int keywordID = 0;

                for (int i = fti; i <= lti; i++)
                {
                    var str = s.ScriptTokenStream[i];

                    if (str.TokenType != TSqlTokenType.WhiteSpace)
                    {
                        if (str.IsKeyword())
                        {
                            if (isNextFlg)
                            {
                                keywordstat = str.Text;
                                isNextFlg = false;
                                keywordID++;
                            }
                            else
                            {
                                keywordstat = keywordstat + " " + str.Text;
                            }
                        }
                        else
                        {
                            string[] = new string[SaveType.Count]



                            sqlstat.Add(new string[] {
                                statementID.ToString(), keywordID.ToString(), keywordstat, str.Text
                            });
                            isNextFlg = true;
                        }
                    }
                }
            }
        }

    }
}
