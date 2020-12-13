using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Draw_Shapes

{
    class IfCommand
    {
        CommandLine commands = new CommandLine();
       
        bool expression;
        String one = null;
        String two = null;
        public bool commandCheck(String line,IDictionary<String,int>store_variables)
        {
            String[] richLines = CommandLine.RichTextBoxLines;

            DataTable table = new DataTable();

            String[] signs = { "<=", ">=", "<", ">", "==", "!=" };
            String text = line.ToLower().Trim();
            String[] splitter = text.Split(' ');

            String parameters = splitter[1];

            String[] splitbysign = parameters.Split(signs, StringSplitOptions.None);

            if (store_variables.ContainsKey(splitbysign[0]) && store_variables.ContainsKey(splitbysign[1]))
            {
                one = store_variables[splitbysign[0]].ToString();
                two = store_variables[splitbysign[1]].ToString();
            }
            else if (store_variables.ContainsKey(splitbysign[0]) &&store_variables.ContainsKey(splitbysign[1]))
            {

                one = store_variables[splitbysign[0]].ToString();
                two = splitbysign[1];

            }
            else if (store_variables.ContainsKey(splitbysign[1]) && store_variables.ContainsKey(splitbysign[1]))
            {
                one = splitbysign[0];
                two = store_variables[splitbysign[1]].ToString();

            }

            else
            {
                one = splitbysign[0].ToString();
                two = splitbysign[1].ToString();
            }

            String replaced = parameters.Replace(splitbysign[0], one).Replace(splitbysign[1], two);
           
            expression = Convert.ToBoolean(table.Compute(replaced, String.Empty));
            return expression;
        }
    }
}
